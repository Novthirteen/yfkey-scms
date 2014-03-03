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
    public class BatchJobParameterBaseMgr : SessionBase, IBatchJobParameterBaseMgr
    {
        protected IBatchJobParameterDao entityDao;
        
        public BatchJobParameterBaseMgr(IBatchJobParameterDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBatchJobParameter(BatchJobParameter entity)
        {
            entityDao.CreateBatchJobParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BatchJobParameter LoadBatchJobParameter(Int32 id)
        {
            return entityDao.LoadBatchJobParameter(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BatchJobParameter> GetAllBatchJobParameter()
        {
            return entityDao.GetAllBatchJobParameter();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBatchJobParameter(BatchJobParameter entity)
        {
            entityDao.UpdateBatchJobParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobParameter(Int32 id)
        {
            entityDao.DeleteBatchJobParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobParameter(BatchJobParameter entity)
        {
            entityDao.DeleteBatchJobParameter(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobParameter(IList<Int32> pkList)
        {
            entityDao.DeleteBatchJobParameter(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchJobParameter(IList<BatchJobParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBatchJobParameter(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
