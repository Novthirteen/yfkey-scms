using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ILocationDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationDetail(LocationDetail entity);

        LocationDetail LoadLocationDetail(Int32 id);
  
        IList<LocationDetail> GetAllLocationDetail();
  
        void UpdateLocationDetail(LocationDetail entity);
        
        void DeleteLocationDetail(Int32 id);
    
        void DeleteLocationDetail(LocationDetail entity);
    
        void DeleteLocationDetail(IList<Int32> pkList);
    
        void DeleteLocationDetail(IList<LocationDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
