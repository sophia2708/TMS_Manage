using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
using System.Data;
using System.Data.Sql;
using AnalyticBrainsBL;

namespace AnalyticBrainsBL
{
    public class HolidayListBL
    {
        HolidayListDO objHolidayListDO = new HolidayListDO();
        HolidayListDAL objHolidayListDAL = new HolidayListDAL();


        public HolidayListDO bindddlgetholidaylist() 
        {
            return objHolidayListDAL.bindddlgetholidaylist();
        }
        public HolidayListDO bindddlgetdailywishlist() 
        {
            return objHolidayListDAL.bindddlgetdailywishlist();
        }
        //public HolidayListDO bindddlreceivinggreetinglist()
        //{
        //    return objHolidayListDAL.bindddlreceivinggreetinglist();
        //}
        public HolidayListDO bindddlreceivinggreetinglist(HolidayListDO objHolidayListDO)
        {
            objHolidayListDAL.bindddlreceivinggreetinglist(objHolidayListDO);
            return objHolidayListDO;
        }
        public HolidayListDO bindddlgetReminderlist() 
        {
            return objHolidayListDAL.bindddlgetReminderlist();
        }
        public HolidayListDO bindddlgetTimesheetdata(HolidayListDO objHolidayListDO)
        {
            objHolidayListDAL.bindddlgetTimesheetdata(objHolidayListDO);
            return objHolidayListDO;
        }
        
        public void InsertActivitymsg(HolidayListDO objLeaveHistoryDO)
        {
            objHolidayListDAL.InsertActivitymsg(objLeaveHistoryDO);
        }

    }
}