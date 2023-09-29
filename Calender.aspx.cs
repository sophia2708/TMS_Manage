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
using LeaveManagementDO;
using LeaveManagementBL;
using System.Collections.Generic;
using System.Text;




public partial class Includes_WebForm_Calender : System.Web.UI.Page
{
    LeaveHistoryDO objLeaveHistoryDO = new LeaveHistoryDO();
    LeaveHistoryBL ObjLeaveHistoryBL = new LeaveHistoryBL();
    SqlConnection mycn;
    SqlDataAdapter myda;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
    DataSet dsSelDate;
    String strConn;
    DateTime today = DateTime.Today;
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da = new SqlDataAdapter();
    StringBuilder strApproved = new StringBuilder();
    StringBuilder strPending = new StringBuilder();
    StringBuilder strOnHold = new StringBuilder();
    private void Page_Load(object sender, System.EventArgs e)
    {
        //DateTime Datetest = new DateTime();
        // Put user code to initialize the page here
        strConn = "Data Source=analytic10;uid=abuser;pwd=abuser;Initial Catalog=Analyticbrains";
        mycn = new SqlConnection(strConn);
        HolidayList();
        MorethanoneEMp();
        DataTable dt = new DataTable();
        cmd = new SqlCommand("[LM_Data_calender]", mycn);
        //cmd.Parameters.Add(new SqlParameter("@startdate", today));
        cmd.CommandType = CommandType.StoredProcedure;
        da.SelectCommand = cmd;
        da.Fill(ds);
    }

    public object getdatacalender()
    {
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("[LM_Data_calender]", mycn);
            // cmd.Parameters.Add(new SqlParameter("@startdate"));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
    }

    public object HolidayList()
    {

        SqlCommand cmd1 = new SqlCommand();
        SqlDataAdapter da1 = new SqlDataAdapter();
        DataTable dt1 = new DataTable();
        cmd1 = new SqlCommand("select Holiday_leaveId,Holiday_date,Holiday_name  from lm_holidaylist", mycn);
        // cmd.Parameters.Add(new SqlParameter("@startdate"));
        cmd.CommandType = CommandType.StoredProcedure;
        da1.SelectCommand = cmd1;
        da1.Fill(ds1);
        return ds1;
    }

    public object MorethanoneEMp()
    {

        SqlCommand cmd2 = new SqlCommand();
        SqlDataAdapter da2 = new SqlDataAdapter();
        DataTable dt2 = new DataTable();
        cmd2 = new SqlCommand("LM_EMP_morethan1", mycn);
        // cmd.Parameters.Add(new SqlParameter("@startdate"));
        cmd.CommandType = CommandType.StoredProcedure;
        da2.SelectCommand = cmd2;
        da2.Fill(ds2);
        return ds2;
    }
    protected void CalendarDRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
    {
        // getdatacalender();

        StringBuilder strStatus = new StringBuilder();
        StringBuilder strEndDate = new StringBuilder();

        if (!e.Day.IsOtherMonth)
        {
            //string 

            DataTable contacts = ds.Tables[0];
            DataView view = contacts.AsDataView();
            view.RowFilter = "StartDate='" + e.Day.Date + "'";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                string STATUSNAME = dr["STATUSNAME"].ToString();
                DateTime STARTDATE = Convert.ToDateTime(dr["STARTDATE"]);
                DateTime ENDDATE = Convert.ToDateTime(dr["ENDDATE"]);
                //string STARTDATE1 = STARTDATE.ToString("dd-MMM-yyyy");
                //string ENDDATE1 = ENDDATE.ToString("dd-MMM-yyyy");
                //decimal NOofDays = Convert.ToDecimal(dr["NO_OF_DAYS"]);
                if ((dr["StartDate"].ToString() != DBNull.Value.ToString()) || (dr["ENDDATE"].ToString() != DBNull.Value.ToString()))
                {
                    DateTime dtEvent = (DateTime)dr["StartDate"];
                    DateTime dtEvent1 = (DateTime)dr["ENDDATE"];

                    if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday || e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        e.Cell.ForeColor = System.Drawing.Color.Black;
                        e.Cell.BackColor = System.Drawing.Color.White;
                    }

                    if ((dtEvent.Equals(e.Day.Date)) || (dtEvent1.Equals(e.Day.Date)))
                    {

                        if (STATUSNAME == "Approved")
                        {

                            //str.Add(appendtext);
                            e.Cell.BackColor = System.Drawing.Color.Tomato;

                        }
                        else if (STATUSNAME == "Pending")
                        {

                            e.Cell.BackColor = System.Drawing.Color.Silver;
                            //e.Cell.ForeColor = System.Drawing.Color.Black;

                        }
                        else if (STATUSNAME == "On Hold")
                        {
                            e.Cell.BackColor = System.Drawing.Color.Yellow;

                        }
                        else
                        {
                            e.Cell.Text = "";
                        }
                    }

                }
            }

            foreach (DataRowView rowView in view)
            {
                string FirstName = rowView.Row.ItemArray[0].ToString();
                string LeaveType = rowView.Row.ItemArray[1].ToString();
                DateTime StartDate = Convert.ToDateTime(rowView.Row.ItemArray[2]);
                DateTime EndDate = Convert.ToDateTime(rowView.Row.ItemArray[3]);
                decimal NOofDays = Convert.ToDecimal(rowView.Row.ItemArray[4]);
                string STATUSNAME = rowView.Row.ItemArray[5].ToString();
                string AppendText = "[" + FirstName + " - " + LeaveType + " - " + NOofDays + " days " + " - " + STATUSNAME + "]" + " ; ";
                strStatus.Append(AppendText);
                e.Cell.Attributes.Add("onmouseover", e.Cell.ToolTip = strStatus.ToString());
            }



            //DataTable contacts1 = ds.Tables[0];
            //DataView view1 = contacts.AsDataView();
            //view1.RowFilter = "EndDate='" + e.Day.Date + "'";



            //foreach (DataRowView rowView in view1)
            //{
            //    string FirstName = rowView.Row.ItemArray[0].ToString();
            //    string LeaveType = rowView.Row.ItemArray[1].ToString();
            //    DateTime StartDate = Convert.ToDateTime(rowView.Row.ItemArray[2]);
            //    DateTime EndDate = Convert.ToDateTime(rowView.Row.ItemArray[3]);
            //    decimal NOofDays = Convert.ToDecimal(rowView.Row.ItemArray[4]);
            //    string STATUSNAME = rowView.Row.ItemArray[5].ToString();
            //    string AppendText1 = "[" + FirstName + " - " + LeaveType + "-" + NOofDays + "-" + STATUSNAME + "]";
            //    strEndDate.Append(AppendText1);
            //    e.Cell.Attributes.Add("onmouseover", e.Cell.ToolTip = strEndDate.ToString());
            //}



            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {

                string HolidayName = dr1["Holiday_name"].ToString();
                // string Holidaylist = 
                if ((dr1["Holiday_date"].ToString() != DBNull.Value.ToString()))
                {
                    DateTime dtEvent = (DateTime)dr1["Holiday_date"];
                    // String dtEvent = (DateTime)dr["StartDate"];
                    if (dtEvent.Equals(e.Day.Date))
                    {

                        e.Cell.BackColor = System.Drawing.Color.Green;
                        e.Cell.Attributes.Add("onmouseover", e.Cell.ToolTip = HolidayName);
                        // Calendar1.ShowNextPrevMonth = true;
                        //Calendar1.ShowNextPrevMonth = ;
                        Calendar1.ShowNextPrevMonth = false;
                        if (e.Day.IsOtherMonth)
                        {
                            e.Cell.Text = "";
                            e.Cell.BackColor = System.Drawing.Color.White;
                        }
                    }



                }
                else
                {
                    e.Cell.Text = "";
                }
            }

            foreach (DataRow dr2 in ds2.Tables[0].Rows)
            {

                string Morethanoneempapp = dr2["Startdate"].ToString();
                // string Holidaylist = 


                if ((dr2["StartDate"].ToString() != DBNull.Value.ToString()))
                {
                    DateTime dtEvent = (DateTime)dr2["Startdate"];

                    int Count = Convert.ToInt32(dr2["Count"]);
                    // String dtEvent = (DateTime)dr["StartDate"];
                    if (Count > 1)
                    {
                        if (dtEvent.Equals(e.Day.Date))
                        {
                            e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#d8b511");
                        }
                        if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday || e.Day.Date.DayOfWeek == DayOfWeek.Saturday)
                        {
                            e.Cell.ForeColor = System.Drawing.Color.Black;
                            e.Cell.BackColor = System.Drawing.Color.White;
                        }
                    }



                }
                else
                {
                    e.Cell.Text = "";
                }
            }


        }
    }
    private void Calendar1_SelectionChanged(object sender, System.EventArgs e)
    {
        myda = new SqlDataAdapter("LM_DATACALENDER", mycn);
        dsSelDate = new DataSet();
        myda.Fill(dsSelDate, "LM_DATACALENDER");
        if (dsSelDate.Tables[0].Rows.Count == 0)
        {
            DataGrid1.Visible = false;
        }
        else
        {
            DataGrid1.Visible = true;
            DataGrid1.DataSource = dsSelDate;
            DataGrid1.DataBind();
        }
    }

    public class dataCalendar
    {
        public string Firstname { get; set; }
        public string Leavename { get; set; }
        public string STATUSNAME { get; set; }
        public DateTime STARTDATE { get; set; }
        public DateTime ENDDATE { get; set; }

    }



}