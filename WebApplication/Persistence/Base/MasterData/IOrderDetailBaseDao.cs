using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IOrderDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderDetail(OrderDetail entity);

        OrderDetail LoadOrderDetail(Int32 id);
  
        IList<OrderDetail> GetAllOrderDetail();
  
        void UpdateOrderDetail(OrderDetail entity);
        
        void DeleteOrderDetail(Int32 id);
    
        void DeleteOrderDetail(OrderDetail entity);
    
        void DeleteOrderDetail(IList<Int32> pkList);
    
        void DeleteOrderDetail(IList<OrderDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
