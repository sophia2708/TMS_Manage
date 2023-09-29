using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaveManagementDO;
using LeaveManagementDAL;

namespace LeaveManagementBL
{
    public class LeaveHistoryBL
    {
        LeaveHistoryDO objLeaveHistoryDO = new LeaveHistoryDO();
        LeaveHistoryDAL objLeaveHistoryDAL = new LeaveHistoryDAL();

        public LeaveHistoryDO bindgrdleavehistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdLeaveHistory(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }

        public LeaveHistoryDO binddatacalender()
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.binddatacalender();
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindgrdLeavebalance(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdLeavebalance(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO getleavetype(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.getleavetype(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindgrdLeaveDecision(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdLeaveDecision(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO InsertLeaveApplication(LeaveHistoryDO objLeaveHistoryDO)
        {

            return objLeaveHistoryDAL.InsertLeaveApplication(objLeaveHistoryDO);


        }
        public void EmailReportingManager(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDAL.EmailReportingManager(objLeaveHistoryDO);


        }
        public int ReportMGRLoginCredential(string Username)
        {

            int TotalCount = 0;
            TotalCount = objLeaveHistoryDAL.ReportMGRLoginCredential(Username);
            return TotalCount;

        }
        public LeaveHistoryDO GetReportManagerName(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDO = objLeaveHistoryDAL.Reportmanagername(objLeaveHistoryDO);

            return objLeaveHistoryDO;
        }
        //to be retrieved
        public void LeaveDecisionMail(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDAL.LeaveDecisionMail(objLeaveHistoryDO);

        }
        public void StatusApprove(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.StatusApprove(objLeaveHistoryDO);

        }
        public void StatusUpdatecurrentblc(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            objLeaveHistoryDAL.StatusUpdatecurrentblc(objLeaveHistoryDO);

        }
        public void StatusApproveprevious(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            objLeaveHistoryDAL.StatusApproveprevious(objLeaveHistoryDO);

        }
        public void StatusRejected(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDAL.StatusRejected(objLeaveHistoryDO);

        }
        public void CancelRejected(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDAL.CancelRejected(objLeaveHistoryDO);

        }

        public void CancelOnhold(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDAL.CancelOnhold(objLeaveHistoryDO);

        }
        public void StatusOnHold(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDAL.StatusOnHold(objLeaveHistoryDO);

        }
        public void LeaveCancel(LeaveHistoryDO objLeaveHistoryDO, int result)
        {
            objLeaveHistoryDAL.LeaveCancel(objLeaveHistoryDO, result);
        }
        public void BackdatedLeaveCancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.BackdatedLeaveCancel(objLeaveHistoryDO);

        }
        public LeaveHistoryDO getvalue_LeaveCancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.getvalue_LeaveCancel(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }

        //Permission History
        public LeaveHistoryDO bindgrdPermissionHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdPermissionHistory(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }

        public LeaveHistoryDO bindRequestpayslip(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindRequestpayslip(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindRequestfeedback(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindRequestfeedback(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public void InsertPermissionApplication(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.InsertPermissionApplication(objLeaveHistoryDO);
        }

       
        public LeaveHistoryDO bindgrdPermissionApprovals(LeaveHistoryDO objLeaveHistoryDO)
        {

            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdPermissionApprovals(objLeaveHistoryDO);

            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO CancelPermission_GetStatus(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.CancelPermission_GetStatus(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public void CancelBackdatedPermission(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.CancelBackdatedPermission(objLeaveHistoryDO);
        }
        public void CancelPermission(LeaveHistoryDO objLeaveHistoryDO,int result)
        {
            objLeaveHistoryDAL.CancelPermission(objLeaveHistoryDO, result);
        }
        public void PermissionCancelRejected(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.PermissionCancelRejected(objLeaveHistoryDO);
        }
        public void PermissionStatusRejected(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.PermissionStatusRejected(objLeaveHistoryDO);
        }
        public void PermissionStatusApprove(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.PermissionStatusApprove(objLeaveHistoryDO);
        }
        public LeaveHistoryDO bindddlSelectEmpName(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.bindddlSelectEmpName(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindddlEmpName(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.bindddlEmpName(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindLeaveblcupdation(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.bindLeaveblcupdation(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindgrdEmpLeaveHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdEmpLeaveHistory(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindgrdEmpPerHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindgrdEmpPerHistory(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO GetPermissionCount(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.GetPermissionCount(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO GetHolidayList(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.GetHolidayList(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindemplopcancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.bindemplopcancel(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public void Insertlopcancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.Insertlopcancel(objLeaveHistoryDO);
        }
        public LeaveHistoryDO GetTimeSheetHours(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.GetTimeSheetHours(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO SP_LM_LOP_TimesheetHrs(LeaveHistoryDO objLeaveHistoryDO)//Added by SOPHIA
        {
            objLeaveHistoryDO = objLeaveHistoryDAL.SP_LM_LOP_TimesheetHrs(objLeaveHistoryDO);
            return objLeaveHistoryDO;
        }
        public void LopStatusApprove(LeaveHistoryDO objLeaveHistoryDO) //Added by LOGEHS
        {
            objLeaveHistoryDAL.LopStatusApprove(objLeaveHistoryDO);
        }

     
        public LeaveHistoryDO bindddlcompensationholidaylist(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            return objLeaveHistoryDAL.bindddlcompensationholidaylist(objLeaveHistoryDO);
        }
       
        public LeaveHistoryDO bindgrdExpeselist(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            return objLeaveHistoryDAL.bindgrdExpeselist(objLeaveHistoryDO);
        }
        public LeaveHistoryDO ExpenseslistSaveDetail(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            return objLeaveHistoryDAL.ExpenseslistSaveDetail(objLeaveHistoryDO);
        }
        public void LeaveUpdateSaveDetail(LeaveHistoryDO objLeaveHistoryDO)
        {
            objLeaveHistoryDAL.LeaveUpdateSaveDetail(objLeaveHistoryDO);
        }

    }
}

