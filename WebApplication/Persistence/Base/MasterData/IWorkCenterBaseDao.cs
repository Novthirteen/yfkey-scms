using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IWorkCenterBaseDao
    {
        #region Method Created By CodeSmith

        void CreateWorkCenter(WorkCenter entity);

        WorkCenter LoadWorkCenter(String code);
  
        IList<WorkCenter> GetAllWorkCenter();
  
        IList<WorkCenter> GetAllWorkCenter(bool includeInactive);
  
        void UpdateWorkCenter(WorkCenter entity);
        
        void DeleteWorkCenter(String code);
    
        void DeleteWorkCenter(WorkCenter entity);
    
        void DeleteWorkCenter(IList<String> pkList);
    
        void DeleteWorkCenter(IList<WorkCenter> entityList);    
        #endregion Method Created By CodeSmith
    }
}
