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

public partial class Transportation_Vehicle_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbCarrier.ServiceParameter = "string:" + this.CurrentUser.Code;
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
        if (actionParameter.ContainsKey("Carrier"))
        {
            this.tbCarrier.Text = actionParameter["Carrier"];
        }
        if (actionParameter.ContainsKey("Type"))
        {
            this.tbCarrier.Text = actionParameter["Type"];
        }
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string carrier = this.tbCarrier.Text != string.Empty ? this.tbCarrier.Text.Trim() : string.Empty;
        string type = this.ddlType.SelectedIndex != -1 ? this.ddlType.SelectedValue : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Vehicle));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Vehicle))
                .SetProjection(Projections.Count("Code"));

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
            }

            if (carrier != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Carrier.Code", carrier));
                selectCountCriteria.Add(Expression.Eq("Carrier.Code", carrier));
            }

            DetachedCriteria[] carrierCrieteria = SecurityHelper.GetCarrierPermissionCriteria(this.CurrentUser.Code);
            selectCriteria.Add(
                Expression.Or(
                  Expression.Or(
                      Subqueries.PropertyIn("Carrier.Code", carrierCrieteria[0]),
                      Subqueries.PropertyIn("Carrier.Code", carrierCrieteria[1])
                                ),
                      Expression.IsNull("Carrier.Code")
                              )
                );

            selectCountCriteria.Add(
                Expression.Or(
                  Expression.Or(
                      Subqueries.PropertyIn("Carrier.Code", carrierCrieteria[0]),
                      Subqueries.PropertyIn("Carrier.Code", carrierCrieteria[1])
                                ),
                      Expression.IsNull("Carrier.Code")
                              )
                );

            if (type != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Type", type));
                selectCountCriteria.Add(Expression.Eq("Type", type));
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
