<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="ApprovalTimesheet.aspx.cs" Inherits="Includes_WebForm_ApprovalTimesheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function test(test) {
            //debugger;
            var FromDate = document.getElementById('<%=txtFromdate.ClientID%>').value;
            var ToDate = document.getElementById('<%=txtTodate.ClientID%>').value;
            alert(FromDate + ToDate);

        }
        window.history.forward(1);
    </script>
    <link rel="stylesheet" href="../css/fontawesomemin.css" /> <%--folder saved in file--%>
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
            $('#Alert_container4').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(9000);   //ADDED BY SOPHIA//

        }


    </script>

    <script type='text/javascript'>
//        function scrollToDiv() {
//            document.getElementById('Alert_container4').scrollIntoView();
//        }
//        $(document).ready(function () {
//            var input = $('#Alert_container4');

//            if (input) {
//                input.focus();
//            }
//        });
</script>
    <script type="text/javascript">
        function ClientCheck() {
            var checkedBoxesCount = $("<%=grdApprovalTimesheet.ClientID%>").find("input:checkbox:checked").length;
            if (checkedBoxesCount == 0);
        }
    </script>
    <style type="text/css">
        .gvItemCenter
        {
            text-align: center;
        }
         .body
         {
             height:90vh;
             overflow-y:auto
            }
          body
          {
              height:90vh;
              overflow-y:hidden
          }
    </style>
    <%--Leave DEcision Scripts--%>
    <script type="text/javascript" language="javascript">
        var gridViewId = '#<%= grdApprovalTimesheet.ClientID %>';
        function checkAll(selectAllCheckbox) {
            //get all checkboxes within item rows and select/deselect based on select all checked status
            //:checkbox is jquery selector to get all checkboxes
            $('td :checkbox', gridViewId).not(":disabled").prop("checked", selectAllCheckbox.checked);
        }
        function unCheckSelectAll(selectCheckbox) {
            //if any item is unchecked, uncheck header checkbox as well
            if (!selectCheckbox.checked)
                $('th :checkbox', gridViewId).prop("checked", false);
            else if (($("tbody tr").find("input:checkbox:checked").length) == ($("tbody tr").find("input:checkbox").length)) { $('th :checkbox', gridViewId).prop("checked", true); }
        }
        //ADDED BY SOPHIA//
        function getFocus() {
            debugger;
            //alert('test')
            scrollTop =
              window.pageYOffset || document.documentElement.scrollTop;
            scrollLeft =
              window.pageXOffset || document.documentElement.scrollLeft,
          
            window.onscroll = function () {
                window.scrollTo(scrollLeft, scrollTop + 150);
            };
        }

        //src="../../Includes/WebForm/ViewEditTimesheet.aspx?EmpId=62&sessionid=D5ACF438-1F28-4727-A610-D9FFBEED9420&Username=manikandan.a@analyticbrains.com
    </script>
    <script type="text/javascript" language="javascript">
        //        function SetScrollPosition() {
        //            var div = document.getElementById('btnview1');
        //            div.scrollIntoView(false);
        //(or) 
        //            div.scrollTop = 10000;
        //(both are checking)

        //        function getFocus() {
        //            debugger;
        //            document.getElementById("txtdate").focus();
        //        }

        //        function Button1_onclick() {
        //            document.location = "C:\Users\admin\Desktop\Upgraded Timesheet version 18-01-2018\AnalyticBrainsUI\Includes\WebForm\ViewEditTimesheet.aspx";
        //        }

        //ADDED BY SOPHIA//
        function scrolldown1() {
       
            var len = $(".danger").length;
            var obj = $('.danger')[len - 1];
            $('.body').animate({
                scrollTop: $(obj).offset().top
            }, 500);

        }
        function scrollWin() {
       
            
            var obj = $('#alert_div')[0];
            $('.body').animate({
                scrollTop: $(obj).offset().top
            }, 500);

//            $("#btnview1").on("click", function () {
//                scrolled = scrolled - 300;
//                $(".messagealert").animate({
//                    scrollTop: scrolled
//                });
//            })
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px;" align="center">
        <table border="0" width="750px" align="center" class="table table-responsive wpn_content">
            <tr>
                <td colspan="2" class="Header_style">
                    Approve Time Sheet
                </td>
            </tr>
        </table>
        <%--<tr>
                <td>--%>
        <%--<table align="center" border="0" width="80%" cellpadding="3"
                        cellspacing="2">--%>
        <%-- <tr>
                            <td align="center">
                                
                                <asp:RadioButton ID="rbtnByRange" runat="server" Text="By Range" AutoPostBack="True"
                                    GroupName="gridview" OnCheckedChanged="rbtnByRange_CheckedChanged"></asp:RadioButton>
                                
                            </td>
                        </tr>--%>
        <%--      
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlRange" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Select Range:
                                            </td>
                                            <td>
                                             From:<asp:TextBox ID="txtFromdate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtFromdate.ClientID%>','NoFuture')"><img
                                                        id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                            <td>
                                                To:<asp:TextBox ID="txtTodate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtTodate.ClientID%>','NoFuture')"><img
                                                        id="img2" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>--%>
        <%-- <tr>
                            <td align="center">
                                <asp:Button ID="btnview1" runat="server" Text="View" CssClass="btn btn-primary" ValidationGroup="ViewTimesheet"
                                    OnClick="btnview_Click" />--%>
        <%-- <%--<asp:Button ID="btnWeeklyStatus" runat="server" Text="View" ValidationGroup="ViewTimesheet"
                                    OnClick="btnWeeklyStatus_Click" CssClass="btn btn-primary" Visible="false" />--%>
        <%--  </td>
                        </tr>--%>
        <%--</table>--%>
        <%--  </td>
            </tr>
        --%>
        <br />
    </div>
    <asp:Panel ID="pnlgridview" runat="server">
        <div class="tab-content">
            <div id="ApprovalTimesheet" class="tab-pane fade in active">
                <%--<div class="messagealert" id="alert_container">
                </div>--%>
                <asp:GridView ID="grdApprovalTimesheet" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                    UseAccessibleHeader="true" AutoGenerateColumns="false" CellSpacing="0" Width="50%" OnRowDataBound="grdApprovalTimesheet_RowDataBound" OnSelectedIndexChanged="grdLeaveHistory_SelectedIndexChanged"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" onclick="checkAll(this);" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkselect" onclick="unCheckSelectAll(this);" runat="server" />
                                <asp:HiddenField ID="hdnchkselect" runat="server" Value='<%# bind("empid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server"  Text='<%#Bind("FirstName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("FirstName")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Approved Status" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Width="80px" Text='<%#Bind("Approval_Status")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Width="80px" Text='<%#Bind("Approval_Status")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Start Date" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblsdate" runat="server" Width="80px" Text='<%#Bind("StartDate", "{0:dd-M-yyyy}")%>'></asp:Label>
                                <asp:HiddenField ID="hdnSdate" runat="server" Value='<%# Bind("StartDate") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblsdate" runat="server" Width="80px" Text='<%#Bind("StartDate", "{0:dd-M-yyyy}")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lbledate" runat="server" Width="80px" Text='<%#Bind("EndDate" , "{0:dd-M-yyyy}")%>'></asp:Label>
                                <asp:HiddenField ID="hdnEdate" runat="server" Value='<%# Bind("EndDate") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lbledate" runat="server" Width="80px" Text='<%#Bind("EndDate" , "{0:dd-M-yyyy}")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Hours" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Width="80px" Text='<%#Bind("TotalHours")%>'></asp:Label>
                                <asp:HiddenField ID="hdnHours" runat="server" Value='<%#Bind("TotalHours")%>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Width="80px" Text='<%#Bind("TotalHours")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                    <HeaderStyle BackColor="#428bca" />
                </asp:GridView>
                <div class="container">
                    <div>
                        <asp:LinkButton ID="btnApprove" UseSubmitBehavior="false" class="btn btn-primary"
                            ToolTip="click to Approve the Selected Leave" runat="server" Text="Approve" OnClick="Approve_Click"><span class="glyphicon glyphicon-thumbs-up"></span> Approve</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" Value="0" ID="hdnPageLoad" />
        <asp:HiddenField runat="server" Value="0" ID="hdnEmpTd" />
        <asp:HiddenField runat="server" Value="0" ID="hdnSessionId" />
        <asp:HiddenField runat="server" Value="0" ID="hdnUsername" />
    </asp:Panel>
    <div id="lblgrdtimesheet" class="bg-danger row text-center" runat="server" visible="false">
        <asp:Label ID="label3" runat="server" CssClass="label" Width="250px" Height="25px"
            ForeColor="Black" Font-Size="Small">No Data Found!</asp:Label>
    </div>
    <%--ADDED BY SOPHIA" --%>
    <%--<div style="margin:20px">
        <iframe id="urIframe" runat="server" style="width: 100%;height:600px;border:0" scrolling="yes"></iframe>
    </div>--%>
</asp:Content>
<%--//ADDED BY SOPHIA//--%>
<asp:Content ID="Content3" class="tets" ContentPlaceHolderID="cphBasic_" runat="Server">
    <div style="padding-top: 30px;" align="center">
        <table border="0" width="750px" align="center" class="table table-responsive wpn_content">
            <tr>
                <td colspan="2" class="Header_style">
                    View TimeSheet
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" border="0" width="80%" cellpadding="3" cellspacing="2">  <%-- //ADDED BY SOPHIA//--%>
                        <tr>
                            <td align="center">
                                <%--<div class="col-lg-4" style="padding-top: 30px;" align="center"></div>--%>
                                <asp:DropDownList ID="ddlSelectEmpName" runat="server" AutoPostBack="false">       
                                </asp:DropDownList>
                                <%--<asp:Button ID="btnGO" runat="server" UseSubmitBehavior="false" OnClick="btnGetEmpLeaveHistory_Click"
                                Text="GO" AutoPostBack="false" CssClass="btn btn-primary" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:RadioButton ID="rbtnBydate" runat="server" Text="By Date" OnCheckedChanged="Bydate_CheckedChanged"
                                    AutoPostBack="True" GroupName="gridview"></asp:RadioButton>
                                <asp:RadioButton ID="rbtnByRange" runat="server" Text="By Range" AutoPostBack="True"
                                    OnCheckedChanged="rbtnByRange_CheckedChanged" GroupName="gridview"></asp:RadioButton>
                                <asp:RadioButton ID="rbtnBymonth" runat="server" Text="By Month" OnCheckedChanged="Bymonth_CheckedChanged1"
                                    AutoPostBack="True" GroupName="gridview"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlday" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Select Date:
                                            </td>
                                            <td class="td_input_Style">
                                                <asp:TextBox ID="txtdate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox>
                                                <a href="#" onclick="showCalendarControl('<%=txtdate.ClientID%>','NoFuture'),getFocus()">     <%-- //ADDED BY SOPHIA//--%>
                                                    <img id="imgDOR" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif"
                                                        style="border: 0; vertical-align: middle;" /></a>
                                                <asp:RequiredFieldValidator ID="rfvTaskDate" ControlToValidate="txtdate" runat="server"
                                                    Font-Size="X-Small" ValidationGroup="ViewTimesheet" ErrorMessage="Enter task date">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlRange" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Select Range:
                                            </td>
                                            <td>
                                                From:<asp:TextBox ID="txtFromdate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtFromdate.ClientID%>','NoFuture'),getFocus()"><img     
                                                        id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                            <td>
                                                To:<asp:TextBox ID="txtTodate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtTodate.ClientID%>','NoFuture'),getFocus()"><img      
                                                        id="img2" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="PnlMonth" runat="server">
                                    <table>
                                        <tr>
                                            <td width="120px">
                                                Select Month:
                                            </td>
                                            <td class="td_input_Style">
                                                <asp:DropDownList ID="ddlmonth_" runat="server" Width="120px">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonth" ControlToValidate="ddlmonth_" InitialValue="0"
                                                    runat="server" Font-Size="X-Small" ValidationGroup="ViewTimesheet" ErrorMessage="Select the month">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td width="100px">
                                                Select Year:
                                            </td>
                                            <td class="td_input_Style">
                                                <asp:DropDownList ID="ddlyear" runat="server" Width="120px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvYear" ControlToValidate="ddlyear" InitialValue="--Select--"
                                                    runat="server" Font-Size="X-Small" ValidationGroup="ViewTimesheet" ErrorMessage="Select the Year">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnview1" runat="server" Text="View" CssClass="btn btn-primary" ValidationGroup="ViewTimesheet"
                                    OnClick="btnview_Click" />
                                <asp:Button ID="btnWeeklyStatus" runat="server" Text="View" ValidationGroup="ViewTimesheet"
                                    OnClick="btnWeeklyStatus_Click" CssClass="btn btn-primary" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="messagealert" id="alert_container">     <%-- //ADDED BY SOPHIA//--%>
        </div>
        <br />
        <asp:Panel ID="Panel1" runat="server">
            <%--<table border="0" width="750px" align="center" class="wpn_content">
                <tr>
                    <td>--%>
            <div class="table table-responsive">
                <asp:GridView ID="grdTimesheet" CssClass="table table-bordered table-responsive"
                    runat="server" Width="100%" CellSpacing="0" CellPadding="4" ForeColor="#333333"
                    ShowFooter="True" GridLines="None" OnRowCancelingEdit="grdTimesheet_RowCancelingEdit"
                    OnRowDeleting="grdTimesheet_RowDeleting" OnRowEditing="grdTimesheet_RowEditing"
                    OnRowUpdating="grdTimesheet_RowUpdating" AutoGenerateColumns="False" OnRowDataBound="grdTimesheet_RowDataBound"
                    Font-Size="X-Small">
                    <Columns>
                        <asp:TemplateField HeaderText="RowID" Visible="False" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblRowID" runat="server" Text='<%#Bind("id")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRowID" runat="server" Text='<%#Bind("id")%>'>
                                </asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TaskDate" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Width="80px" Text='<%#Bind("TaskDate")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Width="80px" Text='<%#Bind("TaskDate")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblProject" runat="server" Width="100px" Text='<%# Bind("ProjectName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblProjectEdit" Visible="false" runat="server" Text='<%# Bind("ProjectId")%>'></asp:Label>
                                <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlProject_SelectedIndexChanged1" AppendDataBoundItems="true"
                                    DataValueField="ProjectId" Width="100px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblModule" runat="server" Width="100px" Text='<%# Bind("ModuleName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblModuleEdit" Visible="false" runat="server" Text='<%# Bind("ModuleId") %>'></asp:Label>
                                <asp:DropDownList ID="ddlModuleList" runat="server" DataTextField="ModuleName" DataValueField="ModuleId"
                                    OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged1" AutoPostBack="true"
                                    AppendDataBoundItems="true" Width="100px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Task" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblTask" runat="server" Width="100px" Text='<%# Bind("TaskName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTaskEdit" runat="server" Visible="false" Text='<%# Bind("TaskId") %>'></asp:Label>
                                <asp:DropDownList ID="ddlTask" runat="server" DataTextField="TaskName" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"
                                    AutoPostBack="true" DataValueField="TaskId" AppendDataBoundItems="true" Width="100px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Task Description" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Width="250px" Text='<%# Bind("TaskDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" runat="server" Width="250px" Height="70px" Text='<%# Bind("TaskDescription") %>'
                                    TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issues" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblIssues" runat="server" Width="100px" Text='<%# Bind("Issues") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIssues" runat="server" Width="100px" Text='<%# Bind("Issues") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Object" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblObject" runat="server" Width="100px" Text='<%# Bind("Object") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtObject" runat="server" Width="100px" Text='<%# Bind("Object")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbltxttotal" runat="server" Text="Total Hours" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>' Width="100px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblStatusEdit" runat="server" Text='<%# Bind("StatusId") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="90px">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvStatus" ControlToValidate="ddlStatus" runat="server" 
                            SetFocusOnError="true" ValidationGroup="test" ErrorMessage="Select a Status "
                            Font-Size="XX-Small" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblHours" runat="server" Width="40px" Text='<%# Bind("Hours") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHours" runat="server" Width="50px" MaxLength="5" Text='<%# Bind("Hours") %>'
                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="rfvHours" ControlToValidate="txtHours" runat="server"
                                            SetFocusOnError="true" ValidationGroup="test" ErrorMessage="Enter hours" Font-Size="XX-Small"></asp:RequiredFieldValidator>--%>
                                <asp:RangeValidator ID="HoursValidator" runat="server" Font-Size="XX-Small" ValidationGroup="test"
                                    ErrorMessage="Hours exceeded" ControlToValidate="txtHours" MaximumValue="23.59"
                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ValidationGroup="test" ShowEditButton="True" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                    <HeaderStyle BackColor="#428bca" />
                    <%--         <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                </asp:GridView>
            </div>
            <%-- </td>
                </tr>
                <tr>--%>
            <%--<td align="center">--%>
            <%--<asp:Button ID="btnSendmail" runat="server" class="btn btn-primary" Text="Send Mail"
                ValidationGroup="test" OnClick="btnSendmail_Click" />
            <asp:Button ID="btnExportToExcel" runat="server" class="btn btn-primary" Text="ExportToExcel" OnClick="btnExportExcel_Click" 
              />--%>
            <%--</td>--%>
            <%-- </tr>
            </table>--%>
        </asp:Panel>
    </div>
</asp:Content>
