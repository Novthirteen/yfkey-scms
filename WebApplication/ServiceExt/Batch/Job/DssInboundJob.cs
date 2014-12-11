using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Utility;
using System.Collections;

using com.Sconit.Entity.Dss;
using com.Sconit.Entity;
using System.IO;
using Castle.Windsor;
using com.Sconit.Service.Dss;

namespace com.Sconit.Service.Batch.Job
{
    public class DssInboundJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IDssInboundControlMgr dssInboundControlMgr;

        public DssInboundJob(IDssInboundControlMgr dssInboundControlMgr)
        {
            this.dssInboundControlMgr = dssInboundControlMgr;
        }

        public void Execute(JobRunContext context)
        {
            try
            {
                ProcessData(context.Container);
            }
            catch (Exception ex)
            {
                log.Error("Import Data Error.", ex);
            }
        }

        private void ProcessData(IWindsorContainer container)
        {
            log.Info("Start process data according to DssInboundControl table.");
            IList<DssInboundControl> dssInboundControlList = this.dssInboundControlMgr.GetDssInboundControl();

            if (dssInboundControlList != null && dssInboundControlList.Count > 0)
            {
                foreach (DssInboundControl dssInboundControl in dssInboundControlList)
                {
                    string serviceName = dssInboundControl.ServiceName;
                    IInboundMgr processor = container.Resolve<IInboundMgr>(serviceName);
                    processor.ProcessInboundRecord(dssInboundControl);
                }
            }
            else
            {
                log.Info("No record found in DssInboundControl table.");
            }

            log.Info("End process data according to DssInboundControl table.");
        }

    }
}
