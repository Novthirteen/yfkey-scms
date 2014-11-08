using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Batch;
using com.Sconit.Entity;
using System.Reflection;
using Castle.Windsor;
using com.Sconit.Utility;

namespace com.Sconit.Service.Batch.Impl
{
    public class JobRunMgr : IJobRunMgr
    {
        #region IJobRunMgr Members

        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.BatchJob");
        private static log4net.ILog lelog = log4net.LogManager.GetLogger("Log.BatchJobLeanEngine");

        private IBatchTriggerMgr batchTriggerMgr;
        private IBatchJobParameterMgr batchJobParameterMgr;
        private IBatchTriggerParameterMgr batchTriggerParameterMgr;
        private IBatchRunLogMgr batchRunLogMgr;

        public JobRunMgr(IBatchTriggerMgr batchTriggerMgr,
            IBatchJobParameterMgr batchJobParameterMgr,
            IBatchTriggerParameterMgr batchTriggerParameterMgr,
            IBatchRunLogMgr batchRunLogMgr)
        {
            this.batchTriggerMgr = batchTriggerMgr;
            this.batchJobParameterMgr = batchJobParameterMgr;
            this.batchTriggerParameterMgr = batchTriggerParameterMgr;
            this.batchRunLogMgr = batchRunLogMgr;
        }

        public void RunBatchJobs(IWindsorContainer container)
        {
            Run(container, false);
        }

        public void RunLeanEngineJob(IWindsorContainer container)
        {
            Run(container, true);
        }

        private void Run(IWindsorContainer container, bool isLeanEngine)
        {
            if (isLeanEngine)
            {
                lelog.Info("----------------------------------Invincible's dividing line---------------------------------------");
                lelog.Info("BatchJobs run start.");
            }
            else
            {
                log.Info("----------------------------------Invincible's dividing line---------------------------------------");
                log.Info("BatchJobs run start.");
            }

            IList<BatchTrigger> tobeFiredTriggerList = this.batchTriggerMgr.GetTobeFiredTrigger();

            if (tobeFiredTriggerList != null && tobeFiredTriggerList.Count > 0)
            {
                foreach (BatchTrigger tobeFiredTrigger in tobeFiredTriggerList)
                {
                    bool isSuccess = true;
                    if ((isLeanEngine && tobeFiredTrigger.Id != 2 && tobeFiredTrigger.Id != 23 && tobeFiredTrigger.Id != 7 && tobeFiredTrigger.Id != 50)
                        || (!isLeanEngine && (tobeFiredTrigger.Id == 2 || tobeFiredTrigger.Id == 23 || tobeFiredTrigger.Id == 7 || tobeFiredTrigger.Id == 50)))
                    {
                        continue;
                    }

                    BatchJobDetail jobDetail = tobeFiredTrigger.BatchJobDetail;
                    BatchRunLog runLog = new BatchRunLog();
                    try
                    {
                        #region Job运行前处理
                        if (isLeanEngine)
                        {
                            lelog.Info("Start run job. JobId:" + jobDetail.Id + ", JobName:" + jobDetail.Name);
                        }
                        else
                        {
                            log.Info("Start run job. JobId:" + jobDetail.Id + ", JobName:" + jobDetail.Name);
                        }
                        runLog.BatchJobDetail = jobDetail;
                        runLog.BatchTrigger = tobeFiredTrigger;
                        runLog.StartTime = DateTime.Now;
                        runLog.Status = "InProcess";
                        this.batchRunLogMgr.CreateBatchRunLog(runLog);
                        #endregion

                        #region 运行Job

                        JobDataMap dataMap = new JobDataMap();
                        #region Job参数获取
                        IList<BatchJobParameter> batchJobParameterList = this.batchJobParameterMgr.GetBatchJobParameter(jobDetail.Id);
                        if (batchJobParameterList != null && batchJobParameterList.Count > 0)
                        {
                            foreach (BatchJobParameter batchJobParameter in batchJobParameterList)
                            {
                                if (isLeanEngine)
                                {
                                    lelog.Debug("Set Job Parameter Name:" + batchJobParameter.ParameterName + ", Value:" + batchJobParameter.ParameterValue);
                                }
                                else
                                {
                                    log.Debug("Set Job Parameter Name:" + batchJobParameter.ParameterName + ", Value:" + batchJobParameter.ParameterValue);
                                }
                                dataMap.PutData(batchJobParameter.ParameterName, batchJobParameter.ParameterValue);
                            }
                        }
                        #endregion

                        #region Trigger参数获取
                        IList<BatchTriggerParameter> batchTriggerParameterList = this.batchTriggerParameterMgr.GetBatchTriggerParameter(tobeFiredTrigger.Id);
                        if (batchTriggerParameterList != null && batchTriggerParameterList.Count > 0)
                        {
                            foreach (BatchTriggerParameter batchTriggerParameter in batchTriggerParameterList)
                            {
                                if (isLeanEngine)
                                {
                                    lelog.Debug("Set Trigger Parameter Name:" + batchTriggerParameter.ParameterName + ", Value:" + batchTriggerParameter.ParameterValue);
                                }
                                else
                                {
                                    log.Debug("Set Trigger Parameter Name:" + batchTriggerParameter.ParameterName + ", Value:" + batchTriggerParameter.ParameterValue);
                                }
                                dataMap.PutData(batchTriggerParameter.ParameterName, batchTriggerParameter.ParameterValue);
                            }
                        }
                        #endregion

                        #region 初始化JobRunContext
                        JobRunContext jobRunContext = new JobRunContext(tobeFiredTrigger, jobDetail, dataMap, container);
                        #endregion

                        #region 调用Job



                        IJob job = container.Resolve<IJob>(jobDetail.ServiceName);
                        if (isLeanEngine)
                        {
                            lelog.Debug("Start run job: " + jobDetail.ServiceName);
                        }
                        else
                        {
                            log.Debug("Start run job: " + jobDetail.ServiceName);
                        }
                        job.Execute(jobRunContext);
                        #endregion

                        #endregion

                        #region Job运行后处理
                        if (isLeanEngine)
                        {
                            lelog.Info("Job run successful. JobId:" + jobDetail.Id + ", JobName:" + jobDetail.Name);
                        }
                        else
                        {
                            log.Info("Job run successful. JobId:" + jobDetail.Id + ", JobName:" + jobDetail.Name);
                        }
                        runLog.EndTime = DateTime.Now;
                        runLog.Status = "Successful";
                        this.batchRunLogMgr.UpdateBatchRunLog(runLog);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            this.batchTriggerMgr.CleanSession();
                            isSuccess = false;
                            if (isLeanEngine)
                            {
                                lelog.Error("Job run failure. JobId:" + jobDetail.Id + ", JobName:" + jobDetail.Name, ex);
                            }
                            else
                            {
                                log.Error("Job run failure. JobId:" + jobDetail.Id + ", JobName:" + jobDetail.Name, ex);
                            }
                            runLog.EndTime = DateTime.Now;
                            runLog.Status = "Failure";
                            if (ex.Message != null && ex.Message.Length > 255)
                            {
                                runLog.Message = ex.Message.Substring(0, 255);
                            }
                            else
                            {
                                runLog.Message = ex.Message;
                            }
                            this.batchRunLogMgr.UpdateBatchRunLog(runLog);
                        }
                        catch (Exception ex1)
                        {
                            if (isLeanEngine)
                            {
                                lelog.Error("", ex1);
                            }
                            else
                            {
                                log.Error("", ex1);
                            }
                        }
                    }
                    finally
                    {
                        #region 更新BatchTrigger
                        try
                        {

                            BatchTrigger oldTobeFiredTrigger = this.batchTriggerMgr.LoadBatchTrigger(tobeFiredTrigger.Id);
                            oldTobeFiredTrigger.TimesTriggered++;
                            oldTobeFiredTrigger.PreviousFireTime = oldTobeFiredTrigger.NextFireTime;
                            if (oldTobeFiredTrigger.RepeatCount != 0 && oldTobeFiredTrigger.TimesTriggered >= oldTobeFiredTrigger.RepeatCount)
                            {
                                //关闭Trigger
                                if (isLeanEngine)
                                {
                                    lelog.Debug("Close Trigger:" + oldTobeFiredTrigger.Name);
                                }
                                else
                                {
                                    log.Debug("Close Trigger:" + oldTobeFiredTrigger.Name);
                                }
                                oldTobeFiredTrigger.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                                oldTobeFiredTrigger.NextFireTime = null;
                            }
                            else
                            {
                                if (isSuccess)
                                {
                                    //设置下次运行时间
                                    if (isLeanEngine)
                                    {
                                        lelog.Debug("Set Trigger Next Start Time, Add:" + oldTobeFiredTrigger.Interval.ToString() + " " + oldTobeFiredTrigger.IntervalType);
                                    }
                                    else
                                    {
                                        log.Debug("Set Trigger Next Start Time, Add:" + oldTobeFiredTrigger.Interval.ToString() + " " + oldTobeFiredTrigger.IntervalType);
                                    }
                                    DateTime dateTimeNow = DateTime.Now;
                                    if (!oldTobeFiredTrigger.NextFireTime.HasValue)
                                    {
                                        oldTobeFiredTrigger.NextFireTime = dateTimeNow;
                                    }

                                    while (oldTobeFiredTrigger.NextFireTime.Value <= dateTimeNow)
                                    {
                                        if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_YEAR)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddYears(oldTobeFiredTrigger.Interval);
                                        }
                                        else if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_MONTH)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddMonths(oldTobeFiredTrigger.Interval);
                                        }
                                        else if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_DAY)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddDays(oldTobeFiredTrigger.Interval);
                                        }
                                        else if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_HOUR)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddHours(oldTobeFiredTrigger.Interval);
                                        }
                                        else if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_MINUTE)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddMinutes(oldTobeFiredTrigger.Interval);
                                        }
                                        else if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_SECOND)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddSeconds(oldTobeFiredTrigger.Interval);
                                        }
                                        else if (oldTobeFiredTrigger.IntervalType == BusinessConstants.DATETIME_TYPE_MILLISECOND)
                                        {
                                            oldTobeFiredTrigger.NextFireTime = oldTobeFiredTrigger.NextFireTime.Value.AddMilliseconds(oldTobeFiredTrigger.Interval);
                                        }
                                        else
                                        {
                                            throw new ArgumentException("invalid Interval Type:" + oldTobeFiredTrigger.IntervalType);
                                        }
                                    }
                                    if (isLeanEngine)
                                    {
                                        lelog.Debug("Trigger Next Start Time is set as:" + oldTobeFiredTrigger.NextFireTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                                    }
                                    else
                                    {
                                        log.Debug("Trigger Next Start Time is set as:" + oldTobeFiredTrigger.NextFireTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                                    }
                                }
                            }
                            this.batchTriggerMgr.UpdateBatchTrigger(oldTobeFiredTrigger);

                        }
                        catch (Exception ex)
                        {
                            if (isLeanEngine)
                            {
                                lelog.Error("Error occur when update batch trigger.", ex);
                            }
                            else
                            {
                                log.Error("Error occur when update batch trigger.", ex);
                            }
                        }
                        #endregion
                    }
                }
            }
            else
            {
                if (isLeanEngine)
                {
                    lelog.Info("No job found may run in this batch.");
                }
                else
                {
                    log.Info("No job found may run in this batch.");
                }
            }

            if (isLeanEngine)
            {
                lelog.Info("BatchJobs run end.");
            }
            else
            {
                log.Info("BatchJobs run end.");
            }
        }
        #endregion
    }
}
