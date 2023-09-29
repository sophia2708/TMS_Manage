using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyticBrainsDO
{
    public class EmployeeDO
    {
        #region Declarations
        private string _firstname;
        private int _empid;
        private string _gender;
        private string _lastname;
        private long   _phonenumber;
        private string _emailid;
        private string _address;
        private string _username;
        private string _password;
        private string _confirmpassword;
        private string _Forgetpwques;
        private string _forgetpwans;
        private string _dateofbirth;
        private string _dateofjoin;
        private string _newpassword;
        private string _sessionid;
        private DateTime _Taskdate;
        private int _maxInvalidPasswordAttempts;
        private int _intCounter;
        private string _taskid;
        private int _rptmgr;
        private int _Designation;
        private string _Add_rpts_to;
        private DateTime _Expensedate;
        private string _ExpenseType;
        private string _Amount;
        private string _PaidName;//ADDED BY SOPHIA
        private string _WeeklyName;

        
        //private int _result;      

        #endregion

        #region Public Properties
        public string Taskid
        {
            get
            {
                return _taskid;
            }
            set
            {
                _taskid = value;
            }
        }
        public int intCounter
        {
            get 
            {
                return _intCounter;
            }
            set 
            {
                _intCounter = value;
            }
        }
        public   int MaxInvalidPasswordAttempts
        {
            get
            {
                return _maxInvalidPasswordAttempts;
            }
            set 
            {
                _maxInvalidPasswordAttempts = value;
            }

        }
        public string FirstName
        {
            get
            { 
                return _firstname; 
            }
            set
            {
                _firstname = value;
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
        public string Gender
        {
            get
            { 
                return _gender; 
            }
            set
            {
                _gender = value;
            }
        }
        public string Lastname
        {
            get
            { 
                return _lastname; 
            }
            set
            {
                _lastname = value;
            }
        }
        public long Phonenumber
        {
            get
            { 
                return _phonenumber; 
            }
            set
            {
                _phonenumber = value;
            }
        }
        public string Emailid
        {
            get
            { 
                return _emailid; 
            }
            set
            {
                _emailid = value;
            }
        }
        public string Address
        {
            get
            { 
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public string Username
        {
            get
            {
                return _username; 
            }
            set
            {
                _username = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public string Confirmpassword
        {
            get
            {
                return _confirmpassword;
            }
            set
            {
                _confirmpassword = value;
            }
        }
        public string Forgetpwques
        {
            get
            {
                return _Forgetpwques;
            }
            set
            {
                _Forgetpwques = value;
            }
        }
        public string Forgetpwans
        {
            get
            {
                return _forgetpwans;
            }
            set
            {
                _forgetpwans = value;
            }
        }
        public string Dateofbirth
        {
            get
            {
                return _dateofbirth;
            }
            set
            {
                _dateofbirth = value;
            }
        }
        public string Dateofjoin
        {
            get
            {
                return _dateofjoin;
            }
            set
            {
                _dateofjoin = value;
            }
        }
        public string Newpassword
        {
            get
            {
                return _newpassword;
            }
            set
            {
            _newpassword=value;
            }
        }
        public string sessionid
        {
            get
            {
                return _sessionid;
            }
            set
            {
                _sessionid = value;
            }
        }
        public DateTime Taskdate
        {
            get
            {
                return _Taskdate;
            }
            set
            {
                _Taskdate = value;
            }
        }
        public int Rptmgr
        {
            get
            {
                return _rptmgr;
            }
            set
            {
                _rptmgr = value;
            }
        }
        public int Designation
        {
            get
            {
                return _Designation;
            }
            set
            {
                _Designation = value;
            }
        }
        public string Add_rpts_to        //ADDED BY SOPHIA
          {
            get
            {
                return _Add_rpts_to;
            }
            set
            {
                _Add_rpts_to = value;
            }
        }
        public string WeeklyName        //ADDED BY SOPHIA
        {
            get
            {
                return _WeeklyName;
            }
            set
            {
                _WeeklyName = value;
            }
        }
        public DateTime Expensedate       //ADDED BY SOPHIA
        {
            get
            {
                return _Expensedate;
            }
            set
            {
                _Expensedate = value;
            }
        }
        
        public string ExpenseType       //ADDED BY SOPHIA
        {
            get
            {
                return _ExpenseType;
            }
            set
            {
                _ExpenseType = value;
            }
        }
        public string PaidName       //ADDED BY SOPHIA
        {
            get
            {
                return _PaidName;
            }
            set
            {
                _PaidName = value;
            }
        }
        public string Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                _Amount = value;
            }
        }
        #endregion
    }
}
