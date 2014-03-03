using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ILocationLotDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationLotDetail(LocationLotDetail entity);

        LocationLotDetail LoadLocationLotDetail(Int32 id);
  
        IList<LocationLotDetail> GetAllLocationLotDetail();
  
        void UpdateLocationLotDetail(LocationLotDetail entity);
        
        void DeleteLocationLotDetail(Int32 id);
    
        void DeleteLocationLotDetail(LocationLotDetail entity);
    
        void DeleteLocationLotDetail(IList<Int32> pkList);
    
        void DeleteLocationLotDetail(IList<LocationLotDetail> entityList);    
        
        #endregion Method Created By CodeSmith
    }
}
