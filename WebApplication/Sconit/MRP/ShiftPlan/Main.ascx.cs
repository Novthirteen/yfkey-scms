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

public partial class MRP_ShiftPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbManualClickEvent += new EventHandler(this.TabManualClick_Render);
        this.ucTabNavigator.lbImportClickEvent += new EventHandler(this.TabImportClick_Render);

        if (!IsPostBack)
        {
        }
    }

    //The event handler when user click tab "Manual" 
    void TabManualClick_Render(object sender, EventArgs e)
    {
        this.ucManual.Visible = true;
        this.ucImport.Visible = false;
    }

    //The event handler when user click tab "Import" 
    void TabImportClick_Render(object sender, EventArgs e)
    {
        this.ucManual.Visible = false;
        this.ucImport.Visible = true;
    }
}
