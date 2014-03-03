using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IOrderLocTransViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderLocTransView(OrderLocTransView entity);

        OrderLocTransView LoadOrderLocTransView(Int32 id);
  
        IList<OrderLocTransView> GetAllOrderLocTransView();
  
        void UpdateOrderLocTransView(OrderLocTransView entity);
        
        void DeleteOrderLocTransView(Int32 id);
    
        void DeleteOrderLocTransView(OrderLocTransView entity);
    
        void DeleteOrderLocTransView(IList<Int32> pkList);
    
        void DeleteOrderLocTransView(IList<OrderLocTransView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
