using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IOrderHeadBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderHead(OrderHead entity);

        OrderHead LoadOrderHead(String orderNo);
  
        IList<OrderHead> GetAllOrderHead();
  
        void UpdateOrderHead(OrderHead entity);
        
        void DeleteOrderHead(String orderNo);
    
        void DeleteOrderHead(OrderHead entity);
    
        void DeleteOrderHead(IList<String> pkList);
    
        void DeleteOrderHead(IList<OrderHead> entityList);    
        #endregion Method Created By CodeSmith
    }
}
