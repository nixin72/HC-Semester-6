Imports CSAdminCode.BusinessLogic

Public Class CountriesLookup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblUnique.Text = ""

    End Sub
    Protected Sub gvUsernames_DataBinding(sender As Object, e As EventArgs) Handles gvCountries.DataBinding
        gvCountries.PageSize = ddlPageSize.getPageSize
    End Sub

    Private Sub odsCountries_Inserting(sender As Object, e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsCountries.Inserting
        e.InputParameters.Clear()
        e.InputParameters("Name") = CType(gvCountries.FooterRow.FindControl("txtCountryName"), TextBox).Text
    End Sub

    Protected Sub gvCountries_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvCountries.RowCommand
        If e.CommandName = "Insert" Then
            Try
                odsCountries.Insert()
            Catch ex As Exception
                If ex.InnerException.Message.ToString().Contains("UNIQUE") Then
                    lblUnique.Text = "This country cannot be added because it already exists."
                End If
            End Try

        End If
    End Sub

    Protected Sub gvCountries_RowUpdating(sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvCountries.RowUpdating
        Dim IDCountry As String = CType(gvCountries.Rows(gvCountries.EditIndex).FindControl("lblIDCountry"), Label).Text
        Dim country As String = CType(gvCountries.Rows(gvCountries.EditIndex).FindControl("txtCountryName"), TextBox).Text

        odsCountries.UpdateParameters("IDCountry").DefaultValue = IDCountry
        odsCountries.UpdateParameters("Name").DefaultValue = country
    End Sub

    Protected Sub gvCountries_RowDeleting(sender As Object, ByVal e As GridViewDeleteEventArgs) Handles gvCountries.RowDeleting
        Dim index As Integer = e.RowIndex
        Dim row As GridViewRow = gvCountries.Rows(index)

        Dim IDCountry As String = CType(row.FindControl("lblIDCountry"), Label).Text
        odsCountries.UpdateParameters("IDCountry").DefaultValue = IDCountry
    End Sub

    Protected Sub odsCountries_Deleted(sender As Object, e As ObjectDataSourceStatusEventArgs)
        If CType(e.ReturnValue, Integer) > 0 Then
            valCannotDelete.IsValid = False
        End If
    End Sub
End Class