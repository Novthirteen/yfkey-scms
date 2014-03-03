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
    public class OrderDetailBaseMgr : SessionBase, IOrderDetailBaseMgr
    {
        protected IOrderDetailDao entityDao;
        
        public OrderDetailBaseMgr(IOrderDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateOrderDetail(OrderDetail entity)
        {
            entityDao.CreateOrderDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual OrderDetail LoadOrderDetail(Int32 id)
        {
            return entityDao.LoadOrderDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<OrderDetail> GetAllOrderDetail()
        {
            return entityDao.GetAllOrderDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateOrderDetail(OrderDetail entity)
        {
            entityDao.UpdateOrderDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetail(Int32 id)
        {
            entityDao.DeleteOrderDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetail(OrderDetail entity)
        {
            entityDao.DeleteOrderDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetail(IList<Int32> pkList)
        {
            entityDao.DeleteOrderDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteOrderDetail(IList<OrderDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteOrderDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
