using com.Sconit.Service.Ext.MasterData;
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
using com.Sconit.Service.MRP;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class MRPJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.BatchJob");
        public IFlatBomMgr flatBomMgrE;

        public MRPJob(IFlatBomMgr flatBomMgrE)
        {
            this.flatBomMgrE = flatBomMgrE;
        }

        [Transaction(TransactionMode.Requires)]
        public void Execute(JobRunContext context)
        {
            flatBomMgrE.GenFlatBom(BusinessConstants.SYSTEM_USER_MONITOR);
        }
    }
}

