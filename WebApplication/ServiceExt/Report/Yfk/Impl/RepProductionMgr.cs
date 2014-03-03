using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using NPOI.HSSF.UserModel;
using com.Sconit.Service.MasterData;
using NPOI.SS.UserModel;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    /**
     * 
     * 原材料条码
     * 
     */
    [Transactional]
    public class RepProductionMgr : ReportBaseMgr
    {
        
        private static readonly int ROW_COUNT = 10;
        //列数   1起始
        private static readonly int COLUMN_COUNT = 5;

        private IOrderHeadMgr orderHeadMgr;
        private IFlowMgr flowMgr;

        public RepProductionMgr(String reportTemplateFolder, IOrderHeadMgr orderHeadMgr, IFlowMgr flowMgr) 
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.orderHeadMgr = orderHeadMgr;
            this.flowMgr = flowMgr;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {
            
        }

        /**
         * 填充报表
         * 
         * Param list [0]huDetailList
         */
        [Transaction(TransactionMode.Requires)]
        public override bool FillValues(String templateFileName, IList<object> list)
        {
            try
            {
                
                    this.init(templateFileName, ROW_COUNT);

                    if (list == null || list.Count < 0) return false;

                    OrderHead orderHead = (OrderHead)(list[0]);

                    string userName = "";
                    if (list.Count == 2)
                    {
                        userName = (string)list[1];
                    }

                    /*
                    this.sheet.DefaultRowHeightInPoints = 14.25F;
                    //this.sheet.DefaultColumnWidth = 7;
                    //this.sheet.SetColumnWidth(1, 100 * 256);
                    sheet.DefaultColumnWidth = 7;
                    //sheet.DefaultRowHeight = 30 * 20;
                 
                    //sheet.PrintSetup.PaperSize = 
                    sheet.SetColumnWidth(0, 60 * 32);
                    sheet.SetColumnWidth(1, 60 * 32);
                    sheet.SetColumnWidth(2, 60 * 32);
                    sheet.SetColumnWidth(3, 60 * 32);
                    sheet.SetColumnWidth(4, 58 * 32);
                    */
                    this.sheet.DisplayGridlines = false;
                    this.sheet.IsPrintGridlines = false;


                    //this.sheet.DisplayGuts = false;

                    //加页删页
                    //this.CopyPage(pageCount, COLUMN_COUNT, 1);
                    this.barCodeFontName = this.GetBarcodeFontName(0, 0);

                    CellStyle cellStyleT = workbook.CreateCellStyle();
                    Font fontT = workbook.CreateFont();
                    fontT.FontHeightInPoints = (short)9;
                    fontT.FontName = "宋体";
                    fontT.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.BOLD;
                    cellStyleT.SetFont(fontT);

                    int pageIndex = 1;


                    string barCode = Utility.BarcodeHelper.GetBarcodeStr(orderHead.OrderNo, this.barCodeFontName);
                    this.SetRowCell(pageIndex, 0, 0, barCode);

                    this.SetRowCell(pageIndex, 1, 0, orderHead.OrderNo);

                    //生产线编号	
                    Flow flow = this.flowMgr.LoadFlow(orderHead.Flow);
                    this.SetRowCell(pageIndex, 3, 0, flow.Code);

                    //班次编号
                    this.SetRowCell(pageIndex, 3, 2, orderHead.Shift == null ? string.Empty : orderHead.Shift.Code);

                    //生产线名称
                    this.SetRowCell(pageIndex, 4, 0, flow.Description);

                    //产品描述	
                    if (orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
                    {
                        this.SetRowCell(pageIndex, 6, 0, orderHead.OrderDetails[0].Item.Description);
                    }

                    //开始时间	
                    this.SetRowCell(pageIndex, 8, 0, orderHead.WindowTime.ToString("yyyy-MM-dd HH:mm"));

                    //Printed Date:
                    this.SetRowCell(pageIndex, 9, 3, DateTime.Now.ToString("MM/dd/yy"));

                    this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, ROW_COUNT - 1));

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
            }
            return list;
        }
    }

}
