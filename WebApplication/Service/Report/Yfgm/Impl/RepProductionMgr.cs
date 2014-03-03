using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using NPOI.HSSF.UserModel;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfgm.Impl
{
    ///
    ///作用：生产单
    ///作者：tiansu
    ///编写日期：2010-01-22
    ///
    [Transactional]
    public class RepProductionMgr : ReportBaseMgr
    {
        //第一页明细行数
        private static readonly int FIRST_PAGE_DETAIL_ROW_COUNT = 16;
        //非第一页明细行数
        private static readonly int NO_FIRST_PAGE_DETAIL_ROW_COUNT = 40;

        private static readonly int ROW_COUNT = 51;

        private IOrderHeadMgr orderHeadMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IFlowMgr flowMgr;

        public RepProductionMgr(String reportTemplateFolder, String barCodeFontName, short barCodeFontSize,
            IOrderHeadMgr orderHeadMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IFlowMgr flowMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.barCodeFontName = barCodeFontName;
            this.barCodeFontSize = barCodeFontSize;
            this.orderHeadMgr = orderHeadMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.flowMgr = flowMgr;
        }

        /*
         * 计算页数
         * 
         * Param firstPageDetailRowCount 第一页明细行数
         *                                  参见 FIRST_PAGE_DETAIL_ROW_COUNT
         * Param noFirstPageDetailRowCount 非第一页明细行数
         *                                  参见 NO_FIRST_PAGE_DETAIL_ROW_COUNT
         * Param resultCount 明细行数
         *                     
         */
        public int getPageCount(int firstPageDetailRowCount, int noFirstPageDetailRowCount, int resultCount)
        {
            int pageCount = 1;
            //计算总页数
            if (resultCount <= firstPageDetailRowCount)
            {
                pageCount = 1;
            }
            else
            {
                pageCount = (int)Math.Ceiling((resultCount - firstPageDetailRowCount) / (noFirstPageDetailRowCount * 1.0) + 1);
            }
            return pageCount;
        }

        /*
         * 填充报表头
         * 
         * Param pageIndex 页号
         * Param orderHead 订单头对象
         * Param orderDetails 订单明细对象
         */
        protected void FillHead(int pageIndex, OrderHead orderHead, IList<OrderDetail> orderDetails)
        {

            #region 报表头
            //报表头
            //工单号码Order code
            

            string orderCode = Utility.BarcodeHelper.GetBarcodeStr(orderHead.OrderNo, this.barCodeFontName);
            this.SetRowCell(pageIndex, 1, 6, orderCode);
            this.SetRowCell(pageIndex, 2, 6, orderHead.OrderNo);

            //this.CopyCell(pageIndex, 1, 6, "G3");

            //生产线Production Line
            Flow flow = this.flowMgr.LoadFlow(orderHead.Flow);
            this.SetRowCell(pageIndex, 4, 2, flow.Description);

            //窗口时间Window Time
            this.SetRowCell(pageIndex, 4, 6, orderHead.WindowTime.ToString("yyyy-MM-dd HH:mm"));

            //班次Shift
            this.SetRowCell(pageIndex, 4, 8, orderHead.Shift == null ? string.Empty : orderHead.Shift.Code);


            //工单类型Type//TODO
            this.SetRowCell(pageIndex, 4, 10, string.Empty);

            #endregion

            #region 产品信息  Product Information
            if (pageIndex == 1)//首页
            {
                foreach (OrderDetail orderDetail in orderDetails)
                {
                    //产品信息  Product Information		
                    //"零件号QAD Code"
                    this.SetRowCell(pageIndex, 9, 1, orderDetail.Item.Code);
                    //"零件名Description"	
                    this.SetRowCell(pageIndex, 9, 2, orderDetail.Item.Description);
                    //单位Unit"
                    this.SetRowCell(pageIndex, 9, 4, orderDetail.Uom.Code);
                    //计划数Plan Qty.
                    this.SetRowCell(pageIndex, 9, 5, orderDetail.OrderedQty.ToString("0.########"));
                    //条码标签Barcode Label
                    //生产数Produce Qty.
                    //合格数Good Qty.
                    //废品数Scrap Qty.
                    //待处理Waiting Qty.
                    //备注Comment
                }
            }
            #endregion

        }

        /*
         * 填充报表尾
         * 
         * Param pageIndex 页号
         * Param orderHead 订单头对象
         */
        protected void FillFooter(int pageIndex, OrderHead orderHead)
        {
            //计划员Planner
            this.SetRowCell(pageIndex, (rowCount - 1), 2, orderHead.CreateUser.Name);
        }

        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {

            this.SetMergedRegion(pageIndex, 0, 6, 0, 9);
            this.SetMergedRegion(pageIndex, 0, 1, 1, 3);
            this.SetMergedRegion(pageIndex, 1, 6, 1, 9);
            //"工单号码Order code"
            this.SetMergedRegion(pageIndex, 1, 5, 2, 5);

            //"版本Version"
            this.SetMergedRegion(pageIndex, 1, 10, 2, 10);
            this.SetMergedRegion(pageIndex, 1, 11, 2, 11);

            this.SetMergedRegion(pageIndex, 2, 6, 2, 9);
            //"生产线Production Line"	
            this.SetMergedRegion(pageIndex, 4, 0, 4, 1);

            this.SetMergedRegion(pageIndex, 4, 2, 4, 3);

            //"窗口时间Window Time"	
            this.SetMergedRegion(pageIndex, 4, 4, 4, 5);

            this.SetMergedRegion(pageIndex, 4, 10, 4, 11);

            //物料信息  Material Information		
            this.SetMergedRegion(pageIndex, 7, 0, 7, 2);


            this.SetMergedRegion(pageIndex, 50, 0, 50, 1);

            this.SetMergedRegion(pageIndex, 50, 4, 50, 5);


            //8-41
            for (int i = 8; i <= 41; i++)
            {
                this.SetMergedRegion(pageIndex, i, 2, i, 3);
            }


            this.CopyCell(pageIndex, 0, 1, "B52");

            //生产订单Production Order
            this.CopyCell(pageIndex, 0, 6, "G52");

            this.CopyCell(pageIndex, 1, 5, "F53");

            this.CopyCell(pageIndex, 1, 6, "G53");

            //"版本Version"
            this.CopyCell(pageIndex, 1, 10, "K53");

            this.CopyCell(pageIndex, 1, 11, "L53");


            this.CopyCell(pageIndex, 4, 0, "A56");

            //"窗口时间Window Time"	
            this.CopyCell(pageIndex, 4, 4, "E56");
            //"班次Shift"
            this.CopyCell(pageIndex, 4, 7, "H56");
            //"工单类型Type"
            this.CopyCell(pageIndex, 4, 9, "J56");

            //物料信息  Material Information		
            this.CopyCell(pageIndex, 7, 0, "A59");

            //(接上页）
            this.CopyCell(pageIndex, 7, 3, "D59");


            //No.
            this.CopyCell(pageIndex, 8, 0, "A60");

            //零件号QAD Code
            this.CopyCell(pageIndex, 8, 1, "B60");
            //零件名Description
            this.CopyCell(pageIndex, 8, 2, "C60");
            //"单位Unit"
            this.CopyCell(pageIndex, 8, 4, "E60");
            //"标准耗量Plan Qty."
            this.CopyCell(pageIndex, 8, 5, "F60");
            //"需求量Dmd Qty"
            this.CopyCell(pageIndex, 8, 6, "G60");
            //"领用数Request Qty."
            this.CopyCell(pageIndex, 8, 7, "H60");
            //"返回数Return Qty."
            this.CopyCell(pageIndex, 8, 8, "I60");
            //"废品数Scrap Qty."
            this.CopyCell(pageIndex, 8, 9, "J60");
            //"待处理Waiting Qty."
            this.CopyCell(pageIndex, 8, 10, "K60");
            //备注Comment
            this.CopyCell(pageIndex, 8, 11, "L60");

            //计划员Planner
            this.CopyCell(pageIndex, 50, 0, "A102");

            //班组长Team Leader
            this.CopyCell(pageIndex, 50, 4, "E102");

        }


        /**
         * 填充报表
         * 
         * Param list [0]订单头对象
         *            [1]订单明细对象
         *            [2]订单库位事物对象
         */
        [Transaction(TransactionMode.Requires)]
        public override bool FillValues(String templateFileName, IList<object> list)
        {
            try
            {

                this.init(templateFileName, ROW_COUNT);
                if (list == null || list.Count < 3) return false;

                OrderHead orderHead = (OrderHead)(list[0]);
                IList<OrderDetail> orderDetails = (IList<OrderDetail>)(list[1]);
                IList<OrderLocationTransaction> orderLocationTransactions = (IList<OrderLocationTransaction>)(list[2]);

                int pageIndex = 1;
                int pageCount = this.getPageCount(FIRST_PAGE_DETAIL_ROW_COUNT, NO_FIRST_PAGE_DETAIL_ROW_COUNT, orderLocationTransactions.Count);


                this.SetRowCellBarCode(1, 1, 6);
                //加页删页
                this.CopyPage(pageCount, 12);

                #region 物料信息  Material Information
                //物料信息  Material Information
                if (orderLocationTransactions == null || orderLocationTransactions.Count == 0)
                {
                    //填充第一页;
                    this.FillHead(pageIndex, orderHead, orderDetails);
                    this.FillFooter(pageIndex, orderHead);
                }
                else
                {
                    int i = 1;
                    foreach (OrderLocationTransaction orderLocationTransaction in orderLocationTransactions)
                    {
                        if (orderLocationTransaction.IOType.Equals(BusinessConstants.IO_TYPE_OUT))
                        {
                            if (pageIndex == 1 && i == 1) //第一页,明细部分 第一行,不换页
                            {
                                this.FillHead(pageIndex, orderHead, orderDetails);
                                this.FillFooter(pageIndex, orderHead);
                            }

                            if ((pageIndex == 1 && i == 17) //第一页,明细部分  16行换页
                                    || (pageIndex > 1 && ((i - 1 - 16) % 40) == 0))//非第一页,明细部分 40行换页
                            {
                                pageIndex++;
                                this.FillHead(pageIndex, orderHead, orderDetails);
                                this.FillFooter(pageIndex, orderHead);
                            }

                            //零件号QAD Code
                            this.SetRowCell(pageIndex, 15 + i - 1, 1, orderLocationTransaction.Item.Code);
                            //零件名Description
                            this.SetRowCell(pageIndex, 15 + i - 1, 2, orderLocationTransaction.Item.Description);
                            // 单位Unit
                            this.SetRowCell(pageIndex, 15 + i - 1, 4, orderLocationTransaction.Uom.Code);

                            //"标准耗量Plan Qty.
                            this.SetRowCell(pageIndex, 15 + i - 1, 5, orderLocationTransaction.UnitQty.ToString("0.########"));

                            //"需求量Dmd Qty"
                            this.SetRowCell(pageIndex, 15 + i - 1, 6, orderLocationTransaction.OrderedQty.ToString("0.########"));


                            //领用数Request Qty.

                            //返回数Return Qty.

                            //废品数Scrap Qty.

                            //待处理Waiting Qty.

                            //备注Comment

                            i++;

                        }
                    }
                }
                #endregion

                //人员信息  People Information
                //空

                //生产记录  Production Information		
                //空

                if (orderHead.IsPrinted == null || orderHead.IsPrinted == false)
                {
                    orderHead.IsPrinted = true;
                    orderHeadMgr.UpdateOrderHead(orderHead);
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            OrderHead orderHead = orderHeadMgr.LoadOrderHead(code, true);
            if (orderHead != null)
            {
                list.Add(orderHead);
                list.Add(orderHead.OrderDetails);
                IList<OrderLocationTransaction> orderLocationTransactions = orderLocationTransactionMgr.GetOrderLocationTransaction(orderHead.OrderNo);
                list.Add(orderLocationTransactions);
            }
            return list;
        }

    }

}
