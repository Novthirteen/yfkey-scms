using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Batch.Job
{
    public class OrderCompleteJob : IJob
    {
         private IOrderMgr orderMgr;

        #region 构造函数
         public OrderCompleteJob(
            IOrderMgr orderMgr)
        {
            this.orderMgr = orderMgr;
        }
        #endregion

         [Transaction(TransactionMode.Unspecified)]
         public void Execute(JobRunContext context)
         {
             string flowCode = context.JobDataMap.GetStringValue("FlowCode");
             if (flowCode != null && flowCode != string.Empty)
             {
                 string[] flowCodeArray = flowCode.Split(',');
                 orderMgr.TryCompleteOrder(flowCodeArray);
             }
         }
    }
}
