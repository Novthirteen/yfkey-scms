using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.Sconit.Entity.Dss;
using com.Sconit.Entity;
using com.Sconit.Utility;
using System.IO;
using Castle.Windsor;
using com.Sconit.Service.Dss;

namespace com.Sconit.Service.Batch.Job
{
    public class DssOutboundJob : IJob
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssOutbound");

        private string defaultFileEncoding;
        private IDssFtpControlMgr dssFtpControlMgr;
        private IDssOutboundControlMgr dssOutboundControlMgr;

        public DssOutboundJob(string defaultFileEncoding,
            IDssFtpControlMgr dssFtpControlMgr,
            IDssOutboundControlMgr dssOutboundControlMgr)
        {
            this.defaultFileEncoding = defaultFileEncoding;
            this.dssFtpControlMgr = dssFtpControlMgr;
            this.dssOutboundControlMgr = dssOutboundControlMgr;
        }

        public void Execute(JobRunContext context)
        {
            try
            {
                ExportData(context.Container);
            }
            catch (Exception ex)
            {
                log.Error("Export Data Error.", ex);
            }

            try
            {
                UploadFile();
            }
            catch (Exception ex)
            {
                log.Error("Upload File Error.", ex);
            }
        }

        private void ExportData(IWindsorContainer container)
        {
            IList<DssOutboundControl> dssOutboundControlList = this.dssOutboundControlMgr.GetDssOutboundControl();

            if (dssOutboundControlList != null && dssOutboundControlList.Count > 0)
            {
                foreach (DssOutboundControl dssOutboundControl in dssOutboundControlList)
                {
                    //string outFolder = dssOutboundControl.OutFolder;
                    string serviceName = dssOutboundControl.ServiceName;
                    //string archiveFolder = dssOutboundControl.ArchiveFolder;
                    //string tempFolder = dssOutboundControl.TempFolder;
                    //string filePrefix = dssOutboundControl.FilePrefix;
                    string fileEncoding = dssOutboundControl.FileEncoding;
                    if (fileEncoding == null || fileEncoding.Trim() == string.Empty)
                    {
                        dssOutboundControl.FileEncoding = this.defaultFileEncoding;
                    }

                    IOutboundMgr processor = container.Resolve<IOutboundMgr>(serviceName);
                    processor.ProcessOutbound(dssOutboundControl);
                }
            }
        }

        private void UploadFile()
        {
            log.Info("Start upload file to ftp according to FtpControl table.");
            IList<DssFtpControl> dssFtpControlList = this.dssFtpControlMgr.GetDssFtpControl(BusinessConstants.IO_TYPE_OUT);

            if (dssFtpControlList != null && dssFtpControlList.Count > 0)
            {
                foreach (DssFtpControl dssFtpControl in dssFtpControlList)
                {
                    string ftpServer = string.Empty;
                    int ftpPort = 21;
                    string ftpTempFolder = string.Empty;
                    string ftpFolder = string.Empty;
                    string ftpUser = string.Empty;
                    string ftpPass = string.Empty;
                    string filePattern = string.Empty;
                    string localFolder = string.Empty;
                    try
                    {
                        #region 获取参数
                        ftpServer = dssFtpControl.FtpServer;
                        ftpPort = dssFtpControl.FtpPort.HasValue ? dssFtpControl.FtpPort.Value : 21;
                        ftpTempFolder = dssFtpControl.FtpTempFolder;
                        ftpFolder = dssFtpControl.FtpFolder;
                        ftpUser = dssFtpControl.FtpUser;
                        ftpPass = dssFtpControl.FtpPassword;
                        filePattern = dssFtpControl.FilePattern;
                        localFolder = dssFtpControl.LocalFolder;
                        #endregion

                        #region 初始化远程目录
                        SocketFtpHelper ftp = new SocketFtpHelper(ftpServer, ftpUser, ftpPass, 60, ftpPort);
                        ftp.Login();
                        ftp.ChangeDir(ftpTempFolder);
                        ftp.BinaryMode = true;

                        ftpTempFolder = ftpTempFolder.Replace("\\", "/");
                        if (!ftpTempFolder.EndsWith("/"))
                        {
                            ftpTempFolder += "/";
                        }

                        try
                        {
                            //清空Temp目录
                            foreach (string fileName in ftp.GetFileList(filePattern))
                            {

                                ftp.DeleteFile(fileName);

                            }
                        }
                        catch (Exception)
                        {
                        }
                        //if (!ftp.DirectoryExist(ftpTempFolder))
                        //{
                        //    ftp.MakeDir(ftpTempFolder);
                        //}

                        ftpFolder = ftpFolder.Replace("\\", "/");
                        if (!ftpFolder.EndsWith("/"))
                        {
                            ftpFolder += "/";
                        }
                        //if (!ftp.DirectoryExist(ftpFolder))
                        //{
                        //    ftp.MakeDir(ftpFolder);
                        //}
                        #endregion

                        #region 获取本地上传文件列表
                        string[] files = null;
                        if (filePattern != null)
                        {
                            files = Directory.GetFiles(localFolder, filePattern);
                        }
                        else
                        {
                            files = Directory.GetFiles(localFolder);
                        }
                        #endregion

                        #region 上传文件
                        if (files != null && files.Length > 0)
                        {
                            foreach (string fileFullPath in files)
                            {
                                try
                                {
                                    string fomatedFileFullPath = fileFullPath.Replace("\\", "/");
                                    string fileName = fomatedFileFullPath.Substring(fomatedFileFullPath.LastIndexOf("/") + 1);
                                    ftp.Upload(fomatedFileFullPath);
                                    ftp.RenameFile(fileName, ftpFolder + fileName, true);
                                    log.Info("Delete file: " + fomatedFileFullPath);
                                    File.Delete(fomatedFileFullPath);
                                }
                                catch (Exception ex)
                                {
                                    log.Error("Upload file:" + fileFullPath, ex);
                                }
                            }
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Upload files from ftpServer:" + ftpServer, ex);
                    }
                }
            }
            else
            {
                log.Info("No record found in FtpControl table.");
            }

            log.Info("End upload file to ftp according to FtpControl table.");
        }
    }
}
