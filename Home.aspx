<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/ABMaster.Master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Includes_WebForm_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>HomePage</title>
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
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="../DataTables-1.10.10/media/js/dataTables.bootstrap.min.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/dataTables.responsive.min.js" type="text/javascript"></script>
    <script src="../DataTables-1.10.10/media/js/responsive.bootstrap.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="../Scripts/moment.min.js" type="text/javascript"></script>
    <script src="../bootstrap-3.3.6-dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Scripts/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/bootbox.min.js"></script>
    <script type="text/javascript">
        function showMessage(button) {
            var hidMessage = button.parentNode.querySelector('[id$="hidMessage"]');
            var modalMessage = document.getElementById("modalMessage");

            if (hidMessage && modalMessage) {
                var message = hidMessage.value;
                modalMessage.innerHTML = message;
                $("#myModalmessageshowing").modal(); 
            }
        }


        function MessagePopup() {
            $('#MessagePopup').modal({ backdrop: "static" });
            $('#MessagePopup').css("position", "absolute").css('display', 'block');
        }
        function mymodalclose() {

            $('#MessagePopup').modal('hide').css('display', 'none');
            $('#MessagePopup').fadeOut();

        }

        function Click_close() { $('#MessagePopup').modal('hide').css('display', 'none'); }

        function EnabledisableSend(obj) {
            var message = obj.value.length;

            if (message > 0) {

                document.getElementById('<%= btnsendmsg.ClientID %>').disabled = false;

            }
            else
                document.getElementById('<%= btnsendmsg.ClientID %>').disabled = true;
        }


        //        function openPrompt() {
        //            bootbox.prompt({
        //                buttons: {
        //                    confirm: {
        //                        label: "Send",
        //                        className: "btn-primary"
        //                    },
        //                    cancel: {
        //                        label: "Cancel",
        //                        className: "btn-secondary" 
        //                    }
        //                }, title: 'Enter Your Message',
        //                centerVertical: true,
        //                callback: function (result) {
        //                    //console.log(result);
        //                }
        //            });
        //            return false
        //        }
    </script>
    <style type="text/css">
        .my-label-style
        {
            font-size: Small; /* Adjust the font size as needed */
            background-color: #f2dede; /* Set the text color to white for better visibility on red background */
            padding: 5px; /* Optional: Add padding for better spacing */
        }
        /* CSS for the modal */
        .text-primary
        {
            font-size: 1.5em;
        }
        
        #MessagePopup
        {
            display: none;
            position: inherit;
            left: 29%;
            z-index: 10005 !important;
        }
        .modal-header-primary
        {
            color: #fff;
            padding: 9px 15px;
            border-bottom: 1px solid #eee;
            background-color: #428bca;
            -webkit-border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-radius-topleft: 5px;
            -moz-border-radius-topright: 5px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBasic" runat="Server">
    <h3 class="text-primary">
        Activity Today
    </h3>
    <div id="MessagePopup" class="mode fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="model-header modal-header-primary">
                    <h4 class="modal-title">
                        <%--LOP Label changing added by SOPHIA--%>
                        <b>Enter Your Message</b>
                        <%--<asp:Label ID="loplevtype" runat="server" Font-Bold="true"></asp:Label>--%>
                    </h4>
                </div>
                <div class="modal-body">
                    <form action="" class="form-horizontal">
                    <br />
                    <div class="form-group">
                        <label class="col-xs-3 control-label">
                            Enter Message</label>
                        <%--<label class="validation-error hide" id="fullNameValidationError">This field is required.</label>--%>
                        <div class="col-xs-5 input-group">
                            <asp:TextBox ID="Messagetaken" Width="240px" Height="72px" TextMode="MultiLine" CssClass="form-control"
                                placeholder="Enter Your Wish" runat="server" data-toggle="tooltip" data-placement="bottom"
                                onkeyup="EnabledisableSend(this)"></asp:TextBox>
                        </div>
                    </div>
                    <asp:HiddenField ID="hfdidmsg" runat="server" />
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" onclick="mymodalclose()">
                        Cancel</button>
                    <asp:Button ID="btnsendmsg" class="btn btn-primary" runat="server" Text="Send message"  index="commandArgument"
                        Enabled="false" OnClick="Activitymsg" />
                        <%--<asp:LinkButton ID="Lnkgreetsend" data-toggle="tooltip" data-placement="top" ToolTip="Click to Send"
                                runat="server" ForeColor="#0066CC" Font-Size="Medium" Text="Send" CommandArgument='<%# Bind("Empid") %>'
                                CommandName="Lnkgreetsend" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"
                                >Send</asp:LinkButton>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalmessageshowing" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="model-header modal-header-primary">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Message</h4>
            </div>
            <div class="modal-body">
                <p id="modalMessage"></p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>


    <asp:GridView ID="grdactivities" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
        UseAccessibleHeader="true" AutoGenerateColumns="false" OnRowCommand="Greeting_RowCommand"
        CellSpacing="0" Width="30%" CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Anniversary/Birthday List">
                <ItemTemplate>
                    <asp:Label ID="txtlist" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <div id="editorContainer">
                       <%-- <button type="button" id="btnOpenPrompt" runat="server" usesubmitbehavior="true"
                            validationgroup="test" class="btn btn-primary" commandname="Add" commandargument='<%# Bind("ToMessage") %>'
                            onclick="return MessagePopup();">
                            Send Message
                        </button>--%>
                          <asp:LinkButton ID="Lnkgreetsend" data-toggle="tooltip" data-placement="top" ToolTip="Click to Send"
                                runat="server" ForeColor="#0066CC" Font-Size="Medium" Text="Send" CommandArgument='<%# Bind("Empid") %>'
                                CommandName="Lnkgreetsend" onmouseover="this.style.color='#f49430'" onmouseout="this.style.color='#0066CC'"
                                >Send</asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
        <HeaderStyle BackColor="#428bca" />
    </asp:GridView>
    <asp:GridView ID="grdmsgreceiving" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
     UseAccessibleHeader="true" AutoGenerateColumns="false" CellSpacing="0" Width="30%"
     CellPadding="4" ForeColor="#333333">
     <AlternatingRowStyle BackColor="White" />
     <Columns>
         <asp:TemplateField HeaderText="Greetings message">
             <ItemTemplate>
                 <asp:Label ID="txtlists" runat="server" Text='<%# Bind("CombinedMessage") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="">
             <ItemTemplate>
                 <div id="editorContainer">
                     <button type="button" id="btndelete" runat="server" usesubmitbehavior="true" validationgroup="test"
                         CssClass="btn btn-sm btn-default" commandname="Add" onclick="showMessage(this);">
                         Click
                     </button>
                     <asp:HiddenField ID="hidMessage" runat="server" Value='<%# Eval("Message") %>' />
                 </div>
             </ItemTemplate>
         </asp:TemplateField>
     </Columns>
     <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
     <HeaderStyle BackColor="#428bca" />
 </asp:GridView>


    <div id="dailyrecord" runat="server" visible="false">
        <asp:Label ID="lblActivity" runat="server" CssClass="label" Width="160px" Height="25px"
            BackColor="#f2dede" ForeColor="Black" Font-Size="Small">No Activities Found!</asp:Label>
    </div>
    <%-- <div class="col-lg-12"  >--%>
    <%--<div class="Taskremindersecondgrid" id="Alert_remindercontainer">
                            </div>--%>
    <%-- <h3  class="text-primary">Task Reminder </h3>  --%>
    <div class="col-lg-12">
        <asp:Label ID="lblClientid" runat="server" Visible="false" CssClass="text-primary">Task Reminder</asp:Label>
        <asp:GridView ID="grdtaskreminderrecord" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
            UseAccessibleHeader="true" AutoGenerateColumns="false" CellSpacing="0" Width="30%"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Task Name">
                    <ItemTemplate runat="server">
                        <asp:Label ID="lblTaskName" runat="server" Text='<%# Bind("TaskName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate runat="server">
                        <asp:Label ID="lbltaskdate" runat="server" Text='<%# Bind("NextSendOutDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
            <HeaderStyle BackColor="#428bca" />
        </asp:GridView>
        </br>
        <asp:Label ID="nolabel" runat="server" CssClass="label" Width="180px" Height="25px"
            Font-Size="Small" Text="No Reminders Found!!!" Visible="false" BackColor="#f2dede"
            ForeColor="Black"></asp:Label>
    </div>
    </br></br></br>
    <h3 class="text-primary">
        Timesheet Data</h3>
    <asp:GridView ID="grdtimesheetdataentries" runat="server" CssClass="table table-striped table-bordered table-hover display gvdatatable dt-responsive nowrap"
        UseAccessibleHeader="true" AutoGenerateColumns="false" CellSpacing="0" Width="45%"
        CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Date">
                <ItemTemplate runat="server">
                    <asp:Label ID="lblmissdate" runat="server" Text='<%#Bind("MissingDate","{0:d}")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Button ID="btnapplytimesheet" class="btn btn-primary" runat="server" Visible="true"
                        UseSubmitBehavior="true" Text="Fill Timesheet" OnClick="Entertimesheet_Click" />
                    <%--<div id="editorContainer">
                           <button type="button" ID="btnaplytimesheet" runat="server"  UseSubmitBehavior="true"
                                ValidationGroup="test" class="btn btn-primary" onclick="Entertimesheet_Click">Enter Timesheet
                                </button>
                                </div>--%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Button ID="btnapplyleave" class="btn btn-primary" Visible="true" runat="server"
                        UseSubmitBehavior="true" Text="Apply Leave" OnClick="ApplyLeave_Click" />
                    <%--<div id="editorContainer">
                           <button type="button" ID="btnapplyleave" runat="server"  UseSubmitBehavior="true"
                                ValidationGroup="test" class="btn btn-primary" onclick="ApplyLeave_Click">Apply Leave
                                </button>
                                </div>--%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle Font-Bold="True" Font-Names="cambria" ForeColor="#ffffff" />
        <HeaderStyle BackColor="#428bca" />
    </asp:GridView>
    <asp:Label ID="NoRecordsLabel" runat="server" Font-Size="Small" Text="No Datas found!!!"
        CssClass="label" BackColor="#f2dede" ForeColor="Black" Visible="false"></asp:Label>
    <%--</div>    --%>
</asp:Content>
