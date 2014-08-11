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
    public class ShipPlanDailyMgr : RepTemplate1
    {
        public ShipPlanDailyMgr(String reportTemplateFolder)
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
                if (list == null || list.Count < 4) return false;

              //  PrintOrderMaster orderMaster = (PrintOrderMaster)(list[0]);
                var shipPlanList = (IList<ShipPlanDet>)(list[0]);
                var traceList = (IList<ShipPlanDetTrace>)(list[1]);
                var shipPlanOpenOrderList = (IList<ShipPlanOpenOrder>)(list[2]);
                var ipDets = (IList<ShipPlanIpDet>)(list[3]);

                if (shipPlanList == null || shipPlanList.Count == 0)
                {
                    return false;
                }

                var planByDateIndexs = shipPlanList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
                var planByFlowItems = shipPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item, p.LocFrom, p.LocTo });

                this.pageDetailRowCount = planByFlowItems.Count()+2;

                this.CopyPage(planByFlowItems.Count()+2);

                int cellIndex = 11;
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var planDate = planByDateIndex.Key.ToString("yyyy-MM-dd");
                    this.SetRowCell(1, 0, cellIndex, planDate);
                    cellIndex += 5;
                }

                int pageIndex = 1;
                int rowIndex = 2;
                int rowTotal = 0;
                foreach (var planByFlowItem in planByFlowItems)
                {
                    var firstPlan = planByFlowItem.First();
                    var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.ShipQty));
                    /*rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Flow);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MrpLeadTime.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.RefItemCode);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.InitStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.InTransitQty.ToString("0.##"));*/
                    this.SetRowCell(pageIndex, rowIndex, 0, rowIndex-1);
                    this.SetRowCell(pageIndex, rowIndex, 1, firstPlan.Flow);
                    this.SetRowCell(pageIndex, rowIndex, 2, firstPlan.MrpLeadTime.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 3, firstPlan.Item);
                    this.SetRowCell(pageIndex, rowIndex, 4, firstPlan.ItemDesc);
                    this.SetRowCell(pageIndex, rowIndex, 5, firstPlan.RefItemCode);
                    this.SetRowCell(pageIndex, rowIndex, 6, firstPlan.UnitCount.ToString("0.##"));
                    this.SetRowCell(pageIndex, rowIndex, 7, Convert.ToDouble(firstPlan.SafeStock));
                    this.SetRowCell(pageIndex, rowIndex, 8, Convert.ToDouble(firstPlan.MaxStock));
                    this.SetRowCell(pageIndex, rowIndex, 9, Convert.ToDouble(firstPlan.InitStock));
                    this.SetRowCell(pageIndex, rowIndex, 10, Convert.ToDouble(firstPlan.InTransitQty));

                    int cell = 11;
                    foreach (var planByDateIndex in planByDateIndexs)
                    {
                        var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                        var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                        this.SetRowCell(pageIndex, rowIndex, cell++, shipPlanDet.ReqQty.ToString("0.##"));
                        this.SetRowCell(pageIndex, rowIndex, cell++, shipPlanDet.OrderQty.ToString("0.##"));
                        this.SetRowCell(pageIndex, rowIndex, cell++, shipPlanDet.ShipQty.ToString("0.##"));


                        var ipQty = ipDets.Where(i => i.Item == firstPlan.Item &&  i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item  && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                        var orderQtySum = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                        var shipQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.ShipQty);
                        var reqQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Sum(i => i.ReqQty) : 0;

                        var initStockQty = firstPlan.InitStock + ipQty + orderQtySum + shipQtySum - reqQtySum;
                        this.SetRowCell(pageIndex, rowIndex, cell++, Convert.ToDouble(initStockQty));

                        var inTransitQty = firstPlan.InTransitQty;
                        var ipQty2 = ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item  && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                        var orderQtySum2 = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                        var shipQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Sum(i => i.ShipQty) : 0;
                        inTransitQty = inTransitQty - ipQty2 + orderQtySum2 + shipQtySum2;

                        this.SetRowCell(pageIndex, rowIndex, cell++, Convert.ToDouble(inTransitQty));

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
