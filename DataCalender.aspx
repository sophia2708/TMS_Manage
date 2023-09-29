<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DataCalender.aspx.cs" Inherits="Includes_WebForm_DataCalender" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" Runat="Server">--%>
<%--<asp:Calendar id="Calendar1" OnDayRender="CalendarDRender" runat="server" BorderWidth="1px" NextPrevFormat="FullMonth" BackColor="White" Width="350px"ForeColor="Black" Height="190px" Font-Size="9pt" Font-Names="Verdana" BorderColor="White">
<TodayDayStyle BackColor="#CCCCCC"></TodayDayStyle>
<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom"></NextPrevStyle>
<DayHeaderStyle Font-Size="8pt" Font-Bold="True"></DayHeaderStyle>
<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
<TitleStyle Font-Size="12pt" Font-Bold="True" BorderWidth="4px" ForeColor="#333399" BorderColor="Black" BackColor="White"></TitleStyle>
<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
</asp:Calendar>--%>
<asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
<asp:DataGrid id="DataGrid1" style="Z-INDEX: 102; LEFT: 23px; POSITION: absolute; TOP: 271px" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Visible="False"></asp:DataGrid>
<%--</asp:Content>--%>


