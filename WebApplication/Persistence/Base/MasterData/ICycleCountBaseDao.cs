using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ICycleCountBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCycleCount(CycleCount entity);

        CycleCount LoadCycleCount(String code);
  
        IList<CycleCount> GetAllCycleCount();
  
        void UpdateCycleCount(CycleCount entity);
        
        void DeleteCycleCount(String code);
    
        void DeleteCycleCount(CycleCount entity);
    
        void DeleteCycleCount(IList<String> pkList);
    
        void DeleteCycleCount(IList<CycleCount> entityList);    
        #endregion Method Created By CodeSmith
    }
}
