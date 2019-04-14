Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections

Namespace DataAccess

    Public Class RoleDB

        Public Shared Sub InsertUserRole(ByVal IDEmploye As Integer, ByVal RoleCode As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertUserRole", conn)
                cmd.Parameters.AddWithValue("@IDEmploye", IDEmploye)
                cmd.Parameters.AddWithValue("@RoleCode", RoleCode)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub

        Public Shared Sub DeleteUserRole(ByVal IDEmploye As Integer, ByVal RoleCode As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspDeleteUserRole", conn)
                cmd.Parameters.AddWithValue("@IDEmploye", IDEmploye)
                cmd.Parameters.AddWithValue("@RoleCode", RoleCode)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function SelectSpecialRoles() As RoleList

            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectSpecialRoles", conn)

            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim roleList As New RoleList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aRole As New Role

                        If Not IsDBNull(rdr("IDRole")) Then
                            Integer.TryParse(rdr("IDRole").ToString, aRole.IDRole)
                        Else
                            aRole.IDRole = Nothing
                        End If
                        If Not IsDBNull(rdr("Code")) Then
                            aRole.Code = rdr("Code").ToString
                        Else
                            aRole.Code = Nothing
                        End If
                        If Not IsDBNull(rdr("Description")) Then
                            aRole.Description = rdr("Description").ToString
                        Else
                            aRole.Description = Nothing
                        End If

                        roleList.Add(aRole)
                    Loop While rdr.Read()
                    Return roleList
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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns>returns a list of User Roles</returns>
        ''' <remarks></remarks>
        Public Shared Function SelectAllUserWithSpecialRoles(ByVal IDRoles As String) As UserRoleList

            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectAllUserWithSpecialRoles", conn)

            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim userRoleList As New UserRoleList
            Try
                conn.Open()

                cmd.Parameters.AddWithValue("@IDRoles", IDRoles)

                rdr = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim userRole As New UserRole

                        Dim aRole As New Role
                        Dim aUser As New User

                        If Not IsDBNull(rdr("IDRole")) Then
                            Integer.TryParse(rdr("IDRole").ToString, aRole.IDRole)
                        Else
                            aRole.IDRole = Nothing
                        End If
                        If Not IsDBNull(rdr("Code")) Then
                            aRole.Code = rdr("Code").ToString
                        Else
                            aRole.Code = Nothing
                        End If
                        If Not IsDBNull(rdr("Description")) Then
                            aRole.Description = rdr("Description").ToString
                        Else
                            aRole.Description = Nothing
                        End If

                        If Not IsDBNull(rdr("IDEmploye")) Then
                            Integer.TryParse(rdr("IDEmploye").ToString, userRole.IDEmploye)
                        Else
                            userRole.IDEmploye = Nothing
                        End If
                        If Not IsDBNull(rdr("IDUser")) Then
                            Integer.TryParse(rdr("IDUser").ToString, aUser.IDUser)
                        Else
                            aUser.IDUser = Nothing
                        End If
                        If Not IsDBNull(rdr("FirstName")) Then
                            aUser.FirstName = rdr("FirstName").ToString
                        Else
                            aUser.FirstName = Nothing
                        End If
                        If Not IsDBNull(rdr("LastName")) Then
                            aUser.LastName = rdr("LastName").ToString
                        Else
                            aUser.LastName = Nothing
                        End If
                        If Not IsDBNull(rdr("Username")) Then
                            aUser.Username = rdr("Username").ToString
                        Else
                            aUser.Username = Nothing
                        End If



                        userRole.Employee = aUser
                        userRole.Role = aRole

                        userRoleList.Add(userRole)
                    Loop While rdr.Read()
                    Return userRoleList
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

#Region "Application and Role Management"

        ''' <summary>
        ''' Selects all roles
        ''' </summary>
        ''' <returns>RoleList containing all roles in CSAdmin</returns>
        ''' <remarks></remarks>
        Public Shared Function selectAllRoles() As RoleList
            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectAllRoles", conn)

            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim roleList As New RoleList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aRole As New Role

                        If Not IsDBNull(rdr("IDRole")) Then
                            Integer.TryParse(rdr("IDRole").ToString, aRole.IDRole)
                        Else
                            aRole.IDRole = Nothing
                        End If
                        If Not IsDBNull(rdr("Code")) Then
                            aRole.Code = rdr("Code").ToString
                        Else
                            aRole.Code = Nothing
                        End If
                        If Not IsDBNull(rdr("Description")) Then
                            aRole.Description = rdr("Description").ToString
                        Else
                            aRole.Description = Nothing
                        End If

                        roleList.Add(aRole)
                    Loop While rdr.Read()
                    Return roleList
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

        Public Shared Function SelectRolesByApplication(ByVal IDApplication As Integer) As RoleList
            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectRolesByApplication", conn)
            cmd.Parameters.AddWithValue("@IDApplication", IDApplication)
            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim roleList As New RoleList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aRole As New Role

                        If Not IsDBNull(rdr("IDRole")) Then
                            Integer.TryParse(rdr("IDRole").ToString, aRole.IDRole)
                        Else
                            aRole.IDRole = Nothing
                        End If
                        If Not IsDBNull(rdr("Code")) Then
                            aRole.Code = rdr("Code").ToString
                        Else
                            aRole.Code = Nothing
                        End If
                        If Not IsDBNull(rdr("Description")) Then
                            aRole.Description = rdr("Description").ToString
                        Else
                            aRole.Description = Nothing
                        End If
                        If Not IsDBNull(rdr("isActive")) Then
                            If Convert.ToInt32(rdr("isActive")) = 1 Then
                                aRole.isActive = True
                            Else
                                aRole.isActive = False
                            End If
                        Else
                            aRole.isActive = False
                        End If
                        If Not IsDBNull(rdr("IDApplicationRole")) Then
                            aRole.IDApplicationRole = Convert.ToInt32(rdr("IDApplicationRole"))
                        Else
                            aRole.IDApplicationRole = Nothing
                        End If

                        roleList.Add(aRole)
                    Loop While rdr.Read()
                    Return roleList
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

        Public Shared Function SelectApplicationsByRole(ByVal IDRole As Integer) As ApplicationList
            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectApplicationsByRole", conn)
            cmd.Parameters.AddWithValue("@IDRole", IDRole)
            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim appList As New ApplicationList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim anApp As New Application

                        If Not IsDBNull(rdr("IDApplication")) Then
                            Integer.TryParse(rdr("IDApplication").ToString, anApp.IDApplication)
                        Else
                            anApp.IDApplication = Nothing
                        End If
                        If Not IsDBNull(rdr("Code")) Then
                            anApp.Code = rdr("Code").ToString
                        Else
                            anApp.Code = Nothing
                        End If
                        If Not IsDBNull(rdr("Description")) Then
                            anApp.Description = rdr("Description").ToString
                        Else
                            anApp.Description = Nothing
                        End If
                        If Not IsDBNull(rdr("isActive")) Then
                            If Convert.ToInt32(rdr("isActive")) = 1 Then
                                anApp.isActive = True
                            Else
                                anApp.isActive = False
                            End If
                        Else
                            anApp.isActive = False
                        End If

                        appList.Add(anApp)
                    Loop While rdr.Read()
                    Return appList
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

        ''' <summary>
        ''' Selects all applications
        ''' </summary>
        ''' <returns>ApplicationList containing all applications in CSAdmin</returns>
        ''' <remarks></remarks>
        Public Shared Function selectAllApplications() As ApplicationList
            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectAllApplications", conn)

            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader
            Dim applicationList As New ApplicationList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim anApplication As New Application

                        If Not IsDBNull(rdr("IDApplication")) Then
                            Integer.TryParse(rdr("IDApplication").ToString, anApplication.IDApplication)
                        Else
                            anApplication.IDApplication = Nothing
                        End If
                        If Not IsDBNull(rdr("Code")) Then
                            anApplication.Code = rdr("Code").ToString
                        Else
                            anApplication.Code = Nothing
                        End If
                        If Not IsDBNull(rdr("Description")) Then
                            anApplication.Description = rdr("Description").ToString
                        Else
                            anApplication.Description = Nothing
                        End If

                        applicationList.Add(anApplication)
                    Loop While rdr.Read()
                    Return applicationList
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

        ''' <summary>
        ''' Updates the code and/or description of an application given the ID
        ''' </summary>
        ''' <param name="IDApplication">Application to modify</param>
        ''' <param name="code">New Application Code</param>
        ''' <param name="description">New Application Description</param>
        ''' <remarks></remarks>
        Public Shared Function updateApplication(ByVal IDApplication As Integer, ByVal code As String, ByVal description As String) As Boolean


            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspUpdateApplication", conn)
                cmd.Parameters.AddWithValue("@IDApplication", IDApplication)
                cmd.Parameters.AddWithValue("@code", code)
                cmd.Parameters.AddWithValue("@description", description)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    Return False 'return false, unique key constraint violated
                Else
                    Throw ex
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Inserts a new application
        ''' </summary>
        ''' <param name="code">New Application Code</param>
        ''' <param name="description">New Application Description</param>
        ''' <returns>false if the insert did not work due to a unique key constraint</returns>
        ''' <remarks></remarks>
        Public Shared Function addApplication(ByVal code As String, ByVal description As String) As Boolean


            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspInsertApplication", conn)
                cmd.Parameters.AddWithValue("@code", code)
                cmd.Parameters.AddWithValue("@description", description)


                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    Return False 'return false, unique key constraint violated
                Else
                    Throw ex
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Updates the code and/or description of an role given the ID
        ''' </summary>
        ''' <param name="IDRole">Role to modify</param>
        ''' <param name="code">New Application Code</param>
        ''' <param name="description">New Application Description</param>
        ''' <remarks></remarks>
        Public Shared Function updateRole(ByVal IDRole As Integer, ByVal code As String, ByVal description As String) As Boolean


            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspUpdateRole", conn)
                cmd.Parameters.AddWithValue("@IDRole", IDRole)
                cmd.Parameters.AddWithValue("@code", code)
                cmd.Parameters.AddWithValue("@description", description)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    Return False 'return false, unique key constraint violated
                Else
                    Throw ex
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Inserts a new role
        ''' </summary>
        ''' <param name="code">New Role Code</param>
        ''' <param name="description">New Role Description</param>
        ''' <remarks></remarks>
        Public Shared Function addRole(ByVal code As String, ByVal description As String) As Boolean


            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspInsertRole", conn)
                cmd.Parameters.AddWithValue("@code", code)
                cmd.Parameters.AddWithValue("@description", description)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    Return False 'return false, unique key constraint violated
                Else
                    Throw ex
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return True
        End Function

        ''' <summary>
        ''' Selects all application roles for the given IDRole
        ''' </summary>
        ''' <param name="IDRole">The ID of the role for which to select associated applications</param>
        ''' <returns>an ApplicationRoleList object containing all the applications for the given IDRole</returns>
        ''' <remarks>Modified to also use IDApplication</remarks>
        Public Shared Function selectAllApplicationRoles(ByVal IDRole As Integer) As ApplicationRoleList
            Dim conn As New SqlConnection

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectApplicationRoles", conn)

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@IDRole", IDRole)
            Dim rdr As SqlDataReader
            Dim applicationRoleList As New ApplicationRoleList
            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim anApplicationRole As New ApplicationRole
                        Dim anApplication As New Application
                        Dim aRole As New Role

                        If Not IsDBNull(rdr("IDApplicationRole")) Then
                            Integer.TryParse(rdr("IDApplicationRole").ToString, anApplicationRole.IDApplicationRole)
                        Else
                            anApplicationRole.IDApplicationRole = Nothing
                        End If

                        If Not IsDBNull(rdr("IDRole")) Then
                            Integer.TryParse(rdr("IDRole").ToString, aRole.IDRole)
                        Else
                            aRole.IDRole = Nothing
                        End If

                        If Not IsDBNull(rdr("IDApplication")) Then
                            Integer.TryParse(rdr("IDApplication").ToString, anApplication.IDApplication)
                        Else
                            anApplication.IDApplication = Nothing
                        End If

                        If Not IsDBNull(rdr("AppDescription")) Then
                            anApplication.Description = rdr("AppDescription").ToString
                        Else
                            anApplication.Description = Nothing
                        End If

                        If Not IsDBNull(rdr("isActive")) Then
                            anApplicationRole.isActive = rdr("isActive")
                        Else
                            anApplicationRole.isActive = False
                        End If


                        anApplicationRole.Application = anApplication
                        anApplicationRole.Role = aRole

                        applicationRoleList.Add(anApplicationRole)
                    Loop While rdr.Read()
                    Return applicationRoleList
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

        ''' <summary>
        ''' Updates the isActive state of an application role given the ID
        ''' </summary>
        ''' <param name="IDApplicationRole">Application Role to modify</param>
        ''' <param name="isActive">Whether the role is active or not</param>
        ''' <remarks></remarks>
        Public Shared Sub updateApplicationRole(ByVal IDApplicationRole As Integer, ByVal isActive As Boolean)


            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspUpdateApplicationRole", conn)
                cmd.Parameters.AddWithValue("@IDApplicationRole", IDApplicationRole)
                If isActive Then
                    cmd.Parameters.AddWithValue("@isActive", 1)
                Else
                    cmd.Parameters.AddWithValue("@isActive", 0)
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
        '''Inserts an application role for the selected role and application
        ''' </summary>
        ''' <param name="IDApplication">The Selected application</param>
        ''' <param name="IDRole">The Selected role</param>
        ''' <remarks></remarks>
        Public Shared Sub addApplicationRole(ByVal IDApplication As Integer, ByVal IDRole As Integer)


            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspInsertApplicationRole", conn)
                cmd.Parameters.AddWithValue("@IDApplication", IDApplication)
                cmd.Parameters.AddWithValue("@IDRole", IDRole)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Public Shared Sub deleteApplicationRole(ByVal IDApplicationRole As Integer)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteApplicationRole", conn)
                cmd.Parameters.AddWithValue("@IDApplicationRole", IDApplicationRole)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Public Shared Sub deleteApplication(ByVal IDApplication As Integer)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteApplication", conn)
                cmd.Parameters.AddWithValue("@IDApplication", IDApplication)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
#End Region

        Public Shared Sub deleteRole(ByVal IDRole As Integer)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteRole", conn)
                cmd.Parameters.AddWithValue("@IDRole", IDRole)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
    End Class
End Namespace

