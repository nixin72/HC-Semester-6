<%@ Page Title="CSAdmin - Provinces and States" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ProvinceStateLookup.aspx.vb" Inherits="CSAdmin.ProvinceStateLookup" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ObjectDataSource ID="odsProvinceStateCA" runat="server" TypeName="CSAdminCode.BusinessLogic.ProvinceStateManager"
        SelectMethod="SelectProvinceStatesByCountry" InsertMethod="InsertProvinceState" UpdateMethod="UpdateProvinceState" DeleteMethod="DeleteProvinceState" OldValuesParameterFormatString="{0}">
    <SelectParameters>
        <asp:Parameter Name="IDCountry" Type="Int32" DefaultValue="1" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDProvinceState" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
    </UpdateParameters>
    <DeleteParameters>
        <asp:Parameter Name="IDProvinceState" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Country" Type="String" />
        <asp:Parameter Name="Name" Type="String" />
    </InsertParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsProvinceStateUS" runat="server" TypeName="CSAdminCode.BusinessLogic.ProvinceStateManager"
        SelectMethod="SelectProvinceStatesByCountry" InsertMethod="InsertProvinceState" UpdateMethod="UpdateProvinceState" DeleteMethod="DeleteProvinceState" OldValuesParameterFormatString="{0}">
    <SelectParameters>
        <asp:Parameter Name="IDCountry" Type="Int32" DefaultValue="2" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDProvinceState" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
    </UpdateParameters>
    <DeleteParameters>
        <asp:Parameter Name="IDProvinceState" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Country" Type="String" />
        <asp:Parameter Name="Name" Type="String" />
    </InsertParameters>
</asp:ObjectDataSource>
    <h3>
        Provinces and States
    </h3>
    <asp:GridView ID="gvProvinceStateCA" runat="server" AutoGenerateColumns="False" CssClass="gridViewLeft"
        SelectedRowStyle-CssClass="gridSelectedRow" HeaderStyle-CssClass="gridHead" Width="75%"
        DataSourceID="odsProvinceStateCA"  DataKeyNames="IDProvinceState" ShowFooter="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblIDProvinceState" runat="server" Text='<%# Bind("IDProvinceState") %>' Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Canada">
                <ItemTemplate>
                    <asp:Label ID="lblProvinceName" runat="server" Text='<%# Bind("Name") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtProvinceName" runat="server" Text='<%# Bind("Name") %>' />
                    <asp:RequiredFieldValidator ID="valrProvinceName" runat="server" ValidationGroup="UpdateValidation" ControlToValidate="txtProvinceName" ForeColor="Red" ErrorMessage="Please enter the province name" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtProvinceName" runat="server" Text='' />
                    <asp:RequiredFieldValidator ID="valrProvinceName" runat="server" ValidationGroup="InsertValidation" ControlToValidate="txtProvinceName" ForeColor="Red" ErrorMessage="Please enter the province name" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" />
                    <asp:Label ID="lblDivider" runat="server" Text="&nbsp;|&nbsp;" />
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" />
                    <asp:ConfirmButtonExtender ID="cbeDelete" runat="server" TargetControlID="btnDelete"
                        ConfirmText="Are you sure you want to delete this province?">
                    </asp:ConfirmButtonExtender>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" runat="server" ValidationGroup="UpdateValidation" CausesValidation="true" CommandName="Update" Text="Update" />
                    <asp:Label ID="lblDivider2" runat="server" Text="&nbsp;|&nbsp;" />
                    <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="InsertValidation" CausesValidation="true" CommandName="Insert" Text="Add" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no provinces to display.
        </EmptyDataTemplate>
        <HeaderStyle CssClass="gridHead" />
        <SelectedRowStyle CssClass="gridSelectedRow" />
    </asp:GridView>

    
    <asp:GridView ID="gvProvinceStateUS" runat="server" AutoGenerateColumns="False" CssClass="gridViewLeft"
        SelectedRowStyle-CssClass="gridSelectedRow" HeaderStyle-CssClass="gridHead" Width="75%"
        DataSourceID="odsProvinceStateUS"  DataKeyNames="IDProvinceState" ShowFooter="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblIDProvinceState" runat="server" Text='<%# Bind("IDProvinceState") %>' Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="United States">
                <ItemTemplate>
                    <asp:Label ID="lblProvinceName" runat="server" Text='<%# Bind("Name") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtProvinceName" runat="server" Text='<%# Bind("Name") %>' />
                    <asp:RequiredFieldValidator ID="valrProvinceName" runat="server" ValidationGroup="UpdateValidation" ControlToValidate="txtProvinceName" ForeColor="Red" ErrorMessage="Please enter the province name" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtProvinceName" runat="server" Text='' />
                    <asp:RequiredFieldValidator ID="valrProvinceName" runat="server" ValidationGroup="InsertValidation" ControlToValidate="txtProvinceName" ForeColor="Red" ErrorMessage="Please enter the province name" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" />
                    <asp:Label ID="lblDivider" runat="server" Text="&nbsp;|&nbsp;" />
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" />
                    <asp:ConfirmButtonExtender ID="cbeDelete" runat="server" TargetControlID="btnDelete"
                        ConfirmText="Are you sure you want to delete this state?">
                    </asp:ConfirmButtonExtender>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" runat="server" ValidationGroup="UpdateValidation" CausesValidation="true" CommandName="Update" Text="Update" />
                    <asp:Label ID="lblDivider2" runat="server" Text="&nbsp;|&nbsp;" />
                    <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="false" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnAdd" runat="server" ValidationGroup="InsertValidation" CausesValidation="true" CommandName="Insert" Text="Add" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            There are no states to display.
        </EmptyDataTemplate>
        <HeaderStyle CssClass="gridHead" />
        <SelectedRowStyle CssClass="gridSelectedRow" />
    </asp:GridView>
</asp:Content>
