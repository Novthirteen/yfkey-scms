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
    public class RepRequisitionOrderContractMgr : RepTemplate1
    {

        private IOrderHeadMgr orderHeadMgr;
        public RepRequisitionOrderContractMgr(String reportTemplateFolder, IOrderHeadMgr orderHeadMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.orderHeadMgr = orderHeadMgr;

            //明细部分的行数
            this.pageDetailRowCount = 7;
            //列数   1起始
            this.columnCount = 8;
            //报表头的行数  1起始
            this.headRowCount = 12;
            //报表尾的行数  1起始
            this.bottomRowCount = 35;

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

                this.CopyPage(orderDetails.Count);

                this.FillHead(orderHead);

                //自动换行
                //style.WrapText = true;

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (OrderDetail orderDetail in orderDetails)
                {

                    //Item	
                    this.SetRowCell(pageIndex, rowIndex, 0, "" + (rowIndex + 1));

                    //Part No.	
                    this.SetRowCell(pageIndex, rowIndex, 1, orderDetail.ReferenceItemCode);
                    //QAD
                    this.SetRowCell(pageIndex, rowIndex, 2, orderDetail.Item.Code);
                    //Description 	
                    this.SetRowCell(pageIndex, rowIndex, 3, orderDetail.Item.Description);
                    //Quantity		
                    this.SetRowCell(pageIndex, rowIndex, 4, orderDetail.OrderedQty.ToString("0.########"));
                    this.SetRowCell(pageIndex, rowIndex, 5, orderDetail.Uom.Code);
                    //Unit Price
                    this.SetRowCell(pageIndex, rowIndex, 6, orderDetail.PriceListDetailFrom == null ? "" : orderDetail.PriceListDetailFrom.UnitPrice.ToString("0.########"));
                    //Total //=G3*E3
                    this.SetRowCell(pageIndex, rowIndex, 7, orderDetail.PriceListDetailFrom == null ? 0 : Double.Parse((orderDetail.OrderedQty * orderDetail.PriceListDetailFrom.UnitPrice).ToString("0.########")));

                    if (this.isPageBottom(rowIndex, rowTotal))//页的最后一行
                    {

                        this.SetRowCellFormula(this.GetRowIndexAbsolute(pageIndex, this.pageDetailRowCount), 7, "SUBTOTAL(9,H" + (this.GetRowIndexAbsolute(pageIndex, 0) + 1) + ":H" + (this.GetRowIndexAbsolute(pageIndex, this.pageDetailRowCount)) + ")");

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


            this.SetRowCell(0, 6, "No.: " + orderHead.OrderNo);
            this.SetRowCell(1, 6, "Date: " + orderHead.CreateDate.ToString("yyyy-MM-dd"));

            //C6
            this.SetRowCell(5, 2, orderHead.PartyFrom.Name);
            //C7
            this.SetRowCell(6, 2, "Add:" + orderHead.ShipFrom.Address);
            //C8
            this.SetRowCell(7, 2, "Tel: " + orderHead.ShipFrom.TelephoneNumber);
            //G8
            this.SetRowCell(7, 5, "Fax: " + orderHead.ShipFrom.Fax);



            //(1) Packing: 
            //this.SetRowCell(20, 2, );
            //(2) Country of origin and manufacturers:
            this.SetRowCell(21, 3, orderHead.PartyFrom.Country);
            //(3) Payment Terms:
            this.SetRowCell(22, 3, orderHead.PartyFrom.PaymentTerm);
            //(4) Freight and Insurance:
            //this.SetRowCell(23, 3, );
            //(5) Pick up date
            //this.SetRowCell(24, 3, );
            //Time of Delivery (ETD) 
            this.SetRowCell(25, 3, orderHead.StartTime.ToString("yyyy-MM-dd"));
            //Time of Arrival (ETA): 
            this.SetRowCell(26, 3, orderHead.WindowTime.ToString("yyyy-MM-dd"));
            //(6) Shipping marks and label:
            // this.SetRowCell(27, 3, );
            //this.SetRowCell(28, 3, );

            //KEY AUTOMOTIVE OF FLORIDA, INC.
            this.SetRowCell(50, 0, orderHead.PartyFrom.Name);
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {

            this.SetMergedRegion(pageIndex, 19 - this.headRowCount, 0, 19 - this.headRowCount, 6);

            //(1) Packing: 
            this.CopyCell(pageIndex, 20 - this.headRowCount, 0, "A21");

            //(2) Country of origin and manufacturers:
            this.CopyCell(pageIndex, 21 - this.headRowCount, 0, "A22");

            //(3) Payment Terms:
            this.CopyCell(pageIndex, 22 - this.headRowCount, 0, "A23");

            //(4) Freight and Insurance:
            this.CopyCell(pageIndex, 23 - this.headRowCount, 0, "A24");
            //(5) Pick up date
            this.CopyCell(pageIndex, 24 - this.headRowCount, 0, "A25");
            //Time of Delivery (ETD) 
            this.CopyCell(pageIndex, 25 - this.headRowCount, 1, "B26");
            //Time of Arrival (ETA): 
            this.CopyCell(pageIndex, 26 - this.headRowCount, 1, "B27");
            //(6) Shipping marks and label:
            this.CopyCell(pageIndex, 27 - this.headRowCount, 0, "A28");
            //Each package shall be stenciled conspicuously: port of destination, package number, gross and net 
            this.CopyCell(pageIndex, 29 - this.headRowCount, 1, "B30");
            //weight , measurement and the shipping mark shown above. (For dangerous and /or poisonous cargo 
            this.CopyCell(pageIndex, 30 - this.headRowCount, 1, "B31");
            //the nature and the generally adopted symbol shall be marked conspicuously on each package) 
            this.CopyCell(pageIndex, 31 - this.headRowCount, 1, "B32");
            //If the package contains the wood material, it should be stamped ‘IPPC’ on the pallet.
            this.CopyCell(pageIndex, 32 - this.headRowCount, 1, "B33");
            //(7) Port of destination: 
            this.CopyCell(pageIndex, 33 - this.headRowCount, 0, "A34");
            //(8) Planning Engineer: 
            this.CopyCell(pageIndex, 34 - this.headRowCount, 0, "A35");
            //Consignee (Attention) :
            this.CopyCell(pageIndex, 35 - this.headRowCount, 1, "B36");
            //(9) Sea freight documents required: All the Bill of Lading should be surrender bill. The sellers should send 
            this.CopyCell(pageIndex, 36 - this.headRowCount, 0, "A37");
            //all set of clean documents, B/L, invoice, packing list, non-wood packing certificate ( if packing is non-
            this.CopyCell(pageIndex, 37 - this.headRowCount, 1, "B38");
            //wood) and insurance bill if needed to the buyers in one working days by fax after the Vessel delivery 
            this.CopyCell(pageIndex, 38 - this.headRowCount, 1, "B39");
            //date. 
            this.CopyCell(pageIndex, 39 - this.headRowCount, 1, "B40");
            //Airfreight documents required: The sellers should send all set of clean documents, AWB, invoice, 
            this.CopyCell(pageIndex, 40 - this.headRowCount, 1, "B41");
            //packing list, non-wood packing certificate ( if packing is non-wood), and insurance bill (CIF terms) to 
            this.CopyCell(pageIndex, 41 - this.headRowCount, 1, "B42");
            //the buyers in one working days by fax after shipping date.
            this.CopyCell(pageIndex, 42 - this.headRowCount, 1, "B43");
            //All these documents can be used to clear Chinese Customs. If the delay or wrong documents brings
            this.CopyCell(pageIndex, 44 - this.headRowCount, 1, "B45");
            //extra customs clearance fee such as storage and documents revision, the sellers should pay for the 
            this.CopyCell(pageIndex, 45 - this.headRowCount, 1, "B46");
            //corresponding expenditure. 
            this.CopyCell(pageIndex, 46 - this.headRowCount, 1, "B47");
            //This contract is conclude in accordance with the exchange of Buyers’ Fax/FIX/Letters dated and Seller dated
            this.CopyCell(pageIndex, 47 - this.headRowCount, 0, "A48");
            //THE SELLERS
            this.CopyCell(pageIndex, 49 - this.headRowCount, 1, "B50");
            //KEY AUTOMOTIVE OF FLORIDA, INC.
            this.CopyCell(pageIndex, 50 - this.headRowCount, 0, "A51");
            //THE BUYERS
            this.CopyCell(pageIndex, 49 - this.headRowCount, 6, "G50");
            //Yanfeng KSS (Shanghai) Automotive Safety Systems Co., Ltd
            this.CopyCell(pageIndex, 50 - this.headRowCount, 4, "E51");
            //this.CopyCell(pageIndex, 49, 6, "G50");
            this.CopyCell(pageIndex, 54 - this.headRowCount, 1, "B55");
            //Authorized Signature
            this.CopyCell(pageIndex, 54 - this.headRowCount, 6, "G55");

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
