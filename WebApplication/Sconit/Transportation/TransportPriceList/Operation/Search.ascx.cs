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


public partial class Transportation_TransportPriceList_Operation_Search : SearchModuleBase
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
        string item = this.tbItem.Text != string.Empty ? this.tbItem.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportPriceListDetail));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportPriceListDetail))
                .SetProjection(Projections.Count("Id"));

            selectCriteria.Add(Expression.Eq("TransportPriceList.Code", this.lbCurrentTransportPriceList.Text));
            selectCountCriteria.Add(Expression.Eq("TransportPriceList.Code", this.lbCurrentTransportPriceList.Text));

            selectCriteria.Add(Expression.Eq("Type", BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION));
            selectCountCriteria.Add(Expression.Eq("Type", BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION));

            if (item != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Item.Code", item));
                selectCountCriteria.Add(Expression.Eq("Item.Code", item));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("TransportPriceList"))
        {
            this.lbCurrentTransportPriceList.Text = actionParameter["TransportPriceList"];
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
