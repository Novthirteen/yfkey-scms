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
    public class ProductionPlanDailyMgr : RepTemplate1
    {
        public ProductionPlanDailyMgr(String reportTemplateFolder)
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
                if (list == null || list.Count < 1) return false;

                var productionPlanList = (IList<ProductionPlanDet>)(list[0]);

                if (productionPlanList == null || productionPlanList.Count == 0)
                {
                    return false;
                }
                var planByDateIndexs = productionPlanList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
                var planByItems = productionPlanList.GroupBy(p => p.Item);

                this.pageDetailRowCount = planByItems.Count() + 2;

                this.CopyPage(planByItems.Count() + 2);

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
                foreach (var planByItem in planByItems)
                {
                    var firstPlan = planByItem.First();
                    var planDic = planByItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
                    this.SetRowCell(pageIndex, rowIndex, 0, rowIndex-1);
                    this.SetRowCell(pageIndex, rowIndex, 1, firstPlan.Item);
                    this.SetRowCell(pageIndex, rowIndex, 2, firstPlan.ItemDesc);
                    this.SetRowCell(pageIndex, rowIndex, 3, firstPlan.RefItemCode);
                    this.SetRowCell(pageIndex, rowIndex, 4, firstPlan.UnitCount.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 5, firstPlan.MinLotSize.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 6, Convert.ToDouble(firstPlan.SafeStock));
                    this.SetRowCell(pageIndex, rowIndex, 7, Convert.ToDouble(firstPlan.MaxStock));
                    this.SetRowCell(pageIndex, rowIndex, 8, Convert.ToDouble(firstPlan.InitStock));
                    this.SetRowCell(pageIndex, rowIndex, 9, Convert.ToDouble(firstPlan.InTransitQty));

                    int cell = 10;
                    var initStockQty = firstPlan.InitStock + firstPlan.InspectQty;
                    foreach (var planByDateIndex in planByDateIndexs)
                    {
                        var curenPlan = planByItem.Where(p => p.StartTime == planByDateIndex.Key);
                        var planDet = curenPlan.Count() > 0 ? curenPlan.First() : new ProductionPlanDet();
                        this.SetRowCell(pageIndex, rowIndex, cell++, planDet.ReqQty.ToString("0.##"));
                        this.SetRowCell(pageIndex, rowIndex, cell++, planDet.OrderQty.ToString("0.##"));
                        this.SetRowCell(pageIndex, rowIndex, cell++, planDet.Qty.ToString("0.##"));


                        initStockQty = initStockQty + planDet.Qty - planDet.ReqQty + planDet.OrderQty;

                        this.SetRowCell(pageIndex, rowIndex, cell++, Convert.ToDouble(initStockQty));

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
