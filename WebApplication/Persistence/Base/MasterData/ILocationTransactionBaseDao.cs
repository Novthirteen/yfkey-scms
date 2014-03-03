using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ILocationTransactionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationTransaction(LocationTransaction entity);

        LocationTransaction LoadLocationTransaction(Int32 id);
  
        IList<LocationTransaction> GetAllLocationTransaction();
  
        void UpdateLocationTransaction(LocationTransaction entity);
        
        void DeleteLocationTransaction(Int32 id);
    
        void DeleteLocationTransaction(LocationTransaction entity);
    
        void DeleteLocationTransaction(IList<Int32> pkList);
    
        void DeleteLocationTransaction(IList<LocationTransaction> entityList);    
        #endregion Method Created By CodeSmith
    }
}
