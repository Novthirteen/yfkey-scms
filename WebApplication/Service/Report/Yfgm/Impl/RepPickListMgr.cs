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
    public class RepPickListMgr : RepTemplate1
    {
        private IPickListMgr pickListMgr;
        public RepPickListMgr(String reportTemplateFolder, String barCodeFontName, short barCodeFontSize, IPickListMgr pickListMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.barCodeFontName = barCodeFontName;
            this.barCodeFontSize = barCodeFontSize;
            this.pickListMgr = pickListMgr;

            //明细部分的行数
            this.pageDetailRowCount = 40;
            //列数   1起始
            this.columnCount = 10;
            //报表头的行数  1起始
            this.headRowCount = 7;
            //报表尾的行数  1起始
            this.bottomRowCount = 2;
        }

        /**
         * 填充报表
         * 
         * Param list [0]PickList
         *            
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {
                PickList pickList = (PickList)list[0];
                IList<PickListDetail> pickListDetails = pickList.PickListDetails;

                if (pickList == null
                    || pickListDetails == null || pickListDetails.Count == 0)
                {
                    return false;
                }

               
                this.SetRowCellBarCode(0, 2, 7);
                this.CopyPage(pickListDetails.Count);

                this.FillHead(pickList);


                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (PickListDetail pickListDetail in pickListDetails)
                {

                    //"订单号Order No."	
                    this.SetRowCell(pageIndex, rowIndex, 0, pickListDetail.OrderLocationTransaction.OrderDetail.OrderHead.OrderNo);
                    //物料号	
                    this.SetRowCell(pageIndex, rowIndex, 1, pickListDetail.Item.Code);
                    //物料描述	
                    this.SetRowCell(pageIndex, rowIndex, 2, pickListDetail.Item.Description);
                    //"单位Unit"	
                    this.SetRowCell(pageIndex, rowIndex, 3, pickListDetail.Uom.Code);
                    //"单包装UC"	
                    this.SetRowCell(pageIndex, rowIndex, 4, pickListDetail.UnitCount.ToString("0.########"));
                    //库位	
                    this.SetRowCell(pageIndex, rowIndex, 5, pickListDetail.Location.Code);
                    //库区	
                    this.SetRowCell(pageIndex, rowIndex, 6, pickListDetail.StorageArea == null ? string.Empty : pickListDetail.StorageArea.Code);
                    //库格
                    this.SetRowCell(pageIndex, rowIndex, 7, pickListDetail.StorageBin == null ? string.Empty : pickListDetail.StorageBin.Code);
                    //批号
                    this.SetRowCell(pageIndex, rowIndex, 8, pickListDetail.LotNo);
                    //订单数
                    this.SetRowCell(pageIndex, rowIndex, 9, pickListDetail.Qty.ToString("0.########"));


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

                if (pickList.IsPrinted == null || pickList.IsPrinted == false)
                {
                    pickList.IsPrinted = true;
                    pickListMgr.UpdatePickList(pickList);
                }
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
         * Param pickList 订单头对象
         */
        private void FillHead(PickList pickList)
        {
            string pickListCode = Utility.BarcodeHelper.GetBarcodeStr(pickList.PickListNo, this.barCodeFontName);
            //拣货单号
            this.SetRowCell(2, 7, pickListCode);
            //拣货单号
            this.SetRowCell(3, 7, pickList.PickListNo);
            //创建时间
            this.SetRowCell(4, 7, pickList.CreateDate.ToString("yyyy-MM-dd HH:mm"));
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            this.SetMergedRegion(pageIndex, 47 , 2, 47 , 3);
            this.SetMergedRegion(pageIndex, 47 , 6, 47 , 7);
            this.SetMergedRegion(pageIndex, 47 , 8, 47 , 9);

            //实际到货时间:
            this.CopyCell(pageIndex, 47 , 1, "B48");
            //拣货人签字:	
            this.CopyCell(pageIndex, 47 , 6, "G48");
            //* 我已阅读延锋杰华的安全告知！
            this.CopyCell(pageIndex, 48 , 0, "A49");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            PickList pickList = pickListMgr.LoadPickList(code, true);
            if (pickList != null)
            {
                list.Add(pickList);
            }
            return list;
        }

    }
}
