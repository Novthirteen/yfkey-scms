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
    public class OrderHeadBaseMgr : SessionBase, IOrderHeadBaseMgr
    {
        protected IOrderHeadDao entityDao;
        
        public OrderHeadBaseMgr(IOrderHeadDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderHead(OrderHead entity)
        {
            entityDao.CreateOrderHead(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderHead LoadOrderHead(String orderNo)
        {
            return entityDao.LoadOrderHead(orderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderHead> GetAllOrderHead()
        {
            return entityDao.GetAllOrderHead();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderHead(OrderHead entity)
        {
            entityDao.UpdateOrderHead(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderHead(String orderNo)
        {
            entityDao.DeleteOrderHead(orderNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderHead(OrderHead entity)
        {
            entityDao.DeleteOrderHead(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderHead(IList<String> pkList)
        {
            entityDao.DeleteOrderHead(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderHead(IList<OrderHead> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderHead(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
