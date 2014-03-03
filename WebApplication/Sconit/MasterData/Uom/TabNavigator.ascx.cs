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

public partial class MasterData_Uom_TabNavigator : System.Web.UI.UserControl
{

    public event EventHandler lbUomClickEvent;
    public event EventHandler lbUomConvClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbUom_Click(object sender, EventArgs e)
    {
        if (lbUomClickEvent != null)
        {
            lbUomClickEvent(this, e);
        }

        this.tab_Uom.Attributes["class"] = "ajax__tab_active";
        this.tab_UomConv.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbUomConv_Click(object sender, EventArgs e)
    {
        if (lbUomConvClickEvent != null)
        {
            lbUomConvClickEvent(this, e);

            this.tab_Uom.Attributes["class"] = "ajax__tab_inactive";
            this.tab_UomConv.Attributes["class"] = "ajax__tab_active";
        }
    }

}
