using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Batch;
using com.Sconit.Persistence.Batch;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch.Impl
{
    [Transactional]
    public class BatchTriggerBaseMgr : SessionBase, IBatchTriggerBaseMgr
    {
        protected IBatchTriggerDao entityDao;
        
        public BatchTriggerBaseMgr(IBatchTriggerDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBatchTrigger(BatchTrigger entity)
        {
            entityDao.CreateBatchTrigger(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BatchTrigger LoadBatchTrigger(Int32 id)
        {
            return entityDao.LoadBatchTrigger(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BatchTrigger> GetAllBatchTrigger()
        {
            return entityDao.GetAllBatchTrigger();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBatchTrigger(BatchTrigger entity)
        {
            entityDao.UpdateBatchTrigger(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTrigger(Int32 id)
        {
            entityDao.DeleteBatchTrigger(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTrigger(BatchTrigger entity)
        {
            entityDao.DeleteBatchTrigger(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTrigger(IList<Int32> pkList)
        {
            entityDao.DeleteBatchTrigger(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTrigger(IList<BatchTrigger> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBatchTrigger(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
