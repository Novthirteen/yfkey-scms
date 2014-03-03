using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;

public partial class Hu_Inbound_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucUpload.ListResultEvent += new System.EventHandler(this.ListResult_Render);
    }

    private void ListResult_Render(object sender, EventArgs e)
    {
        //this.ucResult.InitPageParameter((IList<HuDetail>)sender);
        this.ucResult.Visible = true;
    }
}
