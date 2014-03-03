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

public partial class MasterData_Bom_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbBomViewClickEvent += new System.EventHandler(this.TabBomViewClick_Render);
        this.ucTabNavigator.lbBomClickEvent += new System.EventHandler(this.TabBomClick_Render);
        this.ucTabNavigator.lbBomDetailClickEvent += new System.EventHandler(this.TabBomDetailClick_Render);

        if (!IsPostBack)
        {
            this.ucBomViewMain.Visible = true;
            this.ucBomMain.Visible = false;
            this.ucBomDetailMain.Visible = false;
        }
    }

    //The event handler when user click link button to "BomView" tab
    void TabBomViewClick_Render(object sender, EventArgs e)
    {
        this.ucBomViewMain.Visible = true;
        this.ucBomMain.Visible = false;
        this.ucBomDetailMain.Visible = false;
    }

    //The event handler when user click link button to "Bom" tab
    void TabBomClick_Render(object sender, EventArgs e)
    {
        this.ucBomViewMain.Visible = false;
        this.ucBomMain.Visible = true;
        this.ucBomDetailMain.Visible = false;
    }

    //The event handler when user click link button to "BomDetail" tab
    void TabBomDetailClick_Render(object sender, EventArgs e)
    {
        this.ucBomViewMain.Visible = false;
        this.ucBomMain.Visible = false;
        this.ucBomDetailMain.Visible = true;
        if (this.ucBomMain.BomCode != null)
        {
            this.ucBomDetailMain.BomCode = this.ucBomMain.BomCode;
            this.ucBomDetailMain.InitPageParameter();
        }
    }
}
