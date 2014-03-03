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
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Production_Feed_TabNavigator : System.Web.UI.UserControl
{

    public event EventHandler lblCreateByHuClickEvent;
    public event EventHandler lblCreateByQtyClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbCreateByHu_Click(object sender, EventArgs e)
    {
        if (lblCreateByHuClickEvent != null)
        {
            lblCreateByHuClickEvent(this, e);
        }

        this.tab_hu.Attributes["class"] = "ajax__tab_active";
        this.tab_qty.Attributes["class"] = "ajax__tab_inactive";
       
    }

    protected void lbCreateByQty_Click(object sender, EventArgs e)
    {
        if (lblCreateByQtyClickEvent != null)
        {
            lblCreateByQtyClickEvent(this, e);

            this.tab_hu.Attributes["class"] = "ajax__tab_inactive";
            this.tab_qty.Attributes["class"] = "ajax__tab_active";
         
        }
    }

    public void UpdateView()
    {
        this.tab_hu.Attributes["class"] = "ajax__tab_active";
        this.tab_qty.Attributes["class"] = "ajax__tab_inactive";
    }
  
}
