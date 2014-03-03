using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class CycleCountProcessJob : IJob
    {
        private ICycleCountMgr cycleCountMgr;
        private ICriteriaMgr criteriaMgr;
        private IUserMgr userMgr;

        #region 构造函数
        public CycleCountProcessJob(
            ICycleCountMgr cycleCountMgr, 
            ICriteriaMgr criteriaMgr, 
            IUserMgr userMgr)
        {
            this.cycleCountMgr = cycleCountMgr;
            this.criteriaMgr = criteriaMgr;
            this.userMgr = userMgr;
        }
        #endregion

        public void Execute(JobRunContext context)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(CycleCount));
            criteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
            criteria.Add(Expression.Not(Expression.Eq("CreateUser", "su")));
            IList<CycleCount> cycleCountList = criteriaMgr.FindAll<CycleCount>(criteria);

            if (cycleCountList.Count > 0)
            {
                int count = 0;
                foreach (CycleCount cycleCount in cycleCountList)
                {
                    if (count < 10)
                    {
                        cycleCountMgr.ProcessCycleCountResult(cycleCount.Code, userMgr.GetMonitorUser());
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        [Transaction(TransactionMode.RequiresNew)]
        public virtual void Process(CycleCount c)
        {
            cycleCountMgr.ProcessCycleCountResult(c.Code, userMgr.GetMonitorUser());
        }
    }
}
