using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Entity.MRP;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    public class PurchasePlanDailyMgr : RepTemplate1
    {
        public PurchasePlanDailyMgr(String reportTemplateFolder)
        {
            //明细部分的行数
            this.pageDetailRowCount = 51;
            //列数   1起始
            this.columnCount = 131;
            //报表头的行数  1起始
            this.headRowCount = 0;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;

            this.reportTemplateFolder = reportTemplateFolder;
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
                if (list == null || list.Count < 3) return false;

                var pPlanList = (IList<PurchasePlanDet>)(list[0]);
                var traceList = (IList<PurchasePlanDetTrace>)(list[1]);
                var planOpenOrderList = (IList<PurchasePlanOpenOrder>)(list[2]);

                if (pPlanList == null || pPlanList.Count == 0)
                {
                    return false;
                }


                var planByDateIndexs = pPlanList.GroupBy(p => p.WindowTime).OrderBy(p => p.Key);
                var planByFlowItems = pPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

                this.pageDetailRowCount = planByFlowItems.Count() + 2;

                this.CopyPage(planByFlowItems.Count()+2);

                int cellIndex = 10;
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var planDate = planByDateIndex.Key.ToString("yyyy-MM-dd");
                    this.SetRowCell(1, 0, cellIndex, planDate);
                    cellIndex += 4;
                }

                int pageIndex = 1;
                int rowIndex = 2;
                int rowTotal = 0;
                foreach (var planByFlowItem in planByFlowItems)
                {
                    //序号	路线	物料号	物料描述	包装量	经济批量	安全库存	最大库存	期初库存	在途
									
                    var firstPlan = planByFlowItem.First();
                    var planDic = planByFlowItem.GroupBy(d => d.WindowTime).ToDictionary(d => d.Key, d => d.Sum(q => q.PurchaseQty));
                    this.SetRowCell(pageIndex, rowIndex, 0, rowIndex-1);
                    this.SetRowCell(pageIndex, rowIndex, 1, firstPlan.Flow);
                    this.SetRowCell(pageIndex, rowIndex, 2, firstPlan.Item);
                    this.SetRowCell(pageIndex, rowIndex, 3, firstPlan.ItemDesc);
                    this.SetRowCell(pageIndex, rowIndex, 4, firstPlan.UnitCount.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 5, firstPlan.MinLotSize.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 6, firstPlan.SafeStock.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 7, firstPlan.MaxStock.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 8, firstPlan.InitStock.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 9, firstPlan.InspectQty.ToString("0.##"));

                    int cell = 10;
                    var initStockQty = firstPlan.InitStock + firstPlan.InspectQty;
                    foreach (var planByDateIndex in planByDateIndexs)
                    {
                        var curenPlan = planByFlowItem.Where(p => p.WindowTime == planByDateIndex.Key);
                        var pPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new PurchasePlanDet();
                        this.SetRowCell(pageIndex, rowIndex, cell++, pPlanDet.ReqQty.ToString("0.##"));
                        this.SetRowCell(pageIndex, rowIndex, cell++, pPlanDet.OrderQty.ToString("0.##"));
                        this.SetRowCell(pageIndex, rowIndex, cell++, pPlanDet.PurchaseQty.ToString("0.##"));

                        initStockQty = initStockQty - pPlanDet.ReqQty + pPlanDet.OrderQty + pPlanDet.PurchaseQty;

                        this.SetRowCell(pageIndex, rowIndex, cell++, initStockQty.ToString("0.##"));
                       

                    }
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
        private void FillHead()
        {
            
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //this.CopyCell(pageIndex, 50, 1, "B51");
            //this.CopyCell(pageIndex, 50, 5, "F51");
            //this.CopyCell(pageIndex, 50, 9, "J51");
            //this.CopyCell(pageIndex, 51, 0, "A52");
        }


    }
}
