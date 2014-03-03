using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ISpecialTimeBaseDao
    {
        #region Method Created By CodeSmith

        void CreateSpecialTime(SpecialTime entity);

        SpecialTime LoadSpecialTime(Int32 iD);
  
        IList<SpecialTime> GetAllSpecialTime();
  
        void UpdateSpecialTime(SpecialTime entity);
        
        void DeleteSpecialTime(Int32 iD);
    
        void DeleteSpecialTime(SpecialTime entity);
    
        void DeleteSpecialTime(IList<Int32> pkList);
    
        void DeleteSpecialTime(IList<SpecialTime> entityList);    
        #endregion Method Created By CodeSmith
    }
}
