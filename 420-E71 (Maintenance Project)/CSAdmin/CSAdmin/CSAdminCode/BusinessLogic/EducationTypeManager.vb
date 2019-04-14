' ====================================================
' Purpose: EducationType Manager
' Author:  Catherine Losinger
' Date:    April 29, 2013
'
' History:
' 
' Name              Date            Description
' ====================================================
' Catherine Losinger April 29,2013   Created the EducationType Manager.
' ----------------------------------------------------
' ====================================================
Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.DataAccess
Imports System.ComponentModel

Namespace BusinessLogic
    <DataObject(True)> _
    Public Class EducationTypeManager
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectEducationTypes() As EducationTypeList
            Return EducationTypeDB.SelectEducationTypes()
        End Function

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateEducationType(ByVal IDEducationType As Int32, ByVal Name As String)
            EducationTypeDB.UpdateEducationType(IDEducationType, Name)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Sub InsertEducationType(ByVal Name As String)
            EducationTypeDB.InsertEducationType(Name)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Function DeleteEducationType(ByVal IDEducationType As Int32) As Integer
            Return EducationTypeDB.DeleteEducationType(IDEducationType)
        End Function
    End Class
End Namespace
