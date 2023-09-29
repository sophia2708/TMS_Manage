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
using AnalyticBrainsBL;
using System.Data.SqlClient;

public partial class Includes_WebForm_Login : System.Web.UI.Page
{
    public string sessionid = string.Empty;
    public string FirstName = string.Empty;
    public int EmpId = 0;
    public enum MessageType { Success, Error, Info, Warning };

   protected void Page_Load(object sender, EventArgs e)
   {
       //txtUserName.Text = "Thangamani@analyticbrains.com";
       //txtPassword.Text = "Test123@";

       //txtUserName.Text = "Admin@analyticbrains.com";
       //txtPassword.Text = "Test123@";
       
       
       Session["FirstName"] = "";
       //txtUserName.Focus(); 
   }
   protected void ShowMessage(string Message, MessageType type)
   {
       ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
   }
   protected void Login_Click(object sender, EventArgs e)
   {
       try
       {
           if (txtUserName.Text != "" && txtPassword.Text != "")
           { 
               LoginValidation();
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
   public void LoginValidation()
   {
       string password = txtPassword.Text;
       string encodedpassword = encryption(password);

       EmployeeDO objEmployeeDO = new EmployeeDO();
       EmployeeBL objEmployeeBL = new EmployeeBL();

       objEmployeeDO.Username = txtUserName.Text;
       objEmployeeDO.Password = encodedpassword;
       int result=0;
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
               Response.Redirect(String.Format("Admin.aspx?EmpId=" + EmpId + "&username={0}&sessionid={1}", Server.UrlEncode(txtUserName.Text), Server.UrlEncode(sessionid)));
           }
           else
           {
               Response.Redirect(String.Format("Home.aspx?EmpId=" + EmpId + "&Username={0}&sessionid={1}", Server.UrlEncode(txtUserName.Text), Server.UrlEncode(sessionid)));
           }
                 }
       if (result == 0)
       {
           ShowMessage("Please enter the correct User Name and Password!", MessageType.Error);
           //ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptName", "alert(\"Login failed!\\nPlease enter the correct User Name and Password!\");", true);
           txtPassword.Text = "";
           txtUserName.Focus();
       }
       if (result == 2)
       {
           ShowMessage("No. of login attempts exceeded!! Please Contact admin!", MessageType.Error);
          // ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptName", "alert(\"You reached maximum number of attempts!\\nPlease contact admin!\");", true);
           ClearForm();
       }
       if (result == 3)
       {
           ShowMessage("Your account has been locked!!! Please Contact admin!", MessageType.Error);
           //ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptName", "alert(\"Your account has been locked!\\nPlease contact admin!\");", true);
           ClearForm();
       }
       if (result == 4)
       {
           ShowMessage("Please enter valid username and password!", MessageType.Error);
           //ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptName", "alert(\"Please enter correct username!\");", true);
           //  Response.Write("<script type=text/javascript>bootbox.alert(\"Please enter correct username!\");");
           ClearForm();
       }
   }
   protected void lbtnForgetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ForgotPassword.aspx");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   public void ClearForm()
   {
       try
       {
           txtUserName.Text = "";
           txtPassword.Text = "";
       }
       catch (Exception ex)
       {
           throw ex;
       }
      
   }
   protected void txtUserName_TextChanged(object sender, EventArgs e)
   {

   }
}