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

public partial class MasterData_PriceList_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbPriceListClickEvent;
    public event EventHandler lbPriceListDetailClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void lbPriceList_Click(object sender, EventArgs e)
    {
        if (lbPriceListClickEvent != null)
        {
            lbPriceListClickEvent(this, e);
        }

        this.tab_PriceList.Attributes["class"] = "ajax__tab_active";
        this.tab_PriceListdetail.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbPriceListDetail_Click(object sender, EventArgs e)
    {
        if (lbPriceListDetailClickEvent != null)
        {
            lbPriceListDetailClickEvent(this, e);
        }

        this.tab_PriceList.Attributes["class"] = "ajax__tab_inactive";
        this.tab_PriceListdetail.Attributes["class"] = "ajax__tab_active";
    }

    public void UpdateView()
    {
        lbPriceList_Click(this, null);
    }
}
