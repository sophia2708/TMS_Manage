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
    public class TimesheetDAL
    {
        #region Declaration

        ResultDO retresultdo = new ResultDO();
        SQLDataAccessHelper daHelper = new SQLDataAccessHelper();
        IDataReader reader = null;
        ArrayList myArrlst = new ArrayList();
        #endregion
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd;
        SqlDataAdapter da;
        // DataTable dt;
        DataSet ds;
        //  ResultDO retresultdo;
        CultureInfo provider = CultureInfo.InvariantCulture;

        public ResultDO EmployeeTaskList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmployeeTaskList");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmployeeTaskList", objparam);
                return retresultdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                retresultdo = null;
            }
        }
        public ResultDO ClientId(TimesheetDO ObjTimesheetDO)
        {
            try
            {
            retresultdo = new ResultDO();
            IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_PopclientID");
            objparam[0].Value = ObjTimesheetDO.ProjectId;
            retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_PopclientID", objparam);
            return retresultdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                retresultdo = null;
            }
        }

        public ResultDO EmpProjectList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmployeeProjectList");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmployeeProjectList", objparam);
                return retresultdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                retresultdo = null;
            }
        }
        public int SendMail(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_sendmail");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.ToDate;
                objparam[2].Direction = ParameterDirection.Output;
                //objparam[2].Value = "";
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_sendmail", objparam);
                //cmd.ExecuteNonQuery();
                int result = int.Parse(objparam[2].Value.ToString());
                // conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTimeSheet(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_DeleteTimeSheet");
                objparam[0].Value = ObjTimesheetDO.RowId;
                daHelper.ExecuteDataset("AB_DeleteTimeSheet", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEnterTimeSheet(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_DeleteTest");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.RowId;
                objparam[2].Value = ObjTimesheetDO.FromDate;
                objparam[3].Direction = ParameterDirection.Output;
                daHelper.ExecuteDataset("AB_DeleteTest", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void DeleteRemindersheet(TimesheetDO ObjTimesheetDO)
        //{
        //    try
        //    {
        //        retresultdo = new ResultDO();
        //        IDataParameter[] objparam = daHelper.GetSpParameterSet("SP_DELETE_REMINDERLIST");
        //        objparam[0].Value = ObjTimesheetDO.EmpId;
        //        daHelper.ExecuteDataset("SP_DELETE_REMINDERLIST", objparam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void UpdateTimesheet(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_UpdateTimesheetDetail");
                objparam[0].Value = ObjTimesheetDO.ToDate;
                objparam[1].Value = ObjTimesheetDO.ProjectId;
                objparam[2].Value = ObjTimesheetDO.TaskId;
                objparam[3].Value = ObjTimesheetDO.Taskdesc;
                objparam[4].Value = ObjTimesheetDO.Issues;
                objparam[5].Value = ObjTimesheetDO.Object;
                objparam[6].Value = ObjTimesheetDO.Status;
                objparam[7].Value = ObjTimesheetDO.Hours;
                objparam[8].Value = ObjTimesheetDO.EmpId;
                objparam[9].Value = ObjTimesheetDO.RowId;
                daHelper.ExecuteDataset("AB_UpdateTimesheetDetail", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SaveTimesheetDetail(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_SaveTimesheetDetail");
                objparam[0].Value = ObjTimesheetDO.ToDate;
                objparam[1].Value = ObjTimesheetDO.ModeofWork;
                objparam[2].Value = ObjTimesheetDO.ProjectId;
                objparam[3].Value = ObjTimesheetDO.TaskId;
                objparam[4].Value = ObjTimesheetDO.Taskdesc;
                objparam[5].Value = ObjTimesheetDO.Issues;
                objparam[6].Value = ObjTimesheetDO.Object;
                objparam[7].Value = ObjTimesheetDO.Status;
                objparam[8].Value = ObjTimesheetDO.Hours;
                //objparam[8].Value = ObjTimesheetDO.ModeofWork;
                objparam[9].Value = ObjTimesheetDO.EmpId;
                objparam[10].Value = ObjTimesheetDO.RowNum;
                objparam[11].Value = ObjTimesheetDO.ModuleId;
                daHelper.ExecuteDataset("AB_SaveTimesheetDetail", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void ReminderSaveDetail(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_RemindersaveDetail");
                objparam[0].Value = ObjTimesheetDO.TaskNameReminder;
                objparam[1].Value = ObjTimesheetDO.TaskFrequency;
                objparam[2].Value = ObjTimesheetDO.Dateofreminder;
                objparam[3].Value = ObjTimesheetDO.PeopleToBeReminded;
                objparam[4].Value = ObjTimesheetDO.EmpId;
                objparam[5].Value = ObjTimesheetDO.id;
                objparam[6].Value = ObjTimesheetDO.Biweekly;
                objparam[7].Value = ObjTimesheetDO.BiMonthly;
                objparam[8].Value = ObjTimesheetDO.Weekly;
                objparam[9].Value = ObjTimesheetDO.Monthly;
                objparam[10].Value = ObjTimesheetDO.Yearly;
                objparam[11].Value = ObjTimesheetDO.Yearly1;                
                //objparam[5].Value = ObjTimesheetDO.RowNum;
                 daHelper.ExecuteDataset("AB_RemindersaveDetail", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ResultDO ViewEmpTimesheet1(TimesheetDO ObjTimesheetDO)// Hold item need to display on fresh page  itself - modified sp

        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ViewTimesheet_hold");
                objparam[0].Value = ObjTimesheetDO.Type;
                objparam[1].Value = ObjTimesheetDO.EmpId;
                objparam[2].Value = ObjTimesheetDO.FromDate;
                objparam[3].Value = ObjTimesheetDO.ToDate;
                objparam[4].Value = ObjTimesheetDO.Month;
                objparam[5].Value = ObjTimesheetDO.Year;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ViewTimesheet_hold", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO GetWorkmodeStatus(TimesheetDO ObjTimesheetDO)// Hold item need to display on fresh page  itself - modified sp
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetWorkmode");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.FromDate;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetWorkmode", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public ResultDO Inprogressgridviewtimesheet(TimesheetDO ObjTimesheetDO)
        {
            retresultdo = new ResultDO();
            IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_InprogressMergegrid");
            objparam[0].Value = ObjTimesheetDO.EmpId;
            retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_InprogressMergegrid", objparam);
            return retresultdo;

        }

        public ResultDO InprogressEmpTimesheet(TimesheetDO ObjTimesheetDO)// show inprogress items in grid
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_Inprogressgrid");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.FromDate;
            
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Inprogressgrid", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public ResultDO ViewEmpTimesheet(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("[AB_ViewTimesheet_New]");
                objparam[0].Value = ObjTimesheetDO.Type;
                objparam[1].Value = ObjTimesheetDO.EmpId;
                objparam[2].Value = ObjTimesheetDO.FromDate;
                objparam[3].Value = ObjTimesheetDO.ToDate;
                objparam[4].Value = ObjTimesheetDO.Month;
                objparam[5].Value = ObjTimesheetDO.Year;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("[AB_ViewTimesheet_New]", objparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO WeeklyStatus(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_WeeklyStatus");
                objparam[0].Value = ObjTimesheetDO.Type;
                objparam[1].Value = ObjTimesheetDO.EmpId;
                objparam[2].Value = ObjTimesheetDO.FromDate;
                objparam[3].Value = ObjTimesheetDO.ToDate;
                objparam[4].Value = ObjTimesheetDO.Month;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_WeeklyStatus", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO ApprovalTimesheet(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_WorkHoursApproval");
                
                objparam[0].Value = ObjTimesheetDO.EmpId;
                
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_WorkHoursApproval", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public int Approval(TimesheetDO ObjTimesheetDO)
        {
            int RtnId = 0;
            try
            {
                
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("Ab_MgrApproval_Status");
                
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.Status;
                objparam[2].Value = ObjTimesheetDO.FromDate;
                objparam[3].Value = ObjTimesheetDO.ToDate;
                

                retresultdo.Resultdtset = daHelper.ExecuteDataset("Ab_MgrApproval_Status", objparam);
                RtnId = Convert.ToInt32(retresultdo.Resultdtset.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RtnId;
        }

        public ResultDO Approval_mail(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("Ab_ApproveTimesheet_mail");

                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.TaskName;
                
                retresultdo.Resultdtset = daHelper.ExecuteDataset("Ab_ApproveTimesheet_mail", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }





        public ResultDO GetProjectList()
        {
            try
            {
                ResultDO objresultdo = new ResultDO();
                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ProjectList");
                return objresultdo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO GetProjectdropdown(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                ResultDO objresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ProjectddList");
                objparam[0].Value = ObjTimesheetDO.Clients;
                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ProjectddList", objparam);
                return objresultdo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddNewModule(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewModule");
                objparam[0].Value = ObjTimesheetDO.ModuleName;
                objparam[1].Value = ObjTimesheetDO.ProjectId;

                daHelper.ExecuteDataset("AB_AddNewModule", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddNewTask(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewTask");
                objparam[0].Value = ObjTimesheetDO.TaskName;
                objparam[1].Value = ObjTimesheetDO.Taskdesc;
                objparam[2].Value = ObjTimesheetDO.ModuleId;
                objparam[3].Value = ObjTimesheetDO.ProjectId;
                daHelper.ExecuteDataset("AB_AddNewTask", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddNewProject(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewProject");
                objparam[0].Value = ObjTimesheetDO.ProjectName;
                daHelper.ExecuteDataset("AB_AddNewProject", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddNewProjectList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewProjectList");
                objparam[0].Value = ObjTimesheetDO.ProjectName;
                objparam[1].Value = ObjTimesheetDO.Clients;
                objparam[2].Direction = ParameterDirection.Output;
                daHelper.ExecuteNonQuery("AB_AddNewProjectList", objparam);
                retresultdo.UpdateResult = int.Parse(objparam[2].Value.ToString());
                return retresultdo.UpdateResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ResultDO GetEmployeeList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmployeeList");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmployeeList", objparam);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO Gethybridvalues(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_Hybridselected_options");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Hybridselected_options", objparam);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public ResultDO GetActiveemployeelist(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetEmployeeList");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetEmployeeList", objparam);
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
                                             
        public ResultDO GetModuleList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_ModuleList");
                //objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[0].Value = ObjTimesheetDO.ProjectId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_ModuleList", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public int AddNewModuleList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewModuleList");
                objparam[0].Value = ObjTimesheetDO.ModuleName;
                objparam[1].Value = ObjTimesheetDO.ProjectId;
                objparam[2].Direction = ParameterDirection.Output;
                daHelper.ExecuteNonQuery("AB_AddNewModuleList", objparam);
                retresultdo.UpdateResult = int.Parse(objparam[2].Value.ToString());
                return retresultdo.UpdateResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int AddNewTaskList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewTaskList");
                objparam[0].Value = ObjTimesheetDO.Status;
                objparam[1].Value = ObjTimesheetDO.TaskName;
                objparam[2].Value = ObjTimesheetDO.Taskdesc;
                objparam[3].Value = ObjTimesheetDO.ModuleId;
                objparam[4].Value = ObjTimesheetDO.ProjectId;
                objparam[5].Value = ObjTimesheetDO.plannedstartdtask;
                objparam[6].Value = ObjTimesheetDO.plannedenddate;
                objparam[7].Value = ObjTimesheetDO.Clients;
                objparam[8].Value = ObjTimesheetDO.TaskId;
                objparam[9].Direction = ParameterDirection.Output;
                daHelper.ExecuteNonQuery("AB_AddNewTaskList", objparam);
                retresultdo.UpdateResult = int.Parse(objparam[9].Value.ToString());
                return retresultdo.UpdateResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ResultDO GetTaskList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_TaskList");
                objparam[0].Value = ObjTimesheetDO.ProjectId;
                objparam[1].Value = ObjTimesheetDO.ModuleId;
                objparam[2].Value = ObjTimesheetDO.inputvalue;
                objparam[3].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_TaskList", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO GetPriorityList()
        {
            try
            {
               ResultDO objresultdo = new ResultDO();
               objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Priority");
               return objresultdo;
            }
            catch (Exception ex)
            {
               throw ex;
            }
        }
        public int SaveEmployeeTask(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_SaveEmpParent");
                objparam[0].Value = ObjTimesheetDO.TaskId;
                objparam[1].Value = ObjTimesheetDO.EmpId;
                objparam[2].Value = ObjTimesheetDO.RowNum;
                objparam[3].Value = ObjTimesheetDO.Priority;
                objparam[4].Value = ObjTimesheetDO.ToDate;
                objparam[5].Value = ObjTimesheetDO.Clients;
                objparam[6].Value = ObjTimesheetDO.Taskcat;
                objparam[7].Value = ObjTimesheetDO.Plannedstartdate;
                objparam[8].Value = ObjTimesheetDO.Plannedeffort;
                objparam[9].Value = ObjTimesheetDO.Actualstartdate;
                objparam[10].Value = ObjTimesheetDO.Actualeffort;
                objparam[11].Value = ObjTimesheetDO.FTR;
                objparam[12].Value = ObjTimesheetDO.OTD;
                objparam[13].Value = ObjTimesheetDO.Status;
                objparam[14].Value = ObjTimesheetDO.Parentid;
                objparam[15].Value = ObjTimesheetDO.EmpTaskid;
                objparam[16].Value = ObjTimesheetDO.RowId;
                objparam[17].Direction = ParameterDirection.Output;
                daHelper.ExecuteDataset("AB_SaveEmpParent", objparam);
                int result = int.Parse(objparam[17].Value.ToString());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultDO GetclientBoard(TimesheetDO ObjTimesheetDO)
        {
            try
            {

                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_Clientgrid");

                objparam[0].Value = ObjTimesheetDO.EmpId;

                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Clientgrid", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public ResultDO GetEmpDashBoard(TimesheetDO ObjTimesheetDO)
        {

            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmpDashBoard");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmpDashBoard", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO GetEmpTaskSummary(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();


                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_Employee_Tasksummary");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.inputvalue;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Employee_Tasksummary", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }


        public ResultDO GetEmpTaskEdit(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_Task_Edit");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.TaskName;
                objparam[2].Value = ObjTimesheetDO.RowId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Task_Edit", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        //  public ResultDO Getdashdeatils()
        //{
        //    try
        //    {
        //       retresultdo = new ResultDO();
        //       IDataParameter[] objparam = daHelper.GetSpParameterSet("Ab_Task_Edit");
        //       objparam[0].Value = ObjTimesheetDO.EmpId;
        //       objparam[1].Value = ObjTimesheetDO.TaskId;
        //       retresultdo.Resultdtset = daHelper.ExecuteDataset("Ab_Task_Edit", objparam);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return retresultdo;
        //}

        public void UpdateEmployeeStatus(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_UpdateUserStatus");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.Status;
                daHelper.ExecuteDataset("AB_UpdateUserStatus", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// getting years list to view time sheet by month
        /// </summary>
        /// <returns></returns>
        public ResultDO GetYearList()
        {
            try
            {
                ResultDO objresultdo = new ResultDO();

                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetYears");
                return objresultdo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO GetYearandMonthList()
        {
            try
            {
                ResultDO objresultdo = new ResultDO();

                objresultdo.Resultdtset = daHelper.ExecuteDataset("AB_Sp_BindMonthAndYear");
                return objresultdo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResultDO GetProjectList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmployeeProjectList");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmployeeProjectList", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
  public ResultDO Getstatus()
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
  
        public ResultDO EmployeeModuleList(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmployeeModuleList");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.ProjectId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmployeeModuleList", objparam);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO GetClients()
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetClientId");
                //objparam[0].Value = ObjTimesheetDO.ProjectId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetClientId", objparam);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO GetTaskcat()
        {
            try
            {
                retresultdo = new ResultDO();
                daHelper = new SQLDataAccessHelper();
                //IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetTaskCategory");
                //objparam[0].Value = ObjTimesheetDO.ProjectId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetTaskCategory");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO Getholidaylist()
        {
            try
            {
                retresultdo = new ResultDO();
                daHelper = new SQLDataAccessHelper();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetHolidayList");
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetHolidayList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public ResultDO GetTaskMaster(TimesheetDO ObjTimesheetDO)
        {
            try
            {

                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_EmpTaskMaster");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmpTaskMaster", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public ResultDO GetTaskMasteredit(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet(" AB_EmpTaskMasteredit");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                objparam[1].Value = ObjTimesheetDO.TaskName;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_EmpTaskMasteredit", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }
        public int AddNewClientlist(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_AddNewClientlist");
                objparam[0].Value = ObjTimesheetDO.ClientName;
                //objparam[1].Value = ObjTimesheetDO.ProjectId;
                objparam[1].Direction = ParameterDirection.Output;
                daHelper.ExecuteNonQuery("AB_AddNewClientlist", objparam);
                retresultdo.UpdateResult = int.Parse(objparam[1].Value.ToString());
                return retresultdo.UpdateResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ResultDO bindddlSelectEmpName(TimesheetDO ObjTimesheetDO)
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("LM_List_of_Emp");
                objparam[0].Value = ObjTimesheetDO.EmpId;
                retresultdo.Resultdtset = daHelper.ExecuteDataset("LM_List_of_Emp", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        public ResultDO getRemainderList()
        {
            try
            {
                retresultdo = new ResultDO();
                IDataParameter[] objparam = daHelper.GetSpParameterSet("AB_GetRemainderList");
                retresultdo.Resultdtset = daHelper.ExecuteDataset("AB_GetRemainderList", objparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retresultdo;
        }

        //public void DeleteData(int Id)
        //{
        //    SqlDataAdapter sqladp = new SqlDataAdapter("delete from AB_Reminder where Id='" + Id + "'", conn);
        //    DataTable dt = new DataTable();
        //    sqladp.Fill(dt);

        //}
        //public object SelectData()
        //{
        //    SqlDataAdapter SQLAdp = new SqlDataAdapter("Select * from AB_Reminder", conn);
        //    DataTable DT = new DataTable();
        //    SQLAdp.Fill(DT);
        //    return DT;
        //}
    }
}
