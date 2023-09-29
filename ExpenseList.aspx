<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="ExpenseList.aspx.cs" Inherits="Includes_WebForm_ExpenseList" Title="ExpenseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>PaySlips</title>
    <style type="text/css">
      
    </style>
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
     <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
   <%--<script type="text/javascript" src="https://cdn.jsdelivr.net/jquery/latest/jquery.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
<link rel="stylesheet" type="text/css" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />--%>
    
    <%--ADDED BY SOPHIA--%>
   
    <script type="text/javascript">
        $(document).mouseup(function (e) {
            var container = $("#CalendarControl");
            if (!container.is(e.target) && container.has(e.target).length === 0)
                hideCalendarControl()
        });
//        $(function () {
//            $('input[id*="txtDateRange"]').daterangepicker({
//                autoUpdateInput: true,
//                opens: 'center',
//                timePicker: false,
//                showDropdowns: false,
//                timePicker24Hour: true,
//                timePickerIncrement: 15,
//                minDate: moment(),
//                startDate: moment().startOf('hour'),
//                endDate: moment().startOf('hour').add(32, 'hour'),
//                locale: {
//                    separator: "-",
//                    format: 'DD/MM/YYYY',
//                    "firstDay": 1
//                }
//            });
//        });
    </script>
    <style type="text/css">       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server" >
  <div  style="padding-top: 30px;" align="center">
        <%--<asp:TextBox runat="server" ID="txtDateRange" Width="300px" class="form-control" />
        <br />
        <asp:Button ID="Button1" Text="Submit" runat="server"  class="btn btn-primary" />
        <br />
        <br />
        <asp:GridView runat="server" ID="gvDates" CssClass="table table-responsive" />--%>
        <table border="0" width="750px" align="center" class="table table-responsive wpn_content">
            <tr>
                <td colspan="2" class="Header_style">
                    View Expense List
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" border="0" width="80%" cellpadding="3"
                        cellspacing="2">
                        <tr>
                            <td align="center">
                                <asp:Label ID="rbtnByRange" runat="server" Text="By Range" AutoPostBack="True"
                                    GroupName="gridview" OnCheckedChanged="rbtnByRange1_CheckedChanged"></asp:Label>
                                
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
                                                From:<asp:TextBox ID="txtFromdate" runat="server" Width="110px"  ToolTip="dd-mm-yyyy" ></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtFromdate.ClientID%>','NoFuture')"><img
                                                        id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                            <td>
                                                To:<asp:TextBox ID="txtTodate" runat="server" Width="110px"  ToolTip="dd-mm-yyyy" ></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtTodate.ClientID%>','NoFuture')"><img
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
                                <asp:Button ID="btngo" runat="server" Text="GO" CssClass="btn btn-primary" OnClick="btnSave_Click1" ValidationGroup="ViewTimesheet"
                                     />
                               <%-- <asp:Button ID="btnWeeklyStatus" runat="server" Text="View" ValidationGroup="ViewTimesheet"
                                    CssClass="btn btn-primary" Visible="false" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlgridviewAddExpenseHistory" runat="server">
    <asp:GridView ID="grdAddExpenseHistory" runat="server"
                CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                UseAccessibleHeader="true" OnRowCommand="grdAddExpenseHistory_RowCommand" AutoGenerateColumns="false"
                OnPreRender="grdAddExpenseHistory_PreRender" CellSpacing="0" Width="50%" OnRowDataBound="grdAddExpenseHistory_RowDataBound"
                CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grdAddExpenseHistory_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("Expensedate","{0:d}") %>'></asp:Label>                             
                        </ItemTemplate>
                             </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExpenseType" FooterStyle-CssClass="danger">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblExpenseType" runat="server" Text='<%# Bind("ExpenseType") %>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                                <asp:Label ID="lbltotalamount" runat="server" Text="Total Amount" Font-Bold="true" />
                            </FooterTemplate>   
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblAmount" runat="server" Text=' <%# Bind("Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paid Name">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPaidName" runat="server" Text='<%# Bind("PaidName") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField> 
                                    
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
            </asp:GridView>
            <asp:Button ID="btnExportToExcel" runat="server" class="btn btn-primary" Text="ExportToExcel"
              OnClick="btnExcel_Click"  />
            </asp:Panel>
    </div>          
</asp:Content>


