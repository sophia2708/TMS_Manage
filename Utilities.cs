using System;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Security.Cryptography;


namespace AB.Utilities
{
    public class Utilities
    {
        #region Declarations

        private static string connectionString = "";
        private static string ServerName = "";
        private static string DatabaseName = "";
        private static string integratedSecurity = "";

        #endregion

        #region getValMessageforConnection

        /// <summary>
        /// <name>getValMessageforConnection</name>
        /// <purpose>To return connection string</purpose>
        /// </summary>
        /// <param name="pKey"></param>
        /// <returns></returns>

        public static string getValMessageforConnection(string pKey)
        {
            ServerName = ConfigurationSettings.AppSettings["server"];
            DatabaseName = ConfigurationSettings.AppSettings["database"];
            integratedSecurity = ConfigurationSettings.AppSettings["ConnectionString"];

            connectionString = "Data Source = " + ServerName
                 + ";Initial Catalog=" + DatabaseName
                 + ";" + integratedSecurity;

            return connectionString;
        }

        /* This function returns the connection string */

        #endregion
    }
}
