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
using com.Sconit.Web;

public partial class Order_GoodsReceipt_TabNavigator : ModuleBase
{
    public event EventHandler lblOrderClickEvent;
    public event EventHandler lblAsnClickEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public void SelectFirstTab()
    {
        lbOrder_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbOrder_Click(object sender, EventArgs e)
    {
        if (lblOrderClickEvent != null)
        {
            lblOrderClickEvent(this, e);
        }

        this.tab_order.Attributes["class"] = "ajax__tab_active";
        this.tab_asn.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbAsn_Click(object sender, EventArgs e)
    {
        if (lblAsnClickEvent != null)
        {
            lblAsnClickEvent(this, e);
        }

        this.tab_order.Attributes["class"] = "ajax__tab_inactive";
        this.tab_asn.Attributes["class"] = "ajax__tab_active";
    }
}
