﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated. 
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class ManageUsernames

    '''<summary>
    '''ddlPageSize control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ddlPageSize As Global.CSAdmin.PagingDropDown

    '''<summary>
    '''SelectAllUsernames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents SelectAllUsernames As Global.System.Web.UI.WebControls.ObjectDataSource

    '''<summary>
    '''SelectDuplicateUsernames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents SelectDuplicateUsernames As Global.System.Web.UI.WebControls.ObjectDataSource

    '''<summary>
    '''SelectUsersNotInAD control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents SelectUsersNotInAD As Global.System.Web.UI.WebControls.ObjectDataSource

    '''<summary>
    '''SelectADByLastName control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents SelectADByLastName As Global.System.Web.UI.WebControls.ObjectDataSource

    '''<summary>
    '''btnDeletePending control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnDeletePending As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''ConfirmButtonExtender1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents ConfirmButtonExtender1 As Global.AjaxControlToolkit.ConfirmButtonExtender

    '''<summary>
    '''Panel1 control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents Panel1 As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''captionLastName control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents captionLastName As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''txtLastName control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents txtLastName As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''captionFirstName control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents captionFirstName As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''txtFirstName control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents txtFirstName As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''captionUsername control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents captionUsername As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''txtUsername control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents txtUsername As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''btnSearchNames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnSearchNames As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''btnClearSearch control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents btnClearSearch As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''rblDisplayOptions control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents rblDisplayOptions As Global.System.Web.UI.WebControls.RadioButtonList

    '''<summary>
    '''addLightbox control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents addLightbox As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''panelFindInAd control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents panelFindInAd As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''lblFindUser control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents lblFindUser As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''imgClose control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents imgClose As Global.System.Web.UI.WebControls.ImageButton

    '''<summary>
    '''gvADUsernames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents gvADUsernames As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''lblUnique control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents lblUnique As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''gvUsernames control.
    '''</summary>
    '''<remarks>
    '''Auto-generated field.
    '''To modify move field declaration from designer file to code-behind file.
    '''</remarks>
    Protected WithEvents gvUsernames As Global.System.Web.UI.WebControls.GridView
End Class
