using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class OrderOperationBaseMgr : SessionBase, IOrderOperationBaseMgr
    {
        protected IOrderOperationDao entityDao;
        
        public OrderOperationBaseMgr(IOrderOperationDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderOperation(OrderOperation entity)
        {
            entityDao.CreateOrderOperation(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderOperation LoadOrderOperation(Int32 id)
        {
            return entityDao.LoadOrderOperation(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderOperation> GetAllOrderOperation()
        {
            return entityDao.GetAllOrderOperation();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderOperation(OrderOperation entity)
        {
            entityDao.UpdateOrderOperation(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderOperation(Int32 id)
        {
            entityDao.DeleteOrderOperation(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderOperation(OrderOperation entity)
        {
            entityDao.DeleteOrderOperation(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderOperation(IList<Int32> pkList)
        {
            entityDao.DeleteOrderOperation(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderOperation(IList<OrderOperation> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderOperation(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderOperation LoadOrderOperation(com.Sconit.Entity.MasterData.OrderHead orderHead, Int32 operation)
        {
            return entityDao.LoadOrderOperation(orderHead, operation);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderOperation(String orderHeadOrderNo, Int32 operation)
        {
            entityDao.DeleteOrderOperation(orderHeadOrderNo, operation);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderOperation LoadOrderOperation(String orderHeadOrderNo, Int32 operation)
        {
            return entityDao.LoadOrderOperation(orderHeadOrderNo, operation);
        }
        #endregion Method Created By CodeSmith
    }
}
