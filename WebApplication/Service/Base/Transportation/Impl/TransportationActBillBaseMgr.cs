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
    public class TransportationActBillBaseMgr : SessionBase, ITransportationActBillBaseMgr
    {
        protected ITransportationActBillDao entityDao;
        
        public TransportationActBillBaseMgr(ITransportationActBillDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationActBill(TransportationActBill entity)
        {
            entityDao.CreateTransportationActBill(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationActBill LoadTransportationActBill(Int32 id)
        {
            return entityDao.LoadTransportationActBill(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationActBill> GetAllTransportationActBill()
        {
            return entityDao.GetAllTransportationActBill();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationActBill(TransportationActBill entity)
        {
            entityDao.UpdateTransportationActBill(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationActBill(Int32 id)
        {
            entityDao.DeleteTransportationActBill(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationActBill(TransportationActBill entity)
        {
            entityDao.DeleteTransportationActBill(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationActBill(IList<Int32> pkList)
        {
            entityDao.DeleteTransportationActBill(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationActBill(IList<TransportationActBill> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationActBill(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
