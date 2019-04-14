' ====================================================
' Purpose: Country Manager
' Author:  Catherine Losinger
' Date:    April 26, 2013
'
' History:
' 
' Name              Date            Description
' ====================================================
' Catherine Losinger April 26,2013   Created the Country Manager.
' ----------------------------------------------------
' ====================================================
Imports CSAdminCode.BusinessObjects.Collections
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.DataAccess
Imports System.ComponentModel

Namespace BusinessLogic
    <DataObject(True)> _
    Public Class CountryManager
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectCountries() As CountryList
            Return CountryDB.SelectCountries()
        End Function

        <DataObjectMethod(DataObjectMethodType.Insert)> _
        Public Shared Sub InsertCountry(ByVal Name As String)
            CountryDB.InsertCountry(Name)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateCountry(ByVal IDCountry As Int32, ByVal Name As String)
            CountryDB.UpdateCountry(IDCountry, Name)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Delete)> _
        Public Shared Function DeleteCountry(ByVal IDCountry As Int32) As Integer
            Return CountryDB.DeleteCountry(IDCountry)
        End Function
    End Class
End Namespace
