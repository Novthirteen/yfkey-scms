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

public partial class MasterData_Routing_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbRoutingClickEvent;
    public event EventHandler lbRoutingDetailClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbRouting_Click(object sender, EventArgs e)
    {
        if (lbRoutingClickEvent != null)
        {
            lbRoutingClickEvent(this, e);
        }

        this.tab_Routing.Attributes["class"] = "ajax__tab_active";
        this.tab_RoutingDetail.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbRoutingDetail_Click(object sender, EventArgs e)
    {
        if (lbRoutingDetailClickEvent != null)
        {
            lbRoutingDetailClickEvent(this, e);
        }

        this.tab_Routing.Attributes["class"] = "ajax__tab_inactive";
        this.tab_RoutingDetail.Attributes["class"] = "ajax__tab_active";
    }

    public void UpdateView()
    {
        lbRouting_Click(this, null);
    }
}
