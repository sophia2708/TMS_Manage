<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="ChangeUserPassword.aspx.cs" Inherits="Includes_WebForm_ChangeUserPassword"
    Title="Change Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
     <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        window.history.forward(1);
        //Added
        setTimeout(function () {
            $('body').mouseup(function () {
                $('.dropdown.open').removeClass('open');

            });
         },1000);
        

        function tSpeedValue(test) {

            var OldPassword = document.getElementById('<%=txtPassword.ClientID%>').value;
            var NewPassword = document.getElementById('<%=txtNewpassword.ClientID%>').value;
            if (OldPassword == NewPassword) {
                alert('New Password Cannot be same as Old Password');
                document.getElementById('<%=txtNewpassword.ClientID%>').value = "";
                document.getElementById('<%=txtNewpassword.ClientID%>').focus();
                return false;
            }
            else {
                return true;
            }
        }
        function clear() {
            try {
                document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value = "";
                alert("Enter the New Password and Confirm Password");
            }
            catch (e) {
                alert(e.message);
            }
        }
        //    function clearConfirmpassword() {

        //        if (document.getElementById('<%=txtNewpassword.ClientID%>').value == "") {
        //            clear();
        //        }
        //        var NewPassword = document.getElementById('<%=txtNewpassword.ClientID%>').value;
        //        var Confirmpassword = document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value;
        //        if (NewPassword != Confirmpassword) {
        //            alert('Confirm Password should be same as New password');
        //            document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value = "";
        //            document.getElementById('<%=txtConfirmNewpassword.ClientID%>').focus();
        //            return false;
        //        }

        //    }
        function mandatory() {
            try {
                var OldPassword = document.getElementById('<%=txtPassword.ClientID%>').value;
                var NewPassword = document.getElementById('<%=txtNewpassword.ClientID%>').value;
                var Confirmpassword = document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value;
                if (OldPassword == '' && NewPassword == '' && Confirmpassword == '') {
                    alert('please enter Old Password, New Password and Confirm password');
                    document.getElementById('<%=txtPassword.ClientID%>').focus();
                    return false;
                }
                else if (OldPassword == '') {
                    alert('please enter Old password');
                    document.getElementById('<%=txtNewpassword.ClientID%>').value = "";
                    document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value = "";
                    document.getElementById('<%=txtPassword.ClientID%>').focus();

                    return false;
                }
                else if (NewPassword == '' && Confirmpassword == '') {
                    alert('please enter New Password and Confirm password');
                    document.getElementById('<%=txtNewpassword.ClientID%>').focus();
                    return false;
                }
                else if (NewPassword == '') {
                    alert('please enter New Password ');
                    document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value = "";
                    document.getElementById('<%=txtNewpassword.ClientID%>').focus();
                    return false;
                }
                else if (Confirmpassword == '') {
                    alert('please enter Confirm password');
                    document.getElementById('<%=txtConfirmNewpassword.ClientID%>').focus();
                    return false;
                }
                if (document.getElementById('<%=txtNewpassword.ClientID%>').value == "") {
                    clear();
                }
                if (NewPassword != Confirmpassword) {
                    alert('Confirm Password should be same as New password');
                    document.getElementById('<%=txtConfirmNewpassword.ClientID%>').value = "";
                    document.getElementById('<%=txtConfirmNewpassword.ClientID%>').focus();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-lg-2">
            </div>
            <div class="col-lg-8">
                <div class="jumbotron" style="padding-top: 30px;">
                    <asp:Panel ID="pnlChangePassword" runat="server" DefaultButton="btnSave">
                        <div class="row">
                            <h3 class="text-primary">
                                Change Password
                            </h3>
                        </div>
                        <div class="row">
                            <span class="Mandatory">*</span> Represents Mandatory Feilds
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                Your User Name
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtUserName" runat="server" disabled="true" Width="190px" Height="31px"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                Enter Old Password<span class="Mandatory">*</span>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="190px" Height="31px"
                                    onchange="return tSpeedValue(this);"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                New Password<span class="Mandatory">*</span> <a id="A2" runat="server" href="javascript:;"
                                    onclick="PasswordPolicy();" style="font-size: 10px">Password Policy</a>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtNewpassword" runat="server" TextMode="Password" onchange="return tSpeedValue(this);"
                                    Width="190px" Height="31px"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="txtNewpassword"
                                    EnableClientScript="true" SetFocusOnError="true" Font-Size="XX-Small" ErrorMessage="Read password policy"
                                    ValidationExpression="(?=^.{8,14}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$"
                                    ValidationGroup="Registration"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-6">
                                Confirm New Password<span class="Mandatory">*</span>
                            </div>
                            <div class="col-lg-6">
                                <asp:TextBox ID="txtConfirmNewpassword" runat="server" TextMode="Password" Width="190px"
                                    Height="31px"></asp:TextBox>
                                <br />
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <asp:Button ID="btnSave" runat="server" OnClientClick="return mandatory();" OnClick="btnSave_Click"
                                Text="Save" CssClass="btn btn-primary" ValidationGroup="Registration" Width="65px" />
                            <asp:Button ID="Btncancel" runat="server" OnClick="Btncancel_Click" Text="Clear"
                                CssClass="btn btn-default" />
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
    </div>
</asp:Content>
