using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
namespace AnalyticBrainsDO
{
    public class TimesheetDO
    {
        #region Declarations
        private int _EmpId;
        private string _Type;
        private DateTime _FromDate;
        private DateTime _ToDate;
        private int _ProjectId;
        private string _Project;
        private int _ModuleId;
        private string _Module;
        private int _TaskId;
        private int _Parent_TaskId;
        private string _TaskName;
        private string _taskdesc;
        private string _Issues;
        private string _object;
        private string _hours;

        private int _rtn;
        private int _RowNum;
        private int _RowId;
        private string _ddl;
        private DataSet _edittable;
        
        private int _Month;
        private string _Username;
        private string _FirstName;
        private int _Priority;
        private int _Status;
        private int _ModeofWork;
        private int _year;
        private int _userid;

        private int _clinetid;
        private int _taskcat;
        private DateTime _plannedstartdate;
        private float _plannedeffort;
        private DateTime _actualstartdate;
        private float _actualeffort;
        private int _inputvalue;
        private int _parent_id;
        private string _FTR;
        private string _OTD;
        private int _EmpTaskid;
        private int _rework;
      
        private DateTime _plannedstartdtask;
        private DateTime _plannedenddate;
        private string _clientname;

        private int _LabelId;
        private int _id;
        private string _TaskNameReminder;
        private string _TaskFrequency;
        private string _Dateofreminder;
        private string _PeopleToBeReminded;
        private string _Weekly;
        private string _Monthly;
        private string _Yearly;
        private string _Yearly1;
        private string _Biweekly;
        private string _BiMonthly;
 

        #endregion

        #region Public Properties
        
        public string TaskNameReminder
        {
            get
            {
                return _TaskNameReminder;
            }
            set
            {
                _TaskNameReminder = value;
            }
        }
        public string TaskFrequency
        {
            get
            {
                return _TaskFrequency;
            }
            set
            {
                _TaskFrequency = value;
            }
        }
        public string PeopleToBeReminded
        {
            get
            {
                return _PeopleToBeReminded;
            }
            set
            {
                _PeopleToBeReminded = value;
            }
        }
        public string Weekly
        {
            get
            {
                return _Weekly;
            }
            set
            {
                _Weekly = value;
            }
        }
        public string Monthly
        {
            get
            {
                return _Monthly;
            }
            set
            {
                _Monthly = value;
            }
        }
        public string Yearly
        {
            get
            {
                return _Yearly;
            }
            set
            {
                _Yearly = value;
            }
        }
        public string Yearly1
        {
            get
            {
                return _Yearly1;
            }
            set
            {
                _Yearly1 = value;
            }
        }
        public string Biweekly
        {
            get
            {
                return _Biweekly;
            }
            set
            {
                _Biweekly = value;
            }
        }
        public string BiMonthly
        {
            get
            {
                return _BiMonthly;
            }
            set
            {
                _BiMonthly = value;
            }
        }

        public string Dateofreminder
        {
            get
            {
                return _Dateofreminder;
            }
            set
            {
                _Dateofreminder = value;
            }
        }
        private int LabelId
        {
            get
            {
                return _LabelId;
            }
            set
            {
                _LabelId = value;
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
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
            }
        }
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        public DateTime FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
            }
        }
        public int ProjectId
        {
            get
            {
                return _ProjectId;
            }
            set
            {
                _ProjectId = value;
            }
        }
        public string ProjectName
        {
            get
            {
                return _Project;
            }
            set
            {
                _Project = value;
            }
        }
        public int ModuleId
        {
            get
            {
                return _ModuleId;
            }
            set
            {
                _ModuleId = value;
            }
        }
        public string ModuleName
        {
            get
            {
                return _Module;
            }
            set
            {
                _Module = value;
            }
        }
        public int TaskId
        {
            get
            {
                return _TaskId;
            }
            set
            {
                _TaskId = value;
            }
        }
        public int ParentTaskid
        {
            get
            {
                return _Parent_TaskId;
            }
            set
            {
                _Parent_TaskId = value;
            }
        }
        public string TaskName
        {
            get
            {
                return _TaskName;
            }
            set
            {
                _TaskName = value;
            }
        }
        public string Taskdesc
        {
            get
            {
                return _taskdesc;
            }
            set
            {
                _taskdesc = value;
            }
        }
        public string Issues
        {
            get
            {
                return _Issues;
            }
            set
            {
                _Issues = value;
            }
        }
        public string Object
        {
            get
            {
                return _object;
            }
            set
            {
                _object = value;
            }
        }
        public string Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = value;
            }
        }
        public int rtn
        {
            get
            {
                return _rtn;
            }
            set
            {
                _rtn = value;
            }
        }
        public int RowNum
        {
            get
            {
                return _RowNum;
            }
            set
            {
                _RowNum = value;
            }
        }
        public int RowId
        {
            get
            {
                return _RowId;
            }
            set
            {
                _RowId = value;
            }
        }
        public string ddl
        {
            get
            {
                return _ddl;
            }
            set
            {
                _ddl = value;
            }
        }
        public int Month
        {
            get
            {
                return _Month;
            }
            set
            {
                _Month = value;
            }
        }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public int Priority
        {
            get { return _Priority; }
            set { _Priority=value;}
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public int ModeofWork
        {
            get { return _ModeofWork; }
            set { _ModeofWork = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public string _name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Clients
        {
            get { return _clinetid; }
            set { _clinetid = value; }
        }
        public int Taskcat
        {
            get { return _taskcat; }
            set { _taskcat = value; }
        }
        public DateTime Plannedstartdate
        {
            get { return _plannedstartdate; }
            set { _plannedstartdate = value; }
        }
        public float Plannedeffort
        {
            get { return _plannedeffort; }
            set { _plannedeffort = value; }
        }
        public DateTime Actualstartdate
        {
            get { return _actualstartdate; }
            set { _actualstartdate = value; }

        }

        public float Actualeffort
              {
                  get { return _actualeffort; }
                  set { _actualeffort= value; }

              }
         public int inputvalue
        {
            get { return _inputvalue; }
            set { _inputvalue = value; }
        }
         public DataSet edittable
         {
             get { return _edittable; }
             set { _edittable = value; }
         }
         public string FTR
         {
             get { return _FTR; }
             set { _FTR = value; }
         }
         public string OTD
         {
             get { return _OTD; }
             set { _OTD = value; }
         }
         public int Parentid
         {
             get { return _parent_id; }
             set { _parent_id = value; }
         }
         public int EmpTaskid
         {
             get { return _EmpTaskid; }
             set { _EmpTaskid = value; }
         }
        public int reworkid
         {
             get { return _rework; }
             set { _rework = value; }
         }
        public DateTime plannedstartdtask
        {
            get
            {
                return _plannedstartdtask;
            }
            set
            {
                _plannedstartdtask = value;

            }
        }
        public DateTime plannedenddate
        {
            get
            {
                return _plannedenddate;
            }
            set
            {
                _plannedenddate = value;

            }
        }
        public string ClientName
        {
            get
            {
                return _clientname;
            }
            set
            {
                _clientname = value;
            }
        }
        #endregion
    }
}
