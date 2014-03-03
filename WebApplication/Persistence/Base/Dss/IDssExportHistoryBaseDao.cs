using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssExportHistoryBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssExportHistory(DssExportHistory entity);

        DssExportHistory LoadDssExportHistory(Int32 id);
  
        IList<DssExportHistory> GetAllDssExportHistory();
  
        IList<DssExportHistory> GetAllDssExportHistory(bool includeInactive);
  
        void UpdateDssExportHistory(DssExportHistory entity);
        
        void DeleteDssExportHistory(Int32 id);
    
        void DeleteDssExportHistory(DssExportHistory entity);
    
        void DeleteDssExportHistory(IList<Int32> pkList);
    
        void DeleteDssExportHistory(IList<DssExportHistory> entityList);    
        #endregion Method Created By CodeSmith
    }
}
