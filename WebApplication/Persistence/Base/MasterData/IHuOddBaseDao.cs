using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IHuOddBaseDao
    {
        #region Method Created By CodeSmith

        void CreateHuOdd(HuOdd entity);

        HuOdd LoadHuOdd(Int32 id);
  
        IList<HuOdd> GetAllHuOdd();
  
        void UpdateHuOdd(HuOdd entity);
        
        void DeleteHuOdd(Int32 id);
    
        void DeleteHuOdd(HuOdd entity);
    
        void DeleteHuOdd(IList<Int32> pkList);
    
        void DeleteHuOdd(IList<HuOdd> entityList);    
        #endregion Method Created By CodeSmith
    }
}
