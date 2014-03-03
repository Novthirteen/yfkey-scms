using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface ILocationTransactionViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocationTransactionView(LocationTransactionView entity);

        LocationTransactionView LoadLocationTransactionView(Int32 id);
  
        IList<LocationTransactionView> GetAllLocationTransactionView();
  
        void UpdateLocationTransactionView(LocationTransactionView entity);
        
        void DeleteLocationTransactionView(Int32 id);
    
        void DeleteLocationTransactionView(LocationTransactionView entity);
    
        void DeleteLocationTransactionView(IList<Int32> pkList);
    
        void DeleteLocationTransactionView(IList<LocationTransactionView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
