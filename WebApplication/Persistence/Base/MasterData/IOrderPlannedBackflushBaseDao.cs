using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IOrderPlannedBackflushBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderPlannedBackflush(OrderPlannedBackflush entity);

        OrderPlannedBackflush LoadOrderPlannedBackflush(Int32 id);
  
        IList<OrderPlannedBackflush> GetAllOrderPlannedBackflush();
  
        void UpdateOrderPlannedBackflush(OrderPlannedBackflush entity);
        
        void DeleteOrderPlannedBackflush(Int32 id);
    
        void DeleteOrderPlannedBackflush(OrderPlannedBackflush entity);
    
        void DeleteOrderPlannedBackflush(IList<Int32> pkList);
    
        void DeleteOrderPlannedBackflush(IList<OrderPlannedBackflush> entityList);    
        #endregion Method Created By CodeSmith
    }
}
