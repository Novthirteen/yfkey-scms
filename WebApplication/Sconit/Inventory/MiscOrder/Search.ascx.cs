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
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Entity.Exception;


public partial class MasterData_MiscOrder_Search : SearchModuleBase
{
    public event EventHandler EditEvent;
    public event EventHandler BackEvent;
    public event EventHandler NewEvent;
    public event EventHandler SearchEvent;
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {

        this.tbMiscOrderRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
     
        
    }
    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo

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
        string code = this.tbMiscOrderCode.Text != string.Empty ? this.tbMiscOrderCode.Text.Trim() : string.Empty;
        string type = this.ModuleType;

        string tbRegion = this.tbMiscOrderRegion.Text != string.Empty ? this.tbMiscOrderRegion.Text.Trim() : string.Empty;
        string tbLocation = this.tbMiscOrderLocation.Text != string.Empty ? this.tbMiscOrderLocation.Text.Trim() : string.Empty;
        string tbEffectDate = this.tbMiscOrderEffectDate.Text != string.Empty ? this.tbMiscOrderEffectDate.Text.Trim() : string.Empty;
        string tbStartDate = this.startDate.Text != string.Empty ? this.startDate.Text.Trim() : string.Empty;
        string tbEndDate = this.endDate.Text != string.Empty ? this.endDate.Text.Trim() : string.Empty;
        string subjectCode = this.tbSubjectCode.Text != string.Empty ? this.tbSubjectCode.Text.Trim() : string.Empty;
        string costCenterCode = this.tbCostCenterCode.Text != string.Empty ? this.tbCostCenterCode.Text.Trim() : string.Empty;



        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(MiscOrder));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(MiscOrder))
                .SetProjection(Projections.Count("OrderNo"));
            selectCriteria.CreateAlias("Location", "l");
            selectCountCriteria.CreateAlias("Location", "l");

            selectCriteria.CreateAlias("SubjectList", "s");
            selectCountCriteria.CreateAlias("SubjectList", "s");

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("OrderNo", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("OrderNo", code, MatchMode.Start));
            }
            if (type != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Type", type, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Type", type, MatchMode.Start));

            }
            if (tbRegion != string.Empty)
            {
                selectCriteria.Add(Expression.Like("l.Region.Code", tbRegion, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("l.Region.Code", tbRegion, MatchMode.Start));

            }
            if (tbLocation != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Location.Code", tbLocation, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Location.Code", tbLocation, MatchMode.Start));

            }
         
            if (tbEffectDate != string.Empty)
            {
                DateTime tmpEffectDate = DateTime.Parse(tbEffectDate);
                selectCriteria.Add(Expression.Eq("EffectiveDate", tmpEffectDate));
                selectCountCriteria.Add(Expression.Eq("EffectiveDate", tmpEffectDate));
            }

            if (tbStartDate != string.Empty)
            {
                DateTime tmpStartDate = DateTime.Parse(tbStartDate);
                selectCriteria.Add(Expression.Gt("CreateDate", tmpStartDate));
                selectCountCriteria.Add(Expression.Gt("CreateDate", tmpStartDate));

            }
            if (tbEndDate != string.Empty)
            {
                DateTime tmpEndDate = DateTime.Parse(tbEndDate);
                selectCriteria.Add(Expression.Lt("CreateDate", tmpEndDate));
                selectCountCriteria.Add(Expression.Lt("CreateDate", tmpEndDate));
            }

            if (costCenterCode != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("s.CostCenterCode", costCenterCode));
                selectCountCriteria.Add(Expression.Eq("s.CostCenterCode", costCenterCode));
            }
            if (subjectCode != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("s.SubjectCode", subjectCode));
                selectCountCriteria.Add(Expression.Eq("s.SubjectCode", subjectCode));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }
}
