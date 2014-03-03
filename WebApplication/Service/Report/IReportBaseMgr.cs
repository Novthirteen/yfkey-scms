using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;

namespace com.Sconit.Service.Report
{
    public interface IReportBaseMgr
    {
        bool FillValues(String templateFileName, IList<object> list);
        bool FillValues(String templateFileName, string orderNo);
        int CopyPage(int pageCount, int columnCount);
        void CopyPageValues(int pageIndex);
        HSSFWorkbook GetWorkbook();
        IList<object> GetDataList(string code);
    }
}
