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
using com.Sconit.Web;
using System.Collections.Generic;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

public partial class ManageSconit_LeanEngine_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string flowCode = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            string flowType = this.ddlFlowType.SelectedValue;

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Flow));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Flow))
                .SetProjection(Projections.ProjectionList()
               .Add(Projections.Count("Code")));

            selectCriteria.Add(Expression.Eq("IsAutoCreate", true));
            selectCountCriteria.Add(Expression.Eq("IsAutoCreate", true));

            if (flowCode != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Code", flowCode));
                selectCountCriteria.Add(Expression.Eq("Code", flowCode));
            }
            selectCriteria.Add(Expression.Eq("Type", flowType));
            selectCountCriteria.Add(Expression.Eq("Type", flowType));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Flow"))
        {
            this.tbFlow.Text = actionParameter["Flow"];
        }
        if (actionParameter.ContainsKey("FlowType"))
        {
            this.ddlFlowType.SelectedValue = actionParameter["FlowType"];
        }
    }
}
