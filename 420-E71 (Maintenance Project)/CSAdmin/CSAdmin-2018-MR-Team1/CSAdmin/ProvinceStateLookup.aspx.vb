Imports CSAdminCode.BusinessLogic

Public Class ProvinceStateLookup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub odsProvinceStateCA_Inserting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsProvinceStateCA.Inserting
        e.InputParameters.Clear()
        e.InputParameters("Country") = "Canada"
        e.InputParameters("Name") = CType(gvProvinceStateCA.FooterRow.FindControl("txtProvinceName"), TextBox).Text
    End Sub

    Protected Sub gvProvinceStateCA_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvProvinceStateCA.RowCommand
        If e.CommandName = "Insert" Then
            odsProvinceStateCA.Insert()
        End If
    End Sub

    Protected Sub gvProvinceStateCA_RowUpdating(sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvProvinceStateCA.RowUpdating
        Dim IDProvinceState As String = CType(gvProvinceStateCA.Rows(gvProvinceStateCA.EditIndex).FindControl("lblIDProvinceState"), Label).Text
        Dim provinceState As String = CType(gvProvinceStateCA.Rows(gvProvinceStateCA.EditIndex).FindControl("txtProvinceName"), TextBox).Text

        odsProvinceStateCA.UpdateParameters("IDProvinceState").DefaultValue = IDProvinceState
        odsProvinceStateCA.UpdateParameters("Name").DefaultValue = provinceState
    End Sub

    Protected Sub gvProvinceStateCA_RowDeleting(sender As Object, ByVal e As GridViewDeleteEventArgs) Handles gvProvinceStateCA.RowDeleting
        Dim index As Integer = e.RowIndex
        Dim row As GridViewRow = gvProvinceStateCA.Rows(index)

        Dim IDProvinceState As String = CType(row.FindControl("lblIDProvinceState"), Label).Text
        odsProvinceStateCA.UpdateParameters("IDProvinceState").DefaultValue = IDProvinceState
    End Sub

    Private Sub odsProvinceStateUS_Inserting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsProvinceStateUS.Inserting
        e.InputParameters.Clear()
        e.InputParameters("Country") = "United States"
        e.InputParameters("Name") = CType(gvProvinceStateUS.FooterRow.FindControl("txtProvinceName"), TextBox).Text
    End Sub

    Protected Sub gvProvinceStateUS_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvProvinceStateUS.RowCommand
        If e.CommandName = "Insert" Then
            odsProvinceStateUS.Insert()
        End If
    End Sub

    Protected Sub gvProvinceStateUS_RowUpdating(sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvProvinceStateUS.RowUpdating
        Dim IDProvinceState As String = CType(gvProvinceStateUS.Rows(gvProvinceStateUS.EditIndex).FindControl("lblIDProvinceState"), Label).Text
        Dim provinceState As String = CType(gvProvinceStateUS.Rows(gvProvinceStateUS.EditIndex).FindControl("txtProvinceName"), TextBox).Text

        odsProvinceStateUS.UpdateParameters("IDProvinceState").DefaultValue = IDProvinceState
        odsProvinceStateUS.UpdateParameters("Name").DefaultValue = provinceState
    End Sub

    Protected Sub gvProvinceStateUS_RowDeleting(sender As Object, ByVal e As GridViewDeleteEventArgs) Handles gvProvinceStateUS.RowDeleting
        Dim index As Integer = e.RowIndex
        Dim row As GridViewRow = gvProvinceStateUS.Rows(index)

        Dim IDProvinceState As String = CType(row.FindControl("lblIDProvinceState"), Label).Text
        odsProvinceStateUS.UpdateParameters("IDProvinceState").DefaultValue = IDProvinceState
    End Sub

End Class