using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IOrderOperationBaseDao
    {
        #region Method Created By CodeSmith

        void CreateOrderOperation(OrderOperation entity);

        OrderOperation LoadOrderOperation(Int32 id);
  
        IList<OrderOperation> GetAllOrderOperation();
  
        void UpdateOrderOperation(OrderOperation entity);
        
        void DeleteOrderOperation(Int32 id);
    
        void DeleteOrderOperation(OrderOperation entity);
    
        void DeleteOrderOperation(IList<Int32> pkList);
    
        void DeleteOrderOperation(IList<OrderOperation> entityList);    
        
        OrderOperation LoadOrderOperation(com.Sconit.Entity.MasterData.OrderHead orderHead, Int32 operation);
    
        void DeleteOrderOperation(String orderHeadOrderNo, Int32 operation);
        
        OrderOperation LoadOrderOperation(String orderHeadOrderNo, Int32 operation);
        #endregion Method Created By CodeSmith
    }
}
