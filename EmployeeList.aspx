<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="EmployeeList.aspx.cs" Inherits="EmployeeList" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <p>
    </p>
    <asp:GridView ID="grdEmployeeList" runat="server" BackColor="LightGoldenrodYellow"
        BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"
        Height="122px" Width="151px" AutoGenerateColumns="False" OnRowDeleting="grdEmployeeList_RowDeleting"
        OnRowEditing="grdEmployeeList_RowEditing" 
        OnRowCreated="grdEmployeeList_RowCreated" 
        onrowcommand="grdEmployeeList_RowCommand" 
        onrowdatabound="grdEmployeeList_RowDataBound" 
        onselectedindexchanged="grdEmployeeList_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField HeaderText="Empid" SortExpression="Empid" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblEmpid" runat="server" Text='<%# Bind("Empid") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FirstName" SortExpression="FirstName" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active" ItemStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:CheckBox ID="chkActive" CommandName="Change" runat="server" Checked='<%#Eval("Active").ToString()=="0"?true:false %>' 
                        AutoPostBack="true" />
                </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
            <asp:CommandField HeaderText="Status" ShowSelectButton="True" SelectText="" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>
    
</asp:Content>
