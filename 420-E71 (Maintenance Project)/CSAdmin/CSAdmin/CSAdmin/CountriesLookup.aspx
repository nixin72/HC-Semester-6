<%@ Page Title="CSAdmin - Countries" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CountriesLookup.aspx.vb" Inherits="CSAdmin.CountriesLookup" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register Src="Controls/PagingDropDown.ascx" TagName="DDLPageSize" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ObjectDataSource ID="odsCountries" runat="server" TypeName="CSAdminCode.BusinessLogic.CountryManager"
        SelectMethod="SelectCountries" UpdateMethod="UpdateCountry" InsertMethod="InsertCountry" DeleteMethod="DeleteCountry" OldValuesParameterFormatString="{0}" OnDeleted="odsCountries_Deleted">
    <SelectParameters>
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="IDCountry" Type="Int32" />
        <asp:Parameter Name="Name" Type="String" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="Name" Type="String" />
    </InsertParameters>
    <DeleteParameters>
        <asp:Parameter Name="IDCountry" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
    <div id="notification">
    <uc2:DDLPageSize ID="ddlPageSize" runat="server" Visible="True" Control="gvCountries">
        </uc2:DDLPageSize>
    </div>
    <h3>
        Countries
    </h3>
    <asp:CustomValidator ID="valCannotDelete" runat="server" ErrorMessage="Cannot delete country due to references." Display="Dynamic" ForeColor="Red"></asp:CustomValidator>
    <asp:GridView ID="gvCountries" runat="server" AutoGenerateColumns="False" CssClass="gridViewLeft"
        SelectedRowStyle-CssClass="gridSelectedRow" HeaderStyle-CssClass="gridHead" Width="75%"
        DataSourceID="odsCountries"  DataKeyNames="IDCountry" ShowFooter="True"
        AllowPaging="true" PagerSettings-Mode="NextPreviousFirstLast" PageSize="15" PagerStyle-HorizontalAlign="Center">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblIDCountry" runat="server" Text='<%# Bind("IDCountry") %>' Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblCountryName" runat="server" Text='<%# Bind("Name") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Textbox ID="txtCountryName" runat="server" Text='<%# Bind("Name") %>' />
                    <asp:RequiredFieldValidator ID="valrCountryName" runat="server" ValidationGroup="UpdateValidation" ControlToValidate="txtCountryName" ForeColor="Red" ErrorMessage="Please enter the country name" />
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Textbox ID="txtCountryName" runat="server" Text='' />
                    <asp:RequiredFieldValidator ID="valrCountryName" runat="server" ValidationGroup="InsertValidation" ControlToValidate="txtCountryName" ForeColor="Red" ErrorMessage="Please enter the country name" />
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" />
                    <asp:Label ID="lblDivider" runat="server" Text="&nbsp;|&nbsp;" />
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" />
                    <asp:ConfirmButtonExtender ID="cbeDelete" runat="server" TargetControlID="btnDelete"
                        ConfirmText="Are you sure you want to delete this country? Note: countries that have provinces or states associated with them cannot be deleted.">
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
            There are no countries to display.
        </EmptyDataTemplate>
        <HeaderStyle CssClass="gridHead" />
        <SelectedRowStyle CssClass="gridSelectedRow" />
    </asp:GridView>
    <asp:Label ID="lblUnique" runat="server" Text="" ForeColor="Red"></asp:Label>
</asp:Content>
