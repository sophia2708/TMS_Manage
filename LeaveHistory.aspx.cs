using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
using AnalyticBrainsBL;
using System.Data.SqlClient;
using LeaveManagementBL;
using LeaveManagementDAL;
using LeaveManagementDO;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;


public partial class Includes_WebForm_LeaveHistory : System.Web.UI.Page
{
    LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    LeaveHistoryDO retresultdo = new LeaveHistoryDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public string HeaderLeaveName = string.Empty;
    string total;
    int GetCount = 0;
    decimal TotalBeginingBalance = 0;
    decimal TotalEarnedLeave = 0;
    decimal TotalAvailed = 0;
    decimal TotalCurrentbalance = 0;
    decimal TotalLOP = 0;
    //Decimal Noofdays = 0;
    public enum MessageType { Success, Error, Info, Warning };
    //LeaveHistoryDO objLeaveHistoryDO = new LeaveHistoryDO();
    //LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    SqlConnection mycn;
    SqlDataAdapter myda;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet dsSelDate;
    String strConn;
    DateTime today = DateTime.Today;
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    StringBuilder strApproved = new StringBuilder();
    StringBuilder strPending = new StringBuilder();
    StringBuilder strOnHold = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["FirstName"].ToString() != "")
                {

                }
                if (Request.QueryString["sessionid"] != null)
                {
                    sessionid = Request.QueryString["sessionid"].ToString();
                }

                if (Request.QueryString["Username"] != null)
                {
                    Username = Request.QueryString["Username"].ToString();
                    Session["Username"] = Username;

                }

                if (Request.QueryString["EmpId"] != null)
                {
                    EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
                    Session["EmpId"] = EmpId;
                    //if (EmpId == 31 || EmpId == 48) { datacal.Visible = true; } else { datacal.Visible = false; }
                }
                //System.Threading.Thread.Sleep(5000);
                GetLeaveHistory();
                getLeavebalance();
                getLeaveDecision();
                getRptmgr();
                GetEmpName();
                emplopcancel();
                Getcompensationholidaylist();   //ADDED BY SOPHIA

                pnlgridview.Visible = false;

                int IsSubmit = Convert.ToInt32(Session["IsSubmit"]);
                if (IsSubmit == 1)
                {                //System.Threading.Thread.Sleep(5000);
                    ShowMessage("Leave has been Applied successfully", MessageType.Info);

                }
                GetCount = CheckCredential(Session["Username"].ToString());
                if (GetCount > 0)
                {
                    datacal.Visible = true;
                    approvals.Visible = true;
                    EmpHistory.Visible = true;

                }
                else
                {
                    approvals.Visible = false;
                    datacal.Visible = false;
                    EmpHistory.Visible = false;
                    LOPRemovalRequest.Visible = false;
                    //grdLeaveDecision.UseAccessibleHeader  = false;
                }

                Session["IsSubmit"] = 0;

                txtstartdate.Attributes.Add("readonly", "readonly");
                txtEndDate.Attributes.Add("readonly", "readonly");
                txtNoofdays.Attributes.Add("readonly", "readonly");
                txtNoofdays.Attributes.Add("type", "text");
                lopstartdate.Attributes.Add("readonly", "readonly");
                lopenddate.Attributes.Add("readonly", "readonly");
                lopleavehrs.Attributes.Add("readonly", "readonly");
                loptimshthrs.Attributes.Add("readonly", "readonly");
                //UpnlLeaveDecision.Update();
            }
            //strConn = "Data Source=analytic10;uid=abuser;pwd=abuser10;Initial Catalog=Analyticbrains";
            strConn = ConfigurationManager.ConnectionStrings["ABConnectionString"].ToString();
            mycn = new SqlConnection(strConn);
            HolidayList();
            MorethanoneEMp();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("[LM_Data_calender]", mycn);
            //cmd.Parameters.Add(new SqlParameter("@startdate", today));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(ds);
        }
        catch (Exception ) 
        {
            Response.Redirect("Login.aspx");
        }
    }

    public object GetLeaveHistory()
    {
        try
        {

            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdleavehistory(ObjLeaveHistoryDO);

            grdLeaveHistory.DataBind();


            if (grdLeaveHistory.Rows.Count > 0)
            {

                TableCellCollection cells = grdLeaveHistory.HeaderRow.Cells;
                grdLeaveHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                // LabelHistory.Visible = false;

            }


            return ObjLeaveHistoryDO.Resultdtset.Tables[0];
        }
        catch (Exception e) { throw e; }
    }

    protected void grdLeaveHistory_PreRender(object sender, EventArgs e)
    {
        grdLeaveHistory.DataSource = GetLeaveHistory();
        //string SelectedStartDate = "";
        //HiddenField hfd = new HiddenField();
        //hfd = ((HiddenField)StartDate.FindControl("hdfAppliedOnCDate"));

        grdLeaveHistory.DataBind();
        if (grdLeaveHistory != null)
        {

            if (grdLeaveHistory.Rows.Count > 0)
            {


                //Replace the <td> with <th> and adds the scope attribute
                grdLeaveHistory.UseAccessibleHeader = true;

                if (grdLeaveHistory.HeaderRow != null)
                {
                    //Adds the <thead> and <tbody> elements required for DataTables to work
                    grdLeaveHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdLeaveHistory.HeaderRow != null)
                {
                    //Adds the <tfoot> element required for DataTables to work
                    grdLeaveHistory.FooterRow.TableSection = TableRowSection.TableFooter;
                }


            }
            else { LabelHistory.Visible = true; }

        }
    }

    public void getLeavebalance()
    {
        {

            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdLeavebalance(ObjLeaveHistoryDO);
            ViewState["Leavebalance"] = ObjLeaveHistoryDO.Resultdtset.Tables[0];
            grdLeaveBalance.DataSource = ViewState["Leavebalance"];
            grdLeaveBalance.DataBind();


            if (grdLeaveBalance.Rows.Count > 0)
            {
                TableCellCollection cells = grdLeaveBalance.HeaderRow.Cells;
                grdLeaveBalance.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }

    public object getLeaveDecision()
    {

        ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);

        ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdLeaveDecision(ObjLeaveHistoryDO);

        grdLeaveDecision.DataBind();
        //grdLeaveDecision.UseAccessibleHeader = true;

        if (grdLeaveDecision.Rows.Count > 0)
        {
            TableCellCollection cells = grdLeaveDecision.HeaderRow.Cells;
            grdLeaveDecision.HeaderRow.TableSection = TableRowSection.TableHeader;

            btnApprove.Visible = true;
            btnOnHold.Visible = true;
            btnReject.Visible = true;

        }
        //else
        //{
        //   // LabelDecision.Visible = true;
        //    btnApprove.Visible = false;
        //    btnOnHold.Visible = false;
        //    btnReject.Visible = false;

        //    //ShowMessage("No Records Found", MessageType.Info);
        //    //lblNoRecords.Text = "No Records Found";
        //    //lblNoRecords.Visible = true;
        //}

        return ObjLeaveHistoryDO.Resultdtset.Tables[0];

    }

    protected void grdLeaveDecision_PreRender(object sender, EventArgs e)
    {
        grdLeaveDecision.DataSource = getLeaveDecision();  //GetData is your method that you will use to obtain the data you're populating the GridView with

        grdLeaveDecision.DataBind();

        if (approvals.Visible == true)
        {

            if (grdLeaveDecision.Rows.Count > 0)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(),"CallMyFunction","$('#<%= grdLeaveDecision.ClientID %>').dataTable({});",true);}
                //Replace the <td> with <th> and adds the scope attribute
                grdLeaveDecision.UseAccessibleHeader = true;


                if (grdLeaveDecision.HeaderRow != null)
                {
                    //Adds the<thead> and <tbody> elements required for DataTables to work
                    grdLeaveDecision.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                if (grdLeaveDecision.HeaderRow != null)
                {
                    //Adds the <tfoot> element required for DataTables to work
                    grdLeaveDecision.FooterRow.TableSection = TableRowSection.TableFooter;
                }
                // else { Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('.gvdatatable').dataTable({destroy:true});", true); }




                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('.gvdatatable').dataTable({});", true);
            }
            else
            {
                btnApprove.Visible = false;
                btnOnHold.Visible = false;
                btnReject.Visible = false;
                LabelDecision.Visible = true;


            }
            //if (grdLeaveHistory.Rows.Count <= 0) { Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('.gvdatatable').dataTable({destroy:true});", true); }
        }
        // if (grdLeaveDecision.Rows.Count <= 0) { Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "var prm = Sys.WebForms.PageRequestManager.getInstance();if (prm != null) {prm.add_beginRequest(function(sender, e){if (sender._postBackSettings.panelsToUpdate.parentNode != null) {$('.gvdatatable').dataTable({ destroy: true,retrieve:true});}});};", true); }
    }

    protected void grdLeaveBalance_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "lnkApply")
        {
            decimal a;
            clndtxtStartDate.SelectedDate = DateTime.Now.Date;
            clndtxtEndDate.SelectedDate = DateTime.Now.Date;

            int index = Convert.ToInt32(e.CommandArgument);
            Session["LeaveID"] = index;
            string LeaveName = GetLeaveName(index);

            //foreach (GridViewRow item in grdLeaveBalance.Rows)
            //{
            //    HiddenField hfd = new HiddenField();
            //    hfd = ((HiddenField)item.FindControl("hdfCurrentBalance"));
            //    a = Convert.ToDecimal(hfd.Value);

            //}
            //HiddenField hfd = new HiddenField();
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            HiddenField hdfCurrentBalance = (HiddenField)row.Cells[4].FindControl("hdfCurrentBalance");
            a = Convert.ToDecimal(hdfCurrentBalance.Value);
            hdfCurrent.Value = hdfCurrentBalance.Value;

            if (a <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "pop()", true);
                //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "show",
                //"<script>$('#mymodal').modal('show');</script>", false);
                //ScriptManager.RegisterStartupScript(UpnlLeaveBalance, UpnlLeaveBalance.GetType(),
                //    "show", "$('#mymodal').modal('show');", true);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                //modalPopup.Show();
                //LOP.Visible = true;
                //lblLeaveNamePopup.Text = LeaveName;
                lblLeaveNamePopup.Text = LeaveName;
            }
            if (a != 0)
            {

                // LOP.Visible = false;
                ClearPopup();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();", true);
                // modalPopup.Show();
                lblLeaveNamePopup.Text = LeaveName;

            }
            //lblLeaveNamePopup.Text = LeaveName;
            //modalPopup.Show();
            //ClearPopup();

        }



    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = string.Empty;
            string Leaveoptions = string.Empty; //Addedy by sof
            ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);
            //goto  exit;
            if (validate() == true)
            {
                foreach (GridViewRow item in grdLeaveDecision.Rows)
                {
                    CheckBox chkbox = new CheckBox();
                    string cmt = string.Empty;

                    string SelectedEmpLeaveApplicationId = "";
                    HiddenField hfd = new HiddenField();
                    hfd = ((HiddenField)item.FindControl("hfdEmpLeaveApplicationId"));

                    HiddenField hfdemp = new HiddenField();
                    hfdemp = ((HiddenField)item.FindControl("hfdEmpid"));

                    HiddenField hdfYear1 = new HiddenField();
                    hdfYear1 = ((HiddenField)item.FindControl("hfdYear"));

                    HiddenField hdfLeaveId1 = new HiddenField();
                    hdfLeaveId1 = ((HiddenField)item.FindControl("hfdLeaveId"));

                    HiddenField hfdStatusCode = new HiddenField();
                    hfdStatusCode = ((HiddenField)item.FindControl("hfdStatusCode"));

                    SelectedEmpLeaveApplicationId = hfd.Value;
                    chkbox = ((CheckBox)item.FindControl("chkselect"));

                    if (chkbox.Checked == true)
                    {

                        cmt = ((TextBox)item.FindControl("txtComments")).Text;
                        Status = ((Label)item.FindControl("lblStatus")).Text;
                        Leaveoptions = ((Label)item.FindControl("lblExisting_Compensation")).Text; //ADDED BY SOF

                        ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(SelectedEmpLeaveApplicationId);
                        ObjLeaveHistoryDO.Status = 1;
                        ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                        ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfLeaveId1.Value);
                        ObjLeaveHistoryDO.YearId = Convert.ToInt32(hdfYear1.Value);
                        ObjLeaveHistoryDO.Comments = cmt;
                        //ObjLeaveHistoryDO.StatusComments = Status;
                        ObjLeaveHistoryDO.Statuscode = Convert.ToByte(hfdStatusCode.Value);
                        ObjLeaveHistoryDO.Leaveoptions = Leaveoptions; //
                        //ObjLeaveHistoryBL.LeaveDecisionMail(ObjLeaveHistoryDO);
                        if (Leaveoptions == "Deduct from my leave balance")     
                        {
                            ObjLeaveHistoryBL.StatusApprove(ObjLeaveHistoryDO);
                        }
                        else if (Leaveoptions == "Compensate leave in upcoming week")
                        {
                            ObjLeaveHistoryBL.StatusUpdatecurrentblc(ObjLeaveHistoryDO);
                        }
                        else
                        {
                            ObjLeaveHistoryBL.StatusApproveprevious(ObjLeaveHistoryDO);
                        }
                    }    //Till here by sophia


                }



                if (Status == "Pending")
                {
                    //  System.Threading.Thread.Sleep(5000);
                    ShowMessage("Leave Approved Successfully", MessageType.Info);

                }

                if (Status == "On Hold")
                {
                    // System.Threading.Thread.Sleep(5000);
                    ShowMessage("The On-holded Leave Approved Successfully", MessageType.Info);

                }

                if (Status == "Cancelled")
                {
                    //System.Threading.Thread.Sleep(5000);
                    ShowMessage("Leave cancellation approved", MessageType.Info);

                }
                //if (ObjLeaveHistoryDO.Status == 1)
                //{


                //}
                //System.Threading.Thread.Sleep(5000);
                getLeaveDecision();

                //exit: ;
            }
        }
        catch (Exception ex)
        {
            //throw (ex);
            string filePath = @"D:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

            Response.Redirect("Error.htm");

        }

    }

    //protected void lnkApply_ondatabinding(object sender, EventArgs e)
    //{
    //    LinkButton lb = (LinkButton)sender;
    //    ScriptManager sm = (ScriptManager)Page.Master.FindControl("SM_ID");
    //    sm.RegisterPostBackControl(lb);
    //}

    public string GetTruncatedString(string lblReason)
    {
        if (lblReason.Length > 50)
        {
            var charlimit = lblReason.Substring(0, 50);
            var aaa = '.';
            var concatetest = charlimit + aaa + aaa + aaa + aaa;
            return concatetest;
        }
        else
            return lblReason;
    }

    public string GetTruncatedString_approvals(string lblReason_approvals)
    {
        if (lblReason_approvals.Length > 15)
        {
            var charlimit = lblReason_approvals.Substring(0, 15);
            var aaa = '.';
            var concatetest = charlimit + aaa + aaa + aaa + aaa;
            return concatetest;
        }
        else
            return lblReason_approvals;
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        ClearPopup();
        //int index = Convert.ToInt32();
        //Session["LeaveID"] = index;
        //string LeaveName = GetLeaveName(index);

        //lblLeaveNamePopup.Text = LeaveName;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();", true);
        //modalPopup.Show();
    }

    protected void ok_click(object sender, EventArgs e)
    {
        string URL = "";
        try
        {

            ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(ViewState["Startdate"]);
            ObjLeaveHistoryDO.EndDate = Convert.ToDateTime(ViewState["EndDate"]);
            ObjLeaveHistoryDO.Noofdays = Convert.ToString(ViewState["Noofdays"]);

            ObjLeaveHistoryDO.Frequency = ddlDuration.SelectedIndex;
            ObjLeaveHistoryDO.Reason = ViewState["Reason"].ToString();



            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(ViewState["EmpId"]);
            ObjLeaveHistoryDO.Rptmgr = Convert.ToInt32(ViewState["Rptmgr"]);
            ObjLeaveHistoryDO.Status = Convert.ToInt32(ViewState["Status"]);   // 0-Pending ,1-Approved ,2 -Reject
            ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(ViewState["LeaveID"]);
            ObjLeaveHistoryDO.Leaveoptions = ddlexistingcompensation.SelectedItem.Value;


            if (ddlexistingcompensation.SelectedItem.Value == "1")
            {
                ObjLeaveHistoryDO.CompensateLeavedates = DateTime.Parse(DropDownselectdates.SelectedItem.Value.ToString());
                ObjLeaveHistoryDO.PreviousCompensatedates = new DateTime();
            }
            else if (ddlexistingcompensation.SelectedItem.Value == "2")
            {
                ObjLeaveHistoryDO.CompensateLeavedates = new DateTime();
                ObjLeaveHistoryDO.PreviousCompensatedates = DateTime.Parse(DropDownselectholidays.SelectedItem.Value.ToString());
            }
            else
            {

                ObjLeaveHistoryDO.CompensateLeavedates = new DateTime();
                ObjLeaveHistoryDO.PreviousCompensatedates = new DateTime();
            }   //Till here added by sophia

            ObjLeaveHistoryBL.InsertLeaveApplication(ObjLeaveHistoryDO);

            // System.Threading.Thread.Sleep(5000);
            GetLeaveHistory();
            getLeavebalance();
            emplopcancel();
            Session["IsSubmit"] = 1;

            URL = Request.Url.AbsoluteUri;
            Response.Redirect(URL);

        }

        catch (Exception ex)
        {
            if (URL == "")
            {
                string filePath = @"D:\Error.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                        "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }


                Response.Redirect("Error.htm");

            }


        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //String a = hdfld.Value.ToString();
        //int Result = 0;
        //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //HiddenField hdfCurrentBalance = (HiddenField)row.Cells[4].FindControl("hdfCurrentBalance");
        //a = Convert.ToDecimal(hdfCurrentBalance.Value);
        decimal abc;
        //GridViewRow grow = grdLeaveBalance.Rows[0];
        //HiddenField hdfCurrentBalance = (HiddenField)grow.Cells[4].FindControl("hdfCurrentBalance");
        abc = Convert.ToDecimal(hdfCurrent.Value);
        //TableCell tc = grow.Cells[4];


        string URL = "";
        try
        {
            DateTime mStartDate = Convert.ToDateTime(DateTime.ParseExact(txtstartdate.Text, "MM/dd/yyyy", null));
            DateTime mEndDate = Convert.ToDateTime(DateTime.ParseExact(txtEndDate.Text, "MM/dd/yyyy", null));
            //if (mStartDate.DayOfWeek = DayOfWeek.Sunday && mStartDate.DayOfWeek = DayOfWeek.Saturday)

            string Restrict = "01/01/2016";
            DateTime ConRestrict = Convert.ToDateTime(Restrict);

            //if (Convert.ToDateTime(txtstartdate.Text) == null)
            //{
            //    ShowMessage("thambi startdate enga?", MessageType.Error);
            //    ClearPopup();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();", true);
            //    // modalPopup.Show();
            //    return;
            //}


            if (mStartDate < ConRestrict)
            {
                ShowMessage("You are Not Allowed to Apply Leave for the Previous Year", MessageType.Error);
                ClearPopup();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();", true);
                // modalPopup.Show();
                return;

            }
            //if (mStartDate.DayOfWeek == DayOfWeek.Saturday)
            //{
            //    ShowMessage("Selected Day is an Holiday", MessageType.Error);
            //    ClearPopup();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();", true);
            //    //modalPopup.Show();
            //    return;
            //}
            if (mStartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                ShowMessage("Selected Day is an Holiday", MessageType.Error);
                ClearPopup();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();readyDropDown()", true); //Added by sophia
                //modalPopup.Show();
                return;
            }


            if (mStartDate > mEndDate)
            {
                ShowMessage("End date should be > = Start date", MessageType.Error);
                // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('End date should not less than equal to Start date ')", true);
                ClearPopup();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();readyDropDown()", true); //Added by sophia
                //modalPopup.Show();
                return;
            }



            if (Convert.ToDecimal(txtNoofdays.Text) <= 0)
            {
                ShowMessage("No.of.days cannot be a zero and negative value", MessageType.Error);
                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('No.of.days cannot be a zero and negative value')", true);
                ClearPopup();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();", true);
                //modalPopup.Show();
                return;
            }
            if (Convert.ToDecimal(txtNoofdays.Text) > abc)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenconformModal();", true);
                //Convert.ToDateTime(DateTime.ParseExact(lopstartdate.Text, "yyyy/MM/dd", null));
                //modalPopup.Show();
                ViewState["Startdate"] = Convert.ToString(DateTime.ParseExact(txtstartdate.Text, "MM/dd/yyyy", null));
                ViewState["EndDate"] = Convert.ToString(DateTime.ParseExact(txtEndDate.Text, "MM/dd/yyyy", null));
                ViewState["Noofdays"] = Convert.ToString(txtNoofdays.Text);
                ViewState["Frequency"] = ddlDuration.SelectedIndex;
                ViewState["Reason"] = txtReason.Text;
                ViewState["Leaveoptions"] = ddlexistingcompensation.SelectedIndex;  // Added BY SOPHIA
                ViewState["EmpId"] = Convert.ToInt32(Session["EmpId"]);
                ViewState["Rptmgr"] = Convert.ToInt32(Session["RptMgrId"]);
                ViewState["Status"] = 0;   // 0-Pending ,1-Approved ,2 -Reject
                ViewState["LeaveID"] = Convert.ToInt32(Session["LeaveID"]);

                //ObjLeaveHistoryBL.InsertLeaveApplication(ObjLeaveHistoryDO);

                return;
            }
            //Convert.ToString(DateTime.ParseExact(txtDOB.Text, "dd-MM-yyyy", null));MM/dd/yyyy
            ViewState["Startdate"] = Convert.ToString(DateTime.ParseExact(txtstartdate.Text, "MM/dd/yyyy", null));
            ViewState["EndDate"] = Convert.ToString(DateTime.ParseExact(txtEndDate.Text, "MM/dd/yyyy", null));
            ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(DateTime.ParseExact(txtstartdate.Text, "MM/dd/yyyy", null));
            ObjLeaveHistoryDO.EndDate = Convert.ToDateTime(DateTime.ParseExact(txtEndDate.Text, "MM/dd/yyyy", null));
            ObjLeaveHistoryDO.Noofdays = Convert.ToString(txtNoofdays.Text);

            ObjLeaveHistoryDO.Frequency = ddlDuration.SelectedIndex;
            ObjLeaveHistoryDO.Reason = txtReason.Text;

            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO.Rptmgr = Convert.ToInt32(Session["RptMgrId"]);
            ObjLeaveHistoryDO.Status = 0;   // 0-Pending ,1-Approved ,2 -Reject
            ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(Session["LeaveID"]);
            ObjLeaveHistoryDO.Leaveoptions = ddlexistingcompensation.SelectedItem.Value; //ADDED BY SOPHIA 
            //ObjLeaveHistoryDO.CompensateLeavedates = DropDownselectdates.Items;
            //DateTime dt = DateTime.Parse(DropDownselectdates.SelectedItem.Value);
            //Console.WriteLine(dt);
            // Loop through all items the ListBox.
            for (int x = 0; x < DropDownselectdates.Items.Count; x++)
            {
                Console.WriteLine(x);
                //  Determine if the item is selected..
                var t = DropDownselectdates.SelectedIndex;
                Console.WriteLine(DropDownselectdates.SelectedIndex);

            }
            if (ddlexistingcompensation.SelectedItem.Value == "1")
            {
                ObjLeaveHistoryDO.CompensateLeavedates = DateTime.Parse(DropDownselectdates.SelectedItem.Value.ToString());
                ObjLeaveHistoryDO.PreviousCompensatedates = new DateTime();
            }
            else if (ddlexistingcompensation.SelectedItem.Value == "2")
            {
                ObjLeaveHistoryDO.CompensateLeavedates = new DateTime();
                ObjLeaveHistoryDO.PreviousCompensatedates = DateTime.Parse(DropDownselectholidays.SelectedItem.Value.ToString());
            }
            else
            {
                ObjLeaveHistoryDO.CompensateLeavedates = new DateTime();
                ObjLeaveHistoryDO.PreviousCompensatedates = new DateTime();
            }   //Till here
            ObjLeaveHistoryBL.InsertLeaveApplication(ObjLeaveHistoryDO);
            //int isExists = Convert.ToInt32(ObjLeaveHistoryDO.EmpLeaveApplicationId);  //Added by sophia

            //if (isExists == 0)
            //{
            //    ShowMessage("Leave is already applied for this date.", MessageType.Error);
            //    ClearPopup();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalpopup();readyDropDown()", true); //Added by sophia
            //modalPopup.Show();
            //return;
            //}
            // System.Threading.Thread.Sleep(5000);
            GetLeaveHistory();
            getLeavebalance();
            emplopcancel();
            Session["IsSubmit"] = 1;

            URL = Request.Url.AbsoluteUri;
            Response.Redirect(URL);



        }

        catch (Exception ex)
        {

            if (URL == "")
            {

                string filePath = @"D:\Error.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }

                Response.Redirect("Error.htm");

            }
            //throw ex;

        }

    }

    private void getRptmgr()
    {

        try
        {
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.GetReportManagerName(ObjLeaveHistoryDO);
            // ObjLeaveHistoryDO.Rptmgr = reportmanagername;

            int RptMgrId = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][2]);
            Session["RptMgrId"] = RptMgrId;

            string RptMgrName = ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][3].ToString();

            lblRptmgr.Text = RptMgrName;

        }
        catch (Exception ex)
        {
            //throw ex;
            string filePath = @"D:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

            Response.Redirect("Error.htm");

        }
    }

    private void ClearPopup()
    {

        txtstartdate.Text = "";
        txtEndDate.Text = "";
        txtNoofdays.Text = "";
        txtReason.Text = "";
    }

    private string GetLeaveName(int LeaveId)
    {
        // string mStrSQL = "SELECT LeaveType FROM LM_LeaveTYpe Where LeaveId =" + LeaveId + " AND Empid =  " + EmpId;
        ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
        ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(Session["LeaveID"]);
        ObjLeaveHistoryDO = ObjLeaveHistoryBL.getleavetype(ObjLeaveHistoryDO);
        ViewState["LeaveType"] = ObjLeaveHistoryDO.Resultdtset.Tables[0];

        string leavetype = ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0].ToString();
        return leavetype;
    }

    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

    }

    private bool validate()
    {
        bool flag = false;
        foreach (GridViewRow gvrow in grdLeaveDecision.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkselect");
            if (chk.Checked)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select atleast one checkbox');", true);
            ShowMessage("Please select the Records to be Actioned", MessageType.Error);

        }
        return flag;
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {

        try
        {
            string Status = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);

            if (validate() == true)
            {

                foreach (GridViewRow item in grdLeaveDecision.Rows)
                {
                    CheckBox chkbox = new CheckBox();
                    string cmt = string.Empty;
                    //string Status = string.Empty;
                    string SelectedEmpLeaveApplicationId = "";
                    HiddenField hfd = new HiddenField();
                    hfd = ((HiddenField)item.FindControl("hfdEmpLeaveApplicationId"));


                    HiddenField hfdemp = new HiddenField();
                    hfdemp = ((HiddenField)item.FindControl("hfdEmpid"));

                    HiddenField hdfYear1 = new HiddenField();
                    hdfYear1 = ((HiddenField)item.FindControl("hfdYear"));

                    HiddenField hdfLeaveId1 = new HiddenField();
                    hdfLeaveId1 = ((HiddenField)item.FindControl("hfdLeaveId"));

                    HiddenField hfdStatusCode = new HiddenField();
                    hfdStatusCode = ((HiddenField)item.FindControl("hfdStatusCode"));

                    SelectedEmpLeaveApplicationId = hfd.Value;



                    chkbox = ((CheckBox)item.FindControl("chkselect"));

                    if (chkbox.Checked == true)
                    {

                        cmt = ((TextBox)item.FindControl("txtComments")).Text;
                        Status = ((Label)item.FindControl("lblStatus")).Text;

                        if (cmt == "")
                        {
                            ShowMessage("Please Enter Comment", MessageType.Error);
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter Comment(Reason) Before Rejecting');", true);

                            //UpnlLeaveHistory.Update();
                            //UpnlLeaveBalance.Update();
                            //UpnlLeaveDecision.Update();
                            return;

                        }
                        if (Status == "Cancelled")
                        {
                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                            ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfLeaveId1.Value);
                            ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(SelectedEmpLeaveApplicationId);
                            ObjLeaveHistoryDO.Status = 1;
                            ObjLeaveHistoryDO.Comments = cmt;
                            ObjLeaveHistoryBL.CancelRejected(ObjLeaveHistoryDO);
                        }
                        else
                        {



                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                            ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfLeaveId1.Value);
                            ObjLeaveHistoryDO.YearId = Convert.ToInt32(hdfYear1.Value);

                            ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(SelectedEmpLeaveApplicationId);
                            ObjLeaveHistoryDO.Status = 2;
                            ObjLeaveHistoryDO.Comments = cmt;
                            //ObjLeaveHistoryDO.StatusComments = Status;
                            ObjLeaveHistoryDO.Statuscode = Convert.ToByte(hfdStatusCode.Value);
                            //ObjLeaveHistoryBL.LeaveDecisionMail(ObjLeaveHistoryDO);
                            ObjLeaveHistoryBL.StatusRejected(ObjLeaveHistoryDO);


                            //ObjLeaveHistoryBL.CancelRejected(ObjLeaveHistoryDO);

                        }
                    }


                }
                if (Status == "Pending")
                {
                    ShowMessage("Selected Leave has been Rejected", MessageType.Info);

                }

                if (Status == "On Hold")
                {
                    ShowMessage("The On-holded Leave Rejected", MessageType.Info);


                }

                if (Status == "Cancelled")
                {
                    ShowMessage("Cancellation Leave has been Rejected", MessageType.Info);

                }
                getLeaveDecision();
            }
        }
        catch (Exception ex)
        {

            //throw (ex);
            string filePath = @"D:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

            Response.Redirect("Error.htm");

        }


    }

    protected void btnOnHold_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);


            if (validate() == true)
            {

                foreach (GridViewRow item in grdLeaveDecision.Rows)
                {
                    CheckBox chkbox = new CheckBox();
                    string cmt = string.Empty;

                    string SelectedEmpLeaveApplicationId = "";


                    HiddenField hfd = new HiddenField();
                    hfd = ((HiddenField)item.FindControl("hfdEmpLeaveApplicationId"));
                    HiddenField hfdemp = new HiddenField();
                    hfdemp = ((HiddenField)item.FindControl("hfdEmpid"));

                    HiddenField hdfYear1 = new HiddenField();
                    hdfYear1 = ((HiddenField)item.FindControl("hfdYear"));

                    HiddenField hdfLeaveId1 = new HiddenField();
                    hdfLeaveId1 = ((HiddenField)item.FindControl("hfdLeaveId"));

                    HiddenField hfdStatusCode = new HiddenField();
                    hfdStatusCode = ((HiddenField)item.FindControl("hfdStatusCode"));

                    SelectedEmpLeaveApplicationId = hfd.Value;



                    chkbox = ((CheckBox)item.FindControl("chkselect"));

                    if (chkbox.Checked == true)
                    {

                        cmt = ((TextBox)item.FindControl("txtComments")).Text;
                        Status = ((Label)item.FindControl("lblStatus")).Text;

                        if (cmt == "")
                        {
                            ShowMessage("Please enter Your comment for On-Holding", MessageType.Error);

                            return;

                        }
                        if (Status == "Cancelled")
                        {
                            ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(SelectedEmpLeaveApplicationId);
                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                            ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfLeaveId1.Value);
                            ObjLeaveHistoryDO.Comments = cmt;
                            ObjLeaveHistoryBL.CancelOnhold(ObjLeaveHistoryDO);
                        }
                        else
                        {
                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                            ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfLeaveId1.Value); 
                            ObjLeaveHistoryDO.YearId = Convert.ToInt32(hdfYear1.Value);
                            ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(SelectedEmpLeaveApplicationId);
                            ObjLeaveHistoryDO.Status = 3;
                            ObjLeaveHistoryDO.Comments = cmt;
                            //ObjLeaveHistoryDO.StatusComments = Status;
                            ObjLeaveHistoryDO.Statuscode = Convert.ToByte(hfdStatusCode.Value);
                            // ObjLeaveHistoryBL.LeaveDecisionMail(ObjLeaveHistoryDO);
                            ObjLeaveHistoryBL.StatusOnHold(ObjLeaveHistoryDO);

                        }
                    }

                }
                if (Status == "Pending")
                {
                    ShowMessage("Selected Leave has been On-Holded", MessageType.Info);

                }

                if (Status == "Cancelled")
                {
                    ShowMessage("Leave Cancellation has been On-Holded", MessageType.Info);


                }
                //System.Threading.Thread.Sleep(5000);
                getLeaveDecision();

            }
        }
        catch (Exception ex)
        {

            //throw (ex);
            string filePath = @"D:\Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

            Response.Redirect("Error.htm");

        }
    }

    private int CheckCredential(string Username)
    {
        try
        {

            int GetCount = 0;
            GetCount = ObjLeaveHistoryBL.ReportMGRLoginCredential(Username);
            return GetCount;

        }
        catch (Exception ex)
        {
            throw ex;
            //string filePath = @"D:\Error.txt";

            //using (StreamWriter writer = new StreamWriter(filePath, true))
            //{
            //    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
            //       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
            //    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            //}

            //Response.Redirect("Error.htm");
        }

    }

    protected void grdLeaveBalance_Databound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.DataItem != null)

        //{
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = e.Row.DataItem as DataRowView;

            HiddenField hdfBeginingBalance = (HiddenField)e.Row.FindControl("hdfBeginingBalance");
            TotalBeginingBalance = TotalBeginingBalance + Convert.ToDecimal(hdfBeginingBalance.Value);

            HiddenField hdfEarnedLeave = (HiddenField)e.Row.FindControl("hdfEarnedLeave");
            TotalEarnedLeave = TotalEarnedLeave + Convert.ToDecimal(hdfEarnedLeave.Value);

            HiddenField hdfAvailed = (HiddenField)e.Row.FindControl("hdfAvailed");
            TotalAvailed = TotalAvailed + Convert.ToDecimal(hdfAvailed.Value);

            HiddenField hdfCurrentBalance = (HiddenField)e.Row.FindControl("hdfCurrentBalance");
            TotalCurrentbalance = TotalCurrentbalance + Convert.ToDecimal(hdfCurrentBalance.Value);

            HiddenField hdfLop = (HiddenField)e.Row.FindControl("hdfLop");
            TotalLOP = TotalLOP + Convert.ToDecimal(hdfLop.Value);



        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalBeginningbalance = (Label)e.Row.FindControl("lblTotalBeginningbalance");
            lblTotalBeginningbalance.Text = TotalBeginingBalance.ToString();

            Label lblTotalEarnedLeave = (Label)e.Row.FindControl("lblTotalEarnedLeave");
            lblTotalEarnedLeave.Text = TotalEarnedLeave.ToString();

            Label lblTotalAvailed = (Label)e.Row.FindControl("lblTotalAvailed");
            lblTotalAvailed.Text = TotalAvailed.ToString();

            Label lbltotalCurrentbalance = (Label)e.Row.FindControl("lbltotalCurrentbalance");
            lbltotalCurrentbalance.Text = TotalCurrentbalance.ToString();

            Label lbltotalLOP = (Label)e.Row.FindControl("lbltotalLOP");
            lbltotalLOP.Text = TotalLOP.ToString();


        }
        //}

    }
    protected void lbtnPermission_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["FirstName"].ToString() != "")
            {

            }
            if (Request.QueryString["sessionid"] != null)
            {
                sessionid = Request.QueryString["sessionid"].ToString();
            }

            if (Request.QueryString["Username"] != null)
            {
                Username = Request.QueryString["Username"].ToString();
                Session["Username"] = Username;

            }

            if (Request.QueryString["EmpId"] != null)
            {
                EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
                Session["EmpId"] = EmpId;
            }

            Response.Redirect("Permission.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdLeaveHistory_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {

                DataRowView dr = e.Row.DataItem as DataRowView;
                // CheckBox chkActive = (CheckBox)e.Row.FindControl("chkActive");
                HiddenField hdfStatus = (HiddenField)e.Row.FindControl("hdfEmpLeaveApplicationID");
                Label lbllopReason = (Label)e.Row.FindControl("lbllopStatus");
                LinkButton lnkCancel = (LinkButton)e.Row.FindControl("LnkCancel");
                LinkButton LnklopCancel = (LinkButton)e.Row.FindControl("LnklopCancel");
                //Response.Write("RowDataBound" + hdfStatus.Value);
                HiddenField hdfComments = (HiddenField)e.Row.FindControl("hdfLeaveComments");
                Label lblReason = (Label)e.Row.FindControl("lblReason");
                Label lblExisting_Compensation = ((Label)e.Row.FindControl("lblExisting_Compensation")); //ADDED BY SOPHIA
                Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                //Response.Write("RowDataBound" + hdfStatus.Value);
                if (hdfStatus.Value == "Cancelled" || hdfStatus.Value == "Rejected" || hdfStatus.Value == "On Hold" || hdfComments.Value == "Auto Process" || hdfComments.Value == "Auto process")
                {
                    lnkCancel.Enabled = false;
                    lnkCancel.Attributes.Remove("href");
                    lnkCancel.Attributes.Remove("onmouseover");
                    lnkCancel.Attributes.Remove("onmouseout");
                    lnkCancel.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                    lnkCancel.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                    lnkCancel.Attributes.Add("onmouseover", lnkCancel.ToolTip = "Cancelled!!");

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('.gvdatatable').dataTable({});", true);


                }
                if (lblReason.Text == "Timesheet not filled")
                {
                    LnklopCancel.Visible = true;
                }
                else if (lblStatus.Text == "Cancelled" || lblStatus.Text == "Rejected" || lblStatus.Text == "On Hold" || lblStatus.Text == "Processing")
                {
                    LnklopCancel.Visible = false;
                }
            }
        }
    }

    protected void grdLeaveHistory_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "LnklopCancel") // Added by LOGESH
        {
            lopactiontaken.Text = "";
            loptimshthrs.Text = "";
            lopleavehrs.Text = "0";
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.GetReportManagerName(ObjLeaveHistoryDO);
            int RptMgrId = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][2]);
            Session["RptMgrId"] = RptMgrId;
            string RptMgrName = ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][3].ToString();
            loprptmgr.Text = RptMgrName;
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label strdate = (Label)row.FindControl("lblStartDate");
            Label enddate = (Label)row.FindControl("lblEndDate");
            Label No_of_Days = (Label)row.FindControl("lblNo_of_Days");
            Label lblLeaveType = (Label)row.FindControl("lblLeaveType");
            Label Leaveoptions = (Label)row.FindControl("lblExisting_Compensation"); //ADDED BY SOPHIA
            HiddenField hdfStatus = (HiddenField)row.FindControl("hdfEmpLeaveApplicationID");
            Session["LeaveID"] = hdfStatus.Value;
            htnNoOfDays.Value = No_of_Days.Text;
            lopimgbtnCalendarStartDate.Enabled = false;
            lopimgbtnCalendarEndDate.Enabled = false;

            lopenddate.Text = enddate.Text;
            lopstartdate.Text = strdate.Text;
            //loplevtype.Text = lblLeaveType.Text;

            ObjLeaveHistoryDO.TaskDate = lopstartdate.Text.Replace('/', '-');
            //ObjLeaveHistoryDO.StartDate = strdate.Text;
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.GetTimeSheetHours(ObjLeaveHistoryDO);
            var LeaveStatus = ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0].ToString();
            string timesheethours = "0";
            string per_hours = "0";
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.SP_LM_LOP_TimesheetHrs(ObjLeaveHistoryDO);


            if (true)
            {
                per_hours = ObjLeaveHistoryDO.Resultdtset.Tables[1].Rows[0][0].ToString();
                //if (per_hours == "") per_hours = "0";

                timesheethours = ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0].ToString();



                if (No_of_Days.Text == "1.0") // value = 1.0//
                {
                    //for (int i = 0; i < ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows.Count; i++)
                    //{
                    //    timesheethours += Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[i][0]);
                    //}

                    //for (int i = 0; i < ObjLeaveHistoryDO.Resultdtset.Tables[1].Rows.Count; i++)
                    //{
                    //    per_hours += Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[1].Rows[i][0]);
                    //}

                    /// Response.Write(per_hours);
                    loptimshthrs.Text = timesheethours.ToString();
                    lopleavehrs.Text = per_hours.ToString();
                    //lopleavehrs.Text = 8.ToString();
                    divloptimshthrs.Visible = true;
                    total = ObjLeaveHistoryDO.Resultdtset.Tables[2].Rows[0][0].ToString();

                    if (Convert.ToDouble(total) >= 8 && lopactiontaken.Text != "")
                    {

                        Button2.Enabled = true;

                    }
                    else
                    {
                        Button2.Enabled = false;

                    }
                    //lopleavehrs.Text = 8.ToString();
                    //divloptimshthrs.Visible = true;
                    // loptimshthrs.Text = 0.ToString();

                }

                else if (No_of_Days.Text == "0.5") // value = 0.5
                {
                    //for (int i = 0; i < ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows.Count; i++)
                    //{
                    //    timesheethours += Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[i][0]);
                    //}

                    //for (int i = 0; i < ObjLeaveHistoryDO.Resultdtset.Tables[1].Rows.Count; i++)
                    //{
                    //    per_hours += Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[1].Rows[i][0]);
                    //}

                    //int LOP = 4 + per_hours;
                    //int LOP = per_hours;
                    loptimshthrs.Text = timesheethours.ToString();
                    lopleavehrs.Text = per_hours;
                    divloptimshthrs.Visible = true;
                    //sum = Convert.ToInt32(loptimshthrs.Text) + Convert.ToInt32(lopleavehrs.Text);
                    //if (sum >= 4)
                    //{

                    //    //divloptimshthrs.Visible = true;
                    //    //loptimeshthrswrng.Visible = true;
                    //    //loptimeshthrswrng.ForeColor = System.Drawing.Color.Red;
                    //    Button2.Enabled = true;

                    //}
                    //else
                    //{
                    //    loptimeshthrswrng.Visible = false;
                    //}

                    if (Convert.ToDouble(total) >= 4 && lopactiontaken.Text != "")
                    {

                        Button2.Enabled = true;

                    }
                    else
                    {
                        Button2.Enabled = false;

                    }
                    //if (Convert.ToInt32(loptimshthrs.Text) < 4)
                    //{
                    //    divloptimshthrs.Visible = true;
                    //    loptimeshthrswrng.Visible = true;
                    //    loptimeshthrswrng.ForeColor = System.Drawing.Color.Red;
                    //    Button2.Enabled = false;
                    //}
                    //else
                    //{
                    //    loptimeshthrswrng.Visible = false;
                    //}






                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "lopPopup();", true);
            }
            else if (LeaveStatus == "0")
            {
                ShowMessage("Leave Request is Pending", MessageType.Info);
            }
            else if (LeaveStatus == "2")
            {
                ShowMessage("Leave Request is Rejected", MessageType.Info);
            }
            else if (LeaveStatus == "3")
            {
                ShowMessage("Leave Request is On-Hold", MessageType.Info);
            }
            else if (LeaveStatus == "4")
            {
                ShowMessage("Leave Request is cancelled", MessageType.Info);
            }
            else if (LeaveStatus == "5")
            {
                ShowMessage("Please apply leave for this date before submitting", MessageType.Info);
            }
        }
        if (e.CommandName == "LnkCancel")
        {
            try
            {
                int result = 0;
                int index = Convert.ToInt32(e.CommandArgument);
                LeaveHistoryDAL objLeaveHistoryDAL = new LeaveHistoryDAL();
                ObjLeaveHistoryDO.EmpLeaveApplicationId = index;
                ObjLeaveHistoryDO = objLeaveHistoryDAL.getvalue_LeaveCancel(ObjLeaveHistoryDO);
                result = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0]);


                if (result == 0)
                {
                    objLeaveHistoryDAL.BackdatedLeaveCancel(ObjLeaveHistoryDO);
                    objLeaveHistoryDAL.LeaveCancel(ObjLeaveHistoryDO, result);
                    //  System.Threading.Thread.Sleep(5000);
                    ShowMessage("Leave Cancellation Request has been sent to the Reporting Manager", MessageType.Info);
                }
                else if (result == 1)
                {

                    objLeaveHistoryDAL.LeaveCancel(ObjLeaveHistoryDO, result);
                    // System.Threading.Thread.Sleep(5000);
                    ShowMessage("Leave has been cancelled successfully ", MessageType.Info);
                }
                else { ShowMessage("You are Not Allowed to Cancel this Leave", MessageType.Error); return; }
                // LeaveCancel(LeaveHistoryDO objLeaveHistoryDO);
                // System.Threading.Thread.Sleep(5000);
                GetLeaveHistory();
                getLeavebalance();
                getLeaveDecision();
                emplopcancel();
                //UpnlMain.Update();
                //UpnlLeaveBalance.Update();
                //UpnlLeaveDecision.Update();
                //UpnlLeaveHistory.Update();
                //ShowMessage("Leave has been cancelled successfully ", MessageType.Info);

                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Leave has been cancelled successfully ')", true);
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('.gvdatatable').dataTable({ destroy: true, retrieve: true });", true);
            }


            catch (Exception ex)
            { //throw ex;
                string filePath = @"D:\Error.txt";

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }

                Response.Redirect("Error.htm");
            }
        }

    }

    public object getdatacalender()
    {
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("[LM_Data_calender]", mycn);
            // cmd.Parameters.Add(new SqlParameter("@startdate"));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
    }

    public object HolidayList()
    {

        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter da1 = new SqlDataAdapter();
        DataTable dt1 = new DataTable();
        cmd1 = new SqlCommand("select Holiday_leaveId,Holiday_date,Holiday_name  from lm_holidaylist", mycn);
        // cmd.Parameters.Add(new SqlParameter("@startdate"));
        cmd.CommandType = CommandType.StoredProcedure;
        da1.SelectCommand = cmd1;
        da1.Fill(ds1);
        return ds1;
    }

    public object MorethanoneEMp()
    {

        SqlCommand cmd2 = new SqlCommand();
        SqlDataAdapter da2 = new SqlDataAdapter();
        DataTable dt2 = new DataTable();
        cmd2 = new SqlCommand("LM_EMP_morethan1", mycn);
        // cmd.Parameters.Add(new SqlParameter("@startdate"));
        cmd.CommandType = CommandType.StoredProcedure;
        da2.SelectCommand = cmd2;
        da2.Fill(ds2);
        return ds2;
    }

    protected void CalendarDRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
    {
        // getdatacalender();

        StringBuilder strStatus = new StringBuilder();
        StringBuilder strEndDate = new StringBuilder();

        if (!e.Day.IsOtherMonth)
        {
            //string 

            if (ds.Tables.Count > 0)
            {
                DataTable contacts = ds.Tables[0];
                DataView view = contacts.AsDataView();
                view.RowFilter = "StartDate='" + e.Day.Date + "'";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    string STATUSNAME = dr["STATUSNAME"].ToString();
                    DateTime STARTDATE = Convert.ToDateTime(dr["STARTDATE"]);
                    DateTime ENDDATE = Convert.ToDateTime(dr["ENDDATE"]);
                    //string STARTDATE1 = STARTDATE.ToString("dd-MMM-yyyy");
                    //string ENDDATE1 = ENDDATE.ToString("dd-MMM-yyyy");
                    //decimal NOofDays = Convert.ToDecimal(dr["NO_OF_DAYS"]);
                    if ((dr["StartDate"].ToString() != DBNull.Value.ToString()) || (dr["ENDDATE"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTime dtEvent = (DateTime)dr["StartDate"];
                        DateTime dtEvent1 = (DateTime)dr["ENDDATE"];

                        //if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday || e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
                        //{
                        //    e.Cell.ForeColor = System.Drawing.Color.Black;
                        //    e.Cell.BackColor = System.Drawing.Color.White;
                        //}

                        if ((dtEvent.Equals(e.Day.Date)) || (dtEvent1.Equals(e.Day.Date)))
                        {

                            if (STATUSNAME == "Approved")
                            {

                                //str.Add(appendtext);
                                e.Cell.BackColor = System.Drawing.Color.Tomato;

                            }
                            else if (STATUSNAME == "Pending")
                            {

                                e.Cell.BackColor = System.Drawing.Color.Silver;
                                //e.Cell.ForeColor = System.Drawing.Color.Black;

                            }
                            else if (STATUSNAME == "On Hold")
                            {
                                e.Cell.BackColor = System.Drawing.Color.Yellow;

                            }
                            else
                            {
                                e.Cell.Text = "";
                            }
                        }

                    }
                }

                foreach (DataRowView rowView in view)
                {
                    string FirstName = rowView.Row.ItemArray[0].ToString();
                    string LeaveType = rowView.Row.ItemArray[1].ToString();
                    DateTime StartDate = Convert.ToDateTime(rowView.Row.ItemArray[2]);
                    DateTime EndDate = Convert.ToDateTime(rowView.Row.ItemArray[3]);
                    decimal NOofDays = Convert.ToDecimal(rowView.Row.ItemArray[4]);
                    string STATUSNAME = rowView.Row.ItemArray[5].ToString();
                    string AppendText = "[" + FirstName + " - " + LeaveType + " - " + NOofDays + " days " + " - " + STATUSNAME + "]";
                    strStatus.Append(AppendText);
                    e.Cell.Attributes.Add("onmouseover", e.Cell.ToolTip = strStatus.ToString());
                }

            }



            if (ds1.Tables.Count > 0)
            {


                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                {

                    string Morethanoneempapp = dr2["Startdate"].ToString();
                    // string Holidaylist = 


                    if ((dr2["StartDate"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTime dtEvent = (DateTime)dr2["Startdate"];

                        int Count = Convert.ToInt32(dr2["Count"]);
                        // String dtEvent = (DateTime)dr["StartDate"];
                        if (Count > 1)
                        {
                            if (dtEvent.Equals(e.Day.Date))
                            {
                                e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#d8b511");
                            }
                            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)// || e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                e.Cell.ForeColor = System.Drawing.Color.Black;
                                e.Cell.BackColor = System.Drawing.Color.White;
                            }
                        }



                    }
                    else
                    {
                        e.Cell.Text = "";
                    }
                }

                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {

                    string HolidayName = dr1["Holiday_name"].ToString();
                    // string Holidaylist = 
                    if ((dr1["Holiday_date"].ToString() != DBNull.Value.ToString()))
                    {
                        DateTime dtEvent = (DateTime)dr1["Holiday_date"];
                        // String dtEvent = (DateTime)dr["StartDate"];
                        if (dtEvent.Equals(e.Day.Date))
                        {

                            e.Cell.BackColor = System.Drawing.Color.Green;
                            e.Cell.Attributes.Add("onmouseover", e.Cell.ToolTip = HolidayName);
                            // Calendar1.ShowNextPrevMonth = true;
                            //Calendar1.ShowNextPrevMonth = ;
                            Calendar1.ShowNextPrevMonth = false;
                            if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)// || e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
                            {
                                e.Cell.ForeColor = System.Drawing.Color.Black;
                                e.Cell.BackColor = System.Drawing.Color.Green;
                            }
                            if (e.Day.IsOtherMonth)
                            {
                                e.Cell.Text = "";
                                e.Cell.BackColor = System.Drawing.Color.White;
                            }
                        }
                    }
                    else
                    {
                        e.Cell.Text = "";
                    }
                }

            }

        }

    }

    private void Calendar1_SelectionChanged(object sender, System.EventArgs e)
    {
        myda = new SqlDataAdapter("LM_DATACALENDER", mycn);
        dsSelDate = new DataSet();
        myda.Fill(dsSelDate, "LM_DATACALENDER");
        if (dsSelDate.Tables[0].Rows.Count == 0)
        {
            DataGrid1.Visible = false;
        }
        else
        {
            DataGrid1.Visible = true;
            DataGrid1.DataSource = dsSelDate;
            DataGrid1.DataBind();
        }
    }

    public class dataCalendar
    {
        public string Firstname { get; set; }
        public string Leavename { get; set; }
        public string STATUSNAME { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }

    }


    public void GetEmpName()
    {
        try
        {

            //DataSet ds = new DataSet();
            ObjLeaveHistoryDO.EmpId = EmpId;
            retresultdo = ObjLeaveHistoryBL.bindddlSelectEmpName(ObjLeaveHistoryDO);
            ddlSelectEmpName.DataSource = retresultdo.Resultdtset;
            ddlSelectEmpName.DataTextField = "Empname";
            ddlSelectEmpName.DataValueField = "Empid";
            ddlSelectEmpName.DataBind();
            //ddlSelectEmpName.Items.Insert(0, "  ---Select Employee---  ");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public object GetEmpLeaveHistory()
    {
        try
        {
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            string GetEmpname = "";
            string GetLeaveType = "";
            int[] EmpnameArray = new int[ddlSelectEmpName.Items.Count];
            foreach (ListItem listItem in ddlSelectEmpName.Items)
            {
                if (listItem.Selected)
                {
                    int i = 0;
                    EmpnameArray[i] = Convert.ToInt32(ddlSelectEmpName.Items[i].Value.ToString());
                    i++;
                    var val = listItem.Value;
                    var txt = listItem.Text;
                    GetEmpname += val + ",";

                }
            }
            int[] LeaveTypeArray = new int[lstleavetype.Items.Count];
            foreach (ListItem listItem in lstleavetype.Items)
            {
                if (listItem.Selected)
                {
                    int i = 0;
                    LeaveTypeArray[i] = Convert.ToInt32(lstleavetype.Items[i].Value.ToString());
                    i++;
                    var val = listItem.Value;
                    var txt = listItem.Text;
                    GetLeaveType += val + ",";

                }
            }
            if (ddlEmpleaveHistory.SelectedItem.Text == "All")
            {                
                ObjLeaveHistoryDO.Alldate = ddlEmpleaveHistory.SelectedItem.Text;
            }
            if (ddlEmpleaveHistory.SelectedItem.Value == "2")
            {
                //ObjLeaveHistoryDO.ParticularDate = txtdate.Text;
                ObjLeaveHistoryDO.FromChooseDate = txtdate.Text;
            }
            if (ddlEmpleaveHistory.SelectedItem.Value == "3")
            {
                ObjLeaveHistoryDO.FromChooseDate = txtFromdate.Text;
                ObjLeaveHistoryDO.ToChooseDate = txtTodate.Text;

            }
            if (ObjLeaveHistoryDO.ParticularDate == null) ObjLeaveHistoryDO.ParticularDate="";
            ObjLeaveHistoryDO.EmployeeName = GetEmpname;
            ObjLeaveHistoryDO.LeavesType = GetLeaveType;
            LeaveHistoryDO ds_EmpLeaveHistory = ObjLeaveHistoryBL.bindgrdEmpLeaveHistory(ObjLeaveHistoryDO);
            grdEmpLeaveHistory.DataSource = ds_EmpLeaveHistory.Resultdtset;
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdEmpLeaveHistory(ObjLeaveHistoryDO);
            grdEmpLeaveHistory.DataBind();
            pnlgridview.Visible = true;
            return ObjLeaveHistoryDO.Resultdtset.Tables[0];
        }
        catch (Exception e) { throw e; }

    }

    //public object GetEmpLeaveHistory()
    //{
    //    try
    //    {

    //        ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
    //        ObjLeaveHistoryDO.EmpId = ((ddlSelectEmpName.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlSelectEmpName.SelectedValue.ToString()));
    //        LeaveHistoryDO ds_EmpLeaveHistory = ObjLeaveHistoryBL.bindgrdEmpLeaveHistory(ObjLeaveHistoryDO);

    //        grdEmpLeaveHistory.DataSource = ds_EmpLeaveHistory.Resultdtset;
    //        ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdEmpLeaveHistory(ObjLeaveHistoryDO);
    //        grdEmpLeaveHistory.DataBind();
    //        pnlgridview.Visible = true;

    //        if (grdEmpLeaveHistory.Rows.Count > 0)
    //        {

    //            TableCellCollection cells = grdEmpLeaveHistory.HeaderRow.Cells;
    //            grdEmpLeaveHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
    //            LabelEmpHistory.Visible = false;

    //        }

    //        return ObjLeaveHistoryDO.Resultdtset.Tables[0];

    //    }
    //    catch (Exception e) { throw e; }

    //}
    protected void grdEmpLeaveHistory_PreRender(object sender, EventArgs e)
    {
        grdEmpLeaveHistory.DataSource = GetEmpLeaveHistory();
        grdEmpLeaveHistory.DataBind();

        if (grdEmpLeaveHistory != null)
        {

            if (grdEmpLeaveHistory.Rows.Count > 0)
            {


                //Replace the <td> with <th> and adds the scope attribute
                grdEmpLeaveHistory.UseAccessibleHeader = true;

                if (grdEmpLeaveHistory.HeaderRow != null)
                {
                    //Adds the <thead> and <tbody> elements required for DataTables to work
                    grdEmpLeaveHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdEmpLeaveHistory.HeaderRow != null)
                {
                    //Adds the <tfoot> element required for DataTables to work
                    grdEmpLeaveHistory.FooterRow.TableSection = TableRowSection.TableFooter;
                }


             }
            else
            {
                LabelEmpHistory.Visible = true;
            }

        }
    }

    protected void btnGetEmpLeaveHistory_Click(object sender, EventArgs e)
    {

        
            GetEmpLeaveHistory();
            btnGO.Visible = true;
       

    }
    [WebMethod]
    public static string Validate_Holiday(string EmpId, string Startdate, string EndDate)
    {
        LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
        LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
        LeaveHistoryDO retresultdo = new LeaveHistoryDO();
        string ret = "";
        {

            ObjLeaveHistoryDO.EmpId = Convert.ToInt32("61");
            //ObjLeaveHistoryDO.StartTime = 
            //ObjLeaveHistoryDO.EndTime = 
            ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(Startdate);//, "MM-dd-yyyy", null);
            ObjLeaveHistoryDO.EndDate = Convert.ToDateTime(EndDate);//DateTime.ParseExact(EndDate, "MM-dd-yyyy", null));

            ObjLeaveHistoryDO = ObjLeaveHistoryBL.GetHolidayList(ObjLeaveHistoryDO);
            int HolidayList = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0]);
            //hdnlist.Value = HolidayList.ToString();
            ret = HolidayList.ToString();
        }
        return ret;
    }
    protected void LnklopCancel_Click(object sender, EventArgs e)
    {

    }

    protected void lopsubmit(object sender, EventArgs e) // Added by LOGESH
    {
        ObjLeaveHistoryDO.LopStartdate = Convert.ToDateTime(DateTime.ParseExact(lopstartdate.Text, "yyyy/MM/dd", null));
        ObjLeaveHistoryDO.LopEndDate = Convert.ToDateTime(DateTime.ParseExact(lopenddate.Text, "yyyy/MM/dd", null));
        ObjLeaveHistoryDO.LopAction = lopactiontaken.Text;
        ObjLeaveHistoryDO.LopTimesheethrs = float.Parse(loptimshthrs.Text);
        ObjLeaveHistoryDO.Lopleavehrs = float.Parse(lopleavehrs.Text);
        //ObjLeaveHistoryDO.Noofdays = Int32.Parse(No_of_Days.Text);
        ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
        ObjLeaveHistoryDO.Rptmgr = Convert.ToInt32(Session["RptMgrId"]);
        ObjLeaveHistoryDO.Status = 0;   // 0-Pending ,1-Approved ,2 -Reject
        ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(Session["LeaveID"]);
        ObjLeaveHistoryBL.Insertlopcancel(ObjLeaveHistoryDO);

        emplopcancel();
        // System.Threading.Thread.Sleep(5000);
        //GetLeaveHistory();
        //getLeavebalance();

        //Session["IsSubmit"] = 1;

        //URL = Request.Url.AbsoluteUri;
        //Response.Redirect(URL);

        //lopstartdate.Text;Flopsub

        //    lopenddate.Text;
        //    lopactiontaken.Text;
        //    .Text;
        //    lopleavehrs.Text;
    }

    protected void grdEmplopcancle_DataBound(object sender, EventArgs e)
    {

    }

    protected void grdEmplopcancle_PreRender(object sender, EventArgs e)
    {
        grdEmplopcancle.DataSource = emplopcancel();
        grdEmplopcancle.DataBind();
        if (grdEmplopcancle != null)
        {
            if (grdEmplopcancle.Rows.Count > 0)
            {
                emplopnorecord.Visible = false;

                //Replace the <td> with <th> and adds the scope attribute
                grdEmplopcancle.UseAccessibleHeader = true;

                if (grdEmplopcancle.HeaderRow != null)
                {
                    //Adds the <thead> and <tbody> elements required for DataTables to work
                    grdEmplopcancle.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                if (grdEmplopcancle.HeaderRow != null)
                {
                    //Adds the <tfoot> element required for DataTables to work
                    grdEmplopcancle.FooterRow.TableSection = TableRowSection.TableFooter;
                }
            }
            else
            {
                lopApproveBtn.Visible = false;
                lopRejectBtn.Visible = false;
            }
        }
    }

    public object emplopcancel()
    {
        try
        {
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindemplopcancel(ObjLeaveHistoryDO);
            grdEmplopcancle.DataBind();
            if (grdEmplopcancle.Rows.Count > 0)
            {
                TableCellCollection cells = grdEmplopcancle.HeaderRow.Cells;
                grdEmplopcancle.HeaderRow.TableSection = TableRowSection.TableHeader;
                // LabelHistory.Visible = false;
            }
            return ObjLeaveHistoryDO.Resultdtset.Tables[0];
        }
        catch (Exception e) { throw e; }
    }

    protected void lopapprove_Click(object sender, EventArgs e)
    {
        string Status = string.Empty;
        //ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);
        //goto  exit;
        if (lopvalidate() == true)
        {
            foreach (GridViewRow item in grdEmplopcancle.Rows)
            {
                CheckBox chkbox = new CheckBox();
                string cmt = string.Empty;

                HiddenField hfdemp = new HiddenField();
                hfdemp = ((HiddenField)item.FindControl("lophfdEmpid"));

                HiddenField hfdEmpLeaveApplicationId = new HiddenField();
                hfdEmpLeaveApplicationId = ((HiddenField)item.FindControl("lophdnleaveappid"));

                HiddenField hdfleaveId = new HiddenField();
                hdfleaveId = ((HiddenField)item.FindControl("lophdnleaveid"));

                HiddenField hdfYearID = new HiddenField();
                hdfYearID = ((HiddenField)item.FindControl("lophfdYear"));

                //HiddenField hdfStatusCode = new HiddenField();
                //hdfStatusCode = ((HiddenField)item.FindControl("lophfdStatusCode"));

                chkbox = ((CheckBox)item.FindControl("lopchkselect"));

                if (chkbox.Checked == true)
                {
                    cmt = ((TextBox)item.FindControl("lbllopComment")).Text.Trim();
                    ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                    ObjLeaveHistoryDO.Status = 1;
                    ObjLeaveHistoryDO.Comments = cmt;
                    ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(hfdEmpLeaveApplicationId.Value);
                    //ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfleaveId.Value);
                    //ObjLeaveHistoryDO.YearId= Convert.ToInt32(hdfYearID.Value);
                    // ObjLeaveHistoryDO.Statuscode= Convert.ToByte(hdfStatusCode.Value);
                    //ObjLeaveHistoryBL.LeaveDecisionMail(ObjLeaveHistoryDO);
                    ObjLeaveHistoryBL.LopStatusApprove(ObjLeaveHistoryDO);
                }
            }
        }
    }

    private bool lopvalidate()
    {
        bool flag = false;
        foreach (GridViewRow gvrow in grdEmplopcancle.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("lopchkselect");
            if (chk.Checked)
            {
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select atleast one checkbox');", true);
            ShowMessage("Please select the Records to be Actioned", MessageType.Error);
        }
        return flag;
    }

    protected void lopRejectBtn_Click(object sender, EventArgs e)
    {
        string Status = string.Empty;
        //ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);
        //goto  exit;
        if (lopvalidate() == true)
        {
            foreach (GridViewRow item in grdEmplopcancle.Rows)
            {
                CheckBox chkbox = new CheckBox();
                string cmt = string.Empty;

                HiddenField hfdemp = new HiddenField();
                hfdemp = ((HiddenField)item.FindControl("lophfdEmpid"));

                HiddenField hfdEmpLeaveApplicationId = new HiddenField();
                hfdEmpLeaveApplicationId = ((HiddenField)item.FindControl("lophdnleaveappid"));

                HiddenField hdfleaveId = new HiddenField();
                hdfleaveId = ((HiddenField)item.FindControl("lophdnleaveid"));

                HiddenField hdfYearID = new HiddenField();
                hdfYearID = ((HiddenField)item.FindControl("lophfdYear"));

                //HiddenField hdfStatusCode = new HiddenField();
                //hdfStatusCode = ((HiddenField)item.FindControl("lophfdStatusCode"));

                chkbox = ((CheckBox)item.FindControl("lopchkselect"));

                if (chkbox.Checked == true)
                {
                    cmt = ((TextBox)item.FindControl("lbllopComment")).Text.Trim();
                    if (cmt == "")
                    {
                        ShowMessage("Please enter Your comment for Cancel", MessageType.Error);
                        return;
                    }
                    ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdemp.Value);
                    ObjLeaveHistoryDO.Status = 2;
                    ObjLeaveHistoryDO.Comments = cmt;
                    ObjLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(hfdEmpLeaveApplicationId.Value);
                    //ObjLeaveHistoryDO.LeaveID = Convert.ToInt32(hdfleaveId.Value);
                    //ObjLeaveHistoryDO.YearId = Convert.ToInt32(hdfYearID.Value);
                    // ObjLeaveHistoryDO.Statuscode= Convert.ToByte(hdfStatusCode.Value);
                    //ObjLeaveHistoryBL.LeaveDecisionMail(ObjLeaveHistoryDO);
                    ObjLeaveHistoryBL.LopStatusApprove(ObjLeaveHistoryDO);
                }
            }
        }
    }
    protected void grdLeaveHistory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void Getcompensationholidaylist() //ADDED BY SOPHIA //
    {
        try
        {

            //DataSet ds = new DataSet();
            ObjLeaveHistoryDO.EmpId = EmpId;
            retresultdo = ObjLeaveHistoryBL.bindddlcompensationholidaylist(ObjLeaveHistoryDO);
            DropDownselectdates.DataSource = retresultdo.Resultdtset;
            DropDownselectdates.DataTextField = "Date";
            DropDownselectdates.DataValueField = "Date_";
            DropDownselectdates.DataBind();
            //DropDownselectdates.Items.Insert(0, "  -Select- ");

            DropDownselectholidays.DataSource = retresultdo.Resultdtset.Tables[1];
            DropDownselectholidays.DataTextField = "Date";
            DropDownselectholidays.DataValueField = "Date_";
            DropDownselectholidays.DataBind();
            // DropDownselectholidays.Items.Insert(0, "  -Select-  ");



        } //Till here
        catch (Exception ex)
        {
            throw ex;
        }
    }
}