using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Batch;
using com.Sconit.Entity.Batch;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch.Impl
{
    [Transactional]
    public class BatchTriggerMgr : BatchTriggerBaseMgr, IBatchTriggerMgr
    {       
        private ICriteriaMgr criteriaMgr;
        public BatchTriggerMgr(IBatchTriggerDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<BatchTrigger> GetTobeFiredTrigger()
        {
            DetachedCriteria criteria = DetachedCriteria.For<BatchTrigger>();

            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
            criteria.Add(Expression.Le("NextFireTime", DateTime.Now));

            return this.criteriaMgr.FindAll<BatchTrigger>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BatchTrigger> GetActiveTrigger()
        {
            DetachedCriteria criteria = DetachedCriteria.For<BatchTrigger>();

            criteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)));
            return this.criteriaMgr.FindAll<BatchTrigger>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public BatchTrigger LoadLeanEngineTrigger()
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BatchTrigger));
            criteria.Add(Expression.Eq("Name", "LeanEngineTrigger"));//todo,constants

            IList<BatchTrigger> result = criteriaMgr.FindAll<BatchTrigger>(criteria);
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        #endregion Customized Methods
    }
}