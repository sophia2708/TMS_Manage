<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Includes_WebForm_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link rel="icon" type="image/x-icon" href="~/Includes/Images/Logo_AB.png" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="../bootstrap-3.3.6/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Bootstrap/css/Glyphicons.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/LoginPage.js" type="text/javascript"></script>
    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.12.0.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        window.history.forward(1);

    </script>
    <style type="text/css">
        .messagealert
        {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
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
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
        window.setTimeout(function () {
            $("#alert_container").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 3000);
    </script>
</head>
<body>
     <%--    <form id="form1" runat="server">
    <div align="center" style="padding-top: 80px; height: 238px;">
        <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/Includes/Images/ablogo.png" />
        <br />
        <asp:Panel ID="PnlLogin" runat="server" Width="450px" Height="250px">
            <table border="0" width="98%" align="center" class="wpn_content">
                <tr>
                    <td width="100px" align="left">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name" ForeColor="#666666"></asp:Label>
                    </td>
                    <td width="200px" align="left">
                        <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="100px" align="left">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" ForeColor="#666666"></asp:Label>
                    </td>
                    <td width="200px" align="left">
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td align="left" width="120px">
                        <asp:LinkButton ID="lbtnForgetPassword" runat="server" CausesValidation="false" OnClick="lbtnForgetPassword_Click"
                            onmouseover="this.style.color='red'" onmouseout="this.style.color='blue'" Font-Size="X-Small">Forgot Password</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td width="100px">
                    </td>
                    <td width="200px" align="center">
                        <asp:Button ID="btnLogin" runat="server" OnClientClick="return UsernamePasswordValidation();"
                            OnClick="Login_Click" Text="Login" CssClass="button_Style" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClientClick="return ClearLoginForm();"
                            CssClass="button_Style" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>--%>
    <form id="form1" runat="server">
    <div class="container">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="container">
            <div class="row">
            </div>
        </div>
        <div align="center" class="container">
            <div class="row">
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4">
                    <img src="../Images/ablogo.png" alt="" />
                </div>
                <div class="col-lg-4">
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <br />
                <div class="col-lg-4">
                </div>
                <div class="col-lg-4 jumbotron">
                    <form role="form">
                    <div class="form-group">
                        <h3 align="center" style="font-family: 'Courier New', Courier, monospace; font-size: x-large;
                            font-weight: bold; color: #333333;">
                            Login</h3>
                        <br />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblresult" Style="color: green;" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="txtUserName" placeholder="UserID" runat="server" ToolTip="UserId@Ab.com"
                                CssClass="form-control" required="" 
                                ontextchanged="txtUserName_TextChanged" /><span class="input-group-addon"> <span class="glyphicon glyphicon-user">
                                </span></span>
                        </div>
                        <asp:RegularExpressionValidator ID="ReGVal" runat="server" ControlToValidate="txtUserName"
                            EnableClientScript="true" ErrorMessage="Enter Valid EmailId" Font-Size="XX-Small"
                            SetFocusOnError="true" ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$"
                            ValidationGroup="Registration"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="txtPassword" placeholder="Password" TextMode="Password" runat="server"
                                CssClass="form-control" required="" /><span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                        </div>
                    </div>
                    <asp:LinkButton ID="lbtnForgetPassword" runat="server" CausesValidation="false" OnClick="lbtnForgetPassword_Click"
                        Font-Size="9">Forgot Password</asp:LinkButton>
                    <br />
                    <br />
                    <div align="center">
                        <asp:Button ID="btnLogin" runat="server" Text="Submit" CssClass="btn btn-primary"
                            OnClientClick="return UsernamePasswordValidation();" OnClick="Login_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary"
                            OnClientClick="return ClearLoginForm();" Width="70px" /></div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
