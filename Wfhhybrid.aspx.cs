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
using System.Data.SqlClient;
using AnalyticBrainsDO;
using AnalyticBrainsBL;
using AnalyticBrainsDAL;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Wfhhybrid : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

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
            GetEmployeeList();

            //GetActiveemployeelist();
            //BindEmployeelistDDL();
        }

      
    }
    public void GetEmployeeList()
    {
        try
        {
            //DataSet ds = new DataSet();
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.GetEmployeeList(ObjTimesheetDO);
            grdWfhhybrid.DataSource = retresultdo.Resultdtset;
            grdWfhhybrid.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
          
    protected void grdWfhhybrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Control Empid = e.Row.FindControl("lblEmpid");
                Control lstEmployees = e.Row.FindControl("ddlweeklyhybrid"); //ADDED BY SOPHIA
                Label lblEmpId = Empid as Label;                       
                ListBox dd3 = lstEmployees as ListBox;
                ResultDO ds_Emplist = ObjTimesheetBL.Gethybridvalues(ObjTimesheetDO);                                
                //string s = "";// select newCol from emp ehree enpid = 45
                if (Convert.ToInt32(lblEmpId.Text) == 31 || Convert.ToInt32(lblEmpId.Text) == 32)
                {
                   
                    dd3.Visible = false; //ADDED BY SOPHIA
                }
                else
                {
                    
                    DataSet GetRptmgr = GetEmployeeRptmgr(Convert.ToInt32(lblEmpId.Text));
                    DataSet GetEmployees = GetEmployeesName(Convert.ToInt32(lblEmpId.Text));
                   

                    if (GetRptmgr.Tables[0].Rows.Count > 0)
                    {
                        

                        string s = GetRptmgr.Tables[0].Rows[0][2].ToString();
                        int[] nums = Array.ConvertAll(s.Split(','), int.Parse);
                        ListBox lstemployee = ((ListBox)e.Row.FindControl("ddlweeklyhybrid")); //ADDED BY SOF
                        foreach (ListItem listItem in dd3.Items)
                        {
                            if (Array.IndexOf(nums, Int16.Parse(listItem.Value)) != -1)
                            {
                                listItem.Selected = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    public void Cancel()
    {
        Response.Redirect("Login.aspx");
    }

    private DataSet GetEmployeeRptmgr(int EmpID)
    {

        DataSet ds = new DataSet();
        string str = "[SP_LM_Emphybrid_options] " + EmpID + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }


    private DataSet GetEmployeesName(int EmpID)       //ADDED BY SOPHIA
    {

        DataSet ds = new DataSet();
        string str = "[AB_GetEmployeeList] " + EmpID + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
 
    protected void btnSaveEmpList_Click(object sender, EventArgs e)
    {
        try
        {

            foreach (GridViewRow dr in grdWfhhybrid.Rows)
            {
                string  SelEmpid="";
                Label lblEmpid = ((Label)dr.FindControl("lblEmpid"));
                ListBox lstemployee = ((ListBox)dr.FindControl("ddlweeklyhybrid")); 
                foreach (ListItem listItem in lstemployee.Items)
                {
                    if (listItem.Selected)
                    {
                        var val = listItem.Value;
                        var txt = listItem.Text;
                        SelEmpid += val + ",";
                    }
                }
                               
                if (SelEmpid == "") SelEmpid = "0";                
                ObjEmployeeDO.EmpID = Convert.ToInt32(lblEmpid.Text);
                ObjEmployeeDO.WeeklyName = SelEmpid; 
                ObjEmployeeBL.InsertReportHybrid(ObjEmployeeDO);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Saved successfully!')</script>");
               

            }

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
}