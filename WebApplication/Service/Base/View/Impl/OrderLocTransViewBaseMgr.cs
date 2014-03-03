using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Persistence.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class OrderLocTransViewBaseMgr : SessionBase, IOrderLocTransViewBaseMgr
    {
        protected IOrderLocTransViewDao entityDao;
        
        public OrderLocTransViewBaseMgr(IOrderLocTransViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderLocTransView(OrderLocTransView entity)
        {
            entityDao.CreateOrderLocTransView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderLocTransView LoadOrderLocTransView(Int32 id)
        {
            return entityDao.LoadOrderLocTransView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderLocTransView> GetAllOrderLocTransView()
        {
            return entityDao.GetAllOrderLocTransView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderLocTransView(OrderLocTransView entity)
        {
            entityDao.UpdateOrderLocTransView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocTransView(Int32 id)
        {
            entityDao.DeleteOrderLocTransView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocTransView(OrderLocTransView entity)
        {
            entityDao.DeleteOrderLocTransView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocTransView(IList<Int32> pkList)
        {
            entityDao.DeleteOrderLocTransView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderLocTransView(IList<OrderLocTransView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderLocTransView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
