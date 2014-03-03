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
    public class TransportPriceListBaseMgr : SessionBase, ITransportPriceListBaseMgr
    {        
        protected ITransportPriceListDao entityDao;

        public TransportPriceListBaseMgr(ITransportPriceListDao entityDao)
        {
            this.entityDao = entityDao;
        }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportPriceList(TransportPriceList entity)
        {
            entityDao.CreateTransportPriceList(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportPriceList LoadTransportPriceList(String code)
        {
            return entityDao.LoadTransportPriceList(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportPriceList> GetAllTransportPriceList()
        {
            return entityDao.GetAllTransportPriceList(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportPriceList> GetAllTransportPriceList(bool includeInactive)
        {
            return entityDao.GetAllTransportPriceList(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportPriceList(TransportPriceList entity)
        {
            entityDao.UpdateTransportPriceList(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceList(String code)
        {
            entityDao.DeleteTransportPriceList(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceList(TransportPriceList entity)
        {
            entityDao.DeleteTransportPriceList(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceList(IList<String> pkList)
        {
            entityDao.DeleteTransportPriceList(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportPriceList(IList<TransportPriceList> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportPriceList(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
