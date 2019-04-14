Imports CSAdminCode.BusinessLogic
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections

Public Class ManageUserRoles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim YearSemester As New Semester
        YearSemester = SemesterManager.SelectSettings

        lblErrorRoles.Text = ""
        'AddCoopCoordinatorErrorMessage.Text = ""

        Session("Semester") = YearSemester

        If Not Page.IsPostBack Then
            odsOtherRoles.SelectParameters("IDRoles").DefaultValue = "|0|"
        End If
    End Sub

#Region " Variables "

    Private status As String

    Dim IsFooterInsert As Boolean
    Private selectedItemString As String

#End Region

#Region " Loading for gvRoles "

    Public Sub LoadRolesInFooter()
        Dim roleList As New RoleList
        'gets a list of all the coop programs
        roleList = RoleManager.SelectAllRoles

        Dim ddl As New DropDownList
        'assigns the drop down from the gridview to a dynamic drop down
        If Not IsNothing(gvRoles.FooterRow) Then


            ddl = gvRoles.FooterRow.FindControl("ddlRoles")

            If Not IsNothing(roleList) Then
                If ddl.Items.Count = 0 Then
                    ddl.Items.Add(New ListItem("Select a Role", -1))
                    'loops through the teacherList object and adds it to the teacher drop down in the gridview
                    For Each aRole As Role In roleList
                        'IDUser is the replacements for IDEmploye
                        ddl.Items.Add(New ListItem(aRole.Description, aRole.Code))
                    Next
                End If
            Else
                ddl.Items.Add(New ListItem("There are no roles to display", -1))
            End If
        End If

    End Sub

    Public Sub LoadEmployeesInFooter()
        Dim employeeList As New UserList
        'gets a list of all the employees
        employeeList = UserManager.SelectAllTeachers(CType(Session("Semester"), Semester).YearSemester)

        Dim ddl As New DropDownList
        'assigns the drop down from the gridview to a dynamic drop down
        If Not IsNothing(gvRoles.FooterRow) Then
            ddl = gvRoles.FooterRow.FindControl("ddlEmployee")

            If Not IsNothing(employeeList) Then
                If ddl.Items.Count = 0 Then
                    ddl.Items.Add(New ListItem("Select an Employee", -1))
                    'loops through the teacherList object and adds it to the teacher drop down in the gridview
                    For Each anEmployee As User In employeeList
                        'IDUser is the replacements for IDEmploye
                        ddl.Items.Add(New ListItem(anEmployee.FullName, anEmployee.IDUser))
                    Next
                End If
            Else
                ddl.Items.Add(New ListItem("There are no employees to display", -1))
            End If
        End If

    End Sub

#End Region

#Region " CES Functionality "

    Private Sub gvRoles_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRoles.PreRender
        If Not Session("Semester") Is Nothing Then
            Call LoadEmployeesInFooter()
            Call LoadRolesInFooter()
        End If
    End Sub

    Private Sub gvRoles_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRoles.RowCommand
        'adding in the main content
        If e.CommandName.Equals("Insert") Then
            IsFooterInsert = True
            odsOtherRoles.Insert() 'go to the function called odsRoles_Inserting
        ElseIf e.CommandName.Equals("EmptyInsert") Then 'an add in the footer of the gridview
            IsFooterInsert = False
            odsOtherRoles.Insert() 'go to the function called odsRoles_Inserting
            'ElseIf e.CommandName.Equals("Delete") Then
            '    odsOtherRoles.Delete()
        End If

        If Not Page.IsPostBack Then
            gvRoles.DataBind()
        End If

    End Sub

    Private Sub gvRoles_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRoles.RowCreated
            'checks if the gridviews has rows
            If e.Row.RowType = DataControlRowType.EmptyDataRow Then

                Dim ddlRoles As New DropDownList
                'assigns the drop down in the gridview to a dynamic drop down
                ddlRoles = CType(e.Row.FindControl("ddlRoles"), DropDownList)

                'checks how many items are in the drop down
                If ddlRoles.Items.Count = 0 Then
                    ddlRoles.Items.Add(New ListItem("Select a Role", -1))
                    Dim roleList As New RoleList

                    'gets a list of all the programs
                    roleList = RoleManager.SelectAllRoles

                    'loops through each program and adds them to the dynamic drop down
                    'the dropdown shows the long name of the program and the value of the CoopProgramID
                    For Each aRole As Role In roleList
                        ddlRoles.Items.Add(New ListItem(aRole.Description, aRole.Code))
                    Next
                End If

                Dim ddlEmployee As New DropDownList
                'assigns the drop down in the gridview to a dynamic drop down
                ddlEmployee = CType(e.Row.FindControl("ddlEmployee"), DropDownList)

                'checks how many items are in the drop down
                If ddlEmployee.Items.Count = 0 Then
                    ddlEmployee.Items.Add(New ListItem("Select an Employee", -1))
                    Dim employeeList As New UserList

                    'gets a list of all the employees in the college
                    employeeList = UserManager.SelectAllTeachers(CType(Session("Semester"), Semester).YearSemester)

                    'loops through each employee and adds them to the dynamic drop down
                    'Last name followed by first name, and a value of IDemploye which is IDUser.

                For Each anEmployee As User In employeeList
                    ddlEmployee.Items.Add(New ListItem(anEmployee.FullName, anEmployee.IDUser))
                Next


        End If
            End If
    End Sub


#Region " Inserting "

    Private Sub odsOtherRoles_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles odsOtherRoles.Inserted
        gvRoles.DataBind()
    End Sub

    'This occurs before odsOtherRoles_Inserted routine
    Private Sub odsOtherRoles_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsOtherRoles.Inserting
        e.InputParameters.Clear()
        Dim ddlEmployee As New DropDownList
        Dim ddlRoles As New DropDownList

        'ddlTeacherList = gvProgramCoopCoordinatorList.Controls(0).Controls(0).FindControl("ddlCoopProgramList")
        If IsFooterInsert Then
            'assigns the same drop downs in the gridview
            ddlEmployee = gvRoles.FooterRow.FindControl("ddlEmployee")
            ddlRoles = gvRoles.FooterRow.FindControl("ddlRoles")
        Else
            'assigns the same drop downs in the gridview
            ddlEmployee = gvRoles.Controls(0).Controls(0).FindControl("ddlEmployee")
            ddlRoles = gvRoles.Controls(0).Controls(0).FindControl("ddlRoles")
        End If

        'gets the selected value of the drop downs
        e.InputParameters("IDEmploye") = ddlEmployee.SelectedValue
        e.InputParameters("RoleCode") = ddlRoles.SelectedValue

    End Sub

#End Region 'Inserting

#Region " Deleting "

    Private Sub gvRoles_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvRoles.RowDeleting
        odsOtherRoles.DeleteParameters.Clear()
        odsOtherRoles.DeleteParameters.Add("IDEmploye", CInt(CType(gvRoles.Rows(e.RowIndex).FindControl("lblIDEmploye"), Label).Text))
        odsOtherRoles.DeleteParameters.Add("RoleCode", CType(gvRoles.Rows(e.RowIndex).FindControl("lblRoleCode"), Label).Text)
    End Sub

    Private Sub odsOtherRoles_Deleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles odsOtherRoles.Deleted
        gvRoles.DataBind()
    End Sub

#End Region 'Deleting

#Region "Updating"
    Protected Sub odsOtherRoles_Updating(sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsOtherRoles.Updating
        e.InputParameters.Clear()
        Dim IDUser As String = CType(gvRoles.Rows(gvRoles.EditIndex).FindControl("lblIDUser"), Label).Text
        Dim userName As String = CType(gvRoles.Rows(gvRoles.EditIndex).FindControl("txtUsername"), TextBox).Text
        e.InputParameters("IDUser") = IDUser
        e.InputParameters("Username") = userName

        If (UserManager.CheckDuplicateUsername(IDUser, userName)) Then
            lblUnique.Text = "A user with that username already exists."
            e.Cancel = True
        End If
    End Sub

    Private Sub odsOtherRoles_Updated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles odsOtherRoles.Updated
         
        gvRoles.DataBind()
    End Sub
#End Region

#End Region 'CES Functionality
    Protected Sub chkRoles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkRoles.SelectedIndexChanged
        Dim roles As String = "|"

        For Each item As ListItem In chkRoles.Items
            If item.Selected = True Then
                roles += item.Value + "|"
            End If
        Next

        odsOtherRoles.SelectParameters("IDRoles").DefaultValue = roles
        odsOtherRoles.DataBind()
        gvRoles.DataBind()
    End Sub
End Class