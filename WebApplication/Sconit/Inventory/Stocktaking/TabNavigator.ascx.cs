using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory_Stocktaking_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbManualClick;
    public event EventHandler lbResultClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void show_Result(bool isShow)
    {
        this.tab_result.Visible = isShow;
    }

    public void lbManual_Click(object sender, EventArgs e)
    {
        if (lbManualClick != null)
        {
            lbManualClick(this, e);
        }

        this.tab_manual.Attributes["class"] = "ajax__tab_active";
        this.tab_result.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbResult_Click(object sender, EventArgs e)
    {
        if (lbResultClick != null)
        {
            lbResultClick(this, e);
        }

        this.tab_manual.Attributes["class"] = "ajax__tab_inactive";
        this.tab_result.Attributes["class"] = "ajax__tab_active";
    }
}
