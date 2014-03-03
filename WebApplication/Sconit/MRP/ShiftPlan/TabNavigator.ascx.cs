using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MRP_ShiftPlan_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbManualClickEvent;
    public event EventHandler lbImportClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbManual_Click(object sender, EventArgs e)
    {
        if (lbManualClickEvent != null)
        {
            lbManualClickEvent(this, e);
        }

        this.tab_manual.Attributes["class"] = "ajax__tab_active";
        this.tab_import.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbImport_Click(object sender, EventArgs e)
    {
        if (lbImportClickEvent != null)
        {
            lbImportClickEvent(this, e);
        }

        this.tab_manual.Attributes["class"] = "ajax__tab_inactive";
        this.tab_import.Attributes["class"] = "ajax__tab_active";
    }
}
