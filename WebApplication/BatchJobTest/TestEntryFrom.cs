using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using com.Sconit.Service.Batch;
using com.Sconit.Entity.Dss;

namespace BatchJobTest
{
    public partial class TestEntryFrom : Form
    {
        public TestEntryFrom()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IWindsorContainer container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            com.Sconit.Service.Dss.IOutboundMgr dssOutboundJob = container.Resolve<com.Sconit.Service.Dss.IOutboundMgr>("RctwoOutboundMgr.Service");
            com.Sconit.Service.Dss.IDssOutboundControlMgr dssOutboundControlMgr = container.Resolve<com.Sconit.Service.Dss.IDssOutboundControlMgr>("DssOutboundControlMgr.Service");
            DssOutboundControl dssOutboundControl = dssOutboundControlMgr.LoadDssOutboundControl(6);
            dssOutboundJob.ProcessOutbound(dssOutboundControl);
            //IJobRunMgr jobRunMgr = container.Resolve<IJobRunMgr>("JobRunMgr.service");
            //jobRunMgr.RunBatchJobs(container);
            container.Dispose();
        }
    }
}
