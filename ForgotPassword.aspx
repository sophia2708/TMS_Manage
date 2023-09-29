<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Includes_WebForm_ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Forgot Password</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
    <link href="../bootstrap-3.3.6/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/jquery.min.js" type="text/javascript"></script>
    <script src="http://jqueryvalidation.org/files/dist/jquery.validate.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        window.history.forward(1);


        function ReqiredFeildValidation1() {
            try {
                if (document.getElementById('<%=txthintans.ClientID%>').value == "") {
                    alert("Enter an Answer");
                    document.getElementById('<%=txthintans.ClientID%>').focus();
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
        function ReqiredFeildValidation3() {
            try {

                if (document.getElementById('<%=txthintans.ClientID%>').value == "") {
                    alert("Enter an Answer");
                    document.getElementById('<%=txthintans.ClientID%>').focus();
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
        function ReqiredFeildValidation2() {
            try {

                if (document.getElementById('<%=txtUsername.ClientID%>').value == "") {
                    alert("Enter your UserName");
                    document.getElementById('<%=txtUsername.ClientID%>').focus();
                    return false;
                }
                var mailinput = document.getElementById('<%=txtUsername.ClientID%>').value;
                var emailFormat = /^([\w\d\-\.]+)@{1}(([\w\d\-]{1,67})|([\w\d\-]+\.[\w\d\-]{1,67}))\.(([a-zA-Z\d]{2,4}))$/;
                if (mailinput.search(emailFormat) == -1) {
                    alert('Your E-mail ID seems incorrect. Please try again!');
                    document.getElementById('<%=txtUsername.ClientID%>').focus();
                    return false;
                }

                else {
                    return true;
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
        /*
        function checkForm(obj) {
        var mailinput = document.getElementById('<%=txtUsername.ClientID%>').value;
        var emailFormat = /^([\w\d\-\.]+)@{1}(([\w\d\-]{1,67})|([\w\d\-]+\.[\w\d\-]{1,67}))\.(([a-zA-Z\d]{2,4}))$/;
        if (obj.value.search(emailFormat) == -1) {
        alert('Your E-mail ID seems incorrect. Please try again!');
        obj.focus();
        return false;
        }
        
        return true;
        }*/
        //        $(document).ready(function () {
        //            //fnloadvaluation(event);
        //            summaryclone = $("#divSummarydetails").clone();
        //            $("#<%= txtPassword.ClientID %>").keyup(function () {
        //                var strongRegex = new RegExp("^(?=.{8,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
        //                var mediumRegex = new RegExp("^(?=.{7,})(((?=.*[A-Z])(?=.*[a-z]))|((?=.*[A-Z])(?=.*[0-9]))|((?=.*[a-z])(?=.*[0-9]))).*$", "g");
        //                var weekRegex =   new RegExp("^(?=.{2,})(?=.*[a-z]).*$", "g");
        //                var enoughRegex = new RegExp("(?=.{8,}).*", "g");
        //                if (false == enoughRegex.test($(this).val())) {
        //                    
        //                   $('#lblstrength').html('Minimum 8 Characters required !!').css("color","Black");
        //                  return false;
        //                else if (strongRegex.test($(this).val())) {
        //                    $('#lblstrength').html('Strong!').css("color", "#00cc33");
        //                } else if (mediumRegex.test($(this).val())) {
        //                    $('#lblstrength').html('Medium!').css("color", "#ff8000");
        //                } else if (weekRegex.test($(this.val())) {
        //                    $('#lblstrength').html('Weak!').css("color", "#e60000");
        //                }
        //                else{
        //                $('#lblstrength').html('');
        //                }
        //                return true;
        //            });
        //        });


        function ReqiredFeildValidation() {
            try {
                if (document.getElementById('<%=txtConfirmPassword.ClientID%>').value == "" & document.getElementById('<%=txtPassword.ClientID%>').value == "") {
                    alert("Enter the New Password and Confirm Password");
                    document.getElementById('<%=txtPassword.ClientID%>').focus();
                    return false;
                }
                else if (document.getElementById('<%=txtPassword.ClientID%>').value == "") {
                    alert("Enter a New Password");
                    document.getElementById('<%=txtPassword.ClientID%>').focus();
                    return false;
                }
                else if (document.getElementById('<%=txtConfirmPassword.ClientID%>').value == "") {
                    alert("Enter a Confirm Password");
                    document.getElementById('<%=txtConfirmPassword.ClientID%>').focus();
                    return false;
                }
                else if (document.getElementById('<%=txtConfirmPassword.ClientID%>').value != document.getElementById('<%=txtPassword.ClientID%>').value) {
                    alert("Enter the same Password");
                    //document.getElementById('<%=txtConfirmPassword.ClientID%>').value = "";
                    document.getElementById('<%=txtConfirmPassword.ClientID%>').focus();
                    return false;
                }
                else {
                    return true;
                }
            }
            catch (e) {
                alert(e.message);
            }
        }
        function clearConfirmpassword() {
            try {

                if (document.getElementById('<%=txtPassword.ClientID%>').value == "") {
                    clear();
                }
            }
            catch (e) {
                alert(e.message);
            }
        }

        function clear() {
            try {
                document.getElementById('<%=txtConfirmPassword.ClientID%>').value = "";
            }
            catch (e) {
                alert(e.message);
            }
        }
    </script>
    <%--<style type="text/css">
       .jumbotron {
      background-color: #f4511e;
      color: #fff;
      padding: 100px 25px;
      font-family: Montserrat, sans-serif;
  }
    </style>--%>
    <%--<--------------------------Jumbotronbg----------------------------->--%>
    <%--  <style type="text/css">
    .bg {
  background: url('../Images/ablogo_2.png') no-repeat center center;
  position: fixed;
  width: 100%;
  height: 350px; /*same height as jumbotron */
  top:0;
  left:0;
  z-index: -1;
}

.jumbotron {
  margin-bottom: 0px;
  height: 350px;
  /*color: white;*/
  /*text-shadow: black 0.3em 0.3em 0.3em;*/
  background:transparent;
}

</style>
<script type="text/javascript">
var jumboHeight = $('.jumbotron').outerHeight();
function parallax(){
    var scrolled = $(window).scrollTop();
    $('.bg').css('height', (jumboHeight-scrolled) + 'px');
}

$(window).scroll(function(e){
    parallax();
});
</script>--%>
    <%--<style type="text/css">
.jumbotron
{
	background:url('../Images/ablogo_2.png') no-repeat center center;
	background-size:cover;
	height:100%;
	
	}


</style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-top: 30px;" align="center">
        <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/Includes/Images/ablogo.png" />
        <div class="jumbotron container">
            <div align="center">
                <h5 align="center" style="font-family: 'Courier New', Courier, monospace; font-size: xx-large; font-weight: bold;
                    font-style: normal;">
                    Forgot Password</h5>
            </div>
            <asp:Panel ID="Panel1" runat="server" BorderColor="Black" DefaultButton="">
                <div>
                    <div class="row">
                        <div class="col-lg-2">
                        </div>
                        <div class="col-lg-5">
                            <asp:Label ID="lblUserName" runat="server" Text="Enter Your Office Mail ID:" ForeColor="Black"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtUsername" runat="server"
                                Width="190px" Height="31px"></asp:TextBox></div>
                        <div class="col-lg-1">
                            <asp:Button ID="btnGo" CssClass="btn btn-primary" runat="server" Text="Go" Font-Size="Smaller"
                                OnClientClick="return ReqiredFeildValidation2();" OnClick="btnGo_Click" Width="80px">
                            </asp:Button></div>
                        <div class="col-lg-1">
                            <asp:Button ID="btnBack" runat="server" Text="Back" Width="80px" Font-Size="Smaller"
                                CssClass="btn btn-primary" OnClick="btnBack_Click"></asp:Button>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnlSecAnswer" runat="server" DefaultButton="">
                        <div class="row">
                            <div class="col-lg-2">
                            </div>
                            <div class="col-lg-5">
                                <asp:Label ID="lblhintques" runat="server" ForeColor="Black"></asp:Label>
                                &nbsp;<asp:TextBox ID="txthintans" TextMode="Password" runat="server" Width="190px"
                                    Height="31px"></asp:TextBox>
                            </div>
                            <div class="col-lg-1" id="pnlButton" runat="server">
                                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Width="80px"
                                    OnClientClick="return ReqiredFeildValidation1();" OnClick="btnsubmit_Click" Text="Submit" /></div>
                            <div class="col-lg-1">
                                <asp:Button ID="btnCancel1" CssClass="btn btn-primary" Width="80px" runat="server"
                                    OnClick="btnCancel1_Click" Text="Cancel" /></div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlNewPassword" runat="server" DefaultButton="">
                <div class="row">
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-5">
                        <asp:Label ID="lblPassword" ForeColor="Black" runat="server" Text="New Password "></asp:Label>
                        <a id="hrefPassword" href="javascript:;" onclick="PasswordPolicy();" style="font-size: 10px"
                            runat="server">Password Policy</a> &nbsp; &nbsp;<asp:TextBox ID="txtPassword" runat="server"
                                TextMode="Password" Width="190px" Height="31px" onchange="return clearConfirmpassword(this);"></asp:TextBox><label
                                    id="lblstrength" runat="server"></label></div>
                    <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                        EnableClientScript="true" SetFocusOnError="true" Font-Size="XX-Small" ErrorMessage="Read password policy"
                        ValidationExpression="(?=^.{8,14}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                        ValidationGroup="Registration"></asp:RegularExpressionValidator>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2">
                    </div>
                    <div class="col-lg-5">
                        <asp:Label ID="lblConfirm" runat="server" ForeColor="Black" Text="Confirm Password "></asp:Label>
                        &nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                        <asp:TextBox ID="txtConfirmPassword" runat="server" Height="31px" onchange="return clearConfirmpassword(this);"
                            TextMode="Password" Width="190px"></asp:TextBox></div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-3">
                    </div>
                    <div class="col-lg-5">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Registration"
                            CssClass="btn btn-primary" OnClientClick="return ReqiredFeildValidation();" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                            OnClick="btnCancel_Click" /></div>
                </div>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>


