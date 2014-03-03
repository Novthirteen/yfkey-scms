using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.MasterData
{
    public interface IWorkCalendarMgr
    {
        #region Customized Methods

        List<WorkCalendar> GetWorkCalendar(DateTime date, string region, string workcenter);

        List<WorkCalendar> GetWorkCalendar(DateTime startdate, DateTime enddate, string region, string workcenter);

        DateTime GetWorkTime(DateTime originalTime, string region, bool isSup);

        DateTime GetWorkTime(DateTime originalTime, string region, string workCenter, bool isSup);

        DateTime GetDayShiftStart(DateTime originalTime, string region);

        DateTime GetDayShiftStart(DateTime originalTime, string region, string workCenter);

        IList<Shift> GetShiftByDate(DateTime date, string region, string workcenter);

        #endregion Customized Methods
    }
}
