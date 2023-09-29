<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="Feedback.aspx.cs" Inherits="Includes_WebForm_Feedback" Title="Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Feedback</title>
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
    <script type="text/javascript">

        $(document).ready(function () {
            $('#idUserName').html(document.querySelector('#ctl00_lblFirstName').innerText.replace('Welcome ', ''));
        });
        function checkOnChange(e) {
        let name = document.querySelector('#ctl00_lblFirstName').innerText.replace('Welcome ', '');
        if(e.checked)
            $('#idUserName').html('Anonymous')
            else
            $('#idUserName').html(name);
        }
        function sendMail() {
        
            var html = (document.getElementById('<%= txttextarea.ClientID %>').innerHTML).replaceAll('contenteditable="false"', '').replaceAll('contenteditable="true"', '');
            var data = { Contect: html,Ananymous: document.getElementById('<%= checkBox.ClientID %>').checked ? 1 : 0 };
            $.ajax({
                type: "POST",
                url: "Feedback.aspx/submit",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data)
            }).done(function (result) {
                alert("Feedback Sent Successfully.");
                document.getElementById('<%= txtcontent.ClientID %>').innerHTML = '<br />';
            });
        }

        function EnableDisableButton(sender, target) {
            if (sender.textContent.length > 0)
                document.getElementById('btnsendmail').disabled = false;
            else
                document.getElementById('btnsendmail').disabled = true;
        }
        function fnPreventHeader() {
            var contentHtml = $.trim(document.getElementById('<%= txtcontent.ClientID %>').innerHTML)
            if (contentHtml == '<br>' || contentHtml == '') {
                var evt = event || window.event;
                if (evt) {
                    var keyCode = evt.charCode || evt.keyCode;
                    if (keyCode === 8) {
                        if (evt.preventDefault) {
                            evt.preventDefault();
                        } else {
                            evt.returnValue = false;
                        }
                    }
                }

            }
        }
     
    </script>
    <style type="text/css">
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px; align: center">
        <table border="0" width="750px;align:center" class="table table-responsive wpn_content">
            <tr>
                <td colspan="2" class="Header_style">
                    Feedback
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div class="row" style="text-align: center;">
        <div class="col-lg-12" style="margin-bottom: 10px">
            <asp:Label ID="lbltxtarea" runat="server" Text="Feedback Description" Font-Size="20px"
                Width="100%" />
            <%--<asp:Label class="col-xs-3 control-label"  style="margin-left:425px"  >
                                 TextArea</label>--%>
        </div>
        <div class="col-lg-12">
            <%--<asp:TextBox ID="txttextarea" runat="server"   Width="400px" Height="150px" placeholder="Enter the Reason"
                TextMode="MultiLine" data-toggle="tooltip" ToolTip="Reason" onkeyup="EnableDisableButton(this,'btnsendmail')"></asp:TextBox>--%>
            <%-- <textarea  Width="400px" Height="150px" placeholder="Enter the Reason"
                TextMode="MultiLine" data-toggle="tooltip" ToolTip="Reason" onkeyup="EnableDisableButton(this,'btnsendmail')"></textarea>--%>
            <div id="txttextarea" runat="server" style="width: 400px; padding: 10px; border: 1px solid;
                height: 200; text-align: left; overflow-y: auto; margin: auto;" contenteditable="true"
                onkeydown="fnPreventHeader()" onkeyup="EnableDisableButton(this,'btnsendmail')">
                <div id="txtfield" contenteditable="false">
                    Hi Sir/Madam,</div>
                <div id="txtcontent" runat="server" contenteditable="true">
                    <div>
                        <br />
                       
                    </div>
                </div>
                <div contenteditable="false">
                    <br />
                    <span style="display: block;"><b>Thanks & Regards,</b></span><b><span id="idUserName"
                        style="display: inline-block"></span></b>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row col-lg-12" style="display:flex;">
        <div class="checkbox-container" style="width: 400px; padding: 10px; text-align: left;
            margin: auto;">
            <span><input type="checkbox" id="checkBox" runat="server" Font-Size=larger onchange="checkOnChange(this)"/>
            <span>Send Anonymous FeedBack</span></span>
            <input type="button" style="float:right" value="Send Mail" class="btn btn-primary"
                id="btnsendmail" disabled="false" onclick="sendMail()" />
        </div>
        <br />
    </div>
    <input type="hidden" id="hdnContent" value="" />
</asp:Content>
