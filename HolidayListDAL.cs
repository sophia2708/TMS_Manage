using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
using System.Globalization;
using AB.DataAccessLayer;
using AB.Utilities;
using System.Collections;

namespace AnalyticBrainsDAL
{
    public class HolidayListDAL
    {
        #region Declaration

        ResultDO retresultdo = new ResultDO();
        SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
        IDataReader reader = null;
        ArrayList myArrlst = new ArrayList();
        #endregion
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        // DataTable dt;
        DataSet ds;
        //  ResultDO retresultdo;
        CultureInfo provider = CultureInfo.InvariantCulture;

        public HolidayListDO bindddlgetholidaylist() //ADDED BY SOPHIA
        {
            HolidayListDO objHolidayListDO = new  HolidayListDO();
            SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
            DataTable dt = new DataTable();
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("Menus_HolidayList");
                objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("Menus_HolidayList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objHolidayListDO;
        }
        public HolidayListDO bindddlgetdailywishlist() //ADDED BY SOPHIA
        {
            HolidayListDO objHolidayListDO = new HolidayListDO();
            SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
            DataTable dt = new DataTable();
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_DailyWish_List");
                objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("SP_DailyWish_List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objHolidayListDO;
        }
        //public HolidayListDO bindddlreceivinggreetinglist() //ADDED BY SOPHIA
        //{
        //    HolidayListDO objHolidayListDO = new HolidayListDO();
        //    SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        IDataParameter[] objparam = daHelper.GetSpParameterSet("GreetingReceive");
        //        objparam[0].Value = objHolidayListDO.Togreetingmsg;
        //        objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("GreetingReceive", objparam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return objHolidayListDO;
        //}
        public HolidayListDO bindddlreceivinggreetinglist(HolidayListDO objHolidayListDO)
        {
            try
            {

                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("GreetingReceive");
                objparam1[0].Value = objHolidayListDO.EmpId;
                //objparam1[1].Value = objHolidayListDO.Togreetingmsg;
                objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("GreetingReceive", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objHolidayListDO;
        }
        public HolidayListDO bindddlgetTimesheetdata(HolidayListDO objHolidayListDO)
        {
            try
            {

                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("AB_TimesheetData_entry");
                objparam1[0].Value = objHolidayListDO.EmpId;
                objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("AB_TimesheetData_entry", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objHolidayListDO;
        }
        public HolidayListDO bindddlgetReminderlist() //ADDED BY SOPHIA
        {
            HolidayListDO objHolidayListDO = new HolidayListDO();
            SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
            DataTable dt = new DataTable();
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_Get_Taskreminder_List");
                objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("AB_Get_Taskreminder_List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objHolidayListDO;
        }
        public void InsertActivitymsg(HolidayListDO objHolidayListDO) 
        {
            IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_HM_Activitymsg");
            //objparam[0].Value = objHolidayListDO.Id;
            objparam[0].Value = objHolidayListDO.EmpId;
            objparam[1].Value = objHolidayListDO.Tomessage;
            objparam[2].Value = objHolidayListDO.Activitymsg;
            objHolidayListDO.Resultdtset = daHelper.ExecuteDataset("SP_HM_Activitymsg", objparam);
        }
       }
}
