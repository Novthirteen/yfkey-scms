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
    public class TPriceList2TActBillJob : IJob
    {
        private ITransportationActBillMgr transportationActBillMgr;
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.BatchJob");

        #region 构造函数
        public TPriceList2TActBillJob(
            ITransportationActBillMgr transportationActBillMgr)
        {
            this.transportationActBillMgr = transportationActBillMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            try
            {
                transportationActBillMgr.CalculateFixPriceList();
            }
            catch (Exception ex)
            {
                log.Error("CalculateFixPriceList Error.", ex);
            }
        }
    }
}
