using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
namespace AnalyticBrainsDO
{
    public class HolidayListDO
    {
        #region Declarations
        private int _Id;
        private int _EmpId;
        private DataSet _Resultdtset;
        private string _Activitymsg;
        private int _Tomessage;
        private int _Togreetingmsg;
        
        #endregion

        #region Public Properties
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        public string Activitymsg
        {
            get
            {
                return _Activitymsg;
            }
            set
            {
                _Activitymsg = value;
            }
        }
        public int  EmpId
        {
            get
            {
                return _EmpId;
            }
            set
            {
                _EmpId = value;
            }
        }
        public int Tomessage
        {
            get
            {
                return _Tomessage;
            }
            set
            {
                _Tomessage = value;
            }
        }
        public int Togreetingmsg
        {
            get
            {
                return _Togreetingmsg;
            }
            set
            {
                _Togreetingmsg = value;
            }
        }
        public DataSet Resultdtset
        {
            get
            {
                return _Resultdtset;
            }

            set
            {
                _Resultdtset = value;
            }
        }
       
        #endregion
    }
}
