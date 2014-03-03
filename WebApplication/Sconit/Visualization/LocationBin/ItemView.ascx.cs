using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;

public partial class Visualization_LocationBin_ItemView : ListModuleBase
{
    public EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string location, string bin, string item)
    {
        this.Visible = true;

        #region DetachedCriteria
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationBinItemDetail));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(LocationBinItemDetail))
            .SetProjection(Projections.Count("Id"));

        if (location != null && location != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Location", location));
            selectCountCriteria.Add(Expression.Eq("Location", location));
        }
        if (bin != null && bin != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Bin", bin));
            selectCountCriteria.Add(Expression.Eq("Bin", bin));
        }
        else
        {
            selectCriteria.Add(Expression.IsNull("Bin"));
            selectCountCriteria.Add(Expression.IsNull("Bin"));
        }

        if (item != null && item != string.Empty)
        {
            selectCriteria.Add(Expression.Like("Item", item, MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("Item", item, MatchMode.Start));
        }

        #endregion

        this.SetSearchCriteria(selectCriteria, selectCountCriteria);
        this.UpdateView();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        //if (BackEvent != null)
        //{
        //    BackEvent(this, null);
        //}
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }
}
