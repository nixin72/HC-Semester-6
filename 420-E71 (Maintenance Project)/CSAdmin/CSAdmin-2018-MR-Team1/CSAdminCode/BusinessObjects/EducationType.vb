' ====================================================
' Purpose: EducationType Business Object
' Author:  Catherine Losinger
' Date:    April 29, 2013
'
' History:
' 
' Name              Date            Description
' ====================================================
' Catherine Losinger April 29,2013   Created the EducationType Business Object.
' ----------------------------------------------------
' ====================================================
Namespace BusinessObjects
    Public Class EducationType
        Private _IDEducationType As Integer
        Private _Name As String

        Public Property IDEducationType() As Integer
            Get
                Return _IDEducationType
            End Get
            Set(ByVal value As Integer)
                _IDEducationType = value
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
