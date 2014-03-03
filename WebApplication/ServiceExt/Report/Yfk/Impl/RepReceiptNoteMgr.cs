using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    [Transactional]
    public class RepReceiptNoteMgr : RepTemplate1
    {
        private IReceiptMgr receiptMgr;

        public RepReceiptNoteMgr(String reportTemplateFolder, IReceiptMgr receiptMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.receiptMgr = receiptMgr;

            //明细部分的行数
            this.pageDetailRowCount = 25;
            //列数   1起始
            this.columnCount = 10;
            //报表头的行数  1起始
            this.headRowCount = 11;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;

        }

        /**
         * 填充报表
         * 
         * Param list [0]Receipt
         *            [1]ReceiptDetailList
         */
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2) return false;

                Receipt receipt = (Receipt)list[0];
                IList<ReceiptDetail> receiptDetailList = (IList<ReceiptDetail>)list[1];

                if (receipt == null
                    || receiptDetailList == null || receiptDetailList.Count == 0)
                {
                    return false;
                }

                this.barCodeFontName = this.GetBarcodeFontName(2, 5);
                //this.SetRowCellBarCode(0, 2, 5);
                this.CopyPage(receiptDetailList.Count);

                this.FillHead(receipt);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (ReceiptDetail receiptDetail in receiptDetailList)
                {
                    OrderDetail orderDetail = receiptDetail.OrderLocationTransaction.OrderDetail;

                    //订单号	
                    this.SetRowCell(pageIndex, rowIndex, 0, orderDetail.OrderHead.OrderNo);

                    //序号	
                    this.SetRowCell(pageIndex, rowIndex, 1, orderDetail.Sequence.ToString());
                    //零件号
                    this.SetRowCell(pageIndex, rowIndex, 2, orderDetail.Item.Code);
                    //参考号
                    this.SetRowCell(pageIndex, rowIndex, 3, orderDetail.ReferenceItemCode);
                    //描述
                    this.SetRowCell(pageIndex, rowIndex, 4, orderDetail.Item.Description);
                    //单位
                    this.SetRowCell(pageIndex, rowIndex, 5, orderDetail.Uom.Code);
                    //单包装
                    this.SetRowCell(pageIndex, rowIndex, 6, orderDetail.UnitCount.ToString("0.########"));
                    //发货数
                    decimal shippedQty = receiptDetail.ShippedQty.HasValue ? receiptDetail.ShippedQty.Value : 0;
                    this.SetRowCell(pageIndex, rowIndex, 7, shippedQty.ToString("0.########"));
                    //实收数	数量
                    decimal receivedQty = receiptDetail.ReceivedQty.HasValue ? (decimal)receiptDetail.ReceivedQty : 0;
                    this.SetRowCell(pageIndex, rowIndex, 8, receivedQty.ToString("0.########"));
                    //实收数  包装
                    decimal UC = orderDetail.UnitCount > 0 ? orderDetail.UnitCount : 1;
                    int UCs = (int)Math.Ceiling(receivedQty / UC);
                    this.SetRowCell(pageIndex, rowIndex, 9, UCs.ToString());

                    if (this.isPageBottom(rowIndex, rowTotal))//页的最后一行
                    {
                        //实际到货时间:
                        //this.SetRowCell(pageIndex, rowIndex, , "");

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
         * Param pageIndex 页号
         * Param orderHead 订单头对象
         * Param orderDetails 订单明细对象
         */
        private void FillHead(Receipt Receipt)
        {

            string receiptCode = Utility.BarcodeHelper.GetBarcodeStr(Receipt.ReceiptNo, this.barCodeFontName);
            //收货单号:
            this.SetRowCell(2, 5, receiptCode);
            //收货单号:
            this.SetRowCell(3, 5, Receipt.ReceiptNo);

            //外部单据号:
            this.SetRowCell(4, 1, Receipt.ExternalReceiptNo);
            //ASN号:
            this.SetRowCell(4, 5, Receipt.ReferenceIpNo);

            //供应商代码:
            this.SetRowCell(5, 1, Receipt.PartyFrom.Code);
            //供应商名称:
            this.SetRowCell(6, 1, Receipt.PartyFrom.Name);
            //承运商:
            //this.SetRowCell(6, 1,  );
            //收货日期:
            this.SetRowCell(5, 5, Receipt.CreateDate.ToString("yyyy-MM-dd HH:mm"));
            //收货部门:
            this.SetRowCell(6, 5, Receipt.PartyTo.Code + " [" + Receipt.PartyTo.Name + "]");
            //收货地点:
            this.SetRowCell(7, 5, Receipt.DockDescription);

            //实际收货时间: 
            this.SetRowCell(36, 1, Receipt.CreateDate.ToString());
            //收货人签字:
            this.SetRowCell(36, 4, Receipt.CreateUser.Name);

        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //this.SetMergedRegion(pageIndex, 40, 0, 40, 1);
            //this.SetMergedRegion(pageIndex, 40, 4, 40, 5);
            //this.SetMergedRegion(pageIndex, 40, 8, 40, 9);

            //实际收货时间: 
            this.CopyCell(pageIndex, 36 - this.headRowCount, 0, "A37");
            //收货人签字:
            this.CopyCell(pageIndex, 36 - this.headRowCount, 3, "D37");
            //承运商签字:	
            this.CopyCell(pageIndex, 36 - this.headRowCount, 6, "G37");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            Receipt receipt = receiptMgr.LoadReceipt(code, true);
            if (receipt != null)
            {
                list.Add(receipt);
                list.Add(receipt.ReceiptDetails);
            }
            return list;
        }

    }


}
