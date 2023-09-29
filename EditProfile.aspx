<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="EditProfile.aspx.cs" Inherits="Includes_WebForm_EditProfile" Title="Edit Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
     <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <%--    <link href="../bootstrap-3.3.6/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../bootstrap-3.3.6/js/jquery-1.12.0.min.js" type="text/javascript"></script>--%>
    <%--<script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript" language="javascript">

        window.history.forward(1);


        function AgeValidation(test) {
            var dob = document.getElementById('<%=txtDOB.ClientID%>').value;
            //alert(dob.length);
            if (dob != "") {
                var temp = dob.substring(3, 5);
                var temp = temp + '/' + dob.substring(0, 2);
                var temp = temp + '/' + dob.substring(6, 10);
                var EnteredDate = new Date(temp);

                // alert(EnteredDate);

                var TodayDate = new Date();
                var diff = new Date - new Date(EnteredDate);
                var diffdays = diff / 1000 / (60 * 60 * 24);
                var age = Math.floor(diffdays / 365.25);

                //alert(age);

                if (EnteredDate <= TodayDate) {
                    if (age < 18) {
                        alert('Age should be above 18');
                        document.getElementById('<%=txtDOB.ClientID%>').value = "";
                        document.getElementById('<%=txtDOB.ClientID%>').focus();
                    }
                    else {
                        var dateString = day + "/" + month + "/" + year;
                        document.getElementById('<%=txtDOB.ClientID%>').value = dateString;
                    }
                }
                else {
                    alert('Future date is not allowed.');
                    document.getElementById('<%=txtDOB.ClientID%>').value = "";
                    document.getElementById('<%=txtDOB.ClientID%>').focus();
                }
            }
            else {
                alert("Select a Date of birth");
                document.getElementById('<%=txtDOB.ClientID%>').value = "";
                document.getElementById('<%=txtDOB.ClientID%>').focus();
            }
        }
        function onlyAlphabet(event, input) {
            var charCode = event.which || event.keyCode;

            if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode === 8) {
                return true; // Allow uppercase and lowercase letters, and Backspace
            } else {
                event.preventDefault();
                return false; // Block other characters, including spaces
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
                <div class="jumbotron">
                    <div class="row">
                        <h2 class="text-primary">
                            Edit Profile</h2>
                    </div>
                    <br />
                    <div class="row">
                        <span class="Mandatory">*</span> Represents Mandatory Feilds
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            First Name<span class="Mandatory">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="190px" Height="31px" onkeypress="return onlyAlphabet(event,this);"
                                required=""></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Last Name
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtLastName" runat="server" Width="190px" Height="31px" onkeypress="return onlyAlphabet(event,this);"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Date Of Birth <span style="color: #FF0000">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtDOB" runat="server" Width="190px" Height="31px" ToolTip="dd-mm-yyyy"
                                ReadOnly="true"></asp:TextBox>
                            <a onclick="showCalendarControl('<%=txtDOB.ClientID%>','DOB')">
                                <img id="imgDOB" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif"
                                    style="border: 0; vertical-align: middle;" /></a>
                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDOB"
                                ErrorMessage="Select your Date of birth" Font-Size="XX-Small" SetFocusOnError="true"
                                ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Mobile.No<span class="Mandatory">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" Width="190px" Height="31px"
                                onkeypress="return isNumberKey(event)" required=""></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regMobileNo" runat="server" Font-Size="XX-Small"
                                ErrorMessage="Enter the correct number" ValidationExpression="^[1-9][0-9]{9}$"
                                ValidationGroup="Registration" ControlToValidate="txtMobileNo"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Gender<span class="Mandatory">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:DropDownList ID="ddlGender" runat="server" Style="text-align: right" Width="190px"
                                Height="31px">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Male" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rgvGender" runat="server" ControlToValidate="ddlGender"
                                ErrorMessage="Select the Gender" Font-Size="XX-Small" InitialValue="0" SetFocusOnError="true"
                                ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Personal Email Id<span class="Mandatory">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtEmailId" runat="server" Width="190px" required="" Height="31px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtEmailId"
                                EnableClientScript="true" ErrorMessage="Enter valid Email-ID" SetFocusOnError="true"
                                Font-Size="XX-Small" ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$"
                                ValidationGroup="Registration"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Address
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="190px" Height="31px"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Date Of Joining
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtDOJ" runat="server" Width="190px" Height="31px" BackColor="LightGray"
                                ToolTip="dd-mm-yyyy" ReadOnly="true"></asp:TextBox>
                            <a onclick="showCalendarControl('<%=txtDOJ.ClientID%>','NoFuture')">
                                <img id="imgDOJ" runat="Server" alt="Calendar" visible="false" src="../Images/PopupCalendar.gif"
                                    style="border: 0; vertical-align: middle;" /></a>
                            <asp:RequiredFieldValidator ID="reqfvDOJ" runat="server" ControlToValidate="txtDOJ"
                                EnableClientScript="true" ErrorMessage="Select your Date of Joining" Font-Size="XX-Small"
                                SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Official Mail ID(User Name)
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtUserName" runat="server" BackColor="LightGray" Width="190px"
                                Height="31px" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Enter your Email-ID" Font-Size="XX-Small" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="revUserName" runat="server" ControlToValidate="txtUserName"
                                EnableClientScript="true" ErrorMessage="Enter valid Email-ID" Font-Size="XX-Small"
                                SetFocusOnError="true" ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$"
                                ValidationGroup="Registration"> </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Select security question?<span class="Mandatory">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:DropDownList ID="ddlQuestions" runat="server" CausesValidation="True" DataTextField="Question"
                                DataValueField="QuesID" Width="190px" Height="31px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlQuestions"
                                ErrorMessage="Select question" Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"
                                InitialValue="--Select--"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            Answer<span class="Mandatory">*</span>
                        </div>
                        <div class="col-lg-6">
                            <asp:TextBox ID="txtAnswer" runat="server" Width="190px" required="" Height="31px"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-6">
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Save" CssClass="btn btn-primary"
                                ValidationGroup="Registration" />
                           <%-- <asp:Button ID="btnGotoHome" runat="server" OnClick="btnGotoHome_Click" Text="Home"
                                CssClass="btn btn-default" /></div>--%>
                    </div>
                    <br />
                </div>
            </div>
            <div class="col-lg-2">
            </div>
        </div>
    </div>
    </div>
</asp:Content>
