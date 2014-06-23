using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity.MRP;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;

public partial class NewMrp_ProcurementPlan_Main : MainModuleBase
{
    private string FlowCode
    {
        get { return (string)ViewState["FlowCode"]; }
        set { ViewState["FlowCode"] = value; }
    }

    private Dictionary<string, decimal> ItemMinLotSizeDic
    {
        get { return (Dictionary<string, decimal>)ViewState["ItemMinLotSize"]; }
        set { ViewState["ItemMinLotSize"] = value; }
    }

    private string Item
    {
        get { return (string)ViewState["Item"]; }
        set { ViewState["Item"] = value; }
    }

    private bool isSupplier
    {
        get
        {
            if (this.ModuleParameter.ContainsKey("IsSupplier"))
            {
                return bool.Parse(this.ModuleParameter["IsSupplier"]);
            }
            return false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbSupplier.ServiceParameter = "string:Procurement,string:" + this.CurrentUser.Code;
        if (isSupplier)
        {
            //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:false,bool:false,bool:true,bool:true,string:"
            //    + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
        }
        else
        {
            //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:false,bool:false,bool:true,bool:true,string:"
            //    + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        }

        this.tbLocation.Visible = !isSupplier;
        this.ltLocation.Visible = !isSupplier;
        this.trSupplierItem.Visible = !isSupplier;

        if (!IsPostBack)
        {
            //this.rblDateType.Items.Add(new ListItem("天", "4"));
            //this.rblDateType.Items.Add(new ListItem("周", "5"));
            //this.rblDateType.Items.Add(new ListItem("月", "6"));
            //this.rblDateType.Items[0].Selected = true;
            //if (this.CurrentUser.HasPermission("NewMrp_ViewDailyPlan"))
            //{
            //    this.rblDateType.Items.Add(new ListItem("天", "4"));
            //}
            //if (this.CurrentUser.HasPermission("NewMrp_ViewWeeklyPlan"))
            //{
            //    this.rblDateType.Items.Add(new ListItem("周", "5"));
            //}
            //if (this.CurrentUser.HasPermission("NewMrp_ViewMonthlyPlan"))
            //{
            //    this.rblDateType.Items.Add(new ListItem("月", "6"));
            //}
            //if (this.rblDateType.Items.Count > 0)
            //{
            //    this.rblDateType.Items[0].Selected = true;
            //}
        }
        var eventargument = Request["__EVENTARGUMENT"];
        if (eventargument != null && eventargument.StartsWith("jsPostback$"))
        {
            this.search.Visible = false;
            this.newOrder.Visible = true;
            this.tbWinTime.Attributes["onchange"] += "setStartTime();";
            this.cbIsUrgent.Attributes["onchange"] += "setStartTime();";

            var splitStr = eventargument.Split('$');
            var planDateIndex = splitStr[1];
            var planDate = DateTime.Now;

            var dateType = (CodeMaster.TimeUnit)(0);
            DateTime startTime = DateTime.Today;
            if (this.tbStartDate.Text.Trim() != string.Empty)
            {
                DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
            }
            var firmPlans = new List<FirmPlan>();
            if (dateType == CodeMaster.TimeUnit.Day)
            {
                planDate = DateTime.Parse(planDateIndex + " 12:00:00");
                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@FlowType", "Procurement");
                sqlParams[1] = new SqlParameter("@Operation", null);
                sqlParams[2] = new SqlParameter("@FlowCode", splitStr[2]);
                sqlParams[3] = new SqlParameter("@DateFrom", startTime);
                sqlParams[4] = new SqlParameter("@IsShow0", 1);
                var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
                var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
                var planInvDic = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1])
                    .ToDictionary(d => d.ItemCode, d => d);

                firmPlans = firmPlanList.GroupBy(p => p.ItemCode)
                    .Select(p =>
                    {
                        var firmPlan = new FirmPlan();
                        firmPlan.ItemCode = p.Key;
                        firmPlan.ItemDescription = p.First().ItemDescription;
                        firmPlan.UomCode = p.First().UomCode;
                        firmPlan.InQty = p.Where(q => q.PlanDate <= planDate.Date).Sum(q => q.InQty);
                        firmPlan.OutQty = p.Where(q => q.PlanDate <= planDate.Date).Sum(q => q.OutQty);
                        var plan = planInvDic.ValueOrDefault(firmPlan.ItemCode) ?? new PlanInv();
                        firmPlan.InvQty = plan.InvQty + plan.RecQty;
                        firmPlan.SafeStock = plan.SafeStock;
                        firmPlan.Qty = firmPlan.OutQty + firmPlan.SafeStock - firmPlan.InvQty - firmPlan.InQty;
                        firmPlan.Uc = p.First().Uc;
                        firmPlan.Qty = firmPlan.Qty > 0 ? firmPlan.Qty : 0;
                        return firmPlan;
                    }).ToList();
            }
            else
            {
                string dateIndex = startTime.ToString("yyyy-MM");
                string dateIndexTo = planDateIndex;
                if (dateType == CodeMaster.TimeUnit.Week)
                {
                    dateIndex = DateTimeHelper.GetWeekOfYear(startTime);
                    planDate = DateTimeHelper.GetWeekIndexDateFrom(planDateIndex);
                }
                else
                {
                    dateIndex = startTime.ToString("yyyy-MM");
                    planDate = DateTime.Parse(planDateIndex + "-01 12:00:00");
                }

                var sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@FlowCode", splitStr[2]);
                sqlParams[1] = new SqlParameter("@DateType", dateType);
                sqlParams[2] = new SqlParameter("@DateIndexFrom", dateIndex);
                sqlParams[3] = new SqlParameter("@DateIndexTo", dateIndexTo);
                sqlParams[4] = new SqlParameter("@IsShow0", 1);
                var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetForcastPlan", sqlParams);
                var forcastPlanList = com.Sconit.Utility.IListHelper.DataTableToList<ForcastPlan>(ds.Tables[0]);
                var planInvDic = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1])
                    .ToDictionary(d => d.ItemCode, d => d);

                firmPlans = forcastPlanList.GroupBy(p => p.ItemCode)
                   .Select(p =>
                   {
                       var inv = planInvDic.ValueOrDefault(p.Key) ?? new PlanInv();
                       var firmPlan = new FirmPlan();
                       firmPlan.ItemCode = p.Key;
                       firmPlan.ItemDescription = p.First().ItemDescription;
                       firmPlan.UomCode = p.First().UomCode;
                       firmPlan.SafeStock = inv.SafeStock;
                       firmPlan.InvQty = inv.InvQty + inv.RecQty;
                       var forcastPlans = forcastPlanList.Where(q => q.ItemCode == p.Key && q.DateIndexTo.CompareTo(planDateIndex) <= 0);
                       firmPlan.InQty = forcastPlans.Sum(q => q.InQty);
                       firmPlan.OutQty = forcastPlans.Sum(q => q.Qty);
                       firmPlan.Qty = firmPlan.OutQty + firmPlan.SafeStock - firmPlan.InvQty - firmPlan.InQty;
                       firmPlan.Qty = firmPlan.Qty > 0 ? firmPlan.Qty : 0;
                       firmPlan.Uc = p.First().Uc;
                       return firmPlan;
                   }).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbItem.Text))
            {
                firmPlans = firmPlans.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
            }

            var list = firmPlans.Where(p => p.Qty > 0).ToList();
            list.AddRange(firmPlans.Where(p => p.Qty == 0));
            
            var flow = TheFlowMgr.LoadFlow(splitStr[2], true);
            this.ltlFlow.Text = string.Format("{0} [{1}]", flow.Code, flow.Description);
            this.cbReleaseOrder.Checked = flow.IsAutoRelease;
            this.tbWinTime.Text = planDate.ToString("yyyy-MM-dd HH:mm");
            double leadTime = 0;
            if (flow.LeadTime.HasValue)
            {
                leadTime = (double)flow.LeadTime.Value;
            }
            this.tbStartTime.Text = planDate.AddHours(-leadTime).ToString("yyyy-MM-dd HH:mm");

            ItemMinLotSizeDic = flow.FlowDetails.GroupBy(p => p.Item.Code).ToDictionary(d => d.Key, d => d.First().MinLotSize ?? 0);

            this.GV_Order.DataSource = list;
            this.GV_Order.DataBind();
        }
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    //if (isSupplier && tbFlow.Text.Trim() == string.Empty)
    //    //{
    //    //    ShowWarningMessage("请输入路线");
    //    //    return;
    //    //}
    //    //if (this.rblDateType.SelectedIndex == -1)
    //    //{
    //    //    ShowWarningMessage("请选择计划类型");
    //    //    return;
    //    //}
    //    var dateType = (CodeMaster.TimeUnit)(int.Parse(this.rblDateType.SelectedValue));
    //    DateTime startTime = DateTime.Today;
    //    if (this.tbStartDate.Text.Trim() != string.Empty)
    //    {
    //        DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
    //    }
    //    if (startTime < DateTime.Today)
    //    {
    //        ShowWarningMessage("时间不能小于今天");
    //        return;
    //    }
    //    string dateIndex = startTime.ToString("yyyy-MM-dd");
    //    string dateIndexTo = startTime.AddDays(15).ToString("yyyy-MM-dd");
    //    if (dateType == CodeMaster.TimeUnit.Week)
    //    {
    //        dateIndex = com.Sconit.Utility.DateTimeHelper.GetWeekOfYear(startTime);
    //        dateIndexTo = com.Sconit.Utility.DateTimeHelper.GetWeekOfYear(startTime.AddDays(7 * 16));
    //    }
    //    else if (dateType == CodeMaster.TimeUnit.Month)
    //    {
    //        dateIndex = startTime.ToString("yyyy-MM");
    //        dateIndexTo = startTime.AddMonths(12).ToString("yyyy-MM");
    //    }

    //    if (dateType == CodeMaster.TimeUnit.Day)
    //    {
    //        var sqlParams = new SqlParameter[5];
    //        sqlParams[0] = new SqlParameter("@FlowType", "Procurement");
    //        sqlParams[1] = new SqlParameter("@Operation", null);
    //        sqlParams[2] = new SqlParameter("@FlowCode", this.tbFlow.Text.Trim());
    //        sqlParams[3] = new SqlParameter("@DateFrom", startTime);
    //        sqlParams[4] = new SqlParameter("@IsShow0", this.cbIsShow0.Checked);
    //        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
    //        var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
    //        if (!string.IsNullOrEmpty(this.tbLocation.Text))
    //        {
    //            firmPlanList = firmPlanList.Where(p => p.LocationTo.Equals(this.tbLocation.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
    //        }
    //        if (!string.IsNullOrEmpty(this.tbSupplier.Text))
    //        {
    //            firmPlanList = firmPlanList.Where(p => p.Supplier.Equals(this.tbSupplier.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
    //        }
    //        if (!string.IsNullOrEmpty(this.tbItem.Text))
    //        {
    //            firmPlanList = firmPlanList.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
    //        }
    //        var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
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
    //        ListFirmTable(firmPlanList, planInvList);
    //    }
    //    else if (dateType == CodeMaster.TimeUnit.Week)
    //    {
    //        var sqlParams = new SqlParameter[5];
    //        sqlParams[0] = new SqlParameter("@FlowCode", tbFlow.Text.Trim());
    //        sqlParams[1] = new SqlParameter("@DateType", dateType);
    //        sqlParams[2] = new SqlParameter("@DateIndexFrom", dateIndex);
    //        sqlParams[3] = new SqlParameter("@DateIndexTo", dateIndexTo);
    //        sqlParams[4] = new SqlParameter("@IsShow0", this.cbIsShow0.Checked);
    //        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetForcastPlan", sqlParams);
    //        var forcastPlanList = com.Sconit.Utility.IListHelper.DataTableToList<ForcastPlan>(ds.Tables[0]);
    //        if (!string.IsNullOrEmpty(this.tbLocation.Text))
    //        {
    //            forcastPlanList = forcastPlanList.Where(p => p.LocationTo.Equals(this.tbLocation.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
    //        }
    //        if (!string.IsNullOrEmpty(this.tbSupplier.Text))
    //        {
    //            forcastPlanList = forcastPlanList.Where(p => p.Supplier.Equals(this.tbSupplier.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
    //        }
    //        if (!string.IsNullOrEmpty(this.tbItem.Text))
    //        {
    //            forcastPlanList = forcastPlanList.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
    //        }
    //        var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
    //        //ListWeeklyTable(forcastPlanList, planInvList);
    //        string searchSql = string.Format("select Code,Desc1 from Item ");
    //        var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
    //        foreach (System.Data.DataRow row in itemDescs.Rows)
    //        {
    //            var plans = forcastPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
    //            if (plans != null && plans.Count() > 0)
    //            {
    //                foreach (var plan in plans)
    //                {
    //                    plan.ItemDescription = row[1].ToString();
    //                }
    //            }
    //        }
    //        ListForcastTable(forcastPlanList, planInvList, dateType);
    //    }
    //    else
    //    {
    //        var sqlParams = new SqlParameter[5];
    //        sqlParams[0] = new SqlParameter("@FlowCode", tbFlow.Text.Trim());
    //        sqlParams[1] = new SqlParameter("@DateType", dateType);
    //        sqlParams[2] = new SqlParameter("@DateIndexFrom", dateIndex);
    //        sqlParams[3] = new SqlParameter("@DateIndexTo", dateIndexTo);
    //        sqlParams[4] = new SqlParameter("@IsShow0", this.cbIsShow0.Checked);
    //        var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetForcastPlan", sqlParams);
    //        var forcastPlanList = com.Sconit.Utility.IListHelper.DataTableToList<ForcastPlan>(ds.Tables[0]);
    //        if (!string.IsNullOrEmpty(this.tbLocation.Text))
    //        {
    //            forcastPlanList = forcastPlanList.Where(p => p.LocationTo.Equals(this.tbLocation.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
    //        }
    //        if (!string.IsNullOrEmpty(this.tbSupplier.Text))
    //        {
    //            forcastPlanList = forcastPlanList.Where(p => p.Supplier.Equals(this.tbSupplier.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
    //        }
    //        if (!string.IsNullOrEmpty(this.tbItem.Text))
    //        {
    //            forcastPlanList = forcastPlanList.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
    //        }
    //        var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
    //        string searchSql = string.Format("select Code,Desc1 from Item ");
    //        var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
    //        foreach (System.Data.DataRow row in itemDescs.Rows)
    //        {
    //            var plans = forcastPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
    //            if (plans != null && plans.Count() > 0)
    //            {
    //                foreach (var plan in plans)
    //                {
    //                    plan.ItemDescription = row[1].ToString();
    //                }
    //            }
    //        }
    //        ListForcastTable(forcastPlanList, planInvList, dateType);
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string hql = " select p From {0} as p where 1=1 ";
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        }
        hql += string.Format(" and p.PlanDate >= '{0}'  ", startTime);
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
        if (!string.IsNullOrEmpty(this.tbLocation.Text))
        {
            hql += string.Format(" and p.Location = '{0}' ", this.tbLocation.Text);
        }
        if (!string.IsNullOrEmpty(this.tbSupplier.Text))
        {
            hql += string.Format(" and p.Supplier = '{0}' ", this.tbSupplier.Text);
        }
        if (!string.IsNullOrEmpty(this.tbItem.Text))
        {
            hql += string.Format(" and p.Item= '{0}' ", this.tbItem.Text);
        }
        //if (!string.IsNullOrEmpty(this.tbPlanVersion.Text))
        //{
        //    hql += string.Format(" and p.PlanVersion= {0} ", this.tbPlanVersion.Text);
        //}
        var planVersion = 0;
        if (!string.IsNullOrEmpty(this.tbPlanVersion.Text))
        {
            int.TryParse(this.tbPlanVersion.Text.Trim(), out planVersion);
        }
        if (planVersion == 0)
        {
            IList<ProcurementPlan> pPlanList = TheGenericMgr.FindAllWithCustomQuery<ProcurementPlan>(string.Format(hql, "ProcurementPlan"));
            ListFirmTable(pPlanList);
        }
        else
        {
            hql += string.Format(" and p.PlanVersion= {0} ", planVersion);
            var pPlanLogList = this.TheGenericMgr.FindAllWithCustomQuery<ProcurementPlanLog>(string.Format(hql, "ProcurementPlanLog"));
            pPlanLogList = pPlanLogList ?? new List<ProcurementPlanLog>();
            var pPlanList = pPlanLogList.Select(p =>
            {
                var pPlan = new ProcurementPlan();
                pPlan.Id = p.PlanId;
                pPlan.PlanDate = p.PlanDate;
                pPlan.Flow = p.Flow;
                pPlan.Item = p.Item;
                pPlan.ItemDescription = p.ItemDescription;
                pPlan.Uom = p.Uom;
                pPlan.UnitQty = p.UnitQty;
                pPlan.Location = p.Location;
                pPlan.SafeStock = p.SafeStock;
                pPlan.MaxStock = p.MaxStock;
                pPlan.InvQty = p.InvQty;
                pPlan.InQty = p.InQty;
                pPlan.OutQty = p.OutQty;
                pPlan.OrderQty = p.OrderQty;
                pPlan.FinalQty = p.FinalQty;
                pPlan.PlanVersion = p.PlanVersion;
                pPlan.LastModifyUser = p.LastModifyUser;
                pPlan.LastModifyDate = p.LastModifyDate;
                pPlan.Supplier = p.Supplier;
                return pPlan;
            }).ToList();
            ListFirmTable(pPlanList);
        }
        
    }

    private void ListFirmTable(IList<ProcurementPlan> pPlanList)
    {
        if (pPlanList == null || pPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        string headStr = CopyString();

        var planByDateIndexs = pPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key).ToList();
        var planByFlowItems = pPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item }).ToDictionary(d=>d.Key,d=>d.ToList());
        if (isSupplier && !this.CurrentUser.HasPermission("NewMrp_PurchasePlanToOrder"))
        {
            #region 供应商/不能查看库存
            //head
            //str.Append("<table border='1' style='width:100%;border-collapse:collapse;'><thead class='GVHeader'><tr>");
            //str.Append("<th>Seq</th><th>路线</th><th>库位</th><th>物料号</th><th>物料描述</th><th>待收</th>");
            //foreach (var planByDateIndex in planByDateIndexs)
            //{
            //    str.Append("<th>");
            //    var flowCode = this.tbFlow.Text.Trim();
            //    str.Append(planByDateIndex.Key.ToString("MM-dd"));
            //    str.Append("</th>");
            //}
            //str.Append("</tr></thead>");
            //str.Append("<tbody>");
            //int l = 0;
            //foreach (var planByFlowItem in planByFlowItems)
            //{
            //    var firstPlan = planByFlowItem.First();
            //    var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
            //    var planDic = planByFlowItem.GroupBy(p => p.PlanDate)
            //        .ToDictionary(d => d.Key, d => new { OutQty = d.Sum(q => q.OutQty), InQty = d.Sum(q => q.InQty) });
            //    l++;
            //    if (l % 2 == 0)
            //    {
            //        str.Append("<tr class='GVAlternatingRow'>");
            //    }
            //    else
            //    {
            //        str.Append("<tr class='GVRow'>");
            //    }
            //    str.Append("<td>");
            //    str.Append(l);
            //    str.Append("</td>");
            //    str.Append("<td>");
            //    str.Append(planByFlowItem.Key.FlowCode);
            //    str.Append("</td>");
            //    str.Append("<td>");
            //    str.Append(firstPlan.LocationTo);
            //    str.Append("</td>");
            //    str.Append("<td>");
            //    str.Append(planByFlowItem.Key.ItemCode);
            //    str.Append("</td>");
            //    str.Append("<td>");
            //    str.Append(firstPlan.ItemDescription);
            //    str.Append("</td>");
            //    str.Append("<td>");
            //    str.Append(planInv.InQty.ToString("0.##"));
            //    str.Append("</td>");
            //    foreach (var planByDateIndex in planByDateIndexs)
            //    {
            //        var plan = planDic.ValueOrDefault(planByDateIndex.Key) ?? new { OutQty = 0.0, InQty = 0.0 };
            //        str.Append("<td >");
            //        str.Append(plan.OutQty.ToString("0.##"));
            //        str.Append("</td>");
            //    }
            //    str.Append("</tr>");
            //}
            //str.Append("</tbody></table>");
            #endregion
        }
        else
        {
            #region 计划员
            //head
            //str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead >");
            str.Append("<tr class='GVHeader'><th rowspan='2'>Seq</th><th rowspan='2'>路线</th><th rowspan='2'>库位</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th>");
            str.Append("<th rowspan='2'>安全</th><th rowspan='2'>最大</th><th rowspan='2'>期初</th>");  //<th rowspan='2'>已收</th>
            var planDateIndexCount = planByDateIndexs.Count();
            for (int i = 0; i < planDateIndexCount; i++)
            {
                var planByDateIndex = planByDateIndexs[i];
                str.Append("<th colspan='4'>");
                var planDate = planByDateIndex.Key.ToString("yyyy-MM-dd");
                str.Append(planDate);
                str.Append("</th>");
            }
            #region  通过长度控制table的宽度
            string widths = "100%";
            if (planDateIndexCount > 13)
            {
                widths = "350%";
            }
            else if (planDateIndexCount > 10)
            {
                widths = "300%";
            }
            else if (planDateIndexCount > 8)
            {
                widths = "230%";
            }
            else if (planDateIndexCount > 5)
            {
                widths = "150%";
            }
            else
            {
                widths = "100%";
            }

            headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'><thead>", widths);
            #endregion
            str.Append("</tr><tr class='GVHeader'>");
            for (int i = 0; i < planDateIndexCount; i++)
            {
                str.Append("<th style='min-width:30px'>待收</th>");
                str.Append("<th style='min-width:30px'>需求</th>");
                str.Append("<th style='min-width:30px'>采购数量</th>");
                str.Append("<th style='min-width:30px'>期末</th>");
            }
            str.Append("</tr></thead>");
            str.Append("<tbody>");
            int l = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
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
                str.Append(firstPlan.Location);
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
                str.Append(firstPlan.MaxStock.ToString("0.##"));
                str.Append("</td>");
                if (firstPlan.InvQty < firstPlan.SafeStock)
                {
                    str.Append("<td style='background:red'>");
                }
                else if (firstPlan.InvQty > firstPlan.MaxStock)
                {
                    str.Append("<td style='background:orange'>");
                }
                else if (firstPlan.InvQty <= firstPlan.MaxStock && firstPlan.InvQty >= firstPlan.SafeStock)
                {
                    str.Append("<td style='background:green'>");
                }
                else
                {
                    str.Append("<td >");
                }
                str.Append(firstPlan.InvQty.ToString("0.##"));
                str.Append("</td>");
                //str.Append("<td>");
                //str.Append(planInv.RecQty.ToString("0.##"));
                //str.Append("</td>");

                for (int i = 0; i < planDateIndexCount; i++)
                {
                    var pPlan = planByFlowItem.Value.Where(v => v.PlanDate == planByDateIndexs[i].Key).Count() > 0 ? planByFlowItem.Value.Where(v => v.PlanDate == planByDateIndexs[i].Key).First() : new ProcurementPlan();
                    str.Append("<td>");
                    str.Append(pPlan.InQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(pPlan.OutQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(pPlan.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    if (pPlan.FinalQty < pPlan.SafeStock)
                    {
                        str.Append("<td style='background:red'>");
                    }
                    else if (pPlan.FinalQty > pPlan.MaxStock)
                    {
                        str.Append("<td style='background:orange'>");
                    }
                    else if (pPlan.FinalQty <= pPlan.MaxStock && pPlan.FinalQty >= pPlan.SafeStock)
                    {
                        str.Append("<td style='background:green'>");
                    }
                    else
                    {
                        str.Append("<td >");
                    }
                    str.Append(pPlan.FinalQty.ToString("0.##"));
                    str.Append("</td>");
                }

                //foreach (var pPlan in planByFlowItem.Value)
                //{
                //    str.Append("<td>");
                //    str.Append(pPlan.InQty.ToString("0.##"));
                //    str.Append("</td>");
                //    str.Append("<td>");
                //    str.Append(pPlan.OutQty.ToString("0.##"));
                //    str.Append("</td>");
                //    str.Append("<td>");
                //    str.Append(pPlan.OrderQty.ToString("0.##"));
                //    str.Append("</td>");
                //    if (pPlan.FinalQty < pPlan.SafeStock)
                //    {
                //        str.Append("<td style='background:red'>");
                //    }
                //    else if (pPlan.FinalQty > pPlan.MaxStock)
                //    {
                //        str.Append("<td style='background:orange'>");
                //    }
                //    else if (pPlan.FinalQty <= pPlan.MaxStock && pPlan.FinalQty >= pPlan.SafeStock)
                //    {
                //        str.Append("<td style='background:green'>");
                //    }
                //    else
                //    {
                //        str.Append("<td >");
                //    }
                //    str.Append(pPlan.FinalQty.ToString("0.##"));
                //    str.Append("</td>");
                //}

                str.Append("</tr>");
            }
            str.Append("</tbody></table>");
            #endregion
        }
        this.list.InnerHtml = headStr + str.ToString();
    }


    private void ListForcastTable(IList<ForcastPlan> forcastPlanList, IList<PlanInv> planInvList, CodeMaster.TimeUnit dateType)
    {
        if (forcastPlanList == null || forcastPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        StringBuilder str = new StringBuilder();
        str.Append(CopyString());

        if (isSupplier && !this.CurrentUser.HasPermission("NewMrp_PurchasePlanToOrder"))
        {
            #region 供应商/不能查看库存
            var planInvDic = planInvList.ToDictionary(d => d.ItemCode, d => d);
            var planByDateIndexs = forcastPlanList.GroupBy(p => p.DateIndexFrom).OrderBy(p => p.Key);
            var planByFlowItems = forcastPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode });

            //head
            str.Append("<table border='1' style='width:100%;border-collapse:collapse;'><thead class='GVHeader'><tr>");
            str.Append("<th>Seq</th><th>路线</th><th style='min-width:30px'>库位</th><th>物料号</th><th>物料描述</th><th style='min-width:30px'>待收</th>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var planIndexHead = planByDateIndex.Key;
                if (dateType == CodeMaster.TimeUnit.Week)
                {
                    planIndexHead = string.Format("{0}<br/>({1})", planByDateIndex.Key, DateTimeHelper.GetWeekIndexDateFrom(planByDateIndex.Key).ToString("MM-dd"));
                }
                str.Append("<th style='min-width:50px'>");
                //var flowCode = this.tbFlow.Text.Trim();
                str.Append(planIndexHead);
                str.Append("</th>");
            }
            str.Append("</tr></thead>");
            str.Append("<tbody>");
            int l = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
                var firstPlan = planByFlowItem.First();
                var planDic = planByFlowItem.ToDictionary(d => d.DateIndexFrom, d => d.Qty);
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
                str.Append(firstPlan.LocationTo);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planByFlowItem.Key.ItemCode);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(firstPlan.ItemDescription);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.InQty.ToString("0.##"));
                str.Append("</td>");
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var qty = planDic.ValueOrDefault(planByDateIndex.Key);
                    str.Append("<td >");
                    str.Append(qty.ToString("0.##"));
                    str.Append("</td>");
                }
                str.Append("</tr>");
            }
            str.Append("</tbody></table>");
            #endregion
        }
        else
        {
            #region 计划员
            var planInvDic = planInvList
                .GroupBy(p => p.ItemCode).Select(p => new PlanInv
                {
                    ItemCode = p.Key,
                    InvQty = p.Sum(q => q.InvQty),
                    SafeStock = p.First().SafeStock,
                    MaxStock = p.First().MaxStock,
                    RecQty = p.Sum(q => q.RecQty),
                    FgBreakQty = p.Sum(q => q.FgBreakQty)
                })
                .ToDictionary(d => d.ItemCode, d => d);
            var planByDateIndexs = forcastPlanList.GroupBy(p => p.DateIndexFrom).OrderBy(p => p.Key);
            var planByFlowItems = forcastPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode });

            str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead >");
            str.Append("<tr class='GVHeader'><th rowspan='2'>Seq</th><th rowspan='2'>路线</th><th rowspan='2'>库位</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th>");
            str.Append("<th rowspan='2'>安全</th><th rowspan='2'>最大</th><th rowspan='2'>期初</th><th rowspan='2'>折合</th><th rowspan='2'>待收</th><th rowspan='2'>已收</th>");
            for (int i = 0; i < planByDateIndexs.Count(); i++)
            {
                var planByDateIndex = planByDateIndexs.ElementAt(i);
                str.Append("<th colspan='3'>");
                var weekIndex = planByDateIndex.Key;
                //var flowCode = this.tbFlow.Text.Trim();
                var planIndexHead = weekIndex;
                if (dateType == CodeMaster.TimeUnit.Week)
                {
                    planIndexHead = string.Format("{0}({1})", weekIndex, DateTimeHelper.GetWeekIndexDateFrom(weekIndex).ToString("MM-dd"));
                }

                //if (flowCode != string.Empty && !isSupplier && this.CurrentUser.HasPermission("NewMrp_PurchasePlanToOrder"))
                //{
                //    str.Append(string.Format("<a href=\"javascript:__doPostBack('','jsPostback${0}${1}')\">{2}</a>", weekIndex, flowCode, planIndexHead));
                //}
                //else
                //{
                //    str.Append(planIndexHead);
                //}
                str.Append("</th>");
            }
            str.Append("</tr><tr class='GVHeader'>");

            foreach (var planByDateIndex in planByDateIndexs)
            {
                str.Append("<th style='min-width:30px'>待收</th>");
                str.Append("<th style='min-width:30px'>需求</th>");
                str.Append("<th style='min-width:30px'>期末</th>");
            }
            str.Append("</tr></thead>");
            str.Append("<tbody>");
            int l = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
                var planDic = planByFlowItem.GroupBy(p => p.DateIndexFrom)
                    .ToDictionary(d => d.Key, d => new { OutQty = d.Sum(q => q.Qty), InQty = d.Sum(q => q.InQty) });
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
                str.Append(firstPlan.LocationTo);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planByFlowItem.Key.ItemCode);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(firstPlan.ItemDescription);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.SafeStock.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.MaxStock.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.InvQty.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.FgBreakQty.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.InQty.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.RecQty.ToString("0.##"));
                str.Append("</td>");

                var qty = planInv.InvQty + planInv.RecQty;

                for (int i = 0; i < planByDateIndexs.Count(); i++)
                {
                    var planByDateIndex = planByDateIndexs.ElementAt(i);
                    var plan = planDic.ValueOrDefault(planByDateIndex.Key) ?? new { OutQty = 0.0, InQty = 0.0 };
                    qty = qty + plan.InQty - plan.OutQty;

                    str.Append("<td>");
                    str.Append(plan.InQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(plan.OutQty.ToString("0.##"));
                    str.Append("</td>");
                    if (qty < 0)
                    {
                        str.Append("<td style='background:orangered'>");
                    }
                    else if (qty < planInv.SafeStock)
                    {
                        str.Append("<td style='background:orange'>");
                    }
                    else if (qty > planInv.MaxStock)
                    {
                        str.Append("<td style='background:yellow'>");
                    }
                    else
                    {
                        str.Append("<td>");
                    }
                    str.Append(qty.ToString("0.##"));
                    str.Append("</td>");
                }
                str.Append("</tr>");
            }
            str.Append("</tbody></table>");
            #endregion
        }
        this.list.InnerHtml = str.ToString();
    }

    private void ListFirmTable(IList<FirmPlan> firmPlanList, IList<PlanInv> planInvList)
    {
        if (firmPlanList == null || firmPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        string headStr = CopyString();

        var planInvDic = planInvList
            .GroupBy(p => p.ItemCode).Select(p => new PlanInv
            {
                InvQty = p.Sum(q => q.InvQty),
                ItemCode = p.Key,
                SafeStock = p.First().SafeStock,
                MaxStock = p.First().MaxStock,
                RecQty = p.Sum(q => q.RecQty)
            })
            .ToDictionary(d => d.ItemCode, d => d);
        var planByDateIndexs = firmPlanList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key).ToList();
        var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode }).ToList();
        if (isSupplier && !this.CurrentUser.HasPermission("NewMrp_PurchasePlanToOrder"))
        {
            #region 供应商/不能查看库存
            //head
            str.Append("<table border='1' style='width:100%;border-collapse:collapse;'><thead class='GVHeader'><tr>");
            str.Append("<th>Seq</th><th>路线</th><th>库位</th><th>物料号</th><th>物料描述</th><th>待收</th>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                str.Append("<th>");
                //var flowCode = this.tbFlow.Text.Trim();
                str.Append(planByDateIndex.Key.ToString("MM-dd"));
                str.Append("</th>");
            }
            str.Append("</tr></thead>");
            str.Append("<tbody>");
            int l = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
                var planDic = planByFlowItem.GroupBy(p => p.PlanDate)
                    .ToDictionary(d => d.Key, d => new { OutQty = d.Sum(q => q.OutQty), InQty = d.Sum(q => q.InQty) });
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
                str.Append(firstPlan.LocationTo);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planByFlowItem.Key.ItemCode);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(firstPlan.ItemDescription);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.InQty.ToString("0.##"));
                str.Append("</td>");
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var plan = planDic.ValueOrDefault(planByDateIndex.Key) ?? new { OutQty = 0.0, InQty = 0.0 };
                    str.Append("<td >");
                    str.Append(plan.OutQty.ToString("0.##"));
                    str.Append("</td>");
                }
                str.Append("</tr>");
            }
            str.Append("</tbody></table>");
            #endregion
        }
        else
        {
            #region 计划员
            //head
            //str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead >");
            str.Append("<tr class='GVHeader'><th rowspan='2'>Seq</th><th rowspan='2'>路线</th><th rowspan='2'>库位</th><th rowspan='2'>物料号</th><th rowspan='2'>物料描述</th>");
            str.Append("<th rowspan='2'>安全</th><th rowspan='2'>最大</th><th rowspan='2'>期初</th>");  //<th rowspan='2'>已收</th>
            var planDateIndexCount = planByDateIndexs.Count();
            for (int i = 0; i < planDateIndexCount; i++)
            {
                var planByDateIndex = planByDateIndexs[i];
                str.Append("<th colspan='3'>");
                var planDate = planByDateIndex.Key.ToString("yyyy-MM-dd");
                //var flowCode = this.tbFlow.Text.Trim();
                //if (flowCode != string.Empty && !isSupplier && this.CurrentUser.HasPermission("NewMrp_PurchasePlanToOrder"))
                //{
                  //  str.Append(string.Format("<a href=\"javascript:__doPostBack('','jsPostback${0}${1}')\">{2}</a>", planDate, flowCode, planByDateIndex.Key.ToString("MM-dd")));
                //}
                //else
                //{
                str.Append(planByDateIndex.Key.ToString("MM-dd"));
                //}
                str.Append("</th>");
            }
            #region  通过长度控制table的宽度
            string widths = "100%";
            if (planDateIndexCount > 13)
            {
                widths = "210%";
            }
            else if (planDateIndexCount > 10)
            {
                widths = "180%";
            }
            else if (planDateIndexCount > 8)
            {
                widths = "150%";
            }
            else if (planDateIndexCount > 5)
            {
                widths = "130%";
            }
            else 
            {
                widths = "100%";
            }
            
            headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'><thead>", widths);
            #endregion
            str.Append("</tr><tr class='GVHeader'>");
            for (int i = 0; i < planDateIndexCount; i++)
            {
                str.Append("<th style='min-width:30px'>待收</th>");
                str.Append("<th style='min-width:30px'>需求</th>");
                str.Append("<th style='min-width:30px'>期末</th>");
            }
            str.Append("</tr></thead>");
            str.Append("<tbody>");
            int l = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planInv = planInvDic.ValueOrDefault(planByFlowItem.Key.ItemCode) ?? new PlanInv();
                var planDic = planByFlowItem.GroupBy(p => p.PlanDate)
                    .ToDictionary(d => d.Key, d => new { OutQty = d.Sum(q => q.OutQty), InQty = d.Sum(q => q.InQty) });
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
                str.Append(firstPlan.LocationTo);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planByFlowItem.Key.ItemCode);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(firstPlan.ItemDescription);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.SafeStock.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.MaxStock.ToString("0.##"));
                str.Append("</td>");
                str.Append("<td>");
                str.Append(planInv.InvQty.ToString("0.##"));
                str.Append("</td>");
                //str.Append("<td>");
                //str.Append(planInv.RecQty.ToString("0.##"));
                //str.Append("</td>");

                var qty = planInv.InvQty + planInv.RecQty;

                for (int i = 0; i < planDateIndexCount; i++)
                {
                    var planByDateIndex = planByDateIndexs[i];

                    var plan = planDic.ValueOrDefault(planByDateIndex.Key) ?? new { OutQty = 0.0, InQty = 0.0 };
                    qty = qty + plan.InQty - plan.OutQty;

                    str.Append("<td>");
                    str.Append(plan.InQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(plan.OutQty.ToString("0.##"));
                    str.Append("</td>");
                    if (qty < planInv.SafeStock)
                    {
                        str.Append("<td style='background:red'>");
                    }
                    else if (qty > planInv.MaxStock)
                    {
                        str.Append("<td style='background:orange'>");
                    }
                    else if (qty <= planInv.MaxStock && qty >= planInv.SafeStock)
                    {
                        str.Append("<td style='background:green'>");
                    }
                    else
                    {
                        str.Append("<td >");

                    }
                    str.Append(qty.ToString("0.##"));
                    str.Append("</td>");
                }
                str.Append("</tr>");
            }
            str.Append("</tbody></table>");
            #endregion
        }
        this.list.InnerHtml =headStr+ str.ToString();
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (isSupplier)
            {
                return;
            }
            //if (this.tbFlow.Text == string.Empty)
            {
                ShowErrorMessage("${MRP.Schedule.Import.CustomerSchedule.Result.SelectFlow}");
                return;
            }
            DateTime winTime = this.tbWinTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbWinTime.Text);
            DateTime startTime = this.tbStartTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbStartTime.Text);

            Flow flow = TheFlowMgr.CheckAndLoadFlow(this.tbFlow.Value.Trim());
            OrderHead orderHead = this.TheOrderMgr.TransferFlow2Order(flow);
            orderHead.WindowTime = winTime;
            orderHead.StartTime = startTime;
            orderHead.IsAutoRelease = this.cbReleaseOrder.Checked;

            IList<OrderDetail> resultOrderDetailList = new List<OrderDetail>();
            foreach (GridViewRow row in this.GV_Order.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string item = row.Cells[1].Text;
                    decimal qty = 0;
                    string qtyStr = ((TextBox)row.Cells[10].FindControl("tbQty")).Text.Trim();
                    qtyStr = qtyStr == string.Empty ? "0" : qtyStr;
                    if (!decimal.TryParse(qtyStr, out qty))
                    {
                        this.ShowErrorMessage("${MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error}");
                        return;
                    }
                    if (qty > 0)
                    {
                        OrderDetail orderDetail = orderHead.OrderDetails.FirstOrDefault(p => p.Item.Code == item);

                        if (orderDetail != null)
                        {
                            orderDetail.OrderedQty = qty;
                            orderDetail.RequiredQty = decimal.Parse(row.Cells[9].Text);

                            var ordertracers = new List<OrderTracer>();
                            var ordertracer = new OrderTracer();
                            ordertracer.Code = "FromPlan";
                            ordertracer.Item = orderDetail.Item.Code;
                            //ordertracer.Memo = "MRP";
                            ordertracer.OrderDetail = orderDetail;
                            ordertracer.OrderedQty = orderDetail.OrderedQty;
                            ordertracer.Qty = orderDetail.RequiredQty;
                            //ordertracer.RefId = orderDetail.Id;
                            ordertracer.ReqTime = orderHead.WindowTime;
                            ordertracer.TracerType = "MRP";
                            ordertracers.Add(ordertracer);
                            orderDetail.OrderTracers = ordertracers;

                            resultOrderDetailList.Add(orderDetail);
                        }
                    }
                }
            }

            if (resultOrderDetailList.Count == 0)
            {
                this.ShowErrorMessage("MasterData.Order.OrderHead.OrderDetail.Required");
                return;
            }
            else
            {
                if (this.tbStartTime.Text != string.Empty)
                {
                    startTime = DateTime.Parse(this.tbStartTime.Text.Trim());
                }

                orderHead.OrderDetails = resultOrderDetailList;

                if (this.cbIsUrgent.Checked)
                {
                    orderHead.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_URGENT;
                }
                else
                {
                    orderHead.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;
                }
            }

            TheOrderMgr.CreateOrder(orderHead, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Order.OrderHead.AddOrder.Successfully", orderHead.OrderNo);
            //跳转到相应的订单查询一面
            //string url = "Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";
            //Page.ClientScript.RegisterStartupScript(GetType(), "method",
            //    " <script language='javascript' type='text/javascript'>timedMsg('" + url + "'); </script>");
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
        catch (Exception ex)
        {
            this.ShowErrorMessage(ex.Message);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.search.Visible = true;
        this.newOrder.Visible = false;
    }

    protected void GV_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FirmPlan firmPlan = (FirmPlan)(e.Row.DataItem);
            var qty = Math.Ceiling(firmPlan.Qty / firmPlan.Uc) * firmPlan.Uc;
            var minLotSize = (double)(ItemMinLotSizeDic.ValueOrDefault(firmPlan.ItemCode));
            qty = minLotSize > qty ? minLotSize : qty;
            ((TextBox)e.Row.Cells[9].FindControl("tbQty")).Text = qty.ToString();
        }
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (isSupplier && tbFlow.Value.Trim() == string.Empty)
        {
            ShowWarningMessage("请输入路线");
            return;
        }
        //if (this.rblDateType.SelectedIndex == -1)
        //{
        //    ShowWarningMessage("请选择计划类型");
        //    return;
        //}
        var dateType = (CodeMaster.TimeUnit)(0);
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
        }
        if (startTime < DateTime.Today)
        {
            ShowWarningMessage("时间不能小于今天");
            return;
        }
        string dateIndex = startTime.ToString("yyyy-MM-dd");
        string dateIndexTo = startTime.AddDays(15).ToString("yyyy-MM-dd");
        if (dateType == CodeMaster.TimeUnit.Week)
        {
            dateIndex = com.Sconit.Utility.DateTimeHelper.GetWeekOfYear(startTime);
            dateIndexTo = com.Sconit.Utility.DateTimeHelper.GetWeekOfYear(startTime.AddDays(7 * 16));
        }
        else if (dateType == CodeMaster.TimeUnit.Month)
        {
            dateIndex = startTime.ToString("yyyy-MM");
            dateIndexTo = startTime.AddMonths(12).ToString("yyyy-MM");
        }

        if (dateType == CodeMaster.TimeUnit.Day)
        {
            var sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@FlowType", "Procurement");
            sqlParams[1] = new SqlParameter("@Operation", null);
            sqlParams[2] = new SqlParameter("@FlowCode", this.tbFlow.Value.Trim());
            sqlParams[3] = new SqlParameter("@DateFrom", startTime);
            sqlParams[4] = new SqlParameter("@IsShow0", false);
            var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
            var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
            if (!string.IsNullOrEmpty(this.tbLocation.Text))
            {
                firmPlanList = firmPlanList.Where(p => p.LocationTo.Equals(this.tbLocation.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbSupplier.Text))
            {
                firmPlanList = firmPlanList.Where(p => p.Supplier.Equals(this.tbSupplier.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbItem.Text))
            {
                firmPlanList = firmPlanList.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
            }
            var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
            string searchSql = string.Format("select Code,Desc1 from Item ");
            var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
            foreach (System.Data.DataRow row in itemDescs.Rows)
            {
                var plans = firmPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
                if (plans != null && plans.Count() > 0)
                {
                    foreach (var plan in plans)
                    {
                        plan.ItemDescription = row[1].ToString();
                    }
                }
            }
            IList<object> data = new List<object>();
            data.Add(firmPlanList);
            data.Add(planInvList);
            TheReportMgr.WriteToClient("ProcurementPlan(Day).xls", data, "ProcurementPlan(Day).xls");
        }
        else if (dateType == CodeMaster.TimeUnit.Week)
        {
            var sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@FlowCode", tbFlow.Value.Trim());
            sqlParams[1] = new SqlParameter("@DateType", dateType);
            sqlParams[2] = new SqlParameter("@DateIndexFrom", dateIndex);
            sqlParams[3] = new SqlParameter("@DateIndexTo", dateIndexTo);
            sqlParams[4] = new SqlParameter("@IsShow0", false);
            var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetForcastPlan", sqlParams);
            var forcastPlanList = com.Sconit.Utility.IListHelper.DataTableToList<ForcastPlan>(ds.Tables[0]);
            if (!string.IsNullOrEmpty(this.tbLocation.Text))
            {
                forcastPlanList = forcastPlanList.Where(p => p.LocationTo.Equals(this.tbLocation.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbSupplier.Text))
            {
                forcastPlanList = forcastPlanList.Where(p => p.Supplier.Equals(this.tbSupplier.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbItem.Text))
            {
                forcastPlanList = forcastPlanList.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
            }
            var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
            //ListWeeklyTable(forcastPlanList, planInvList);
            string searchSql = string.Format("select Code,Desc1 from Item ");
            var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
            foreach (System.Data.DataRow row in itemDescs.Rows)
            {
                var plans = forcastPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
                if (plans != null && plans.Count() > 0)
                {
                    foreach (var plan in plans)
                    {
                        plan.ItemDescription = row[1].ToString();
                    }
                }
            }
            ListForcastTable(forcastPlanList, planInvList, dateType);
        }
        else
        {
            var sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@FlowCode", tbFlow.Value.Trim());
            sqlParams[1] = new SqlParameter("@DateType", dateType);
            sqlParams[2] = new SqlParameter("@DateIndexFrom", dateIndex);
            sqlParams[3] = new SqlParameter("@DateIndexTo", dateIndexTo);
            sqlParams[4] = new SqlParameter("@IsShow0", false);
            var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetForcastPlan", sqlParams);
            var forcastPlanList = com.Sconit.Utility.IListHelper.DataTableToList<ForcastPlan>(ds.Tables[0]);
            if (!string.IsNullOrEmpty(this.tbLocation.Text))
            {
                forcastPlanList = forcastPlanList.Where(p => p.LocationTo.Equals(this.tbLocation.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbSupplier.Text))
            {
                forcastPlanList = forcastPlanList.Where(p => p.Supplier.Equals(this.tbSupplier.Text.Trim(), StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(this.tbItem.Text))
            {
                forcastPlanList = forcastPlanList.Where(p => p.ItemCode == this.tbItem.Text.Trim()).ToList();
            }
            var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
            string searchSql = string.Format("select Code,Desc1 from Item ");
            var itemDescs = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
            foreach (System.Data.DataRow row in itemDescs.Rows)
            {
                var plans = forcastPlanList.Where(f => f.ItemCode.ToUpper() == row[0].ToString().ToUpper());
                if (plans != null && plans.Count() > 0)
                {
                    foreach (var plan in plans)
                    {
                        plan.ItemDescription = row[1].ToString();
                    }
                }
            }
            ListForcastTable(forcastPlanList, planInvList, dateType);
        }
    }

    protected void btnImport_ClickShow(object sender, EventArgs e)
    {
        this.tblImport.Visible = true;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            //var dateType = (CodeMaster.TimeUnit)(int.Parse(this.rblDateType.SelectedValue));
            var dateType = (CodeMaster.TimeUnit)(0);
            string orderNos = TheMrpMgr.ReadProcurementPlanFromXls(fileUpload.PostedFile.InputStream,dateType, this.CurrentUser);
            ShowSuccessMessage(orderNos);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnImportBack_Click(object sender, EventArgs e)
    {
        this.tblImport.Visible = false;
        this.search.Visible = true;
    }



}