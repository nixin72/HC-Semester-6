' ====================================================
' Purpose: CoopCoordinator Business Object
' Author:  Josh Bryans
' Date:    Febuary 1, 2011
'
' History:
' 
' Name              Date            Description
' ====================================================
' Josh Bryans       Feb 2           Added Default constuctor 
' ----------------------------------------------------
' Josh Bryans       Feb 3           Updated FullName property to account for null values
' ====================================================

Namespace BusinessObjects

    Public Class CoopCoordinator

        Private _CoopCoordinatorID As Integer
        Private _LastName As String
        Private _FirstName As String
        Private _EmployeeID As Integer
        Private _Email As String
        Private _isActive As Integer

        Public Sub New()
            _LastName = ""
            _FirstName = ""
        End Sub

        Public Property CoopCoordinatorID As Integer
            Get
                Return _CoopCoordinatorID
            End Get
            Set(ByVal value As Integer)
                _CoopCoordinatorID = value
            End Set
        End Property

        Public Property LastName As String
            Get
                Return _LastName
            End Get
            Set(ByVal value As String)
                If value <> Nothing Then
                    _LastName = value
                End If

            End Set
        End Property

        Public Property FirstName As String
            Get
                Return _FirstName
            End Get
            Set(ByVal value As String)
                If value <> Nothing Then
                    _FirstName = value
                End If

            End Set
        End Property

        Public Property EmployeeID As Integer
            Get
                Return _EmployeeID
            End Get
            Set(ByVal value As Integer)
                _EmployeeID = value
            End Set
        End Property

        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(ByVal value As String)
                _Email = value
            End Set
        End Property

        Public Property isActive() As String
            Get
                Return _isActive
            End Get
            Set(ByVal value As String)
                _isActive = value
            End Set
        End Property


        Public ReadOnly Property FullName As String
            Get
                If _LastName = "" And _FirstName = "" Then
                    Return ""
                ElseIf _LastName = "" Then
                    Return _FirstName
                ElseIf _FirstName = "" Then
                    Return _LastName
                Else
                    Return _LastName & ", " & _FirstName
                End If

            End Get
        End Property


    End Class

End Namespace

