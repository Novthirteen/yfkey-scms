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
    public static string currentRefScheduleNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

        if (!IsPostBack)
        {
            this.tbFlow.Text = string.Empty;
        }

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
       
    }

    #region   明细查询
    public void GetView(string refScheduleNo)
    {
        this.tbFlow.Text = string.Empty;
        this.list.InnerHtml = "";
        currentRefScheduleNo = refScheduleNo;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var hql = "select c from CustomerScheduleDetail as c where 1=1 ";
        var paramList = new List<object>();
        hql += " and c.ReferenceScheduleNo=? ";
        paramList.Add(currentRefScheduleNo);
        if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        {
            hql += " and c.Flow =? ";
            paramList.Add(this.tbFlow.Text.Trim());
        }
       
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            hql += " and c.Item =? ";
            paramList.Add(this.tbItemCode.Text.Trim());
        }

        var customerPlanList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerScheduleDetail>(hql, paramList.ToArray()) ?? new List<CustomerScheduleDetail>();
       
        ListTable(customerPlanList);
    }

    private void ListTable(IList<CustomerScheduleDetail> customerPlanList)
    {
        if (customerPlanList == null || customerPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }

        var planByDateIndexs = customerPlanList.GroupBy(p => p.DateFrom).OrderBy(p => p.Key);
        var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = string.Empty;
        //CopyString();
        str.Append("<thead><tr class='GVHeader'><th>序号</th><th>路线</th><th>版本号</th><th>物料号</th><th>物料描述</th><th>客户零件号</th>");
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
            var planDic = planByFlowItem.GroupBy(d => d.DateFrom).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
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
            str.Append(firstPlan.Flow);
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
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;

                str.Append("<td>");
                str.Append(qty.ToString("0.##"));
                str.Append("</td>");
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


    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }
}