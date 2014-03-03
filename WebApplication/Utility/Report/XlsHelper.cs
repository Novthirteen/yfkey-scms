using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Web;
using com.Sconit.Entity;
using NPOI.SS.UserModel;


namespace com.Sconit.Utility.Report
{
    public class XlsHelper
    {
        /**
         * UTF8编码文件名
         * 
         * Param fileName 文件名
         * 
         * Return 文件名
         */
        public static string UTF_FileName(string filename)
        {
            return HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8);
        }



        /*
        * 响应到客户端
        *
        * Param fileName 文件名
        */
        public static void WriteToClient(String fileName, HSSFWorkbook workbook)
        {
            //Write the stream data of workbook to the root directory
            //FileStream file = new FileStream(@"c:/test.xls", FileMode.Create);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";

            HttpContext.Current.Response.ContentType = "application/x-excel";
            //inline
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + UTF_FileName(fileName));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            workbook.Write(HttpContext.Current.Response.OutputStream);


            Sheet sheet = workbook.GetSheetAt(0);
            sheet = null;
            workbook.Dispose();
            workbook = null;

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
            //file.Close();
        }



        /*
         * 生成文件
         *
         * Return 生成文件的URL
         */
        public static string WriteToFile(HSSFWorkbook workbook)
        {
            return WriteToFile("temp.xls", workbook);
        }

        /*
         * 生成文件
         *
         * Param fileName 文件名
         * 
         * Return 生成文件的URL
         */
        public static string WriteToFile(String fileName, HSSFWorkbook workbook)
        {

            //临时文件路径
            string tempFilePath = HttpContext.Current.Server.MapPath("~/" + BusinessConstants.TEMP_FILE_PATH);
            string tempFileName = GetRandomFileName(fileName);

            if (!Directory.Exists(tempFilePath))
                Directory.CreateDirectory(tempFilePath);

            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(tempFilePath + tempFileName, FileMode.Create);
            workbook.Write(file);
            file.Flush();
            file.Close();
            file.Dispose();
            file = null;

            Sheet sheet = workbook.GetSheetAt(0);
            sheet = null;
            workbook.Dispose();
            workbook = null;

            return GetShowFileUrl(tempFileName);
        }


        /**
        * 生成随即文件名
        * 
        * Param tempFileName 模版文件名
        * 
        * Return 随即文件名
        */
        private static string GetRandomFileName(string tempFileName)
        {
            string templateFileName = tempFileName.Substring(0, tempFileName.LastIndexOf("."));
            string extension = tempFileName.Substring(tempFileName.LastIndexOf(".") + 1);

            string fileName = templateFileName + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N");
            if (extension != null && extension.Trim() != string.Empty)
                fileName += "." + extension;

            return fileName;
        }


        /**
        * 获得报表URL
        * 
        * Param fileName 文件名
        * 
        * Return 报表URL
        */
        private static string GetShowFileUrl(string fileName)
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //string showFileURL = string.Empty;
            //int index = 0;
            //if (url.EndsWith(".aspx"))
            //{
            //    index = url.IndexOf(".aspx");
            //    if (index > 0)
            //    {
            //        url = url.Remove(index);
            //        index = url.LastIndexOf("/");
            //        showFileURL = url.Remove(index + 1) + BusinessConstants.TEMP_FILE_PATH + fileName;
            //    }
            //}
            //else if (url.EndsWith(".asmx"))//支持webservice
            //{
            //    index = url.IndexOf(".asmx");
            //}

            string url = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/";
            url = url + BusinessConstants.TEMP_FILE_PATH + fileName;
            return url;
        }
    }
}
