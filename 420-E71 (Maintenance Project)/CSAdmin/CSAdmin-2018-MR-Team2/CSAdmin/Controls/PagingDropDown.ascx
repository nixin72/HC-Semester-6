<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PagingDropDown.ascx.vb" Inherits="CSAdmin.PagingDropDown" %>
<asp:Label ID="lblPaging" runat="server" Text="Results Per Page:" CssClass="label"></asp:Label>&nbsp;
<asp:DropDownList
    ID="ddlPageSize" runat="server" AutoPostBack="True">
    <asp:ListItem>5</asp:ListItem>
    <asp:ListItem>10</asp:ListItem>
    <asp:ListItem Selected="True">15</asp:ListItem>
    <asp:ListItem>20</asp:ListItem>
    <asp:ListItem>25</asp:ListItem>
    <asp:ListItem>30</asp:ListItem>
    <asp:ListItem Value="99999">All</asp:ListItem>
</asp:DropDownList>
