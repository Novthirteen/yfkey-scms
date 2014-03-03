using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class OrderCloseJob : IJob
    {
        private IOrderMgr orderMgr;

        #region 构造函数
        public OrderCloseJob(
            IOrderMgr orderMgr)
        {
            this.orderMgr = orderMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            orderMgr.TryCloseOrder();
        }
    }
}
