using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ICycleCountDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCycleCountDetail(CycleCountDetail entity);

        CycleCountDetail LoadCycleCountDetail(Int32 id);
  
        IList<CycleCountDetail> GetAllCycleCountDetail();
  
        void UpdateCycleCountDetail(CycleCountDetail entity);
        
        void DeleteCycleCountDetail(Int32 id);
    
        void DeleteCycleCountDetail(CycleCountDetail entity);
    
        void DeleteCycleCountDetail(IList<Int32> pkList);
    
        void DeleteCycleCountDetail(IList<CycleCountDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
