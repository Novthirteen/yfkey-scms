using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterData_Location_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbLocationClickEvent;
    public event EventHandler lbLocationAdvClickEvent;
    public event EventHandler lbLocationBinClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tab_Location.Attributes["class"] = "ajax__tab_active";
        //this.tab_LocationAdv.Attributes["class"] = "ajax__tab_inactive";
        //this.tab_LocationBin.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbLocation_Click(object sender, EventArgs e)
    {
        if (lbLocationClickEvent != null)
        {
            lbLocationClickEvent(this, e);
        }

        this.tab_Location.Attributes["class"] = "ajax__tab_active";
        this.tab_LocationAdv.Attributes["class"] = "ajax__tab_inactive";
        this.tab_LocationBin.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbLocationAdv_Click(object sender, EventArgs e)
    {
        if (lbLocationAdvClickEvent != null)
        {
            lbLocationAdvClickEvent(this, e);
        }

        this.tab_Location.Attributes["class"] = "ajax__tab_inactive";
        this.tab_LocationAdv.Attributes["class"] = "ajax__tab_active";
        this.tab_LocationBin.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbLocationBin_Click(object sender, EventArgs e)
    {
        if (lbLocationBinClickEvent != null)
        {
            lbLocationBinClickEvent(this, e);
        }

        this.tab_Location.Attributes["class"] = "ajax__tab_inactive";
        this.tab_LocationAdv.Attributes["class"] = "ajax__tab_inactive";
        this.tab_LocationBin.Attributes["class"] = "ajax__tab_active";
    }

    public void ShowTabKit(bool isShow)
    {
        //this.tab_LocationAdv.Visible = isShow;
        //this.tab_LocationBin.Visible = isShow;
        this.tab_LocationAdv.Visible = true;
        this.tab_LocationBin.Visible = true;
    }

    public void UpdateView()
    {
        lbLocation_Click(this, null);
    }
}
