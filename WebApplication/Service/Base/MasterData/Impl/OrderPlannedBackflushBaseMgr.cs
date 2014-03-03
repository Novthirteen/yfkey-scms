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
    public class OrderPlannedBackflushBaseMgr : SessionBase, IOrderPlannedBackflushBaseMgr
    {
        protected IOrderPlannedBackflushDao entityDao;
        
        public OrderPlannedBackflushBaseMgr(IOrderPlannedBackflushDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderPlannedBackflush(OrderPlannedBackflush entity)
        {
            entityDao.CreateOrderPlannedBackflush(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderPlannedBackflush LoadOrderPlannedBackflush(Int32 id)
        {
            return entityDao.LoadOrderPlannedBackflush(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderPlannedBackflush> GetAllOrderPlannedBackflush()
        {
            return entityDao.GetAllOrderPlannedBackflush();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderPlannedBackflush(OrderPlannedBackflush entity)
        {
            entityDao.UpdateOrderPlannedBackflush(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderPlannedBackflush(Int32 id)
        {
            entityDao.DeleteOrderPlannedBackflush(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderPlannedBackflush(OrderPlannedBackflush entity)
        {
            entityDao.DeleteOrderPlannedBackflush(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderPlannedBackflush(IList<Int32> pkList)
        {
            entityDao.DeleteOrderPlannedBackflush(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderPlannedBackflush(IList<OrderPlannedBackflush> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderPlannedBackflush(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
