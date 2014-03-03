using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ICycleCountResultBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCycleCountResult(CycleCountResult entity);

        CycleCountResult LoadCycleCountResult(Int32 id);
  
        IList<CycleCountResult> GetAllCycleCountResult();
  
        void UpdateCycleCountResult(CycleCountResult entity);
        
        void DeleteCycleCountResult(Int32 id);
    
        void DeleteCycleCountResult(CycleCountResult entity);
    
        void DeleteCycleCountResult(IList<Int32> pkList);
    
        void DeleteCycleCountResult(IList<CycleCountResult> entityList);    
        #endregion Method Created By CodeSmith
    }
}
