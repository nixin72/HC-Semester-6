<%@ Page Title="CSAdmin - User Roles" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManageUserRoles.aspx.vb" Inherits="CSAdmin.ManageUserRoles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>Roles</h2>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ObjectDataSource ID="odsOtherRoles" runat="server" OldValuesParameterFormatString="{0}"
                SelectMethod="SelectAllUserWithSpecialRoles" TypeName="CSAdminCode.BusinessLogic.RoleManager"
                DeleteMethod="DeleteUserRole" InsertMethod="InsertUserRole" UpdateMethod="UpdateUsername">
                <SelectParameters>
                    <asp:Parameter Name="IDRoles" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="IDEmploye" Type="Int32" />
                    <asp:Parameter Name="RoleCode" Type="String" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="IDEmploye" Type="Int32" />
                    <asp:Parameter Name="RoleCode" Type="String" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="IDUser" Type="Int32" />
                    <asp:Parameter Name="Username" Type="String" />
                </UpdateParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsRoles" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="SelectAllRoles"
                TypeName="CSAdminCode.BusinessLogic.RoleManager"></asp:ObjectDataSource>
            <div class="floatLeft">
                <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CssClass="gridViewLeft"
                    SelectedRowStyle-CssClass="gridSelectedRow" HeaderStyle-CssClass="gridHead" AllowSorting="True"
                    DataKeyNames="IDEmploye" DataSourceID="odsOtherRoles" ShowFooter="True">
                    <Columns>
                        <%-- IDUser Column--%>
                        <asp:TemplateField HeaderText="IDUser" Visible="False">
                            <%-- %>SortExpression="IDUser"--%>
                            <ItemTemplate>
                                <asp:Label ID="lblIDUser" runat="server" Text='<%# Bind("Employee.IDUser") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- IDEmploye Column--%>
                        <asp:TemplateField HeaderText="IDEmploye" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIDEmploye" runat="server" Text='<%# Bind("IDEmploye") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--Displays the name of the Employee--%>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="Name" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlEmployee" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Username">
                            <ItemTemplate>
                                <asp:Label ID="Username" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUsername" runat="server"
                                    Text='<%# Bind("Username") %>' MaxLength="60"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--  Displays the role --%>
                        <asp:TemplateField HeaderText="Role">
                            <ItemTemplate>
                                <asp:Label ID="lblRole" runat="server" Text='<%# Bind("Role.Description") %>'></asp:Label><asp:Label
                                    ID="lblRoleCode" runat="server" Text='<%# Bind("Role.Code")  %>' Visible="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="ddlRoles" runat="server">
                                </asp:DropDownList>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- Adds 2 buttons at the end of each row --%>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Edit"></asp:LinkButton>
                                | 
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Unassign"></asp:LinkButton>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="DeleteButton"
                                    ConfirmText="Are you sure you want to unassign this user from its current role?">
                                </asp:ConfirmButtonExtender>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update" ValidationGroup="EditEducationType"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="lnkBtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button Text="Add" CommandName="Insert" CausesValidation="true" runat="server"
                                    ID="btInsert" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmployeeList" CssClass="label" runat="server" Text="Employees : " />
                        <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblRoleList" CssClass="label" runat="server" Text="Roles : " />
                        <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <br />
                        <br />
                        <asp:Button ID="btnSend" Text="Add" runat="server" CommandName="EmptyInsert" UseSubmitBehavior="False" />
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gridHead" />
                    <SelectedRowStyle CssClass="gridSelectedRow" />
                </asp:GridView>
                <asp:Label ID="lblUnique" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div class="floatRight">
                <asp:CheckBoxList ID="chkRoles" runat="server" Height="34px"
                    DataSourceID="odsRoles" DataTextField="Description"
                    DataValueField="IDRole" AutoPostBack="True">
                </asp:CheckBoxList>
            </div>
            <asp:Label runat="server" ID="lblErrorRoles" Text="" Visible="true" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
