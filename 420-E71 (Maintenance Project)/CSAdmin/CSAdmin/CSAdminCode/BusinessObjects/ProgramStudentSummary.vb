' ====================================================
' Purpose: ProgramStudentSummary Business Object
' Author:  Kevin Brascoupe
' Date:    March 31, 2011
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

    ''' <summary>
    ''' This class will hold the number of new students in a program for a given year
    ''' </summary>
    ''' <remarks>Used in the Clara extraction process in the WPF windows application</remarks>
    Public Class ProgramStudentSummary

        Private _CoopProgram As CoopProgram
        Private _Program As Program
        Private _NumNewStudentsInProgram As Integer

        Public Property CoopProgram() As CoopProgram
            Get
                Return _CoopProgram
            End Get
            Set(ByVal value As CoopProgram)
                _CoopProgram = value
            End Set
        End Property

        Public Property Program() As Program
            Get
                Return _Program
            End Get
            Set(ByVal value As Program)
                _Program = value
            End Set
        End Property

        Public Property NumNewStudentsInProgram() As Integer
            Get
                Return _NumNewStudentsInProgram
            End Get
            Set(ByVal value As Integer)
                _NumNewStudentsInProgram = value
            End Set
        End Property

    End Class
End Namespace

