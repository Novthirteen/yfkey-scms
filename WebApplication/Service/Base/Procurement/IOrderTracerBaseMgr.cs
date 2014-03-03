using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface IOrderTracerBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateOrderTracer(OrderTracer entity);

        OrderTracer LoadOrderTracer(Int32 id);

        IList<OrderTracer> GetAllOrderTracer();
    
        void UpdateOrderTracer(OrderTracer entity);

        void DeleteOrderTracer(Int32 id);
    
        void DeleteOrderTracer(OrderTracer entity);
    
        void DeleteOrderTracer(IList<Int32> pkList);
    
        void DeleteOrderTracer(IList<OrderTracer> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
