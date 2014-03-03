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
    public class TransportationOrderDetailBaseMgr : SessionBase, ITransportationOrderDetailBaseMgr
    {
        protected ITransportationOrderDetailDao entityDao;
        
        public TransportationOrderDetailBaseMgr(ITransportationOrderDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationOrderDetail(TransportationOrderDetail entity)
        {
            entityDao.CreateTransportationOrderDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationOrderDetail LoadTransportationOrderDetail(Int32 id)
        {
            return entityDao.LoadTransportationOrderDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationOrderDetail> GetAllTransportationOrderDetail()
        {
            return entityDao.GetAllTransportationOrderDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationOrderDetail(TransportationOrderDetail entity)
        {
            entityDao.UpdateTransportationOrderDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrderDetail(Int32 id)
        {
            entityDao.DeleteTransportationOrderDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrderDetail(TransportationOrderDetail entity)
        {
            entityDao.DeleteTransportationOrderDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrderDetail(IList<Int32> pkList)
        {
            entityDao.DeleteTransportationOrderDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationOrderDetail(IList<TransportationOrderDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationOrderDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
