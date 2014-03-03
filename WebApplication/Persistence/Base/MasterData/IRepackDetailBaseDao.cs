using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRepackDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRepackDetail(RepackDetail entity);

        RepackDetail LoadRepackDetail(Int32 id);
  
        IList<RepackDetail> GetAllRepackDetail();
  
        void UpdateRepackDetail(RepackDetail entity);
        
        void DeleteRepackDetail(Int32 id);
    
        void DeleteRepackDetail(RepackDetail entity);
    
        void DeleteRepackDetail(IList<Int32> pkList);
    
        void DeleteRepackDetail(IList<RepackDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
