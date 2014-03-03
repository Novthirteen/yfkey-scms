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
    public class BillAddressBaseMgr : SessionBase, IBillAddressBaseMgr
    {
        protected IBillAddressDao entityDao;
        
        public BillAddressBaseMgr(IBillAddressDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBillAddress(BillAddress entity)
        {
            entityDao.CreateBillAddress(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BillAddress LoadBillAddress(String code)
        {
            return entityDao.LoadBillAddress(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BillAddress> GetAllBillAddress()
        {
            return entityDao.GetAllBillAddress(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BillAddress> GetAllBillAddress(bool includeInactive)
        {
            return entityDao.GetAllBillAddress(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBillAddress(BillAddress entity)
        {
            entityDao.UpdateBillAddress(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAddress(String code)
        {
            entityDao.DeleteBillAddress(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAddress(BillAddress entity)
        {
            entityDao.DeleteBillAddress(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAddress(IList<String> pkList)
        {
            entityDao.DeleteBillAddress(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBillAddress(IList<BillAddress> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBillAddress(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
