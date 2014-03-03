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

public partial class MRP_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbPlanningClickEvent;
    public event EventHandler lbImportClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbPlanning_Click(object sender, EventArgs e)
    {
        if (lbPlanningClickEvent != null)
        {
            lbPlanningClickEvent(this, e);
        }

        this.tab_planning.Attributes["class"] = "ajax__tab_active";
        this.tab_import.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbImport_Click(object sender, EventArgs e)
    {
        if (lbImportClickEvent != null)
        {
            lbImportClickEvent(this, e);
        }

        this.tab_planning.Attributes["class"] = "ajax__tab_inactive";
        this.tab_import.Attributes["class"] = "ajax__tab_active";
    }
}
