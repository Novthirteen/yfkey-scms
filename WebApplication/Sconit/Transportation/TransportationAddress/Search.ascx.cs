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

public partial class Transportation_TransportationAddress_Search : SearchModuleBase
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
        if (actionParameter.ContainsKey("Address"))
        {
            this.tbAddress.Text = actionParameter["Address"];
        }
    }

    protected override void DoSearch()
    {
        string address = this.tbAddress.Text != string.Empty ? this.tbAddress.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationAddress));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationAddress))
                .SetProjection(Projections.Count("Id"));
            if (address != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Address", address, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Address", address, MatchMode.Start));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
