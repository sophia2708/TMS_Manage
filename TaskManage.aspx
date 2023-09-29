<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="TaskManage.aspx.cs" Inherits="Includes_WebForm_TaskManage" Title="Task Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
     <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.history.forward(1);

        function ConfirmCancel() {
            try {
                var conf = window.confirm("Do you want to cancel??");
                if (conf == false) {
                    return false;
                }
            }
            catch (e) {
                alert(e.message);
            }
        }

        function datevalidate() {
            var startdate = new Date(2014, 01, 01);
            var start = new Date(startdate);
            start = document.getElementById('<%=txtplanneddate.ClientID%>').value;
            // alert(start); 
            var enddate = new Date(2014, 01, 01);
            var end = new Date(enddate);
            end = document.getElementById('<%=txtExpectedCompDate.ClientID%>').value;
            var isValid = false;
            isValid = Page_ClientValidate('Save');
            if (isValid) {
                isValid = Page_ClientValidate('date');
            }
            return isValid;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px;">
        <div class="container">
            <div class="row">
                <div class="col-lg-2">
                </div>
                <div class="col-lg-8">
                    <div class="jumbotron">
                        <div class="row">
                            <h2 class="text-primary">
                                Task Assignment
                            </h2>
                        </div>
                        <br />
                        <div class="row">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblclient" runat="server" Text="Client"></asp:Label></div>
                                <div class="col-lg-6">
                                    <asp:DropDownList Width="190px" Height="31px" ID="ddlclient" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlclient_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <asp:Panel runat="server" ID="pnlproject">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblEmployee" runat="server" Text="Employee"></asp:Label></div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlEmployeeList" Width="190px" Height="31px" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeeList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="reqfvEmployeeList" runat="server" InitialValue="--Select--"
                                            ErrorMessage="Select the Employee" ControlToValidate="ddlEmployeeList" Font-Size="XX-Small"
                                            ValidationGroup="Save"></asp:RequiredFieldValidator>--%></div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlProjectList" runat="server" Width="190px" Height="31px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddProject" runat="server">
                                <div class="row">
                                    <div class=" col-md-6 col-lg-6">
                                        <asp:Label ID="lblAddProject" runat="server" Text="Enter the Project"></asp:Label>
                                    </div>
                                    <div class=" col-md-6 col-lg-6">
                                        <asp:TextBox ID="txtAddProject" Width="190px" Height="31px" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqfvProject" runat="server" ErrorMessage="Enter the Project"
                                            ControlToValidate="txtAddProject" Font-Size="XX-Small" ValidationGroup="AddNewPro"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnAddProject" runat="server" Text="Add" CssClass="button_Small_Style"
                                            ValidationGroup="AddNewPro" OnClick="btnAddProject_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel runat="server" ID="pnlModule">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlModuleList" runat="server" Width="190px" Height="31px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlAddModule" runat="server">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblAddModule" runat="server" Text="Enter the Module"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtAddModule" runat="server" Width="190px" Height="31px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqfvModule" runat="server" ErrorMessage="Enter the Module"
                                            ControlToValidate="txtAddModule" Font-Size="XX-Small" ValidationGroup="AddNewMod"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnAddModule" runat="server" ValidationGroup="AddNewMod" CssClass="button_Small_Style"
                                            Text="Add" OnClick="btnAddModule_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlTask">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblTask" runat="server" Text="Task"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlTaskList" Width="190px" Height="31px" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlTaskList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel runat="server" ID="pnlcategory">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbltaskcat" runat="server" Text="TaskCategory"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddltaskcat" Width="190px" Height="31px" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddltaskcat_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddTask" runat="server">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblAddTask" runat="server" Text="Enter the Task"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtAddTask" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="refvTask" Width="190px" Height="31px" runat="server"
                                            ErrorMessage="Enter the Task" ControlToValidate="txtAddTask" Font-Size="XX-Small"
                                            ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblTaskDesc" runat="server" Text="Description"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtDescription" Width="190px" Height="31px" TextMode="MultiLine"
                                            runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqfDescp" runat="server" ErrorMessage="Enter the Description"
                                            ControlToValidate="txtDescription" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                        <asp:Button ID="btnAddTask" runat="server" Text="Add" ValidationGroup="AddNewTask"
                                            CssClass="button_Small_Style" OnClick="btnAddTask_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlEmpList" runat="server">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbl" runat="server" Text="Task Priority"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlPriority" runat="server" Width="190px" Height="31px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqfvPriority" runat="server" InitialValue="0" ErrorMessage="Select the priority"
                                            ControlToValidate="ddlPriority" Font-Size="XX-Small" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblPlanneddate" runat="server" Text="Planned Start Date"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtplanneddate" runat="server" ToolTip="dd-mm-yyyy" Width="190px"
                                            Height="31px" ReadOnly="true"></asp:TextBox><a href="#" onclick="showCalendarControl('<%=txtplanneddate.ClientID%>','')">
                                                <img id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                    vertical-align: middle;" /></a>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter the Date"
                                            ControlToValidate="txtplanneddate" Font-Size="XX-Small" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblplannedeffortdate" runat="server" Text="Planned Effort Hours"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtplannedeffortdate" Width="190px" Height="31px" runat="server"
                                            placeholder="HH:MM:SS"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter the Hours"
                                            ControlToValidate="txtplannedeffortdate" Font-Size="XX-Small" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblDate" runat="server" Text="Expected Completion Date"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtExpectedCompDate" runat="server" ToolTip="dd-mm-yyyy" ReadOnly="true"
                                            Width="190px" Height="31px"></asp:TextBox><a href="#" onclick="showCalendarControl('<%=txtExpectedCompDate.ClientID%>','')">
                                                <img id="imgDOR" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif"
                                                    style="border: 0; vertical-align: middle;" /></a>
                                        <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="Enter the Date"
                                            ControlToValidate="txtExpectedCompDate" Font-Size="XX-Small" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" ValidationGroup="date" runat="server"
                                            ControlToValidate="txtplanneddate" ControlToCompare="txtExpectedCompDate" Operator="LessThanEqual"
                                            Type="Date" ErrorMessage="End Date must be greater or equal to start Date." Display="None"></asp:CompareValidator>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlotd" runat="server">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblactualstartdate" runat="server" Text="Actual Start Date"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:TextBox ID="txtactualstartdate" Width="190px" Height="31px" runat="server" ToolTip="dd-mm-yyyy"
                                            AutoPostBack="true">
                                        </asp:TextBox>
                                        <a href="#" onclick="showCalendarControl('<%=txtactualstartdate.ClientID%>','')">
                                            <img id="img3" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                vertical-align: middle;" /></a>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter the Date"
                                            ControlToValidate="txtactualstartdate" Font-Size="XX-Small" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblactualeffortdate" runat="server" Text="Actual Effort"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <%-- <asp:TextBox ID="txtactualeffortdate" runat="server" Width="100px" ></asp:TextBox>--%>
                                        &nbsp; &nbsp;
                                        <asp:Label ID="lblactualeffort" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblftr" runat="server" Text="FTR"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlftr" Width="190px" Height="31px" runat="server">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="PV" Value="PV"></asp:ListItem>
                                            <asp:ListItem Text="NA" Value="NA"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblotd" runat="server" Text="OTD"></asp:Label>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:DropDownList ID="ddlotd" runat="server" Width="190px" Height="31px">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                            <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlbtn" runat="server">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Button ID="btnsavs" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="return datevalidate()"
                                            OnClick="btnsavs_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default"
                                            OnClientClick=" return ConfirmCancel();" OnClick="btnCancel_Click" />
                                    </div>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="date" />
                                </div>
                            </asp:Panel>
                            <br />
                        </div>
                    </div>
                </div>
                <div class="col-lg-2">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
