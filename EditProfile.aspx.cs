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
using AnalyticBrainsDAL;


public partial class Includes_WebForm_EditProfile : System.Web.UI.Page
{
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    ResultDO retresultdo = new ResultDO();

    public int EmpId = 0;
    public string username = string.Empty;
    public string sessionid = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["username"] != null || Request.QueryString["username"] != string.Empty)
        {
            username = Request.QueryString["username"].ToString();
        }
        if (Request.QueryString["sessionid"] != null || Request.QueryString["sessionid"] != string.Empty)
        {
            sessionid = Request.QueryString["sessionid"].ToString();
        }

        if (Request.QueryString["EmpId"] != null || Request.QueryString["EmpId"] != string.Empty)
        {
            EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
        }

        if (Session["FirstName"].ToString() == "Admin")
        {
            txtDOJ.BackColor = System.Drawing.Color.FromName("White");
            imgDOJ.Visible = true;
            //txtUserName.ReadOnly = false;
            //txtUserName.BackColor = System.Drawing.Color.FromName("White");
        }

        this.txtDOB.Text = Request[this.txtDOB.UniqueID];
        this.txtDOJ.Text = Request[this.txtDOJ.UniqueID];

        if (!IsPostBack)
        {
            GetSecurityQuestionList();

            ObjEmployeeDO.EmpID = Convert.ToInt32(EmpId.ToString()); //chnages done by sof
            ObjEmployeeBL.ViewEmpProfile(ObjEmployeeDO);

            txtFirstName.Text = ObjEmployeeDO.FirstName;
            txtLastName.Text = ObjEmployeeDO.Lastname;
            txtDOB.Text = ObjEmployeeDO.Dateofbirth.ToString();
            txtMobileNo.Text = ObjEmployeeDO.Phonenumber.ToString();
            ddlGender.SelectedValue = ObjEmployeeDO.Gender.ToString();
            txtEmailId.Text = ObjEmployeeDO.Emailid;
            txtAddress.Text = ObjEmployeeDO.Address;
            txtDOJ.Text = ObjEmployeeDO.Dateofjoin.ToString();
            txtUserName.Text = ObjEmployeeDO.Username;
            ddlQuestions.SelectedValue = ObjEmployeeDO.Forgetpwques.ToString();
            txtAnswer.Text = ObjEmployeeDO.Forgetpwans; 
        }
     }
    private string Int32(string p)
    {
        throw new NotImplementedException();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       try
        {
            ObjEmployeeDO.EmpID = EmpId;
            ObjEmployeeDO.FirstName = txtFirstName.Text.ToString().Trim();
            ObjEmployeeDO.Lastname = txtLastName.Text.ToString().Trim();           
            ObjEmployeeDO.Dateofbirth = Convert.ToString(DateTime.ParseExact(txtDOB.Text,"dd-MM-yyyy", null));
            ObjEmployeeDO.Phonenumber = Convert.ToInt64(txtMobileNo.Text);
            ObjEmployeeDO.Gender = ddlGender.SelectedValue.ToString().Trim();
            ObjEmployeeDO.Emailid = txtEmailId.Text.ToString().Trim();
            ObjEmployeeDO.Address = txtAddress.Text.ToString().Trim();
            ObjEmployeeDO.Dateofjoin = Convert.ToString(DateTime.ParseExact(txtDOJ.Text, "dd-MM-yyyy", null));
            ObjEmployeeDO.Username = txtUserName.Text.ToString().Trim();
            ObjEmployeeDO.Password = "";
            ObjEmployeeDO.Forgetpwques = ddlQuestions.SelectedValue.ToString();
            ObjEmployeeDO.Forgetpwans = txtAnswer.Text.ToString();
            int result = ObjEmployeeBL.CreateNewUser(ObjEmployeeDO);
            if (result == 2)
            {
                //string url = "Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + username;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Profile updated successfully!')",true);
            }
            else
            {

            }
        }
        catch(Exception ex)
        {
            throw ex;
           // Response.Write("Enter the correct data.");
        }
    }
    protected void btnGotoHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?EmpId=32&sessionid=" + sessionid + "&Username=admin@analyticbrains.com");
    }
}
