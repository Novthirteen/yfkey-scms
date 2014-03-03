using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Transportation;
using com.Sconit.Persistence.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation.Impl
{
    [Transactional]
    public class TransportationOrderBaseMgr : SessionBase, ITransportationOrderBaseMgr
    {
        protected ITransportationOrderDao entityDao;
        
        public TransportationOrderBaseMgr(ITransportationOrderDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationOrder(TransportationOrder entity)
        {
            entityDao.CreateTransportationOrder(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationOrder LoadTransportationOrder(String orderNo)
        {
            return entityDao.LoadTransportationOrder(orderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationOrder> GetAllTransportationOrder()
        {
            return entityDao.GetAllTransportationOrder();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationOrder(TransportationOrder entity)
        {
            entityDao.UpdateTransportationOrder(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrder(String orderNo)
        {
            entityDao.DeleteTransportationOrder(orderNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrder(TransportationOrder entity)
        {
            entityDao.DeleteTransportationOrder(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrder(IList<String> pkList)
        {
            entityDao.DeleteTransportationOrder(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrder(IList<TransportationOrder> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationOrder(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
