<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="Includes_WebForm_ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<%--Added by sophia--%>
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
  <%--  Till here--%>

    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language ="javascript">

          window.history.forward(1);

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" Runat="Server">
<br />
<div>
   <table border="0" width="750px" align="center" class="wpn_content">
   <tbody>
        <tr>
            <td colspan="2" class="Header_style">
                Reset Blocked User
            </td>
        </tr>
        <tr>
        <td>
        <table align="center" border="0" width="80%" cellpadding="3" cellspacing="2" class="stylized">
        <tr>
            <td class="td_smalllabel_Style" valign="top">
                <asp:Label ID="lblBlockedUser" Text="" runat="server"></asp:Label>
            </td>
            <td class="td_input_Style">
                <asp:CheckBoxList ID="cbkBlockedUserList" runat="server" >
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="td_smalllabel_Style"></td>
        </tr>
        <tr>
            <td class="td_smalllabel_Style">
            </td>
            <td class="td_input_Style">
                <asp:Button ID="btnResetPass" Text="Reset" runat="server" CssClass="button_Style"
                    onclick="btnResetPass_Click" />
                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="button_Style"
                    onclick="btnCancel_Click" />
                    <asp:Button ID="btnBack" runat="server" CssClass="button_Style" 
                                 Text="Back" onclick="btnBack_Click" />
            </td>
        </tr>
    </table>
    </td>
    </tr>
    </tbody>
    </table> 
</div>
</asp:Content>

