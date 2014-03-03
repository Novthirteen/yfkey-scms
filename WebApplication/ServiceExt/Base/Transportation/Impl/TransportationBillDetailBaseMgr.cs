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
    public class TransportationBillDetailBaseMgr : SessionBase, ITransportationBillDetailBaseMgr
    {
        protected ITransportationBillDetailDao entityDao;
        
        public TransportationBillDetailBaseMgr(ITransportationBillDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateTransportationBillDetail(TransportationBillDetail entity)
        {
            entityDao.CreateTransportationBillDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationBillDetail LoadTransportationBillDetail(Int32 id)
        {
            return entityDao.LoadTransportationBillDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<TransportationBillDetail> GetAllTransportationBillDetail()
        {
            return entityDao.GetAllTransportationBillDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateTransportationBillDetail(TransportationBillDetail entity)
        {
            entityDao.UpdateTransportationBillDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBillDetail(Int32 id)
        {
            entityDao.DeleteTransportationBillDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBillDetail(TransportationBillDetail entity)
        {
            entityDao.DeleteTransportationBillDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBillDetail(IList<Int32> pkList)
        {
            entityDao.DeleteTransportationBillDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBillDetail(IList<TransportationBillDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteTransportationBillDetail(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual TransportationBillDetail LoadTransportationBillDetail(String bill, Int32 tActingBill)
        {
            return entityDao.LoadTransportationBillDetail(bill, tActingBill);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteTransportationBillDetail(String bill, Int32 tActingBill)
        {
            entityDao.DeleteTransportationBillDetail(bill, tActingBill);
        }   
        #endregion Method Created By CodeSmith
    }
}
