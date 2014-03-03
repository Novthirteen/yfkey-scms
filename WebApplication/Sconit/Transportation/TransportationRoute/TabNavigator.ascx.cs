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

public partial class Transportation_TransportationRoute_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lblTransportationRouteClickEvent;
    public event EventHandler lblTransportationRouteDetailClickEvent;

    public void UpdateView()
    {
        lbTransportationRoute_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbTransportationRoute_Click(object sender, EventArgs e)
    {
        if (lblTransportationRouteClickEvent != null)
        {
            lblTransportationRouteClickEvent(this, e);

            this.tab_transportationRoute.Attributes["class"] = "ajax__tab_active";
            this.tab_transportationRouteDetail.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbTransportationRouteDetail_Click(object sender, EventArgs e)
    {
        if (lblTransportationRouteDetailClickEvent != null)
        {
            lblTransportationRouteDetailClickEvent(this, e);

            this.tab_transportationRoute.Attributes["class"] = "ajax__tab_inactive";
            this.tab_transportationRouteDetail.Attributes["class"] = "ajax__tab_active";
        }
    }
}
