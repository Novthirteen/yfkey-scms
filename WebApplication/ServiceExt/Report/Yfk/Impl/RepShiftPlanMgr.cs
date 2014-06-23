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
    public class RepShiftPlanMgr : RepTemplate1
    {
        public RepShiftPlanMgr(String reportTemplateFolder)
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

              //  PrintOrderMaster orderMaster = (PrintOrderMaster)(list[0]);
                var firmPlanList = (IList<FirmPlan>)(list[0]);
                var planInvList = (IList<PlanInv>)(list[1]);
                var shiftPlanList = (IList<ShiftPlan>)(list[2]);

               // orderDetails = orderDetails.OrderBy(o => o.Sequence).ThenBy(o => o.Item).ToList();

                if (firmPlanList == null || firmPlanList.Count == 0)
                {
                    return false;
                }

                var planInvDic = planInvList.ToDictionary(d => d.ItemCode, d => d);
                var planByDateIndexs = firmPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);
                var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode)
                    .GroupBy(p => new { p.FlowCode, p.ItemCode });

                this.pageDetailRowCount = planByFlowItems.Count()+2;

                this.CopyPage(planByFlowItems.Count()+2);

                int cellIndex = 5;
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var planDate = planByDateIndex.Key.ToString("yyyy-MM-dd");
                    this.SetRowCell(1, 0, cellIndex, planDate);
                    cellIndex += 9;
                }

                int pageIndex = 1;
                int rowIndex = 2;
                int rowTotal = 0;
                foreach (var planByFlowItem in planByFlowItems)
                {
                    var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
                    var firstPlan = planByFlowItem.First();
                    var planDic = planByFlowItem.GroupBy(p => p.PlanDate).ToDictionary(d => d.Key, d =>new double[]{ d.Sum(q => q.OutQty),d.Sum(q=>q.InProdQty)});
                    //生产线
                    this.SetRowCell(pageIndex, rowIndex, 0, planByFlowItem.Key.FlowCode);
                    // No.零件号	
                    this.SetRowCell(pageIndex, rowIndex, 1, planByFlowItem.Key.ItemCode);

                    //物料描述
                    this.SetRowCell(pageIndex, rowIndex, 2, firstPlan.ItemDescription);

                    //安全
                    this.SetRowCell(pageIndex, rowIndex, 3, firstPlan.SafeStock.ToString("0.00"));

                    //库存
                    this.SetRowCell(pageIndex, rowIndex, 4, planInv.InvQty.ToString("0.00"));


                    double invQtyByDate = planInv.InvQty;
                    int dateI = 5;
                    foreach (var planByDateIndex in planByDateIndexs)
                    {
                        var qtys = planDic.ValueOrDefault(planByDateIndex.Key);
                        //待生产                                                                           
                        this.SetRowCell(pageIndex, rowIndex, dateI, qtys[1].ToString("0.##"));
                        dateI++;
                        //var outQty = invQtyByDate + qtys[1] - qtys[0];//需求
                        //if (outQty < firstPlan.SafeStock)
                        //{
                        //    outQty = firstPlan.SafeStock - outQty;
                        //}
                        this.SetRowCell(pageIndex, rowIndex, dateI, qtys[0].ToString("0.##"));
                        dateI++;
                        var shiftplansByDate = shiftPlanList.Where(s => s.PlanDate == planByDateIndex.Key && s.Flow == planByFlowItem.Key.FlowCode && s.Item == planByFlowItem.Key.ItemCode);
                        if (shiftplansByDate != null && shiftplansByDate.Count() > 0)
                        {
                            //foreach (var shiftplans in shiftplansByDate.OrderBy(s => s.Shift))
                            //{
                            //    this.SetRowCell(pageIndex, rowIndex, dateI, shiftplans.Qty.ToString("0.##"));
                            //    dateI++;
                            //    this.SetRowCell(pageIndex, rowIndex, dateI, shiftplans.Memo);
                            //    dateI++;
                            //}
                            var aShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "A");
                            var bShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "B");
                            var cShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "C");
                            if (aShift != null && aShift.Count() > 0)
                            {
                                this.SetRowCell(pageIndex, rowIndex, dateI, aShift.First().Qty.ToString("0.##"));
                                dateI++;
                                this.SetRowCell(pageIndex, rowIndex, dateI, aShift.First().Memo);
                                dateI++;
                            }
                            else
                            {
                                dateI += 2;
                            }
                            if (bShift != null && bShift.Count() > 0)
                            {
                                this.SetRowCell(pageIndex, rowIndex, dateI, bShift.First().Qty.ToString("0.##"));
                                dateI++;
                                this.SetRowCell(pageIndex, rowIndex, dateI, bShift.First().Memo);
                                dateI++;
                            }
                            else
                            {
                                dateI += 2;
                            }
                            if (cShift != null && cShift.Count() > 0)
                            {
                                this.SetRowCell(pageIndex, rowIndex, dateI, cShift.First().Qty.ToString("0.##"));
                                dateI++;
                                this.SetRowCell(pageIndex, rowIndex, dateI, cShift.First().Memo);
                                dateI++;
                            }
                            else
                            {
                                dateI += 2;
                            }
                        }
                        else
                        {
                            dateI += 6;
                        }
                        invQtyByDate = invQtyByDate + qtys[1] - qtys[0] + (shiftplansByDate != null ? shiftplansByDate.Sum(s => s.Qty) : 0);
                        //当天剩余库存
                        this.SetRowCell(pageIndex, rowIndex, dateI, invQtyByDate.ToString("0.00"));
                        dateI++;
                       
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
