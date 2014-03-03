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

public partial class ManageSconit_LeanEngine_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbSetupClickEvent;
    public event EventHandler lbViewClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbSetup_Click(object sender, EventArgs e)
    {
        if (lbSetupClickEvent != null)
        {
            lbSetupClickEvent(this, e);
        }

        this.tab_view.Attributes["class"] = "ajax__tab_inactive";
        this.tab_setup.Attributes["class"] = "ajax__tab_active";
    }

    protected void lbView_Click(object sender, EventArgs e)
    {
        if (lbViewClickEvent != null)
        {
            lbViewClickEvent(this, e);
        }

        this.tab_view.Attributes["class"] = "ajax__tab_active";
        this.tab_setup.Attributes["class"] = "ajax__tab_inactive";
    }

}
