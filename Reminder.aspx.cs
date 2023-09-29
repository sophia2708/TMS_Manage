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


public partial class Includes_WebForm_Reminder : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();
    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();
    ResultDO retresultdo = new ResultDO();
    SqlConnection mycn;
    SqlDataAdapter myda;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();

    String strConn;
    DateTime today = DateTime.Today;
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    public int i = 0;
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0, flag = 0;

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
            Session["EmpId"] = EmpId;
        }
        //this.txtDate.Text = Request[this.txtDate.UniqueID];
        if (!this.IsPostBack)
        {
            //   txtDate.va = DateTime.Now.ToString("dd-MM-yyyy");
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["CurrentTable"];
            if (dt == null) SetInitialRow();
            else if (dt.Rows.Count == 0)
            {
                SetInitialRow();
            }
            //strConn = ConfigurationManager.ConnectionStrings["ABConnectionString"].ToString();
            //mycn = new SqlConnection(strConn);
            //grdreminderlist.DataSource = showDetails();
            ////ViewState["CurrentTable"] = grdreminderlist.DataSource;
            //grdreminderlist.DataBind();
            //cmd.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand = cmd;
            showDetails();

        }
    }

    protected void btnReminderSave_Click(object sender, EventArgs e)
    {
        try
        {

            Remindersave();
            //strConn = ConfigurationManager.ConnectionStrings["ABConnectionString"].ToString();
            //mycn = new SqlConnection(strConn);
            //grdreminderlist.DataSource = showDetails();
            //grdreminderlist.DataBind();
            //cmd.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand = cmd;
        }
        catch (Exception ex)
        {
            throw (ex);
        }


    }


    //private void getrow()
    //{

    //    int rowIndex = 0;
    //    try
    //    {
    //        if (ViewState["CurrentTable"] != null)
    //        {
    //            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        }
    //        else
    //        {
    //            Response.Write("ViewState is null");
    //        }
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];
    //        if (grdreminderlist.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < grdreminderlist.Rows.Count; i++)
    //            {
    //                //TextBox Id = (TextBox)grdreminderlist.Rows[rowIndex].Cells[1].FindControl("lblId");
    //                TextBox TaskName = (TextBox)grdreminderlist.Rows[rowIndex].Cells[1].FindControl("txtTaskName");
    //                //TextBox TaskFrequency = (TextBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("txtTaskFrequency");
    //                //DropDownList ddlModuleList = (DropDownList)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlTaskFrequency");
    //                //TextBox Dateofreminder = (TextBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("txtDateofreminder");
    //                ListBox Peopletobereminded = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[4].FindControl("employeeslist"));
    //                // DateTime Dateofreminder = Convert.ToDateTime(DateTime.ParseExact(txtDateofreminder.Text, "MM/dd/yyyy", null));
    //                Label RowId = (Label)grdreminderlist.Rows[rowIndex].Cells[5].FindControl("lblRowId");


    //                if (RowId.Text == "")
    //                {
    //                    int j = 0;
    //                    RowId.Text = j.ToString();
    //                }

    //                dt.Rows[i]["TaskName"] = TaskName.Text.ToString();
    //                //dt.Rows[i]["TaskFrequency"] = TaskFrequency.Text.ToString();
    //                //if (Dateofreminder.Text.Length > 10)
    //                //{
    //                //    Dateofreminder.Text = Dateofreminder.Text.Remove(10, Dateofreminder.Text.Length-1);
    //                //}
    //                //dt.Rows[i]["Dateofreminder"] = Dateofreminder.Text.ToString();
    //                string SelEmpid = dt.Rows[i]["Peopletobereminded"].ToString();
    //                if (SelEmpid == "")
    //                {
    //                    foreach (ListItem listItem in Peopletobereminded.Items)
    //                    {
    //                        if (listItem.Selected)
    //                        {
    //                            var val = listItem.Value;
    //                            var txt = listItem.Text;
    //                            SelEmpid += val + ",";
    //                        }
    //                    }
    //                    if (SelEmpid != "")
    //                        SelEmpid = SelEmpid.Remove(SelEmpid.Length - 1, 1);
    //                    else
    //                        SelEmpid = "0";
    //                }

    //                dt.Rows[i]["Peopletobereminded"] = SelEmpid;

    //                //string msg = "";
    //                //Array splitedValues;
    //                //foreach (ListItem li in Peopletobereminded.Items)
    //                //{
    //                //    if (li.Selected == true)
    //                //    {
    //                //        msg += li.Text + ',';
    //                //    }
    //                //}
    //                //splitedValues = msg.Split(',');
    //                //Peopletobereminded.Text = msg;



    //                //List<ListBox> items = new List<ListItem>();
    //                //Peopletobereminded.DataSource = items;
    //                //Peopletobereminded.DataValueField = "Value";
    //                //Peopletobereminded.DataTextField = "Text";
    //                //Peopletobereminded.DataBind();

    //                //dt.Rows[i]["Peopletobereminded"] = ((Peopletobereminded.SelectedIndex == 0) ? 0 : Convert.ToInt32(Peopletobereminded.SelectedValue.ToString()));


    //                dt.Rows[i]["Id"] = RowId.Text.ToString();
    //                rowIndex++;
    //            }
    //        }
    //        dt.AcceptChanges();
    //        ViewState["CurrentTable"] = dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw (ex);
    //    }
    //}


    protected void Remindersave()
    {
        try
        {

            foreach (GridViewRow dr in grdreminderlist.Rows)
            {
                ObjTimesheetDO.EmpId = EmpId;
                ResultDO ds_Emplist = ObjTimesheetBL.GetEmployeeList(ObjTimesheetDO);
                string SelEmpid = "", SelWeekly = "", SelMonthly = "", SelYearly = "", SelYearly1 = "";
                TextBox TaskName = (TextBox)dr.FindControl("txtTaskName");
                DropDownList TaskFrequency = (DropDownList)dr.FindControl("ddlTaskFrequency");

                //ListBox Dateofreminder = ((ListBox)dr.FindControl("employeeslist"));
                //TextBox TaskFrequency = (TextBox)dr.FindControl("txtTaskFrequency");
                //TextBox Dateofreminder = (TextBox)dr.FindControl("txtDateofreminder");
                ListBox employeeslist = ((ListBox)dr.FindControl("employeeslist"));
                HiddenField hfdemp = new HiddenField();
                hfdemp = ((HiddenField)dr.FindControl("hfdid"));
                //Label RowId = (Label)dr.FindControl("lblRowId");
                foreach (ListItem listItem in employeeslist.Items)
                {
                    if (listItem.Selected)
                    {
                        var val = listItem.Value;
                        var txt = listItem.Text;
                        SelEmpid += val + ",";
                    }
                }
                if (SelEmpid == "") SelEmpid = "0";
                if (hfdemp.Value == "") hfdemp.Value = "0";
                //ObjTimesheetDO.EmpId = Convert.ToInt32(ViewState["EmpId"]);
                ObjTimesheetDO.TaskNameReminder = Convert.ToString(TaskName.Text);
                ObjTimesheetDO.id = Convert.ToInt32(hfdemp.Value);
                //ObjTimesheetDO.TaskFrequency = Convert.ToString(TaskFrequency.Text);
                //if (Dateofreminder.Text.Length > 10)
                //{
                //    Dateofreminder.Text = Dateofreminder.Text.Remove(10, Dateofreminder.Text.Length-1);
                //}
                //ObjTimesheetDO.Dateofreminder = Dateofreminder.Text;
                ObjTimesheetDO.TaskFrequency = TaskFrequency.SelectedItem.Value.ToString();
                if (TaskFrequency.SelectedItem.Value == "2")
                {

                    ListBox ddlweekly = ((ListBox)dr.FindControl("ddlweekly"));
                    foreach (ListItem listItem in ddlweekly.Items)
                    {
                        if (listItem.Selected)
                        {
                            var val = listItem.Value;
                            //var txt = listItem.Text;
                            SelWeekly += val + ",";
                        }
                    }
                }
                else if (TaskFrequency.SelectedItem.Value == "3")
                {
                    DropDownList ddlbiweekly = ((DropDownList)dr.FindControl("ddlbiweekly"));
                    ObjTimesheetDO.Biweekly = ddlbiweekly.SelectedValue;

                }
                else if (TaskFrequency.SelectedItem.Value == "4")
                {

                    ListBox ddlMonthly = ((ListBox)dr.FindControl("ddlMonthly"));
                    foreach (ListItem listItem in ddlMonthly.Items)
                    {
                        if (listItem.Selected)
                        {
                            var val = listItem.Value;
                            //var txt = listItem.Text;
                            SelMonthly += val + ",";
                        }
                    }
                }
                else if (TaskFrequency.SelectedItem.Value == "5")
                {
                    DropDownList ddlbimonthly = ((DropDownList)dr.FindControl("ddlbimonthlywise"));
                    ObjTimesheetDO.BiMonthly = ddlbimonthly.SelectedValue;

                }
                else if (TaskFrequency.SelectedItem.Value == "6")
                {

                    ListBox ddlyearlywise = ((ListBox)dr.FindControl("ddlyearlywise"));
                    foreach (ListItem listItem in ddlyearlywise.Items)
                    {
                        if (listItem.Selected)
                        {
                            var val = listItem.Value;
                            //var txt = listItem.Text;
                            SelYearly += val + ",";
                        }
                    }

                    ListBox ddlmonth = ((ListBox)dr.FindControl("ddlmonth"));
                    foreach (ListItem listItem in ddlmonth.Items)
                    {
                        if (listItem.Selected)
                        {
                            var val = listItem.Value;
                            //var txt = listItem.Text;
                            SelYearly1 += val + ",";
                        }
                    }
                }
                ObjTimesheetDO.Weekly = SelWeekly;
                ObjTimesheetDO.Monthly = SelMonthly;
                ObjTimesheetDO.Yearly = SelYearly;
                ObjTimesheetDO.Yearly1 = SelYearly1;
                ObjTimesheetDO.PeopleToBeReminded = SelEmpid;
                ObjTimesheetDO.EmpId = EmpId;
                ObjTimesheetBL.ReminderSaveDetail(ObjTimesheetDO);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Saved successfully!');fnShowRmainder()</script>");

            }

        }

        catch (Exception ex)
        {
            throw ex;
        }


    }


    protected void ButtonAddReminder_Click(object sender, EventArgs e)
    {
        AddNewRowToReminderGrid();
    }
    private void AddNewRowToReminderGrid()
    {
        int rowIndex = 0;
        flag = 1;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //DataTable dtCurrentTable = grdreminderlist.DataSource as DataTable;
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //Label Id = ((Label)grdreminderlist.Rows[rowIndex].Cells[0].FindControl("lblid"));
                    TextBox TaskName = (TextBox)grdreminderlist.Rows[rowIndex].Cells[0].FindControl("txtTaskName");
                    DropDownList TaskFrequency = (DropDownList)grdreminderlist.Rows[rowIndex].Cells[1].FindControl("ddlTaskFrequency");
                    string SelWeekly = "";// dtCurrentTable.Rows[i - 1]["TaskFrequency"].ToString();
                    if (TaskFrequency.SelectedItem.Value == "2")
                    {

                        ListBox ddlweekly = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlweekly"));
                        foreach (ListItem listItem in ddlweekly.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                //var txt = listItem.Text;
                                SelWeekly += val + ",";




                            }
                        }
                        if (SelWeekly != "")
                            SelWeekly = SelWeekly.Remove(SelWeekly.Length - 1, 1);
                        dtCurrentTable.Rows[i - 1]["DateofReminder"] = SelWeekly;
                    }

                    if (TaskFrequency.SelectedItem.Value == "3")
                    {
                        DropDownList ddlbiweekly = ((DropDownList)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlbiweekly"));
                        //ObjTimesheetDO.Biweekly = ddlbiweekly.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["DateofReminder"] = ddlbiweekly.SelectedValue;
                       
                        //dtCurrentTable.Rows[i - 1]["DateofReminder"] = ddlbiweekly.SelectedValue;
                        
                    }
                    // dtCurrentTable.Rows[i - 1]["DateofReminder"] = ddlbiweekly.SelectedValue; 
                    string SelMonthly = "";
                    if (TaskFrequency.SelectedItem.Value == "4")
                    {

                        ListBox ddlMonthly = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlMonthly"));
                        foreach (ListItem listItem in ddlMonthly.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                //var txt = listItem.Text;
                                SelMonthly += val + ",";
                            }
                        }
                        if (SelMonthly != "")
                            SelMonthly = SelMonthly.Remove(SelMonthly.Length - 1, 1);
                        dtCurrentTable.Rows[i - 1]["DateofReminder"] = SelMonthly;
                    }
                    if (TaskFrequency.SelectedItem.Value == "5")
                    {
                        DropDownList ddlbimonthly = ((DropDownList)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlbimonthlywise"));
                        //ObjTimesheetDO.BiMonthly = ddlbimonthly.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["DateofReminder"] = ddlbimonthly.SelectedValue;

                    }
                    string SelYearly = "";
                    string SelYearly1 = "";
                    if (TaskFrequency.SelectedItem.Value == "6")
                    {
                        ListBox ddlyearlywise = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlyearlywise"));
                        foreach (ListItem listItem in ddlyearlywise.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                SelYearly += val + ",";
                            }
                        }
                        if (SelYearly != "")
                            SelYearly = SelYearly.Remove(SelYearly.Length - 1, 1);
                        dtCurrentTable.Rows[i - 1]["DateofReminder"] = SelYearly;
                        ListBox ddlmonth = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlmonth"));
                        foreach (ListItem listItem in ddlmonth.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                //var txt = listItem.Text;
                                SelYearly1 += val + ",";
                            }
                        }
                        if (SelYearly1 != "")
                            SelYearly1 = SelYearly1.Remove(SelYearly1.Length - 1, 1);
                        dtCurrentTable.Rows[i - 1]["DateofReminder"] = SelYearly;
                        dtCurrentTable.Rows[i - 1]["DateofReminder_"] = SelYearly1;
                    }

                    //TextBox DateofReminder = (TextBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("txtDateofreminder");
                    ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("employeeslist");
                    string SelEmpid ="" ;//dtCurrentTable.Rows[i - 1]["Peopletobereminded"].ToString();
                    if (SelEmpid == "")
                    {
                        foreach (ListItem listItem in Peopletobereminded.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                var txt = listItem.Text;
                                SelEmpid += val + ",";
                            }
                        }
                        if (SelEmpid != "")
                            SelEmpid = SelEmpid.Remove(SelEmpid.Length - 1, 1);
                        else
                            SelEmpid = "0";
                    }


                    //Label lblRowId = (Label)grdreminderlist.Rows[rowIndex].Cells[5].FindControl("lblRowId");
                    drCurrentRow = dtCurrentTable.NewRow();
                    //drCurrentRow["Row"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["TaskName"] = TaskName.Text;
                    dtCurrentTable.Rows[i - 1]["TaskFrequency"] = TaskFrequency.SelectedValue;


                    //dtCurrentTable.Rows[i - 1]["DateofReminder"] = DateofReminder.Text;
                    dtCurrentTable.Rows[i - 1]["Peopletobereminded"] = SelEmpid;
                    //dtCurrentTable.Rows[i - 1]["TaskFrequency"] = 2;
                    //dtCurrentTable.Rows[i - 1]["DateofReminder"] = 3;
                    //dtCurrentTable.Rows[i - 1]["Id"] = 3;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                dtCurrentTable.AcceptChanges();
                ViewState["CurrentTable"] = dtCurrentTable;
                grdreminderlist.DataSource = dtCurrentTable;
                grdreminderlist.DataBind();
                grdreminderlist.Rows[rowIndex].Focus();

            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks

          //  SetPreviousData();
        }
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
                    //string Dateofreminder = txtDate.Text;
                    TextBox TaskName = (TextBox)grdreminderlist.Rows[rowIndex].Cells[0].FindControl("txtTaskName");
                    DropDownList TaskFrequency = (DropDownList)grdreminderlist.Rows[rowIndex].Cells[1].FindControl("ddlTaskFrequency");
                    //TextBox TaskFrequency = (TextBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("txtTaskFrequency");
                    //TextBox DateofReminder = (TextBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("txtDateofreminder");
                    string SelWeekly = "";// dtCurrentTable.Rows[i - 1]["TaskFrequency"].ToString();
                    if (TaskFrequency.SelectedItem.Value == "2")
                    {

                        ListBox ddlweekly = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlweekly"));
                        foreach (ListItem listItem in ddlweekly.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value.ToString();
                                //var txt = listItem.Text;
                                SelWeekly += val + ",";
                            }

                        }
                        dt.Rows[i]["DateofReminder"] = SelWeekly;
                    }

                    if (TaskFrequency.SelectedItem.Value == "3")
                    {
                        DropDownList ddlbiweekly = ((DropDownList)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlbiweekly"));
                        ObjTimesheetDO.Biweekly = ddlbiweekly.SelectedValue;

                    }
                    // dtCurrentTable.Rows[i - 1]["DateofReminder"] = ddlbiweekly.SelectedValue; 
                    string SelMonthly = "";
                    if (TaskFrequency.SelectedItem.Value == "4")
                    {

                        ListBox ddlMonthly = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlMonthly"));
                        foreach (ListItem listItem in ddlMonthly.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                //var txt = listItem.Text;
                                SelMonthly += val + ",";
                            }
                        }
                        dt.Rows[i]["DateofReminder"] = SelMonthly;
                    }
                    if (TaskFrequency.SelectedItem.Value == "5")
                    {
                        DropDownList ddlbimonthly = ((DropDownList)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlbimonthlywise"));
                        ObjTimesheetDO.BiMonthly = ddlbimonthly.SelectedValue;

                    }
                    string SelYearly = "";
                    string SelYearly1 = "";
                    if (TaskFrequency.SelectedItem.Value == "6")
                    {

                        ListBox ddlyearlywise = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlyearlywise"));
                        foreach (ListItem listItem in ddlyearlywise.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                //var txt = listItem.Text;
                                SelYearly += val + ",";
                            }
                        }
                        dt.Rows[i]["DateofReminder"] = SelYearly;
                        ListBox ddlmonth = ((ListBox)grdreminderlist.Rows[rowIndex].Cells[2].FindControl("ddlmonth"));
                        foreach (ListItem listItem in ddlmonth.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                //var txt = listItem.Text;
                                SelYearly1 += val + ",";
                            }
                        }
                        dt.Rows[i]["DateofReminder"] = SelYearly;
                        dt.Rows[i]["DateofReminder_"] = SelYearly1;
                    }

                    //TextBox DateofReminder = (TextBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("txtDateofreminder");
                    ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("employeeslist");
                    //Label lblRowId = (Label)grdreminderlist.Rows[rowIndex].Cells[5].FindControl("lblRowId");

                    //drCurrentRow["Row"] = i + 1;
                    dt.Rows[i]["TaskName"] = TaskName.Text;
                    dt.Rows[i]["TaskFrequency"] = TaskFrequency.SelectedValue;


                    //DateTime DateofReminder = (TextBox)grdreminderlist.Rows[rowIndex].Cells[3].FindControl("txtTaskName");
                    //  Label RowId = (Label)grdreminderlist.Rows[rowIndex].Cells[6].FindControl("lblRowId");
                    TaskName.Text = dt.Rows[i]["TaskName"].ToString();
                    //TaskFrequency.Text = dt.Rows[i]["TaskFrequency"].ToString();
                    //string date = dt.Rows[i]["DateofReminder"].ToString();
                    //if (date.Length > 10)
                    //{
                    //    date = date.Remove(10, date.Length-10);
                    //}
                    //DateofReminder.Text = date;
                    Peopletobereminded.Text = dt.Rows[i]["Peopletobereminded"].ToString();
                    string SelEmpid = "";// dt.Rows[i]["Peopletobereminded"].ToString();
                    if (SelEmpid == "")
                    {
                        foreach (ListItem listItem in Peopletobereminded.Items)
                        {
                            if (listItem.Selected)
                            {
                                var val = listItem.Value;
                                var txt = listItem.Text;
                                SelEmpid += val + ",";
                            }
                        }
                        if (SelEmpid != "")
                            SelEmpid = SelEmpid.Remove(SelEmpid.Length - 1, 1);
                        else
                            SelEmpid = "0";
                    }

                    dt.Rows[i]["Peopletobereminded"] = SelEmpid;

                    //RowId.Text = dt.Rows[i]["id"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    //protected void grdreminderlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{


    //    int id = Convert.ToInt32(grdreminderlist.DataKeys[e.RowIndex].Value);
    //    string query = "delete from AB_Reminder where Id= '" + id + "'";


        //getrow();
        //ObjTimesheetDO.EmpId = EmpId;

        //DataTable dt = (DataTable)ViewState["CurrentTable"];
        //int index = Convert.ToInt32(e.RowIndex);
        //GridViewRow row = grdreminderlist.Rows[index];
        //Label lblRowId = row.FindControl("lblRowId") as Label;
        //if (lblRowId.Text != "" && lblRowId.Text != "0")
        //{
        //    int RowId = Convert.ToInt32(lblRowId.Text);
        //    //ObjTimesheetDO.id = RowId;
        //    ObjTimesheetBL.DeleteRemindersheet(ObjTimesheetDO);
        //    //to reset the rownum after deleteing a row
        //    ResetRowID(dt);
        //}
        //if (dt.Rows.Count > 0)
        //{
        //    DataRow dr = dt.Rows[index];
        //    dr.Delete();
        //}

        //dt.AcceptChanges();
        ////to reset the rownum after deleteing a row
        //ResetRowID(dt);
        ////getrow();
        //ViewState["CurrentTable"] = dt;
        //grdreminderlist.DataSource = dt;
        //grdreminderlist.DataBind();
        ////SetPreviousData1();
        //DataTable dtt = new DataTable();
        //dtt = (DataTable)ViewState["CurrentTable"];
        //if (dtt.Rows.Count == 0)
        //{
        //    SetInitialRow();

        //}
        ////int Id = Convert.ToInt32(grdreminderlist.DataKeys[e.RowIndex].Value);
        //// ObjTimesheetBL.DeleteUser(Id);
        //// grdreminderlist.DataSource = ObjTimesheetBL.SelectUser();
        //// // Delete here the database record for the selected id//
        //// grdreminderlist.DataBind();

    //}
    //protected void grdreminderlist_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    grdreminderlist.EditIndex = e.NewEditIndex;
    //    grdreminderlist.DataBind();
    //}
    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("TaskName", typeof(string)));
        dt.Columns.Add(new DataColumn("TaskFrequency", typeof(string)));
        dt.Columns.Add(new DataColumn("DateofReminder", typeof(string)));
        dt.Columns.Add(new DataColumn("DateofReminder_", typeof(string)));
        dt.Columns.Add(new DataColumn("Peopletobereminded", typeof(string)));
        dt.Columns.Add(new DataColumn("Id", typeof(string)));


        dr = dt.NewRow();
        dt.Rows.Add(dr);


        dt.Rows[0]["TaskName"] = string.Empty;
        dt.Rows[0]["TaskFrequency"] = string.Empty;
        dt.Rows[0]["DateofReminder"] = string.Empty;
        dt.Rows[0]["DateofReminder_"] = string.Empty;
        dt.Rows[0]["Peopletobereminded"] = string.Empty;
        dt.Rows[0]["Id"] = string.Empty;

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;
        grdreminderlist.DataSource = dt;
        grdreminderlist.DataBind();


    }


    //public void GetEmployeesName()
    //{
    //    try
    //    {

    //        DataSet ds = new DataSet();
    //        ObjTimesheetDO.EmpId = EmpId;
    //        ResultDO ds_Emplist = ObjTimesheetBL.GetEmployeeList(ObjTimesheetDO);
    //        //emplist.DataSource = ds_Emplist.Resultdtset;
    //        //Employeeslist.DataTextField = "FirstName";
    //        //Employeeslist.DataValueField = "EmpId";
    //        //Employeeslist.DataBind();
    //        //Employeeslist.Items.Insert(0, "  --None selected---  ");

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    private DataSet GetEmployeesName(int EmpID)       //ADDED BY SOPHIA
    {

        DataSet ds = new DataSet();
        string str = "[AB_GetEmployeeList] " + EmpID + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
    public void showDetails()
    {
        retresultdo = ObjTimesheetBL.getRemainderList();//AB_GetRemainderList
        ViewState["CurrentTable"] = retresultdo.Resultdtset.Tables[0];
        grdreminderlist.DataSource = ViewState["CurrentTable"];
        grdreminderlist.DataBind();
        flag = 0;
        if (retresultdo.Resultdtset.Tables[0].Rows.Count == 0)    // get inprogress items
        {
            SetInitialRow();
        }
        //SqlCommand cmd1 = new SqlCommand();
        //SqlDataAdapter da1 = new SqlDataAdapter();
        //DataTable dt1 = new DataTable();
        //cmd1 = new SqlCommand("select TaskName,TaskFrequency ,DateofReminder,Peopletobereminded  from AB_Reminder", mycn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //da1.SelectCommand = cmd1;
        //da1.Fill(ds1);
        //ViewState["CurrentTable"]=dt1
        //return ds1;
    }

    private DataSet GetEmployeeRptmgr(int Id)
    {

        DataSet ds = new DataSet();
        string str = "select Peopletobereminded from AB_Reminder  where Id=" + Id + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
    private DataSet GetWeekly(int Id)
    {

        DataSet ds = new DataSet();
        string str = "select DateofReminder from AB_Reminder  where Id=" + Id + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
    private DataSet GetBiWeekly(int Id)
    {

        DataSet ds = new DataSet();
        string str = "select DateofReminder from AB_Reminder  where Id=" + Id + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
    private DataSet GetMonthly(int Id)
    {

        DataSet ds = new DataSet();
        string str = "select DateofReminder from AB_Reminder  where Id=" + Id + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
    private DataSet GetBiMonthly(int Id)
    {

        DataSet ds = new DataSet();
        string str = "select DateofReminder from AB_Reminder  where Id=" + Id + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
    }
    private DataSet GetYearly(int Id)
    {

        DataSet ds = new DataSet();
        string str = "select YearDays from AB_Reminder  where Id=" + Id + "";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ABConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(str, cn);
        da.Fill(ds);
        return ds;
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
    protected void grdreminderlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hdfId = (HiddenField)e.Row.FindControl("hfdid");
            Control lstEmployees = e.Row.FindControl("employeeslist");
            Control ddbiweek = e.Row.FindControl("ddlbiweekly");
            Control ddbimonth = e.Row.FindControl("ddlbimonthlywise");//ADDED BY SOPHIA
            ListBox dd3 = lstEmployees as ListBox;
            DropDownList dd2 = ddbiweek as DropDownList;
            DropDownList dd1 = ddbimonth as DropDownList;
            DataSet ds = new DataSet();
            ObjTimesheetDO.EmpId = EmpId;
            DataSet GetEmployees = GetEmployeesName(Convert.ToInt32(EmpId));
            dd3.DataSource = GetEmployees;       //ADDED BY SOPHIA
            dd3.DataTextField = "FirstName";
            dd3.DataValueField = "Empid";
            dd3.DataBind();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            string s = "";
            DropDownList ddlTaskFrequency = (DropDownList)e.Row.FindControl("ddlTaskFrequency") as DropDownList;
            ddlTaskFrequency.SelectedValue = dtCurrentTable.Rows[e.Row.RowIndex]["TaskFrequency"].ToString();
            if (ddlTaskFrequency.SelectedItem.Value == "2")
            {
                if (hdfId.Value == "") hdfId.Value = "0";
                DataSet GetRptmgr = GetWeekly(Convert.ToInt32(hdfId.Value));

                if (GetRptmgr.Tables[0].Rows.Count > 0)
                {
                    s = GetRptmgr.Tables[0].Rows[0][0].ToString();
                    index++;
                }
                else if (dtCurrentTable.Rows.Count != 0)
                {
                    // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                    s = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                }
                if (s != "")
                {
                    int[] nums = Array.ConvertAll(s.Split(','), int.Parse);
                    ListBox ddlweekly = ((ListBox)e.Row.FindControl("ddlweekly")); //ADDED BY SOF
                    foreach (ListItem listItem in ddlweekly.Items)
                    {
                        if (Array.IndexOf(nums, Int16.Parse(listItem.Value)) != -1)
                        {
                            listItem.Selected = true;
                        }
                    }
                }
            }
            if (ddlTaskFrequency.SelectedItem.Value == "3")
            {
               
                if (dtCurrentTable.Rows.Count != 0)
                {
                    // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                    dd2.SelectedValue = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                }


            }
            if (ddlTaskFrequency.SelectedItem.Value == "4")
            {
                if (hdfId.Value == "") hdfId.Value = "0";
                DataSet GetRptmgr = GetMonthly(Convert.ToInt32(hdfId.Value));

                if (GetRptmgr.Tables[0].Rows.Count > 0)
                {
                    s = GetRptmgr.Tables[0].Rows[0][0].ToString();
                    index++;
                }
                else if (dtCurrentTable.Rows.Count != 0)
                {
                    // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                    s = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                }
                if (s != "")
                {
                    int[] nums = Array.ConvertAll(s.Split(','), int.Parse);
                    ListBox ddlMonthly = ((ListBox)e.Row.FindControl("ddlMonthly")); //ADDED BY SOF
                    foreach (ListItem listItem in ddlMonthly.Items)
                    {
                        if (Array.IndexOf(nums, Int16.Parse(listItem.Value)) != -1)
                        {
                            listItem.Selected = true;
                        }
                    }
                }
            }
            if (ddlTaskFrequency.SelectedItem.Value == "5")
            {

                if (dtCurrentTable.Rows.Count != 0)
                {
                    // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                    dd1.SelectedValue = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                }


            }
            if (ddlTaskFrequency.SelectedItem.Value == "6")
            {
                if (hdfId.Value == "") hdfId.Value = "0";
                DataSet GetRptmgr = GetYearly(Convert.ToInt32(hdfId.Value));

                if (GetRptmgr.Tables[0].Rows.Count > 0)
                {
                    s = GetRptmgr.Tables[0].Rows[0][0].ToString();
                    index++;
                }
                if (dtCurrentTable.Rows.Count != 0)
                {
                    // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                    s = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                    //s = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                }
                if (s != "")
                {
                    int[] nums = Array.ConvertAll(s.Split(','), int.Parse);
                    ListBox ddlyearlywise = ((ListBox)e.Row.FindControl("ddlyearlywise"));

                    foreach (ListItem listItem in ddlyearlywise.Items)
                    {
                        if (Array.IndexOf(nums, Int16.Parse(listItem.Value)) != -1)
                        {
                            listItem.Selected = true;
                        }
                    }
                }
                if (dtCurrentTable.Rows.Count != 0)
                {
                    // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                    s = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder_"].ToString();
                    //s = dtCurrentTable.Rows[e.Row.RowIndex]["DateofReminder"].ToString();
                }
                if (s != "")
                {
                    int[] nums = Array.ConvertAll(s.Split(','), int.Parse);

                    ListBox ddlmonth = ((ListBox)e.Row.FindControl("ddlmonth"));
                    foreach (ListItem listItem in ddlmonth.Items)
                    {
                        if (Array.IndexOf(nums, Int16.Parse(listItem.Value)) != -1)
                        {
                            listItem.Selected = true;
                        }
                    }
                }
            }
            if (hdfId.Value != "")
            {
                //if (hdfId.Value == "") hdfId.Value = "0";
                DataSet GetRptmgr = GetEmployeeRptmgr(Convert.ToInt32(hdfId.Value));

                if (GetRptmgr.Tables[0].Rows.Count > 0)
                {
                    s = GetRptmgr.Tables[0].Rows[0][0].ToString();
                    index++;
                }
            }
            //else if (grdreminderlist.Rows.Count != 0)
             if (dtCurrentTable.Rows.Count != 0)
            {
                // ListBox Peopletobereminded = (ListBox)grdreminderlist.Rows[e.Row.RowIndex].Cells[4].FindControl("employeeslist");
                s = dtCurrentTable.Rows[e.Row.RowIndex]["Peopletobereminded"].ToString();
            }
            if (s != "")
            {
                int[] nums = Array.ConvertAll(s.Split(','), int.Parse);
                ListBox lstemployee = ((ListBox)e.Row.FindControl("employeeslist")); //ADDED BY SOF
                foreach (ListItem listItem in dd3.Items)
                {
                    if (Array.IndexOf(nums, Int16.Parse(listItem.Value)) != -1)
                    {
                        listItem.Selected = true;
                    }
                }
            }
        }
        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>fnShowRmainder();</script>");
    }
}