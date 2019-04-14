' ====================================================
' Purpose: Language Manager
' Author:  Renee Ghattas
' Date:    April 19, 2012
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
    Public Class LanguageManager
        Public Shared Function SelectDefaultLanguage() As LanguageList
            Return LanguageDB.SelectDefaultLanguage()
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllLanguages() As LanguageList
            Return LanguageDB.SelectAllLanguages()
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Sub InsertLanguage(ByVal Language As String)
            LanguageDB.InsertLanguage(Language)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateLanguage(ByVal LanguageID As Integer, ByVal Language As String)
            LanguageDB.UpdateLanguage(LanguageID, Language)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub SetDefaultLanguage(ByVal LanguageID As Integer)
            LanguageDB.SetDefaultLanguage(LanguageID)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub DeleteLanguage(ByVal LanguageID As Integer)
            LanguageDB.DeleteLanguage(LanguageID)
        End Sub
    End Class
End Namespace
