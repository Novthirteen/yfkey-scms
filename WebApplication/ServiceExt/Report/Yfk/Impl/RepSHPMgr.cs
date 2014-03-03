using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Transportation;
using com.Sconit.Entity;
using com.Sconit.Service;

using Castle.Services.Transaction;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Transportation;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    [Transactional]
    public class RepSHPMgr : RepTemplate1
    {
        private ITransportationOrderMgr transportationOrderMgr;
        private IInProcessLocationMgr inProcessLocationMgr;

        public RepSHPMgr(String reportTemplateFolder, ITransportationOrderMgr transportationOrderMgr, IInProcessLocationMgr inProcessLocationMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.transportationOrderMgr = transportationOrderMgr;
            this.inProcessLocationMgr = inProcessLocationMgr;

            //明细部分的行数
            this.pageDetailRowCount = 30;
            //列数   1起始
            this.columnCount = 11;
            //报表头的行数  1起始
            this.headRowCount = 15;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            TransportationOrder transportationOrder = transportationOrderMgr.LoadTransportationOrder(code, true);
            if (transportationOrder != null)
            {
                list.Add(transportationOrder);
                list.Add(transportationOrder.OrderDetails);
            }
            return list;
        }

        /*
         * 填充报表头
         */
        private void FillHead(TransportationOrder transportationOrder)
        {
            string shpCode = Utility.BarcodeHelper.GetBarcodeStr(transportationOrder.OrderNo, this.barCodeFontName);
            //SHP号:
            this.SetRowCell(2, 8, shpCode);
            //this.AddPicture
            //SHP No.:
            this.SetRowCell(3, 8, transportationOrder.OrderNo);

            //发出时间 Start Date:
            this.SetRowCell(4, 9, ((DateTime)transportationOrder.StartDate).ToString("yyyy-MM-dd HH:mm"));

            //发货地址 Shipping address:		
            this.SetRowCell(6, 3, transportationOrder.TransportationRoute == null || transportationOrder.TransportationRoute.ShipFrom == null ? string.Empty : transportationOrder.TransportationRoute.ShipFrom.FullAddress);
            //车牌 Vehicle Code:			
            this.SetRowCell(7, 3, transportationOrder.Vehicle == null ? string.Empty : transportationOrder.Vehicle);
            //车辆类型 Vehicle Type:			
            this.SetRowCell(8, 3, transportationOrder.VehicleType);
            //司机 Driver:		
            this.SetRowCell(9, 3,  string.Empty );
            //托盘数 Pallent Count:		
            this.SetRowCell(10, 3, transportationOrder.PallentCount.ToString());


            //收货地址 Delivery Address:		
            this.SetRowCell(6, 8, transportationOrder.TransportationRoute == null || transportationOrder.TransportationRoute.ShipTo == null ? string.Empty : transportationOrder.TransportationRoute.ShipTo.FullAddress);
            //承运商 Carrier Name:		
            this.SetRowCell(7, 8, transportationOrder.Carrier == null ? string.Empty : transportationOrder.Carrier.Name);
            //承运商电话 Carrier Tel:		
            //this.SetRowCell(8, 8, inProcessLocation.ShipTo == null ? string.Empty : inProcessLocation.ShipTo.Address);
            //承运商联系人 Carrier Contact:		
            //this.SetRowCell(9, 8, inProcessLocation.DockDescription);
        }

        /**
         * 填充报表
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {
                if (list == null || list.Count < 2) return false;

                TransportationOrder transportationOrder = (TransportationOrder)list[0];
                IList<TransportationOrderDetail> transportationOrderDetailList = (IList<TransportationOrderDetail>)list[1];

                if (transportationOrder == null
                    || transportationOrderDetailList == null || transportationOrderDetailList.Count == 0)
                {
                    return false;
                }

                this.barCodeFontName = this.GetBarcodeFontName(2, 8);

                this.CopyPage(transportationOrderDetailList.Count);

                this.FillHead(transportationOrder);

                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;

                foreach (TransportationOrderDetail transportationOrderDetail in transportationOrderDetailList)
                {
                    InProcessLocation inProcessLocation = transportationOrderDetail.InProcessLocation;
                    if (inProcessLocation == null) return false;

                    //ASN号 ASN No.
                    this.SetRowCell(pageIndex, rowIndex, 0, inProcessLocation.IpNo);

                    //ASN.PartyFrom	
                    this.SetRowCell(pageIndex, rowIndex, 1, inProcessLocation.PartyFrom == null ? string.Empty : inProcessLocation.PartyFrom.Name);

                    //ASN.PartyTo
                    this.SetRowCell(pageIndex, rowIndex, 2, inProcessLocation.PartyTo == null ? string.Empty : inProcessLocation.PartyTo.Name);

                    //ASN.ShipFrom
                    this.SetRowCell(pageIndex, rowIndex, 3, inProcessLocation.ShipFrom == null ? string.Empty : inProcessLocation.ShipFrom.Address);
                    
                    //ASN.ShipTo
                    this.SetRowCell(pageIndex, rowIndex, 4, inProcessLocation.ShipTo == null ? string.Empty : inProcessLocation.ShipTo.Address);
                    
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

                //if (inProcessLocation.IsPrinted == null || inProcessLocation.IsPrinted == false)
                //{
                //    inProcessLocation.IsPrinted = true;
                //    inProcessLocationMgr.UpdateInProcessLocation(inProcessLocation);
                //}
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            ////this.SetMergedRegion(pageIndex, 45 , 2, 45 , 3);
            ////this.SetMergedRegion(pageIndex, 45 , 5, 45 , 7);
            ////this.SetMergedRegion(pageIndex, 45 , 9, 45 , 10);

            ////包装合计
            ////this.CopyCell(pageIndex, 49 , 6, "G50");
            ////实际到货时间:
            //this.CopyCell(pageIndex, 45 - this.headRowCount, 1, "B46");
            ////发单人签字:
            //this.CopyCell(pageIndex, 45 - this.headRowCount, 3, "D46");
            ////客户签字:
            //this.CopyCell(pageIndex, 45 - this.headRowCount, 7, "H46");
            ////* 我已阅读延锋杰华的安全告知！
            //this.CopyCell(pageIndex, 46 - this.headRowCount, 0, "A47");
        }
    }
}
