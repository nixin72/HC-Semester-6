Imports CSAdminCode.BusinessObjects
Namespace BusinessLogic
    Public Class SemesterManager

        Shared Function SelectSettings() As Semester
            Return SemesterDB.SelectSettings()
        End Function

        Shared Sub UpdateCurrentSemester(newYearSemester As Integer, theEndDate As Date)
            SemesterDB.UpdateCurrentSemester(newYearSemester, theEndDate)
        End Sub

    End Class
End Namespace