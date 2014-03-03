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

public partial class Security_TabNavigator : System.Web.UI.UserControl
{

    public event EventHandler lblBasicClickEvent;
    public event EventHandler lblThemeClickEvent;
    public event EventHandler lblNamedQueryClickEvent;
    public event EventHandler lblScFavClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["TabStatus"] != null && Request.Cookies["TabStatus"].Value == "Theme")
        {
            this.tab_Basic.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Theme.Attributes["class"] = "ajax__tab_active";
            this.tab_NamedQuery.Attributes["class"] = "ajax__tab_inactive";
            this.tab_ScFav.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    public void InitTheme()
    {
        this.tab_Basic.Attributes["class"] = "ajax__tab_inactive";
        this.tab_Theme.Attributes["class"] = "ajax__tab_active";
        this.tab_NamedQuery.Attributes["class"] = "ajax__tab_inactive";
        this.tab_ScFav.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbBasic_Click(object sender, EventArgs e)
    {
        if (lblBasicClickEvent != null)
        {
            lblBasicClickEvent(this, e);
        }

        this.tab_Basic.Attributes["class"] = "ajax__tab_active";
        this.tab_Theme.Attributes["class"] = "ajax__tab_inactive";
        this.tab_NamedQuery.Attributes["class"] = "ajax__tab_inactive";
        this.tab_ScFav.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbTheme_Click(object sender, EventArgs e)
    {
        if (lblThemeClickEvent != null)
        {
            lblThemeClickEvent(this, e);

            this.tab_Basic.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Theme.Attributes["class"] = "ajax__tab_active";
            this.tab_NamedQuery.Attributes["class"] = "ajax__tab_inactive";
            this.tab_ScFav.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbNamedQuery_Click(object sender, EventArgs e)
    {
        if (lblNamedQueryClickEvent != null)
        {
            lblNamedQueryClickEvent(this, e);

            this.tab_Basic.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Theme.Attributes["class"] = "ajax__tab_inactive";
            this.tab_NamedQuery.Attributes["class"] = "ajax__tab_active";
            this.tab_ScFav.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbScFav_Click(object sender, EventArgs e)
    {
        if (lblScFavClickEvent != null)
        {
            lblScFavClickEvent(this, e);

            this.tab_Basic.Attributes["class"] = "ajax__tab_inactive";
            this.tab_Theme.Attributes["class"] = "ajax__tab_inactive";
            this.tab_NamedQuery.Attributes["class"] = "ajax__tab_inactive";
            this.tab_ScFav.Attributes["class"] = "ajax__tab_active";
        }
    }
}
