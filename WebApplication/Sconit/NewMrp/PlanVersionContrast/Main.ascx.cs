using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.MRP;
using System.Text;

public partial class NewMrp_PlanVersionContrast_Main : MainModuleBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var planType = this.rblPlanType.SelectedValue;
            string searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_shipplanmstr order by createdate desc";
            if (planType == "ShipPlan")
            {
                searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_shipplanmstr order by createdate desc ";
            }
            else if (planType == "ProductionPlan")
            {
                searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_productionplanmstr order by createdate desc ";
            }
            else if (planType == "PurchasePlan")
            {
                searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_purchaseplanmstr order by createdate desc ";
            }
            else if (planType == "PurchasePlan2")
            {
                searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_purchaseplanmstr2 order by createdate desc ";
            }

            var versions = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
            foreach (System.Data.DataRow row in versions.Rows)
            {
                this.versionSelect1.Items.Add(new ListItem(row[0].ToString()+"["+row[1].ToString()+"]",row[0].ToString()));
                this.versionSelect2.Items.Add(new ListItem(row[0].ToString()+"["+row[1].ToString()+"]",row[0].ToString()));
            }
           
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var planType = this.rblPlanType.SelectedValue;
        int version1 = 0;
        int version2 = 0;
        try
        {
            int.TryParse(this.versionSelect1.Value, out version1);
            int.TryParse(this.versionSelect2.Value, out version2);
        }
        catch (Exception)
        {

            throw;
        }

        if (version1 == 0 || version2 == 0)
        {
            ShowErrorMessage("请输入正确的版本号,版本号只能为整数。");
            return;
        }
        if (version1 == version2)
        {
            ShowErrorMessage("版本1跟版本2 步能相同。");
            return;
        }
        string flowCodeValues = this.tbFlow.Value.Trim();
        if (!string.IsNullOrEmpty(flowCodeValues))
        {
            flowCodeValues = flowCodeValues.Replace("\r\n", ",");
            flowCodeValues = flowCodeValues.Replace("\n", ",");
        }
        else
        {
            if (planType != "ProductionPlan")
            {
                ShowErrorMessage("请输入路线。");
                return;
            }
        }
        string[] flowArr = flowCodeValues.Split(',');
        string item = this.tbItemCode.Text;
        if (planType == "ShipPlan")
        {
            ContrastShipPlan(version1, version2, flowArr, item);
        }
        else if (planType == "ProductionPlan")
        {
            ContrastProductionPlan(version1, version2, item);
        }
        else if (planType == "PurchasePlan")
        {
            ContrastPurchasePlan(version1, version2, flowArr, item);
        }
        else if (planType == "PurchasePlan2")
        {
            ContrastPurchasePlan2(version1, version2, flowArr, item);
        }
    }

    #region   发货计划
    private void ContrastShipPlan(int version1, int version2, string[] flowArr, string item)
    {
        var mstrs1 = TheGenericMgr.FindAllWithCustomQuery<ShipPlanMstr>("select m from ShipPlanMstr as m where m.ReleaseNo=?", version1);
        var mstrs2 = TheGenericMgr.FindAllWithCustomQuery<ShipPlanMstr>("select m from ShipPlanMstr as m where m.ReleaseNo=?", version2);
        if (mstrs1 == null || mstrs1.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version1));
            return;
        }
        if (mstrs2 == null || mstrs2.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version2));
            return;
        }

        string searchHql = " select d from  ShipPlanDet as d where Type='Daily' ";
        if (!string.IsNullOrEmpty(item))
        {
            searchHql += string.Format(" and d.Item='{0}' ", item);
        }
        string flowstr = string.Empty;
        if (flowArr.Length > 0)
        {
            foreach (var flow in flowArr)
            {
                if (string.IsNullOrEmpty(flowstr))
                {
                    flowstr = string.Format(" and ( d.Flow='{0}' ", flow);
                }
                else {
                    flowstr += string.Format(" or d.Flow='{0}' ",flow);
                }
            }
            searchHql +=flowstr+")";
        }

        var dets1 = TheGenericMgr.FindAllWithCustomQuery<ShipPlanDet>(searchHql + " and d.ShipPlanId= " + mstrs1.First().Id);
        var dets2 = TheGenericMgr.FindAllWithCustomQuery<ShipPlanDet>(searchHql + " and d.ShipPlanId= " + mstrs2.First().Id);
        var minStartTime1 = dets1.Min(s => s.StartTime);
        dets1 = (from d in dets1
                 where  d.StartTime < minStartTime1.AddDays(14)
                 select d).ToList();


        var minStartTime2 = dets2.Min(s => s.StartTime);
        dets2 = (from d in dets2
                 where  d.StartTime < minStartTime2.AddDays(14)
                 select d).ToList();

        var allResult1 = new System.Collections.Generic.List<ShipPlanDet>();
        allResult1.AddRange(dets1);
        allResult1.AddRange(dets2);

        var planByFlowItems = allResult1.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        var sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

        var eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;

        StringBuilder str = new StringBuilder();
        str.Append("<table id='tt' runat='server' border='1' class='GV' style='width:150%;border-collapse:collapse;'>");
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th>");

        if (sTime.AddDays(14) <= eTime)
        {
            for (int i = 0; i < 14; i++)
			{
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }
            for (int i = 0; i < 14; i++)
            {
                str.Append("<th colspan='2'>");
                str.Append(eTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                eTime = eTime.AddDays(1);
            }
            str.Append("</tr><tr class='GVHeader'>");

            for (int i = 0; i < 28; i++)
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
            }

        }
        else
        {
            while (sTime <= eTime.AddDays(14))
            {
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }

            str.Append("</tr><tr class='GVHeader'>");
            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;
            while (sTime <= eTime.AddDays(14))
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
                sTime = sTime.AddDays(1);
            }
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
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
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.RefItemCode);
            str.Append("</td>");

             sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

             eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;
            if (sTime.AddDays(14) <= eTime)
            {
                for (int i = 0; i < 28; i++)
                {
                    var curenPlan1 = dets1.Where(p => p.StartTime.Date == sTime.Date && p.Item==firstPlan.Item && p.Flow==firstPlan.Flow);
                    var curenPlan2 = dets2.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    if (i >14)
                    {
                        curenPlan1 = dets1.Where(p => p.StartTime.Date == eTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                        curenPlan2 = dets2.Where(p => p.StartTime.Date == eTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    }
                   
                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new ShipPlanDet();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new ShipPlanDet();
                    if (shipPlanDet1.ShipQty == shipPlanDet2.ShipQty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    if (i <= 14)
                    {
                        sTime = sTime.AddDays(1);
                    }
                    else {
                        eTime = eTime.AddDays(1);
                    }
                }
            }
            else
            {
                while (sTime <= eTime.AddDays(14))
                {
                    var curenPlan1 = dets1.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var curenPlan2 = dets2.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new ShipPlanDet();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new ShipPlanDet();
                    if (shipPlanDet1.ShipQty == shipPlanDet2.ShipQty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.ShipQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    sTime = sTime.AddDays(1);
                }
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.Resultlist.InnerHtml = str.ToString();
    }
    #endregion

    #region   生产计划
    private void ContrastProductionPlan(int version1, int version2,  string item)
    {
        var mstrs1 = TheGenericMgr.FindAllWithCustomQuery<ProductionPlanMstr>("select m from ProductionPlanMstr as m where m.ReleaseNo=?", version1);
        var mstrs2 = TheGenericMgr.FindAllWithCustomQuery<ProductionPlanMstr>("select m from ProductionPlanMstr as m where m.ReleaseNo=?", version2);
        if (mstrs1 == null || mstrs1.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version1));
            return;
        }
        if (mstrs2 == null || mstrs2.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version2));
            return;
        }

        string searchHql = " select d from  ProductionPlanDet as d where Type='Daily' ";
        if (!string.IsNullOrEmpty(item))
        {
            searchHql += string.Format(" and d.Item='{0}' ", item);
        }

        var dets1 = TheGenericMgr.FindAllWithCustomQuery<ProductionPlanDet>(searchHql + " and d.ProductionPlanId= " + mstrs1.First().Id);
        var dets2 = TheGenericMgr.FindAllWithCustomQuery<ProductionPlanDet>(searchHql + " and d.ProductionPlanId= " + mstrs2.First().Id);
        var minStartTime1 = dets1.Min(s => s.StartTime);
        dets1 = (from d in dets1
                 where d.StartTime < minStartTime1.AddDays(14)
                 select d).ToList();


        var minStartTime2 = dets2.Min(s => s.StartTime);
        dets2 = (from d in dets2
                 where  d.StartTime < minStartTime2.AddDays(14)
                 select d).ToList();

        var allResult1 = new System.Collections.Generic.List<ProductionPlanDet>();
        allResult1.AddRange(dets1);
        allResult1.AddRange(dets2);

        var planByFlowItems = allResult1.GroupBy(p => new {  p.Item });

        var sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

        var eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;

        StringBuilder str = new StringBuilder();
        str.Append("<table id='tt' runat='server' border='1' class='GV' style='width:150%;border-collapse:collapse;'>");
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th><th rowspan='2'>客户零件号</th>");

        if (sTime.AddDays(14) <= eTime)
        {
            for (int i = 0; i < 28; i++)
            {
                if (i <= 14)
                {
                    str.Append("<th colspan='2'>");
                    str.Append(sTime.ToString("yyyy-MM-dd"));
                    str.Append("</th>");
                    sTime = sTime.AddDays(1);
                }
                else
                {
                    str.Append("<th colspan='2'>");
                    str.Append(eTime.ToString("yyyy-MM-dd"));
                    str.Append("</th>");
                    eTime = eTime.AddDays(1);
                }
            }
            str.Append("</tr><tr class='GVHeader'>");

            for (int i = 0; i < 28; i++)
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
            }

        }
        else
        {
            while (sTime <= eTime.AddDays(14))
            {
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }

            str.Append("</tr><tr class='GVHeader'>");
            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;
            while (sTime <= eTime.AddDays(14))
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
                sTime = sTime.AddDays(1);
            }
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
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
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.RefItemCode);
            str.Append("</td>");

            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

            eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;
            if (sTime.AddDays(14) <= eTime)
            {
                for (int i = 0; i < 28; i++)
                {
                    var curenPlan1 = dets1.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item );
                    var curenPlan2 = dets2.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item );
                    if (i > 14)
                    {
                        curenPlan1 = dets1.Where(p => p.StartTime.Date == eTime.Date && p.Item == firstPlan.Item );
                        curenPlan2 = dets2.Where(p => p.StartTime.Date == eTime.Date && p.Item == firstPlan.Item );
                    }

                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new ProductionPlanDet();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new ProductionPlanDet();
                    if (shipPlanDet1.Qty == shipPlanDet2.Qty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.Qty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.Qty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.Qty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.Qty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    if (i <= 14)
                    {
                        sTime = sTime.AddDays(1);
                    }
                    else
                    {
                        eTime = eTime.AddDays(1);
                    }
                }
            }
            else
            {
                while (sTime <= eTime.AddDays(14))
                {
                    var curenPlan1 = dets1.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item );
                    var curenPlan2 = dets2.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item );
                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new ProductionPlanDet();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new ProductionPlanDet();
                    if (shipPlanDet1.Qty == shipPlanDet2.Qty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.Qty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.Qty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.Qty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.Qty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    sTime = sTime.AddDays(1);
                }
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.Resultlist.InnerHtml = str.ToString();
    }
    #endregion

    #region   采购计划
    private void ContrastPurchasePlan(int version1, int version2, string[] flowArr, string item)
    {
        var mstrs1 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr>("select m from PurchasePlanMstr as m where m.ReleaseNo=?", version1);
        var mstrs2 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr>("select m from PurchasePlanMstr as m where m.ReleaseNo=?", version2);
        if (mstrs1 == null || mstrs1.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version1));
            return;
        }
        if (mstrs2 == null || mstrs2.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version2));
            return;
        }

        string searchHql = " select d from  PurchasePlanDet as d where Type='Daily' ";
        if (!string.IsNullOrEmpty(item))
        {
            searchHql += string.Format(" and d.Item='{0}' ", item);
        }

        string flowstr = string.Empty;
        if (flowArr.Length > 0)
        {
            foreach (var flow in flowArr)
            {
                if (string.IsNullOrEmpty(flowstr))
                {
                    flowstr = string.Format(" and (d.Flow='{0}' ", flow);
                }
                else
                {
                    flowstr += string.Format(" or d.Flow='{0}' ", flow);
                }
            }
            searchHql += flowstr + ")";
        }

        var dets1 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDet>(searchHql + " and d.PurchasePlanId= " + mstrs1.First().Id);
        var dets2 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDet>(searchHql + " and d.PurchasePlanId= " + mstrs2.First().Id);
        var minStartTime1 = dets1.Min(s => s.WindowTime);
        dets1 = (from d in dets1
                 where  d.WindowTime < minStartTime1.AddDays(14)
                 select d).ToList();


        var minStartTime2 = dets2.Min(s => s.WindowTime);
        dets2 = (from d in dets2
                 where  d.WindowTime < minStartTime2.AddDays(14)
                 select d).ToList();

        var allResult1 = new System.Collections.Generic.List<PurchasePlanDet>();
        allResult1.AddRange(dets1);
        allResult1.AddRange(dets2);

        var planByFlowItems = allResult1.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        var sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

        var eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;

        StringBuilder str = new StringBuilder();
        str.Append("<table id='tt' runat='server' border='1' class='GV' style='width:150%;border-collapse:collapse;'>");
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th>");

        if (sTime.AddDays(14) <= eTime)
        {
            for (int i = 0; i < 14; i++)
            {
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }
            for (int i = 0; i < 14; i++)
            {
                str.Append("<th colspan='2'>");
                str.Append(eTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                eTime = eTime.AddDays(1);
            }
            str.Append("</tr><tr class='GVHeader'>");

            for (int i = 0; i < 28; i++)
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
            }

        }
        else
        {
            while (sTime <= eTime.AddDays(14))
            {
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }

            str.Append("</tr><tr class='GVHeader'>");
            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;
            while (sTime <= eTime.AddDays(14))
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
                sTime = sTime.AddDays(1);
            }
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
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

            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

            eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;
            if (sTime.AddDays(14) <= eTime)
            {
                for (int i = 0; i < 28; i++)
                {
                    var curenPlan1 = dets1.Where(p => p.WindowTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var curenPlan2 = dets2.Where(p => p.WindowTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    if (i > 14)
                    {
                        curenPlan1 = dets1.Where(p => p.WindowTime.Date == eTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                        curenPlan2 = dets2.Where(p => p.WindowTime.Date == eTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    }

                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new PurchasePlanDet();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new PurchasePlanDet();
                    if (shipPlanDet1.PurchaseQty == shipPlanDet2.PurchaseQty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    if (i <= 14)
                    {
                        sTime = sTime.AddDays(1);
                    }
                    else
                    {
                        eTime = eTime.AddDays(1);
                    }
                }
            }
            else
            {
                while (sTime <= eTime.AddDays(14))
                {
                    var curenPlan1 = dets1.Where(p => p.WindowTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var curenPlan2 = dets2.Where(p => p.WindowTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new PurchasePlanDet();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new PurchasePlanDet();
                    if (shipPlanDet1.PurchaseQty == shipPlanDet2.PurchaseQty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    sTime = sTime.AddDays(1);
                }
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.Resultlist.InnerHtml = str.ToString();
    }
    #endregion

    #region   采购计划 发货
    private void ContrastPurchasePlan2(int version1, int version2, string[] flowArr, string item)
    {
        var mstrs1 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr2>("select m from PurchasePlanMstr2 as m where m.ReleaseNo=?", version1);
        var mstrs2 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanMstr2>("select m from PurchasePlanMstr2 as m where m.ReleaseNo=?", version2);
        if (mstrs1 == null || mstrs1.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version1));
            return;
        }
        if (mstrs2 == null || mstrs2.Count == 0)
        {
            this.Resultlist.InnerHtml = "";
            ShowErrorMessage(string.Format("版本号{0}不存在，请确认。", version2));
            return;
        }

        string searchHql = " select d from  PurchasePlanDet2 as d where Type='Daily' ";
        if (!string.IsNullOrEmpty(item))
        {
            searchHql += string.Format(" and d.Item='{0}' ", item);
        }

        string flowstr = string.Empty;
        if (flowArr.Length > 0)
        {
            foreach (var flow in flowArr)
            {
                if (string.IsNullOrEmpty(flowstr))
                {
                    flowstr = string.Format(" and (d.Flow='{0}' ", flow);
                }
                else
                {
                    flowstr += string.Format(" or d.Flow='{0}' ", flow);
                }
            }
            searchHql += flowstr + ")";
        }

        var dets1 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDet2>(searchHql + " and d.PurchasePlanId= " + mstrs1.First().Id);
        var dets2 = TheGenericMgr.FindAllWithCustomQuery<PurchasePlanDet2>(searchHql + " and d.PurchasePlanId= " + mstrs2.First().Id);
        var minStartTime1 = dets1.Min(s => s.StartTime);
        dets1 = (from d in dets1
                 where  d.StartTime < minStartTime1.AddDays(14)
                 select d).ToList();


        var minStartTime2 = dets2.Min(s => s.StartTime);
        dets2 = (from d in dets2
                 where d.StartTime < minStartTime2.AddDays(14)
                 select d).ToList();

        var allResult1 = new System.Collections.Generic.List<PurchasePlanDet2>();
        allResult1.AddRange(dets1);
        allResult1.AddRange(dets2);

        var planByFlowItems = allResult1.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        var sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

        var eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;

        StringBuilder str = new StringBuilder();
        str.Append("<table id='tt' runat='server' border='1' class='GV' style='width:150%;border-collapse:collapse;'>");
        str.Append("<thead><tr class='GVHeader'><th rowspan='2'>序号</th><th rowspan='2'>路线</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th>");

        if (sTime.AddDays(14) <= eTime)
        {
            for (int i = 0; i < 14; i++)
            {
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }
            for (int i = 0; i < 14; i++)
            {
                str.Append("<th colspan='2'>");
                str.Append(eTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                eTime = eTime.AddDays(1);
            }
            str.Append("</tr><tr class='GVHeader'>");

            for (int i = 0; i < 28; i++)
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
            }

        }
        else
        {
            while (sTime <= eTime.AddDays(14))
            {
                str.Append("<th colspan='2'>");
                str.Append(sTime.ToString("yyyy-MM-dd"));
                str.Append("</th>");
                sTime = sTime.AddDays(1);
            }

            str.Append("</tr><tr class='GVHeader'>");
            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;
            while (sTime <= eTime.AddDays(14))
            {
                str.Append(string.Format("<th >{0}</th><th >{1}</th>", version1, version2));
                sTime = sTime.AddDays(1);
            }
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
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

            sTime = minStartTime1 < minStartTime2 ? minStartTime1 : minStartTime2;

            eTime = minStartTime1 < minStartTime2 ? minStartTime2 : minStartTime1;
            if (sTime.AddDays(14) <= eTime)
            {
                for (int i = 0; i < 28; i++)
                {
                    var curenPlan1 = dets1.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var curenPlan2 = dets2.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    if (i > 14)
                    {
                        curenPlan1 = dets1.Where(p => p.StartTime.Date == eTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                        curenPlan2 = dets2.Where(p => p.StartTime.Date == eTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    }

                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new PurchasePlanDet2();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new PurchasePlanDet2();
                    if (shipPlanDet1.PurchaseQty == shipPlanDet2.PurchaseQty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    if (i <= 14)
                    {
                        sTime = sTime.AddDays(1);
                    }
                    else
                    {
                        eTime = eTime.AddDays(1);
                    }
                }
            }
            else
            {
                while (sTime <= eTime.AddDays(14))
                {
                    var curenPlan1 = dets1.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var curenPlan2 = dets2.Where(p => p.StartTime.Date == sTime.Date && p.Item == firstPlan.Item && p.Flow == firstPlan.Flow);
                    var shipPlanDet1 = curenPlan1.Count() > 0 ? curenPlan1.First() : new PurchasePlanDet2();
                    var shipPlanDet2 = curenPlan2.Count() > 0 ? curenPlan2.First() : new PurchasePlanDet2();
                    if (shipPlanDet1.PurchaseQty == shipPlanDet2.PurchaseQty)
                    {
                        str.Append("<td>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    else
                    {
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet1.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                        str.Append("<td style='background-color:red;'>");
                        str.Append(shipPlanDet2.PurchaseQty.ToString("0.##"));
                        str.Append("</td>");
                    }
                    sTime = sTime.AddDays(1);
                }
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.Resultlist.InnerHtml = str.ToString();
    }
    #endregion

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        var planType = this.rblPlanType.SelectedValue;
        string searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_shipplanmstr order by createdate desc";
        if (planType == "ShipPlan")
        {
            searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_shipplanmstr order by createdate desc ";
        }
        else if (planType == "ProductionPlan")
        {
            searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_productionplanmstr order by createdate desc ";
        }
        else if (planType == "PurchasePlan")
        {
            searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_purchaseplanmstr order by createdate desc ";
        }
        else if (planType == "PurchasePlan2")
        {
            searchSql = " select top 50 ReleaseNo,Convert(varchar(10),CreateDate,121) as CreateDate1 from mrp_purchaseplanmstr2 order by createdate desc ";
        }

        var versions = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        this.versionSelect1.Items.Clear();
        this.versionSelect2.Items.Clear();
        this.versionSelect1.Items.Add(new ListItem("",""));
        this.versionSelect2.Items.Add(new ListItem("",""));
        foreach (System.Data.DataRow row in versions.Rows)
        {
            this.versionSelect1.Items.Add(new ListItem(row[0].ToString() + "[" + row[1].ToString() + "]", row[0].ToString()));
            this.versionSelect2.Items.Add(new ListItem(row[0].ToString() + "[" + row[1].ToString() + "]", row[0].ToString()));
        }
    }


}
