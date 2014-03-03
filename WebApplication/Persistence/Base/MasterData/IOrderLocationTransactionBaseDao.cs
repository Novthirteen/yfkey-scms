using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IOrderLocationTransactionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderLocationTransaction(OrderLocationTransaction entity);

        OrderLocationTransaction LoadOrderLocationTransaction(Int32 id);
  
        IList<OrderLocationTransaction> GetAllOrderLocationTransaction();
  
        void UpdateOrderLocationTransaction(OrderLocationTransaction entity);
        
        void DeleteOrderLocationTransaction(Int32 id);
    
        void DeleteOrderLocationTransaction(OrderLocationTransaction entity);
    
        void DeleteOrderLocationTransaction(IList<Int32> pkList);
    
        void DeleteOrderLocationTransaction(IList<OrderLocationTransaction> entityList);    
        #endregion Method Created By CodeSmith
    }
}
