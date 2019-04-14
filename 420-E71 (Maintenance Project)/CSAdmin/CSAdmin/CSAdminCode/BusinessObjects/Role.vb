' ====================================================
' Purpose: Role Business Object
' Author:  Matt Riopel
' Date:    April 30, 2012
'
' History:
' 
' Name              Date            Description
' ====================================================
' Matt Riopel       April 30,2012   Removed the User object
' ----------------------------------------------------
' 
' ====================================================

Namespace BusinessObjects

    Public Class Role

        Private _IDRole As Integer
        Private _Code As String
        Private _Description As String
        Private _isActive As Boolean
        Private _IDApplicationRole As String

        Public Property IDRole() As Integer
            Get
                Return _IDRole
            End Get
            Set(ByVal value As Integer)
                _IDRole = value
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

        'These break normalization stuff but the alternative is trying to get properties 
        Public Property isActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

        Public Property IDApplicationRole() As Integer
            Get
                Return _IDApplicationRole
            End Get
            Set(ByVal value As Integer)
                _IDApplicationRole = value
            End Set
        End Property


    End Class

End Namespace
