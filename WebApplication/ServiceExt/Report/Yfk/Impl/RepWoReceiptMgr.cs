using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.View;

namespace com.Sconit.Service.Report.Yfk.Impl
{

    /**
     * 
     * 生产完工报表
     * 
     */
    [Transactional]
    public class RepWoReceiptMgr : RepTemplate1
    {

        private ICodeMasterMgr codeMasterMgr;

        public RepWoReceiptMgr(String reportTemplateFolder)
        {
            this.reportTemplateFolder = reportTemplateFolder;

            //明细部分的行数
            this.pageDetailRowCount = 40;
            //列数  1起始
            this.columnCount = 4;
            //报表头的行数  1起始
            this.headRowCount = 5;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;
        }

        /**
         * 填充报表
         * 
         * Param list [0]InspectOrder
         * Param list [0]IList<InspectOrderDetail>           
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2)
                {
                    return false;
                }
                OrderHead order = (OrderHead)(list[0]);
                IList<WoReceiptView> woReceiptList = (IList<WoReceiptView>)(list[1]);


                if (order == null
                    || woReceiptList == null || woReceiptList.Count == 0)
                {
                    return false;
                }


                this.CopyPage(woReceiptList.Count);

                this.FillHead(order);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (WoReceiptView woReceipt in woReceiptList)
                {

                    //生产线
                    this.SetRowCell(pageIndex, rowIndex, 0, woReceipt.Flow);
                    //物料代码
                    this.SetRowCell(pageIndex, rowIndex, 1, woReceipt.Item);
                    //物料描述
                    this.SetRowCell(pageIndex, rowIndex, 2, woReceipt.ItemDesc);
                    //"数量     QTY."
                    this.SetRowCell(pageIndex, rowIndex, 3, woReceipt.RecQty.Value.ToString("0.########"));
                    //"箱数     BoxCount."
                    this.SetRowCell(pageIndex, rowIndex, 4, woReceipt.BoxCount.ToString("0.########"));

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
        private void FillHead(OrderHead orderHead)
        {
            //区域
            this.SetRowCell(1, 1, orderHead.PartyFrom.Code);
            //订单号
            this.SetRowCell(1, 3, orderHead.OrderNo);
            //开始时间
            if (orderHead.ReleaseDate != null)
            {
                this.SetRowCell(2, 1, orderHead.ReleaseDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            //结束时间
            if (orderHead.StartDate != null)
            {
                this.SetRowCell(2, 3, orderHead.StartDate.Value.ToString("yyyy-MM-dd HH:mm"));
            }
            //制表人
            this.SetRowCell(3, 1, orderHead.CreateUser.Code);
            //制表日期
            this.SetRowCell(3, 3, orderHead.CreateDate.ToString("yyyy-MM-dd HH:mm"));
        }

        public override void CopyPageValues(int pageIndex)
        {
            //* 生产班组长
            this.CopyCell(pageIndex, 45 - this.headRowCount, 3, "D46");
        }

        public override IList<object> GetDataList(string code)
        {
            return null;
        }

    }
}
