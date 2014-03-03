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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;


public partial class MasterData_Flow_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

        if (actionParameter.ContainsKey("Code"))
        {
            this.tbFlow.Text = actionParameter["Code"];
        }
        if (actionParameter.ContainsKey("LocFrom"))
        {
            this.tbLocFrom.Text = actionParameter["LocFrom"];
        }
        if (actionParameter.ContainsKey("LocTo"))
        {
            this.tbLocTo.Text = actionParameter["LocTo"];
        }
        if (actionParameter.ContainsKey("PartyFrom"))
        {
            this.tbPartyFrom.Text = actionParameter["PartyFrom"];
        }
        if (actionParameter.ContainsKey("Strategy"))
        {
            this.ddlStrategy.SelectedValue = actionParameter["Strategy"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true";
        this.tbFlow.DataBind();
        this.tbPartyFrom.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION + ",string:" + this.CurrentUser.Code;
        this.tbPartyFrom.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }
    protected override void DoSearch()
    {
        string code = this.tbFlow.Text != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
        string partyFrom = this.tbPartyFrom.Text != string.Empty ? this.tbPartyFrom.Text.Trim() : string.Empty;
        string locFrom = this.tbLocFrom.Text != string.Empty ? this.tbLocFrom.Text.Trim() : string.Empty;
        string locTo = this.tbLocTo.Text != string.Empty ? this.tbLocTo.Text.Trim() : string.Empty;
        string strategy = this.ddlStrategy.SelectedIndex != -1 ? this.ddlStrategy.SelectedValue : string.Empty;
        if (SearchEvent != null)
        {
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Flow));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Flow))
                .SetProjection(Projections.ProjectionList()
               .Add(Projections.Count("Code")));
            selectCriteria.CreateAlias("PartyFrom", "pf");
            selectCountCriteria.CreateAlias("PartyFrom", "pf");

            #region partyFrom
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, (this.tbPartyFrom != null ? this.tbPartyFrom.Text : null), BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION, this.CurrentUser.Code);
            #endregion

            #region flowType

            selectCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            selectCountCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            #endregion

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Code", code));
                selectCountCriteria.Add(Expression.Eq("Code", code));
            }

            if (locFrom != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("LocationFrom.Code", locFrom));
                selectCountCriteria.Add(Expression.Eq("LocationFrom.Code", locFrom));
            }
            if (locTo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("LocationTo.Code", locTo));
                selectCountCriteria.Add(Expression.Eq("LocationTo.Code", locTo));
            }
            if (strategy != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("FlowStrategy", strategy));
                selectCountCriteria.Add(Expression.Eq("FlowStrategy", strategy));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
