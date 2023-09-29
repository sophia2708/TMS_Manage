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
using AnalyticBrainsDAL;
using AnalyticBrainsDO;

public partial class Includes_WebForm_ChangeUserPassword : System.Web.UI.Page
{
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public string FirstName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["sessionid"] != "")
        {
            sessionid = Request.QueryString["sessionid"].ToString();
        }
        if (Request.QueryString["Username"] != "")
        {
            Username = Request.QueryString["Username"].ToString();
            txtUserName.Text = Username;
        }
        if (Request.QueryString["EmpId"] != "" || Request.QueryString["EmpId"] != "")
        {
            EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string password = txtPassword.Text;
            string newpassword = txtNewpassword.Text;
            string encodedpassword = encryption(password);
            string encodednewpassword = encryption(newpassword);

            EmployeeDO ObjEmployeeDO = new EmployeeDO();
            EmployeeBL ObjEmployeeBL = new EmployeeBL();

            ObjEmployeeDO.Username = txtUserName.Text.ToString();
            ObjEmployeeDO.Password = encodedpassword;
            ObjEmployeeDO.Newpassword = encodednewpassword;

            int result = ObjEmployeeBL.ChangePassword(ObjEmployeeDO);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password updated successfully!');window.location ='Login.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Old Password does not match with Username. Password not saved!')", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
    protected void Btncancel_Click(object sender, EventArgs e)
    {
       try
       {
           Response.Redirect("ChangeUserPassword.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username); //changes done by sof
       }
       catch (Exception ex)
       {
           throw ex;
       }
   }
}
