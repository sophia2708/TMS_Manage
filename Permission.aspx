<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" CodeFile="Permission.aspx.cs"
    Inherits="Includes_WebForm_Permission" AutoEventWireup="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script src="../Scripts/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/bootbox.min.js"></script>


    <%-- <script type="text/javascript">
//        function durationchangeTime() {
//         var select = document.getElementById("SelectTime");
//         if(document.getElementById("ddlPDuration").Value=='5')
//         {
//         select.style.visibility= 'visible';
//         }
</script>--%>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal({ backdrop: "static" });
        }
        function NoofdaysTime() {

            var stTime = $("#<%=ddlFromTime.ClientID %> option:selected").text();
            var edTime = $("#<%=ddlToTime.ClientID %> option:selected").text();
            if (stTime > edTime) {
                bootbox.alert('To Time must be greater than From Time').find('.modal-content').css({ 'font-size': '15', 'font-weight': 'bold' });
                $("#<%=Noofdays.ClientID %>").val(diff)="";
                $("#<%=ddlToTime.ClientID %>").val(0);
            }
            var diff = ((edTime - stTime).toFixed(2));
            diff = diff.replace('.7', '.3');
            // var x = ((diff / 8).toFixed(2));
            //return x;
            if (diff > 2) {
                bootbox.alert('Total Time limits exceeded, only 2 hrs/ day is applicable').find('.modal-content').css({ 'font-size': '15', 'font-weight': 'bold' });
                
                return false;
            }
            
            $("#<%=Noofdays.ClientID %>").val(diff);

        }
        
        function CallSubmit() {
            var hidden = document.getElementById('<%=hdfnsubmithome.ClientID %>');
            var sdate = document.getElementById('<%=textStartDate1.ClientID  %>').value;
            var edate = document.getElementById('<%=textStartDate1.ClientID  %>').value;
            var preason = document.getElementById('<%=TextReason.ClientID %>').value;
            if ((sdate != "") && (edate != "") && (preason != "")) {
                hidden.value = 1;
                
            }
            else {
                hidden.value = 0;
                //alert('Please fill the field');
            }
            return false;
              }

              function submithours() {
//                  if ($("#<%=hdntestcount.ClientID %>").val() > 3) {
//                      bootbox.alert('test').find('.modal-content').css({ 'font-size': '15', 'font-weight': 'bold' });
//                      return false;
//                  }
                  var hdn = document.getElementById('<%=hdnworkhours.ClientID  %>');
                  var s_date = document.getElementById('<%=txtStartdate.ClientID  %>').value;
                  var days = document.getElementById('<%=Noofdays.ClientID %>').value;
                  var reason = document.getElementById('<%=txtPReason.ClientID  %>').value;
                  if ((s_date != "")&& (days !="") && (reason != "")) {
                      hdn.value = 1;
                  }
                  else {
                      hdn.value = 0;
                  }
                  return false;
              }

              function message()
              {
              bootbox.alert('Please fill out the Field').find('.modal-content').css({'font-size':'15', 'font-weight':'bold'});
          }
          //  function Call() {
          //  bootbox.alert('Please fill the Comments').find('.modal-content').css({ 'font-size': '15', 'font-weight': 'bold' });
          //      return false;
          //         }

           </script>
    
    <script type="text/javascript">
        function BetweenDates(strtdate, eddate) {
            var startDate = strtdate;
            var endDate = eddate;
            if (endDate > startDate) {
            }
            else { }
            var millisecondsPerDay = 86400 * 1000;
            startDate.setHours(0, 0, 0, 1);

            endDate.setHours(23, 59, 59, 999);

            var diff = endDate - startDate;

            var days = Math.ceil(diff / millisecondsPerDay);

            var weeks = Math.floor(days / 7);


            var startDay = startDate.getDay();
            var endDay = endDate.getDay();

            if (startDay - endDay > 1)
                days = days - 2;
            var calculatedDays = days;
            document.getElementById('<%=TextNoDays.ClientID  %>').value = calculatedDays;

            if (calculatedDays > 1) {

                document.getElementById("<%=ddlHomeDuration.ClientID  %>").selectedIndex = '0';
            }
            else if (calculatedDays <= 1) {

                document.getElementById("<%=ddlHomeDuration.ClientID  %>").selectedIndex = '0';
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
            var startdate = document.getElementById('<%=textStartDate1.ClientID  %>').value;
            var a = new Date(startdate);
            var Enddate = document.getElementById('<%=textStartDate1.ClientID %>').value;
            var b = new Date(Enddate);
            if (a > b) {
               bootbox.alert('EndDate must be greater than StartDate').find('.modal-content').css({'font-size':'15', 'font-weight':'bold'});
               document.getElementById('<%=textStartDate1.ClientID %>').value = startdate;

            }
            BetweenDates(a, b);
            if (startdate != Enddate) {
                $('#<%=ddlHomeDuration.ClientID  %> option[value="1"]').attr("disabled", true);
                $('#<%=ddlHomeDuration.ClientID  %> option[value="2"]').attr("disabled", true);
            }
            if (startdate == Enddate) {
                $('#<%=ddlHomeDuration.ClientID  %> option[value="1"]').attr("disabled", false);
                $('#<%=ddlHomeDuration.ClientID  %> option[value="2"]').attr("disabled", false);
            }
        }
        function checkDate(sender, args) {

            if (sender._selectedDate < new Date()) {

                alert("You cannot select a day earlier than today!");

                sender._selectedDate = new Date();

                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
                var startdate = document.getElementById('<%=textStartDate1.ClientID  %>').value;
                var a = new Date(startdate);
                var Enddate = document.getElementById('<%=textStartDate1.ClientID %>').value;
                var b = new Date(Enddate);
                if (a != "" && b != "") {
                    test();
                }
            }
            else {
                test();
            }
        }
        function durationchanges() {
            var startdate = document.getElementById('<%=textStartDate1.ClientID  %>').value;
            var Enddate = document.getElementById('<%=textStartDate1.ClientID  %>').value;
            if ((document.getElementById("<%=ddlHomeDuration.ClientID  %>").selectedIndex != '0') && (startdate == Enddate)) {
                document.getElementById('<%=TextNoDays.ClientID  %>').value = "0.5";
            }
            else {
                if ((startdate != "") && (Enddate != "") && (startdate == Enddate)) {
                    test();
                }
            }
        }

        
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Given the Variables for the purpose of getting the Values of the Table so that the parentnode=null Metadata=null err wont come
            var totalRows = $("#<%= grdPermissionDecision.ClientID %> tr").length;
            var Totrow = $("#<%= grdPermissionHistory.ClientID %> tr").length;

            if (totalRows >= 1) { $('#<%= grdPermissionDecision.ClientID %>').dataTable({}); }
            if (Totrow >= 1) { $('#<%= grdPermissionHistory.ClientID %>').dataTable({ "order": [[2, "desc"]] }); }
        });
    </script>
    <%--   function durationchangeTime() {
            var select = document.getElementById("SelectTime");
            if (onChange.Value == '4'){
                select.style.visibility = 'visible';
        }
    </script>--%>
    <%--<script>

      function SpecifyTime() {
          var STime = document.getElementById('ddlPDuration');
          if (STime.style.visibility === 'hidden') {
              STime.style.visibility = 'visible';
          } else {
              STime.style.visibility = 'hidden';
          }
      }--%>
    <%-- <script type="text/javascript">
          function durationchangeTime() {
              var STime = document.getElementById("SelectTime");
              if (Selected.Value == '5')
              { STime.style.visibility = 'visible'; }
              else
              { STime.style.visibility = 'hidden'; }
          }

         </script>--%>
   
    <%--<script type = "text/javascript">
       window.onload function () {
           var ddlPDuration = document.getElementById("<%=ddlPDuration.ClientID %>");
           ddlPDuration.onchange = function () {
               var selectedValue = ddlPDuration.value;
               alert(selectedValue);

    --%>
    <script type="text/javascript" language="javascript">
         function Check() {
            var checkedBoxesCount = $("<%=grdPermissionDecision.ClientID%>").find("input:checkbox:checked").length;
            if (checkedBoxesCount == 0);
        }


        var gridViewId = '#<%= grdPermissionDecision.ClientID %>';
        function checkAll(selectAllCheckbox) {

            $('td :checkbox', gridViewId).prop("checked", selectAllCheckbox.checked);
        }
        function unCheckSelectAll(selectCheckbox) {

            if (!selectCheckbox.checked)
                $('th :checkbox', gridViewId).prop("checked", false);
            else if (($("tbody tr").find("input:checkbox:checked").length) == ($("tbody tr").find("input:checkbox").length)) { $('th :checkbox', gridViewId).prop("checked", true); }
        }
    </script>
    <script type="text/javascript">

        function openModalpopup() {
            var Today = new Date();
            var dd = Today.getDate();
            var mm = Today.getMonth() + 1;
            var yyyy = Today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            Today = mm + '-' + dd + '-' + yyyy;
            document.getElementById('<%=txtStartdate.ClientID  %>').value = Today;
           
          //  test1();
            $('#MyPopup').modal({ backdrop: 'static' });
            return false;
        }
        function openMyModalPopupWindow() {
            var Today = new Date();
            var dd = Today.getDate();
            var mm = Today.getMonth() + 1;
            var yyyy = Today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            Today = mm + '/' + dd + '/' + yyyy;
            document.getElementById('<%=textStartDate1.ClientID  %>').value = Today;
            test();
            $('#MyModalPopup').modal({ backdrop: "static" });
            return false;
        }
        function mymodalclose() {
            //$('#txtStartdate').val('');
            document.getElementById('<%=txtStartdate.ClientID  %>').value = "";
           
            document.getElementById('<%=txtPReason.ClientID  %>').value = "";
            document.getElementById('<%=Noofdays.ClientID  %>').value = "";
            document.getElementById('<%=textStartDate1.ClientID  %>').value = "";
            document.getElementById('<%=TextNoDays.ClientID  %>').value = "";
            document.getElementById('<%=TextReason.ClientID  %>').value = "";
            document.getElementById('<%=ddlToTime.ClientID %>').value = 0;
            document.getElementById('<%=ddlFromTime.ClientID %>').value = 0;
           

            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#MyPopup').modal('hide');
            $('#MyPopup').fadeOut();

            $('#MyModalPopup').modal('hide');
            $('#MyModalPopup').fadeOut();
            return false;
        }
    </script>
   
    <style type="text/css">
    .bootbox.modal
          { 
            position: absolute;
            z-index: 10001 !important;
          }


        #MyPopup
        {
            position: absolute;
            z-index: 10000 !important;
        }
        #MyModalPopup
        {
            position: absolute;
            z-index: 10000 !important;
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
        function fnhidemsgbox(obj) {
            $("#Alert_container1").css("display", "none");
            $("#Alert_container2").css("display", "none");
            $("#alert_container").css("display", "none");
        }
        function Click_close() { $('#MyModalPopup').modal('hide'); $('#MyPopup').modal('hide'); }
    </script>
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

        }

    </script>
    <script type="text/javascript">
        $(function () {
            $('[data-toggle=tooltip]').tooltip();
            $('[rel=tooltip]').tooltip();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div class="container-fluid">
        <ul id="myTab" class="nav nav-tabs">
            <li id="lnkP_history" class="active" runat="server"><a data-toggle="tab" href="#PermissionHistory"
                style="color: #428bca" id="P_History" runat="server" onclick="fnhidemsgbox(this);">
                Permission History</a></li>
            <li id="lnkPermission" runat="server"><a data-toggle="tab" href="#ApplyPermission"
                style="color: #428bca" id="Permission" runat="server" onclick="fnhidemsgbox(this); ">
                Apply for Permission</a></li>
            <li id="lnkPDecision" runat="server"><a data-toggle="tab" href="#PermissionDecision"
                style="color: #428bca" id="PDecision" onclick="fnhidemsgbox(this)" runat="server">
                Permission Approvals</a></li>
            <li id="lnkEmpPerHistory" runat="server"><a data-toggle="tab" href="#EmpPerHistory" 
                style="color: #428bca" id="PerEmpHistory" onclick="fnhidemsgbox(this)" runat="server">
                 Employee Permission History</a></li></ul>
    </div>
    <div class="tab-content">
        <div id="PermissionHistory" class="tab-pane fade in active ">
            <div class="messagealert" id="alert_container">
            </div>
            <asp:GridView ID="grdPermissionHistory" runat="server" CssClass="table table-striped table-bordered table-hover nowrap"
                DataKeyNames="PermissionApplicationID" OnRowCommand="grdPermissionHistory_RowCommand"
                UseAccessibleHeader="true" AutoGenerateColumns="false" OnPreRender="grdPermissionHistory_PreRender"
                OnRowDataBound="grdPermissionHistory_RowDataBound" CellSpacing="0" Width="100%"
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Permission Type">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPermissionType" runat="server" Text='<%# Bind("PermissionType") %>'></asp:Label>
                            <asp:HiddenField ID="hdfPermissionApplicationID" runat="server" Value='<%# Bind("Status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applied On">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblAppliedOn" runat="server" Text='<%# Bind("AppliedOn") %>' Format="MMM-dd-yyyy"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start DateTime">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblStartTime" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End DateTime">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblEndTime" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No.of Days">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPNoofdays" runat="server" Text='<%# Bind("No_of_days") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblReason" runat="server" data-toggle="tooltip" data-placement="top"
                                ToolTip='<%# Bind("Reason") %>' Text='<%# Bind("Reason") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkPCancel" data-toggle="tooltip" data-placement="top" ToolTip="Click to Cancel"
                                runat="server" ForeColor="#0066CC" Font-Size="Medium" Text="Cancel" CommandArgument='<%# Bind("PermissionApplicationID") %>'
                                CommandName="LnkPCancel" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'">
                                <span class="glyphicon glyphicon-trash">Cancel</span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
            </asp:GridView>
            <div id="LabelPermissionHistory" class="bg-danger row text-center" runat="server"
                visible="false">
                <asp:Label runat="server" CssClass="label" Width="350px" Height="25px" ForeColor="Black"
                    Font-Size="Small">No History Records Found!!!</asp:Label></div>
        </div>
        <div id="ApplyPermission" class="tab-pane ">
            <asp:UpdatePanel ID="upnlapplypermission" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtnPermissionWorkHours" runat="server" OnClientClick="openModalpopup()"
                        ForeColor="#0066CC" data-toggle="tooltip" ToolTip="Click here to Apply" class="btn btn-default"
                        Font-Size="Medium" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"> Permission for Work Hours
                    </asp:LinkButton><br />
                    <br />
                    <asp:LinkButton ID="lbtnWorkfromHome" runat="server" OnClientClick="openMyModalPopupWindow()"
                        ForeColor="#0066CC" data-toggle="tooltip" ToolTip="Click here to Apply" class="btn btn-default"
                        Font-Size="Medium" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"> Permission for Work from Home</asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
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
        
        <div id="MyPopup" class="modal fade">
       <asp:UpdatePanel ID="UpdWorkhours" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="model-header modal-header-primary">
                        <h4 class="modal-title">
                            <b>Permission Application : Work Hours</b></h4>
                    </div>
                    <div class="modal-body">
                        <form action="" role="form" class="form-horizontal">
                        <div class="form-group">
                            <h4 class="modal-title" style="color: #999">
                                <b>Reporting Manager: </b>
                                <asp:Label ID="lblRptmgrworkhrs" runat="server" Font-Bold="true"></asp:Label></h4>
                        </div>
                        <br />
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                 Date:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="txtStartdate" placeholder="Enter your date" data-toggle="tooltip"
                                    data-placement="top" ToolTip=" Date" OnChange="test1();" onkeypress='return validateQty(event);'
                                    CssClass="text-primary form-control" runat="server" MaxLength="10" /><span class="input-group-btn">
                                        <asp:ImageButton ID="imgbtnCalendarDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                            Style="border: 0;" Height="27px" Width="25px" />
                                        <ajaxToolkit:CalendarExtender ID="clndrtxtDate" runat="server" CssClass="cal_Theme1"
                                            TargetControlID="txtStartdate" PopupButtonID="imgbtnCalendarDate" Format="MM-dd-yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </span>
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <label class="col-xs-3 control-label">
                                End Date:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="txtEnddate" placeholder="Enter your End Date" runat="server" data-toggle="tooltip"
                                    onkeypress='return validateQty(event);' OnChange="test1();" data-placement="top"
                                    ToolTip="End Date" CssClass="text-primary form-control" MaxLength="10" /><span class="input-group-btn">
                                        <asp:ImageButton ID="imgbtnCalndarEndDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                            Style="border: 0;" Height="27px" Width="25px" />
                                        <ajaxToolkit:CalendarExtender ID="clndrtxtEndDate" runat="server" CssClass="cal_Theme1"
                                            Enabled="true" TargetControlID="txtEnddate" PopupButtonID="imgbtnCalndarEndDate"
                                            Format="MM-dd-yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </span>
                            </div>
                        </div>--%>
                     <%-- <div class="form-group">
                            <label class="col-xs-3 control-label">
                                Specify Time:</label>
                            <div class="col-xs-5 input-group">
                               <asp:DropDownList ID="ddlPDuration" data-toggle="tooltip" data-placement="right"
                                    ToolTip="Duration" CssClass="form-control" OnChange="durationchangeTime();" runat="server"
                                    required="">
                                    <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1st Half" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2nd Half" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Specify Time" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                      
                       <%-- <div class="form-group" id="SelectTime" style="display: none;">--%>
                       <div class="form-group">
                    
                      
                            <label class="col-xs-3 control-label">
                                From Time:</label>
                            <div class="col-xs-3 input-group">
                                <asp:DropDownList ID="ddlFromTime" data-toggle="tooltip" data-placement="right" ToolTip=" From Time"
                                    CssClass="form-control" runat="server" required="">
                                    <asp:ListItem Text="00.00" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="09.30" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="10.00" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="10.30" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="11.00" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="11.30" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="12.00" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="12.30" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="13.00" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="13.30" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="14.00" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="14.30" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="15.00" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="15.30" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="16.00" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="16.30" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="17.00" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="17.30" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18.00" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="18.30" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="19.00" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="19.30" Value="21"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label class="col-xs-3 control-label">
                                To Time: </label>
                            <div class="col-xs-3 input-group">
                                <asp:DropDownList ID="ddlToTime" data-toggle="tooltip" data-placement="right" ToolTip=" To Time"
                                    CssClass="form-control" OnChange="NoofdaysTime()" runat="server" required="">
                                    <asp:ListItem Text="00.00" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="10.00" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="10.30" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="11.00" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="11.30" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="12.00" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="12.30" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="13.00" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="13.30" Value="8"></asp:ListItem>
                                    <asp:ListItem Text="14.00" Value="9"></asp:ListItem>
                                    <asp:ListItem Text="14.30" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="15.00" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="15.30" Value="12"></asp:ListItem>
                                    <asp:ListItem Text="16.00" Value="13"></asp:ListItem>
                                    <asp:ListItem Text="16.30" Value="14"></asp:ListItem>
                                    <asp:ListItem Text="17.00" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="17.30" Value="16"></asp:ListItem>
                                    <asp:ListItem Text="18.00" Value="17"></asp:ListItem>
                                    <asp:ListItem Text="18.30" Value="18"></asp:ListItem>
                                    <asp:ListItem Text="19.00" Value="19"></asp:ListItem>
                                    <asp:ListItem Text="19.30" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="20.00" Value="21"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                       
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                No.Of Hours:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="Noofdays" CssClass="form-control" placeholder="Total No. Of Hours"
                                    MaxLength="3" runat="server" onChange="test1()" data-toggle="tooltip" data-placement="bottom"
                                    ToolTip="No. of Hours"></asp:TextBox></div>
                        </div>
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                Reason:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="txtPReason" CssClass="form-control" placeholder="Enter the Reason" width="240px" height="72px" TextMode="MultiLine" 
                                    runat="server" data-toggle="tooltip" data-placement="bottom" ToolTip="Reason"
                                    required=""></asp:TextBox>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" id="hdntestcount" Value="0"/>
                        
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
                                        "You are applying Permission more than 3 times, It will be considered as a Half day with 
                                         LOP!!</p>
                                    <p class="text-danger">
                                        Do you want to proceed?
                                    </p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                                        Close</button>
                                    <asp:Button runat="server" ID="btnOK" Text="Proceed" class="btn btn-danger"
                                        OnClick="btnProceedLOP_Click" UseSubmitBehavior="false" data-dismiss="modal" />
                                </div>
                            </div>
                        </div>
                    </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="button" CssClass="btn btn-default" OnClientClick="mymodalclose()"
                            Text="Cancel" runat="server" />   
                         <asp:HiddenField Id="hdnworkhours" runat="server" Value="0" />
                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" OnClientClick="submithours();" 
                        Text="Submit" UseSubmitBehavior="false" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
             </ContentTemplate>
        </asp:UpdatePanel>
        </div>
       

       
      
        <div id="MyModalPopup" class="modal fade">
         <asp:UpdatePanel ID="UpdWorkfromhome" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="model-header modal-header-primary">
                        <h4 class="modal-title">
                            <b>Permission Application : Work from Home </b>
                        </h4>
                    </div>
                 
                    <div class="modal-body">
                     <%--   <form action=""  id="new" class="form-horizontal">--%>
                        <div class="form-group">
                            <h4 class="modal-title" style="color: #999">
                                <b>Reporting Manager: </b>
                                <asp:Label ID="lblRptMgrHome" runat="server" Font-Bold="true"></asp:Label></h4>
                        </div>
                        <br />
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                 Date:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="textStartDate1" placeholder="Enter your date" data-toggle="tooltip"
                                    data-placement="top" ToolTip="Date" OnChange="test();" onkeypress='return validateQty(event);'
                                    CssClass="text-primary form-control" runat="server" MaxLength="10" required="" />
                                <span class="input-group-btn">
                                    <asp:ImageButton ID="imgCalendarStartDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                        Style="border: 0;" Height="27px" Width="25px" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarStartDate" runat="server" CssClass="cal_Theme1"
                                        TargetControlID="textStartDate1" PopupButtonID="imgCalendarStartDate" Format="MM/dd/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </span>
                            </div>
                        </div>
                       <%-- <div class="form-group">
                            <label class="col-xs-3 control-label">
                                End Date:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="textEndDate1" placeholder="Enter your End Date" onkeypress='return validateQty(event);'
                                    runat="server" data-toggle="tooltip" OnChange="test();" data-placement="top"
                                    ToolTip="End Date" CssClass="text-primary form-control" required="" MaxLength="10" />
                                <span class="input-group-btn">
                                    <asp:ImageButton ID="imgCalendarEndDate" runat="server" ImageUrl="../Images/calendar-06.jpg"
                                        Style="border: 0;" Height="27px" Width="25px" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarEndDate" runat="server" CssClass="cal_Theme1"
                                        TargetControlID="textEndDate1" PopupButtonID="imgCalendarEndDate" Format="MM-dd-yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </span>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                Duration:
                            </label>
                            <div class="col-xs-5 input-group">
                                <asp:DropDownList ID="ddlHomeDuration" data-toggle="tooltip" data-placement="right"
                                    ToolTip="Duration" OnChange="durationchanges()" CssClass="text-primary form-control"
                                    runat="server" required="">
                                    <asp:ListItem Text="Full Day" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="1st Half" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2nd Half" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                No.Of Days:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="TextNoDays" CssClass="text-primary form-control" placeholder="Total No. Of Days"
                                    MaxLength="3" runat="server" onChange="test()" data-toggle="tooltip" data-placement="bottom"
                                    ToolTip="No. of Days"></asp:TextBox></div>
                        </div>
                        <div class="form-group">
                            <label class="col-xs-3 control-label">
                                Reason:</label>
                            <div class="col-xs-5 input-group">
                                <asp:TextBox ID="TextReason" CssClass="text-primary form-control" placeholder="Enter the Reason" width="240px" height="72px" TextMode="MultiLine" 
                                    runat="server" data-toggle="tooltip" data-placement="bottom" ToolTip="Reason"
                                    required=""></asp:TextBox>
                            </div>
                        </div>
                      <%--  </form>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" onclick="mymodalclose()" data-dismiss="modal">
                            Cancel</button>
                         <asp:HiddenField ID="hdfnsubmithome" runat="server" Value="" />
                        <asp:Button ID="btnworkhomesubmit" runat="server" Text="Submit" OnClientClick="CallSubmit();" class="btn btn-primary"
                            OnClick="SubmittedFromHome_Click" UseSubmitBehavior="false"/>
                    </div>
                </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
            <div id="PermissionDecision" class="tab-pane">
            <asp:UpdatePanel ID="UpdatePerDecision" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="messagealert1" id="Alert_container1">
                    </div>
                    <asp:GridView ID="grdPermissionDecision" runat="server" CssClass="table table-striped table-bordered nowrap"
                        OnPreRender="grdPermissionDecision_PreRender" UseAccessibleHeader="true" CellSpacing="0"
                        Width="100%" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False"
                        ForeColor="#333333" CellPadding="4">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="40px">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="checkAll" onclick="checkAll(this);" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkPer_select" onclick="unCheckSelectAll(this);" runat="server" />
                                    <asp:HiddenField ID="hdfPermissionAppID" runat="server" Value='<%#Bind("PermissionApplicationID") %>' />
                                    <asp:HiddenField ID="hdfEmpId" runat="server" Value='<%#Bind("EmpId") %>' />
                                    <asp:HiddenField ID="hdfPerID" runat="server" Value='<%#Bind("PermissionID") %>' />
                                    <asp:HiddenField ID="hdfPStatusCode" runat="server" Value='<%#Bind("Statuscode" )%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="First Name">
                                <ItemTemplate runat="server">
                                    <asp:HyperLink ID="lblFirstName" runat="server" Text='<%#Bind("FirstName")%>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Permission Type">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblPermissionType" runat="server" Text='<%#Bind("PermissionType")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblPermissionStartDate" runat="server" Text='<%#Bind("StartDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblPermissionEndDate" runat="server" Text='<%#Bind("EndDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Applied On">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblPermissionAppliedOn" runat="server" Text='<%#Bind("AppliedOn") %>'
                                        Format="MMM-dd-yyyy"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No.of Days">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblPer_Noofdays" runat="server" Text='<%#Bind("No_of_days") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reason1">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblReason_approvals" runat="server" Text='<%#Bind("Reason") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comments">
                                <ItemTemplate runat="server">
                                    <asp:TextBox ID="txtPComments" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate runat="server">
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                        <HeaderStyle BackColor="#428bca" />
                    </asp:GridView>
                    <div id="LabelPermissionDecision" class="bg-danger row text-center" runat="server"
                        visible="false">
                        <asp:Label runat="server" CssClass="label" Width="350px" Height="25px" ForeColor="Black"
                            Font-Size="Small">No Approvals Found!!!</asp:Label>
                    </div>
                    <div class="container">
                        <div>
                            <asp:LinkButton ID="btnPer_Approve" UseSubmitBehavior="false" class="btn btn-primary"
                                OnClick="Per_ApprovebtnClick" ToolTip="Click to Approve the Selected Permission"
                                runat="server" Text="Approve"><span class="glyphicon glyphicon-thumbs-up"></span> Approve</asp:LinkButton>
                            <asp:LinkButton ID="btnPer_Reject" UseSubmitBehavior="false" class="btn btn-primary" 
                                OnClick="Per_RejectbtnClick" ToolTip="Click to Reject the Selected Permission" 
                                runat="server" Text="Reject"><span class="glyphicon glyphicon-thumbs-down"></span> Reject</asp:LinkButton>
                        </div>
                    </div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="EmpPerHistory" class="tab-pane fade">
        <asp:UpdatePanel ID="UpnlEmpLeaveHistory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
        <div class="container-fluid">
        <div class="col-lg-4" style="padding-top:30px;">
        <asp:Label ID="lblSelectEmpName" Style="color: #2970AD; font-family:Cambria; font-size:inherit; font-style:normal; font-weight:bolder;" runat="server" 
        Text="Select the Employee to view their Permission History"><span class="glyphicon glyphicon-user"></span> Select Employee to view their Permission History </asp:Label>
        </div>
        <div class="col-lg-4" style="padding-top:30px;" align="center">
        <asp:DropDownList ID="ddlSelectEmpName" runat="server" AutoPostBack="false"></asp:DropDownList>
        <asp:Button ID="btnGO" runat="server" Text="GO" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClick="btnGetEmpPerHistory_Click" />
        </div>
        </div>

        <asp:Panel ID="pnlgridview" runat="server">
        <div class="col-lg-12" style="padding-top :30px;" align="center">
        <div class="messagealert" id="Alert_container2"></div>
              <asp:GridView ID="grdEmpPerHistory" DataKeyNames="PermissionApplicationID" runat="server" 
                  CssClass="table table-stripped table-bordered table-hover display gvdatatable dt-responsive nowrap active"
                  UseAccessibleHeader="true" AutoGenerateColumns="false" OnPreRender="grdEmpPerHistory_PreRender"
                  CellSpacing="0" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
                  <AlternatingRowStyle BackColor="White" />
                 <Columns>
                    <asp:TemplateField HeaderText="Permission Type">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPermissionType" runat="server" Text='<%# Bind("PermissionType") %>'></asp:Label>
                            <asp:HiddenField ID="hdfPermissionApplicationID" runat="server" Value='<%# Bind("Status") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Applied On">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblAppliedOn" runat="server" Text='<%# Bind("AppliedOn") %>' Format="MMM-dd-yyyy"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start DateTime">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblStartTime" runat="server" Text='<%# Bind("StartDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="End DateTime">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblEndTime" runat="server" Text='<%# Bind("EndDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No.of Days">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPNoofdays" runat="server" Text='<%# Bind("No_of_days") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason for Permission">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblReason" runat="server" data-toggle="tooltip" data-placement="top"
                                ToolTip='<%# Bind("Reason") %>' Text='<%# Bind("Reason") %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Day">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPDay" runat="server" Text='<%# Bind("Day") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
           </asp:GridView>
          
           <div id="LabelEmpPerHistory" class="bg-danger row text-center" runat="server">
               <asp:Label ID="Label2" runat="server" CssClass="label" Width="350px" Height="25px" ForeColor="Black"
                      Font-Size="Small">No History Records Found!</asp:Label></div>
                
        </div>
         </asp:Panel>

        </ContentTemplate>
        </asp:UpdatePanel>
        </div>



    </div>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        var totalRows = $("#<%= grdPermissionDecision.ClientID %> tr").length;
        var Totrow = $("#<%= grdPermissionHistory.ClientID %> tr").length;

        if (totalRows >= 1) {

            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('#<%= grdPermissionDecision.ClientID %>').dataTable({ destroy: true, retrieve: true });
                    }
                });
            };
        }
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="image" ImageUrl="../Images/350.gif" Width="80" Height="80" runat="server" /></ProgressTemplate>
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
</asp:Content>
