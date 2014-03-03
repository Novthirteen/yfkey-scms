using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IBomDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBomDetail(BomDetail entity);

        BomDetail LoadBomDetail(Int32 id);
  
        IList<BomDetail> GetAllBomDetail();
  
        void UpdateBomDetail(BomDetail entity);
        
        void DeleteBomDetail(Int32 id);
    
        void DeleteBomDetail(BomDetail entity);
    
        void DeleteBomDetail(IList<Int32> pkList);
    
        void DeleteBomDetail(IList<BomDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
