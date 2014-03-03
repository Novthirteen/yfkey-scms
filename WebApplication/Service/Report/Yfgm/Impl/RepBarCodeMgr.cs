using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfgm.Impl
{
    [Transactional]
    public class RepBarCodeMgr : ReportBaseMgr
    {

        private static readonly int ROW_COUNT = 7;
        //列数   1起始
        private static readonly int COLUMN_COUNT = 5;

        private IHuMgr huMgr;

        public RepBarCodeMgr(String reportTemplateFolder, String barCodeFontName, short barCodeFontSize, IHuMgr huMgr) 
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.barCodeFontName = barCodeFontName;
            this.barCodeFontSize = barCodeFontSize;
            this.huMgr = huMgr;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {
            //hu id
            this.SetMergedRegion(pageIndex, 0, 0, 0, 3);
            //hu id
            this.SetMergedRegion(pageIndex, 1, 0, 1, 3);
            this.SetMergedRegion(pageIndex, 2, 1, 2, 3);
            this.SetMergedRegion(pageIndex, 3, 1, 3, 3);
            this.SetMergedRegion(pageIndex, 4, 1, 4, 3);
            //this.SetMergedRegion(pageIndex, 6, 1, 6, 3);
            //hu id
            //this.CopyCell(pageIndex, 0, 0, "A1");
            
            //hu id
            //this.CopyCell(pageIndex, 1, 0, "A2");
            this.CopyCell(pageIndex, 2, 0, "A3");
            this.CopyCell(pageIndex, 3, 0, "A4");
            this.CopyCell(pageIndex, 4, 0, "A5");
            this.CopyCell(pageIndex, 5, 0, "A6");
            this.CopyCell(pageIndex, 5, 2, "C6");
            this.CopyCell(pageIndex, 6, 0, "A7");

            this.CopyCell(pageIndex, 6, 2, "C7");

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


                int pageCount = huList.Count;
                
                this.SetRowCellBarCode(0, 0, 0);

                //加页删页
                this.CopyPage(pageCount, COLUMN_COUNT, 1);

                int pageIndex = 1;
                foreach (Hu hu in huList)
                {
                    hu.PrintCount += 1;

                    string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId,this.barCodeFontName);
                    //hu id
                    this.SetRowCell(pageIndex, 0, 0, barCode);

                    //hu id
                    this.SetRowCell(pageIndex, 1, 0, hu.HuId);

                    //零件号ItemNo
                    this.SetRowCell(pageIndex, 2, 1, hu.Item.Code);

                    //描述Desc
                    this.SetRowCell(pageIndex, 3, 1, hu.Item.Description);

                    //批号LotNo
                    this.SetRowCell(pageIndex, 4, 1, hu.LotNo);

                    //数量quantity
                    this.SetRowCell(pageIndex, 5, 1, hu.Qty.ToString("0.########"));

                    //单位Unit
                    this.SetRowCell(pageIndex, 5, 3, hu.Uom.Code);

                    //生产日期Manufacture date
                    this.SetRowCell(pageIndex, 6, 1, hu.ManufactureDate.ToString("yyyyMMdd"));

                    //生产商Manufacture
                    this.SetRowCell(pageIndex, 6, 3, hu.ManufactureParty == null ? string.Empty : hu.ManufactureParty.Name);

                    this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, 6));
                    pageIndex++;

                }

                foreach (Hu hu in huList)
                {
                    huMgr.UpdateHu(hu);
                }

                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public override IList<object> GetDataList(string code)
        {
            throw new NotImplementedException();
        }
    }

}
