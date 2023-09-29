using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq; //sof//
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq; //sof//
using AnalyticBrainsBL;
using AnalyticBrainsDO;
using System .IO;
using System.Text;

public partial class Includes_WebForm_ApprovalTimesheet : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();

    ResultDO retresultdo = new ResultDO();

    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public int type = 0;
    public enum MessageType { Success, Error, Info, Warning };
    Decimal total = 0;
    public int result = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        //btnSendmail.Enabled = true;
        if (Request.QueryString["sessionid"] != null)
        {
            sessionid = Request.QueryString["sessionid"].ToString();
        }
        if (Request.QueryString["Username"] != null)
        {
            Username = Request.QueryString["Username"].ToString();
        }
        if (Request.QueryString["EmpId"] != null)
        {
            EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
        }
        this.txtdate.Text = Request[this.txtdate.UniqueID];  //s//
        this.txtFromdate.Text = Request[this.txtFromdate.UniqueID];
        this.txtTodate.Text = Request[this.txtTodate.UniqueID]; //s//

        if (!IsPostBack)

       
        {
            type = 1;
            DateTime dt = DateTime.Now;
            txtdate.Text = dt.ToString("dd-MM-yyyy"); //s//
            //txtdate.Text = dt.ToString("dd-MM-yyyy");
            //txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            ddlmonth_.SelectedValue = dt.Month.ToString();

            rbtnBymonth.Checked = true;
            pnlday.Visible = false;
            PnlMonth.Visible = true;
            pnlRange.Visible = false;
            pnlgridview.Visible = false;
            BindYears();

            ddlmonth_.SelectedValue = dt.Month.ToString();
            ddlyear.SelectedValue = dt.Year.ToString();
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.onload= scrolldown(){window.scrollTo(0,document.body.scrollHeight)};", true);

           // ClientScript.RegisterStartupScript(GetType(),"ScrollScript", "document.getElementById('btnview1').scrollIntoVie‌​w(true)", true);

           // RegisterStartupScript("ScrollScript", "document.getElementById('objectId').scrollIntoView(true);");

        }
        //btnview1.Focus();

        if (rbtnBydate.Checked == true)
        {
            type = 1;
            btnview1.Visible = true;
            btnview1.Focus();   //ADDED BY SOPHIA//
           // grdTimesheet.Focus(); 
            
        }
        if (rbtnByRange.Checked == true)
        {
            type = 2;
            btnWeeklyStatus.Visible = true;
            btnWeeklyStatus.Focus();  //ADDED BY SOPHIA// 
           // grdTimesheet.Focus();

           // btnview1.Focus();
        }
        if (rbtnBymonth.Checked == true)
        {
            type = 3;
            btnview1.Visible = true;
            btnview1.Focus();   //ADDED BY SOPHIA//
           // grdTimesheet.Focus();

        }
      
        

        if (hdnPageLoad.Value == "0")
        {
            GetApprovalTimesheet();    //ADDED BY SOPHIA//
            GetEmpName();
            hdnPageLoad.Value = "1";
           
        }




       

    }

  


    protected void grdTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[11].Attributes.Add("onClick", "return confirm('Do you want to delete this record?');");
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Hours"));
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                e.Row.Cells[6].Attributes.Add("style", "words-break:break-all;word-wrap:break-word");
                e.Row.Cells[7].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblhours = (Label)e.Row.FindControl("lblTotal");
                lblhours.Text = total.ToString();

                if (type == 1)
                {
                    if (total >= 24)
                    {
                        lblhours.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Hours exceeds!')", true);
                        //btnSendmail.Enabled = false;

                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    ObjTimesheetDO.EmpId = EmpId;

                    ResultDO ds_ProjectList = ObjTimesheetBL.GetProjectList();
                    ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
                    ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
                    ResultDO ds_Status = ObjTimesheetBL.Getstatus();
                    DataRowView dr = e.Row.DataItem as DataRowView;

                    DropDownList ddlProject = (DropDownList)e.Row.FindControl("ddlProject");
                    ddlProject.DataSource = ds_ProjectList.Resultdtset;
                    ddlProject.DataValueField = "ProjectId";
                    ddlProject.DataTextField = "ProjectName";
                    ddlProject.DataBind();
                    ddlProject.Items.Insert(0, "--Select--");

                    ddlProject.SelectedValue = dr["ProjectId"].ToString();

                    DropDownList ddlModuleList = (DropDownList)e.Row.FindControl("ddlModuleList");
                    ObjTimesheetDO.EmpId = EmpId;
                    ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
                    ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);

                    ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
                    ddlModuleList.DataValueField = "ModuleId";
                    ddlModuleList.DataTextField = "ModuleName";
                    ddlModuleList.DataBind();
                    ddlModuleList.Items.Insert(0, "--Select--");
                    ddlModuleList.SelectedValue = dr["ModuleId"].ToString();

                    DropDownList ddlTask = (DropDownList)e.Row.FindControl("ddlTask");
                    ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
                    ObjTimesheetDO.ModuleId = Convert.ToInt32(ddlModuleList.SelectedValue.ToString());
                    ObjTimesheetDO.inputvalue = 2;
                    ObjTimesheetDO.EmpId = EmpId;
                    ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
                    ddlTask.DataSource = ds_task.Resultdtset;
                    ddlTask.DataValueField = "TaskId";
                    ddlTask.DataTextField = "TaskName";
                    ddlTask.DataBind();
                    ddlTask.Items.Insert(0, "--Select--");
                    ddlTask.SelectedValue = dr["TaskId"].ToString();

                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    ddlStatus.DataSource = ds_Status.Resultdtset;
                    ddlStatus.DataValueField = "TypeOptionID";
                    ddlStatus.DataTextField = "TypeName";
                    ddlStatus.DataBind();
                    ddlStatus.SelectedValue = dr["StatusId"].ToString();
                }
            }
            //ClientScript.RegisterStartupScript(GetType(), "ScrollScript", "document.getElementById('grdTimesheet').scrollIntoVie‌​w(true)", true); //toscroll bottom page
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //try
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox chkbox = (CheckBox)e.Row.FindControl("chkSelect");
        //        HiddenField hdfStatus = (HiddenField)e.Row.FindControl("hdnHours");
        //        HiddenField hdnchkselect = (HiddenField)e.Row.FindControl("hdnchkselect");
        //        HiddenField hdnStartdate = ((HiddenField)e.Row.FindControl("hdnSdate"));
        //        var date = DateTime.Now;
        //        DateTime mondayOfLastWeek = date.AddDays(-(int)date.DayOfWeek - 6).Date;
        //        if (hdnchkselect.Value == "10" || hdnchkselect.Value == "53")
        //        {
        //            if ((Convert.ToDouble(hdfStatus.Value) < 20) && (hdnStartdate.Value.ToString() == mondayOfLastWeek.ToString()))
        //            {

        //                e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                //e.Row.Cells[4].Attributes.CssStyle[HtmlTextWriterStyle.Color] = "Red";
        //                e.Row.Cells[4].BackColor = System.Drawing.Color.Pink;
        //                chkbox.Enabled = false;
        //            }
        //            else
        //            {
        //                e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                // e.Row.Cells[5].Attributes.Add("style", "words-break:break-all;word-wrap:break-word");
        //                chkbox.Enabled = true;
        //            }
        //        }
        //        else
        //        {
        //            if ((Convert.ToDouble(hdfStatus.Value) < Convert.ToInt32(retresultdo.Resultdtset.Tables[1].Rows[0][0].ToString())) && (hdnStartdate.Value.ToString() == mondayOfLastWeek.ToString()))
        //            {
        //                e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                //e.Row.Cells[4].Attributes.CssStyle[HtmlTextWriterStyle.Color] = "Red";
        //                e.Row.Cells[4].BackColor = System.Drawing.Color.Pink;
        //                chkbox.Enabled = false;
        //            }
        //            else
        //            {
        //                e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
        //                // e.Row.Cells[5].Attributes.Add("style", "words-break:break-all;word-wrap:break-word");
        //                chkbox.Enabled = true;
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    //protected void btnSendmail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ObjTimesheetDO.EmpId = EmpId;
    //        ObjTimesheetBL.SendMail(ObjTimesheetDO);
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your mail has been sent successfully!')", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //base.VerifyRenderingInServerForm(control);
    //}
    protected void grdApprovalTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
    try
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkbox = (CheckBox)e.Row.FindControl("chkSelect");
            HiddenField hdfStatus = (HiddenField)e.Row.FindControl("hdnHours");
            HiddenField hdnchkselect = (HiddenField)e.Row.FindControl("hdnchkselect");
            HiddenField hdnStartdate = ((HiddenField)e.Row.FindControl("hdnSdate"));
            var date = DateTime.Now;
            DateTime mondayOfLastWeek = date.AddDays(-(int)date.DayOfWeek - 6).Date;
            if (hdnchkselect.Value == "10" || hdnchkselect.Value == "53")
            {
                if ((Convert.ToDouble(hdfStatus.Value) < 20) && (hdnStartdate.Value.ToString() == mondayOfLastWeek.ToString()))
                {

                    e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    //e.Row.Cells[4].Attributes.CssStyle[HtmlTextWriterStyle.Color] = "Red";
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Pink;
                    chkbox.Enabled = false;
                }
                else
                {
                    e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    // e.Row.Cells[5].Attributes.Add("style", "words-break:break-all;word-wrap:break-word");
                    chkbox.Enabled = true;
                }
            }
            else
            {
                if ((Convert.ToDouble(hdfStatus.Value) < Convert.ToInt32(retresultdo.Resultdtset.Tables[1].Rows[0][0].ToString())) && (hdnStartdate.Value.ToString() == mondayOfLastWeek.ToString()))
                {
                    e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    //e.Row.Cells[4].Attributes.CssStyle[HtmlTextWriterStyle.Color] = "Red";
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Pink;
                    chkbox.Enabled = false;
                }
                else
                {
                    e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[2].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word");
                    // e.Row.Cells[5].Attributes.Add("style", "words-break:break-all;word-wrap:break-word");
                    chkbox.Enabled = true;
                }
            }
        }
    }
    catch (Exception ex)
    {
        throw ex;
    }
    
    
    
    
    }

    public void GetApprovalTimesheet()
    {
        try
        {
            DateTime lowerbound = new DateTime(1900, 1, 1);
            DataSet dts = new DataSet();
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.ApprovalTimesheet(ObjTimesheetDO);
            Gettempdataset();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        }
    public void Gettempdataset1()// modified for binding the dataset into the grid
    {
        try
        {
            if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
            {
                grdTimesheet.DataSource = retresultdo.Resultdtset.Tables[0];
                grdTimesheet.EditIndex = -1;
                grdTimesheet.DataBind();
                pnlgridview.Visible = true;
            }
            else
            {
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('No data found.')", true);
                ShowMessage("No data found.", MessageType.Info);  //ADDED BY SOPHIA//
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrollWin();", true);  //ADDED BY SOPHIA//
                pnlgridview.Visible = false;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrolldown1();", true);  //ADDED BY SOPHIA//
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrollToDiv();", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void Gettempdataset()// modified for binding the dataset into the grid
    {
        try
        {
            if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
            {
                grdApprovalTimesheet.DataSource = retresultdo.Resultdtset.Tables[0];

                grdApprovalTimesheet.EditIndex = -1;

                grdApprovalTimesheet.DataBind();

                pnlgridview.Visible = true;
            }
            else
            {
                 //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('No data found.')", true);
                 
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(''No data found.')", true);
                // pnlgridview.Visible = false;
                lblgrdtimesheet.Visible = true;
                 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);

    }
    protected void Approve_Click(object sender, EventArgs e)
    {
        try
        {
            string Status = string.Empty;
            string StingId = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "CheckboxIsChecked", "ClientCheck();", true);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //goto  exit;
            if (validate() == true)
            {
                foreach (GridViewRow item in grdApprovalTimesheet.Rows)
                {
                    CheckBox chkbox = new CheckBox();
                    chkbox = ((CheckBox)item.FindControl("chkselect"));

                    HiddenField hdnEmp = new HiddenField();
                    hdnEmp = ((HiddenField)item.FindControl("hdnchkselect"));

                    HiddenField hdnStartdate = new HiddenField();
                    hdnStartdate = ((HiddenField)item.FindControl("hdnSdate"));

                    HiddenField hfdEnddate = new HiddenField();
                    hfdEnddate = ((HiddenField)item.FindControl("hdnEdate"));

                    if (chkbox.Checked == true)
                    {

                        ObjTimesheetDO.EmpId = Convert.ToInt32(hdnEmp.Value);
                        ObjTimesheetDO.Status = 1;

                        ObjTimesheetDO.FromDate = Convert.ToDateTime(hdnStartdate.Value);
                        ObjTimesheetDO.ToDate = Convert.ToDateTime(hfdEnddate.Value);

                        int RtnId = Convert.ToInt32(ObjTimesheetBL.Approval(ObjTimesheetDO).ToString());
                        //int RtnId = Convert.ToInt32(retresultdo.Resultdtset.Tables[0].Rows[0][0].ToString());

                        if (sb.Length > 0)
                        {
                            sb.Append(',');
                        }

                        // in its first pass it will take the first string , then others string: because this line must execute  

                        sb.Append(RtnId);
                        //ShowMessage("Your Approval mail has been sent successfully", MessageType.Info);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Selected Timesheets are Approved!')", true);
                    }
                }
                GetApprovalTimesheet();
            }
            if (grdApprovalTimesheet.Rows.Count > 0)
            {
                InitEmail(sb.ToString());
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

    public void InitEmail(string sb)
    {
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.TaskName = sb;
        ObjTimesheetBL.Approval_mail(ObjTimesheetDO);
    }

    private bool validate()
    {
        bool flag = false;
        foreach (GridViewRow gvrow in grdApprovalTimesheet.Rows)
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
    protected void grdLeaveHistory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Bydate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            btnview1.Visible = true;//
            rbtnByRange.Checked = false;
            rbtnBymonth.Checked = false;
            pnlday.Visible = true;
            pnlRange.Visible = false;
            PnlMonth.Visible = false;
            pnlgridview.Visible = false;
            btnWeeklyStatus.Visible = false;
            grdTimesheet.DataSource = null;//Added newly
            grdTimesheet.DataBind();
            GetApprovalTimesheet();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rbtnByRange_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;
            DateTime StartOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            txtFromdate.Text = StartOfWeek.ToString("dd-MM-yyyy");
            //txtFromdate.Text = StartOfWeek.ToString("dd/MM/yyyy");
            txtTodate.Text = dt.ToString("dd-MM-yyyy");
            //txtTodate.Text = dt.ToString("dd/MM/yyyy");
            rbtnBydate.Checked = false;
            rbtnBymonth.Checked = false;
            pnlday.Visible = false;
            pnlRange.Visible = true;
            PnlMonth.Visible = false;
            pnlgridview.Visible = false;
            btnWeeklyStatus.Visible = true;
            btnview1.Visible = false;
            // btnSendmail.Visible = false;
            //btnExportToExcel.Visible = false;
            grdTimesheet.DataSource = null;//Added newly
            grdTimesheet.DataBind();

            GetApprovalTimesheet();
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }
    protected void Bymonth_CheckedChanged1(object sender, EventArgs e)
    {
        try
        {

            rbtnBydate.Checked = false;
            rbtnByRange.Checked = false;
            pnlday.Visible = false;
            pnlRange.Visible = false;
            PnlMonth.Visible = true;
            pnlgridview.Visible = false;
            btnWeeklyStatus.Visible = false;
            btnview1.Visible = true;//
            // btnSendmail.Visible = false;
            //  btnExportToExcel.Visible = false;
            BindYears();
            DateTime dt = DateTime.Now;
            ddlmonth_.SelectedValue = dt.Month.ToString();
            ddlyear.SelectedValue = dt.Year.ToString();
            grdTimesheet.DataSource = null;//Added newly
            grdTimesheet.DataBind();
            GetApprovalTimesheet();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void month()
    {
        rbtnBymonth.Checked = true;
        rbtnBydate.Checked = false;
        rbtnByRange.Checked = false;
        pnlday.Visible = false;
        pnlRange.Visible = false;
        PnlMonth.Visible = true;
        pnlgridview.Visible = false;
        //btnWeeklyStatus.Visible = false;
        //btnview1.Visible = true;


        // btnSendmail.Visible = false;
        //  btnExportToExcel.Visible = false;
        BindYears();
        DateTime dt = DateTime.Now;
        //ddlmonth_.SelectedValue = dt.Month.ToString();
        // ddlmonth_.SelectedValue = dt.Month.ToString(); //"5"//
        ddlyear.SelectedValue = dt.Year.ToString();
    }
    protected void BindYears()
    {
        try
        {
            retresultdo = ObjTimesheetBL.GetYearList();
            ddlyear.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlyear.DataValueField = "years";
            ddlyear.DataTextField = "years";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {

            if (ddlSelectEmpName.SelectedIndex == 0)    //ADDED BY SOPHIA//
            {
                
                ShowMessage("Please Select Employee Name", MessageType.Error);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrollWin();", true);
                grdTimesheet.DataBind();
                return; 


            }

            else
            {
                //btnWeeklyStatus.Visible = true;
                btnview1.Visible = true;
            }
           
            if (rbtnBydate.Checked == true)
            {
                type = 1;
                GetEmpTimesheet();
                pnlday.Visible = true;

                DateTime SelectedDate = Convert.ToDateTime(DateTime.ParseExact(txtdate.Text, "dd-MM-yyyy", null));

                //DateTime SelectedDate = Convert.ToDateTime(txtdate.Text);
                DateTime Today = DateTime.Today.Date;
                TimeSpan diff = Today.Subtract(SelectedDate);

                if (diff.TotalDays <= 25)
                {
                    // btnSendmail.Visible = true;
                    grdTimesheet.Columns[9].Visible = true;
                    grdTimesheet.Columns[10].Visible = true;
                    grdTimesheet.Columns[11].Visible = true;
                }
                else
                {
                    // btnSendmail.Visible = false;
                    grdTimesheet.Columns[9].Visible = false;
                    grdTimesheet.Columns[10].Visible = false;
                }
                //btnExportToExcel.Visible = false;
                btnview1.Visible = true;
            }
            if (rbtnByRange.Checked == true)
            {
                type = 2;
                GetEmpTimesheet1();
                pnlRange.Visible = true;
                btnview1.Visible = false;
                grdTimesheet.Columns[9].Visible = true;
                grdTimesheet.Columns[10].Visible = false;
                //btnSendmail.Visible = false;
                //btnExportToExcel.Visible = true;
            }
            if (rbtnBymonth.Checked == true)
            {
                type = 3;
                GetEmpTimesheet();
                PnlMonth.Visible = true;
                btnview1.Visible = true;
                grdTimesheet.Columns[9].Visible = true;
                grdTimesheet.Columns[10].Visible = false;
                grdTimesheet.Columns[11].Visible = false;
                //btnSendmail.Visible = false;
                //btnExportToExcel.Visible = true;
                
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrollToDiv();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrolldown1();", true);    //ADDED BY SOPHIA//
            GetApprovalTimesheet();
           
           
            
        }

        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void btnWeeklyStatus_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlSelectEmpName.SelectedIndex == 0)    //ADDED BY SOPHIA//
            {

                ShowMessage("Please Select Employee Name", MessageType.Error);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrollWin();", true);
                grdTimesheet.DataBind();
                return;

            }
            else
            {
                btnWeeklyStatus.Visible = true;
                //btnview1.Visible = true;
            }
            
            if (rbtnBydate.Checked == true)
            {
                type = 1;
                GetEmpTimesheet1();
                pnlday.Visible = true;

                DateTime SelectedDate = Convert.ToDateTime(DateTime.ParseExact(txtdate.Text, "dd-MM-yyyy", null));
                DateTime Today = DateTime.Today.Date;
                TimeSpan diff = Today.Subtract(SelectedDate);

                if (diff.TotalDays <= 5)
                {
                    //btnSendmail.Visible = true;
                    grdTimesheet.Columns[9].Visible = true;
                    grdTimesheet.Columns[10].Visible = true;
                }
                else
                {
                    // btnSendmail.Visible = false;
                    grdTimesheet.Columns[9].Visible = false;
                    grdTimesheet.Columns[10].Visible = false;
                }
                //btnExportToExcel.Visible = false;
            }
            if (rbtnByRange.Checked == true)
            {
                type = 2;
                GetEmpTimesheet1();
                pnlRange.Visible = true;

                grdTimesheet.Columns[9].Visible = true;
                grdTimesheet.Columns[10].Visible = false;
                grdTimesheet.Columns[11].Visible = false;

                //btnSendmail.Visible = false;
                //btnExportToExcel.Visible = true;
            }
            if (rbtnBymonth.Checked == true)
            {
                type = 3;
                GetEmpTimesheet1();
                PnlMonth.Visible = true;

                grdTimesheet.Columns[9].Visible = true;
                grdTimesheet.Columns[10].Visible = false;
                grdTimesheet.Columns[11].Visible = false;

                //btnSendmail.Visible = false;
                //btnExportToExcel.Visible = true;
            }
            GetApprovalTimesheet();       //ADDED BY SOPHIA// 
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlProject_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlModuleList = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        DropDownList ddlTask = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlTask");
        ddlModuleList.Enabled = false;
        ddlTask.Enabled = false;
        ddlModuleList.Items.Clear();
        ddlTask.Items.Clear();
        ddlModuleList.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList ddlProject = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlProject");
        int ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : int.Parse(ddlProject.SelectedItem.Value));
        if (ProjectId > 0)
        {
            BindModule(rowindex);
        }
        ddlModuleList.Focus();
    }
    protected void ddlModuleList_SelectedIndexChanged1(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlModuleList = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        DropDownList ddlTask = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlTask");
        ddlTask.Enabled = false;
        ddlTask.Items.Clear();
        ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
        int ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : int.Parse(ddlModuleList.SelectedItem.Value));
        if (ModuleId > 0)
        {
            BindTask(rowindex);
        }
        ddlTask.Focus();
    }
    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;

    }


    public void GetEmpTimesheet()
    {
        try
        {
            DateTime lowerbound = new DateTime(1900, 1, 1);
            //DataSet dts = new DataSet();
            ResultDO retresultdo = new ResultDO();

            //To get the select empId from the DropDown
            int EmpId_ = Convert.ToInt32(Request.Form[ddlSelectEmpName.UniqueID]);
            ddlSelectEmpName.SelectedValue = EmpId_.ToString();     //ADDED BY SOPHIA//
   
            ObjTimesheetDO.EmpId = EmpId_;


            if (type == 1)
            {
                string viewtype = "day";

                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = lowerbound;
                // ObjTimesheetDO.ToDate = Convert.ToDateTime(txtdate.Text);
                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtdate.Text, "dd-MM-yyyy", null));
                retresultdo = ObjTimesheetBL.ViewEmpTimesheet(ObjTimesheetDO);
            }
            if (type == 2)
            {
                string viewtype = "range";

                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = Convert.ToDateTime(DateTime.ParseExact(txtFromdate.Text, "dd-MM-yyyy", null));
                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtTodate.Text, "dd-MM-yyyy", null));
                // ObjTimesheetDO.FromDate = Convert.ToDateTime(txtFromdate.Text);
                // ObjTimesheetDO.ToDate = Convert.ToDateTime(txtTodate.Text);
                retresultdo = ObjTimesheetBL.WeeklyStatus(ObjTimesheetDO);

            }
            if (type == 3)
            {
                string viewtype = "month";
                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = lowerbound;
                ObjTimesheetDO.ToDate = lowerbound;
                ObjTimesheetDO.Month = int.Parse(ddlmonth_.SelectedValue);
                ObjTimesheetDO.Year = int.Parse(ddlyear.SelectedItem.ToString());
                retresultdo = ObjTimesheetBL.ViewEmpTimesheet(ObjTimesheetDO);
            }
            if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
            {
                grdTimesheet.DataSource = retresultdo.Resultdtset.Tables[0];
                if (type == 3)
                {
                    grdTimesheet.EditIndex = -1;
                    grdTimesheet.DataBind();
                }
                else
                {
                    grdTimesheet.DataBind();
                }
                //grdTimesheet.DataBind();
                pnlgridview.Visible = true;
               
            }
            else
            {
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No data found.')", true);
                ShowMessage("No data found.", MessageType.Info);          //ADDED BY SOPHIA//
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "scrollDown", "scrollWin();", true);
               // ClientScript.RegisterStartupScript(this.GetType(), "prompt", "alert('No data found.')", true);
                btnview1.Visible = true;
                pnlgridview.Visible = false;
                grdTimesheet.DataBind();//to bind empty datas// Added by sophia//
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetEmpTimesheet1()
    {
        try
        {
            grdTimesheet.DataSource = null;//Added newly
            grdTimesheet.DataBind();
            DateTime lowerbound = new DateTime(1900, 1, 1);
            DataSet dts = new DataSet();
            int EmpId_ = Convert.ToInt32(Request.Form[ddlSelectEmpName.UniqueID]);   //ADDED BY SOPHIA//
            ddlSelectEmpName.SelectedValue = EmpId_.ToString();

            ObjTimesheetDO.EmpId = EmpId_;

            if (type == 1)
            {
                string viewtype = "day";

                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = lowerbound;
                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtdate.Text, "dd-MM-yyyy", null));
                retresultdo = ObjTimesheetBL.WeeklyStatus(ObjTimesheetDO);
                Gettempdataset1();//modified
            }
            if (type == 2)
            {
                string viewtype = "range";

                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = Convert.ToDateTime(DateTime.ParseExact(txtFromdate.Text, "dd-MM-yyyy", null));
                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtTodate.Text, "dd-MM-yyyy", null));
                //ObjTimesheetDO.FromDate = Convert.ToDateTime(txtFromdate.Text);
                //ObjTimesheetDO.ToDate = Convert.ToDateTime(txtTodate.Text);
                if (ObjTimesheetDO.FromDate < ObjTimesheetDO.ToDate)
                {
                    retresultdo = ObjTimesheetBL.WeeklyStatus(ObjTimesheetDO);
                    Gettempdataset1();//modified
                    btnview1.Visible = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('End Date must be greater than Start Date.')", true);
                    btnview1.Visible = false;
                    pnlgridview.Visible = false;
                }
            }
            if (type == 3)
            {
                string viewtype = "month";

                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = lowerbound;
                ObjTimesheetDO.ToDate = lowerbound;
                ObjTimesheetDO.Month = int.Parse(ddlmonth_.SelectedValue);
                retresultdo = ObjTimesheetBL.WeeklyStatus(ObjTimesheetDO);
                Gettempdataset1();//modified
                
            }
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindModule(int rowindex)
    {
        DropDownList ddlProject = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlProject");
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
        ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
        DropDownList ddlModuleList = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
        ddlModuleList.DataValueField = "ModuleId";
        ddlModuleList.DataTextField = "ModuleName";
        ddlModuleList.Enabled = true;
        ddlModuleList.DataBind();
        //ddlModuleList.SelectedValue = "ModuleName";
    }
    protected void BindTask(int rowindex)
    {
        retresultdo = new ResultDO();
        DropDownList ddlProject = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlProject");
        ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
        DropDownList ddlModuleList = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
        ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
        DropDownList ddlTask = (DropDownList)grdTimesheet.Rows[rowindex].FindControl("ddlTask");
        ddlTask.DataSource = ds_task.Resultdtset;
        ddlTask.DataValueField = "TaskId";
        ddlTask.DataTextField = "TaskName";
        ddlTask.Enabled = true;
        ddlTask.DataBind();
    }
    //protected void btnExportExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Timesheet.xls"));
    //        Response.ContentType = "application/ms-excel";
    //        Response.Charset = "";
    //        Response.ContentType = "application/vnd.xls";
    //        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    //        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    //        grdTimesheet.RenderControl(htmlWrite);
    //        Response.Write(stringWrite.ToString());
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    protected void grdTimesheet_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdTimesheet.EditIndex = -1;
            grdTimesheet.DataBind();
            GetEmpTimesheet();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdTimesheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Label lblRowID = (Label)grdTimesheet.Rows[e.RowIndex].FindControl("lblRowID");
            ObjTimesheetDO.RowId = Convert.ToInt32(lblRowID.Text);
            ObjTimesheetBL.DeleteTimeSheet(ObjTimesheetDO);
            DateTime lowerbound = new DateTime(1900, 1, 1);        // Modified for showing alert when deleting the the last row in the view time sheet 
            ResultDO retresultdo = new ResultDO();
            ObjTimesheetDO.EmpId = EmpId;
            if (type == 1)
            {
                string viewtype = "day";

                ObjTimesheetDO.Type = viewtype;
                ObjTimesheetDO.FromDate = lowerbound;
                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtdate.Text, "dd-MM-yyyy", null));
                retresultdo = ObjTimesheetBL.ViewEmpTimesheet(ObjTimesheetDO);
                if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
                {
                    grdTimesheet.DataSource = retresultdo.Resultdtset.Tables[0];
                    grdTimesheet.DataBind();
                    pnlgridview.Visible = true;
                }
                else
                {
                    pnlgridview.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdTimesheet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdTimesheet.EditIndex = e.NewEditIndex;
            grdTimesheet.DataBind();
            GetEmpTimesheet();
            // PopulateDDL(grdTimesheet.EditIndex);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdTimesheet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblRowID = (Label)grdTimesheet.Rows[e.RowIndex].FindControl("lblRowID");

            DropDownList ddlProject = (DropDownList)grdTimesheet.Rows[e.RowIndex].FindControl("ddlProject");
            DropDownList ddlModuleList = (DropDownList)grdTimesheet.Rows[e.RowIndex].FindControl("ddlModuleList");
            DropDownList ddlTask = (DropDownList)grdTimesheet.Rows[e.RowIndex].FindControl("ddlTask");
            DropDownList ddlStatus = (DropDownList)grdTimesheet.Rows[e.RowIndex].FindControl("ddlStatus");

            TextBox txtDescription = (TextBox)grdTimesheet.Rows[e.RowIndex].FindControl("txtDescription");
            TextBox txtIssues = (TextBox)grdTimesheet.Rows[e.RowIndex].FindControl("txtIssues");
            TextBox txtObject = (TextBox)grdTimesheet.Rows[e.RowIndex].FindControl("txtObject");
            TextBox txtHours = (TextBox)grdTimesheet.Rows[e.RowIndex].FindControl("txtHours");

            ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtdate.Text, "dd-MM-yyyy", null));
            //ObjTimesheetDO.ToDate = Convert.ToDateTime(txtdate.Text);
            ObjTimesheetDO.RowId = Convert.ToInt32(lblRowID.Text);
            ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
            ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
            ObjTimesheetDO.TaskId = ((ddlTask.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlTask.SelectedValue.ToString()));
            ObjTimesheetDO.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
            ObjTimesheetDO.Taskdesc = txtDescription.Text;
            ObjTimesheetDO.Issues = txtIssues.Text;
            ObjTimesheetDO.Object = txtObject.Text;
            ObjTimesheetDO.Hours = txtHours.Text;
            ObjTimesheetDO.EmpId = EmpId;
            //ObjTimesheetBL.UpdateTimesheet(ObjTimesheetDO);

            //grdTimesheet.EditIndex = -1;
            //GetEmpTimesheet();
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Updated successfully!')</script>");

            if (ddlProject.SelectedIndex == 0 || ddlModuleList.SelectedIndex == 0 || ddlTask.SelectedIndex == 0 || ddlStatus.SelectedIndex == 0 || txtHours.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Please fill the Mandatory Fields')</script>");
            }
            else
            {
                //   ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "Calculate();", true);
                ObjTimesheetBL.UpdateTimesheet(ObjTimesheetDO);

                grdTimesheet.EditIndex = -1;
                GetEmpTimesheet();

                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Updated successfully!')</script>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetEmpName()
    {
        try
        {

            //DataSet ds = new DataSet();
            ObjTimesheetDO.EmpId = EmpId;
            //ObjLeaveHistoryDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.bindddlSelectEmpName(ObjTimesheetDO);
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






}
