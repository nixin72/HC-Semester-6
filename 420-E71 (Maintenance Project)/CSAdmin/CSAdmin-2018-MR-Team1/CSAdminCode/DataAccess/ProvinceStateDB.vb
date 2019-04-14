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
' Alex Desbiens     MAY 09 2013     Added SelectProvinceStatesByCountry
' ====================================================
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.Data.SqlClient
Imports System.Configuration

Namespace DataAccess
    Public Class ProvinceStateDB
#Region "Select Methods"
        Public Shared Function SelectProvinceStates() As ProvinceStateList
            Dim provinceStateList As New ProvinceStateList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectProvinceStates", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim reader As SqlDataReader

            Try
                conn.Open()
                reader = cmd.ExecuteReader()
                reader.Read()

                If reader.HasRows Then
                    Do
                        Dim provinceState As New ProvinceState

                        If Not reader("IDProvinceState") Is DBNull.Value Then
                            provinceState.IDProvinceState = CInt(reader("IDProvinceState"))
                        Else
                            provinceState.IDProvinceState = Nothing
                        End If

                        If Not reader("Name") Is DBNull.Value Then
                            provinceState.Name = CStr(reader("Name"))
                        Else
                            provinceState.Name = Nothing
                        End If
                        provinceStateList.Add(provinceState)
                    Loop While reader.Read()
                End If

                reader.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return provinceStateList

        End Function

        Public Shared Function SelectProvinceStatesByCountry(ByVal IDCountry As Integer) As ProvinceStateList
            Dim provinceStateList As New ProvinceStateList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectProvinceStates", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@IDCountry", IDCountry)
            Dim reader As SqlDataReader

            Try
                conn.Open()
                reader = cmd.ExecuteReader()
                reader.Read()

                If reader.HasRows Then
                    Do
                        Dim provinceState As New ProvinceState

                        If Not reader("IDProvinceState") Is DBNull.Value Then
                            provinceState.IDProvinceState = CInt(reader("IDProvinceState"))
                        Else
                            provinceState.IDProvinceState = Nothing
                        End If

                        If Not reader("Name") Is DBNull.Value Then
                            provinceState.Name = CStr(reader("Name"))
                        Else
                            provinceState.Name = Nothing
                        End If
                        provinceStateList.Add(provinceState)
                    Loop While reader.Read()
                End If

                reader.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return provinceStateList

        End Function
#End Region

#Region "Update Methods"
        Public Shared Sub UpdateProvinceState(ByVal IDProvinceState As Integer, ByVal Name As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspUpdateProvinceState", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try
                conn.Open()
                If Not (String.IsNullOrEmpty(IDProvinceState)) Then
                    cmd.Parameters.AddWithValue("@IDProvinceState", IDProvinceState)
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
        Public Shared Sub InsertProvinceState(ByVal Name As String, ByVal Country As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertProvinceState", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try

                If Not (String.IsNullOrEmpty(CStr(Name))) Then
                    cmd.Parameters.AddWithValue("@Name", Name)
                End If

                If Not (String.IsNullOrEmpty(CStr(Country))) Then
                    cmd.Parameters.AddWithValue("@Country", Country)
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
        Public Shared Sub DeleteProvinceState(ByVal IDProvinceState As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeleteProvinceState", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try
                If Not (String.IsNullOrEmpty(CInt(IDProvinceState))) Then
                    cmd.Parameters.AddWithValue("@IDProvinceState", IDProvinceState)
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
    End Class
End Namespace
