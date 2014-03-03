using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using System.Collections;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfgm.Impl
{
    [Transactional]
    public class RepRequisitionOrderMgr : ReportBaseMgr
    {

        private static readonly int ROW_COUNT = 51;

        private IOrderHeadMgr orderHeadMgr;
        public RepRequisitionOrderMgr(String reportTemplateFolder, String barCodeFontName, short barCodeFontSize, IOrderHeadMgr orderHeadMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.barCodeFontName = barCodeFontName;
            this.barCodeFontSize = barCodeFontSize;
            this.orderHeadMgr = orderHeadMgr;
        }

        /**
         * 
         * 填充报表
         * 
         * Param list [0]:订单头
         *            [1]:订单明细
         * 
         */
        [Transaction(TransactionMode.Requires)]
        public override bool FillValues(String templateFileName, IList<object> list)
        {
            try
            {
                this.init(templateFileName, ROW_COUNT);

                OrderHead orderHead = (OrderHead)(list[0]);
                IList<OrderDetail> orderDetails = (IList<OrderDetail>)(list[1]);
                int rowNum = 2;

                #region 报表头

                this.SetRowCellBarCode(1, 2, 8);

                //订单号:
                string orderCode = Utility.BarcodeHelper.GetBarcodeStr(orderHead.OrderNo, this.barCodeFontName);
                this.SetRowCell(rowNum++, 8, orderCode);
                //Order No.:
                this.SetRowCell(rowNum++, 8, orderHead.OrderNo);

                if ("Normal".Equals(orderHead.Priority))
                {
                    this.SetRowCell(4, 5, "");
                }
                else
                {
                    this.SetRowCell(3, 5, "");
                }
                //发出时间 Issue Time:
                this.SetRowCell(rowNum++, 9, orderHead.StartDate.ToString());

                //供应商代码 Supplier Code:	
                this.SetRowCell(++rowNum, 3, orderHead.PartyFrom != null ? orderHead.PartyFrom.Code : String.Empty);

                //交货日期 Delivery Date:getDemandDeliverDate
                this.SetRowCell(rowNum, 8, orderHead.WindowTime.ToLongDateString());


                //供应商名称 Supplier Name:		
                this.SetRowCell(++rowNum, 3, orderHead.PartyFrom != null ? orderHead.PartyFrom.Name : String.Empty);
                //窗口时间 Window Time:
                this.SetRowCell(rowNum, 8, orderHead.WindowTime.ToLongTimeString());

                //供应商地址 Address:	
                this.SetRowCell(++rowNum, 3, orderHead.ShipFrom != null ? orderHead.ShipFrom.Address : String.Empty);
                //交货道口 Delivery Dock:
                this.SetRowCell(rowNum, 8, orderHead.DockDescription);

                //供应商联系人 Contact:	
                this.SetRowCell(++rowNum, 3, orderHead.ShipFrom != null ? orderHead.ShipFrom.ContactPersonName : String.Empty);
                //物流协调员 Follow Up:
                this.SetRowCell(rowNum, 8, orderHead.ShipTo != null ? orderHead.ShipTo.ContactPersonName : String.Empty);

                //供应商电话 Telephone:		
                this.SetRowCell(++rowNum, 3, orderHead.ShipFrom != null ? orderHead.ShipFrom.TelephoneNumber : String.Empty);
                //YFV电话 Telephone:
                this.SetRowCell(rowNum, 8, orderHead.ShipTo != null ? orderHead.ShipTo.TelephoneNumber : String.Empty);

                //供应商传真 Fax:	
                this.SetRowCell(++rowNum, 3, orderHead.ShipFrom != null ? orderHead.ShipFrom.Fax : String.Empty);
                //YFV传真 Fax:
                this.SetRowCell(rowNum, 8, orderHead.ShipTo != null ? orderHead.ShipTo.Fax : String.Empty);

                //系统号 SysCode:
                this.SetRowCell(++rowNum, 3, "");
                //版本号 Version:
                this.SetRowCell(rowNum, 8, "");

                #endregion

                #region 明细部分

                rowNum++; //指向13
                rowNum++; //指向14
                rowNum++; //指向15

                for (int detailRowCount = 33, i = 0; i < orderDetails.Count; i++, rowNum++)
                {
                    OrderDetail orderDetail = orderDetails[i];

                    // No.	
                    this.SetRowCell(rowNum, 0, "" + orderDetail.Sequence);

                    //零件号 Item Code
                    this.SetRowCell(rowNum, 1, orderDetail.Item.Code);

                    //参考号 Ref No.
                    this.SetRowCell(rowNum, 2, orderDetail.ReferenceItemCode);

                    //描述Description
                    this.SetRowCell(rowNum, 3, orderDetail.Item.Description);

                    //单位Unit
                    this.SetRowCell(rowNum, 4, orderDetail.Item.Uom.Code);

                    //单包装UC
                    this.SetRowCell(rowNum, 5, orderDetail.UnitCount.ToString("0.########"));

                    //需求 Request	包装
                    int UCs = (int)Math.Ceiling(orderDetail.OrderedQty / orderDetail.UnitCount);
                    this.SetRowCell(rowNum, 6, UCs.ToString());

                    //需求 Request	零件数
                    this.SetRowCell(rowNum, 7, orderDetail.OrderedQty.ToString("0.########"));

                    //发货数
                    this.SetRowCell(rowNum, 8, orderDetail.ShippedQty.HasValue ? orderDetail.ShippedQty.Value.ToString("0.########") : string.Empty);

                    //实收 Received	包装
                    this.SetRowCell(rowNum, 9, "");

                    //实收 Received	零件数
                    this.SetRowCell(rowNum, 10, orderDetail.ReceivedQty.HasValue ? orderDetail.ReceivedQty.Value.ToString("0.########") : string.Empty);

                    //批号/备注
                    this.SetRowCell(rowNum, 11, "");


                    if (i != 0 // 第一行,模版有一页,所以不处理
                            && (i % detailRowCount == 0// 非第一页,当页最后一行
                            || (i > detailRowCount && i == (orderDetails.Count - 1))))// 非第一页,明细最后一条
                    {
                        //非第一页,明细最后一条
                        if (i > detailRowCount && i == (orderDetails.Count - 1))
                        {// 补足空行
                            //需要补白的行数
                            rowNum = this.FillBlankLine(detailRowCount, rowNum, i);
                        }
                        if (i == detailRowCount)
                        {// 第一页
                            rowNum += 3;
                        }
                        else
                        {
                            // 第一页的尾部三行模版中有了,第二页以后需要手动加入
                            rowNum = this.FillPagefooting(rowNum);
                        }
                    }
                    else if (i == 0)
                    {
                        this.SetRowCellFormula(49, 6, "SUM(G16:G49)");
                    }

                }
                #endregion

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

        /*
         * 补白空行
         *
         * Param detailRowCount 明细行数
         * Param rowNum 行号
         * Param i 值
         * 
         * Return 行号
         */
        public int FillBlankLine(int detailRowCount,
            int rowNum, int i)
        {
            int blMax = detailRowCount - i % detailRowCount + 1;
            // 行数blank line
            for (int bl = 0; bl <= blMax; bl++)
            {
                int page = 1;
                int temp = 15;
                if (rowNum >= 52)
                {
                    page += (int)(Math.Ceiling((rowNum - 51) / 51.0));
                }
                if (page == 2)
                {
                    temp = rowNum - 37;
                }
                else if (page > 2)
                {
                    temp = rowNum - 37 - (page - 2) * 37;
                }
                //System.out.println(rowNum+"="+temp);
                //列
                for (int bc = 0; bc <= 11; bc++)
                {
                    this.CopyCellStyle(this.GetCell(temp, bc), this.GetCell(rowNum, bc));
                }
                //设定行高
                this.sheet.GetRow(rowNum).HeightInPoints = 15.75F;
                if (bl != (blMax))
                    rowNum++;
            }
            return rowNum;
        }

        /*
         * 补页脚
         *
         * Param rowNum 行号
         * 
         * Return 行号
         */
        public int FillPagefooting(int rowNum)
        {
            // 包装合计
            int pageEndRowNo = rowNum + 3;
            for (; rowNum <= pageEndRowNo; rowNum++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (rowNum == (pageEndRowNo - 2))
                    {
                        if (j == 5)
                        {
                            this.SetRowCellFormula(rowNum, j, "F50");
                        }
                        if (j == 6)
                        {
                            this.SetRowCellFormula(rowNum, j, "SUBTOTAL(9,G" + (rowNum - 33) + ":G" + rowNum + ")");
                        }
                        this.CopyCellStyle(this.GetCell(49, j), this.GetCell(rowNum, j));
                    }

                    if (rowNum == (pageEndRowNo - 1))
                    {

                        // //实际到货时间:
                        if (j == 1)
                        {
                            // xls.setMergedRegion(rowNum, j, rowNum,
                            // 1);
                            this.SetRowCellFormula(rowNum, j, "B51");
                        }
                        else if (j == 5)
                        {
                            // 发单人签字:
                            this.SetRowCellFormula(rowNum, j, "F51");
                        }
                        else if (j == 9)
                        {
                            // 供应商签字:
                            this.SetRowCellFormula(rowNum, j, "J51");
                        }

                        this.CopyCellStyle(this.GetCell(50, j), this.GetCell(rowNum, j));
                    }
                    if (rowNum == (pageEndRowNo))
                    {
                        // * 我已阅读上海恺杰的安全告知！
                        this.SetRowCellFormula(rowNum, 0, "A52");
                        CopyCellStyle(this.GetCell(51, 0), this.GetCell(rowNum, 0));
                        break;
                    }
                }

                //设定行高
                this.sheet.GetRow(rowNum).HeightInPoints = 14.25F;
            }
            rowNum--;
            return rowNum;
        }

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
