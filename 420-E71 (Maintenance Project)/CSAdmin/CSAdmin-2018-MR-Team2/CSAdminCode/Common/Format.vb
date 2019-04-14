Namespace Common
    Public Class Format
        ''' <summary>
        ''' Method that formats date. eg. Date: 2011-05-14 Formatted: 2011-MAY-14
        ''' </summary>
        ''' <param name="EvalDate">Date to be formatted</param>
        ''' <returns>Formatted date</returns>
        ''' <remarks>Author: Renee Ghattas</remarks>
        Public Shared Function FormatDate(ByVal EvalDate As Date) As String
            Dim theResult As String = String.Empty

            theResult = EvalDate.ToString("yyyy-MMM-dd")

            Return theResult
        End Function

        ''' <summary>
        ''' Method that formats year and semester. eg. YearSemester: 20121 Formatted: Winter 2012
        ''' </summary>
        ''' <param name="YearSemester">Year and Semester to be formatted</param>
        ''' <returns>Formatted year and semester</returns>
        ''' <remarks>Author: Renee Ghattas</remarks>
        Public Shared Function FormatSemester(ByVal YearSemester As Integer) As String
            Dim theResult As String = String.Empty
            Dim snum As Integer = CType(Right(YearSemester, 1), Integer)
            Dim y As String = YearSemester.ToString.Substring(0, YearSemester.ToString.Length - 1)

            If snum = 0 Then
                theResult = Nothing
            ElseIf snum = 1 Then
                theResult = "Winter " & y
            ElseIf snum = 2 Then
                theResult = "Summer " & y
            ElseIf snum = 3 Then
                theResult = "Fall " & y
            End If

            Return theResult
        End Function

        ''' <summary>
        ''' Method that formats first and last name. eg. FirstName: Renee LastName: Ghattas Formatted: Ghattas, Renee
        ''' </summary>
        ''' <param name="FirstName">First name to be formatted</param>
        ''' <param name="LastName">Last name to be formatted</param>
        ''' <returns>Formatted full name</returns>
        ''' <remarks>Author: Renee Ghattas</remarks>
        Public Shared Function FormatLastFirst(ByVal FirstName As String, ByVal LastName As String) As String
            Dim theResult As String = String.Empty

            theResult = LastName & ", " & FirstName

            Return theResult
        End Function

        ''' <summary>
        ''' Method that formats first and last name. eg. FirstName: Renee LastName: Ghattas Formatted: Renee Ghattas
        ''' </summary>
        ''' <param name="FirstName">First name to be formatted</param>
        ''' <param name="LastName">Last name to be formatted</param>
        ''' <returns>Formatted full name</returns>
        ''' <remarks>Author: Renee Ghattas</remarks>
        Public Shared Function FormatFirstLast(ByVal FirstName As String, ByVal LastName As String) As String
            Dim theResult As String = String.Empty

            theResult = FirstName & " " & LastName

            Return theResult
        End Function
    End Class
End Namespace

