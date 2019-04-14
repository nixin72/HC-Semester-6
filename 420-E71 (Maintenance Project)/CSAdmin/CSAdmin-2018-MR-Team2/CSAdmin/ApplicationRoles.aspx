<%@ Page Title="CSAdmin - Application Roles" Language="vb" AutoEventWireup="false"
    MasterPageFile="~/Site.Master" CodeBehind="ApplicationRoles.aspx.vb" Inherits="CSAdmin.ApplicationRoles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
    <script type="text/javascript" src="Scripts/jquery-color.js"></script>

    <style type="text/css">
        body {
            overflow-y: scroll;
        }

        .vtabActive {
            font-family: sans-serif;
            border: 1px solid #fff;
            border-radius: 1em;
            /*background-color:#ccc;*/
            padding: 0 2em 0 2em;
        }

            .vtabActive > span {
                font-weight: bold;
                cursor: pointer;
                display: block;
            }

        .vtabActiveChild {
            font-weight: normal;
            display: block;
        }

        .vtabIdle {
            cursor: pointer;
            border: 1px solid #fff;
            border-radius: 1em;
            /*background-color:#666;*/
            /*color:#fff;*/
            font-family: sans-serif;
            padding: 0 2em 0 2em;
        }

            .vtabIdle > span {
                display: block;
            }

        .vtabIdleChild {
            display: none;
        }

        .tabTitle {
            font-weight: bold;
            cursor: pointer;
        }

        .nonTitle {
            border: none;
        }

        .chkbox {
            height: 29px !important;
        }

            .chkbox > td {
                padding: 4.5px !important;
            }

        .applist {
            display: inline;
        }

            .applist li {
                display: inline;
                float: left;
            }

                .applist li:not(:last-child):after {
                    content: ",\00A0";
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server"
    ViewStateMode="Enabled">
    <h2>Application Roles
    </h2>
    <asp:ObjectDataSource ID="odsApplications" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="SelectAllApplications" TypeName="CSAdminCode.BusinessLogic.RoleManager"
        UpdateMethod="updateApplication" InsertMethod="addApplication" DeleteMethod="deleteApplication">
        <InsertParameters>
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="description" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDApplication" Type="Int32" />
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="description" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="IDApplication" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsRoles" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="SelectAllRoles" TypeName="CSAdminCode.BusinessLogic.RoleManager"
        UpdateMethod="updateRole" InsertMethod="addRole" DeleteMethod="DeleteRole">
        <InsertParameters>
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="description" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDRole" Type="Int32" />
            <asp:Parameter Name="code" Type="String" />
            <asp:Parameter Name="description" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="IDRole" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsApplicationRoles" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="SelectAllApplicationRoles" TypeName="CSAdminCode.BusinessLogic.RoleManager"
        UpdateMethod="updateApplicationRole" InsertMethod="addApplicationRole">
        <InsertParameters>
            <asp:Parameter Name="IDApplication" Type="Int32" />
            <asp:Parameter Name="IDRole" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="gvRoles" Name="IDRole" PropertyName="SelectedValue"
                Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDApplicationRole" Type="Int32" />
            <asp:Parameter Name="isActive" Type="Boolean" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <h3>Applications</h3>
    <asp:GridView ID="gvApplications" runat="server" AutoGenerateColumns="False" CssClass="gridView"
        DataKeyNames="IDApplication" DataSourceID="odsApplications" EmptyDataText="No Applications Exist"
        ShowFooter="True" ShowHeaderWhenEmpty="True" ClientIDMode="Static">
        <Columns>
            <asp:BoundField DataField="IDApplication" HeaderText="IDApplication" ReadOnly="True"
                SortExpression="IDApplication" Visible="False" />
            <asp:TemplateField Visible="false"><%-- Magical data source --%>
                <ItemTemplate>
                    <asp:ObjectDataSource ID="odsRolesByApplication" runat="server" OldValuesParameterFormatString="{0}"
                        SelectMethod="SelectRolesByApplication" TypeName="CSAdminCode.BusinessLogic.RoleManager" OnInserted="ForceStyle"
                        UpdateMethod="updateApplicationRole" InsertMethod="addApplicationRole" DeleteMethod="deleteApplicationRole">
                        <InsertParameters>
                            <asp:Parameter Name="IDApplication" Type="Int32" />
                            <asp:Parameter Name="IDRole" Type="Int32" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:Parameter Name="IDApplication" Type="Int32" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="IDApplicationRole" Type="Int32" />
                            <asp:Parameter Name="isActive" Type="Boolean" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="IDApplicationRole" Type="Int32" />
                        </DeleteParameters>
                    </asp:ObjectDataSource>
                    <asp:Label ID="lblIDApplication" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Application/Role Description" SortExpression="Description"><%-- Description col --%>
                <ItemTemplate>
                    <asp:Label ID="Label1" CssClass="tabTitle" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    <br />
                    <asp:GridView ID="gvApplicationRoleDescriptions" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDApplicationRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="true" CssClass="nonTitle">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblAppRoleDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlDescription" runat="server" CssClass="ddlDescription" DataSourceID="odsRoles" DataValueField="IDRole" DataTextField="Description">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditDescription" runat="server" Text='<%# Bind("Description") %>' Width="15em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEditDescription" runat="server" ControlToValidate="txtEditDescription"
                        ErrorMessage="An Application Description is required." ForeColor="Red" Display="None" ValidationGroup="ApplicationEdit">*</asp:RequiredFieldValidator>
                    <br />
                    <asp:GridView ID="gvApplicationRoleDescriptions" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDApplicationRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="true" CssClass="nonTitle">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblAppRoleDesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlDescription" runat="server" CssClass="ddlDescription" DataSourceID="odsRoles" DataValueField="IDRole" DataTextField="Description">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="tbApplicationDesc" runat="server" Width="15em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvApplicationDesc" runat="server" ControlToValidate="tbApplicationDesc"
                        ErrorMessage="An Application Description is required." ForeColor="Red" Display="None" ValidationGroup="ApplicationAdd">*</asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Application/Role Code" SortExpression="Code"><%-- Code col --%>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" CssClass="tabTitle" Text='<%# Bind("Code") %>'></asp:Label>
                    <br />
                    <asp:GridView ID="gvApplicationRoleCodes" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDApplicationRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="True" CssClass="nonTitle">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditApplicationCode" runat="server" Text='<%# Bind("Code") %>' MaxLength="3" Width="3em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEditApplicationCod" runat="server" ControlToValidate="txtEditApplicationCode"
                        ErrorMessage="An Application Code is required." ForeColor="Red" Display="None" ValidationGroup="ApplicationEdit">*</asp:RequiredFieldValidator>
                    <br />
                    <asp:GridView ID="gvApplicationRoleCodes" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDApplicationRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="True" CssClass="nonTitle">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlCode" runat="server" CssClass="ddlCode" DataSourceID="odsRoles" DataValueField='IDRole' DataTextField="Code">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="tbApplicationCode" runat="server" MaxLength="3" Width="3em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvApplicationCode" runat="server" ControlToValidate="tbApplicationCode"
                        ErrorMessage="An Application Code is required." ForeColor="Red" Display="None" ValidationGroup="ApplicationAdd">*</asp:RequiredFieldValidator>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="Active" ControlStyle-Height="29px"><%-- Active col --%>
                <ItemTemplate>
                    <br />
                    <asp:GridView ID="gvApplicationRoleActive" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDApplicationRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="True" CssClass="nonTitle" RowStyle-CssClass="chkbox">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDRole" runat="server" Text='<%# Eval("IDRole") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("isActive") %>' Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("isActive") %>' Enabled="true" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False"><%-- Command col --%>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="true" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    | 
                            <asp:LinkButton ID="btnDeleteApplication" runat="server" Font-Bold="true" CausesValidation="False" CommandName="Delete"
                                Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this application?')"></asp:LinkButton>
                    <br />
                    <asp:GridView ID="gvApplicationRoleCommand" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDApplicationRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="True" CssClass="nonTitle">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIDApplicationRole" runat="server" Text='<%# Bind("IDApplicationRole") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditAppRole" runat="server" CausesValidation="False" CommandName="Edit"
                                        Text="Edit" CommandArgument='<%# Eval("IDRole") %>' OnClick="gvApplicationRoleCommand_Edit"></asp:LinkButton> | 
                                    <asp:LinkButton ID="btnDeleteAppRole" runat="server" CausesValidation="False" CommandName="Delete"
                                        Text="Delete" CommandArgument='<%# Eval("IDApplicationRole")%>' 
                                        OnClientClick="return confirm('Are you sure you want to delete this application role?')" OnClick="gvApplicationRoleCommand_Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="btnUpdateAppRole" runat="server" CausesValidation="true" CommandName="Update"
                                        Text="Update" CommandArgument='<%# Eval("IDRole") %>' OnClick="gvApplicationRoleCommand_Update"></asp:LinkButton>
                                    &nbsp;
                                            <asp:LinkButton ID="btnCancelAppRole" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="Cancel" CommandArgument='<%# Eval("IDRole") %>' OnClick="gvApplicationRoleCommand_Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAddAppRole" runat="server" Text="Add" CausesValidation="true" CommandName="InsertAppRole"
                                        CommandArgument='<%# Eval("IDRole") %>' OnClick="gvApplicationRoleCommand_Insert" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" ValidationGroup="ApplicationEdit" Font-Bold="true"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel" Font-Bold="true"></asp:LinkButton>
                    <br />
                    <asp:GridView ID="gvApplicationRoleCommand" runat="server" AutoGenerateColumns="false"
                        DataKeyNames="IDRole" DataSourceID="odsRolesByApplication" ShowHeader="false" ShowFooter="True" CssClass="nonTitle">
                        <Columns>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:Button ID="btnAddAppRole" runat="server" Text="Add" />
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnAddApplication" runat="server" CommandName="Insert" Text="Add" ValidationGroup="ApplicationAdd" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No applications exist.<br />
            <br />
            <asp:Label ID="lblAppDesc" runat="server" CssClass="label">Application Description:</asp:Label>
            <asp:TextBox ID="tbApplicationDesc" runat="server"></asp:TextBox>
            <asp:Label ID="lblAppCode" runat="server" CssClass="label">Application Code:</asp:Label>
            <asp:TextBox ID="tbApplicationCode" runat="server" MaxLength="3" Width="3em"></asp:TextBox>
            <asp:Button ID="btnAddApplication" runat="server" CommandName="EmptyInsert" Text="Add" />
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="lblAppCodeUniqueError" runat="server" CssClass="error" Text="Insert failed: The application code must be unique."
        Visible="False"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red"
        DisplayMode="List" ValidationGroup="ApplicationAdd" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red"
        DisplayMode="List" ValidationGroup="ApplicationEdit" />
    <h3>Roles</h3>
    <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" CssClass="gridView"
        DataKeyNames="IDRole" DataSourceID="odsRoles" EmptyDataText="No Roles Exist"
        ShowFooter="True" ShowHeaderWhenEmpty="True">
        <Columns>
            <%-- 
                    <asp:CommandField ShowSelectButton="True" ItemStyle-Width="10%">
                        <ItemStyle Width="10%" />
                    </asp:CommandField>
            --%>
            <asp:BoundField DataField="IDRole" HeaderText="IDRole" ReadOnly="True" SortExpression="IDRole"
                Visible="False" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ObjectDataSource ID="odsApplicationsByRole" runat="server" OldValuesParameterFormatString="{0}"
                        SelectMethod="SelectApplicationsByRole" TypeName="CSAdminCode.BusinessLogic.RoleManager">
                        <SelectParameters>
                            <asp:Parameter Name="IDRole" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Role Description" SortExpression="Description">
                <FooterTemplate>
                    <asp:TextBox ID="tbRoleDesc" runat="server" Width="14em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvRoleDesc" runat="server" ControlToValidate="tbRoleDesc"
                        ErrorMessage="A Role Description is required." Display="None" ValidationGroup="RolesAdd" ForeColor="Red">*</asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAddRoleDiscription" runat="server" Text='<%# Bind("Description") %>' Width="14em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddRoleDiscription" runat="server" ControlToValidate="txtAddRoleDiscription"
                        ErrorMessage="A Role Description is required." ValidationGroup="RolesEdit" ForeColor="Red" Display="None">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Role Code" SortExpression="Code">
                <FooterTemplate>
                    <asp:TextBox ID="tbRoleCode" runat="server" MaxLength="3" Width="3em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvRoleCode" runat="server" ControlToValidate="tbRoleCode"
                        ErrorMessage="A Role Code is required." ValidationGroup="RolesAdd" ForeColor="Red" Display="None">*</asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditRoleCode" runat="server" Text='<%# Bind("Code") %>' MaxLength="3" Width="3em"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEditRoleCode" runat="server" ControlToValidate="txtEditRoleCode"
                        ErrorMessage="A Role Code is required." ValidationGroup="RolesEdit" ForeColor="Red" Display="None">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active in Applications">
                <ItemTemplate>
                    <asp:ListView ID="lvApplications" runat="server" DataSourceID="odsApplicationsByRole" DataKeyNames="IDApplication">
                        <LayoutTemplate>
                            <ul class="applist">
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li><%# Eval("Description")%></li>
                        </ItemTemplate>
                    </asp:ListView>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <FooterTemplate>
                    <asp:Button ID="btnAddRole" runat="server" CommandName="Insert" Text="Add" ValidationGroup="RolesAdd" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    | 
                            <asp:LinkButton ID="btnDeleteRole" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this role?')"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                        Text="Update" ValidationGroup="RolesEdit"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            No roles exist.<br />
            <br />
            <asp:Label ID="lblRoleDesc" runat="server" CssClass="label">Role Description:</asp:Label>
            <asp:TextBox ID="tbRoleDesc" runat="server"></asp:TextBox>
            <asp:Label ID="lblRoleCode" runat="server" CssClass="label">Role code:</asp:Label>
            <asp:TextBox ID="tbRoleCode" runat="server"></asp:TextBox>
            <asp:Button ID="btnAddApplication" runat="server" CommandName="EmptyInsert" Text="Add" />
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="gridSelectedRow" />
    </asp:GridView>
    <asp:Label ID="lblRoleCodeUniqueError" runat="server" CssClass="error" Text="Insert failed: The role code must be unique."
        Visible="False"></asp:Label>
    <%-- 
            <asp:GridView ID="gvApplicationRoles" runat="server" CssClass="gridViewSmall floatLeft"
                DataSourceID="odsApplicationRoles" AutoGenerateColumns="False" DataKeyNames="IDApplicationRole"
                ShowFooter="True" EmptyDataText="No Applications have been assigned to this role"
                Width="40%" Visible="False">
                <Columns>
                    <asp:BoundField DataField="IDApplicationRole" HeaderText="IDApplicationRole" ReadOnly="True"
                        SortExpression="IDApplicationRole" Visible="False" />
                    <asp:TemplateField HeaderText="Application Description">
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlApplication" runat="server" DataSourceID="odsApplications"
                                DataTextField="Description" DataValueField="IDApplication">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Application.Description")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active" SortExpression="isActive">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isActive") %>' Enabled="false" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("isActive") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <FooterTemplate>
                            <asp:Button ID="btnAddAppRole" runat="server" CommandName="Insert" Text="Add" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No applications have been assigned to this role.<br />
                    <br />
                    <asp:DropDownList ID="ddlApplication" runat="server" DataSourceID="odsApplications"
                        DataTextField="Description" DataValueField="IDApplication">
                    </asp:DropDownList>
                    <asp:Button ID="btnAddAppRole" runat="server" CommandName="EmptyInsert" Text="Add"
                        UseSubmitBehavior="False" />
                </EmptyDataTemplate>
            </asp:GridView>
    --%>
    <asp:Label ID="lblDeactivateUsers" runat="server" Visible="false" CssClass="floatRight clear" Text="Note: Deactivating an application role does not deactivate the associated user roles.<br /><br />"></asp:Label>
    <asp:ValidationSummary ID="vsRolesAdd" runat="server" ForeColor="Red"
        DisplayMode="List" ValidationGroup="RolesAdd" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
        DisplayMode="List" ValidationGroup="RolesEdit" />
    <br />

    <script type="text/javascript">
        $("#gvApplications").load(function () {
            console.log("Grid loaded");
            vtab();
        });
        /*
        $(document).on("submit", function () {
        window.setTimeout(setTimeout(checkVtabState(0), 10), 0);
        });
        */
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (!!prm) {
            prm.add_endRequest(function (s, e) {
                vtab();
                $(".ddlDescription").on("change", function () {
                    console.log("Changed ddlDescription");
                    subchildren($($(this).closest(".vtab")), ".ddlCode")[0].prop("selectedIndex", $(this).prop("selectedIndex"));
                    //vtab();
                });
                $(".ddlCode").on("change", function () {
                    console.log("Changed ddlCode");
                    subchildren($($(this).closest(".vtab")), ".ddlDescription")[0].prop("selectedIndex", $(this).prop("selectedIndex"));
                    //vtab();
                });
            });
        } else {
            console.log("Could not find prm");
        }
        $(function () {
            $(".ddlDescription").on("change", function () {
                console.log("Changed ddlDescription");
                subchildren($($(this).closest(".vtab")), ".ddlCode")[0].prop("selectedIndex", $(this).prop("selectedIndex"));
                document.forms[0].submit();
                //vtab();
            });
            $(".ddlCode").on("change", function () {
                console.log("Changed ddlCode");
                subchildren($($(this).closest(".vtab")), ".ddlDescription")[0].prop("selectedIndex", $(this).prop("selectedIndex"));
                document.forms[0].submit();
                //vtab();
            });
        });
        function vtab() {
            var animationLength = 0;
            console.log("Called vtab");
            $(".vtab").each(function () {
                var vtab = $(this);
                var nonTitle = $(subchildren(vtab, ".nonTitle"));
                var tabTitle = $(subchildren(vtab, ".tabTitle"));
                nonTitle.each(function () {
                    $(this).hide();
                });
                vtab.addClass("vtabIdle");
                vtab.css("background-color", "#DDDAD0")
                tabTitle.each(function () {
                    $(this).on("click", function () {
                        if (vtab.hasClass("vtabIdle")) {
                            $(".vtab").each(function () {
                                if ($(this).hasClass("vtabActive")) {
                                    $(this).removeClass("vtabActive");
                                    $(this).addClass("vtabIdle");
                                    $(this).animate({ backgroundColor: "#DDDAD0" }, animationLength);
                                    var newnonTitle = $(subchildren($(this), ".nonTitle"));
                                    newnonTitle.each(function () {
                                        $(this).hide(animationLength);
                                    });
                                }
                            });
                            vtab.removeClass("vtabIdle");
                            vtab.addClass("vtabActive");
                            vtab.animate({ backgroundColor: "#EEE" }, animationLength);
                            nonTitle.each(function () {
                                $(this).show(animationLength);
                            });
                            document.cookie = "activeid=" + vtab.attr("class").match(/ tabid\d* /g)[0].trim(); // Set the cookie to remember position
                        } else {
                            vtab.removeClass("vtabActive");
                            vtab.addClass("vtabIdle");
                            vtab.animate({ backgroundColor: "#DDDAD0" }, animationLength);
                            nonTitle.each(function () {
                                $(this).hide(animationLength);
                            });
                            // Kill the cookie
                            var newdate = new Date();
                            newdate.setDate(newdate.getDate() - 10);
                            document.cookie = "activeid=0; expires=" + newdate.toUTCString();
                        }
                    });
                });
            });
            // Cure amnesia: remember the active tab
            var activeid = getCookie("activeid");
            console.log("activeid: " + activeid);
            if (!!activeid && activeid != "") {
                activeid = "." + activeid;
                $(activeid).removeClass("vtabIdle");
                $(activeid).addClass("vtabActive");
                $(activeid).animate({ backgroundColor: "#EEE" }, animationLength);
                var newnonTitle = $(subchildren($(activeid), ".nonTitle"));
                newnonTitle.each(function () {
                    $(this).show(0);
                });
            }
        }
        function subchildren(source, target) {
            var children = [];
            source.children(target).each(function () {
                children.push($(this));
            });
            source.children().each(function () {
                var subchilds = subchildren($(this), target);
                $(subchilds).each(function () {
                    children.push($(this));
                });
            });
            return children;
        }
        function getCookie(c_name) {
            var c_value = document.cookie;
            var c_start = c_value.indexOf(" " + c_name + "=");
            if (c_start == -1) {
                c_start = c_value.indexOf(c_name + "=");
            }
            if (c_start == -1) {
                c_value = null;
            }
            else {
                c_start = c_value.indexOf("=", c_start) + 1;
                var c_end = c_value.indexOf(";", c_start);
                if (c_end == -1) {
                    c_end = c_value.length;
                }
                c_value = unescape(c_value.substring(c_start, c_end));
            }
            return c_value;
        } // getCookie, credit to W3Schools http://www.w3schools.com/js/js_cookies.asp
    </script>
</asp:Content>
