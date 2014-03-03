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
    public class TransportationBillBaseMgr : SessionBase, ITransportationBillBaseMgr
    {
        protected ITransportationBillDao entityDao;
        
        public TransportationBillBaseMgr(ITransportationBillDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationBill(TransportationBill entity)
        {
            entityDao.CreateTransportationBill(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationBill LoadTransportationBill(String billNo)
        {
            return entityDao.LoadTransportationBill(billNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationBill> GetAllTransportationBill()
        {
            return entityDao.GetAllTransportationBill();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationBill(TransportationBill entity)
        {
            entityDao.UpdateTransportationBill(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBill(String billNo)
        {
            entityDao.DeleteTransportationBill(billNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBill(TransportationBill entity)
        {
            entityDao.DeleteTransportationBill(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBill(IList<String> pkList)
        {
            entityDao.DeleteTransportationBill(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBill(IList<TransportationBill> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationBill(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
