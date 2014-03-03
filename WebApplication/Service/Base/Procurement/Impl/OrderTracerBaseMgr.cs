using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class OrderTracerBaseMgr : SessionBase, IOrderTracerBaseMgr
    {
        protected IOrderTracerDao entityDao;
        
        public OrderTracerBaseMgr(IOrderTracerDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderTracer(OrderTracer entity)
        {
            entityDao.CreateOrderTracer(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderTracer LoadOrderTracer(Int32 id)
        {
            return entityDao.LoadOrderTracer(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderTracer> GetAllOrderTracer()
        {
            return entityDao.GetAllOrderTracer();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderTracer(OrderTracer entity)
        {
            entityDao.UpdateOrderTracer(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderTracer(Int32 id)
        {
            entityDao.DeleteOrderTracer(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderTracer(OrderTracer entity)
        {
            entityDao.DeleteOrderTracer(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderTracer(IList<Int32> pkList)
        {
            entityDao.DeleteOrderTracer(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderTracer(IList<OrderTracer> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderTracer(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
