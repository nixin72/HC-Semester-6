Imports CSAdminCode.BusinessLogic

Public Class ManageUsernames
    Inherits System.Web.UI.Page

    Private Sub UpdateUsername(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvUsernames.RowUpdating
        If rblDisplayOptions.SelectedValue = 1 Then
            setDataSourceForUpdate(SelectAllUsernames, e.RowIndex)
        ElseIf rblDisplayOptions.SelectedValue = 2 Then
            setDataSourceForUpdate(SelectDuplicateUsernames, e.RowIndex)
        ElseIf rblDisplayOptions.SelectedValue = 3 Then
            setDataSourceForUpdate(SelectUsersNotInAD, e.RowIndex)
        End If

        Dim IDUser As Integer = Convert.ToInt32(CType(gvUsernames.Rows(e.RowIndex).FindControl("IDUser"), Label).Text)
        Dim userName As String = CType(gvUsernames.Rows(e.RowIndex).FindControl("txtUsername"), TextBox).Text
        If (UserManager.CheckDuplicateUsername(IDUser, userName)) Then
            lblUnique.Text = "A user with that username already exists."
            e.Cancel = True
        End If
    End Sub

    Protected Sub setDataSourceForUpdate(ByVal ods As ObjectDataSource, ByVal index As Integer)
        ods.UpdateParameters.Clear()
        ods.UpdateParameters.Add("IDUser", CType(gvUsernames.Rows(index).FindControl("IDUser"), Label).Text)
    End Sub



#Region "Search Functionality"
    Protected Sub btnSearchNames_Click(sender As Object, e As EventArgs) Handles btnSearchNames.Click
        gvUsernames.EditIndex = -1
        Dim firstName As String = String.Empty
        Dim lastName As String = String.Empty
        Dim username As String = String.Empty

        If txtFirstName.Text.Trim <> "" Then
            firstName = txtFirstName.Text
        End If

        If txtLastName.Text.Trim <> "" Then
            lastName = txtLastName.Text
        End If

        If txtUsername.Text.Trim <> "" Then
            username = txtUsername.Text
        End If

        If rblDisplayOptions.SelectedValue = 1 Then
            setDataSourceForSearch(SelectAllUsernames, firstName, lastName, username)
        ElseIf rblDisplayOptions.SelectedValue = 2 Then
            setDataSourceForSearch(SelectDuplicateUsernames, firstName, lastName, username)
        ElseIf rblDisplayOptions.SelectedValue = 3 Then
            setDataSourceForSearch(SelectUsersNotInAD, firstName, lastName, username)
        End If

    End Sub

    Protected Sub setDataSourceForSearch(ByVal ods As ObjectDataSource, ByVal firstName As String, ByVal lastName As String, ByVal username As String)
        ods.SelectParameters.Clear()
        ods.SelectParameters.Add("SearchFirstName", firstName)
        ods.SelectParameters.Add("SearchLastName", lastName)
        ods.SelectParameters.Add("SearchUsername", username)
        gvUsernames.DataSourceID = ods.ID
        gvUsernames.AllowSorting = False
        gvUsernames.DataBind()
    End Sub

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        If rblDisplayOptions.SelectedValue = 1 Then
            resetDataSource(SelectAllUsernames)
        ElseIf rblDisplayOptions.SelectedValue = 2 Then
            resetDataSource(SelectDuplicateUsernames)
        ElseIf rblDisplayOptions.SelectedValue = 3 Then
            resetDataSource(SelectUsersNotInAD)
        End If

        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtUsername.Text = ""
    End Sub

    Protected Sub resetDataSource(ByVal ods As ObjectDataSource)
        ods.SelectParameters.Clear()
        ods.SelectParameters.Add("SortExpression", String.Empty)
        ods.SelectParameters.Add("SearchFirstName", String.Empty)
        ods.SelectParameters.Add("SearchLastName", String.Empty)
        ods.SelectParameters.Add("SearchUsername", String.Empty)
        gvUsernames.DataSourceID = ods.ID
        gvUsernames.AllowSorting = True
        gvUsernames.DataBind()
    End Sub

#End Region

    Protected Sub rblDisplayOptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblDisplayOptions.SelectedIndexChanged
        gvUsernames.EditIndex = -1
        If rblDisplayOptions.SelectedValue = 1 Then
            resetDataSource(SelectAllUsernames)
        ElseIf rblDisplayOptions.SelectedValue = 2 Then
            resetDataSource(SelectDuplicateUsernames)
        ElseIf rblDisplayOptions.SelectedValue = 3 Then
            resetDataSource(SelectUsersNotInAD)
        End If

        gvUsernames.DataBind()

    End Sub

    Protected Sub gvUsernames_DataBinding(sender As Object, e As EventArgs) Handles gvUsernames.DataBinding
        gvUsernames.PageSize = ddlPageSize.getPageSize
    End Sub

    Protected Sub gvUsernames_rowcommands(sender As Object, e As GridViewCommandEventArgs) Handles gvUsernames.RowCommand
        If e.CommandName = "Replace" Then
            'Find the row that was chosen to replace
            Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument)
            Dim selectedRow As GridViewRow = gvUsernames.Rows(rowIndex)

            'Finding controls in the gridview row
            Dim lblIDUser As Label = selectedRow.FindControl("IDUser")
            Dim chkIsActive As CheckBox = selectedRow.FindControl("CheckBox1")
            Dim lblADUserName As Label = selectedRow.FindControl("lblADUserName")

            'Getting values prepared for the stored procedure
            Dim idUser As Integer = Convert.ToInt32(lblIDUser.Text)
            Dim userName As String = CType(selectedRow.FindControl("txtUsername"), TextBox).Text
            Dim adUsername As String = lblADUserName.Text
            Dim isActive As Boolean = chkIsActive.Checked

            If (UserManager.CheckDuplicateUsername(idUser, userName)) Then
                lblUnique.Text = "A user with that username already exists."
            Else
                UserManager.UpdateUsername(idUser, adUsername, adUsername, isActive)
            End If
            gvUsernames.DataBind()
        End If

        If e.CommandName = "FindInAD" Then
            'Find the row that was chosen to find
            Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument)
            Dim selectedRow As GridViewRow = gvUsernames.Rows(rowIndex)
            Dim chkIsActive As CheckBox = selectedRow.FindControl("CheckBox1")
            Dim lblLastName As Label = selectedRow.FindControl("lblLastName")
            Dim lblFullname As Label = selectedRow.FindControl("lblFullname")
            Dim fullName As String = lblFullname.Text
            Dim lblIDUser As Label = selectedRow.FindControl("IDUser")
            Dim IDUser As Integer = Convert.ToInt32(lblIDUser.Text)
            Dim lastName As String = lblLastName.Text
            Dim isActive As Boolean = chkIsActive.Checked

            lblFindUser.Text = fullName
            gvADUsernames.DataSource = SelectADByLastName
            SelectADByLastName.SelectParameters("SearchLastName").DefaultValue = lastName
            SelectADByLastName.SelectParameters("IDUser").DefaultValue = IDUser
            SelectADByLastName.SelectParameters("IsActive").DefaultValue = isActive
            SelectADByLastName.Select()
            gvADUsernames.DataBind()
            gvADUsernames.Visible = True
            activateFindInADWindow()
        End If
    End Sub

    Protected Sub activateFindInADWindow(Optional ByVal active As Boolean = True)
        If active Then
            addLightbox.Visible = True
        Else
            addLightbox.Visible = False
        End If
    End Sub

    Protected Sub closeWindow() Handles imgClose.Click
        activateFindInADWindow(False)
    End Sub

    Protected Sub gvADUsernames_rowcommands(sender As Object, e As GridViewCommandEventArgs) Handles gvADUsernames.RowCommand
        If e.CommandName = "Replace" Then
            ' The index of an item is based on the total number of rows in a gridview. However, the gridview only knows about the rows on the current page.
            ' Ex. if the index is 15 and the page size is 10, there is no index of 15 on page 2 (only from 0 to 9).
            ' Therefore if the page index is greater than the first page, the index is calculated based on the page and page size
            ' Change the value in the web config to change both the number here and the pagesize on the grid
            Dim rowIndex As Int32 = Convert.ToInt32(e.CommandArgument) - (Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings("gvADUsernamePageSize")) * gvADUsernames.PageIndex)
            Dim selectedRow As GridViewRow = gvADUsernames.Rows(rowIndex)

            Dim lblADUsername As Label = selectedRow.FindControl("lblADUsername")
            Dim lblIDUser As Label = selectedRow.FindControl("lblIDUser")
            Dim chkIsActive As CheckBox = selectedRow.FindControl("CheckBox1")

            Dim IDUser As Integer
            IDUser = Convert.ToInt32(lblIDUser.Text)
            Dim userName As String = CType(selectedRow.FindControl("txtUsername"), TextBox).Text
            Dim ADUsername As String = lblADUsername.Text
            Dim isActive As Boolean = chkIsActive.Checked

            If (UserManager.CheckDuplicateUsername(idUser, userName)) Then
                lblUnique.Text = "A user with that username already exists."
            Else
                UserManager.UpdateUsername(IDUser, ADUsername, ADUsername, isActive)
            End If
            gvUsernames.DataBind()
            activateFindInADWindow(False)

        End If
    End Sub

    Protected Sub gvADUsernames_pageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvADUsernames.PageIndexChanging
        gvADUsernames.PageIndex = e.NewPageIndex
        gvADUsernames.DataSource = SelectADByLastName
        gvADUsernames.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        gvADUsernames.PageSize = System.Configuration.ConfigurationManager.AppSettings("gvADUsernamePageSize")
    End Sub

    Protected Sub gvUsernames_rowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUsernames.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow AndAlso ((CType(e.Row.FindControl("lblADUserName"), Label).Text Is Nothing OrElse CType(e.Row.FindControl("lblADUserName"), Label).Text.Trim() = "") OrElse (CType(e.Row.FindControl("lblUsername"), Label) IsNot Nothing AndAlso CType(e.Row.FindControl("lblADUserName"), Label).Text = (CType(e.Row.FindControl("lblUsername"), Label).Text))) Then
            Dim replaceButton As LinkButton = e.Row.FindControl("btnReplace")
            replaceButton.Enabled = False
            replaceButton.ForeColor = System.Drawing.Color.DarkGray
        End If
    End Sub

    Protected Sub btnDeletePending_Click(sender As Object, e As EventArgs) Handles btnDeletePending.Click
        UserManager.DeletePendingUsers()
    End Sub
End Class