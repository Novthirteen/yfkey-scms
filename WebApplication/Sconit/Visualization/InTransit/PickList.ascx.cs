using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Utility;

public partial class Visualization_InTransit_PickList : ModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(string itemCode, string locationCode)
    {
        string[] statusArray = new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS };

        IList<PickListResult> pickListResults = ThePickListResultMgr.GetPickListResult(locationCode, itemCode, null, null, statusArray);
        
        this.GV_List.DataSource = pickListResults;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }
}
