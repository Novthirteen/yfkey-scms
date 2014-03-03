using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using System.IO;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Production;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class WOBackflushJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.BatchJob");

        private IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr;
        private IUserMgr userMgr;
        #region 构造函数
        public WOBackflushJob(
          IProductLineInProcessLocationDetailMgr productLineInProcessLocationDetailMgr,
            IUserMgr userMgr)
        {
            this.productLineInProcessLocationDetailMgr = productLineInProcessLocationDetailMgr;
            this.userMgr = userMgr;
        }
        #endregion

        public void Execute(JobRunContext context)
        {

            string flowCode = context.JobDataMap.GetStringValue("FlowCode");
            if (flowCode != null && flowCode != string.Empty)
            {
                string[] flowCodeArray = flowCode.Split(',');
                foreach (string singleFlowCode in flowCodeArray)
                {
                    try
                    {
                        log.Info("Start backflush production line " + singleFlowCode);
                        TryRawMaterialBackflush(singleFlowCode);
                        log.Info("End backflush production line " + singleFlowCode);
                    }
                    catch (Exception ex)
                    {
                        log.Error("Backflush Error.", ex);
                    }
                }
            }
        }

        [Transaction(TransactionMode.RequiresNew)]
        public virtual void TryRawMaterialBackflush(string flowCode)
        {
            IList<ProductLineInProcessLocationDetail> productLineIpList = this.productLineInProcessLocationDetailMgr.GetProductLineInProcessLocationDetailGroupByItem(flowCode, BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
            if (productLineIpList != null && productLineIpList.Count > 0)
            {
                IDictionary<string, decimal> itemQtydic = new Dictionary<string, decimal>();
                foreach (ProductLineInProcessLocationDetail plIp in productLineIpList)
                {
                    itemQtydic.Add(plIp.Item.Code, 0);
                }
            
                this.productLineInProcessLocationDetailMgr.RawMaterialBackflush(flowCode, itemQtydic, userMgr.GetMonitorUser());
            }
        }
    }
}
