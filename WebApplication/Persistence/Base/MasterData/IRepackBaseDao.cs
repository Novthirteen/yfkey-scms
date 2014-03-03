using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRepackBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRepack(Repack entity);

        Repack LoadRepack(String repackNo);
  
        IList<Repack> GetAllRepack();
  
        void UpdateRepack(Repack entity);
        
        void DeleteRepack(String repackNo);
    
        void DeleteRepack(Repack entity);
    
        void DeleteRepack(IList<String> pkList);
    
        void DeleteRepack(IList<Repack> entityList);    
        #endregion Method Created By CodeSmith
    }
}
