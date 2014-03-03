using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;

public partial class Inventory_Stocktaking_CycleCountResultList : ModuleBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }

  
    public void InitPageParameter(CycleCount cycleCount)
    {
        IList<CycleCountResult> cycleCountResultList = TheCycleCountMgr.CalcCycleCount(cycleCount.Code, true, true, true , null, null);
        this.GV_List.DataSource = cycleCountResultList;
        this.GV_List.DataBind();
    }

 
}
