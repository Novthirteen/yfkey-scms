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
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;

public partial class Transportation_TransportationRoute_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
        if (actionParameter.ContainsKey("ShipFrom"))
        {
            this.tbShipFrom.Text = actionParameter["ShipFrom"];
        }
        if (actionParameter.ContainsKey("ShipTo"))
        {
            this.tbShipTo.Text = actionParameter["ShipTo"];
        }
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string shipFrom = this.tbShipFrom.Text != string.Empty ? this.tbShipFrom.Text.Trim() : string.Empty;
        string shipTo = this.tbShipTo.Text != string.Empty ? this.tbShipTo.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationRoute));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationRoute))
                .SetProjection(Projections.Count("Code"));
            IDictionary<string, string> alias = new Dictionary<string, string>();

            selectCriteria.CreateAlias("ShipFrom", "sf");
            selectCriteria.CreateAlias("ShipTo", "st");
            selectCountCriteria.CreateAlias("ShipFrom", "sf");
            selectCountCriteria.CreateAlias("ShipTo", "st");
            alias.Add("ShipFrom", "sf");
            alias.Add("ShipTo", "st");

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
            }

            if (shipFrom != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("sf.Address", shipFrom));
                selectCountCriteria.Add(Expression.Eq("sf.Address", shipFrom));
            }

            if (shipTo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("st.Address", shipTo));
                selectCountCriteria.Add(Expression.Eq("st.Address", shipTo));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria, alias }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
