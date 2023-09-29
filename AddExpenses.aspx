<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="AddExpenses.aspx.cs" Inherits="Includes_WebForm_AddExpenses" Title="ExpensesRecords" %>

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
       
        function validateexpensedate() {
            var grid = document.getElementById('<%=txtchoosedate.ClientID%>');
                            if (grid.value == "") {
                            alert("Select a date");
                        }
                        else {
                            return "";
                        }
                    }                
    </script>
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server" >
   <div style="padding-top: 30px;" align="center">
   <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
        <table border="0" width="750px" align="center" class="wpn_content">
       <tbody>
            <tr>
                <td colspan="2" class="Header_style" >
                   Expenses Entry
                </td>
            </tr>
            <tr>
            <td>
            <table align="center" border="0" cellpadding="3" cellspacing="2" 
                class="stylized" width="80%">                
                <tbody>
                <tr>
                        <td class="td_smalllabel_Style">
                            Select a date:
                        </td>
                         <td class="td_input_Style">
                         <%--<asp:HiddenField ID="hifdid" runat="server" Value='<%#Bind("Id")%>' />    --%>       
                                <asp:TextBox ID="txtchoosedate" runat="server" Width="147px" ToolTip="dd-mm-yyyy" ></asp:TextBox>
                            <a onclick="showCalendarControl('<%=txtchoosedate.ClientID%>','NoFuture')">    
                            <img ID="img1" runat="Server" alt="Calendar" 
                                src="../Images/PopupCalendar.gif" style="border:0; vertical-align:middle;" /></a>
                            <asp:RequiredFieldValidator ID="reqfvtxtchoosedate" runat="server" 
                                ControlToValidate="txtchoosedate" EnableClientScript="true" 
                                ErrorMessage="Select your Date" Font-Size="XX-Small" 
                                SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>

                        </td>
                        <td>
                                    <asp:Button Text="Go" ID="btnGo" runat="server" CssClass="btn btn-sm btn-primary" OnClientClick="return validateexpensedate();" OnClick="btnGetExpenseHistory_Click"
                                        />
                                </td>
                   </tr> 
                   <tr>
                        <td class="td_smalllabel_Style">
                           TimeZone
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txttimezone" runat="server" 
                                onkeypress="return onlyAlphabets(event,this);" Width="147px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtExpenseType" ErrorMessage="Enter the Type" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                           Product
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtExpenseType" runat="server" 
                                onkeypress="return onlyAlphabets(event,this);" Width="147px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvExpenseType" runat="server" 
                                ControlToValidate="txtExpenseType" ErrorMessage="Enter the Type" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                           Enter Amount:
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" 
                                onkeypress="return isNumberKey(event)" Width="147px" 
                                ontextchanged="txtAmount_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" 
                                ControlToValidate="txtAmount" ErrorMessage="Enter the Amount" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator ID="regAmount" ErrorMessage="Enter number"  runat="server" Font-Size="XX-Small" ValidationExpression="^[1-9][0-9]{9}$" ValidationGroup="Registration" ControlToValidate="txtAmount"></asp:RegularExpressionValidator>  --%>       
                        </td>
                    </tr>
                      <tr>
                        <td class="td_smalllabel_Style">
                            Paid By
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtPaidPersonName" onkeypress="return onlyAlphabets(event,this);" 
                                runat="server" Width="147px" ontextchanged="txtPaidPersonName_TextChanged"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvtxtPaidPersonName" runat="server" 
                                ControlToValidate="txtPaidPersonName" ErrorMessage="Enter Name" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                            Mode
                        </td>
                        <td class="td_input_Style">
                            <asp:TextBox ID="txtmode" onkeypress="return onlyAlphabets(event,this);" 
                                runat="server" Width="147px" ontextchanged="txtPaidPersonName_TextChanged"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtmode" ErrorMessage="Enter Name" 
                                Font-Size="XX-Small" SetFocusOnError="true" ValidationGroup="Registration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_smalllabel_Style">
                        </td>
                        <td class="td_input_Style">
                            <asp:Button ID="btnSave" runat="server" CssClass="button_Style" OnClick="btnSave_Click"
                                 Text="Save" ValidationGroup="Registration" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="button_Style" onclick="btnCancel_Click"
                                Text="Cancel" />
                                <asp:Button ID="btnBack" runat="server" CssClass="button_Style" 
                                 Text="Back" Visible="false" />
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
    <div style="padding-top: 30px;" align="center">
    <asp:Panel ID="pnlgridviewExpenseHistory" runat="server">
    <asp:GridView ID="grdExpenseHistory" runat="server"
                CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
                UseAccessibleHeader="true" OnRowCommand="grdExpenseHistory_RowCommand" AutoGenerateColumns="false"
                OnPreRender="grdExpenseHistory_PreRender" CellSpacing="0" Width="50%" OnRowDataBound="grdExpenseHistory_RowDataBound"
                CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="grdExpenseHistory_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("Expensedate") %>'></asp:Label>
                             
                        </ItemTemplate>
                                                     
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Expense Type">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblExpenseType" runat="server" Text='<%# Bind("ExpenseType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblAmount" runat="server" Text=' <%# Bind("Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paid Name">
                        <ItemTemplate runat="server">
                            <asp:Label ID="lblPaidName" runat="server" Text='<%# Bind("PaidName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Edit" ItemStyle-ForeColor="#2E6DA4" ShowEditButton="true"
                        runat="server">
                    </asp:CommandField>
                    <asp:CommandField HeaderText="Delete" ItemStyle-ForeColor="#2E6DA4" ShowDeleteButton="true"
                        runat="server">
                    </asp:CommandField>
                </Columns>
                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                <HeaderStyle BackColor="#428bca" />
            </asp:GridView>
            </asp:Panel>
            </div>
            <%--<div id="LabelExpenseHistory" class="bg-danger row text-center" runat="server" visible="false">
                <asp:Label ID="idexhistory" runat="server" CssClass="label" Width="350px" Height="25px"
                    ForeColor="Black" Font-Size="Small">No History Records Found!</asp:Label>
            </div>--%>
</asp:Content>


