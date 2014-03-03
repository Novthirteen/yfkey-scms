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
using System.Collections.Generic;
using com.Sconit.Entity.MRP;

public partial class Schedule_DemandSchedule_RequiredQtyList : ModuleBase
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    public void InitParamater(string itemCode, DateTime date, DateTime effDate, bool isFlow, string flowOrLoc, bool isWinTime)
    {

        IList<MrpShipPlan> mrpShipPlansList = TheMrpShipPlanMgr.GetMrpShipPlans((isFlow ? flowOrLoc : null), (!isFlow ? flowOrLoc : null), itemCode, effDate, date, null);


        this.GV_List.DataSource = mrpShipPlansList;
        this.GV_List.DataBind();

    }

}
