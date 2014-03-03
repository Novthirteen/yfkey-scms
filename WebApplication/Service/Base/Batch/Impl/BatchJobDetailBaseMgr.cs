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
    public class BatchJobDetailBaseMgr : SessionBase, IBatchJobDetailBaseMgr
    {
        protected IBatchJobDetailDao entityDao;
        
        public BatchJobDetailBaseMgr(IBatchJobDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBatchJobDetail(BatchJobDetail entity)
        {
            entityDao.CreateBatchJobDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BatchJobDetail LoadBatchJobDetail(Int32 id)
        {
            return entityDao.LoadBatchJobDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BatchJobDetail> GetAllBatchJobDetail()
        {
            return entityDao.GetAllBatchJobDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBatchJobDetail(BatchJobDetail entity)
        {
            entityDao.UpdateBatchJobDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobDetail(Int32 id)
        {
            entityDao.DeleteBatchJobDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobDetail(BatchJobDetail entity)
        {
            entityDao.DeleteBatchJobDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobDetail(IList<Int32> pkList)
        {
            entityDao.DeleteBatchJobDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobDetail(IList<BatchJobDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBatchJobDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
