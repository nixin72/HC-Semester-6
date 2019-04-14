' ====================================================
' Purpose: Master Page VB
' Description: Code behind for the Site.Master page
' Author:  Renee Ghattas
' Date:    April 16, 2012
' ====================================================
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessLogic
Imports CSAdminCode.Common
Imports System.Data.SqlClient

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'verify if user is logged in successfully and if the CurrentYearSemester and SemesterEndDate are available
        If Session("Admin") Is Nothing Or Session("Semester") Is Nothing Then
            LoginSetVisibility(False)
            FormsAuthentication.SignOut()
            If Not Page.Title.Contains("Login") Then
                Response.Redirect("Login.aspx")
            End If
        Else
            LoginSetVisibility(True)
            Dim admin As New Admin
            admin = Session("Admin")
            lblName.Text = Format.FormatFirstLast(admin.FirstName, admin.LastName)
            LoginStatus.LogoutText = "[ Logout ]"

            Dim conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("CSAdminConnectionString").ConnectionString
            lblConnection.Text = conn.Database
        End If

        lblAbout.Text = "The CS Application Administration system was created as part of the Maintenance project by the Winter 2012 3<sup>rd</sup> year Computer Science class:"
        lblAbout.Text += "<br/><br/>&nbsp;&nbsp;Myriam Derôme<br/>&nbsp;&nbsp;Jean-Michel Dunn<br/>&nbsp;&nbsp;Renee Ghattas<br/>&nbsp;&nbsp;Weiman Guo"
        lblAbout.Text += "<br/>&nbsp;&nbsp;Justin Hum<br/>&nbsp;&nbsp;Louis-Philippe Cloutier<br/>&nbsp;&nbsp;Nicholas Renaud<br/>&nbsp;&nbsp;Matt Riopel"
        lblAbout.Text += "<br/><br/>as part of the Development Project courses taught by Susan Turanyi."

    End Sub

    Protected Sub LoginStatus_LoggingOut(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LoginCancelEventArgs) Handles LoginStatus.LoggingOut
        FormsAuthentication.SignOut()
        Session.Clear()
        Session.Abandon()
    End Sub

    Protected Sub LoginSetVisibility(ByVal status As Boolean)
        lblLogin.Visible = status
        lblName.Visible = status
        LoginStatus.Visible = status
        MenuNav.Visible = status
    End Sub

End Class