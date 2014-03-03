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
    public class RepInsideBarCodeA4Mgr : ReportBaseMgr
    {

        private static readonly int PAGE_DETAIL_ROW_COUNT = 20;
        
        private static readonly int ROW_COUNT = 39;
        //列数   1起始
        private static readonly int COLUMN_COUNT = 11;

        private IHuMgr huMgr;

        public RepInsideBarCodeA4Mgr(String reportTemplateFolder, IHuMgr huMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.huMgr = huMgr;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {

            //十行 二个
            for (int rowNum = 0; rowNum < 37; rowNum += 4)
            {
                //第一个
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum, 0, rowNum, 4);
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum + 1, 0, rowNum + 1, 3);

                //第二个
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum, 6, rowNum, 10);
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum + 1, 6, rowNum + 1, 9);

                this.CopyCell(pageIndex, rowNum + 2, 2, "C" + (rowNum + 3));
                this.CopyCell(pageIndex, rowNum + 2, 8, "I" + (rowNum + 3));
            }

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
                    string userName = "";
                    if (list.Count == 2)
                    {
                        userName = (string)list[1];
                    }

                    int count = 0;
                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("P"))
                        {
                            count++;
                        }
                    }
                    if (count == 0) return false;
                    int pageCount = (int)Math.Ceiling(count / (PAGE_DETAIL_ROW_COUNT * 1.0));

                    /*
                    //十行 二个
                    for (int rowNum = 0; rowNum < 37; rowNum += 4)
                    {
                        this.SetRowCellBarCode(1, rowNum, 0);
                        this.SetRowCellBarCode(1, rowNum, 6);
                    }
                    */

                    this.barCodeFontName = this.GetBarcodeFontName(0, 0);

                    //加页删页
                    this.CopyPage(pageCount, COLUMN_COUNT, 1);

                    this.sheet.DisplayGridlines = false;
                    this.sheet.IsPrintGridlines = false;

                    int pageIndex = 1;
                    int num = 1;
                    foreach (Hu hu in huList)
                    {

                        //10行每行2个   //原材料
                        if (hu.Item.Type.Equals("P"))
                        {
                            this.writeContent(userName, pageIndex, num, hu);

                            if (num == count || num % PAGE_DETAIL_ROW_COUNT == 0)
                            {
                                pageIndex++;
                            }
                            num++;
                        }
                        /*
                        if (num == huList.Count + 1)
                        {
                            for (int i = 1; i <= (PAGE_DETAIL_ROW_COUNT - (num % PAGE_DETAIL_ROW_COUNT)); i++)
                            {
                                //PRINTED DATE:

                                this.CopyCell(pageIndex - 1, this.getRowIndex(2, num - 1 + i), 2, "");
                                this.SetRowCell(pageIndex - 1, 2, 2, string.Empty, num - 1 + i);
                            }
                        }*/
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

        private void writeContent(string userName, int pageIndex, int num, Hu hu)
        {

            if (hu.PrintCount > 1)
            {
                this.SetRowCell(pageIndex, 1, 4, "(R)", num);
            }
            hu.PrintCount += 1;


            //hu id内容
            string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId, this.barCodeFontName);
            this.SetRowCell(pageIndex, 0, 0, barCode, num);
            //hu id内容
            this.SetRowCell(pageIndex, 1, 0, hu.HuId, num);
            //PRINTED DATE:内容
            this.SetRowCell(pageIndex, 2, 3, DateTime.Now.ToString("MM/dd/yy"), num);

        }

        private int getRowIndex(int rowIndexRelative, int num)
        {
            return (num - 1) / 2 % 10 * 4 + rowIndexRelative; ;
        }

        private int getColumnIndex(int cellIndex, int num)
        {
            return cellIndex + ((num - 1) % 2) * 6;
        }


        public void SetRowCell(int pageIndex, int rowIndexRelative, int cellIndex, String value, int num)
        {
           // this.SetRowCell(pageIndex, ((int)(Math.Ceiling(num / 2.0)) - 1) * 4 + rowIndexRelative, cellIndex + ((num - 1) % 2) * 6, value);

            this.SetRowCell(pageIndex, getRowIndex(rowIndexRelative, num), getColumnIndex(cellIndex, num), value);
        }


    }

}
