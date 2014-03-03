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

public partial class Transportation_Carrier_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lblCarrierClickEvent;
    public event EventHandler lblBillAddressClickEvent;

    public void UpdateView()
    {
        lbCarrier_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbCarrier_Click(object sender, EventArgs e)
    {
        if (lblCarrierClickEvent != null)
        {
            lblCarrierClickEvent(this, e);

            this.tab_carrier.Attributes["class"] = "ajax__tab_active";
            this.tab_billaddress.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbBillAddress_Click(object sender, EventArgs e)
    {
        if (lblBillAddressClickEvent != null)
        {
            lblBillAddressClickEvent(this, e);

            this.tab_carrier.Attributes["class"] = "ajax__tab_inactive";
            this.tab_billaddress.Attributes["class"] = "ajax__tab_active";
        }
    }
}
