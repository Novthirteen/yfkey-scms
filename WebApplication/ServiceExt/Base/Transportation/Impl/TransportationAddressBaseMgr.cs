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
    public class TransportationAddressBaseMgr : SessionBase, ITransportationAddressBaseMgr
    {
        public ITransportationAddressDao entityDao { get; set; }

        public TransportationAddressBaseMgr(ITransportationAddressDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationAddress(TransportationAddress entity)
        {
            entityDao.CreateTransportationAddress(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationAddress LoadTransportationAddress(Int32 id)
        {
            return entityDao.LoadTransportationAddress(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationAddress> GetAllTransportationAddress()
        {
            return entityDao.GetAllTransportationAddress();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationAddress(TransportationAddress entity)
        {
            entityDao.UpdateTransportationAddress(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationAddress(Int32 id)
        {
            entityDao.DeleteTransportationAddress(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationAddress(TransportationAddress entity)
        {
            entityDao.DeleteTransportationAddress(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationAddress(IList<Int32> pkList)
        {
            entityDao.DeleteTransportationAddress(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationAddress(IList<TransportationAddress> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationAddress(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
