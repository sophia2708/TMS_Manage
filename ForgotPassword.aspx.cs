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
using AnalyticBrainsDAL;
using AnalyticBrainsBL;

public partial class Includes_WebForm_ForgotPassword : System.Web.UI.Page
{
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();

    public string sessionid = string.Empty;
    public string FirstName = string.Empty;
    public int EmpId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsername.Focus();
        pnlSecAnswer.Visible = false;
        pnlButton.Visible = false;
        pnlNewPassword.Visible = false;
    }
    public void LoginValidation()
    {
       
        string password = txtPassword.Text;
        string encodedpassword = encryption(password);
        
        EmployeeDO objEmployeeDO = new EmployeeDO();
        EmployeeBL objEmployeeBL = new EmployeeBL();

        objEmployeeDO.Username = txtUsername.Text;
        objEmployeeDO.Password = encodedpassword;
        int result = 0;
        try
        {
            result = objEmployeeBL.LoginValidation(objEmployeeDO);
        }
        catch (Exception)
        {
            Response.Redirect("Error.htm");
        }
        if (result == 1)
        {
            sessionid = objEmployeeDO.sessionid;
            FirstName = objEmployeeDO.FirstName;
            EmpId = objEmployeeDO.EmpID;

            Session["FirstName"] = FirstName;
            if (Session["FirstName"].ToString() == "Admin")
            {
                Response.Redirect(String.Format("Admin.aspx?Empid=" + EmpId + "&username={0}&sessionid={1}", Server.UrlEncode(txtUsername.Text), Server.UrlEncode(sessionid)));
            }
            else
            {
                Response.Redirect(String.Format("EnterTimeSheet.aspx?Empid=" + EmpId + "&username={0}&sessionid={1}", Server.UrlEncode(txtUsername.Text), Server.UrlEncode(sessionid)));
            }
            //Response.Redirect(String.Format("NewRegistration.aspx?Empid=" + EmpId + "&username={0}&sessionid={1}", Server.UrlEncode(txtUserName.Text), Server.UrlEncode(sessionid)));
        }
        //if (result == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptName", "alert(\"Login failed!\\nPlease enter the correct User Name and Password!\");", true);
        //    txtUsername.Focus();
        //}
        //if (result == 2)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptName", "alert(\"You reached maximum number of attempts!\\nPlease contact admin!\");", true);
        //    ClearForm();
        //}
        if (result == 3)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "scriptName", "alert(\"Your account has been locked!\\nPlease contact admin!\");", true);
            lblhintques.Visible = false;
            txthintans.Visible = false;
            btnSubmit.Visible = false;
            btnCancel1.Visible = false;
            pnlButton.Visible = false;
            pnlSecAnswer.Visible = false;//ClearForm();
        }
    }
    public void ClearForm()
    {
        txtUsername.Text = "";
        txtPassword.Text = "";
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            LoginValidation();
            int result;
            // DataSet ds = new DataSet();
            ResultDO retresultdo = new ResultDO();
            ObjEmployeeDO.Username = txtUsername.Text;
            retresultdo = ObjEmployeeBL.UserNameValidation(ObjEmployeeDO);
            result = Convert.ToInt32(retresultdo.Resultdtset.Tables[0].Rows[0][0].ToString());

            if (result == 0)
            {
                lblhintques.Text = retresultdo.Resultdtset.Tables[0].Rows[0][1].ToString();
                Session["SecurityAnswer"] = retresultdo.Resultdtset.Tables[0].Rows[0][2].ToString();
                Session["returnresult"] = Convert.ToInt32(result);
                pnlSecAnswer.Visible = true;
                pnlButton.Visible = true;
                pnlNewPassword.Visible = false;
                btnGo.Visible = false;
                txthintans.Focus();
            }
            if (result == 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Enter a valid User Name(Office Mail-ID)!')", true);

                pnlSecAnswer.Visible = false;
                pnlButton.Visible = false;
                pnlNewPassword.Visible = false;
                txtUsername.Focus();
            }
            if (result == 2)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('User has been locked. Please contact Admin!')", true);

                pnlSecAnswer.Visible = false;
                pnlButton.Visible = false;
                pnlNewPassword.Visible = false;
                txtUsername.Focus();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int result;
            ResultDO retresultdo = new ResultDO();
            ObjEmployeeDO.Username = txtUsername.Text;
            ObjEmployeeDO.Forgetpwans = txthintans.Text;
            result = ObjEmployeeBL.SecAnsValidation(ObjEmployeeDO);

            if (result == 0)
            {
                pnlSecAnswer.Visible = true;
                lblhintques.Enabled = false;
                txthintans.Enabled = false;
                pnlNewPassword.Visible = true;
                pnlButton.Visible = false;
                txtPassword.Focus();
            }
            if (result == 1)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Your answer is incorrect. Try again!')", true);
                pnlSecAnswer.Visible = true;
                pnlButton.Visible = true;
                pnlNewPassword.Visible = false;
                txthintans.Text = "";
                txthintans.Focus();
            }
            if (result == 2)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Reached Maximum attempts. Please Contact Admin!')", true);
                pnlSecAnswer.Visible = true;
                pnlButton.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel1_Click(object sender, EventArgs e)
    {
        Cancel();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string password = txtPassword.Text;
            string encodedpassword = encryption(password);

            ObjEmployeeDO.Username = txtUsername.Text;
            ObjEmployeeDO.Password = encodedpassword;

            bool reslt = ObjEmployeeBL.ForgotPassword(ObjEmployeeDO);
            if (reslt)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Your password has been changed successfully!');window.location ='Login.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Your password has not changed. Try again!')", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Cancel();
    }
    public string encryption(string password)
    {
        try
        {
            string clearpassword = password;
            byte[] encData_byte = new byte[clearpassword.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(clearpassword);
            string encodedpassword = Convert.ToBase64String(encData_byte);
            return encodedpassword;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Cancel();
    }
    public void Cancel()
    {
        Response.Redirect("Login.aspx");
    }
}
