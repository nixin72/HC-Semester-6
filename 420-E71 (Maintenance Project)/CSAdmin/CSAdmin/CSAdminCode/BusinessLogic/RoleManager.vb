Option Strict On
Option Explicit On

Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessObjects
Imports System.ComponentModel

Namespace BusinessLogic

    <DataObject(True)> _
    Public Class RoleManager

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Sub InsertUserRole(ByVal IDEmploye As Integer, ByVal RoleCode As String)
            RoleDB.InsertUserRole(IDEmploye, RoleCode)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub DeleteUserRole(ByVal IDEmploye As Integer, ByVal RoleCode As String)
            RoleDB.DeleteUserRole(IDEmploye, RoleCode)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectSpecialRoles() As RoleList 'ByVal IDEmploye As Integer, ByVal RoleCode As String, ByVal ApplicationCode As String) As RoleList
            Return RoleDB.SelectSpecialRoles()
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllUserWithSpecialRoles(ByVal IDRoles As String) As UserRoleList
            Return RoleDB.SelectAllUserWithSpecialRoles(IDRoles)
        End Function

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub DeleteRole(ByVal IDRole As Integer)
            RoleDB.deleteRole(IDRole)
        End Sub

#Region "Applications"
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllApplications() As ApplicationList
            Return RoleDB.selectAllApplications()
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllApplicationRoles(ByVal IDRole As Integer) As ApplicationRoleList
            Return RoleDB.selectAllApplicationRoles(IDRole)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectRolesByApplication(ByVal IDApplication As Integer) As RoleList
            Return RoleDB.SelectRolesByApplication(IDApplication)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectApplicationsByRole(ByVal IDRole As Integer) As ApplicationList
            Return RoleDB.SelectApplicationsByRole(IDRole)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllRoles() As RoleList
            Return RoleDB.selectAllRoles()
        End Function

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Function updateApplication(ByVal IDApplication As Integer, ByVal code As String, ByVal description As String) As Boolean
            Return RoleDB.updateApplication(IDApplication, code, description)
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Function addApplication(ByVal code As String, ByVal description As String) As Boolean
            Return RoleDB.addApplication(code, description)
        End Function

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Function updateRole(ByVal IDRole As Integer, ByVal code As String, ByVal description As String) As Boolean
            Return RoleDB.updateRole(IDRole, code, description)
        End Function

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub updateApplicationRole(ByVal IDApplicationRole As Integer, ByVal isActive As Boolean)
            RoleDB.updateApplicationRole(IDApplicationRole, isActive)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Function addRole(ByVal code As String, ByVal description As String) As Boolean
            Return RoleDB.addRole(code, description)
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Sub addApplicationRole(ByVal IDApplication As Integer, ByVal IDRole As Integer)
            RoleDB.addApplicationRole(IDApplication, IDRole)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub deleteApplicationRole(ByVal IDApplicationRole As Integer)
            RoleDB.deleteApplicationRole(IDApplicationRole)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub deleteApplication(ByVal IDApplication As Integer)
            RoleDB.deleteApplication(IDApplication)
        End Sub
#End Region

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateUsername(ByVal IDUser As Integer, ByVal Username As String)
            UserManager.UpdateUsername(IDUser, Username, Username, True)
        End Sub

    End Class

End Namespace

