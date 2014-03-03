using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Batch;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Batch;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch.Impl
{
    [Transactional]
    public class BatchTriggerParameterMgr : BatchTriggerParameterBaseMgr, IBatchTriggerParameterMgr
    {
        private ICriteriaMgr criteriaMgr;
        public BatchTriggerParameterMgr(IBatchTriggerParameterDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<BatchTriggerParameter> GetBatchTriggerParameter(int jobId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<BatchTriggerParameter>();

            criteria.Add(Expression.Eq("BatchTrigger.Id", jobId));

            return this.criteriaMgr.FindAll<BatchTriggerParameter>(criteria);
        }

        #endregion Customized Methods
    }
}