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
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;
        //bool includeProcurement, bool includeDistribution, bool includeTransfer, bool includeProduction, bool includeCustomerGoods, bool includeSubconctracting,
        if (!IsPostBack)
        {
            //this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
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
        //,ip.StartTime as ipStartTime,ip.WindowTime as ipWindowTime,isnull(ip.Qty,0)
        var searchSql = @" select det.Flow,det.Item,det.ItemDesc,det.RefItemCode,det.LocFrom,det.LocTo,det.WindowTime,det.Version,isnull(det.ShipQty,0),isnull(det.OrgShipQty,0),m.ReleaseNo,m.Status,m.LastModifyDate,m.LastModifyUser,det.Id,isnull(det.ReqQty,0),isnull(l.InitStock,0),isnull(l.SafeStock,0), isnull(l.InTransitQty,0),det.UUID ,det.StartTime,isnull(det.OrderQty,0),isnull(l.MaxStock,0) ,det.uc,isnull(f.MrpLeadTime,0),m.Id
from  MRP_ShipPlanDet as det 
 inner join MRP_ShipPlanMstr as m on det.ShipPlanId=m.Id 
 inner join FlowMstr as f on det.Flow=f.Code
 left join MRP_ShipPlanInitLocationDet as l on det.ShipPlanId=l.ShipPlanId and det.Item=l.Item and det.LocTo=l.Location where 1=1  ";
        //left join MRP_ShipPlanIpDet as ip on ip.ShipPlanId=m.Id and ip.Item=det.Item where 1=1  ";
        searchSql += string.Format(" and det.Type='{0}' ", this.rbType.SelectedValue);

        string flowCodeValues = this.tbFlow.Text.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", ",");
            flowCodeValues = flowCodeValues.Replace("\n", ",");
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

        searchSql += " order by det.Item asc ";

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

        #region    在途
        IList<ShipPlanIpDet> ipDets = TheGenericMgr.FindAllWithCustomQuery<ShipPlanIpDet>(" select s from  ShipPlanIpDet as s where s.ShipPlanId=? and s.Type=? ",new object[]{ shipPlanDetList.First().ShipPlanId,this.rbType.SelectedValue});
        ipDets = ipDets == null ? new List<ShipPlanIpDet>() : ipDets;
        if (ipDets != null && ipDets.Count > 0)
        {
            foreach (var sd in shipPlanDetList)
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
                    str.Append(shipPlanDet.ShipQty.ToString("0.##"));
                    str.Append("</td>");
                }
                var ipQty = ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
                var orderQtySum = shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? shipPlanOpenOrderList.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.OrderQty - i.ShipQty) : 0;
                var shipQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime.AddDays(Convert.ToDouble(firstPlan.MrpLeadTime)) <= planByDateIndex.Key).Sum(i => i.ShipQty);
                var reqQtySum = planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Count() > 0 ? planByFlowItem.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.StartTime <= planByDateIndex.Key).Sum(i => i.ReqQty) : 0;

                InitStockQty = firstPlan.InitStock + ipQty + orderQtySum + shipQtySum - reqQtySum;

                var inTransitQty = firstPlan.InTransitQty;


                var ipQty2 = ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Count() > 0 ? ipDets.Where(i => i.Item == firstPlan.Item && i.Flow == firstPlan.Flow && i.WindowTime <= planByDateIndex.Key).Sum(i => i.Qty) : 0;
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
}