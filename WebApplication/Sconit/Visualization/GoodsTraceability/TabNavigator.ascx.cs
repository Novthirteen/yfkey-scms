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

public partial class Visualization_GoodsTraceability_TabNavigator : ModuleBase
{
    public event EventHandler lbHuSearchClickEvent;
    public event EventHandler lbTraceabilityClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbHuSearch_Click(object sender, EventArgs e)
    {
        if (lbHuSearchClickEvent != null)
        {
            lbHuSearchClickEvent(this, e);
        }

        this.tab_HuSearch.Attributes["class"] = "ajax__tab_active";
        this.tab_Traceability.Attributes["class"] = "ajax__tab_inactive";
    }

    public void lbTraceability_Click(object sender, EventArgs e)
    {
        if (lbTraceabilityClickEvent != null)
        {
            lbTraceabilityClickEvent(this, e);
        }

        this.tab_HuSearch.Attributes["class"] = "ajax__tab_inactive";
        this.tab_Traceability.Attributes["class"] = "ajax__tab_active";
    }
}
