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

public partial class EmployeeList : System.Web.UI.Page
{
    TimesheetDO ObjTimesheetDO = new TimesheetDO();
    TimesheetBL ObjTimesheetBL = new TimesheetBL();

    EmployeeDO ObjEmployeeDO = new EmployeeDO();
    EmployeeBL ObjEmployeeBL = new EmployeeBL();

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
        }
    }
    public void GetEmployeeList()
    {
        ResultDO retresultdo = new ResultDO();
        ObjTimesheetDO.EmpId = EmpId;
        retresultdo = ObjTimesheetBL.GetEmployeeList(ObjTimesheetDO);
        grdEmployeeList.DataSource = retresultdo;
        grdEmployeeList.DataBind();

    }
    protected void grdEmployeeList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdEmployeeList.EditIndex = e.NewEditIndex;

        int CurrentRow = grdEmployeeList.EditIndex;
        Label lblEmpId = (Label)grdEmployeeList.Rows[CurrentRow].FindControl("lblEmpid");
        Response.Write(lblEmpId.Text);
        Response.Redirect("EditProfile.aspx?EmpId=" + lblEmpId.Text + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    protected void grdEmployeeList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblempid = (Label)grdEmployeeList.Rows[e.RowIndex].FindControl("lblEmpid");
        ObjEmployeeDO.EmpID = Convert.ToInt32(lblempid.Text);
        bool result;
        result = ObjEmployeeBL.DeleteEmployee(ObjEmployeeDO);
        if (result)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Deleted successfully!');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Employee not deleted!');", true);
        }
        GetEmployeeList();
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
    //    //foreach (GridViewRow rw in grdEmployeeList.Rows)
    //    //{
    //    //    CheckBox chk = (CheckBox)rw.Cells[2].FindControl("chkActive");
    //    //    if (chk.Checked == true)
    //    //    {
    //    //        grdEmployeeList.SelectedIndex = rw.RowIndex;
    //    //        ViewState["SavIndex"] = rw.RowIndex;
    //    //    }
    //   // }
    }

    protected void grdEmployeeList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
               // CheckBox chkActive = (CheckBox)e.Row.FindControl("chkActive");
                Label lblEmpid = (Label)e.Row.FindControl("lblEmpid");
                Response.Write(lblEmpid.Text);

                //GetEmployeeList();
            }
        }
    }
    protected void grdEmployeeList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName=="Change")
        {
            int index = int.Parse(e.CommandArgument.ToString());

            GridViewRow row = grdEmployeeList.Rows[index];
            CheckBox chkActive = (CheckBox)row.FindControl("chkActive");
            Label lblEmpid = (Label)row.FindControl("lblEmpid");
            Response.Write("Rowcommand"+lblEmpid.Text);
        }
    }
    protected void grdEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void grdEmployeeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            grdEmployeeList.EditIndex = e.Row.DataItemIndex;
            int CurrentRow = grdEmployeeList.EditIndex;
            Label lblEmpId = (Label)grdEmployeeList.Rows[CurrentRow].FindControl("lblEmpid");
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
                DataRowView dr = e.Row.DataItem as DataRowView;
                // CheckBox chkActive = (CheckBox)e.Row.FindControl("chkActive");
                Label lblEmpid = (Label)e.Row.FindControl("lblEmpid");
                Response.Write("RowDataBound" + lblEmpid.Text);

            }
        }
    }
}