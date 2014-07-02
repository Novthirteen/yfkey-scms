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
        try
        {
            var shiftPlanList = TheMrpMgr.ReadShiftPlanFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser);
            ShowSuccessMessage("导入成功。");

            //this.ListTable(shiftPlanList);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
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
        string hql = @" select det.Id,det,PlanId,det.RefPlanNo,det.ProdLine,det.Item,det.ItemDesc,det.RefItemCode,det.Qty,det.Uom,Det.PlanDate,det.Shift,det.CreateDate,m.Status,m.Version from MRP_ShiftPlanDet as det inner join MRP_ShiftPlanMstr as m on m.Id=det.PlanId where 1=1 ";
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        }
        hql += string.Format(" and det.PlanDate between '{0}' and '{1}' ", startTime, startTime.AddDays(14));

        if (this.tbRefPlanNo.Text.Trim() != string.Empty)
        {
            hql += string.Format(" and det.RefPlanNo ='{0}' ", this.tbRefPlanNo.Text.Trim());
        }

        string flowCodeValues = this.tbFlow.Value.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", ",");
            flowCodeValues = flowCodeValues.Replace("\n", ",");
        }
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            hql += string.Format(" and det.ProdLine in ('{0}') ", flowCodeValues);
        }
        if (this.ltlPlanVersion.Text.Trim()!=string.Empty)
        {
            hql += string.Format(" and m.Version ={0} ", this.ltlPlanVersion.Text.Trim());
        }

        var allResult = TheGenericMgr.GetDatasetBySql(hql).Tables[0];
        var shiftPlans = new List<ShiftPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //det.Id,det,PlanId,det.RefPlanNo,det.ProdLine,det.Item,det.ItemDesc,det.RefItemCode,
            //det.Qty,det.Uom,Det.PlanDate,det.Shift,det.CreateDate,m.Status,m.Version
            shiftPlans.Add(new ShiftPlanDet
            {
                Id =int.Parse(row[0].ToString()),
                PlanId = int.Parse(row[1].ToString()),
                RefPlanNo = row[2].ToString(),
                ProdLine = row[3].ToString(),
                Item = row[4].ToString(),
                ItemDesc = row[5].ToString(),
                RefItemCode =row[6].ToString(),
                Qty = Convert.ToDecimal(row[7]),
                Uom = row[8].ToString(),
                PlanDate = Convert.ToDateTime(row[9]),
                Shift = row[10].ToString(),
                CreateDate = Convert.ToDateTime(row[11]),
                Status = row[12].ToString(),
                Version = Convert.ToInt32(row[13].ToString()),
            });
        }

        IList<ShiftPlanDet> pdPlanList = TheGenericMgr.FindAllWithCustomQuery<ShiftPlanDet>(hql);
        pdPlanList = pdPlanList == null || pdPlanList.Count == 0 ? new List<ShiftPlanDet>() : pdPlanList;

        if (string.IsNullOrEmpty(this.ltlPlanVersion.Text.Trim()) && pdPlanList.Count > 0)
        {
            var activePlanList = new List<ShiftPlanDet>();
            var groupByFlowVersion = pdPlanList.GroupBy(d => new { d.ProdLine }).ToDictionary(d => d.Key, d => d.OrderByDescending(g => g.Version).ToList());
            foreach (var g in groupByFlowVersion)
            {
                activePlanList.AddRange(pdPlanList.Where(s => s.ProdLine == g.Key.ProdLine && s.Version == g.Value.Max(m => m.Version)));
            }
            ListTable(activePlanList);
        }
        else
        {
            ListTable(pdPlanList);
        }

        ListTable(pdPlanList);
    }

    private void ListTable(IList<ShiftPlanDet> shiftPlanList)
    {
        if (shiftPlanList == null || shiftPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        var groupByFlowItems = shiftPlanList.OrderBy(p => p.ProdLine).GroupBy(d => new { d.ProdLine, d.Item }).ToDictionary(d => d.Key, d => d.ToList());
        var planByDateIndexs = shiftPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);



        #region
        //        var shiptPlanListDic = shiptPlanList.GroupBy(p => new { p.ProdLine, p.Version })
//           .ToDictionary(d => d.Key, d => d.ToList());
//        List<ShiftPlanDet> shiftPlanDetLogs = new List<ShiftPlanDet>();
//        string hql = @" select det.Id,det,PlanId,det.RefPlanNo,det.ProdLine,det.Item,det.ItemDesc,det.RefItemCode,det.Qty,det.Uom,Det.PlanDate,det.Shift,det.CreateDate,m.Status,m.Version from MRP_ShiftPlanDet as det inner join MRP_ShiftPlanMstr as m on m.Id=det.PlanId
//                     where 1=1 and det.ProdLine=? and m.Version=? ";
//        foreach (var dic in shiptPlanListDic)
//        {
//            var customerPlans = dic.Value;
//            var paramList = new List<object> { customerPlans.First().Type, customerPlans.First().Version - 1, customerPlans.First().Flow };
//            var rr = this.TheGenericMgr.FindAllWithCustomQuery<CustomerScheduleDetail>(hql, paramList.ToArray());
//            if (rr != null && rr.Count > 0)
//            {
//                customerPlanLogList.AddRange(rr);
//            }
        //        }
        #endregion

        StringBuilder str = new StringBuilder();
        string headStr = CopyString();
        str.Append("<tr class='GVHeader'><th rowspan='2'>Seq</th><th rowspan='2'>客户版本号</th><th rowspan='2'>生产线</th><th rowspan='2'>物料号</th><th rowspan='2' >物料描述</th><th rowspan='2'>版本号</th><th rowspan='2'>排产时间</th><th rowspan='2'>上次排产时间</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            var currentDate = planByDateIndex.Key;
            ii++;
            str.Append("<th rowspan='3'>");
            str.Append(currentDate.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        #region  通过长度控制table的宽度
        string widths = "100%";
        //if (ii > 13)
        //{
        //    widths = "380%";
        //}
        //else if (ii > 12)
        //{
        //    widths = "350%";
        //}
        //else if (ii > 10)
        //{
        //    widths = "300%";
        //}
        //else if (ii > 8)
        //{
        //    widths = "265%";
        //}
        //else if (ii > 6)
        //{
        //    widths = "215%";
        //}
        //else if (ii > 4)
        //{
        //    widths = "170%";
        //}
        //else if (ii > 2)
        //{
        //    widths = "130%";
        //}
        headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'><thead>", widths);
        #endregion
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >早班</th><th>中班</th><th >晚班</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in groupByFlowItems)
        {
            var firstPlan = planByFlowItem.Value.First();
            //var currentShiftPlans = shiftPlanList.Where(s => s.ProdLine == firstPlan.ProdLine);
            //var currentShiftPlan = currentShiftPlans != null && currentShiftPlans.Count() > 0 ? currentShiftPlans.First() : new ShiftPlanDet();
            var prevShiftPlan = planByFlowItem.Value.Where(sl => sl.Version != firstPlan.Version).Count()>0? planByFlowItem.Value.Where(sl => sl.Version != firstPlan.Version).OrderByDescending(sl => sl.Version).First() : new ShiftPlanDet();
            //var planDic = planByFlowItem.Value.GroupBy(p => p.PlanDate).ToDictionary(d => d.Key, d =>d.ToList() });
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
            str.Append(firstPlan.RefPlanNo);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ProdLine);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Version);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.CreateDate);
            str.Append("</td>");
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
            foreach (var planByDateIndex in planByDateIndexs)
            {
                //var shiftplansByDate = planByFlowItem.Value.Where(s => s.PlanDate == planByDateIndex.Key  && s.Item == planByFlowItem.Key.Item);
                //if (shiftplansByDate != null && shiftplansByDate.Count() > 0)
                //{
                //    var shiftplanLogByDates = shiftPlanLogList.Where(s => s.PlanDate == planByDateIndex.Key && s.Flow == planByFlowItem.Key.Flow && s.Item == planByFlowItem.Key.Item);
                //    var shiftPlnaLogs = shiftplanLogByDates != null ? shiftplanLogByDates.Where(s => s.PlanVersion == shiftplanLogByDates.Max(m => m.PlanVersion)) : null;

                //    #region
                //    var aShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "A");
                //    var bShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "B");
                //    var cShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "C");
                //    if (aShift != null && aShift.Count() > 0)
                //    {
                //        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == aShift.First().Shift) : null;
                //        if (currentLog != null && currentLog.Count() > 0)
                //        {
                //            if (currentLog.First().Qty != aShift.First().Qty)
                //            {
                //                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                //            }
                //            else
                //            {
                //                str.Append("<td style='background:yellow'>");
                //            }
                //        }
                //        else
                //        {
                //            str.Append("<td>");
                //        }
                //        str.Append(aShift.First().Qty.ToString("0.##") + " </td> ");
                //        str.Append("<td>" + aShift.First().Memo + "</td>");
                //    }
                //    else
                //    {
                //        str.Append("<td></td>");
                //        str.Append("<td></td>");

                //    }
                //    if (bShift != null && bShift.Count() > 0)
                //    {
                //        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == bShift.First().Shift) : null;
                //        if (currentLog != null && currentLog.Count() > 0)
                //        {
                //            if (currentLog.First().Qty != bShift.First().Qty)
                //            {
                //                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                //            }
                //            else
                //            {
                //                str.Append("<td style='background:yellow'>");
                //            }
                //        }
                //        else
                //        {
                //            str.Append("<td>");
                //        }
                //        str.Append(bShift.First().Qty.ToString("0.##") + " </td> ");
                //        str.Append("<td>" + bShift.First().Memo + "</td>");
                //    }
                //    else
                //    {
                //        str.Append("<td></td>");
                //        str.Append("<td></td>");

                //    }
                //    if (cShift != null && cShift.Count() > 0)
                //    {
                //        var currentLog = shiftPlnaLogs != null ? shiftPlnaLogs.Where(s => s.Shift == cShift.First().Shift) : null;
                //        if (currentLog != null && currentLog.Count() > 0)
                //        {
                //            if (currentLog.First().Qty != cShift.First().Qty)
                //            {
                //                str.Append(string.Format("<td style='background:orange' title='{0}'>", currentLog.First().Qty.ToString("0.##")));
                //            }
                //            else
                //            {
                //                str.Append("<td style='background:yellow'>");
                //            }
                //        }
                //        else
                //        {
                //            str.Append("<td>");
                //        }
                //        str.Append(cShift.First().Qty.ToString("0.##") + " </td> ");
                //        str.Append("<td>" + cShift.First().Memo + "</td>");
                //    }
                //    else
                //    {
                //        str.Append("<td></td>");
                //        str.Append("<td></td>");

                //    }
                //    #endregion
                //}
                //else
                //{
                //    str.Append("<td></td>");
                //    str.Append("<td></td>");
                //    str.Append("<td></td>");
                //    str.Append("<td></td>");
                //    str.Append("<td></td>");
                //    str.Append("<td></td>");

                //}
                //invQtyByDate = invQtyByDate + qtys[1] - outQty + Convert.ToDecimal((shiftplansByDate != null ? shiftplansByDate.Sum(s => s.Qty) : 0));
                //str.Append("<td>");
                //str.Append(invQtyByDate.ToString("0.##"));
                //str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
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