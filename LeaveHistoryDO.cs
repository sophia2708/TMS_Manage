using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace LeaveManagementDO
{
    public class LeaveHistoryDO
    {
        //declarations history

        #region declarations
        private int _EmpLeaveApplicationId;

        private int _Rptmgr;
        private String _LeaveType;
        private int _LeaveID;
        private DateTime _AppliedOn;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private string _No_Of_Days;
        private int _Frequency;
        private String _Reason;
        private int _Ananymous;
        private string _Leaveoptions; //ADDED BY SOF
        private DateTime _CompensateLeavedates;  //ADDED BY SOF
        private DateTime _PreviousCompensatedates; //ADDED BY SOF
        //private string _Previousdates;
        private int _Status;
        private byte _StatusCode;
        private DateTime _CreatedOn;
        private DateTime _ModifiedOn;

        //declarations leavebalance
        private int _EmpLeaveTransactionID;
        private int _YearId;
        private int _Beginingbalance;
        private int _Currentbalance;

        //declarations employeedetails
        private String _MonthandYear;
        private int _EmpId;
        private String _FirstName;
        private DataSet dtset;

        //declarations managers grid

        private String _Comments;
        private String _StatusComments;

        //declaration Permission
        private int _PremissionApplicationID;
        private String _StartTime;
        private String _EndTime;
        private String _PermissionType;
        private int _PermissionID;
        private int _Duration;

        //lop cancel request
        private DateTime _lopStartdate;
        private DateTime _lopEndDate;
        private string _lopAction;
        private float _lopTimesheethrs;
        private float _lopleavehrs;
        private string _Comment;
        private DataSet _Resultdtset;
        private string _TaskDate;

        private int _empid;
        private DateTime _FromExpensedate;
        private DateTime _ToFromExpensedate;
        private DateTime _Fromdate;
        private DateTime _ToStartdate;

        private string _Singledate;
        private string _ParticularDate;
        private string _Alldate;
        private string _FromChooseDate;
        private string _ToChooseDate;
        private string _EmployeeName;
        private string _LeavesType;

        private float _OpenLeave;
        private float _EarnLeave;
        private float _AvailedLeave;
        private float _CurrentBalance;
        private float _Lossofpay;
        private int _EmptransactId;

        #endregion

        #region public properties
        public float OpenLeave
        {
            get
            {
                return _OpenLeave;
            }
            set
            {
                _OpenLeave = value;
            }
        }
        public float EarnLeave
        {
            get
            {
                return _EarnLeave;
            }
            set
            {
                _EarnLeave = value;
            }
        }
        public float AvailedLeave
        {
            get
            {
                return _AvailedLeave;
            }
            set
            {
                _AvailedLeave = value;
            }
        }
        public float CurrentBalance
        {
            get
            {
                return _CurrentBalance;
            }
            set
            {
                _CurrentBalance = value;
            }
        }
        public float Lossofpay
        {
            get
            {
                return _Lossofpay;
            }
            set
            {
                _Lossofpay = value;
            }
        }
        public string LeavesType  //ADDED BY SOPHIA
        {
            get
            {
                return _LeavesType;
            }
            set
            {
                _LeavesType = value;
            }
        }
        public string EmployeeName  //ADDED BY SOPHIA
        {
            get
            {
                return _EmployeeName;
            }
            set
            {
                _EmployeeName = value;
            }
        }
        public string ToChooseDate     //ADDED BY SOPHIA
        {
            get
            {
                return _ToChooseDate;
            }
            set
            {
                _ToChooseDate = value;
            }
        }
        public string Alldate     //ADDED BY SOPHIA
        {
            get
            {
                return _Alldate;
            }
            set
            {
                _Alldate = value;
            }
        }
        public string FromChooseDate      //ADDED BY SOPHIA
        {
            get
            {
                return _FromChooseDate;
            }
            set
            {
                _FromChooseDate = value;
            }
        }
        public string ParticularDate      //ADDED BY SOPHIA
        {
            get
            {
                return _ParticularDate;
            }
            set
            {
                _ParticularDate = value;
            }
        }
        public string Singledate       //ADDED BY SOPHIA
        {
            get
            {
                return _Singledate;
            }
            set
            {
                _Singledate = value;
            }
        }
        public DateTime Fromdate       //ADDED BY SOPHIA
        {
            get
            {
                return _Fromdate;
            }
            set
            {
                _Fromdate = value;
            }
        }
        public DateTime ToStartdate      //ADDED BY SOPHIA
        {
            get
            {
                return _ToStartdate;
            }
            set
            {
                _ToStartdate = value;
            }
        }
        public DateTime FromExpensedate       //ADDED BY SOPHIA
        {
            get
            {
                return _FromExpensedate;
            }
            set
            {
                _FromExpensedate = value;
            }
        }
        public DateTime ToExpensedate       //ADDED BY SOPHIA
        {
            get
            {
                return _ToFromExpensedate;
            }
            set
            {
                _ToFromExpensedate = value;
            }
        }
        public int EmpID
        {
            get
            {
                return _empid;
            }
            set
            {
                _empid = value;
            }
        }
        public int EmptransactId
        {
            get
            {
                return _EmptransactId;
            }
            set
            {
                _EmptransactId = value;
            }
        }
        
        public int EmpLeaveApplicationId
        {
            get
            {
                return _EmpLeaveApplicationId;
            }
            set
            {
                _EmpLeaveApplicationId = value;
            }
        }

        public int Rptmgr
        {
            get
            {
                return _Rptmgr;
            }
            set
            {
                _Rptmgr = value;
            }
        }


        public int LeaveID
        {
            get
            {
                return _LeaveID;
            }
            set
            {
                _LeaveID = value;
            }
        }

        public DateTime Startdate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }

        public string Noofdays
        {
            get
            {
                return _No_Of_Days;

            }
            set
            {
                _No_Of_Days = value;
            }
        }

        public int Frequency
        {
            get
            {
                return _Frequency;

            }
            set
            {
                _Frequency = value;
            }
        }
        public int Ananymous
        {
            get
            {
                return _Ananymous;

            }
            set
            {
                _Ananymous = value;
            }
        }
        public string Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                _Reason = value;
            }
        }


        public string Leaveoptions    //ADDED BY SOF
        {
            get
            {
                return _Leaveoptions;

            }
            set
            {
                _Leaveoptions = value;
            }
        }
        public DateTime CompensateLeavedates   //ADDED BY SOF
        {
            get
            {
                return _CompensateLeavedates;
            }
            set
            {
                _CompensateLeavedates = value;
            }
        }
        public DateTime PreviousCompensatedates   //ADDED BY SOF
        {
            get
            {
                return _PreviousCompensatedates;
            }
            set
            {
                _PreviousCompensatedates = value;
            }
        }
        //public string Previousdates   //ADDED BY SOF
        //{
        //    get
        //    {
        //        return _Previousdates;
        //    }
        //    set
        //    {
        //        _Previousdates = value;
        //    }
        //}


        public int Status
        {
            get
            {
                return _Status;

            }
            set
            {
                _Status = value;
            }
        }

        public Byte Statuscode
        {
            get
            {
                return _StatusCode;

            }
            set
            {
                _StatusCode = value;
            }
        }

        public DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                _CreatedOn = value;
            }
        }

        public DateTime ModifiedOn
        {
            get
            {
                return _ModifiedOn;
            }
            set
            {
                _ModifiedOn = value;
            }
        }

        public int EmpLeaveTransactionID
        {
            get
            {
                return _EmpLeaveTransactionID;

            }
            set
            {
                _EmpLeaveTransactionID = value;
            }
        }

        public int YearId
        {
            get
            {
                return _YearId;

            }
            set
            {
                _YearId = value;
            }
        }

        public int BeginingBalance
        {
            get
            {
                return _YearId;

            }
            set
            {
                _YearId = value;
            }
        }

        public int EmpId
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

        public String FirstName
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
        public String MonthandYear
        {
            get
            {
                return _MonthandYear;

            }
            set
            {
                _MonthandYear = value;
            }
        }

        public DataSet Resultdtset
        {
            get
            {
                return dtset;
            }
            set
            {
                dtset = value;
            }
        }
        public String Comments
        {
            get
            {
                return _Comments;

            }
            set
            {
                _Comments = value;
            }
        }
        public String StatusComments
        {
            get
            {
                return _StatusComments;

            }
            set
            {
                _StatusComments = value;
            }
        }
        public String LeaveType
        {
            get
            {
                return _LeaveType;

            }
            set
            {
                _LeaveType = value;
            }
        }
        // Permission 
        public int PermissionApplicationID
        {
            get
            {
                return _PremissionApplicationID;
            }
            set
            {
                _PremissionApplicationID = value;
            }
        }
        public String StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
            }
        }
        public String EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
            }
        }
        public String PermissionType
        {
            get
            {
                return _PermissionType;
            }
            set
            {
                _PermissionType = value;
            }
        }
        public int PermissionID
        {
            get
            {
                return _PermissionID;
            }
            set
            {
                _PermissionID = value;
            }
        }
        public int Duration
        {
            get
            {
                return _Duration;
            }
            set
            {
                _Duration = value;
            }
        }
        public DateTime LopStartdate
        {
            get
            {
                return _lopStartdate;
            }

            set
            {
                _lopStartdate = value;
            }
        }

        public DateTime LopEndDate
        {
            get
            {
                return _lopEndDate;
            }

            set
            {
                _lopEndDate = value;
            }
        }

        public string LopAction
        {
            get
            {
                return _lopAction;
            }

            set
            {
                _lopAction = value;
            }
        }

        public float LopTimesheethrs
        {
            get
            {
                return _lopTimesheethrs;
            }

            set
            {
                _lopTimesheethrs = value;
            }
        }

        public float Lopleavehrs
        {
            get
            {
                return _lopleavehrs;
            }

            set
            {
                _lopleavehrs = value;
            }
        }

        public DataSet Resultdtset1
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

        public string TaskDate
        {
            get
            {
                return _TaskDate;
            }

            set
            {
                _TaskDate = value;
            }
        }

        #endregion;

    }
}
