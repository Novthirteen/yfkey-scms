using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Service.Distribution;


namespace com.Sconit.Service.Report.Yfk.Impl
{
    [Transactional]
    public class RepDeliveryNoteMgr : RepTemplate1
    {

        private IInProcessLocationMgr inProcessLocationMgr;
        private IOrderHeadMgr orderHeadMgr;

        public RepDeliveryNoteMgr(String reportTemplateFolder, IInProcessLocationMgr inProcessLocationMgr, IOrderHeadMgr orderHeadMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.inProcessLocationMgr = inProcessLocationMgr;
            this.orderHeadMgr = orderHeadMgr;

            //明细部分的行数
            this.pageDetailRowCount = 30;
            //列数   1起始
            this.columnCount = 10;
            //报表头的行数  1起始
            this.headRowCount = 15;
            //报表尾的行数  1起始
            this.bottomRowCount = 2;

        }

        /**
         * 填充报表
         * 
         * Param list [0]InProcessLocation
         *            [1]inProcessLocationDetailList
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                if (list == null || list.Count < 2) return false;

                InProcessLocation inProcessLocation = (InProcessLocation)list[0];
                IList<InProcessLocationDetail> inProcessLocationDetailList = (IList<InProcessLocationDetail>)list[1];

                if (inProcessLocation == null
                    || inProcessLocationDetailList == null || inProcessLocationDetailList.Count == 0)
                {
                    return false;
                }

                //this.SetRowCellBarCode(0, 2, 5);
                List<Transformer> transformerList = Utility.TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocationDetailList);

                this.barCodeFontName = this.GetBarcodeFontName(2, 5);

                this.CopyPage(transformerList.Count);

                this.FillHead(inProcessLocation);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                //ASN号:
              //  List<Transformer> transformerList = Utility.TransformerHelper.ConvertInProcessLocationDetailsToTransformers(inProcessLocationDetailList);

                foreach (Transformer transformer in transformerList)
                {
                    OrderHead orderHead = orderHeadMgr.LoadOrderHead(transformer.OrderNo);

                    //订单号 (销售发运显示客户订单号,其它为订单号)
                    if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                        this.SetRowCell(pageIndex, rowIndex, 0, orderHead.ExternalOrderNo);
                    else
                        this.SetRowCell(pageIndex, rowIndex, 0, orderHead.OrderNo);

                    //序号.	
                    this.SetRowCell(pageIndex, rowIndex, 1, transformer.Sequence.ToString());

                    //"零件号Item Code"	
                    this.SetRowCell(pageIndex, rowIndex, 2, transformer.ItemCode);
                    //"参考号Ref No."	
                    //this.SetRowCell(pageIndex, rowIndex, 3, orderDetail.ReferenceItemCode);

                    //"描述Description"	
                    this.SetRowCell(pageIndex, rowIndex, 3, transformer.ItemDescription);
                    //"单位Unit"	
                    this.SetRowCell(pageIndex, rowIndex, 4, transformer.UomCode);
                    //"单包装UC"	
                    this.SetRowCell(pageIndex, rowIndex, 5, transformer.UnitCount.ToString("0.########"));
                    //发货 Delivery	包装
                    int UCs = (int)Math.Ceiling(transformer.Qty / transformer.UnitCount);
                    this.SetRowCell(pageIndex, rowIndex, 6, UCs.ToString());

                    //发货 Delivery	发货数
                    this.SetRowCell(pageIndex, rowIndex, 7, transformer.Qty.ToString("0.########"));

                    //实收 Received	包装
                    //this.SetRowCell(pageIndex, rowIndex, 8, "");
                    //实收 Received	零件数
                    //this.SetRowCell(pageIndex, rowIndex, 9, "");


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

                if (inProcessLocation.IsPrinted == null || inProcessLocation.IsPrinted == false)
                {
                    inProcessLocation.IsPrinted = true;
                }
                inProcessLocation.PrintCount += 1;
                inProcessLocationMgr.UpdateInProcessLocation(inProcessLocation);

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
        private void FillHead(InProcessLocation inProcessLocation)
        {
            string asnCode = Utility.BarcodeHelper.GetBarcodeStr(inProcessLocation.IpNo, this.barCodeFontName);
            //ASN号:
            this.SetRowCell(2, 5, asnCode);
            //this.AddPicture
            //ASN No.:
            this.SetRowCell(3, 5, inProcessLocation.IpNo);

            //窗口时间 Window Time:
            this.SetRowCell(4, 5, inProcessLocation.InProcessLocationDetails[0].OrderLocationTransaction.OrderDetail.OrderHead.WindowTime.ToString("yyyy-MM-dd HH:mm"));

            //供应商代码 Supplier Code:
            this.SetRowCell(6, 3, inProcessLocation.PartyFrom == null ? string.Empty : inProcessLocation.PartyFrom.Code);
            //供应商名称 Supplier Name:		
            this.SetRowCell(7, 3, inProcessLocation.PartyFrom == null ? string.Empty : inProcessLocation.PartyFrom.Name);
            //供应商地址 Supplier Address:		
            this.SetRowCell(8, 3, inProcessLocation.ShipFrom == null ? string.Empty : inProcessLocation.ShipFrom.Address);
            //供应商联系人 Contact:		
            this.SetRowCell(9, 3, inProcessLocation.ShipFrom == null ? string.Empty : inProcessLocation.ShipFrom.ContactPersonName);
            //供应商联系人 Telephone:		
            this.SetRowCell(10, 3, inProcessLocation.ShipFrom == null ? string.Empty : inProcessLocation.ShipFrom.TelephoneNumber);
            //供应商电话 Fax:		
            this.SetRowCell(11, 3, inProcessLocation.ShipFrom == null ? string.Empty : inProcessLocation.ShipFrom.Fax);


            //客户代码 Customer Code:
            this.SetRowCell(6, 8, inProcessLocation.PartyTo == null ? string.Empty : inProcessLocation.PartyTo.Code);
            //客户名称 Customer Name:		
            this.SetRowCell(7, 8, inProcessLocation.PartyTo == null ? string.Empty : inProcessLocation.PartyTo.Name);
            //收货地址 Address:		
            this.SetRowCell(8, 8, inProcessLocation.ShipTo == null ? string.Empty : inProcessLocation.ShipTo.Address);
            //交货道口 Delivery Dock:		
            this.SetRowCell(9, 8, inProcessLocation.DockDescription);
            //物流协调员 Follow Up:		
            this.SetRowCell(10, 8, inProcessLocation.ShipTo == null ? string.Empty : inProcessLocation.ShipTo.ContactPersonName);
            //客户电话 Telephone:		
            this.SetRowCell(11, 8, inProcessLocation.ShipTo == null ? string.Empty : inProcessLocation.ShipTo.TelephoneNumber);
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //this.SetMergedRegion(pageIndex, 45 , 2, 45 , 3);
            //this.SetMergedRegion(pageIndex, 45 , 5, 45 , 7);
            //this.SetMergedRegion(pageIndex, 45 , 9, 45 , 10);

            //包装合计
            //this.CopyCell(pageIndex, 49 , 6, "G50");
            //实际到货时间:
            this.CopyCell(pageIndex, 45 - this.headRowCount, 1, "B46");
            //发单人签字:
            this.CopyCell(pageIndex, 45 - this.headRowCount, 3, "D46");
            //客户签字:
            this.CopyCell(pageIndex, 45 - this.headRowCount, 7, "H46");
            //* 我已阅读延锋杰华的安全告知！
            this.CopyCell(pageIndex, 46 - this.headRowCount, 0, "A47");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            InProcessLocation inProcessLocation = inProcessLocationMgr.LoadInProcessLocation(code, true);
            if (inProcessLocation != null)
            {
                list.Add(inProcessLocation);
            }
            return list;
        }

    }


}
