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

public partial class Mes_Shelf_EditMain : MainModuleBase
{
    public event EventHandler BackEvent;

    protected string Code
    {
        get
        {
            return (string)ViewState["Code"];
        }
        set
        {
            ViewState["Code"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucShelfMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucShelfItemMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lbShelfClickEvent += new System.EventHandler(this.TabShelfClick_Render);
        this.ucTabNavigator.lbShelfItemClickEvent += new System.EventHandler(this.TabShelfItemClick_Render);

        if (!IsPostBack)
        {
            this.ucShelfMain.Visible = true;
            this.ucShelfItemMain.Visible = false;
        }
    }

    //The event handler when user click link button to "Shelf" tab
    void TabShelfClick_Render(object sender, EventArgs e)
    {
        this.ucShelfMain.Visible = true;
        this.ucShelfItemMain.Visible = false;
    }

    //The event handler when user click link button to "ShelfItem" tab
    void TabShelfItemClick_Render(object sender, EventArgs e)
    {
        this.ucShelfMain.Visible = false;
        this.ucShelfItemMain.Visible = true;
        this.ucShelfItemMain.ShelfCode = this.Code;
    }

    void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.Code = Code;
        this.ucShelfMain.InitPageParameter(code);
        this.ucShelfItemMain.InitPageParameter(code);
        this.ucTabNavigator.UpdateView();
    }
}
