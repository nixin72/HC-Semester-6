' ====================================================
' Purpose: Country Data Access
' Author:  Catherine Losinger
' Date:    April 29, 2013
'
' History:
' 
' Name              Date            Description
' ====================================================
' Catherine Losinger April 26,2013   Created the Country Data Access.
' ----------------------------------------------------
' ====================================================
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.Data.SqlClient
Imports System.Configuration

Namespace DataAccess
    Public Class EducationTypeDB
#Region "Select Methods"
        Public Shared Function SelectEducationTypes() As EducationTypeList
            Dim educationTypeList As New EducationTypeList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectEducationTypes", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim reader As SqlDataReader

            Try
                conn.Open()
                reader = cmd.ExecuteReader()
                reader.Read()

                If reader.HasRows Then
                    Do
                        Dim educationType As New EducationType

                        If Not reader("IDEducationType") Is DBNull.Value Then
                            educationType.IDEducationType = CInt(reader("IDEducationType"))
                        Else
                            educationType.IDEducationType = Nothing
                        End If

                        If Not reader("Name") Is DBNull.Value Then
                            educationType.Name = CStr(reader("Name"))
                        Else
                            educationType.Name = Nothing
                        End If
                        educationTypeList.Add(educationType)
                    Loop While reader.Read()
                End If

                reader.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return educationTypeList

        End Function
#End Region

#Region "Update Methods"
        Public Shared Sub UpdateEducationType(ByVal IDEducationType As Integer, ByVal Name As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspUpdateEducationType", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try
                conn.Open()
                If Not (String.IsNullOrEmpty(IDEducationType)) Then
                    cmd.Parameters.AddWithValue("@IDEducationType", IDEducationType)
                End If

                If Not (String.IsNullOrEmpty(Name)) Then
                    cmd.Parameters.AddWithValue("@Name", Name)
                End If

                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
#End Region

#Region "Insert Methods"
        Public Shared Sub InsertEducationType(ByVal Name As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertEducationType", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try

                If Not (String.IsNullOrEmpty(CStr(Name))) Then
                    cmd.Parameters.AddWithValue("@Name", Name)
                End If

                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
#End Region

#Region "Delete Methods"
        Public Shared Function DeleteEducationType(ByVal IDEducationType As String) As Integer
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeleteEducationType", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim reader As SqlDataReader

            Try
                If Not (String.IsNullOrEmpty(CInt(IDEducationType))) Then
                    cmd.Parameters.AddWithValue("@IDEducationType", IDEducationType)
                End If

                conn.Open()
                reader = cmd.ExecuteReader()
                reader.Read()

                If reader.HasRows Then
                    Return CInt(reader(0))
                End If

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return 0
        End Function
#End Region
    End Class
End Namespace
