<%@ Page Title="CSAdmin - Languages" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="ManageLanguages.aspx.vb" Inherits="CSAdmin.ManageLanguages" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>
        Languages
    </h2>
    <asp:ObjectDataSource ID="odsLanguages" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="SelectAllLanguages" TypeName="CSAdminCode.BusinessLogic.LanguageManager"
        DeleteMethod="DeleteLanguage" InsertMethod="InsertLanguage" UpdateMethod="UpdateLanguage">
        <DeleteParameters>
            <asp:Parameter Name="LanguageID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Language" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="LanguageID" Type="Int32" />
            <asp:Parameter Name="Language" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
    <div style="width:65%;margin:auto;">
    <asp:GridView ID="gvLanguages" runat="server" DataSourceID="odsLanguages" CssClass="gridView"
        AutoGenerateColumns="False" ShowFooter="True" DataKeyNames="LanguageID">
        <Columns>
            <asp:TemplateField HeaderText="LanguageID" SortExpression="LanguageID" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="LanguageID" runat="server" Text='<%# Bind("LanguageID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Language" SortExpression="Language">
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditLanguage" runat="server" Text='<%# Bind("Language") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtEditLanguage" ValidationGroup="editLan" CssClass="error" ErrorMessage="Please enter valid language."></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="txtAddLanguage" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtAddLanguage" CssClass="error" ErrorMessage="*"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblEditLanguage" runat="server" Text='<%# Bind("Language") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Default">
                <ItemTemplate>
                 <asp:radiobutton ID="rbLanguageDefault" runat="server" Enabled="false" GroupName='<%# Bind("Languageid") %>' />
                </ItemTemplate>
                </asp:TemplateField>
                
            
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="btnUpdate" ValidationGroup="editLan"  runat="server" CausesValidation="true" CommandName="Update"
                        Text="Update"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="btnDefault" runat="server" CausesValidation="False" CommandName="Default"
                        Text="Default"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnInsert" runat="server" Text="Insert" CommandName="Insert" />
                </FooterTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                        Text="Edit"></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete"></asp:LinkButton>
                    <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnDelete"
                                ConfirmText="Are you sure you want to delete this language?" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="gridHead" />
    </asp:GridView>
    </div>
</asp:Content>

