using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Utility;
using System.IO;
using com.Sconit.Entity;
using com.Sconit.Entity.Dss;
using com.Sconit.Entity.Exception;
using Castle.Services.Transaction;

namespace com.Sconit.Service.Dss
{
    public abstract class AbstractInboundMgr : SessionBase, IInboundMgr
    {
        private static log4net.ILog logLoadFile = log4net.LogManager.GetLogger("Log.DssInboundLoadFile");
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");
        private IDssImportHistoryMgr dssImportHistoryMgr;

        public AbstractInboundMgr(IDssImportHistoryMgr dssImportHistoryMgr)
        {
            this.dssImportHistoryMgr = dssImportHistoryMgr;
        }

        public virtual void ProcessInboundFile(DssInboundControl dssInboundControl, string[] files)
        {
            logLoadFile.Info("Start process inbound ");

            //重新提交数据
            #region DataReader
            foreach (string fileName in files)
            {
                try
                {
                    IList<DssImportHistory> dssImportHistoryList = new List<DssImportHistory>();

                    #region 读取文件
                    logLoadFile.Info("Start load file " + fileName);
                    FlatFileReader reader = null;
                    try
                    {
                        DssImportHistory dssImportHistory = new DssImportHistory();
                        dssImportHistory.DssInboundCtrl = dssInboundControl;
                        dssImportHistory.IsActive = true;
                        dssImportHistory.KeyCode = Path.GetFileNameWithoutExtension(fileName);
                        dssImportHistory.CreateDate = DateTime.Now;

                        reader = this.DataReader(fileName, Encoding.GetEncoding(dssInboundControl.FileEncoding), "|");
                        for (string[] lineData = reader.ReadLine(); lineData != null; lineData = reader.ReadLine())
                        {
                            this.FillDssImportHistory(lineData, dssImportHistory);

                            if (dssImportHistory[0] == "0")
                            {
                                dssImportHistory.EventCode = BusinessConstants.DSS_EVENT_CODE_DELETE;
                                DssHelper.FormatDeleteData(lineData, BusinessConstants.DSS_SYSTEM_CODE_QAD);//QAD删除去引号
                            }
                            else
                            {
                                dssImportHistory.EventCode = BusinessConstants.DSS_EVENT_CODE_CREATE;
                            }
                        }

                        dssImportHistoryList.Add(dssImportHistory);
                    }
                    catch (Exception ex)
                    {
                        reader.Dispose();
                        logLoadFile.Error("Process inbound file: " + fileName + " Error.", ex);
                        throw ex;
                    }
                    finally
                    {
                        reader.Dispose();
                        logLoadFile.Info("Process inbound file: " + fileName + " successful.");
                    }
                    logLoadFile.Info("End load file " + fileName);
                    #endregion

                    #region CreateDssImportHistory
                    logLoadFile.Info("Start save file" + fileName);
                    CreateDssImportHistory(dssInboundControl, dssImportHistoryList, files);
                    logLoadFile.Info("End save file" + fileName);
                    #endregion

                    #region Archive download file
                    try
                    {
                        logLoadFile.Info("Start backup file " + fileName);
                        ArchiveFile(new string[] { fileName }, dssInboundControl.ArchiveFloder);
                        logLoadFile.Info("End backup file" + fileName);
                    }
                    catch (Exception ex)
                    {
                        logLoadFile.Error("Archive download file error:", ex);
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    logLoadFile.Error("Create DssImportHistory error:", ex);
                }
            }
            #endregion
        }

        public virtual void ProcessInboundRecord(DssInboundControl dssInboundControl)
        {
            #region 执行导入程序
            if (dssInboundControl.Id != 9)  //工单导入不通过程序执行，改为在存储过程中执行
            {
                log.Debug("Start process record for " + dssInboundControl.ServiceName);
                IList<DssImportHistory> activeDssImportHistoryList = dssImportHistoryMgr.GetActiveDssImportHistory(dssInboundControl.Id);
                if (activeDssImportHistoryList != null && activeDssImportHistoryList.Count > 0)
                {
                    log.Debug("total " + activeDssImportHistoryList.Count + " records.");
                    foreach (DssImportHistory activeDssImportHistory in activeDssImportHistoryList)
                    {
                        log.Debug("Start process record " + activeDssImportHistory.Id);
                        try
                        {
                            if (activeDssImportHistory.EventCode == BusinessConstants.DSS_EVENT_CODE_CREATE)
                            {
                                this.CreateOrUpdateObject(this.DeserializeForCreate(activeDssImportHistory));
                            }
                            else if (activeDssImportHistory.EventCode == BusinessConstants.DSS_EVENT_CODE_DELETE)
                            {
                                this.DeleteObject(this.DeserializeForDelete(activeDssImportHistory));
                            }

                            activeDssImportHistory.IsActive = false;
                            activeDssImportHistory.LastModifyDate = DateTime.Now;
                            activeDssImportHistory.LastModifyUser = "su";
                            this.dssImportHistoryMgr.Update(activeDssImportHistory);
                            this.dssImportHistoryMgr.FlushSession();
                        }
                        catch (Exception ex)
                        {
                            this.dssImportHistoryMgr.CleanSession();
                            activeDssImportHistory.ErrorCount++;
                            activeDssImportHistory.LastModifyDate = DateTime.Now;
                            activeDssImportHistory.LastModifyUser = "su";
                            this.dssImportHistoryMgr.Update(activeDssImportHistory);
                            logLoadFile.Error("Write to database error:", ex);
                        }
                        log.Debug("End process record " + activeDssImportHistory.Id);
                    }
                }

                log.Debug("End process record for " + dssInboundControl.ServiceName);
            }
            #endregion
        }

        public virtual void CreateDssImportHistory(DssInboundControl dssInboundControl, IList<DssImportHistory> dssImportHistoryList, string[] files)
        {
            #region Create DssImportHistory
            try
            {
                dssImportHistoryMgr.CreateDssImportHistory(dssImportHistoryList);
            }
            catch (Exception ex)
            {
                logLoadFile.Error("Write to database error:", ex);
                throw new BusinessErrorException("Write to database error.", ex);
            }
            #endregion
        }

        public virtual FlatFileReader DataReader(string fileName, Encoding enc, string delimiter)
        {
            return new FlatFileReader(fileName, enc, delimiter); ;
        }

        public virtual void ArchiveFile(IList<string> fileFullPaths, string archiveFloder)
        {
            if (fileFullPaths != null && fileFullPaths.Count > 0)
            {
                foreach (var fileFullPath in fileFullPaths)
                {
                    this.ArchiveFile(fileFullPath, archiveFloder);
                }
            }
        }
        public virtual void ArchiveFile(string fileFullPath, string archiveFloder)
        {
            string fomatedFileFullPath = fileFullPath.Replace("\\", "/");
            string fileName = fomatedFileFullPath.Substring(fomatedFileFullPath.LastIndexOf("/") + 1);

            logLoadFile.Info("Archive file : " + archiveFloder + fileName);
            archiveFloder = archiveFloder.Replace("\\", "/");
            if (!archiveFloder.EndsWith("/"))
            {
                archiveFloder += "/";
            }

            if (!Directory.Exists(archiveFloder))
            {
                Directory.CreateDirectory(archiveFloder);
            }

            if (File.Exists(archiveFloder + fileName))
            {
                File.Delete(archiveFloder + fileName);
            }

            File.Move(fileFullPath, archiveFloder + fileName);
        }

        public virtual IList<object> ProcessCreateData(IList<DssImportHistory> dssImportHistoryList)
        {
            var query = dssImportHistoryList
                .Where(d => StringHelper.Eq(d.EventCode, BusinessConstants.DSS_EVENT_CODE_CREATE))
                .ToList<DssImportHistory>();

            return this.DeserializeForCreate(query);
        }

        public virtual IList<object> ProcessDeleteData(IList<DssImportHistory> dssImportHistoryList)
        {
            var query = dssImportHistoryList
                .Where(d => StringHelper.Eq(d.EventCode, BusinessConstants.DSS_EVENT_CODE_DELETE))
                .ToList<DssImportHistory>();

            return this.DeserializeForDelete(query);
        }

        protected virtual IList<object> DeserializeForCreate(IList<DssImportHistory> dssImportHistoryList)
        {
            IList<object> objList = new List<object>();
            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                foreach (var item in dssImportHistoryList)
                {
                    try
                    {
                        object obj = this.DeserializeForCreate(item);
                        objList.Add(obj);
                    }
                    catch (Exception ex)
                    {
                        logLoadFile.Error("DeserializeForCreate error:" + ex.Message);
                        continue;
                    }
                }
            }
            return objList;
        }

        protected virtual IList<object> DeserializeForDelete(IList<DssImportHistory> dssImportHistoryList)
        {
            IList<object> objList = new List<object>();
            if (dssImportHistoryList != null && dssImportHistoryList.Count > 0)
            {
                foreach (var item in dssImportHistoryList)
                {
                    try
                    {
                        object obj = this.DeserializeForDelete(item);
                        objList.Add(obj);
                    }
                    catch (Exception ex)
                    {
                        logLoadFile.Error("DeserializeForDelete error:" + ex);
                        continue;
                    }
                }
            }
            return objList;
        }

        protected virtual void CreateOrUpdateObject(IList<object> objList)
        {
            if (objList != null && objList.Count > 0)
            {

                foreach (var obj in objList)
                {
                    try
                    {
                        this.CreateOrUpdateObject(obj);
                    }
                    catch (Exception ex)
                    {
                        this.dssImportHistoryMgr.CleanSession();
                        logLoadFile.Error("DeserializeForDelete error:" + ex);
                        continue;
                    }
                }

            }
        }

        protected virtual void DeleteObject(IList<object> objList)
        {
            if (objList != null && objList.Count > 0)
            {
                foreach (var obj in objList)
                {
                    try
                    {
                        this.DeleteObject(obj);
                    }
                    catch (Exception ex)
                    {
                        this.dssImportHistoryMgr.CleanSession();
                        logLoadFile.Error("DeserializeForDelete error:" + ex);
                        continue;
                    }
                }
            }
        }

        protected virtual void FillDssImportHistory(string[] lineData, DssImportHistory dssImportHistory)
        {
            if (lineData != null && lineData.Length > 0 && dssImportHistory != null)
            {
                for (int i = 0; i < lineData.Length; i++)
                {
                    if (lineData[i] == "?")
                        lineData[i] = null;
                    else
                        dssImportHistory[i] = lineData[i];
                    //log.Debug("Read Data[" + i + "]: " + lineData[i]);
                }
            }
        }

        protected abstract object DeserializeForCreate(DssImportHistory dssImportHistory);
        protected abstract object DeserializeForDelete(DssImportHistory dssImportHistory);

        protected abstract void CreateOrUpdateObject(object obj);
        protected abstract void DeleteObject(object obj);

        public void DoAsyncProcessDssInboundRecord(DssInboundControl dssInboundControl)
        {
            AsyncProcessDssInboundRecord asyncProcessDssInboundRecord = new AsyncProcessDssInboundRecord(this.ProcessDssInboundRecord);
            asyncProcessDssInboundRecord.BeginInvoke(dssInboundControl, null, null);
        }

        public delegate void AsyncProcessDssInboundRecord(DssInboundControl dssInboundControl);

        private static object ProcessDssInboundRecordLock = new object();
        public void ProcessDssInboundRecord(DssInboundControl dssInboundControl)
        {
            lock (ProcessDssInboundRecordLock)
            {
                IList<DssImportHistory> activeDssImportHistoryList = dssImportHistoryMgr.GetActiveDssImportHistory(dssInboundControl.Id);

                IList<object> objCreate = this.ProcessCreateData(activeDssImportHistoryList);
                IList<object> objDelete = this.ProcessDeleteData(activeDssImportHistoryList);

                try
                {
                    this.CreateOrUpdateObject(objCreate);
                    this.DeleteObject(objDelete);
                }
                catch (Exception ex)
                {
                    logLoadFile.Error("Write to database error:", ex);
                }
            }
        }
    }
}
