using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using System.Data;
using System.IO;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IShiftPlanScheduleMgr : IShiftPlanScheduleBaseMgr
    {
        #region Customized Methods

        ShiftPlanSchedule GetShiftPlanSchedule(int flowDetailId, DateTime reqDate, string code, int seq);

        ShiftPlanSchedule GetShiftPlanScheduleByItemFlowPlanDetId(int ItemFlowPlanDetId);

        IList<ShiftPlanSchedule> GetShiftPlanScheduleList(string region, string flow, DateTime date, string code, string itemCode, string userCode);

        DataTable ConvertShiftPlanScheduleToDataTable(IList<ShiftPlanSchedule> spsList, IList<Shift> shiftList);

        void GenOrdersByShiftPlanScheduleId(int ShiftPlanScheduleId, string userCode);

        void SaveShiftPlanSchedule(IList<ShiftPlanSchedule> shiftPlanScheduleList, User user);

        void SaveShiftPlanSchedule(ShiftPlanSchedule shiftPlanSchedule, User user);

        void ClearOldShiftPlanSchedule(ShiftPlanSchedule shiftPlanSchedule);

        #endregion Customized Methods
    }
}
