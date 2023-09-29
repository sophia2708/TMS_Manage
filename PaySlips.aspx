<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="PaySlips.aspx.cs" Inherits="Includes_WebForm_PaySlips" Title="PaySlips" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>PaySlips</title>
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
                    includeSelectAllOption: false,
                });
                $('.multiselect.dropdown-toggle ').append('<b class="caret"></b>')
            $('[id*=employeeslist]').change(function(event) {
                if ($(this).val().length == 6) {
                    $('.multiselect-option.dropdown-item,.multiselect-option.dropdown-item *').attr('disabled',true).css('pointer-events','none');
                    $('.multiselect-option.dropdown-item.active,.multiselect-option.dropdown-item.active *').attr('disabled',false).css('pointer-events','auto');
                }else{
                    $('.multiselect-option.dropdown-item,.multiselect-option.dropdown-item *').attr('disabled',false).css('pointer-events','auto');
                }
          });
            $('button.multiselect').click(function () {
                $(this).closest('div.btn-group').toggleClass('open');
            });
            document.querySelector('.body').setAttribute('onclick', 'hideCalendar()')
        })
        

        function isvalidMonthandYear() {
            var grid = document.getElementById('<%=employeeslist.ClientID%>');
            if (grid.selectedOptions.length == 0) 
            {
                alert("Select the Month and Year");
               return false;
            }
            else {
                return "";
            }
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
            width: 200px;
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
            margin-top: -9px;
            width: 215px;
            background-color: white;
            border: 1.6px solid #cccccc;
            padding: 4px;
        }
        
        .body
        {
            overflow: hidden;
        }
        .multiselect.dropdown-toggle .caret
        {
            float: right;
            top: 10px;
            position: relative;
            right: 10px;
            width:9px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px; align: center">
        <table border="0" width="750px;align:center" class="table table-responsive wpn_content">
            <tr>
                <td colspan="2" class="Header_style" >
                    Request Payslips
                </td>
            </tr>
        </table>
    </div>
   <p style="padding-left: 1px;">To request pay slip, select the months in the below dropdown and click on the Send Mail Button.The admin team will process the <br />request and send you the requested pay slips to your email.</p>
     <br />
    <div style="display: inline-block;" >
        <label  style="padding-left: 1px;">
            Month and Year:</label>
    </div>
    <div style="display: inline-block;">
        <asp:ListBox runat="server" 
           autocomplete="off" ID="employeeslist" SelectionMode="multiple"
            DataTextField="" DataValueField=""></asp:ListBox>
        <%--<span class="glyphicon glyphicon-chevron-down" runat="server" onserverclick="LogOff_Click" style="margin-left: -402px;position: absolute"></span>--%>
    </div>
    <br />
    <br />
    <div  >
        <asp:Button ID="btnsendmail" runat="server" Text="Send Mail" Width="100px" class="btn btn-primary"
            OnClick="ButtonSendMail_Click" OnClientClick="return isvalidMonthandYear();" />
    </div>
</asp:Content>
