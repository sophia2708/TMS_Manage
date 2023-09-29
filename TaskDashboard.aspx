<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="TaskDashboard.aspx.cs"  Inherits="Includes_WebForm_TaskDashboard"
    Title="TaskDashboard" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
     <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        window.history.forward(1);  
    </script>
    <style type="text/css">
        .hideid
        {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px;" align="center">
        <table border="0" width="750px" align="center" class="wpn_content">
            <tr>
                <td class="text-primary">
                    My Task:
                    <asp:CheckBox ID="chkmytask" runat="server" OnCheckedChanged="chkmytask_CheckedChanged"
                        AutoPostBack="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Include Completed Task:<asp:CheckBox ID="compbox"
                        runat="server" OnCheckedChanged="completedtask_CheckedChanged" AutoPostBack="true" />
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnltaskboard_Click">TaskMaster</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Header_style">
                    Task DashBoard
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" border="0" width="98%" class="table table-responsive" cellpadding="3"
                        cellspacing="2">
                        <tr>
                            <td width="500px">
                                <asp:GridView ID="grdTaskSummary" runat="server" CssClass="table table-bordered table-responsive"
                                    AutoGenerateColumns="False" Width="100%" CellSpacing="0" CellPadding="4" AllowSorting="True"
                                    OnSorting="grdTaskSummary_Sorting" OnRowEditing="grdTaskSummary_RowEditing" OnRowDataBound="grdTaskSummary_RowDataBound"
                                    GridLines="None">
                                    <%--<HeaderStyle BackColor="#507CD1" Font-Bold="True" />
                                    <AlternatingRowStyle BackColor="White" />--%>
                                    <Columns>
                                        <asp:BoundField DataField="id" ItemStyle-CssClass="hideid" HeaderStyle-CssClass="hideid">
                                            <HeaderStyle CssClass="hideid"></HeaderStyle>
                                            <ItemStyle CssClass="hideid"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Client">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtclient" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblClientid" runat="server" Text='<%# Bind("Clientid") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_selectedIndexChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblclient" runat="server" Text='<%# Bind("Client")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assigned By" SortExpression="AssignedBy">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtemployeeby" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblemployeeby" runat="server" Text='<%# Bind("AssignedTo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AssignedTo" SortExpression="AssignedTo">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtemployee" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblemployee" runat="server" Text='<%# Bind("AssignedBy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ProjectId") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AppendDataBoundItems="true"
                                                    DataValueField="ProjectId" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblModule" runat="server" Text='<%# Bind("ModuleId") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlModuleList" runat="server" DataTextField="ModuleName" DataValueField="ModuleId"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged"
                                                    AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModule" runat="server" Text='<%# Bind("ModuleName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Task">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblTask" runat="server" Text='<%# Bind("TaskId") %>' Visible="false"></asp:Label>
                                                <%-- <asp:DropDownList ID="ddlTask" runat="server">
                                                </asp:DropDownList>--%>
                                                <asp:DropDownList ID="ddlTask" runat="server" DataTextField="TaskName" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"
                                                    AutoPostBack="true" DataValueField="TaskId" AppendDataBoundItems="true" Width="100px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTask" runat="server" Text='<%# Bind("TaskName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priority" SortExpression="TaskPriority">
                                            <EditItemTemplate>
                                                <%--<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlPriority" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# Bind("TaskPriority") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" SortExpression="TaskStatus">
                                            <EditItemTemplate>
                                                <%--    <asp:Label ID="lblStatus" Width="70px" runat="server" Text='<%# Bind("TypeOptionID") %>'></asp:Label>--%>
                                                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" Width="70px" runat="server" Text='<%# Bind("TaskStatus") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expected Completed Date">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblExpCompDate" runat="server" Text='<%# Bind("ExpCompDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--   <asp:TemplateField HeaderText="Button" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" 
                                                    CommandName="" Text="Button" OnClick="LinkButton2_click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="FTR">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlftr" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="N" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="PV" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="NA" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OTD">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DropDownList1" runat="server">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="N" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblftr" runat="server" Text='<%# Bind("OTD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit/Update" ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                                    OnClick="LinkButton1_Click" Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Create Sub-Task">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtntask" runat="server" CommandName="fetchdetails" OnClick="lnkbtntask_Click">Re-Work</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                                    <HeaderStyle BackColor="#428bca" />
                                    <%--<EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="cambria" ForeColor="White" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                                    <RowStyle Font-Names="Calibri" BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                                </asp:GridView>
                                <asp:GridView ID="grdtaskmaster" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" HeaderStyle-BackColor="#F4A460" HeaderStyle-ForeColor="Black"
                                    GridLines="None">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Client">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblClientid" runat="server" Text='<%#Bind("Clientid") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_selectedIndexChanged"
                                                    AutoPostBack="true" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblclien" runat="server" Text='<%#Bind("ClientShortCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Bind("ProjectId")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AppendDataBoundItems="true"
                                                    DataValueField="ProjectId" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblclient" runat="server" Text='<%# Bind("ProjectName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblModule" runat="server" Text='<%#Bind("ModuleId")%>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlModuleList" runat="server" DataTextField="ModuleName" DataValueField="ModuleId"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged"
                                                    AppendDataBoundItems="true" Width="100px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblModule" runat="server" Text='<%# Bind("ModuleName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TaskName">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblTask" runat="server" Text='<%#Bind("TaskId") %>' Visible="false"></asp:Label>
                                                <asp:DropDownList ID="ddlTask" runat="server" DataTextField="TaskName" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"
                                                    AutoPostBack="true" DataValueField="TaskId" AppendDataBoundItems="true" Width="100px">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTask" runat="server" Text='<%#Bind("TaskName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Priority">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%#Bind("Status")%>'></asp:Label>
                                                <asp:DropDownList ID="ddlPriority" runat="server" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%#Bind("TypeName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Planned Start Date">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtplannedstartdate" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblplannedstartdate" runat="server" Text='<%#Bind("PlannedStartDate","{0:d}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Planned End Date">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtplannedenddate" runat="server" Text=''></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblplannedenddate" runat="server" Text='<%#Bind("PlannedendDate","{0:d}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit/Update" ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkupdate" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Update"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="lnkcancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkedit" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" OnClick="lnkedit_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#3E3E3E" Font-Bold="True" Font-Names="cambria" ForeColor="White" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#2461BF" />
                                    <RowStyle Font-Names="Calibri" BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </td>
                            <td width="100px" valign="top">
                                <div>
                                    <b>Total Hours For The Month</b>
                                    <table width="150px" frame="box" border="3">
                                        <tr align="left" class="bg-danger">
                                            <td width="200px">
                                                Day
                                            </td>
                                            <td width="50px">
                                                <asp:Label ID="lblForDay" runat="server" Width="50px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td width="200px">
                                                Week
                                            </td>
                                            <td width="50px">
                                                <asp:Label ID="lblForWeek" runat="server" Width="50px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr align="left" class="bg-danger">
                                            <td width="200px">
                                                Month
                                            </td>
                                            <td width="50px">
                                                <asp:Label ID="lblForMonth" runat="server" Width="50px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <b>Total Hours For The Month</b>
                                    <center>
                                        <b>-Client Wise</b></center>
                                    <asp:GridView ID="clientgrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="clientgrd_RowDataBound"
                                        Width="150px" frame="box" border="3" CellPadding="2" CellSpacing="0">
                                        <%--   OnRowDataBound="clientgrd_RowDataBound"--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="bg-danger">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrow" runat="server" Text='<%# Bind("Sno")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Client" HeaderStyle-CssClass="bg-danger">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclient" runat="server" Text='<%# Bind("clients")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="bg-danger">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhour" runat="server" Text='<%# Bind("Hours")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
