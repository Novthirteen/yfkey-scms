using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class CycleCountResultMgr : CycleCountResultBaseMgr, ICycleCountResultMgr
    {
        private ICriteriaMgr criteriaMgr;

        public CycleCountResultMgr(ICycleCountResultDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<CycleCountResult> GetCycleCountResult(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<CycleCountResult>();
            criteria.CreateAlias("CycleCount", "cc");
            criteria.Add(Expression.Eq("cc.Code", orderNo));

            return this.criteriaMgr.FindAll<CycleCountResult>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public void ClearOldCycleCountResult(string orderNo)
        {
            IList<CycleCountResult> cycleCountResultList = this.GetCycleCountResult(orderNo);
            if (cycleCountResultList != null && cycleCountResultList.Count > 0)
            {
                foreach (var item in cycleCountResultList)
                {
                    this.DeleteCycleCountResult(item.Id);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateCycleCountResult(IList<CycleCountResult> cycleCountResultList)
        {
            if (cycleCountResultList != null && cycleCountResultList.Count > 0)
            {
                foreach (var item in cycleCountResultList)
                {
                    this.CreateCycleCountResult(item);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void SaveCycleCountResult(string orderNo, IList<CycleCountResult> cycleCountResultList)
        {
            this.ClearOldCycleCountResult(orderNo);
            this.CreateCycleCountResult(cycleCountResultList);
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateCycleCountResult(IList<CycleCountResult> cycleCountResultList)
        {
            if (cycleCountResultList != null && cycleCountResultList.Count > 0)
            {
                foreach (var item in cycleCountResultList)
                {
                    this.UpdateCycleCountResult(item);
                }
            }
        }

        #endregion Customized Methods
    }
}