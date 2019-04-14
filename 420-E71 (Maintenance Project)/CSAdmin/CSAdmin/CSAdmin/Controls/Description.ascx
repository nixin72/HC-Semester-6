<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Description.ascx.vb" Inherits="CSAdmin.Description" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

    <h2><div><div style="float:left"><asp:Label ID="lblPageTitle" runat="server" Text=""></asp:Label></div>&nbsp;&nbsp;<div style="float:left;margin-top:1px;margin-left:5px;"><asp:Image Width="20px" Height="20px" ID="btnDescInfo" CssClass="DescButton" runat="server" ImageUrl="~/Images/descinfo2.png" /></div></div></h2>
        <asp:Panel ID="descPanel" runat="server">
            <p><asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="balloonCloseBtn">Close</asp:LinkButton>
        </asp:Panel>
        <asp:BalloonPopupExtender ID="PopupControlExtender" runat="server"
        TargetControlID="btnDescInfo"
        BalloonPopupControlID="descPanel"
        Position="BottomRight" 
        BalloonStyle="Custom"
        BalloonSize="medium"
        DisplayOnMouseOver="true"
        CustomClassName="custom"
        CustomCssUrl="~/Site.css"
        ScrollBars="None"  />
    </ContentTemplate>
</asp:UpdatePanel>

