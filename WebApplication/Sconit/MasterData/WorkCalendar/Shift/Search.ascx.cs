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

public partial class MasterData_WorkCalendar_Shift_Search : SearchModuleBase
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
        string shiftCode = this.tbCode.Text.Trim() != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string shiftName = this.tbShiftName.Text.Trim() != string.Empty ? this.tbShiftName.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Shift));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Shift))
                .SetProjection(Projections.Count("Code"));

            if (shiftCode != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", shiftCode, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Code", shiftCode, MatchMode.Start));
            }
            if (shiftName != string.Empty)
            {
                selectCriteria.Add(Expression.Like("ShiftName", shiftName, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("ShiftName", shiftName, MatchMode.Start));
            }
            #endregion

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
        if (actionParameter.ContainsKey("ShiftName"))
        {
            this.tbShiftName.Text = actionParameter["ShiftName"];
        }
    }
}
