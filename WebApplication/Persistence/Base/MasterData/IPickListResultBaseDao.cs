using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPickListResultBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePickListResult(PickListResult entity);

        PickListResult LoadPickListResult(Int32 id);
  
        IList<PickListResult> GetAllPickListResult();
  
        void UpdatePickListResult(PickListResult entity);
        
        void DeletePickListResult(Int32 id);
    
        void DeletePickListResult(PickListResult entity);
    
        void DeletePickListResult(IList<Int32> pkList);
    
        void DeletePickListResult(IList<PickListResult> entityList);    
        
        PickListResult LoadPickListResult(com.Sconit.Entity.MasterData.PickListDetail pickListDetail, com.Sconit.Entity.MasterData.LocationLotDetail locationLotDetail);
    
        void DeletePickListResult(Int32 pickListDetailId, Int32 locationLotDetailId);
        
        PickListResult LoadPickListResult(Int32 pickListDetailId, Int32 locationLotDetailId);
        #endregion Method Created By CodeSmith
    }
}
