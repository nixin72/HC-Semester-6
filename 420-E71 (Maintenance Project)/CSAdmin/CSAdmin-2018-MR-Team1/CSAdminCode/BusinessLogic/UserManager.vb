Imports CSAdminCode.DataAccess
Imports CSAdminCode.BusinessObjects
Imports CSAdminCode.BusinessObjects.Collections
Imports System.ComponentModel




Namespace BusinessLogic

    <DataObject(True)> _
    Public Class UserManager

#Region "User Inserts"
        Public Shared Sub InsertNewStudentsFromClara(ByVal YearSemester As Integer)
            UserDB.InsertNewStudentsFromClara(YearSemester)
            UserDB.ActivateStudentsFromClara(YearSemester)
            UserDB.InsertNewCESStudentsFromClara(YearSemester)
        End Sub

        Public Shared Sub removeCESStudents(ByVal YearSemester As Integer)
            UserDB.deactivateStudents(YearSemester)
            UserDB.deleteCESStudents(YearSemester)
        End Sub
        Public Shared Sub InsertNewCoopStudentsFromClara(ByVal YearSemester As Integer)
            UserDB.InsertNewCoopStudentsFromClara(YearSemester)
        End Sub

        Public Shared Sub InsertAllNewFacultyFromClara(ByVal YearSemester As Integer)
            UserDB.InsertNewProgramCoordinatorsFromClara(YearSemester)
            UserDB.InsertNewTeachersFromClara(YearSemester)
            UserDB.InsertNewCESTeachersFromClara(YearSemester)
        End Sub

        Public Shared Sub removeFaculty(ByVal YearSemester As Integer)
            UserDB.DeactivateProgramCoordinators(YearSemester)
        End Sub
#End Region

#Region "Usernames"
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectAllUsernames(ByVal SortExpression As String, ByVal SearchLastName As String, ByVal SearchFirstName As String, ByVal SearchUsername As String) As UserList
            Return UserDB.SelectAllUsernames(SortExpression, SearchLastName, SearchFirstName, SearchUsername)
        End Function

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectUsersNotInAD(ByVal SortExpression As String, ByVal SearchLastName As String, ByVal SearchFirstName As String, ByVal SearchUsername As String) As UserList
            Return UserDB.SelectUsersNotInAD(SortExpression, SearchLastName, SearchFirstName, SearchUsername)
        End Function

        <DataObjectMethod(DataObjectMethodType.Update)> _
        Public Shared Sub UpdateUsername(ByVal IDUser As Integer, ByVal Username As String, ByVal ADUsern As String, ByVal IsActive As Boolean)
            UserDB.UpdateUsername(IDUser, Username, IsActive)
        End Sub

        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectDuplicateUsernames(ByVal SortExpression As String, ByVal SearchLastName As String, ByVal SearchFirstName As String, ByVal SearchUsername As String) As UserList
            Return UserDB.SelectDuplicateUsernames(SortExpression, SearchLastName, SearchFirstName, SearchUsername)
        End Function
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Public Shared Function SelectADByLastName(ByVal SearchLastName As String, ByVal IDUser As Integer, ByVal isActive As Boolean) As UserList
            Return UserDB.SelectADByLastName(SearchLastName, IDUser, isActive)
        End Function

        Public Shared Function CheckDuplicateUsername(ByVal IDUser As Integer, ByVal Username As String)
            Dim count As Integer = UserDB.CheckDuplicateUsername(IDUser, Username)
            Return count > 0
        End Function
#End Region

#Region "Teachers"
        <DataObjectMethod(DataObjectMethodType.Select)> _
        Shared Function SelectAllTeachers(ByVal AnSession As Integer) As UserList
            Return UserDB.SelectAllTeachers(AnSession)
        End Function
#End Region

        Public Shared Sub DeletePendingUsers()
            UserDB.DeletePendingUsers()
        End Sub
    End Class
End Namespace

