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


 
public partial class Includes_WebForm_DataCalender : System.Web.UI.Page
{
    SqlConnection mycn;
    SqlDataAdapter myda;
    DataSet ds = new DataSet();
    DataSet dsSelDate;
    String strConn;

    
    private void Page_Load(object sender, System.EventArgs e)
    {
       
        // Put user code to initialize the page here
        strConn = "Data Source=analytic10;uid=abuser;pwd=abuser;Initial Catalog=Analyticbrains";
        mycn = new SqlConnection(strConn);
        myda = new SqlDataAdapter("LM_DATACALENDER", mycn);
        myda.Fill(ds, "Table");
    }
    protected void CalendarDRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
    {
        // If the month is CurrentMonth
        if (!e.Day.IsOtherMonth)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if ((dr["EventDate"].ToString() != DBNull.Value.ToString()))
                {
                    DateTime dtEvent = (DateTime)dr["EventDate"];
                    if (dtEvent.Equals(e.Day.Date))
                    {
                        //e.Cell.BackColor = color.PaleVioletRed;
                    }
                }
            }
        }
        //If the month is not CurrentMonth then hide the Dates
        else
        {
            e.Cell.Text = "";
        }
    }
    private void Calendar2_SelectionChanged(object sender, System.EventArgs e)
    {
        myda = new SqlDataAdapter("Select * from EventsTable where EventDate='" + Calendar2.SelectedDate.ToString() + "'", mycn);
        dsSelDate = new DataSet();
        myda.Fill(dsSelDate, "AllTables");
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

}