using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IInspectResultBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInspectResult(InspectResult entity);

        InspectResult LoadInspectResult(Int32 id);
  
        IList<InspectResult> GetAllInspectResult();
  
        void UpdateInspectResult(InspectResult entity);
        
        void DeleteInspectResult(Int32 id);
    
        void DeleteInspectResult(InspectResult entity);
    
        void DeleteInspectResult(IList<Int32> pkList);
    
        void DeleteInspectResult(IList<InspectResult> entityList);    
        #endregion Method Created By CodeSmith
    }
}
