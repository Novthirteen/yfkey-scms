using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class CycleCountDetailMgr : CycleCountDetailBaseMgr, ICycleCountDetailMgr
    {
        private ICriteriaMgr criteriaMgr;
        public CycleCountDetailMgr(ICycleCountDetailDao entityDao,
            ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<CycleCountDetail> GetCycleCountDetail(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(CycleCountDetail));
            criteria.Add(Expression.Eq("CycleCount.Code", orderNo));

            return criteriaMgr.FindAll<CycleCountDetail>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public void CreateCycleCountDetail(CycleCount cycleCount, IList<CycleCountDetail> cycleCountDetailList)
        {
            if (cycleCountDetailList != null && cycleCountDetailList.Count > 0)
            {
                foreach (var item in cycleCountDetailList)
                {
                    item.CycleCount = cycleCount;
                    this.CreateCycleCountDetail(item);
                }
            }
        }
        #endregion Customized Methods
    }
}