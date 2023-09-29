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
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
using AnalyticBrainsBL;
using System.IO;
using LeaveManagementBL;
using LeaveManagementDAL;
using LeaveManagementDO;

using System.Data.SqlClient;


namespace AnalyticBrains.MasterPage
{
    public partial class ABMaster : System.Web.UI.MasterPage
    {
        LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
        LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
        public string sessionid = string.Empty;
        public string Username = string.Empty;
        public int EmpId = 0;
        public int GetCount = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            //{
            //    DataTable dt = this.GetData(0);
            //    PopulateMenu(dt, 0, null);
            //}

            try
            {
                if (Session["FirstName"].ToString() != "")
                {
                    lblFirstName.Text = "Welcome " + Session["FirstName"].ToString();
                    if (Session["FirstName"].ToString() != "Admin")
                    {
                        btnAdmin.Visible = false;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Your session has expired. Please login again!')</script>");
                    Response.Write("<script>window.location.href='Login.aspx';</script>");
                }

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
                GetCount = CheckCredential(Request.QueryString["Username"].ToString());
                if (EmpId == 31 || EmpId == 2)
                {
                    ddlmanagement.Visible = true;
                   
                    
                }
                else
                {
                     ddlmanagement.Visible = false;
                }
                
                if (GetCount > 0)
                {

                    ApproveTimesheet.Visible = true;


                }
                else
                {

                    ApproveTimesheet.Visible = false;

                    //grdLeaveDecision.UseAccessibleHeader  = false;
                }
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }
            

        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            string sessionid = Request.QueryString["sessionid"].ToString();

            EmployeeDO ObjEmployeeDO = new EmployeeDO();
            EmployeeBL ObjEmployeeBL = new EmployeeBL();

            ObjEmployeeDO.sessionid = sessionid;
            ObjEmployeeBL.logout(ObjEmployeeDO);

            Session.Abandon();
            Session.Contents.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Your session has been successfully logout!')", true);

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Your session has been successfully logout!');window.location ='Login.aspx';", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your session has been successfully logout!');window.location ='Login.aspx';", true);
            Response.Redirect("Login.aspx");
        }
        protected void Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnTimesheet_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnterTimeSheet.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnViewTimesheet_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewEditTimesheet.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnProfile_Click(object sender, EventArgs e)
        {
            Session["EmpId"] = EmpId;
            Response.Redirect("EditProfile.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangeUserPassword.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnTaskManage_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaskManage.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnTaskMaster_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaskMaster.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnLMS_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeaveHistory.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        protected void btnPermission_Click(object sender, EventArgs e)
        {
            Response.Redirect("Permission.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }

        protected void btnPoliciesandProcedures_Click(object sender, EventArgs e) //ADDED BY SOF
        {
            Response.Redirect("PoliciesandProcedures.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }

        protected void btnPaySlips_Click(object sender, EventArgs e)
        {
            Response.Redirect("PaySlips.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }







        protected int CheckCredential(string Username)
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
    }
}
