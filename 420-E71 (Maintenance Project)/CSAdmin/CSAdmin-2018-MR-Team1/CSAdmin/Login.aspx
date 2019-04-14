<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="Login.aspx.vb" Inherits="CSAdmin.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>
        Login</h2>
    <br />
    <div id="logincontrol">
        <asp:Login ID="LoginCSAdmin" runat="server" Width="445px">
            <LayoutTemplate>
                <table cellpadding="4">
                    <tr>
                        <td style="width: 90px;">
                            <asp:Label ID="lblUserName" runat="server" AssociatedControlID="UserName" CssClass="loginLabel">Username:</asp:Label>
                        </td>
                        <td class="loginData">
                            <asp:TextBox ID="UserName" runat="server" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"
                                CssClass="error">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px;">
                            <asp:Label ID="lblPassword" runat="server" AssociatedControlID="Password" CssClass="loginLabel">Password:</asp:Label>
                        </td>
                        <td class="loginData">
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                                CssClass="error">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px;">
                            &nbsp;
                        </td>
                        <td class="loginData">
                            <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 90px;">
                            &nbsp;
                        </td>
                        <td class="loginData">
                            <span class="error">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="True"></asp:Literal>
                                <asp:Label ID="SystemAdmin" runat="server"></asp:Label>
                            </span>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
    </div>
    <div id="loginpage">
        <p>
            Welcome to the CSAdmin System. In order to access the secure system, you
            must be a administrator at Heritage College. To login, enter your Heritage network username and password on the right.
        </p>
    </div>
</asp:Content>
