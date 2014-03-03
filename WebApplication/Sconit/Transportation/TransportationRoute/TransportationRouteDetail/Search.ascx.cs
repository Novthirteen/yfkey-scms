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
using com.Sconit.Entity.Transportation;


public partial class Transportation_TransportationRoute_TransportationRouteDetail_Search : SearchModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string tAddress = this.tbTAddress.Text != string.Empty ? this.tbTAddress.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationRouteDetail));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationRouteDetail))
                .SetProjection(Projections.Count("Id"));

            selectCriteria.CreateAlias("TransportationAddress", "ta");
            selectCountCriteria.CreateAlias("TransportationAddress", "ta"); 

            if (tAddress != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("ta.Address", tAddress));
                selectCountCriteria.Add(Expression.Eq("ta.Address", tAddress));
            }

            selectCriteria.Add(Expression.Eq("TransportationRoute.Code", this.lbCurrentTransportationRoute.Text));
            selectCountCriteria.Add(Expression.Eq("TransportationRoute.Code", this.lbCurrentTransportationRoute.Text));

            selectCriteria.AddOrder(Order.Asc("Sequence"));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("TRoute"))
        {
            this.lbCurrentTransportationRoute.Text = actionParameter["TRoute"];
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    public void UpdateView()
    {
        this.btnSearch_Click(this, null);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
