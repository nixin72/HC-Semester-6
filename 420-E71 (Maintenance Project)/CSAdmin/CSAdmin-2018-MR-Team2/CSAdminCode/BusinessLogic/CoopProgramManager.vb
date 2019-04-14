Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessObjects.Collections

Namespace BusinessLogic
    Public Class CoopProgramManager

        Private Shared _DeleteError As Boolean

        ''' <summary>
        ''' A call is made to the database to retrieve a list of CoopPrograms which will be used to determine the type of 
        ''' user control to be displayed to the user
        ''' </summary>
        ''' <param name="ProgramCoopCoordinatorID">The CoopCoordinatorID is passed in as an Integer to recieve a list of CoopPrograms</param>
        ''' <returns>A list of CoopPrograms</returns>
        ''' <remarks>Created by Mikael-Raymond Paul</remarks>
        Public Shared Function SelectCoopProgram(ByVal ProgramCoopCoordinatorID As Integer) As CoopProgramList
            Return CoopProgramDB.SelectProgram(ProgramCoopCoordinatorID)
        End Function

        Public Shared Function SelectCoopProgramByID(ByVal CoopProgramID As Integer) As CoopProgram
            Return CoopProgramDB.SelectProgramByID(CoopProgramID)
        End Function

        ''' <summary>
        ''' Method that returns a CoopProgramList filled with all coop programs
        ''' </summary>
        ''' <returns>CoopProgramList with all coop programs</returns>
        ''' <remarks>Author: Kevin Brascoupe</remarks>
        Public Shared Function SelectAllCoopPrograms() As CoopProgramList
            Return CoopProgramDB.SelectAllPrograms()
        End Function

        ''' <summary>
        ''' Inserts a new coop program
        ''' </summary>
        ''' <remarks>Author: Marta Chmielowska</remarks>
        Public Shared Function InsertCoopProgram(ByVal ShortName As String, ByVal LongName As String, ByVal isActive As Boolean)
            Return CoopProgramDB.InsertCoopProgram(ShortName, LongName, isActive)
        End Function

        ''' <summary>
        ''' Updates an existing coop program
        ''' </summary>
        ''' <remarks>Author: Marta Chmielowska</remarks>
        Public Overloads Shared Sub UpdateCoopProgram(ByVal CoopProgramID As Integer, ByVal ShortName As String, ByVal LongName As String, ByVal isActive As Boolean)
            CoopProgramDB.UpdateCoopProgram(CoopProgramID, ShortName, LongName, isActive)

            ' If the Coop Program is now inactive, deactivate all their program versions
            If Not isActive Then
                Dim allProgramList As ProgramList = ProgramManager.SelectAllPrograms
                Dim programCoopList As New ProgramList
                For n As Integer = 0 To allProgramList.Count - 1 Step 1
                    If allProgramList.Item(n).coopprogramID = CoopProgramID Then
                        programCoopList.Add(allProgramList.Item(n))
                    End If
                Next

                For i As Integer = 0 To programCoopList.Count - 1 Step 1
                    ProgramManager.UpdateProgram(programCoopList.Item(i).programID, 0)
                Next

            End If
        End Sub


        Public Shared Property CoopProgramErrorDelete() As Boolean
            Get
                Return _DeleteError
            End Get
            Set(ByVal value As Boolean)
                _DeleteError = value
            End Set
        End Property


        ''' <summary>
        ''' Deletes an existing coop program
        ''' </summary>
        ''' <param name="aCoopProgram"></param>
        ''' <remarks>Author: Marta Chmielowska</remarks>
        Public Shared Sub DeleteCoopProgram(ByVal aCoopProgram As CoopProgram)
            CoopProgramDB.DeleteCoopProgram(aCoopProgram)

            If CoopProgramDB.CoopProgramErrorDelete = True Then
                CoopProgramErrorDelete = True
            Else
                CoopProgramErrorDelete = False
            End If
        End Sub

    End Class
End Namespace

