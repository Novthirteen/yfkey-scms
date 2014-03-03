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

public partial class Transportation_TransportPriceList_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
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
        if (actionParameter.ContainsKey("Party"))
        {
            this.tbParty.Text = actionParameter["Party"];
        }
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string party = this.tbParty.Text != string.Empty ? this.tbParty.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportPriceList));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportPriceList))
                .SetProjection(Projections.Count("Code"));
            IDictionary<string, string> alias = new Dictionary<string, string>();

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
            }

            if (party != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Party.Code", party));
                selectCountCriteria.Add(Expression.Eq("Party.Code", party));
            }
            
            //DetachedCriteria[] partyCrieteria = SecurityHelper.GetPartyPermissionCriteria(this.CurrentUser.Code,
            //    BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION,
            //    BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CARRIER);

            //selectCriteria.Add(
            //    Expression.Or(
            //      Expression.Or(
            //          Subqueries.PropertyIn("Party.Code", partyCrieteria[0]),
            //          Subqueries.PropertyIn("Party.Code", partyCrieteria[1])
            //                    ),
            //          Expression.IsNull("Party.Code")
            //                  )
            //    );

            //selectCountCriteria.Add(
            //    Expression.Or(
            //      Expression.Or(
            //          Subqueries.PropertyIn("Party.Code", partyCrieteria[0]),
            //          Subqueries.PropertyIn("Party.Code", partyCrieteria[1])
            //                    ),
            //          Expression.IsNull("Party.Code")
            //                  )
            //    );

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
