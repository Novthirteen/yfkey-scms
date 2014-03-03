using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MRP
{
    public interface IMrpRunLogBaseDao
    {
        #region Method Created By CodeSmith

        void CreateMrpRunLog(MrpRunLog entity);

        MrpRunLog LoadMrpRunLog(Int32 id);
  
        IList<MrpRunLog> GetAllMrpRunLog();
  
        void UpdateMrpRunLog(MrpRunLog entity);
        
        void DeleteMrpRunLog(Int32 id);
    
        void DeleteMrpRunLog(MrpRunLog entity);
    
        void DeleteMrpRunLog(IList<Int32> pkList);
    
        void DeleteMrpRunLog(IList<MrpRunLog> entityList);    
        #endregion Method Created By CodeSmith
    }
}
