using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity;
using Castle.Services.Transaction;
using com.Sconit.Utility.Report;
using NPOI.HSSF.UserModel;
using com.Sconit.Entity.Exception;
using System.Collections;
using com.Sconit.Utility;


namespace com.Sconit.Service.Report.Impl
{
    [Transactional]
    public class ReportMgr : IReportMgr
    {
        public IDictionary<string, string> dicReportService { get; set; }

        public IReportBaseMgr GetIReportBaseMgr(String template, IList<object> list)
        {
            IReportBaseMgr iReportMgr = this.GetImplService(template);

            if (iReportMgr != null)
            {
                iReportMgr.FillValues(template, list);
            }

            return iReportMgr;

        }

        private IReportBaseMgr GetImplService(String template)
        {
            if (template == null || !dicReportService.ContainsKey(template)
                || dicReportService[template] == null || dicReportService[template].Length == 0)
            {
                throw new BusinessErrorException("Common.Business.Error.EntityNotExist", template);
            }

            return ServiceLocator.GetService<IReportBaseMgr>(dicReportService[template]);
        }

        public string WriteToFile(String template, string entityId)
        {
            IReportBaseMgr iReportMgr = this.GetImplService(template);
            iReportMgr.FillValues(template, entityId);
            return this.WriteToFile(iReportMgr.GetWorkbook());
        }

        public string WriteToFile(String template, IList<object> list)
        {
            IReportBaseMgr iReportMgr = GetIReportBaseMgr(template, list);
            return this.WriteToFile(iReportMgr.GetWorkbook());
        }

        public string WriteToFile(String template, IList<object> list, String fileName)
        {
            IReportBaseMgr iReportMgr = GetIReportBaseMgr(template, list);
            return this.WriteToFile(fileName, iReportMgr.GetWorkbook());
        }

        public void WriteToClient(String template, string entityId, String fileName)
        {
            IReportBaseMgr iReportMgr = this.GetImplService(template);
            iReportMgr.FillValues(template, entityId);
            this.WriteToClient(fileName, iReportMgr.GetWorkbook());
        }

        public void WriteToClient(String template, IList<object> list, String fileName)
        {
            IReportBaseMgr iReportMgr = GetIReportBaseMgr(template, list);
            this.WriteToClient(fileName, iReportMgr.GetWorkbook());
        }

        public void WriteToClient(String fileName, HSSFWorkbook workbook)
        {
            XlsHelper.WriteToClient(fileName, workbook);
        }

        public string WriteToFile(HSSFWorkbook workbook)
        {
            return XlsHelper.WriteToFile(workbook);
        }
        public string WriteToFile(String fileName, HSSFWorkbook workbook)
        {
            return XlsHelper.WriteToFile(fileName, workbook);
        }



    }
}
