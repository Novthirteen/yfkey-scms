using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Batch;
using com.Sconit.Entity.Batch;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch.Impl
{
    [Transactional]
    public class BatchJobParameterMgr : BatchJobParameterBaseMgr, IBatchJobParameterMgr
    {
        private ICriteriaMgr criteriaMgr;
        public BatchJobParameterMgr(IBatchJobParameterDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<BatchJobParameter> GetBatchJobParameter(int jobId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BatchJobParameter>();

            criteria.Add(Expression.Eq("BatchJobDetail.Id", jobId));

            return this.criteriaMgr.FindAll<BatchJobParameter>(criteria);
        }

        #endregion Customized Methods
    }
}