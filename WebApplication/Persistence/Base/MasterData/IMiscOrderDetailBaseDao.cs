using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IMiscOrderDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateMiscOrderDetail(MiscOrderDetail entity);

        MiscOrderDetail LoadMiscOrderDetail(Int32 id);
  
        IList<MiscOrderDetail> GetAllMiscOrderDetail();
  
        void UpdateMiscOrderDetail(MiscOrderDetail entity);
        
        void DeleteMiscOrderDetail(Int32 id);
    
        void DeleteMiscOrderDetail(MiscOrderDetail entity);
    
        void DeleteMiscOrderDetail(IList<Int32> pkList);
    
        void DeleteMiscOrderDetail(IList<MiscOrderDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
