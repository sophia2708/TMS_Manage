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

public partial class Includes_WebForm_Home : System.Web.UI.Page
{
    
    HolidayListDO ObjHolidayListDO = new HolidayListDO();
    HolidayListBL ObjHolidayListBL = new HolidayListBL();
    HolidayListDO retresultdo = new HolidayListDO();
    public string sessionid = string.Empty;
    public string Username = string.Empty;
    public int EmpId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["FirstName"].ToString() != "")
            {

            }
            if (Request.QueryString["sessionid"] != null)
            {
                sessionid = Request.QueryString["sessionid"].ToString();
            }

            if (Request.QueryString["Username"] != null)
            {
                Username = Request.QueryString["Username"].ToString();
                Session["Username"] = Username;

            }

            if (Request.QueryString["EmpId"] != null)
            {
                EmpId = Convert.ToInt32(Request.QueryString["EmpId"].ToString());
                Session["EmpId"] = EmpId;

            }
            
            if (!IsPostBack)
            {

                if (EmpId == 2)
                {

                    lblClientid.Visible = true;
                    grdtaskreminderrecord.Visible = true;
                    GetReminder();


                }
                else
                {
                    grdtaskreminderrecord.Visible = false;
                }

            }
           
            Getdailywish();
            GetTimesheetdataEntry();
            Getgreetingreceiving();
           

            }

        catch
        {
            Response.Redirect("Login.aspx");
        }

    }
    protected void Entertimesheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("EnterTimeSheet.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    protected void ApplyLeave_Click(object sender, EventArgs e)
    {
        Response.Redirect("LeaveHistory.aspx?EmpId=" + EmpId + "&sessionid=" + sessionid + "&Username=" + Username);
    }
    public void Getgreetingreceiving()
    {
        try
        {
            int sendingId = Convert.ToInt32(Session["EmpId"]);
            //int recipientsId;

            //if (int.TryParse(btnsendmsg.Attributes["index"], out recipientsId))
            //{
                ObjHolidayListDO.EmpId = sendingId;
               // ObjHolidayListDO.Togreetingmsg = recipientsId;
                retresultdo = ObjHolidayListBL.bindddlreceivinggreetinglist(ObjHolidayListDO);
                //ObjHolidayListDO.EmpId = Convert.ToInt32(Session["EmpId"]);
               // retresultdo = ObjHolidayListBL.bindddlgetTimesheetdata(ObjHolidayListDO);
                grdmsgreceiving.DataSource = retresultdo.Resultdtset;
                grdmsgreceiving.DataBind();
                //grdmsgreceiving.Visible = true;
            //}   
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Getdailywish() 
    {
        try
        {           
            retresultdo = ObjHolidayListBL.bindddlgetdailywishlist();
            grdactivities.DataSource = retresultdo.Resultdtset;            
            grdactivities.DataBind();
            if (grdactivities.Rows.Count == 0)
            {
                dailyrecord.Visible = true;
            }
            else
            {
                dailyrecord.Visible = false;
            }
        } 
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void GetReminder() 
    {
        try
        {
            retresultdo = ObjHolidayListBL.bindddlgetReminderlist();
            grdtaskreminderrecord.DataSource = retresultdo.Resultdtset;            
            grdtaskreminderrecord.DataBind();

            if (grdtaskreminderrecord.Rows.Count == 0)
            {
                nolabel.Visible = true; // Show the label when no records are found
            }
            else
            {
                nolabel.Visible = false; // Hide the label when records are found
            }
        } 
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void ClearPopup()
    {

        Messagetaken.Text = "";

    }

    //protected void Activitymsg(object sender, EventArgs e) 
    //{
    //    //hfdidmsg = ((HiddenField).FindControl("hfdidmsg"));
    //    //ObjHolidayListDO.Id = Convert.ToInt32(hfdidmsg.Value);
    //    ObjHolidayListDO.EmpId = Convert.ToInt32(Session["EmpId"]);
    //    //ObjHolidayListDO.Tomessage = Convert.ToInt32(Session["EmpId"]);
    //    ObjHolidayListDO.Activitymsg = Messagetaken.Text;
    //    ObjHolidayListBL.InsertActivitymsg(ObjHolidayListDO);
    //    string script = "alert('Message sent successfully!');";
    //    ClientScript.RegisterStartupScript(this.GetType(), "MessageSent", script, true);
    //    ClearPopup();
    //    return;

        
    //}
    protected void Activitymsg(object sender, EventArgs e)
    {
        int senderId = Convert.ToInt32(Session["EmpId"]);

        
        int recipientId;

        if (int.TryParse(btnsendmsg.Attributes["index"], out recipientId))
        {
            string message = Messagetaken.Text;
            ObjHolidayListDO.EmpId = senderId;
            ObjHolidayListDO.Tomessage = recipientId; // Assign the parsed recipientId
            ObjHolidayListDO.Activitymsg = message;
            ObjHolidayListBL.InsertActivitymsg(ObjHolidayListDO);
            string script = "alert('Message sent successfully!');";
            ClientScript.RegisterStartupScript(this.GetType(), "MessageSent", script, true);
            ClearPopup();
            Getgreetingreceiving();
            return;
        }
        else
        {
            
        }
    }

   public void GetTimesheetdataEntry()
    {
        try
        {
            
            ObjHolidayListDO.EmpId = Convert.ToInt32(Session["EmpId"]);
            retresultdo = ObjHolidayListBL.bindddlgetTimesheetdata(ObjHolidayListDO);
            grdtimesheetdataentries.DataSource = retresultdo.Resultdtset;
            grdtimesheetdataentries.DataBind();
            if (grdtimesheetdataentries.Rows.Count == 0)
            {
                NoRecordsLabel.Visible = true; 
            }
            else
            {
                NoRecordsLabel.Visible = false; 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
//    protected void grdmsgreceiving_RowCommand(object sender, GridViewCommandEventArgs e)
//{
//    if (e.CommandName == "ShowMessage")
//    {
//        int rowIndex = Convert.ToInt32(e.CommandArgument);
//        Label label = grdmsgreceiving.Rows[rowIndex].FindControl("txtlists") as Label;

//        if (label != null)
//        {
//            string combinedMessage = label.Text;

//            // Assuming 'combinedMessage' is the message content from the database.
//            // You can pass it to a JavaScript function to display in a message box.
//            ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", "showMessageBox('{combinedMessage}');", true);
//        }
//    }
//}



   protected void Greeting_RowCommand(Object sender, GridViewCommandEventArgs e)
   {

       if (e.CommandName == "Lnkgreetsend") 
       {
           
           int result = 0;
           int index = Convert.ToInt32(e.CommandArgument);
           HolidayListDAL objLeaveHistoryDAL = new HolidayListDAL();
           ObjHolidayListDO.EmpId = index;
           string commandArgument = e.CommandArgument.ToString();
           btnsendmsg.Attributes["index"] = commandArgument;
           ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "MessagePopup();", true);

           //ObjHolidayListDO = objLeaveHistoryDAL.getvalue_LeaveCancel(ObjLeaveHistoryDO);
           //result = Convert.ToInt32(ObjHolidayListDO.Resultdtset.Tables[0].Rows[0][0]);
           //btnsendmsg.value = e.CommandArgument;
           //openpopup();
       }
       
   }
    
}





