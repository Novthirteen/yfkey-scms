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

public partial class MasterData_Bom_TabNavigator : System.Web.UI.UserControl
{
    public event EventHandler lbBomViewClickEvent;
    public event EventHandler lbBomClickEvent;
    public event EventHandler lbBomDetailClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbBomView_Click(object sender, EventArgs e)
    {
        if (lbBomViewClickEvent != null)
        {
            lbBomViewClickEvent(this, e);
        }

        this.tab_bomview.Attributes["class"] = "ajax__tab_active";
        this.tab_bom.Attributes["class"] = "ajax__tab_inactive";
        this.tab_bomdetail.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbBom_Click(object sender, EventArgs e)
    {
        if (lbBomClickEvent != null)
        {
            lbBomClickEvent(this, e);
        }

        this.tab_bomview.Attributes["class"] = "ajax__tab_inactive";
        this.tab_bom.Attributes["class"] = "ajax__tab_active";
        this.tab_bomdetail.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbBomDetail_Click(object sender, EventArgs e)
    {
        if (lbBomDetailClickEvent != null)
        {
            lbBomDetailClickEvent(this, e);
        }

        this.tab_bomview.Attributes["class"] = "ajax__tab_inactive";
        this.tab_bom.Attributes["class"] = "ajax__tab_inactive";
        this.tab_bomdetail.Attributes["class"] = "ajax__tab_active";
    }
}
