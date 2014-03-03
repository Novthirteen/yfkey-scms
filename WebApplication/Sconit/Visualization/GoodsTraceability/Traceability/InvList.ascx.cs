using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

public partial class Visualization_GoodsTraceability_Traceability_InvList : ListModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string huId)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationLotDetail));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(LocationLotDetail))
            .SetProjection(Projections.Count("Id"));

        selectCriteria.Add(Expression.Eq("Hu.HuId", huId));
        selectCountCriteria.Add(Expression.Eq("Hu.HuId", huId));

        decimal qty = 0;
        selectCriteria.Add(Expression.Gt("Qty", qty));
        selectCountCriteria.Add(Expression.Gt("Qty", qty));

        this.SetSearchCriteria(selectCriteria, selectCountCriteria);
        this.UpdateView();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }
}
