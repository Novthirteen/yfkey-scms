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

public partial class MasterData_Region_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lblRegionClickEvent;
    public event EventHandler lblWorkCenterClickEvent;
    public event EventHandler lblBillAddressClickEvent;
    public event EventHandler lblShipAddressClickEvent;

    public void UpdateView()
    {
        lbRegion_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbRegion_Click(object sender, EventArgs e)
    {
        if (lblRegionClickEvent != null)
        {
            lblRegionClickEvent(this, e);
        }

        this.tab_region.Attributes["class"] = "ajax__tab_active";
        this.tab_workcenter.Attributes["class"] = "ajax__tab_inactive";
        this.tab_billaddress.Attributes["class"] = "ajax__tab_inactive";
        this.tab_shipaddress.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbWorkCenter_Click(object sender, EventArgs e)
    {
        if (lblWorkCenterClickEvent != null)
        {
            lblWorkCenterClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_workcenter.Attributes["class"] = "ajax__tab_active";
            this.tab_billaddress.Attributes["class"] = "ajax__tab_inactive";
            this.tab_shipaddress.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbBillAddress_Click(object sender, EventArgs e)
    {
        if (lblBillAddressClickEvent != null)
        {
            lblBillAddressClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_workcenter.Attributes["class"] = "ajax__tab_inactive";
            this.tab_billaddress.Attributes["class"] = "ajax__tab_active";
            this.tab_shipaddress.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbShipAddress_Click(object sender, EventArgs e)
    {
        if (lblShipAddressClickEvent != null)
        {
            lblShipAddressClickEvent(this, e);

            this.tab_region.Attributes["class"] = "ajax__tab_inactive";
            this.tab_workcenter.Attributes["class"] = "ajax__tab_inactive";
            this.tab_billaddress.Attributes["class"] = "ajax__tab_inactive";
            this.tab_shipaddress.Attributes["class"] = "ajax__tab_active";
        }
    }
}
