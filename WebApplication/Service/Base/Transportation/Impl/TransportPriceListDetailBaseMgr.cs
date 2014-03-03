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
    public class TransportPriceListDetailBaseMgr : SessionBase, ITransportPriceListDetailBaseMgr
    {
        protected ITransportPriceListDetailDao entityDao;

        public TransportPriceListDetailBaseMgr(ITransportPriceListDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportPriceListDetail(TransportPriceListDetail entity)
        {
            entityDao.CreateTransportPriceListDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportPriceListDetail LoadTransportPriceListDetail(Int32 id)
        {
            return entityDao.LoadTransportPriceListDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportPriceListDetail> GetAllTransportPriceListDetail()
        {
            return entityDao.GetAllTransportPriceListDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportPriceListDetail(TransportPriceListDetail entity)
        {
            entityDao.UpdateTransportPriceListDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceListDetail(Int32 id)
        {
            entityDao.DeleteTransportPriceListDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceListDetail(TransportPriceListDetail entity)
        {
            entityDao.DeleteTransportPriceListDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceListDetail(IList<Int32> pkList)
        {
            entityDao.DeleteTransportPriceListDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceListDetail(IList<TransportPriceListDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportPriceListDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
