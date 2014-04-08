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
    public class RunBatJob : IJob
    {
        private IEDIMgr theEDIMgr;

        #region 构造函数
        public RunBatJob(
            IEDIMgr theEDIMgr)
        {
            this.theEDIMgr = theEDIMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            this.theEDIMgr.RunBat();
        }
    }
}
