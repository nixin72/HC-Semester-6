' ====================================================
' Purpose: ProgramManager
' Author:  Renee Ghattas
' Date:    April 16, 2012
'
' History:
' 
' Name              Date            Description
' ====================================================
' 
' ----------------------------------------------------
' 
' ====================================================

Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.DataAccess
Imports System.ComponentModel

Namespace BusinessLogic
    <DataObject(True)> _
    Public Class ProgramManager

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllPrograms() As ProgramList
            Return ProgramDB.SelectAllprograms()
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectCegepPrograms() As ProgramList
            Return ProgramDB.SelectcegepPrograms
        End Function
        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Function InsertProgramVersion(ByVal ProgramID As Integer, ByVal Number As String, ByVal CoopProgramID As Integer) As Boolean
            ProgramDB.InsertProgramVersion(ProgramID, Number, CoopProgramID)
            If Not ProgramDB.ErrorDuplicate Then
                Return True
            Else
                Return False
            End If
        End Function

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub DeleteProgramVersion(ByVal programID As Integer)
            ProgramDB.DeleteProgramVersion(programID)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateProgram(ByVal programID As Integer, ByVal isActive As Boolean)
            ProgramDB.UpdateProgram(programID, isActive)
        End Sub
    End Class
End Namespace
