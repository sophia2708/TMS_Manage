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
using AnalyticBrainsDO;
using AnalyticBrainsBL;
using AnalyticBrainsDAL;

public partial class Includes_WebForm_ResetPassword : System.Web.UI.Page
{
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();
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
            GetBlockedUserList();
        }
    }
    protected void btnResetPass_Click(object sender, EventArgs e)
    {
        try
        {
            string ResetPassword = "Test123@";

            for (int chkcount = 0; chkcount < cbkBlockedUserList.Items.Count; chkcount++)
            {
                if (cbkBlockedUserList.Items[chkcount].Selected)
                {
                    ObjEmployeeDO.EmpID = Convert.ToInt32(cbkBlockedUserList.Items[chkcount].Value);
                    ObjEmployeeDO.Password = EncryptingPassword(ResetPassword);
                    ObjEmployeeBL.ResetPassword(ObjEmployeeDO);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User has been activated!');", true);
            GetBlockedUserList();
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
            Response.Redirect("Admin.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetBlockedUserList()
    {
        try
        {
            retresultdo = new ResultDO();
            retresultdo = ObjEmployeeBL.GetBlockedUserList();
            if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
            {
                lblBlockedUser.Text = "Select the blocked user";
                btnCancel.Visible = true;
                btnResetPass.Visible = true;
                cbkBlockedUserList.Visible = true;

                cbkBlockedUserList.DataSource = retresultdo.Resultdtset.Tables[0];
                cbkBlockedUserList.DataTextField = "FirstName";
                cbkBlockedUserList.DataValueField = "Empid";
                cbkBlockedUserList.DataBind();
            }
            else
            {
                lblBlockedUser.Text = "No records found";
                btnCancel.Visible = false;
                btnResetPass.Visible = false;
                cbkBlockedUserList.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected string EncryptingPassword(string password)
    {
        try
        {
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Admin.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
            //Cancel();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void Cancel()
    {
        try
        {
            Response.Redirect("Login.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
}
