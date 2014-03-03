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
using com.Sconit.Entity;
using System.Collections.Generic;

public partial class Visualization_GoodsTraceability_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbHuSearchClickEvent += new System.EventHandler(this.TabHuSearchClick_Render);
        this.ucTabNavigator.lbTraceabilityClickEvent += new System.EventHandler(this.TabTraceabilityClick_Render);
        this.ucHuSearchMain.ViewEvent += new EventHandler(ucHuSearchMain_ViewEvent);

        if (!IsPostBack)
        {
        }
    }

    //The event handler when user click link button to "HuSearch" tab
    void TabHuSearchClick_Render(object sender, EventArgs e)
    {
        this.ucHuSearchMain.Visible = true;
        this.ucTraceabilityMain.Visible = false;
    }

    //The event handler when user click link button to "Traceability" tab
    void TabTraceabilityClick_Render(object sender, EventArgs e)
    {
        this.ucHuSearchMain.Visible = false;
        this.ucTraceabilityMain.Visible = true;
    }

    void ucHuSearchMain_ViewEvent(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbTraceability_Click(sender, null);
        this.ucTraceabilityMain.QuickSearch(sender, null);
    }
}
