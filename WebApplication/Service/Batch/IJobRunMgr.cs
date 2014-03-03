using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;

namespace com.Sconit.Service.Batch
{
    public interface IJobRunMgr
    {
        void RunBatchJobs(IWindsorContainer container);

        void RunLeanEngineJob(IWindsorContainer container);
    }
}
