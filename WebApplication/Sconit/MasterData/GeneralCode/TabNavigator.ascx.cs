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

public partial class Security_TabNavigator : System.Web.UI.UserControl
{

    public event EventHandler lbEntityOptClickEvent;
    public event EventHandler lbCodeMstrClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbEntityOpt_Click(object sender, EventArgs e)
    {
        if (lbEntityOptClickEvent != null)
        {
            lbEntityOptClickEvent(this, e);
        }

        this.tab_EntityOpt.Attributes["class"] = "ajax__tab_active";
        this.tab_CodeMstr.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbCodeMstr_Click(object sender, EventArgs e)
    {
        if (lbCodeMstrClickEvent != null)
        {
            lbCodeMstrClickEvent(this, e);

            this.tab_EntityOpt.Attributes["class"] = "ajax__tab_inactive";
            this.tab_CodeMstr.Attributes["class"] = "ajax__tab_active";
        }
    }

}
