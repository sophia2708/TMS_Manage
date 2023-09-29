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

public partial class Includes_WebForm_Feedback : System.Web.UI.Page
{
    LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    LeaveHistoryDO retresultdo = new LeaveHistoryDO();
    // public string sessionid = string.Empty;
    public string Username = string.Empty;
    public static int EmpId = 0;
    public static string UserName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EmpId"] != null)
        {
            EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
        }

    }
    [WebMethod]
    public static string getUserName(string Contect)
    {
        return UserName;
    }
    [WebMethod]
    public static string submit(string Contect, int Ananymous)
    {
        LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
        LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
        LeaveHistoryDO retresultdo = new LeaveHistoryDO();
        ObjLeaveHistoryDO.EmpId = EmpId;
        ObjLeaveHistoryDO.Reason = Contect;
        ObjLeaveHistoryDO.Ananymous = Ananymous;
        ObjLeaveHistoryDO = ObjLeaveHistoryBL.bindRequestfeedback(ObjLeaveHistoryDO);
        return "1";
    }


    protected void btnsendmail_Click(object sender, EventArgs e)
    {

        try
        {


        }


        catch (Exception ex)
        {
            throw ex;
        }

    }

    
        //protected void checkBox_CheckedChanged(object sender, EventArgs e)
        //{           
        //    if (checkBox.Checked)
        //    {
                
        //        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Checkbox is checked');", true);
        //    }
        //    else
        //    {
                
        //        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Checkbox is unchecked');", true);
        //    }
        //}
}
    

    
