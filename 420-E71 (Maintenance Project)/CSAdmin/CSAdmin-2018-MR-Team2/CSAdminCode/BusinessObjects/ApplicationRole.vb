' ====================================================
' Purpose: ApplicationRole Business Object
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
    Public Class ApplicationRole
        Private _IDApplicationRole As Integer
        Private _Application As Application
        Private _Role As Role
        Private _isActive As Boolean

        Public Property IDApplicationRole As Integer
            Get
                Return _IDApplicationRole
            End Get
            Set(ByVal value As Integer)
                _IDApplicationRole = value
            End Set
        End Property

        Public Property Application As Application
            Get
                Return _Application
            End Get
            Set(ByVal value As Application)
                _Application = value
            End Set
        End Property

        Public Property Role As Role
            Get
                Return _Role
            End Get
            Set(ByVal value As Role)
                _Role = value
            End Set
        End Property

        Public Property isActive As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

    End Class
End Namespace
