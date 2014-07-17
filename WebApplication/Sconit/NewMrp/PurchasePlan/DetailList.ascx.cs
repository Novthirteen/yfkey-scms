using System;
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

public partial class NewMrp_ShipPlan_DetailList : MainModuleBase
{
    public static string currentRelesNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;

        if (!IsPostBack)
        {
            this.btQtyHidden.Value = string.Empty;
            this.btSeqHidden.Value = string.Empty;
            this.tbFlow.Text = string.Empty;
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
        this.tbFlow.Text = string.Empty;
        this.list.InnerHtml = "";
        currentRelesNo = relesNo;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"
select det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,isnull(det.ReqQty,0),isnull(det.PurchaseQty,0),det.WindowTime,det.Version,m.ReleaseNo,m.Status,isnull(l.InitStock,0),isnull(l.SafeStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.OrderQty,0),isnull(l.MaxStock,0),det.StartTime,isnull(det.uc,0),isnull(f.MrpLeadTime,0),m.Id,isnull(det.MinLotSize,0)
 from MRP_PurchasePlanDet as det 
inner join MRP_PurchasePlanMstr as m on m.Id=det.PurchasePlanId
 inner join FlowMstr as f on det.Flow=f.Code
left join MRP_PurchasePlanInitLocationDet as l on det.PurchasePlanId=l.PurchasePlanId and det.Item=l.Item where 1=1  ";

        searchSql += string.Format(" and det.Type='{0}' ", this.rbType.SelectedValue);

        if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        {
            searchSql += string.Format(" and det.Flow ='{0}' ", this.tbFlow.Text.Trim());
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

        searchSql += " order by det.Item asc ";

        var flowCodes = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var purchasePlanDetList = new List<PurchasePlanDet>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            //det.Id,det.UUID,det.Flow,det.Item,det.ItemDesc,det.RefItemCode,isnull(det.ReqQty,0),isnull(det.PurchaseQty,0),det.WindowTime,det.Version,m.ReleaseNo,m.Status,isnull(l.InitStock,0),
            //isnull(l.SafeStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.OrderQty,0),isnull(l.MaxStock,0)
            purchasePlanDetList.Add(new PurchasePlanDet
            {
                Id = Convert.ToInt32(row[0].ToString()),
                UUID = row[1].ToString(),
                Flow = row[2].ToString(),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                ReqQty = Convert.ToDecimal(row[6]),
                PurchaseQty = Convert.ToInt32(row[7]),
                WindowTime = Convert.ToDateTime(row[8]),
                Version = Convert.ToInt32(row[9]),
                ReleaseNo = Convert.ToInt32(row[10]),
                Status = row[11].ToString(),
                InitStock = Convert.ToDecimal(row[12]),
                SafeStock = Convert.ToDecimal(row[13]),
                InTransitQty = Convert.ToDecimal(row[14]),
                InspectQty = Convert.ToDecimal(row[15]),
                OrderQty = Convert.ToDecimal(row[16]),
                MaxStock = Convert.ToDecimal(row[17]),
                StartTime = Convert.ToDateTime(row[18]),
                UnitCount = Convert.ToDecimal(row[19]),
                MrpLeadTime = Convert.ToDecimal(row[20]),
                PurchasePlanId = Convert.ToInt32(row[21]),
                MinLotSize = Convert.ToDecimal(row[22]),
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

    private void ListTable(IList<PurchasePlanDet> pPlanDetList)
    {
        if (pPlanDetList == null || pPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        var minStartTime = pPlanDetList.Min(s => s.StartTime).AddDays(13);
        pPlanDetList = pPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        List<PurchasePlanDetTrace> traceList = new List<PurchasePlanDetTrace>();
        int len = 0;
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
        //IList<PurchasePlanIpDet> ipDets = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanIpDet>(" select s from  PurchasePlanIpDet as s where s.PurchasePlanId=? and s.Type='" + this.rbType.SelectedValue + "'  ", pPlanDetList.First().PurchasePlanId);    //
        //ipDets = ipDets == null ? new List<PurchasePlanIpDet>() : ipDets;
        //if (ipDets != null && ipDets.Count > 0)
        //{
        //    foreach (var sd in pPlanDetList)
        //    {
        //        var currentIpdets = ipDets.Where(d => d.Item == sd.Item && d.Flow == ss.Flow).ToList();
        //        var showText = string.Empty;
        //        if (currentIpdets != null && currentIpdets.Count > 0)
        //        {
        //            showText = "<table><thead><tr><th>ASN号</th><th>路线</th><th>物料</th><th>数量</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
        //            foreach (var c in currentIpdets)
        //            {
        //                showText += "<td>" + c.IpNo + "</td><td>" + c.Flow + "</td><td>" + c.Item + "</td><td>" + c.Qty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
        //            }
        //            showText += " </tr></tbody></table> ";
        //        }
        //        sd.IpDets = showText;
        //    }
        //}
        #endregion

        var planByDateIndexs = pPlanDetList.GroupBy(p => p.WindowTime).OrderBy(p => p.Key);
        var planByFlowItems = pPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item});

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>包装量</th><th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th><th rowspan='2'>期初库存</th><th rowspan='2'>报验</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='4'>");
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >需求数</th><th >订单数</th><th >采购数</th><th >期末</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii >= 14)
        {
            widths = "350%";
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
            str.Append(firstPlan.MinLotSize.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.MaxStock.ToString("0.##"));
            str.Append("</td>");
            var InitStockQty = firstPlan.InitStock + firstPlan.InTransitQty+firstPlan.InspectQty;
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
            //if (InitStockQty < firstPlan.SafeStock)
            //{
            //    str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:red;color:white' >", firstPlan.IpDets));
            //}
            //else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
            //{
            //    str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:green;color:white' >", firstPlan.IpDets));
            //}
            //else if (InitStockQty > firstPlan.MaxStock)
            //{
            //    str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:orange' >", firstPlan.IpDets));
            //}
            //str.Append((firstPlan.InTransitQty).ToString("0.##"));
            //str.Append("</td>");
            str.Append(colorStr);
            str.Append((firstPlan.InspectQty).ToString("0.##"));
            str.Append("</td>");
            InitStockQty = InitStockQty - firstPlan.InTransitQty;
            int fi = 0;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                // str.Append("<th >需求数</th><th >订单数</th><th >采购数</th><th >期末</th>")
                var curenPlan = planByFlowItem.Where(p => p.WindowTime == planByDateIndex.Key);
                var pPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new PurchasePlanDet();
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
                InitStockQty = InitStockQty - pPlanDet.ReqQty + pPlanDet.OrderQty + pPlanDet.PurchaseQty;
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
                str.Append(colorStr);
                str.Append(InitStockQty.ToString("0.##"));
                str.Append("</td>");

                #region
                //var ipQty = ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                //var orderQtySum = pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                //var shipQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.WindowTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.PurchaseQty);
                //var reqQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Sum(i => i.ReqQty) : 0;

                //InitStockQty = firstPlan.InitStock + ipQty + orderQtySum + shipQtySum - reqQtySum;

                //var inTransitQty = firstPlan.InTransitQty;


                //var ipQty2 = ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                //var orderQtySum2 = pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Count() > 0 ? pPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                //var shipQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key && i.WindowTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key && i.WindowTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Sum(i => i.PurchaseQty) : 0;
                ////var reqQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.StartTime <= planByDateIndex.Key).Sum(i => i.ReqQty);

                //inTransitQty = inTransitQty - ipQty2 + orderQtySum2 + shipQtySum2;
                //var redColor = InitStockQty + inTransitQty;


                //if (redColor < firstPlan.SafeStock)
                //{
                //    colorStr = "<td style='background:red;color:white'>";
                //}
                //else if (redColor >= firstPlan.SafeStock && redColor <= firstPlan.MaxStock)
                //{
                //    colorStr = "<td style='background:green;color:white' >";
                //}
                //else if (redColor > firstPlan.MaxStock)
                //{
                //    colorStr = "<td style='background:orange;'>";
                //}
                //str.Append(colorStr);
                //str.Append(InitStockQty.ToString("0.##"));
                //str.Append("</td>");
                ////InitStockQty = InitStockQty + shipPlanDet.OrderQty;

                //str.Append(colorStr);
                //str.Append(inTransitQty.ToString("0.##"));
                //str.Append("</td>");
                #endregion
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    private void WeeklyListTable(IList<PurchasePlanDet> pPlanDetList)
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


        var planByDateIndexs = pPlanDetList.GroupBy(p => p.WindowTime).OrderBy(p => p.Key);
        var planByFlowItems = pPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
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
            str.Append("<th >需求数</th><th >订单数</th><th >采购数</th>");
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
                // str.Append("<th >需求数</th><th >订单数</th><th >采购数</th><th >期末</th>")
                var curenPlan = planByFlowItem.Where(p => p.WindowTime == planByDateIndex.Key);
                var pPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new PurchasePlanDet();
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
                    str.Append(pPlanDet.PurchaseQty.ToString("0.##"));
                    str.Append("</td>");
                }
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
            TheMrpMgr.UpdatePurchasePlanQty(flowList, itemList, idList, shipQtyList, releaseNoList, dateFromList, this.CurrentUser,this.rbType.SelectedValue);
            ShowSuccessMessage("修改成功。");
            this.btnSearch_Click(null, null);
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
}