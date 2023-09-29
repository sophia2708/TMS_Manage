<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="EnterTimeSheet.aspx.cs" Inherits="Includes_WebForm_EnterTimeSheet"
    Title="Enter Timesheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/Calendar.js" type="text/javascript">    
    </script>
    <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Scripts/moment.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <link href="../CSS/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <style type="text/css">
        .modal-header-primary
        {
            color: #fff;
            padding: 9px 15px;
            border-bottom: 1px solid #eee;
            background-color: #428bca;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
        .modal-title
        {
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        //          To close the datepicker wherever click the mouse
        $(document).mouseup(function (e) {
            var container = $("#CalendarControl");
            if (!container.is(e.target) && container.has(e.target).length === 0)
                hideCalendarControl()
        });

        $(document).ready(function () {
            //alert()
            $('#dtpickerstart').datetimepicker(
            //maxDate: new Date(),
              {format: 'DD-MM-YYYY' }
            );
            $('#dtpickerend').datetimepicker({
                //                maxDate: new Date(), format: 'MM/DD/YYYY'
                format: 'DD-MM-YYYY'
            });
            $("#dtpickerstart").on("dp.change", function (e) {
                $('#dtpickerend').data("DateTimePicker").minDate(e.date)
            });
            $("#dtpickerend").on("dp.change", function (e) {
                // $('#dtpickerstart').data("DateTimePicker").maxDate(e.date);
            });
        });

        window.history.forward(1);
        function OpenPopUp(popDiv) {
            document.getElementById('popDiv').style.display = 'block'
        }
        function Click_close() { $('#MyModalPopup').modal('hide'); $('#MyPopup').modal('hide'); }
        function HidePopUp(popDiv) {
            document.getElementById('popDiv').style.display = 'none';
            //             return false;
        }
        function daterange() {
            //                    var startdate = new Date(2014, 01, 01);
            //                    var start = new Date(startdate);
            //                    start = document.getElementById('<%=txtplanneddate.ClientID%>').value;
            //                    // alert(start); 
            //                    var enddate = new Date(2014, 01, 01);
            //                    var end = new Date(enddate);
            //                    end = document.getElementById('<%=txtplanndend.ClientID%>').value;

            var isValid = false;
            isValid = Page_ClientValidate('AddNewTask');
            if (isValid) {
                isValid = Page_ClientValidate('Date');
            }
            return isValid;
        }
        function ReqiredFeildValidation() {

            if (document.getElementById('<%=txtDate.ClientID%>').value == "") {
                alert("Select the task date");
                document.getElementById('<%=txtDate.ClientID%>').focus();
                return false;
            }
            else {
                return true;
            }
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else {
                return true;
            }
            return false;
        }
        function validaterequired() {
        debugger
            var summary = "";          
            summary += isvalidProject();
            summary += isvalidModule();
            summary += isvalidTask();
            summary += isvalidStatus();          
//            if (strUser1 != null) {
            summary += isvalidWorkmode();
//            }
            //            summary += isvalidHours();
            if (summary != "") {
                alert("Please correct the following fields:\n" + summary);
                return false;
            }
            else {
                return Calculate();
            }
        }
        //        Added by sophia
        function weekdays(flag) {
            // var today = new date();

            var gettingdate = document.getElementById('<%=txtDate.ClientID%>');
            var date = gettingdate.value.split('-');
            var holiDayList = JSON.parse(document.getElementById('<%=holiDayList.ClientID%>').value);
            var obj = holiDayList.filter(function (val) { return val.Holiday_Date == date[2] + '-' + date[1] + '-' + date[0] });
            date = new Date(date[1] + '/' + date[0] + '/' + date[2]);

            var holiday = '';
            if (obj.length > 0) holiday = obj[0].Holiday_Name
            if (flag == 0) {
                if (date.getDay() == 6)
                    document.getElementById('holidays').innerHTML = 'The selected day is <b>Saturday</b>.<br/> Are you Sure want to continue the timesheet entry for the day?'
                else if (date.getDay() == 0)
                    document.getElementById('holidays').innerHTML = ('The selected day is <b>Sunday</b>.<br/> Are you Sure want to continue the timesheet entry for the day?');
            } else if (flag == 1) {
                document.getElementById('holidays').innerHTML = ('The selected day is a <b>Holiday(' + holiday + ')</b>.<br/> Are you Sure want to continue the timesheet entry for the day?');

            }


            document.getElementById('myModal').classList.add("in");
            document.getElementById('myModal').style.display = 'block';
        }


        function closeModel() {
            document.getElementById('myModal').classList.remove("in");
            document.getElementById('myModal').style.display = 'none';
        }
        function isvalidWorkmode() {
            var grid = document.getElementById('<%=ddlworkmode.ClientID%>');
            var strUser = grid.options[grid.selectedIndex].value;
            if (strUser == 0) {
                return (" - WorkMode\n");
            }
            else {
                return "";
            }
        }

        function isvalidProject() {

            var grid = document.getElementById('<%=grdEnterTimesheet.ClientID%>');

            if (grid.rows.length > 0) {

                if (grid.rows[grid.rows.length - 2].cells[1].childNodes[1].selectedIndex == 0) {
                    return (" - Project\n");
                }
                else {
                    return "";
                }
            }
        }
        function isvalidModule() {

            var grid = document.getElementById('<%=grdEnterTimesheet.ClientID%>');

            if (grid.rows.length > 0) {

                if (grid.rows[grid.rows.length - 2].cells[2].childNodes[1].selectedIndex == 0) {
                    return (" - Module\n");
                }
                else {
                    return "";
                }
            }
        }
        function isvalidTask() {

            var grid = document.getElementById('<%=grdEnterTimesheet.ClientID%>');

            if (grid.rows.length > 0) {

                if (grid.rows[grid.rows.length - 2].cells[3].childNodes[1].selectedIndex == 0) {
                    return (" - Task\n");
                }
                else {
                    return "";
                }
            }
        }
        function isvalidStatus() {

            var grid = document.getElementById('<%=grdEnterTimesheet.ClientID%>');
            if (grid.rows.length > 0) {
                //alert("test " + grid.rows[2].cells[3].childNodes[1].selectedIndex);
                if (grid.rows[grid.rows.length - 2].cells[7].childNodes[1].selectedIndex == 0) {
                    return (" - Status\n");
                }
                else {
                    return "";
                }
            }
        }
        function isvalidHours() {

            var grid = document.getElementById('<%=grdEnterTimesheet.ClientID%>');
            if (grid.rows.length > 0) {
                if (grid.rows[grid.rows.length - 2].cells[8].childNodes[1].value == "") {
                    return (" - Hours\n");
                }
                if (grid.rows[grid.rows.length - 2].cells[8].childNodes[1].value >= 24) {
                    //alert('Hours exceeds');
                    return (" - Hours exceeds");
                }
                else {
                    return "";
                }
            }
        }
        var Hours = /^[0-9.]+$/;
        function is_valid_integer(textField) {
            debugger;
            if (!Hours.test(textField.value)) {
                alert("Please enter numeric values");
                textField.value = "";
                textField.focus();
                return false;
            }
        }
        function fundelete() {
            var result = confirm("Want to delete?");
            if (result == true) {
                return true;
            }
            return false;
        }
        function fun() {
            //validaterequired();
            var grid = document.getElementById('<%=grdEnterTimesheet.ClientID%>');

            if (grid.rows[grid.rows.length - 2].cells[12].childNodes[1].value >= 24) {
                alert('Hours exceeds');
                return false;
            }
            else {
                return true;
            }
        }
        function Calculate() {
            var total = 0.0;
            var gridview = document.getElementById("<%=grdEnterTimesheet.ClientID%>");
            var rowcount = document.getElementById("<%=grdEnterTimesheet.ClientID%>").rows.length;
            for (i = 1; i <= rowcount - 2; i++) {

                if (gridview.rows[i].cells[12].childNodes[1].value != "") {
                    var node = gridview.rows[i].cells[12].childNodes[1].value;
                    total += parseFloat(node);
                    if (total >= 23.60) {
                        alert('Hours Exceeds');
                        gridview.rows[i].cells[12].childNodes[1].value = "";
                        gridview.rows[i].cells[12].childNodes[1].focus();
                        return false;
                    }
                }
            }
        }
        document.addEventListener('DOMContentLoaded', function () {
            // Function to get the value of a query parameter from the URL
            function getQueryVariable(variable) {
                var query = window.location.search.substring(1);
                var vars = query.split('&');
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split('=');
                    if (decodeURIComponent(pair[0]) === variable) {
                        return decodeURIComponent(pair[1]);
                    }
                }
                return null;
            }

            // Retrieve the selected date from the query parameter
            var selectedDate = getQueryVariable('selectedDate');

            // Set the selected date in the DatePicker control (if available)
            if (selectedDate) {
                var datePicker2 = document.getElementById('<%= txtDate.ClientID %>');
                datePicker2.value = selectedDate;
            }
        });           
    </script>
    <div id="entertimesheet" style="padding-top: 10px;" align="center">
        <table border="0" width="750px" align="center" class="jumbotron table-responsive">
            <tbody>
                <tr>
                    <td colspan="2" class="Header_style">
                        Enter TimeSheet
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="table-responsive" align="center" border="0" width="90%" cellpadding="3"
                            cellspacing="2">
                            <tr>
                                <td class="td_smalllabel_Style" >
                                    <asp:Label ID="lblDate" runat="server" Text="Select task Date" Style="font-size: 12pt"
                                        ForeColor="#666666"></asp:Label>
                                </td>
                                <td class="td_input_Style_" width="150px"  >
   
                                    <asp:TextBox ID="txtDate" ToolTip="dd-mm-yyyy" runat="server" Width="100px" ReadOnly="true"></asp:TextBox>
                                    <a href="#" onclick="showCalendarControl('<%=txtDate.ClientID%>','')">
                                        <img id="imgDOR" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif"
                                            style="border: 0; vertical-align: middle;" /></a>
                                </td>
                                <td>                              
                                    <asp:Button Text="Go" ID="btnGo" runat="server" CssClass="btn btn-sm btn-primary"
                                        OnClick="btnGo_Click" />
                                </td>
                                </tr>
                                <tr>
                                <td>

                                     <asp:Label ID="lblworkmode" runat="server" Text="Mode of Work"   Style="font-size: 12pt"
                                        ForeColor="#666666"></asp:Label>
                                </td>                           
                                <td><asp:DropDownList ID="ddlworkmode" onChange="" runat="server">
                                        <asp:ListItem Value="0" Text="--Select--" />
                                        <asp:ListItem Value="1" Text="Work from office" />
                                        <asp:ListItem Value="2" Text="Work from home" />
                                    </asp:DropDownList></td>
                                
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <div class="table table-responsive">
            <asp:GridView ID="grdEnterTimesheet" runat="server" CssClass="table table-bordered table-responsive"
                DataKeyNames="Row" ShowFooter="True" AutoGenerateColumns="False" CellPadding="4"
                Width="100%" ForeColor="#333333" GridLines="None" CellSpacing="0" OnRowDataBound="grdEnterTimesheet_RowDataBound"
                OnRowDeleting="grdEnterTimesheet_RowDeleting" Font-Size="Small" OnSelectedIndexChanged="grdEnterTimesheet_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="Row" HeaderText="No">
                        <%--<ControlStyle Width="50px"></ControlStyle>--%>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblProjectName" runat="server" Visible="false" Text='<%# Bind("ProjectName") %>'></asp:Label>
                            <asp:Label ID="lblProjectId" runat="server" Visible="false" Text='<%# Bind("ProjectId") %>'></asp:Label>
                            <%--<asp:Label ID="lblWorkModeId" runat="server" Visible="false" Text='<%# Bind("WorkModeId") %>'></asp:Label>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskName" runat="server" Visible="false" Text='<%# Bind("TaskName")%>'></asp:Label>
                            <asp:Label ID="lblTaskId" runat="server" Visible="false" Text='<%# Bind("TaskId") %>'></asp:Label>
                        </ItemTemplate>
                        <%-- <ItemStyle Width="50px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblModuleName" runat="server" Visible="false" Text='<%# Bind("ModuleName")%>'> </asp:Label>
                            <asp:Label ID="lblModuleId" runat="server" Visible="false" Text='<%# Bind("ModuleId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Project">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlProject" runat="server" Width="100px" DataTextField="ProjectName"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged"
                                AppendDataBoundItems="true" DataValueField="ProjectId">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <%--  <HeaderStyle Width="90px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Module">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlModuleList" Width="100px" runat="server" DataTextField="ModuleName"
                                DataValueField="ModuleId" AutoPostBack="true" OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged"
                                AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <%--<HeaderStyle Width="110px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlTask" Width="100px" runat="server" DataTextField="TaskName"
                                OnSelectedIndexChanged="ddlTask_SelectedIndexChanged" AutoPostBack="true" DataValueField="TaskId"
                                AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <%--    <HeaderStyle Width="100px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Task Description">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTaskDesc" Width="240px" TextMode="MultiLine" runat="server" Text='<%#Bind("TaskDescription")%>'></asp:TextBox>
                        </ItemTemplate>
                        <%-- <HeaderStyle Width="230px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issues" FooterStyle-CssClass="danger">
                        <ItemTemplate>
                            <asp:TextBox ID="txtIssues" Width="100px" runat="server" Text='<%#Bind("Issues")%>'></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" UseSubmitBehavior="true"
                                ValidationGroup="test" CssClass="btn btn-sm btn-default" CommandName="Add" OnClick="ButtonAdd_Click"
                                OnClientClick="return validaterequired();" />
                        </FooterTemplate>
                        <%--   <HeaderStyle Width="160px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Object">
                        <ItemTemplate>
                            <asp:TextBox ID="txtObject" Width="100px" runat="server" Text='<%# Bind("Object")%>'></asp:TextBox>
                        </ItemTemplate>
                        <%--  <HeaderStyle Width="70px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("StatusId")%>' Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalText" runat="server" Text="Total Hours" Font-Bold="true" />
                        </FooterTemplate>
                        <%--<HeaderStyle Width="130px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hours">
                        <ItemTemplate>
                            <asp:TextBox ID="txtHours" Width="70px" runat="server" MaxLength="5" Text='<%#Bind("Hours")%>'
                                onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:RangeValidator ID="HoursValidator" runat="server" Font-Size="XX-Small" ValidationGroup="test"
                                ErrorMessage="Hours exceeded" ControlToValidate="txtHours" MaximumValue="23.59"
                                MinimumValue="0" Type="Double">
                            </asp:RangeValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalHours" runat="server" onchange="HoursExceed(this);" Font-Bold="true" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRowId" runat="server" Text='<%#Bind("id")%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Delete" ItemStyle-ForeColor="#2E6DA4" ShowDeleteButton="true"
                        runat="server">
                        <%--  <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle Width="60px"></ItemStyle>--%>
                    </asp:CommandField>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
                <%--<EditRowStyle BackColor="#2461BF" />
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" CssClass="FixedHeader" Font-Size="12px"
                                    ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
            </asp:GridView>
        </div>
        <div id="myModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="model-header modal-header-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                            &times;</button>
                        <h4 class="modal-title" direction="left">
                            Alert</h4>
                    </div>
                    <div class="modal-body">
                        <p id="holidays">
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="closeModel()">
                            Cancel</button>
                        <asp:Button runat="server" Text="Yes" ToolTip="Save Timesheet" class="btn btn-primary"
                            OnClick="btnYes_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="container">
            <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" class="btn btn-primary"
                ValidationGroup="test" OnClientClick="return validaterequired();weekdays()" OnClick="btnSave_Click" /></div>
    </div>
    <div class="popup" id="popDiv" style="display: none; position: absolute; margin-top: -14%;
        margin-left: 11.5%;">
        <table align="center" border="0" width="750px" class="wpn_content table-responsive">
            <tbody>
                <tr>
                    <td colspan="2" class="Header_style">
                        Task Master & Self Assignment
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="center" class="table-responsive" border="0" width="60%" cellpadding="3"
                            cellspacing="2">
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lblclient" runat="server" Text="Client"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <asp:DropDownList ID="ddlclient" runat="server">
                                        <%--AutoPostBack="True"   onselectedindexchanged="ddlclient_SelectedIndexChanged"--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                        ErrorMessage="Select the client" ControlToValidate="ddlclient" Font-Size="XX-Small"
                                        ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lblAddTask" runat="server" Text="Enter the Task"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <asp:TextBox ID="txtAddTask" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="refvTask" runat="server" ErrorMessage="Enter the Task"
                                        ControlToValidate="txtAddTask" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lblTaskDesc" runat="server" Text="Description"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqfDescp" runat="server" ErrorMessage="Enter the Description"
                                        ControlToValidate="txtDescription" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lbl" runat="server" Text="Task Priority"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <asp:DropDownList ID="ddlPriority" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqfvPriority" runat="server" InitialValue="0" ErrorMessage="Select the priority"
                                        ControlToValidate="ddlPriority" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lbltaskcat" runat="server" Text="TaskCategory"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <asp:DropDownList ID="ddltaskcat" runat="server">
                                        <%-- AutoPostBack="True"  OnSelectedIndexChanged="ddltaskcat_SelectedIndexChanged"--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select the category"
                                        ControlToValidate="txtDescription" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lblPlanneddate" runat="server" Text="Planned Start Date"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <%--<div class='input-group date' id='dtpickerstart'>
                                        <asp:TextBox ID="txtplanneddate" CssClass="date" runat="server" ToolTip="dd-mm-yyyy"
                                            Width="100px" ReadOnly="true"></asp:TextBox>
                                        <a href="#" onclick="showCalendarControl('<%=txtplanneddate.ClientID%>')">
                                            <img id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                vertical-align: middle;" /></a>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter the Date"
                                            ControlToValidate="txtplanneddate" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <div class="form-group" style="margin-bottom: 0px;">
                                        <div class='input-group date' id='dtpickerstart'>
                                            <asp:TextBox ID="txtplanneddate" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter the Date"
                                                ControlToValidate="txtplanneddate" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="lblplannedeffortdate" runat="server" Text="Planned Effort Hours"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <asp:TextBox ID="txtplannedeffortdate" runat="server" placeholder="HH:MM:SS" Width="100px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter the Hours"
                                        ControlToValidate="txtplannedeffortdate" onkeypress="return isNumberKey(event)"
                                        Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck"
                                        Type="Double" ControlToValidate="txtplannedeffortdate" ErrorMessage="Text must be Numeric Values"
                                        Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_label_Style">
                                    <asp:Label ID="Label1" runat="server" Text="Planned End Date"></asp:Label>
                                </td>
                                <td class="td_input_Style">
                                    <%--<asp:TextBox ID="txtplanndend" runat="server" ToolTip="dd-mm-yyyy" Width="100px"
                                        ReadOnly="true"></asp:TextBox>
                                    <a href="#" onclick="showCalendarControl('<%=txtplanndend.ClientID%>')">
                                            <img id="img2" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                vertical-align: middle;" /></a>
                                    <a href="#" class="date">
                                        <img id="img2" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                            vertical-align: middle;" /></a>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter the Date"
                                        ControlToValidate="txtplanndend" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup="Date" runat="server"
                                        ControlToValidate="txtplanneddate" ControlToCompare="txtplanndend" Operator="LessThanEqual"
                                        Type="Date" ErrorMessage="End Date must be greater or equal to start Date." Display="None"></asp:CompareValidator>--%>
                                    <div class="form-group" style="margin-bottom: 0px;">
                                        <div class='input-group date' id='dtpickerend'>
                                            <asp:TextBox ID="txtplanndend" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter the Date"
                                                ControlToValidate="txtplanndend" Font-Size="XX-Small" ValidationGroup="AddNewTask"></asp:RequiredFieldValidator>
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <%--OnClientClick="return daterange()"--%>
                                <td>
                                    <asp:Button ID="btnAddTask" runat="server" Text="Save" CssClass="btn btn-Primary"
                                        OnClick="btnAddTask_Click" OnClientClick="return daterange()" />
                                    <asp:Button ID="Button3" runat="server" Text="Cancel" CssClass="btn btn-default"
                                        OnClick="btnCancel_Click" />
                                </td>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Date" />
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
        <input type="hidden" runat="server" value="" id="holiDayList" />
    </div>
</asp:Content>
