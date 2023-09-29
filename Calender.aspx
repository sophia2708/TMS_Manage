<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calender.aspx.cs" Inherits="Includes_WebForm_Calender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
  <%--  <script src="JQuery/jquery.tooltip.min.js" type="text/javascript"></script>--%>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" />
    <script type="text/javascript">
//        function InitializeToolTip() {
//            $(".Showtooltip").tooltip({
//                track: true,
//                delay: 0,
//                showURL: false,
//                fade: 150,
//                bodyHandler: function () {
//                    return $($(this).next().html());
//                },
//                showURL: false
//            });
//        }
    </script>
    <script type="text/javascript">
//        $(function () {
//            InitializeToolTip();
//        })
    </script>
    <style type="text/css">
        a
        {
            text-decoration: none;
        }
        a :hover
        {
            color: #F7100C;
        }
        
        #tooltip
        {
            width: 320px;
            height: 80px;
            background: silver;
            position: absolute;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }
        
        #tooltip h3, #tooltip div
        {
            margin: 0;
        }
        .Red
        {
            color:#ff6666;
        }
        
        .Yellow
        {
            color: Yellow;
        }
        
        .Silver
        {
            color: Silver;
        }
        .Overlap
        {
            color: #d8b511;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Calendar ID="Calendar1" OnDayRender="CalendarDRender" class="Showtooltip" runat="server"
            BorderWidth="1px" NextPrevFormat="FullMonth" BackColor="White" Width="414px"
            ForeColor="Black" Height="274px" Font-Size="9pt" Font-Names="Verdana" BorderColor="White">
            <DayHeaderStyle Font-Size="8pt" Font-Bold="True"></DayHeaderStyle>
            <OtherMonthDayStyle BackColor="white" />
              <WeekendDayStyle BackColor="White"/>
        <WeekendDayStyle ForeColor="Black"/>
            <SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
            <TitleStyle Font-Size="12pt" Font-Bold="True" BorderWidth="4px" ForeColor="#333399"
                BorderColor="Black" BackColor="White"></TitleStyle>
        </asp:Calendar>
        <asp:Label ID="MessageBox" runat="server" ></asp:Label>
    </div>
    <asp:DataGrid ID="DataGrid1" runat="server" Style="z-index: 102; left: 23px; position: absolute;
        top: 383px; height: 3px;" Font-Size="XX-Small" Font-Names="Verdana">
    </asp:DataGrid>
    <ul class="fa-ul">
        <li><i class="fa-li fa fa-square Red"></i>Approved</li>
        <li><i class="fa-li fa fa-square Silver"></i>Pending</li>
        <li><i class="fa-li fa fa-square Yellow"></i>On hold</li>
        <li><i class="fa-li fa fa-square Overlap"></i>More than one employee</li>
    </ul>
    </form>
</body>
</html>
