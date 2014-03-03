using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IShiftPlanScheduleBaseDao
    {
        #region Method Created By CodeSmith

        void CreateShiftPlanSchedule(ShiftPlanSchedule entity);

        ShiftPlanSchedule LoadShiftPlanSchedule(Int32 id);
  
        IList<ShiftPlanSchedule> GetAllShiftPlanSchedule();
  
        void UpdateShiftPlanSchedule(ShiftPlanSchedule entity);
        
        void DeleteShiftPlanSchedule(Int32 id);
    
        void DeleteShiftPlanSchedule(ShiftPlanSchedule entity);
    
        void DeleteShiftPlanSchedule(IList<Int32> pkList);
    
        void DeleteShiftPlanSchedule(IList<ShiftPlanSchedule> entityList);    
        #endregion Method Created By CodeSmith
    }
}
