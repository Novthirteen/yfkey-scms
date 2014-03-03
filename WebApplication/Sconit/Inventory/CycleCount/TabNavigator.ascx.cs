using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory_CycleCount_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbManualClick;
    public event EventHandler lbImportClick;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void lbManual_Click(object sender, EventArgs e)
    {
        if (lbManualClick != null)
        {
            lbManualClick(this, e);
        }

        this.tab_manual.Attributes["class"] = "ajax__tab_active";
        this.tab_import.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbImport_Click(object sender, EventArgs e)
    {
        if (lbImportClick != null)
        {
            lbImportClick(this, e);
        }

        this.tab_manual.Attributes["class"] = "ajax__tab_inactive";
        this.tab_import.Attributes["class"] = "ajax__tab_active";
    }
}
