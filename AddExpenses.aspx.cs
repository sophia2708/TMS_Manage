using System;
using System.Collections;
using System.Configuration;
//using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using LeaveManagementBL;
using LeaveManagementDAL;
using LeaveManagementDO;
using AnalyticBrainsDO;
using AnalyticBrainsBL;
using AnalyticBrainsDAL;
using System.Globalization;
using System.Data;


public partial class Includes_WebForm_AddExpenses : System.Web.UI.Page
{

    LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    LeaveHistoryDO retresultdo = new LeaveHistoryDO();
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();   
    SqlConnection mycn;
    SqlDataAdapter myda;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();

    String strConn;
    DateTime today = DateTime.Today;
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    public int i = 0;
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0, flag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
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
            Session["EmpId"] = EmpId;
        }
        //this.txtDate.Text = Request[this.txtDate.UniqueID];
        if (!this.IsPostBack)
        {
           

        }
    }
    protected void txtPaidPersonName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        try
        {
            //HiddenField hfdemp = new HiddenField();
            //hfdemp = ((HiddenField)e.FindControl("hifdid"));
            ObjEmployeeDO.EmpID = EmpId;
            ObjEmployeeDO.Expensedate = Convert.ToDateTime(DateTime.ParseExact(txtchoosedate.Text, "dd-MM-yyyy", null));
            ObjEmployeeDO.ExpenseType = txtExpenseType.Text.ToString();
            ObjEmployeeDO.Amount = Convert.ToString(txtAmount.Text);
            ObjEmployeeDO.PaidName = txtPaidPersonName.Text.ToString();
            ObjEmployeeBL.ExpensesSaveDetail(ObjEmployeeDO);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Saved successfully!')</script>");
            clearform();
          
                 }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearform();
            Response.Redirect("ExpensesRecords.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnGetExpenseHistory_Click(object sender, EventArgs e)
    {

        GetExpensesheet();

    }
    protected void clearform()
    {
        try
        {
            
            txtchoosedate.Text = string.Empty;          
            txtExpenseType.Text = "";
            txtPaidPersonName.Text = "";
            txtAmount.Text = "";
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetExpensesheet()
    {
        try
        {
            ObjLeaveHistoryDO.EmpID = EmpId;
            ObjLeaveHistoryDO.FromExpensedate = Convert.ToDateTime(DateTime.ParseExact(txtchoosedate.Text, "dd-MM-yyyy", null));
            //ObjLeaveHistoryDO.ToExpensedate = Convert.ToDateTime(DateTime.ParseExact(txtchoosedate.Text, "dd-MM-yyyy", null));
            LeaveHistoryDO ds_EmpLeaveHistory = ObjLeaveHistoryBL.bindgrdExpeselist(ObjLeaveHistoryDO);
            grdExpenseHistory.DataSource = ds_EmpLeaveHistory.Resultdtset;
            grdExpenseHistory.DataBind();
            pnlgridviewExpenseHistory.Visible = true; 
        }
        catch (Exception e) { throw e; }

    }
    protected void grdExpenseHistory_RowDataBound(Object sender, GridViewRowEventArgs e)
    { 
    
    
    }
    protected void grdExpenseHistory_RowCommand(Object sender, GridViewCommandEventArgs e)
    { 
    }
    protected void grdExpenseHistory_PreRender(object sender, EventArgs e)
    { 
    }
    protected void grdExpenseHistory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}
    
         
    