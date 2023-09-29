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
using System.Drawing;
using AnalyticBrainsDO;
using AnalyticBrainsBL;
using AnalyticBrainsDAL;

public partial class Includes_WebForm_NewRegistration : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public int type = 0;

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

        this.txtDOB.Text = Request[this.txtDOB.UniqueID];
        this.txtDOJ.Text = Request[this.txtDOJ.UniqueID];

        if (!IsPostBack)
        {
            
            GetSecurityQuestionList();
        }
     }
    protected void GetSecurityQuestionList()
    {
        try
        {
            retresultdo = ObjEmployeeBL.GetSecurityQuestionList();

            if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
            {
                ddlQuestions.DataSource = retresultdo.Resultdtset;
                ddlQuestions.DataValueField = "QuesID";
                ddlQuestions.DataTextField = "Question";
                ddlQuestions.DataBind();
            }
            ddlQuestions.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected string encryptpass(string password)
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            ObjEmployeeDO.EmpID = 0;
            ObjEmployeeDO.FirstName = txtFirstName.Text.ToString().Trim();
            ObjEmployeeDO.Lastname = txtLastName.Text.ToString().Trim();
            //ObjEmployeeDO.Dateofbirth = txtDOB.Text;
            //ObjEmployeeDO.Dateofbirth = DateTime.ParseExact(txtDOB.Text,"dd/MM/yyyy", null).ToString();
            //ObjEmployeeDO.Dateofbirth = Convert.ToDateTime(txtDOB.Text.ToString().Trim()).ToString();
            ObjEmployeeDO.Dateofbirth = Convert.ToString(DateTime.ParseExact(txtDOB.Text, "dd-MM-yyyy", null));
            ObjEmployeeDO.Phonenumber = Convert.ToInt64(txtMobileNo.Text);
            ObjEmployeeDO.Gender = ddlGender.SelectedItem.Value;
            ObjEmployeeDO.Emailid = txtEmailId.Text.ToString().Trim();
            ObjEmployeeDO.Address = txtAddress.Text.ToString().Trim();
            //ObjEmployeeDO.Dateofjoin = txtDOJ.Text;
            //ObjEmployeeDO.Dateofjoin = DateTime.ParseExact(txtDOJ.Text,"dd/MM/yyyy", null).ToString();
            //ObjEmployeeDO.Dateofjoin = Convert.ToDateTime(txtDOJ.Text.ToString().Trim()).ToString();
            ObjEmployeeDO.Dateofjoin = Convert.ToString(DateTime.ParseExact(txtDOJ.Text, "dd-MM-yyyy", null));
            ObjEmployeeDO.Username = txtUserName.Text.ToString().Trim();
            ObjEmployeeDO.Password = encryptpass(txtPassword.Text.Trim());
            ObjEmployeeDO.Forgetpwques = ddlQuestions.SelectedValue.ToString();
            ObjEmployeeDO.Forgetpwans = txtAnswer.Text.ToString();

            int result = ObjEmployeeBL.CreateNewUser(ObjEmployeeDO);
            if (result == 0)
            {
                string Url = "Admin.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New User registered Successfully!');window.location ='" + Url + "';", true);
            }
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User is already exist!')", true);
                
                txtUserName.Focus();
                txtUserName.BackColor = System.Drawing.Color.FromName("#ff4c4c");
            }
            if(result == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Re-Enter the data!')", true);
                txtFirstName.Focus();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            clearform();
            Response.Redirect("Admin.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void clearform()
    {
        try
        {
            ddlGender.SelectedValue = "0";
            txtDOB.Text = string.Empty;
            txtAddress.Text = "";
            txtConfirmPassword.Text = "";
            txtEmailId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMobileNo.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtAnswer.Text = "";
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
            Cancel();
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
    protected void txtLastName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtMobileNo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
