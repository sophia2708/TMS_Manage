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
using System.Data.SqlClient;
using AnalyticBrainsDO;
using AnalyticBrainsBL;
using AnalyticBrainsDAL;
using System.Globalization;

public partial class TaskMaster : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();

    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public int TaskName = 0;
    public int ProjectId;

    public int ModuleId;
    public int TaskId;
    public int ClientId;
    public int parent_id;
    public int ftr;

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

        txtplanneddate.Text = Request[this.txtplanneddate.UniqueID];

        txtplanndend.Text = Request[this.txtplanndend.UniqueID];

        ObjTimesheetDO.TaskName = Request.QueryString["task"];

        ObjTimesheetDO.Parentid = Convert.ToInt32(Request.QueryString["update"]);

        ObjTimesheetDO.reworkid = Convert.ToInt32(Request.QueryString["taskmaster"]);

        if (!IsPostBack)
        {
            GetClients();
            GetProjectList();
            GetPriorityList();
            pnlAddProject.Visible = false;
            pnlModule.Visible = false;
            pnlAddModule.Visible = false;
            //pnlTask.Visible = false;
            pnladdclient.Visible = false;
            pnlAddTask.Visible = false;
            pnlproject.Visible = false;
            pnltaskdes.Visible = false;
            if(ObjTimesheetDO.TaskName != null)
            {
                Gettaskmasteredit();
            }
        }
    }

    protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            if(ddlProjectList.SelectedValue.ToString() == "--Add New Project--")
            {
                pnlAddProject.Visible = true;
                pnlModule.Visible = false;
                //pnlTask.Visible = false;
                pnlAddModule.Visible = false;
                pnlAddTask.Visible = false;
                pnltaskdes.Visible = false;
                Button2.Visible = false;
            }
            else
            {
                if(ddlProjectList.SelectedIndex != 0)
                {
                    ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
                    GetModuleList(ProjectId);
                    pnlModule.Visible = true;
                    Button2.Visible = true;
                    Button2.Enabled = false;
                    pnltaskdes.Visible = false;
                 }
                else
                {
                    pnltaskdes.Visible = false;
                    pnlModule.Visible = false;
                }
                pnlAddProject.Visible = false;
                pnlAddModule.Visible = false;
                pnlAddTask.Visible = false;
                //pnlTask.Visible = false;
            }
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
            Button2.Enabled = false;
            if (ddlModuleList.SelectedValue.ToString() == "--Add New Module--")
            {
                pnlAddModule.Visible = true;
                pnlAddProject.Visible = false;
                //pnlTask.Visible = false;
                pnltaskdes.Visible = false;
                pnlAddTask.Visible = false;
                Button2.Visible = false;
            }
            else
            {
                if (ddlModuleList.SelectedIndex != 0)
                {
                    Button2.Visible = true;
                    Button2.Enabled = true;
                    ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
                    ModuleId = Convert.ToInt32(ddlModuleList.SelectedItem.Value);
                    pnltaskdes.Visible = false;
                    //GetTaskList(ProjectId, ModuleId);
                    //pnlTask.Visible = true;
                }
                else
                {
                    Button2.Visible = true;
                    pnltaskdes.Visible = false;
                    Button2.Enabled = false;
                    pnltaskdes.Visible = false;
                    //pnlTask.Visible = false;
                }
                pnlAddProject.Visible = false;
                pnlAddModule.Visible = false;
                pnlAddTask.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void ddlTaskList_SelectedIndexChanged(object sender, EventArgs e) Commented to covert "addtask" dropdown into "addTask" Button
    //{
    //    try
    //    {
    //        if (ddlTaskList.SelectedValue.ToString() == "--Add New Task--")
    //        {
    //            pnlAddModule.Visible = false;
    //            pnlAddProject.Visible = false;
    //            pnlAddTask.Visible = true;
    //            pnltaskdes.Visible = true;
    //        }
    //        else
    //        {
    //            if (ddlTaskList.SelectedIndex != 0)
    //            {
    //                pnlAddTask.Visible = false;
    //                pnltaskdes.Visible = true;
    //                GetPriorityList();
    //                TaskId = Convert.ToInt32(ddlTaskList.SelectedItem.Value);
    //            }
    //            else
    //            {

    //            }
    //            pnlAddProject.Visible = false;
    //            pnlAddModule.Visible = false;
    //            pnlAddTask.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public void GetProjectdropdown(int clientid)
    {
        try
        {
            ObjTimesheetDO.Clients = clientid;
            retresultdo = ObjTimesheetBL.GetProjectdropdown(ObjTimesheetDO);
            ddlProjectList.DataSource = retresultdo.Resultdtset.Tables[0];
            //if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
            //{
                ddlProjectList.DataValueField = "ProjectId";
                ddlProjectList.DataTextField = "ProjectName";
                ddlProjectList.DataBind();
                ddlProjectList.Items.Insert(0, "--Select--");
                ddlProjectList.Items.Insert(1, "--Add New Project--");
                //ddlProjectList.Items.Add("--Add New Project--");
            //}
            //else
            //{
               
            //}
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
            retresultdo = ObjTimesheetBL.GetProjectList();
            ddlProjectList.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlProjectList.DataValueField = "ProjectId";
            ddlProjectList.DataTextField = "ProjectName";
            ddlProjectList.DataBind();
            ddlProjectList.Items.Insert(0, "--Select--");
            ddlProjectList.Items.Add("--Add New Project--");
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
           ObjTimesheetDO.ProjectId = ProjectId;
           retresultdo = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
           ddlModuleList.DataSource = retresultdo.Resultdtset.Tables[0];
           ddlModuleList.DataValueField = "ModuleId";
           ddlModuleList.DataTextField = "ModuleName";
           ddlModuleList.DataBind();
           ddlModuleList.Items.Insert(0, "--Select--");
           ddlModuleList.Items.Insert(1, "--Add New Module--");
           //ddlModuleList.Items.Add("--Add New Module--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public void GetTaskList(int projectid, int moduleid) 
    //{
    //    try
    //    {
    //        objtimesheetdo.projectid = projectid;
           
    //        objtimesheetdo.moduleid = moduleid;
    //        retresultdo = objtimesheetbl.gettasklist(objtimesheetdo);
    //        ddltasklist.datasource = retresultdo.resultdtset.tables[0];
    //        ddltasklist.datavaluefield = "taskid";
    //        ddltasklist.datatextfield = "taskname";
    //        ddltasklist.items.clear();
    //        ddltasklist.databind();  commented to clear existing tasklist
    //        ddltasklist.items.insert(0, "--select--");
    //        ddltasklist.items.insert(1, "--add new task--");
    //        ddltasklist.items.add("--add new task--");
    //    }
    //    catch (exception ex)
    //    {
    //        throw ex;
    //    }
    //}

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
    
    protected void btnAddProject_Click(object sender, EventArgs e)// implemented for: Do not allowed same project name to be created again
    {
        try
        {
            ObjTimesheetDO.ProjectName = txtAddProject.Text;
            ObjTimesheetDO.Clients = Convert.ToInt32(ddlclient.SelectedValue);
            int result = ObjTimesheetBL.AddNewProjectList(ObjTimesheetDO);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Project Name already exist!')", true);
                pnlAddProject.Visible = false;
            }
            GetProjectdropdown(Convert.ToInt32(ddlclient.SelectedValue));
            txtAddProject.Text = "";
            pnlAddProject.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAddModule_Click(object sender, EventArgs e) //implemented for: Do not allowed same module name to be created again
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

    protected void btnAdd_Click(object sender, EventArgs e) //implemented for: Do not allowed same task name to be created again
    {
        Button2.Visible = false;
        pnlAddModule.Visible = false;
        pnlAddProject.Visible = false;
        pnlAddTask.Visible = true;
        pnltaskdes.Visible = true;
        txtAddTask.Text = "";
        txtDescription.Text = "";
        ddlPriority.SelectedIndex = 0;
    }

    protected void btnAddTask_Click(object sender, EventArgs e) //implemented for: Do not allowed same task name to be created again
    {
        try
        {
            ProjectId = Convert.ToInt32(ddlProjectList.SelectedItem.Value);
            ModuleId = Convert.ToInt32(ddlModuleList.SelectedItem.Value);
            ObjTimesheetDO.TaskName = txtAddTask.Text;
            ObjTimesheetDO.Taskdesc = txtDescription.Text;
            ObjTimesheetDO.ProjectId = ProjectId;
            ObjTimesheetDO.ModuleId = ModuleId;
            
            //if (ddlTaskList.SelectedItem.Value != "--Add New Task--")
            //{
            //    ObjTimesheetDO.TaskId = Convert.ToInt32(ddlTaskList.SelectedItem.Value);
            //}
            //else
            //{
            //    TaskId = 0;
            //}
            
            ObjTimesheetDO.Status = Convert.ToInt32(ddlPriority.SelectedItem.Value);       
            //ObjTimesheetDO.plannedstartdtask = Convert.ToDateTime(txtplanneddate.Text);
            ObjTimesheetDO.plannedstartdtask = Convert.ToDateTime(DateTime.ParseExact(txtplanneddate.Text, "dd-MM-yyyy", null));
            //ObjTimesheetDO.plannedenddate = Convert.ToDateTime(txtplanndend.Text);
            ObjTimesheetDO.plannedenddate = Convert.ToDateTime(DateTime.ParseExact(txtplanndend.Text, "dd-MM-yyyy", null));
            ObjTimesheetDO.Clients = Convert.ToInt32(ddlclient.SelectedItem.Value);
            int result = ObjTimesheetBL.AddNewTaskList(ObjTimesheetDO);
            if(result == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Task Name already exist!')", true);
                txtAddTask.Text = "";
                txtDescription.Text = "";
            }
            if(ObjTimesheetDO.Parentid == 1)
            {
               Response.Redirect("Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username + "&taskmaster=1");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully!')", true);
            }
            //GetTaskList(ProjectId, ModuleId);
            Response.Redirect("Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username + "&taskmaster=1");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Task Created Successfully!')", true);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.Redirect("TaskMaster.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
        //ddlclient.Visible = true;
        //lblProject.Visible = false;
        //lblModule.Visible = false;
        //ddlModuleList.Visible = false;
        //ddlProjectList.Visible = false;
        //Button2.Visible = false;
        //pnlAddTask.Visible = false;
        //pnltaskdes.Visible = false;
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

            if (ddlclient.SelectedValue.ToString() == "--Add New Client--")
            {
                pnladdclient.Visible = true;
                pnlModule.Visible = false;
                //pnlTask.Visible = false;
                pnlproject.Visible = false;
                pnlAddModule.Visible = false;
                pnlAddTask.Visible = false;
                pnltaskdes.Visible = false;
                pnlAddProject.Visible = false;
            }
            else
            {
                if (ddlclient.SelectedIndex != 0)
                {
                    pnladdclient.Visible = false;
                    ClientId = Convert.ToInt32(ddlclient.SelectedItem.Value);
                    GetProjectdropdown(ClientId);
                    pnlproject.Visible = true;
                    pnlModule.Visible = false;
                    pnltaskdes.Visible = false;
                   
                }
                else
                {
                    pnladdclient.Visible = false;
                    pnltaskdes.Visible = false;
                    pnlAddModule.Visible = false;
                    pnlproject.Visible = false;
                    pnlModule.Visible = false;
                }
                pnlAddProject.Visible = false;
                pnlAddModule.Visible = false;
                pnlAddTask.Visible = false;
                //pnlTask.Visible = false;
            }
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
            ddlclient.Items.Insert(1, "--Add New Client--");
            //ddlclient.Items.Add("--Add New Clilent--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Gettaskmasteredit()
    {
        try 
        {
            pnlproject.Visible = true;
            //pnlTask.Visible = true;
            pnlModule.Visible = true;
            pnltaskdes.Visible = true;
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.GetTaskMasteredit(ObjTimesheetDO);
            ddlPriority.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["Status"].ToString();
            txtplanneddate.Text = retresultdo.Resultdtset.Tables[0].Rows[0]["PlannedStartDate"].ToString().Replace("12:00:00 AM", "");
            txtplanndend.Text = retresultdo.Resultdtset.Tables[0].Rows[0]["PlannedendDate"].ToString().Replace("12:00:00 AM", "");
            ddlclient.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["ClientId"].ToString();
            ddlProjectList.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["ProjectId"].ToString();
            int proj = Convert.ToInt32(ddlProjectList.SelectedValue);
            GetModuleList(proj);
            ddlModuleList.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["ModuleId"].ToString();
            int mod = Convert.ToInt32(ddlModuleList.SelectedValue);
            //GetTaskList(proj, mod);
            //ddlTaskList.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["TaskId"].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnaddclient_Click(object sender, EventArgs e)
    {
       
        try
        {
            ObjTimesheetDO.ClientName = txtaddclient.Text;
            //ObjTimesheetDO.Clients = Convert.ToInt32(ddlclient.SelectedValue);
            int result = ObjTimesheetBL.AddNewClientlist(ObjTimesheetDO);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Client Name already exist!')", true);
                pnlAddProject.Visible = false;
            }
            if (ddlclient.SelectedValue.ToString() != "--Add New Client--")
            {
            GetProjectdropdown(Convert.ToInt32(ddlclient.SelectedValue));
            }

            txtAddProject.Text = "";
            pnlAddProject.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
     }
}