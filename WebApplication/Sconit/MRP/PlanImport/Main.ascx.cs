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
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

public partial class MRP_PlanImport_Main : MainModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucImport.ImportEvent += new System.EventHandler(this.Import_Render);

        if (!IsPostBack)
        {
            this.ucImport.ModuleType = this.ModuleType;
        }
    }

    //The event handler when user click button "Search" button
    void Import_Render(object sender, EventArgs e)
    {
        this.ucPreview.Visible = true;
        this.ucPreview.InitPageParameter((IList<OrderHead>)((object[])sender)[0], this.ModuleType);
    }
}
