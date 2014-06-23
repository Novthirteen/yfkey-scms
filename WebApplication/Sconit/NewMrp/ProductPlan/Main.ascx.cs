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
using com.Sconit.Service.MRP.Impl;
using System.Data.SqlClient;

public partial class NewMrp_ProductPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:false,bool:true,bool:false,bool:false,string:"
        //    + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    DateTime startTime = DateTime.Today;
    //    if (this.tbStartDate.Text.Trim() != string.Empty)
    //    {
    //        DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
    //    }
    //    //var firmPlanList = this.TheMrpMgr.GetFirmPlanList(this.tbFlow.Text.Trim(), startTime);
    //    string flowCodeValues  =this.tbFlow.Value.Trim();
    //    if (!string.IsNullOrEmpty(flowCodeValues))
    //    {
    //        flowCodeValues = flowCodeValues.Replace("\r\n", ",");
    //        flowCodeValues = flowCodeValues.Replace("\n", ",");
    //    }
    //    var sqlParams = new SqlParameter[5];
    //    sqlParams[0] = new SqlParameter("@FlowType", "Production");
    //    sqlParams[1] = new SqlParameter("@Operation","");
    //    sqlParams[2] = new SqlParameter("@FlowCode", flowCodeValues);
    //    sqlParams[3] = new SqlParameter("@DateFrom", startTime);
    //    sqlParams[4] = new SqlParameter("@IsShow0", this.cbIsShow0.Checked);
    //    var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
    //    var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
    //    var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
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

    //    ListTable(firmPlanList, planInvList);
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string hql = " select p From ProductionPlan as p where 1=1 ";
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        }
        hql += string.Format(" and p.PlanDate>='{0}' ", startTime);
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

        IList<ProductionPlan> pdPlanList = TheGenericMgr.FindAllWithCustomQuery<ProductionPlan>(hql);

        ListTable(pdPlanList);
    }
    private void ListTable(IList<ProductionPlan> pdPlanList)
    {
        if (pdPlanList == null || pdPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        var groupByFlowItems = pdPlanList.OrderBy(p => p.Flow).GroupBy(d => new { d.Flow, d.Item }).ToDictionary(d => d.Key, d => d.ToList());
        var planByDateIndexs = pdPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);
        //var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode)
        //    .GroupBy(p => new { p.FlowCode, p.ItemCode });

        StringBuilder str = new StringBuilder();
        str.Append(CopyString());
        //head
        str.Append("<table border='1' class='GV' style='width:180%;border-collapse:collapse;'><thead>");
        str.Append("<tr class='GVHeader'><th>Seq</th><th>生产线</th><th>物料号</th><th>物料描述</th><th>安全</th><th>当前</th>");

        foreach (var planByDateIndex in planByDateIndexs)
        {
            var currentDate = planByDateIndex.Key;
            str.Append("<th >");
            str.Append(currentDate.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in groupByFlowItems)
        {
            //var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
            var firstPlan = planByFlowItem.Value.First();
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
            str.Append(firstPlan.ItemDescription);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.InvQty.ToString("0.##"));
            str.Append("</td>");
            var invQty = firstPlan.InvQty;
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var ProdPlanByDates = planByFlowItem.Value.Where(d => d.PlanDate == planByDateIndex.Key);
                var ProdPlanByDate = ProdPlanByDates != null && ProdPlanByDates.Count()>0?ProdPlanByDates.First():new ProductionPlan();
                var outQty = ProdPlanByDate.OutQty;
                invQty = invQty + ProdPlanByDate.InProdQty -outQty;
                if (invQty < firstPlan.SafeStock)
                {
                    outQty = firstPlan.SafeStock - invQty;
                    invQty = firstPlan.SafeStock;
                }
                str.Append("<td>");
                str.Append(outQty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = str.ToString();
    }


    private void ListTable(IList<FirmPlan> firmPlanList, IList<PlanInv> planInvList)
    {
        if (firmPlanList == null || firmPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }
        var planInvDic = planInvList.ToDictionary(d => d.ItemCode, d => d);
        var planByDateIndexs = firmPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);
        var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode)
            .GroupBy(p => new { p.FlowCode, p.ItemCode });

        StringBuilder str = new StringBuilder();
        str.Append(CopyString());
        //head
        str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead>");
        str.Append("<tr class='GVHeader'><th>Seq</th><th>生产线</th><th>物料号</th><th>物料描述</th><th>安全</th><th>当前</th>");
        
        foreach (var planByDateIndex in planByDateIndexs)
        {
            var currentDate = planByDateIndex.Key;
            str.Append("<th>");
            str.Append(currentDate.ToString("MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(p => p.PlanDate).ToDictionary(d => d.Key, d => d.Sum(q => q.OutQty));
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
            str.Append(firstPlan.ItemDescription);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.SafeStock.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planInv.InvQty.ToString("0.##"));
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qty = planDic.ValueOrDefault(planByDateIndex.Key);
                str.Append("<td>");
                str.Append(qty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = str.ToString();
    }

    private string CopyString()
    {
        return @"<a type='text/html' onclick='copyHtml()' href='#' id='copy'>复制</a>
                <script type='text/javascript'>
                    function copyHtml()
                    {
                        window.clipboardData.setData('Text', $('#ctl01_list')[0].innerHTML);
                    }
                </script>";
    }
}