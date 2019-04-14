Public Class EducationTypeLookup
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

    Protected Sub odsEducationType_Deleted(sender As Object, e As ObjectDataSourceStatusEventArgs)
        If CType(e.ReturnValue, Integer) > 0 Then
            valCannotDelete.IsValid = False
        End If
    End Sub
End Class