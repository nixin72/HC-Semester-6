' ====================================================
' Purpose: CoopCoordinatorDB Data Access
' Author:  Cedric Burgins
' Date:    March 29, 2011
'
' History:
' 
' Name              Date            Description
' ====================================================
' 
' ----------------------------------------------------
' 
' ====================================================

Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections

Namespace DataAccess
    Public Class CoopCoordinatorDB
        Private Shared _DeleteError As Boolean
        Private Shared _DuplicateNameError As Boolean

        Public Shared Property CoopCoordinatorDeleteError() As Boolean
            Get
                Return _DeleteError
            End Get
            Set(ByVal value As Boolean)
                _DeleteError = value
            End Set
        End Property

        Public Shared Property ErrorDuplicateName() As Boolean
            Get
                Return _DuplicateNameError
            End Get
            Set(ByVal value As Boolean)
                _DuplicateNameError = value
            End Set
        End Property



        ''' <summary>
        ''' This subroutine updates or inserts a co-op coordinator in the CoopCoordinator table in the database
        ''' </summary>
        ''' <param name="aCoopCoordinator">A CoopCoordinator object that holds the CoopCoordinator properties</param>
        ''' <remarks></remarks>
        Public Shared Sub UpdateCoopCoordinator(ByVal aCoopCoordinator As CoopCoordinator)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspUpdateCoopCoordinator", conn)
                cmd.Parameters.AddWithValue("@CoopCoordinatorID", aCoopCoordinator.CoopCoordinatorID)
                cmd.Parameters.AddWithValue("@LastName", aCoopCoordinator.LastName)
                cmd.Parameters.AddWithValue("@FirstName", aCoopCoordinator.FirstName)
                If aCoopCoordinator.Email IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@Email", aCoopCoordinator.Email)
                Else
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value)
                End If

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        ''' <summary>
        ''' This subroutine updates or inserts a co-op coordinator in the CoopCoordinator table in the database
        ''' </summary>
        ''' <param name="aCoopCoordinator">A CoopCoordinator object that holds the CoopCoordinator properties</param>
        ''' <remarks></remarks>
        Public Shared Sub InsertCoopCoordinator(ByVal aCoopCoordinator As CoopCoordinator)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0

            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspInsertCoopCoordinator", conn)
                cmd.Parameters.AddWithValue("@EmployeeID", aCoopCoordinator.EmployeeID)
                cmd.Parameters.AddWithValue("@LastName", aCoopCoordinator.LastName)
                cmd.Parameters.AddWithValue("@FirstName", aCoopCoordinator.FirstName)

                If aCoopCoordinator.Email IsNot Nothing Then
                    cmd.Parameters.AddWithValue("@Email", aCoopCoordinator.Email)
                Else
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value)
                End If

                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)

                If returnError = -1 Then
                    ErrorDuplicateName = True
                    ' cannot insert a company with the same name
                Else
                    ErrorDuplicateName = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub


        ''' <summary>
        ''' A function that gets a list of all co-op coordinators
        ''' </summary>
        ''' <returns>A list of co-op coordinators</returns>
        ''' <remarks></remarks>
        Public Shared Function SelectAllCoopCoordinators() As CoopCoordinatorList
            Dim ccList As New CoopCoordinatorList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectAllCoopCoordinators", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                rdr.Read()
                If rdr(0) <> "-1" Then
                    Do
                        Dim aCoopCoordinator As New CoopCoordinator
                        aCoopCoordinator.CoopCoordinatorID = CInt(rdr("CoopCoordinatorID"))
                        aCoopCoordinator.EmployeeID = CInt(rdr("EmployeeID"))
                        aCoopCoordinator.LastName = rdr("LastName")
                        aCoopCoordinator.FirstName = rdr("FirstName")
                        If Not rdr("Email") Is DBNull.Value Then
                            aCoopCoordinator.Email = rdr("Email")
                        End If
                        ccList.Add(aCoopCoordinator)
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
            Return ccList
        End Function

        ''' <summary>
        ''' A function that gets a list of all co-op coordinators
        ''' </summary>
        ''' <returns>A list of co-op coordinators</returns>
        ''' <remarks></remarks>
        Public Shared Function SelectCoopCoordinator(ByVal ProgramCoopCoordinatorID As Integer) As CoopCoordinator
            Dim ccList As New CoopCoordinator
            Dim conn As New SqlConnection()
            Dim aCoopCoordinator As New CoopCoordinator
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectCoopCoordinatorByProgramCoopCoordinatorID", conn)
            cmd.Parameters.AddWithValue("@ProgramCoopCoordinatorID", ProgramCoopCoordinatorID)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        'Dim aCoopCoordinator As New CoopCoordinator
                        aCoopCoordinator.CoopCoordinatorID = CInt(rdr("CoopCoordinatorID"))
                        aCoopCoordinator.EmployeeID = CInt(rdr("EmployeeID"))
                        aCoopCoordinator.LastName = rdr("LastName")
                        aCoopCoordinator.FirstName = rdr("FirstName")
                        If Not rdr("Email") Is DBNull.Value Then
                            aCoopCoordinator.Email = rdr("Email")
                        End If
                        'ccList.Add(aCoopCoordinator)
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
            Return aCoopCoordinator
        End Function

        ''' <summary>
        ''' A function that gets a list of all co-op coordinators
        ''' </summary>
        ''' <returns>A list of co-op coordinators</returns>
        ''' <remarks></remarks>
        Public Shared Function SelectCoopCoordinatorByEmployeeID(ByVal EmployeeID As Integer) As CoopCoordinator
            Dim ccList As New CoopCoordinatorList
            Dim conn As New SqlConnection()
            Dim aCoopCoordinator As New CoopCoordinator
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectCoopCoordinatorByEmployeeID", conn)
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        'Dim aCoopCoordinator As New CoopCoordinator
                        aCoopCoordinator.CoopCoordinatorID = CInt(rdr("CoopCoordinatorID"))
                        'aCoopCoordinator.EmployeeID = CInt(rdr("EmployeeID"))
                        'aCoopCoordinator.LastName = rdr("LastName")
                        'aCoopCoordinator.FirstName = rdr("FirstName")
                        'If Not rdr("Email") Is DBNull.Value Then
                        '    aCoopCoordinator.Email = rdr("Email")
                        'End If
                        ccList.Add(aCoopCoordinator)
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
            Return aCoopCoordinator
        End Function


        ''' <summary>
        ''' A function that gets an employee of Heritage College from the Clara database
        ''' </summary>
        ''' <returns>A Heritage College Employee</returns>
        ''' <remarks></remarks>
        Public Shared Function SelectClaraEmployeeByID(ByVal EmployeeID As Integer) As User
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectClaraEmployeeByID", conn)
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID.ToString)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                If rdr.Read() Then
                    Dim aClaraEmployee As New User
                    aClaraEmployee.IDUser = CInt(rdr("IDEmploye"))
                    aClaraEmployee.LastName = rdr("Nom")
                    aClaraEmployee.FirstName = rdr("Prenom")
                    Return aClaraEmployee
                Else
                    Return Nothing
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Function

    End Class
End Namespace
