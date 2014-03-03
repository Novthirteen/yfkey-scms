using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssExportHistoryDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssExportHistoryDetail(DssExportHistoryDetail entity);

        DssExportHistoryDetail LoadDssExportHistoryDetail(Int32 id);
  
        IList<DssExportHistoryDetail> GetAllDssExportHistoryDetail();
  
        void UpdateDssExportHistoryDetail(DssExportHistoryDetail entity);
        
        void DeleteDssExportHistoryDetail(Int32 id);
    
        void DeleteDssExportHistoryDetail(DssExportHistoryDetail entity);
    
        void DeleteDssExportHistoryDetail(IList<Int32> pkList);
    
        void DeleteDssExportHistoryDetail(IList<DssExportHistoryDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
