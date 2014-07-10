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
using com.Sconit.Entity.MRP;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using System.Collections.Generic;

public partial class NewMrp_ProductionPlan_ShowErrorMsg : MainModuleBase
{
    public EventHandler BackEvent;

    public static IList<RunProductionPlanLog> tempPlanLogList;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void InitPageParameter(IList<RunProductionPlanLog> runProductionPlanLogs)
    {
        tempPlanLogList = runProductionPlanLogs;
        this.GV_List.DataSource = tempPlanLogList.Where(r => r.Msg != null).ToList();
        this.GV_List.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GV_List.PageIndex = e.NewPageIndex;
        this.GV_List.DataSource = tempPlanLogList.Where(r => r.Msg != null).ToList();
        this.GV_List.DataBind();
    }
}
