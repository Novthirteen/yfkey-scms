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

    /*
     * 废弃
     * 
     */ 
    [Transactional]
    public class RepRequisitionOrderInternalMgr : RepTemplate1
    {
        
        private IOrderHeadMgr orderHeadMgr;
        public RepRequisitionOrderInternalMgr(String reportTemplateFolder, IOrderHeadMgr orderHeadMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.orderHeadMgr = orderHeadMgr;

            //明细部分的行数
            this.pageDetailRowCount = 31;
            //列数   1起始
            this.columnCount = 11;
            //报表头的行数  1起始
            this.headRowCount = 7;
            //报表尾的行数  1起始
            this.bottomRowCount = 0;

        }

        /**
         * 填充报表
         * 
         * Param list [0]OrderHead
         * Param list [0]IList<OrderDetail>           
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {
                

                    if (list == null || list.Count < 2) return false;

                    OrderHead orderHead = (OrderHead)(list[0]);
                    IList<OrderDetail> orderDetails = (IList<OrderDetail>)(list[1]);


                    if (orderHead == null
                        || orderDetails == null || orderDetails.Count == 0)
                    {
                        return false;
                    }
                    //this.SetRowCellBarCode(0, 0, 7);
                    this.barCodeFontName = this.GetBarcodeFontName(0, 7);
                    this.CopyPage(orderDetails.Count);

                    this.FillHead(orderHead);

                    int pageIndex = 1;
                    int rowIndex = 0;
                    int rowTotal = 0;
                    foreach (OrderDetail orderDetail in orderDetails)
                    {

                        // No.	
                        this.SetRowCell(pageIndex, rowIndex, 0, "" + orderDetail.Sequence);

                        //零件号 Item Code
                        this.SetRowCell(pageIndex, rowIndex, 1, orderDetail.Item.Code);

                        //描述Description
                        this.SetRowCell(pageIndex, rowIndex, 2, orderDetail.Item.Description);

                        //单包装UC
                        this.SetRowCell(pageIndex, rowIndex, 3, orderDetail.UnitCount.ToString("0.########"));

                        //需求 Request	包装
                        int UCs = (int)Math.Ceiling(orderDetail.OrderedQty / orderDetail.UnitCount);
                        this.SetRowCell(pageIndex, rowIndex, 4, UCs.ToString());

                        //需求 Request	零件数
                        this.SetRowCell(pageIndex, rowIndex, 5, orderDetail.OrderedQty.ToString("0.########"));

                        //库位(loc)
                        this.SetRowCell(pageIndex, rowIndex, 6, orderDetail.LocationFrom.Code);
                        //批号（LOT）
                        this.SetRowCell(pageIndex, rowIndex, 7, "");

                        //实收 Received	包装
                        this.SetRowCell(pageIndex, rowIndex, 8, orderDetail.UnitCount.ToString("0.########"));

                        //实收 Received	零件数
                        this.SetRowCell(pageIndex, rowIndex, 9, orderDetail.ReceivedQty.HasValue ? orderDetail.ReceivedQty.Value.ToString("0.########") : string.Empty);

                        //备注
                        this.SetRowCell(pageIndex, rowIndex, 10, "");

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

                    //this.sheet.DisplayGridlines = false;
                    //this.sheet.IsPrintGridlines = false;

                    if (orderHead.IsPrinted == null || orderHead.IsPrinted == false)
                    {
                        orderHead.IsPrinted = true;
                        orderHeadMgr.UpdateOrderHead(orderHead);
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
         * Param repack 报验单头对象
         */
        private void FillHead(OrderHead orderHead)
        {
            

            //订单号:
            string orderCode = Utility.BarcodeHelper.GetBarcodeStr(orderHead.OrderNo, this.barCodeFontName);
            this.SetRowCell(0, 7, orderCode);
            //Order No.:
            this.SetRowCell(1, 7, orderHead.OrderNo);

            if ("Normal".Equals(orderHead.Priority))
            {
                this.SetRowCell(2, 4, "");
            }
            else
            {
                this.SetRowCell(1, 4, "");
            }

            //源超市：
            this.SetRowCell(2, 2, "");
            //目的超市：
            this.SetRowCell(3, 2, "");
            //领料地点：
            this.SetRowCell(4, 2, "");

            //窗口时间
            this.SetRowCell(2, 8, orderHead.WindowTime.ToLongTimeString());
            //订单时间
            this.SetRowCell(3, 8, "");
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {

        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            OrderHead orderHead = orderHeadMgr.LoadOrderHead(code, true);
            if (orderHead != null)
            {
                list.Add(orderHead);
                list.Add(orderHead.OrderDetails);
            }
            return list;
        }
    }
    
}
