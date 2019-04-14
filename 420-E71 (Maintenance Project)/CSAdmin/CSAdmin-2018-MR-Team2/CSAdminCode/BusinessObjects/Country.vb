' ====================================================
' Purpose: Country Business Object
' Author:  Catherine Losinger
' Date:    April 26, 2013
'
' History:
' 
' Name              Date            Description
' ====================================================
' Catherine Losinger April 26,2013   Created the Country Business Object.
' ----------------------------------------------------
' ====================================================

Namespace BusinessObjects

    Public Class Country
        Private _IDCountry As Integer
        Private _Name As String

        Public Property IDCountry() As Integer
            Get
                Return _IDCountry
            End Get
            Set(ByVal value As Integer)
                _IDCountry = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

    End Class
End Namespace
