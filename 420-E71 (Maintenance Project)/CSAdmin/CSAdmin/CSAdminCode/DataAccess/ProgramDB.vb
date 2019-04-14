' ====================================================
' Purpose: ProgramDB Data Access
' Author:  Renee Ghattas
' Date:    April 16, 2012
'
' History:
' 
' Name              Date            Description
' ====================================================
' 
' ----------------------------------------------------
' 
' ====================================================

Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.Data.SqlClient
Imports System.Configuration
Namespace DataAccess
    Public Class ProgramDB
        Private Shared _DuplicateError As Boolean



        Public Shared Property ErrorDuplicate() As Boolean
            Get
                Return _DuplicateError
            End Get
            Set(ByVal value As Boolean)
                _DuplicateError = value
            End Set
        End Property

        Public Shared Function SelectAllprograms() As ProgramList
            Dim theResult As New ProgramList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectAllPrograms", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim aprogram As New Program
                        Dim acoopprogram As New CoopProgram

                        If Not IsDBNull(rdr("programID")) Then
                            Integer.TryParse(rdr("programID").ToString, aprogram.programID)
                        Else
                            aprogram.programID = Nothing
                        End If
                        If Not IsDBNull(rdr("number")) Then
                            aprogram.Number = rdr("number").ToString()
                        Else
                            aprogram.Number = Nothing
                        End If

                        If Not IsDBNull(rdr("IsActive")) Then
                            Boolean.TryParse(rdr("IsActive").ToString, aprogram.IsActive)
                        Else
                            aprogram.IsActive = Nothing
                        End If

                        If Not IsDBNull(rdr("longName")) Then
                            aprogram.longName = rdr("longName").ToString
                        Else
                            aprogram.longName = Nothing
                        End If

                        aprogram.coopProgram = acoopprogram

                        'acoopprogram.LongName = rdr("longName")

                        If Not IsDBNull(rdr("coopprogramID")) Then
                            Integer.TryParse(rdr("coopprogramID").ToString, acoopprogram.CoopProgramID)
                        Else
                            acoopprogram.CoopProgramID = Nothing
                        End If

                        ''aprogram.coopProgram = acoopprogram

                        theResult.Add(aprogram)
                    Loop While rdr.Read()
                Else

                    Dim aprogram As New Program
                    aprogram.longName = "No data found."
                    theResult.Add(aprogram)
                    Return theResult
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return theResult
        End Function
        Public Shared Function SelectcegepPrograms() As ProgramList
            Dim theResult As New ProgramList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectCegepPrograms", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                rdr.Read()
                If rdr(0) <> "-1" Then
                    Do
                        Dim aprogram As New Program

                        aprogram.programID = rdr("IDProgramme")
                        aprogram.Number = rdr("numero")
                        'aprogram.coopProgram = rdr("longName")


                        theResult.Add(aprogram)
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
        Public Shared Sub InsertProgramVersion(ByVal ProgramID As Integer, ByVal Number As String, ByVal CoopProgramID As Integer)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertProgram", conn)
                cmd.Parameters.AddWithValue("@programNumber", Number)
                cmd.Parameters.AddWithValue("@coopProgramID", CoopProgramID)
                cmd.Parameters.AddWithValue("@programID", ProgramID)
                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)

                If returnError = -1 Then
                    ErrorDuplicate = True
                    ' cannot insert a company with the same name
                Else
                    ErrorDuplicate = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        Public Shared Sub DeleteProgramVersion(ByVal programID As Integer)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspDeleteProgramVersion", conn)
                cmd.Parameters.AddWithValue("@programID", programID)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        Public Shared Sub UpdateProgram(ByVal programID As Integer, ByVal isActive As Boolean)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("UspUpdateProgram", conn)
                cmd.Parameters.AddWithValue("@programID", programID)
                cmd.Parameters.AddWithValue("@isActive", isActive)

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
