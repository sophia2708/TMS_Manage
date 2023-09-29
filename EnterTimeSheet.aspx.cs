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

public partial class Includes_WebForm_EnterTimeSheet : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();
    public int i = 0;
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    Decimal total = 0;
    public int ProjectId;
    public int ModuleId;
    public int TaskId;
    string[] HolidayArray = { }; //Added by sof
    protected void Page_Load(object sender, EventArgs e)
    {
        //Ajax.Utility.RegisterTypeForAjax(typeof(Includes_WebForm_EnterTimeSheet));
        
        btnSave.Enabled = true;
        Session["TotHour"] = 0;
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
        
        this.txtDate.Text = Request[this.txtDate.UniqueID];

        this.txtplanneddate.Text = Request[this.txtplanneddate.UniqueID];
        this.txtplanndend.Text = Request[this.txtplanndend.UniqueID];

        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            GetEmpTimesheet(); //call this function here added by sophia

            //GetClients();
            GetPriorityList();
            GetTaskcat();
            Getholidaylist();
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["CurrentTable"];
            
            if (dt.Rows.Count == 0)
            {
                SetInitialRow();
            }
            //if (EmpId == 35 || EmpId == 45)
            //{
            //    ddlworkmode.Visible = false;
            //    lblworkmode.Visible = false;

            //}
            
            //divgrid.Style.Add("overflow-y", "none");
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        getWM();
        GetEmpTimesheet();

        if (i == 1)
        {

        }
        else if (i == 2)
        {
            SetInitialRow();
        }
        else
        {

        }
        btnSave.Visible = true;
    }

    protected void grdEnterTimesheet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ResultDO ds_Project = ObjTimesheetBL.GetProjectList();
        ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
        ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);

        ResultDO ds_Status = ObjTimesheetBL.Getstatus();

        decimal temp = 24;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.RowIndex == 0)
            {
                e.Row.Style.Add("height", "135px");
                e.Row.Style.Add("vertical-align", "Center");
            }

            e.Row.Cells[14].Attributes.Add("onClick", "return confirm('Do you want to delete this record?');");

            total += Convert.ToDecimal((DataBinder.Eval(e.Row.DataItem, "Hours").ToString() == "" ? "0" : DataBinder.Eval(e.Row.DataItem, "Hours")));

            DropDownList ddlProject = e.Row.FindControl("ddlProject") as DropDownList;
            ddlProject.DataSource = ds_Project.Resultdtset;
            ddlProject.DataValueField = "ProjectId";
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "--Select--");

            DropDownList ddlModuleList = e.Row.FindControl("ddlModuleList") as DropDownList;
            ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
            ddlModuleList.DataValueField = "ModuleId";
            ddlModuleList.DataTextField = "ModuleName";
            ddlModuleList.DataBind();
            ddlModuleList.Items.Insert(0, "--Select--");
            ddlModuleList.Enabled = false;

            DropDownList ddlTask = e.Row.FindControl("ddlTask") as DropDownList;
            ddlTask.DataSource = ds_task.Resultdtset;
            ddlTask.DataValueField = "TaskId";
            ddlTask.DataTextField = "TaskName";
            ddlTask.DataBind();
            ddlTask.Items.Insert(0, "--Select--");
            ddlTask.Enabled = false;

            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
            ddlStatus.DataSource = ds_Status.Resultdtset;
            ddlStatus.DataValueField = "TypeOptionID";
            ddlStatus.DataTextField = "TypeName";
            ddlStatus.DataBind();

            

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalhours = (Label)e.Row.FindControl("lblTotalHours");
            lblTotalhours.Text = total.ToString();
            if (total >= temp)
            {
                lblTotalhours.ForeColor = System.Drawing.Color.Red;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Hours Exceeds 24!')", true);
                Button ButtonAdd = (Button)e.Row.FindControl("ButtonAdd");
                ButtonAdd.Enabled = true;
                btnSave.Enabled = false;
            }
            else
            {
                //lblTotalhours.Visible = true;
            }
        }
    }

    protected void grdEnterTimesheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        getrow();
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.FromDate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        int index = Convert.ToInt32(e.RowIndex);
        GridViewRow row = grdEnterTimesheet.Rows[index];
        Label lblRowId = row.FindControl("lblRowId") as Label;
        if (lblRowId.Text != "" && lblRowId.Text != "0")
        {
            int RowId = Convert.ToInt32(lblRowId.Text);
            ObjTimesheetDO.RowId = RowId;
            ObjTimesheetBL.DeleteEnterTimeSheet(ObjTimesheetDO);
            //to reset the rownum after deleteing a row
            ResetRowID(dt);
        }
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[index];
            dr.Delete();
        }

        dt.AcceptChanges();
        //to reset the rownum after deleteing a row
        ResetRowID(dt);
        //getrow();
        ViewState["CurrentTable"] = dt;
        grdEnterTimesheet.DataSource = dt;
        grdEnterTimesheet.DataBind();
        SetPreviousData1();
        DataTable dtt = new DataTable();
        dtt = (DataTable)ViewState["CurrentTable"];
        if (dtt.Rows.Count == 0)
        {
            SetInitialRow();

        }

    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();

    }
    protected void btnYes_Click(object sender, EventArgs e) 
    {
        Timesheetsave();
    }
    protected void btnSave_Click(object sender, EventArgs e) //changes added by sof
    {
        //int day = 0;
        
        
        DateTime date = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));
        if (ViewState["CurrentTable"] != null)
        {
            try
            {
                retresultdo = ObjTimesheetBL.Getholidaylist();
                holiDayList.Value = retresultdo.Resultdtset.Tables[1].Rows[0].ItemArray[0].ToString();
                HolidayArray = new[] { retresultdo.Resultdtset.Tables[0].Rows[0].ItemArray[0].ToString() };
                //if (day == 6 || day == 0)
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "weekdays(0);", true);
                else if (HolidayArray[0].Contains(date.ToString("dd-MM-yyyy")))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "weekdays(1);", true);
                else
                    Timesheetsave();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        
    }
    protected void Timesheetsave()
    {
        int rowIndex = 0;
        DataTable dt = (DataTable)ViewState["CurrentTable"];
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[1].FindControl("ddlProject");
                DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[2].FindControl("ddlModuleList");
                DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[3].FindControl("ddlTask");
                TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[4].FindControl("txtTaskDesc");
                TextBox Issues = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[5].FindControl("txtIssues");
                TextBox Object = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[6].FindControl("txtObject");
                DropDownList ddlStatus = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[7].FindControl("ddlStatus");
                TextBox Hours = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[8].FindControl("txtHours");
                //DropDownList ddlmodeofwork = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("ddlworkmode");
                Label RowId = (Label)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("lblRowId");

                if (RowId.Text == "")
                {
                    int j = 0;
                    RowId.Text = j.ToString();
                }

                rowIndex++;
                ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));                
                ObjTimesheetDO.ModeofWork = Convert.ToInt32(ddlworkmode.SelectedItem.Value);                           
                ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedItem.Value);
                ObjTimesheetDO.ModuleId = Convert.ToInt32(ddlModuleList.SelectedItem.Value);
                ObjTimesheetDO.TaskId = Convert.ToInt32(ddlTask.SelectedItem.Value);
                ObjTimesheetDO.Issues = Convert.ToString(Issues.Text);
                ObjTimesheetDO.Taskdesc = Convert.ToString(Description.Text);
                ObjTimesheetDO.Object = Convert.ToString(Object.Text);
                ObjTimesheetDO.Status = Convert.ToInt32(ddlStatus.SelectedItem.Value);
                ObjTimesheetDO.Hours = Convert.ToString(Hours.Text);
                //ObjTimesheetDO.ModeofWork = Convert.ToInt32(ddlmodeofwork.SelectedItem.Value);
                ObjTimesheetDO.EmpId = EmpId;
                ObjTimesheetDO.RowNum = Convert.ToInt32(RowId.Text);

                //&& ddlStatus.SelectedItem.Text!="In Progress"
                if (Hours.Text.Length > 0)
                {
                    ObjTimesheetBL.SaveTimesheetDetail(ObjTimesheetDO);

                }

            }

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully!');", true);
        GetEmpTimesheet();
    }
    //till here
    public void getWM() {
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.FromDate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));
        retresultdo = ObjTimesheetBL.GetWorkmodeStatus(ObjTimesheetDO);
        if (retresultdo.Resultdtset.Tables[0].Rows.Count == 0)
        {
            ddlworkmode.SelectedIndex  = 0;
        }
        else
        {
        ddlworkmode.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0][0].ToString();
        }
        //ddlworkmode.Items.Clear();
               
    }
    public void GetEmpTimesheet()//view emptimesheet link
    {
        //getWM();
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.Type = "day";
        ObjTimesheetDO.FromDate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));
        ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));
        //ObjTimesheetDO.ModeofWork = Convert.ToInt32(ddlworkmode.SelectedItem.Value);
        retresultdo = ObjTimesheetBL.ViewEmpTimesheet1(ObjTimesheetDO);
        ViewState["CurrentTable"] = retresultdo.Resultdtset.Tables[0];
        grdEnterTimesheet.DataSource = ViewState["CurrentTable"];
        grdEnterTimesheet.DataBind();
        if (retresultdo.Resultdtset.Tables[0].Rows.Count == 0)    // get inprogress items
        {
            InprogressEmpTimesheet();

        }

        else
        {
            ddlworkmode.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["WorkMode"].ToString();
            int Row = retresultdo.Resultdtset.Tables[0].Rows.Count;
            for (int k = 0; k < Row; k++)
            {
                PopulateDDL(k);

            }
            i = 1;
        }
        //else
        //{
        //    i = 2;
        //}

        btnSave.Enabled = true;

    }

    public void InprogressEmpTimesheet()//Inprogress items in grid
    {

        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.FromDate = Convert.ToDateTime(DateTime.ParseExact(txtDate.Text, "dd-MM-yyyy", null));
        //ObjTimesheetDO.ModeofWork = Convert.ToInt32(ddlworkmode.SelectedItem.Value);
        retresultdo = ObjTimesheetBL.InprogressEmpTimesheet(ObjTimesheetDO);
        ViewState["CurrentTable"] = retresultdo.Resultdtset.Tables[0];        
        grdEnterTimesheet.DataSource = ViewState["CurrentTable"];
        grdEnterTimesheet.DataBind();

        if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
        {
            //ddlworkmode.SelectedValue = retresultdo.Resultdtset.Tables[0].Rows[0]["WorkMode"].ToString();

            int Row = retresultdo.Resultdtset.Tables[0].Rows.Count;
            for (int k = 0; k < Row; k++)
            {
                PopulateDDL(k);

            }
            i = 1;

        }

        if (retresultdo.Resultdtset.Tables[0].Rows.Count == 0)
        {
            retresultdo = ObjTimesheetBL.ViewEmpTimesheet1(ObjTimesheetDO);
            ViewState["CurrentTable"] = retresultdo.Resultdtset.Tables[0];
            grdEnterTimesheet.DataSource = ViewState["CurrentTable"];
            grdEnterTimesheet.DataBind();
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count == 0)
            {
                SetInitialRow();
            }

        }
        btnSave.Enabled = true;
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("Row", typeof(string)));
        dt.Columns.Add(new DataColumn("ProjectId", typeof(string)));
        dt.Columns.Add(new DataColumn("ProjectName", typeof(string)));
        dt.Columns.Add(new DataColumn("ModuleName", typeof(string)));
        dt.Columns.Add(new DataColumn("ModuleId", typeof(string)));
        dt.Columns.Add(new DataColumn("TaskId", typeof(string)));
        dt.Columns.Add(new DataColumn("TaskName", typeof(string)));
        dt.Columns.Add(new DataColumn("TaskDescription", typeof(string)));
        dt.Columns.Add(new DataColumn("Issues", typeof(string)));
        dt.Columns.Add(new DataColumn("Object", typeof(string)));
        dt.Columns.Add(new DataColumn("StatusId", typeof(string)));
        dt.Columns.Add(new DataColumn("Hours", typeof(string)));
        //dt.Columns.Add(new DataColumn("ModeWork", typeof(string)));
        dt.Columns.Add(new DataColumn("id", typeof(string)));

        dr = dt.NewRow();

        dr["Row"] = 1;
        dr["ProjectId"] = string.Empty;
        dr["ProjectName"] = string.Empty;
        dr["ModuleId"] = string.Empty;
        dr["ModuleName"] = string.Empty;
        dr["TaskId"] = string.Empty;
        dr["TaskName"] = string.Empty;
        dr["TaskDescription"] = string.Empty;
        dr["Issues"] = string.Empty;
        dr["Object"] = string.Empty;
        dr["StatusId"] = string.Empty;
        dr["Hours"] = string.Empty;
        //dr["ModeWork"] = string.Empty;
        dr["id"] = string.Empty;

        dt.Rows.Add(dr);
        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        grdEnterTimesheet.DataSource = dt;
        grdEnterTimesheet.DataBind();

    }

    private void AddNewRowToGrid()
    {

        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //DataTable dtCurrentTable = grdEnterTimesheet.DataSource as DataTable;
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[3].FindControl("ddlProject");
                    //ddlProject.Focus();
                    DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[4].FindControl("ddlModuleList");

                    DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[5].FindControl("ddlTask");

                    TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[6].FindControl("txtTaskDesc");
                    TextBox Issues = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[7].FindControl("txtIssues");
                    TextBox Object = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[8].FindControl("txtObject");
                    DropDownList ddlStatus = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("ddlStatus");
                    TextBox Hours = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[10].FindControl("txtHours");
                    //DropDownList ddlmodeofwork = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[11].FindControl("ddlworkmode");
                    Label RowId = (Label)grdEnterTimesheet.Rows[rowIndex].Cells[11].FindControl("lblRowId");

                    drCurrentRow = dtCurrentTable.NewRow();

                    drCurrentRow["Row"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["ProjectId"] = ddlProject.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["ModuleId"] = ddlModuleList.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["TaskId"] = ddlTask.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["TaskDescription"] = Description.Text;
                    dtCurrentTable.Rows[i - 1]["Issues"] = Issues.Text;
                    dtCurrentTable.Rows[i - 1]["Object"] = Object.Text;

                    int j = 0;

                    if (Hours.Text == "")
                    {
                        Hours.Text = j.ToString();
                    }
                    if (RowId.Text == "")
                    {
                        RowId.Text = j.ToString();
                    }
                    dtCurrentTable.Rows[i - 1]["StatusId"] = ddlStatus.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Hours"] = Hours.Text;
                    //dtCurrentTable.Rows[i - 1]["ModeWork"] = ddlmodeofwork.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["id"] = RowId.Text;
                    rowIndex++;

                }
            }
            dtCurrentTable.Rows.Add(drCurrentRow);
            dtCurrentTable.AcceptChanges();
            ViewState["CurrentTable"] = dtCurrentTable;
            grdEnterTimesheet.DataSource = dtCurrentTable;
            grdEnterTimesheet.DataBind();
            grdEnterTimesheet.Rows[rowIndex].Focus();

        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks

        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    string selecteddate = txtDate.Text;
                    ObjTimesheetDO.ModeofWork = Convert.ToInt32(ddlworkmode.SelectedItem.Value);
                    DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[3].FindControl("ddlProject");
                    DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[4].FindControl("ddlModuleList");
                    DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[5].FindControl("ddlTask");
                    TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[6].FindControl("txtTaskDesc");
                    TextBox Issues = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[7].FindControl("txtIssues");
                    TextBox Object = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[8].FindControl("txtObject");
                    DropDownList ddlStatus = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("ddlStatus");
                    TextBox Hours = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[10].FindControl("txtHours");
                    //DropDownList ddlmodeofwork = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[11].FindControl("ddlworkmode");
                    Label RowId = (Label)grdEnterTimesheet.Rows[rowIndex].Cells[11].FindControl("lblRowId");

                    ddlProject.SelectedValue = dt.Rows[i]["ProjectId"].ToString();

                    ObjTimesheetDO.EmpId = EmpId;

                    ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));

                    ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);

                    ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
                    ddlModuleList.DataValueField = "ModuleId";
                    ddlModuleList.DataTextField = "ModuleName";
                    ddlModuleList.DataBind();
                    //  ddlModuleList.Items.Insert(0, "--Select--");

                    ddlModuleList.SelectedValue = dt.Rows[i]["ModuleId"].ToString();

                    //to get task value
                    ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
                    ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
                    ObjTimesheetDO.inputvalue = 2;
                    ObjTimesheetDO.EmpId = EmpId;
                    ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);

                    ddlTask.DataSource = ds_task.Resultdtset;
                    ddlTask.DataValueField = "TaskId";
                    ddlTask.DataTextField = "TaskName";
                    ddlTask.DataBind();
                    ddlTask.SelectedValue = dt.Rows[i]["TaskId"].ToString();

                    Description.Text = dt.Rows[i]["TaskDescription"].ToString();
                    Issues.Text = dt.Rows[i]["Issues"].ToString();
                    Object.Text = dt.Rows[i]["Object"].ToString();
                    ddlStatus.SelectedValue = dt.Rows[i]["StatusId"].ToString();
                    Hours.Text = dt.Rows[i]["Hours"].ToString();
                   // ddlmodeofwork.SelectedValue = dt.Rows[i]["ModeWork"].ToString();
                    RowId.Text = dt.Rows[i]["id"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    private void SetPreviousData1()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string selecteddate = txtDate.Text;

                    //ResultDO ds_ProjectList = ObjTimesheetBL.GetProjectList(); //
                    ObjTimesheetDO.ModeofWork = Convert.ToInt32(ddlworkmode.SelectedItem.Value);
                    DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[3].FindControl("ddlProject");
                    DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[4].FindControl("ddlModuleList");
                    DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[5].FindControl("ddlTask");
                    TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[6].FindControl("txtTaskDesc");
                    TextBox Issues = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[7].FindControl("txtIssues");
                    TextBox Object = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[8].FindControl("txtObject");
                    DropDownList ddlStatus = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("ddlStatus");
                    TextBox Hours = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[10].FindControl("txtHours");
                    //DropDownList ddlmodeofwork = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[11].FindControl("ddlworkmode");
                    Label RowId = (Label)grdEnterTimesheet.Rows[rowIndex].Cells[11].FindControl("lblRowId");

                    ddlProject.SelectedValue = dt.Rows[i]["ProjectId"].ToString();

                    ObjTimesheetDO.EmpId = EmpId;
                    //ddlProject.DataSource = ds_ProjectList.Resultdtset;
                    ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));

                    ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);

                    ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
                    ddlModuleList.DataValueField = "ModuleId";
                    ddlModuleList.DataTextField = "ModuleName";
                    ddlModuleList.DataBind();

                    ddlModuleList.SelectedValue = dt.Rows[i]["ModuleId"].ToString();
                    //to get task value
                    ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
                    ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
                    ObjTimesheetDO.inputvalue = 2;
                    ObjTimesheetDO.EmpId = EmpId;
                    ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);

                    ddlTask.DataSource = ds_task.Resultdtset;
                    ddlTask.DataValueField = "TaskId";
                    ddlTask.DataTextField = "TaskName";
                    ddlTask.DataBind();

                    ddlTask.SelectedValue = dt.Rows[i]["TaskId"].ToString();

                    Description.Text = dt.Rows[i]["TaskDescription"].ToString();
                    Issues.Text = dt.Rows[i]["Issues"].ToString();
                    Object.Text = dt.Rows[i]["Object"].ToString();
                    ddlStatus.SelectedValue = dt.Rows[i]["StatusId"].ToString();
                    Hours.Text = dt.Rows[i]["Hours"].ToString();
                    //ddlmodeofwork.SelectedValue = dt.Rows[i]["ModeWork"].ToString();
                    RowId.Text = dt.Rows[i]["id"].ToString();
                    rowIndex++;
                     
                }
            }
        }
    }

    public void PopulateDDL(int CurrentRow)
    {

        DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[CurrentRow].FindControl("ddlProject");
        Label lblProjectId = (Label)grdEnterTimesheet.Rows[CurrentRow].FindControl("lblProjectId");

        ObjTimesheetDO.EmpId = EmpId;
        ResultDO ds_ProjectList = ObjTimesheetBL.GetProjectList();

        ddlProject.DataSource = ds_ProjectList.Resultdtset;
        ddlProject.DataValueField = "ProjectId";
        ddlProject.DataTextField = "ProjectName";
        ddlProject.Items.Clear();
        ddlProject.DataBind();

        ddlProject.SelectedValue = lblProjectId.Text;

        DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[CurrentRow].FindControl("ddlModuleList");
        Label lblModuleId = (Label)grdEnterTimesheet.Rows[CurrentRow].FindControl("lblModuleId");

        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
        ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);

        ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
        ddlModuleList.DataValueField = "ModuleId";
        ddlModuleList.DataTextField = "ModuleName";
        ddlModuleList.DataBind();

        ddlModuleList.SelectedValue = lblModuleId.Text;

        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[CurrentRow].FindControl("ddlTask");
        Label lblTaskId = (Label)grdEnterTimesheet.Rows[CurrentRow].FindControl("lblTaskId");

        ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
        ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
        ObjTimesheetDO.inputvalue = 2;
        ObjTimesheetDO.EmpId = EmpId;
        ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);

        ddlTask.DataSource = ds_task.Resultdtset;
        ddlTask.DataValueField = "TaskId";
        ddlTask.DataTextField = "TaskName";
        ddlTask.DataBind();

        ddlTask.SelectedValue = lblTaskId.Text;

        Label lblStatus = (Label)grdEnterTimesheet.Rows[CurrentRow].FindControl("lblStatus");
        DropDownList ddlStatus = (DropDownList)grdEnterTimesheet.Rows[CurrentRow].FindControl("ddlStatus");
        ddlStatus.SelectedValue = lblStatus.Text;
        //Label lblworkmode = (Label)grdEnterTimesheet.Rows[CurrentRow].FindControl("lblWorkModeId");
        //DropDownList ddlmodeofwork = (DropDownList).Rows[CurrentRow].FindControl("lblWorkModeId");
        //ddlworkmode.SelectedValue = lblworkmode.Text;
        //ddlworkmode.SelectedValue = lblworkmode.Text;
    }

    protected void BindProject()
    {
        ObjTimesheetDO.EmpId = EmpId;
        ResultDO ds_Project = ObjTimesheetBL.GetProjectList();
        DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[i].FindControl("ddlProject");
        ddlProject.DataSource = ds_Project.Resultdtset;
        ddlProject.DataValueField = "ProjectId";
        ddlProject.DataTextField = "ProjectName";
        ddlProject.DataBind();
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlTask");
        ddlModuleList.Enabled = false;
        ddlTask.Enabled = false;
        ddlModuleList.Items.Clear();
        ddlTask.Items.Clear();
        ddlModuleList.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
        DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlProject");
        int ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : int.Parse(ddlProject.SelectedItem.Value));
        if (ProjectId > 0)
        {
            BindModule(rowindex);
        }
        ddlModuleList.Focus();
    }

    protected void BindModule(int rowindex)
    {
        DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlProject");
        ObjTimesheetDO.EmpId = EmpId;
        ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
        ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
        DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
        ddlModuleList.DataValueField = "ModuleId";
        ddlModuleList.DataTextField = "ModuleName";
        ddlModuleList.Enabled = true;
        ddlModuleList.DataBind();

    }

    protected void BindTask(int rowindex)
    {
        ResultDO retresult = new ResultDO();
        DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlProject");
        ObjTimesheetDO.ProjectId = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
        DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        ObjTimesheetDO.ModuleId = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
        if ((ObjTimesheetDO.ProjectId == 7) && (ObjTimesheetDO.ModuleId == 51))
        {
            ObjTimesheetDO.inputvalue = 2;
        }
        else
            ObjTimesheetDO.inputvalue = 3;
        ObjTimesheetDO.EmpId = EmpId;
        //GetTaskList(ProjectId, ModuleId, EmpId);
        ResultDO ds_task = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);

        //retresultdo = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlTask");
        //ddlTask.DataSource = retresultdo.Resultdtset.Tables[0];
        ddlTask.DataSource = ds_task.Resultdtset;
        ddlTask.DataValueField = "TaskId";
        ddlTask.DataTextField = "TaskName";
        ddlTask.Items.Clear();
        ddlTask.Enabled = true;
        ddlTask.DataBind();
        ddlTask.Items.Insert(0, "--Select--");
        ddlTask.Items.Insert(1, "--add new task--");

    }

    protected void ddlModuleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlModuleList");
        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlTask");
        var a = Convert.ToString(ddlTask);
        ddlTask.Enabled = false;
        ddlTask.Items.Clear();
        ddlTask.Items.Insert(0, new ListItem("--Select--", "0"));
        int ModuleId = int.Parse(ddlModuleList.SelectedItem.Value);
        if (ModuleId > 0)
        {
            BindTask(rowindex);
        }
        ddlTask.Focus();
    }

    protected void ddlTask_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl.NamingContainer;
        //Get rowindex
        int rowindex = gvr.RowIndex;
        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowindex].FindControl("ddlTask");

        if (ddlTask.SelectedValue.ToString() == "--add new task--")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "script", "OpenPopUp(popDiv)", true);
            ClientId(ProjectId);
            ddlclient.Enabled = false;
            btnSave.Visible = false;
            grdEnterTimesheet.Enabled = false;
        }

        TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowindex].Cells[6].FindControl("txtTaskDesc");

        Description.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "script", "HidePopUp(popDiv)", true);
        btnSave.Visible = true;
        grdEnterTimesheet.Enabled = true;
        ddlclient.SelectedIndex = 0;
        txtAddTask.Text = "";
        txtDescription.Text = "";
        ddlPriority.SelectedIndex = 0;
        ddltaskcat.SelectedIndex = 0;
        txtplanneddate.Text = "";
        txtplanndend.Text = "";
        txtplannedeffortdate.Text = "";
        ObjTimesheetDO.EmpId = EmpId;

        int lastrow = grdEnterTimesheet.Rows.Count - 1;
        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[lastrow].FindControl("ddlTask");
        ddlTask.SelectedIndex = 0;
    }

    protected void btnAddTask_Click(object sender, EventArgs e)
    {
        //DateTime mEndDate = Convert.ToDateTime(txtplanndend.Text);
        //DateTime mStartDate = Convert.ToDateTime(txtplanneddate.Text);

        //if (mStartDate > mEndDate)
        //{

        // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('End date should not be less than Start date ')", true);
        // return;
        //}
        ObjTimesheetDO.EmpId = EmpId;

        int rowIndex = 0;

        DataTable dt = (DataTable)ViewState["CurrentTable"];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ResultDO ds_Project = ObjTimesheetBL.GetProjectList();
                DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].FindControl("ddlProject");
                ddlProject.DataSource = ds_Project.Resultdtset;

                ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
                DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].FindControl("ddlModuleList");
                ddlModuleList.DataSource = ds_ModuleList.Resultdtset;

                ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());
                ObjTimesheetDO.ModuleId = Convert.ToInt32(ddlModuleList.SelectedValue.ToString());
                ObjTimesheetDO.Status = Convert.ToInt32(ddlPriority.SelectedItem.Value);
                ObjTimesheetDO.TaskName = txtAddTask.Text;
                ObjTimesheetDO.Taskdesc = txtDescription.Text;
                ObjTimesheetDO.Priority = Convert.ToInt32(ddlPriority.SelectedItem.Value);
                ObjTimesheetDO.Taskcat = Convert.ToInt32(ddltaskcat.SelectedItem.Value);
                ObjTimesheetDO.plannedstartdtask = Convert.ToDateTime(DateTime.ParseExact(txtplanneddate.Text, "dd-MM-yyyy", null));
                ObjTimesheetDO.plannedenddate = Convert.ToDateTime(DateTime.ParseExact(txtplanndend.Text, "dd-MM-yyyy", null));

                rowIndex++;
            }
        }
        ObjTimesheetDO.Clients = Convert.ToInt32(ddlclient.SelectedValue);

        int result = ObjTimesheetBL.AddNewTaskList(ObjTimesheetDO);

        GetTaskList(ProjectId, ModuleId, EmpId);

        int Index = grdEnterTimesheet.Rows.Count - 1;
        DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[Index].FindControl("ddlTask");
        TextBox Description = (TextBox)grdEnterTimesheet.Rows[Index].Cells[6].FindControl("txtTaskDesc");
        retresultdo = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);
        ddlTask.DataSource = retresultdo.Resultdtset.Tables[0];
        ddlTask.DataValueField = "TaskId";
        ddlTask.DataTextField = "TaskName";
        ddlTask.Items.Clear();
        ddlTask.DataBind();
        ddlTask.Items.Insert(0, "--Select--");
        ddlTask.Items.Insert(1, "--add new task--");
        ObjTimesheetDO.TaskId = Convert.ToInt32(ddlTask.Items[ddlTask.Items.Count - 1].Value);
        ddlTask.SelectedIndex = ddlTask.Items.Count - 1;

        int Id = 0;
        ObjTimesheetDO.EmpId = EmpId;
        Id = EmpId;
        ObjTimesheetDO.RowNum = Id;

        ObjTimesheetDO.Actualstartdate = Convert.ToDateTime("01-01-1900");
        ObjTimesheetDO.Actualeffort = Convert.ToSingle(0.0);

        ObjTimesheetDO.Priority = Convert.ToInt32(ddlPriority.SelectedItem.Value);
        ObjTimesheetDO.Taskcat = Convert.ToInt32(ddltaskcat.SelectedValue);
        ObjTimesheetDO.Plannedeffort = Convert.ToSingle(txtplannedeffortdate.Text);
        ObjTimesheetDO.Plannedstartdate = Convert.ToDateTime(DateTime.ParseExact(txtplanneddate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture));
        ObjTimesheetDO.ToDate = Convert.ToDateTime(DateTime.ParseExact(txtplanndend.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture));

        int result1 = ObjTimesheetBL.SaveEmployeeTask(ObjTimesheetDO);

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully!')", true);

        grdEnterTimesheet.Enabled = true;

        ddlclient.SelectedIndex = 0;
        txtAddTask.Text = "";
        txtDescription.Text = "";
        ddlPriority.SelectedIndex = 0;
        ddltaskcat.SelectedIndex = 0;
        txtplanneddate.Text = "";
        txtplanndend.Text = "";
        txtplannedeffortdate.Text = "";
        btnSave.Visible = true;
        Description.Focus();

    }

    public void ClientId(int ProjectID)
    {
        retresultdo = new ResultDO();
        //int lastrow = grdEnterTimesheet.Rows.Count - 1;
        int rowIndex = 0;

        DataTable dt = (DataTable)ViewState["CurrentTable"];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].FindControl("ddlProject");

                ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue);
                rowIndex++;
            }
        }
        retresultdo = ObjTimesheetBL.ClientId(ObjTimesheetDO);
        ddlclient.DataSource = retresultdo.Resultdtset.Tables[0];
        ddlclient.DataValueField = "Clientid";
        ddlclient.DataTextField = "ClientShortCode";
        ddlclient.DataBind();

    }

    public void GetTaskList(int ProjectId, int ModuleId, int EmpId)
    {
        int rowIndex = 0;
        DataTable dt = (DataTable)ViewState["CurrentTable"];

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                retresultdo = new ResultDO();
                ObjTimesheetDO.EmpId = EmpId;
                ResultDO ds_Project = ObjTimesheetBL.GetProjectList();
                DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].FindControl("ddlProject");
                ddlProject.DataSource = ds_Project.Resultdtset;
                ObjTimesheetDO.ProjectId = Convert.ToInt32(ddlProject.SelectedValue.ToString());

                ResultDO ds_ModuleList = ObjTimesheetBL.GetModuleList(ObjTimesheetDO);
                DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].FindControl("ddlModuleList");
                ddlModuleList.DataSource = ds_ModuleList.Resultdtset;
                TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[6].FindControl("txtTaskDesc");

                ObjTimesheetDO.ModuleId = Convert.ToInt32(ddlModuleList.SelectedValue.ToString());

                ObjTimesheetDO.TaskName = txtAddTask.Text;

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
                DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowIndex].FindControl("ddlTask");

                rowIndex++;
                retresultdo = ObjTimesheetBL.GetTaskList(ObjTimesheetDO);

                ddlTask.DataSource = retresultdo.Resultdtset.Tables[0];
                ddlTask.DataValueField = "TaskId";
                ddlTask.DataTextField = "TaskName";
                ddlTask.DataBind();

            }
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

    public void GetTaskcat()
    {
        try
        {
            //ObjTimesheetDO.ProjectId = ProjectId;
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
    public void Getholidaylist() {
       
    }

    private void ResetRowID(DataTable dt)
    {
        int rowNumber = 1;

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }

    }

    private void getrow()
    {

        int rowIndex = 0;
        try
        {
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            }
            else
            {
                Response.Write("ViewState is null");
            }
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (grdEnterTimesheet.Rows.Count > 0)
            {
                for (int i = 0; i < grdEnterTimesheet.Rows.Count; i++)
                {
                    DropDownList ddlProject = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[1].FindControl("ddlProject");
                    DropDownList ddlModuleList = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[2].FindControl("ddlModuleList");
                    DropDownList ddlTask = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[3].FindControl("ddlTask");
                    TextBox Description = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[4].FindControl("txtTaskDesc");
                    TextBox Issues = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[5].FindControl("txtIssues");
                    TextBox Object = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[6].FindControl("txtObject");
                    DropDownList ddlStatus = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[7].FindControl("ddlStatus");
                    TextBox Hours = (TextBox)grdEnterTimesheet.Rows[rowIndex].Cells[8].FindControl("txtHours");
                   // DropDownList ddlmodeofwork = (DropDownList)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("ddlworkmode");
                    Label RowId = (Label)grdEnterTimesheet.Rows[rowIndex].Cells[9].FindControl("lblRowId");

                    if (RowId.Text == "")
                    {
                        int j = 0;
                        RowId.Text = j.ToString();
                    }
                    dt.Rows[i]["ProjectId"] = ((ddlProject.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlProject.SelectedValue.ToString()));
                    dt.Rows[i]["ModuleId"] = ((ddlModuleList.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlModuleList.SelectedValue.ToString()));
                    dt.Rows[i]["TaskId"] = ((ddlTask.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlTask.SelectedValue.ToString()));
                    dt.Rows[i]["TaskDescription"] = Description.Text.ToString();
                    dt.Rows[i]["Issues"] = Issues.Text.ToString();
                    dt.Rows[i]["Object"] = Object.Text.ToString();
                    dt.Rows[i]["StatusId"] = ((ddlStatus.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlStatus.SelectedValue.ToString()));
                    dt.Rows[i]["Hours"] = ((Hours.Text == "") ? 0 : Convert.ToDecimal(Hours.Text.ToString()));
                    //dt.Rows[i]["ModeWork"] = ((ddlmodeofwork.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlmodeofwork.SelectedValue.ToString())); 

                    dt.Rows[i]["id"] = RowId.Text.ToString();
                    rowIndex++;
                }
            }
            dt.AcceptChanges();
            ViewState["CurrentTable"] = dt;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    protected void grdEnterTimesheet_SelectedIndexChanged(object sender, EventArgs e)  
    {

    }
    
}
