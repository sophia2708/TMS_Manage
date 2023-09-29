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
    public class EmployeeDAL
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        
        DataTable dt;
        //DataSet ds;
       

        #region Declaration
       // ResultDO retresultdo;
        ResultDO retresultdo = new ResultDO();
        SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
        IDataReader reader = null;
        SqlDataReader rdr1;
        ArrayList resarraylist = new ArrayList();

        #endregion
        /// <summary>
        /// 
        /// </summary> SqlDataReader rdr1
        ///

        
        public int LoginValidation(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_LoginValidation");
                objparam[0].Value = ObjEmployeeDO.Username;
                objparam[1].Value = ObjEmployeeDO.Password;
                objparam[2].Direction = ParameterDirection.Output;
                objparam[2].Value = "";
                objparam[3].Direction = ParameterDirection.Output;
                objparam[3].Value = "";
                objparam[4].Direction = ParameterDirection.Output;
                objparam[4].Value = "";
                objparam[5].Direction = ParameterDirection.Output;
                objparam[5].Value = "";
                daHelper.ExecuteDataset("AB_LoginValidation", objparam);

                int result = Convert.ToInt16(objparam[2].Value.ToString());
               
                if (result == 1)
                {
                    ObjEmployeeDO.EmpID = Convert.ToInt16(objparam[4].Value.ToString());
                    ObjEmployeeDO.sessionid = objparam[3].Value.ToString();
                    ObjEmployeeDO.FirstName = objparam[5].Value.ToString(); 
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int CreateNewUser(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_SaveNewEmployeeDetail");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                objparam[1].Value = ObjEmployeeDO.FirstName;
                objparam[2].Value = ObjEmployeeDO.Lastname;
                objparam[3].Value = ObjEmployeeDO.Dateofbirth;
                objparam[4].Value = ObjEmployeeDO.Phonenumber;
                objparam[5].Value = ObjEmployeeDO.Gender;
                objparam[6].Value = ObjEmployeeDO.Emailid;
                objparam[7].Value = ObjEmployeeDO.Address;
                objparam[8].Value = ObjEmployeeDO.Dateofjoin; 
                objparam[9].Value = ObjEmployeeDO.Username;
                objparam[10].Value = ObjEmployeeDO.Password;
                objparam[11].Value = ObjEmployeeDO.Forgetpwques;
                objparam[12].Value = ObjEmployeeDO.Forgetpwans;
                objparam[13].Direction = ParameterDirection.Output;
                objparam[13].Value = "";
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_SaveNewEmployeeDetail", objparam);
                int result = Convert.ToInt16(objparam[13].Value.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ViewEmpProfile(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ViewEmpProfile");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                IDataReader rdr1 = daHelper.ExecuteReader("AB_ViewEmpProfile", objparam);

                while (rdr1.Read())
                {
                    ObjEmployeeDO.FirstName = rdr1["FirstName"].ToString();
                    ObjEmployeeDO.Lastname = rdr1["LastName"].ToString();
                    ObjEmployeeDO.Dateofbirth = rdr1["DateOfBirth"].ToString();
                    ObjEmployeeDO.Phonenumber = Convert.ToInt64(rdr1["PhoneNumber"]);
                    ObjEmployeeDO.Gender = rdr1["Gender"].ToString();
                    ObjEmployeeDO.Emailid = rdr1["EmailID"].ToString();
                    ObjEmployeeDO.Address = rdr1["Address"].ToString();
                    ObjEmployeeDO.Dateofjoin = rdr1["DateOfJoin"].ToString();
                    ObjEmployeeDO.Username = rdr1["UserName"].ToString();
                    ObjEmployeeDO.Forgetpwques = rdr1["Question"].ToString();
                    ObjEmployeeDO.Forgetpwans = rdr1["Hintans"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ChangePassword(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ChangePassword");
                objparam[0].Value = ObjEmployeeDO.Username;
                objparam[1].Value = ObjEmployeeDO.Password;
                objparam[2].Value = ObjEmployeeDO.Newpassword;
                objparam[3].Direction = ParameterDirection.Output;
                objparam[3].Value = "";
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ChangePassword", objparam);

                int result = Convert.ToInt16(objparam[3].Value.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int SessionValidation(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_SessionValidation");
                objparam[0].Value = ObjEmployeeDO.sessionid;
                objparam[1].Direction = ParameterDirection.Output;
                objparam[1].Value = "";
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_SessionValidation", objparam);

                int result = Convert.ToInt32(cmd.Parameters["@retn"].Value);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void logout(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_SessionIDExpire");
                objparam[0].Value = ObjEmployeeDO.sessionid;
                daHelper.ExecuteDataset("AB_SessionIDExpire", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO UserNameValidation(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_UserNameValidation");
                objparam[0].Value = ObjEmployeeDO.Username;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_UserNameValidation", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public int SecAnsValidation(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_SecAnswerValidation");
                objparam[0].Value = ObjEmployeeDO.Username;
                objparam[1].Value = ObjEmployeeDO.Forgetpwans;
                objparam[2].Direction = ParameterDirection.Output;
                objparam[2].Value = "";
                daHelper.ExecuteDataset("AB_SecAnswerValidation", objparam);

                int result = Convert.ToInt16(objparam[2].Value.ToString());
                return result;
            }

              catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void InsertReportingHierarchy(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_Reporting_Hierarchy");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                objparam[1].Value = ObjEmployeeDO.Rptmgr;
                objparam[2].Value = ObjEmployeeDO.Designation;
                objparam[3].Value = ObjEmployeeDO.Add_rpts_to;   //ADDED BY SOF
                retresultdo.Resultdtset = daHelper.ExecuteDataset("SP_LM_Reporting_Hierarchy", objparam);

                //IDataParameter[] objparam1 = daHelper.GetSpParameterSet("LM_PM_NotificationEmail");
                //objparam1[0].Value = ObjEmployeeDO.EmpID;
                //objparam1[1].Value = ObjEmployeeDO.Rptmgr;
                //retresultdo.Resultdtset = daHelper.ExecuteDataset("LM_PM_NotificationEmail", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertReportHybrid(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_Reporting_Hybrid");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                objparam[1].Value = ObjEmployeeDO.WeeklyName;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("SP_Reporting_Hybrid", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public ResultDO bindgrdExpeselist()
        //{

        //    try
        //    {
        //        ResultDO objresultdo = new ResultDO();

        //        objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetExpensesList");
        //        return objresultdo;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
       
        public void ExpensesSaveDetail(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ExpensesaveDetail");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                objparam[1].Value = ObjEmployeeDO.Expensedate;
                objparam[2].Value = ObjEmployeeDO.ExpenseType;
                objparam[3].Value = ObjEmployeeDO.Amount;
                objparam[4].Value = ObjEmployeeDO.PaidName;//ADDED BY SOF
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ExpensesaveDetail", objparam);

            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool ForgotPassword(EmployeeDO ObjEmployeeDO)
        {
            try
            {
               

                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ForgotPassword");
                objparam[0].Value = ObjEmployeeDO.Username;
                objparam[1].Value = ObjEmployeeDO.Password;
                retresultdo.Resultdtset=daHelper.ExecuteDataset("AB_ForgotPassword", objparam);
                                
                if (retresultdo.Resultdtset.Tables[0].Rows.Count > 0)
                {
                   return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO GetSecurityQuestionList()
        {

            try
            {
                ResultDO objresultdo = new ResultDO();

                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_SecurityQuestionList");
                return objresultdo;
                              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO GetBlockedUserList()
        {
            try
            {
                ResultDO objresultdo = new ResultDO();
                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_BlockedUserList");
                return objresultdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO  GetStatusList()
        {
            try
            {
                ResultDO objresultdo = new ResultDO();

                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_StatusList");
                return objresultdo;
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ResetPassword(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ResetPassword");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                objparam[1].Value = ObjEmployeeDO.Password;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ResetPassword", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteEmployee(EmployeeDO ObjEmployeeDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_DeleteEmployee");
                objparam[0].Value = ObjEmployeeDO.EmpID;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_DeleteEmployee", objparam);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
