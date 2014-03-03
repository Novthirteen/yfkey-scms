using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface ILocationBinItemDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationBinItemDetail(LocationBinItemDetail entity);

        LocationBinItemDetail LoadLocationBinItemDetail(Int32 id);
  
        IList<LocationBinItemDetail> GetAllLocationBinItemDetail();
  
        void UpdateLocationBinItemDetail(LocationBinItemDetail entity);
        
        void DeleteLocationBinItemDetail(Int32 id);
    
        void DeleteLocationBinItemDetail(LocationBinItemDetail entity);
    
        void DeleteLocationBinItemDetail(IList<Int32> pkList);
    
        void DeleteLocationBinItemDetail(IList<LocationBinItemDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
