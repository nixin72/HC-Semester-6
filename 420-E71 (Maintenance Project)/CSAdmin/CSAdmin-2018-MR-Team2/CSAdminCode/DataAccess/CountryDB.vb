' ====================================================
' Purpose: Country Data Access
' Author:  Catherine Losinger
' Date:    April 26, 2013
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
    Public Class CountryDB

#Region "Select Methods"
        Public Shared Function SelectCountries() As CountryList
            Dim countryList As New CountryList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectCountries", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim reader As SqlDataReader

            Try
                conn.Open()
                reader = cmd.ExecuteReader()
                reader.Read()

                If reader.HasRows Then
                    Do
                        Dim country As New Country

                        If Not reader("IDCountry") Is DBNull.Value Then
                            Country.IDCountry = CInt(reader("IDCountry"))
                        Else
                            Country.IDCountry = Nothing
                        End If

                        If Not reader("Name") Is DBNull.Value Then
                            Country.Name = CStr(reader("Name"))
                        Else
                            Country.Name = Nothing
                        End If
                        countryList.Add(Country)
                    Loop While reader.Read()
                End If

                reader.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return countryList

        End Function
#End Region

#Region "Update Methods"
        Public Shared Sub UpdateCountry(ByVal IDCountry As Integer, ByVal Name As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspUpdateCountry", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Try
                conn.Open()
                If Not (String.IsNullOrEmpty(IDCountry)) Then
                    cmd.Parameters.AddWithValue("@IDCountry", IDCountry)
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
        Public Shared Sub InsertCountry(ByVal Name As String)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertCountry", conn)
            cmd.CommandType = CommandType.StoredProcedure


                If Not (String.IsNullOrEmpty(CStr(Name))) Then
                    cmd.Parameters.AddWithValue("@Name", Name)
                End If

                conn.Open()
                cmd.ExecuteNonQuery()

                conn.Close()

        End Sub
#End Region

#Region "Delete Methods"
        Public Shared Function DeleteCountry(ByVal IDCountry As String) As Integer
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeleteCountry", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim reader As SqlDataReader

            Try
                If Not (String.IsNullOrEmpty(CInt(IDCountry))) Then
                    cmd.Parameters.AddWithValue("@IDCountry", IDCountry)
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
