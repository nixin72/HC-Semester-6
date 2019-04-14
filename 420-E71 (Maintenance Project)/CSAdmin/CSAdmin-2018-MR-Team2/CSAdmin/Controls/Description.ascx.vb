Public Class Description
    Inherits System.Web.UI.UserControl

    Private _description As String
    Private _pageTitle As String

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    Public Property PageTitle() As String
        Get
            Return _pageTitle
        End Get
        Set(value As String)
            _pageTitle = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        lblDescription.Text = Description
        lblPageTitle.text = PageTitle
    End Sub
End Class
