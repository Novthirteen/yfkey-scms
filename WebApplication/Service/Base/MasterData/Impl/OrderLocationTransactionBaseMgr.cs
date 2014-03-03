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
    public class OrderLocationTransactionBaseMgr : SessionBase, IOrderLocationTransactionBaseMgr
    {
        protected IOrderLocationTransactionDao entityDao;
        
        public OrderLocationTransactionBaseMgr(IOrderLocationTransactionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderLocationTransaction(OrderLocationTransaction entity)
        {
            entityDao.CreateOrderLocationTransaction(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderLocationTransaction LoadOrderLocationTransaction(Int32 id)
        {
            return entityDao.LoadOrderLocationTransaction(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderLocationTransaction> GetAllOrderLocationTransaction()
        {
            return entityDao.GetAllOrderLocationTransaction();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderLocationTransaction(OrderLocationTransaction entity)
        {
            entityDao.UpdateOrderLocationTransaction(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocationTransaction(Int32 id)
        {
            entityDao.DeleteOrderLocationTransaction(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocationTransaction(OrderLocationTransaction entity)
        {
            entityDao.DeleteOrderLocationTransaction(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocationTransaction(IList<Int32> pkList)
        {
            entityDao.DeleteOrderLocationTransaction(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocationTransaction(IList<OrderLocationTransaction> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderLocationTransaction(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
