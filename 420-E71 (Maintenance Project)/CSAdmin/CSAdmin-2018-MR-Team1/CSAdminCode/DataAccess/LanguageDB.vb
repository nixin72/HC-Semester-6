Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.Data.SqlClient
Imports System.Configuration

Namespace DataAccess
    Public Class LanguageDB

        Private Shared _DeleteError As Boolean
        Private Shared _DuplicateNameError As Boolean

        Public Shared Property LanguageErrorDelete() As Boolean
            Get
                Return _DeleteError
            End Get
            Set(ByVal value As Boolean)
                _DeleteError = value
            End Set
        End Property

        Public Shared Property LanguageErrorDuplicateName() As Boolean
            Get
                Return _DuplicateNameError
            End Get
            Set(ByVal value As Boolean)
                _DuplicateNameError = value
            End Set
        End Property

        Public Shared Function SelectDefaultLanguage() As LanguageList
            Dim theResult As New LanguageList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectDefaultLanguage", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim aLanguage As New Language
                        If Not IsDBNull(rdr("LanguageID")) Then
                            aLanguage.LanguageID = rdr("LanguageID")
                        Else
                            aLanguage.LanguageID = Nothing
                        End If
                        If Not IsDBNull(rdr("Language")) Then
                            aLanguage.Language = rdr("Language")
                        Else
                            aLanguage.Language = Nothing
                        End If
                        theResult.Add(aLanguage)
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
            Return theResult
        End Function
        Public Shared Sub UpdateLanguage(ByVal LanguageID As Integer, ByVal Language As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspUpdateLanguage", conn)
                cmd.Parameters.AddWithValue("@LanguageID", LanguageID)
                cmd.Parameters.AddWithValue("@Language", Language)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Shared Sub SetDefaultLanguage(ByVal LanguageID As Integer)

            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("dbo.uspSetDefaultLanguage", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.Parameters.AddWithValue("@LanguageID", LanguageID)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
      

        Public Shared Function SelectAllLanguages() As LanguageList
            Dim theResult As New LanguageList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectAllLanguages", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim aLanguage As New Language
                        If Not IsDBNull(rdr("LanguageID")) Then
                            aLanguage.LanguageID = rdr("LanguageID")
                        Else
                            aLanguage.LanguageID = Nothing
                        End If
                        If Not IsDBNull(rdr("Language")) Then
                            aLanguage.Language = rdr("Language")
                        Else
                            aLanguage.Language = Nothing
                        End If
                        theResult.Add(aLanguage)
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
            Return theResult
        End Function

        Public Shared Sub InsertLanguage(ByVal Language As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertLanguage", conn)
                cmd.Parameters.AddWithValue("@Language", Language)

                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)
                ' newID = Integer.Parse(cmd.Parameters("@newID").Value)

                '   NewItemInserted = newID

                If returnError = -1 Then
                    LanguageErrorDuplicateName = True
                    ' cannot insert a company with the same name
                Else
                    LanguageErrorDuplicateName = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub

       
        Public Shared Sub DeleteLanguage(ByVal LanguageID As Integer)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeleteLanguage", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try
                If Not (String.IsNullOrEmpty(CInt(LanguageID))) Then
                    cmd.Parameters.AddWithValue("@LanguageID", LanguageID)
                End If

                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
    End Class
End Namespace


