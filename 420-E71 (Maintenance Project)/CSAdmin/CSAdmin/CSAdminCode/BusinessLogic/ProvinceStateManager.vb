' ====================================================
' Purpose: ProvinceState Manager
' Author:  Catherine Losinger
' Date:    April 29, 2013
'
' History:
' 
' Name              Date            Description
' ====================================================
' Catherine Losinger April 29,2013   Created the ProvinceState Manager.
' ----------------------------------------------------
' Alex Desbiens     MAY 09 2013     Added SelectProvinceStatesByCountry
' ====================================================
Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.DataAccess
Imports System.ComponentModel

Namespace BusinessLogic
    <DataObject(True)> _
    Public Class ProvinceStateManager
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectProvinceStates() As ProvinceStateList
            Return ProvinceStateDB.SelectProvinceStates()
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectProvinceStatesByCountry(ByVal IDCountry As Integer) As ProvinceStateList
            Return ProvinceStateDB.SelectProvinceStatesByCountry(IDCountry)
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Sub InsertProvinceState(ByVal Name As String, ByVal Country As String)
            ProvinceStateDB.InsertProvinceState(Name, Country)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateProvinceState(ByVal IDProvinceState As Int32, ByVal Name As String)
            ProvinceStateDB.UpdateProvinceState(IDProvinceState, Name)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Sub DeleteProvinceState(ByVal IDProvinceState As Int32)
            ProvinceStateDB.DeleteProvinceState(IDProvinceState)
        End Sub
    End Class
End Namespace
