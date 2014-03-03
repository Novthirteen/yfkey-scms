using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.Batch.Impl;

using com.Sconit.Service.Mes;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class MesJob : IJob
    {
        private IMesMgr mesMgr;
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.Mes");
        #region 构造函数
        public MesJob(IMesMgr mesMgr)
        {

            this.mesMgr = mesMgr;
        }
        #endregion

        [Transaction(TransactionMode.Requires)]
        public void Execute(JobRunContext context)
        {
            try
            {
                mesMgr.RunMes();
            }
            catch (Exception ex)
            {
                log.Error("Mes Run Error.", ex);
            }
        }


    }
}
