' ====================================================
' Purpose: Program Coop Coordinator BLL Class
' Author:  Mikael-Raymond Paul
' Date:    March 29, 2011
'
' History:
' 
' Name              Date            Description
' ====================================================
' 
' ----------------------------------------------------
' 
' ====================================================

Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.DataAccess
Imports System.ComponentModel

Namespace BusinessLogic
    <DataObject(True)> _
    Public Class ProgramCoopCoordinatorManager

        ''' <summary>
        ''' Retrieves all program coop coordinators from the database
        ''' </summary>
        ''' <returns>List of Program Coop Coordinatora</returns>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllProgramCoopCoordinators(ByVal SortExpression As String) As ProgramCoopCoordinatorList
            Return ProgramCoopCoordinatorDB.SelectAllProgramCoopCoordinators(SortExpression)
        End Function

        ''' <summary>
        ''' Deletes a specified program coop coordinator combination from database
        ''' </summary>
        ''' <param name="ProgramCoopCoordinatorID">ID of Program Coop Coordinator to delete</param>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Function DeleteProgramCoordinator(ByVal ProgramCoopCoordinatorID As Integer) As Boolean
            Dim count As Integer

            Dim aCoopCoordinator As CoopCoordinator
            aCoopCoordinator = CoopCoordinatorManager.SelectCoopCoordinator(ProgramCoopCoordinatorID)

            count = ProgramCoopCoordinatorDB.DeleteProgramCoordinator(ProgramCoopCoordinatorID)

            If count = 0 Then
                RoleDB.DeleteUserRole(aCoopCoordinator.EmployeeID, "CC")
                Return True
            Else : Return False
            End If
            'MsgBox("Deleted", MsgBoxStyle.SystemModal, "test")
            'ProgramCoopCoordinatorDB.DeleteProgramCoordinator(ProgramCoopCoordinatorID)
        End Function

        ''' <summary>
        ''' Inserts a program coop coordinator combination into the database with the specified parameters
        ''' </summary>
        ''' <param name="EmployeeID">The employee ID of the Coop Coordinator to insert</param>
        ''' <param name="CoopProgramID">ID of Coop Program to insert</param>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Function InsertProgramCoordinator(ByVal EmployeeID As Integer, ByVal CoopProgramID As Integer) As Boolean
            Dim anEmployee As New User
            Dim aCoopCoordinator As New CoopCoordinator

            anEmployee = UserDB.SelectClaraEmployeeByID(EmployeeID)

            aCoopCoordinator.EmployeeID = anEmployee.IDEmploye
            aCoopCoordinator.FirstName = anEmployee.FirstName
            aCoopCoordinator.LastName = anEmployee.LastName

            CoopCoordinatorManager.InsertCoopCoordinator(aCoopCoordinator)

            aCoopCoordinator = CoopCoordinatorManager.SelectCoopCoordinatorByEmployeeID(anEmployee.IDEmploye)

            Dim result As Integer
            result = ProgramCoopCoordinatorDB.InsertProgramCoordinator(aCoopCoordinator.CoopCoordinatorID, CoopProgramID)
            If result = Nothing Then
                Return False
            Else : Return True
                RoleManager.InsertUserRole(EmployeeID, "CC")
            End If
        End Function

    End Class
End Namespace

