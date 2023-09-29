using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using Ajax;



public partial class Includes_WebForm_Permission : System.Web.UI.Page
{

    LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    LeaveHistoryDO retresultdo = new LeaveHistoryDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    int GetCount = 0;
    public enum MessageType { Success, Error, Info, Warning };

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
                    //if (EmpId == 31 || EmpId == 48) { decision.Visible = true; } 
                }
                GetPermissionHistory();
                GetRptmgr();
                GetPermissionApprovals();
                pnlgridview.Visible = false;
                GetEmpName();
                
                //CalendarStartDate.SelectedDate = DateTime.Today;
                //CalendarEndDate.SelectedDate = DateTime.Today;

                //clndrtxtDate.SelectedDate = DateTime.Today;
                //clndrtxtEndDate.SelectedDate = DateTime.Today;

                int IsSubmit = Convert.ToInt32(Session["IsSubmit"]);
                if (IsSubmit == 1)
                {
                    ShowMessage("Permission has been Applied successfully", MessageType.Info);
                }
                GetCount = CheckCredential(Session["Username"].ToString());
                if (GetCount > 0)
                {
                    PDecision.Visible = true;
                    PerEmpHistory.Visible = true;
                }
                else
                {
                    PDecision.Visible = false;
                    PerEmpHistory.Visible = false;
                }
                Session["IsSubmit"] = 0;

                txtStartdate.Attributes.Add("readonly", "readonly");
                // txtEnddate.Attributes.Add("readonly", "readonly");
                Noofdays.Attributes.Add("readonly", "readonly");
                textStartDate1.Attributes.Add("readonly", "readonly");
                //  textEndDate1.Attributes.Add("readonly", "readonly");
                TextNoDays.Attributes.Add("readonly", "readonly");

            }

        }
        catch
        {
            Response.Redirect("Login.aspx");
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

        }
    }



    public object GetPermissionHistory()
    {
        try
        {
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdPermissionHistory(ObjLeaveHistoryDO);

            grdPermissionHistory.DataBind();

            if (grdPermissionHistory.Rows.Count > 0)
            {
                TableCellCollection cells = grdPermissionHistory.HeaderRow.Cells;
                grdPermissionHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            return ObjLeaveHistoryDO.Resultdtset.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void grdPermissionHistory_PreRender(object sender, EventArgs e)
    {
        grdPermissionHistory.DataSource = GetPermissionHistory();
        grdPermissionHistory.DataBind();
        if (grdPermissionHistory != null)
        {
            if (grdPermissionHistory.Rows.Count > 0)
            {
                grdPermissionHistory.UseAccessibleHeader = true;
            }
            if (grdPermissionHistory.HeaderRow != null)
            {
                grdPermissionHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (grdPermissionHistory.FooterRow != null)
            {
                grdPermissionHistory.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            else
            {
                LabelPermissionHistory.Visible = true;
            }
        }
    }

    protected void grdPermissionHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {

                DataRowView dr = e.Row.DataItem as DataRowView;

                HiddenField hdfPStatus = (HiddenField)e.Row.FindControl("hdfPermissionApplicationID");
                LinkButton LnkPCancel = (LinkButton)e.Row.FindControl("LnkPCancel");
                if (hdfPStatus.Value == "Cancelled" || hdfPStatus.Value == "Rejected" || hdfPStatus.Value == "Cancel Rejected" || hdfPStatus.Value == "Cancel Approved")
                {
                    LnkPCancel.Enabled = false;
                    LnkPCancel.Attributes.Remove("href");
                    LnkPCancel.Attributes.Remove("onmouseover");
                    LnkPCancel.Attributes.Remove("onmouseout");
                    LnkPCancel.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
                    LnkPCancel.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
                    LnkPCancel.Attributes.Add("onmouseover", LnkPCancel.ToolTip = "Cancelled!!");
                }


            }
        }
    }

    protected void grdPermissionHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "LnkPCancel")
        {
            try
            {
                int result = 0;
                int index = Convert.ToInt32(e.CommandArgument);
                LeaveHistoryDAL objLeaveHistoryDAL = new LeaveHistoryDAL();
                ObjLeaveHistoryDO.PermissionApplicationID = index;
                ObjLeaveHistoryDO = objLeaveHistoryDAL.CancelPermission_GetStatus(ObjLeaveHistoryDO);
                result = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0]);


                if (result == 0)
                {
                    objLeaveHistoryDAL.CancelBackdatedPermission(ObjLeaveHistoryDO);
                    //objLeaveHistoryDAL.CancelPermission(ObjLeaveHistoryDO,result);
                    ShowMessage("Permission Cancellation Request has been Sent to the Reporting Manager", MessageType.Info);
                }
                else if (result == 1)
                {

                    objLeaveHistoryDAL.CancelPermission(ObjLeaveHistoryDO, result);

                    ShowMessage("Permission has been Cancelled Successfully ", MessageType.Info);
                }
                else { ShowMessage("You are Not Allowed to Cancel this Leave", MessageType.Error); return; }


                GetPermissionHistory();
                GetPermissionApprovals();

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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string URL = "";

        try
        {
            GetPermissionCount(); //ADDED BY SOPHIA
            //if (hours == 0)
            //{
            //  //  ShowMessage("Please fill the fields", MessageType.Error);

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunction", "submithours();", true);
            //    return;
            //}

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunction", "submithours();", true);
            int hours = (Convert.ToInt32(hdnworkhours.Value));
            if (Convert.ToInt32(hdntestcount.Value) <= 2)
            {
                if (hours == 1)
                {
                    //DateTime pStartDate = Convert.ToDateTime(DateTime.ParseExact(txtStartdate.Text, "MM-dd-yyyy", null));
                    //DateTime pEndDate = Convert.ToDateTime(DateTime.ParseExact(txtEnddate.Text, "MM-dd-yyyy", null));


                    //if (pStartDate > pEndDate)
                    //    ShowMessage("End date should be > = Start date", MessageType.Error);
                    //    ClearPopup();
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MyPopup();", true);
                    //    return;
                    //}




                    ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(DateTime.ParseExact(txtStartdate.Text, "MM-dd-yyyy", null));
                    ObjLeaveHistoryDO.EndDate = Convert.ToDateTime(DateTime.ParseExact(txtStartdate.Text, "MM-dd-yyyy", null));


                    //  ObjLeaveHistoryDO.Duration = ddlPDuration.SelectedIndex;
                    ObjLeaveHistoryDO.Duration = 3;
                    ObjLeaveHistoryDO.Reason = txtPReason.Text;
                    ObjLeaveHistoryDO.StartTime = ddlFromTime.SelectedItem.Text;
                    ObjLeaveHistoryDO.EndTime = ddlToTime.SelectedItem.Text;
                    ObjLeaveHistoryDO.Noofdays = Convert.ToString(Noofdays.Text);

                    ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
                    ObjLeaveHistoryDO.Rptmgr = Convert.ToInt32(Session["RptMgrId"]);
                    ObjLeaveHistoryDO.Status = 0;
                    ObjLeaveHistoryDO.PermissionID = 1;//Convert.ToInt32(Session["PermissionID"]);

                    ObjLeaveHistoryBL.InsertPermissionApplication(ObjLeaveHistoryDO);

                    GetPermissionHistory();



                    Session["IsSubmit"] = 1;

                    URL = Request.Url.AbsoluteUri;
                    Response.Redirect(URL);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "message();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            //btnOk_Click
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
                //ShowMessage("Please fill the fields", MessageType.Error);
            }

        }


    }



    protected void btnProceedLOP_Click(object sender, EventArgs e)
    {
        string URL = "";

        try
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallFunction", "submithours();", true);
            int hours = (Convert.ToInt32(hdnworkhours.Value));

            if (hours == 1)
            {
                ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(DateTime.ParseExact(txtStartdate.Text, "MM-dd-yyyy", null));
                ObjLeaveHistoryDO.EndDate = Convert.ToDateTime(DateTime.ParseExact(txtStartdate.Text, "MM-dd-yyyy", null));


                //  ObjLeaveHistoryDO.Duration = ddlPDuration.SelectedIndex;
                ObjLeaveHistoryDO.Duration = 3;
                ObjLeaveHistoryDO.Reason = txtPReason.Text;
                ObjLeaveHistoryDO.StartTime = ddlFromTime.SelectedItem.Text;
                ObjLeaveHistoryDO.EndTime = ddlToTime.SelectedItem.Text;
                ObjLeaveHistoryDO.Noofdays = Convert.ToString(Noofdays.Text);

                ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
                ObjLeaveHistoryDO.Rptmgr = Convert.ToInt32(Session["RptMgrId"]);
                ObjLeaveHistoryDO.Status = 0;
                ObjLeaveHistoryDO.PermissionID = 1;//Convert.ToInt32(Session["PermissionID"]);

                ObjLeaveHistoryBL.InsertPermissionApplication(ObjLeaveHistoryDO);

                GetPermissionHistory();//Added by sophia



                Session["IsSubmit"] = 1;

                URL = Request.Url.AbsoluteUri;
                Response.Redirect(URL);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "message();", true);
            }
        }
        //btnOk_Click

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
                //ShowMessage("Please fill the fields", MessageType.Error);
            }

        }
    }
    protected void SubmittedFromHome_Click(object sender, EventArgs e)
    {

        string URL = "";
        try
        {


            //if (home == 0)
            //{

            //    ShowMessage("Please fill the fields", MessageType.Error);

            //    
            //    return;
            //}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Callmyfunction", "CallSubmit();", true);
            int home = (Convert.ToInt32(hdfnsubmithome.Value));

            if (home == 1)
            {

                //DateTime dStartDate = Convert.ToDateTime(DateTime.ParseExact(textStartDate1.Text, "MM-dd-yyyy", null));
                //DateTime dEndDate = Convert.ToDateTime(DateTime.ParseExact(textEndDate1.Text, "MM-dd-yyyy", null));

                //if (dStartDate > dEndDate)
                //{
                //    ShowMessage("End date should be > = Start date", MessageType.Error);
                //    ClearHomePopup();
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "MyModalPopup();", true);
                //    return;
                //}

                ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(DateTime.ParseExact(textStartDate1.Text, "MM/dd/yyyy", null));
                ObjLeaveHistoryDO.EndDate = Convert.ToDateTime(DateTime.ParseExact(textStartDate1.Text, "MM/dd/yyyy", null));
                ObjLeaveHistoryDO.StartTime = "00.00";
                ObjLeaveHistoryDO.EndTime = "00.00";
                ObjLeaveHistoryDO.Noofdays = Convert.ToString(TextNoDays.Text);
                ObjLeaveHistoryDO.Duration = ddlHomeDuration.SelectedIndex;
                ObjLeaveHistoryDO.Reason = TextReason.Text;

                ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
                ObjLeaveHistoryDO.Rptmgr = Convert.ToInt32(Session["RptMgrId"]);
                ObjLeaveHistoryDO.Status = 0;
                ObjLeaveHistoryDO.PermissionID = 2;//Convert.ToInt32(Session["PermissionID"]);

                ObjLeaveHistoryBL.InsertPermissionApplication(ObjLeaveHistoryDO);

                GetPermissionHistory();


                Session["IsSubmit"] = 1;

                URL = Request.Url.AbsoluteUri;
                Response.Redirect(URL);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "message();", true);
            }


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

    private void ClearPopup()
    {
        txtStartdate.Text = "";
        //  txtEnddate.Text = "";
        Noofdays.Text = "";
        txtPReason.Text = "";

    }

    private void ClearHomePopup()
    {

        textStartDate1.Text = "";
        //  textEndDate1.Text = "";
        TextReason.Text = "";
    }

    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

    }

    private void GetRptmgr()
    {

        try
        {
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.GetReportManagerName(ObjLeaveHistoryDO);
            // ObjLeaveHistoryDO.Rptmgr = reportmanagername;

            int RptMgrId = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][2]);
            Session["RptMgrId"] = RptMgrId;

            string RptMgrName = ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][3].ToString();

            lblRptmgrworkhrs.Text = RptMgrName;
            lblRptMgrHome.Text = RptMgrName;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public object GetPermissionApprovals()
    {
        try
        {
            //EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
            //Session["EmpId"] = EmpId;
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdPermissionApprovals(ObjLeaveHistoryDO);

            grdPermissionDecision.DataBind();

            if (grdPermissionDecision.Rows.Count > 0)
            {
                TableCellCollection cells = grdPermissionDecision.HeaderRow.Cells;
                grdPermissionDecision.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            return ObjLeaveHistoryDO.Resultdtset.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void grdPermissionDecision_PreRender(object sender, EventArgs e)
    {
        grdPermissionDecision.DataSource = GetPermissionApprovals();
        grdPermissionDecision.DataBind();
        if (PDecision.Visible == true)
        {
            if (grdPermissionDecision.Rows.Count > 0)
            {
                grdPermissionDecision.UseAccessibleHeader = true;
            }
            if (grdPermissionDecision.HeaderRow != null)
            {
                grdPermissionDecision.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (grdPermissionDecision.FooterRow != null)
            {
                grdPermissionDecision.FooterRow.TableSection = TableRowSection.TableFooter;
            }
            else
            {
                btnPer_Approve.Visible = false;
                btnPer_Reject.Visible = false;
                LabelPermissionDecision.Visible = true;
            }


        }
    }

    private bool validatechk()
    {

        bool flag = false;
        foreach (GridViewRow gvrow in grdPermissionDecision.Rows)
        {
            CheckBox Per_Chk = (CheckBox)gvrow.FindControl("chkPer_select");
            if (Per_Chk.Checked)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
        {

            ShowMessage("Please select the Records ", MessageType.Error);

        }
        return flag;

    }


    //protected void btnPer_ApprovebtnClick(Object sender, EventArgs e)
    protected void Per_ApprovebtnClick(object sender, EventArgs e)
    {
        try
        {
            string Status = string.Empty;
            //ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);
            if (validatechk() == true)
            {
                foreach (GridViewRow item in grdPermissionDecision.Rows)
                {
                    CheckBox chkbox = new CheckBox();
                    string cmts = string.Empty;
                    string SelectedPermissionApplicationId = "";
                    HiddenField hfd = new HiddenField();
                    hfd = ((HiddenField)item.FindControl("hdfPermissionAppID"));

                    HiddenField hfdempid = new HiddenField();
                    hfdempid = ((HiddenField)item.FindControl("hdfEmpId"));

                    HiddenField hfdPermissionId = new HiddenField();
                    hfdPermissionId = ((HiddenField)item.FindControl("hdfPerID"));

                    HiddenField hfdPerStatus = new HiddenField();
                    hfdPerStatus = ((HiddenField)item.FindControl("hdfPStatusCode"));

                    SelectedPermissionApplicationId = hfd.Value;
                    chkbox = ((CheckBox)item.FindControl("chkPer_select"));


                    if (chkbox.Checked == true)
                    {
                        cmts = ((TextBox)item.FindControl("txtPComments")).Text;
                        Status = ((Label)item.FindControl("lblStatus")).Text;

                        if (Status == "Cancelled")
                        {
                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdempid.Value);
                            ObjLeaveHistoryDO.PermissionID = Convert.ToInt32(hfdPermissionId.Value);
                            ObjLeaveHistoryDO.PermissionApplicationID = Convert.ToInt32(SelectedPermissionApplicationId);
                            ObjLeaveHistoryDO.Comments = cmts;
                            ObjLeaveHistoryDO.Status = 6;
                            ObjLeaveHistoryDO.Statuscode = Convert.ToByte(hfdPerStatus.Value);
                            ObjLeaveHistoryBL.PermissionStatusApprove(ObjLeaveHistoryDO);

                        }
                        else
                        {
                            ObjLeaveHistoryDO.PermissionApplicationID = Convert.ToInt32(SelectedPermissionApplicationId);
                            ObjLeaveHistoryDO.Status = 1;
                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdempid.Value);
                            ObjLeaveHistoryDO.PermissionID = Convert.ToInt32(hfdPermissionId.Value);

                            ObjLeaveHistoryDO.Comments = cmts;
                            ObjLeaveHistoryDO.Statuscode = Convert.ToByte(hfdPerStatus.Value);
                            ObjLeaveHistoryBL.PermissionStatusApprove(ObjLeaveHistoryDO);

                        }
                    }
                }


                if (Status == "Pending")
                {
                    ShowMessage("Permission Approved Successfully", MessageType.Info);
                }

                if (Status == "Cancelled")
                {
                    ShowMessage("Cancelled Permission as been Approved Successfully", MessageType.Info);

                }
                GetPermissionApprovals();
            }
        }
        catch (Exception ex)
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

    protected void Per_RejectbtnClick(object sender, EventArgs e)
    {
        try
        {
            string Status = string.Empty;
            // ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "Check();", true);
            if (validatechk() == true)
            {
                foreach (GridViewRow item in grdPermissionDecision.Rows)
                {
                    CheckBox chkbox = new CheckBox();
                    string cmts = string.Empty;
                    string SelectedPermissionApplicationId = "";
                    HiddenField hfd1 = new HiddenField();
                    hfd1 = ((HiddenField)item.FindControl("hdfPermissionAppID"));

                    HiddenField hfdempid_rej = new HiddenField();
                    hfdempid_rej = ((HiddenField)item.FindControl("hdfEmpId"));

                    HiddenField hfdRejPermissionId = new HiddenField();
                    hfdRejPermissionId = ((HiddenField)item.FindControl("hdfPerID"));

                    HiddenField hfdPerStatus2 = new HiddenField();
                    hfdPerStatus2 = ((HiddenField)item.FindControl("hdfPStatusCode"));

                    SelectedPermissionApplicationId = hfd1.Value;
                    chkbox = ((CheckBox)item.FindControl("chkPer_select"));

                    if (chkbox.Checked == true)
                    {

                        cmts = ((TextBox)item.FindControl("txtPComments")).Text;
                        Status = ((Label)item.FindControl("lblStatus")).Text;

                        if (cmts == "")
                        {
                            ShowMessage("Please Enter Comment", MessageType.Error);
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "myfunc", "Call();", true);
                            return;

                        }

                        if (Status == "Cancelled")
                        {
                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdempid_rej.Value);
                            ObjLeaveHistoryDO.PermissionID = Convert.ToInt32(hfdRejPermissionId.Value);
                            ObjLeaveHistoryDO.PermissionApplicationID = Convert.ToInt32(SelectedPermissionApplicationId);
                            ObjLeaveHistoryDO.Status = 5;
                            ObjLeaveHistoryDO.Comments = cmts;
                            ObjLeaveHistoryBL.PermissionCancelRejected(ObjLeaveHistoryDO);
                        }
                        else
                        {



                            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(hfdempid_rej.Value);
                            ObjLeaveHistoryDO.PermissionID = Convert.ToInt32(hfdRejPermissionId.Value);
                            ObjLeaveHistoryDO.PermissionApplicationID = Convert.ToInt32(SelectedPermissionApplicationId);

                            //ObjLeaveHistoryDO.PermissionApplicationId = Convert.ToInt32(SelectedPermissionApplicationId);
                            ObjLeaveHistoryDO.Status = 2;
                            ObjLeaveHistoryDO.Comments = cmts;
                            ObjLeaveHistoryDO.Statuscode = Convert.ToByte(hfdPerStatus2.Value);
                            ObjLeaveHistoryBL.PermissionStatusRejected(ObjLeaveHistoryDO);
                        }
                    }
                }

                if (Status == "Pending")
                {
                    ShowMessage("Permission as been Rejected Successfully", MessageType.Info);
                }
                if (Status == "Cancelled")
                {
                    ShowMessage("Cancelled Permission as been Rejected Successfully", MessageType.Info);
                }
                GetPermissionApprovals();
            }

        }
        catch (Exception ex)
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

    public void GetEmpName()
    {
        try
        {

            //DataSet ds = new DataSet();
            ObjLeaveHistoryDO.EmpId = EmpId;
            retresultdo = ObjLeaveHistoryBL.bindddlSelectEmpName(ObjLeaveHistoryDO);
            ddlSelectEmpName.DataSource = retresultdo.Resultdtset;
            ddlSelectEmpName.DataTextField = "Empname";
            ddlSelectEmpName.DataValueField = "EmpId";
            ddlSelectEmpName.DataBind();
            ddlSelectEmpName.Items.Insert(0, "  ---Select Employee---  ");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public object GetEmpPerHistory()
    {
        try
        {
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO.EmpId = ((ddlSelectEmpName.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlSelectEmpName.SelectedValue.ToString()));
            LeaveHistoryDO ds_EmpLeaveHistory = ObjLeaveHistoryBL.bindgrdEmpPerHistory(ObjLeaveHistoryDO);

            grdEmpPerHistory.DataSource = ds_EmpLeaveHistory.Resultdtset;
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindgrdEmpPerHistory(ObjLeaveHistoryDO);
            grdEmpPerHistory.DataBind();
            pnlgridview.Visible = true;

            if (grdEmpPerHistory.Rows.Count > 0)
            {
                TableCellCollection cells = grdEmpPerHistory.HeaderRow.Cells;
                grdEmpPerHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                LabelEmpPerHistory.Visible = false;
            }

            return ObjLeaveHistoryDO.Resultdtset.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void grdEmpPerHistory_PreRender(object sender, EventArgs e)
    {
        grdEmpPerHistory.DataSource = GetEmpPerHistory();
        grdEmpPerHistory.DataBind();

        if (grdEmpPerHistory != null)
        {
            if (grdEmpPerHistory.Rows.Count > 0)
            {
                grdEmpPerHistory.UseAccessibleHeader = true;
            }
            if (grdEmpPerHistory.HeaderRow != null)
            {
                grdEmpPerHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (grdEmpPerHistory.FooterRow != null)
            {
                grdEmpPerHistory.FooterRow.TableSection = TableRowSection.TableFooter;
            }

            else
            {
                LabelEmpPerHistory.Visible = true;
            }
        }
    }

    protected void btnGetEmpPerHistory_Click(object sender, EventArgs e)
    {

        if (ddlSelectEmpName.SelectedIndex == 0)
        {
            LabelEmpPerHistory.Visible = false;
            ShowMessage("Please select the employee", MessageType.Error);


        }
        else
        {
            GetEmpPerHistory();
            btnGO.Visible = true;
        }

    }
    public void GetPermissionCount()
    {
        try
        {

            //DataSet ds = new DataSet();
            ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            ObjLeaveHistoryDO.Startdate = Convert.ToDateTime(DateTime.ParseExact(txtStartdate.Text, "MM-dd-yyyy", null));
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.GetPermissionCount(ObjLeaveHistoryDO);
            int PermissionCount = Convert.ToInt32(ObjLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0]);
            hdntestcount.Value = PermissionCount.ToString();
           

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}