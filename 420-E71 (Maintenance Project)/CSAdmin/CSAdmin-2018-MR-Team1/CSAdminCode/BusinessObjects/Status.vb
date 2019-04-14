Option Strict On
Option Explicit On

Namespace BusinessObjects
    Public Class Status
        Private _IDStatus As Integer
        Private _StatusName As String

        Public Property IDStatus() As Integer
            Get
                Return _IDStatus
            End Get
            Set(ByVal value As Integer)
                _IDStatus = value
            End Set
        End Property

        Public Property StatusName As String
            Get
                Return _StatusName
            End Get
            Set(ByVal value As String)
                _StatusName = value
            End Set
        End Property

    End Class
End Namespace

