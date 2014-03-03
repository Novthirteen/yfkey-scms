using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Reports_PassRate_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string flow = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
            string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
            string shift = this.tbShift.Text.Trim() != string.Empty ? this.tbShift.Text.Trim() : string.Empty;
            string item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : string.Empty;

            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderDetailView));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(OrderDetailView))
                .SetProjection(Projections.Count("Id"));

            selectCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            selectCountCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));

            //区域权限
            SecurityHelper.SetRegionSearchCriteria(selectCriteria, selectCountCriteria, "PartyTo.Code", this.CurrentUser.Code);
            //订单状态
            OrderHelper.SetActiveOrderStatusCriteria(selectCriteria, "Status");
            OrderHelper.SetActiveOrderStatusCriteria(selectCountCriteria, "Status");

            if (flow != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Flow", flow));
                selectCountCriteria.Add(Expression.Eq("Flow", flow));
            }
            if (region != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("PartyTo.Code", region));
                selectCountCriteria.Add(Expression.Eq("PartyTo.Code", region));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("EffDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("EffDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Lt("EffDate", DateTime.Parse(endDate).AddDays(1)));
                selectCountCriteria.Add(Expression.Lt("EffDate", DateTime.Parse(endDate).AddDays(1)));
            }
            if (shift != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Shift.Code", shift));
                selectCountCriteria.Add(Expression.Eq("Shift.Code", shift));
            }
            if (item != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Item.Code", item, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Item.Code", item, MatchMode.Start));
            }

            #endregion

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Flow"))
        {
            this.tbFlow.Text = actionParameter["Flow"];
        }
        if (actionParameter.ContainsKey("Region"))
        {
            this.tbRegion.Text = actionParameter["Region"];
        }
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
        if (actionParameter.ContainsKey("Shift"))
        {
            this.tbShift.Text = actionParameter["Shift"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
    }
}
