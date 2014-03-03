using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.Batch.Impl;
using com.Mes.Dss;
using com.Mes.Dss.Entity;
using com.Mes.Dss.Service;
using com.Sconit.Entity;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class MesDssOutJob : IJob
    {

        private IScmsTableIndexMgr scmsTableIndexMgr;
        public IMesDssOutMgr mesDssOutMgr { get; set; }
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssOut");
        #region 构造函数
        public MesDssOutJob(IScmsTableIndexMgr scmsTableIndexMgr)
        {
            this.scmsTableIndexMgr = scmsTableIndexMgr;
        }
        #endregion

        [Transaction(TransactionMode.Requires)]
        public void Execute(JobRunContext context)
        {
            try
            {
                IList<ScmsTableIndex> scmsTableIndexList = scmsTableIndexMgr.GetUpdateScmsTableIndex();
                if (scmsTableIndexList != null && scmsTableIndexList.Count > 0)
                {
                    foreach (ScmsTableIndex scmsTableIndex in scmsTableIndexList)
                    {
                        //IMesDssOutMgr mesDssOutMgr = context.Container.Resolve<IMesDssOutMgr>(context.JobDataMap.GetStringValue(scmsTableIndex.TableName.ToUpper()));
                        
                        mesDssOutMgr.ProcessOut(scmsTableIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Export Data Error.", ex);
            }
        }


    }
}
