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

public partial class NewMrp_WeeklyShipPlan_DetailList : MainModuleBase
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
        var searchSql = @"select det.Id,m.BatchNo,m.ReleaseNo,m.EffDate,m.Status,det.UUID,det.Flow,det.Item,det,ItemDesc,det.RefItemCode,isnull(det.ReqQty,0),isnull(det.PurchaseQty,0),isnull(det.OrderQty,0),det.Uom,det.StartTime,det.WindowTime 
           from MRP_WeeklyPurchasePlanDet as det inner join MRP_WeeklyPurchasePlanMstr as m on m.Id=det.PurchasePlanId where 1=1 ";
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
        var purchasePlanDetList = new List<WeeklyPurchasePlanDet>();
        foreach (System.Data.DataRow row in flowCodes.Rows)
        {
            //m.Id,m.BatchNo,m.ReleaseNo,m.EffDate,m.Status,det.UUID,det.Flow,det.Item,det,ItemDesc,det.RefItemCode,
            //isnull(det.ReqQty,0),isnull(det.PurchaseQty,0),isnull(det.OrderQty,0),det.Uom,det.StartTime,det.WindowTime
            purchasePlanDetList.Add(new WeeklyPurchasePlanDet
            {
                Id = Convert.ToInt32(row[0].ToString()),
                BatchNo = Convert.ToInt32(row[1].ToString()),
                ReleaseNo = Convert.ToInt32(row[2].ToString()),
                EffDate = Convert.ToDateTime(row[3].ToString()),
                Status = row[4].ToString(),
                UUID = row[5].ToString(),
                Flow = row[6].ToString(),
                Item = row[7].ToString(),
                ItemDesc =row[8].ToString(),
                RefItemCode =row[9].ToString(),
                ReqQty = Convert.ToDecimal(row[10]),
                PurchaseQty = Convert.ToDecimal(row[11].ToString()),
                OrderQty = Convert.ToDecimal(row[12]),
                Uom = row[13].ToString(),
                StartTime = Convert.ToDateTime(row[14]),
                WindowTime = Convert.ToDateTime(row[15]),
            });
        }
        ListTable(purchasePlanDetList);
    }

    private void ListTable(IList<WeeklyPurchasePlanDet> wPlanDetList)
    {
        if (wPlanDetList == null || wPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        #region  orderQty
        IList<WeeklyPurchasePlanOpenOrder> pPlanOpenOrderList = this.TheGenericMgr.FindAllWithCustomQuery<WeeklyPurchasePlanOpenOrder>(string.Format(" select l from WeeklyPurchasePlanOpenOrder as l where l.UUID in ('{0}') ", string.Join("','", wPlanDetList.Select(d => d.UUID).Distinct().ToArray())));
        if (pPlanOpenOrderList != null && pPlanOpenOrderList.Count > 0)
        {
            foreach (var sd in wPlanDetList)
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

        var planByDateIndexs = wPlanDetList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
        var planByFlowItems = wPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th>");
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
            widths = "120%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.PurchaseQty));
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
            str.Append(firstPlan.RefItemCode);
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                // str.Append("<th >需求数</th><th >订单数</th><th >采购数</th><th >期末</th>")
                var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                var pPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new WeeklyPurchasePlanDet();
                str.Append("<td>");
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
            TheMrpMgr.UpdateWeeklyPurchasePlanQty(flowList, itemList, idList, shipQtyList, releaseNoList, dateFromList, this.CurrentUser);
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