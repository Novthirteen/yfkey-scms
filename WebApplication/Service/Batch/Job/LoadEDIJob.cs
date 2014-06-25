using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.EDI;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class LoadEDIJob : IJob
    {
        private IEDIMgr theEDIMgr;
        private IUserMgr userMgr;

        #region 构造函数
        public LoadEDIJob(
            IEDIMgr theEDIMgr, IUserMgr userMgr)
        {
            this.theEDIMgr = theEDIMgr;
            this.userMgr = userMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            User user= this.userMgr.GetMonitorUser();
            this.theEDIMgr.LoadEDI(user);
            this.theEDIMgr.TransformationPlan(user);
        }
    }
}
