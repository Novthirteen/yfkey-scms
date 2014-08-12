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
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

public partial class NewMrp_ShipPlan_DetailList : MainModuleBase
{
    public static string currentRelesNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;
        //bool includeProcurement, bool includeDistribution, bool includeTransfer, bool includeProduction, bool includeCustomerGoods, bool includeSubconctracting,
        if (!IsPostBack)
        {
            //this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
            this.btQtyHidden.Value = string.Empty;
            this.btSeqHidden.Value = string.Empty;
            this.tbFlow.Value = string.Empty;
        }

    }

    #region   明细查询
    public void GetView(string relesNo)
    {
        this.tbFlow.Value = string.Empty;
        this.list.InnerHtml = "";
        currentRelesNo = relesNo;
        var shipPlanMstr = TheGenericMgr.FindAllWithCustomQuery<ShipPlanMstr>(" select s from ShipPlanMstr as s where s.ReleaseNo=? ", currentRelesNo).First();
        if (shipPlanMstr.Status == BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT)
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
        //,ip.StartTime as ipStartTime,ip.WindowTime as ipWindowTime,isnull(ip.Qty,0)
        var searchSql = @" select det.Flow,det.Item,det.ItemDesc,det.RefItemCode,det.LocFrom,det.LocTo,det.WindowTime,det.Version,isnull(det.ShipQty,0),isnull(det.OrgShipQty,0),m.ReleaseNo,m.Status,m.LastModifyDate,m.LastModifyUser,det.Id,isnull(det.ReqQty,0),isnull(l.InitStock,0),isnull(l.SafeStock,0), isnull(l.InTransitQty,0),det.UUID ,det.StartTime,isnull(det.OrderQty,0),isnull(l.MaxStock,0) ,det.uc,isnull(f.MrpLeadTime,0),m.Id
from  MRP_ShipPlanDet as det 
 inner join MRP_ShipPlanMstr as m on det.ShipPlanId=m.Id 
 inner join FlowMstr as f on det.Flow=f.Code
 left join MRP_ShipPlanInitLocationDet as l on det.ShipPlanId=l.ShipPlanId and det.Item=l.Item and det.LocTo=l.Location where 1=1  ";
        //left join MRP_ShipPlanIpDet as ip on ip.ShipPlanId=m.Id and ip.Item=det.Item where 1=1  ";
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
            ShowErrorMessage("发运路线不能为空。");
            return;
        }
        //if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        //{
        //    searchSql += string.Format(" and det.Flow  in '{0}' ", this.tbFlow.Text.Trim());
        //}
        //else
        //{
        //    this.list.InnerHtml = "";
        //    ShowErrorMessage("发运路线不能为空。");
        //    return;
        //}

        //DateTime startTime = DateTime.Today;
        //if (!string.IsNullOrEmpty(this.tbStartDate.Text.Trim()))
        //{
        //    DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        //    searchSql += string.Format(" and det.StartTime>='{0}' ", startTime.Date);
        //}
        //else
        //{
        //    this.list.InnerHtml = "";
        //    ShowErrorMessage("开始日期不能为空。");
        //    return;
        //}
        //if (!string.IsNullOrEmpty(this.tbEndDate.Text.Trim()))
        //{
        //    DateTime endTime = DateTime.Today;
        //    DateTime.TryParse(this.tbEndDate.Text.Trim(), out endTime);
        //    searchSql += string.Format(" and det.StartTime<='{0}' ", endTime.Date);
        //}
        //else
        //{
        //    DateTime endTime = startTime.AddDays(14);
        //    searchSql += string.Format(" and det.StartTime<='{0}' ", endTime.Date);
        //}

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " order by det.Flow,det.Item asc ";

        var flowCodes = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var shipPlanDetList = new List<ShipPlanDet>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            //det.Flow,det.Item,i.Desc1,det.RefItemCode,det.LocFrom,det.LocTo,det.WindowTime,det.Version,det.ShipQty,
            //det.OrgShipQty,m.ReleaseNo,m.Status,m.LastModifyDate,m.LastModifyUser,det.Id,det.ReqQty,l.InitStock,l.SafeStock, l.InTransitQty
            shipPlanDetList.Add(new ShipPlanDet
            {
                Flow = row[0].ToString(),
                Item = row[1].ToString(),
                ItemDesc = row[2].ToString(),
                RefItemCode = row[3].ToString(),
                LocFrom = row[4].ToString(),
                LocTo = row[5].ToString(),
                WindowTime = Convert.ToDateTime(row[6]),
                Version = Convert.ToInt32(row[7]),
                ShipQty = Convert.ToDecimal(row[8]),
                OrgShipQty = Convert.ToDecimal(row[9]),
                ReleaseNo = Convert.ToInt32(row[10]),
                Status = row[11].ToString(),
                Id = Convert.ToInt32(row[14].ToString()),
                ReqQty = Convert.ToDecimal(row[15]),
                InitStock = Convert.ToDecimal(row[16]),
                SafeStock = Convert.ToDecimal(row[17]),
                InTransitQty = Convert.ToDecimal(row[18]),
                UUID = row[19].ToString(),
                StartTime = Convert.ToDateTime(row[20]),
                OrderQty = Convert.ToDecimal(row[21]),
                MaxStock = Convert.ToDecimal(row[22]),
                UnitCount = Convert.ToDecimal(row[23]),
                MrpLeadTime = Convert.ToDecimal(row[24]),
                ShipPlanId = Convert.ToInt32(row[25]),
                //IpStartTime =(object)row[25]!=null? (DateTime?)Convert.ToDateTime(row[25]):null,
                //IpWindowTime = (object)row[26] != null ? (DateTime?)Convert.ToDateTime(row[26]) : null,
                //IpQty = (object)row[27] != null ? (decimal?)Convert.ToDecimal(row[27]) : null,
            });
        }
        if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            ListTable(shipPlanDetList);
        }
        else
        {
            WeeklyListTable(shipPlanDetList);
        }
    }

    private void ListTable(IList<ShipPlanDet> shipPlanDetList)
    {
        if (shipPlanDetList == null || shipPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        var minStartTime = shipPlanDetList.Min(s => s.StartTime).AddDays(13);
        shipPlanDetList = shipPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        List<ShipPlanDetTrace> traceList = new List<ShipPlanDetTrace>();
        int len = 0;
        int j = shipPlanDetList.Count % 2000 == 0 ? shipPlanDetList.Count / 2000 : shipPlanDetList.Count / 2000 + 1;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
       // traceList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        
        if (traceList!=null && traceList.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).OrderBy(d=>d.ReqDate).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count > 0)
                {
                    showText = "<table><thead><tr><th>销售路线</th><th>物料</th><th>需求日期</th><th>需求数</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.DistributionFlow + "</td><td>" + c.Item + "</td><td>" + c.ReqDate.ToShortDateString() + "</td><td>" + c.ReqQty.ToString("0.##") + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }
        #endregion

        #region  orderQty
        List<ShipPlanOpenOrder> shipPlanOpenOrderList = new List<ShipPlanOpenOrder>();
        len = 0;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanOpenOrder>(string.Format(" select l from ShipPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { shipPlanOpenOrderList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        //shipPlanOpenOrderList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanOpenOrder>(string.Format(" select l from ShipPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        //shipPlanOpenOrderList = shipPlanOpenOrderList == null ? new List<ShipPlanOpenOrder>() : shipPlanOpenOrderList;    
        if (shipPlanOpenOrderList!=null && shipPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
            {
                var currentOrders = shipPlanOpenOrderList.Where(d => d.UUID == sd.UUID).OrderBy(d=>d.WindowTime).ToList();
                var showText = string.Empty;
                if (currentOrders != null && currentOrders.Count > 0)
                {
                    showText = "<table><thead><tr><th>订单号</th><th>物料</th><th>订单数</th><th>发货数</th><th>收货数</th><th>开始时间</th><th>窗口时间</th><th>计划转订单</th></tr></thead><tbody><tr>";
                    foreach (var c in currentOrders)
                    {
                        string sTime = c.StartTime != c.OrgStartTime ? c.StartTime.ToShortDateString() + "(" + c.OrgStartTime.ToShortDateString() + ")" : c.StartTime.ToShortDateString();
                        string sWime = c.WindowTime != c.OrgWindowTime ? c.WindowTime.ToShortDateString() + "(" + c.OrgWindowTime.ToShortDateString() + ")" : c.WindowTime.ToShortDateString();
                        showText += "<td>" + c.OrderNo + "</td><td>" + c.Item + "</td><td>" + c.OrderQty.ToString("0.##") + "</td><td>" + c.ShipQty.ToString("0.##") + "</td><td>" + c.RecQty.ToString("0.##") + "</td><td>" + sTime + "</td><td>" + sWime + "</td><td>" + c.TransferOrderFormat + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.OrderDets = showText;
            }
        }
        #endregion

        #region    在途
        IList<ShipPlanIpDet> ipDets = TheGenericMgr.FindAllWithCustomQuery<ShipPlanIpDet>(" select s from  ShipPlanIpDet as s where s.ShipPlanId=? and s.Type=? ",new object[]{ shipPlanDetList.First().ShipPlanId,this.rbType.SelectedValue});
        ipDets = ipDets == null ? new List<ShipPlanIpDet>() : ipDets;
        if (ipDets != null && ipDets.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
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

        var planByDateIndexs = shipPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByFlowItems = shipPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item, p.LocFrom, p.LocTo });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>提前期</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th><th rowspan='2'>包装量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th><th rowspan='2'>3PL期初</th><th rowspan='2'>在途</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='5'>");
            if (shipPlanDetList.First().Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT )
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
            widths = "290%";
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
            var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.ShipQty));
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
            str.Append(firstPlan.MrpLeadTime.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.RefItemCode);
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
            var InitStockQty = firstPlan.InitStock;
            var checkedColor = InitStockQty + firstPlan.InTransitQty;
            string colorStr = "";
            if (checkedColor < firstPlan.SafeStock)
            {
                colorStr = "<td style='background:red;color:white'>";
            }
            else if (checkedColor >= firstPlan.SafeStock && checkedColor <= firstPlan.MaxStock)
            {
                colorStr = "<td style='background:green;color:white' >";
            }
            else if (checkedColor > firstPlan.MaxStock)
            {
                colorStr = "<td style='background:orange;'>";
            }
            str.Append(colorStr);
            str.Append((InitStockQty).ToString("0.##"));
            str.Append("</td>");
            //str.Append("<td>");
            if (checkedColor < firstPlan.SafeStock)
            {
                str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:red;color:white' >", firstPlan.IpDets));
            }
            else if (checkedColor >= firstPlan.SafeStock && checkedColor <= firstPlan.MaxStock)
            {
                str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:green;color:white' >", firstPlan.IpDets));
            }
            else if (checkedColor > firstPlan.MaxStock)
            {
                str.Append(string.Format("<td tital='{0}'  onclick='doShowIpdets(this)' style='background:orange' >", firstPlan.IpDets));
            }
            str.Append((firstPlan.InTransitQty).ToString("0.##"));
            str.Append("</td>");
           // InitStockQty = InitStockQty + firstPlan.InTransitQty;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                //str.Append("<th >需求数</th><th >发货数</th><th >期末</th>");
                //var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;
                var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", shipPlanDet.Logs));
                str.Append(shipPlanDet.ReqQty.ToString("0.##"));
                str.Append("</td>");
                str.Append(string.Format("<td tital='{0}'  onclick='doShowDetsClick(this)'>", shipPlanDet.OrderDets));
                str.Append(shipPlanDet.OrderQty.ToString("0.##"));
                str.Append("</td>");
                if (firstPlan.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    seq++;
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.Flow + "' item='" + firstPlan.Item + "'  name='UpQty' id='" + shipPlanDet.Id + "'value='" + shipPlanDet.ShipQty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "' style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' />");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td>");
                    //if (planByDateIndex.Key.Date == System.DateTime.Now.Date)
                    //{
                    str.Append("<input type='checkbox' id='CheckBoxGroup' name='D" + planByDateIndex.Key.ToString("yyyyMMdd") + "' value='" + shipPlanDet.Id + "' runat='' onclick='doCheckClick(this)' />");
                    //}
                    str.Append(shipPlanDet.ShipQty.ToString("0.##"));
                    str.Append("</td>");
                }
                var ipQty = ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item  && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                var orderQtySum = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                var shipQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.ShipQty);
                var reqQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime<= planByDateIndex.Key).Sum(i => i.ReqQty) : 0;

                InitStockQty = firstPlan.InitStock + ipQty + orderQtySum + shipQtySum - reqQtySum;

                var inTransitQty = firstPlan.InTransitQty;


                var ipQty2 = ipDets.Where(i => i.Item == firstPlan.Item && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item  && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                var orderQtySum2 = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                var shipQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Sum(i => i.ShipQty) : 0;
                //var reqQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.StartTime <= planByDateIndex.Key).Sum(i => i.ReqQty);

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
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    private void WeeklyListTable(IList<ShipPlanDet> shipPlanDetList)
    {
        if (shipPlanDetList == null || shipPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        //var minStartTime = shipPlanDetList.Min(s => s.StartTime).AddDays(14);
        //shipPlanDetList = shipPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        List<ShipPlanDetTrace> traceList = new List<ShipPlanDetTrace>();
        int len = 0;
        int j = shipPlanDetList.Count % 200 == 0 ? shipPlanDetList.Count / 200 : shipPlanDetList.Count / 200 + 1;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Skip(len * 200).Take((len + 1) * 200).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        // traceList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Select(d => d.UUID).Distinct().ToArray())));

        if (traceList != null && traceList.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).OrderBy(d => d.ReqDate).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count > 0)
                {
                    showText = "<table><thead><tr><th>销售路线</th><th>物料</th><th>需求日期</th><th>需求数</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.DistributionFlow + "</td><td>" + c.Item + "</td><td>" + c.ReqDate.ToShortDateString() + "</td><td>" + c.ReqQty.ToString("0.##") + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }
        #endregion

        #region  orderQty
        List<ShipPlanOpenOrder> shipPlanOpenOrderList = new List<ShipPlanOpenOrder>();
        len = 0;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanOpenOrder>(string.Format(" select l from ShipPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Skip(len * 200).Take((len + 1) * 200).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { shipPlanOpenOrderList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        //shipPlanOpenOrderList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanOpenOrder>(string.Format(" select l from ShipPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        //shipPlanOpenOrderList = shipPlanOpenOrderList == null ? new List<ShipPlanOpenOrder>() : shipPlanOpenOrderList;    
        if (shipPlanOpenOrderList != null && shipPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
            {
                var currentOrders = shipPlanOpenOrderList.Where(d => d.UUID == sd.UUID).OrderBy(d => d.WindowTime).ToList();
                var showText = string.Empty;
                if (currentOrders != null && currentOrders.Count > 0)
                {
                    showText = "<table><thead><tr><th>订单号</th><th>物料</th><th>订单数</th><th>发货数</th><th>收货数</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentOrders)
                    {
                        string sTime = c.StartTime != c.OrgStartTime ? c.StartTime.ToShortDateString() + "(" + c.OrgStartTime.ToShortDateString() + ")" : c.StartTime.ToShortDateString();
                        string sWime = c.WindowTime != c.OrgWindowTime ? c.WindowTime.ToShortDateString() + "(" + c.OrgWindowTime.ToShortDateString() + ")" : c.WindowTime.ToShortDateString();
                        showText += "<td>" + c.OrderNo + "</td><td>" + c.Item + "</td><td>" + c.OrderQty.ToString("0.##") + "</td><td>" + c.ShipQty.ToString("0.##") + "</td><td>" + c.RecQty.ToString("0.##") + "</td><td>" + sTime + "</td><td>" + sWime + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.OrderDets = showText;
            }
        }
        #endregion


        var planByDateIndexs = shipPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByFlowItems = shipPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item, p.LocFrom, p.LocTo });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
       
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>提前期</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th><th rowspan='2'>包装量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
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
            str.Append("<th >需求数</th><th >订单数</th><th >发货数</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 14)
        {
            widths = "350%";
        }
        else if (ii > 10)
        {
            widths = "200%";
        }
        else if (ii > 6)
        {
            widths = "250%";
        }
        else if (ii > 4)
        {
            widths = "130%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.ShipQty));
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
            str.Append(firstPlan.MrpLeadTime.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.RefItemCode);
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
                //            str.Append("<th >需求数</th><th >订单数</th><th >发货数</th>");

                var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", shipPlanDet.Logs));
                str.Append(shipPlanDet.ReqQty.ToString("0.##"));
                str.Append("</td>");
                str.Append(string.Format("<td tital='{0}'  onclick='doShowDetsClick(this)'>", shipPlanDet.OrderDets));
                str.Append(shipPlanDet.OrderQty.ToString("0.##"));
                str.Append("</td>");
                if (firstPlan.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    seq++;
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.Flow + "' item='" + firstPlan.Item + "'  name='UpQty' id='" + shipPlanDet.Id + "'value='" + shipPlanDet.ShipQty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "' style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' />");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td>");
                    str.Append(shipPlanDet.ShipQty.ToString("0.##"));
                    str.Append("</td>");
                }
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }
    #endregion

    protected void btnRunProdPlan_Click(object sender, EventArgs e)
    {
        try
        {
            TheMrpMgr.RunProductionPlan(this.CurrentUser);
            ShowSuccessMessage("生成成功。");
        }
        catch (SqlException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (Exception ee)
        {
            ShowErrorMessage(ee.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //}
        //foreach (Control c in this.list.Controls)
        //{
        //    if (c is TextBox)
        //    {
        //        TextBox currentBox = (TextBox)c;
        //    }
        //}

        //str.Append("<input  type='text' flow='" + firstPlan.Flow + "' item='" + firstPlan.Item + "'  name='UpQty' id='" + shipPlanDet.Id + "'value='" + shipPlanDet.ShipQty.ToString("0.##") + "' style='width:70px' />");
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
            TheMrpMgr.UpdateShipPlanQty(flowList, itemList, idList, shipQtyList, releaseNoList, dateFromList, this.CurrentUser,this.rbType.SelectedValue);
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @" select det.Flow,det.Item,det.ItemDesc,det.RefItemCode,det.LocFrom,det.LocTo,det.WindowTime,det.Version,isnull(det.ShipQty,0),isnull(det.OrgShipQty,0),m.ReleaseNo,m.Status,m.LastModifyDate,m.LastModifyUser,det.Id,isnull(det.ReqQty,0),isnull(l.InitStock,0),isnull(l.SafeStock,0), isnull(l.InTransitQty,0),det.UUID ,det.StartTime,isnull(det.OrderQty,0),isnull(l.MaxStock,0) ,det.uc,isnull(f.MrpLeadTime,0),m.Id
from  MRP_ShipPlanDet as det 
 inner join MRP_ShipPlanMstr as m on det.ShipPlanId=m.Id 
 inner join FlowMstr as f on det.Flow=f.Code
 left join MRP_ShipPlanInitLocationDet as l on det.ShipPlanId=l.ShipPlanId and det.Item=l.Item and det.LocTo=l.Location where 1=1  ";
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

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " order by det.Flow,det.Item asc ";

        var flowCodes = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var shipPlanDetList = new List<ShipPlanDet>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            shipPlanDetList.Add(new ShipPlanDet
            {
                Flow = row[0].ToString(),
                Item = row[1].ToString(),
                ItemDesc = row[2].ToString(),
                RefItemCode = row[3].ToString(),
                LocFrom = row[4].ToString(),
                LocTo = row[5].ToString(),
                WindowTime = Convert.ToDateTime(row[6]),
                Version = Convert.ToInt32(row[7]),
                ShipQty = Convert.ToDecimal(row[8]),
                OrgShipQty = Convert.ToDecimal(row[9]),
                ReleaseNo = Convert.ToInt32(row[10]),
                Status = row[11].ToString(),
                Id = Convert.ToInt32(row[14].ToString()),
                ReqQty = Convert.ToDecimal(row[15]),
                InitStock = Convert.ToDecimal(row[16]),
                SafeStock = Convert.ToDecimal(row[17]),
                InTransitQty = Convert.ToDecimal(row[18]),
                UUID = row[19].ToString(),
                StartTime = Convert.ToDateTime(row[20]),
                OrderQty = Convert.ToDecimal(row[21]),
                MaxStock = Convert.ToDecimal(row[22]),
                UnitCount = Convert.ToDecimal(row[23]),
                MrpLeadTime = Convert.ToDecimal(row[24]),
                ShipPlanId = Convert.ToInt32(row[25]),
            });
        }
        if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            //IList<object> data = new List<object>();
            //data.Add(shipPlanDetList);
            //TheReportMgr.WriteToClient("ShipDayPlan.xls", data, "ShipDayPlan.xls");
            var minStartTime = shipPlanDetList.Min(s => s.StartTime).AddDays(13);
            shipPlanDetList = shipPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();
            #region   trace
            List<ShipPlanDetTrace> traceList = new List<ShipPlanDetTrace>();
            int len = 0;
            int j = shipPlanDetList.Count % 2000 == 0 ? shipPlanDetList.Count / 2000 : shipPlanDetList.Count / 2000 + 1;
            while (true)
            {
                var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
                if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
                len++;
                if (len == j) break;
            }
            #endregion

            #region  orderQty
            List<ShipPlanOpenOrder> shipPlanOpenOrderList = new List<ShipPlanOpenOrder>();
            len = 0;
            while (true)
            {
                var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanOpenOrder>(string.Format(" select l from ShipPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", shipPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
                if (cList != null && cList.Count > 0) { shipPlanOpenOrderList.AddRange(cList); }
                len++;
                if (len == j) break;
            }
            #endregion

            #region    在途
            IList<ShipPlanIpDet> ipDets = TheGenericMgr.FindAllWithCustomQuery<ShipPlanIpDet>(" select s from  ShipPlanIpDet as s where s.ShipPlanId=? and s.Type=? ", new object[] { shipPlanDetList.First().ShipPlanId, this.rbType.SelectedValue });
            ipDets = ipDets == null ? new List<ShipPlanIpDet>() : ipDets;
            #endregion
            //ExportDailyExcel(shipPlanDetList);
            IList<object> data = new List<object>();
            data.Add(shipPlanDetList);
            data.Add(traceList);
            data.Add(shipPlanOpenOrderList);
            data.Add(ipDets);
            TheReportMgr.WriteToClient("ShipPlanDaily.xls", data, "ShipPlanDaily.xls");
        }
        else
        {
            ExportWeeklyExcel(shipPlanDetList);
        }

    }

    private void ExportDailyExcel(IList<ShipPlanDet> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            #region   trace
            List<ShipPlanDetTrace> traceList = new List<ShipPlanDetTrace>();
            int len = 0;
            int j = exportList.Count % 2000 == 0 ? exportList.Count / 2000 : exportList.Count / 2000 + 1;
            while (true)
            {
                var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDetTrace>(string.Format(" select l from ShipPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", exportList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
                if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
                len++;
                if (len == j) break;
            }
            #endregion

            #region  orderQty
            List<ShipPlanOpenOrder> shipPlanOpenOrderList = new List<ShipPlanOpenOrder>();
            len = 0;
            while (true)
            {
                var cList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanOpenOrder>(string.Format(" select l from ShipPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", exportList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
                if (cList != null && cList.Count > 0) { shipPlanOpenOrderList.AddRange(cList); }
                len++;
                if (len == j) break;
            }
            #endregion

            #region    在途
            IList<ShipPlanIpDet> ipDets = TheGenericMgr.FindAllWithCustomQuery<ShipPlanIpDet>(" select s from  ShipPlanIpDet as s where s.ShipPlanId=? and s.Type=? ", new object[] { exportList.First().ShipPlanId, this.rbType.SelectedValue });
            ipDets = ipDets == null ? new List<ShipPlanIpDet>() : ipDets;
            #endregion

            var planByDateIndexs = exportList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
            var planByFlowItems = exportList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item, p.LocFrom, p.LocTo });
            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
            Row rowHeader2 = sheet1.CreateRow(1);
            for (int i = 0; i < 9 + planByDateIndexs.Count()*5; i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)  //路线
                {
                    rowHeader.CreateCell(i).SetCellValue("路线");
                }
                else if (i == 2) //提前期
                {
                    rowHeader.CreateCell(i).SetCellValue("提前期");
                }
                else if (i == 3)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 4)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 5)      //客户零件号
                {
                    rowHeader.CreateCell(i).SetCellValue("客户零件号");
                }
                else if (i == 6)      //包装量
                {
                    rowHeader.CreateCell(i).SetCellValue("包装量");
                }
                else if (i == 7)      //安全库存
                {
                    rowHeader.CreateCell(i).SetCellValue("安全库存");
                }
                else if (i == 8)      //最大库存
                {
                    rowHeader.CreateCell(i).SetCellValue("最大库存");
                }
                else if (i == 5)      //3PL期初
                {
                    rowHeader.CreateCell(i).SetCellValue("3PL期初");
                }
                else if (i == 9)      //在途
                {
                    rowHeader.CreateCell(i).SetCellValue("在途");
                }
                else
                {
                    foreach (var date in planByDateIndexs)
                    {
                        rowHeader.CreateCell(i).SetCellValue(date.Key.ToShortDateString());
                        //<th >需求数</th><th >订单数</th><th >发货数</th><th >期末</th><th >在途期末</th>
                        int i2 = i;
                        rowHeader2.CreateCell(i2++).SetCellValue("需求数");
                        rowHeader2.CreateCell(i2++).SetCellValue("订单数");
                        rowHeader2.CreateCell(i2++).SetCellValue("发货数");
                        rowHeader2.CreateCell(i2++).SetCellValue("期末");
                        rowHeader2.CreateCell(i2++).SetCellValue("在途期末");
                        i += 5;
                    }
                }
            }
            #endregion

            #region 写入数值
            int l = 0;
            int rowIndex = 2;
            int seq = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.ShipQty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Flow);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MrpLeadTime.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.RefItemCode);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.InitStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.InTransitQty.ToString("0.##"));
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                    var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                    var createCell = rowDetail.CreateCell(cell++);
                    createCell.SetCellType(CellType.NUMERIC);
                    createCell.SetCellValue(Convert.ToDouble(shipPlanDet.ReqQty));

                    var createCell2 = rowDetail.CreateCell(cell++);
                    createCell2.SetCellType(CellType.NUMERIC);
                    createCell2.SetCellValue(Convert.ToDouble(shipPlanDet.OrderQty));

                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(shipPlanDet.ShipQty));

                    var ipQty = ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                    var orderQtySum = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                    var shipQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.ShipQty);
                    var reqQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Sum(i => i.ReqQty) : 0;

                    var initStockQty = firstPlan.InitStock + ipQty + orderQtySum + shipQtySum - reqQtySum;
                    var createInitStockQty = rowDetail.CreateCell(cell++);
                    createInitStockQty.SetCellType(CellType.NUMERIC);
                    createInitStockQty.SetCellValue(Convert.ToDouble(initStockQty));

                    var inTransitQty = firstPlan.InTransitQty;
                    var ipQty2 = ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                    var orderQtySum2 = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.WindowTime > planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                    var shipQtySum2 = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) > planByDateIndex.Key).Sum(i => i.ShipQty) : 0;
                    inTransitQty = inTransitQty - ipQty2 + orderQtySum2 + shipQtySum2;

                    var creatIinTransitQty = rowDetail.CreateCell(cell++);
                    creatIinTransitQty.SetCellType(CellType.NUMERIC);
                    creatIinTransitQty.SetCellValue(Convert.ToDouble(inTransitQty));

                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "ShipPlan.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }

    private void ExportWeeklyExcel(IList<ShipPlanDet> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var planByDateIndexs = exportList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
            var planByFlowItems = exportList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item, p.LocFrom, p.LocTo });
            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
            Row rowHeader2 = sheet1.CreateRow(1);
           // str.Append("<th rowspan='2'>包装量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
            for (int i = 0; i < 9 + planByDateIndexs.Count() * 3; i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)  //路线
                {
                    rowHeader.CreateCell(i).SetCellValue("路线");
                }
                else if (i == 2) //提前期
                {
                    rowHeader.CreateCell(i).SetCellValue("提前期");
                }
                else if (i == 3)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 4)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 5)      //客户零件号
                {
                    rowHeader.CreateCell(i).SetCellValue("客户零件号");
                }
                else if (i == 6)      //包装量
                {
                    rowHeader.CreateCell(i).SetCellValue("包装量");
                }
                else if (i == 7)      //安全库存
                {
                    rowHeader.CreateCell(i).SetCellValue("安全库存");
                }
                else if (i == 8)      //最大库存
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
                        rowHeader2.CreateCell(i2++).SetCellValue("发货数");
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
                var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.ShipQty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Flow);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MrpLeadTime.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.RefItemCode);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                    var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                    var createCell = rowDetail.CreateCell(cell++);
                    createCell.SetCellType(CellType.NUMERIC);
                    createCell.SetCellValue(Convert.ToDouble(shipPlanDet.ReqQty));

                    var createCell2 = rowDetail.CreateCell(cell++);
                    createCell2.SetCellType(CellType.NUMERIC);
                    createCell2.SetCellValue(Convert.ToDouble(shipPlanDet.OrderQty));

                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(shipPlanDet.ShipQty));
                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "ShipPlanWeekly.xls";
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
                var shipPlanMstr = TheGenericMgr.FindAllWithCustomQuery<ShipPlanMstr>(" select s from ShipPlanMstr as s where s.ReleaseNo=? ",currentRelesNo).First();
                if (shipPlanMstr.Status == BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT)
                {
                    throw new BusinessErrorException("已释放的发运计划不能导入。");
                }
                TheMrpMgr.ReadShipPlanFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, shipPlanMstr);
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
                TheMrpMgr.CreateOrderByShipPlan(ids.Substring(0, ids.Length - 1), this.CurrentUser);
                ShowSuccessMessage("发运计划生成订单成功。");
                this.btnSearch_Click(null, null);
            }
            else
            {
                throw new BusinessErrorException("请选择要转订单明细。");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (Exception et)
        {
            ShowErrorMessage(et.Message);
        }
       

    }

}