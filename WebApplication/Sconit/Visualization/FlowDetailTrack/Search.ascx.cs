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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity.Dss;


public partial class Visualization__FlowDetailTrack_Search : ModuleBase
{

    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (SearchEvent != null)
        {
            DoSearch();
        }

    }
    private void DoSearch()
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(FlowDetailTrack));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(FlowDetailTrack)).SetProjection(Projections.Count("Id"));

        if (this.tbFlow.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Like("Flow", this.tbFlow.Text.Trim(), MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("Flow", this.tbFlow.Text.Trim(), MatchMode.Start));
        }
        if (this.tbFlow.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Like("Item", this.tbItem.Text.Trim(), MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("Item", this.tbItem.Text.Trim(), MatchMode.Start));
        }

        SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);

    }

}
