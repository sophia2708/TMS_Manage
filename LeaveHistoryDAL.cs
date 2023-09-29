using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaveManagementDO;
using AnalyticBrainsDO;
using AB.DataAccessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using AB.DataAccessLayer;
using AB.Utilities;
using System.Collections;

namespace LeaveManagementDAL
{


    public class LeaveHistoryDAL
    {
        LeaveHistoryDO objLeaveHistoryDO = new LeaveHistoryDO();

        SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
        DataTable dt = new DataTable();
        ResultDO retresultdo = new ResultDO();


        //SqlConnection con = null;         

        public LeaveHistoryDO bindgrdLeaveHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();


                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_LeaveHistory");

                objparam[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_LeaveHistory", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }

        public LeaveHistoryDO bindgrdLeavebalance(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();

                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_LeaveBalance");

                objparam[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_LeaveBalance", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }


        public LeaveHistoryDO binddatacalender()
        {
            try
            {
                DataSet ds = new DataSet();
                //objLeaveHistoryDO = new LeaveHistoryDO();

                IDataParameter[] objparam = daHelper.GetSpParameterSet("test_calender");

                //objparam[0].Value = objLeaveHistoryDO.Startdate;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("test_calender");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }

        public LeaveHistoryDO getleavetype(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();

                IDataParameter[] objparam = daHelper.GetSpParameterSet("Sp_LM_getLeavetype");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.LeaveID;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("Sp_LM_getLeavetype", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }

        public LeaveHistoryDO bindgrdLeaveDecision(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();

                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_LEAVEDECISION");
                objparam[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_LEAVEDECISION", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }

        public LeaveHistoryDO InsertLeaveApplication(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("Sp_LM_Leaveapplication");  

                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.Rptmgr;
                objparam[2].Value = objLeaveHistoryDO.Status;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;


                objparam[4].Value = objLeaveHistoryDO.Startdate;
                objparam[5].Value = objLeaveHistoryDO.EndDate;
                objparam[6].Value = objLeaveHistoryDO.Noofdays;
                objparam[7].Value = objLeaveHistoryDO.Frequency;
                objparam[8].Value = objLeaveHistoryDO.Reason;
                objparam[9].Value = objLeaveHistoryDO.Leaveoptions; // ADDED BY SOF 
                objparam[10].Value = objLeaveHistoryDO.CompensateLeavedates;
                objparam[11].Value = objLeaveHistoryDO.PreviousCompensatedates;
               // objparam[12].Value = objLeaveHistoryDO.Previousdates;
                objparam[12].Value = ParameterDirection.Output;



                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("Sp_LM_Leaveapplication", objparam);
                objLeaveHistoryDO.EmpLeaveApplicationId = Convert.ToInt32(objparam[12].Value);  //CHANGES BY SOF

                //To Be Retreived for_Email & not applying leave dates again
                if (Convert.ToInt32(objparam[12].Value) != 0) {
                    IDataParameter[] objparam1 = daHelper.GetSpParameterSet("SP_LM_Email_Imm_Rep_Manager");
                    objparam1[0].Value = objLeaveHistoryDO.EmpId;
                    objparam1[1].Value = objLeaveHistoryDO.Startdate;
                    objparam1[2].Value = objLeaveHistoryDO.EndDate;
                    objparam1[3].Value = objLeaveHistoryDO.LeaveID;
                    objparam1[4].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                    objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("Sp_LM_Email_Imm_Rep_Manager", objparam1);

                
                
                }
                return objLeaveHistoryDO;
               

                //IDataParameter[] objparam2 = daHelper.GetSpParameterSet("LM_Restrict_Leaveapplication");
                //objparam2[0].Value = objLeaveHistoryDO.Startdate;
                //objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_Restrict_Leaveapplication", objparam2);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int ReportMGRLoginCredential(string Username)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();
                int TotalCount = 0;
                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("SP_LM_RPTMGR_Credentials");
                objparam1[0].Value = Username;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_RPTMGR_Credentials", objparam1);
                TotalCount = Convert.ToInt32(objLeaveHistoryDO.Resultdtset.Tables[0].Rows[0][0]);
                return TotalCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EmailReportingManager(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_Email_Imm_Rep_Manager");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.Startdate;
                objparam[2].Value = objLeaveHistoryDO.EndDate;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.EmpLeaveApplicationId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_Email_Imm_Rep_Manager", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //to be retrieved
        public void LeaveDecisionMail(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_decisionEmail");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[2].Value = objLeaveHistoryDO.LeaveID;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_decisionEmail", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public LeaveHistoryDO Reportmanagername(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("Sp_LM_Show_RPRmgr");
                objparam[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("Sp_LM_Show_RPRmgr", objparam);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }

        public void StatusApprove(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_UpdateStatus]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.YearId;
                objparam[5].Value = objLeaveHistoryDO.Comments;
                //objparam[6].Value = objLeaveHistoryDO.StatusComments;
                objparam[6].Value = objLeaveHistoryDO.Statuscode;
                //objparam[7].Value = objLeaveHistoryDO.Leaveoptions;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_UpdateStatus]", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public void StatusUpdatecurrentblc(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_UpdateLeavebalances]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.YearId;
                objparam[5].Value = objLeaveHistoryDO.Comments;
                //objparam[6].Value = objLeaveHistoryDO.StatusComments;
                objparam[6].Value = objLeaveHistoryDO.Statuscode;
                objparam[7].Value = objLeaveHistoryDO.Leaveoptions;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_UpdateLeavebalances]", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public void StatusApproveprevious(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_previousupdate_currentbalance]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.YearId;
                objparam[5].Value = objLeaveHistoryDO.Comments;
                //objparam[6].Value = objLeaveHistoryDO.StatusComments;
                objparam[6].Value = objLeaveHistoryDO.Statuscode;
                objparam[7].Value = objLeaveHistoryDO.Leaveoptions;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_previousupdate_currentbalance]", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void StatusRejected(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_UpdateStatus]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.YearId;
                objparam[5].Value = objLeaveHistoryDO.Comments;
                objparam[6].Value = objLeaveHistoryDO.Statuscode;
                //objparam[6].Value = objLeaveHistoryDO.StatusComments;
                //objparam[6].Value = objLeaveHistoryDO.StatusComments;


                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_UpdateStatus]", objparam);


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void CancelRejected(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {

                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_CancelReject]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.Comments;




                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_CancelReject]", objparam);


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public void CancelOnhold(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {

                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_CancelOnhold]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                //objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[1].Value = objLeaveHistoryDO.EmpId;
                objparam[2].Value = objLeaveHistoryDO.LeaveID;
                objparam[3].Value = objLeaveHistoryDO.Comments;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_CancelOnhold]", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void StatusOnHold(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_UpdateStatus]");

                objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                objparam[4].Value = objLeaveHistoryDO.YearId;
                objparam[5].Value = objLeaveHistoryDO.Comments;
                objparam[6].Value = objLeaveHistoryDO.Statuscode;
                // objparam[6].Value = objLeaveHistoryDO.StatusComments;


                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_UpdateStatus]", objparam);


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void LeaveCancel(LeaveHistoryDO objLeaveHistoryDO, int Result)
        {
            try
            {
                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("[dbo].[SP_LM_CANCELEMAIL]");
                objparam1[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_CANCELEMAIL]", objparam1);
                if (Result == 1)
                {

                    IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[SP_LM_CANCELLEAVE]");
                    objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;

                    objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[Sp_LM_CANCELLEAVE]", objparam);

                }



                //IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[LM_CANCELBACKDATED]");
                //objparam[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;

                //objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[LM_CANCELBACKDATED]", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void BackdatedLeaveCancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("[dbo].[SP_LM_CANCELBACKDATED]");
                objparam1[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_CANCELBACKDATED]", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveHistoryDO getvalue_LeaveCancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("[dbo].[SP_LM_CHECKCANCELSTATUS]");
                objparam1[0].Value = objLeaveHistoryDO.EmpLeaveApplicationId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[SP_LM_CHECKCANCELSTATUS]", objparam1);

            }

            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }

        //Permission
        public LeaveHistoryDO bindgrdPermissionHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {



                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_PermissionHistory");

                objparam[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_PermissionHistory", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }
        public LeaveHistoryDO bindRequestpayslip(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {



                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_ShowMonthandYear");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.MonthandYear;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_ShowMonthandYear", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }
        public LeaveHistoryDO bindRequestfeedback(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {



                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_Feedback");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.Reason;
                objparam[2].Value = objLeaveHistoryDO.Ananymous;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_Feedback", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }


        public void InsertPermissionApplication(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_PermissionApply");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.Rptmgr;
                objparam[2].Value = objLeaveHistoryDO.Startdate;
                objparam[3].Value = objLeaveHistoryDO.EndDate;
                objparam[4].Value = objLeaveHistoryDO.StartTime;
                objparam[5].Value = objLeaveHistoryDO.EndTime;
                objparam[6].Value = objLeaveHistoryDO.Noofdays;
                objparam[7].Value = objLeaveHistoryDO.Duration;
                objparam[8].Value = objLeaveHistoryDO.Status;
                objparam[9].Value = objLeaveHistoryDO.PermissionID;
                objparam[10].Value = objLeaveHistoryDO.Reason;
                objparam[11].Value = ParameterDirection.Output;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_PermissionApply", objparam);
                objLeaveHistoryDO.PermissionApplicationID = Convert.ToInt32(objparam[11].Value);


                //IDataParameter[] objparam1 = daHelper.GetSpParameterSet("LM_PM_PermissionApplicationEmail");
                //objparam1[0].Value = objLeaveHistoryDO.EmpId;
                //objparam1[1].Value = objLeaveHistoryDO.Startdate;
                //objparam1[2].Value = objLeaveHistoryDO.EndDate;
                //objparam1[3].Value = objLeaveHistoryDO.PermissionID;
                //objparam1[4].Value = objLeaveHistoryDO.PermissionApplicationID;
                ////objparam1[5].Value = objLeaveHistoryDO.EmpLeaveApplicationId;


                //objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_PermissionApplicationEmail", objparam1);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LeaveHistoryDO bindgrdPermissionApprovals(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_PermissionApproval");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_PermissionApproval", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }

        public LeaveHistoryDO CancelPermission_GetStatus(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("LM_PM_ReturnCancelStatus");
                objparam1[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_ReturnCancelStatus", objparam1);
            }
            catch (Exception e)
            {
                throw e;
            }
            return objLeaveHistoryDO;
        }
        public void CancelBackdatedPermission(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_CancelBackdated");
                objparam[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_CancelBackdated", objparam);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void CancelPermission(LeaveHistoryDO objLeaveHistoryDO, int Result)
        {
            try
            {
                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("LM_PM_PermissionCancel");
                objparam1[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_PermissionCancel", objparam1);

                //if (Result == 1)
                //{
                //    IDataParameter[] objparam = daHelper.GetSpParameterSet("PermissionCancel");
                //    objparam[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                //    objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("PermissionCancel", objparam);
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PermissionCancelRejected(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_CancelReject");
                objparam[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.PermissionID;
                objparam[4].Value = objLeaveHistoryDO.Comments;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_CancelReject", objparam);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void PermissionStatusRejected(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_Permission_UpdateStatus");
                objparam[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.PermissionID;
                objparam[4].Value = objLeaveHistoryDO.Comments;
                objparam[5].Value = objLeaveHistoryDO.Statuscode;


                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_Permission_UpdateStatus", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void PermissionStatusApprove(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_Permission_UpdateStatus");

                objparam[0].Value = objLeaveHistoryDO.PermissionApplicationID;
                objparam[1].Value = objLeaveHistoryDO.Status;
                objparam[2].Value = objLeaveHistoryDO.EmpId;
                objparam[3].Value = objLeaveHistoryDO.PermissionID;
                objparam[4].Value = objLeaveHistoryDO.Comments;
                objparam[5].Value = objLeaveHistoryDO.Statuscode;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_Permission_UpdateStatus", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveHistoryDO bindddlSelectEmpName(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {

                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("[LM_List_of_Emp]");
                objparam1[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[LM_List_of_Emp]", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindddlEmpName(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {

                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("[AB_GetEmployeeList]");
                objparam1[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[AB_GetEmployeeList]", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindLeaveblcupdation(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {

                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("AB_Leaveblc_updation");
                objparam1[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("AB_Leaveblc_updation", objparam1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }

        public LeaveHistoryDO bindgrdEmpLeaveHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();


                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_Employee_LeaveHistory");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.EmployeeName;
                objparam[2].Value = objLeaveHistoryDO.Alldate;
                objparam[3].Value = objLeaveHistoryDO.ParticularDate;
                objparam[4].Value = objLeaveHistoryDO.FromChooseDate;
                objparam[5].Value = objLeaveHistoryDO.ToChooseDate;
                objparam[6].Value = objLeaveHistoryDO.LeavesType;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_Employee_LeaveHistory", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }


        public LeaveHistoryDO bindgrdEmpPerHistory(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                //objLeaveHistoryDO = new LeaveHistoryDO();


                IDataParameter[] objparam1 = daHelper.GetSpParameterSet("LM_PM_EmpPermissionHistory");

                objparam1[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_EmpPermissionHistory", objparam1);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;


        }
        public LeaveHistoryDO GetPermissionCount(LeaveHistoryDO objLeaveHistoryDO) //changes added by sophia
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_GetPermissionCount");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.Startdate;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_GetPermissionCount", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }

        public LeaveHistoryDO GetHolidayList(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_PM_HOLIDAYlIST");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.Startdate;
                objparam[2].Value = objLeaveHistoryDO.EndDate;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("LM_PM_HOLIDAYlIST", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindemplopcancel(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_Lop_Bind");

                objparam[0].Value = objLeaveHistoryDO.EmpId;

                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_Lop_Bind", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;

        }
        public void LopStatusApprove(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[dbo].[Sp_LM_Lop_Lopupdate]");

                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objparam[2].Value = objLeaveHistoryDO.Status;
                objparam[3].Value = objLeaveHistoryDO.LeaveID;
                //objparam[4].Value = objLeaveHistoryDO.YearId;
                objparam[4].Value = objLeaveHistoryDO.Comments;
                //objparam[6].Value = objLeaveHistoryDO.Statuscode;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("[dbo].[Sp_LM_Lop_Lopupdate]", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Insertlopcancel(LeaveHistoryDO objLeaveHistoryDO) // Added by LOGESH
        {
            IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_LOP_Cancel_Request");
            objparam[0].Value = objLeaveHistoryDO.EmpId;
            objparam[1].Value = objLeaveHistoryDO.LopStartdate;
            objparam[2].Value = objLeaveHistoryDO.LopEndDate;
            objparam[3].Value = objLeaveHistoryDO.LopAction;
            objparam[4].Value = objLeaveHistoryDO.Status;
            objparam[5].Value = objLeaveHistoryDO.LopTimesheethrs;
            objparam[6].Value = objLeaveHistoryDO.Lopleavehrs;
            objparam[7].Value = objLeaveHistoryDO.LeaveID;
            objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_LOP_Cancel_Request", objparam);
        }
        public LeaveHistoryDO GetTimeSheetHours(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_GetTimesheetHours");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.TaskDate;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_GetTimesheetHours", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO SP_LM_LOP_TimesheetHrs(LeaveHistoryDO objLeaveHistoryDO)//ADDED BY SOPHIA
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_LM_LOP_TimesheetHrs");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objparam[1].Value = objLeaveHistoryDO.TaskDate;
                objparam[2].Value = objLeaveHistoryDO.EmpLeaveApplicationId;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("SP_LM_LOP_TimesheetHrs", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO bindgrdExpeselist(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetExpensesList");
                objparam[0].Value = objLeaveHistoryDO.EmpID;
                objparam[1].Value = objLeaveHistoryDO.FromExpensedate;
                //objparam[2].Value = objLeaveHistoryDO.ToExpensedate;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("AB_GetExpensesList", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public LeaveHistoryDO ExpenseslistSaveDetail(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetExpensesListtable");
                objparam[0].Value = objLeaveHistoryDO.EmpID;
                objparam[1].Value = objLeaveHistoryDO.Fromdate;
                objparam[2].Value = objLeaveHistoryDO.ToStartdate;
                objLeaveHistoryDO.Resultdtset = daHelper.ExecuteDataset("AB_GetExpensesListtable", objparam);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO;
        }
        public void LeaveUpdateSaveDetail(LeaveHistoryDO objLeaveHistoryDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_LeaveUpdate_Savedetails");
                objparam[0].Value = objLeaveHistoryDO.OpenLeave;
                objparam[1].Value = objLeaveHistoryDO.EarnLeave;
                objparam[2].Value = objLeaveHistoryDO.AvailedLeave;
                objparam[3].Value = objLeaveHistoryDO.CurrentBalance;
                objparam[4].Value = objLeaveHistoryDO.Lossofpay;
                objparam[5].Value = objLeaveHistoryDO.EmpId;
                objparam[6].Value = objLeaveHistoryDO.EmptransactId;
                daHelper.ExecuteDataset("AB_LeaveUpdate_Savedetails", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public LeaveHistoryDO bindddlcompensationholidaylist(LeaveHistoryDO objLeaveHistoryDO) //ADDED BY SOPHIA
        {
            LeaveHistoryDO objLeaveHistoryDO_ = new LeaveHistoryDO();

            SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
            DataTable dt = new DataTable();
            try
            {
                

                IDataParameter[] objparam = daHelper.GetSpParameterSet("sp_get_lmholiday_list");
                objparam[0].Value = objLeaveHistoryDO.EmpId;
                objLeaveHistoryDO_.Resultdtset = daHelper.ExecuteDataset("sp_get_lmholiday_list", objparam);
               


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objLeaveHistoryDO_;
        }
    }
}


 
