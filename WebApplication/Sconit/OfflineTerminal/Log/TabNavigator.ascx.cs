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

public partial class MasterData_Client_TabNavigator : System.Web.UI.UserControl
{

    public event EventHandler lbOnlineClickEvent;
    public event EventHandler lbOfflineClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbOnline_Click(object sender, EventArgs e)
    {
        if (lbOnlineClickEvent != null)
        {
            lbOnlineClickEvent(this, e);
        }

        this.tab_Online.Attributes["class"] = "ajax__tab_active";
        this.tab_Offline.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbOffline_Click(object sender, EventArgs e)
    {
        if (lbOfflineClickEvent != null)
        {
            lbOfflineClickEvent(this, e);

            this.tab_Online.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Offline.Attributes["class"] = "ajax__tab_active";
        }
    }

}
