Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections

Namespace DataAccess
    Public Class LoginDB
        Private Shared UserID As Integer

        ''' <summary>
        ''' Method that selects the admin information of the user that is logging into the system
        ''' </summary>
        ''' <param name="username">Username of the user logging in</param>
        ''' <returns>Admin object</returns>
        ''' <remarks>Author: Renee Ghattas</remarks>
        Public Shared Function SelectAdmin(ByVal Username As String) As Admin
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim ApplicationCode = ConfigurationManager.AppSettings.Get("Application")
            Dim cmd As New SqlCommand("dbo.uspLogin", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader
            Dim aAdmin As New Admin
            Try
                cmd.Parameters.AddWithValue("@Username", Username)
                cmd.Parameters.AddWithValue("@ApplicationCode", ApplicationCode)
                conn.Open()
                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        If Not rdr("Username") Is DBNull.Value Then
                            aAdmin.Username = CStr(rdr("Username"))
                        Else
                            aAdmin.Username = Nothing
                        End If
                        If Not rdr("LastName") Is DBNull.Value Then
                            aAdmin.LastName = CStr(rdr("LastName"))
                        Else
                            aAdmin.LastName = Nothing
                        End If
                        If Not rdr("FirstName") Is DBNull.Value Then
                            aAdmin.FirstName = CStr(rdr("FirstName"))
                        Else
                            aAdmin.FirstName = Nothing
                        End If
                    Loop While rdr.Read()
                Else
                    aAdmin = Nothing
                End If
                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return aAdmin
        End Function
    End Class
End Namespace