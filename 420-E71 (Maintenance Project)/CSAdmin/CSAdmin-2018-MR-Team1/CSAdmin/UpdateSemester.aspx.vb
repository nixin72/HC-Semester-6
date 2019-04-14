Imports CSAdminCode
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessLogic
Imports CSAdminCode.Common
Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessObjects.Collections

Public Class UpdateSemester
    Inherits System.Web.UI.Page

    Dim semester As Semester

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        semester = CType(Session("Semester"), Semester)
        If Not IsPostBack Then
            loadSemesterData()
        End If

        addUsersToSemester.InnerText = "Select Users to Add to the " & Format.FormatSemester(semester.YearSemester) & " Semester"

        lblSemesterUpdated.Visible = False
        lblSemesterCantBeUpdated.Visible = False
    End Sub

#Region "Year Semester"
    ''' <summary>
    ''' Displays the current semester information and builds a radio list to select from
    ''' </summary>
    ''' <remarks>Displays the previous, current and next two semesters as options</remarks>
    Private Sub loadSemesterData()
        'semester may have changed, get latest value
        Dim aSemester As New Semester
        aSemester = SemesterManager.SelectSettings()
        Session("Semester") = aSemester
        semester = aSemester

        rblSemesters.Items.Clear()
        txtSemesterEndDate.Text = Format.FormatDate(semester.SemesterEndDate)

        Dim curSemesterString As String
        Dim CurSemesterNum As Integer
        Dim lastSemesterNum As Integer
        Dim lastSemesterString As String
        Dim nextSemValue As Integer
        Dim secondNextSemValue As Integer
        Dim nextSemString As String
        Dim secondNextSemString As String

        'get current Semester info
        CurSemesterNum = semester.YearSemester - (semester.CurrentYear * 10) 'calculate semester number
        curSemesterString = Format.FormatSemester(semester.YearSemester)


        'calculate last semester
        If CurSemesterNum = 1 Then 'last semester was in last year
            lastSemesterNum = ((semester.CurrentYear - 1) * 10) + 3

        Else 'last semester was in this year
            lastSemesterNum = semester.YearSemester - 1
        End If

        'add last semester to list
        lastSemesterString = Format.FormatSemester(lastSemesterNum)
        rblSemesters.Items.Add(New ListItem(lastSemesterString, lastSemesterNum))


        'add current semester to list (checked)
        Dim tempCurSemListItem As New ListItem(curSemesterString & " (Current Semester)", semester.YearSemester)
        tempCurSemListItem.Selected = True 'current semester selected by default
        rblSemesters.Items.Add(tempCurSemListItem)

        'calculate next two semesters
        If CurSemesterNum = 3 Then 'next two semesters are in next year
            nextSemValue = ((semester.CurrentYear + 1) * 10) + 1
            secondNextSemValue = nextSemValue + 1


        ElseIf CurSemesterNum = 2 Then 'second next semester is in next year
            nextSemValue = semester.YearSemester + 1
            secondNextSemValue = ((semester.CurrentYear + 1) * 10) + 1

        Else 'next two semesters are in current year
            nextSemValue = semester.YearSemester + 1
            secondNextSemValue = nextSemValue + 1

        End If

        'add next two semesters to list
        nextSemString = Format.FormatSemester(nextSemValue)
        secondNextSemString = Format.FormatSemester(secondNextSemValue)

        rblSemesters.Items.Add(New ListItem(nextSemString, nextSemValue))
        rblSemesters.Items.Add(New ListItem(secondNextSemString, secondNextSemValue))


        'display current semester end date
    End Sub

    Protected Sub btnUpdateSemester_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateSemester.Click
        SemesterManager.UpdateCurrentSemester(rblSemesters.SelectedValue, CType(txtSemesterEndDate.Text, DateTime))
        Dim aSemester As New Semester
        aSemester = SemesterManager.SelectSettings()
        Session("Semester") = aSemester
        If semester.YearSemester = aSemester.YearSemester Then
            lblSemesterCantBeUpdated.Visible = True
        Else
            lblSemesterUpdated.Visible = True
            loadSemesterData()
        End If
        addUsersToSemester.InnerText = "Select Users to Add to the " & Format.FormatSemester(aSemester.YearSemester) & " Semester"
    End Sub
#End Region

#Region "Summary Reports"
    Private Sub ShowNewCoopStudentsReport()
        cbAllStudents.Checked = False
        AddAllCoopStudentsReportTxt.InnerHtml = SelectCoopProgramStudentSummary(semester.YearSemester)
    End Sub

    Private Sub ShowCESStudentsReport()
        cbCoopStudents.Checked = False
        AddAllStudentsReportTxt.InnerHtml = SelectProgramStudentSummary(semester.YearSemester)
    End Sub

    Private Sub ShowNewCESFacultyReport()
        cbTeachersAndCoordinators.Checked = False
        AddAllTeachersAndCoordinatorsReportTxt.InnerHtml = SelectNumNewFacultyReport(semester.YearSemester)
    End Sub

    Shared Function SelectCoopProgramStudentSummary(ByVal AnSession As Integer) As String
        Dim theResult As String = ""
        Dim theProgramStudentSummary As ProgramStudentSummaryList = UserDB.SelectCoopProgramStudentSummary(AnSession)
        If theProgramStudentSummary.Count > 1 Then
            theResult += "<table><tr><th>New co-op students</th><th>Program Name</th></tr>"

            For Each aSummary As ProgramStudentSummary In theProgramStudentSummary
                theResult &= "<tr><td>" & aSummary.NumNewStudentsInProgram & "</td><td>" & aSummary.CoopProgram.LongName & " (" & aSummary.CoopProgram.Number & ")</td></tr>"
            Next

            theResult += "</table><br /><br />"
        End If

        If theProgramStudentSummary.Count = 0 Then
            theResult &= "<em>There are no co-op students to add from eCo-op for this semester</em><br /><br />"
        End If

        Return theResult
    End Function

    Shared Function SelectProgramStudentSummary(ByVal AnSession As Integer) As String
        Dim theResult As String = ""
        'get the new student information
        Dim theProgramStudentSummary As ProgramStudentSummaryList = UserDB.SelectProgramStudentSummary(AnSession)
        Dim theCSAdminProgramStudentSummary As ProgramStudentSummaryList = UserDB.SelectCSAdminProgramStudentSummary(AnSession)
        Dim programStudentSummaryToActivate As ProgramStudentSummaryList = UserDB.SelectActivateProgramStudentSummary(AnSession)
        If theProgramStudentSummary.Count >= 1 Then
            theResult += "<table><tr><th>New CES Students</th><th>Program Name</th></tr>"

            For Each aSummary As ProgramStudentSummary In theProgramStudentSummary
                theResult &= "<tr><td>" & aSummary.NumNewStudentsInProgram & "</td><td>" & aSummary.Program.longName & "</td></tr>"
            Next

            theResult += "</table>"
        End If
        If theCSAdminProgramStudentSummary.Count >= 1 Then
            theResult += "<table><tr><th>New CSAdmin Students</th><th>Program Name</th></tr>"

            For Each aSummary As ProgramStudentSummary In theCSAdminProgramStudentSummary
                theResult &= "<tr><td>" & aSummary.NumNewStudentsInProgram & "</td><td>" & aSummary.Program.longName & "</td></tr>"
            Next

            theResult += "</table>"
        End If
        If programStudentSummaryToActivate.Count >= 1 Then
            theResult += "<table><tr><th>CSAdmin Students to Activate</th><th>Program Name</th></tr>"

            For Each aSummary As ProgramStudentSummary In programStudentSummaryToActivate
                theResult &= "<tr><td>" & aSummary.NumNewStudentsInProgram & "</td><td>" & aSummary.Program.longName & "</td></tr>"
            Next

            theResult += "</table>"
        End If


        Dim numberOfDelStudents As Integer = 0
        Dim numberOfDeactivateStudents As Integer = 0
        numberOfDelStudents = UserDB.SelectNumDelStudents(AnSession)
        numberOfDeactivateStudents = UserDB.SelectNumDeactivateStudents(AnSession)

        If numberOfDelStudents <> 0 Then
            theResult &= "<br />"
            theResult &= "<em>Number of students to delete from CES:</em> " & numberOfDelStudents & "<br /><br />"
            theResult &= "<em class=""error"">Notice:</em> This will remove any outstanding student evaluation invitations as well as"
            theResult &= " the associated student accounts from CES"
            theResult &= " <br /><br />"
        End If
        If numberOfDeactivateStudents <> 0 Then
            theResult &= "<br />"
            theResult &= "<em>Number of students to deactivate in CSAdmin:</em> " & numberOfDelStudents & "<br /><br />"
        End If

        If theProgramStudentSummary.Count = 0 And numberOfDelStudents = 0 Then
            theResult &= "<em>There are no students to add or remove from CES for this semester</em><br /><br />"
        End If
        If theCSAdminProgramStudentSummary.Count = 0 And programStudentSummaryToActivate.Count = 0 And numberOfDeactivateStudents = 0 Then
            theResult &= "<em>There are no students to add, activate, or deactivate in CSAdmin for this semester</em><br /><br />"
        End If

        Return theResult
    End Function

    Shared Function SelectNumNewFacultyReport(ByVal AnSession As Integer) As String
        Dim theResult As String = ""

        theResult = "<em>Number of faculty to be added:</em> "
        theResult &= UserDB.SelectNumNewFaculty(AnSession)
        theResult &= "<br />"

        theResult = "<em>Number of coordinators to be added:</em> "
        theResult &= UserDB.SelectNumNewCoordinators(AnSession)
        theResult &= "<br />"

        'get the coordinators to be deactivated information
        Dim numberOfDelCoordinators As Integer = 0
        numberOfDelCoordinators = UserDB.SelectNumDelCoordinators(AnSession)

        If numberOfDelCoordinators <> 0 Then
            theResult &= "<br />"
            theResult &= "<em>Number of coordinators to deactivate from CSAdmin:</em> " & numberOfDelCoordinators & "<br /><br />"
            theResult &= "<em class=""error"">Notice:</em> The deactivated coordinators will no longer have access to CES as coordinators."
            theResult &= " <br /><br />"
        End If

        Return theResult
    End Function

#End Region

#Region "Add and remove users Wizard"
    ''' <summary>
    ''' Handles the paging of the add users wizard
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub wizardAddUsers_NextButtonClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.WizardNavigationEventArgs) Handles wizardAddUsers.NextButtonClick

        If wizardAddUsers.ActiveStepIndex = 0 And cblUsersToAdd.SelectedIndex <> -1 Then 'Which Users to add step
            If cblUsersToAdd.Items(0).Selected Then
                ShowCESStudentsReport()
                wizardAddUsers.ActiveStepIndex = 1
            ElseIf cblUsersToAdd.Items(1).Selected Then
                ShowNewCESFacultyReport()
                wizardAddUsers.ActiveStepIndex = 3
            Else
                wizardAddUsers.ActiveStepIndex = 4
            End If

        ElseIf wizardAddUsers.ActiveStepIndex = 1 And cbAllStudents.Checked = True Then 'Add All Students step
            If cblUsersToAdd.Items(1).Selected Then
                ShowNewCESFacultyReport()
                wizardAddUsers.ActiveStepIndex = 3
            Else
                wizardAddUsers.ActiveStepIndex = 4
            End If
        ElseIf wizardAddUsers.ActiveStepIndex = 2 And cbCoopStudents.Checked = True Then 'Add All co-op Students step
            If cblUsersToAdd.Items(1).Selected Then
                ShowNewCESFacultyReport()
                wizardAddUsers.ActiveStepIndex = 3
            Else
                wizardAddUsers.ActiveStepIndex = 4
            End If

        ElseIf wizardAddUsers.ActiveStepIndex = 3 And cbTeachersAndCoordinators.Checked = True Then 'Add All teachers and coordinators step
            wizardAddUsers.ActiveStepIndex = 4

        ElseIf wizardAddUsers.ActiveStepIndex = 4 Then 'confirm changes step
            processUserChanges()
            wizardAddUsers.ActiveStepIndex = 5


        Else 'did not select any users to add
            e.Cancel = True
        End If


    End Sub
    ''' <summary>
    ''' Ensures that the next button on the final step indicates that
    ''' it is the final step
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>If you add or remove steps from this wizard, you must change the activestepindex statement in this function.
    ''' Created by Louis Cloutier</remarks>
    Private Sub wizardAddUsers_ActiveStepIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles wizardAddUsers.ActiveStepChanged
        If wizardAddUsers.ActiveStepIndex = 4 Then
            wizardAddUsers.StepNextButtonText = "Add Selected Users" 'change text for next step "next" button
        Else
            wizardAddUsers.StepNextButtonText = "Next"
        End If
    End Sub


    ''' <summary>
    ''' Processes the add user functionality of the wizard
    ''' </summary>
    ''' <remarks>If you add any steps, you must make changes in this section to handle new checkboxes in the first wizard screen.
    ''' Created by Louis Cloutier</remarks>
    Protected Sub processUserChanges()
        If cblUsersToAdd.Items(0).Selected Then
            'add all students to CES
            UserManager.InsertNewStudentsFromClara(semester.YearSemester)
            UserManager.removeCESStudents(semester.YearSemester)
        End If

        If cblUsersToAdd.Items(1).Selected Then
            'add all faculty to CES
            UserManager.InsertAllNewFacultyFromClara(semester.YearSemester)
            'remove faculty
            UserManager.removeFaculty(semester.YearSemester)
        End If

    End Sub

#End Region

    Protected Sub rblSemesters_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblSemesters.SelectedIndexChanged
        If rblSemesters.SelectedIndex = 1 Then
            txtSemesterEndDate.Text = Format.FormatDate(semester.SemesterEndDate)
        Else
            txtSemesterEndDate.Text = ""
        End If
    End Sub
End Class
