using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssImportHistoryBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssImportHistory(DssImportHistory entity);

        DssImportHistory LoadDssImportHistory(Int32 id);
  
        IList<DssImportHistory> GetAllDssImportHistory();
  
        IList<DssImportHistory> GetAllDssImportHistory(bool includeInactive);
  
        void UpdateDssImportHistory(DssImportHistory entity);
        
        void DeleteDssImportHistory(Int32 id);
    
        void DeleteDssImportHistory(DssImportHistory entity);
    
        void DeleteDssImportHistory(IList<Int32> pkList);
    
        void DeleteDssImportHistory(IList<DssImportHistory> entityList);    
        #endregion Method Created By CodeSmith
    }
}
