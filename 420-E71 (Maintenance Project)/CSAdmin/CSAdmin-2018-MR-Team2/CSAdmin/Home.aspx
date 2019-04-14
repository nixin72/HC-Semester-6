<%@ Page Title="CSAdmin - Home" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Home.aspx.vb" Inherits="CSAdmin.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>Home</h2>
    <div id="HomeContent" runat="server">
        <h3><a href="UpdateSemester.aspx">New Semester</a></h3>
        <p>Update the systems to a new semester and add the new semesters students, co-op students, teachers and coordinators to the database.</p>
        <hr/>
        <h3><a href="ManageUsernames.aspx">Manage Usernames</a></h3>
        <p>Search and modify usernames from the database and locate them in LDAP.</p>
        <hr/>
        <h3><a href="ApplicationRoles.aspx">Manage Application Roles</a></h3>
        <p>Add, edit and remove applications and roles associated with them.</p>
        <hr/>
        <h3><a href="ManageUserRoles.aspx">Manage Roles</a></h3>
        <p>Manage the roles of Heritage faculty.</p>
        <hr/>
        <h3><a href="CountriesLookup.aspx">Manage Countries</a></h3>
        <p>Add, edit and remove countries for lists used throughout CS Systems.</p>
        <hr/>
        <h3><a href="ProvinceStateLookup.aspx">Manage Provinces and States</a></h3>
        <p>Add, edit and remove provinces and states for countries throughout CS Systems.</p>
        <hr/>
        <h3><a href="EducationTypeLookup.aspx">Manage Education Types</a></h3>
        <p>Add, edit and remove education types for Heritage College throughout CS Systems.</p>
        <hr/>
        <h3><a href="ManageLanguages.aspx">Manage Languages</a></h3>
        <p>Add, edit and remove languages to use and set a default.</p>
        <hr/>
    </div>
</asp:Content>
