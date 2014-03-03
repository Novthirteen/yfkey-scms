using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using NPOI.HSSF.UserModel;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    /**
     * 
     * 原材料条码
     * 
     */
    [Transactional]
    public class RepInsideBarCodeMgr : ReportBaseMgr
    {
        
        private static readonly int ROW_COUNT = 3;

        //列数   1起始
        private static readonly int COLUMN_COUNT = 5;

        private IHuMgr huMgr;

        public RepInsideBarCodeMgr(String reportTemplateFolder, IHuMgr huMgr) 
        {
            this.reportTemplateFolder = reportTemplateFolder;;
            this.huMgr = huMgr;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {
            this.SetMergedRegion(pageIndex, 0, 0, 0, 4);
            this.SetMergedRegion(pageIndex, 1, 0, 1, 3);

            this.CopyCell(pageIndex, 2, 2, "C3");
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

                    IList<Hu> huList = (IList<Hu>)list[0];
                    /*
                    this.sheet.DefaultRowHeightInPoints = 12.00F;
                    //this.sheet.DefaultColumnWidth = 7;
                    //this.sheet.SetColumnWidth(1, 100 * 256);
                    sheet.DefaultColumnWidth = 7;
                    //sheet.DefaultRowHeight = 30 * 20;
                 
                    //sheet.PrintSetup.PaperSize = 
                    sheet.SetColumnWidth(0, 68 * 32);
                    sheet.SetColumnWidth(1, 68 * 32);
                    sheet.SetColumnWidth(2, 68 * 32);
                    sheet.SetColumnWidth(3, 68 * 32);
                    sheet.SetColumnWidth(4, 24 * 32);
                    */

                    this.sheet.DisplayGridlines = false;
                    this.sheet.IsPrintGridlines = false;

                    int count = 0;
                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("P")) //原材料
                        {
                            count++;
                        }
                    }

                    if (count == 0) return false;

                    this.barCodeFontName = this.GetBarcodeFontName(0, 0);

                    //加页删页
                    this.CopyPage(count, COLUMN_COUNT, 1);

                    int pageIndex = 1;
                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("P")) //原材料
                        {

                            if (hu.PrintCount > 1)
                            {
                                this.SetRowCell(pageIndex, 1, 4, "(R)");
                            }
                            hu.PrintCount += 1;

                            //hu id内容
                            string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId, this.barCodeFontName);
                            this.SetRowCell(pageIndex, 0, 0, barCode);

                            //hu id内容
                            this.SetRowCell(pageIndex, 1, 0, hu.HuId);

                            //PRINTED DATE:内容
                            this.SetRowCell(pageIndex, 2, 3, DateTime.Now.ToString("MM/dd/yy"));

                            this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, ROW_COUNT - 1));
                            pageIndex++;
                        }

                    }

                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("P")) //原材料
                        {
                            huMgr.UpdateHu(hu);
                        }
                    }
                
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

    }

}
