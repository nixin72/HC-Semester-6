Imports CSAdminCode.BusinessLogic

Public Class ManageLanguages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            gvLanguages.DataBind()
            setDefault()
        End If
    End Sub

    Public Sub setDefaultsOnBinding() Handles gvLanguages.DataBound
        setDefault()
    End Sub

    Protected Sub gvLanguages_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvLanguages.RowCommand
        If e.CommandName = "Insert" Then
            odsLanguages.Insert()
        End If
        If e.CommandName = "Default" Then
            Dim gv As GridView = DirectCast(sender, GridView)
            Dim lan As GridViewRow = gv.Rows.Item(gv.EditIndex)
            Dim rdb As RadioButton = lan.FindControl("rbLanguageDefault")
            rdb.Checked = True
            Dim uid As String = lan.UniqueID
            For Each gvr As GridViewRow In gv.Rows
                If gvr.UniqueID IsNot uid Then
                    Dim rdb2 As RadioButton = gvr.FindControl("rbLanguageDefault")
                    rdb2.Checked = False
                End If
            Next
            LanguageManager.SetDefaultLanguage(CInt(rdb.GroupName))
        End If
        If e.CommandName = "Update" Then
            Dim languageID As String = CType(gvLanguages.Rows(gvLanguages.EditIndex).FindControl("LanguageID"), Label).Text
            Dim language As String = CType(gvLanguages.Rows(gvLanguages.EditIndex).FindControl("txtEditLanguage"), TextBox).Text
            LanguageManager.UpdateLanguage(languageID, language)
        End If
    End Sub

    'Private Sub rbLanguageDefault(ByVal sender As Object, ByVal e As GridViewUpdatedEventArgs) Handles gvLanguages.RowUpdated
    '    setDefault()
    'End Sub

    Private Sub setDefault()
        Dim defaultLanguage As String = If(Not IsNothing(LanguageManager.SelectDefaultLanguage()), LanguageManager.SelectDefaultLanguage().Item(0).LanguageID.ToString, "No Default Found")
        For Each gvr As GridViewRow In gvLanguages.Rows
            Dim rdb2 As RadioButton = gvr.FindControl("rbLanguageDefault")
            Dim editLan As Label = CType(gvr.FindControl("LanguageID"), Label)
            If editLan.Text = defaultLanguage Then
                rdb2.Checked = True
            End If
        Next
    End Sub

    'Private Sub odsLanguages_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsLanguages.Updating
    '    odsLanguages.UpdateParameters.Clear()
    '    Dim language As String = CType(gvLanguages.FindControl("txtEditLanguage"), TextBox).Text
    '    Dim languageID As String = CType(gvLanguages.FindControl("LanguageIDEdit"), Label).Text

    '    e.InputParameters("LanguageID") = languageID
    '    e.InputParameters("Language") = language
    'End Sub

    Protected Sub gvLanguages_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs) Handles gvLanguages.RowUpdating
        odsLanguages.UpdateParameters.Clear()

        Dim languageID As String = CType(gvLanguages.Rows(gvLanguages.EditIndex).FindControl("LanguageID"), Label).Text
        Dim language As String = CType(gvLanguages.Rows(gvLanguages.EditIndex).FindControl("txtEditLanguage"), TextBox).Text

        odsLanguages.UpdateParameters.Add("LanguageID", DbType.Int32, languageID)
        odsLanguages.UpdateParameters.Add("Language", DbType.String, language)
    End Sub

    Protected Sub gvLanguages_RowUpdated(ByVal sender As Object, ByVal e As EventArgs) Handles gvLanguages.RowUpdated
        BindData()
    End Sub


    Private Sub odsLanguages_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles odsLanguages.Inserting
        odsLanguages.InsertParameters.Clear()
        Dim Language As String = CType(gvLanguages.FooterRow.FindControl("txtAddLanguage"), TextBox).Text
        e.InputParameters("Language") = Language
    End Sub

    Private Sub odsLanguages_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles odsLanguages.Inserted
        BindData()
    End Sub

    Protected Sub gvLanguages_Deleting(sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvLanguages.RowDeleting
        odsLanguages.DeleteParameters.Clear()
        Dim languageID As String = CType(gvLanguages.Rows(e.RowIndex).FindControl("LanguageID"), Label).Text
        odsLanguages.DeleteParameters.Add("LanguageID", DbType.Int32, languageID)
    End Sub

    Protected Sub odsLanguages_Deleted(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsLanguages.Deleted
        BindData()
    End Sub

    Protected Sub BindData()
        odsLanguages.DataBind()
        gvLanguages.DataBind()
    End Sub

    'Private Sub btnDeleteL_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnDeleteL.Click
    '    Dim aLanguage As New Language

    '    aLanguage = grdLanguages.SelectedItem

    '    If (MessageBox.Show("Are you sure you want to delete this Language?", "Confirmation", MessageBoxButton.YesNo) = MessageBoxResult.Yes) Then
    '        'Remove from collection in order to refresh the data grid
    '        languagesList.Remove(aLanguage)
    '        grdLanguages.Items.Refresh()

    '        'remove from database
    '        LanguageManager.DeleteLanguage(aLanguage)
    '        selectFirstLanguageRow()
    '        grdLanguages.Items.Refresh()

    '    End If
    'End Sub

    'Private Sub btnAddUpdate_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btnAddUpdate.Click
    '    Dim aLanguage As New Language
    '    aLanguage.Language = txtLanguage.Text
    '    Dim errCount As Integer = 0
    '    Dim errMsgLanguage As String = String.Empty

    '    If txtLanguage.Text = String.Empty Then
    '        errMsgLanguage &= "Please enter a language." & vbCrLf
    '        errCount += 1
    '    End If

    '    If errCount > 0 Then
    '        lblErrLanguage.Content = errMsgLanguage
    '    Else
    '        lblErrLanguage.Content = String.Empty

    '        If isAddModeLanguage Then
    '            'add to collection in order to refresh the grid
    '            languagesList.Add(aLanguage)
    '            grdLanguages.Items.Refresh()

    '            'add to database
    '            LanguageManager.InsertLanguage(aLanguage)

    '            isAddModeLanguage = False
    '        Else
    '            Dim anOldLanguage As New Language
    '            anOldLanguage = grdLanguages.SelectedItem
    '            aLanguage = grdLanguages.SelectedItem

    '            aLanguage.Language = txtLanguage.Text

    '            languagesList.Remove(anOldLanguage)
    '            languagesList.Add(aLanguage)
    '            grdLanguages.Items.Refresh()

    '            LanguageManager.UpdateLanguage(aLanguage)

    '            txtLanguage.Text = String.Empty

    '        End If



    '        btnAddUpdate.Content = "Update"
    '        btnAddNew.IsEnabled = True
    '        btnCancelL.Visibility = Windows.Visibility.Hidden
    '    End If

    'End Sub


    
End Class