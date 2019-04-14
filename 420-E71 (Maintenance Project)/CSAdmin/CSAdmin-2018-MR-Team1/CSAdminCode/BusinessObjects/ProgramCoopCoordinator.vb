' ====================================================
' Purpose: Program Coop Coordinator Business Object
' Author:  Josh Bryans
' Date:    January 28, 2011
'
' History:
' 
' Name              Date            Description
' ====================================================
' Josh Bryans       Feb 2           Added Default constuctor 
' ----------------------------------------------------
' Awet Tekeste      Mar 22          Changed _coopProgramID to _coopProgram
' ====================================================

Namespace BusinessObjects
    Public Class ProgramCoopCoordinator

        Private _ProgramCoopCoordinatorID As Integer
        Private _CoopCoordinator As CoopCoordinator
        Private _CoopProgram As CoopProgram

        Public Sub New()

        End Sub

        Public Property ProgramCoopCoordinatorID As Integer
            Get
                Return _ProgramCoopCoordinatorID
            End Get
            Set(ByVal value As Integer)
                _ProgramCoopCoordinatorID = value
            End Set
        End Property

        Public Property CoopCoordinator As CoopCoordinator
            Get
                Return _CoopCoordinator
            End Get
            Set(ByVal value As CoopCoordinator)
                _CoopCoordinator = value
            End Set
        End Property

        Public Property CoopProgram As CoopProgram
            Get
                Return _CoopProgram
            End Get
            Set(ByVal value As CoopProgram)
                _CoopProgram = value
            End Set
        End Property

        '*********************************************************'ReadOnly values from nested objects*******************************************************************

        Public ReadOnly Property CoopCoordinatorID As Integer
            Get
                Return _CoopCoordinator.CoopCoordinatorID
            End Get
        End Property

        Public ReadOnly Property CoopProgramID As Integer
            Get
                Return _CoopProgram.CoopProgramID
            End Get
        End Property

        Public ReadOnly Property CoopCoordinatorName As String
            Get
                Return _CoopCoordinator.FullName
            End Get
        End Property

        Public ReadOnly Property CoopProgramName As String
            Get
                Return _CoopProgram.LongName
            End Get
        End Property

    End Class
End Namespace