<%@ Page Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="ViewEditTimesheet.aspx.cs" Inherits="Includes_WebForm_ViewEditTimesheet"
    Title="View or Edit Timesheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Javascript/Calendar.js" type="text/javascript"></script>
    <link href="../CSS/Calendar.css" rel="stylesheet" type="text/css" />
    <script src="../bootstrap-3.3.6/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    <script src="https://igniteui.com/js/external/FileSaver.js"></script>
    <script src="https://igniteui.com/js/external/Blob.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/infragistics.core.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_core.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_collections.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_text.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_io.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_ui.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.documents.core_core.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_collectionsextended.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.excel_core.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_threading.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.ext_web.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.xml.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.documents.core_openxml.js"></script>
    <script src="https://cdn-na.infragistics.com/igniteui/latest/js/modules/infragistics.excel_serialization_openxml.js"></script>
    <script type="text/javascript">
        $(document).mouseup(function (e) {
            var container = $("#CalendarControl");
            if (!container.is(e.target) && container.has(e.target).length === 0)
                hideCalendarControl()
        });

        function test(test) {
            //debugger;
            var FromDate = document.getElementById('<%=txtFromdate.ClientID%>').value;
            var ToDate = document.getElementById('<%=txtTodate.ClientID%>').value;
            alert(FromDate + ToDate);
        }
        window.history.forward(1);
        //var daa = [{"Expected Completion date": "2023-03-01","Hours": 8,"Issues": "","ModuleId": 6,"ModuleName": "Time sheet","Object": "","ProjectId": 3,"ProjectName": "Time Sheet","Row": 1,"Status": "In Progress","StatusId": 2,"TaskDate": "07-02-2022","TaskDescription": "Working on the SQL side for getting the drop down values and adding on multi select drop down.","TaskId": 96014,"TaskName": "leave compensation","id": 130307},{},{}]
       var TimeSheetDatas = [
        {
          ExpectedCompletiondate: "2023-03-01",
          Hours: 10,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "07-02-2022",
          TaskDescription: "Working on the SQL side ",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
        {
          ExpectedCompletiondate: "2023-03-02",
          Hours: 10,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "08-02-2022",
          TaskDescription:
            "getting the drop down values and adding on multi select drop down.",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
        {
          ExpectedCompletiondate: "2023-03-03",
          Hours: 10,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "09-02-2022",
          TaskDescription: "Working on multi select drop down.",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
        {
          ExpectedCompletiondate: "2023-03-04",
          Hours: 10,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "10-02-2022",
          TaskDescription: "adding on multi select drop down.",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
        {
          ExpectedCompletiondate: "2023-03-05",
          Hours: 10,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "11-02-2022",
          TaskDescription: "SQL side for getting the drop down values",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
        {
          ExpectedCompletiondate: "2023-03-06",
          Hours: 10,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "12-02-2022",
          TaskDescription:
            "SQL side for getting the drop down values and adding on multi select drop down.",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
        {
          ExpectedCompletiondate: "2023-03-06",
          Hours: 111,
          Issues: "",
          ModuleId: 6,
          ModuleName: "Time sheet",
          Object: "",
          ProjectId: 3,
          ProjectName: "Time Sheet",
          Row: 1,
          Status: "In Progress",
          StatusId: 2,
          TaskDate: "12-02-2022",
          TaskDescription:
            "SQL side for getting the drop down values and adding on multi select drop down.",
          TaskId: 96014,
          TaskName: "leave compensation",
          id: 130307,
        },
      ];
      //createFormattingWorkbook()
      function createFormattingWorkbook() {
        var workbook = new $.ig.excel.Workbook(
          $.ig.excel.WorkbookFormat.excel2007
        );
        var SheetOne = workbook.worksheets().add("Effort Metrics");
        var ReportDates = new Date();
        var noTime = new Date(
          ReportDates.getFullYear(),
          ReportDates.getMonth(),
          ReportDates.getDate()
        );
        SheetOne.rows(6).setCellValue(8, "Report Date");
        SheetOne.rows(6).setCellValue(9, noTime);
        SheetOne.rows(6).cells(8).cellFormat().font().bold(true);
        SheetOne.rows(6).cells(9).cellFormat().font().bold(true);
        SheetOne.rows(6)
          .cells(9)
          .cellFormat()
          .alignment($.ig.excel.HorizontalCellAlignment.left);
        //Row 2
        SheetOne.mergedCellsRegions().add(7, 5, 7, 9);
        SheetOne.rows(7).setCellValue(
          5,
          // "Summary of Effort Metrics for " + "FileName".replace("_", "-")
          "Summary of Effort Metrics for Current Month"
        );

        SheetOne.rows(6).cells(5).cellFormat().font().bold(true);
        SheetOne.rows(7).cells(5).cellFormat().bottomBorderStyle(1);
        SheetOne.rows(7).cells(5).cellFormat().leftBorderStyle(1);
        SheetOne.rows(7).cells(5).cellFormat().topBorderStyle(1);
        SheetOne.rows(7).cells(5).cellFormat().rightBorderStyle(1);
        SheetOne.rows(6).cells(9).cellFormat().font();

        var borderRow = [7, 8, 9, 10, 12, 13];
        var borderCell = [5, 6, 7, 8, 9];
        for (let row = 0; row < borderRow.length; row++) {
          for (let cell = 0; cell < borderCell.length; cell++) {
            SheetOne.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .bottomBorderStyle(1);
            SheetOne.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .leftBorderStyle(1);
            SheetOne.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .topBorderStyle(1);
            SheetOne.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .rightBorderStyle(1);

            if (borderRow[row] == 7 || borderRow[row] == 12) {
              SheetOne.rows(borderRow[row])
                .cells(borderCell[cell])
                .cellFormat()
                .fill($.ig.excel.CellFill.createSolidFill("#0077b6"));
              SheetOne.rows(borderRow[row])
                .cells(borderCell[cell])
                .cellFormat()
                .font()
                .colorInfo(
                  new $.ig.excel.WorkbookColorInfo(
                    $.ig.excel.WorkbookThemeColorType.light1
                  )
                );
            }
            if (
              borderRow[row] == 8 ||
              borderRow[row] == 9 ||
              borderRow[row] == 10 ||
              borderRow[row] == 13
            ) {
              SheetOne.rows(borderRow[row])
                .cells(borderCell[cell])
                .cellFormat()
                .fill($.ig.excel.CellFill.createSolidFill("#dbe5f1"));
            }

            if (borderRow[row] == 7) {
              SheetOne.rows(borderRow[row])
                .cells(borderCell[cell])
                .cellFormat()
                .alignment($.ig.excel.HorizontalCellAlignment.center);
            }
          }
        }
        SheetOne.rows(8).setCellValue(5, "No of Billable Days");
        SheetOne.rows(8).setCellValue(6, 21);
        SheetOne.rows(8).setCellValue(7, "No of Billable Hours");
        SheetOne.rows(8).cells(8).applyFormula("=G9*8");

        //Row 4
        SheetOne.rows(9).setCellValue(5, "MTD Days Passed");
        SheetOne.rows(9).setCellValue(
          6,
          21
          //parseInt(WorkingDaysCount) - parseInt(detailsRow[0]["EmpLeaveCount"])
        );
        SheetOne.rows(9).setCellValue(7, "MTD Expected Hours");
        SheetOne.rows(9).cells(8).applyFormula("=G10*8");

        //Row 5
        SheetOne.rows(10).setCellValue(5, "No. Of Leaves");
        SheetOne.rows(10).setCellValue(
          6,
          1
          //parseInt(detailsRow[0]["EmpLeaveCount"])
        );
        SheetOne.rows(10).setCellValue(7, "Min MTD Expected Hours");
        SheetOne.rows(10).cells(8).applyFormula("=(G10-G11)*8");

        const SheetOneColumns = [
          "Emp Id",
          "Emp Name",
          "Effort Metrics Hours",
          "Deficit/Surplus as of Date",
          "Deficit for the Month",
        ];

        for (let col = 0; col < SheetOneColumns.length; col++) {
          SheetOne.columns(col + 5).setWidth(165, 3);
          SheetOne.rows(12).setCellValue(col + 5, SheetOneColumns[col]);
          //   SheetOne.rows(12).cells(col + 5).cellFormat.fill =
          //     CellFill.createSolidFill("#387dc2");

          //   SheetOne.rows(12).cells(col + 5).cellFormat.font.colorInfo =
          //     new WorkbookColorInfo(color);
        }

        // const SheetRow = [
        //   parseInt(detailsRow[0]["EmpId"]),
        //   detailsRow[0]["FirstName"] + " " + detailsRow[0]["LastName"],
        //   "=(" + detailsRow[0]["FirstName"] + "!D1)",
        //   "=$H$14-$I$10",
        //   "=$H$14-$I$9",
        // ];
        SheetRow = [
          84,
          "Test Case",
          "=(Sophia!D1)",
          "=$H$14-$I$10",
          "=$H$14-$I$9",
        ];

        for (let col = 0; col < SheetRow.length; col++) {
          if (col <= 2) {
            SheetOne.columns(col + 5).width = 5000;
            SheetOne.rows(13).setCellValue(col + 5, SheetRow[col]);
          } else if (col !== 2) {
            SheetOne.rows(13)
              .cells(col + 5)
              .applyFormula(SheetRow[col]);
          }
        }
        var SheetTwo = workbook.worksheets().add("Sophia");

        SheetTwo.mergedCellsRegions().add(0, 0, 0, 2);
        SheetTwo.rows(0).setCellValue(
          0,
          "Effort Metrics Reported Through the Email"
        );
        SheetTwo.rows(0).cells(0).cellFormat().font().bold(true);
        SheetTwo.rows(0).cells(3).cellFormat().font().bold(true);
        SheetTwo.rows(0)
          .cells(3)
          .applyFormula("=SUM(G4:G" + (TimeSheetDatas.length + 3) + ")");
        const SheetTwoColumns = [
          "Date",
          "Status",
          "Project",
          "Description",
          "Actual / Estimated completion Date",
          "Objects Changed",
          "Hours",
        ];
        for (let col = 0; col < SheetTwoColumns.length; col++) {
          SheetTwo.columns(col + 1).setWidth(150, 3);
          SheetTwo.rows(2).setCellValue(col + 0, SheetTwoColumns[col]);
        }
        SheetTwo.rows(2).cells(0).cellFormat().bottomBorderStyle(1);
        SheetTwo.rows(2).cells(0).cellFormat().leftBorderStyle(1);
        SheetTwo.rows(2).cells(0).cellFormat().topBorderStyle(1);
        SheetTwo.rows(2).cells(0).cellFormat().rightBorderStyle(1);
        var borderRow = [2, 3, 4, 5, 6, 7, 8, 9, 10, 11];
        var borderCell = [0, 1, 2, 3, 4, 5, 6];
        for (let row = 0; row < borderRow.length; row++) {
          for (let cell = 0; cell < borderCell.length; cell++) {
            SheetTwo.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .bottomBorderStyle(1);
            SheetTwo.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .leftBorderStyle(1);
            SheetTwo.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .topBorderStyle(1);
            SheetTwo.rows(borderRow[row])
              .cells(borderCell[cell])
              .cellFormat()
              .rightBorderStyle(1);

            if (borderRow[row] == 2) {
              SheetTwo.rows(borderRow[row])
                .cells(borderCell[cell])
                .cellFormat()
                .fill($.ig.excel.CellFill.createSolidFill("#0077b6"));
              SheetTwo.rows(borderRow[row])
                .cells(borderCell[cell])
                .cellFormat()
                .font()
                .colorInfo(
                  new $.ig.excel.WorkbookColorInfo(
                    $.ig.excel.WorkbookThemeColorType.light1
                  )
                );
            }
          }
        }
        for (let col = 0; col < TimeSheetDatas.length; col++) {
          let rowData = TimeSheetDatas[col];
          let i = col + 4;
          SheetTwo.getCell("A" + i).value(rowData["TaskDate"]);
          SheetTwo.getCell("B" + i).value(rowData["Status"]);
          SheetTwo.getCell("C" + i).value(rowData["ProjectName"]);
          SheetTwo.getCell("D" + i).value(rowData["TaskDescription"]);
          SheetTwo.getCell("D" + i)
            .cellFormat()
            .wrapText(true);
          SheetTwo.getCell("E" + i).value(
            rowData["Actual / Estimated completion Date"]
          );
          SheetTwo.getCell("F" + i).value(rowData["Object"]);
          SheetTwo.getCell("G" + i).value(rowData["Hours"]);
        }

        try {
          SheetOne.rows(13).cells(7).applyFormula(SheetRow[2]);
          SheetOne.rows(13).cells(7).cellFormat().font().bold(true);
        } catch (error) {
          alert(error);
        }
        saveWorkbook(workbook, "Formatting.xlsx");
      }

      function saveWorkbook(workbook, name) {
        workbook.save(
          { type: "blob" },
          function (data) {
            saveAs(data, name);
          },
          function (error) {
            alert("Error exporting: : " + error);
          }
        );
      }
      //this.workbookParse(wb);
      //this.workbookSave(FileName);
    </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <div style="padding-top: 30px;" align="center">
        <table border="0" width="750px" align="center" class="table table-responsive wpn_content">
            <tr>
                <td colspan="2" class="Header_style">
                    View TimeSheet
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" border="0" width="80%" cellpadding="3" cellspacing="2">
                        <tr>
                            <td align="center">
                                <asp:RadioButton ID="rbtnBydate" runat="server" Text="By Date" OnCheckedChanged="Bydate_CheckedChanged"
                                    AutoPostBack="True" GroupName="gridview"></asp:RadioButton>
                                <asp:RadioButton ID="rbtnByRange" runat="server" Text="By Range" AutoPostBack="True"
                                    GroupName="gridview" OnCheckedChanged="rbtnByRange_CheckedChanged"></asp:RadioButton>
                                <asp:RadioButton ID="rbtnBymonth" runat="server" Text="By Month" OnCheckedChanged="Bymonth_CheckedChanged1"
                                    AutoPostBack="True" GroupName="gridview"></asp:RadioButton>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlday" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Select Date:
                                            </td>
                                            <td class="td_input_Style">
                                                <asp:TextBox ID="txtdate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox>
                                                <a href="#" onclick="showCalendarControl('<%=txtdate.ClientID%>','NoFuture')">
                                                    <img id="imgDOR" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif"
                                                        style="border: 0; vertical-align: middle;" /></a>
                                                <asp:RequiredFieldValidator ID="rfvTaskDate" ControlToValidate="txtdate" runat="server"
                                                    Font-Size="X-Small" ValidationGroup="ViewTimesheet" ErrorMessage="Enter task date">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="pnlRange" runat="server">
                                    <table>
                                        <tr>
                                            <td width="100px">
                                                Select Range:
                                            </td>
                                            <td>
                                                From:<asp:TextBox ID="txtFromdate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtFromdate.ClientID%>','NoFuture')"><img
                                                        id="img1" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                            <td>
                                                To:<asp:TextBox ID="txtTodate" runat="server" Width="110px" ReadOnly="true"></asp:TextBox><a
                                                    href="#" onclick="showCalendarControl('<%=txtTodate.ClientID%>','NoFuture')"><img
                                                        id="img2" runat="Server" alt="Calendar" src="../Images/PopupCalendar.gif" style="border: 0;
                                                        vertical-align: middle;" /></a>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Panel ID="PnlMonth" runat="server">
                                    <table>
                                        <tr>
                                            <td width="120px">
                                                Select Month:
                                            </td>
                                            <td class="td_input_Style">
                                                <asp:DropDownList ID="ddlmonth" runat="server" Width="120px">
                                                    <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvMonth" ControlToValidate="ddlmonth" InitialValue="0"
                                                    runat="server" Font-Size="X-Small" ValidationGroup="ViewTimesheet" ErrorMessage="Select the month">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td width="100px">
                                                Select Year:
                                            </td>
                                            <td class="td_input_Style">
                                                <asp:DropDownList ID="ddlyear" runat="server" Width="120px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvYear" ControlToValidate="ddlyear" InitialValue="--Select--"
                                                    runat="server" Font-Size="X-Small" ValidationGroup="ViewTimesheet" ErrorMessage="Select the Year">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnview1" runat="server" Text="View" CssClass="btn btn-primary" ValidationGroup="ViewTimesheet"
                                    OnClick="btnview_Click" />
                                <asp:Button ID="btnWeeklyStatus" runat="server" Text="View" ValidationGroup="ViewTimesheet"
                                    OnClick="btnWeeklyStatus_Click" CssClass="btn btn-primary" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       <%-- <asp:Button ID="exportButton" runat="server" CssClass="btn btn-primary" OnClick="btndownload_Click"
            Text="Download" />--%>
        <br />
        <asp:Panel ID="pnlgridview" runat="server">
            <%--<table border="0" width="750px" align="center" class="wpn_content">
                <tr>
                    <td>--%>
            <div class="table table-responsive">
                <asp:GridView ID="grdTimesheet" CssClass="table table-bordered table-responsive"
                    runat="server" Width="100%" CellSpacing="0" CellPadding="4" ForeColor="#333333"
                    ShowFooter="True" GridLines="None" OnRowCancelingEdit="grdTimesheet_RowCancelingEdit"
                    OnRowDeleting="grdTimesheet_RowDeleting" OnRowEditing="grdTimesheet_RowEditing"
                    OnRowUpdating="grdTimesheet_RowUpdating" AutoGenerateColumns="False" OnRowDataBound="grdTimesheet_RowDataBound"
                    Font-Size="X-Small">
                    <Columns>
                        <asp:TemplateField HeaderText="RowID" Visible="False" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblRowID" runat="server" Text='<%#Bind("id")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblRowID" runat="server" Text='<%#Bind("id")%>'>
                                </asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TaskDate" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Width="80px" Text='<%#Bind("TaskDate")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTaskDate" runat="server" Width="80px" Text='<%#Bind("TaskDate")%>'></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblProject" runat="server" Width="100px" Text='<%# Bind("ProjectName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblProjectEdit" Visible="false" runat="server" Text='<%# Bind("ProjectId")%>'></asp:Label>
                                <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlProject_SelectedIndexChanged1" AppendDataBoundItems="true"
                                    DataValueField="ProjectId" Width="100px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Module" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblModule" runat="server" Width="100px" Text='<%# Bind("ModuleName")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblModuleEdit" Visible="false" runat="server" Text='<%# Bind("ModuleId") %>'></asp:Label>
                                <asp:DropDownList ID="ddlModuleList" runat="server" DataTextField="ModuleName" DataValueField="ModuleId"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlModuleList_SelectedIndexChanged1"
                                    AppendDataBoundItems="true" Width="100px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Task" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblTask" runat="server" Width="100px" Text='<%# Bind("TaskName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblTaskEdit" runat="server" Visible="false" Text='<%# Bind("TaskId") %>'></asp:Label>
                                <asp:DropDownList ID="ddlTask" runat="server" DataTextField="TaskName" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged"
                                    AutoPostBack="true" DataValueField="TaskId" AppendDataBoundItems="true" Width="100px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Task Description" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Width="250px" Text='<%# Bind("TaskDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescription" runat="server" Width="250px" Height="70px" Text='<%# Bind("TaskDescription") %>'
                                    TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Issues" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblIssues" runat="server" Width="100px" Text='<%# Bind("Issues") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIssues" runat="server" Width="100px" Text='<%# Bind("Issues") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Object" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblObject" runat="server" Width="100px" Text='<%# Bind("Object") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtObject" runat="server" Width="100px" Text='<%# Bind("Object")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbltxttotal" runat="server" Text="Total Hours" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>' Width="100px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblStatusEdit" runat="server" Text='<%# Bind("StatusId") %>' Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="90px">
                                </asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="rfvStatus" ControlToValidate="ddlStatus" runat="server" 
                            SetFocusOnError="true" ValidationGroup="test" ErrorMessage="Select a Status "
                            Font-Size="XX-Small" InitialValue="0"></asp:RequiredFieldValidator>--%>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" FooterStyle-CssClass="danger">
                            <ItemTemplate>
                                <asp:Label ID="lblHours" runat="server" Width="40px" Text='<%# Bind("Hours") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHours" runat="server" Width="50px" MaxLength="5" Text='<%# Bind("Hours") %>'
                                    onkeypress="return isNumberKey(event)"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="rfvHours" ControlToValidate="txtHours" runat="server"
                                            SetFocusOnError="true" ValidationGroup="test" ErrorMessage="Enter hours" Font-Size="XX-Small"></asp:RequiredFieldValidator>--%>
                                <asp:RangeValidator ID="HoursValidator" runat="server" Font-Size="XX-Small" ValidationGroup="test"
                                    ErrorMessage="Hours exceeded" ControlToValidate="txtHours" MaximumValue="23.59"
                                    MinimumValue="0" Type="Double"></asp:RangeValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ValidationGroup="test" ShowEditButton="True" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                    </Columns>
                    <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
                    <HeaderStyle BackColor="#428bca" />
                    <%--         <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
                </asp:GridView>
            </div>
            <%-- </td>
                </tr>
                <tr>--%>
            <%--<td align="center">--%>
            <asp:Button ID="btnSendmail" runat="server" class="btn btn-primary" Text="Send Mail"
                ValidationGroup="test" OnClick="btnSendmail_Click" />
            <asp:Button ID="btnExportToExcel" runat="server" class="btn btn-primary" Text="ExportToExcel"
                OnClick="btnExportExcel_Click" />
            <%--</td>--%>
            <%-- </tr>
            </table>--%>
            <asp:HiddenField runat="server" ID="tableData"/>
        </asp:Panel>
    </div>
</asp:Content>
