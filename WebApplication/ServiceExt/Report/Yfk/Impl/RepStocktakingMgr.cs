using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using NPOI.HSSF.UserModel;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using NPOI.SS.UserModel;
using com.Sconit.Utility;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    /**
     * 
     * 原材料条码
     * 
     */
    [Transactional]
    public class RepStocktakingMgr : ReportBaseMgr
    {

        private static readonly int ROW_COUNT = 8;
        //列数   1起始
        private static readonly int COLUMN_COUNT = 3;

        private ICycleCountMgr cycleCountMgr { get; set; }
        //public IEntityPreferenceMgr entityPreferenceMgr { get; set; }

        public RepStocktakingMgr(String reportTemplateFolder)
        {
            this.reportTemplateFolder = reportTemplateFolder;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {
            this.SetMergedRegion(pageIndex, 0, 0, 0, 2);
            this.SetMergedRegion(pageIndex, 1, 0, 1, 2);

            this.CopyCell(pageIndex, 6, 2, "C7");
            this.CopyCell(pageIndex, 7, 2, "C8");
        }

        /**
         * 填充报表
         * 
         * Param list [0]huDetailList
         */
        [Transaction(TransactionMode.Requires)]
        public override bool FillValues(String templateFileName, IList<object> list)
        {
            try
            {

                this.init(templateFileName, ROW_COUNT);

                if (list == null || list.Count == 0) return false;

                IList<CycleCount> cycleCountList = (IList<CycleCount>)list[0];
                string userName = "";
                if (list.Count == 2)
                {
                    userName = (string)list[1];
                }

                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;

                //this.sheet.DisplayGuts = false;
                int count = cycleCountList.Count;
                if (count == 0) return false;

                this.barCodeFontName = this.GetBarcodeFontName(0, 0);

                //加页删页
                this.CopyPage(count, COLUMN_COUNT, 1);

                int pageIndex = 1;

                //string companyCode = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_CODE).Value;
                //if (companyCode == null) companyCode = string.Empty;

                foreach (CycleCount cycleCount in cycleCountList)
                {

                    string barCode = Utility.BarcodeHelper.GetBarcodeStr(cycleCount.Code, this.barCodeFontName);
                    this.SetRowCell(pageIndex, 0, 0, barCode);

                    this.SetRowCell(pageIndex, 1, 0, cycleCount.Code);

                    //库位:
                    this.SetRowCell(pageIndex, 2, 2, StringHelper.GetCodeDescriptionString(cycleCount.Location.Code, cycleCount.Location.Name));
                    //类型:
                    this.SetRowCell(pageIndex, 3, 2, cycleCount.Type);
                    //生效日期:
                    this.SetRowCell(pageIndex, 4, 2, cycleCount.EffectiveDate.ToString("yyyy-MM-dd"));
                    //是否扫描条码:
                    this.SetRowCell(pageIndex, 5, 2, cycleCount.IsScanHu ? "是" : "否");

                    //打印时间:
                    this.SetRowCell(pageIndex, 6, 2, DateTime.Now.ToString("yyyy-MM-dd"));

                    //打印人:
                    this.SetRowCell(pageIndex, 7, 2, userName);

                    this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, ROW_COUNT - 1));
                    pageIndex++;
                }

                /*

                foreach (CycleCount cycleCount in cycleCountList)
                {
                    cycleCountMgr.UpdateHu(hu);

                }
                 */

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

    }

}
