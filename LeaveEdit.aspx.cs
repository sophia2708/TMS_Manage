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

public partial class Includes_WebForm_LeaveEdit : System.Web.UI.Page
{
    LeaveHistoryDO ObjLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    LeaveHistoryDO retresultdo = new LeaveHistoryDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public string HeaderLeaveName = string.Empty;
    string total;
    int GetCount = 0;
    decimal TotalBeginingBalance = 0;
    decimal TotalEarnedLeave = 0;
    decimal TotalAvailed = 0;
    decimal TotalCurrentbalance = 0;
    decimal TotalLOP = 0;
    //Decimal Noofdays = 0;
    public enum MessageType { Success, Error, Info, Warning };
    //LeaveHistoryDO objLeaveHistoryDO = new LeaveHistoryDO();
    //LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    SqlConnection mycn;
    SqlDataAdapter myda;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet dsSelDate;
    String strConn;
    DateTime today = DateTime.Today;
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    StringBuilder strApproved = new StringBuilder();
    StringBuilder strPending = new StringBuilder();
    StringBuilder strOnHold = new StringBuilder();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!this.IsPostBack)
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
                //btnExample.Visible = false;
                //if (EmpId == 31 || EmpId == 48) { datacal.Visible = true; } else { datacal.Visible = false; }
            }

        }
        //btnExample.Visible = false;
        GetEmpName();


    }

    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }


    protected void grdEmpLeaveBalance_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGetEmpLeaveBalance_Click(object sender, EventArgs e)
    {

        //if (ddempname.SelectedIndex == 0)
        //{
        //    //LabelEmpHistory.Visible = false;
        //    ShowMessage("Please Select Employee Name", MessageType.Error);
        //    //return;

        //}
        //else
        //{

        //    btngo.Visible = true;
        //}
        GetEmpLeavebalance();
        btnExample.Visible = true;

    }

    public void GetEmpName()
    {
        try
        {

            //DataSet ds = new DataSet();
            ObjLeaveHistoryDO.EmpId = EmpId;
            retresultdo = ObjLeaveHistoryBL.bindddlEmpName(ObjLeaveHistoryDO);
            ddempname.DataSource = retresultdo.Resultdtset;
            ddempname.DataTextField = "FirstName";
            ddempname.DataValueField = "EmpId";
            ddempname.DataBind();
            ddempname.Items.Insert(0, "  ---Select Employee---  ");


        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetEmpLeavebalance()
    {
        try
        {

            int EmpId_ = Convert.ToInt32(Request.Form[ddempname.UniqueID]);
            ddempname.SelectedValue = EmpId_.ToString();
            ObjLeaveHistoryDO.EmpId = EmpId_;
            //ObjLeaveHistoryDO.EmpId = EmpId;
            retresultdo = ObjLeaveHistoryBL.bindLeaveblcupdation(ObjLeaveHistoryDO);
            grdEmpLeaveBalance.DataSource = retresultdo.Resultdtset;
            grdEmpLeaveBalance.DataBind();

            grdEmpLeavetransaction.DataSource = retresultdo.Resultdtset.Tables[1];
            grdEmpLeavetransaction.DataBind();
            //btnExample.Visible = true;
            // btnleaveupdate.Visible = true;
            //btnsave.Visible = true;

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnLeaveEdit_Click(object sender, EventArgs e)
    {
        try
        {

            EmployeeLeaveUpdate();

        }
        catch (Exception ex)
        {
            throw (ex);
        }


    }
    protected void EmployeeLeaveUpdate()
    {
        try
        {

            foreach (GridViewRow dr in grdEmpLeaveBalance.Rows)
            {
                HiddenField hfdemp = new HiddenField();
                hfdemp = ((HiddenField)dr.FindControl("hfdEmptransactId"));
                ObjLeaveHistoryDO.EmpId = Convert.ToInt32(Session["EmpId"]);
                TextBox OpeningBalance = (TextBox)dr.FindControl("lblBeginingBalance");
                TextBox EarnedLeave = (TextBox)dr.FindControl("lblEarnedLeave");
                TextBox Availed = (TextBox)dr.FindControl("lblAvailed");
                TextBox CurrentBalance = (TextBox)dr.FindControl("lblCurrentBalance");
                TextBox LossofPay = (TextBox)dr.FindControl("lblLOP");              
                ObjLeaveHistoryDO.EmpId = EmpId;
                ObjLeaveHistoryDO.OpenLeave = float.Parse(OpeningBalance.Text);
                ObjLeaveHistoryDO.EarnLeave = float.Parse(EarnedLeave.Text);
                ObjLeaveHistoryDO.AvailedLeave = float.Parse(Availed.Text);
                ObjLeaveHistoryDO.CurrentBalance = float.Parse(CurrentBalance.Text);
                ObjLeaveHistoryDO.Lossofpay = float.Parse(LossofPay.Text);
                //if (hfdemp.Value == "") hfdemp.Value = "0";
                //ObjLeaveHistoryDO.EmptransactId = Convert.ToInt32(hfdemp.Value);
                ObjLeaveHistoryBL.LeaveUpdateSaveDetail(ObjLeaveHistoryDO);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully!');", true);

            }

        }

        catch (Exception ex)
        {
            throw ex;
        }


    }

}




