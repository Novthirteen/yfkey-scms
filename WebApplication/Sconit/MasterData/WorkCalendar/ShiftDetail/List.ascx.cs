using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

public partial class MasterData_WorkCalendar_ShiftDetail_List : ListModuleBase
{
    public EventHandler EditEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ShiftDetail));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ShiftDetail))
            .SetProjection(Projections.Count("Id"));

        if (code != null && code.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Shift.Code", code));
            selectCountCriteria.Add(Expression.Eq("Shift.Code", code));

            this.SetSearchCriteria(selectCriteria, selectCountCriteria);
            this.UpdateView();
        }
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string code = ((LinkButton)sender).CommandArgument;
        try
        {
            TheShiftDetailMgr.DeleteShiftDetail(int.Parse(code));
            ShowSuccessMessage("MasterData.WorkCalendar.Delete.Successfully");
            UpdateView();
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.WorkCalendar.Delete.Failed");
        }
    }
}
