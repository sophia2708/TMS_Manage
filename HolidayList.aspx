<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="HolidayList.aspx.cs"  Inherits="Includes_WebForm_HolidayList"
    Title="HolidayList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HolidayList</title>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
        <script type="text/javascript">
            $(document).ready(function () {
                document.getElementById("currentYear").innerHTML = new Date().getFullYear(); 
            });
        </script>   
       <h3 class="text-primary">Holiday List for Year <span id="currentYear"></span></h3>
         <asp:GridView ID="grdLeaveHistory1"  runat="server"
                CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                UseAccessibleHeader="true"  AutoGenerateColumns="false"
                 CellSpacing="0" Width="30%" 
                CellPadding="4" ForeColor="#333333"  >
                <AlternatingRowStyle BackColor="White" /> 
                <Columns>
                    <asp:TemplateField HeaderText="Holiday Date">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblHolidayDate" runat="server" Text='<%# Bind("Holiday_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Holiday Name">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
                 </asp:GridView>
                 <%--<div class="bg-danger row text-center" id="holidaynorecord" runat="server">
                <asp:Label ID="Label5" runat="server" CssClass="label" Width="350px" Height="25px"
                    ForeColor="Black" Font-Size="Small">No Records Found!</asp:Label>
            </div>     --%>           
</asp:Content>
