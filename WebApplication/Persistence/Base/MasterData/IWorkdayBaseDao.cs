using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IWorkdayBaseDao
    {
        #region Method Created By CodeSmith

        void CreateWorkday(Workday entity);

        Workday LoadWorkday(Int32 id);
  
        IList<Workday> GetAllWorkday();
  
        void UpdateWorkday(Workday entity);
        
        void DeleteWorkday(Int32 id);
    
        void DeleteWorkday(Workday entity);
    
        void DeleteWorkday(IList<Int32> pkList);
    
        void DeleteWorkday(IList<Workday> entityList);    
        #endregion Method Created By CodeSmith
    }
}
