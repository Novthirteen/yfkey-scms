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
    public class ReceiptInProcessLocationBaseMgr : SessionBase, IReceiptInProcessLocationBaseMgr
    {
        protected IReceiptInProcessLocationDao entityDao;
        
        public ReceiptInProcessLocationBaseMgr(IReceiptInProcessLocationDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateReceiptInProcessLocation(ReceiptInProcessLocation entity)
        {
            entityDao.CreateReceiptInProcessLocation(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ReceiptInProcessLocation LoadReceiptInProcessLocation(Int32 id)
        {
            return entityDao.LoadReceiptInProcessLocation(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ReceiptInProcessLocation> GetAllReceiptInProcessLocation()
        {
            return entityDao.GetAllReceiptInProcessLocation();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateReceiptInProcessLocation(ReceiptInProcessLocation entity)
        {
            entityDao.UpdateReceiptInProcessLocation(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptInProcessLocation(Int32 id)
        {
            entityDao.DeleteReceiptInProcessLocation(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptInProcessLocation(ReceiptInProcessLocation entity)
        {
            entityDao.DeleteReceiptInProcessLocation(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptInProcessLocation(IList<Int32> pkList)
        {
            entityDao.DeleteReceiptInProcessLocation(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteReceiptInProcessLocation(IList<ReceiptInProcessLocation> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteReceiptInProcessLocation(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
