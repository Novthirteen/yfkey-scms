using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class WorkCalendarMgr : IWorkCalendarMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IWorkdayMgr WorkdayMgr;
        private IShiftMgr ShiftMgr;
        private ISpecialTimeMgr SpecialTimeMgr;
        private IWorkdayShiftMgr WorkdayShiftMgr;
        public WorkCalendarMgr(ICriteriaMgr criteriaMgr,
            IWorkdayMgr WorkdayMgr,
            ISpecialTimeMgr SpecialTimeMgr,
            IShiftMgr ShiftMgr,
            IWorkdayShiftMgr WorkdayShiftMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.WorkdayMgr = WorkdayMgr;
            this.ShiftMgr = ShiftMgr;
            this.SpecialTimeMgr = SpecialTimeMgr;
            this.WorkdayShiftMgr = WorkdayShiftMgr;
        }

        [Transaction(TransactionMode.Unspecified)]
        public List<WorkCalendar> GetWorkCalendar(DateTime date, string region, string workcenter)
        {
            List<WorkCalendar> workCalendars = new List<WorkCalendar>();

            date = date.Date;
            string dayofweek = date.DayOfWeek.ToString();
            IList<Workday> workdayList = WorkdayMgr.GetWorkdayByDayofweekWizard(dayofweek, region, workcenter);
            if (workdayList == null || workdayList.Count == 0)
                return null;
            Workday workday = workdayList[0];

            WorkCalendar workCalendar = new WorkCalendar();
            if (workday != null)
            {
                IList<Shift> shifts = WorkdayShiftMgr.GetShiftsByWorkdayId(workday.Id);
                if (shifts.Count > 0)
                {
                    foreach (Shift s in shifts)
                    {
                        Shift shift = ShiftMgr.LoadShift(s.Code, date);
                        if (shift == null)
                            continue;

                        DateTime lastendtime = DateTime.MaxValue;
                        int length = shift.ShiftTime.Split('|').GetLength(0);
                        string[] times = shift.ShiftTime.Split('|');
                        foreach (string str in times)
                        {
                            string[] time = str.Split('-');
                            DateTime starttime = this.AssembleActualTime(date, time[0]);
                            DateTime endtime = this.AssembleActualTime(date, time[1]);
                            if (DateTime.Compare(starttime, endtime) >= 0)
                            {
                                endtime = endtime.AddDays(1);
                            }

                            if (DateTime.Compare(lastendtime, starttime) < 0)
                            {
                                workCalendar = new WorkCalendar();
                                workCalendar.Date = date;
                                workCalendar.DayOfWeek = dayofweek;
                                workCalendar.ShiftCode = shift.Code;
                                workCalendar.ShiftName = shift.ShiftName;
                                workCalendar.StartTime = lastendtime;
                                workCalendar.EndTime = starttime;
                                workCalendar.Type = BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST;
                                workCalendars.Add(workCalendar);
                            }

                            workCalendar = new WorkCalendar();
                            workCalendar.Date = date;
                            workCalendar.DayOfWeek = dayofweek;
                            workCalendar.ShiftCode = shift.Code;
                            workCalendar.ShiftName = shift.ShiftName;
                            workCalendar.StartTime = starttime;
                            workCalendar.EndTime = endtime;
                            workCalendar.Type = workday.Type;
                            workCalendars.Add(workCalendar);

                            lastendtime = endtime;
                        }
                    }
                }
                else
                {
                    workCalendar = new WorkCalendar();
                    workCalendar.Date = date;
                    workCalendar.DayOfWeek = dayofweek;
                    workCalendar.StartTime = date;
                    workCalendar.EndTime = date.AddDays(1);
                    workCalendar.Type = workday.Type;
                    workCalendars.Add(workCalendar);
                }
            }
            else
            {
                workCalendar = new WorkCalendar();
                workCalendar.Date = date;
                workCalendar.DayOfWeek = dayofweek;
                workCalendar.StartTime = date;
                workCalendar.EndTime = date.AddDays(1);
                workCalendar.Type = BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_WORK;
                workCalendars.Add(workCalendar);
            }

            workCalendars.Sort(WorkCalendarTimeCompare);
            workCalendars = this.SpecialTimeWizard(date, region, workcenter, workCalendars);
            workCalendars = this.WorkCalendarDataClean(workCalendars);

            return workCalendars;
        }

        [Transaction(TransactionMode.Unspecified)]
        public List<WorkCalendar> GetWorkCalendar(DateTime startdate, DateTime enddate, string region, string workcenter)
        {
            List<WorkCalendar> workCalendars = new List<WorkCalendar>();
            startdate = startdate.Date;
            enddate = enddate.Date;
            while (DateTime.Compare(startdate, enddate) <= 0)
            {
                List<WorkCalendar> newWorkCalendars = this.GetWorkCalendar(startdate, region, workcenter);
                if (newWorkCalendars != null)
                    workCalendars.AddRange(newWorkCalendars);
                startdate = startdate.AddDays(1);
            }

            this.WorkCalendarContinuousTime(workCalendars);
            return workCalendars;
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetWorkTime(DateTime originalTime, string region, bool isSup)
        {
            return this.GetWorkTime(originalTime, region, null, isSup);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetWorkTime(DateTime originalTime, string region, string workCenter, bool isSup)
        {
            DateTime newTime = originalTime;
            //double totalRestHours = 0;
            double totalHours = this.GetRestTimeHours(originalTime, region, workCenter);
            if (totalHours == 24)
            {
                for (int i = 1; i < 7; i++)
                {
                    double t = this.GetRestTimeHours(originalTime.AddDays(i), region, workCenter);
                    totalHours += t;
                    if (t == 0)
                    {
                        break;
                    }
                }
                for (int j = 1; j < 7; j++)
                {
                    double t = this.GetRestTimeHours(originalTime.AddDays(-1 * j), region, workCenter);
                    totalHours += t;
                    if (t == 0)
                    {
                        break;
                    }
                }
            }

            if (isSup)
            {
                newTime = originalTime.AddHours(-1 * totalHours);
            }
            else
            {
                newTime = originalTime.AddHours(totalHours);
            }

            return newTime;
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetDayShiftStart(DateTime originalTime, string region)
        {
            return this.GetDayShiftStart(originalTime, region, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DateTime GetDayShiftStart(DateTime originalTime, string region, string workCenter)
        {
            DateTime dayShiftStart = originalTime.Date;
            List<WorkCalendar> workCalendars = this.GetWorkCalendar(originalTime, region, workCenter);
            if (workCalendars != null && workCalendars.Count > 0)
            {
                dayShiftStart = workCalendars[0].StartTime;
            }

            return dayShiftStart;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Shift> GetShiftByDate(DateTime date, string region, string workcenter)
        {
            List<WorkCalendar> workCalendars = this.GetWorkCalendar(date, region, workcenter);
            IList<Shift> shiftList = new List<Shift>();
            if (workCalendars != null && workCalendars.Count > 0)
            {
                foreach (WorkCalendar workCalendar in workCalendars)
                {
                    if (workCalendar.ShiftCode != null && workCalendar.ShiftCode.Trim() != string.Empty)
                    {
                        bool isExist = false;
                        if (shiftList != null && shiftList.Count > 0)
                        {
                            foreach (Shift sf in shiftList)
                            {
                                if (sf.Code.Trim().ToUpper() == workCalendar.ShiftCode.Trim().ToUpper())
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                        }

                        if (!isExist)
                        {
                            Shift shift = new Shift();
                            shift = ShiftMgr.LoadShift(workCalendar.ShiftCode, date);
                            shiftList.Add(shift);
                        }
                    }
                }
            }

            return shiftList;
        }

        #region private methods
        //time format 08:00
        private DateTime AssembleActualTime(DateTime date, string time)
        {
            DateTime actualTime = date;
            try
            {
                actualTime = Convert.ToDateTime(date.ToString("yyyy-MM-dd") + " " + time);
            }
            catch (Exception)
            { }

            return actualTime;
        }

        [Transaction(TransactionMode.Unspecified)]
        private List<WorkCalendar> SpecialTimeWizard(DateTime date, string region, string workcenter, List<WorkCalendar> workCalendars)
        {
            List<WorkCalendar> addList = new List<WorkCalendar>();
            date = date.Date;
            string dayofweek = date.DayOfWeek.ToString();
            DateTime dateShiftStart = date;
            DateTime dateShiftEnd = date.AddDays(1);
            if (workCalendars.Count > 0)
            {
                dateShiftStart = workCalendars[0].StartTime;
                dateShiftEnd = workCalendars[workCalendars.Count - 1].EndTime;
            }
            IList specialTimes = SpecialTimeMgr.GetReferSpecialTimeWizard(dateShiftStart, dateShiftEnd, region, workcenter);

            if (specialTimes.Count > 0)
            {
                foreach (WorkCalendar workCalendar in workCalendars)
                {
                    WorkCalendar newWorkCalendar = new WorkCalendar();
                    foreach (SpecialTime specialTime in specialTimes)
                    {
                        //SpecialTime            ------
                        //WorkCalendar                   ------
                        if (DateTime.Compare(specialTime.EndTime, workCalendar.StartTime) <= 0)
                        {
                            continue;
                        }
                        //SpecialTime                            ------
                        //WorkCalendar                   ------
                        else if (DateTime.Compare(specialTime.StartTime, workCalendar.EndTime) >= 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (specialTime.Type == workCalendar.Type)
                            {
                                continue;
                            }
                            else
                            {
                                if (DateTime.Compare(specialTime.StartTime, workCalendar.StartTime) <= 0)
                                {
                                    //SpecialTime              ----------
                                    //WorkCalendar               ------
                                    if (DateTime.Compare(specialTime.EndTime, workCalendar.EndTime) >= 0)
                                    {
                                        workCalendar.Type = specialTime.Type;
                                    }
                                    //SpecialTime            ------
                                    //WorkCalendar               ------
                                    else
                                    {
                                        newWorkCalendar = new WorkCalendar();
                                        CloneHelper.CopyProperty(workCalendar, newWorkCalendar);
                                        newWorkCalendar.EndTime = specialTime.EndTime;
                                        newWorkCalendar.Type = specialTime.Type;
                                        addList.Add(newWorkCalendar);

                                        workCalendar.StartTime = specialTime.EndTime;
                                    }
                                }
                                else
                                {
                                    //SpecialTime                    ------
                                    //WorkCalendar               ------
                                    if (DateTime.Compare(specialTime.EndTime, workCalendar.EndTime) >= 0)
                                    {
                                        newWorkCalendar = new WorkCalendar();
                                        CloneHelper.CopyProperty(workCalendar, newWorkCalendar);
                                        newWorkCalendar.StartTime = specialTime.StartTime;
                                        newWorkCalendar.EndTime = workCalendar.EndTime;
                                        newWorkCalendar.Type = specialTime.Type;
                                        addList.Add(newWorkCalendar);

                                        workCalendar.EndTime = specialTime.StartTime;
                                    }
                                    //SpecialTime                  ------
                                    //WorkCalendar               ----------
                                    else
                                    {
                                        newWorkCalendar = new WorkCalendar();
                                        CloneHelper.CopyProperty(workCalendar, newWorkCalendar);
                                        newWorkCalendar.EndTime = specialTime.StartTime;
                                        newWorkCalendar.Type = workCalendar.Type;
                                        addList.Add(newWorkCalendar);

                                        newWorkCalendar = new WorkCalendar();
                                        CloneHelper.CopyProperty(workCalendar, newWorkCalendar);
                                        newWorkCalendar.StartTime = specialTime.EndTime;
                                        newWorkCalendar.Type = workCalendar.Type;
                                        addList.Add(newWorkCalendar);

                                        workCalendar.StartTime = specialTime.StartTime;
                                        workCalendar.EndTime = specialTime.EndTime;
                                        workCalendar.Type = specialTime.Type;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return workCalendars;
            }

            workCalendars.AddRange(addList);
            workCalendars.Sort(WorkCalendarTimeCompare);

            return workCalendars;
        }

        private static int WorkCalendarTimeCompare(WorkCalendar x, WorkCalendar y)
        {
            return DateTime.Compare(x.StartTime, y.StartTime);
        }

        private List<WorkCalendar> WorkCalendarDataClean(List<WorkCalendar> workCalendars)
        {
            List<WorkCalendar> removeList = new List<WorkCalendar>();
            if (workCalendars.Count > 1)
            {
                foreach (WorkCalendar workCalendar in workCalendars)
                {
                    if (workCalendar.Type == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST)
                    {
                        workCalendar.ShiftCode = null;
                        workCalendar.ShiftName = null;
                    }

                    int index = workCalendars.IndexOf(workCalendar);
                    if (index == 0)
                    {
                        continue;
                    }

                    if (workCalendars[index - 1].Type == null || workCalendar.Type == null)
                    {
                        continue;
                    }

                    if (workCalendars[index - 1].Type == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST
                        && workCalendar.Type == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST)
                    {
                        if (DateTime.Compare(workCalendars[index - 1].EndTime, workCalendar.StartTime) >= 0)
                        {
                            workCalendar.StartTime = workCalendars[index - 1].StartTime;
                            removeList.Add(workCalendars[index - 1]);
                        }
                    }
                }
            }

            List<WorkCalendar> returnList = new List<WorkCalendar>();
            if (removeList.Count > 0)
            {
                foreach (WorkCalendar returnWorkCalendar in workCalendars)
                {
                    if (removeList.Contains(returnWorkCalendar))
                    {
                        continue;
                    }
                    else
                    {
                        returnList.Add(returnWorkCalendar);
                    }
                }
            }
            else
            {
                returnList = workCalendars;
            }

            return returnList;
        }

        private void WorkCalendarContinuousTime(List<WorkCalendar> workCalendars)
        {
            List<WorkCalendar> addList = new List<WorkCalendar>();
            if (workCalendars != null && workCalendars.Count > 0)
            {
                for (int i = 0; i < workCalendars.Count - 1; i++)
                {
                    if (DateTime.Compare(workCalendars[i].EndTime, workCalendars[i + 1].StartTime) < 0)
                    {
                        if (workCalendars[i].Type == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_WORK)
                        {
                            WorkCalendar workCalendar = new WorkCalendar();
                            DateTime startTime = workCalendars[i].EndTime;
                            CloneHelper.CopyProperty(workCalendars[i], workCalendar);
                            workCalendar.ShiftCode = null;
                            workCalendar.ShiftName = null;
                            workCalendar.Type = BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST;
                            workCalendar.StartTime = workCalendars[i].EndTime;
                            workCalendar.EndTime = workCalendars[i + 1].StartTime;

                            addList.Add(workCalendar);
                        }
                        else
                        {
                            workCalendars[i].EndTime = workCalendars[i + 1].StartTime;
                        }
                    }
                }
            }

            if (addList.Count > 0)
            {
                workCalendars.AddRange(addList);
            }
            workCalendars.Sort(WorkCalendarTimeCompare);
        }

        [Transaction(TransactionMode.Unspecified)]
        private double GetRestTimeHours(DateTime originalTime, string region, string workCenter)
        {
            double totalHours = 0;
            List<WorkCalendar> workCalendars = this.GetWorkCalendar(originalTime, region, workCenter);
            if (workCalendars != null && workCalendars.Count > 0)
            {
                foreach (WorkCalendar workCalendar in workCalendars)
                {
                    if (workCalendar.Type == BusinessConstants.CODE_MASTER_WORKCALENDAR_TYPE_VALUE_REST)
                    {
                        if (DateTime.Compare(workCalendar.StartTime, originalTime) <= 0 && DateTime.Compare(workCalendar.EndTime, originalTime) >= 0)
                        {
                            TimeSpan ts = workCalendar.EndTime - workCalendar.StartTime;
                            totalHours = ts.TotalHours;
                        }
                    }
                }
            }
            return totalHours;
        }

        #endregion
    }
}
