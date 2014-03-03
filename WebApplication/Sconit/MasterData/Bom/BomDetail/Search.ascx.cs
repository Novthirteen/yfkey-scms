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
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

public partial class MasterData_Bom_BomDetail_Search : SearchModuleBase
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

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected override void DoSearch()
    {
        string parcode = this.tbParCode.Text.Trim() != string.Empty ? this.tbParCode.Text.Trim() : string.Empty;
        string compcode = this.tbCompCode.Text.Trim() != string.Empty ? this.tbCompCode.Text.Trim() : string.Empty;

        if (parcode == string.Empty && compcode == string.Empty)
        {
            ShowErrorMessage("MasterData.Bom.Condition.Required");
            return;
        }
        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(BomDetail));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(BomDetail))
                .SetProjection(Projections.Count("Id"));

            if (parcode != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Bom.Code", parcode, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Bom.Code", parcode, MatchMode.Start));
            }
            if (compcode != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Item.Code", compcode, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Item.Code", compcode, MatchMode.Start));
            }

            #endregion

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("ParCode"))
        {
            this.tbParCode.Text = actionParameter["ParCode"];
        }
        if (actionParameter.ContainsKey("CompCode"))
        {
            this.tbCompCode.Text = actionParameter["CompCode"];
        }
    }
}
