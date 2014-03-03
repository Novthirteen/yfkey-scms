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
    public class OrderDetailViewBaseMgr : SessionBase, IOrderDetailViewBaseMgr
    {
        protected IOrderDetailViewDao entityDao;
        
        public OrderDetailViewBaseMgr(IOrderDetailViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderDetailView(OrderDetailView entity)
        {
            entityDao.CreateOrderDetailView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderDetailView LoadOrderDetailView(Int32 id)
        {
            return entityDao.LoadOrderDetailView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderDetailView> GetAllOrderDetailView()
        {
            return entityDao.GetAllOrderDetailView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderDetailView(OrderDetailView entity)
        {
            entityDao.UpdateOrderDetailView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetailView(Int32 id)
        {
            entityDao.DeleteOrderDetailView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetailView(OrderDetailView entity)
        {
            entityDao.DeleteOrderDetailView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetailView(IList<Int32> pkList)
        {
            entityDao.DeleteOrderDetailView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetailView(IList<OrderDetailView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderDetailView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
