using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FlowPlanMgr : FlowPlanBaseMgr, IFlowPlanMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IFlowDetailMgr flowDetailMgr;

        public FlowPlanMgr(IFlowPlanDao entityDao,
            ICriteriaMgr criteriaMgr,
            IFlowDetailMgr flowDetailMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.flowDetailMgr = flowDetailMgr;
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}