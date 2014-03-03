using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IWorkingHoursBaseDao
    {
        #region Method Created By CodeSmith

        void CreateWorkingHours(WorkingHours entity);

        WorkingHours LoadWorkingHours(Int32 id);
  
        IList<WorkingHours> GetAllWorkingHours();
  
        void UpdateWorkingHours(WorkingHours entity);
        
        void DeleteWorkingHours(Int32 id);
    
        void DeleteWorkingHours(WorkingHours entity);
    
        void DeleteWorkingHours(IList<Int32> pkList);
    
        void DeleteWorkingHours(IList<WorkingHours> entityList);    
        #endregion Method Created By CodeSmith
    }
}
