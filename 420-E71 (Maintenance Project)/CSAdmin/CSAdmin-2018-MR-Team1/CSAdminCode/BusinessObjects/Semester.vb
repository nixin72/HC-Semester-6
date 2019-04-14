Namespace BusinessObjects
    Public Class Semester
        Private _YearSemester As Integer
        Private _SemesterEndDate As Date
        Private _CurrentYear As Integer

        Public Property YearSemester() As String
            Get
                Return _YearSemester
            End Get
            Set(ByVal value As String)
                _YearSemester = value
            End Set
        End Property

        Public Property SemesterEndDate As Date
            Get
                Return _SemesterEndDate
            End Get
            Set(ByVal value As Date)
                _SemesterEndDate = value
            End Set
        End Property

        Public Property CurrentYear As Integer
            Get
                Return _CurrentYear
            End Get
            Set(ByVal value As Integer)
                _CurrentYear = value
            End Set
        End Property
    End Class
End Namespace

