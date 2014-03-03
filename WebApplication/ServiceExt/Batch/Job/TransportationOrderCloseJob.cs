using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Transportation;


namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class TransportationOrderCloseJob : IJob
    {
        private ITransportationOrderMgr transportationOrderMgr;

        #region 构造函数
        public TransportationOrderCloseJob(
            ITransportationOrderMgr transportationOrderMgr)
        {
            this.transportationOrderMgr = transportationOrderMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            transportationOrderMgr.TryCloseTransportationOrder();
        }
    }
}
