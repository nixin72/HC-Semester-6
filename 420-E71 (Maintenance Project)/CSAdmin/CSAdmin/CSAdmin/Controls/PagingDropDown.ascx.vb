Public Class PagingDropDown
    Inherits System.Web.UI.UserControl

    Private _control As String

    Public Property Control() As String
        Get
            Return _control
        End Get
        Set(value As String)
            _control = value
        End Set
    End Property

    Public Function getPageSize() As Integer
        Return Integer.Parse(ddlPageSize.SelectedValue)
    End Function

    Protected Sub ddlPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPageSize.SelectedIndexChanged
        CType(Me.Parent.FindControl(Control), GridView).DataBind()
    End Sub

End Class