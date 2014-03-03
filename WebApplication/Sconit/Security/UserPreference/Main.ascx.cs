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

public partial class Security_Main : MainModuleBase
{
    public Security_Main(string[] selectModule)
    {
        if (selectModule[0] == "Theme")
        {
            TabThemeClick_Render(null, null);
            this.ucTabNavigator.InitTheme();
        }

    }
    public Security_Main()
    { }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lblBasicClickEvent += new System.EventHandler(this.TabBasicClick_Render);
        this.ucTabNavigator.lblThemeClickEvent += new System.EventHandler(this.TabThemeClick_Render);
        this.ucTabNavigator.lblNamedQueryClickEvent += new System.EventHandler(this.TabNamedQueryClick_Render);
        this.ucTabNavigator.lblScFavClickEvent += new System.EventHandler(this.TabScFavClick_Render);
        this.ucEdit.InitPageParameter(((com.Sconit.Entity.MasterData.UserBase)(this.Session["Current_User"])).Code, true);
        if (Request.Cookies["TabStatus"]!=null && Request.Cookies["TabStatus"].Value == "Theme")
        {
            this.ucEdit.Visible = false;
            this.ucTheme.Visible = true;
            //Response.Cookies["TabStatus"].Expires = DateTime.Now.AddDays(-1);  
        }
    }

    protected void TabBasicClick_Render(object sender, EventArgs e)
    {
        this.ucTheme.Visible = false;
        this.ucNamedQuery.Visible = false;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(((com.Sconit.Entity.MasterData.UserBase)(this.Session["Current_User"])).Code, true);
    }

    protected void TabThemeClick_Render(object sender, EventArgs e)
    {
        this.ucTheme.Visible = true;
        this.ucNamedQuery.Visible = false;
        this.ucEdit.Visible = false;
    }

    protected void TabNamedQueryClick_Render(object sender, EventArgs e)
    {
        this.ucTheme.Visible = false;
        this.ucNamedQuery.Visible = true;
        this.ucEdit.Visible = false;
    }

    protected void TabScFavClick_Render(object sender, EventArgs e)
    {
        this.ucTheme.Visible = false;
        this.ucNamedQuery.Visible = false;
        this.ucEdit.Visible = false;
    }
}
