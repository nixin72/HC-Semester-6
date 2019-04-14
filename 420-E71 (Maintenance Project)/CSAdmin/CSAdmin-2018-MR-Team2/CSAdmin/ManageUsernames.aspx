<%@ Page Title="CSAdmin - Usernames" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ManageUsernames.aspx.vb" Inherits="CSAdmin.ManageUsernames" %>

<%@ Register Src="Controls/PagingDropDown.ascx" TagName="DDLPageSize" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="notification">
        <uc2:DDLPageSize ID="ddlPageSize" runat="server" Visible="True" Control="gvUsernames"></uc2:DDLPageSize>
    </div>
    <h2>Usernames</h2>
    <asp:ObjectDataSource ID="SelectAllUsernames" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectAllUsernames" TypeName="CSAdminCode.BusinessLogic.UserManager"
        UpdateMethod="UpdateUsername" SortParameterName="SortExpression">
        <SelectParameters>
            <asp:Parameter Name="SortExpression" Type="String" />
            <asp:Parameter Name="SearchLastName" Type="String" />
            <asp:Parameter Name="SearchFirstName" Type="String" />
            <asp:Parameter Name="SearchUsername" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="ADUsern" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SelectDuplicateUsernames" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectDuplicateUsernames" TypeName="CSAdminCode.BusinessLogic.UserManager"
        UpdateMethod="UpdateUsername" SortParameterName="SortExpression">
        <SelectParameters>
            <asp:Parameter Name="SortExpression" Type="String" />
            <asp:Parameter Name="SearchLastName" Type="String" />
            <asp:Parameter Name="SearchFirstName" Type="String" />
            <asp:Parameter Name="SearchUsername" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="ADUsern" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SelectUsersNotInAD" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectUsersNotInAD" TypeName="CSAdminCode.BusinessLogic.UserManager"
        SortParameterName="SortExpression" UpdateMethod="UpdateUsername">
        <SelectParameters>
            <asp:Parameter Name="SortExpression" Type="String" />
            <asp:Parameter Name="SearchLastName" Type="String" />
            <asp:Parameter Name="SearchFirstName" Type="String" />
            <asp:Parameter Name="SearchUsername" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="ADUsern" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SelectADByLastName" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="SelectADByLastName" TypeName="CSAdminCode.BusinessLogic.UserManager">
        <SelectParameters>
            <asp:Parameter Name="SearchLastName" Type="String" />
            <asp:Parameter Name="IDUser" Type="Int32" />
            <asp:Parameter Name="isActive" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Button ID="btnDeletePending" runat="server" Text="Delete Pending Users" Style="float: right" />
    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnDeletePending"
        ConfirmText="Are you sure you want to delete all pending users?" />
    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearchNames">
        <table style="width: 50%; margin-left: auto; margin-right: auto;">
            <tr>
                <td style="font-weight: bold; text-align: center;">Search
                </td>
                <td style="font-weight: bold; text-align: center;">Display Options
                </td>
            </tr>
            <tr>
                <td>
                    <table style="margin-left: auto; margin-right: auto; border: 1px solid #BF8100; margin-bottom: 20px;">
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="captionLastName" runat="server" Text="Last Name: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLastName" runat="server" ViewStateMode="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="captionFirstName" runat="server" Text="First Name: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFirstName" runat="server" ViewStateMode="Disabled"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="captionUsername" runat="server" Text="Username: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsername" runat="server" ViewStateMode="Disabled"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td colspan="2" style="display: inline !important; text-align: center;">
                                <asp:Button ID="btnSearchNames" runat="server" Text="Search" />&nbsp;|&nbsp;<asp:LinkButton
                                    ID="btnClearSearch" runat="server">Clear</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblDisplayOptions" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="1" Selected="True">Show All</asp:ListItem>
                        <asp:ListItem Value="2">Show Duplicate Usernames</asp:ListItem>
                        <asp:ListItem Value="3">Show Usernames Not In AD</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    
        <div class="lightbox" runat="server" id="addLightbox" visible="false">
            <asp:Panel ID="panelFindInAd" runat="server" CssClass="FindADUsersLightbox">
                <div id="FindInAD" class="InnerFindADUsersLightbox">
                    <h2>Find "<asp:Label ID="lblFindUser" runat="server" Text=""></asp:Label>" in Active Directory <span style="float: right">
                        <asp:ImageButton ID="imgClose" runat="server" ImageUrl="Images/close.png" Width="25"
                            Height="25" />
                    </span>
                    </h2>
                    <asp:GridView ID="gvADUsernames" runat="server" AutoGenerateColumns="False" Visible="false"
                        AllowPaging="True" CssClass="gridView" HeaderStyle-CssClass="gridViewHeaders">
                        <Columns>
                            <asp:TemplateField HeaderText="Full Name">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LastName") & ", " & Eval("FirstName") %>'></asp:Label>
                                    <asp:Label ID="lblIDUser" runat="server" Text='<%# Bind("IDUser") %>' Visible="false"></asp:Label>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsActive") %>' Visible="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Username" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AD Username">
                                <ItemTemplate>
                                    <asp:Label ID="lblADUsername" runat="server" Text='<%# Bind("ADUsern") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartment" runat="server" Text='<%# Bind("Department") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnReplace" runat="server" Text="Replace" CommandName="Replace"
                                        CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No users returned
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="gridViewHeaders"></HeaderStyle>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
        <asp:Label ID="lblUnique" runat="server" Text="" ForeColor="Red"></asp:Label>
        <asp:GridView ID="gvUsernames" runat="server" AutoGenerateColumns="False" DataSourceID="SelectAllUsernames"
            AllowPaging="True" AllowSorting="True" PageSize="15" CssClass="gridView" HeaderStyle-CssClass="gridViewHeaders">
            <Columns>
                <asp:TemplateField HeaderText="IDUser" Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="IDUser" runat="server" Text='<%# Bind("IDUser") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="IDUser" runat="server" Text='<%# Bind("IDUser") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Full Name" SortExpression="Name">
                    <ItemTemplate>
                        <asp:Label ID="lblFullname" runat="server" Text='<%# Eval("LastName") & ", " & Eval("FirstName") %>'></asp:Label>
                        <asp:Label runat="server" ID="lblLastName" Text='<%# Eval("LastName") %>' Visible="False"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username" SortExpression="Username">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUsername" runat="server" Text='<%# Bind("Username") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AD Username" SortExpression="Username">
                    <ItemTemplate>
                        <asp:Label ID="lblADUserName" runat="server" Text='<%# Bind("ADUsern") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is Active">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsActive") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" />
                <asp:TemplateField>
                    <ItemTemplate>
                        |&nbsp;<asp:LinkButton ID="btnReplace" runat="server" Text="Replace" CommandName="Replace"
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />&nbsp;|&nbsp;
                        <asp:LinkButton ID="btnFindInAD" runat="server" Text="Find In AD" CommandName="FindInAD"
                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No users returned
            </EmptyDataTemplate>
            <HeaderStyle CssClass="gridViewHeaders"></HeaderStyle>
        </asp:GridView>
    </asp:Panel>
    <br />
    <br />
</asp:Content>
