using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    [Transactional]
    public class RepPickListMgr : RepTemplate1
    {
      
        private IPickListMgr pickListMgr;
        private IFlowMgr flowMgr;
        
        public RepPickListMgr(String reportTemplateFolder, IPickListMgr pickListMgr, IFlowMgr flowMgr)
        {
            this.flowMgr = flowMgr;
            this.reportTemplateFolder = reportTemplateFolder;
            this.pickListMgr = pickListMgr;

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
         * Param list [0]PickList
         *            
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {
                
                    PickList pickList = (PickList)list[0];
                    IList<PickListDetail> pickListDetails = pickList.PickListDetails;

                    if (pickList == null
                        || pickListDetails == null || pickListDetails.Count == 0)
                    {
                        return false;
                    }

                    this.barCodeFontName = this.GetBarcodeFontName(0, 7);
                    //this.SetRowCellBarCode(0, 0, 7);
                    this.CopyPage(pickListDetails.Count);

                    this.FillHead(pickList);


                    int pageIndex = 1;
                    int rowIndex = 0;
                    int rowTotal = 0;
                    int no = 1;
                    foreach (PickListDetail pickListDetail in pickListDetails)
                    {

                        // No.	
                        this.SetRowCell(pageIndex, rowIndex, 0, "" + (no++));

                        //零件号 Item Code
                        this.SetRowCell(pageIndex, rowIndex, 1, pickListDetail.Item.Code);

                        //描述Description
                        this.SetRowCell(pageIndex, rowIndex, 2, pickListDetail.Item.Description);

                        //单包装UC
                        this.SetRowCell(pageIndex, rowIndex, 3, pickListDetail.UnitCount.ToString("0.########"));

                        //需求 Request	包装
                        int UCs = (int)Math.Ceiling(pickListDetail.Qty / pickListDetail.UnitCount);
                        this.SetRowCell(pageIndex, rowIndex, 4, UCs.ToString());

                        //需求 Request	零件数
                        this.SetRowCell(pageIndex, rowIndex, 5, pickListDetail.Qty.ToString("0.########"));

                        //库位(loc)
                        this.SetRowCell(pageIndex, rowIndex, 6, pickListDetail.PrintLocationCode);
                        //批号（LOT）
                        this.SetRowCell(pageIndex, rowIndex, 7, pickListDetail.LotNo != null ? pickListDetail.LotNo : String.Empty);

                        //实收 Received	包装
                        this.SetRowCell(pageIndex, rowIndex, 8, String.Empty);

                        //实收 Received	零件数
                        this.SetRowCell(pageIndex, rowIndex, 9, String.Empty);

                        //备注
                        this.SetRowCell(pageIndex, rowIndex, 10, pickListDetail.Memo);


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

                    if (pickList.IsPrinted == null || pickList.IsPrinted == false)
                    {
                        pickList.IsPrinted = true;
                        pickListMgr.UpdatePickList(pickList);
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
         * Param pickList 订单头对象
         */
        private void FillHead(PickList pickList)
        {
            

            //订单号:
            string orderCode = Utility.BarcodeHelper.GetBarcodeStr(pickList.PickListNo, this.barCodeFontName);
            this.SetRowCell(0, 7, orderCode);
            //Order No.:
            this.SetRowCell(1, 7, pickList.PickListNo);

            if (pickList.PickListDetails == null 
                    || pickList.PickListDetails[0] == null 
                    || pickList.PickListDetails[0].OrderLocationTransaction == null 
                    || pickList.PickListDetails[0].OrderLocationTransaction.OrderDetail == null 
                    || pickList.PickListDetails[0].OrderLocationTransaction.OrderDetail.OrderHead == null
                    || "Normal".Equals(pickList.PickListDetails[0].OrderLocationTransaction.OrderDetail.OrderHead.Priority)) 
            {
                this.SetRowCell(2, 4, "");
            }
            else
            {
                this.SetRowCell(1, 4, "");
            }

            //源超市：
            if (pickList.Flow != null && pickList.Flow.Trim() != string.Empty)
            {
                Flow flow = this.flowMgr.LoadFlow(pickList.Flow);
                this.SetRowCell(2, 2, flow.LocationFrom == null ? string.Empty : flow.LocationFrom.Code);
                //目的超市：
                this.SetRowCell(3, 2, pickList.Flow);
                //领料地点：
                this.SetRowCell(4, 2, flow.LocationFrom == null ? string.Empty : flow.LocationFrom.Region.Code);
            }

            //窗口时间
            this.SetRowCell(2, 8, pickList.WindowTime.ToString("yyyy-MM-dd HH:mm"));
            //订单时间
            this.SetRowCell(3, 8, pickList.CreateDate.ToString("yyyy-MM-dd HH:mm"));
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
            PickList pickList = pickListMgr.LoadPickList(code, true);
            if (pickList != null)
            {
                list.Add(pickList);
            }
            return list;
        }

    }
}
