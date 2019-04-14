Imports System.Data.SqlClient
Imports System.Configuration
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.Common

Public Class SemesterDB

    Shared Function SelectSettings() As Semester
        Dim connSQL As New SqlConnection
        connSQL.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

        Try
            connSQL.Open()
            Dim cmd As New SqlCommand("uspSelectSettings", connSQL)
            cmd.CommandType = CommandType.StoredProcedure

            'Year Ouput Parameter
            Dim YearParam As New SqlParameter("@CurrentYearSemester", SqlDbType.SmallInt)
            YearParam.Direction = ParameterDirection.Output

            'Semester Output Parameter
            Dim EndDateParam As New SqlParameter("@SemesterEndDate", SqlDbType.Date)
            EndDateParam.Direction = ParameterDirection.Output

            'Add Parameter to parameter list
            cmd.Parameters.Add(YearParam)
            cmd.Parameters.Add(EndDateParam)

            'Run Stored Procedure
            cmd.ExecuteReader()
            Dim settings As New Semester

            If Not IsDBNull(YearParam.Value) Then
                settings.YearSemester = Convert.ToInt32(YearParam.Value)
            End If

            If Not IsDBNull(EndDateParam.Value) Then
                settings.SemesterEndDate = Format.FormatDate(Convert.ToDateTime(EndDateParam.Value))
            End If

            If Not IsDBNull(YearParam.Value) Then
                settings.CurrentYear = CInt(Convert.ToString(YearParam.Value).Substring(0, 4))
            End If

            connSQL.Close()

            Return settings
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Shared Sub UpdateCurrentSemester(newYearSemester As Integer, theEndDate As Date)
        Dim conn As New SqlConnection
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

        Try
            conn.Open()
            Dim cmd As New SqlCommand("uspUpdateCurrentSemester", conn)
            cmd.Parameters.AddWithValue("@NewYearSemester", newYearSemester)
            cmd.Parameters.AddWithValue("@NewSemesterEndDate", theEndDate)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
