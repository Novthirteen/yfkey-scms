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
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;


public partial class Inventory_Repack_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;
    public event EventHandler RepackHuEvent;


    public string RepackType
    {
        get
        {
            return (string)ViewState["RepackType"];
        }
        set
        {
            ViewState["RepackType"] = value;
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblRepackNo.Text = RepackHelper.GetRepackLabel(this.RepackType, true);
            this.btnRepackHu.Visible = this.RepackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK;
        }


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (NewEvent != null)
        {
            NewEvent(sender, e);
        }
    }

    protected void btnRepackHu_Click(object sender, EventArgs e)
    {
        if (RepackHuEvent != null)
        {
            RepackHuEvent(sender, e);
        }
    }


    protected override void DoSearch()
    {
        string repackNo = this.tbRepackNo.Text != string.Empty ? this.tbRepackNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Repack));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Repack)).SetProjection(Projections.ProjectionList()
               .Add(Projections.Count("RepackNo")));

        selectCriteria.Add(Expression.Eq("Type", this.RepackType));
        selectCountCriteria.Add(Expression.Eq("Type", this.RepackType));

        if (repackNo != string.Empty)
        {
            selectCriteria.Add(Expression.Like("RepackNo", repackNo));
            selectCountCriteria.Add(Expression.Like("RepackNo", repackNo));
        }

        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }

        if (SearchEvent != null)
        {
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }


}
