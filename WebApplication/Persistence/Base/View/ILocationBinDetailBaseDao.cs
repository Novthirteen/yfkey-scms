using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface ILocationBinDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationBinDetail(LocationBinDetail entity);

        LocationBinDetail LoadLocationBinDetail(Int32 id);
  
        IList<LocationBinDetail> GetAllLocationBinDetail();
  
        void UpdateLocationBinDetail(LocationBinDetail entity);
        
        void DeleteLocationBinDetail(Int32 id);
    
        void DeleteLocationBinDetail(LocationBinDetail entity);
    
        void DeleteLocationBinDetail(IList<Int32> pkList);
    
        void DeleteLocationBinDetail(IList<LocationBinDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
