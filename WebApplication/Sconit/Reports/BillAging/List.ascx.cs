using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;

public partial class Reports_BillAging_List : ListModuleBase
{
    public event EventHandler DetailEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.GV_List.Columns[1].HeaderText = "${Reports.BillAging.PartyFrom}";
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.GV_List.Columns[1].HeaderText = "${Reports.BillAging.PartyTo}";
            }
        }
    }

    private void OnDetailClick(int id, int range)
    {
        BillAgingView billAgingView = TheBillAgingViewMgr.LoadBillAgingView(id);

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ActingBillView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ActingBillView))
            .SetProjection(Projections.Count("Id"));

        IDictionary<string, string> alias = new Dictionary<string, string>();
        selectCriteria.CreateAlias("BillAddress", "ba");
        selectCountCriteria.CreateAlias("BillAddress", "ba");
        selectCriteria.CreateAlias("Item", "i");
        selectCountCriteria.CreateAlias("Item", "i");

        alias.Add("BillAddress", "ba");
        alias.Add("Item", "i");

        #region 账龄区间处理
        DateTime dateTimeNow = DateTime.Now;
        switch (range)
        {
            case 1:
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-30)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-30)));
                break;
            case 2:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-30)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-60)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-30)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-60)));
                break;
            case 3:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-60)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-90)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-60)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-90)));
                break;
            case 4:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-90)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-120)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-90)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-120)));
                break;
            case 5:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-120)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-150)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-120)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-150)));
                break;
            case 6:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-150)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-180)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-150)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-180)));
                break;
            case 7:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-180)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-210)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-180)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-210)));
                break;
            case 8:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-210)));
                selectCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-360)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-210)));
                selectCountCriteria.Add(Expression.Ge("EffectiveDate", dateTimeNow.AddDays(-360)));
                break;
            case 9:
                selectCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-360)));
                selectCountCriteria.Add(Expression.Lt("EffectiveDate", dateTimeNow.AddDays(-360)));
                break;
        }
        #endregion

        selectCriteria.Add(Expression.Eq("i.Code", billAgingView.Item.Code));
        selectCountCriteria.Add(Expression.Eq("i.Code", billAgingView.Item.Code));
        selectCriteria.Add(Expression.Eq("ba.Code", billAgingView.BillAddress.Code));
        selectCountCriteria.Add(Expression.Eq("ba.Code", billAgingView.BillAddress.Code));

        DetailEvent((new object[] { selectCriteria, selectCountCriteria, alias }), null);
    }

    protected void lbtnDetail1_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 1);
    }

    protected void lbtnDetail2_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 2);
    }

    protected void lbtnDetail3_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 3);
    }

    protected void lbtnDetail4_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 4);
    }

    protected void lbtnDetail5_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 5);
    }

    protected void lbtnDetail6_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 6);
    }

    protected void lbtnDetail7_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 7);
    }

    protected void lbtnDetail8_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 8);
    }

    protected void lbtnDetail9_Click(object sender, EventArgs e)
    {
        OnDetailClick(int.Parse(((LinkButton)sender).CommandArgument), 9);
    }
}
