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
using System.Collections.Generic;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;

public partial class MasterData_Client_Search : SearchModuleBase
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

        if (actionParameter.ContainsKey("ClientId"))
        {
            this.tbClientId.Text = actionParameter["ClientId"];
        }
        if (actionParameter.ContainsKey("Description"))
        {
            this.tbDescription.Text = actionParameter["Description"];
        }
    }

    protected override void DoSearch()
    {
        string ClientId = this.tbClientId.Text.Trim();
        string Description = this.tbDescription.Text.Trim();
        bool isActive = this.cbIsActive.Checked;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Client));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Client)).SetProjection(Projections.Count("ClientId"));
            if (ClientId != string.Empty)
            {
                selectCriteria.Add(Expression.Like("ClientId", ClientId, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("ClientId", ClientId, MatchMode.Start));
            }

            if (Description != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Description", Description, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Description", Description, MatchMode.Start));
            }
            selectCriteria.Add(Expression.Eq("IsActive", isActive));
            selectCountCriteria.Add(Expression.Eq("IsActive", isActive));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
