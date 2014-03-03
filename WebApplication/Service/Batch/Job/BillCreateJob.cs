using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class BillCreateJob : IJob
    {
        private IBillMgr billMgr;

        #region 构造函数
        public BillCreateJob(
            IBillMgr billMgr)
        {
            this.billMgr = billMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            string customers = context.JobDataMap.GetStringValue("Customers");

            this.billMgr.TryCreateBill(customers);
        }
    }
}
