Imports CSAdminCode.BusinessLogic

Public Class ManageGeneral
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub odsEducationType_Inserting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsEducationType.Inserting
        e.InputParameters.Clear()
        e.InputParameters("Name") = CType(gvEducatuionType.FooterRow.FindControl("txtInsertEducationType"), TextBox).Text
    End Sub

    Protected Sub gvEducatuionType_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvEducatuionType.RowCommand
        If e.CommandName = "Insert" Then
            odsEducationType.Insert()
        End If
    End Sub

    Protected Sub gvEducatuionType_RowUpdating(sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvEducatuionType.RowUpdating
            Dim IDEducationType As String = CType(gvEducatuionType.Rows(gvEducatuionType.EditIndex).FindControl("lblEditEducationTypeID"), Label).Text
            Dim educationType As String = CType(gvEducatuionType.Rows(gvEducatuionType.EditIndex).FindControl("txtEducationType"), TextBox).Text

            odsEducationType.UpdateParameters("IDEducationType").DefaultValue = IDEducationType
            odsEducationType.UpdateParameters("Name").DefaultValue = educationType
    End Sub

    Protected Sub gvEducatuionType_RowDeleting(sender As Object, ByVal e As GridViewDeleteEventArgs) Handles gvEducatuionType.RowDeleting
        Dim index As Integer = e.RowIndex
        Dim row As GridViewRow = gvEducatuionType.Rows(index)

        Dim IDEducationType As String = CType(row.FindControl("lblEducationTypeID"), Label).Text
        odsEducationType.UpdateParameters("IDEducationType").DefaultValue = IDEducationType
    End Sub

    Protected Sub btnSaveCountry_Click(sender As Object, e As EventArgs) Handles btnSaveCountry.Click
        Dim toDo As String = rblCountries.SelectedValue

        If toDo.Equals("Add") Then
            CountryManager.InsertCountry(txtCountry.Text)
            lblCountries.Text = "The country has been added."
        End If
        If toDo.Equals("Delete") Then
            CountryManager.DeleteCountry(ddlCountries.SelectedValue)
            lblCountries.Text = "The country has been deleted."
        End If
        If toDo.Equals("Edit") Then
            CountryManager.UpdateCountry(ddlCountries.SelectedValue, txtCountry.Text)
            lblCountries.Text = "The country has been changed."
        End If

        ddlCountries.DataBind()
        txtCountry.Text = ""
        txtCountry.Enabled = False
        lblCountries.Visible = True
    End Sub

    Protected Sub btnSaveProvinceState_Click(sender As Object, e As EventArgs) Handles btnSaveProvinceState.Click
        Dim toDo As String = rblProvinceState.SelectedValue

        If toDo.Equals("Canada") Then
            ProvinceStateManager.InsertProvinceState(txtProvinceState.Text, "Canada")
            lblConProvinceState.Text = "The province or state has been added."
        ElseIf toDo.Equals("USA") Then
            ProvinceStateManager.InsertProvinceState(txtProvinceState.Text, "United States")
            lblConProvinceState.Text = "The province or state has been added."
        ElseIf toDo.Equals("Delete") Then
            ProvinceStateManager.DeleteProvinceState(ddlProvinceState.SelectedValue)
            lblConProvinceState.Text = "The province or state has been deleted."
        ElseIf toDo.Equals("Edit") Then
            ProvinceStateManager.UpdateProvinceState(ddlProvinceState.SelectedValue, txtProvinceState.Text)
            lblConProvinceState.Text = "The province or state has been changed."
        End If

        ddlProvinceState.DataBind()
        txtProvinceState.Text = ""
        txtProvinceState.Enabled = False
        lblConProvinceState.Visible = True
    End Sub

    Protected Sub rblCountries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblCountries.SelectedIndexChanged
        Dim toDo As String = rblCountries.SelectedValue
        txtCountry.Text = ""
        txtCountry.Enabled = False
        rfvCountry.Enabled = False
        lblCountries.Visible = False

        If toDo.Equals("Add") Then
            txtCountry.Enabled = True
            rfvCountry.Enabled = True
        ElseIf toDo.Equals("Edit") Then
            txtCountry.Enabled = True
            rfvCountry.Enabled = True
            txtCountry.Text = ddlCountries.SelectedItem.ToString
        End If
    End Sub

    Protected Sub rblProvinceState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblProvinceState.SelectedIndexChanged
        Dim toDo As String = rblProvinceState.SelectedValue
        txtProvinceState.Text = ""
        txtProvinceState.Enabled = False
        rfvProvinceState.Enabled = False
        lblConProvinceState.Visible = False

        If toDo.Equals("Canada") Or toDo.Equals("USA") Then
            txtProvinceState.Enabled = True
            rfvProvinceState.Enabled = True
        ElseIf toDo.Equals("Edit") Then
            txtProvinceState.Enabled = True
            rfvProvinceState.Enabled = True
            txtProvinceState.Text = ddlProvinceState.SelectedItem.ToString
        End If
    End Sub
End Class