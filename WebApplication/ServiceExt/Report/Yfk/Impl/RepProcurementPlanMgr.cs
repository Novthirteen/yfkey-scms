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
    public class RepProcurementPlanMgr : RepTemplate1
    {
        public RepProcurementPlanMgr(String reportTemplateFolder)
        {
            //明细部分的行数
            this.pageDetailRowCount = 51;
            //列数   1起始
            this.columnCount = 31;
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
                if (list == null || list.Count < 2) return false;

                var firmPlanList = (IList<FirmPlan>)(list[0]);
                var planInvList = (IList<PlanInv>)(list[1]);

               // orderDetails = orderDetails.OrderBy(o => o.Sequence).ThenBy(o => o.Item).ToList();

                if (firmPlanList == null || firmPlanList.Count == 0)
                {
                    return false;
                }


                var planInvDic = planInvList
                    .GroupBy(p => p.ItemCode).Select(p => new PlanInv
                    {
                        InvQty = p.Sum(q => q.InvQty),
                        ItemCode = p.Key,
                        SafeStock = p.First().SafeStock,
                        MaxStock = p.First().MaxStock,
                        RecQty = p.Sum(q => q.RecQty)
                    })
                    .ToDictionary(d => d.ItemCode, d => d);
                var planByDateIndexs = firmPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key).ToList();
                var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode }).ToList();

                this.pageDetailRowCount = planByFlowItems.Count()+2;

                this.CopyPage(planByFlowItems.Count()+2);

                int cellIndex = 3;
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var planDate = planByDateIndex.Key.ToString("yyyy-MM-dd");
                    this.SetRowCell(1, 0, cellIndex, planDate);
                    cellIndex +=2;
                }

                int pageIndex = 1;
                int rowIndex = 2;
                int rowTotal = 0;
                foreach (var planByFlowItem in planByFlowItems)
                {
                    var firstPlan = planByFlowItem.First();
                    var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
                    var planDic = planByFlowItem.GroupBy(p => p.PlanDate)
                        .ToDictionary(d => d.Key, d => new { OutQty = d.Sum(q => q.OutQty), InQty = d.Sum(q => q.InQty) });
                    //生产线
                    this.SetRowCell(pageIndex, rowIndex, 0, planByFlowItem.Key.FlowCode);
                    // No.零件号	
                    this.SetRowCell(pageIndex, rowIndex, 1, planByFlowItem.Key.ItemCode);

                    //物料描述
                    this.SetRowCell(pageIndex, rowIndex, 2, firstPlan.ItemDescription);

                    double invQtyByDate = planInv.InvQty + planInv.RecQty;
                    double forcastQty = 0;
                    int dateI = 3;
                    foreach (var planByDateIndex in planByDateIndexs)
                    {
                        var plan = planDic.ValueOrDefault(planByDateIndex.Key) ?? new { OutQty = 0.0, InQty = 0.0 };
                        invQtyByDate = invQtyByDate + plan.InQty - plan.OutQty;
                        if (invQtyByDate < planInv.SafeStock)
                        {
                            forcastQty = planInv.SafeStock - invQtyByDate;
                            this.SetRowCell(pageIndex, rowIndex, dateI, planByDateIndex.Key.ToString("yyyy-MM-dd 00:00"));
                            dateI++;
                            this.SetRowCell(pageIndex, rowIndex, dateI, forcastQty.ToString("0.00"));
                            dateI++;
                            invQtyByDate += forcastQty;
                        }
                        else
                        {
                            dateI += 2;
                            forcastQty = 0;
                        }
                       
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
