using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IOrderDetailViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderDetailView(OrderDetailView entity);

        OrderDetailView LoadOrderDetailView(Int32 id);
  
        IList<OrderDetailView> GetAllOrderDetailView();
  
        void UpdateOrderDetailView(OrderDetailView entity);
        
        void DeleteOrderDetailView(Int32 id);
    
        void DeleteOrderDetailView(OrderDetailView entity);
    
        void DeleteOrderDetailView(IList<Int32> pkList);
    
        void DeleteOrderDetailView(IList<OrderDetailView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
