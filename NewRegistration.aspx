<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="NewRegistration.aspx.cs" Inherits="Includes_WebForm_NewRegistration"
    Title="Registration Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

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










    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ABStyle.css" rel="stylesheet" type="text/css" />

    <script src="../Javascript/GenericForm.js" type="text/javascript"></script>
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>

<script type="text/javascript">

          window.history.forward(1);

    function AgeValidation(test) {
       var dob =document.getElementById('<%=txtDOB.ClientID%>').value;
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
                   var CorrectDate = dob.substring(0, 2) + '-' + dob.substring(3, 5) + '-' + dob.substring(6, 10);
                   document.getElementById('<%=txtDOB.ClientID%>').value = CorrectDate;
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
   function Change() {
        var v = document.getElementById("<%=txtUserName.ClientID%>");
        v.style.backgroundColor = "White";
    }
</script>

   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server" >

   <div style="padding-top: 30px;" align="center">
   <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
        <table border="0" width="750px" align="center" class="wpn_content">
       <tbody>
            <tr>
                <td colspan="2" class="Header_style" >
                   Registration
                </td>
            </tr>
            <tr>
                <td colspan="3" >
                    <span class="Mandatory">*</span> Represents Mandatory Feilds
                </td>
            </tr>
            <tr>
            <td>
            <table align="center" border="0" cellpadding="3" cellspacing="2" 
                class="stylized" width="80%">
                <tbody>
                    <tr>
                        <td class="td_smalllabel_Style">
                            First Name<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtFirstName" runat="server" 
                                onkeypress="return onlyAlphabets(event,this);" Width="147px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                                ControlToValidate="txtFirstName" ErrorMessage="Enter the First name" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                        <tr>
                        <td class="td_smalllabel_Style">
                            Last Name
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtLastName" onkeypress="return onlyAlphabets(event,this);" 
                                runat="server" Width="147px" ontextchanged="txtLastName_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Date Of Birth: <span style="color: #FF0000">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtDOB" runat="server" ReadOnly="true" 
                                ToolTip="dd-mm-yyyy" Width="147px"></asp:TextBox>
                            <a onclick="showCalendarControl('<%=txtDOB.ClientID%>','DOB')">
                            <img ID="imgDOR" runat="Server" alt="Calendar" 
                                src="../Images/PopupCalendar.gif" style="border:0; vertical-align:middle;" /></a>
                            <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" 
                                ControlToValidate="txtDOB" EnableClientScript="true" 
                                ErrorMessage="Select your Date of birth" Font-Size="XX-Small" 
                                SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Mobile.No<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" 
                                onkeypress="return isNumberKey(event)" Width="147px" 
                                ontextchanged="txtMobileNo_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" 
                                ControlToValidate="txtMobileNo" ErrorMessage="Enter the Mobile No." 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regMobileNo" runat="server" Font-Size="XX-Small" 
                                ErrorMessage="Enter the correct number" 
                                ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="Registration" 
                                ControlToValidate="txtMobileNo"></asp:RegularExpressionValidator>         
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Gender<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:DropDownList ID="ddlGender" runat="server" Style="text-align: right" 
                                Width="150px" onselectedindexchanged="ddlGender_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Male" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rgvGender" runat="server" 
                                ControlToValidate="ddlGender" ErrorMessage="Select the Gender" 
                                Font-Size="XX-Small" InitialValue="0" SetFocusOnError="true" 
                                ValidationGroup="Registration">
                     </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Personal Email Id<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtEmailId" runat="server" Width="190px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" 
                                ControlToValidate="txtEmailId" ErrorMessage="Enter your Email-ID" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
                                ControlToValidate="txtEmailId" EnableClientScript="true" 
                                ErrorMessage="Enter valid Email-ID" Font-Size="XX-Small" SetFocusOnError="true" 
                                ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$" 
                                ValidationGroup="Registration"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Address
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="146px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Date Of Joining<span class="Mandatory">*</span>
                        </td>
                         <td class="td_input_Style">           
                                <asp:TextBox ID="txtDOJ" runat="server" Width="147px" ToolTip="dd-mm-yyyy" ReadOnly="true"></asp:TextBox>
                            <a onclick="showCalendarControl('<%=txtDOJ.ClientID%>','NoFuture')">
                            <img ID="img1" runat="Server" alt="Calendar" 
                                src="../Images/PopupCalendar.gif" style="border:0; vertical-align:middle;" /></a>
                            <asp:RequiredFieldValidator ID="reqfvDOJ" runat="server" 
                                ControlToValidate="txtDOJ" EnableClientScript="true" 
                                ErrorMessage="Select your Date of Joining" Font-Size="XX-Small" 
                                SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                   </tr> 
                    <tr>
                        <td class="td_smalllabel_Style">
                            Official Mail ID(User Name)<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtUserName" runat="server" onkeypress="Change()" Width="190px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" 
                                ControlToValidate="txtUserName" ErrorMessage="Enter your Email-ID" 
                                Font-Size="XX-Small" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="revUserName" runat="server" 
                                ControlToValidate="txtUserName" EnableClientScript="true" 
                                ErrorMessage="Enter valid Email-ID" Font-Size="XX-Small" SetFocusOnError="true" 
                                ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$" 
                                ValidationGroup="Registration"> </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Password <span class="Mandatory">*</span><a ID="A2" runat="server" 
                                href="javascript:;" onclick="PasswordPolicy();" style="font-size: 10px">Password 
                            Policy</a>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtPassword" runat="server" Style="margin-left: 0px" 
                                TextMode="Password" Width="147px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                                ControlToValidate="txtPassword" ErrorMessage="Enter the Password" 
                                Font-Size="XX-Small" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="revPassword" runat="server" 
                                ControlToValidate="txtPassword" EnableClientScript="true" 
                                ErrorMessage="Read password policy" Font-Size="XX-Small" SetFocusOnError="true" 
                                ValidationExpression="(?=^.{8,14}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" 
                                ValidationGroup="Registration"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Confirm Password <span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" Style="margin-left: 0px" 
                                TextMode="Password" Width="147px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
                                ControlToValidate="txtPassword" ErrorMessage="Enter the confirm Password" 
                                Font-Size="XX-Small" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                            <br />
                            <asp:CompareValidator ID="cvConfirmPassword" runat="server" 
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" 
                                ErrorMessage=" Enter same password" Font-Size="XX-Small" 
                                ValidationGroup="Registration"> </asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Select security question?<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:DropDownList ID="ddlQuestions" runat="server" CausesValidation="True" 
                                DataTextField="Question" DataValueField="QuesID" Width="195px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvHintQuestion" runat="server" 
                                ControlToValidate="ddlQuestions" ErrorMessage="Select a question" 
                                Font-Size="XX-Small" InitialValue="--Select--" SetFocusOnError="true" 
                                ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Answer<span class="Mandatory">*</span>
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtAnswer" runat="server" Width="147px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAnswerText" runat="server" 
                                ControlToValidate="txtAnswer" ErrorMessage="Answer for a question" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                        </td>
                        <td class="td_input_Style">
                            <asp:Button ID="btnSave" runat="server" CssClass="button_Style" 
                                OnClick="btnSave_Click" Text="Save" ValidationGroup="Registration" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="button_Style" 
                                onclick="btnCancel_Click" Text="Cancel" />
                                <asp:Button ID="btnBack" runat="server" CssClass="button_Style" 
                                 Text="Back" onclick="btnBack_Click" Visible="false" />
                        </td>
                    </tr>
                </tbody>
            </table>
            </td>
            </tr>
        </tbody>
        </table>
        </asp:Panel>
    </div>
</asp:Content>
