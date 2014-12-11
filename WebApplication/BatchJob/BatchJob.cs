using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using BatchJob.Properties;
using com.Sconit.Utility;
using com.Sconit.Service.Batch;

namespace BatchJob
{
    partial class BatchJob : ServiceBase
    {
        private static log4net.ILog log;
        private static IWindsorContainer container = null;

        public System.Timers.Timer timer;
        public System.Timers.Timer leanEngineTimer;
        public System.Timers.Timer receiveWOTimer;

        public BatchJob()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("Log.BatchJob");
            this.ServiceName = "BatchJob";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                log.Info("BatchJob Service Start");
                container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));

                timer = new System.Timers.Timer();
                timer.Interval = Convert.ToDouble(TimerHelper.GetInterval(Settings.Default.IntervalType, 1));
                timer.Enabled = true;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

                leanEngineTimer = new System.Timers.Timer();
                leanEngineTimer.Interval = Convert.ToDouble(TimerHelper.GetInterval(Settings.Default.IntervalType, 1));
                leanEngineTimer.Enabled = true;
                leanEngineTimer.Elapsed += new System.Timers.ElapsedEventHandler(leanEngineTimer_Elapsed);

                receiveWOTimer = new System.Timers.Timer();
                receiveWOTimer.Interval = Convert.ToDouble(TimerHelper.GetInterval(Settings.Default.IntervalType, 1));
                receiveWOTimer.Enabled = true;
                receiveWOTimer.Elapsed += new System.Timers.ElapsedEventHandler(receiveWOTimer_Elapsed);
            }
            catch (Exception ex)
            {
                log.Error("BatchJob Service Start Failure", ex);
            }
        }

        protected override void OnStop()
        {
            log.Info("BatchJob Service Stop");
            if (container != null)
            {
                container.Dispose();
            }
        }

        private void RunJob()
        {
            try
            {
                IJobRunMgr jobRunMgr = container.Resolve<IJobRunMgr>("JobRunMgr.service");
                jobRunMgr.RunBatchJobs(container);
            }
            catch (Exception ex)
            {
                log.Error("Batch Job Run Failure", ex);
            }
        }

        private void RunLeanEngineJob()
        {
            try
            {
                IJobRunMgr jobRunMgr = container.Resolve<IJobRunMgr>("JobRunMgr.service");
                jobRunMgr.RunLeanEngineJob(container);
            }
            catch (Exception ex)
            {
                log.Error("Batch Job Run Failure", ex);
            }
        }

        private void RunReceiveWOJob()
        {
            try
            {
                IJobRunMgr jobRunMgr = container.Resolve<IJobRunMgr>("JobRunMgr.service");
                jobRunMgr.RunReceiveWOJob(container);
            }
            catch (Exception ex)
            {
                log.Error("Batch Job Run Failure", ex);
            }
        }


        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Enabled = !Settings.Default.InterruptTimer;
            RunJob();
            timer.Enabled = true;
        }

        private void leanEngineTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            leanEngineTimer.Enabled = !Settings.Default.InterruptTimer;
            RunLeanEngineJob();
            leanEngineTimer.Enabled = true;
        }

        private void receiveWOTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            receiveWOTimer.Enabled = !Settings.Default.InterruptTimer;
            RunReceiveWOJob();
            receiveWOTimer.Enabled = true;
        }
    }
}
