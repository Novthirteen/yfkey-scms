using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    [Transactional]
    public class RepRepackMgr : RepTemplate1
    {

        private IRepackMgr repackMgr;
        public RepRepackMgr(String reportTemplateFolder, IRepackMgr repackMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.repackMgr = repackMgr;

            //明细部分的行数
            this.pageDetailRowCount = 35;
            //列数   1起始
            this.columnCount = 9;
            //报表头的行数  1起始
            this.headRowCount = 8;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;

        }


        /**
         * 填充报表
         * 
         * Param list [0]Repack
         *            
         */
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 1) return false;

                Repack repack = (Repack)list[0];
                IList<RepackDetail> repackDetails = repack.RepackDetails;

                //投入/产出 排序
                repackDetails = IListHelper.Sort(repackDetails, "IOType");

                if (repack == null
                    || repackDetails == null || repackDetails.Count == 0)
                {
                    return false;
                }


                //this.SetRowCellBarCode(0, 2, 6);
                this.barCodeFontName = this.GetBarcodeFontName(2, 6);

                this.CopyPage(repackDetails.Count);

                this.FillHead(repack);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (RepackDetail repackDetail in repackDetails)
                {
                    //物料号
                    this.SetRowCell(pageIndex, rowIndex, 0, repackDetail.LocationLotDetail.Item.Code);
                    //物料描述	
                    this.SetRowCell(pageIndex, rowIndex, 1, repackDetail.LocationLotDetail.Item.Description);
                    //"单位Unit"	
                    this.SetRowCell(pageIndex, rowIndex, 2, repackDetail.LocationLotDetail.Hu.Uom.Code);
                    //"单包装UC"	
                    this.SetRowCell(pageIndex, rowIndex, 3, repackDetail.LocationLotDetail.Hu.UnitCount.ToString("0.########"));
                    //库位	
                    this.SetRowCell(pageIndex, rowIndex, 4, repackDetail.LocationLotDetail.Location.Code);
                    //批号	
                    this.SetRowCell(pageIndex, rowIndex, 5, repackDetail.LocationLotDetail.Hu.LotNo);
                    //Hu编号	
                    this.SetRowCell(pageIndex, rowIndex, 6, repackDetail.LocationLotDetail.Hu.HuId);
                    //数量	
                    this.SetRowCell(pageIndex, rowIndex, 7, (repackDetail.Qty / repackDetail.LocationLotDetail.Hu.UnitQty).ToString("0.########"));
                    //类型
                    this.SetRowCell(pageIndex, rowIndex, 8, repackDetail.IOType);

                    if (this.isPageBottom(rowIndex, rowTotal))//页的最后一行
                    {
                        pageIndex++;
                        rowIndex = 0;
                    }
                    else
                    {
                        rowIndex++;
                    }
                    rowTotal++;
                }

                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /*
         * 填充报表头
         * 
         * Param repack  翻箱头对象
         */
        private void FillHead(Repack repack)
        {

            string repackCode = Utility.BarcodeHelper.GetBarcodeStr(repack.RepackNo, this.barCodeFontName);
            //翻箱单号
            this.SetRowCell(2, 6, repackCode);
            //翻箱单号
            this.SetRowCell(3, 6, repack.RepackNo);
            //翻箱人 Create User:
            this.SetRowCell(5, 1, repack.CreateUser.Name);
            //翻箱时间
            this.SetRowCell(5, 7, repack.CreateDate.ToString("yyyy-MM-dd HH:mm"));
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //* 我已阅读延锋杰华的安全告知！
            this.CopyCell(pageIndex, 43 - this.headRowCount, 0, "A44");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            Repack repack = repackMgr.LoadRepack(code, true);
            if (repack != null)
            {
                list.Add(repack);
            }
            return list;
        }

    }
}
