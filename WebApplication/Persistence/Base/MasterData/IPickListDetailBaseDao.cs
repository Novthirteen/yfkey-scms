using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPickListDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePickListDetail(PickListDetail entity);

        PickListDetail LoadPickListDetail(Int32 id);
  
        IList<PickListDetail> GetAllPickListDetail();
  
        void UpdatePickListDetail(PickListDetail entity);
        
        void DeletePickListDetail(Int32 id);
    
        void DeletePickListDetail(PickListDetail entity);
    
        void DeletePickListDetail(IList<Int32> pkList);
    
        void DeletePickListDetail(IList<PickListDetail> entityList);    
        
        #endregion Method Created By CodeSmith
    }
}
