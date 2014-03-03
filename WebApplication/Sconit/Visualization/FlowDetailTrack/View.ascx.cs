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
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData.Impl;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;

public partial class Visualization_FlowDetailTrack_View : ModuleBase
{
    public event EventHandler BackEvent;

    public void InitPageParameter(int id)
    {
        this.ODS_FlowDetailTrack.SelectParameters["id"].DefaultValue = id.ToString();
        this.FV_FlowDetailTrack.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void FV_FlowDetailTrack_DataBound(object sender, EventArgs e)
    {
       

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

  

}
