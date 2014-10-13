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
using NPOI.SS.UserModel;
using System.IO;

public partial class NewMrp_ProductionPlan_DetailList : MainModuleBase
{
    public static string currentRelesNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:false,bool:true,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

        //if (!IsPostBack)
        //{
        //    this.tbFlow.Text = string.Empty;
        //}

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
        //this.tbFlow.Text = string.Empty;
        this.list.InnerHtml = "";
        currentRelesNo = relesNo;
        var shipPlanMstr = TheGenericMgr.FindAllWithCustomQuery<ProductionPlanMstr>(" select s from ProductionPlanMstr as s where s.ReleaseNo=? ", currentRelesNo).First();
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
        var searchSql = @"  select m.Id,m.ReleaseNo,det.Id,det.Item,det.itemDesc,det.RefItemCode,isnull(det.OrgQty,0),isnull(det.Qty,0),det.Uom,det.StartTime,det.WindowTime,det.UUID,isnull(det.OrderQty,0),isnull(l.initStock,0),isnull(l.SafeStock,0),isnull(l.MaxStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.ReqQty,0),isnull(det.UC,0),isnull(MinLotSize,0),isnull(m.Status,'Submit'),l.InventoryCountDown
 from  dbo.MRP_ProductionPlanDet as det with(nolock) inner join MRP_ProductionPlanMstr as m with(nolock) on det.ProductionPlanId=m.Id
inner join MRP_ProductionPlanInitLocationDet as l with(nolock) on det.ProductionPlanId=l.ProductionPlanId and det.Item=l.Item  where 1=1  ";

        //if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        //{
        //    searchSql += string.Format(" and det.Flow ='{0}' ", this.tbFlow.Text.Trim());
        //}
        //else
        //{
        //    this.list.InnerHtml = "";
        //    ShowErrorMessage("发运路线不能为空。");
        //    return;
        //}

        searchSql += string.Format(" and det.Type='{0}' ", this.rbType.SelectedValue);
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " order by det.Item asc ";



        var allResult = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var productionPlanDetList = new List<ProductionPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.Id,m.ReleaseNo,det.Id,det.Item,det.itemDesc,det.RefItemCode,isnull(det.OrgQty,0),isnull(det.Qty,0),
            //det.Uom,det.StartTime,det.WindowTime,det.UUID,isnull(det.OrderQty,0),isnull(l.initStock,0),
            //isnull(l.SafeStock,0),isnull(l.MaxStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0)
            productionPlanDetList.Add(new ProductionPlanDet
            {
                ProductionPlanId = Int32.Parse(row[0].ToString()),
                ReleaseNo = Int32.Parse(row[1].ToString()),
                Id = Int32.Parse(row[2].ToString()),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                OrgQty = Math.Round(Convert.ToDecimal(row[6])),
                Qty = Math.Round(Convert.ToDecimal(row[7])),
                Uom = row[8].ToString(),
                StartTime = Convert.ToDateTime(row[9]),
                WindowTime = Convert.ToDateTime(row[10]),
                UUID = row[11].ToString(),
                OrderQty = Math.Round(Convert.ToDecimal(row[12])),
                InitStock = Math.Round(Convert.ToDecimal(row[13])),
                SafeStock = Math.Round(Convert.ToDecimal(row[14])),
                MaxStock = Math.Round(Convert.ToDecimal(row[15])),
                InTransitQty = Math.Round(Convert.ToDecimal(row[16])),
                InspectQty = Math.Round(Convert.ToDecimal(row[17])),
                ReqQty = Math.Round(Convert.ToDecimal(row[18])),
                UnitCount = Math.Round(Convert.ToDecimal(row[19])),
                MinLotSize = Math.Round(Convert.ToDecimal(row[20])),
                Status =  row[21].ToString(),
                InventoryCountDown = !string.IsNullOrEmpty(row[22].ToString())? (decimal?)row[22] : null,
            });
        }
        if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            ListTable(productionPlanDetList);
        }
        else
        {
            WeeklyListTable(productionPlanDetList);
        }
    }

    private void ListTable(IList<ProductionPlanDet> productionPlanDetList)
    {
        if (productionPlanDetList == null || productionPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        var minStartTime = productionPlanDetList.Min(s => s.StartTime).AddDays(13);
        productionPlanDetList = productionPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        List<ProductionPlanDetTrace> traceList = new List<ProductionPlanDetTrace>();
        int len = 0;
        int j = productionPlanDetList.Count % 2000 == 0 ? productionPlanDetList.Count / 2000 : productionPlanDetList.Count / 2000 + 1;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanDetTrace>(string.Format(" select l from ProductionPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", productionPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        traceList = traceList == null ? new List<ProductionPlanDetTrace>() : traceList;
        if (traceList != null && traceList.Count > 0)
        {
            foreach (var sd in productionPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).OrderBy(d => d.ReqDate).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count > 0)
                {
                    showText = "<table><thead><tr><th>发运路线</th><th>物料</th><th>Bom</th><th>需求日期</th><th>需求数</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.Flow + "</td><td>" + c.Item + "</td><td>" + c.Bom + "</td><td>" + c.ReqDate.ToShortDateString() + "</td><td>" + c.ReqQty.ToString("0.##") + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }
        #endregion

        #region  orderQty
        List<ProductionPlanOpenOrder> productionPlanOpenOrderList = new List<ProductionPlanOpenOrder>();
        len = 0;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanOpenOrder>(string.Format(" select l from ProductionPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", productionPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { productionPlanOpenOrderList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        // productionPlanOpenOrderList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanOpenOrder>(string.Format(" select l from ProductionPlanOpenOrder as l where l.Type='{0}' and l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", productionPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        productionPlanOpenOrderList = productionPlanOpenOrderList == null ? new List<ProductionPlanOpenOrder>() : productionPlanOpenOrderList;
        if (productionPlanOpenOrderList != null && productionPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in productionPlanDetList)
            {
                var currentOrders = productionPlanOpenOrderList.Where(d => d.UUID == sd.UUID).OrderBy(d=>d.WindowTime).ToList();
                var showText = string.Empty;
                if (currentOrders != null && currentOrders.Count > 0)
                {
                    showText = "<table><thead><tr><th>路线</th><th>订单号</th><th>物料</th><th>订单数</th><th>收货数</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentOrders)
                    {
                        showText += "<td>" + c.Flow + "</td><td>" + c.OrderNo + "</td><td>" + c.Item + "</td><td>" + c.OrderQty.ToString("0.##") + "</td><td>" + c.RecQty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.OrderDets = showText;
            }
        }
        #endregion

        var planByDateIndexs = productionPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByItems = productionPlanDetList.GroupBy(p => p.Item);

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th><th rowspan='2'>包装量</th><th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th><th rowspan='2'>期初库存</th><th rowspan='2'>报验</th><th rowspan='2'>库存倒数</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='4'>");
            //if (productionPlanDetList.First().Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT && planByDateIndex.Key.Date == System.DateTime.Now.Date)
            //{
            //    str.Append("<input type='checkbox' id='CheckAll' name='CheckAll'  onclick='doCheckAllClick()' />");
            //}
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th >需求</th><th >订单数</th><th >计划数</th><th >期末</th>");
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
            widths = "120%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByItem in planByItems)
        {
            var firstPlan = planByItem.First();
            var planDic = planByItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
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
            str.Append(planByItem.Key);
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
            str.Append(firstPlan.MinLotSize.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.MaxStock.ToString("0.##"));
            str.Append("</td>");
            var InitStockQty = firstPlan.InitStock + firstPlan.InspectQty;
            if (InitStockQty < firstPlan.SafeStock)
            {
                str.Append("<td style='background:red;color:white'>");
            }
            else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
            {
                str.Append("<td style='background:green;color:white'>");
            }
            else if (InitStockQty > firstPlan.MaxStock)
            {
                str.Append("<td style='background:orange'>");
            }
            str.Append((firstPlan.InitStock).ToString("0.##"));
            str.Append("</td>");
            //str.Append("<td>");
            //str.Append((firstPlan.InTransitQty).ToString("0.##"));
            //str.Append("</td>");
            if (InitStockQty < firstPlan.SafeStock)
            {
                str.Append("<td style='background:red;color:white'>");
            }
            else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
            {
                str.Append("<td style='background:green;color:white'>");
            }
            else if (InitStockQty > firstPlan.MaxStock)
            {
                str.Append("<td style='background:orange'>");
            }
            str.Append((firstPlan.InspectQty).ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.InventoryCountDown.HasValue?firstPlan.InventoryCountDown.Value.ToString("0.##"):"");
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var curenPlan = planByItem.Where(p => p.StartTime == planByDateIndex.Key);
                var pdPlan = curenPlan.Count() > 0 ? curenPlan.First() : new ProductionPlanDet();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", pdPlan.Logs));
                str.Append(pdPlan.ReqQty.ToString("0.##"));
                str.Append("</td>");
                str.Append(string.Format("<td tital='{0}'  onclick='doShowDetsClick(this)'>", pdPlan.OrderDets));
                str.Append(pdPlan.OrderQty.ToString("0.##"));
                str.Append("</td>");

                //str.Append("<td>");
                //str.Append(pdPlan.Qty.ToString("0.##"));
                //str.Append("</td>");

                if (firstPlan.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    seq++;
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text'  item='" + firstPlan.Item + "'  name='UpQty' id='" + pdPlan.Id + "'value='" + pdPlan.Qty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "' style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' />");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td>");
                    //if (planByDateIndex.Key.Date == System.DateTime.Now.Date)
                    //{
                    //    str.Append("<input type='checkbox' id='CheckBoxGroup' name='CheckBoxGroup' value='" + pdPlan.Id + "' runat='' onclick='doCheckClick()' />");
                    //}
                    str.Append(pdPlan.Qty.ToString("0.##"));
                    str.Append("</td>");
                }


                InitStockQty = InitStockQty + pdPlan.Qty-pdPlan.ReqQty+pdPlan.OrderQty;
                if (InitStockQty < firstPlan.SafeStock)
                {
                    str.Append("<td style='background:red;color:white'>");
                }
                else if (InitStockQty >= firstPlan.SafeStock && InitStockQty <= firstPlan.MaxStock)
                {
                    str.Append("<td style='background:green;color:white'>");
                }
                else if (InitStockQty > firstPlan.MaxStock)
                {
                    str.Append("<td style='background:orange'>");
                }
                str.Append(InitStockQty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    private void WeeklyListTable(IList<ProductionPlanDet> productionPlanDetList)
    {
        if (productionPlanDetList == null || productionPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        //var minStartTime = productionPlanDetList.Min(s => s.StartTime).AddDays(14);
        //productionPlanDetList = productionPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        List<ProductionPlanDetTrace> traceList = new List<ProductionPlanDetTrace>();
        int len = 0;
        int j = productionPlanDetList.Count % 2000 == 0 ? productionPlanDetList.Count / 2000 : productionPlanDetList.Count / 2000 + 1;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanDetTrace>(string.Format(" select l from ProductionPlanDetTrace as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", productionPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { traceList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        traceList = traceList == null ? new List<ProductionPlanDetTrace>() : traceList;
        if (traceList != null && traceList.Count > 0)
        {
            foreach (var sd in productionPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).OrderBy(d => d.ReqDate).ToList();
                var showText = string.Empty;
                if (currentLogs != null && currentLogs.Count > 0)
                {
                    showText = "<table><thead><tr><th>发运路线</th><th>物料</th><th>Bom</th><th>需求日期</th><th>需求数</th></tr></thead><tbody><tr>";
                    foreach (var c in currentLogs)
                    {
                        showText += "<td>" + c.Flow + "</td><td>" + c.Item + "</td><td>" + c.Bom + "</td><td>" + c.ReqDate.ToShortDateString() + "</td><td>" + c.ReqQty.ToString("0.##") + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.Logs = showText;
            }
        }
        #endregion

        #region  orderQty
        List<ProductionPlanOpenOrder> productionPlanOpenOrderList = new List<ProductionPlanOpenOrder>();
        len = 0;
        while (true)
        {
            var cList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanOpenOrder>(string.Format(" select l from ProductionPlanOpenOrder as l where l.Type='{0}' and  l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", productionPlanDetList.Skip(len * 2000).Take((len + 1) * 2000).Select(d => d.UUID).Distinct().ToArray())));
            if (cList != null && cList.Count > 0) { productionPlanOpenOrderList.AddRange(cList); }
            len++;
            if (len == j) break;
        }
        // productionPlanOpenOrderList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanOpenOrder>(string.Format(" select l from ProductionPlanOpenOrder as l where l.Type='{0}' and l.UUID in ('{1}') ", this.rbType.SelectedValue, string.Join("','", productionPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        productionPlanOpenOrderList = productionPlanOpenOrderList == null ? new List<ProductionPlanOpenOrder>() : productionPlanOpenOrderList;
        if (productionPlanOpenOrderList != null && productionPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in productionPlanDetList)
            {
                var currentOrders = productionPlanOpenOrderList.Where(d => d.UUID == sd.UUID).OrderBy(d => d.WindowTime).ToList();
                var showText = string.Empty;
                if (currentOrders != null && currentOrders.Count > 0)
                {
                    showText = "<table><thead><tr><th>路线</th><th>订单号</th><th>物料</th><th>订单数</th><th>收货数</th><th>开始时间</th><th>窗口时间</th></tr></thead><tbody><tr>";
                    foreach (var c in currentOrders)
                    {
                        showText += "<td>" + c.Flow + "</td><td>" + c.OrderNo + "</td><td>" + c.Item + "</td><td>" + c.OrderQty.ToString("0.##") + "</td><td>" + c.RecQty.ToString("0.##") + "</td><td>" + c.StartTime.ToShortDateString() + "</td><td>" + c.WindowTime.ToShortDateString() + "</td></tr><tr>";
                    }
                    showText += " </tr></tbody></table> ";
                }
                sd.OrderDets = showText;
            }
        }
        #endregion

        var planByDateIndexs = productionPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByItems = productionPlanDetList.GroupBy(p => p.Item);

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th><th rowspan='2'>包装量</th><th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
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
            str.Append("<th >需求</th><th >订单数</th><th >计划数</th>");
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
            widths = "120%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByItem in planByItems)
        {
            var firstPlan = planByItem.First();
            var planDic = planByItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
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
            str.Append(planByItem.Key);
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
            str.Append(firstPlan.MinLotSize.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.MaxStock.ToString("0.##"));
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var curenPlan = planByItem.Where(p => p.StartTime == planByDateIndex.Key);
                var pdPlan = curenPlan.Count() > 0 ? curenPlan.First() : new ProductionPlanDet();
                str.Append(string.Format("<td tital='{0}'  onclick='doTdClick(this)'>", pdPlan.Logs));
                str.Append(pdPlan.ReqQty.ToString("0.##"));
                str.Append("</td>");
                str.Append(string.Format("<td tital='{0}'  onclick='doShowDetsClick(this)'>", pdPlan.OrderDets));
                str.Append(pdPlan.OrderQty.ToString("0.##"));
                str.Append("</td>");

                str.Append("<td>");
                str.Append(pdPlan.Qty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    #endregion

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
        var searchSql = @"  select m.Id,m.ReleaseNo,det.Id,det.Item,det.itemDesc,det.RefItemCode,isnull(det.OrgQty,0),isnull(det.Qty,0),det.Uom,det.StartTime,det.WindowTime,det.UUID,isnull(det.OrderQty,0),isnull(l.initStock,0),isnull(l.SafeStock,0),isnull(l.MaxStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.ReqQty,0),isnull(det.UC,0),isnull(MinLotSize,0),l.InventoryCountDown
 from  dbo.MRP_ProductionPlanDet as det with(nolock) inner join MRP_ProductionPlanMstr as m with(nolock) on det.ProductionPlanId=m.Id
inner join MRP_ProductionPlanInitLocationDet as l with(nolock) on det.ProductionPlanId=l.ProductionPlanId and det.Item=l.Item  where 1=1  ";

        searchSql += string.Format(" and det.Type='{0}' ", this.rbType.SelectedValue);
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " order by det.Item asc ";

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var productionPlanDetList = new List<ProductionPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            productionPlanDetList.Add(new ProductionPlanDet
            {
                ProductionPlanId = Int32.Parse(row[0].ToString()),
                ReleaseNo = Int32.Parse(row[1].ToString()),
                Id = Int32.Parse(row[2].ToString()),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                OrgQty = Math.Round(Convert.ToDecimal(row[6])),
                Qty = Math.Round(Convert.ToDecimal(row[7])),
                Uom = row[8].ToString(),
                StartTime = Convert.ToDateTime(row[9]),
                WindowTime = Convert.ToDateTime(row[10]),
                UUID = row[11].ToString(),
                OrderQty = Math.Round(Convert.ToDecimal(row[12])),
                InitStock = Math.Round(Convert.ToDecimal(row[13])),
                SafeStock = Math.Round(Convert.ToDecimal(row[14])),
                MaxStock = Math.Round(Convert.ToDecimal(row[15])),
                InTransitQty = Math.Round(Convert.ToDecimal(row[16])),
                InspectQty = Math.Round(Convert.ToDecimal(row[17])),
                ReqQty = Math.Round(Convert.ToDecimal(row[18])),
                UnitCount = Math.Round(Convert.ToDecimal(row[19])),
                MinLotSize = Math.Round(Convert.ToDecimal(row[20])),
                InventoryCountDown = (decimal?)row[21],
            });
        }
        if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        {
            var minStartTime = productionPlanDetList.Min(s => s.StartTime).AddDays(13);
            productionPlanDetList = productionPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();
            IList<object> data = new List<object>();
            data.Add(productionPlanDetList);
            TheReportMgr.WriteToClient("ProductionPlanDaily.xls", data, "ProductionPlanDaily.xls");
        }
        else
        {
            ExportWeeklyExcel(productionPlanDetList);
        }

    }

    private void ExportWeeklyExcel(IList<ProductionPlanDet> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var planByDateIndexs = exportList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
            var planByItems = exportList.GroupBy(p => p.Item);
            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
            Row rowHeader2 = sheet1.CreateRow(1);
            //<th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
            for (int i = 0; i < 8 + planByDateIndexs.Count() * 3; i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 2)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 3)      //客户零件号
                {
                    rowHeader.CreateCell(i).SetCellValue("客户零件号");
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
            foreach (var planByItem in planByItems)
            {
                var firstPlan = planByItem.First();
                var planDic = planByItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.RefItemCode);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MinLotSize.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var curenPlan = planByItem.Where(p => p.StartTime == planByDateIndex.Key);
                    var pdPlan = curenPlan.Count() > 0 ? curenPlan.First() : new ProductionPlanDet();
                    var createCell = rowDetail.CreateCell(cell++);
                    createCell.SetCellType(CellType.NUMERIC);
                    createCell.SetCellValue(Convert.ToDouble(pdPlan.ReqQty));

                    var createCell2 = rowDetail.CreateCell(cell++);
                    createCell2.SetCellType(CellType.NUMERIC);
                    createCell2.SetCellValue(Convert.ToDouble(pdPlan.OrderQty));

                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(pdPlan.Qty));
                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "ProductionPlanWeekly.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            var allSeqArr = string.IsNullOrEmpty(this.btSeqHidden.Value) ? new string[0] : this.btSeqHidden.Value.Split(',');
            var allShipQty = string.IsNullOrEmpty(this.btQtyHidden.Value) ? new string[0] : this.btQtyHidden.Value.Split(',');
            string allHtml = this.list.InnerHtml;
            IList<string> itemList = new List<string>();
            IList<string> qtyList = new List<string>();
            IList<string> idList = new List<string>();
            IList<string> releaseNoList = new List<string>();
            IList<string> dateFromList = new List<string>();

            string item = string.Empty;
            string qty = string.Empty;
            string id = string.Empty;
            string releaseNo = string.Empty;
            string dateFrom = string.Empty;
            string seq = string.Empty;
            while (allHtml.Length > 0)
            {
                //int startIndex = allHtml.IndexOf("flow='");
                //if (startIndex == -1) { allHtml = string.Empty; break; }
                //allHtml = allHtml.Substring(startIndex + 6);
                //int endIndex = allHtml.IndexOf("'");
                //flow = allHtml.Substring(0, endIndex);

                int startIndex = allHtml.IndexOf("item='");
                if (startIndex == -1) { allHtml = string.Empty; break; }
                allHtml = allHtml.Substring(startIndex + 6);
                int endIndex = allHtml.IndexOf("'");
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
                        //flowList.Add(flow);
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
            if (itemList.Count == 0)
            {
                ShowErrorMessage("没有要修改的计划。");
            }
            TheMrpMgr.UpdateProductionPlanQty( itemList, idList, shipQtyList, releaseNoList, dateFromList, this.CurrentUser, this.rbType.SelectedValue);
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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            this.btQtyHidden.Value = string.Empty;
            this.btSeqHidden.Value = string.Empty;
            if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
            {
                var productionPlanMstr = TheGenericMgr.FindAllWithCustomQuery<ProductionPlanMstr>(" select s from ProductionPlanMstr as s where s.ReleaseNo=? ", currentRelesNo).First();
                if (productionPlanMstr.Status == BusinessConstants.CODE_MASTER_BINDING_TYPE_VALUE_SUBMIT)
                {
                    throw new BusinessErrorException("已释放的生产计划不能导入。");
                }
                TheMrpMgr.ReadProductionPlanFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, productionPlanMstr);
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

    //protected void btnCreateOrder_Click(object sender, EventArgs e)
    //{
    //    string ids = this.btIds.Value;
    //    try
    //    {
    //        if (!string.IsNullOrEmpty(ids))
    //        {
    //            TheMrpMgr.CreateOrderByProductionPlan(ids.Substring(0, ids.Length - 1), this.CurrentUser);
    //            ShowSuccessMessage("生产计划生成订单成功。");
    //            this.btnSearch_Click(null, null);
    //        }
    //        else
    //        {
    //            throw new BusinessErrorException("请选择要转订单明细。");
    //        }
    //    }
    //    catch (BusinessErrorException ex)
    //    {
    //        ShowErrorMessage(ex.Message);
    //    }
    //    catch (Exception et)
    //    {
    //        ShowErrorMessage(et.Message);
    //    }


    //}


}