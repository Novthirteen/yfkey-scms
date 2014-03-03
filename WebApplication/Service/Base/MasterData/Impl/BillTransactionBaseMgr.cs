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
    public class BillTransactionBaseMgr : SessionBase, IBillTransactionBaseMgr
    {
        protected IBillTransactionDao entityDao;
        
        public BillTransactionBaseMgr(IBillTransactionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBillTransaction(BillTransaction entity)
        {
            entityDao.CreateBillTransaction(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BillTransaction LoadBillTransaction(Int32 id)
        {
            return entityDao.LoadBillTransaction(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BillTransaction> GetAllBillTransaction()
        {
            return entityDao.GetAllBillTransaction();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBillTransaction(BillTransaction entity)
        {
            entityDao.UpdateBillTransaction(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillTransaction(Int32 id)
        {
            entityDao.DeleteBillTransaction(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillTransaction(BillTransaction entity)
        {
            entityDao.DeleteBillTransaction(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillTransaction(IList<Int32> pkList)
        {
            entityDao.DeleteBillTransaction(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillTransaction(IList<BillTransaction> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBillTransaction(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
