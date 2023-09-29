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

public partial class Includes_WebForm_TaskManage : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo;

    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public int TaskName = 0;
    public int ProjectId;
    public int ModuleId;
    public int TaskId;
    public int ClientId;
    public int parent_id;
    public int selectedid;
    public int empid = 0;
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

        if (Request.QueryString["selectedid"] != null)
        {
            selectedid = Convert.ToInt32(Request.QueryString["selectedid"].ToString());
        }
        this.txtExpectedCompDate.Text = Request[this.txtExpectedCompDate.UniqueID];
        this.txtplanneddate.Text = Request[this.txtplanneddate.UniqueID];
        //this.txtactualstartdate.Text = Request[this.txtactualstartdate.UniqueID];
        this.ObjTimesheetDO.TaskName = Request.QueryString["task"];
        this.ObjTimesheetDO.reworkid = Convert.ToInt32(Request.QueryString["rework"]);


        if (!IsPostBack)
        {
            GetProjectList();
            GetClients();
            GetTaskcat();
            GetEmployeeList();
            GetPriorityList();
            pnlAddProject.Visible = false;
            pnlModule.Visible = false;
            pnlAddModule.Visible = false;
            pnlTask.Visible = false;
            pnlAddTask.Visible = false;
            pnlEmpList.Visible = false;
            pnlproject.Visible = false;
            pnlcategory.Visible = false;
            pnlotd.Visible = false;
            pnlbtn.Visible = false;
            //pnlbtn.Visible = false;
            //pnlotd.Visible = false;
            //btnsavs.Visible = false;
            //btnCancel.Visible = false;
            if (ObjTimesheetDO.TaskName != null)
            {
                Getemp_dash_details();
            }
        }
        if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 0)// EDIT
        {
            pnlotd.Visible = true;
            pnlbtn.Visible = true;
        }
        if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 1)// REWORK
        {
            pnlotd.Visible = false;
            pnlbtn.Visible = true;
        }
        if (ObjTimesheetDO.TaskName == null)
        {

        }
    }

    protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectList.SelectedIndex != 0)
            {
                ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
                GetModuleList(ProjectId);
                pnlModule.Visible = true;
                pnlcategory.Visible = false;
                pnlbtn.Visible = false;
            }
            else
            {
                pnlbtn.Visible = false;
                pnlcategory.Visible = false;
                pnlModule.Visible = false;
            }
            pnlAddProject.Visible = false;
            pnlAddModule.Visible = false;
            pnlAddTask.Visible = false;
            pnlTask.Visible = false;
            pnlEmpList.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlModuleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlModuleList.SelectedIndex != 0)
            {
                ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
                ModuleId = Convert.ToInt32(ddlModuleList.SelectedItem.Value);
                empid = Convert.ToInt32(ddlEmployeeList.SelectedValue);
                GetTaskList(ProjectId, ModuleId, empid);
                pnlbtn.Visible = false;
                pnlTask.Visible = true;
                pnlcategory.Visible = false;
            }
            else
            {
                pnlbtn.Visible = false;
                pnlcategory.Visible = false;
                pnlTask.Visible = false;
            }
            pnlAddProject.Visible = false;
            pnlAddModule.Visible = false;
            pnlAddTask.Visible = false;
            pnlEmpList.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmployeeList.SelectedIndex != 0)
        {
            EmpId = Convert.ToInt32(ddlEmployeeList.SelectedItem.Value);
            GetProjectList();
            pnlproject.Visible = true;
            pnlModule.Visible = false;
            pnlTask.Visible = false;
            pnlcategory.Visible = false; 
            ddlProjectList.SelectedIndex = 0;
        }
        else
        {
            ddlProjectList.SelectedIndex = 0;
            pnlproject.Visible = false;
            pnlModule.Visible = false;
            pnlTask.Visible = false;
            pnlcategory.Visible = false;
        }
            pnlAddTask.Visible = false;
            pnlAddModule.Visible = false;
            pnlAddProject.Visible = false;
            pnlAddTask.Visible = false;
            pnlEmpList.Visible = false;
            pnlbtn.Visible = false;
            
    }
    
    protected void ddlTaskList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTaskList.SelectedIndex != 0)
            {
                //GetEmployeeList();
                GetPriorityList();
                pnlEmpList.Visible = true;
                pnlbtn.Visible = true;
                pnlcategory.Visible = true;
                pnlbtn.Visible = true;
                TaskId = Convert.ToInt32(ddlTaskList.SelectedItem.Value);
            }
            else
            {
                pnlbtn.Visible = false;
                pnlcategory.Visible = false;
                pnlEmpList.Visible = false;
                pnlbtn.Visible = false;
            }
            txtExpectedCompDate.Text = "";
            txtplanneddate.Text = "";
            txtplannedeffortdate.Text = "";
            pnlAddProject.Visible = false;
            pnlAddModule.Visible = false;
            pnlAddTask.Visible = false;
            ddltaskcat.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddltaskcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltaskcat.SelectedIndex != 0)
        {
            pnlEmpList.Visible = true;
            pnlbtn.Visible = true;

        }
        else
        {
            pnlEmpList.Visible = true;
            pnlbtn.Visible = true;
        }
        ddlPriority.SelectedIndex = 0;
        txtExpectedCompDate.Text = "";
        txtplanneddate.Text = "";
        txtplannedeffortdate.Text = "";
    }

    public void GetProjectdropdown(int clientid)
    {
        try
        {
            ObjTimesheetDO.Clients = clientid;
            retresultdo = ObjTimesheetBL.GetProjectdropdown(ObjTimesheetDO);

            ddlProjectList.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlProjectList.DataValueField = "ProjectId";
            ddlProjectList.DataTextField = "ProjectName";
            ddlProjectList.DataBind();
            ddlProjectList.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetProjectList()
    {
        try
        {
            retresultdo = new ResultDO();
            retresultdo = ObjTimesheetBL.GetProjectList();

            ddlProjectList.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlProjectList.DataValueField = "ProjectId";
            ddlProjectList.DataTextField = "ProjectName";
            ddlProjectList.DataBind();
            ddlProjectList.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetModuleList(int ProjectId)
    {
        try
        {
            retresultdo = new ResultDO();
            ObjTimesheetDO.ProjectId = ProjectId;
            retresultdo = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);

            ddlModuleList.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlModuleList.DataValueField = "ModuleId";
            ddlModuleList.DataTextField = "ModuleName";
            ddlModuleList.DataBind();
            ddlModuleList.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetTaskList(int ProjectId, int ModuleId, int empid)
    {
        try
        {
            retresultdo = new ResultDO();
            ObjTimesheetDO.ProjectId = ProjectId;
            ObjTimesheetDO.ModuleId = ModuleId;
            ObjTimesheetDO.EmpId = empid;
            if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 1)
            {
                ObjTimesheetDO.inputvalue = 1;
            }
            else if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 0)
            {
                ObjTimesheetDO.inputvalue = 2;
            }
            else
            {
                ObjTimesheetDO.inputvalue = 0;
            }
            retresultdo = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
            ddlTaskList.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlTaskList.DataValueField = "TaskId";
            ddlTaskList.DataTextField = "TaskName";
            ddlTaskList.DataBind();
            ddlTaskList.Items.Insert(0, "--Select--");
          }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetEmployeeList()
    {
        try
        {
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.GetEmployeeList(ObjTimesheetDO);
            ddlEmployeeList.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlEmployeeList.DataTextField = "FirstName";
            ddlEmployeeList.DataValueField = "Empid";
            ddlEmployeeList.DataBind();
            ddlEmployeeList.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetPriorityList()
    {
        try
        {
            retresultdo = ObjTimesheetBL.GetPriorityList();
            ddlPriority.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlPriority.DataTextField = "TypeName";
            ddlPriority.DataValueField = "TypeOptionID";
            ddlPriority.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnsavs_Click(object sender, EventArgs e)
    {
        try
        {
            int Id = 0;
            Id = Convert.ToInt32(ddlEmployeeList.SelectedItem.Value);
            if (Id != 0)
            {
                if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 1 && selectedid != 0)// Rework
                {
                    ObjTimesheetDO.TaskId = Convert.ToInt32(ddlTaskList.SelectedItem.Value);
                    ObjTimesheetDO.Parentid = Convert.ToInt32(ViewState["parentid"]);
                    ObjTimesheetDO.EmpTaskid = 0;
                    ObjTimesheetDO.RowId = 0;
                    ObjTimesheetDO.Actualstartdate = Convert.ToDateTime("01-01-1900");
                    ObjTimesheetDO.Actualeffort = Convert.ToSingle(0.0);

                }
                else if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 0 && selectedid != 0)// Edit
                {
                    ObjTimesheetDO.TaskId = Convert.ToInt32(ddlTaskList.SelectedItem.Value);
                    ObjTimesheetDO.EmpTaskid = parent_id;
                    ObjTimesheetDO.RowId = selectedid;
                    if (lblactualeffort.Text == "")
                    {
                        ObjTimesheetDO.Actualeffort = Convert.ToSingle(0.0);
                    }
                    else
                    {
                        ObjTimesheetDO.Actualeffort = Convert.ToSingle(lblactualeffort.Text);// ((lblactualeffort.Text == 0.0) ? 0.0 : float.Parse(lblactualeffort.Text));
                    }
                    if (txtactualstartdate.Text == "")
                    {
                        ObjTimesheetDO.Actualstartdate = Convert.ToDateTime("01-01-1900");
                    }
                    else
                    {
                        //ObjTimesheetDO.Actualstartdate = Convert.ToDateTime(txtactualstartdate.Text);
                        ObjTimesheetDO.Actualstartdate = Convert.ToDateTime(DateTime.ParseExact(txtactualstartdate.Text, "dd-MM-yyyy", null));
//txtactualstartdate.Text != "" ? DateTime.Parse(txtactualstartdate.Text) : new DateTime?(); //
                    }
                }
                else
                {
                    ObjTimesheetDO.TaskId = Convert.ToInt32(ddlTaskList.SelectedItem.Value);
                    Response.Write(Convert.ToDateTime("01-01-1900"));
                    Response.Write("\n");
                    ObjTimesheetDO.Actualstartdate = Convert.ToDateTime("01-01-1900"); 
                    ObjTimesheetDO.Actualeffort = Convert.ToSingle(0.0);
                }
                ObjTimesheetDO.EmpId = EmpId;
                ObjTimesheetDO.RowNum = Id;
                ObjTimesheetDO.Priority = Convert.ToInt32(ddlPriority.SelectedItem.Value);

                ObjTimesheetDO.Clients = Convert.ToInt32(ddlclient.SelectedValue);
                ObjTimesheetDO.Taskcat = Convert.ToInt32(ddltaskcat.SelectedValue);
                ObjTimesheetDO.Plannedeffort = Convert.ToSingle(txtplannedeffortdate.Text);
                
               // ObjTimesheetDO.Plannedstartdate = Convert.ToDateTime(txtplanneddate.Text.Trim().ToString());
                ObjTimesheetDO.Plannedstartdate = Convert.ToDateTime(DateTime.ParseExact(txtplanneddate.Text, "dd-MM-yyyy", null));

                ObjTimesheetDO.OTD = ddlotd.SelectedItem.Value;
                ObjTimesheetDO.FTR = ddlftr.SelectedItem.Value;
               
               // ObjTimesheetDO.ToDate = Convert.ToDateTime(txtExpectedCompDate.Text.Trim().ToString());

                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtExpectedCompDate.Text, "dd-MM-yyyy", null));
                int result = ObjTimesheetBL.SaveEmployeeTask(ObjTimesheetDO);
                if (result == 0)
                {
                    string Url = "Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully!');window.location ='" + Url + "';", true);
                }
                else
                {
                    string Url = "Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully!');window.location ='" + Url + "';", true);
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnAddProject_Click(object sender, EventArgs e)// implemented for: Do not allowed same project name to be created again
    {
        try
        {
            ObjTimesheetDO.ProjectName = txtAddProject.Text;
            int result = ObjTimesheetBL.AddNewProjectList(ObjTimesheetDO);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Project Name already exist!')", true);
                pnlAddProject.Visible = false;
            }
            GetProjectList();
            txtAddProject.Text = "";
            pnlAddProject.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnAddModule_Click(object sender, EventArgs e)// implemented for: Do not allowed same module name to be created again
    {
        try
        {
            ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
            ObjTimesheetDO.ModuleName = txtAddModule.Text;
            ObjTimesheetDO.ProjectId = ProjectId;
            int result = ObjTimesheetBL.AddNewModuleList(ObjTimesheetDO);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Module Name already exist!')", true);
                pnlModule.Visible = true;
                txtAddModule.Text = "";
            }
            GetModuleList(ProjectId);
            txtAddModule.Text = "";
            pnlAddModule.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnAddTask_Click(object sender, EventArgs e)// implemented for: Do not allowed same task name to be created again
    {
        try
        {
            ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
            ModuleId = Convert.ToInt32(ddlModuleList.SelectedItem.Value);
            ObjTimesheetDO.TaskName = txtAddTask.Text;
            ObjTimesheetDO.Taskdesc = txtDescription.Text;
            ObjTimesheetDO.ProjectId = ProjectId;
            ObjTimesheetDO.ModuleId = ModuleId;
            int result = ObjTimesheetBL.AddNewTaskList(ObjTimesheetDO);

            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Task Name already exist!')", true);

                txtAddTask.Text = "";
                txtDescription.Text = "";
            }
            GetTaskList(ProjectId, ModuleId, empid);
            txtAddTask.Text = "";
            txtDescription.Text = "";
            pnlAddTask.Visible = false;
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

            Response.Redirect("TaskManage.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlclient.SelectedIndex != 0)
            {
                ClientId = Convert.ToInt32(ddlclient.SelectedItem.Value);
                GetProjectdropdown(ClientId);
                pnlproject.Visible = true;
                pnlcategory.Visible = false;
                pnlModule.Visible = false;
                pnlbtn.Visible = false; 
                
            }
            else
            {
                pnlbtn.Visible = false;
                pnlcategory.Visible = false;
                pnlModule.Visible = false;
                pnlproject.Visible = false;
            }
            pnlAddProject.Visible = false;
            pnlAddModule.Visible = false;
            pnlAddTask.Visible = false;
            pnlTask.Visible = false;
            pnlEmpList.Visible = false;
            ddlEmployeeList.SelectedIndex = 0; 
            ddlProjectList.SelectedIndex = 0;
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetClients()
    {
        try
        {
            ObjTimesheetDO.ProjectId = ProjectId;
            retresultdo = ObjTimesheetBL.GetClients();

            ddlclient.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlclient.DataValueField = "Clientid";
            ddlclient.DataTextField = "ClientShortCode";
            ddlclient.DataBind();
            ddlclient.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetTaskcat()
    {
        try
        {
            ObjTimesheetDO.ProjectId = ProjectId;
            retresultdo = ObjTimesheetBL.GetTaskcat();
            ddltaskcat.DataSource = retresultdo.Resultdtset.Tables[0];
            ddltaskcat.DataValueField = "taskcategoryid";
            ddltaskcat.DataTextField = "taskcategory";
            ddltaskcat.DataBind();
            ddltaskcat.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Block of code is to load the values into the relevant fields for edit/Re-Work

    public void Getemp_dash_details()
    {
        try
        {
            ResultDO resultdo = new ResultDO();
            pnlAddProject.Visible = false;
            pnlModule.Visible = true;
            pnlAddModule.Visible = false;
            pnlTask.Visible = true;
            pnlAddTask.Visible = false;
            pnlEmpList.Visible = true;
            pnlproject.Visible = true;
            pnlcategory.Visible = true;
            ObjTimesheetDO.EmpId = EmpId;
            ObjTimesheetDO.RowId = selectedid;
            resultdo = ObjTimesheetBL.GetEmpTaskEdit(ObjTimesheetDO);
            if (resultdo.Resultdtset.Tables[0].Rows.Count > 0)
            {
                parent_id = Convert.ToInt32(resultdo.Resultdtset.Tables[0].Rows[0]["Id"].ToString());
                ViewState["parentid"] = parent_id;
                ddlclient.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["Clientid"].ToString();
                ddlEmployeeList.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["Empid"].ToString();
                ddlPriority.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["Priority"].ToString();
                txtExpectedCompDate.Text = resultdo.Resultdtset.Tables[0].Rows[0]["ExpectedCompletionDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
                 if(txtExpectedCompDate.Text != "")
                {
                DateTime dt = Convert.ToDateTime(txtExpectedCompDate.Text);
                string NewFormat =   dt.Day.ToString() + "-" + dt.Month.ToString() + "-" + dt.Year.ToString();
                txtExpectedCompDate.Text = NewFormat;    
                }

                ddltaskcat.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["taskcategoryid"].ToString();
                txtplanneddate.Text = resultdo.Resultdtset.Tables[0].Rows[0]["PlannedStartDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
                 if(txtplanneddate.Text != "")
                {
                DateTime dt1 = Convert.ToDateTime(txtplanneddate.Text);
                string NewFormat1 = dt1.Day.ToString() + "-" + dt1.Month.ToString() + "-" + dt1.Year.ToString();
                txtplanneddate.Text = NewFormat1;    
                }

                txtplannedeffortdate.Text = resultdo.Resultdtset.Tables[0].Rows[0]["PlannedEffort"].ToString();
                if (ObjTimesheetDO.TaskName != null && ObjTimesheetDO.reworkid == 0)// EDIT
                {
                    ddlftr.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["FTR"].ToString();
                   ddlotd.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["OTD"].ToString();
                   txtactualstartdate.Text = resultdo.Resultdtset.Tables[0].Rows[0]["ActualStartDate"].ToString().Replace("12:00:00 AM", "").Replace("00:00:00", "");
                    lblactualeffort.Text = resultdo.Resultdtset.Tables[0].Rows[0]["ActualEffort"].ToString();
                }
                ddlProjectList.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["ProjectId"].ToString();
                GetModuleList(Convert.ToInt32(ddlProjectList.SelectedValue));
                ddlModuleList.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["ModuleId"].ToString();
                GetTaskList(Convert.ToInt32(ddlProjectList.SelectedValue), Convert.ToInt32(ddlModuleList.SelectedValue), Convert.ToInt32(ddlEmployeeList.SelectedValue));
                ddlTaskList.SelectedValue = resultdo.Resultdtset.Tables[0].Rows[0]["TaskId"].ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
