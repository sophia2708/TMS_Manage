using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalyticBrainsDO;
using AnalyticBrainsDAL;
using System.Data;
using System.Data.Sql;
using AnalyticBrainsBL;

namespace AnalyticBrainsBL
{
    public class TimesheetBL
    {
        TimesheetDAL ObjTimesheetDAL = new TimesheetDAL();

        public ResultDO EmployeeTaskList(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.EmployeeTaskList(ObjTimesheetDO);
        }
        public ResultDO EmpProjectList(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.EmpProjectList(ObjTimesheetDO);
        }
        public void SaveTimesheetDetail(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.SaveTimesheetDetail(ObjTimesheetDO);
        }
        public void ReminderSaveDetail(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.ReminderSaveDetail(ObjTimesheetDO);
        }
        public int SendMail(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.SendMail(ObjTimesheetDO);
        }
        //Need to format
        public void DeleteTimeSheet(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.DeleteTimeSheet(ObjTimesheetDO);
        }
        public void DeleteEnterTimeSheet(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.DeleteEnterTimeSheet(ObjTimesheetDO);
        }
        //public void DeleteRemindersheet(TimesheetDO ObjTimesheetDO)
        //{
        //    ObjTimesheetDAL.DeleteRemindersheet(ObjTimesheetDO);
        //}
        public void UpdateTimesheet(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.UpdateTimesheet(ObjTimesheetDO);
        }
        public ResultDO ViewEmpTimesheet1(TimesheetDO ObjTimesheetDO)// modified sp - Hold item need to display on fresh page  itself
        {
            return ObjTimesheetDAL.ViewEmpTimesheet1(ObjTimesheetDO);
        }
        public ResultDO GetWorkmodeStatus(TimesheetDO ObjTimesheetDO)// modified sp - Hold item need to display on fresh page  itself
        {
            return ObjTimesheetDAL.GetWorkmodeStatus(ObjTimesheetDO);
        }
        public ResultDO ViewEmpTimesheet(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.ViewEmpTimesheet(ObjTimesheetDO);
        }
        public ResultDO WeeklyStatus(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.WeeklyStatus(ObjTimesheetDO);
        }
        public ResultDO ApprovalTimesheet(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.ApprovalTimesheet(ObjTimesheetDO);
        }
        public int Approval(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.Approval(ObjTimesheetDO);
        }

        public ResultDO Approval_mail(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.Approval_mail(ObjTimesheetDO);
        }


        public ResultDO GetProjectdropdown(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetProjectdropdown(ObjTimesheetDO);
        
        }
        public ResultDO GetProjectList()
        {
            return ObjTimesheetDAL.GetProjectList();

        }
        public ResultDO GetModuleList(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetModuleList(ObjTimesheetDO);
       
        }
        public ResultDO GetTaskList(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetTaskList(ObjTimesheetDO);
           
        }
        public ResultDO GetEmployeeList(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetEmployeeList(ObjTimesheetDO);
         
        }
        public ResultDO Gethybridvalues(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.Gethybridvalues(ObjTimesheetDO);

        }
        public ResultDO GetActiveemployeelist(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetActiveemployeelist(ObjTimesheetDO);

        }
        public ResultDO GetPriorityList()
        {
            return ObjTimesheetDAL.GetPriorityList();
           
        }
        public void AddNewProject(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.AddNewProject(ObjTimesheetDO);
          
        }
        public int AddNewProjectList(TimesheetDO ObjTimesheetDO)// for adding new project list 
        {
            return ObjTimesheetDAL.AddNewProjectList(ObjTimesheetDO);
        }
        public int AddNewModuleList(TimesheetDO ObjTimesheetDO)// for adding new module list 
        {
            return ObjTimesheetDAL.AddNewModuleList(ObjTimesheetDO);
        }
        public void AddNewModule(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.AddNewModule(ObjTimesheetDO);
        }
        public void AddNewTask(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.AddNewTask(ObjTimesheetDO);
        }

        public int AddNewTaskList(TimesheetDO ObjTimesheetDO)// for adding new Task list 
        {
            return ObjTimesheetDAL.AddNewTaskList(ObjTimesheetDO);
        }
        public int SaveEmployeeTask(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.SaveEmployeeTask(ObjTimesheetDO);
        }

        public ResultDO GetEmpDashBoard(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetEmpDashBoard(ObjTimesheetDO);
          
        }
        public ResultDO GetclientBoard(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetclientBoard(ObjTimesheetDO);
        }
        public ResultDO GetEmpTaskSummary(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetEmpTaskSummary(ObjTimesheetDO);
            
        }

        public void UpdateEmployeeStatus(TimesheetDO ObjTimesheetDO)
        {
            ObjTimesheetDAL.UpdateEmployeeStatus(ObjTimesheetDO);
        }
        public ResultDO GetYearList()
        {
            return ObjTimesheetDAL.GetYearList();
        }
        public ResultDO GetYearandMonthList()
        {
            return ObjTimesheetDAL.GetYearandMonthList();
        }
        public ResultDO EmployeeModuleList(TimesheetDO ObjTimesheetDO)
        {
            ResultDO ds = ObjTimesheetDAL.EmployeeModuleList(ObjTimesheetDO);
            return ds;
        }
        public ResultDO Getstatus()
        {
            ResultDO ds = ObjTimesheetDAL.Getstatus();
            return ds;
        }
        public ResultDO GetClients()
        {
            ResultDO ds = ObjTimesheetDAL.GetClients();
            return ds;
        }
        public ResultDO GetTaskcat()
        {
            ResultDO ds = ObjTimesheetDAL.GetTaskcat();
            return ds;
        }
        public ResultDO Getholidaylist()
        {
            ResultDO ds = ObjTimesheetDAL.Getholidaylist();
            return ds;
        }
        public ResultDO GetEmpTaskEdit(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetEmpTaskEdit(ObjTimesheetDO);

        }
        public ResultDO ClientId(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.ClientId(ObjTimesheetDO);
        }
        public ResultDO GetTaskMaster(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetTaskMaster(ObjTimesheetDO);
        }
        public ResultDO GetTaskMasteredit(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.GetTaskMasteredit(ObjTimesheetDO);
        }
        public int AddNewClientlist(TimesheetDO ObjTimesheetDO)// for adding new Task list 
        {
            return ObjTimesheetDAL.AddNewClientlist(ObjTimesheetDO);
        }
        public ResultDO InprogressEmpTimesheet(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.InprogressEmpTimesheet(ObjTimesheetDO);
        }
        public ResultDO Inprogressgridviewtimesheet(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.Inprogressgridviewtimesheet(ObjTimesheetDO);
        }
        public ResultDO bindddlSelectEmpName(TimesheetDO ObjTimesheetDO)
        {
            return ObjTimesheetDAL.bindddlSelectEmpName(ObjTimesheetDO);
        }
        public ResultDO getRemainderList()
        {
            return ObjTimesheetDAL.getRemainderList();
        }
        //public void DeleteUser(int Id)
        //{
        //    //call the DAL method here//
        //    ObjTimesheetDAL.DeleteData(Id);
        //}
        //public object SelectUser()
        //{
        //    //call the DAL method here//
        //    return ObjTimesheetDAL.SelectData();
        //}

    }
}