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

public partial class NewMrp_ShipPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var hql = "select s from ShipPlanDet as s where 1=1 ";
        var paramList = new List<object>();
        if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        {
            hql += " and s.Flow =? ";
            paramList.Add(this.tbFlow.Text.Trim());
        }
        
        DateTime startTime = DateTime.Today;
        if (!string.IsNullOrEmpty(this.tbStartDate.Text.Trim()))
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
            hql += " and s.WindowTime>=? ";
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
            hql += " and s.WindowTime<=? ";
            paramList.Add(endTime.Date);
        }
        else
        {
            DateTime endTime = startTime.AddDays(14);
            hql += " and s.WindowTime<=? ";
            paramList.Add(endTime.Date);
        }
       
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            hql += " and s.Item =? ";
            paramList.Add(this.tbItemCode.Text.Trim());
        }

        var flowMaxVersionDic = new Dictionary<string, int>();
        var shipPlanDetList = this.TheGenericMgr.FindAllWithCustomQuery<ShipPlanDet>(hql, paramList.ToArray()) ?? new List<ShipPlanDet>();
        ListTable(shipPlanDetList);
    }

    private void ListTable(IList<ShipPlanDet> shipPlanDetList)
    {
        if (shipPlanDetList == null || shipPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }

        var planByDateIndexs = shipPlanDetList.GroupBy(p => p.WindowTime).OrderBy(p => p.Key);
        var planByFlowItems = shipPlanDetList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item,p.LocFrom,p.LocTo });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        var flowCode = this.tbFlow.Text.Trim();
        string headStr = CopyString();
        str.Append("<thead><tr class='GVHeader'><th>行数</th><th>路线</th><th>物料号</th><th>物料描述</th><th>客户零件号</th><th>来源库位</th><th>目的库位</th>");
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
            var planDic = planByFlowItem.GroupBy(d=>d.WindowTime).ToDictionary(d => d.Key, d => d.Sum(q=>q.ShipQty));
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
            str.Append("<td>");
            str.Append(firstPlan.LocFrom);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.LocTo);
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

    private string CopyString()
    {
                 //底色黄色重新导入无改动,橙色重新导入并有改动

        return @"<a type='text/html' onclick='copyHtml()' href='#' id='copy'>复制</a>
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

    protected void btnExport_Click(object sender, EventArgs e)
    {

    }
}