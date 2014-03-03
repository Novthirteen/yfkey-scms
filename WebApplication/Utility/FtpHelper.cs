using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using com.Sconit.Entity.Exception;

namespace com.Sconit.Utility
{

    public class FtpHelper
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.Ftp");

        string ftpServer;
        int ftpPort;
        string ftpRemoteDirectory;
        string ftpUser;
        string ftpPassword;
        string ftpURI;

        /// <summary>
        /// 连接FTP
        /// </summary>
        /// <param name="ftpServer">FTP连接地址</param>
        /// <param name="ftpRemoteDirectory">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="ftpUser">用户名</param>
        /// <param name="ftpPassword">密码</param>
        public FtpHelper(string ftpServer, string ftpRemoteDirectory, string ftpUser, string ftpPassword)
            : this(ftpServer, 21, ftpRemoteDirectory, ftpUser, ftpPassword)
        {
        }

        /// <summary>
        /// 连接FTP
        /// </summary>
        /// <param name="ftpServer">FTP连接地址</param>
        /// <param name="ftpPort">FTP端口</param>
        /// <param name="ftpRemoteDirectory">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="ftpUser">用户名</param>
        /// <param name="ftpPassword">密码</param>
        public FtpHelper(string ftpServer, int ftpPort, string ftpRemoteDirectory, string ftpUser, string ftpPassword)
        {
            this.ftpServer = ftpServer;
            this.ftpPort = ftpPort;
            this.ftpRemoteDirectory = ftpRemoteDirectory != null ? ftpRemoteDirectory : string.Empty;
            this.ftpUser = ftpUser;
            this.ftpPassword = ftpPassword;
            this.ftpURI = "ftp://" + this.ftpServer + ":" + this.ftpPort + "/";
            if (this.ftpRemoteDirectory != string.Empty)
            {
                this.ftpURI += this.ftpRemoteDirectory + "/";
            }
        }

        // <summary>
        /// 上传
        /// </summary>
        /// <param name="filename"></param>
        public void Upload(string filename)
        {
            log.Info("Start upload file: " + filename);
            FileInfo fileInf = new FileInfo(filename);
            string uri = ftpURI + fileInf.Name;
            FtpWebRequest reqFTP;

            log.Info("Start connect ftp server: " + ftpURI);
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            reqFTP.UseBinary = true;
            reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = fileInf.OpenRead();
            try
            {
                Stream strm = reqFTP.GetRequestStream();
                contentLen = fs.Read(buff, 0, buffLength);
                while (contentLen != 0)
                {
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                strm.Close();
                fs.Close();
                log.Info("Upload file: " + filename + " successful.");
            }
            catch (Exception ex)
            {
                log.Error("Upload file: " + filename + " error.", ex);
                throw new TechnicalException("Upload file: " + filename + " error.", ex);
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                log.Info("Start download file: " + ftpURI + fileName + ", to local directory: " + filePath);
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                log.Info("Start connect ftp server: " + ftpURI);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 1024;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                log.Info("Download file: " + ftpURI + fileName + ", to local directory: " + filePath + " successful.");
            }
            catch (Exception ex)
            {
                log.Error("Download file: " + ftpURI + fileName + ", to local directory: " + filePath + " error.", ex);
                throw new TechnicalException("Download file: " + ftpURI + fileName + ", to local directory: " + filePath + " error.", ex);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete(string fileName)
        {
            try
            {
                log.Info("Start delete file: " + ftpURI + fileName);
                string uri = ftpURI + fileName;

                log.Info("Start connect ftp server: " + ftpURI);
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
                log.Info("Delete file: " + ftpURI + fileName + " successful.");
            }
            catch (Exception ex)
            {
                log.Error("Delete file: " + ftpURI + fileName + " error.", ex);
                throw new TechnicalException("Delete file: " + ftpURI + fileName + " error.", ex);
            }
        }

        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            try
            {
                log.Info("Start get files detail list.");
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;

                log.Info("Start connect ftp server: " + ftpURI);
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                ftp.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();

                log.Info("Get files detail list successful.");
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                log.Error("Get Files Detail List error.", ex);
                throw new TechnicalException("Get Files Detail List error.", ex);
            }
        }

        public string[] GetFileList()
        {
            return GetFileList(null);
        }

        /// <summary>
        /// 获取当前目录下文件列表(仅文件)
        /// </summary>
        /// <returns></returns>
        public string[] GetFileList(string pattern)
        {
            try
            {
                log.Info("Start get files detail list using pattern: " + pattern);
                StringBuilder result = new StringBuilder();
                FtpWebRequest reqFTP;

                log.Info("Start connect ftp server: " + ftpURI);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (pattern != null && pattern.Trim() != string.Empty)
                    {
                        if (Regex.IsMatch(line, pattern))
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                if (result.Length > 0)
                {
                    result.Remove(result.ToString().LastIndexOf('\n'), 1);
                }
                reader.Close();
                response.Close();
                log.Info("Get files detail list using pattern: " + pattern + " successful.");
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                log.Error("Get files detail list using pattern: " + pattern + "error.", ex);
                throw new TechnicalException("Get files detail list using pattern: " + pattern + "error.", ex);
            }
        }

        /// <summary>
        /// 获取当前目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        // <returns></returns>
        public string[] GetDirectoryList()
        {
            string[] drectory = GetFilesDetailList();
            string m = string.Empty;
            foreach (string str in drectory)
            {
                if (str.Trim().Substring(0, 1).ToUpper() == "D")
                {
                    m += str.Substring(54).Trim() + "\n";
                }
            }
            char[] n = new char[] { '\n' };
            return m.Split(n);
        }

        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            string[] dirList = GetDirectoryList();
            foreach (string str in dirList)
            {
                if (str.Trim() == RemoteDirectoryName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断当前目录下指定的文件是否存在
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        public bool FileExist(string RemoteFileName)
        {
            string[] fileList = GetFileList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                log.Info("Start makeDir: " + dirName);
                log.Info("Start connect ftp server: " + ftpURI);
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
                log.Info("MakeDir: " + dirName + " successful.");
            }
            catch (Exception ex)
            {
                log.Error("MakeDir: " + dirName + " error.", ex);
                throw new TechnicalException("MakeDir: " + dirName + " error.", ex);
            }
        }

        /// <summary>
        /// 获取指定文件大小
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            FtpWebRequest reqFTP;
            long fileSize = 0;
            try
            {
                log.Info("Start get file size: " + filename);
                log.Info("Start connect ftp server: " + ftpURI);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
                log.Info("Get file size: " + filename + " successful.");
            }
            catch (Exception ex)
            {
                log.Error("Get file size: " + filename + " error.", ex);
                throw new TechnicalException("Get file size: " + filename + " error.", ex);
            }
            return fileSize;
        }

        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void Rename(string currentFilename, string newFilename)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                log.Error("Rename File from: " + currentFilename + " to: " + newFilename + "error.", ex);
                throw new TechnicalException("Rename File from: " + currentFilename + " to: " + newFilename + "error.", ex);
            }
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void MovieFile(string currentFilename, string newDirectory)
        {
            Rename(currentFilename, newDirectory);
        }

        /// <summary>
        /// 切换当前目录
        /// </summary>
        /// <param name="DirectoryName"></param>
        public void ChangeDirectory(string directory)
        {
            if (directory.StartsWith("/"))
            {
                ChangeDirectory(directory, true);
            }
            else
            {
                ChangeDirectory(directory, false);
            }
        }

        /// <summary>
        /// 切换当前目录
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <param name="IsRoot">true 绝对路径   false 相对路径</param> 
        public void ChangeDirectory(string directory, bool isRoot)
        {
            if (isRoot)
            {
                ftpRemoteDirectory = directory;
            }
            else
            {
                ftpRemoteDirectory += directory + "/";
            }
            ftpURI = "ftp://" + ftpServer + "/" + ftpRemoteDirectory + "/";
            log.Info("Change directory to " + ftpURI);
        }
    }
}

