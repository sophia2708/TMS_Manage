using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AnalyticBrainsBL;
using AnalyticBrainsDO;

public partial class Includes_WebForm_Admin : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();

    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["sessionid"] != "")
        {
            sessionid = Request.QueryString["sessionid"].ToString();
        }
        if (Request.QueryString["Username"] != "")
        {
            Username = Request.QueryString["Username"].ToString();
        }
        if (Request.QueryString["EmpId"] != "")
        {
            EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
        }
    }
    protected void lbtnNewRegistration_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewRegistration.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    protected void lbtnResetPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ResetPassword.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    protected void lbtnEmpList_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeList.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
   
    protected void lbtnhybrid_Click(object sender, EventArgs e)
    {
        Response.Redirect("Wfhhybrid.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    protected void lbtnLeaveedit_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveEdit.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
}
