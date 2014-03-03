using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface ILocationBinBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationBin(LocationBin entity);

        LocationBin LoadLocationBin(Int32 id);
  
        IList<LocationBin> GetAllLocationBin();
  
        void UpdateLocationBin(LocationBin entity);
        
        void DeleteLocationBin(Int32 id);
    
        void DeleteLocationBin(LocationBin entity);
    
        void DeleteLocationBin(IList<Int32> pkList);
    
        void DeleteLocationBin(IList<LocationBin> entityList);    
        #endregion Method Created By CodeSmith
    }
}
