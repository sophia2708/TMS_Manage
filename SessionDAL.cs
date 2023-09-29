using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
//using AnalyticBrainsBL;


namespace AnalyticBrainsDAL
{
    public class SessionDAL
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;

        protected void conestablish(string query)
        {
            string db = ConfigurationSettings.AppSettings["dbserver"].ToString();
            string dbname = ConfigurationSettings.AppSettings["dbname"].ToString();
            string intergratedsec = ConfigurationSettings.AppSettings["integratedsecurity"].ToString();
            conn.ConnectionString = "Server=" + db + "; Database=" + dbname + "; " + intergratedsec;
            string userid = ConfigurationSettings.AppSettings["userid"].ToString();
            string password = ConfigurationSettings.AppSettings["password"].ToString();
            conn.ConnectionString = "Server=" + db + "; Database=" + dbname + "; User ID=" + userid + ";Password=" + password;
            conn.Open();
            cmd = new SqlCommand(query, conn);
        }
    }
}
