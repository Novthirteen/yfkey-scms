using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Batch;
using Castle.Windsor;

namespace com.Sconit.Service.Batch
{
    [Serializable]
    public class JobRunContext
    {
        private readonly BatchTrigger trigger;
        private readonly BatchJobDetail jobDetail;
        private readonly JobDataMap jobDataMap;
        private readonly IWindsorContainer container;

        public JobRunContext(BatchTrigger trigger, BatchJobDetail jobDetail, JobDataMap jobDataMap, IWindsorContainer container)
        {
            this.trigger = trigger;
            this.jobDetail = jobDetail;
            this.jobDataMap = jobDataMap;
            this.container = container;
        }

        public BatchTrigger BatchTrigger
        {
            get
            {
                return trigger;
            }
        }

        public BatchJobDetail BatchJobDetail
        {
            get
            {
                return jobDetail;
            }
        }

        public JobDataMap JobDataMap
        {
            get
            {
                return jobDataMap;
            }
        }

        public IWindsorContainer Container
        {
            get
            {
                return container;
            }
        }
    }
}
