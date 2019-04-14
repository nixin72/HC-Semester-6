' ====================================================
' Purpose: CoopCoordinator Manager
' Author:  Cedric Burgins
' Date:    March 29, 2011
'
' History:
' 
' Name              Date            Description
' ====================================================
' Josh Bryans       Mar 17th        Added methods to email the employer
' ----------------------------------------------------
' 
' ====================================================

Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.ComponentModel

Namespace BusinessLogic
    <DataObject(True)> _
    Public Class CoopCoordinatorManager
        Private Shared _DeleteError As Boolean

        Public Shared Property CoopCoordinatorDeleteError() As Boolean
            Get
                Return _DeleteError
            End Get
            Set(ByVal value As Boolean)
                _DeleteError = value
            End Set
        End Property

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllCoopCoordinators() As CoopCoordinatorList
            Return CoopCoordinatorDB.SelectAllCoopCoordinators
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectCoopCoordinator(ByVal ProgramCoopCoordinatorID As Integer) As CoopCoordinator
            If Not ProgramCoopCoordinatorID = -1 Then
                Return CoopCoordinatorDB.SelectCoopCoordinator(ProgramCoopCoordinatorID)
            Else
                Return Nothing
            End If

        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectCoopCoordinatorByEmployeeID(ByVal EmployeeID As Integer) As CoopCoordinator
            If Not EmployeeID = -1 Then
                Return CoopCoordinatorDB.SelectCoopCoordinatorByEmployeeID(EmployeeID)
            Else
                Return Nothing
            End If
        End Function

        Public Shared Sub UpdateCoopCoordinator(ByVal aCoopCoordinator As CoopCoordinator)
            CoopCoordinatorDB.UpdateCoopCoordinator(aCoopCoordinator)
        End Sub

        Public Shared Sub InsertCoopCoordinator(ByVal aCoopCoordinator As CoopCoordinator) 'As Boolean
            CoopCoordinatorDB.InsertCoopCoordinator(aCoopCoordinator)

            'If Not CoopCoordinatorDB.ErrorDuplicateName Then
            '    'Return "The status was successfuly inserted."
            '    Return True
            'Else
            '    'Return "There is a duplicate status. Please try with a different status name."
            '    Return False
            'End If

        End Sub

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectClaraEmployeeByID(ByVal EmployeeID As Integer) As User
            Return CoopCoordinatorDB.SelectClaraEmployeeByID(EmployeeID)
        End Function

    End Class
End Namespace