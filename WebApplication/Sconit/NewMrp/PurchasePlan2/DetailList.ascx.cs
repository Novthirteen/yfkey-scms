﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using com.Sconit.Web;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Collections;

public partial class NewMrp_ShipPlan_DetailList : MainModuleBase
{
    public static string currentRelesNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;

        if (!IsPostBack)
        {
            this.btQtyHidden.Value = string.Empty;
            this.btSeqHidden.Value = string.Empty;
            this.tbFlow.Value = string.Empty;
        }

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    var dateType = (CodeMaster.TimeUnit)(int.Parse(this.rblDateType.SelectedValue));
        //    var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);

        //    this.ListTable(customerPlanList);
        //}
        //catch (BusinessErrorException ex)
        //{
        //    ShowErrorMessage(ex);
        //}
    }

    #region   明细查询
    public void GetView(string relesNo)
    {
        this.tbFlow.Value = string.Empty;
        this.list.InnerHtml = "";
        currentRelesNo = relesNo;
        var purchasePlanMstr = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr2>(" select s from PurchasePlanMstr2 as s where s.ReleaseNo=? ", currentRelesNo).First();
        if (purchasePlanMstr.Status == BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT)
        {
            this.importDiv.Visible = false;
        }
        else
        {
            this.importDiv.Visible = true;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"
		select det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,max(isnull(det.ReqQty,0)),max(isnull(det.PurchaseQty,0)),det.WindowTime,det.Version,m.ReleaseNo,m.Status,max(isnull(l.InitStock,0)),max(isnull(l.SafeStock,0)),max(isnull(l.InTransitQty,0)),max(isnull(l.InspectQty,0)),max(isnull(det.OrderQty,0)),max(isnull(l.MaxStock,0)),det.StartTime,max(isnull(det.uc,0)),max(isnull(f.MrpLeadTime,0)),m.Id,max(isnull(det.MinLotSize,0)),max(isnull(det.UnitQty,0))
 from MRP_PurchasePlanDet2 as det with(nolock) 
inner join MRP_PurchasePlanMstr2 as m with(nolock) on m.Id=det.PurchasePlanId
inner join FlowMstr as f on det.Flow=f.Code
inner join FlowDet as fd on det.Flow=fd.Flow and det.Item=fd.Item
left join MRP_PurchasePlanInitLocationDet2 as l with(nolock) on det.PurchasePlanId=l.PurchasePlanId and det.Item=l.Item where 1=1   ";

        searchSql += string.Format(" and det.Type='{0}' ", this.rbType.SelectedValue);

        string flowCodeValues = this.tbFlow.Value.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", "','");
            flowCodeValues = flowCodeValues.Replace("\n", "','");
        }
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            searchSql += string.Format(" and det.Flow in ('{0}') ", flowCodeValues);
        }
        else
        {
            this.list.InnerHtml = "";
            ShowErrorMessage("采购路线不能为空。");
            return;
        }

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " group by det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,det.WindowTime,det.Version,m.ReleaseNo,m.Status,det.StartTime,m.Id order by det.Flow,det.Item asc ";

        var flowCodes = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var purchasePlanDetList = new List<PurchasePlanDet2>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            //det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,isnull(det.ReqQty,0),isnull(det.PurchaseQty,0),det.WindowTime,det.Version,m.ReleaseNo,m.Status,isnull(l.InitStock,0),
            //isnull(l.SafeStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.OrderQty,0),isnull(l.MaxStock,0)
            purchasePlanDetList.Add(new PurchasePlanDet2
            {
                Id = Convert.ToInt32(row[0].ToString()),
                UUID = row[1].ToString(),
                Flow = row[2].ToString(),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                ReqQty = Math.Round(Convert.ToDecimal(row[6])),
                PurchaseQty = Convert.ToInt32(row[7]),
                WindowTime = Convert.ToDateTime(row[8]),
                Version = Convert.ToInt32(row[9]),
                ReleaseNo = Convert.ToInt32(row[10]),
                Status = row[11].ToString(),
                InitStock = Math.Round(Convert.ToDecimal(row[12])),
                SafeStock = Math.Round(Convert.ToDecimal(row[13])),
                InTransitQty =Math.Round( Convert.ToDecimal(row[14])),
                InspectQty =Math.Round( Convert.ToDecimal(row[15])),
                OrderQty =Math.Round( Convert.ToDecimal(row[16])),
                MaxStock = Convert.ToDecimal(row[17]),
                StartTime = Convert.ToDateTime(row[18]),
                UnitCount = Math.Round(Convert.ToDecimal(row[19])),
                MrpLeadTime = Math.Round(Convert.ToDecimal(row[20])),
                PurchasePlanId = Convert.ToInt32(row[21]),
                MinLotSize = Math.Round(Convert.ToDecimal(row[22])),
                UnitQty = Math.Round(Convert.ToDecimal(row[23])),
            });
        }
        if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            ListTable(purchasePlanDetList);
        }
        else
        {
            WeeklyListTable(purchasePlanDetList);
        }
    }

    private void ListTable(IList<PurchasePlanDet2> pPlanDetList)
    {
        if (pPlanDetList == null || pPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        var minWindowTime = pPlanDetList.Min(s => s.StartTime).AddDays(13);
        pPlanDetList = pPlanDetList.Where(s => s.StartTime <= minWindowTime).ToList();

        #region   trace
        List<PurchasePlanDetTrace2> traceList = new List<PurchasePlanDetTrace2>();
        int len = 0;
        int j = pPlanDetList.Count % 2000 == 0 ? pPlanDetList.Count / 2000 : pPlanDetList.Count / 2000 + 1;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDetTrace2>(string.Format(" select l from PurchasePlanDetTrace2 as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        //this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDetTrace>(string.Format(" select l from PurchasePlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        traceList = traceList == null ? new List<PurchasePlanDetTrace2>() : traceList;

        if (traceList != null && traceList.Count > 0)
        {
            foreach (var sd in pPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count > 0)
                {
                    showText = "<table><thead><tr><th>成品物料号</th><th>计划日期</th><th>计划数量</th><th>需求数量</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.ProdItem + "</td><td>" + c.PlanDate.ToShortDateString() + "</td><td>" + c.ProdQty.ToString("0.##") + "</td><td>" + (c.ProdQty*(c.ScrapPct/100+c.RateQty)*sd.UnitQty).ToString("0.##") + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }
        #endregion

        #region  orderQty
        List<PurchasePlanOpenOrder2> pPlanOpenOrderList = new List<PurchasePlanOpenOrder2>();
        //this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanOpenOrder>(string.Format(" select l from PurchasePlanOpenOrder as l where Type='{0}' and l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        len = 0;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanOpenOrder2>(string.Format(" select l from PurchasePlanOpenOrder2 as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { pPlanOpenOrderList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        pPlanOpenOrderList = pPlanOpenOrderList == null ? new List<PurchasePlanOpenOrder2>() : pPlanOpenOrderList;
        if (pPlanOpenOrderList != null && pPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in pPlanDetList)
            {
                var currentOrders = pPlanOpenOrderList.Where(d => d.UUID == sd.UUID).ToList();
                var showText = string.Empty;
                if (currentOrders != null && currentOrders.Count > 0)
                {
                    showText = "<table><thead><tr><th>采购路线</th><th>订单号</th><th>物料</th><th>订单数</th><th>发货数</th><th>收货数</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentOrders)
                    {
                        showText += "<td>" + c.Flow + "</td><td>" + c.OrderNo + "</td><td>" + c.Item + "</td><td>" + c.OrderQty.ToString("0.##") + "</td><td>" + c.ShipQty.ToString("0.##") + "</td><td>" + c.RecQty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.OrderDets = showText;
            }
        }
        #endregion

        #region    在途
        IList<PurchasePlanIpDet2> ipDets = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanIpDet2>(" select s from  PurchasePlanIpDet2 as s where s.PurchasePlanId=? and s.Type='" + this.rbType.SelectedValue + "'  ", pPlanDetList.First().PurchasePlanId);    //
        ipDets = ipDets == null ? new List<PurchasePlanIpDet2>() : ipDets;
        if (ipDets != null && ipDets.Count > 0)
        {
            foreach (var sd in pPlanDetList)
            {
                var currentIpdets = ipDets.Where(d => d.Item == sd.Item ).ToList();
                var showText = string.Empty;
                if (currentIpdets != null && currentIpdets.Count > 0)
                {
                    showText = "<table><thead><tr><th>ASN号</th><th>路线</th><th>物料</th><th>数量</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentIpdets)
                    {
                        showText += "<td>" + c.IpNo + "</td><td>" + c.Flow + "</td><td>" + c.Item + "</td><td>" + c.Qty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.IpDets = showText;
            }
        }
        #endregion

        var planByDateIndexs = pPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByFlowItems = pPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item});

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>包装量</th><th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th><th rowspan='2'>期初库存</th><th rowspan='2'>报验</th><th rowspan='2'>在途</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='5'>");
            if (pPlanDetList.First().Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
            {
                str.Append("<input type='checkbox' id='CheckAll' key='HeadCheck' name='" + planByDateIndex.Key.ToString("yyyyMMdd") + "'  onclick='doCheckAllClick(this)' />");
            }
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >需求数</th><th >订单数</th><th >发货数</th><th >期末</th><th >在途期末</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 14)
        {
            widths = "390%";
        }
        else if (ii > 10)
        {
            widths = "300%";
        }
        else if (ii > 6)
        {
            widths = "210%";
        }
        else if (ii > 4)
        {
            widths = "170%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d => d.WindowTime).ToDictionary(d => d.Key, d => d.Sum(q => q.PurchaseQty));
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
            str.Append(planByFlowItem.Key.Flow);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.UnitCount.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.MinLotSize.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.MaxStock.ToString("0.##"));
            str.Append("</td>");
            var InitStockQty = firstPlan.InitStock + firstPlan.InspectQty + firstPlan.InTransitQty;
            string colorStr = "";
            if (InitStockQty < firstPlan.SafeStock)
            {
                colorStr = "<td style='background:red;color:white'>";
            }
            else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
            {
                colorStr = "<td style='background:green;color:white' >";
            }
            else if (InitStockQty > firstPlan.MaxStock)
            {
                colorStr = "<td style='background:orange;'>";
            }
            //if (InitStockQty < firstPlan.SafeStock)
            //{
            //    str.Append("<td style='background:red'>");
            //}
            //else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
            //{
            //    str.Append("<td style='background:green'>");
            //}
            //else if (InitStockQty > firstPlan.MaxStock)
            //{
            //    str.Append("<td style='background:orange'>");
            //}
            str.Append(colorStr);
            str.Append(firstPlan.InitStock.ToString("0.##"));
            str.Append("</td>");
            str.Append(colorStr);
            str.Append(firstPlan.InspectQty.ToString("0.##"));
            str.Append("</td>");
            //str.Append(colorStr);
            //str.Append(firstPlan.InTransitQty.ToString("0.##"));
            //str.Append("</td>");
            if (InitStockQty < firstPlan.SafeStock)
            {
                str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:red;color:white' >", firstPlan.IpDets));
            }
            else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
            {
                str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:green;color:white' >", firstPlan.IpDets));
            }
            else if (InitStockQty > firstPlan.MaxStock)
            {
                str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:orange' >", firstPlan.IpDets));
            }
            str.Append((firstPlan.InTransitQty).ToString("0.##"));
            str.Append("</td>");
            InitStockQty = InitStockQty - firstPlan.InTransitQty;
            int fi = 0;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                //str.Append("<th >需求数</th><th >订单数</th><th >发货数</th><th >期末</th><th >在途期末</th>");
                var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                var pPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new PurchasePlanDet2();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", pPlanDet.Logs));
                str.Append(pPlanDet.ReqQty.ToString("0.##"));
                str.Append("</td>");
                str.Append(string.Format("<td tital='{0}'  onclick='doShowDetsClick(this)'>", pPlanDet.OrderDets));
                str.Append(pPlanDet.OrderQty.ToString("0.##"));
                str.Append("</td>");
                if (firstPlan.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    seq++;
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.Flow + "' item='" + firstPlan.Item + "'  name='UpQty' id='" + pPlanDet.Id + "'value='" + pPlanDet.PurchaseQty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "' style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' />");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td>");
                    str.Append("<input type='checkbox' id='CheckBoxGroup' name='D" + planByDateIndex.Key.ToString("yyyyMMdd") + "' value='" + pPlanDet.Id + "' runat='' onclick='doCheckClick(this)' />");
                    str.Append(pPlanDet.PurchaseQty.ToString("0.##"));
                    str.Append("</td>");
                }

                //var ipQty = ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime == planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime == planByDateIndex.Key).Sum(i => i.Qty) : 0;
                //if (fi == 0)
                //{
                //    ipQty = ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                //    fi++;
                //}
                //InitStockQty = InitStockQty + ipQty - pPlanDet.ReqQty + pPlanDet.OrderQty + pPlanDet.PurchaseQty;
                //InitStockQty = InitStockQty - pPlanDet.ReqQty + pPlanDet.OrderQty + pPlanDet.PurchaseQty;
                //if (InitStockQty < firstPlan.SafeStock)
                //{
                //    colorStr = "<td style='background:red;color:white'>";
                //}
                //else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
                //{
                //    colorStr = "<td style='background:green;color:white' >";
                //}
                //else if (InitStockQty > firstPlan.MaxStock)
                //{
                //    colorStr = "<td style='background:orange;'>";
                //}
                //str.Append(colorStr);
                //str.Append(InitStockQty.ToString("0.##"));
                //str.Append("</td>");

                #region
               var ipQty = ipDets.Where(i => i.Item == firstPlan.Item &&  i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item &&  i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
               var orderQtySum = pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                var shipQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.PurchaseQty);
                var reqQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.ReqQty) : 0;

                InitStockQty = firstPlan.InitStock + firstPlan.InspectQty + ipQty + orderQtySum + shipQtySum - reqQtySum;

                var inTransitQty = firstPlan.InTransitQty;


                var ipQty2 = ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item &&  i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                var orderQtySum2 = pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Count() > 0 ? pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                var shipQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Sum(i => i.PurchaseQty) : 0;
                //var reqQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.StartTime <= planByDateIndex.Key).Sum(i => i.ReqQty); traceList

                inTransitQty = inTransitQty - ipQty2 + orderQtySum2 + shipQtySum2;
                var redColor = InitStockQty + inTransitQty;


                if (redColor < firstPlan.SafeStock)
                {
                    colorStr = "<td style='background:red;color:white'>";
                }
                else if (redColor >= firstPlan.SafeStock && redColor <= firstPlan.MaxStock)
                {
                    colorStr = "<td style='background:green;color:white' >";
                }
                else if (redColor > firstPlan.MaxStock)
                {
                    colorStr = "<td style='background:orange;'>";
                }
                str.Append(colorStr);
                str.Append(InitStockQty.ToString("0.##"));
                str.Append("</td>");
                //InitStockQty = InitStockQty + shipPlanDet.OrderQty;

                str.Append(colorStr);
                str.Append(inTransitQty.ToString("0.##"));
                str.Append("</td>");
                #endregion
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    private void WeeklyListTable(IList<PurchasePlanDet2> pPlanDetList)
    {
        if (pPlanDetList == null || pPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        //var minStartTime = pPlanDetList.Min(s => s.StartTime).AddDays(14);
        //pPlanDetList = pPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        List<PurchasePlanDetTrace> traceList = new List<PurchasePlanDetTrace>();
        int len=0;
        int j = pPlanDetList.Count % 2000 == 0 ? pPlanDetList.Count / 2000 : pPlanDetList.Count / 2000 + 1;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDetTrace>(string.Format(" select l from PurchasePlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
            //this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDetTrace>(string.Format(" select l from PurchasePlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        traceList = traceList == null ? new List<PurchasePlanDetTrace>() : traceList;

        if (traceList != null && traceList.Count > 0)
        {
            foreach (var sd in pPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count > 0)
                {
                    showText = "<table><thead><tr><th>成品物料号</th><th>计划日期</th><th>计划数量</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.ProdItem + "</td><td>" + c.PlanDate.ToShortDateString() + "</td><td>" + c.ProdQty.ToString("0.##") + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }
        #endregion

        #region  orderQty
        List<PurchasePlanOpenOrder> pPlanOpenOrderList = new List<PurchasePlanOpenOrder>();
            //this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanOpenOrder>(string.Format(" select l from PurchasePlanOpenOrder as l where Type='{0}' and l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        len = 0;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanOpenOrder>(string.Format(" select l from PurchasePlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { pPlanOpenOrderList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        pPlanOpenOrderList = pPlanOpenOrderList == null ? new List<PurchasePlanOpenOrder>() : pPlanOpenOrderList;
        if (pPlanOpenOrderList != null && pPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in pPlanDetList)
            {
                var currentOrders = pPlanOpenOrderList.Where(d => d.UUID == sd.UUID).ToList();
                var showText = string.Empty;
                if (currentOrders != null && currentOrders.Count > 0)
                {
                    showText = "<table><thead><tr><th>采购路线</th><th>订单号</th><th>物料</th><th>订单数</th><th>发货数</th><th>收货数</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentOrders)
                    {
                        showText += "<td>" + c.Flow + "</td><td>" + c.OrderNo + "</td><td>" + c.Item + "</td><td>" + c.OrderQty.ToString("0.##") + "</td><td>" + c.ShipQty.ToString("0.##") + "</td><td>" + c.RecQty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.OrderDets = showText;
            }
        }
        #endregion

        #region    在途
        IList<PurchasePlanIpDet2> ipDets = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanIpDet2>(" select s from  PurchasePlanIpDet2 as s where s.PurchasePlanId=? and s.Type='" + this.rbType.SelectedValue + "'  ", pPlanDetList.First().PurchasePlanId);    //
        ipDets = ipDets == null ? new List<PurchasePlanIpDet2>() : ipDets;
        if (ipDets != null && ipDets.Count > 0)
        {
            foreach (var sd in pPlanDetList)
            {
                var currentIpdets = ipDets.Where(d => d.Item == sd.Item && d.Flow == sd.Flow).ToList();
                var showText = string.Empty;
                if (currentIpdets != null && currentIpdets.Count > 0)
                {
                    showText = "<table><thead><tr><th>ASN号</th><th>路线</th><th>物料</th><th>数量</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentIpdets)
                    {
                        showText += "<td>" + c.IpNo + "</td><td>" + c.Flow + "</td><td>" + c.Item + "</td><td>" + c.Qty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.IpDets = showText;
            }
        }
        #endregion

        var planByDateIndexs = pPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByFlowItems = pPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>包装量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='3'>");
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >需求数</th><th >订单数</th><th >计划数</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 14)
        {
            widths = "260%";
        }
        else if (ii > 10)
        {
            widths = "210%";
        }
        else if (ii > 6)
        {
            widths = "160%";
        }
        else if (ii > 4)
        {
            widths = "140%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d => d.WindowTime).ToDictionary(d => d.Key, d => d.Sum(q => q.PurchaseQty));
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
            str.Append(planByFlowItem.Key.Flow);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            
            str.Append("<td>");
            str.Append(firstPlan.UnitCount.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.MaxStock.ToString("0.##"));
            str.Append("</td>");

            foreach (var planByDateIndex in planByDateIndexs)
            {
                // str.Append("<th >需求数</th><th >订单数</th><th >发货数</th><th >期末</th><th >在途期末</th>")
                var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                var pPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new PurchasePlanDet2();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", pPlanDet.Logs));
                str.Append(pPlanDet.ReqQty.ToString("0.##"));
                str.Append("</td>");
                str.Append(string.Format("<td tital='{0}'  onclick='doShowDetsClick(this)'>", pPlanDet.OrderDets));
                str.Append(pPlanDet.OrderQty.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(pPlanDet.PurchaseQty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }


    #endregion



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            var allSeqArr = string.IsNullOrEmpty(this.btSeqHidden.Value) ? new string[0] : this.btSeqHidden.Value.Split(',');
            var allShipQty = string.IsNullOrEmpty(this.btQtyHidden.Value) ? new string[0] : this.btQtyHidden.Value.Split(',');
            string allHtml = this.list.InnerHtml;
            IList<string> flowList = new List<string>();
            IList<string> itemList = new List<string>();
            IList<string> qtyList = new List<string>();
            IList<string> idList = new List<string>();
            IList<string> releaseNoList = new List<string>();
            IList<string> dateFromList = new List<string>();

            string flow = string.Empty;
            string item = string.Empty;
            string qty = string.Empty;
            string id = string.Empty;
            string releaseNo = string.Empty;
            string dateFrom = string.Empty;
            string seq = string.Empty;
            while (allHtml.Length > 0)
            {
                int startIndex = allHtml.IndexOf("flow='");
                if (startIndex == -1) { allHtml = string.Empty; break; }
                allHtml = allHtml.Substring(startIndex + 6);
                int endIndex = allHtml.IndexOf("'");
                flow = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("item='");
                allHtml = allHtml.Substring(startIndex + 6);
                endIndex = allHtml.IndexOf("'");
                item = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("id='");
                allHtml = allHtml.Substring(startIndex + 4);
                endIndex = allHtml.IndexOf("'");
                id = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("value='");
                allHtml = allHtml.Substring(startIndex + 7);
                endIndex = allHtml.IndexOf("'");
                qty = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("releaseNo='");
                allHtml = allHtml.Substring(startIndex + 11);
                endIndex = allHtml.IndexOf("'");
                releaseNo = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("dateFrom='");
                allHtml = allHtml.Substring(startIndex + 10);
                endIndex = allHtml.IndexOf("'");
                dateFrom = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("seq='");
                allHtml = allHtml.Substring(startIndex + 5);
                endIndex = allHtml.IndexOf("'");
                seq = allHtml.Substring(0, endIndex);
                if (allSeqArr.Contains(seq))
                {
                    int i = 0;
                    foreach (var s in allSeqArr)
                    {
                        i++;
                        if (s == seq) break;
                    }
                    if (allShipQty[i - 1] == qty)
                    { }
                    else
                    {
                        flowList.Add(flow);
                        itemList.Add(item);
                        idList.Add(id);
                        qtyList.Add(allShipQty[i - 1]);
                        releaseNoList.Add(releaseNo);
                        dateFromList.Add(dateFrom);
                    }
                }


            }
            IList<decimal> shipQtyList = new List<decimal>();
            foreach (var q in qtyList)
            {
                try
                {
                    shipQtyList.Add(Convert.ToDecimal(q));
                }
                catch (Exception exc)
                {
                    ShowErrorMessage("数量" + q + "填写错误");
                }
            }
            if (flowList.Count == 0)
            {
                ShowErrorMessage("没有要修改的计划。");
            }
            TheMrpMgr.UpdatePurchasePlanQty2(flowList, itemList, idList, shipQtyList, releaseNoList, dateFromList, this.CurrentUser,this.rbType.SelectedValue);
            ShowSuccessMessage("修改成功。");
            this.btnSearch_Click(null, null);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"
	select det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,max(isnull(det.ReqQty,0)),max(isnull(det.PurchaseQty,0)),det.WindowTime,det.Version,m.ReleaseNo,m.Status,max(isnull(l.InitStock,0)),max(isnull(l.SafeStock,0)),max(isnull(l.InTransitQty,0)),max(isnull(l.InspectQty,0)),max(isnull(det.OrderQty,0)),max(isnull(l.MaxStock,0)),det.StartTime,max(isnull(det.uc,0)),max(isnull(f.MrpLeadTime,0)),m.Id,max(isnull(det.MinLotSize,0)),max(isnull(det.UnitQty,0))
 from MRP_PurchasePlanDet2 as det with(nolock) 
inner join MRP_PurchasePlanMstr2 as m with(nolock) on m.Id=det.PurchasePlanId
inner join FlowMstr as f on det.Flow=f.Code
inner join FlowDet as fd on det.Flow=fd.Flow and det.Item=fd.Item
left join MRP_PurchasePlanInitLocationDet2 as l with(nolock) on det.PurchasePlanId=l.PurchasePlanId and det.Item=l.Item where 1=1     ";

        searchSql += string.Format(" and det.Type='{0}' ", this.rbType.SelectedValue);

        string flowCodeValues = this.tbFlow.Value.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", "','");
            flowCodeValues = flowCodeValues.Replace("\n", "','");
        }
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            searchSql += string.Format(" and det.Flow in ('{0}') ", flowCodeValues);
        }
        else
        {
            //this.list.InnerHtml = "";
            //ShowErrorMessage("采购路线不能为空。");
            //return;
        }

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " group by det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,det.WindowTime,det.Version,m.ReleaseNo,m.Status,det.StartTime,m.Id order by det.Flow,det.Item asc ";
        //searchSql += " order by det.Flow,det.Item asc ";

        var flowCodes = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var pPlanDetList = new List<PurchasePlanDet2>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            //det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,isnull(det.ReqQty,0),isnull(det.PurchaseQty,0),det.WindowTime,det.Version,m.ReleaseNo,m.Status,isnull(l.InitStock,0),
            //isnull(l.SafeStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.OrderQty,0),isnull(l.MaxStock,0)
            pPlanDetList.Add(new PurchasePlanDet2
            {
                Id = Convert.ToInt32(row[0].ToString()),
                UUID = row[1].ToString(),
                Flow = row[2].ToString(),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                ReqQty = Math.Round(Convert.ToDecimal(row[6])),
                PurchaseQty = Convert.ToInt32(row[7]),
                WindowTime = Convert.ToDateTime(row[8]),
                Version = Convert.ToInt32(row[9]),
                ReleaseNo = Convert.ToInt32(row[10]),
                Status = row[11].ToString(),
                InitStock = Math.Round(Convert.ToDecimal(row[12])),
                SafeStock = Math.Round(Convert.ToDecimal(row[13])),
                InTransitQty = Math.Round(Convert.ToDecimal(row[14])),
                InspectQty = Math.Round(Convert.ToDecimal(row[15])),
                OrderQty = Math.Round(Convert.ToDecimal(row[16])),
                MaxStock = Convert.ToDecimal(row[17]),
                StartTime = Convert.ToDateTime(row[18]),
                UnitCount = Math.Round(Convert.ToDecimal(row[19])),
                MrpLeadTime = Math.Round(Convert.ToDecimal(row[20])),
                PurchasePlanId = Convert.ToInt32(row[21]),
                MinLotSize = Math.Round(Convert.ToDecimal(row[22])),
                UnitQty = Math.Round(Convert.ToDecimal(row[23])),
            });
        }
        if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            var minWindowTime = pPlanDetList.Min(s => s.StartTime).AddDays(13);
            pPlanDetList = pPlanDetList.Where(s => s.StartTime <= minWindowTime).ToList();

            #region   trace
            List<PurchasePlanDetTrace2> traceList = new List<PurchasePlanDetTrace2>();
            int len = 0;
            int j = pPlanDetList.Count % 2000 == 0 ? pPlanDetList.Count / 2000 : pPlanDetList.Count / 2000 + 1;
            while (true)
            {
                var cList = this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDetTrace2>(string.Format(" select l from PurchasePlanDetTrace2 as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
                if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
                len++;
                if (len == j) break;
            }
            //this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDetTrace>(string.Format(" select l from PurchasePlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
            traceList = traceList == null ? new List<PurchasePlanDetTrace2>() : traceList;
            #endregion

            #region  orderQty
            List<PurchasePlanOpenOrder2> pPlanOpenOrderList = new List<PurchasePlanOpenOrder2>();
            len = 0;
            while (true)
            {
                var cList = this.TheGenericMgr.FindAllWithCustomQuery<PurchasePlanOpenOrder2>(string.Format(" select l from PurchasePlanOpenOrder2 as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", pPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
                if (cList != null && cList.Count > 0) { pPlanOpenOrderList.AddRange(cList); }
                len++;
                if (len == j) break;
            }
            pPlanOpenOrderList = pPlanOpenOrderList == null ? new List<PurchasePlanOpenOrder2>() : pPlanOpenOrderList;
            #endregion

            #region    在途
            IList<PurchasePlanIpDet2> ipDets = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanIpDet2>(" select s from  PurchasePlanIpDet2 as s where s.PurchasePlanId=? and s.Type='" + this.rbType.SelectedValue + "'  ", pPlanDetList.First().PurchasePlanId);    //
            ipDets = ipDets == null ? new List<PurchasePlanIpDet2>() : ipDets;
            if (ipDets != null && ipDets.Count > 0)
            {
                foreach (var sd in pPlanDetList)
                {
                    var currentIpdets = ipDets.Where(d => d.Item == sd.Item && d.Flow == sd.Flow).ToList();
                    var showText = string.Empty;
                    if (currentIpdets != null && currentIpdets.Count > 0)
                    {
                        showText = "<table><thead><tr><th>ASN号</th><th>路线</th><th>物料</th><th>数量</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                        foreach (var c in currentIpdets)
                        {
                            showText += "<td>" + c.IpNo + "</td><td>" + c.Flow + "</td><td>" + c.Item + "</td><td>" + c.Qty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                        }
                        showText += " </tr></tbody></table> ";
                    }
                    sd.IpDets = showText;
                }
            }
            #endregion


            IList<object> data = new List<object>();
            data.Add(pPlanDetList);
            data.Add(traceList);
            data.Add(pPlanOpenOrderList);
            data.Add(ipDets);
            TheReportMgr.WriteToClient("PurchasePlanDaily2.xls", data, "PurchasePlanDaily2.xls");
        }
        else
        {
            ExportWeeklyExcel(pPlanDetList);
        }

    }

    private void ExportWeeklyExcel(IList<PurchasePlanDet2> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var planByDateIndexs = exportList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
            var planByFlowItems = exportList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });
            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
            Row rowHeader2 = sheet1.CreateRow(1);
            //包装量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");

            for (int i = 0; i < 8 + planByDateIndexs.Count() * 3; i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("路线");
                }
                else if (i == 2)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 3)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 4)      //包装量
                {
                    rowHeader.CreateCell(i).SetCellValue("包装量");
                }
                else if (i == 5)      //经济批量
                {
                    rowHeader.CreateCell(i).SetCellValue("经济批量");
                }
                else if (i == 6)      //安全库存
                {
                    rowHeader.CreateCell(i).SetCellValue("安全库存");
                }
                else if (i == 7)      //最大库存
                {
                    rowHeader.CreateCell(i).SetCellValue("最大库存");
                }
                else
                {
                    foreach (var date in planByDateIndexs)
                    {
                        rowHeader.CreateCell(i).SetCellValue(date.Key.ToShortDateString());
                        int i2 = i;
                        rowHeader2.CreateCell(i2++).SetCellValue("需求数");
                        rowHeader2.CreateCell(i2++).SetCellValue("订单数");
                        rowHeader2.CreateCell(i2++).SetCellValue("计划数");
                        i += 3;
                    }
                }
            }
            #endregion

            #region 写入数值
            int l = 0;
            int rowIndex = 2;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.PurchaseQty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Flow);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MinLotSize.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                    var pdPlan = curenPlan.Count() > 0 ? curenPlan.First() : new PurchasePlanDet2();
                    var createCell = rowDetail.CreateCell(cell++);
                    createCell.SetCellType(CellType.NUMERIC);
                    createCell.SetCellValue(Convert.ToDouble(pdPlan.ReqQty));

                    var createCell2 = rowDetail.CreateCell(cell++);
                    createCell2.SetCellType(CellType.NUMERIC);
                    createCell2.SetCellValue(Convert.ToDouble(pdPlan.OrderQty));

                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(pdPlan.PurchaseQty));
                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "PurchasePlanWeekly2.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            this.btQtyHidden.Value = string.Empty;
            this.btSeqHidden.Value = string.Empty;
            if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
            {

                var purchasePlanMstr = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr2>(" select s from PurchasePlanMstr2 as s where s.ReleaseNo=? ", currentRelesNo).First();
                if (purchasePlanMstr.Status == BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT)
                {
                    throw new BusinessErrorException("已释放的采购计划不能导入。");
                }
                this.ReadPurchasePlanFromXls2(fileUpload.PostedFile.InputStream, this.CurrentUser, purchasePlanMstr);
                ShowSuccessMessage("导入成功。");
                this.btnSearch_Click(null, null);
            }
            else
            {
                throw new BusinessErrorException("只能导入日计划。");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnCreateOrder_Click(object sender, EventArgs e)
    {
        string ids = this.btIds.Value;
        try
        {
            if (!string.IsNullOrEmpty(ids))
            {
                TheMrpMgr.CreateOrderByPurchasePlan(ids.Substring(0, ids.Length - 1), this.CurrentUser);
                ShowSuccessMessage("发货成功。");
                this.btnSearch_Click(null, null);
            }
            else
            {
                throw new BusinessErrorException("请选择要发货订单明细。");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception et)
        {
            ShowErrorMessage(et.Message);
        }


    }

    public void ReadPurchasePlanFromXls2(Stream inputStream, User user, PurchasePlanMstr2 purchasePlanMstr)
    {
        DateTime startDate = DateTime.Today;
        DateTime endDate = startDate.AddDays(13);
        DateTime nowTime = System.DateTime.Now;

        if (inputStream.Length == 0)
        {
            throw new BusinessErrorException("Import.Stream.Empty");
        }

        HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

        Sheet sheet = workbook.GetSheetAt(0);

        IEnumerator rows = sheet.GetRowEnumerator();

        Row dateRow = sheet.GetRow(0);

        ImportHelper.JumpRows(rows, 2);

        var purchasePlanDetList = new List<PurchasePlanDet2>();
        var existsDets = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDet2>(" select s from PurchasePlanDet2 as s where s.PurchasePlanId=? ", purchasePlanMstr.Id);

        int colFlow = 1;//路线
        int colItemCode = 2;//物料号

        List<string> errorMessages = new List<string>();


        while (rows.MoveNext())
        {
            string flowCode = null;
            string itemCode = null;

            HSSFRow row = (HSSFRow)rows.Current;
            if (!ImportHelper.CheckValidDataRow(row, 0, 3))
            {
                break;//边界
            }
            string rowIndex = (row.RowNum + 1).ToString();

            #region 读取路线代码
            flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlow));
            if (string.IsNullOrEmpty(flowCode))
            {
                errorMessages.Add(string.Format("路线不能为空,第{0}行", rowIndex));
                continue;
            }
            #endregion

            #region 读取物料代码
            try
            {
                itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                if (itemCode == null)
                {
                    errorMessages.Add(string.Format("物料不能为空,第{0}行", rowIndex));
                    continue;
                }
            }
            catch
            {
                errorMessages.Add(string.Format("读取物料时出错,第{0}行.", rowIndex));
                continue;
            }
            #endregion

            #region 读取数量
            try
            {
                int i = 11;
                Random r = new Random();
                int t = r.Next(0, 300);
                int tt = r.Next(500, 1000);
                while (true)
                {
                    if (i > 80)
                    {
                        break;
                    }
                    Cell dateCell = dateRow.GetCell(i);
                    string dateIndex = null;

                    #region 读取计划日期
                    if (dateCell != null)
                    {
                        DateTime currentDateTime = DateTime.Today;
                        if (dateCell.CellType == CellType.STRING)
                        {
                            dateIndex = dateCell.StringCellValue;
                        }

                        //if (currentDateTime.CompareTo(startDate) < 0)
                        //{
                        //    continue;
                        //}
                        //if (currentDateTime.CompareTo(endDate) > 0)
                        //{
                        //    break;
                        //}
                    }
                    else
                    {
                        break;
                    }
                    #endregion

                    decimal qty = 0;
                    if (row.GetCell(i + 2) != null)
                    {
                        if (row.GetCell(i + 2).CellType == CellType.NUMERIC)
                        {
                            qty = Convert.ToDecimal(row.GetCell(i + 2).NumericCellValue);
                        }
                        else
                        {
                            string qtyValue = ImportHelper.GetCellStringValue(row.GetCell(i + 2));
                            if (qtyValue != null)
                            {
                                if (!decimal.TryParse(qtyValue, out qty))
                                {
                                    errorMessages.Add(string.Format("数量格式不正确,第{0}行", rowIndex));
                                    i += 5;
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        i += 5;
                        continue;
                    }

                    if (qty < 0)
                    {
                        errorMessages.Add(string.Format("数量不能小于0,第{0}行", rowIndex));
                        i += 5;
                        continue;
                    }
                    else
                    {
                        var existsDet = existsDets.Where(e => e.Flow == flowCode && e.Item == itemCode && e.StartTime.ToString("yyyy-MM-dd") == dateIndex);
                        if (existsDet != null && existsDet.Count() > 0)
                        {
                            var first = existsDet.First();
                            if (first.PurchaseQty != qty)
                            {
                                first.PurchaseQty = qty;
                                first.LastModifyDate = nowTime;
                                first.LastModifyUser = user.Code;
                                purchasePlanDetList.Add(first);
                            }
                        }
                        else
                        {
                            if (qty == 0)
                            {
                                i += 5;
                                continue;
                            }
                            var flowDets = TheGenericMgr.GetDatasetBySql(string.Format(@"select d.Flow,d.Item,d.RefItemCode,d.UC,d.UOM,i.Desc1,isnull(i.MinLotSize,0) from flowdet as d inner join Item as i on d.Item=i.Code where d.Flow='{0}' and d.Item='{1}' ", flowCode, itemCode)).Tables[0];          //m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1
                            var getFlowDetList = new List<object[]>();
                            foreach (System.Data.DataRow readRow in flowDets.Rows)
                            {
                                //d.Flow,d.Item,d.RefItemCode,d.UC,d.UOM,i.Desc1,i.MinLotSize 
                                getFlowDetList.Add(new object[] { readRow[0].ToString(), readRow[1].ToString(), readRow[2].ToString(), Convert.ToDecimal(readRow[3].ToString()), readRow[4].ToString(), readRow[5].ToString(), Convert.ToDecimal(readRow[6].ToString()) });
                            }
                            if (getFlowDetList.Count == 0)
                            {
                                errorMessages.Add(string.Format("第{0}行:路线{1}中没有维护物料{2}", rowIndex, flowCode, itemCode));
                                i += 5;
                                continue;
                            }
                            //Id, PurchasePlanId, Type, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom,
                            //UnitQty, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                            PurchasePlanDet2 nDet = new PurchasePlanDet2();
                            nDet.Id = 0;
                            nDet.PurchasePlanId = purchasePlanMstr.Id;
                            nDet.Type = "Daily";
                            nDet.UUID = System.DateTime.Now.ToString() + t++ + tt++;
                            nDet.Flow = getFlowDetList[0][0].ToString();
                            nDet.Item = getFlowDetList[0][1].ToString();
                            nDet.ItemDesc = getFlowDetList[0][5].ToString();
                            nDet.RefItemCode = getFlowDetList[0][2].ToString();
                            nDet.ReqQty = 0;
                            nDet.OrgPurchaseQty = 0;
                            nDet.PurchaseQty = qty;
                            nDet.OrderQty = 0;
                            nDet.Uom = getFlowDetList[0][4].ToString();
                            nDet.BaseUom = getFlowDetList[0][4].ToString();
                            nDet.UnitQty = 1;
                            nDet.UnitCount = Convert.ToDecimal(getFlowDetList[0][3]);
                            nDet.MinLotSize = Convert.ToDecimal(getFlowDetList[0][6]);
                            nDet.StartTime = Convert.ToDateTime(dateIndex);
                            nDet.WindowTime = Convert.ToDateTime(dateIndex);
                            nDet.CreateDate = nowTime;
                            nDet.CreateUser = user.Code;
                            nDet.LastModifyDate = nowTime;
                            nDet.LastModifyUser = user.Code;
                            nDet.Version = 1;
                            purchasePlanDetList.Add(nDet);
                        }
                    }
                    i += 5;
                }
            }
            catch (Exception ex)
            {
                errorMessages.Add(ex.Message);
            }
            #endregion
        }

        if (errorMessages.Count > 0)
        {
            string errorMes = string.Empty;
            foreach (var error in errorMessages)
            {
                errorMes += error + "-";
            }
            throw new BusinessErrorException(errorMes);
        }

        //TheMrpMgr.UpdatePurchaseDets(purchasePlanDetList);
        foreach (var shipPlan in purchasePlanDetList)
        {
            if (shipPlan.Id > 0)
            {
                TheGenericMgr.Update(shipPlan);
            }
            else
            {
                TheGenericMgr.Create(shipPlan);
            }
        }
    }
}