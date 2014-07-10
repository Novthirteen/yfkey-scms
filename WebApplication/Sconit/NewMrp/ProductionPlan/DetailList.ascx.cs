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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var searchSql = @"  select m.Id,m.ReleaseNo,det.Id,det.Item,det.itemDesc,det.RefItemCode,isnull(det.OrgQty,0),isnull(det.Qty,0),det.Uom,det.StartTime,det.WindowTime,det.UUID,isnull(det.OrderQty,0),isnull(l.initStock,0),isnull(l.SafeStock,0),isnull(l.MaxStock,0),isnull(l.InTransitQty,0),isnull(l.InspectQty,0),isnull(det.ReqQty,0),isnull(det.UC,0),isnull(MinLotSize,0)
 from  dbo.MRP_ProductionPlanDet as det inner join MRP_ProductionPlanMstr as m on det.ProductionPlanId=m.Id
inner join MRP_ProductionPlanInitLocationDet as l on det.ProductionPlanId=l.ProductionPlanId and det.Item=l.Item  where 1=1 ";

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
                OrgQty = Convert.ToDecimal(row[6]),
                Qty = Convert.ToDecimal(row[7]),
                Uom = row[8].ToString(),
                StartTime = Convert.ToDateTime(row[9]),
                WindowTime = Convert.ToDateTime(row[10]),
                UUID = row[11].ToString(),
                OrderQty = Convert.ToDecimal(row[12]),
                InitStock = Convert.ToDecimal(row[13]),
                SafeStock = Convert.ToDecimal(row[14]),
                MaxStock = Convert.ToDecimal(row[15]),
                InTransitQty = Convert.ToDecimal(row[16]),
                InspectQty = Convert.ToDecimal(row[17]),
                ReqQty = Convert.ToDecimal(row[18]),
                UnitCount = Convert.ToDecimal(row[19]),
                MinLotSize = Convert.ToDecimal(row[20]),
            });
        }

        ListTable(productionPlanDetList);
    }

    private void ListTable(IList<ProductionPlanDet> productionPlanDetList)
    {
        if (productionPlanDetList == null || productionPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        //var minStartTime = productionPlanDetList.Min(s => s.StartTime).AddDays(14);
        //productionPlanDetList = productionPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();

        #region   trace
        IList<ProductionPlanDetTrace> traceList = new List<ProductionPlanDetTrace>();
        traceList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanDetTrace>(string.Format(" select l from ProductionPlanDetTrace as l where l.UUID in ('{0}') ", string.Join("','", productionPlanDetList.Select(d => d.UUID).Distinct().ToArray())));

        if (traceList != null && traceList.Count > 0)
        {
            foreach (var sd in productionPlanDetList)
            {
                var currentLogs = traceList.Where(d => d.UUID == sd.UUID).ToList();
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
        IList<ProductionPlanOpenOrder> productionPlanOpenOrderList = new List<ProductionPlanOpenOrder>();
        productionPlanOpenOrderList = this.TheGenericMgr.FindAllWithCustomQuery<ProductionPlanOpenOrder>(string.Format(" select l from ProductionPlanOpenOrder as l where l.UUID in ('{0}') ", string.Join("','", productionPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        if (productionPlanOpenOrderList != null && productionPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in productionPlanDetList)
            {
                var currentOrders = productionPlanOpenOrderList.Where(d => d.UUID == sd.UUID).ToList();
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
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th><th rowspan='2'>包装量</th><th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th><th rowspan='2'>期初库存</th><th rowspan='2'>报验</th>");
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
            var InitStockQty = firstPlan.InitStock;
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
            str.Append((InitStockQty).ToString("0.##"));
            str.Append("</td>");
            //str.Append("<td>");
            //str.Append((firstPlan.InTransitQty).ToString("0.##"));
            //str.Append("</td>");
            str.Append("<td>");
            str.Append((firstPlan.InspectQty).ToString("0.##"));
            str.Append("</td>");
            InitStockQty = InitStockQty + firstPlan.InspectQty;
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

    #endregion



    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }
}