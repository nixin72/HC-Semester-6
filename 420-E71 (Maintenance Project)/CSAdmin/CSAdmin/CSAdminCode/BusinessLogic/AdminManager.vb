' ====================================================
' Purpose: Login to CSAdmin System
' Author:  Renee Ghattas
' Date:    April 16, 2012
' ====================================================

Imports System.DirectoryServices.AccountManagement
Imports System.ComponentModel
Imports System.Configuration
Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessLogic
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections

Namespace BusinessLogic
    Public Class AdminManager
        Public Shared Function GetAdminInformation(ByVal Username As String) As Admin
            Dim login As New Admin
            login = LoginDB.SelectAdmin(Username)
            Return login
        End Function

        Public Shared Function checkLDAP(ByVal Username As String, ByVal Password As String) As Boolean
            Dim connLDAP As String = ConfigurationManager.AppSettings("LDAPConnectionString").ToString
            Dim pc As New PrincipalContext(ContextType.Domain, connLDAP)
            Dim isValid As Boolean
            Try
                isValid = pc.ValidateCredentials(Username, Password)
            Catch ex As Exception
                Throw ex
            End Try
            Return (isValid)
        End Function
    End Class
End Namespace
