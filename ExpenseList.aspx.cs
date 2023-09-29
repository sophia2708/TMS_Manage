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


public partial class Includes_WebForm_ExpenseList : System.Web.UI.Page
{
    LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    LeaveHistoryDO retresultdo = new LeaveHistoryDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;


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
        }
        if (!IsPostBack)
        {

            btnExportToExcel.Visible = false;
            //GetActiveemployeelist();
            //BindEmployeelistDDL();
        }


      
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            GetExpensesheetview();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetExpensesheetview()
    {
        try
        {
            //hfdemp = ((HiddenField)e.FindControl("hifdid"));
            
            ObjLeaveHistoryDO.EmpID = EmpId;
            ObjLeaveHistoryDO.Fromdate = Convert.ToDateTime(DateTime.ParseExact(txtFromdate.Text, "dd-MM-yyyy", null));
            ObjLeaveHistoryDO.ToStartdate = Convert.ToDateTime(DateTime.ParseExact(txtTodate.Text, "dd-MM-yyyy", null));
            LeaveHistoryDO ds_EmpLeaveHistory = ObjLeaveHistoryBL.ExpenseslistSaveDetail(ObjLeaveHistoryDO);
            grdAddExpenseHistory.DataSource = ds_EmpLeaveHistory.Resultdtset;
            grdAddExpenseHistory.DataBind();
            pnlgridviewAddExpenseHistory.Visible = true;
            btnExportToExcel.Visible = true;

        }
        catch (Exception e) { throw e; }

    }

    protected void grdAddExpenseHistory_RowDataBound(Object sender, GridViewRowEventArgs e)
    {


    }
    protected void grdAddExpenseHistory_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
    }
    protected void grdAddExpenseHistory_PreRender(object sender, EventArgs e)
    {
    }
    protected void grdAddExpenseHistory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Expenses List" + DateTime.Now + ".xls"));
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            grdAddExpenseHistory.RenderControl(htmlWrite);
            string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
            Response.Write(style);
            grdAddExpenseHistory.GridLines = GridLines.Both;
            grdAddExpenseHistory.HeaderStyle.Font.Bold = true;
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    protected void rbtnByRange1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;
            DateTime StartOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            txtFromdate.Text = StartOfWeek.ToString("dd-MM-yyyy");
            //txtFromdate.Text = StartOfWeek.ToString("dd/MM/yyyy");
            txtTodate.Text = dt.ToString("dd-MM-yyyy");
            //txtTodate.Text = dt.ToString("dd/MM/yyyy");
            
            pnlRange.Visible = true;
            
           // btnWeeklyStatus.Visible = true;
            btngo.Visible = false;
            // btnSendmail.Visible = false;
            //btnExportToExcel.Visible = false;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

}
    
         
    