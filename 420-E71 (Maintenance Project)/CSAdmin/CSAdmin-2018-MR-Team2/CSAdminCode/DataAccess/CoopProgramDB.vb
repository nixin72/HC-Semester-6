Imports System.Data.SqlClient
Imports CSAdminCode.BusinessObjects
Imports System.Configuration
Imports CSAdminCode.BusinessObjects.Collections

Namespace DataAccess
    Public Class CoopProgramDB

        Private Shared _DeleteError As Boolean

        ''' <summary>
        ''' Retrieves CoopPrograms related to the given CoopCoordinatorID
        ''' </summary>
        ''' <param name="CoopCoordinatorID">The CoopCoordinatorID is passed in as an Integer to recieve a list of CoopPrograms</param>
        ''' <returns>List of CoopPrograms</returns>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        Public Shared Function SelectProgram(ByVal CoopCoordinatorID As Integer) As CoopProgramList
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectCoopProgram", conn)
            cmd.Parameters.AddWithValue("@CoopCoordinatorID", CoopCoordinatorID)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader
            Dim coopProgramList As New CoopProgramList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim CoopProgram As New CoopProgram
                        CoopProgram.CoopProgramID = rdr("CoopProgramID").ToString
                        CoopProgram.LongName = rdr("LongName").ToString
                        CoopProgram.ShortName = rdr("ShortName").ToString
                        coopProgramList.Add(CoopProgram)
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
            Return coopProgramList
        End Function

        Public Shared Function SelectProgramByID(ByVal CoopProgramID As Integer) As CoopProgram
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectCoopProgramByCoopProgramID", conn)
            cmd.Parameters.AddWithValue("@CoopProgramID", CoopProgramID)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader
            Dim selectedProgram As New CoopProgram
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        selectedProgram.CoopProgramID = rdr("CoopProgramID").ToString
                        selectedProgram.LongName = rdr("LongName").ToString
                        selectedProgram.ShortName = rdr("ShortName").ToString
                        'If Not rdr("Active") Is DBNull.Value Then
                        '    selectedProgram.isActive = Nothing
                        'Else
                        '    selectedProgram.isActive = rdr("isActive").ToString
                        'End If

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
            Return selectedProgram
        End Function

        ''' <summary>
        ''' Method that returns all coop programs
        ''' </summary>
        ''' <returns>CoopProgramList with all coop programs</returns>
        ''' <remarks>Author: Kevin Brascoupe</remarks>
        Public Shared Function SelectAllPrograms() As CoopProgramList
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectAllPrograms", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader
            Dim coopProgramList As New CoopProgramList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim CoopProgram As New CoopProgram
                        CoopProgram.CoopProgramID = Integer.Parse(rdr("CoopProgramID").ToString)
                        If Not (IsDBNull(rdr("ProgramID"))) Then
                            CoopProgram.ProgramID = rdr("ProgramID")
                        End If

                        CoopProgram.LongName = rdr("LongName").ToString
                        CoopProgram.ShortName = rdr("ShortName").ToString
                        coopProgramList.Add(CoopProgram)
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
            Return coopProgramList
        End Function

        ''' <summary>
        ''' Method that returns all coop programs that have an associated program version
        ''' </summary>
        ''' <returns>CoopProgramList with all coop programs with an associated program version</returns>
        ''' <remarks>Author: Louis Cloutier</remarks>
        Public Shared Function SelectAllProgramsHavingProgramID() As CoopProgramList
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectAllProgramsHavingProgramID", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader
            Dim coopProgramList As New CoopProgramList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim CoopProgram As New CoopProgram
                        CoopProgram.CoopProgramID = Integer.Parse(rdr("CoopProgramID").ToString)
                        CoopProgram.ProgramID = rdr("ProgramID")
                        CoopProgram.LongName = rdr("LongName").ToString
                        CoopProgram.ShortName = rdr("ShortName").ToString
                        CoopProgram.Number = rdr("Number").ToString
                        coopProgramList.Add(CoopProgram)
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
            Return coopProgramList
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks>Author: Marta Chmielowska</remarks>
        Public Shared Function InsertCoopProgram(ByVal ShortName As String, ByVal LongName As String, ByVal isActive As Boolean) As Integer
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim rdr As SqlDataReader
            Dim ProgramCoopID As Integer
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertCoopProgram", conn)
                cmd.Parameters.AddWithValue("@LongName", LongName)
                cmd.Parameters.AddWithValue("@ShortName", ShortName)
                cmd.Parameters.AddWithValue("@isActive", isActive)

                cmd.CommandType = CommandType.StoredProcedure
                rdr = cmd.ExecuteReader()
                rdr.Read()

                If rdr.HasRows Then
                    Do
                        If Not IsDBNull(rdr("CoopProgramID")) Then
                            ProgramCoopID = CType((rdr("CoopProgramID").ToString), Integer)
                        Else
                            ProgramCoopID = 0
                        End If
                    Loop While rdr.Read()
                End If


            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return ProgramCoopID
        End Function

        Public Shared Sub UpdateCoopProgram(ByVal CoopProgramID As Integer, ByVal ShortName As String, ByVal LongName As String, ByVal isActive As Boolean)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspUpdateCoopProgram", conn)
                cmd.Parameters.AddWithValue("@CoopProgramID", CoopProgramID)
                cmd.Parameters.AddWithValue("@LongName", LongName)
                cmd.Parameters.AddWithValue("@ShortName", ShortName)
                cmd.Parameters.AddWithValue("@isActive", isActive)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Property CoopProgramErrorDelete() As Boolean
            Get
                Return _DeleteError
            End Get
            Set(ByVal value As Boolean)
                _DeleteError = value
            End Set
        End Property

        Public Shared Sub DeleteCoopProgram(ByVal aCoopProgram As CoopProgram)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim returnError As Integer = 0

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspDeleteCoopProgram", conn)
                cmd.Parameters.AddWithValue("@CoopProgramID", aCoopProgram.CoopProgramID)

                Dim param As New SqlParameter("@ReturnValue", SqlDbType.Int)

                param.Direction = ParameterDirection.Output
                cmd.Parameters.Add(param)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)

                If returnError = -1 Then
                    CoopProgramErrorDelete = True
                    ' Cannot delete the co-op program because it has StudentConfirmations associated with it
                Else
                    CoopProgramErrorDelete = False
                End If

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

    End Class
End Namespace

