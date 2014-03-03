using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssSystemBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssSystem(DssSystem entity);

        DssSystem LoadDssSystem(String code);
  
        IList<DssSystem> GetAllDssSystem();
  
        void UpdateDssSystem(DssSystem entity);
        
        void DeleteDssSystem(String code);
    
        void DeleteDssSystem(DssSystem entity);
    
        void DeleteDssSystem(IList<String> pkList);
    
        void DeleteDssSystem(IList<DssSystem> entityList);    
        #endregion Method Created By CodeSmith
    }
}
