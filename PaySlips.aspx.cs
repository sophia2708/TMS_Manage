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

public partial class Includes_WebForm_PaySlips : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
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

        if (!this.IsPostBack)
        {
            BindYearsandMonth();
           
            
        }
       

    }
    protected void LogOff_Click(object sender, EventArgs e)
    {
        BindYearsandMonth();
    }
    protected void ButtonSendMail_Click(object sender, EventArgs e)
    {
        LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
        LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
        LeaveHistoryDO retresultdo = new LeaveHistoryDO();
        ObjLeaveHistoryDO.EmpId = EmpId;
        string SelEmpid = "";
        if (employeeslist.Items.Count > 0)
        {
            foreach (ListItem listItem in employeeslist.Items)
            {
                if (listItem.Selected)
                {
                    var val = listItem.Value;
                    var txt = listItem.Text;
                    SelEmpid += val + "<br/>";
                }
            }
            //if (SelEmpid == "") SelEmpid = "0";

            ObjLeaveHistoryDO.MonthandYear = SelEmpid;
            ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindRequestpayslip(ObjLeaveHistoryDO);

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Mail Sent Successfully');", true);
        employeeslist.Items.Clear();
        BindYearsandMonth();
    }
    
    protected void BindYearsandMonth()
    {
        try
        {
            retresultdo = ObjTimesheetBL.GetYearandMonthList();
            employeeslist.DataSource = retresultdo.Resultdtset.Tables[0];
            employeeslist.DataValueField = "Value";
            employeeslist.DataTextField = "Value";
            employeeslist.DataBind();
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void DeleteValues(object sender, EventArgs e)
    //{
    //    List<ListItem> deletedItems = new List<ListItem>();
    //    foreach (ListItem item in employeeslist.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            deletedItems.Add(item);
    //        }
    //    }

    //    foreach (ListItem item in deletedItems)
    //    {
    //        employeeslist.Items.Remove(item);
    //    }
    //}

}

    

    
