Public Class ApplicationRoles
    Inherits System.Web.UI.Page

    Dim isFooterInsert As Boolean 'this is necesarry to handle empty grid inserts

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim scriptMessage As String = "vtab();"
        Page.ClientScript.RegisterStartupScript(Page.[GetType](), "callVtab", scriptMessage, True)

        gvRoles.FindControl("")
    End Sub

#Region "Applications"
    ''' <summary>
    ''' Handles the Insert command for the applications
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gvApplications_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvApplications.RowCommand
        If e.CommandName = "Insert" Then
            isFooterInsert = True
            odsApplications.Insert()
        ElseIf e.CommandName = "EmptyInsert" Then 'adding in an empty grid (with no footer)
            isFooterInsert = False
            odsApplications.Insert()
        End If
        gvApplications.DataBind()

    End Sub

    ''' <summary>
    ''' Handles the binding of the insert parameters for the applications
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub odsApplications_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsApplications.Inserting
        e.InputParameters.Clear()

        If isFooterInsert Then
            e.InputParameters("description") = CType(gvApplications.FooterRow.FindControl("tbApplicationDesc"), TextBox).Text
            e.InputParameters("code") = CType(gvApplications.FooterRow.FindControl("tbApplicationCode"), TextBox).Text
        Else
            e.InputParameters("description") = gvApplications.Controls(0).Controls(1).FindControl("tbApplicationDesc")
            e.InputParameters("code") = gvApplications.Controls(0).Controls(1).FindControl("tbApplicationCode")
        End If

    End Sub
#End Region

#Region "Roles"
    ''' <summary>
    ''' Handles the Insert command for the roles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub gvRoles_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRoles.RowCommand
        If e.CommandName = "Insert" Then
            isFooterInsert = True
            odsRoles.Insert()
        ElseIf e.CommandName = "EmptyInsert" Then 'adding in an empty grid (with no footer)
            isFooterInsert = False
            odsRoles.Insert()
        End If
        gvRoles.DataBind()
    End Sub

    ''' <summary>
    ''' Handles the binding of the insert parameters for the roles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub odsRoles_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsRoles.Inserting
        e.InputParameters.Clear()

        If isFooterInsert Then
            e.InputParameters("description") = CType(gvRoles.FooterRow.FindControl("tbRoleDesc"), TextBox).Text
            e.InputParameters("code") = CType(gvRoles.FooterRow.FindControl("tbRoleCode"), TextBox).Text
        Else
            e.InputParameters("description") = gvRoles.Controls(0).Controls(1).FindControl("tbRoleDesc")
            e.InputParameters("code") = gvRoles.Controls(0).Controls(1).FindControl("tbRoleCode")
        End If

    End Sub

    Protected Sub gvRoles_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvRoles.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'TryCast(e.Row.FindControl("odsRolesByApplication"), ObjectDataSource).SelectParameters("IDApplication").DefaultValue = app.IDApplication
            'TryCast(e.Row.FindControl("odsRolesByApplication"), ObjectDataSource).DataBind()
            'TryCast(e.Row.FindControl("odsRolesByApplication"), ObjectDataSource).Select()
            Dim role As CSAdminCode.BusinessObjects.Role = e.Row.DataItem
            Dim ods As ObjectDataSource = TryCast(e.Row.FindControl("odsApplicationsByRole"), ObjectDataSource)
            ods.SelectParameters("IDRole").DefaultValue = role.IDRole
            ods.DataBind()
            ods.Select()
        End If
    End Sub

#End Region

#Region "Application Roles"
    ''' <summary>
    ''' Handles the Insert command for the application roles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub gvApplicationRoles_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvApplicationRoles.RowCommand
    '    If e.CommandName = "Insert" Then
    '        isFooterInsert = True
    '        odsApplicationRoles.Insert()
    '    ElseIf e.CommandName = "EmptyInsert" Then 'adding in an empty grid (with no footer)
    '        isFooterInsert = False
    '        odsApplicationRoles.Insert()
    '    End If
    '    gvApplicationRoles.DataBind()
    'End Sub



    ''' <summary>
    ''' Handles the binding of the insert parameters for the application roles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub odsApplicationRoles_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsApplicationRoles.Inserting
    '    e.InputParameters.Clear()

    '    If isFooterInsert Then
    '        e.InputParameters("IDApplication") = CType(gvApplicationRoles.FooterRow.FindControl("ddlApplication"), DropDownList).SelectedValue
    '        e.InputParameters("IDRole") = gvRoles.SelectedValue
    '    Else
    '        e.InputParameters("IDApplication") = CType(gvApplicationRoles.Controls(0).Controls(0).FindControl("ddlApplication"), DropDownList).SelectedValue
    '        e.InputParameters("IDRole") = gvRoles.SelectedValue
    '    End If

    'End Sub

    'Protected Sub gvRoles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles gvRoles.SelectedIndexChanged
    '    'Only show the application roles if something has been selected
    '    If gvRoles.SelectedIndex <> -1 Then
    '        gvApplicationRoles.Visible = True
    '        lblDeactivateUsers.Visible = True
    '    Else
    '        gvApplicationRoles.Visible = False
    '        lblDeactivateUsers.Visible = False
    '    End If
    'End Sub

    Private Sub odsApplications_inserted(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs) Handles odsApplications.Inserted
        If Not CType(e.ReturnValue, Boolean) Then 'if insert failed
            lblAppCodeUniqueError.Visible = True
        Else
            lblAppCodeUniqueError.Visible = False
        End If
    End Sub

    Private Sub odsApplications_updated(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs) Handles odsApplications.Updated
        If Not CType(e.ReturnValue, Boolean) Then 'if insert failed
            lblAppCodeUniqueError.Visible = True
        Else
            lblAppCodeUniqueError.Visible = False
            gvRoles.DataBind()
        End If
    End Sub

    Private Sub odsRoles_inserted(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs) Handles odsRoles.Inserted
        If Not CType(e.ReturnValue, Boolean) Then 'if insert failed
            lblRoleCodeUniqueError.Visible = True
        Else
            lblRoleCodeUniqueError.Visible = False
        End If
    End Sub

    Private Sub odsRoles_updated(ByVal sender As Object, ByVal e As ObjectDataSourceStatusEventArgs) Handles odsRoles.Updated
        If Not CType(e.ReturnValue, Boolean) Then 'if insert failed
            lblRoleCodeUniqueError.Visible = True
        Else
            lblRoleCodeUniqueError.Visible = False
            gvApplications.DataBind()
        End If
    End Sub
#End Region

    Protected Sub gvApplications_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvApplications.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim app As CSAdminCode.BusinessObjects.Application
            app = TryCast(e.Row.DataItem, CSAdminCode.BusinessObjects.Application)
            TryCast(e.Row.FindControl("odsRolesByApplication"), ObjectDataSource).SelectParameters("IDApplication").DefaultValue = app.IDApplication
            TryCast(e.Row.FindControl("odsRolesByApplication"), ObjectDataSource).DataBind()
            TryCast(e.Row.FindControl("odsRolesByApplication"), ObjectDataSource).Select()
            Dim gvApplicationRoleDescriptions As GridView = TryCast(e.Row.FindControl("gvApplicationRoleDescriptions"), GridView)
            gvApplicationRoleDescriptions.DataBind()
            If gvApplicationRoleDescriptions.Rows.Count = 0 Then
                Dim gvApplicationRoleCodes As GridView = e.Row.FindControl("gvApplicationRoleCodes")
                Dim gvApplicationRoleCommand As GridView = e.Row.FindControl("gvApplicationRoleCommand")
                'Dim BlankRow As New GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Normal)
                Dim EmptyData As New DataTable()
                Dim EmptyRow = Nothing
                EmptyData.Columns.Add(New DataColumn("RowNumber", GetType(String)))
                EmptyData.Columns.Add(New DataColumn("Description", GetType(String)))
                EmptyData.Columns.Add(New DataColumn("Code", GetType(String)))
                EmptyData.Columns.Add(New DataColumn("IDRole", GetType(String)))
                EmptyData.Columns.Add(New DataColumn("IDApplicationRole", GetType(String)))
                EmptyRow = EmptyData.NewRow()
                EmptyRow("RowNumber") = 1
                EmptyRow("Description") = String.Empty
                EmptyRow("Code") = String.Empty
                EmptyRow("IDRole") = String.Empty
                EmptyRow("IDApplicationRole") = String.Empty
                EmptyData.Rows.Add(EmptyRow)
                ViewState("EmptyTable") = EmptyData

                gvApplicationRoleDescriptions.DataSourceID = Nothing
                gvApplicationRoleCodes.DataSourceID = Nothing
                gvApplicationRoleCommand.DataSourceID = Nothing
                gvApplicationRoleDescriptions.DataKeyNames = Nothing
                gvApplicationRoleCodes.DataKeyNames = Nothing
                gvApplicationRoleCommand.DataKeyNames = Nothing
                gvApplicationRoleDescriptions.DataSource = EmptyData
                gvApplicationRoleCodes.DataSource = EmptyData
                gvApplicationRoleCommand.DataSource = EmptyData
                gvApplicationRoleDescriptions.DataBind()
                gvApplicationRoleCodes.DataBind()
                gvApplicationRoleCommand.DataBind()
                gvApplicationRoleCommand.Rows(0).Visible = False
            End If
            TryCast(e.Row.FindControl("lblIDApplication"), Label).Text = app.IDApplication
            e.Row.CssClass = String.Concat("vtab tabid", TryCast(e.Row.DataItem, CSAdminCode.BusinessObjects.Application).IDApplication)
        End If
    End Sub

    Protected Sub gvApplicationRoleCommand_Edit(ByVal sender As Object, ByVal e As EventArgs)
        ' Hierarchy: GridView > Row > Cell > Control
        Dim Row As GridViewRow = TryCast(TryCast(sender, LinkButton).Parent.Parent, GridViewRow)
        Dim IDRole As String = TryCast(sender, LinkButton).CommandArgument
        Dim AppRow As GridViewRow = TryCast(Row.Parent.Parent.Parent.Parent, GridViewRow)
        Dim gvApplicationRoleActive As GridView = AppRow.FindControl("gvApplicationRoleActive")
        Dim EditingRow As GridViewRow = Nothing
        For Each ActiveRow As GridViewRow In gvApplicationRoleActive.Rows
            If ActiveRow.RowType = DataControlRowType.DataRow Then
                If TryCast(ActiveRow.FindControl("lblIDRole"), Label).Text = IDRole Then
                    EditingRow = ActiveRow
                End If
            End If
        Next
        If Not EditingRow Is Nothing Then
            gvApplicationRoleActive.SetEditRow(EditingRow.RowIndex)
        Else
            Debug.WriteLine("You are editing a row that doesn't exist, please stop")
        End If
    End Sub

    Protected Sub gvApplicationRoleCommand_Cancel(ByVal sender As Object, ByVal e As EventArgs)
        Dim Row As GridViewRow = TryCast(TryCast(sender, LinkButton).Parent.Parent, GridViewRow)
        Dim AppRow As GridViewRow = TryCast(Row.Parent.Parent.Parent.Parent, GridViewRow)
        Dim gvApplicationRoleActive As GridView = AppRow.FindControl("gvApplicationRoleActive")
        gvApplicationRoleActive.EditIndex = -1
        gvApplicationRoleActive.DataBind()
    End Sub

    Protected Sub gvApplicationRoleCommand_Update(ByVal sender As Object, ByVal e As EventArgs)
        Dim Row As GridViewRow = TryCast(TryCast(sender, LinkButton).Parent.Parent, GridViewRow)
        Dim IDApplicationRole As String = TryCast(Row.FindControl("lblIDApplicationRole"), Label).Text
        Dim IDRole As String = TryCast(sender, LinkButton).CommandArgument
        Dim AppRow As GridViewRow = TryCast(Row.Parent.Parent.Parent.Parent, GridViewRow)
        Dim gvApplicationRoleActive As GridView = AppRow.FindControl("gvApplicationRoleActive")
        Dim odsRolesByApplication As ObjectDataSource = AppRow.FindControl("odsRolesByApplication")
        Dim UpdatingRow As GridViewRow = Nothing
        For Each ActiveRow As GridViewRow In gvApplicationRoleActive.Rows
            If ActiveRow.RowType = DataControlRowType.DataRow Then
                If TryCast(ActiveRow.FindControl("lblIDRole"), Label).Text = IDRole Then
                    UpdatingRow = ActiveRow
                End If
            End If
        Next
        If Not UpdatingRow Is Nothing Then
            odsRolesByApplication.UpdateParameters("isActive").DefaultValue = TryCast(UpdatingRow.FindControl("chkIsActive"), CheckBox).Checked
        Else
            Debug.WriteLine("You are updating a row that doesn't exist, please stop")
        End If
        gvApplicationRoleActive.EditIndex = -1
        gvApplicationRoleActive.DataBind()
        gvRoles.DataBind()
    End Sub

    Protected Sub gvApplicationRoleCommand_Insert(ByVal sender As Object, ByVal e As EventArgs)
        Dim Row As GridViewRow = TryCast(TryCast(sender, Button).Parent.Parent, GridViewRow)
        Dim IDRole As String = TryCast(sender, Button).CommandArgument
        Dim AppRow As GridViewRow = TryCast(Row.Parent.Parent.Parent.Parent, GridViewRow)
        Dim IDApplication As String = TryCast(AppRow.FindControl("lblIDApplication"), Label).Text
        Dim gvApplicationRoleDescriptions As GridView = AppRow.FindControl("gvApplicationRoleDescriptions")
        Dim gvApplicationRoleCodes As GridView = AppRow.FindControl("gvApplicationRoleCodes")
        Dim odsRolesByApplication As ObjectDataSource = AppRow.FindControl("odsRolesByApplication")
        odsRolesByApplication.InsertParameters("IDApplication").DefaultValue = IDApplication
        odsRolesByApplication.InsertParameters("IDRole").DefaultValue = TryCast(gvApplicationRoleDescriptions.FooterRow.FindControl("ddlDescription"), DropDownList).SelectedValue
        odsRolesByApplication.Insert()
        gvRoles.DataBind()
        gvApplications.DataBind()
        gvApplications.EditIndex = 0
        gvApplications.EditIndex = -1
    End Sub

    Protected Sub gvApplicationRoleCommand_Delete(ByVal sender As Object, ByVal e As EventArgs)
        Dim Row As GridViewRow = TryCast(TryCast(sender, LinkButton).Parent.Parent, GridViewRow)
        Dim AppRow As GridViewRow = TryCast(Row.Parent.Parent.Parent.Parent, GridViewRow)
        Dim odsRolesByApplication As ObjectDataSource = AppRow.FindControl("odsRolesByApplication")
        odsRolesByApplication.DeleteParameters("IDApplicationRole").DefaultValue = TryCast(sender, LinkButton).CommandArgument
        odsRolesByApplication.Delete()
        gvApplications.DataBind()
        gvRoles.DataBind()
        gvApplications.EditIndex = 0
        gvApplications.EditIndex = -1
    End Sub

    Protected Sub ForceStyle(ByVal sender As Object, ByVal e As EventArgs)
        gvRoles.DataBind()
    End Sub

    Protected Sub odsApplications_Deleted(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsApplications.Deleted
        Response.Redirect("ApplicationRoles.aspx")
    End Sub

    Protected Sub odsRoles_Deleted(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsRoles.Deleted
        Response.Redirect("ApplicationRoles.aspx")
    End Sub
End Class