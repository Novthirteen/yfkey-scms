using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IInventoryAgingViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInventoryAgingView(InventoryAgingView entity);

        InventoryAgingView LoadInventoryAgingView(Int32 id);
  
        IList<InventoryAgingView> GetAllInventoryAgingView();
  
        void UpdateInventoryAgingView(InventoryAgingView entity);
        
        void DeleteInventoryAgingView(Int32 id);
    
        void DeleteInventoryAgingView(InventoryAgingView entity);
    
        void DeleteInventoryAgingView(IList<Int32> pkList);
    
        void DeleteInventoryAgingView(IList<InventoryAgingView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
