<%@ Page Title="CSAdmin - New Semester" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"
    CodeBehind="UpdateSemester.aspx.vb" Inherits="CSAdmin.UpdateSemester" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        // Functions to show(onclick) or hide(onblur) the calendar
        // when the user clicks on the textbox
        function showStartDate() {
            $find("Date").show();
        }

        // When hiding the calendar, we must determine what was clicked
        // since by just hiding on the onblur event, the calendar closes before
        // the date is set to the textbox
        function hideStartDate() {
            // Get the active element, the element that is focused
            var activeElement = document.activeElement;
            var elementID = activeElement.id;

            // if the id does not start with StartDate (the behaviour id of the calendar
            // extender then hide the calendar
            if (elementID.substring(0, 4) !== "Date") {
                $find("Date").hide();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <h2>
        New Semester</h2>
    <div id="updSemester" style="width: 100%; display: block;">
        <h3>
            Update Semester</h3>
        <asp:Label ID="captionSemester" runat="server" Text="Semester:" CssClass="manageLabel"></asp:Label>
        <asp:RadioButtonList ID="rblSemesters" runat="server" AutoPostBack="True">
        </asp:RadioButtonList><br />
        <asp:Label ID="captionSemesterEndDate" runat="server" Text="Semester End Date:" CssClass="manageLabel"></asp:Label><asp:TextBox
            ID="txtSemesterEndDate" runat="server" onclick="showStartDate();" onblur="hideStartDate();"
            CssClass="textbox"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtSemesterEndDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:ImageButton ID="calImg1" runat="server" ImageUrl="~/Images/calendarImg.png" />
        <asp:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="txtSemesterEndDate"
            CssClass="calendar" Format="yyyy-MMM-dd" PopupButtonID="calImg1" BehaviorID="Date">
        </asp:CalendarExtender>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txtSemesterEndDate" ErrorMessage="(YYYY-MMM-DD)" 
            ForeColor="Red" 
            ValidationExpression="(19|20)\d\d[-](Jan|JAN|Feb|FEB|Mar|MAR|Apr|APR|May|MAY|Jun|JUN|Jul|JUL|Aug|AUG|Sep|SEP|Oct|OCT|Nov|NOV|Dec|DEC)[-]\d\d"></asp:RegularExpressionValidator>
        <br />
        <asp:Button ID="btnUpdateSemester" runat="server" Text="Update" CssClass="buttonNudge1" />
        <asp:Label ID="lblSemesterUpdated" runat="server" Text="The semester information was successfully updated!" CssClass="success" Visible="false"></asp:Label>
        <asp:Label ID="lblSemesterCantBeUpdated" runat="server" 
            Text="The semester can't be updated because the semester does not exist in Clara." 
            CssClass="error" Visible="False"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upAddNewUsers" runat="server">
        <ContentTemplate>
            <div id="addNewUsers" style="width: 100%; display: block;" runat="server">
                <h3>
                    Add Users
                </h3>
                <div class="report">
                    <asp:UpdateProgress ID="UpdateProgressAddNewUsers" runat="server" AssociatedUpdatePanelID="upAddNewUsers">
                        <ProgressTemplate>
                            <img alt="progress" src="images/Loading.gif" />
                            <!-- 
                        Loading symbol file is licensed under the Creative Commons Attribution-Share Alike 2.5 Generic license. 
                        See http://creativecommons.org/licenses/by-sa/2.5/deed.en for license details.
                        Source: http://commons.wikimedia.org/wiki/File:Loading.gif
                        -->
                            Processing...
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Wizard ID="wizardAddUsers" runat="server" ActiveStepIndex="0" DisplayCancelButton="True"
                        DisplaySideBar="False" CancelDestinationPageUrl="~/UpdateSemester.aspx" FinishDestinationPageUrl="~/UpdateSemester.aspx"
                        CancelButtonType="Link">
                        <FinishNavigationTemplate>
                            <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" Text="Finish" />
                        </FinishNavigationTemplate>
                        <StartNavigationTemplate>
                            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next" />
                        </StartNavigationTemplate>
                        <WizardSteps>
                            <asp:WizardStep runat="server" StepType="Start" Title="Step 1 - Choose Users to Update">
                                <h4 runat="server" id="addUsersToSemester"></h4>
                                <asp:CheckBoxList ID="cblUsersToAdd" runat="server">
                                    <asp:ListItem Value="1">Add All Students</asp:ListItem>
                                    <asp:ListItem Value="3">Add All Teachers and Coordinators</asp:ListItem>
                                </asp:CheckBoxList>
                            </asp:WizardStep>
                            <asp:WizardStep ID="addAllStudentsStep" runat="server" StepType="Step" Title="Step 2 - Add All Students">
                                <h4>
                                    Add all students - Confirm</h4>
                                <div id="AddAllStudentsReportTxt" runat="server">
                                </div>
                                <br />
                                <br />
                                <asp:CheckBox ID="cbAllStudents" runat="server" Text="I understand the implications of this change"
                                    AutoPostBack="true" />
                            </asp:WizardStep>
                            <asp:WizardStep ID="addCoopAllStudentsStep" runat="server" StepType="Step" Title="Step 3 - Add Co-op Students">
                                <h4>
                                    Add Co-op Students - Confirm</h4>
                                <div id="AddAllCoopStudentsReportTxt" runat="server">
                                </div>
                                <br />
                                <br />
                                <asp:CheckBox ID="cbCoopStudents" runat="server" Text="I understand the implications of this change"
                                    AutoPostBack="true" />
                            </asp:WizardStep>
                            <asp:WizardStep ID="addAllTeachersAndCoordinatorsStep" runat="server" StepType="Step"
                                Title="Step 3 - Add All Teachers and Coordinators">
                                <h4>
                                    Add All Teachers and Coordinators - Confirm</h4>
                                <div id="AddAllTeachersAndCoordinatorsReportTxt" runat="server">
                                </div>
                                <br />
                                <br />
                                <asp:CheckBox ID="cbTeachersAndCoordinators" runat="server" Text="I understand the implications of this change"
                                    AutoPostBack="true" />
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" StepType="Step" Title="Step 5 - Confirm Changes">
                                <h4>
                                    Confirm Changes</h4>
                                <em class="error">Note:</em> The changes you agreed to will be completed. Are you
                                sure?
                            </asp:WizardStep>
                            <asp:WizardStep runat="server" StepType="Finish" Title="Step 6 - Users added and removed"
                                AllowReturn="false">
                                <h4>
                                    Add users - Completed</h4>
                                The changes you selected have been successfully completed.
                            </asp:WizardStep>
                        </WizardSteps>
                    </asp:Wizard>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
