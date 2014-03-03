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
    public class RepBillMgr : RepTemplate1
    {
        private IKPOrderMgr kpOrderMgr;

        public RepBillMgr(String reportTemplateFolder, IKPOrderMgr kpOrderMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.kpOrderMgr = kpOrderMgr;

            //明细部分的行数
            this.pageDetailRowCount = 26;
            //列数   1起始
            this.columnCount = 12;
            //报表头的行数  1起始
            this.headRowCount = 10;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;

        }

        /**
         * 填充报表
         * 
         * Param list [0]OrderHead
         * Param list [0]IList<OrderDetail>           
         */
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2) return false;

                KPOrder kpOrder = (KPOrder)(list[0]);
                IList<KPItem> kpItems = (IList<KPItem>)(list[1]);


                if (kpOrder == null
                    || kpItems == null || kpItems.Count == 0)
                {
                    return false;
                }

                this.CopyPage(kpItems.Count);

                this.FillHead(kpOrder);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                decimal totalPrice = 0;
                foreach (KPItem kpItem in kpItems)
                {

                    //采购单	
                    this.SetRowCell(pageIndex, rowIndex, 0, kpItem.PURCHASE_ORDER_ID);
                    //零件号	
                    this.SetRowCell(pageIndex, rowIndex, 1, kpItem.PART_CODE);
                    //入库单号	
                    this.SetRowCell(pageIndex, rowIndex, 2, kpItem.INCOMING_ORDER_ID);
                    //序号	
                    this.SetRowCell(pageIndex, rowIndex, 3, kpItem.SEQ_ID);
                    //入库数量 
                    if (kpItem.INCOMING_QTY != null)
                    {
                        this.SetRowCell(pageIndex, rowIndex, 4, ((decimal)kpItem.INCOMING_QTY).ToString("0.########"));
                    }
                    //采购单价	
                    if (kpItem.PRICE != null)
                    {
                        this.SetRowCell(pageIndex, rowIndex, 5, ((decimal)kpItem.PRICE).ToString("0.########"));
                    }
                    //单位	
                    this.SetRowCell(pageIndex, rowIndex, 6, kpItem.UM);
                    //发票单价
                    if (kpItem.PRICE1 != null)
                    {
                        this.SetRowCell(pageIndex, rowIndex, 7, ((decimal)kpItem.PRICE1).ToString("0.########"));
                    }
                    //发票单价 @金额	
                    if (kpItem.PRICE1 != null)
                    {
                        this.SetRowCell(pageIndex, rowIndex, 8, ((decimal)kpItem.PRICE2).ToString("0.########"));
                        totalPrice += (decimal)kpItem.PRICE2;
                    }
                    //零件名称	
                    this.SetRowCell(pageIndex, rowIndex, 9, kpItem.PART_NAME);

                    //送货单号
                    this.SetRowCell(pageIndex, rowIndex, 10, kpItem.DELIVER_ORDER_ID);

                    //入库日期
                    if (kpItem.INCOMING_DATE != null)
                    {
                        this.SetRowCell(pageIndex, rowIndex, 11, ((DateTime)kpItem.INCOMING_DATE).ToString("yyyy-MM-dd"));
                    }

                    if (this.isPageBottom(rowIndex, rowTotal))//页的最后一行
                    {
                        //合计发票金额：
                        this.SetRowCell(pageIndex, this.pageDetailRowCount, 10, totalPrice.ToString("0.########"));
                        rowIndex = 0;
                        pageIndex++;
                        totalPrice = 0;
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
        private void FillHead(KPOrder kpOrder)
        {
            this.SetRowCell(8, 2, kpOrder.PARTY_FROM_ID);
            this.SetRowCell(8, 10, kpOrder.QAD_ORDER_ID);
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //采购员：
            this.CopyCell(pageIndex, 36 - this.headRowCount, 1, "B37");
            //主管：
            this.CopyCell(pageIndex, 36 - this.headRowCount, 6, "G37");
            //合计发票金额：
            this.CopyCell(pageIndex, 36 - this.headRowCount, 9, "J37");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            KPOrder kpOrder = kpOrderMgr.LoadKPOrder(decimal.Parse(code), true);
            if (kpOrder != null)
            {
                list.Add(kpOrder);
                list.Add(kpOrder.KPItems);
            }
            return list;
        }
    }
}
