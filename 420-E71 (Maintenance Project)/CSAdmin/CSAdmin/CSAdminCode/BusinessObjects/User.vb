Namespace BusinessObjects
    Public Class User
        Private _IDEmploye As Integer
        Private _IDUser As Integer
        Private _LastName As String
        Private _FirstName As String
        Private _Username As String
        Private _ADUsern As String
        Private _IsActive As Boolean
        Private _Department As String

        Public Property IDEmploye() As Integer
            Get
                Return _IDEmploye
            End Get
            Set(ByVal value As Integer)
                _IDEmploye = value
            End Set
        End Property


        Public Property IDUser() As Integer
            Get
                Return _IDUser
            End Get
            Set(ByVal value As Integer)
                _IDUser = value
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return _LastName
            End Get
            Set(ByVal value As String)
                _LastName = value
            End Set
        End Property

        Public Property FirstName() As String
            Get
                Return _FirstName
            End Get
            Set(ByVal value As String)
                _FirstName = value
            End Set
        End Property

        Public Property Username() As String
            Get
                Return _Username
            End Get
            Set(ByVal value As String)
                _Username = value
            End Set
        End Property

        Public Property ADUsern() As String
            Get
                Return _ADUsern
            End Get
            Set(ByVal value As String)
                _ADUsern = value
            End Set
        End Property

        Public Property IsActive() As Boolean
            Get
                Return _IsActive
            End Get
            Set(ByVal value As Boolean)
                _IsActive = value
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

        Public Property Department() As String
            Get
                Return _Department
            End Get
            Set(ByVal value As String)
                _Department = value
            End Set
        End Property

    End Class
End Namespace

