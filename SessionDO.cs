using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyticBrainsDO
{
    public class SessionDO
    {
        #region Declarations
        private string _loginusername;
        private string _loginfirstname;
        private string _loginid;
        private string _autoempid;
        private string _sessionid;


        #endregion

        #region Public Properties


        public string LoginUsername
        {
            get
            {
                return _loginusername;
            }
            set
            {
                _loginusername = value;
            }
        }
        public string loginfirstname
        {
            get
            {
                return _loginfirstname;
            }
            set
            {
                _loginfirstname = value;
            }
        }

        public string Loginid
        {
            get
            {
                return _loginid;
            }
            set
            {
                _loginid = value;
            }
        }
        public string Autoempid
        {
            get
            {
                return _autoempid;
            }
            set
            {
                _autoempid = value;
            }
        }

        public string Sessionid
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

          #endregion
    }
}
    
