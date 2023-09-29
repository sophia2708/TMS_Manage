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
using AnalyticBrainsDO;
using AnalyticBrainsBL;
using AnalyticBrainsDAL;
using System.Globalization;
using System.Data;


public partial class Includes_WebForm_ExpensesRecords : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();
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

    protected void lbtnAddExpenses_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddExpenses.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    protected void lbtnExpensesList_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExpenseList.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }

}
    
         
    