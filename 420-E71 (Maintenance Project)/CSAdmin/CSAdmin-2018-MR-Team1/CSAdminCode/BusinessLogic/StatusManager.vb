Option Strict On
Option Explicit On

Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessObjects
Imports System.ComponentModel

'Imports CESCode.Format

Namespace BusinessLogic

    <DataObject(True)> _
    Public Class StatusManager

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectStatusTypes() As StatusList
            Return StatusDB.SelectStatusTypes()
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectStatus(ByVal choice As Integer) As StatusList
            If choice = 1 Then
                Return StatusDB.SelectAllConfirmationStatus
            ElseIf choice = 2 Then
                Return StatusDB.SelectAllEligibilityStatus
            ElseIf choice = 3 Then
                Return StatusDB.SelectAllJobApplicationStatus
            ElseIf choice = 4 Then
                Return StatusDB.SelectAllJobPostingStatus
            Else
                Return Nothing
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateStatus(ByVal choice As Integer, ByVal IDStatus As Integer, ByVal StatusName As String) 'As String
            Dim aStatus As New Status
            aStatus.IDStatus = IDStatus
            aStatus.StatusName = StatusName

            If choice = 1 Then
                StatusDB.UpdateConfirmationStatus(aStatus)
            ElseIf choice = 2 Then
                StatusDB.UpdateEligibilityStatus(aStatus)
            ElseIf choice = 3 Then
                StatusDB.UpdateJobApplicationStatus(aStatus)
            ElseIf choice = 4 Then
                StatusDB.UpdatejobPostingStatus(aStatus)
            End If
            'If Not StatusDB.StatusErrorDelete Then
            '    Return "The status was successfuly updated."
            'Else
            '    Return "The attempt to update the status has failed."
            'End If

        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Function DeleteStatus(ByVal choice As Integer, ByVal IDStatus As Integer) As Boolean
            Dim aStatus As New Status
            aStatus.IDStatus = IDStatus
            aStatus.StatusName = Nothing

            If choice = 1 Then
                StatusDB.DeleteConfirmationStatus(aStatus)
            ElseIf choice = 2 Then
                StatusDB.DeleteEligibilityStatus(aStatus)
            ElseIf choice = 3 Then
                StatusDB.DeleteJobApplicationStatus(aStatus)
            ElseIf choice = 4 Then
                StatusDB.DeleteJobPostingStatus(aStatus)
            End If
            If Not StatusDB.StatusErrorDelete Then
                'Return "The status was successfuly deleted."
                Return True
            Else
                Return False
                'Return "The attempt to delete failed."
            End If

        End Function

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Function InsertStatus(ByVal choice As String, ByVal StatusName As String) As Boolean
            Dim aStatus As New Status
            aStatus.IDStatus = Nothing
            aStatus.StatusName = StatusName

            If choice.Equals("Confirmation Status") Then
                StatusDB.InsertConfirmationStatus(aStatus)
            ElseIf choice.Equals("Eligibility Status") Then
                StatusDB.InsertEligibilityStatus(aStatus)
            ElseIf choice.Equals("Job Application Status") Then
                StatusDB.InsertJobApplicationStatus(aStatus)
            ElseIf choice.Equals("Job Posting Status") Then
                StatusDB.InsertJobPostingStatus(aStatus)
            Else
            End If
            If Not StatusDB.StatusErrorDuplicateName Then
                'Return "The status was successfuly inserted."
                Return True
            Else
                'Return "There is a duplicate status. Please try with a different status name."
                Return False
            End If

        End Function

    End Class
End Namespace

