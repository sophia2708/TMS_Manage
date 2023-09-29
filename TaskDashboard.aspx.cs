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



public partial class Includes_WebForm_TaskDashboard : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();

    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    public int TaskId = 0;
    public int ClientId;
    DataSet ds = new DataSet();
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

        ObjTimesheetDO.reworkid = Convert.ToInt32(Request.QueryString["taskmaster"]);

        if (!Page.IsPostBack)
        {
            chkmytask.Checked = false;
            GetEmpDashBoard();
            GetEmpTaskSummary(1);
            GetclientBoard();

            clientgrid.Visible = true;
            grdTaskSummary.Visible = true;
            if (ObjTimesheetDO.reworkid == 1)
            {
                Getmasterboard();
                grdtaskmaster.Visible = true;
                grdTaskSummary.Visible = false;
            }
            else
            {
                grdtaskmaster.Visible = false;

            }
        }

    }

    public void GetclientBoard()
    {
        ObjTimesheetDO.EmpId = EmpId;
        retresultdo = ObjTimesheetBL.GetclientBoard(ObjTimesheetDO);

        clientgrid.DataSource = retresultdo.Resultdtset.Tables[0];
        clientgrid.DataBind();
    }


    public void GetEmpDashBoard()
    {
        try
        {
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.GetEmpDashBoard(ObjTimesheetDO);
            lblForDay.Text = retresultdo.Resultdtset.Tables[0].Rows[0][0].ToString();
            lblForWeek.Text = retresultdo.Resultdtset.Tables[0].Rows[0][1].ToString();
            lblForMonth.Text = retresultdo.Resultdtset.Tables[0].Rows[0][2].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetEmpTaskSummary(int inputvalue)
    {
        try
        {
            if (inputvalue == 0)
            {
                ObjTimesheetDO.EmpId = EmpId;
                ObjTimesheetDO.inputvalue = inputvalue;
                retresultdo = ObjTimesheetBL.GetEmpTaskSummary(ObjTimesheetDO);
                //grdTaskSummary.Columns[2].Visible = false;
                if (EmpId == 5 || EmpId == 31)
                {
                    grdTaskSummary.Columns[12].Visible = true;
                }
                else
                {
                    grdTaskSummary.Columns[12].Visible = false;
                }
                grdTaskSummary.DataSource = retresultdo.Resultdtset.Tables[0];
                grdTaskSummary.DataBind();
            }
            else
            {
                ObjTimesheetDO.EmpId = EmpId;
                ObjTimesheetDO.inputvalue = inputvalue;
                retresultdo = ObjTimesheetBL.GetEmpTaskSummary(ObjTimesheetDO);
                grdTaskSummary.DataSource = retresultdo.Resultdtset.Tables[0];
                //grdTaskSummary.Columns[2].Visible = false;
                if (EmpId == 5 || EmpId == 31)
                {
                    grdTaskSummary.Columns[12].Visible = true;
                }
                else
                {
                    grdTaskSummary.Columns[12].Visible = false;
                }
                grdTaskSummary.DataBind();
            }
            DataTable resultdset = new DataTable();
            resultdset = retresultdo.Resultdtset.Tables[0];
            ViewState["Result"] = resultdset;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void GetEmpTaskEdit()
    {
        int rowIndex = 0;
        try
        {
            Label task = (Label)grdTaskSummary.Rows[rowIndex].Cells[5].FindControl("lbltask");
            ObjTimesheetDO.TaskId = Convert.ToInt32(task);
            retresultdo = ObjTimesheetBL.GetEmpTaskEdit(ObjTimesheetDO);
            ObjTimesheetDO.edittable = retresultdo.Resultdtset;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void completedtask_CheckedChanged(object sender, EventArgs e)
    {
        if (compbox.Checked == true)
        {
            int inputvalue = 0;
            grdTaskSummary.Columns[2].Visible = false;
            GetEmpDashBoard();
            GetEmpTaskSummary(inputvalue);
            grdTaskSummary.Visible = true;
            grdtaskmaster.Visible = false;
            //chkmytask.Checked = false;

        }
        else
        {
            int inputvalue = 1;
            grdTaskSummary.Columns[2].Visible = false;
            GetEmpDashBoard();
            GetEmpTaskSummary(inputvalue);
            grdTaskSummary.Visible = true;
            grdtaskmaster.Visible = false;
            //chkmytask.Checked = true;

        }



    }

    protected void chkmytask_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkmytask.Checked == false)
            {
                int inputvalue = 1;
                GetEmpDashBoard();
                grdTaskSummary.Columns[2].Visible = true;
                GetEmpTaskSummary(inputvalue);
                grdTaskSummary.Visible = true;
                grdtaskmaster.Visible = false;


            }
            else
            {
                int inputvalue = 1;
                GetEmpDashBoard();
                grdTaskSummary.Columns[2].Visible = false;
                GetEmpTaskSummary(inputvalue);
                grdTaskSummary.Visible = true;
                grdtaskmaster.Visible = false;

            }
            //compbox.Checked = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public SortDirection dir
    {
        get
        {

            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }

            return (SortDirection)ViewState["dirState"];
        }

        set
        {
            ViewState["dirState"] = value;
        }
    }

    protected void grdTaskSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable resultdset = new DataTable();
        DataTable dt = (DataTable)ViewState["Result"];

        {

            string SortDir = string.Empty;

            if (dir == SortDirection.Ascending)
            {

                dir = SortDirection.Descending;

                SortDir = "Desc";

            }

            else
            {
                dir = SortDirection.Ascending;

                SortDir = "Asc";

            }

            DataView sortedView = new DataView(dt);

            sortedView.Sort = e.SortExpression + " " + SortDir;

            grdTaskSummary.DataSource = sortedView;

            grdTaskSummary.DataBind();

        }

    }

    protected void grdTaskSummary_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdTaskSummary.EditIndex = e.NewEditIndex;


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void clientgrd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        Label clients = (Label)clientgrid.FindControl("lblclient");
        Label hours = (Label)clientgrid.FindControl("lblhour");

        ObjTimesheetDO.EmpId = EmpId;
        retresultdo = ObjTimesheetBL.GetclientBoard(ObjTimesheetDO);

    }

    protected void grdTaskSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ObjTimesheetDO.EmpId = EmpId;

                DateTime today = DateTime.Today;
                Label complbl = (Label)e.Row.FindControl("lblExpCompDate");
                DateTime compldate = Convert.ToDateTime(DateTime.ParseExact(complbl.Text, "dd-MM-yyyy", null));

                Label lblstatus = (Label)e.Row.FindControl("lblStatus");
                string statusname = lblstatus.Text;

                if (compldate < today && statusname == "In Progress")
                {

                    e.Row.Cells[6].BackColor = System.Drawing.ColorTranslator.FromHtml("#ffb366");
                }


                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    ResultDO ds_ProjectList = ObjTimesheetBL.GetProjectList();
                    ResultDO ds_Clients = ObjTimesheetBL.GetClients();
                    ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
                    ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
                    ResultDO ds_Status = ObjTimesheetBL.Getstatus();
                    DataRowView dr = e.Row.DataItem as DataRowView;


                    DropDownList ddlProject = (DropDownList)e.Row.FindControl("ddlProject");
                    ddlProject.DataSource = ds_ProjectList.Resultdtset;
                    ddlProject.DataValueField = "ProjectId";
                    ddlProject.DataTextField = "ProjectName";
                    ddlProject.DataBind();
                    ddlProject.Items.Insert(0, "--Select--");

                    ddlProject.SelectedValue = dr["ProjectId"].ToString();

                    DropDownList ddlclient = (DropDownList)e.Row.FindControl("ddlclient");
                    ddlclient.DataSource = ds_Clients.Resultdtset;
                    ddlclient.DataValueField = "Clientid";
                    ddlclient.DataTextField = "ClientShortCode";
                    ddlclient.DataBind();
                    ddlclient.Items.Insert(0, "--Select--");

                    ddlclient.SelectedValue = dr["Clientid"].ToString();

                    DropDownList ddlModuleList = (DropDownList)e.Row.FindControl("ddlModuleList");
                    ObjTimesheetDO.EmpId = EmpId;
                    ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
                    ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);

                    ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
                    ddlModuleList.DataValueField = "ModuleId";
                    ddlModuleList.DataTextField = "ModuleName";
                    ddlModuleList.DataBind();
                    ddlModuleList.Items.Insert(0, "--Select--");
                    ddlModuleList.SelectedValue = dr["ModuleId"].ToString();

                    DropDownList ddlTask = (DropDownList)e.Row.FindControl("ddlTask");
                    ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
                    ObjTimesheetDO.ModuleId = Convert.ToInt32(ddlModuleList.SelectedValue.ToString());

                    ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
                    ddlTask.DataSource = ds_task.Resultdtset;
                    ddlTask.DataValueField = "TaskId";
                    ddlTask.DataTextField = "TaskName";
                    ddlTask.DataBind();
                    ddlTask.Items.Insert(0, "--Select--");
                    ddlTask.SelectedValue = dr["TaskId"].ToString();

                    DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    ddlStatus.DataSource = ds_Status.Resultdtset;
                    ddlStatus.DataValueField = "TypeOptionID";
                    ddlStatus.DataTextField = "TypeName";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, "--Select--");
                    ddlStatus.SelectedValue = dr["TaskStatus"].ToString();


                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void BindStatus()
    //{
    //    ObjTimesheetDO.EmpId = EmpId;
    //    ResultDO ds_Status = ObjTimesheetBL.Getstatus();
    //    DropDownList ddlStatus = (DropDownList)grdTaskSummary.Rows[0].FindControl("ddlStatus");
    //    ddlStatus.DataSource = ds_Status.Resultdtset;
    //    ddlStatus.DataValueField = "TypeOptionID";
    //    ddlStatus.DataTextField = "TypeName";
    //    ddlStatus.DataBind();

    //}

    protected void BindProject()
    {
        ObjTimesheetDO.EmpId = EmpId;
        ResultDO ds_ProjectList = ObjTimesheetBL.GetProjectList();
        DropDownList ddlProject = (DropDownList)grdTaskSummary.Rows[0].FindControl("ddlProject");
        ddlProject.DataSource = ds_ProjectList.Resultdtset;
        ddlProject.DataValueField = "ProjectId";
        ddlProject.DataTextField = "ProjectName";
        ddlProject.DataBind();

        ds_ProjectList.Resultdtset.Clear();
    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlModuleList = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlModuleList");
        DropDownList ddlTask = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlTask");
        ddlModuleList.Enabled = false;
        ddlTask.Enabled = false;
        ddlModuleList.Items.Clear();
        ddlTask.Items.Clear();
        ddlModuleList.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList ddlProject = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlProject");
        int ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : int.Parse(ddlProject.SelectedItem.Value));
        if (ProjectId > 0)
        {
            BindModule(rowindex);
        }
        ddlModuleList.Focus();
    }

    protected void ddlclient_selectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
            //Get rowindex
            int rowindex = gvr.RowIndex;
            DropDownList ddlProject = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlProject");
            DropDownList ddlModuleList = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlModuleList");
            DropDownList ddlTask = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlTask");

            ddlProject.Enabled = false;
            ddlModuleList.Enabled = false;
            ddlTask.Enabled = false;

            ddlProject.Items.Clear();
            ddlModuleList.Items.Clear();
            ddlTask.Items.Clear();

            ddlProject.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlModuleList.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));

            DropDownList ddlclient = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlclient");
            int Clientid = ((ddlclient.SelectedIndex == 0) ? 0 : int.Parse(ddlclient.SelectedItem.Value));
            if (Clientid > 0)
            {
                BindProject();
            }
            ddlProject.Focus();
            ddlProject.Enabled = true;

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindModule(int rowindex)
    {
        DropDownList ddlProject = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlProject");
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
        ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
        DropDownList ddlModuleList = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlModuleList");
        ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
        ddlModuleList.DataValueField = "ModuleId";
        ddlModuleList.DataTextField = "ModuleName";
        ddlModuleList.Enabled = true;
        ddlModuleList.DataBind();
    }

    protected void ddlModuleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlModuleList = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlModuleList");
        DropDownList ddlTask = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlTask");
        ddlTask.Enabled = false;
        ddlTask.Items.Clear();
        ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
        int ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : int.Parse(ddlModuleList.SelectedItem.Value));
        if (ModuleId > 0)
        {
            BindTask(rowindex);
        }
        ddlTask.Focus();
    }

    protected void BindTask(int rowindex)
    {
        retresultdo = new ResultDO();
        DropDownList ddlProject = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlProject");
        ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
        DropDownList ddlModuleList = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlModuleList");
        ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
        ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
        DropDownList ddlTask = (DropDownList)grdTaskSummary.Rows[rowindex].FindControl("ddlTask");
        ddlTask.DataSource = ds_task.Resultdtset;
        ddlTask.DataValueField = "TaskId";
        ddlTask.DataTextField = "TaskName";
        ddlTask.Enabled = true;
        ddlTask.DataBind();
    }

    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
    }

    public void GetClients()
    {
        try
        {
            DropDownList ddlclient1 = (DropDownList)clientgrid.FindControl("ddlclient");
            DropDownList ddlclient = (DropDownList)grdTaskSummary.FindControl("ddlclient");
            DropDownList ddlProject = (DropDownList)grdTaskSummary.FindControl("ddlProject");
            ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
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

    public void GetProjectdropdown(int clientid)
    {
        try
        {
            ObjTimesheetDO.Clients = clientid;
            DropDownList ddlProject = (DropDownList)grdTaskSummary.FindControl("ddlProject");
            retresultdo = ObjTimesheetBL.GetProjectdropdown(ObjTimesheetDO);
            ddlProject.DataSource = retresultdo.Resultdtset.Tables[0];
            ddlProject.DataValueField = "ProjectId";
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "--Select--");
            ddlProject.Items.Add("--Add New Project--");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void EmployeeList()
    {
        try
        {
            ObjTimesheetDO.EmpId = EmpId;
            DropDownList ddlEmployeeList = (DropDownList)grdTaskSummary.FindControl("ddlEmployeeList");
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

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
            string selectedid = grdTaskSummary.Rows[Index].Cells[0].Text;
            Label task = (Label)grdTaskSummary.Rows[Index].Cells[5].FindControl("lbltask");
            for (int i = 0; i < grdTaskSummary.Rows.Count; i++)
            {
                Index++;
                ObjTimesheetDO.TaskName = task.Text;
            }
            ObjTimesheetDO.EmpId = EmpId;
            ObjTimesheetDO.RowId = Convert.ToInt32(selectedid);
            retresultdo = ObjTimesheetBL.GetEmpTaskEdit(ObjTimesheetDO);
            Response.Redirect("TaskManage.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username + "&task=" + task.Text + "&rework=0&selectedid=" + selectedid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // code to Re-work

    protected void lnkbtntask_Click(object sender, EventArgs e)
    {
        try
        {
            int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
            string selectedid = grdTaskSummary.Rows[Index].Cells[0].Text;
            Label task = (Label)grdTaskSummary.Rows[Index].Cells[5].FindControl("lbltask");
            Label status = (Label)grdTaskSummary.Rows[Index].Cells[7].FindControl("lblStatus");

            for (int i = 0; i < grdTaskSummary.Rows.Count; i++)
            {
                Index++;
                ObjTimesheetDO.TaskName = task.Text;
            }
            ObjTimesheetDO.EmpId = EmpId;
            ObjTimesheetDO.RowId = Convert.ToInt32(selectedid);
            retresultdo = ObjTimesheetBL.GetEmpTaskEdit(ObjTimesheetDO);
            Response.Redirect("TaskManage.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username + "&task=" + task.Text + "&rework=1&selectedid=" + selectedid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Getmasterboard()
    {
        try
        {
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.GetTaskMaster(ObjTimesheetDO);
            grdtaskmaster.DataSource = retresultdo.Resultdtset.Tables[0];
            grdtaskmaster.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkedit_Click(object sender, EventArgs e)
    {
        try
        {
            int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
            Label task = (Label)grdtaskmaster.Rows[Index].Cells[5].FindControl("lbltask");
            for (int i = 0; i < grdtaskmaster.Rows.Count; i++)
            {
                Index++;
                ObjTimesheetDO.TaskName = task.Text;
            }
            ObjTimesheetDO.EmpId = EmpId;
            retresultdo = ObjTimesheetBL.GetTaskMasteredit(ObjTimesheetDO);
            Response.Redirect("TaskMaster.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username + "&task=" + task.Text + "&update=1");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lnltaskboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username + "&taskmaster=1");
        // chkmytask.Checked = false;
        //chkmytask.Enabled = false;
    }
}