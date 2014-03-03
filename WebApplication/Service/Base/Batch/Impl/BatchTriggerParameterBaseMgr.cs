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
    public class BatchTriggerParameterBaseMgr : SessionBase, IBatchTriggerParameterBaseMgr
    {
        protected IBatchTriggerParameterDao entityDao;
        
        public BatchTriggerParameterBaseMgr(IBatchTriggerParameterDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBatchTriggerParameter(BatchTriggerParameter entity)
        {
            entityDao.CreateBatchTriggerParameter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual BatchTriggerParameter LoadBatchTriggerParameter(Int32 id)
        {
            return entityDao.LoadBatchTriggerParameter(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<BatchTriggerParameter> GetAllBatchTriggerParameter()
        {
            return entityDao.GetAllBatchTriggerParameter();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBatchTriggerParameter(BatchTriggerParameter entity)
        {
            entityDao.UpdateBatchTriggerParameter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTriggerParameter(Int32 id)
        {
            entityDao.DeleteBatchTriggerParameter(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTriggerParameter(BatchTriggerParameter entity)
        {
            entityDao.DeleteBatchTriggerParameter(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTriggerParameter(IList<Int32> pkList)
        {
            entityDao.DeleteBatchTriggerParameter(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBatchTriggerParameter(IList<BatchTriggerParameter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBatchTriggerParameter(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
