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
using com.Sconit.Entity;
using NHibernate.Expression;

public partial class Inventory_Stocktaking_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucEditMain.TabEvent += new EventHandler(Tab_Render);
    }

    protected void lbManual_Click(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = true;
        this.ucResult.clean();
        this.ucResult.Visible = false;
    }

    protected void lbResult_Click(object sender, EventArgs e)
    {
        this.ucEditMain.Visible = false;
        
        this.ucResult.Visible = true;
    }

    protected void ucResult_BtnResultClick(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbManual_Click(null, null);
        this.ucEditMain.ListEdit_Render(sender, null);
    }

    void Tab_Render(object sender, EventArgs e)
    {
        this.ucTabNavigator.show_Result((bool)((object[])sender)[0]);
        this.ucResult.Code = (string)((object[])sender)[1];
    }

}
