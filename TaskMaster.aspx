<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="TaskMaster.aspx.cs" Inherits="TaskMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
     <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function daterange() {
            var startdate = new Date(2014, 01, 01);
            var start = new Date(startdate);
            start = document.getElementById('<%=txtplanneddate.ClientID%>').value;
            // alert(start); 
            var enddate = new Date(2014, 01, 01);
            var end = new Date(enddate);
            end = document.getElementById('<%=txtplanndend.ClientID%>').value;
            //alert(end);
            //         if (start <= end) 
            //         {
            //             return true;
            //         }
            //         else {
            //             alert('End Date must be greater or equal to start Date');
            //             return false;
            //         }

            var isValid = false;
            isValid = Page_ClientValidate('AddNewTask');
            if (isValid) {
                isValid = Page_ClientValidate('Date');
            }
            return isValid;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-lg-2">
            </div>
            <div class="col-lg-8">
                <div class="jumbotron">
                    <div class="row">
                        <h2 class="text-primary">
                            Task Master</h2>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:Label ID="lblclient" runat="server" Text="Client"></asp:Label></div>
                        <div class="col-lg-6">
                            <asp:DropDownList ID="ddlclient" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnladdclient" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lnlclient" runat="server" Text="Enter the Client"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtaddclient" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter the Client"
                                    ControlToValidate="txtaddclient" Font-Size="XX-Small" ValidationGroup="AddNewcli"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lnlclientcode" runat="server" Text="Enter the Client Shortcode"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtclientshortcode" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter the Client Shortcode"
                                    ControlToValidate="txtclientshortcode" Font-Size="XX-Small" ValidationGroup="AddNewcli"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnaddclient" runat="server" Text="Add" CssClass="btn btn-primary"
                                    ValidationGroup="AddNewCli" OnClick="btnaddclient_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel runat="server" ID="pnlproject">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlProjectList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProjectList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlAddProject" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblAddProject" runat="server" Text="Enter the Project"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtAddProject" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqfvProject" runat="server" ErrorMessage="Enter the Project"
                                    ControlToValidate="txtAddProject" Font-Size="XX-Small" ValidationGroup="AddNewPro"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnAddProject" runat="server" Text="Add" CssClass="btn btn-default"
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
                                <asp:DropDownList ID="ddlModuleList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <asp:Button ID="Button2" Text="AddTask" runat="server" CssClass="btn btn-default"
                                OnClick="btnAdd_Click" />
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlAddModule" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblAddModule" runat="server" Text="Enter the Module"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtAddModule" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqfvModule" runat="server" ErrorMessage="Enter the Module"
                                    ControlToValidate="txtAddModule" Font-Size="XX-Small" ValidationGroup="AddNewMod"></asp:RequiredFieldValidator>
                                <asp:Button ID="Button1" runat="server" ValidationGroup="AddNewMod" CssClass="btn btn-default"
                                    Text="Add" OnClick="btnAddModule_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnlAddTask" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblAddTask" runat="server" Text="Enter the Task"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtAddTask" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="refvTask" runat="server" ErrorMessage="Enter the Task"
                                    ControlToValidate="txtAddTask" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblTaskDesc" runat="server" Text="Description"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" required="" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please select the Client"
                                ControlToValidate="ddlclient" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select the Project"
                                ControlToValidate="ddlProjectList" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select the Module"
                                ControlToValidate="ddlModuleList" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="pnltaskdes" runat="server">
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lbl" runat="server" Text="Task Priority"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:DropDownList ID="ddlPriority" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqfvPriority" runat="server" InitialValue="0" ErrorMessage="Select the priority"
                                    ControlToValidate="ddlPriority" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="lblPlanneddate" runat="server" Text="Planned Start Date"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtplanneddate" runat="server" ToolTip="dd-mm-yyyy" Width="100px"
                                    ReadOnly="true"></asp:TextBox><a href="#" onclick="showCalendarControl('<%=txtplanneddate.ClientID%>')">
                                        <img id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                            vertical-align: middle;" /></a>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter the Date"
                                    ControlToValidate="txtplanneddate" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                <asp:Label ID="Label1" runat="server" Text="Planned End Date"></asp:Label>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtplanndend" runat="server" ToolTip="dd-mm-yyyy" Width="100px"
                                    ReadOnly="true"></asp:TextBox><a href="#" onclick="showCalendarControl('<%=txtplanndend.ClientID%>')">
                                        <img id="img2" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                            vertical-align: middle;" /></a>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter the Date"
                                    ControlToValidate="txtplanndend" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Date" runat="server"
                                    ControlToValidate="txtplanneddate" ControlToCompare="txtplanndend" Operator="LessThanEqual"
                                    Type="Date" ErrorMessage="End Date must be greater or equal to start Date." Display="None"></asp:CompareValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <asp:Button ID="btnAddTask" runat="server" Text="Save" CssClass="btn btn-primary"
                                OnClientClick="return daterange()" OnClick="btnAddTask_Click" />
                            <asp:Button ID="Button3" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="btnreset_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="Date" />
                        </div>
                    </asp:Panel>
                    <br />
                </div>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
    </div>
</asp:Content>
