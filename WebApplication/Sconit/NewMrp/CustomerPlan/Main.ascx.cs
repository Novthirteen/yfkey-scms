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

public partial class NewMrp_CustomerPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:"
            + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;

        var eventargument = Request["__EVENTARGUMENT"];
        if (eventargument != null && eventargument.StartsWith("jsPostback$"))
        {
            this.search.Visible = false;
            this.newOrder.Visible = true;
            var splitStr = eventargument.Split('$');
            var dateType = int.Parse(splitStr[1]);
            var planDateIndex = splitStr[2];
            var flowCode = splitStr[3];
            var hql = "from CustomerPlan p where p.DateType=? and p.DateIndex=? and p.Flow = ? ";
            var paramList = new List<object> { dateType, planDateIndex, flowCode };
            var customerPlanList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerPlan>(hql, paramList);

            this.GV_Order.DataSource = customerPlanList;
            this.GV_Order.DataBind();

            var flow = TheFlowMgr.LoadFlow(flowCode);
            this.ltlFlow.Text = string.Format("{0} [{1}]", flow.Code, flow.Description);
            var planDate = DateTime.Parse(planDateIndex + " 12:00:00");
            this.tbWinTime.Text = planDate.ToString("yyyy-MM-dd HH:mm");
            double leadTime = 0;
            if (flow.LeadTime.HasValue)
            {
                leadTime = (double)flow.LeadTime.Value;
            }
            this.tbStartTime.Text = planDate.AddHours(-leadTime).ToString("yyyy-MM-dd HH:mm");
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            var dateType = (CodeMaster.TimeUnit)(int.Parse(this.rblDateType.SelectedValue));
            var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);

            this.ListTable(customerPlanList);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var dateType = (CodeMaster.TimeUnit)(int.Parse(this.rblSearchDateType.SelectedValue));
        DateTime startTime = DateTime.Today;
        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
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

        var planVersion = 0;
        int.TryParse(this.tbPlanVersion.Text.Trim(), out planVersion);
        if (planVersion == 0)
        {
            var hql = "select p from {0} as p where p.DateType=? and p.DateIndex>=? and p.DateIndex<? ";
            var paramList = new List<object> { (int)dateType, dateIndex, dateIndexTo };
            if (this.tbFlow.Text.Trim() != string.Empty)
            {
                hql += " and p.Flow = ? ";
                paramList.Add(this.tbFlow.Text.Trim());
            }
            var customerPlanList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerPlan>(string.Format(hql, "CustomerPlan"), paramList.ToArray()) ?? new List<CustomerPlan>();
            ListTable(customerPlanList);
        }
        else
        {
            var customerPlanLogList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerPlanLog>("from CustomerPlanLog where PlanVersion=? and DateType=? ", new object[] { planVersion, dateType });
            customerPlanLogList = customerPlanLogList ?? new List<CustomerPlanLog>();
            var customerPlanList = customerPlanLogList.Select(p =>
                {
                    var plan = new CustomerPlan();
                    plan.DateIndex = p.DateIndex;
                    plan.DateIndexTo = p.DateIndexTo;
                    plan.DateType = p.DateType;
                    plan.Flow = p.Flow;
                    plan.Item = p.Item;
                    plan.ItemDescription = p.ItemDescription;
                    plan.ItemReference = p.ItemReference;
                    plan.PlanVersion = p.PlanVersion;
                    plan.Qty = p.Qty;
                    plan.UnitQty = p.UnitQty;
                    plan.Uom = p.Uom;
                    plan.LastModifyDate = p.CreateDate;
                    return plan;
                }).ToList();
            var maxPlan = customerPlanList.OrderBy(p => p.PlanVersion).LastOrDefault();
            //this.ltlPlanVersion.Text = string.Format("本次结果第{0}次导入,时间:{1}", maxPlan.PlanVersion, maxPlan.LastModifyDate);
            ListTable(customerPlanList);
        }
    }

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblAction.SelectedIndex == 0)
        {
            this.tblImport.Visible = false;
            this.tblSearch.Visible = true;
        }
        else
        {
            this.tblImport.Visible = true;
            this.tblSearch.Visible = false;
        }
    }

    private void ListTable(IList<CustomerPlan> customerPlanList)
    {
        if (customerPlanList == null || customerPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        var customerPlanListDic = customerPlanList.GroupBy(p => new { p.Flow, p.Item, p.PlanVersion })
           .ToDictionary(d => d.Key, d => d.ToList());

        List<CustomerPlanLog> customerPlanLogList = new List<CustomerPlanLog>();
        var hql = "select p from CustomerPlanLog as p where p.DateType=? and p.DateIndex>=? and p.DateIndex<=? and PlanVersion <> ? and p.Item=? and p.Flow=? ";
        foreach (var dic in customerPlanListDic)
        {
            var customerPlans = dic.Value;
            var paramList = new List<object> { (int)customerPlans.First().DateType, customerPlans.Min(d => d.DateIndex), customerPlans.Max(d => d.DateIndex), customerPlans.First().PlanVersion, customerPlans.First().Item, customerPlans.First().Flow };
            var rr = this.TheGenericMgr.FindAllWithCustomQuery<CustomerPlanLog>(hql, paramList.ToArray());
            if (rr != null && rr.Count > 0)
            {
                customerPlanLogList.AddRange(rr);
            }
        }

        var customerPlanLogDic = (customerPlanLogList ?? new List<CustomerPlanLog>()).GroupBy(p => new { DateIndex = p.DateIndexTo, p.Flow, p.Item })
            .ToDictionary(d => d.Key, d => d.OrderByDescending(dd=>dd.PlanVersion).First());

        var planByDateIndexs = customerPlanList.GroupBy(p => p.DateIndex).OrderBy(p => p.Key);
        var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        var dateType = customerPlanList.First().DateType;
        str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead><tr class='GVHeader'><th>行数</th><th>路线</th><th>物料号</th><th>物料描述</th><th>单位</th><th>版本号</th><th>本次导入时间</th><th>上次导入时间</th>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th>");
            var dateIndex = planByDateIndex.Key;
            if (dateType == CodeMaster.TimeUnit.Day)
            {
                dateIndex = planByDateIndex.Key.Remove(0, 5);
            }
            //if (flowCode != string.Empty && this.CurrentUser.HasPermission("NewMrp_CustomerPlanToOrder"))
            //{
            //    str.Append(string.Format("<a href=\"javascript:__doPostBack('','jsPostback${0}${1}${2}')\">{3}</a>",
            //        (int)dateType, planByDateIndex.Key, flowCode, dateIndex));
            //}
            //else
            //{
                str.Append(dateIndex);
            //}
            str.Append("</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.OrderByDescending(p=>p.LastModifyDate).First();
            var planDic = planByFlowItem.ToDictionary(d => d.DateIndex, d => d.Qty);
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
            str.Append(firstPlan.ItemFullDescription);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Uom);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.PlanVersion);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.LastModifyDate);
            str.Append("</td>");
            var prevCreatedate = customerPlanLogList.Where(c =>  c.Flow == firstPlan.Flow && c.Item == firstPlan.Item).OrderByDescending(d=>d.CreateDate);
            str.Append("<td>");
            str.Append(prevCreatedate != null && prevCreatedate.Count() > 0 ? (DateTime?)prevCreatedate.FirstOrDefault().CreateDate : null);
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;
                var oldPlan = customerPlanLogDic.Keys.Contains(new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item }) ? customerPlanLogDic[new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item }] : null;
                if (oldPlan != null)
                {
                    if (oldPlan.Qty != qty)
                    {
                        str.Append(string.Format("<td style='background:orange' title='{0}'>", oldPlan.Qty.ToString("0.##")));
                    }
                    else
                    {
                        str.Append("<td style='background:yellow'>");
                    }
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
        this.list.InnerHtml = str.ToString();
    }

    private void ListTable(IList<CustomerPlan> customerPlanList, IList<CustomerPlanLog> customerPlanLogList)
    {
        if (customerPlanList == null || customerPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        var customerPlanLogDic = (customerPlanLogList ?? new List<CustomerPlanLog>()).GroupBy(p => new { DateIndex = p.DateIndexTo, p.Flow, p.Item })
            .ToDictionary(d => d.Key, d => d.First());

        var planByDateIndexs = customerPlanList.GroupBy(p => p.DateIndex).OrderBy(p => p.Key);
        var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        var dateType = customerPlanList.First().DateType;
        str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead><tr class='GVHeader'><th>行数</th><th>路线</th><th>物料号</th><th>物料描述</th><th>单位</th>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th>");
            var dateIndex = planByDateIndex.Key;
            if (dateType == CodeMaster.TimeUnit.Day)
            {
                dateIndex = planByDateIndex.Key.Remove(0, 5);
            }
            if (flowCode != string.Empty && this.CurrentUser.HasPermission("NewMrp_CustomerPlanToOrder"))
            {
                str.Append(string.Format("<a href=\"javascript:__doPostBack('','jsPostback${0}${1}${2}')\">{3}</a>",
                    (int)dateType, planByDateIndex.Key, flowCode, dateIndex));
            }
            else
            {
                str.Append(dateIndex);
            }
            str.Append("</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.ToDictionary(d => d.DateIndex, d => d.Qty);
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
            str.Append(firstPlan.ItemFullDescription);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Uom);
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                //var qty = planDic.ValueOrDefault(planByDateIndex.Key);
                //var oldPlan = customerPlanLogDic.ValueOrDefault(new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item });
                var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : -99;
                var oldPlan = customerPlanLogDic.Keys.Contains(new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item })?customerPlanLogDic[new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item }]:null;
                if (oldPlan != null)
                {
                    if (oldPlan.Qty != qty)
                    {
                        str.Append(string.Format("<td style='background:orange' title='{0}'>", oldPlan.Qty.ToString("0.##")));
                    }
                    else
                    {
                        str.Append("<td style='background:yellow'>");
                    }
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
        this.list.InnerHtml = str.ToString();
    }

    private string CopyString()
    {
        return @"<a type='text/html' onclick='copyHtml()' href='#' id='copy'>复制</a>
                 底色黄色重新导入无改动,橙色重新导入并有改动
                <script type='text/javascript'>
                    function copyHtml()
                    {
                        window.clipboardData.setData('Text', $('#ctl01_list')[0].innerHTML);
                    }
                </script>";
    }

    protected void GV_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.tbFlow.Text == string.Empty)
            {
                ShowErrorMessage("${MRP.Schedule.Import.CustomerSchedule.Result.SelectFlow}");
                return;
            }
            DateTime winTime = this.tbWinTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbWinTime.Text);
            DateTime startTime = this.tbStartTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbStartTime.Text);

            Flow flow = TheFlowMgr.CheckAndLoadFlow(this.tbFlow.Text.Trim());
            OrderHead orderHead = this.TheOrderMgr.TransferFlow2Order(flow);
            orderHead.WindowTime = winTime;
            orderHead.StartTime = startTime;
            orderHead.IsAutoRelease = this.cbReleaseOrder.Checked;
            orderHead.ReferenceOrderNo = this.tbRefOrderNo.Text.Trim();
            orderHead.ExternalOrderNo = this.tbExtOrderNo.Text.Trim();

            IList<OrderDetail> resultOrderDetailList = new List<OrderDetail>();
            foreach (GridViewRow row in this.GV_Order.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string item = row.Cells[1].Text;
                    decimal qty = 0;
                    string qtyStr = ((TextBox)row.Cells[5].FindControl("tbQty")).Text.Trim();
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
                            orderDetail.RequiredQty = decimal.Parse(row.Cells[4].Text);

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
            //string url = "Main.aspx?mid=Order.OrderHead.Distribution__mp--ModuleType-Distribution_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";
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

    protected void btnCalculateProdPlan_Click(object sender, EventArgs e)
    {
        try
        {

            var sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@FlowType", "Production");
            sqlParams[1] = new SqlParameter("@Operation", null);
            sqlParams[2] = new SqlParameter("@FlowCode", string.Empty);
            sqlParams[3] = new SqlParameter("@DateFrom", System.DateTime.Now.Date);
            sqlParams[4] = new SqlParameter("@IsShow0", false);
            var ds = TheGenericMgr.GetDatasetByStoredProcedure("USP_Report_MRP_GetFirmPlan", sqlParams);
            var firmPlanList = com.Sconit.Utility.IListHelper.DataTableToList<FirmPlan>(ds.Tables[0]);
            var planInvList = com.Sconit.Utility.IListHelper.DataTableToList<PlanInv>(ds.Tables[1]);
            if (firmPlanList == null || firmPlanList.Count == 0)
            {
                this.list.InnerHtml = "没有生产需求。";
            }
            var planInvDic = planInvList
          .GroupBy(p => p.ItemCode).Select(p => new PlanInv
          {
              InvQty = p.Sum(q => q.InvQty),
              ItemCode = p.Key
          }).ToDictionary(d => d.ItemCode, d => d);

            var planByFlowItems = firmPlanList.OrderBy(p => p.FlowCode).GroupBy(p => new { p.FlowCode, p.ItemCode })
                .ToDictionary(d => d.Key, d => d);
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
            int planVersion = TheNumberControlMgr.GenerateNumberNextSequence("ProductionPlan");
            IList<ProductionPlan> productionPlanList = new List<ProductionPlan>();
            foreach (var flowItems in planByFlowItems)
            {
                var planInv = planInvDic.Keys.Contains(flowItems.Value.First().ItemCode) ? planInvDic[flowItems.Value.First().ItemCode] : new PlanInv();
                var finalQty = planInv.InvQty + planInv.RecQty;
                foreach (var firmPlan in flowItems.Value.OrderBy(s => s.PlanDate))
                {
                    var outQty = firmPlan.OutQty;
                    //finalQty = finalQty + firmPlan.InQty - firmPlan.OutQty;
                    //if (finalQty < planInv.SafeStock)
                    //{
                    //    outQty += planInv.SafeStock - finalQty;
                        //finalQty += planInv.SafeStock - finalQty;
                    //}

                    ProductionPlan prPlan = new ProductionPlan
                    {
                        PlanDate = firmPlan.PlanDate.Date,
                        Flow = firmPlan.FlowCode,
                        Item = firmPlan.ItemCode,
                        ItemDescription = firmPlan.ItemDescription,
                        Uom = firmPlan.UomCode,
                        //UnitQty = firmPlan.uni,
                        SafeStock = Convert.ToDecimal(firmPlan.SafeStock),
                        MaxStock = Convert.ToDecimal(firmPlan.MaxStock),
                        InvQty = Convert.ToDecimal(planInv.InvQty),
                        OutQty = Convert.ToDecimal(outQty),
                        InProdQty = Convert.ToDecimal(firmPlan.InProdQty),
                        PlanVersion = planVersion,
                    };
                    productionPlanList.Add(prPlan);
                }
            }

            var dateTimeNow = DateTime.Now;
            if (productionPlanList != null && productionPlanList.Count > 0)
            {
                //string sql = string.Format(" from ProductionPlan as c where  c.DateType = '{0}' and c.DateIndexTo in('{1}') and c.Flow in ('{2}')",
                //   (int)dateType, string.Join("','", customerPlanList.Select(p => p.DateIndexTo).Distinct().ToArray()),
                //   string.Join("','", customerPlanList.Select(p => p.Flow).Distinct().ToArray()));

                //this.genericMgr.Delete(sql);
                //int planVersion = this.iNumberControlMgr.GenerateNumberNextSequence("ProcurementPlan");
                this.TheGenericMgr.Delete(" from ProductionPlan as c ");
                foreach (var prPlan in productionPlanList)
                {
                    prPlan.LastModifyDate = dateTimeNow;
                    prPlan.LastModifyUser = this.CurrentUser.Code;
                    TheGenericMgr.Create(prPlan);

                    ProductionPlanLog pPlanLog = new ProductionPlanLog
                    {
                        PlanId = prPlan.Id,
                        PlanDate = prPlan.PlanDate.Date,
                        Flow = prPlan.Flow,
                        Item = prPlan.Item,
                        ItemDescription = prPlan.ItemDescription,
                        Uom = prPlan.Uom,
                        UnitQty = prPlan.UnitQty,
                        SafeStock = prPlan.SafeStock,
                        MaxStock = prPlan.MaxStock,
                        InvQty = prPlan.InvQty,
                        OutQty = prPlan.OutQty,
                        InProdQty = prPlan.InProdQty,
                        LastModifyDate = dateTimeNow,
                        LastModifyUser = prPlan.LastModifyUser,
                        PlanVersion = prPlan.PlanVersion,
                    };
                    TheGenericMgr.Create(pPlanLog);
                }
            }
            ShowSuccessMessage("生成成功。");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}