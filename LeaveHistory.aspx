<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="LeaveHistory.aspx.cs" Inherits="Includes_WebForm_LeaveHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
    <title>Leave management System</title>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <%--ADDED BY SOPHIA--%>
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
    <script src="../Scripts/moment.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/bootbox.min.js"></script>
    <link href="../css/multiselect.css" rel="stylesheet" type="text/css" />
    <%--ADDED BY SOPHIA--%>
    <script src="../js/multiselect.js" type="text/javascript"></script>
    <%--ADDED BY SOPHIA--%>
    <%--<style type="text/css">
        .MSDD
        {
            position: relative;
            display: inline-block;
        }
        
        .MSDD option
        {
            display: none;
            position: absolute;
            background-color: #f1f1f1;
            min-width: 160px;
            bottom: 50px;
            z-index: 1;
        }--%>
    <%--</style>--%>
    <script type="text/javascript">

        $(function () {
            $('[id*=ddlSelectEmpName]').each(function () {
                $(this).multiselect({
                    includeSelectAllOption: true
                });
            });

            $('[id*=lstleavetype]').each(function () {
                $(this).multiselect({
                    includeSelectAllOption: true
                });
            });

            $('button.multiselect').click(function () {
                $(this).closest('div.btn-group').toggleClass('open');
                
            });
        })

        //Added by sophia
        function dateshideandshows(obj) {
            debugger
            var value = document.getElementById("<%=ddlEmpleaveHistory.ClientID%>");
            var getvalue = value.options[value.selectedIndex].value;
            Dddselectdates.style.display = "none";
            DddFromtodates.style.display = "none";
            //var html = '<button type="button" class="multiselect-option dropdown-item"  style="width:100%"></button>';
            if (getvalue == 2) {
                if ($('#Dddlblselectdates .multiselect-container button').length == 0) {
//                    $('#Dddlblselectdates .multiselect-container.dropdown-menu').html(html);
                }

                Dddselectdates.style.display = "block"
            }
            else if (getvalue == 3) {
                if ($('#DdlselectlblFromdate.multiselect-container button').length == 0) {
//                    $('#DdlselectlblFromdate .multiselect-container.dropdown-menu').html(html);
                }

                DddFromtodates.style.display = "block"
            }

        }

  
         function resetAllControls() {
            $("#MyPopup").find("#ctl00_cphBasic_ddlexistingcompensation,#ctl00_cphBasic_DropDownselectdates,#ctl00_cphBasic_DropDownselectholidays").val("");

        };

        //        function deselect() {
        //            $("#MyPopup").find("#Dddselectdatesdiv,#Dddselectholidaysdiv").val("");

        //        };


        //ADDED BY SOPHIA

        $(document).mouseup(function (e) {
            var container = $("#CalendarControl");
            if (!container.is(e.target) && container.has(e.target).length === 0)
                hideCalendarControl()
        });
        function dropdownhiding() {
            $($($('#DddDateDiv').find('span')[0]).find('div')[1]).show()
        }
        function dropdownhide() {
            $($($('#DddDateDiv_').find('span')[0]).find('div')[1]).show()

        }
        $('body').mouseup(function () {
            $('div.multiselect-container').each(function () {
                if (this.style.display == 'block')
                    this.style.display = 'none'
            });
        });

        function getdatesFocus(id) {
            $($($($(id).find('span')[0]).find('div')[1]).find('button.active')).focus()
            $($($('#DddDateDiv').find('span')[0]).find('div')[1]).hide()
            $($($('#DddDateDiv_').find('span')[0]).find('div')[1]).hide()
            //$('#DropDownselectdates').blur();
        }
        //$("#DddDateDiv").val([]);


        function readyDropDown() {

            $("[id*=DropDownselectdates]").multiselect('destroy')
            $("[id*=DropDownselectdates]").multiselect({});

            $("[id*=DropDownselectholidays]").multiselect('destroy')
            $("[id*=DropDownselectholidays]").multiselect({});

        }


    </script>
    <script type="text/javascript">
        //// ADDED by SOPHIA

        function leaveoptionsvalidation() {
            var selectedValue = document.getElementById("<%=ddlexistingcompensation.ClientID  %>").value
            if (selectedValue == '0') {
                document.getElementById("<%=btnSubmit.ClientID  %>").disabled = false;
            }

            if (selectedValue == '1' || selectedValue == '2' || selectedValue == '-Select options-') {

                document.getElementById("<%=btnSubmit.ClientID  %>").disabled = true;
            }


        }

        function selectdatesvalidation() {

            var selectedvalue1 = document.getElementById("<%=DropDownselectdates.ClientID  %>").value
            if (selectedvalue1 == '')
                document.getElementById("<%=btnSubmit.ClientID  %>").disabled = true;
            else
                document.getElementById("<%=btnSubmit.ClientID  %>").disabled = false;
        }

        function selectholidaysvalidation() {

            var holidays = document.getElementById("<%=DropDownselectholidays.ClientID  %>").value
            if (holidays == '')
                document.getElementById("<%=btnSubmit.ClientID  %>").disabled = true;
            else
                document.getElementById("<%=btnSubmit.ClientID  %>").disabled = false;
        }



        //       TILL HERE ADDED BY SOPHIA


        $(document).ready(function () {
            //Given the Variables for the purpose of gedding the Values of the Table so that the parentnode=null Metadata=null err wont come
            var totalRows = $("#<%= grdLeaveDecision.ClientID %> tr").length;
            var Totrow = $("#<%= grdLeaveHistory.ClientID %> tr").length;

            if (totalRows >= 1) { $('#<%= grdLeaveDecision.ClientID %>').dataTable({}); }
            if (Totrow >= 1) { $('#<%= grdLeaveHistory.ClientID %>').dataTable({ "order": [[2, "desc"]] }); }



            $('#<%= grdEmplopcancle.ClientID %>').dataTable({});
        });

        function openModal() {
            $('#myModal').modal({ backdrop: "static" });

        }
        function lopPopup() { //Added by LOGESH

            $('#lopPopup').modal({ backdrop: "static" });
            $('#lopPopup').css("position", "absolute").css('display', 'block');
        }
        function openModalpopup() {
            $('#MyPopup').modal({ backdrop: "static" });
            readyDropDown(); //ADDED BY SOF
        }
        function OpenconformModal() { $('#MyPopup').modal('hide'); $('#ConformModal').modal('show'); }
        function mymodalclose() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#myModal').modal('hide');
            $('#myModal').fadeOut();
            $('#MyPopup').modal('hide');
            $('#MyPopup').fadeOut();
            $('#lopPopup').modal('hide').css('display', 'none');
            $('#lopPopup').fadeOut();
            $('#ConformModal').modal('hide');
            $('#ConformModal').fadeOut();

        }


    </script>
    <style type="text/css">
        .dropdown-item
        {
            width: 70%;
            background-color: White !important;
            color: #555555;
            margin-bottom: -2px;
            border: none;
        }
        .btn-group
        {
            width: 70%;
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
        #MyPopup
        {
            position: absolute;
            z-index: 10000 !important;
        }
        
        #lopPopup
        {
            display: none;
            position: inherit;
            left: 29%;
            bottom: 20%;
            z-index: 10005 !important;
        }
        
        #myModal
        {
            position: absolute;
            z-index: 100005 !important;
        }
        
        #ConformModal
        {
            position: absolute;
            z-index: 100008 !important;
        }
        
        #ctl00_cphBasic_pnlLeaveBalance
        {
            z-index: 100;
            left: 485px;
            top: 6px;
        }
        
        .modal-header-danger
        {
            color: #fff;
            padding: 9px 15px;
            border-bottom: 1px solid #eee;
            background-color: #d9534f;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
        
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
        
        .messagealert
        {
            width: 70%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
        
        .messagealert1
        {
            width: 70%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <style type="text/css">
        .cal_Theme1 .ajax__calendar_container
        {
            width: 190px;
            z-index: 1000;
            background-color: White;
            border-right-width: 7px;
            border-bottom-color: Gray;
            border-top-color: Gray;
            border: 1px solid #428bca;
        }
        
        .cal_Theme1 .ajax__calendar_header
        {
            background-color: #ffffff;
            margin-bottom: 4px;
        }
        
        .cal_Theme1 .ajax__calendar_title, .cal_Theme1 .ajax__calendar_next, .cal_Theme1 .ajax__calendar_prev
        {
            color: #428bca;
            padding-top: 3px;
        }
        
        .cal_Theme1 .ajax__calendar_body
        {
            background-color: White;
        }
        
        .cal_Theme1 .ajax__calendar_dayname
        {
            color: #428bca;
            margin-bottom: 4px;
            margin-top: 2px;
        }
        
        .cal_Theme1 .ajax__calendar_day
        {
            text-align: center;
        }
        
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year, .cal_Theme1 .ajax__calendar_active
        {
            color: #EEE;
            font-weight: bold;
            background-color: #428bca;
        }
        
        .cal_Theme1 .ajax__calendar_today
        {
            font-weight: bold;
            color: #428bca;
        }
        
        .cal_Theme1 .ajax__calendar_other, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today, .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title
        {
            color: #ff6600;
        }
        
        .input-group > .form-control, .input-group > .input-group-addon, .input-group > .input-group-btn > .btn
        {
            height: 38px;
        }
    </style>
    <script type="text/javascript">


        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(6000);
            $('#Alert_container1').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(6000);
            $('#Alert_container2').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(6000);
            $('#Alert_container3').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(6000);
            $('#alert_container5').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(6000);

        }

    </script>
    <script type="text/javascript">
        var global_HolidaysCount;
        function validate_Holidays(stdate, eddate) {
            try {
                var Obj = {};
                Obj.EmpId = "";  //"#hfdEmpid").val();
                Obj.Startdate = stdate;
                Obj.EndDate = eddate;
                $.ajax({
                    async: false,
                    type: "POST",
                    url: 'LeaveHistory.aspx/Validate_Holiday',
                    data: JSON.stringify(Obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        //debugger;
                        //console.log(msg.d);
                        global_HolidaysCount = msg.d;
                        //console.log(global_HolidaysCount);

                        //$("#divResult").html("success");
                    },
                    failure: function (response) {
                        //debugger;
                    },
                    error: function (xhr, StatusCode) {
                        //debugger;
                        //$("#divResult").html("Something Wrong.");
                    }
                });
            }
            catch (e) {
                alert(e.message);
            }
        }


        function workingDaysBetweenDates(stdate, eddate) {
            if (stdate != null && eddate != null) {
                validate_Holidays(stdate, eddate);
            }
            var startDate = stdate;
            var endDate = eddate;
            //alert("startdate: " + startDate + "enddate: " + eddate);
            //if (endDate > startDate) {
            //}
            //else { }

            var millisecondsPerDay = 86400 * 1000; // Day in milliseconds
            var days = Math.round(Math.abs((startDate - endDate) / millisecondsPerDay)); //// Modified by LOGESH
            if(endDate>=startDate)
            days=days+1;
            //alert("dii" + days);


            // Start just after midnight
            //startDate.setHours(0, 0, 0, 1);
            // End just before midnight
            //endDate.setHours(23, 59, 59, 999);

            //Diff b/w two dates
            var diff = endDate - startDate;


            // Milliseconds between datetime objects 
            //var days = Math.ceil(diff / millisecondsPerDay);
            // Subtract two weekend days for every week in between
            //console.log(days);
            var weeks = Math.floor(days / 7);
            
            //console.log("weeks " + weeks);
            days = days - (weeks * 2);
            //console.log("No_Of_Days " + days);
            if (global_HolidaysCount > 0)
                days = days - (global_HolidaysCount * 1);
            //console.log("global_HolidaysCount " + global_HolidaysCount);
            //console.log("days " + days);

            // Handle special cases
            var startDay = startDate.getDay();
            var endDay = endDate.getDay();
            //console.log("startDay" + startDay);
            //console.log("endDay" + endDay);
            // Remove weekend not previously removed.
            //alert(startDay - endDay)
            if (startDay - endDay > 1)
                days = days - 2;
            else if(endDay == 6 && days>1)
                days = days - 1;

            var calculatedDays = days;
            document.getElementById('<%=txtNoofdays.ClientID  %>').value = calculatedDays;
            //var duration = document.getElementById('txtNoofdays').value;
            if (calculatedDays > 1) {
                //document.getElementById("ddlDuration").value = "Full day";
                document.getElementById("<%=ddlDuration.ClientID  %>").selectedIndex = '0';
            }
            else if (calculatedDays <= 1) {
                //document.getElementById("ddlDuration").value = "Full day";
                document.getElementById("<%=ddlDuration.ClientID  %>").selectedIndex = '0';
            }
        if (startDay == 0 && endDay != 6) {
            days = days - 1;
        }
        if (endDay == 6 && startDay != 0) {
            days = days - 1;
        }


        return days;
        var calculatedDays = days;
    }

    function test() {
         debugger
        var startdate = document.getElementById('<%=txtstartdate.ClientID  %>').value;        
        var a = new Date(startdate);
        var Enddate = document.getElementById('<%=txtEndDate.ClientID %>').value;
        var b = new Date(Enddate);
        workingDaysBetweenDates(a, b);
        if (startdate != Enddate) {
            $('#<%=ddlDuration.ClientID  %> option[value="1"]').attr("disabled", true);
                $('#<%=ddlDuration.ClientID  %> option[value="2"]').attr("disabled", true);
            }
            if (startdate == Enddate) {
                $('#<%=ddlDuration.ClientID  %> option[value="1"]').attr("disabled", false);
            $('#<%=ddlDuration.ClientID  %> option[value="2"]').attr("disabled", false);
        }
    }
    function checkDate(sender, args) {
        //Check if the date selected is less than todays date
        if (sender._selectedDate < new Date()) {
            //show the alert message
            alert("You cannot select a day earlier than today!");
            //set the selected date to todays date in calendar extender control
            sender._selectedDate = new Date();
            // set the date back to the current date
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            var startdate = document.getElementById('<%=txtstartdate.ClientID  %>').value;
            var a = new Date(startdate);
            var Enddate = document.getElementById('<%=txtEndDate.ClientID %>').value;
                var b = new Date(Enddate);
                if (a != "" && b != "") {
                    test();
                }
            }
            else {
                test();
            }
        }
//        function lopvalidate() {
//        debugger
//            var leavehrs = document.getElementById('<%=lopleavehrs.ClientID  %>').value;
//            var timesheethrs = document.getElementById('<%=loptimshthrs.ClientID  %>').value;
//            if (leavehrs <= timesheethrs) {
//              


//            } else {
//                alert("TimeSheet Hours should Greater then 4");
//                document.getElementById('<%=loptimshthrs.ClientID  %>').focus();
//            }
//        }
       <%-- function noofdays() {
            var noofdays = document.getElementById('<%=hdnlblnoofdays.ClientID  %>').value;

            
        }--%>


        function durationchange() {
            var startdate = document.getElementById('<%=txtstartdate.ClientID  %>').value;
            var Enddate = document.getElementById('<%=txtEndDate.ClientID  %>').value;
            if ((document.getElementById("<%=ddlDuration.ClientID  %>").selectedIndex != '0') && (startdate == Enddate)) {
                document.getElementById('<%=txtNoofdays.ClientID  %>').value = "0.5";
            }
            else {
                if ((startdate != "") && (Enddate != "") && (startdate == Enddate)) {
                    test();
                }
            }
        }

//        Added by SOPHIA
        function dateshideandshowing(obj) {
        //debugger
        var value = document.getElementById("<%=ddlexistingcompensation.ClientID%>");  
        var getvalue = value.options[value.selectedIndex].value;  
        //var gettext = value.options[value.selectedIndex].text;  
        //var DdlDates = document.getElementById('DddDateDiv')
       // var DdlDates1 = document.getElementById('DddDateDiv_')
        DddDateDiv.style.display = "none";
        DddDateDiv_.style.display = "none";
        var html='<button type="button" class="multiselect-option dropdown-item" title="2022-03-19 - Saturday" style="width:100%"><span class="form-check">No dates available</span></button>';
       if(getvalue == 1){
       if($('#Dddselectdatesdiv .multiselect-container button').length == 0){
        $('#Dddselectdatesdiv .multiselect-container.dropdown-menu').html(html);
        }
        $("[id*=DropDownselectdates]").val('').multiselect('refresh');
        DddDateDiv.style.display = "block" 
        }
        else  if(getvalue == 2){
        if($('#Dddselectholidaysdiv .multiselect-container button').length == 0){
        $('#Dddselectholidaysdiv .multiselect-container.dropdown-menu').html(html);
        }
        $("[id*=DropDownselectholidays]").val('').multiselect('refresh');
        DddDateDiv_.style.display = "block" 
        }
//        $('.multiselect-container.dropdown-menu').css('width','120%');
        }

         
          
    </script>
    <style type="text/css">
        .link
        {
            background-color: #f49430;
            color: white;
            padding: 14px 25px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
        }
        
        .modalPopup1
        {
            background-color: #696969;
            filter: alpha(opacity=40);
            opacity: 0.7;
        }
    </style>
    <script type="text/javascript">
        function ClientCheck() {
            var checkedBoxesCount = $("<%=grdLeaveDecision.ClientID%>").find("input:checkbox:checked").length;
            if (checkedBoxesCount == 0);
        }
    </script>
    <style type="text/css">
        .gvItemCenter
        {
            text-align: center;
        }
    </style>
    <%--Leave DEcision Scripts--%>
    <script type="text/javascript" language="javascript">
        var gridViewId = '#<%= grdLeaveDecision.ClientID %>';
        function checkAll(selectAllCheckbox) {
            //get all checkboxes within item rows and select/deselect based on select all checked status
            //:checkbox is jquery selector to get all checkboxes
            $('td :checkbox', gridViewId).prop("checked", selectAllCheckbox.checked);
        }
        function unCheckSelectAll(selectCheckbox) {
            //if any item is unchecked, uncheck header checkbox as well
            if (!selectCheckbox.checked)
                $('th :checkbox', gridViewId).prop("checked", false);
            else if (($("tbody tr").find("input:checkbox:checked").length) == ($("tbody tr").find("input:checkbox").length)) { $('th :checkbox', gridViewId).prop("checked", true); }
        }
    </script>
    <%----------------Modal-----------------%>
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            z-index: 99;
        }
    </style>
    <script type="text/javascript">
        function validateQty(event) {
            var key = window.event ? event.keyCode : event.which;
            if (event.keyCode == 8 || event.keyCode == 45) {
                return true;
            }
            else if (key < 48 || key > 57) {
                return false;
            }
            else return true;
        };
    </script>
    <script type="text/javascript">
        function fnhidemsgbox(obj) {
            $("#Alert_container1").css("display", "none");
            $("#Alert_container2").css("display", "none");
            $("#alert_container").css("display", "none");
            $("#Alert_container3").css("display", "none");
            $("#alert_container5").css("display", "none");
        }

        function Click_close() { $('#ConformModal').modal('hide'); $('#myModal').modal('hide'); $('#MyPopup').modal('hide'); $('#lopPopup').modal('hide').css('display', 'none'); }

    </script>
    <script type="text/javascript">
        //         var leavehrs = document.getElementById('<%=lopleavehrs.ClientID  %>').value;
        //         var timesheethrs = document.getElementById('<%=loptimshthrs.ClientID  %>').value;
        //         var Totalhrs = leavehrs + timesheethrs;
        //         alert(Totalhrs)
<%-- LOP Action Taken  added by SOPHIA --%>
        function EnableDisableButton(obj) {
            //debugger
            var actionTaken = obj.value.length;
            var NoOfDays = document.getElementById('<%=htnNoOfDays.ClientID  %>').value;
            var leavehrs = document.getElementById('<%=lopleavehrs.ClientID  %>').value;
            var timesheethrs = document.getElementById('<%=loptimshthrs.ClientID  %>').value;
            var Totalhrs = parseInt(leavehrs) + parseInt(timesheethrs);
            //debugger;
            if (actionTaken > 0) {

                if (NoOfDays == "1.0") {
                    if (Totalhrs >= 8)
                        document.getElementById('<%= Button2.ClientID %>').disabled = false;
                    else
                        document.getElementById('<%= Button2.ClientID %>').disabled = true;
                }

                if (NoOfDays == "0.5") {
                    if (Totalhrs >= 4)
                        document.getElementById('<%= Button2.ClientID %>').disabled = false;
                    else
                        document.getElementById('<%= Button2.ClientID %>').disabled = true;

                }
                  } else
                document.getElementById('<%= Button2.ClientID %>').disabled = true;

            //            if (leavehrs >= 4 || leavehrs < 8) {
            //                if (sender.value.length > 0 && Totalhrs >= 4)
            //                    document.getElementById('<%= Button2.ClientID %>').disabled = false;

            //                else
            //                    document.getElementById('<%= Button2.ClientID %>').disabled = true;
            //            }
            //            if (leavehrs == 8) {
            //                if (sender.value.length > 0 && Totalhrs >= 8)
            //                    document.getElementById('<%= Button2.ClientID %>').disabled = false;

            //                else
            //                    document.getElementById('<%= Button2.ClientID %>').disabled = true;
            //            }
        }
       
       

//});

        
        
    </script>
    <script type="text/javascript">
        //$("#DropDownList1 option[value='0']").hide();
        // $("#lopchkselect").prop('checked', true);
        //$("#lopchkselect").prop('checked', false);

        //         $("#lopchkselect").click(function () {
        //             $("input[type=checkbox]").prop('checked', $(this).prop('checked'));

        //         });
        //        ADDED BY SOPHIA
    </script>
    <style type="text/css">
        .multiselect-container .multiselect-option .form-check, .multiselect-container .multiselect-group .form-check, .multiselect-container .multiselect-all .form-check
        {
            display: inline-flex !important;
            flex-direction: row !important;
        }
        .multiselect-container .multiselect-option .form-check-label, .multiselect-container .multiselect-group .form-check-label, .multiselect-container .multiselect-all .form-check-label
        {
            min-width: max-content !important;
        }
        .multiselect-container .multiselect-option.dropdown-item, .multiselect-container .multiselect-group.dropdown-item, .multiselect-container .multiselect-all.dropdown-item, .multiselect-container .multiselect-option.dropdown-toggle, .multiselect-container .multiselect-group.dropdown-toggle, .multiselect-container .multiselect-all.dropdown-toggle
        {
            width: 100% !important;
        }
        input[type=radio]
        {
            margin: -8px 0 0 !important;
        }
        .form-check-input
        {
            margin: -8px 0 0 !important;
        }
        .dropdown-item
        {
            width: max-content;
            display: inline-block;
            background-color: White !important;
            color: #555555;
            margin-bottom: -2px;
            border: none;
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
            padding: 8px;
        }
    </style>
    <%-- Till here--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div class="container-fluid">
        <ul id="myTab" class="nav nav-tabs">
            <li id="lnkhistory" class="active" runat="server"><a data-toggle="tab" style="color: #428bca"
                href="#LeaveHistory" id="History" runat="server" onclick="fnhidemsgbox(this);">Leave
                History </a></li>
            <li id="lnkbalance"><a data-toggle="tab" href="#LeaveBalance" style="color: #428bca"
                id="Balance" runat="server" onclick="fnhidemsgbox(this);">Leave Balance</a>
            </li>
            <li><a data-toggle="tab" href="#LeaveDecision" id="approvals" style="color: #428bca;"
                onclick="fnhidemsgbox(this);" runat="server">Approvals</a> </li>
            <li><a data-toggle="tab" href="#Leavecalendar" id="datacal" style="color: #428bca"
                runat="server">Data calendar</a></li>
            <li id="lnkempleavehistory"><a data-toggle="tab" style="color: #428bca" href="#EmpLeaveHistory"
                id="EmpHistory" onclick="fnhidemsgbox(this);" runat="server">Employee LeaveHistory</a></li>
            <%-- LOP Removal Request added by LOGESH --%>
            <li id="lnkloprevalrequest"><a data-toggle="tab" style="color: #428bca" href="#Emplopcancle"
                id="LOPRemovalRequest" onclick="fnhidemsgbox(this);" runat="server">LOP Removal
                Request</a></li>
        </ul>
    </div>
    <div class="tab-content">
        <div id="LeaveHistory" class="tab-pane fade in active">
            <div class="messagealert" id="alert_container">
            </div>
            <asp:GridView ID="grdLeaveHistory" DataKeyNames="EmpLeaveApplicationId" runat="server"
                CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                UseAccessibleHeader="true" OnRowCommand="grdLeaveHistory_RowCommand" AutoGenerateColumns="false"
                OnPreRender="grdLeaveHistory_PreRender" CellSpacing="0" Width="100%" OnRowDataBound="grdLeaveHistory_RowDataBound"
                CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grdLeaveHistory_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Leave Type">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("LeaveType") %>'></asp:Label>
                            <asp:HiddenField ID="hdfEmpLeaveApplicationID" runat="server" Value='<%# Bind("EmpleaveApplicationID") %>' />
                            <asp:HiddenField ID="hdfLeaveComments" runat="server" Value='<%# Bind("Comments") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applied On">
                        <ItemTemplate runat="server">
                            <%--  <asp:HiddenField ID="AppliedOn" runat="server" Value='<%# Bind("AppliedOn_CDate") %>' />--%>
                            <asp:Label ID="lblAppliedOn" runat="server" Text='<%# Bind("AppliedOn") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblStartDate" runat="server" Text=' <%# Bind("StartDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. of Days" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate runat="server">
                            <%-- <asp:HiddenField runat="server" ID="hdnlblNo_of_Days" Value='<%# Bind("No_of_Days") %>' />--%>
                            <asp:HiddenField runat="server" ID="testId" Value='<%# Bind("No_of_Days") %>' />
                            <asp:Label ID="lblNo_of_Days" runat="server" Text='<%# Bind("No_of_Days") %>'></asp:Label>
                            <itemstyle cssclass="gvItemCenter" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason for leave">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblReason" runat="server" data-toggle="tooltip" data-placement="top"
                                ToolTip='<%# Bind("Reason") %>' Text='<%# GetTruncatedString(Eval("Reason").ToString()) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--LEAVE OPTIONS added by SOPHIA--%>
                    <asp:TemplateField HeaderText="Leave Options">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblExisting_Compensation" runat="server" Text='<%# Bind("Leaveoptions") %>'> </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkCancel" data-toggle="tooltip" data-placement="top" ToolTip="Click to Cancel"
                                runat="server" ForeColor="#0066CC" Font-Size="Medium" Text="Cancel" CommandArgument='<%# Bind("EmpLeaveApplicationId") %>'
                                CommandName="LnkCancel" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"><span class="glyphicon glyphicon-trash">Cancel</span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--LOP added by LOGESH--%>
                    <asp:TemplateField HeaderText="LOP">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnklopCancel" data-toggle="tooltip" data-placement="top" ToolTip="Click to Cancel"
                                runat="server" ForeColor="#0066CC" Font-Size="Medium" Text="Cancel" CommandArgument='<%# Bind("EmpLeaveApplicationId") %>'
                                CommandName="LnklopCancel" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"
                                OnClick="LnklopCancel_Click"><span class="glyphicon glyphicon-trash">Cancel</span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
            </asp:GridView>
            <div id="LabelHistory" class="bg-danger row text-center" runat="server" visible="false">
                <asp:Label ID="Label1" runat="server" CssClass="label" Width="350px" Height="25px"
                    ForeColor="Black" Font-Size="Small">No History Records Found!</asp:Label>
            </div>
            <%-- Model for LOP cancel --%>
            <div id="lopPopup" class="mode fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="model-header modal-header-primary">
                            <h4 class="modal-title">
                                <%--LOP Label changing added by SOPHIA--%>
                                <b>Leave Cancel: LOP</b>
                                <%--<asp:Label ID="loplevtype" runat="server" Font-Bold="true"></asp:Label>--%>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <form action="" class="form-horizontal">
                            <div class="form-group">
                                <h4 class="modal-title" style="color: #999">
                                    <b>Reporting Manager: </b>
                                    <asp:Label ID="loprptmgr" runat="server" Font-Bold="true"></asp:Label></h4>
                            </div>
                            <br />
                            <div class="form-group">
                                <label class="col-xs-3 control-label">
                                    Start Date</label>
                                <div class="col-xs-5 input-group">
                                    <asp:TextBox ID="lopstartdate" placeholder="Enter your Start date" data-toggle="tooltip"
                                        data-placement="top" ToolTip="Start Date" onkeypress='return validateQty(event);'
                                        CssClass="text-primary form-control" onChange="test()" runat="server" MaxLength="10"
                                        required="" /><span class="input-group-btn">
                                            <asp:ImageButton ID="lopimgbtnCalendarStartDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                                Style="border: 0; cursor: not-allowed" Height="27px" Width="25px" />
                                            <ajaxToolkit:CalendarExtender ID="LopCalendarExtender" runat="server" CssClass="cal_Theme1"
                                                TargetControlID="lopstartdate" PopupButtonID="lopimgbtnCalendarStartDate" Format="yyyy/MM/dd">
                                            </ajaxToolkit:CalendarExtender>
                                            <%--<ajaxToolkit:CalendarExtender ID="LopCalendarExtender" runat="server"
                                                    TargetControlID="txtAddCountry" PopupButtonID="txtAddCountry" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>--%>
                                        </span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-xs-3 control-label">
                                    End Date</label>
                                <div class="col-xs-5 input-group">
                                    <asp:TextBox ID="lopenddate" placeholder="Enter your End Date" onkeypress='return validateQty(event);'
                                        runat="server" data-toggle="tooltip" data-placement="top" ToolTip="End Date"
                                        CssClass="text-primary form-control" onChange="test()" required="" MaxLength="10" /><span
                                            class="input-group-btn">
                                            <asp:ImageButton ID="lopimgbtnCalendarEndDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                                Style="border: 0; cursor: not-allowed" Height="27px" Width="25px" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="cal_Theme1"
                                                TargetControlID="lopenddate" PopupButtonID="lopimgbtnCalendarEndDate" Format="yyyy/MM/dd">
                                            </ajaxToolkit:CalendarExtender>
                                        </span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-xs-3 control-label">
                                    Action Taken</label>
                                <%--<label class="validation-error hide" id="fullNameValidationError">This field is required.</label>--%>
                                <div class="col-xs-5 input-group">
                                    <asp:TextBox ID="lopactiontaken" Width="240px" Height="72px" TextMode="MultiLine"
                                        CssClass="form-control" placeholder="Enter the Action Took Detail" runat="server"
                                        data-toggle="tooltip" data-placement="bottom" ToolTip="Reason" onkeyup="EnableDisableButton(this)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group" runat="server" id="divloptimshthrs">
                                <label class="col-xs-3 control-label">
                                    TimeSheet Hours</label>
                                <div class="col-xs-5 input-group">
                                    <asp:TextBox ID="loptimshthrs" CssClass="form-control" placeholder="Enter the TimeSheet Hours"
                                        MaxLength="3" runat="server" onChange="lopvalidate()" data-toggle="tooltip" data-placement="bottom"
                                        ToolTip="TimeSheet Hours" required="">
                                    </asp:TextBox><asp:Label ID="loptimeshthrswrng" Font-Size="11px" runat="server" Visible="false">Timesheet hours should be greater then 4 hours</asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-xs-3 control-label">
                                    Leave Hours</label>
                                <div class="col-xs-5 input-group">
                                    <asp:TextBox ID="lopleavehrs" CssClass="form-control" placeholder="Enter the Leave Hours"
                                        runat="server" data-toggle="tooltip" data-placement="bottom" ToolTip="Leave Hours"
                                        required=""></asp:TextBox>
                                </div>
                            </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" onclick="mymodalclose()">
                                Cancel</button>
                            <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Submit" Enabled="false"
                                OnClick="lopsubmit" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="LeaveBalance" class="tab-pane fade">
            <asp:UpdatePanel ID="UpnlLeaveBalance" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="messagealert1" id="Alert_container1">
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="grdLeaveBalance" runat="server" CssClass="table table-striped table-bordered table-hover display nowrap"
                            CellSpacing="0" Width="100%" AutoGenerateColumns="False" CellPadding="4" GridLines="None"
                            DataKeyNames="EmpLeaveTransactionID" OnRowCommand="grdLeaveBalance_RowCommand"
                            OnRowDataBound="grdLeaveBalance_Databound" ForeColor="#333333" ShowFooter="true">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Leave Type" SortExpression="Leave Type">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblLeaveTYpe" runat="server" Text='<%# Bind("Leavetype") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblTotalSummary" runat="server" Text="Total" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance" SortExpression="Opening Balance"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblBeginingBalance" runat="server" Text='<%# Bind("OpeningBalance") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfBeginingBalance" runat="server" Value='<%# Bind("OpeningBalance") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblTotalBeginningbalance" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Earned Leave" SortExpression="Earned Leave" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblEarnedLeave" runat="server" Text='<%# Bind("EarnedLeave") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfEarnedLeave" runat="server" Value='<%# Bind("EarnedLeave") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblTotalEarnedLeave" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Availed/Approved" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="Availed">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblAvailed" runat="server" Text='<%# Bind("LeavesTaken") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfAvailed" runat="server" Value='<%# Bind("LeavesTaken") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblTotalAvailed" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Balance" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="Current Balance">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblCurrentBalance" runat="server" Text='<%# Bind("Currentblc") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfCurrentBalance" runat="server" Value='<%# Bind("Currentblc") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lbltotalCurrentbalance" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LOP" ItemStyle-HorizontalAlign="Right" SortExpression="Lop">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblLOP" runat="server" Text='<%# Bind("LOP") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfLop" runat="server" Value='<%# Bind("LOP") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lbltotalLOP" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" SortExpression="Action" ItemStyle-CssClass="">
                                    <ItemTemplate runat="server">
                                        <asp:LinkButton ID="lnkApply" runat="server" ForeColor="#0066CC" data-toggle="tooltip"
                                            data-placement="top" ToolTip="Click to apply Leave" Text="Apply" CommandArgument='<%# Bind("LeaveID") %>'
                                            Font-Size="Medium" CommandName="lnkApply" onmouseover="this.style.color='#f49430'"
                                            onmouseout="this.style.color='#0066CC'"><span class="glyphicon glyphicon-send">Apply</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                            <HeaderStyle BackColor="#428bca" />
                        </asp:GridView>
                    </div>
                    <asp:HiddenField ID="hdfCurrent" runat="server" Value='' />
                    <div id="MyPopup" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="model-header modal-header-primary">
                                    <h4 class="modal-title">
                                        <b>Leave Application:</b>
                                        <asp:Label ID="lblLeaveNamePopup" runat="server" Font-Bold="true"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <form action="" role="form" class="form-horizontal">
                                    <div class="form-group">
                                        <h4 class="modal-title" style="color: #999">
                                            <b>Reporting Manager: </b>
                                            <asp:Label ID="lblRptmgr" runat="server" Font-Bold="true"></asp:Label></h4>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label class="col-xs-3 control-label">
                                            Start Date:</label>
                                        <div class="col-xs-5 input-group">
                                            <asp:TextBox ID="txtstartdate" placeholder="Enter your Start date" data-toggle="tooltip"
                                                data-placement="top" ToolTip="Start Date" onkeypress='return validateQty(event);'
                                                CssClass="text-primary form-control" onChange="test()" runat="server" MaxLength="10"
                                                required="" /><span class="input-group-btn">
                                                    <asp:ImageButton ID="imgbtnCalendarStartDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                                        Style="border: 0;" Height="27px" Width="25px" />
                                                    <ajaxToolkit:CalendarExtender ID="clndtxtStartDate" runat="server" CssClass="cal_Theme1"
                                                        TargetControlID="txtstartdate" PopupButtonID="imgbtnCalendarStartDate" Format="MM/dd/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-3 control-label">
                                            End Date:</label>
                                        <div class="col-xs-5 input-group">
                                            <asp:TextBox ID="txtEndDate" placeholder="Enter your End Date" onkeypress='return validateQty(event);'
                                                runat="server" data-toggle="tooltip" data-placement="top" ToolTip="End Date"
                                                CssClass="text-primary form-control" onChange="test()" required="" MaxLength="10" /><span
                                                    class="input-group-btn">
                                                    <asp:ImageButton ID="imgbtnCalendarEndDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                                        Style="border: 0;" Height="27px" Width="25px" />
                                                    <ajaxToolkit:CalendarExtender ID="clndtxtEndDate" runat="server" CssClass="cal_Theme1"
                                                        TargetControlID="txtEndDate" PopupButtonID="imgbtnCalendarEndDate" Format="MM/dd/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-3 control-label">
                                            Duration:</label>
                                        <div class="col-xs-5 input-group">
                                            <asp:DropDownList ID="ddlDuration" data-toggle="tooltip" data-placement="right" ToolTip="Duration"
                                                CssClass="form-control" onChange="durationchange()" runat="server" required="">
                                                <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="1st Half" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="2nd Half" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:HiddenField runat="server" ID="hdnlist" Value="0" />
                                    <div class="form-group">
                                        <label class="col-xs-3 control-label">
                                            No.Of Days:</label>
                                        <div class="col-xs-5 input-group">
                                            <asp:TextBox ID="txtNoofdays" TextMode="SingleLine" CssClass="form-control" placeholder="Enter the total No. Of Days"
                                                MaxLength="3" runat="server" onChange="test()" data-toggle="tooltip" data-placement="bottom"
                                                ToolTip="No. of Days" required=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-xs-3 control-label">
                                            Reason:</label>
                                        <div class="col-xs-5 input-group">
                                            <asp:TextBox ID="txtReason" CssClass="form-control" placeholder="Enter the Reason"
                                                runat="server" Width="240px" Height="72px" TextMode="MultiLine" data-toggle="tooltip"
                                                data-placement="bottom" ToolTip="Reason" required=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <%--added by sophia--%>
                                    <div class="form-group">
                                        <label class="col-xs-3 control-label">
                                            Leave Options:</label>
                                        <div class="col-xs-5 input-group">
                                            <asp:DropDownList ID="ddlexistingcompensation" data-toggle="tooltip" data-placement="right"
                                                onChange="dateshideandshowing(this);leaveoptionsvalidation()" ToolTip="Leave Options"
                                                CssClass="form-control" runat="server" required="">
                                                <%-- <asp:ListItem>-Select options-</asp:ListItem>--%>
                                                <asp:ListItem Text="Deduct from my leave balance" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Compensate leave in upcoming week" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Use Previously Compensated working day" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DddDateDiv" style="display: none">
                                        <label runat="server" class="col-xs-3 control-label">
                                            Dates:
                                        </label>
                                        <div class="col-xs-5 input-group" id="Dddselectdatesdiv" onclick="dropdownhiding()">
                                            <asp:ListBox ID="DropDownselectdates" onChange="selectdatesvalidation();getdatesFocus('#Dddselectdatesdiv')"
                                                runat="server"></asp:ListBox>
                                            <%--SelectionMode="Multiple"--%>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DddDateDiv_" style="display: none">
                                        <label id="Label2" runat="server" class="col-xs-3 control-label">
                                            Dates:
                                        </label>
                                        <div class="col-xs-5 input-group" id="Dddselectholidaysdiv" onclick="dropdownhide()">
                                            <asp:ListBox ID="DropDownselectholidays" onChange="selectholidaysvalidation();getdatesFocus('#Dddselectholidaysdiv');"
                                                runat="server"></asp:ListBox>
                                            <%--SelectionMode="Multiple"--%>
                                        </div>
                                    </div>
                                    <%--till added by sophia--%>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" onclick="mymodalclose();resetAllControls()">
                                        <%--added one fuction name by sophia--%>
                                        Cancel</button>
                                    <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Submit" OnClick="btnsubmit_Click" />
                                    <%-- ADDED BY  SOF--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdfld" runat="server" />
                    <div id="myModal" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="model-header modal-header-danger">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        Confirmation</h4>
                                </div>
                                <div class="modal-body">
                                    <p>
                                        "You are applying leave more than the available balance!!
                                    </p>
                                    <p class="text-danger">
                                        Do you want to proceed?
                                    </p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                                        Close</button>
                                    <asp:Button runat="server" ID="btnSaveImage" Text="Goahead" class="btn btn-danger"
                                        OnClick="btnChange_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="ConformModal" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="model-header modal-header-danger">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        &times;</button>
                                    <h4 class="modal-title">
                                        Confirmation</h4>
                                </div>
                                <div class="modal-body">
                                    <p>
                                        "You are applying for leave more than available balance. This would result in loss
                                        of pay.
                                    </p>
                                    <p class="text-danger">
                                        Do you want to proceed?"
                                    </p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" id="h12" class="btn btn-danger" onclick="mymodalclose()">
                                        Close</button>
                                    <asp:Button runat="server" ID="Button1" Text="Yes" class="btn btn-danger" OnClick="ok_click"
                                        UseSubmitBehavior="false" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:LinkButton ID="lbtnPermission" runat="server" OnClick="lbtnPermission_Click" ForeColor="#0066CC" data-toggle="tooltip" ToolTip="Click here to Apply"
  Font-Size="Medium" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"> Permission</asp:LinkButton>--%>
            <script type="text/javascript">
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            $(function () {
                                $('[data-toggle=tooltip]').tooltip();
                                $('[rel=tooltip]').tooltip();
                            });
                        }
                    });
                };  </script>
        </div>
        <div id="LeaveDecision" class="tab-pane fade">
            <asp:UpdatePanel ID="UpnlLeaveDecision" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="messagealert" id="Alert_container2">
                    </div>
                    <div id="LabelDecision" class="bg-danger row text-center" runat="server" visible="false">
                        <asp:Label ID="Label3" runat="server" CssClass="label" Width="350px" Height="25px"
                            ForeColor="Black" Font-Size="Small">No Records Found!</asp:Label>
                    </div>
                    <asp:GridView ID="grdLeaveDecision" runat="server" DataKeyNames="EmpLeaveApplicationId"
                        CssClass="table table-striped table-bordered table-hover display gvdatatable nowrap"
                        UseAccessibleHeader="true" CellSpacing="0" Width="100%" OnPreRender="grdLeaveDecision_PreRender"
                        GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" ForeColor="#333333"
                        CellPadding="4">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" onclick="checkAll(this);" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkselect" onclick="unCheckSelectAll(this);" runat="server" />
                                    <asp:HiddenField ID="hfdEmpLeaveApplicationId" runat="server" Value='<%# Bind("EmpLeaveApplicationId") %>' />
                                    <asp:HiddenField ID="hfdEmpid" runat="server" Value='<%# Bind("EmpId") %>' />
                                    <asp:HiddenField ID="hfdYear" runat="server" Value='<%# Bind("YearId") %>' />
                                    <asp:HiddenField ID="hfdLeaveId" runat="server" Value='<%# Bind("LeaveId") %>' />
                                    <asp:HiddenField ID="hfdStatusCode" runat="server" Value='<%# Bind("statuscode") %>' />
                                    <%-- <asp:HiddenField ID="hfdLeaveOptions" runat="server" Value='<%# Bind("Leaveoptions") %>' />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate runat="server">
                                    <asp:HyperLink ID="lblFirstName" runat="server" Text='<%#Bind("FirstName")%>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave Type">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("LeaveType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No. Of Days" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblNo_Of_Days" runat="server" Text='<%# Bind("No_Of_Days") %>'></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnlblnoofdays" Value='<%# Bind("No_Of_Days") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Applied On">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblAppliedOn" runat="server" Text='<%# Bind("Applied") %>' Format="MMM-dd-yyyy"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Current Balance" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblCurrent_Balance" runat="server" Text='<%# Bind("CurrentBalance") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblReason_approvals" runat="server" ToolTip='<%# Bind("Reason") %>'
                                        Text='<%# GetTruncatedString_approvals(Eval("Reason").ToString()) %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--Added by sophia--%>
                            <asp:TemplateField HeaderText="Leave Options">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblExisting_Compensation" runat="server" Text='<%# Bind("Leaveoptions") %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate runat="server">
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("[Status]") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                        <HeaderStyle BackColor="#428bca" />
                    </asp:GridView>
                    <div class="container">
                        <div>
                            <asp:LinkButton ID="btnApprove" UseSubmitBehavior="false" class="btn btn-primary"
                                ToolTip="click to Approve the Selected Leave" runat="server" Text="Approve" OnClick="btnApprove_Click"><span class="glyphicon glyphicon-thumbs-up"></span> Approve</asp:LinkButton>
                            <asp:LinkButton ID="btnReject" UseSubmitBehavior="false" class="btn btn-primary"
                                ToolTip="click to Reject the Selected Leave" runat="server" Text="Reject" OnClick="btnReject_Click"><span class="glyphicon glyphicon-thumbs-down"></span> Reject</asp:LinkButton>
                            <asp:LinkButton ID="btnOnHold" UseSubmitBehavior="false" class="btn btn-primary"
                                ToolTip="click to Hold the Selected Leave" runat="server" Text="Onhold" OnClick="btnOnHold_Click"><span class="glyphicon glyphicon-pause"></span>On-Hold</asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <script type="text/javascript">
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            var totalRows = $("#<%= grdLeaveDecision.ClientID %> tr").length;
            var Totrow = $("#<%= grdLeaveHistory.ClientID %> tr").length;

            if (totalRows >= 1) {

                if (prm != null) {
                    // prm.add_beginRequest(function(sender,e){if(sender._postBackSettings.panelToUpdate.parentNode!=null){ $('.gvdatatable').dataTable({ destroy: true, retrieve: true });}});
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            //  $('#<%= grdLeaveDecision.ClientID%>').prepend($("<thead></thead>").append($("#<%= grdLeaveDecision.ClientID%>").find("tr:first"))).dataTable({ destroy: true, retrieve: true });
                            $('#<%= grdLeaveDecision.ClientID %>').dataTable({ destroy: true, retrieve: true });
                        }
                    });
                };
            }
        </script>
        <div id="Leavecalendar" class="tab-pane fade">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="container">
                        <asp:Calendar ID="Calendar1" OnDayRender="CalendarDRender" class="Showtooltip" runat="server"
                            BorderWidth="1px" NextPrevFormat="FullMonth" BackColor="White" Width="414px"
                            ForeColor="Black" Height="274px" Font-Size="9pt" Font-Names="Verdana" BorderColor="White">
                            <DayHeaderStyle Font-Size="8pt" Font-Bold="True"></DayHeaderStyle>
                            <OtherMonthDayStyle BackColor="white" />
                            <WeekendDayStyle BackColor="White" />
                            <WeekendDayStyle ForeColor="Black" />
                            <SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
                            <TitleStyle Font-Size="12pt" Font-Bold="True" BorderWidth="4px" ForeColor="#333399"
                                BorderColor="Black" BackColor="White"></TitleStyle>
                        </asp:Calendar>
                        <asp:Label ID="MessageBox" runat="server"></asp:Label>
                    </div>
                    <asp:DataGrid ID="DataGrid1" runat="server" Style="z-index: 102; left: 23px; position: absolute;
                        top: 383px; height: 3px;" Font-Size="XX-Small" Font-Names="Verdana">
                    </asp:DataGrid>
                    <ul class="fa-ul">
                        <li><i class="fa-li fa fa-square Redcolorhighlight"></i>Approved</li>
                        <li><i class="fa-li fa fa-square Silvercolorhighlight"></i>Pending</li>
                        <li><i class="fa-li fa fa-square Yellowcolorhighlight"></i>On hold</li>
                        <li><i class="fa-li fa fa-square Overlap"></i>More than one employee</li>
                    </ul>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="EmpLeaveHistory" class="tab-pane fade ">
            <asp:UpdatePanel ID="UpnlEmpLeaveHistory" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="col-lg-4" style="padding-top: 30px;">
                            <asp:Label ID="lblSelectEmpName" Style="color: #2970AD; font-family: Cambria; font-weight: bolder;"
                                runat="server" Text="Select Employee"><span class="glyphicon glyphicon-user"></span> Select Employee </asp:Label>
                            <asp:ListBox ID="ddlSelectEmpName" runat="server" SelectionMode="multiple" Style="margin: -8px 0 0 !important;">
                            </asp:ListBox>
                        </div>
                        <div class="col-lg-4" style="padding-top: 30px;">
                            <asp:Label ID="lbldates" Style="color: #2970AD; font-family: Cambria; font-weight: bolder;"
                                runat="server" Text="Select Date">Date</asp:Label>
                            <asp:DropDownList ID="ddlEmpleaveHistory" data-toggle="tooltip" data-placement="right"
                                onChange="dateshideandshows(this)" ToolTip="Select Dates" CssClass="form-control ddlEmpleaveHistory"
                                runat="server" required="" Width="200px">
                                <%--<asp:ListItem Text="Select" ></asp:ListItem>--%>
                                <asp:ListItem Text="All" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Date" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Date Between" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                            <div class="form-group" id="Dddselectdates" style="display: none">
                                <asp:Label ID="lblselectdates" runat="server">Select Date:</asp:Label>
                                <div id="Dddlblselectdates">
                                    <asp:TextBox ID="txtdate" runat="server" Width="110px" ></asp:TextBox>
                                    <a href="#" onclick="showCalendarControl('<%=txtdate.ClientID%>')">
                                        <img id="imgDOR" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif"
                                            style="border: 0; vertical-align: middle;" /></a>
                                </div>
                            </div>
                            <div class="form-group" id="DddFromtodates" style="display: none">
                                <asp:Label ID="lblFromdate" runat="server"> From:</asp:Label>
                                <div id="DdlselectlblFromdate">
                                    <asp:TextBox ID="txtFromdate" runat="server" Width="110px" ToolTip="dd-mm-yyyy"></asp:TextBox><a
                                        href="#" onclick="showCalendarControl('<%=txtFromdate.ClientID%>')"><img id="img1"
                                            runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                            vertical-align: middle;" /></a>
                                    <asp:Label ID="lblTodate" runat="server">To:</asp:Label>
                                    <asp:TextBox ID="txtTodate" runat="server" Width="110px" ToolTip="dd-mm-yyyy"></asp:TextBox><a
                                        href="#" onclick="showCalendarControl('<%=txtTodate.ClientID%>')"><img id="img2"
                                            runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                            vertical-align: middle;" /></a>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4" style="padding-top: 30px;" align="center">
                            <asp:Label ID="lblleavetype" Style="color: #2970AD; font-family: Cambria; font-weight: bolder;"
                                runat="server" Text="Select Type">Leave Type</asp:Label>
                            <asp:ListBox ID="lstleavetype" runat="server" SelectionMode="multiple">
                                <asp:ListItem Value="1" Text="Sick Leave" />
                                <asp:ListItem Value="2" Text="Privilege Leave" />
                                <asp:ListItem Value="3" Text="Casual Leave " />
                            </asp:ListBox>
                            <%--<asp:DropDownList ID="ddlSelectEmpName" runat="server" AutoPostBack="false">
                            </asp:DropDownList>--%>
                            <asp:Button ID="btnGO" runat="server" UseSubmitBehavior="false" Text="GO" AutoPostBack="false"
                                OnClick="btnGetEmpLeaveHistory_Click" CssClass="btn btn-primary" />
                        </div>
                    </div>
                    <asp:Panel ID="pnlgridview" runat="server">
                        <div class="col-lg-12" style="padding-top: 30px;" align="center">
                            <div class="messagealert" id="Alert_container3">
                            </div>
                            <asp:GridView ID="grdEmpLeaveHistory" DataKeyNames="EmpLeaveApplicationId" runat="server"
                                CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                                UseAccessibleHeader="true" AutoGenerateColumns="false" OnPreRender="grdEmpLeaveHistory_PreRender"
                                CellSpacing="0" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Type">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblLeaveType" runat="server" Text='<%# Bind("LeaveType") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfEmpLeaveApplicationID" runat="server" Value='<%# Bind("Status") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Applied On">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblAppliedOn" runat="server" Text='<%# Bind("AppliedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblStartDate" runat="server" Text=' <%# Bind("StartDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No. of Days" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblNo_of_Days" runat="server" Text='<%# Bind("No_of_Days") %>'></asp:Label>
                                            <itemstyle cssclass="gvItemCenter" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reason for leave">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblReason" runat="server" data-toggle="tooltip" data-placement="top"
                                                ToolTip='<%# Bind("Reason") %>' Text='<%# GetTruncatedString(Eval("Reason").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Options">
                                        <%--ADDED BY SOPHIA--%>
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblleaveoptions" runat="server" data-toggle="tooltip" data-placement="top"
                                                ToolTip='<%# Bind("Leaveoptions") %>' Text='<%# GetTruncatedString(Eval("Leaveoptions").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Day">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblDay" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                                <HeaderStyle BackColor="#428bca" />
                            </asp:GridView>
                            <div id="LabelEmpHistory" class="bg-danger row text-center" runat="server">
                                <asp:Label ID="Label4" runat="server" CssClass="label" Width="350px" Height="25px"
                                    ForeColor="Black" Font-Size="Small">No History Records Found!</asp:Label>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="Emplopcancle" class="tab-pane fade ">
            <div class="col-md-12" style="margin-left: -90px; margin-top: -1px" align="center">
                <div class="messagealert" id="Alert_container5">
                </div>
                <asp:GridView ID="grdEmplopcancle" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                    UseAccessibleHeader="true" AutoGenerateColumns="false" CellSpacing="0" Width="100%"
                    CellPadding="4" ForeColor="#333333" GridLines="None" OnDataBound="grdEmplopcancle_DataBound"
                    OnPreRender="grdEmplopcancle_PreRender">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" onclick="lopcheckAll(this);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="lopchkselect" onclick="lopunCheckSelectAll(this);" runat="server" />
                                <asp:HiddenField ID="lophfdEmpid" runat="server" Value='<%# Bind("EmpId") %>' />
                                <%--<asp:HiddenField ID="lophfdStatusCode" runat="server" Value='<%# Bind("statuscode") %>' />--%>
                                <asp:HiddenField ID="lophdnleaveid" runat="server" Value='<%# Bind("LeaveId") %>' />
                                <%--<asp:HiddenField ID="lophfdYear" runat="server" Value='<%# Bind("YearId") %>' />--%>
                                <asp:HiddenField ID="lophdnleaveappid" runat="server" Value='<%# Bind("EmpLeaveApplicationId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopname" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave Type">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopLeaveType" runat="server" Text='<%# Bind("LeaveType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate runat="server">
                                <%--  <asp:HiddenField ID="AppliedOn" runat="server" Value='<%# Bind("AppliedOn_CDate") %>' />--%>
                                <asp:Label ID="lbllopStartDate" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopEndDate" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No. of Days" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopNo_of_Days" runat="server" Text='<%# Bind("NoofDays") %>'></asp:Label>
                                <itemstyle cssclass="gvItemCenter" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Applied On">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopAppliedOn" runat="server" Text='<%# Bind("AppliedOn") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopReason" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate runat="server">
                                <%--<asp:Label ID="lbllopComment"  runat="server" Text='<%# Bind("Comment") %>'></asp:Label>--%>
                                <asp:TextBox runat="server" ID="lbllopComment" TextMode="MultiLine"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" Visible="false">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Timesheet Hours">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopTimesheetHours" runat="server" data-placement="top" Text='<%# Bind("TimesheetHours") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave Hours">
                            <ItemTemplate runat="server">
                                <asp:Label ID="lbllopLeaveHours" runat="server" data-placement="top" Text='<%# Bind("LeaveHours") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                    <HeaderStyle BackColor="#428bca" />
                </asp:GridView>
                <div class="" runat="server" style="float: left; margin-left: 20px" id="lopbtns">
                    <asp:LinkButton ID="lopApproveBtn" UseSubmitBehavior="false" class="btn btn-primary"
                        ToolTip="click to Approve the Selected LOP" runat="server" Text="Approve" OnClick="lopapprove_Click"><span class="glyphicon glyphicon-thumbs-up"></span> Approve</asp:LinkButton>
                    <asp:LinkButton ID="lopRejectBtn" UseSubmitBehavior="false" class="btn btn-primary"
                        ToolTip="click to Reject the Selected LOP" runat="server" Text="Reject" OnClick="lopRejectBtn_Click"><span class="glyphicon glyphicon-thumbs-down"></span> Reject</asp:LinkButton>
                </div>
            </div>
            <div class="bg-danger row text-center" id="emplopnorecord" runat="server">
                <asp:Label ID="Label5" runat="server" CssClass="label" Width="350px" Height="25px"
                    ForeColor="Black" Font-Size="Small">No History Records Found!</asp:Label>
            </div>
        </div>
            <div class="col-lg-12" style="padding-top: 30px;" align="center">
                            <div class="messagealertsecondgrid" id="Alert_container4">
                            </div>
                            <asp:GridView ID="grdLeaveBalancehistory" DataKeyNames="EmpLeaveApplicationId" runat="server"
                                CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                                UseAccessibleHeader="true" AutoGenerateColumns="false" 
                                CellSpacing="0" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <%--<asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblEmpname" runat="server" Text='<%# Bind("Employee Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Sick Leave">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblSickLeave" runat="server" Text='<%# Bind("SickLeave") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Privilage Leave">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblPrivilageLeave" runat="server" Text='<%# Bind("PrivilageLeave") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Casual Leave">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lblCasualLeave" runat="server" Text='<%# Bind("CasualLeave") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate runat="server">
                                            <asp:Label ID="lbltotal" runat="server" Text=' <%# Bind("Total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                                <HeaderStyle BackColor="#428bca" />
                            </asp:GridView>
                        </div>
        <input type="hidden" runat="server" value="" id="htnNoOfDays" />
        <script type="text/javascript" language="javascript">

            var gridViewId_ = '#<%= grdEmplopcancle.ClientID %>';      //ADDED BY SOPHIA variable name changed//
            function lopcheckAll(selectAllCheckbox) {
                //get all checkboxes within item rows and select/deselect based on select all checked status
                //:checkbox is jquery selector to get all checkboxes
                $('td :checkbox', gridViewId_).prop("checked", selectAllCheckbox.checked);
            }
            function lopunCheckSelectAll(selectCheckbox) {
                //if any item is unchecked, uncheck header checkbox as well
                if (!selectCheckbox.checked)
                    $('th :checkbox', gridViewId_).prop("checked", false);
                else if (($("tbody tr").find("input:checkbox:checked").length) == ($("tbody tr").find("input:checkbox").length)) { $('th :checkbox', gridViewId_).prop("checked", true); }
            }
        </script>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:Image ID="image" ImageUrl="../Images/350.gif" Width="80" Height="80" runat="server" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup1" />
        <script type="text/javascript">
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
            prm.add_beginRequest(BeginRequestHandler);
            // Raised after an asynchronous postback is finished and control has been returned to the browser.
            prm.add_endRequest(EndRequestHandler);
            function BeginRequestHandler(sender, args) {
                //Shows the modal popup - the update progress
                var popup = $find('<%= ModalPopupExtender1.ClientID %>');
                if (popup != null) {
                    popup.show();
                }
            }
            function EndRequestHandler(sender, args) {
                //Hide the modal popup - the update progress
                var popup = $find('<%= ModalPopupExtender1.ClientID %>');
                if (popup != null) {
                    popup.hide();
                }
            }
        </script>
        <script type="text/javascript">
            $(function () {
                $('[data-toggle=tooltip]').tooltip();
                $('[rel=tooltip]').tooltip();
            });
        </script>
    </div>
</asp:Content>
