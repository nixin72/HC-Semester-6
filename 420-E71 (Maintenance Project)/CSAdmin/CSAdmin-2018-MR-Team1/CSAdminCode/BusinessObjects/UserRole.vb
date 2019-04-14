' ====================================================
' Purpose: UserRole Business Object
' Author:  Matt Riopel
' Date:    April 30, 2012
'
' History:
' 
' Name              Date            Description
' ====================================================
' Matt Riopel       April 30,2012   Initial creation
' ----------------------------------------------------
' 
' ====================================================

Namespace BusinessObjects
    Public Class UserRole
        Private _IDEmploye As Integer
        Private _Employee As User
        Private _Role As Role

        Public Property IDEmploye As Integer
            Get
                Return _IDEmploye
            End Get
            Set(ByVal value As Integer)
                _IDEmploye = value
            End Set
        End Property

        Public Property Employee As User
            Get
                Return _Employee
            End Get
            Set(ByVal value As User)
                _Employee = value
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

        Public ReadOnly Property EmployeeName As String
            Get
                Return _Employee.FullName
            End Get
        End Property

        Public ReadOnly Property Username As String
            Get
                Return _Employee.Username
            End Get
        End Property

    End Class
End Namespace


