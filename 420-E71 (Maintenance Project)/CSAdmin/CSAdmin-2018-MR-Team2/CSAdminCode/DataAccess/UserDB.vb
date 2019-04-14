Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.Data.SqlClient
Imports System.Configuration

Namespace DataAccess
    Public Class UserDB


#Region "CES"
        ''' <summary>
        ''' Inserts new student roles in CSAdmin for this semester 
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub InsertNewStudentsFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertStudents", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        ''' <summary>
        ''' Activates students and student roles in CSAdmin for this semester 
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub ActivateStudentsFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspActivateStudents", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        ''' <summary>
        ''' Inserts new student information in CES for this semester
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub InsertNewCESStudentsFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertNewCESStudents", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        ''' <summary>
        ''' Inserts new program coordinator roles in CSAdmin for this semester
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub InsertNewProgramCoordinatorsFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertCoordinators", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        ''' <summary>
        ''' Deactivates student roles in CSAdmin for students not attending the College in the current semester
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub deactivateStudents(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeactivateStudents", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        ''' <summary>
        ''' Deletes inactive students and their associated student evaluation invitations in CES
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub deleteCESStudents(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeleteCESStudents", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
        ''' <summary>
        ''' Deactivates program coordinator roles in CSAdmin that are not active this semester
        ''' </summary>
        ''' <param name="AnSession">Current YearSemester value (e.g.: 20121)</param>
        ''' <remarks></remarks>
        Shared Sub DeactivateProgramCoordinators(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeactivateCoordinators", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Shared Sub InsertNewTeachersFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertTeachers", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Shared Sub InsertNewCESTeachersFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertNewCESTeachers", conn)
            cmd.Parameters.AddWithValue("@Semester", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Shared Function SelectProgramStudentSummary(ByVal AnSession As Integer) As ProgramStudentSummaryList
            Dim theResult As New ProgramStudentSummaryList
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumNewCESStudents", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim aProgramStudentSummary As New ProgramStudentSummary
                        Dim aProgram As New Program

                        'if it exists, use the translated program name
                        If Not IsDBNull(rdr("Program Name")) Then
                            aProgram.longName = rdr("Program Name").ToString()
                        Else
                            aProgram.longName = "No Matching Program Name"
                        End If

                        aProgramStudentSummary.Program = aProgram

                        If Not IsDBNull(rdr("Number of Students")) Then
                            aProgramStudentSummary.NumNewStudentsInProgram = Integer.Parse(rdr("Number of Students").ToString())
                        Else
                            aProgramStudentSummary.NumNewStudentsInProgram = 0
                        End If

                        If Not aProgram.longName.Equals("") And aProgramStudentSummary.NumNewStudentsInProgram <> 0 Then
                            theResult.Add(aProgramStudentSummary)
                        End If
                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return theResult
        End Function

        Shared Function SelectCSAdminProgramStudentSummary(ByVal AnSession As Integer) As ProgramStudentSummaryList
            Dim theResult As New ProgramStudentSummaryList
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumNewCSAdminStudents", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim aProgramStudentSummary As New ProgramStudentSummary
                        Dim aProgram As New Program

                        'if it exists, use the translated program name
                        If Not IsDBNull(rdr("Program Name")) Then
                            aProgram.longName = rdr("Program Name").ToString()
                        Else
                            aProgram.longName = "No Matching Program Name"
                        End If

                        aProgramStudentSummary.Program = aProgram

                        If Not IsDBNull(rdr("Number of Students")) Then
                            aProgramStudentSummary.NumNewStudentsInProgram = Integer.Parse(rdr("Number of Students").ToString())
                        Else
                            aProgramStudentSummary.NumNewStudentsInProgram = 0
                        End If

                        If Not aProgram.longName.Equals("") And aProgramStudentSummary.NumNewStudentsInProgram <> 0 Then
                            theResult.Add(aProgramStudentSummary)
                        End If
                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return theResult
        End Function

        Shared Function SelectActivateProgramStudentSummary(ByVal AnSession As Integer) As ProgramStudentSummaryList
            Dim theResult As New ProgramStudentSummaryList
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumActivateStudents", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim aProgramStudentSummary As New ProgramStudentSummary
                        Dim aProgram As New Program

                        'if it exists, use the translated program name
                        If Not IsDBNull(rdr("Program Name")) Then
                            aProgram.longName = rdr("Program Name").ToString()
                        Else
                            aProgram.longName = "No Matching Program Name"
                        End If

                        aProgramStudentSummary.Program = aProgram

                        If Not IsDBNull(rdr("Number of Students")) Then
                            aProgramStudentSummary.NumNewStudentsInProgram = Integer.Parse(rdr("Number of Students").ToString())
                        Else
                            aProgramStudentSummary.NumNewStudentsInProgram = 0
                        End If

                        If Not aProgram.longName.Equals("") And aProgramStudentSummary.NumNewStudentsInProgram <> 0 Then
                            theResult.Add(aProgramStudentSummary)
                        End If
                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return theResult
        End Function

        Shared Function SelectNumDelStudents(ByVal AnSession As Integer) As Integer
            Dim numDelStudents As Integer = 0

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumDelCESStudents", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do

                        If Not IsDBNull(rdr("Number of Students")) Then
                            numDelStudents += CInt(rdr("Number of Students").ToString())
                        End If

                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return numDelStudents
        End Function

        Shared Function SelectNumDeactivateStudents(ByVal AnSession As Integer) As Integer
            Dim numDeactivateStudents As Integer = 0

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumDeactivateStudents", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do

                        If Not IsDBNull(rdr("Number of Students")) Then
                            numDeactivateStudents += CInt(rdr("Number of Students").ToString())
                        End If

                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return numDeactivateStudents
        End Function

        Shared Function SelectNumNewFaculty(ByVal AnSession As Integer) As Integer
            Dim numNewFaculty As Integer = 0

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumNewCESFaculty", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do

                        If Not IsDBNull(rdr("New CES Faculty")) Then
                            numNewFaculty += CInt(rdr("New CES Faculty").ToString())
                        End If

                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try


            Return numNewFaculty
        End Function

        Shared Function SelectNumDelCoordinators(ByVal AnSession As Integer) As Integer
            Dim numDelCoordinators As Integer = 0

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumDelCESCoordinators", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do

                        If Not IsDBNull(rdr("Number of Coordinators")) Then
                            numDelCoordinators += CInt(rdr("Number of Coordinators").ToString())
                        End If

                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try


            Return numDelCoordinators
        End Function

        Shared Function SelectNumNewCoordinators(ByVal AnSession As Integer) As Integer
            Dim numNewCoordinators As Integer = 0

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumNewProgramCoordinators", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            conn.Open()
            Try

                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do

                        If Not IsDBNull(rdr("Number of Coordinators")) Then
                            numNewCoordinators += CInt(rdr("Number of Coordinators").ToString())
                        End If

                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try


            Return numNewCoordinators
        End Function

#End Region

#Region "Eco-op"
        Shared Sub InsertNewCoopStudentsFromClara(ByVal AnSession As Integer)
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspInsertNewCoopStudentsFromClara", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Shared Function SelectCoopProgramStudentSummary(ByVal AnSession As Integer) As ProgramStudentSummaryList
            Dim theResult As New ProgramStudentSummaryList

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectNumNewCoopStudents", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure
            Dim rdr As SqlDataReader

            Try
                ' create a summary for each program
                Dim theCoopPrograms As CoopProgramList = CoopProgramDB.SelectAllProgramsHavingProgramID()
                For Each aCoopProgram As CoopProgram In theCoopPrograms
                    Dim aProgramStudentSummary As New ProgramStudentSummary
                    aProgramStudentSummary.CoopProgram = aCoopProgram
                    theResult.Add(aProgramStudentSummary)
                Next

                conn.Open()
                rdr = cmd.ExecuteReader()
                rdr.Read()
                If rdr.HasRows Then
                    Do
                        Dim theCurrentProgramID As Integer = Integer.Parse(rdr("ProgramID").ToString())
                        Dim theCurrentNumNewStudents As Integer = Integer.Parse(rdr("NumStudents").ToString())

                        For Each aSummary As ProgramStudentSummary In theResult
                            If aSummary.CoopProgram.ProgramID = theCurrentProgramID Then
                                If theCurrentNumNewStudents <> 0 Then
                                    aSummary.NumNewStudentsInProgram = theCurrentNumNewStudents
                                End If
                                Exit For
                            End If
                        Next

                    Loop While rdr.Read()
                End If
                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            'create a list of summaries with no students
            Dim itemsToRemove As New ProgramStudentSummaryList

            For Each aSummary As ProgramStudentSummary In theResult
                If aSummary.NumNewStudentsInProgram = 0 Then
                    itemsToRemove.Add(aSummary)
                End If
            Next

            For Each aRemoveSummary As ProgramStudentSummary In itemsToRemove
                theResult.Remove(aRemoveSummary)
            Next

            Return theResult
        End Function
#End Region

        Shared Function SelectAllTeachers(ByVal AnSession As Integer) As UserList
            Dim userList As New UserList

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectAllTeachers", conn)
            cmd.Parameters.AddWithValue("@AnSession", AnSession)
            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader

            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()

                    Do
                        Dim aTeacher As New User

                        If Not IsDBNull(rdr("IDEmploye")) Then
                            Integer.TryParse(rdr("IDEmploye").ToString, aTeacher.IDUser)
                        Else
                            aTeacher.IDUser = Nothing
                        End If

                        If Not IsDBNull(rdr("Nom")) Then
                            aTeacher.LastName = rdr("Nom").ToString
                        Else
                            aTeacher.LastName = Nothing
                        End If

                        If Not IsDBNull(rdr("prenom")) Then
                            aTeacher.FirstName = rdr("prenom").ToString
                        Else
                            aTeacher.FirstName = Nothing
                        End If


                        'adds each status to the StatusList object
                        userList.Add(aTeacher)
                    Loop While rdr.Read()
                Else
                    userList = Nothing
                End If
                Return userList

                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Function

        Shared Function SelectClaraEmployeeByID(ByVal IDEmploye As Integer) As User
            Dim aTeacher As New User

            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspSelectClaraEmployeeByID", conn)
            cmd.Parameters.AddWithValue("@EmployeeID", IDEmploye)
            cmd.CommandType = CommandType.StoredProcedure

            Dim rdr As SqlDataReader

            Try
                conn.Open()
                rdr = cmd.ExecuteReader()
                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        If Not IsDBNull(rdr("IDEmploye")) Then
                            Integer.TryParse(rdr("IDEmploye").ToString, aTeacher.IDEmploye)
                        Else
                            aTeacher.IDEmploye = Nothing
                        End If

                        If Not IsDBNull(rdr("Nom")) Then
                            aTeacher.LastName = rdr("Nom").ToString
                        Else
                            aTeacher.LastName = Nothing
                        End If

                        If Not IsDBNull(rdr("prenom")) Then
                            aTeacher.FirstName = rdr("prenom").ToString
                        Else
                            aTeacher.FirstName = Nothing
                        End If

                    Loop While rdr.Read()
                Else
                    aTeacher = Nothing
                End If
                Return aTeacher

                rdr.Close()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

        End Function

#Region "Usernames"

        Shared Function SelectDuplicateUsernames(ByVal SortExpression As String, ByVal SearchLastName As String, ByVal SearchFirstName As String, ByVal SearchUsername As String) As UserList
            Dim userList As New UserList
            Dim conn As New SqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectDuplicateUsernames", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim _isDescending As Boolean = False
            If SortExpression.EndsWith("DESC") Then
                SortExpression = SortExpression.Replace("DESC", "").Trim
                _isDescending = True
            End If

            If SortExpression <> String.Empty Then
                cmd.Parameters.AddWithValue("@SortExpression", SortExpression)
            Else
                cmd.Parameters.AddWithValue("@SortExpression", DBNull.Value)
            End If

            If _isDescending Then
                cmd.Parameters.AddWithValue("@SortDirection", 1)
            Else
                cmd.Parameters.AddWithValue("@SortDirection", 0)
            End If

            If SearchLastName <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchLastName", SearchLastName)
            Else
                cmd.Parameters.AddWithValue("@SearchLastName", DBNull.Value)
            End If

            If SearchFirstName <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchFirstName", SearchFirstName)
            Else
                cmd.Parameters.AddWithValue("@SearchFirstName", DBNull.Value)
            End If

            If SearchUsername <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchUsername", SearchUsername)
            Else
                cmd.Parameters.AddWithValue("@SearchUsername", DBNull.Value)
            End If

            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim user As New User

                        If Not IsDBNull(rdr("IDUser")) Then
                            Integer.TryParse(rdr("IDUser").ToString, user.IDUser)
                        Else
                            user.IDUser = Nothing
                        End If

                        If Not IsDBNull(rdr("LastName")) Then
                            user.LastName = rdr("LastName").ToString
                        Else
                            user.LastName = Nothing
                        End If

                        If Not IsDBNull(rdr("FirstName")) Then
                            user.FirstName = rdr("FirstName").ToString
                        Else
                            user.FirstName = Nothing
                        End If

                        If Not IsDBNull(rdr("Username")) Then
                            user.Username = rdr("Username").ToString
                        Else
                            user.Username = Nothing
                        End If

                        If Not IsDBNull(rdr("IsActive")) Then
                            Boolean.TryParse(rdr("IsActive").ToString, user.IsActive)
                        Else
                            user.IsActive = Nothing
                        End If

                        If Not IsDBNull(rdr("ADUsername")) Then
                            user.ADUsern = rdr("ADUsername").ToString()
                        Else
                            user.ADUsern = Nothing
                        End If

                        userList.Add(user)
                    Loop While rdr.Read
                Else
                    userList = Nothing
                End If

                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            'returns the list of semesters
            Return userList
        End Function

        Shared Function SelectAllUsernames(ByVal SortExpression As String, ByVal SearchLastname As String, ByVal SearchFirstName As String, ByVal SearchUsername As String) As UserList
            Dim userList As New UserList
            Dim conn As New SqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectUsernames", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim _isDescending As Boolean = False
            If SortExpression.EndsWith("DESC") Then
                SortExpression = SortExpression.Replace("DESC", "").Trim
                _isDescending = True
            End If

            If SortExpression <> String.Empty Then
                cmd.Parameters.AddWithValue("@SortExpression", SortExpression)
            Else
                cmd.Parameters.AddWithValue("@SortExpression", DBNull.Value)
            End If

            If _isDescending Then
                cmd.Parameters.AddWithValue("@SortDirection", 1)
            Else
                cmd.Parameters.AddWithValue("@SortDirection", 0)
            End If

            If SearchLastname <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchLastName", SearchLastname)
            Else
                cmd.Parameters.AddWithValue("@SearchLastName", DBNull.Value)
            End If

            If SearchFirstName <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchFirstName", SearchFirstName)
            Else
                cmd.Parameters.AddWithValue("@SearchFirstName", DBNull.Value)
            End If

            If SearchUsername <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchUsername", SearchUsername)
            Else
                cmd.Parameters.AddWithValue("@SearchUsername", DBNull.Value)
            End If

            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim user As New User

                        If Not IsDBNull(rdr("IDUser")) Then
                            Integer.TryParse(rdr("IDUser").ToString, user.IDUser)
                        Else
                            user.IDUser = Nothing
                        End If

                        If Not IsDBNull(rdr("LastName")) Then
                            user.LastName = rdr("LastName").ToString
                        Else
                            user.LastName = Nothing
                        End If

                        If Not IsDBNull(rdr("FirstName")) Then
                            user.FirstName = rdr("FirstName").ToString
                        Else
                            user.FirstName = Nothing
                        End If

                        If Not IsDBNull(rdr("Username")) Then
                            user.Username = rdr("Username").ToString
                        Else
                            user.Username = Nothing
                        End If

                        If Not IsDBNull(rdr("IsActive")) Then
                            Boolean.TryParse(rdr("IsActive").ToString, user.IsActive)
                        Else
                            user.IsActive = Nothing
                        End If

                        If Not IsDBNull(rdr("ADUsername")) Then
                            user.ADUsern = rdr("ADUsername").ToString()
                        Else
                            user.ADUsern = Nothing
                        End If

                        userList.Add(user)
                    Loop While rdr.Read
                Else
                    userList = Nothing
                End If

                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            Return userList
        End Function

        Shared Sub UpdateUsername(ByVal IDUser As Integer, ByVal Username As String, ByVal IsActive As Boolean)
            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Try
                conn.Open()
                Dim cmd As New SqlCommand("uspUpdateUsername", conn)
                cmd.Parameters.AddWithValue("@Username", Username)
                cmd.Parameters.AddWithValue("@IsActive", IsActive)
                cmd.Parameters.AddWithValue("@IDUser", IDUser)

                cmd.CommandType = CommandType.StoredProcedure
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub
#End Region

        Shared Function SelectUsersNotInAD(ByVal SortExpression As String, ByVal SearchLastName As String, ByVal SearchFirstName As String, ByVal SearchUsername As String) As UserList
            Dim userList As New UserList
            Dim conn As New SqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectUsernamesNotInAD", conn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim _isDescending As Boolean = False
            If SortExpression.EndsWith("DESC") Then
                SortExpression = SortExpression.Replace("DESC", "").Trim
                _isDescending = True
            End If

            If SortExpression <> String.Empty Then
                cmd.Parameters.AddWithValue("@SortExpression", SortExpression)
            Else
                cmd.Parameters.AddWithValue("@SortExpression", DBNull.Value)
            End If

            If _isDescending Then
                cmd.Parameters.AddWithValue("@SortDirection", 1)
            Else
                cmd.Parameters.AddWithValue("@SortDirection", 0)
            End If

            If SearchLastName <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchLastName", SearchLastName)
            Else
                cmd.Parameters.AddWithValue("@SearchLastName", DBNull.Value)
            End If

            If SearchFirstName <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchFirstName", SearchFirstName)
            Else
                cmd.Parameters.AddWithValue("@SearchFirstName", DBNull.Value)
            End If

            If SearchUsername <> String.Empty Then
                cmd.Parameters.AddWithValue("@SearchUsername", SearchUsername)
            Else
                cmd.Parameters.AddWithValue("@SearchUsername", DBNull.Value)
            End If

            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim user As New User

                        If Not IsDBNull(rdr("IDUser")) Then
                            Integer.TryParse(rdr("IDUser").ToString, user.IDUser)
                        Else
                            user.IDUser = Nothing
                        End If

                        If Not IsDBNull(rdr("LastName")) Then
                            user.LastName = rdr("LastName").ToString
                        Else
                            user.LastName = Nothing
                        End If

                        If Not IsDBNull(rdr("FirstName")) Then
                            user.FirstName = rdr("FirstName").ToString
                        Else
                            user.FirstName = Nothing
                        End If

                        If Not IsDBNull(rdr("Username")) Then
                            user.Username = rdr("Username").ToString
                        Else
                            user.Username = Nothing
                        End If

                        If Not IsDBNull(rdr("IsActive")) Then
                            Boolean.TryParse(rdr("IsActive").ToString, user.IsActive)
                        Else
                            user.IsActive = Nothing
                        End If

                        userList.Add(user)
                    Loop While rdr.Read
                Else
                    userList = Nothing
                End If

                rdr.Close()

            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try

            'returns the list of semesters
            Return userList
        End Function

        Public Shared Function SelectADByLastName(ByVal LastName As String, ByVal IDUser As Integer, ByVal isActive As Boolean) As UserList
            Dim userList As New UserList
            Dim conn As New SqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspSelectADByLastName", conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@LastName", LastName)

            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Do
                        Dim user As New User

                        'Passing IDUser & isActive as paramaters so we keep it for when we're replacing usernames
                        user.IDUser = IDUser
                        user.IsActive = isActive

                        If Not IsDBNull(rdr("LastName")) Then
                            user.LastName = rdr("LastName").ToString
                        Else
                            user.LastName = Nothing
                        End If

                        If Not IsDBNull(rdr("FirstName")) Then
                            user.FirstName = rdr("FirstName").ToString
                        Else
                            user.FirstName = Nothing
                        End If

                        If Not IsDBNull(rdr("Username")) Then
                            user.ADUsern = rdr("Username").ToString
                        Else
                            user.ADUsern = Nothing
                        End If

                        If Not IsDBNull(rdr("Department")) Then
                            user.Department = rdr("Department").ToString()
                        Else
                            user.Department = Nothing
                        End If

                        userList.Add(user)
                    Loop While rdr.Read
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return userList
        End Function

        Public Shared Sub DeletePendingUsers()
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            Dim cmd As New SqlCommand("uspDeletePendingUsers", conn)
            Try
                conn.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
        End Sub

        Public Shared Function CheckDuplicateUsername(ByVal IDUser As Integer, ByVal Username As String)
            Dim conn As New SqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString

            Dim cmd As New SqlCommand("uspCheckDuplicateUsername", conn)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@IDUser", IDUser)
            cmd.Parameters.AddWithValue("@Username", Username)

            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()

                If rdr.HasRows Then
                    rdr.Read()
                    Return Convert.ToInt32(rdr("CountDuplicate"))
                End If
            Catch ex As Exception
                Throw ex
            Finally
                conn.Close()
            End Try
            Return 0
        End Function
    End Class
End Namespace

