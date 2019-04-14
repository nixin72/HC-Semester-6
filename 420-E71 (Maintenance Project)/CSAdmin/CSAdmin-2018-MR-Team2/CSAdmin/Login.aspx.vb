Imports CSAdminCode.BusinessLogic
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.Common
Imports System.Web.HttpContext

Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoginCSAdmin.FindControl("UserName").Focus()
        Dim lblFailureText As Literal = CType(LoginCSAdmin.FindControl("FailureText"), Literal)

        If Not Session("Error") Is Nothing Then
            lblFailureText.Text = Session("Error").ToString
        Else
            lblFailureText.Text = ""
        End If
    End Sub

    ''' <summary>
    ''' Method that verifies whether a user has the proper credentials and if they are permitted to 
    ''' access the CSAdmin.
    ''' </summary>
    ''' <remarks>Author: Renee Ghattas</remarks>
    Private Sub LoginCSAdmin_LoggedIn(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginCSAdmin.LoggedIn
        If IsValid Then
            Dim admin As New Admin
            admin = AdminManager.GetAdminInformation(LoginCSAdmin.UserName)
            If admin Is Nothing Then
                'login failed
                Session("Error") = "You do not have permission to access the CSAdmin System."
            ElseIf admin.Username = "Duplication Error" Then
                'login failed - duplicate usernames
                Session("Error") = "You have a duplicate username."
            Else
                'login successful
                Session.Remove("Error")
                createSessionVar(admin)
            End If
        Else
            LoginCSAdmin.DestinationPageUrl = "Login.aspx"
        End If
    End Sub

    ''' <summary>
    '''  Creates session variables for the user and yearsemester
    ''' </summary>
    ''' <param name="admin">the user information of the user logged in</param>
    ''' <remarks>Author: Renee Ghattas</remarks>
    Private Sub createSessionVar(ByVal admin As Admin)
        Current.Session.Add("Admin", admin)
        Dim aSemester As New Semester
        aSemester = SemesterManager.SelectSettings()
        If aSemester.YearSemester = Nothing And aSemester.SemesterEndDate = Nothing Then
            'current semester not found
            Current.Session("Semester") = Nothing
            Session("Error") = "There is a problem with the system settings."
            LoginCSAdmin.DestinationPageUrl = "Login.aspx"
        Else
            'current semester found
            Current.Session("Semester") = aSemester
            LoginCSAdmin.DestinationPageUrl = "Home.aspx"
            Response.Redirect("Home.aspx") 'Temporary fix - solves the double login issue but disables redirecting to target page
        End If
    End Sub
End Class