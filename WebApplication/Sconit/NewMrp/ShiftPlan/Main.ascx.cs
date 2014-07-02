using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MRP;
using com.Sconit.Utility;
using com.Sconit.Web;
using System.Data.SqlClient;
using com.Sconit.Entity.MasterData;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;
using System.Data;

public partial class NewMrp_ShiftPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.ucPreview.BtnCreateClick += new System.EventHandler(this.CalculateProdPlan_Render);

       
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    var shiftPlanList = TheMrpMgr.ReadShiftPlanFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser);
        //    if (shiftPlanList != null && shiftPlanList.Count() > 0)
        //    {
        //        shiftPlanList = shiftPlanList.Where(s => s.Qty > 0).ToList();
        //        IList<OrderHead> ohList = ConvertShiftPlanToOrders(shiftPlanList, 0);
        //        this.ucPreview.Visible = true;
        //        this.ucPreview.InitPageParameter(ohList, BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION);
        //    }
        //    //this.CalculateProdPlan();
        //    ShowSuccessMessage("导入成功。");

        //    //this.ListTable(shiftPlanList);
        //}
        //catch (BusinessErrorException ex)
        //{
        //    ShowErrorMessage(ex);
        //}
    }

    //private IList<OrderHead> ConvertShiftPlanToOrders(IList<ShiftPlan> shiftPlanList, int leadTime)
    //{
    //    IList<OrderHead> orderHeadList = new List<OrderHead>();
    //    if (shiftPlanList != null && shiftPlanList.Count > 0)
    //    {
    //        List<FlowDetail> tempFlowdets = new List<FlowDetail>();
    //        //string searchExistsSql = string.Format("select d.orderNo,d.Item,d.orderqty,isnull(d.recqty,0) as recqty from orderdet as d where exists(select 1 from ordermstr as m where m.orderno=d.orderno and m.type='Production' and m.Status in('In-Process') ) and d.orderqty>d.recqty and d.Item in('{0}') ",string.Join("','", shiftPlanList.Select(s=>s.Item).Distinct().ToArray()));
    //        //var existsDetRows = TheGenericMgr.GetDatasetBySql(searchExistsSql).Tables[0];
    //        //var existsDetList = new List<TempOrderDet>();
    //        //foreach (System.Data.DataRow row in existsDetRows.Rows)
    //        //{
    //        //    existsDetList.Add(new TempOrderDet{
    //        //        OrderNo = row[0].ToString(),
    //        //        ItemCode = row[1].ToString(),
    //        //        OrderedQty = Convert.ToDecimal(row[2].ToString()),
    //        //        ReceivedQty = Convert.ToDecimal(row[3].ToString()),
    //        //    });
    //        //}
    //        foreach (ShiftPlan sps in shiftPlanList)
    //        {
    //            var currentFlowDet = TheGenericMgr.GetDatasetBySql(string.Format("select  Id,orderlotsize,item  from flowdet where flow='{0}' and item='{1}' ", sps.Flow, sps.Item)).Tables[0];
    //            int flowDetId = 0;
    //            decimal orderLotSize = 0;
    //            foreach (System.Data.DataRow row in currentFlowDet.Rows)
    //            {
    //                flowDetId = Convert.ToInt32((object)row[0]);
    //                orderLotSize = !string.IsNullOrEmpty(row[1].ToString()) ? (decimal)Convert.ToDecimal(row[1].ToString()) : 0;
    //            }
    //            IList<decimal> reqQtyList = OrderHelper.SplitByOrderLotSize((decimal)sps.Qty, orderLotSize);
    //            DateTime startTime = TheShiftMgr.GetShiftStartTime(sps.PlanDate, sps.Shift);
    //            DateTime windowTime = startTime.AddHours(Convert.ToDouble(leadTime));

    //            //int i = 0;
    //            foreach (decimal reqQty in reqQtyList)
    //            {
    //                OrderHead oh = new OrderHead();
    //                Flow currentFlow = TheFlowMgr.LoadFlow(sps.Flow, CurrentUser.Code, true);
    //                if (tempFlowdets.Where(d => d.Flow.Code == currentFlow.Code).Count() > 0)
    //                {
    //                    currentFlow.FlowDetails = tempFlowdets.Where(d => d.Flow.Code == currentFlow.Code).ToList();
    //                }
    //                else
    //                {
    //                    tempFlowdets.AddRange(currentFlow.FlowDetails);
    //                }
    //                currentFlow.FlowDetails = currentFlow.FlowDetails.Where(d => d.Id == flowDetId).ToList();
    //                oh = TheOrderMgr.TransferFlow2Order(currentFlow, BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML, false, DateTime.Now);
    //                oh.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;

    //                oh.StartTime = startTime;
    //                oh.WindowTime = windowTime;

    //                oh.GetOrderDetailByFlowDetailIdAndItemCode(flowDetId, sps.Item).RequiredQty = reqQty;
    //                oh.GetOrderDetailByFlowDetailIdAndItemCode(flowDetId, sps.Item).OrderedQty = reqQty;
    //                oh.Shift = TheShiftMgr.LoadShift(sps.Shift);

    //                //if (i == 0)
    //                //{
    //                //    oh.ExistsProdDetails = existsDetList.Where(e => e.ItemCode == sps.Item).ToList();
    //                //} 
    //                //i++;


    //                orderHeadList.Add(oh);

    //                startTime = windowTime;
    //                windowTime = startTime.AddHours(Convert.ToDouble(leadTime));
    //            }
    //        }
    //    }

    //    OrderHelper.FilterZeroOrderQty(orderHeadList);
    //    return orderHeadList;
    //}


    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    DateTime startTime = DateTime.Today;
    //    if (this.tbStartDate.Text.Trim() != string.Empty)
    //    {
    //        DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
    //    }
    //    DateTime endTime = startTime.AddDays(15);
    //    var hql = "select p from {0} p where p.PlanDate>=? and p.PlanDate<? ";
    //    var paramList = new List<object> { startTime, endTime };
    //    if (this.tbFlow.Text.Trim() != string.Empty)
    //    {
    //        hql += " and p.Flow = ? ";
    //        paramList.Add(this.tbFlow.Text.Trim());
    //    }

    //    var planVersion = 0;
    //    int.TryParse(this.tbPlanVersion.Text.Trim(), out planVersion);
    //    if (planVersion == 0)
    //    {
    //        var shiftPlanList = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlan>(string.Format(hql, "ShiftPlan"), paramList.ToArray()) ?? new List<ShiftPlan>();

    //        ListTable(shiftPlanList);
    //    }
    //    else
    //    {
    //        var shiftPlanLogList = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlanLog>("from ShiftPlanLog where PlanVersion=? ", new object[] { planVersion });
    //        shiftPlanLogList = shiftPlanLogList ?? new List<ShiftPlanLog>();
    //        var shiftPlanList = shiftPlanLogList.Select(p =>
    //        {
    //            var plan = new ShiftPlan();
    //            plan.Flow = p.Flow;
    //            plan.Item = p.Item;
    //            plan.ItemDescription = p.ItemDescription;
    //            plan.PlanDate = p.PlanDate;
    //            plan.PlanVersion = p.PlanVersion;
    //            plan.Qty = p.Qty;
    //            plan.UnitQty = p.UnitQty;
    //            plan.Uom = p.Uom;
    //            plan.Shift = p.Shift;
    //            plan.LastModifyDate = p.CreateDate;
    //            return plan;
    //        }).ToList();
    //        var maxPlan = shiftPlanList.OrderBy(p => p.PlanVersion).LastOrDefault();
    //        //this.ltlPlanVersion.Text = string.Format("第{0}次导入,时间:{1}", maxPlan.PlanVersion, maxPlan.LastModifyDate);
    //        ListTable(shiftPlanList);
    //    }
    //}

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    DateTime startTime = DateTime.Today;
    //    if (this.tbStartDate.Text.Trim() != string.Empty)
    //    {
    //        DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
    //    }
    //    string flowCodeValues = this.tbFlow.Value.Trim();
    //    if (!string.IsNullOrEmpty(flowCodeValues))
    //    {
    //        flowCodeValues = flowCodeValues.Replace("\r\n", ",");
    //        flowCodeValues = flowCodeValues.Replace("\n", ",");
    //    }
    //    var sqlParams = new SqlParameter[5];
    //    sqlParams[0] = new SqlParameter("@FlowType", "Production");
    //    sqlParams[1] = new SqlParameter("@Operation", "");
    //    sqlParams[2] = new SqlParameter("@FlowCode", flowCodeValues);
    //    sqlParams[3] = new SqlParameter("@DateFrom", startTime);
    //    sqlParams[4] = new SqlParameter("@IsShow0", false);
    //    var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
    //    var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
    //    var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
    //    //ListTable(firmPlanList, planInvList);
    //    DateTime endTime = startTime.AddDays(15);
    //    var hql = "select p from {0} p where p.PlanDate>=? and p.PlanDate<? ";
    //    var paramList = new List<object> { startTime, endTime };
    //    if (flowCodeValues != string.Empty)
    //    {
    //        hql += string.Format(" and p.Flow  in ( '{0}') ",flowCodeValues.Replace(",","','"));
    //    }

    //    var planVersion = 0;
    //    int.TryParse(this.tbPlanVersion.Text.Trim(), out planVersion);
    //    if (planVersion == 0)
    //    {
    //        var shiftPlanList = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlan>(string.Format(hql, "ShiftPlan"), paramList.ToArray()) ?? new List<ShiftPlan>();

    //        ListTable(firmPlanList, planInvList, shiftPlanList);
    //    }
    //    else
    //    {
    //        var shiftPlanLogList = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlanLog>(string.Format(hql + " and p.PlanVersion={1} ", "ShiftPlan", planVersion), paramList.ToArray());
    //        shiftPlanLogList = shiftPlanLogList ?? new List<ShiftPlanLog>();
    //        var shiftPlanList = shiftPlanLogList.Select(p =>
    //        {
    //            var plan = new ShiftPlan();
    //            plan.Flow = p.Flow;
    //            plan.Item = p.Item;
    //            plan.ItemDescription = p.ItemDescription;
    //            plan.PlanDate = p.PlanDate;
    //            plan.PlanVersion = p.PlanVersion;
    //            plan.Qty = p.Qty;
    //            plan.UnitQty = p.UnitQty;
    //            plan.Uom = p.Uom;
    //            plan.Shift = p.Shift;
    //            plan.LastModifyDate = p.CreateDate;
    //            return plan;
    //        }).ToList();
    //        var maxPlan = shiftPlanList.OrderBy(p => p.PlanVersion).LastOrDefault();
    //        //this.ltlPlanVersion.Text = string.Format("第{0}次导入,时间:{1}", maxPlan.PlanVersion, maxPlan.LastModifyDate);
    //        ListTable(firmPlanList, planInvList, shiftPlanList);
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string hql = " select p From {0} as p where 1=1 ";
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        }
        hql += string.Format(" and p.PlanDate between '{0}' and '{1}' ", startTime,startTime.AddDays(13));
        //var firmPlanList = this.TheMrpMgr.GetFirmPlanList(this.tbFlow.Text.Trim(), startTime);
        string flowCodeValues = this.tbFlow.Value.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", ",");
            flowCodeValues = flowCodeValues.Replace("\n", ",");
        }
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            hql += string.Format(" and p.Flow in ('{0}') ", flowCodeValues);
        }

        IList<ProductionPlan> pdPlanList = TheGenericMgr.FindAllWithCustomQuery<ProductionPlan>(string.Format(hql, "ProductionPlan"));
        IList<ShiftPlan> shiftPlanList = TheGenericMgr.FindAllWithCustomQuery<ShiftPlan>(string.Format(hql, "ShiftPlan"));
        shiftPlanList = shiftPlanList == null || shiftPlanList.Count == 0 ? new List<ShiftPlan>() : shiftPlanList;
        ListTable(pdPlanList, shiftPlanList);
    }

    private void ListTable(IList<ProductionPlan> pdPlanList, IList<ShiftPlan> shiftPlanList)
    {
        if (pdPlanList == null || pdPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        var groupByFlowItems = pdPlanList.OrderBy(p => p.Flow).GroupBy(d => new { d.Flow, d.Item }).ToDictionary(d => d.Key, d => d.ToList());
        var planByDateIndexs = pdPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);
        var shiftPlanListDic = shiftPlanList.GroupBy(p => new { p.Flow, p.Item, p.PlanVersion })
       .ToDictionary(d => d.Key, d => d.ToList());
        List<ShiftPlanLog> shiftPlanLogList = new List<ShiftPlanLog>();
        var hql = "select p from ShiftPlanLog as p where  p.PlanDate>=? and p.PlanDate<=? and p.PlanVersion <> ? and p.Item=? and p.Flow = ? ";
        foreach (var dic in shiftPlanListDic)
        {
            var shiftPlan = dic.Value;
            var paramList = new List<object> { shiftPlan.Min(d => d.PlanDate), shiftPlan.Max(d => d.PlanDate), shiftPlan.First().PlanVersion, shiftPlan.First().Item, shiftPlan.First().Flow };
            var rr = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlanLog>(hql, paramList.ToArray());
            if (rr != null && rr.Count > 0)
            {
                shiftPlanLogList.AddRange(rr);
            }
        }

        StringBuilder str = new StringBuilder();
        string headStr = CopyString();
        str.Append("<tr class='GVHeader'><th rowspan='2' >Seq</th><th rowspan='2'>生产线</th><th rowspan='2' >物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>版本号</th><th rowspan='2'>排产时间</th><th rowspan='2'>上次排产时间</th><th rowspan='2' >安全</th><th rowspan='2' >当前</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            var currentDate = planByDateIndex.Key;
            ii++;
            str.Append("<th colspan='9' >");
            str.Append(currentDate.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 13)
        {
            widths = "380%";
        }
        else if (ii > 12)
        {
            widths = "350%";
        }
        else if (ii > 10)
        {
            widths = "300%";
        }
        else if (ii > 8)
        {
            widths = "265%";
        }
        else if (ii > 6)
        {
            widths = "215%";
        }
        else if (ii > 4)
        {
            widths = "170%";
        }
        else if (ii > 2)
        {
            widths = "130%";
        }
        headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'><thead>", widths);
        #endregion
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >待生产数</th><th >需求</th><th >早班</th><th >备注</th><th>中班</th><th >备注</th><th >晚班</th><th >备注</th><th >库存</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in groupByFlowItems)
        {
            //var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
            var firstPlan = planByFlowItem.Value.First();
            var currentShiftPlans = shiftPlanList.Where(s => s.Flow == firstPlan.Flow);
            var currentShiftPlan = currentShiftPlans != null && currentShiftPlans.Count() > 0 ? currentShiftPlans.First() : new ShiftPlan();
            var prevShiftPlan = shiftPlanLogList.Where(sl => sl.Flow == currentShiftPlan.Flow).Count() > 0 ? shiftPlanLogList.Where(sl => sl.Flow == currentShiftPlan.Flow).OrderByDescending(sl => sl.PlanVersion).First() : new ShiftPlanLog();
            var planDic = planByFlowItem.Value.GroupBy(p => p.PlanDate).ToDictionary(d => d.Key, d => new decimal[] { d.Sum(q => q.OutQty), d.Sum(q => q.InProdQty) });
            l++;
            if (l % 2 == 0)
            {
                str.Append("<tr class='GVAlternatingRow'>");
            }
            else
            {
                str.Append("<tr class='GVRow'>");
            }
          // "</th><th rowspan='2'>版本号</th><th rowspan='2'>排产时间</th><th rowspan='2'>上次排产时间</th><th rowspan='2' >安全</th><th rowspan='2' >当前</th>");
            str.Append("<td>");
            str.Append(l);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Flow);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDescription);
            str.Append("</td>");
            if (currentShiftPlan.PlanVersion != 0)
            {
                str.Append("<td>");
                str.Append(currentShiftPlan.PlanVersion);
                str.Append("</td>");
            }
            else
            {
                str.Append("<td>");
                str.Append("");
                str.Append("</td>");
            }
            if (currentShiftPlan.LastModifyDate == DateTime.MinValue)
            {
                str.Append("<td>");
                str.Append("");
                str.Append("</td>");
            }
            else
            {
                str.Append("<td>");
                str.Append(currentShiftPlan.LastModifyDate);
                str.Append("</td>");
            }
            if (prevShiftPlan.CreateDate == DateTime.MinValue)
            {
                str.Append("<td>");
                str.Append("");
                str.Append("</td>");
            }
            else
            {
                str.Append("<td>");
                str.Append(prevShiftPlan.CreateDate);
                str.Append("</td>");
            }
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.InvQty.ToString("0.##"));
            str.Append("</td>");
            var invQtyByDate = firstPlan.InvQty;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qtys = planDic.ValueOrDefault(planByDateIndex.Key);
                if (qtys[1] == 0)
                {
                    str.Append("<td>");
                    str.Append("");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td>");
                    str.Append(qtys[1].ToString("0.##")); //待生产
                    str.Append("</td>");
                }
                var outQty =  qtys[0];//需求
                //if (outQty < firstPlan.SafeStock)
                //{
                //    outQty = firstPlan.SafeStock - outQty;
                //}
                str.Append("<td>");
                str.Append(outQty.ToString("0.##"));
                str.Append("</td>");
                var shiftplansByDate = currentShiftPlans.Where(s => s.PlanDate == planByDateIndex.Key && s.Flow == planByFlowItem.Key.Flow && s.Item == planByFlowItem.Key.Item);
                if (shiftplansByDate != null && shiftplansByDate.Count() > 0)
                {
                    var shiftplanLogByDates = shiftPlanLogList.Where(s => s.PlanDate == planByDateIndex.Key && s.Flow == planByFlowItem.Key.Flow && s.Item == planByFlowItem.Key.Item);
                    var shiftPlnaLogs = shiftplanLogByDates != null ? shiftplanLogByDates.Where(s => s.PlanVersion == shiftplanLogByDates.Max(m => m.PlanVersion)) : null;

                    #region
                    var aShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "A");
                    var bShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "B");
                    var cShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "C");
                    if (aShift != null && aShift.Count() > 0)
                    {
                        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == aShift.First().Shift) : null;
                        if (currentLog != null && currentLog.Count() > 0)
                        {
                            if (currentLog.First().Qty != aShift.First().Qty)
                            {
                                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                            }
                            else
                            {
                                str.Append("<td style='background:yellow'>");
                            }
                        }
                        else
                        {
                            str.Append("<td>");
                        }
                        str.Append(aShift.First().Qty.ToString("0.##") + " </td> ");
                        str.Append("<td>" + aShift.First().Memo + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                        str.Append("<td></td>");

                    }
                    if (bShift != null && bShift.Count() > 0)
                    {
                        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == bShift.First().Shift) : null;
                        if (currentLog != null && currentLog.Count() > 0)
                        {
                            if (currentLog.First().Qty != bShift.First().Qty)
                            {
                                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                            }
                            else
                            {
                                str.Append("<td style='background:yellow'>");
                            }
                        }
                        else
                        {
                            str.Append("<td>");
                        }
                        str.Append(bShift.First().Qty.ToString("0.##") + " </td> ");
                        str.Append("<td>" + bShift.First().Memo + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                        str.Append("<td></td>");

                    }
                    if (cShift != null && cShift.Count() > 0)
                    {
                        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == cShift.First().Shift) : null;
                        if (currentLog != null && currentLog.Count() > 0)
                        {
                            if (currentLog.First().Qty != cShift.First().Qty)
                            {
                                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                            }
                            else
                            {
                                str.Append("<td style='background:yellow'>");
                            }
                        }
                        else
                        {
                            str.Append("<td>");
                        }
                        str.Append(cShift.First().Qty.ToString("0.##") + " </td> ");
                        str.Append("<td>" + cShift.First().Memo + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                        str.Append("<td></td>");

                    }
                    #endregion
                }
                else
                {
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");

                }
                invQtyByDate = invQtyByDate + qtys[1] - outQty + Convert.ToDecimal((shiftplansByDate != null ? shiftplansByDate.Sum(s => s.Qty) : 0));
                str.Append("<td>");
                str.Append(invQtyByDate.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml =headStr+ str.ToString();
    }


    private void ListTable(IList<FirmPlan> firmPlanList, IList<PlanInv> planInvList,IList<ShiftPlan> shiftPlanList)
    {
        firmPlanList = firmPlanList.Where(f => f.PlanDate >= System.DateTime.Now.Date && f.PlanDate <= System.DateTime.Now.AddDays(13).Date).ToList();
        if (firmPlanList == null || firmPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        shiftPlanList = shiftPlanList ?? new List<ShiftPlan>();
        var shiftPlanListDic = shiftPlanList.GroupBy(p => new { p.Flow, p.Item, p.PlanVersion })
         .ToDictionary(d => d.Key, d => d.ToList());

        List<ShiftPlanLog> shiftPlanLogList = new List<ShiftPlanLog>();
        var hql = "select p from ShiftPlanLog as p where  p.PlanDate>=? and p.PlanDate<=? and p.PlanVersion <> ? and p.Item=? and p.Flow = ? ";
        foreach (var dic in shiftPlanListDic)
        {
            var shiftPlan = dic.Value;
            var paramList = new List<object> { shiftPlan.Min(d => d.PlanDate), shiftPlan.Max(d => d.PlanDate), shiftPlan.First().PlanVersion, shiftPlan.First().Item, shiftPlan.First().Flow };
            var rr = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlanLog>(hql, paramList.ToArray());
            if (rr != null && rr.Count > 0)
            {
                shiftPlanLogList.AddRange(rr);
            }
        }
        var planInvDic = planInvList.ToDictionary(d => d.ItemCode, d => d);
        var planByDateIndexs = firmPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);
        var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode)
            .GroupBy(p => new { p.FlowCode, p.ItemCode });

        string searchSql = string.Format("select Code,Desc1 from Item ");
        var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var itemDescList = new List<object[]>();
        foreach (System.Data.DataRow row in itemDescs.Rows)
        {
            itemDescList.Add(new object[] { row[0].ToString().ToUpper(), row[1].ToString().ToUpper() });
        }

        StringBuilder str = new StringBuilder();
        string headStr = CopyString();
        //str.Append(CopyString());
        //head


        str.Append("<tr class='GVHeader'><th rowspan='2' >Seq</th><th rowspan='2'>生产线</th><th rowspan='2' >物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>版本号</th><th rowspan='2'>排产时间</th><th rowspan='2'>上次排产时间</th><th rowspan='2' >安全</th><th rowspan='2' >当前</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            var currentDate = planByDateIndex.Key;
            ii++;
            str.Append("<th colspan='9' >");
            str.Append(currentDate.ToString("MM-dd"));
            str.Append("</th>");
        }
        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii>13)
        {
            widths = "380%";
        }
        else if (ii > 12)
        {
            widths = "350%";
        }
        else if (ii > 10)
        {
            widths = "300%";
        }
        else if (ii > 8)
        {
            widths = "265%";
        }
        else if (ii > 6)
        {
            widths = "215%";
        }
        else if (ii > 4)
        {
            widths = "170%";
        }
        else if (ii > 2)
        {
            widths = "130%";
        }
        headStr +=string.Format( "<table border='1' class='GV' style='width:{0};border-collapse:collapse;'><thead>",widths);
        #endregion
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >待生产数</th><th >需求</th><th >早班</th><th >备注</th><th>中班</th><th >备注</th><th >晚班</th><th >备注</th><th >库存</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
            var firstPlan = planByFlowItem.First();
            var currentShiftPlans = shiftPlanList.Where(s => s.Flow == firstPlan.FlowCode);
            var currentShiftPlan = currentShiftPlans != null && currentShiftPlans.Count() > 0 ? currentShiftPlans.First() : new ShiftPlan();
            //var shiftPlnaLogs = shiftplanLogByDates != null ? shiftplanLogByDates.Where(s => s.PlanVersion == shiftplanLogByDates.Max(m => m.PlanVersion)) : null;
            var prevShiftPlan = shiftPlanLogList.Where(sl => sl.Flow == currentShiftPlan.Flow).Count() > 0 ? shiftPlanLogList.Where(sl => sl.Flow == currentShiftPlan.Flow).OrderByDescending(sl=>sl.PlanVersion).First() : new ShiftPlanLog();
            var planDic = planByFlowItem.GroupBy(p => p.PlanDate).ToDictionary(d => d.Key, d => new double[] { d.Sum(q => q.OutQty), d.Sum(q => q.InProdQty) });
            l++;
            if (l % 2 == 0)
            {
                str.Append("<tr class='GVAlternatingRow'>");
            }
            else
            {
                str.Append("<tr class='GVRow'>");
            }
            str.Append("<td>");
            str.Append(l);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.FlowCode);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.ItemCode);
            str.Append("</td>");
            str.Append("<td>");
            //str.Append(firstPlan.ItemDescription);
            str.Append(itemDescList.Where(id=>id[0].ToString()==planByFlowItem.Key.ItemCode.ToString()).First()[1]);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(currentShiftPlan.PlanVersion);
            str.Append("</td>");
            if (currentShiftPlan.LastModifyDate == DateTime.MinValue)
            {
                str.Append("<td>");
                str.Append("");
                str.Append("</td>");
            }
            else
            {
                str.Append("<td>");
                str.Append(currentShiftPlan.LastModifyDate == DateTime.MinValue);
                str.Append("</td>");
            }
            if (prevShiftPlan.CreateDate == DateTime.MinValue)
            {
                str.Append("<td>");
                str.Append("");
                str.Append("</td>");
            }
            else
            {
                str.Append("<td>");
                str.Append(prevShiftPlan.CreateDate);
                str.Append("</td>");
            }
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planInv.InvQty.ToString("0.##"));
            str.Append("</td>");
            double invQtyByDate = planInv.InvQty;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qtys = planDic.ValueOrDefault(planByDateIndex.Key);
                if (qtys[1] == 0)
                {
                    str.Append("<td>");
                    str.Append("");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td>");
                    str.Append(qtys[1].ToString("0.##")); //待生产
                    str.Append("</td>");
                }
                var outQty = invQtyByDate+qtys[1] - qtys[0];//需求
                if (outQty < firstPlan.SafeStock)
                {
                    outQty = firstPlan.SafeStock - outQty;
                }
                str.Append("<td>");
                str.Append(outQty.ToString("0.##"));
                str.Append("</td>");
                var shiftplansByDate = currentShiftPlans.Where(s => s.PlanDate == planByDateIndex.Key && s.Flow == planByFlowItem.Key.FlowCode && s.Item == planByFlowItem.Key.ItemCode);
                if (shiftplansByDate != null && shiftplansByDate.Count() > 0)
                {
                    var shiftplanLogByDates = shiftPlanLogList.Where(s => s.PlanDate == planByDateIndex.Key && s.Flow == planByFlowItem.Key.FlowCode && s.Item == planByFlowItem.Key.ItemCode);
                    var shiftPlnaLogs = shiftplanLogByDates != null ? shiftplanLogByDates.Where(s => s.PlanVersion == shiftplanLogByDates.Max(m => m.PlanVersion)) : null;

                    #region
                    var aShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "A");
                    var bShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "B");
                    var cShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "C");
                    if (aShift != null && aShift.Count() > 0)
                    {
                        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == aShift.First().Shift) : null;
                        if (currentLog != null && currentLog.Count()>0)
                        {
                            if (currentLog.First().Qty != aShift.First().Qty)
                            {
                                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                            }
                            else
                            {
                                str.Append("<td style='background:yellow'>");
                            }
                        }
                        else
                        {
                            str.Append("<td>");
                        }
                        str.Append(aShift.First().Qty.ToString("0.##") + " </td> ");
                        str.Append("<td>" + aShift.First().Memo + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                        str.Append("<td></td>");

                    }
                    if (bShift != null && bShift.Count() > 0)
                    {
                        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == bShift.First().Shift) : null;
                        if (currentLog != null && currentLog.Count() > 0)
                        {
                            if (currentLog.First().Qty != bShift.First().Qty)
                            {
                                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                            }
                            else
                            {
                                str.Append("<td style='background:yellow'>");
                            }
                        }
                        else
                        {
                            str.Append("<td>");
                        }
                        str.Append(bShift.First().Qty.ToString("0.##") + " </td> ");
                        str.Append("<td>" + bShift.First().Memo + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                        str.Append("<td></td>");

                    }
                    if (cShift != null && cShift.Count() > 0)
                    {
                        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == cShift.First().Shift) : null;
                        if (currentLog != null && currentLog.Count() > 0)
                        {
                            if (currentLog.First().Qty != cShift.First().Qty)
                            {
                                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                            }
                            else
                            {
                                str.Append("<td style='background:yellow'>");
                            }
                        }
                        else
                        {
                            str.Append("<td>");
                        }
                        str.Append(cShift.First().Qty.ToString("0.##") + " </td> ");
                        str.Append("<td>" + cShift.First().Memo + "</td>");
                    }
                    else
                    {
                        str.Append("<td></td>");
                        str.Append("<td></td>");

                    }
                    #endregion

                    #region
                    //foreach (var shiftplans in shiftplansByDate.OrderBy(s => s.Shift))
                    //{
                    //    var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == shiftplans.Shift) : null;
                    //    if (currentLog != null)
                    //    {
                    //        if (currentLog.First().Qty != shiftplans.Qty)
                    //        {
                    //            str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                    //        }
                    //        else
                    //        {
                    //            str.Append("<td style='background:yellow'>");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        str.Append("<td>");
                    //    }
                    //    str.Append(shiftplans.Qty.ToString("0.##") + " < /td > " );
                    //    str.Append("<td>" + shiftplans.Memo + "</td>");
                    //}
                    #endregion

                }
                else {
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                    str.Append("<td></td>");
                
                }
                invQtyByDate = invQtyByDate + qtys[1] - outQty + (shiftplansByDate != null ? shiftplansByDate.Sum(s => s.Qty) : 0);
                str.Append("<td>");
                str.Append(invQtyByDate.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr+str.ToString();
    }

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblAction.SelectedIndex == 0)
        {
            this.tblImport.Visible = false;
            this.tblSearch.Visible = true;
            this.list.Visible = true;
        }
        else
        {
            this.tblImport.Visible = true;
            this.tblSearch.Visible = false;
            this.list.Visible = false;
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        }
        string flowCodeValues = this.tbFlow.Value.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", ",");
            flowCodeValues = flowCodeValues.Replace("\n", ",");
        }
        var sqlParams = new SqlParameter[5];
        sqlParams[0] = new SqlParameter("@FlowType", "Production");
        sqlParams[1] = new SqlParameter("@Operation", "");
        sqlParams[2] = new SqlParameter("@FlowCode", flowCodeValues);
        sqlParams[3] = new SqlParameter("@DateFrom", startTime);
        sqlParams[4] = new SqlParameter("@IsShow0", false);
        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
        var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
        firmPlanList = firmPlanList.Where(f => f.PlanDate >= System.DateTime.Now.Date && f.PlanDate <= System.DateTime.Now.AddDays(13).Date).ToList();
        var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
        DateTime endTime = startTime.AddDays(15);
        var hql = "select p from {0} p where p.PlanDate>=? and p.PlanDate<? ";
        var paramList = new List<object> { startTime, endTime };

        string searchSql = string.Format("select Code,Desc1 from Item ");
        var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        //var itemDescList = new List<object[]>();
        foreach (System.Data.DataRow row in itemDescs.Rows)
        {
            var plans = firmPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
            if (plans != null && plans.Count() > 0)
            {
                foreach (var plan in plans)
                {
                    plan.ItemDescription = row[1].ToString();
                }
            }
            //itemDescList.Add(new object[] { row[0].ToString().ToUpper(), row[1].ToString().ToUpper() });
        }
       
        if (flowCodeValues != string.Empty)
        {
            hql += string.Format(" and p.Flow = '{0}' ", flowCodeValues.Replace(",", "','"));
            //hql += " and p.Flow = ? ";
            //paramList.Add(this.tbFlow.Value.Trim());
        }

        var planVersion = 0;
        int.TryParse(this.tbPlanVersion.Text.Trim(), out planVersion);
        if (planVersion == 0)
        {
            var shiftPlanList = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlan>(string.Format(hql, "ShiftPlan"), paramList.ToArray()) ?? new List<ShiftPlan>();
            IList<object> data = new List<object>();
            data.Add(firmPlanList);
            data.Add(planInvList);
            data.Add(shiftPlanList);
            TheReportMgr.WriteToClient("ShiftPlan.xls", data, "ShiftPlan.xls");
        }
        else
        {
            var shiftPlanLogList = this.TheGenericMgr.FindAllWithCustomQuery<ShiftPlanLog>(string.Format(hql + " and p.PlanVersion={1} ", "ShiftPlan", planVersion), paramList.ToArray());
            shiftPlanLogList = shiftPlanLogList ?? new List<ShiftPlanLog>();
            var shiftPlanList = shiftPlanLogList.Select(p =>
            {
                var plan = new ShiftPlan();
                plan.Flow = p.Flow;
                plan.Item = p.Item;
                plan.ItemDescription = p.ItemDescription;
                plan.PlanDate = p.PlanDate;
                plan.PlanVersion = p.PlanVersion;
                plan.Qty = p.Qty;
                plan.UnitQty = p.UnitQty;
                plan.Uom = p.Uom;
                plan.Shift = p.Shift;
                plan.LastModifyDate = p.CreateDate;
                return plan;
            }).ToList();
            IList<object> data = new List<object>();
            data.Add(firmPlanList);
            data.Add(planInvList);
            data.Add(shiftPlanList);
            TheReportMgr.WriteToClient("ShiftPlan.xls", data, "ShiftPlan.xls");
        }
      
    }

    private string CopyString()
    {
        return @"<a type='text/html' onclick='copyHtml()' href='#' id='copy'>复制</a>底色黄色重新导入无改动,橙色重新导入并有改动 
                <script type='text/javascript'>
                    function copyHtml()
                    {
                        window.clipboardData.setData('Text', $('#ctl01_list')[0].innerHTML);
                    }
                </script>";
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            bool isCheckA = ((CheckBox)this.GV_Order.HeaderRow.FindControl("cbQtyA")).Checked;
            bool isCheckB = ((CheckBox)this.GV_Order.HeaderRow.FindControl("cbQtyB")).Checked;
            bool isCheckC = ((CheckBox)this.GV_Order.HeaderRow.FindControl("cbQtyC")).Checked;
            var shiftPlanList = new List<ShiftPlan>();
            foreach (GridViewRow row in this.GV_Order.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    var shiftPlan = new ShiftPlan();
                    shiftPlan.Flow = row.Cells[1].Text;
                    shiftPlan.Item = row.Cells[2].Text;
                    double qty = 0;
                    if (isCheckA)
                    {
                        string tbQtyA = ((TextBox)row.Cells[5].FindControl("tbQtyA")).Text.Trim();
                        tbQtyA = tbQtyA == string.Empty ? "0" : tbQtyA;
                        if (!double.TryParse(tbQtyA, out qty) || qty < 0)
                        {
                            this.ShowErrorMessage("${MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error}");
                            return;
                        }
                        shiftPlan.QtyA = qty;
                        shiftPlan.IdA = int.Parse(((HiddenField)row.Cells[5].FindControl("hfIdA")).Value);
                    }
                    if (isCheckB)
                    {
                        string tbQtyB = ((TextBox)row.Cells[7].FindControl("tbQtyB")).Text.Trim();
                        tbQtyB = tbQtyB == string.Empty ? "0" : tbQtyB;
                        if (!double.TryParse(tbQtyB, out qty) || qty < 0)
                        {
                            this.ShowErrorMessage("${MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error}");
                            return;
                        }
                        shiftPlan.QtyB = qty;
                        shiftPlan.IdB = int.Parse(((HiddenField)row.Cells[7].FindControl("hfIdB")).Value);
                    }
                    if (isCheckC)
                    {
                        string tbQtyC = ((TextBox)row.Cells[9].FindControl("tbQtyC")).Text.Trim();
                        tbQtyC = tbQtyC == string.Empty ? "0" : tbQtyC;
                        if (!double.TryParse(tbQtyC, out qty) || qty < 0)
                        {
                            this.ShowErrorMessage("${MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error}");
                            return;
                        }
                        shiftPlan.QtyC = qty;
                        shiftPlan.IdC = int.Parse(((HiddenField)row.Cells[9].FindControl("hfIdC")).Value);
                    }

                    if (shiftPlan.QtyA > 0 || shiftPlan.QtyB > 0 || shiftPlan.QtyC > 0)
                    {
                        shiftPlanList.Add(shiftPlan);
                    }
                }
            }

            var groupPlans = shiftPlanList.GroupBy(p => p.Flow);
            foreach (var groupPlan in groupPlans)
            {
                //TheMrpMgr.CreateWorkOrder(groupPlan.ToList(), this.CurrentUser, this.cbSplitOrder.Checked, DateTime.Parse(ltlWindowTime.Text + " 00:00:00"));
            }
            this.ShowSuccessMessage("MasterData.Order.OrderHead.AddOrder.Successfully");
            //跳转到相应的订单查询一面
            //string url = "Main.aspx?mid=Order.OrderHead.Production__mp--ModuleType-Production_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";
            //Page.ClientScript.RegisterStartupScript(GetType(), "method",
            //    " <script language='javascript' type='text/javascript'>timedMsg('" + url + "'); </script>");
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            this.ShowErrorMessage(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.search.Visible = true;
        this.newOrder.Visible = false;
    }

    protected void GV_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //var shiftCodes = this.TheGenericMgr.FindAll<EntityPreference>("from EntityPreference e where e.Code ='DefaultShift'")[0].Value.Split(',');
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ShiftPlan body = (ShiftPlan)(e.Row.DataItem);
        //    ((TextBox)e.Row.Cells[5].FindControl("tbQtyA")).Text = body.QtyA.ToString();
        //    ((TextBox)e.Row.Cells[6].FindControl("tbQtyB")).Text = body.QtyB.ToString();
        //    if (shiftCodes.Length == 3)
        //    {
        //        ((TextBox)e.Row.Cells[7].FindControl("tbQtyC")).Text = body.QtyC.ToString();
        //    }
        //    else
        //    {
        //        ((TextBox)e.Row.Cells[7].FindControl("tbQtyC")).Enabled = false;
        //    }
        //}
        //else if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    ((CheckBox)e.Row.Cells[5].FindControl("cbQtyA")).Text = this.TheGenericMgr.FindById<Shift>(shiftCodes[0]).ShiftName;
        //    ((CheckBox)e.Row.Cells[6].FindControl("cbQtyB")).Text = this.TheGenericMgr.FindById<Shift>(shiftCodes[1]).ShiftName;
        //    if (shiftCodes.Length == 3)
        //    {
        //        ((CheckBox)e.Row.Cells[7].FindControl("cbQtyC")).Text = this.TheGenericMgr.FindById<Shift>(shiftCodes[2]).ShiftName;
        //    }
        //    else
        //    {
        //        ((CheckBox)e.Row.Cells[7].FindControl("cbQtyC")).Enabled = false;
        //    }
        //}
    }

    #region    MRP运算
    //protected void btnMrpCalculate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DateTime startTime = DateTime.Today;
    //        if (startTime < DateTime.Today)
    //        {
    //            ShowWarningMessage("时间不能小于今天");
    //            return;
    //        }
    //        string dateIndex = startTime.ToString("yyyy-MM-dd");
    //        string dateIndexTo = startTime.AddDays(13).ToString("yyyy-MM-dd");
    //        var sqlParams = new SqlParameter[5];
    //        sqlParams[0] = new SqlParameter("@FlowType", "Procurement");
    //        sqlParams[1] = new SqlParameter("@Operation", null);
    //        sqlParams[2] = new SqlParameter("@FlowCode", string.Empty);
    //        sqlParams[3] = new SqlParameter("@DateFrom", startTime);
    //        sqlParams[4] = new SqlParameter("@IsShow0", false);
    //        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
    //        var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
    //        var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
    //        if (firmPlanList == null || firmPlanList.Count == 0)
    //        {
    //            this.list.InnerHtml = "没有运算采购需求。";
    //        }
    //        var planInvDic = planInvList
    //           .GroupBy(p => p.ItemCode).Select(p => new PlanInv
    //           {
    //               InvQty = p.Sum(q => q.InvQty),
    //               ItemCode = p.Key,
    //               SafeStock = p.First().SafeStock,
    //               MaxStock = p.First().MaxStock,
    //               RecQty = p.Sum(q => q.RecQty)
    //           }).ToDictionary(d => d.ItemCode, d => d);
    //        var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode })
    //            .ToDictionary(d => d.Key, d => d);
    //        string searchSql = string.Format("select Code,Desc1 from Item ");
    //        var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
    //        foreach (System.Data.DataRow row in itemDescs.Rows)
    //        {
    //            var plans = firmPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
    //            if (plans != null && plans.Count() > 0)
    //            {
    //                foreach (var plan in plans)
    //                {
    //                    plan.ItemDescription = row[1].ToString();
    //                }
    //            }
    //        }
    //        //int planVersion = TheNumberControlMgr.GenerateNumberNextSequence("ProcurementPlan");
    //        IList<ProcurementPlan> procurementPlanList = new List<ProcurementPlan>();
    //        foreach (var flowItems in planByFlowItems)
    //        {
    //            var planInv = planInvDic.Keys.Contains(flowItems.Value.First().ItemCode) ? planInvDic[flowItems.Value.First().ItemCode] : new PlanInv();
    //            var finalQty = planInv.InvQty + planInv.RecQty;
    //            foreach (var firmPlan in flowItems.Value.OrderBy(s => s.PlanDate))
    //            {
    //                var orderQty = firmPlan.OutQty;
    //                finalQty = finalQty + firmPlan.InQty - firmPlan.OutQty;
    //                if (finalQty < planInv.SafeStock)
    //                {
    //                    orderQty = planInv.SafeStock - finalQty;
    //                    //outQty += planInv.SafeStock - finalQty;
    //                    finalQty += planInv.SafeStock - finalQty;
    //                }
    //                else if (finalQty > planInv.SafeStock)
    //                {
    //                    orderQty = 0;
    //                }

    //                ProcurementPlan prPlan = new ProcurementPlan
    //                {
    //                    PlanDate = firmPlan.PlanDate.Date,
    //                    Flow = firmPlan.FlowCode,
    //                    Item = firmPlan.ItemCode,
    //                    ItemDescription = firmPlan.ItemDescription,
    //                    Uom = firmPlan.UomCode,
    //                    //UnitQty = firmPlan.uni,
    //                    Location = firmPlan.LocationTo,
    //                    SafeStock = Convert.ToDecimal(firmPlan.SafeStock),
    //                    MaxStock = Convert.ToDecimal(firmPlan.MaxStock),
    //                    InvQty = Convert.ToDecimal(planInv.InvQty),
    //                    InQty = Convert.ToDecimal(firmPlan.InQty),
    //                    OutQty = Convert.ToDecimal(firmPlan.OutQty),
    //                    OrderQty = Convert.ToDecimal(orderQty),
    //                    FinalQty = Convert.ToDecimal(finalQty),
    //                    Supplier = firmPlan.Supplier,
    //                };
    //                procurementPlanList.Add(prPlan);
    //            }
    //        }
    //        TheMrpMgr.MrpCalculate(null, this.CurrentUser.Code, procurementPlanList);
    //    }
    //    catch (BusinessErrorException ex)
    //    {
    //        ShowErrorMessage(ex);
    //    }
    //}

    //void MrpCalculate_Render(object sender, EventArgs e)
    //{
    //    IList<OrderHead> orderHeadList = (IList<OrderHead>)((object[])sender)[0];

    //    DateTime startTime = DateTime.Today;
    //    //if (this.tbStartDate.Text.Trim() != string.Empty)
    //    //{
    //    //    DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
    //    //}
    //    if (startTime < DateTime.Today)
    //    {
    //        ShowWarningMessage("时间不能小于今天");
    //        return;
    //    }
    //    string dateIndex = startTime.ToString("yyyy-MM-dd");
    //    string dateIndexTo = startTime.AddDays(13).ToString("yyyy-MM-dd");
    //    var sqlParams = new SqlParameter[5];
    //    sqlParams[0] = new SqlParameter("@FlowType", "Procurement");
    //    sqlParams[1] = new SqlParameter("@Operation", null);
    //    sqlParams[2] = new SqlParameter("@FlowCode", string.Empty);
    //    sqlParams[3] = new SqlParameter("@DateFrom", startTime);
    //    sqlParams[4] = new SqlParameter("@IsShow0", false);
    //    var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
    //    var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
    //    var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
    //    if (firmPlanList == null || firmPlanList.Count == 0)
    //    {
    //        this.list.InnerHtml = "没有运算采购需求。";
    //    }
    //    var planInvDic = planInvList
    //       .GroupBy(p => p.ItemCode).Select(p => new PlanInv
    //       {
    //           InvQty = p.Sum(q => q.InvQty),
    //           ItemCode = p.Key,
    //           SafeStock = p.First().SafeStock,
    //           MaxStock = p.First().MaxStock,
    //           RecQty = p.Sum(q => q.RecQty)
    //       }).ToDictionary(d => d.ItemCode, d => d);
    //    var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode })
    //        .ToDictionary(d=>d.Key,d=>d);
    //    string searchSql = string.Format("select Code,Desc1 from Item ");
    //    var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
    //    foreach (System.Data.DataRow row in itemDescs.Rows)
    //    {
    //        var plans = firmPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
    //        if (plans != null && plans.Count() > 0)
    //        {
    //            foreach (var plan in plans)
    //            {
    //                plan.ItemDescription = row[1].ToString();
    //            }
    //        }
    //    }
    //    int planVersion = TheNumberControlMgr.GenerateNumberNextSequence("ProcurementPlan");
    //    IList<ProcurementPlan> procurementPlanList = new List<ProcurementPlan>();
    //    foreach (var flowItems in planByFlowItems)
    //    {
    //        var planInv = planInvDic.Keys.Contains(flowItems.Value.First().ItemCode) ? planInvDic[flowItems.Value.First().ItemCode] : new PlanInv();
    //        var finalQty = planInv.InvQty + planInv.RecQty;
    //        foreach (var firmPlan in flowItems.Value.OrderBy(s => s.PlanDate))
    //        {
    //            var outQty = firmPlan.OutQty;
    //            finalQty = finalQty + firmPlan.InQty - firmPlan.OutQty;
    //            if (finalQty < planInv.SafeStock)
    //            {
    //                outQty += planInv.SafeStock - finalQty;
    //                finalQty += planInv.SafeStock - finalQty;
    //            }
               
    //            ProcurementPlan prPlan = new ProcurementPlan
    //            {
    //                PlanDate = firmPlan.PlanDate.Date,
    //                Flow = firmPlan.FlowCode,
    //                Item = firmPlan.ItemCode,
    //                ItemDescription = firmPlan.ItemDescription,
    //                Uom = firmPlan.UomCode,
    //                //UnitQty = firmPlan.uni,
    //                Location = firmPlan.LocationTo,
    //                SafeStock = Convert.ToDecimal(firmPlan.SafeStock),
    //                MaxStock = Convert.ToDecimal(firmPlan.MaxStock),
    //                InvQty = Convert.ToDecimal(planInv.InvQty),
    //                InQty = Convert.ToDecimal(firmPlan.InQty),
    //                OutQty = Convert.ToDecimal(outQty),
    //                FinalQty = Convert.ToDecimal(finalQty),
    //                Supplier = firmPlan.Supplier,
    //            };
    //            procurementPlanList.Add(prPlan);
    //        }
    //    }
    //    TheMrpMgr.MrpCalculate(orderHeadList, this.CurrentUser.Code, procurementPlanList);
    //}
    #endregion

    #region    重新生成生成生产需求

    //private void CalculateProdPlan()
    //void CalculateProdPlan_Render(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        var sqlParams = new SqlParameter[5];
    //        sqlParams[0] = new SqlParameter("@FlowType", "Production");
    //        sqlParams[1] = new SqlParameter("@Operation", null);
    //        sqlParams[2] = new SqlParameter("@FlowCode", string.Empty);
    //        sqlParams[3] = new SqlParameter("@DateFrom", System.DateTime.Now.Date);
    //        sqlParams[4] = new SqlParameter("@IsShow0", false);
    //        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
    //        var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
    //        var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
    //        if (firmPlanList == null || firmPlanList.Count == 0)
    //        {
    //            this.list.InnerHtml = "没有生产需求。";
    //        }
    //        var planInvDic = planInvList
    //      .GroupBy(p => p.ItemCode).Select(p => new PlanInv
    //      {
    //          InvQty = p.Sum(q => q.InvQty),
    //          ItemCode = p.Key
    //      }).ToDictionary(d => d.ItemCode, d => d);

    //        var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode })
    //            .ToDictionary(d => d.Key, d => d);
    //        string searchSql = string.Format("select Code,Desc1 from Item ");
    //        var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
    //        foreach (System.Data.DataRow row in itemDescs.Rows)
    //        {
    //            var plans = firmPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
    //            if (plans != null && plans.Count() > 0)
    //            {
    //                foreach (var plan in plans)
    //                {
    //                    plan.ItemDescription = row[1].ToString();
    //                }
    //            }
    //        }
    //        int planVersion = TheNumberControlMgr.GenerateNumberNextSequence("ProductionPlan");
    //        IList<ProductionPlan> productionPlanList = new List<ProductionPlan>();
    //        foreach (var flowItems in planByFlowItems)
    //        {
    //            var planInv = planInvDic.Keys.Contains(flowItems.Value.First().ItemCode) ? planInvDic[flowItems.Value.First().ItemCode] : new PlanInv();
    //            var finalQty = planInv.InvQty + planInv.RecQty;
    //            foreach (var firmPlan in flowItems.Value.OrderBy(s => s.PlanDate))
    //            {
    //                var outQty = firmPlan.OutQty;
    //                //finalQty = finalQty + firmPlan.InQty - firmPlan.OutQty;
    //                //if (finalQty < planInv.SafeStock)
    //                //{
    //                //    outQty += planInv.SafeStock - finalQty;
    //                //finalQty += planInv.SafeStock - finalQty;
    //                //}

    //                ProductionPlan prPlan = new ProductionPlan
    //                {
    //                    PlanDate = firmPlan.PlanDate.Date,
    //                    Flow = firmPlan.FlowCode,
    //                    Item = firmPlan.ItemCode,
    //                    ItemDescription = firmPlan.ItemDescription,
    //                    Uom = firmPlan.UomCode,
    //                    //UnitQty = firmPlan.uni,
    //                    SafeStock = Convert.ToDecimal(firmPlan.SafeStock),
    //                    MaxStock = Convert.ToDecimal(firmPlan.MaxStock),
    //                    InvQty = Convert.ToDecimal(planInv.InvQty),
    //                    OutQty = Convert.ToDecimal(outQty),
    //                    InProdQty = Convert.ToDecimal(firmPlan.InProdQty),
    //                    PlanVersion = planVersion,
    //                };
    //                productionPlanList.Add(prPlan);
    //            }
    //        }

    //        var dateTimeNow = DateTime.Now;
    //        if (productionPlanList != null && productionPlanList.Count > 0)
    //        {
    //            //string sql = string.Format(" from ProductionPlan as c where  c.DateType = '{0}' and c.DateIndexTo in('{1}') and c.Flow in ('{2}')",
    //            //   (int)dateType, string.Join("','", customerPlanList.Select(p => p.DateIndexTo).Distinct().ToArray()),
    //            //   string.Join("','", customerPlanList.Select(p => p.Flow).Distinct().ToArray()));

    //            //this.genericMgr.Delete(sql);
    //            //int planVersion = this.iNumberControlMgr.GenerateNumberNextSequence("ProcurementPlan");
    //            this.TheGenericMgr.Delete(" from ProductionPlan as c ");
    //            foreach (var prPlan in productionPlanList)
    //            {
    //                prPlan.LastModifyDate = dateTimeNow;
    //                prPlan.LastModifyUser = this.CurrentUser.Code;
    //                TheGenericMgr.Create(prPlan);

    //                ProductionPlanLog pPlanLog = new ProductionPlanLog
    //                {
    //                    PlanId = prPlan.Id,
    //                    PlanDate = prPlan.PlanDate.Date,
    //                    Flow = prPlan.Flow,
    //                    Item = prPlan.Item,
    //                    ItemDescription = prPlan.ItemDescription,
    //                    Uom = prPlan.Uom,
    //                    UnitQty = prPlan.UnitQty,
    //                    SafeStock = prPlan.SafeStock,
    //                    MaxStock = prPlan.MaxStock,
    //                    InvQty = prPlan.InvQty,
    //                    OutQty = prPlan.OutQty,
    //                    InProdQty = prPlan.InProdQty,
    //                    LastModifyDate = dateTimeNow,
    //                    LastModifyUser = prPlan.LastModifyUser,
    //                    PlanVersion = prPlan.PlanVersion,
    //                };
    //                TheGenericMgr.Create(pPlanLog);
    //            }
    //        }
    //    }
    //    catch (BusinessErrorException ex)
    //    {
    //        ShowErrorMessage(ex);
    //    }
    //}

    #endregion


}