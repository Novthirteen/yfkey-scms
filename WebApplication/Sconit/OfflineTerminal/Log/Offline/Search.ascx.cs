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

public partial class MasterData_Client_Log_Offline_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            IList<Client> clientList = TheClientMgr.GetAllClient();
            this.ddlClientId.DataSource = clientList;
            this.ddlClientId.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

        if (actionParameter.ContainsKey("ClientId"))
        {
            this.ddlClientId.Text = actionParameter["ClientId"];
        }
        if (actionParameter.ContainsKey("OrderType"))
        {
            this.ddlOrderType.Text = actionParameter["OrderType"];
        }
    }

    protected override void DoSearch()
    {
        string ClientId = this.ddlClientId.SelectedValue;
        string OrderType = this.ddlOrderType.SelectedValue;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ClientOrderHead));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ClientOrderHead)).SetProjection(Projections.Count("Id"));
            if (ClientId != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Client.ClientId", ClientId, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Client.ClientId", ClientId, MatchMode.Start));
            }
            if (OrderType != string.Empty)
            {
                selectCriteria.Add(Expression.Like("OrderType", OrderType, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("OrderType", OrderType, MatchMode.Start));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

}
