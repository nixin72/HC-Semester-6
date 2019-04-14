Namespace BusinessObjects
    Public Class Language

        Private _LanguageID As Integer
        Private _Language As String

        Public Property LanguageID() As Integer
            Get
                Return _LanguageID
            End Get
            Set(ByVal value As Integer)
                _LanguageID = value
            End Set
        End Property

        Public Property Language() As String
            Get
                Return _Language
            End Get
            Set(ByVal value As String)
                _Language = value
            End Set
        End Property

    End Class
End Namespace