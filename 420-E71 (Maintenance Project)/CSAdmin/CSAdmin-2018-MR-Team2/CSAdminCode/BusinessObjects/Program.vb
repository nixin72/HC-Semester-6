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


Imports CSAdminCode.BusinessObjects
Public Class Program
    Private _programID As Integer
    Private _Number As String
    Private _coopProgram As CoopProgram
    Private _longName As String
    Private _coopprogramID As Integer
    Private _IsActive As Boolean

    Public Sub New()
        _coopProgram = New CoopProgram
    End Sub

    Public Property programID() As Integer
        Get
            Return _programID
        End Get
        Set(ByVal value As Integer)
            _programID = value
        End Set
    End Property

    Public Property Number() As String
        Get
            Return _Number
        End Get
        Set(ByVal value As String)
            _Number = value
        End Set
    End Property

    Public Property coopProgram() As CoopProgram
        Get
            Return _coopProgram
        End Get
        Set(ByVal value As CoopProgram)
            _coopProgram = value
        End Set
    End Property

    Public Property longName() As String
        Get
            If String.IsNullOrEmpty(_longName) Then
                Return coopProgram.LongName()
            Else
                Return _longName
            End If


        End Get
        Set(ByVal value As String)
            _longName = value

        End Set
    End Property

    Public Property coopprogramID() As Integer
        Get

            Return coopProgram.CoopProgramID
        End Get
        Set(ByVal value As Integer)
            _coopprogramID = value

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
End Class
