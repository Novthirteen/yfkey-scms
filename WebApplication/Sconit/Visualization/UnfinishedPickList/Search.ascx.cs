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


public partial class Visualization_UnfinishedPickList_Search : ModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            DoSearch(true);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (SearchEvent != null)
        {
            DoSearch(false);
        }

    }
    private void DoSearch(bool isExport)
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PickList));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(PickList)).SetProjection(Projections.Count("PickListNo"));

        selectCriteria.Add(Expression.Eq("Status",BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
        selectCountCriteria.Add(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));

        selectCriteria.Add(Expression.Eq("PartyFrom.Code", BusinessConstants.SYSTEM_REGION_RW));
        selectCountCriteria.Add(Expression.Eq("PartyFrom.Code", BusinessConstants.SYSTEM_REGION_RW));

        if (this.tbPartyTo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Like("PartyTo.Code", this.tbPartyTo.Text.Trim(), MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("PartyTo.Code", this.tbPartyTo.Text.Trim(), MatchMode.Start));
        }

        if (isExport)
        {
            ExportEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
        else
        {
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

}
