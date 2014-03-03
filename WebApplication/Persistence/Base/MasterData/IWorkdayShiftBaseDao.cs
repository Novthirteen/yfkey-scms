using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IWorkdayShiftBaseDao
    {
        #region Method Created By CodeSmith

        void CreateWorkdayShift(WorkdayShift entity);

        WorkdayShift LoadWorkdayShift(Int32 id);
  
        IList<WorkdayShift> GetAllWorkdayShift();
  
        void UpdateWorkdayShift(WorkdayShift entity);
        
        void DeleteWorkdayShift(Int32 id);
    
        void DeleteWorkdayShift(WorkdayShift entity);
    
        void DeleteWorkdayShift(IList<Int32> pkList);
    
        void DeleteWorkdayShift(IList<WorkdayShift> entityList);    
        #endregion Method Created By CodeSmith
    }
}
