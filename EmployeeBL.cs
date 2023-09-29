using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
using AnalyticBrainsBL;
//using System.Data;
using System.Data.Sql;


namespace AnalyticBrainsBL
{
    public class EmployeeBL
    {
        EmployeeDAL ObjEmployeeDAL = new EmployeeDAL();

        public int LoginValidation(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.LoginValidation(ObjEmployeeDO);
        }
        public int CreateNewUser(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.CreateNewUser(ObjEmployeeDO);
        }
        public void ViewEmpProfile(EmployeeDO ObjEmployeeDO)
        {
            ObjEmployeeDAL.ViewEmpProfile(ObjEmployeeDO);
        }
        public int ChangePassword(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.ChangePassword(ObjEmployeeDO);
        }
        public int SecAnsValidation(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.SecAnsValidation(ObjEmployeeDO);
        }
        public int SessionValidation(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.SessionValidation(ObjEmployeeDO);
        }
        public void logout(EmployeeDO ObjEmployeeDO)
        {
            ObjEmployeeDAL.logout(ObjEmployeeDO);
        }
        public ResultDO UserNameValidation(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.UserNameValidation(ObjEmployeeDO);
        }
        public bool ForgotPassword(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.ForgotPassword(ObjEmployeeDO);
        }
        public ResultDO GetSecurityQuestionList()
        {
            return ObjEmployeeDAL.GetSecurityQuestionList();
        }
        public ResultDO GetStatusList()
        {
            return ObjEmployeeDAL.GetStatusList();
        }
        public ResultDO GetBlockedUserList()
        {
            return ObjEmployeeDAL.GetBlockedUserList();
        }
        public void ResetPassword(EmployeeDO ObjEmployeeDO)
        {
            ObjEmployeeDAL.ResetPassword(ObjEmployeeDO);
        }
        public bool DeleteEmployee(EmployeeDO ObjEmployeeDO)
        {
            return ObjEmployeeDAL.DeleteEmployee(ObjEmployeeDO);
        }
        public void InsertReportingHierarchy(EmployeeDO ObjEmployeeDO)
        {
            //objLeaveHistoryDO = objLeaveHistoryDAL.InsertLeaveApplication(objLeaveHistoryDO);
            ObjEmployeeDAL.InsertReportingHierarchy(ObjEmployeeDO);

            //return objLeaveHistoryDO;
        }
        public void InsertReportHybrid(EmployeeDO ObjEmployeeDO)
        {
            //objLeaveHistoryDO = objLeaveHistoryDAL.InsertLeaveApplication(objLeaveHistoryDO);
            ObjEmployeeDAL.InsertReportHybrid(ObjEmployeeDO);

            //return objLeaveHistoryDO;
        }
        public void ExpensesSaveDetail(EmployeeDO ObjEmployeeDO)
        {
            ObjEmployeeDAL.ExpensesSaveDetail(ObjEmployeeDO);
        }
        
        //public ResultDO bindgrdExpeselist()
        //{
        //    return ObjEmployeeDAL.bindgrdExpeselist();
        //}


    }
}
