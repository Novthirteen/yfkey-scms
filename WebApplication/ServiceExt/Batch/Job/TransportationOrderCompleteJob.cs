using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Transportation;
using com.Sconit.Service.Hql;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;


namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class TransportationOrderCompleteJob : IJob
    {
        private ITransportationOrderMgr transportationOrderMgr;
        private IUserMgr userMgr;
        private IHqlMgr hqlMgr;
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.BatchJob");

        #region 构造函数
        public TransportationOrderCompleteJob(
            ITransportationOrderMgr transportationOrderMgr,
            IUserMgr userMgr,
            IHqlMgr hqlMgr)
        {
            this.transportationOrderMgr = transportationOrderMgr;
            this.userMgr = userMgr;
            this.hqlMgr = hqlMgr;
        }
        #endregion

        [Transaction(TransactionMode.Unspecified)]
        public void Execute(JobRunContext context)
        {
            string hql = @"select t.OrderNo from TransportationOrder as t where t.Status = ?";

            IList<string> orderNoList = hqlMgr.FindAll<string>(hql, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS);
            if (orderNoList != null && orderNoList.Count > 0)
            {
                foreach (string orderNo in orderNoList)
                {
                    try
                    {
                        string dhql = @"select d from TransportationOrderDetail as d where d.TransportationOrder.OrderNo = ?";

                        IList<TransportationOrderDetail> transportationOrderDetailList = hqlMgr.FindAll<TransportationOrderDetail>(dhql, orderNo);
                        var q = transportationOrderDetailList.Where(t => t.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE ||
                                                                         t.InProcessLocation.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS);
                        if (q.ToList() == null || q.ToList().Count == 0)
                        {
                            transportationOrderMgr.CompleteTransportationOrder(orderNo, userMgr.GetMonitorUser());
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error("CalculateFixPriceList Error.", ex);
                        continue;
                    }
                }
            }
        }
    }
}
