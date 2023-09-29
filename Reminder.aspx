<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="Reminder.aspx.cs" Inherits="Includes_WebForm_Reminder" Title="Reminder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>PaySlips</title>
    <style type="text/css">
        .dropdown-menu
        {
            color: #2970AD !important;
            background-color: #fff !important;
        }
    </style>
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-New/js/jquery.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="../bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../DataTables-1.10.10/media/js/jquery-1.11.1.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <link href="../DataTables-1.10.10/media/css/dataTables.jqueryui.css" rel="stylesheet"
        type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <link href="../bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet" type="text/css" />
    <link href="../DataTables-1.10.10/media/css/dataTables.jqueryui.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../DataTables-1.10.10/media/css/jquery.dataTables.css" rel="stylesheet"
        type="text/css" />
    <link href="../DataTables-1.10.11/media/css/dataTables.bootstrap4.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../DataTables-1.10.11/media/css/responsive.bootstrap.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../DataTables-1.10.10/media/js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/dataTables.responsive.min.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/responsive.bootstrap.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../css/multiselect.css" rel="stylesheet" type="text/css" />
    <%--ADDED BY SOPHIA--%>
    <script src="../js/multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=employeeslist]').multiselect({
                includeSelectAllOption: true
            });

            //document.querySelector('.body').setAttribute('onclick', 'hideCalendar()')
            $('[id*=ddlweekly]').multiselect({
                includeSelectAllOption: true
            });
            $('[id*=ddlMonthly]').multiselect({
                includeSelectAllOption: true
            });
            $('[id*=ddlyearlywise]').multiselect({
                includeSelectAllOption: true
            });
            $('[id*=ddlmonth]').multiselect({
                includeSelectAllOption: true
            });
            $('button.multiselect').click(function () {
                $(this).closest('div.btn-group').toggleClass('open');
            });
        })


        //        var id = '';
        //        function showCalendar() {
        //            id = event.target.id
        //            setTimeout(function () {
        //                showCalendarControl(id, '');
        //            }, 0);
        //        }
        //        var old = setCalendarControlDate;
        //        setCalendarControlDate = function (year, month, day) {
        //            calendarControl.setDate(year, month, day);
        //            var val = $('#' + id).val();
        //            $('#' + id).val(val.replaceAll('-', '/'))
        //            old.apply(window, arguments);
        //        };
        function isvalidTaskName() {
            var grid = document.getElementById('<%=grdreminderlist.ClientID%>');

            if (grid.rows.length > 0) {
                if (grid.rows[grid.rows.length - 1].cells[0].childNodes[1].value == "") {
                    return (" - TaskName\n");
                }
                else {
                    return "";
                }
            }
        }
        //        function isvalidTaskFrequency() {
        //            var grid = document.getElementById('<%=grdreminderlist.ClientID%>');

        //            if (grid.rows.length > 0) {
        //                if (grid.rows[grid.rows.length - 1].cells[1].childNodes[1].value == "") {
        //                    return (" - TaskFrequency\n");
        //                }
        //                else {
        //                    return "";
        //                }
        //            }
        //        }
        //        function isvalidDateofReminder() {
        //            var grid = document.getElementById('<%=grdreminderlist.ClientID%>');

        //            if (grid.rows.length > 0) {

        //                if (grid.rows[grid.rows.length - 1].cells[2].childNodes[1].value == "") {
        //                    return (" - DateofReminder\n");
        //                }
        //                else {
        //                    return "";
        //                }
        //            }
        //        }
        function isvalidPeopleReminder() {
            var grid = document.getElementById('<%=grdreminderlist.ClientID%>');
            if (grid.rows.length > 0) {
                if (grid.rows[grid.rows.length - 1].cells[3].childNodes[1].childNodes[0].selectedOptions.length == 0) {
                    return (" - PeopleReminder\n");
                }
                else {
                    return "";
                }
            }
        }

        function validatereminder() {
           
            var summary = "";
            summary += isvalidTaskName();
            //summary += isvalidTaskFrequency();
            //summary += isvalidDateofReminder();
            summary += isvalidPeopleReminder();

            if (summary != "") {
                alert("Please correct the following fields:\n" + summary);
                return false;
            }
            else {
                return "";
            }
        }

        //        function hideCalendar() {
        //            if (document.querySelector('#CalendarControlIFrame').style.display == 'block') {
        //                hideCalendarControl();
        //            }

        //        }
        //
        function fnShowRmainder(a) {
            $('.ddlTaskFrequency').each(function (index, ele) {
                $(ele).attr('index', index).trigger('change')
            })
        }
        function dateshideandshow(obj) {
            debugger
            var value = obj;
            var index = $(obj).attr('index');
            var Ddddatesdiv = $('.Ddddatesdiv')
            var DddDatesDiv_ = $('.DddDatesDiv_')
            var DddYearsDiv_ = $('.DddYearsDiv_')
            var ddlbiweekdiv = $('.ddlbiweekdiv')
            var ddlbimonthlydiv = $('.ddlbimonthlydiv')
            var getvalue = value.options[value.selectedIndex].value;
            //var gettext = value.options[value.selectedIndex].text;
            if (Ddddatesdiv.length != 0) {
                Ddddatesdiv[index].style.display = "none";
                DddDatesDiv_[index].style.display = "none";
                DddYearsDiv_[index].style.display = "none";
                ddlbiweekdiv[index].style.display = "none";
                ddlbimonthlydiv[index].style.display = "none";
            } else {
                Ddddatesdiv.style.display = "none";
                DddDatesDiv_.style.display = "none";
                DddYearsDiv_.style.display = "none";
                ddlbiweekdiv.style.display = "none";
                ddlbimonthlydiv.style.display = "none";
            }
            var html = '<button type="button" class="multiselect-option dropdown-item"  style="width:100%"></button>';
            if (getvalue == 2) {
                if ($('#Dddselectweeklydiv .multiselect-container button').length == 0) {
                    $('#Dddselectweeklydiv .multiselect-container.dropdown-menu').html(html);
                }
                if (Ddddatesdiv[index] != undefined)
                    Ddddatesdiv[index].style.display = 'block';
                else
                    Ddddatesdiv.style.display = 'block';

            }
            else if (getvalue == 3) {
                if ($('#Dddselectbiweek .multiselect-container button').length == 0) {
                    $('#Dddselectbiweek .multiselect-container.dropdown-menu').html(html);

                }
                if (ddlbiweekdiv[index] != undefined)
                    ddlbiweekdiv[index].style.display = "block"
                else
                    ddlbiweekdiv.style.display = 'block';
            }
            else if (getvalue == 4) {
                if ($('#Dddselectmonthlydiv .multiselect-container button').length == 0) {
                    $('#Dddselectmonthlydiv .multiselect-container.dropdown-menu').html(html);

                }
                if (DddDatesDiv_[index] != undefined)
                    DddDatesDiv_[index].style.display = "block"
                else
                    DddDatesDiv_.style.display = 'block';
            }
            else if (getvalue == 5) {
                if ($('#Dddbimonthlydiv .multiselect-container button').length == 0) {
                    $('#Dddbimonthlydiv .multiselect-container.dropdown-menu').html(html);

                }
                if (ddlbimonthlydiv[index] != undefined)
                    ddlbimonthlydiv[index].style.display = "block"
                else
                    ddlbimonthlydiv.style.display = 'block';
            }
            else if (getvalue == 6) {
                if ($('#ddlyearlywise .multiselect-container button').length == 0 && $('#ddlmonth .multiselect-container button').length == 0) {
                    $('#ddlyearlywise .multiselect-container.dropdown-menu').html(html);
                    $('#ddlmonth .multiselect-container.dropdown-menu').html(html);

                }
                if (DddYearsDiv_[index] != undefined)
                    DddYearsDiv_[index].style.display = "block"
                else
                    DddYearsDiv_.style.display = 'block';
            }
            $('.multiselect-container.dropdown-menu').css('width', '120%');
        }

        function fnShowControl(id, index) {

            $($('#' + id)[index]).css('display', '');
        }
        
    </script>
    <style type="text/css">
        .dropdown-item
        {
            width: 100%;
            background-color: White !important;
            color: #555555;
            margin-bottom: -2px;
            border: none;
        }
        .btn-group
        {
            width: 100%;
            padding: 3px;
        }
        .multiselect-container .multiselect-option:hover
        {
            background-color: White !important;
            color: Black;
        }
        .form-check label
        {
            font-weight: 200;
        }
        .form-check input
        {
            margin-right: 10px !important;
        }
        .multiselect-container.dropdown-menu
        {
            max-height: 300px !important;
            overflow-y: auto;
            padding: 0;
            background-color: White !important; /* min-height: 100px;                                                                                                                               
            max-height: 100px;
            overflow-y: scroll;*/
        }
        .form-check
        {
            float: left;
        }
        .multiselect
        {
            width: inherit;
            background-color: white;
            border: 1.6px solid #cccccc;
            padding: 4px;
        }
        #CalendarControl .weekday, #CalendarControl .weekend, #CalendarControl .current
        {
            padding: 2px 11px !important;
            width: 2.645em !important;
        }
        .body
        {
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <asp:GridView ID="grdreminderlist" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
        UseAccessibleHeader="true" AutoGenerateColumns="false" CellSpacing="0" Width="70%"
        CellPadding="4" ForeColor="#333333" OnRowDataBound="grdreminderlist_RowDataBound" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <%--<asp:TemplateField HeaderText="Id" FooterStyle-CssClass="danger">
                <ItemTemplate runat="server">
                     <asp:Label ID="hfdid" Width="5px" runat="server" Text='<%#Bind("Id")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Task Name" FooterStyle-CssClass="danger">
                <ItemTemplate runat="server">
                    <asp:TextBox ID="txtTaskName" CssClass="TextField" Width="200px" runat="server" autocomplete="off"
                        Text='<%#Bind("TaskName")%>'></asp:TextBox>
                    <asp:HiddenField ID="hfdid" runat="server" Value='<%#Bind("Id")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Task Frequency" FooterStyle-CssClass="danger">
                <ItemTemplate runat="server">
                    <div class="col-xs-5 input-group">
                        <asp:DropDownList ID="ddlTaskFrequency" data-toggle="tooltip" data-placement="right"
                            onChange="dateshideandshow(this)" ToolTip="Task Frequency" CssClass="form-control ddlTaskFrequency"
                            runat="server" required="" Width="200px">
                            <%--<asp:ListItem Text="Select" ></asp:ListItem>--%>
                            <asp:ListItem Text="Daily" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Weekly" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Bi Weekly" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Monthly" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Bi Monthly" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Yearly" Value="6"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date of Reminder">
                <ItemTemplate runat="server">
                    <div class="form-group Ddddatesdiv" style="display: none">
                        <div id="Dddselectweeklydiv" onclick="">
                            <asp:ListBox ID="ddlweekly" onChange="" SelectionMode="Multiple" runat="server">
                                <asp:ListItem Value="2" Text="Monday" />
                                <asp:ListItem Value="3" Text="Tuesday" />
                                <asp:ListItem Value="4" Text="Wednesday" />
                                <asp:ListItem Value="5" Text="Thursday" />
                                <asp:ListItem Value="6" Text="Friday" />
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="form-group ddlbiweekdiv" style="display: none">
                        <div id="Dddselectbiweek" onclick="">
                            <asp:DropDownList ID="ddlbiweekly" onChange="" runat="server">
                                <asp:ListItem Value="1" Text="Odd Week" />
                                <asp:ListItem Value="2" Text="Even Week" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group DddDatesDiv_" style="display: none">
                        <div id="Dddselectmonthlydiv" onclick="">
                            <asp:ListBox ID="ddlMonthly" onChange="" SelectionMode="Multiple" runat="server"
                                Width="200px">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                                <asp:ListItem Value="10" />
                                <asp:ListItem Value="11" />
                                <asp:ListItem Value="12" />
                                <asp:ListItem Value="13" />
                                <asp:ListItem Value="14" />
                                <asp:ListItem Value="15" />
                                <asp:ListItem Value="16" />
                                <asp:ListItem Value="17" />
                                <asp:ListItem Value="18" />
                                <asp:ListItem Value="19" />
                                <asp:ListItem Value="20" />
                                <asp:ListItem Value="21" />
                                <asp:ListItem Value="22" />
                                <asp:ListItem Value="23" />
                                <asp:ListItem Value="24" />
                                <asp:ListItem Value="25" />
                                <asp:ListItem Value="26" />
                                <asp:ListItem Value="27" />
                                <asp:ListItem Value="28" />
                                <asp:ListItem Value="29" />
                                <asp:ListItem Value="30" />
                                <asp:ListItem Value="31" />
                            </asp:ListBox>
                        </div>
                    </div>
                    <div class="form-group ddlbimonthlydiv" style="display: none">
                        <div id="Dddbimonthlydiv" onclick="">
                            <asp:DropDownList ID="ddlbimonthlywise" onChange="" runat="server">
                                <asp:ListItem Value="1" Text="Odd Month" />
                                <asp:ListItem Value="2" Text="Even Month" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group DddYearsDiv_" style="display: none">
                        <div id="Dddselectyearlydiv" onclick="">
                            <asp:ListBox ID="ddlyearlywise" onChange="" SelectionMode="Multiple" runat="server"
                                Width="200px">
                                <asp:ListItem Value="1" Text="January" />
                                <asp:ListItem Value="2" Text="February" />
                                <asp:ListItem Value="3" Text="March" />
                                <asp:ListItem Value="4" Text="April" />
                                <asp:ListItem Value="5" Text="May" />
                                <asp:ListItem Value="6" Text="June" />
                                <asp:ListItem Value="7" Text="July" />
                                <asp:ListItem Value="8" Text="August" />
                                <asp:ListItem Value="9" Text="September" />
                                <asp:ListItem Value="10" Text="October" />
                                <asp:ListItem Value="11" Text="November" />
                                <asp:ListItem Value="12" Text="December" />
                            </asp:ListBox>
                            <asp:ListBox ID="ddlmonth" onChange="" SelectionMode="Multiple" runat="server" Width="200px">
                                <asp:ListItem Value="1" />
                                <asp:ListItem Value="2" />
                                <asp:ListItem Value="3" />
                                <asp:ListItem Value="4" />
                                <asp:ListItem Value="5" />
                                <asp:ListItem Value="6" />
                                <asp:ListItem Value="7" />
                                <asp:ListItem Value="8" />
                                <asp:ListItem Value="9" />
                                <asp:ListItem Value="10" />
                                <asp:ListItem Value="11" />
                                <asp:ListItem Value="12" />
                                <asp:ListItem Value="13" />
                                <asp:ListItem Value="14" />
                                <asp:ListItem Value="15" />
                                <asp:ListItem Value="16" />
                                <asp:ListItem Value="17" />
                                <asp:ListItem Value="18" />
                                <asp:ListItem Value="19" />
                                <asp:ListItem Value="20" />
                                <asp:ListItem Value="21" />
                                <asp:ListItem Value="22" />
                                <asp:ListItem Value="23" />
                                <asp:ListItem Value="24" />
                                <asp:ListItem Value="25" />
                                <asp:ListItem Value="26" />
                                <asp:ListItem Value="27" />
                                <asp:ListItem Value="28" />
                                <asp:ListItem Value="29" />
                                <asp:ListItem Value="30" />
                                <asp:ListItem Value="31" />
                            </asp:ListBox>
                        </div>
                    </div>
                    <%--<asp:TextBox ID="txtDateofreminder" ToolTip="dd-mm-yyyy" onclick="showCalendar()"
                        runat="server" autocomplete="off" Width="200px" Text='<%#Bind("DateofReminder","{0:d}")%>'></asp:TextBox>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="People to be Reminded" FooterStyle-CssClass="danger">
                <ItemTemplate runat="server">
                    <asp:ListBox runat="server" autocomplete="off" ID="employeeslist" SelectionMode="multiple"
                        DataTextField="FirstName" DataValueField="Empid"></asp:ListBox>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblRowId" autocomplete="off" runat="server" Text='<%#Bind("Id")%>'>
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <%--<asp:CommandField HeaderText="Delete" ItemStyle-ForeColor="#2E6DA4" ShowDeleteButton="true"
                runat="server"></asp:CommandField>--%>
        </Columns>
        <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
        <HeaderStyle BackColor="#428bca" />
    </asp:GridView>
    <div>
        <asp:Button ID="btnaddrow" runat="server" Text="Add New Row" OnClick="ButtonAddReminder_Click"
            ValidationGroup="test" CssClass="btn btn-sm btn-default" OnClientClick="return validatereminder();" />
    </div>
    <br />
    <div class="container" style="margin-left: 700px">
        <asp:Button ID="btnReminderSave" runat="server" Text="Save" Width="65px" class="btn btn-primary"
            OnClick="btnReminderSave_Click" ValidationGroup="test" OnClientClick="return validatereminder();" />
    </div>
</asp:Content>
