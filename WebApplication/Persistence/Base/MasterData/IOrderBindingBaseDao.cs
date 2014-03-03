using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IOrderBindingBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderBinding(OrderBinding entity);

        OrderBinding LoadOrderBinding(Int32 id);
  
        IList<OrderBinding> GetAllOrderBinding();
  
        void UpdateOrderBinding(OrderBinding entity);
        
        void DeleteOrderBinding(Int32 id);
    
        void DeleteOrderBinding(OrderBinding entity);
    
        void DeleteOrderBinding(IList<Int32> pkList);
    
        void DeleteOrderBinding(IList<OrderBinding> entityList);    
        #endregion Method Created By CodeSmith
    }
}
