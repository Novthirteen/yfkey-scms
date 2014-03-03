using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.Batch.Impl;
using com.Mes.Dss;
using com.Mes.Dss.Entity;
using com.Mes.Dss.Service;

namespace com.Sconit.Service.Batch.Job
{
    [Transactional]
    public class MesDssInJob : IJob
    {
        private IMesScmsTableIndexMgr mesScmsTableIndexMgr;
        public IMesDssInMgr mesDssInMgr { get; set; }
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssIn");
        #region 构造函数
        public MesDssInJob(IMesScmsTableIndexMgr mesScmsTableIndexMgr
            )
        {

            this.mesScmsTableIndexMgr = mesScmsTableIndexMgr;
        }
        #endregion

        [Transaction(TransactionMode.Requires)]
        public void Execute(JobRunContext context)
        {
            try
            {
                IList<MesScmsTableIndex> mesScmsTableIndexList = mesScmsTableIndexMgr.GetUpdateMesScmsTableIndex();
                if (mesScmsTableIndexList != null && mesScmsTableIndexList.Count > 0)
                {
                    foreach (MesScmsTableIndex mesScmsTableIndex in mesScmsTableIndexList)
                    {
                        //IMesDssInMgr mesDssInMgr = context.Container.Resolve<IMesDssInMgr>(context.JobDataMap.GetStringValue(mesScmsTableIndex.TableName.ToUpper()));
                        mesDssInMgr.ProcessIn(mesScmsTableIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Import Data Error.", ex);
            }
        }


    }
}
