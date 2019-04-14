Imports System.Configuration
'Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections

Namespace DataAccess
    Public Class StatusDB

        Private Shared _DeleteError As Boolean
        Private Shared _DuplicateNameError As Boolean

        Public Shared Property StatusErrorDelete() As Boolean
            Get
                Return _DeleteError
            End Get
            Set(ByVal value As Boolean)
                _DeleteError = value
            End Set
        End Property

        Public Shared Property StatusErrorDuplicateName() As Boolean
            Get
                Return _DuplicateNameError
            End Get
            Set(ByVal value As Boolean)
                _DuplicateNameError = value
            End Set
        End Property


        ''' <summary>
        ''' Retrieves all the status types
        ''' </summary>
        ''' <returns>List of Status Types</returns>
        ''' <remarks>Created by Matthew Riopel</remarks>
        Public Shared Function SelectStatusTypes() As StatusList

            Dim statusList As New StatusList
            Dim conn As New SqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectAllStatusTypes", conn)
            'cmd.Parameters.AddWithValue("@employeeID", IDEmploye)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aStatus As New Status

                        If Not IsDBNull(rdr("IDStatus")) Then
                            Integer.TryParse(rdr("IDStatus").ToString, aStatus.IDStatus)
                        Else
                            aStatus.IDStatus = Nothing
                        End If

                        If Not IsDBNull(rdr("Description")) Then
                            aStatus.StatusName = rdr("Description").ToString
                        Else
                            aStatus.IDStatus = Nothing
                        End If

                        'adds each status to the StatusList object
                        statusList.Add(aStatus)
                    Loop While rdr.Read
                Else
                    statusList = Nothing
                End If

                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            'returns the list of semesters
            Return statusList
        End Function

#Region " Confirmation Status "

        Public Shared Function SelectAllConfirmationStatus() As StatusList
            Dim ConfirmationStatusList As New StatusList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectAllConfirmationStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aStatus As New Status
                        If Not IsDBNull(rdr("ConfirmationStatusID")) Then
                            Integer.TryParse(rdr("ConfirmationStatusID").ToString, aStatus.IDStatus)
                        Else
                            aStatus.IDStatus = Nothing
                        End If
                        If Not IsDBNull(rdr("Name")) Then
                            aStatus.StatusName = rdr("Name").ToString
                        Else
                            aStatus.IDStatus = Nothing
                        End If
                        ConfirmationStatusList.Add(aStatus)
                    Loop While rdr.Read
                Else
                    ConfirmationStatusList = Nothing
                End If

                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return ConfirmationStatusList
        End Function

        Public Shared Sub DeleteConfirmationStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteConfirmationStatus", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)

                Dim param As New SqlParameter("@ReturnVal", SqlDbType.Int)

                param.Direction = ParameterDirection.Output
                cmd.Parameters.Add(param)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                'Cannot delete an employer with job postings associated with them
                returnError = Integer.Parse(cmd.Parameters("@ReturnVal").Value)
                If returnError = -1 Then
                    StatusErrorDelete = True
                Else
                    StatusErrorDelete = False
                End If

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Public Shared Sub InsertConfirmationStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertConfirmationStatus", conn)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)
                ' newID = Integer.Parse(cmd.Parameters("@newID").Value)

                '   NewItemInserted = newID

                If returnError = -1 Then
                    StatusErrorDuplicateName = True
                    ' cannot insert a company with the same name
                Else
                    StatusErrorDuplicateName = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub

        Public Shared Sub UpdateConfirmationStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspUpdateConfirmationsStatus", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

#End Region

#Region " Eligibility Status "

        Public Shared Function SelectAllEligibilityStatus() As StatusList
            Dim EligibilityStatusList As New StatusList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectAllEligibilityStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aEligibilityStatus As New Status

                        If Not IsDBNull(rdr("EligibilityStatusID")) Then
                            Integer.TryParse(rdr("EligibilityStatusID"), aEligibilityStatus.IDStatus)
                        Else
                            aEligibilityStatus.IDStatus = Nothing
                        End If

                        If Not IsDBNull(rdr("Name")) Then
                            aEligibilityStatus.StatusName = rdr("Name").ToString
                        Else
                            aEligibilityStatus.StatusName = Nothing
                        End If

                        EligibilityStatusList.Add(aEligibilityStatus)
                    Loop While rdr.Read
                Else
                    EligibilityStatusList = Nothing
                End If
                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return EligibilityStatusList
        End Function


        Public Shared Sub DeleteEligibilityStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteEligibilityStatus", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)

                Dim param As New SqlParameter("@ReturnVal", SqlDbType.Int)

                param.Direction = ParameterDirection.Output
                cmd.Parameters.Add(param)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                'Cannot delete an employer with job postings associated with them
                returnError = Integer.Parse(cmd.Parameters("@ReturnVal").Value)
                If returnError = -1 Then
                    StatusErrorDelete = True
                Else
                    StatusErrorDelete = False
                End If

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub


        Public Shared Sub InsertEligibilityStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertEligibilityStatus", conn)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)
                ' newID = Integer.Parse(cmd.Parameters("@newID").Value)

                '   NewItemInserted = newID

                If returnError = -1 Then
                    StatusErrorDuplicateName = True
                    ' cannot insert a company with the same name
                Else
                    StatusErrorDuplicateName = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub


        Public Shared Sub UpdateEligibilityStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspUpdateEligibilityStatus", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

#End Region

#Region " Job Application Status "

        Public Shared Function SelectAllJobApplicationStatus() As StatusList
            Dim JobApplicationStatusList As New StatusList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectJobApplicationStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aJobApplicationStatus As New Status

                        If Not IsDBNull(rdr("JobApplicationStatusID")) Then
                            Integer.TryParse(rdr("JobApplicationStatusID").ToString, aJobApplicationStatus.IDStatus)
                        Else
                            aJobApplicationStatus.IDStatus = Nothing
                        End If

                        If Not IsDBNull(rdr("Name")) Then
                            aJobApplicationStatus.StatusName = rdr("Name").ToString
                        Else
                            aJobApplicationStatus.IDStatus = Nothing
                        End If

                        JobApplicationStatusList.Add(aJobApplicationStatus)
                    Loop While rdr.Read
                Else
                    JobApplicationStatusList = Nothing
                End If
                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return JobApplicationStatusList
        End Function


        Public Shared Sub DeleteJobApplicationStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteJobApplicationStatus", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)

                Dim param As New SqlParameter("@ReturnVal", SqlDbType.Int)

                param.Direction = ParameterDirection.Output
                cmd.Parameters.Add(param)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                'Cannot delete an employer with job postings associated with them
                returnError = Integer.Parse(cmd.Parameters("@ReturnVal").Value)
                If returnError = -1 Then
                    StatusErrorDelete = True
                Else
                    StatusErrorDelete = False
                End If

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub


        Public Shared Sub InsertJobApplicationStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertJobApplicationStatus", conn)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)
                ' newID = Integer.Parse(cmd.Parameters("@newID").Value)

                '   NewItemInserted = newID

                If returnError = -1 Then
                    StatusErrorDuplicateName = True
                    ' cannot insert a company with the same name
                Else
                    StatusErrorDuplicateName = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub


        Public Shared Sub UpdateJobApplicationStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspUpdateJobApplicationStatusTable", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


#End Region

#Region " Job Posting Status "

        Public Shared Function SelectAllJobPostingStatus() As StatusList
            Dim JobPostingStatusList As New StatusList
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Dim cmd As New SqlCommand("dbo.uspSelectAllJobPostingStatus", conn)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim aJobPostingStatus As New Status

                        If Not IsDBNull(rdr("JobPostingStatusID")) Then
                            Integer.TryParse(rdr("JobPostingStatusID").ToString, aJobPostingStatus.IDStatus)
                        Else
                            aJobPostingStatus.IDStatus = Nothing
                        End If

                        If Not IsDBNull(rdr("Name")) Then
                            aJobPostingStatus.StatusName = rdr("Name").ToString
                        Else
                            aJobPostingStatus.IDStatus = Nothing
                        End If

                        'adds each status to the StatusList object
                        JobPostingStatusList.Add(aJobPostingStatus)
                    Loop While rdr.Read
                Else
                    JobPostingStatusList = Nothing
                End If

                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return JobPostingStatusList
        End Function


        Public Shared Sub DeleteJobPostingStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspDeleteJobPostingStatus", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)

                Dim param As New SqlParameter("@ReturnVal", SqlDbType.Int)

                param.Direction = ParameterDirection.Output
                cmd.Parameters.Add(param)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                'Cannot delete an employer with job postings associated with them
                returnError = Integer.Parse(cmd.Parameters("@ReturnVal").Value)
                If returnError = -1 Then
                    StatusErrorDelete = True
                Else
                    StatusErrorDelete = False
                End If

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub


        Public Shared Sub InsertJobPostingStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString
            Dim returnError As Integer = 0
            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspInsertJobPostingStatus", conn)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                Dim paramReturn As New SqlParameter("@ReturnValue", SqlDbType.Int)
                paramReturn.Direction = ParameterDirection.Output
                cmd.Parameters.Add(paramReturn)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()

                returnError = Integer.Parse(cmd.Parameters("@ReturnValue").Value)
                ' newID = Integer.Parse(cmd.Parameters("@newID").Value)

                '   NewItemInserted = newID

                If returnError = -1 Then
                    StatusErrorDuplicateName = True
                    ' cannot insert a company with the same name
                Else
                    StatusErrorDuplicateName = False
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Sub


        Public Shared Sub UpdatejobPostingStatus(ByVal status As Status)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CoopConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("dbo.uspUpdateJobPostingStatusTable", conn)
                cmd.Parameters.AddWithValue("@StatusID", status.IDStatus)
                cmd.Parameters.AddWithValue("@StatusName", status.StatusName)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub


#End Region
    End Class

End Namespace

