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



public partial class Includes_WebForm_HolidayList : System.Web.UI.Page
{
    HolidayListDO ObjHolidayListDO = new HolidayListDO();
    HolidayListBL ObjHolidayListBL = new HolidayListBL();
    HolidayListDO retresultdo = new HolidayListDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
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
            }
           
                Getholidaylist();
                

            }
            catch
        {
            Response.Redirect("Login.aspx");
        }
        
    }
    


    public void Getholidaylist() //ADDED BY SOPHIA //
    {
        try
        {

            //DataSet ds = new DataSet();
            
            retresultdo = ObjHolidayListBL.bindddlgetholidaylist();
            grdLeaveHistory1.DataSource = retresultdo.Resultdtset;
            //grdLeaveHistory1.DataTextField = "Holiday_Date";
            //grdLeaveHistory1.DataValueField = "Holiday_Name";
            grdLeaveHistory1.DataBind();




        } //Till here
        catch (Exception ex)
        {
            throw ex;
        }






    }
}