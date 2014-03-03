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
    public class RepBillMarketMgr : RepTemplate1
    {
        private IBillMgr billMgr;
        private IReceiptMgr receiptMgr;

        public RepBillMarketMgr(String reportTemplateFolder, IBillMgr billMgr, IReceiptMgr receiptMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.billMgr = billMgr;
            this.receiptMgr = receiptMgr;

            //明细部分的行数
            this.pageDetailRowCount = 32;
            //列数   1起始
            this.columnCount = 10;
            //报表头的行数  1起始
            this.headRowCount = 5;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;

        }

        /**
         * 填充报表
         * 
         * Param list [0]bill
         * Param list [0]IList<BillDetail>           
         */
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2) return false;

                Bill bill = (Bill)(list[0]);
                IList<BillDetail> billDetails = (IList<BillDetail>)(list[1]);


                if (bill == null
                    || billDetails == null || billDetails.Count == 0)
                {
                    return false;
                }

                this.CopyPage(billDetails.Count);

                this.FillHead(bill);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (BillDetail billDetail in billDetails)
                {
                    //客户回单号	
                    this.SetRowCell(pageIndex, rowIndex, 0, billDetail.ActingBill.ExternalReceiptNo);
                    //销售单号	
                    this.SetRowCell(pageIndex, rowIndex, 1, billDetail.ActingBill.OrderNo);
                    if (billDetail.ActingBill.ReceiptNo != null && billDetail.ActingBill.ReceiptNo.Length > 0)
                    {
                        Receipt receipt = receiptMgr.LoadReceipt(billDetail.ActingBill.ReceiptNo);
                        //出门证号
                        this.SetRowCell(pageIndex, rowIndex, 2, receipt.ReferenceIpNo);
                    }

                    //零件号
                    this.SetRowCell(pageIndex, rowIndex, 3, billDetail.ActingBill.Item.Code);
                    //客户零件号
                    this.SetRowCell(pageIndex, rowIndex, 4, string.Empty);//todo
                    //序号	
                    this.SetRowCell(pageIndex, rowIndex, 5, string.Empty);
                    //出库数量	
                    this.SetRowCell(pageIndex, rowIndex, 6, billDetail.BilledQty.ToString("0.########"));
                    //单位	
                    this.SetRowCell(pageIndex, rowIndex, 7, billDetail.ActingBill.Uom.Code);
                    //零件名称	
                    this.SetRowCell(pageIndex, rowIndex, 8, billDetail.ActingBill.Item.Description);
                    //出库日期
                    this.SetRowCell(pageIndex, rowIndex, 9, billDetail.ActingBill.EffectiveDate.ToString("yyyy-MM-dd"));

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
         * Param repack 报验单头对象
         */
        private void FillHead(Bill bill)
        {
            this.SetRowCell(3, 2, bill.BillAddress.Party.Name);
            this.SetRowCell(3, 8, bill.BillNo);
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //对账员：
            this.CopyCell(pageIndex, 37 - this.headRowCount, 1, "B38");
            //主管：
            this.CopyCell(pageIndex, 37 - this.headRowCount, 7, "H38");

        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            Bill bill = billMgr.LoadBill(code, true);
            if (bill != null)
            {
                list.Add(bill);
                list.Add(bill.BillDetails);
            }
            return list;
        }
    }
}
