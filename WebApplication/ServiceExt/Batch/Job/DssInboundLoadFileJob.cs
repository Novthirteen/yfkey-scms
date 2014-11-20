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
    public class DssInboundLoadFileJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInboundLoadFile");

        private string defaultFileEncoding;
        private IDssFtpControlMgr dssFtpControlMgr;
        private IDssInboundControlMgr dssInboundControlMgr;

        public DssInboundLoadFileJob(string defaultFileEncoding,
            IDssFtpControlMgr dssFtpControlMgr,
            IDssInboundControlMgr dssInboundControlMgr)
        {
            this.defaultFileEncoding = defaultFileEncoding;
            this.dssFtpControlMgr = dssFtpControlMgr;
            this.dssInboundControlMgr = dssInboundControlMgr;
        }

        public void Execute(JobRunContext context)
        {
            try
            {
                DownloadFile();
            }
            catch (Exception ex)
            {
                log.Error("Download File Error.", ex);
            }

            try
            {
                ImportData(context.Container);
            }
            catch (Exception ex)
            {
                log.Error("Import Data Error.", ex);
            }
        }

        private void DownloadFile()
        {
            log.Info("Start download file from ftp according to FtpControl table.");
            IList<DssFtpControl> dssFtpControlList = this.dssFtpControlMgr.GetDssFtpControl(BusinessConstants.IO_TYPE_IN);

            if (dssFtpControlList != null && dssFtpControlList.Count > 0)
            {
                foreach (DssFtpControl dssFtpControl in dssFtpControlList)
                {
                    string ftpServer = string.Empty;
                    int ftpPort = 21;
                    string ftpInboundFolder = string.Empty;
                    string ftpUser = string.Empty;
                    string ftpPass = string.Empty;
                    string filePattern = string.Empty;
                    string localTempFolder = string.Empty;
                    string localFolder = string.Empty;
                    string ftpBackupFolder = string.Empty;
                    try
                    {
                        #region 获取ftp参数
                        ftpServer = dssFtpControl.FtpServer;
                        ftpPort = dssFtpControl.FtpPort.HasValue ? dssFtpControl.FtpPort.Value : 21;
                        ftpInboundFolder = dssFtpControl.FtpFolder;
                        ftpBackupFolder = dssFtpControl.FtpTempFolder;
                        ftpUser = dssFtpControl.FtpUser;
                        ftpPass = dssFtpControl.FtpPassword;
                        filePattern = dssFtpControl.FilePattern;
                        #endregion

                        #region 初始化本地目录
                        localTempFolder = dssFtpControl.LocalTempFolder;
                        localTempFolder = localTempFolder.Replace("\\", "/");
                        if (!localTempFolder.EndsWith("/"))
                        {
                            localTempFolder += "/";
                        }
                        if (!Directory.Exists(localTempFolder))
                        {
                            Directory.CreateDirectory(localTempFolder);
                        }

                        localFolder = dssFtpControl.LocalFolder;
                        localFolder = localFolder.Replace("\\", "/");
                        if (!localFolder.EndsWith("/"))
                        {
                            localFolder += "/";
                        }
                        if (!Directory.Exists(localFolder))
                        {
                            Directory.CreateDirectory(localFolder);
                        }
                        #endregion

                        #region 下载文件
                        FtpHelper ftp = new FtpHelper(ftpServer, ftpPort, ftpInboundFolder, ftpUser, ftpPass);
                        foreach (string fileName in ftp.GetFileList(filePattern))
                        {
                            try
                            {
                                ftp.Download(localTempFolder, fileName);
                                log.Info("Move file from folder: " + localTempFolder + fileName + " to folder: " + localFolder + fileName);
                                File.Move(localTempFolder + fileName, localFolder + fileName);
                                if (ftpBackupFolder != null && ftpBackupFolder.Length > 0)
                                {
                                    ftp.MovieFile(fileName, ftpBackupFolder);
                                }
                                else
                                {
                                    ftp.Delete(fileName);
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error("Download file:" + fileName, ex);
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Download files from ftpServer:" + ftpServer, ex);
                    }
                }
            }
            else
            {
                log.Info("No record found in FtpControl table.");
            }

            log.Info("End download file from ftp according to FtpControl table.");
        }

        private void ImportData(IWindsorContainer container)
        {
            log.Info("Start import data file according to DssInboundControl table.");
            IList<DssInboundControl> dssInboundControlList = this.dssInboundControlMgr.GetDssInboundControl();

            if (dssInboundControlList != null && dssInboundControlList.Count > 0)
            {
                foreach (DssInboundControl dssInboundControl in dssInboundControlList)
                {
                    string inFloder = dssInboundControl.InFloder;
                    string filePattern = dssInboundControl.FilePattern;
                    string serviceName = dssInboundControl.ServiceName;
                    string archiveFloder = dssInboundControl.ArchiveFloder;
                    string errorFloder = dssInboundControl.ErrorFloder;
                    string fileEncoding = dssInboundControl.FileEncoding;

                    if (fileEncoding == null || fileEncoding.Trim() == string.Empty)
                    {
                        fileEncoding = this.defaultFileEncoding;
                    }

                    log.Info("Start import data, floder: " + inFloder + ", filePattern: " + filePattern + ", serviceName: " + serviceName);

                    string[] files = null;
                    if (Directory.Exists(inFloder))
                    {
                        if (filePattern != null)
                        {
                            files = Directory.GetFiles(inFloder, filePattern);
                        }
                        else
                        {
                            files = Directory.GetFiles(inFloder);
                        }
                    }

                    if (files != null && files.Length > 0)
                    {
                        try
                        {
                            IInboundMgr processor = container.Resolve<IInboundMgr>(serviceName);
                            processor.ProcessInboundFile(dssInboundControl, files);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Process inbound error: ", ex);
                        }
                    }
                    else
                    {
                        log.Info("No files found to process.");
                    }
                }
            }
            else
            {
                log.Info("No record found in DssInboundControl table.");
            }

            log.Info("End import data file according to DssInboundControl table.");
        }
    }
}
