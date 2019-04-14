<%@ Page Title="CSAdmin - Lookups" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ManageLookups.aspx.vb" Inherits="CSAdmin.ManageGeneral" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>
       Lookup Tables
    </h2>
    <asp:ObjectDataSource ID="odsCountries" runat="server" TypeName="CSAdminCode.BusinessLogic.CountryManager"
        SelectMethod="SelectCountries" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
        </SelectParameters>
        </asp:ObjectDataSource>
     <asp:ObjectDataSource ID="odsProvinceState" runat="server" TypeName="CSAdminCode.BusinessLogic.ProvinceStateManager"
        SelectMethod="SelectProvinceStates" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
        </SelectParameters>
        </asp:ObjectDataSource>
     <asp:ObjectDataSource ID="odsEducationType" runat="server" TypeName="CSAdminCode.BusinessLogic.EducationTypeManager"
        SelectMethod="SelectEducationTypes" UpdateMethod="UpdateEducationType" InsertMethod="InsertEducationType" DeleteMethod="DeleteEducationType" OldValuesParameterFormatString="{0}">
        <SelectParameters>
        </SelectParameters>
         <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
         </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDEducationType" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
             <asp:Parameter Name="IDEducationType" Type="Int32" />
        </DeleteParameters>
        </asp:ObjectDataSource>
    <h3>
        Countries
    </h3>
    <asp:DropDownList ID="ddlCountries" runat="server" DataSourceID="odsCountries" 
        DataTextField="Name" DataValueField="IDCountry">
    </asp:DropDownList>
    <asp:RadioButtonList ID="rblCountries" runat="server" 
        RepeatDirection="Horizontal" AutoPostBack="True">
        <asp:ListItem>Add</asp:ListItem>
        <asp:ListItem>Edit</asp:ListItem>
        <asp:ListItem>Delete</asp:ListItem>
    </asp:RadioButtonList>
    <asp:UpdatePanel ID="upCountry" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtCountry" runat="server" Enabled="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="txtCountry"
                                    ErrorMessage="A country is required." ForeColor="Red" ValidationGroup="Country">*</asp:RequiredFieldValidator>&nbsp;<br />
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblCountries" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnSaveCountry" runat="server" Text="Save" ValidationGroup="Country"/>
    <asp:ValidationSummary ID="vsCountry" runat="server" ValidationGroup="Country" 
        DisplayMode="List" ForeColor="Red"/>

    <asp:UpdatePanel ID="upCountriesLabel" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblCountries" runat="server" Text="Label" Visible="False"></asp:Label>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblCountries" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <h3>
        Provinces and States
    </h3>
    <asp:DropDownList ID="ddlProvinceState" runat="server" DataSourceID="odsProvinceState" 
        DataTextField="Name" DataValueField="IDProvinceState">
    </asp:DropDownList>
    <asp:RadioButtonList ID="rblProvinceState" runat="server" 
        RepeatDirection="Horizontal" AutoPostBack="True">
        <asp:ListItem Value="Canada">Add to Canada</asp:ListItem>
        <asp:ListItem Value="USA">Add to USA</asp:ListItem>
        <asp:ListItem>Edit</asp:ListItem>
        <asp:ListItem>Delete</asp:ListItem>
    </asp:RadioButtonList>
    <asp:UpdatePanel ID="upProvinceState" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtProvinceState" runat="server" Enabled="False"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvProvinceState" runat="server" ControlToValidate="txtProvinceState"
                                    ErrorMessage="A province or state is required." ForeColor="Red" ValidationGroup="ProvinceState">*</asp:RequiredFieldValidator>&nbsp;<br />
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblProvinceState" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="btnSaveProvinceState" runat="server" Text="Save" ValidationGroup="ProvinceState"/>
    <asp:ValidationSummary ID="vsProvinceState" runat="server" 
        ValidationGroup="ProvinceState" DisplayMode="List" ForeColor="Red"/>
    
    <asp:UpdatePanel ID="upProvinceStateLabel" runat="server">
    <ContentTemplate>
        <asp:Label ID="lblConProvinceState" runat="server" Text="Label" Visible="False"></asp:Label>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblProvinceState" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <h3>
        Education Type
    </h3>
     <asp:UpdatePanel ID="upMainContent" runat="server">
        <ContentTemplate>
            <div>
                <asp:GridView ID="gvEducatuionType" runat="server" AutoGenerateColumns="False" CssClass="gridViewLeft"
                    SelectedRowStyle-CssClass="gridSelectedRow" HeaderStyle-CssClass="gridHead" Width="75%"
                    DataSourceID="odsEducationType"  DataKeyNames="IDEducationType" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="IDEducationType" SortExpression="IDStatus" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblEducationTypeID" runat="server" Text='<%# Bind("IDEducationType") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEditEducationTypeID" runat="server" Text='<%# Bind("IDEducationType") %>'></asp:Label>
                            </EditItemTemplate>
                          <FooterTemplate>
                                <asp:Label runat="server" ID="lblInsertStatus" Text='<%# Bind("IDEducationType") %>'
                                    Width="100%"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="Text" ItemStyle-Width="65%"
                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEducationType" Rows="1" TextMode="SingleLine" Width="325px" runat="server"
                                    Text='<%# Bind("Name") %>' MaxLength="50" ValidationGroup="EditStatus"></asp:TextBox><br />
                                <asp:RequiredFieldValidator ID="rfvEditEducationType" runat="server" ControlToValidate="txtEducationType"
                                    ErrorMessage="An Education type is required." ForeColor="Red" Display="None"  ValidationGroup="EditEducationType">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEducationType" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox runat="server" ID="txtInsertEducationType" Text='<%# Bind("Name") %>'
                                    Width="100%" ValidationGroup="AddEducationType"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddEducationType" runat="server" ControlToValidate="txtInsertEducationType"
                                    ErrorMessage="An Education type is required." ForeColor="Red"  Display="None" ValidationGroup="AddEducationType">*</asp:RequiredFieldValidator>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                    Text="Edit"></asp:LinkButton>
                                &nbsp;
                                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="true" CommandName="Delete"
                                    Text="Delete"></asp:LinkButton>
                                <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="DeleteButton"
                                    ConfirmText="Are you sure you want to delete this status?">
                                </asp:ConfirmButtonExtender>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="True" CommandName="Update"
                                    Text="Update" ValidationGroup="EditEducationType"></asp:LinkButton>
                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                    Text="Cancel"></asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button Text="Insert" CommandName="Insert" CausesValidation="true" runat="server" ValidationGroup="AddEducationType"
                                    ID="btInsert" />&nbsp;
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        There are no education types to display.
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gridHead" />
                    <SelectedRowStyle CssClass="gridSelectedRow" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="vsEditEducationType" runat="server" 
        ValidationGroup="EditEducationType" DisplayMode="List" ForeColor="Red"/>
    <asp:ValidationSummary ID="vsAddEducationType" runat="server" 
        ValidationGroup="AddEducationType" DisplayMode="List" ForeColor="Red"/>
</asp:Content>
