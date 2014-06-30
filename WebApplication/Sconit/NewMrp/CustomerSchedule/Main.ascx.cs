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

public partial class NewMrp_CustomerSchedule_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:"
            + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            var dateType = this.rblDateType.SelectedValue;
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
        var hql = "select c from CustomerScheduleDetail as c where 1=1 ";
        var paramList = new List<object>();
        var dateType = this.rblSearchDateType.SelectedValue;
        hql += " and c.Type=? ";
        paramList.Add(dateType);
        if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        {
            hql += " and c.Flow =? ";
            paramList.Add(this.tbFlow.Text.Trim());
        }
        if (!string.IsNullOrEmpty(this.tbReferenceScheduleNo.Text.Trim()))
        {
            hql += " and c.ReferenceScheduleNo =? ";
            paramList.Add(this.tbReferenceScheduleNo.Text.Trim());
        }
            DateTime startTime = DateTime.Today;
        if (!string.IsNullOrEmpty(this.tbStartDate.Text.Trim()))
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
            hql += " and c.DateFrom>=? ";
            paramList.Add(startTime.Date);
        }
        else
        {
            this.list.InnerHtml = "开始日期不能为空。";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        if (!string.IsNullOrEmpty(this.tbEndDate.Text.Trim()))
        {
            DateTime endTime = DateTime.Today;
            DateTime.TryParse(this.tbEndDate.Text.Trim(), out endTime);
            if (dateType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
            {
                if (endTime > startTime.AddDays(7 * 30))
                {
                    this.list.InnerHtml = "周计划一次最多查询30周的计划。";
                    this.ltlPlanVersion.Text = string.Empty;
                    return;
                }
            }
            else
            {
                if (endTime > startTime.AddDays(14))
                {
                    this.list.InnerHtml = "天计划一次最多查询14天的计划。";
                    this.ltlPlanVersion.Text = string.Empty;
                    return;
                }
            }
            hql += " and c.DateTo<=? ";
            paramList.Add(endTime.Date);
        }
        else
        {
            DateTime endTime = startTime.AddDays(14);
            if (dateType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
            {
                endTime = startTime.AddDays(7 * 30);
            }
            hql += " and c.DateTo<=? ";
            paramList.Add(endTime.Date);
        }
        if (!string.IsNullOrEmpty(this.tbVersion.Text.Trim()))
        {
            var planVersion = 0;
            int.TryParse(this.tbVersion.Text.Trim(), out planVersion);
            hql += " and c.Version>=? ";
            paramList.Add(planVersion);
        }
       
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            hql += " and c.Item =? ";
            paramList.Add(this.tbItemCode.Text.Trim());
        }

        var flowMaxVersionDic = new Dictionary<string, int>();
        var customerPlanList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerScheduleDetail>(hql, paramList.ToArray()) ?? new List<CustomerScheduleDetail>();
        if (string.IsNullOrEmpty(this.tbVersion.Text.Trim()))
        {
            var activePlanList = new List<CustomerScheduleDetail>();
            System.Data.DataTable flowVersionG = TheGenericMgr.GetDatasetBySql(string.Format("select Flow,max(Version) as Version from dbo.CustScheduleDet where Type='{0}' group by Flow",dateType)).Tables[0];
            foreach (System.Data.DataRow row in flowVersionG.Rows)
            {
                flowMaxVersionDic.Add(row[0].ToString(), int.Parse(row[1].ToString()));
            }
            foreach (KeyValuePair<string, int> kv in flowMaxVersionDic)
            {
                activePlanList.AddRange(customerPlanList.Where(d => d.Flow == kv.Key && d.Version == kv.Value));
            }
            ListTable(activePlanList);
        }
        else
        {
            ListTable(customerPlanList);
        }
    }

    private void ListTable(IList<CustomerScheduleDetail> customerPlanList)
    {
        if (customerPlanList == null || customerPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        var customerPlanListDic = customerPlanList.GroupBy(p => new { p.Flow, p.Item, p.Version })
           .ToDictionary(d => d.Key, d => d.ToList());

        List<CustomerScheduleDetail> customerPlanLogList = new List<CustomerScheduleDetail>();
        var hql = "select p from CustomerScheduleDetail as p where p.Type=? and Version = ? and p.Flow=? ";
        foreach (var dic in customerPlanListDic)
        {
            var customerPlans = dic.Value;
            var paramList = new List<object> { customerPlans.First().Type,customerPlans.First().Version-1,  customerPlans.First().Flow };
            var rr = this.TheGenericMgr.FindAllWithCustomQuery<CustomerScheduleDetail>(hql, paramList.ToArray());
            if (rr != null && rr.Count > 0)
            {
                customerPlanLogList.AddRange(rr);
            }
        }

        var planByDateIndexs = customerPlanList.GroupBy(p => p.DateFrom).OrderBy(p => p.Key);
        var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = CopyString();
        str.Append("<thead><tr class='GVHeader'><th>行数</th><th>路线</th><th>客户版本号</th><th>物料号</th><th>物料描述</th><th>客户零件号</th><th>版本号</th><th>本次导入时间</th><th>上次导入时间</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th>");
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 25)
        {
            widths = "300%";
        }
        else if (ii > 20)
        {
            widths = "240%";
        }
        else if (ii > 15)
        {
            widths = "190%";
        }
        else if (ii > 10)
        {
            widths = "150%";
        }
        else if (ii > 6)
        {
            widths = "120%";
        }
        //else if (ii > 4)
        //{
        //    widths = "170%";
        //}
        //else if (ii > 2)
        //{
        //    widths = "130%";
        //}
        headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d=>d.DateFrom).ToDictionary(d => d.Key, d => d.Sum(q=>q.Qty));
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
            str.Append(firstPlan.ReferenceScheduleNo);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDescription);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemReference);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Version);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.CustomerSchedule.CreateDate.ToShortDateString());
            str.Append("</td>");
            var prevCreatedate = customerPlanLogList.Where(c => c.Flow == firstPlan.Flow && c.Item == firstPlan.Item);
            str.Append("<td>");
            str.Append(prevCreatedate != null && prevCreatedate.Count() > 0 ?prevCreatedate.FirstOrDefault().CustomerSchedule.CreateDate.ToShortDateString() : string.Empty);
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;
                var oldPlan = customerPlanLogList.Where(d => d.DateFrom == planByDateIndex.Key && d.Flow == firstPlan.Flow && d.Item == firstPlan.Item);
                if (oldPlan != null && oldPlan.Count()>0)
                {
                    if (oldPlan.First().Qty != qty)
                    {
                        str.Append(string.Format("<td style='background:orange' title='{0}'>", oldPlan.First().Qty.ToString("0.##")));
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
        this.list.InnerHtml = headStr+str.ToString();
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
    protected void rblSearchDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblSearchDateType.SelectedValue ==BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(7*30).ToString("yyyy-MM-dd");
        }
        else
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
        }
    }

    //private void ListTable(IList<CustomerPlan> customerPlanList)
    //{
    //    if (customerPlanList == null || customerPlanList.Count == 0)
    //    {
    //        this.list.InnerHtml = "没有查到符合条件的记录";
    //        this.ltlPlanVersion.Text = string.Empty;
    //        return;
    //    }
    //    var customerPlanListDic = customerPlanList.GroupBy(p => new { p.Flow, p.Item, p.PlanVersion })
    //       .ToDictionary(d => d.Key, d => d.ToList());

    //    List<CustomerPlanLog> customerPlanLogList = new List<CustomerPlanLog>();
    //    var hql = "select p from CustomerPlanLog as p where p.DateType=? and p.DateIndex>=? and p.DateIndex<=? and PlanVersion <> ? and p.Item=? and p.Flow=? ";
    //    foreach (var dic in customerPlanListDic)
    //    {
    //        var customerPlans = dic.Value;
    //        var paramList = new List<object> { (int)customerPlans.First().DateType, customerPlans.Min(d => d.DateIndex), customerPlans.Max(d => d.DateIndex), customerPlans.First().PlanVersion, customerPlans.First().Item, customerPlans.First().Flow };
    //        var rr = this.TheGenericMgr.FindAllWithCustomQuery<CustomerPlanLog>(hql, paramList.ToArray());
    //        if (rr != null && rr.Count > 0)
    //        {
    //            customerPlanLogList.AddRange(rr);
    //        }
    //    }

    //    var customerPlanLogDic = (customerPlanLogList ?? new List<CustomerPlanLog>()).GroupBy(p => new { DateIndex = p.DateIndexTo, p.Flow, p.Item })
    //        .ToDictionary(d => d.Key, d => d.OrderByDescending(dd=>dd.PlanVersion).First());

    //    var planByDateIndexs = customerPlanList.GroupBy(p => p.DateIndex).OrderBy(p => p.Key);
    //    var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

    //    StringBuilder str = new StringBuilder();
    //    str.Append(CopyString());
    //    //head
    //    var flowCode = this.tbFlow.Text.Trim();
    //    var dateType = customerPlanList.First().DateType;
    //    str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead><tr class='GVHeader'><th>行数</th><th>路线</th><th>物料号</th><th>物料描述</th><th>单位</th><th>版本号</th><th>本次导入时间</th><th>上次导入时间</th>");
    //    foreach (var planByDateIndex in planByDateIndexs)
    //    {
    //        str.Append("<th>");
    //        var dateIndex = planByDateIndex.Key;
    //        if (dateType == CodeMaster.TimeUnit.Day)
    //        {
    //            dateIndex = planByDateIndex.Key.Remove(0, 5);
    //        }
    //        //if (flowCode != string.Empty && this.CurrentUser.HasPermission("NewMrp_CustomerPlanToOrder"))
    //        //{
    //        //    str.Append(string.Format("<a href=\"javascript:__doPostBack('','jsPostback${0}${1}${2}')\">{3}</a>",
    //        //        (int)dateType, planByDateIndex.Key, flowCode, dateIndex));
    //        //}
    //        //else
    //        //{
    //            str.Append(dateIndex);
    //        //}
    //        str.Append("</th>");
    //    }
    //    str.Append("</tr></thead>");
    //    str.Append("<tbody>");
    //    int l = 0;
    //    foreach (var planByFlowItem in planByFlowItems)
    //    {
    //        var firstPlan = planByFlowItem.OrderByDescending(p=>p.LastModifyDate).First();
    //        var planDic = planByFlowItem.ToDictionary(d => d.DateIndex, d => d.Qty);
    //        l++;
    //        if (l % 2 == 0)
    //        {
    //            str.Append("<tr class='GVAlternatingRow'>");
    //        }
    //        else
    //        {
    //            str.Append("<tr class='GVRow'>");
    //        }
    //        str.Append("<td>");
    //        str.Append(l);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(planByFlowItem.Key.Flow);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(planByFlowItem.Key.Item);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(firstPlan.ItemFullDescription);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(firstPlan.Uom);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(firstPlan.PlanVersion);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(firstPlan.LastModifyDate);
    //        str.Append("</td>");
    //        var prevCreatedate = customerPlanLogList.Where(c =>  c.Flow == firstPlan.Flow && c.Item == firstPlan.Item).OrderByDescending(d=>d.CreateDate);
    //        str.Append("<td>");
    //        str.Append(prevCreatedate != null && prevCreatedate.Count() > 0 ? (DateTime?)prevCreatedate.FirstOrDefault().CreateDate : null);
    //        str.Append("</td>");
    //        foreach (var planByDateIndex in planByDateIndexs)
    //        {
    //            var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;
    //            var oldPlan = customerPlanLogDic.Keys.Contains(new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item }) ? customerPlanLogDic[new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item }] : null;
    //            if (oldPlan != null)
    //            {
    //                if (oldPlan.Qty != qty)
    //                {
    //                    str.Append(string.Format("<td style='background:orange' title='{0}'>", oldPlan.Qty.ToString("0.##")));
    //                }
    //                else
    //                {
    //                    str.Append("<td style='background:yellow'>");
    //                }
    //            }
    //            else
    //            {
    //                str.Append("<td>");
    //            }
    //            str.Append(qty.ToString("0.##"));
    //            str.Append("</td>");
    //        }
    //        str.Append("</tr>");
    //    }
    //    str.Append("</tbody></table>");
    //    this.list.InnerHtml = str.ToString();
    //}

    //private void ListTable(IList<CustomerPlan> customerPlanList, IList<CustomerPlanLog> customerPlanLogList)
    //{
    //    if (customerPlanList == null || customerPlanList.Count == 0)
    //    {
    //        this.list.InnerHtml = "没有查到符合条件的记录";
    //        this.ltlPlanVersion.Text = string.Empty;
    //        return;
    //    }
    //    var customerPlanLogDic = (customerPlanLogList ?? new List<CustomerPlanLog>()).GroupBy(p => new { DateIndex = p.DateIndexTo, p.Flow, p.Item })
    //        .ToDictionary(d => d.Key, d => d.First());

    //    var planByDateIndexs = customerPlanList.GroupBy(p => p.DateIndex).OrderBy(p => p.Key);
    //    var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

    //    StringBuilder str = new StringBuilder();
    //    str.Append(CopyString());
    //    //head
    //    var flowCode = this.tbFlow.Text.Trim();
    //    var dateType = customerPlanList.First().DateType;
    //    str.Append("<table border='1' class='GV' style='width:100%;border-collapse:collapse;'><thead><tr class='GVHeader'><th>行数</th><th>路线</th><th>物料号</th><th>物料描述</th><th>单位</th>");
    //    foreach (var planByDateIndex in planByDateIndexs)
    //    {
    //        str.Append("<th>");
    //        var dateIndex = planByDateIndex.Key;
    //        if (dateType == CodeMaster.TimeUnit.Day)
    //        {
    //            dateIndex = planByDateIndex.Key.Remove(0, 5);
    //        }
    //        if (flowCode != string.Empty && this.CurrentUser.HasPermission("NewMrp_CustomerPlanToOrder"))
    //        {
    //            str.Append(string.Format("<a href=\"javascript:__doPostBack('','jsPostback${0}${1}${2}')\">{3}</a>",
    //                (int)dateType, planByDateIndex.Key, flowCode, dateIndex));
    //        }
    //        else
    //        {
    //            str.Append(dateIndex);
    //        }
    //        str.Append("</th>");
    //    }
    //    str.Append("</tr></thead>");
    //    str.Append("<tbody>");
    //    int l = 0;
    //    foreach (var planByFlowItem in planByFlowItems)
    //    {
    //        var firstPlan = planByFlowItem.First();
    //        var planDic = planByFlowItem.ToDictionary(d => d.DateIndex, d => d.Qty);
    //        l++;
    //        if (l % 2 == 0)
    //        {
    //            str.Append("<tr class='GVAlternatingRow'>");
    //        }
    //        else
    //        {
    //            str.Append("<tr class='GVRow'>");
    //        }
    //        str.Append("<td>");
    //        str.Append(l);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(planByFlowItem.Key.Flow);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(planByFlowItem.Key.Item);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(firstPlan.ItemFullDescription);
    //        str.Append("</td>");
    //        str.Append("<td>");
    //        str.Append(firstPlan.Uom);
    //        str.Append("</td>");
    //        foreach (var planByDateIndex in planByDateIndexs)
    //        {
    //            //var qty = planDic.ValueOrDefault(planByDateIndex.Key);
    //            //var oldPlan = customerPlanLogDic.ValueOrDefault(new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item });
    //            var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : -99;
    //            var oldPlan = customerPlanLogDic.Keys.Contains(new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item })?customerPlanLogDic[new { DateIndex = planByDateIndex.Key, Flow = firstPlan.Flow, Item = firstPlan.Item }]:null;
    //            if (oldPlan != null)
    //            {
    //                if (oldPlan.Qty != qty)
    //                {
    //                    str.Append(string.Format("<td style='background:orange' title='{0}'>", oldPlan.Qty.ToString("0.##")));
    //                }
    //                else
    //                {
    //                    str.Append("<td style='background:yellow'>");
    //                }
    //            }
    //            else
    //            {
    //                str.Append("<td>");
    //            }
    //            str.Append(qty.ToString("0.##"));
    //            str.Append("</td>");
    //        }
    //        str.Append("</tr>");
    //    }
    //    str.Append("</tbody></table>");
    //    this.list.InnerHtml = str.ToString();
    //}

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

    protected void btnRunShipPlan_Click(object sender, EventArgs e)
    {
        try
        {
            TheMrpMgr.RunShipPlan(this.CurrentUser);
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
}