using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;


public partial class Inventory_PrintHu_FlowDetailList : ModuleBase
{
   
    public void InitPageParameter(IList<FlowDetail> flowDetailList)
    {
        this.GV_List.DataSource = flowDetailList;
        this.GV_List.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

}
