<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="Wfhhybrid.aspx.cs" Inherits="Wfhhybrid" Title="Wfhhybrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--Added by sophia--%>
    <%--<script type="text/javascript" src="../js/jquerymin.js"></script>--%>
    <script type="text/javascript" src="../js/bootstrapmin.js"></script>
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
    <link href="../bootstrap-3.3.6-dist/css/bootstrap-theme.css" rel="stylesheet" type="text/css"/>
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
    <%-- till here--%>
    <script type="text/javascript">


        $(function () {
            $('[id*=ddlweekly]').each(function () {
                $(this).multiselect({
                    includeSelectAllOption: true
                });     
           });
            $('button.multiselect').click(function () {
                $(this).closest('div.btn-group').toggleClass('open');
            });
        })    
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
            background-color: White !important; 
            /* min-height: 100px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <script type="text/javascript" language="javascript">

        window.history.forward(1);
    </script>
    <p>
    </p>
    <asp:GridView ID="grdWfhhybrid" runat="server" BackColor="LightGoldenrodYellow"
        BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" 
        Height="122px" Width="760px" AutoGenerateColumns="False" OnRowDataBound="grdWfhhybrid_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="Empid" SortExpression="Empid" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblEmpid" runat="server" Text='<%# Bind("Empid") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>                 
                   <asp:TemplateField HeaderText="Selected WeekDays" ItemStyle-HorizontalAlign="Left"
               ItemStyle-Width="200" HeaderStyle-Wrap="true">
                <ItemTemplate>
                    <div class="form-group Ddddatesdiv1" >
                        <div id="Dddselectweeklydiv2" onclick="">
                            <asp:ListBox ID="ddlweeklyhybrid" onChange="" SelectionMode="Multiple" runat="server">
                                <asp:ListItem Value="2" Text="Monday" />
                                <asp:ListItem Value="3" Text="Tuesday" />
                                <asp:ListItem Value="4" Text="Wednesday" />
                                <asp:ListItem Value="5" Text="Thursday" />
                                <asp:ListItem Value="6" Text="Friday" />
                            </asp:ListBox>
                        </div>
                    </div>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <%--TILL HERE--%>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary" Text="Back" OnClick="btnBack_Click" />
    <asp:Button ID="btnSaveEmpList" runat="server" CssClass="btn btn-primary" Text="Save"
        OnClick="btnSaveEmpList_Click" />       
</asp:Content>
