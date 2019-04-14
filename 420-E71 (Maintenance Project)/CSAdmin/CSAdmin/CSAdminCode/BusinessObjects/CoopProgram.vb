' ====================================================
' Purpose: CoopProgram Business Object
' Author:  Renee Ghattas
' Date:   April 16, 2012
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
    Public Class CoopProgram

        Private _CoopProgramID As Integer
        Private _ProgramID As Integer
        Private _ShortName As String
        Private _LongName As String
        Private _isActive As Boolean
        Private _number As String

        Public Sub New()

        End Sub

        Public Property CoopProgramID() As Integer
            Get
                Return _CoopProgramID
            End Get
            Set(ByVal value As Integer)
                _CoopProgramID = value
            End Set
        End Property

        Public Property ProgramID() As Integer
            Get
                Return _ProgramID
            End Get
            Set(ByVal value As Integer)
                _ProgramID = value
            End Set
        End Property

        Public Property ShortName() As String
            Get
                Return _ShortName
            End Get
            Set(ByVal value As String)
                _ShortName = value
            End Set
        End Property

        Public Property LongName() As String
            Get
                Return _LongName
            End Get
            Set(ByVal value As String)
                _LongName = value
            End Set
        End Property


        Public Property isActive() As Boolean
            Get
                Return _isActive
            End Get
            Set(ByVal value As Boolean)
                _isActive = value
            End Set
        End Property

        Public Property Number() As String
            Get
                Return _number
            End Get
            Set(ByVal value As String)
                _number = value
            End Set
        End Property

    End Class
End Namespace
