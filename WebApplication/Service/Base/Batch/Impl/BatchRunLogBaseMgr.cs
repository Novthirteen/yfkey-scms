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
    public class BatchRunLogBaseMgr : SessionBase, IBatchRunLogBaseMgr
    {
        protected IBatchRunLogDao entityDao;
        
        public BatchRunLogBaseMgr(IBatchRunLogDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBatchRunLog(BatchRunLog entity)
        {
            entityDao.CreateBatchRunLog(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BatchRunLog LoadBatchRunLog(Int32 id)
        {
            return entityDao.LoadBatchRunLog(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BatchRunLog> GetAllBatchRunLog()
        {
            return entityDao.GetAllBatchRunLog();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBatchRunLog(BatchRunLog entity)
        {
            entityDao.UpdateBatchRunLog(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchRunLog(Int32 id)
        {
            entityDao.DeleteBatchRunLog(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchRunLog(BatchRunLog entity)
        {
            entityDao.DeleteBatchRunLog(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchRunLog(IList<Int32> pkList)
        {
            entityDao.DeleteBatchRunLog(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchRunLog(IList<BatchRunLog> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBatchRunLog(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
