Imports CSAdminCode.BusinessObjects.Collections
Imports System.Data.SqlClient
Imports System.Configuration
Imports CSAdminCode.BusinessObjects

' ====================================================
' Purpose: JobPostingDB Data Access
' Author:  Mikael-Raymond Paul
' Date:    March 29, 2011
'
' History:
' 
' Name              Date            Description
' ====================================================
' 
' ----------------------------------------------------
' 

Namespace DataAccess
    Public Class ProgramCoopCoordinatorDB

        ''' <summary>
        ''' Retrieves all program coop coordinator combinations from database
        ''' </summary>
        ''' <returns>List of Program Coop Coordinators</returns>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        Public Shared Function SelectAllProgramCoopCoordinators(ByVal SortExpression As String) As ProgramCoopCoordinatorList
            Dim conn As New SqlConnection
            Dim _isDescending As Boolean = False

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectAllProgramCoopCoordinator", conn)

            'Verify if SortExpression is Ascending or Descending
            If SortExpression.EndsWith("DESC") Then
                SortExpression = SortExpression.Replace("DESC", "").Trim
                _isDescending = True
            End If

            'Check to see if SortExpression has a value or not; add parameter accordingly
            If SortExpression <> String.Empty Then
                cmd.Parameters.AddWithValue("@SortExpression", SortExpression)
            Else
                cmd.Parameters.AddWithValue("@SortExpression", DBNull.Value)
            End If
            'Check to see if SortExpression is Descending or Ascending; add parameter accordingly
            If _isDescending Then
                cmd.Parameters.AddWithValue("@SortDirection", 1)
            Else
                cmd.Parameters.AddWithValue("@SortDirection", 0)
            End If

            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim pccList As New ProgramCoopCoordinatorList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()

                rdr.Read()

                If rdr.HasRows Then

                    Do
                        Dim pcc As New ProgramCoopCoordinator
                        Dim cc As New CoopCoordinator

                        If Not IsDBNull(rdr("ProgramCoopCoordinatorID")) Then
                            Integer.TryParse(rdr("ProgramCoopCoordinatorID").ToString, pcc.ProgramCoopCoordinatorID)
                        Else
                            pcc.ProgramCoopCoordinatorID = Nothing
                        End If

                        ' adds coordinator info to the coop coordinator object
                        If Not IsDBNull(rdr("CoopCoordinatorID")) Then
                            Integer.TryParse(rdr("CoopCoordinatorID").ToString, cc.CoopCoordinatorID)
                        Else
                            cc.CoopCoordinatorID = Nothing
                        End If

                        If Not IsDBNull(rdr("FirstName")) Then
                            cc.FirstName = rdr("FirstName").ToString
                        Else
                            cc.FirstName = Nothing
                        End If

                        If Not IsDBNull(rdr("LastName")) Then
                            cc.LastName = rdr("LastName").ToString
                        Else
                            cc.LastName = Nothing
                        End If

                        ' adds coop program info to the coop program 
                        Dim cp As New CoopProgram

                        If Not IsDBNull(rdr("CoopProgramID")) Then
                            Integer.TryParse(rdr("CoopProgramID").ToString, cp.CoopProgramID)
                        Else
                            cp.CoopProgramID = Nothing
                        End If

                        If Not IsDBNull(rdr("ShortName")) Then
                            cp.ShortName = rdr("ShortName").ToString
                        Else
                            cp.ShortName = Nothing
                        End If

                        If Not IsDBNull(rdr("LongName")) Then
                            cp.LongName = rdr("LongName").ToString
                        Else
                            cp.LongName = Nothing
                        End If

                        pcc.CoopCoordinator = cc
                        pcc.CoopProgram = cp

                        pccList.Add(pcc)
                    Loop While rdr.Read()
                Else
                    Return Nothing
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return pccList
        End Function

        ''' <summary>
        ''' Deletes a specified program coop coordinator combination from the database
        ''' </summary>
        ''' <param name="ProgramCoopCoordinatorID">ID of Program Coop Coordinator to delete</param>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        Public Shared Function DeleteProgramCoordinator(ByVal ProgramCoopCoordinatorID As Integer) As Integer
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspDeleteProgramCoopCoordinator", conn)

                'initializes the output paramater
                Dim rowcount As New SqlParameter("@RowCountOutput", SqlDbType.Int)
                rowcount.Direction = ParameterDirection.Output

                'sets the input paramater
                cmd.Parameters.AddWithValue("@ProgramCoopCoordinatorID", ProgramCoopCoordinatorID)
                'cmd.Parameters.AddWithValue("@CoopProgramID", CoopProgramID)
                cmd.Parameters.Add(rowcount)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                'getting the value from the stored procedure
                Dim theResult As Integer = Integer.Parse(cmd.Parameters("@RowCountOutput").Value.ToString())

                Return theResult
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Function

        ''' <summary>
        ''' Inserts a program coop coordinator combination into the database with the specified parameters
        ''' </summary>
        ''' <param name="CoopCoordinatorID">ID of Coop Coordinator to insert</param>
        ''' <param name="CoopProgramID">ID of Coop Program to insert</param>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        Public Shared Function InsertProgramCoordinator(ByVal CoopCoordinatorID As Integer, ByVal CoopProgramID As Integer) As Integer
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim ProgramCoopCoordinatorID As Integer
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertProgramCoopCoordinator", conn)
                Dim PCCID As New SqlParameter("@ProgramCoopCoordinatorID", SqlDbType.Int)
                PCCID.Direction = ParameterDirection.Output
                cmd.Parameters.AddWithValue("@CoopCoordinatorID", CoopCoordinatorID)
                cmd.Parameters.AddWithValue("@CoopProgramID", CoopProgramID)
                cmd.Parameters.Add(PCCID)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
                Dim theResult As Integer = Integer.Parse(cmd.Parameters("@ProgramCoopCoordinatorID").Value.ToString())
                If Not theResult = Nothing Then
                    ProgramCoopCoordinatorID = theResult
                Else
                    ProgramCoopCoordinatorID = Nothing
                End If
                Return ProgramCoopCoordinatorID
            Catch ex As Exception
                Throw ex
            Finally

                conn.Close()
            End Try
            Return ProgramCoopCoordinatorID
        End Function

    End Class
End Namespace

