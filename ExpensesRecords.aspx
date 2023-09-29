<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="ExpensesRecords.aspx.cs" Inherits="Includes_WebForm_ExpensesRecords" Title="ExpensesRecords" %>

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
       
    </script>
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px;" align="center">
        <table border="0" width="750px" align="center" class="wpn_content">
            <tbody>
                <tr>
                    <td colspan="2" class="Header_style">
                        Expenses List
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        .
                    </td>
                </tr>
                <tr>
                    <td>
                        <table align="center" border="0" width="80%" cellpadding="3" cellspacing="2" class="stylized">
                            <tbody>
                                <tr>
                                    <td align="left">
                                        <asp:LinkButton ID="lbtnAddExpenses" runat="server" CausesValidation="false"
                                            onmouseover="this.style.color='red'" onmouseout="this.style.color='blue'" OnClick="lbtnAddExpenses_Click">Add Expenses</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:LinkButton ID="lbtnExpensesList" runat="server" CausesValidation="false" onmouseover="this.style.color='red'"
                                            onmouseout="this.style.color='blue'" OnClick="lbtnExpensesList_Click">Expenses List</asp:LinkButton>
                                    </td>
                                </tr>
                                
                                
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>

