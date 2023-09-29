<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="LeaveEdit.aspx.cs" Inherits="Includes_WebForm_LeaveEdit" Title="LeaveEdits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>LeaveEdits</title>
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
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>').fadeOut(6000);
           
        }
   
    </script>
    <style type="text/css">
      
   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    
  <div class="container-fluid">
                        <div class="col-lg-4" style="padding-top: 30px;">
                            <asp:Label ID="lblSelectEmployeeName" Style="color: #2970AD; font-family: Cambria; font-weight: bolder;"
                                runat="server" Text="Select the Employee to View their Leave History"><span class="glyphicon glyphicon-user"></span> Select the Employee to Edit their Leave Balance</asp:Label>
                        </div>
                        <div class="col-lg-4" style="padding-top: 30px;" align="center">
                            <asp:DropDownList ID="ddempname" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                            <asp:Button ID="btngo" runat="server" UseSubmitBehavior="false" OnClick="btnGetEmpLeaveBalance_Click"
                                Text="GO" AutoPostBack="false" CssClass="btn btn-primary" />
                        </div>
                    </div>
                    </br></br></br>
<div class="table table-responsive">
                        <asp:GridView ID="grdEmpLeaveBalance" runat="server" CssClass="table table-striped table-bordered table-hover display nowrap"
                            CellSpacing="0" Width="30%" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnSelectedIndexChanged="grdEmpLeaveBalance_SelectedIndexChanged"
                           
                             ForeColor="#333333" ShowFooter="false">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>

                                <asp:TemplateField HeaderText="LeaveType" SortExpression="Leave Type"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:Label ID="lblLeaveTYpe" runat="server" Text='<%# Bind("Leavetype") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Opening Balance" SortExpression="Opening Balance"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblBeginingBalance" runat="server" Text='<%# Bind("LeaveBalance") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfBeginingBalance" runat="server" Value='<%# Bind("LeaveBalance") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Earned Leave" SortExpression="Earned Leave" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblEarnedLeave" runat="server" Text='<%# Bind("EarnedLeaveBlc") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfEarnedLeave" runat="server" Value='<%# Bind("EarnedLeaveBlc") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Availed/Approved" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="Availed">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblAvailed" runat="server" Text='<%# Bind("LeavesTaken") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfAvailed" runat="server" Value='<%# Bind("LeavesTaken") %>' />
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Balance" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="Current Balance">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblCurrentBalance" runat="server" Text='<%# Bind("Currentblc") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfCurrentBalance" runat="server" Value='<%# Bind("Currentblc") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Loss of Pay" ItemStyle-HorizontalAlign="Right" SortExpression="Lop">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblLOP" runat="server" Text='<%# Bind("LOP") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfLop" runat="server" Value='<%# Bind("LOP") %>' />
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                                                                                                                                                                               
                            </Columns>
                            <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                            <HeaderStyle BackColor="#428bca" />
                        </asp:GridView>
                    </div></br></br>
                     <div class="table table-responsive">
                        <asp:GridView ID="grdEmpLeavetransaction" runat="server" CssClass="table table-striped table-bordered table-hover display nowrap"
                            CellSpacing="0" Width="30%" AutoGenerateColumns="False" CellPadding="4" GridLines="None" 
                           
                             ForeColor="#333333" ShowFooter="false">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                   <asp:TemplateField HeaderText="BeginingBalance" SortExpression="Opening Balances"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblBeginingBalance" runat="server" Text='<%# Bind("BeginingBalance") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfBeginingBalance" runat="server" Value='<%# Bind("BeginingBalance") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblTotalBeginningbalance" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CurrentBalance" SortExpression="Earned Leave" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblCurrentBalance" runat="server" Text='<%# Bind("CurrentBalance") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfCurrentBalance" runat="server" Value='<%# Bind("CurrentBalance") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblCurrentBalance" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Availed" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="Availed">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblAvailed" runat="server" Text='<%# Bind("Availed") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfAvailed" runat="server" Value='<%# Bind("Availed") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lblTotalAvailed" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EarnedLeave" ItemStyle-HorizontalAlign="Right"
                                    SortExpression="EarnedLeave">
                                    <ItemTemplate runat="server">
                                        <asp:TextBox ID="lblEarnedLeave" runat="server" Text='<%# Bind("EarnedLeave") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hdfEarnedLeave" runat="server" Value='<%# Bind("EarnedLeave") %>' />
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div>
                                            <asp:Label ID="lbltotalEarnedLeave" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                </Columns>                            
                                <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                            <HeaderStyle BackColor="#428bca" />                            
                        </asp:GridView>
                     </div>
     <asp:HiddenField ID="hfdEmptransactId" runat="server" Value='<%#Bind("EmptransactId")%>' />
     <asp:Button runat="server" ID="btnExample" class="btn btn-primary" Text="Save" Visible="false" OnClick="btnLeaveEdit_Click" />
</asp:Content>
