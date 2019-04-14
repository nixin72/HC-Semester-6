' ====================================================
' Purpose: Application Business Object
' Author:  Louis Cloutier
' Date:    April 30, 2012
'
' History:
' 
' Name              Date            Description
' ====================================================
' 
' ----------------------------------------------------
' 
' ====================================================
Namespace BusinessObjects
    Public Class Application
        Private _IDApplication As Integer
        Private _Code As String
        Private _Description As String
        Private _isActive As String

        Public Property IDApplication() As Integer
            Get
                Return _IDApplication
            End Get
            Set(ByVal value As Integer)
                _IDApplication = value
            End Set
        End Property

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property isActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

    End Class
End Namespace
