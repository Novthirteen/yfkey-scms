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
    public class OrderBindingBaseMgr : SessionBase, IOrderBindingBaseMgr
    {
        protected IOrderBindingDao entityDao;
        
        public OrderBindingBaseMgr(IOrderBindingDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderBinding(OrderBinding entity)
        {
            entityDao.CreateOrderBinding(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderBinding LoadOrderBinding(Int32 id)
        {
            return entityDao.LoadOrderBinding(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderBinding> GetAllOrderBinding()
        {
            return entityDao.GetAllOrderBinding();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderBinding(OrderBinding entity)
        {
            entityDao.UpdateOrderBinding(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderBinding(Int32 id)
        {
            entityDao.DeleteOrderBinding(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderBinding(OrderBinding entity)
        {
            entityDao.DeleteOrderBinding(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderBinding(IList<Int32> pkList)
        {
            entityDao.DeleteOrderBinding(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderBinding(IList<OrderBinding> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderBinding(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
