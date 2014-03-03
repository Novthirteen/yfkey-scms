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

public partial class MasterData_Routing_EditMain : MainModuleBase
{
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucRoutingMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucRoutingDetailMain.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lbRoutingClickEvent += new System.EventHandler(this.TabRoutingClick_Render);
        this.ucTabNavigator.lbRoutingDetailClickEvent += new System.EventHandler(this.TabRoutingDetailClick_Render);

        if (!IsPostBack)
        {
            this.ucRoutingMain.Visible = true;
            this.ucRoutingDetailMain.Visible = false;
        }
    }

    //The event handler when user click link button to "Routing" tab
    void TabRoutingClick_Render(object sender, EventArgs e)
    {
        this.ucRoutingMain.Visible = true;
        this.ucRoutingDetailMain.Visible = false;
    }

    //The event handler when user click link button to "RoutingDetail" tab
    void TabRoutingDetailClick_Render(object sender, EventArgs e)
    {
        this.ucRoutingMain.Visible = false;
        this.ucRoutingDetailMain.Visible = true;
        //if (this.ucRoutingMain.RoutingCode != null)
        //{
        //    this.ucRoutingDetailMain.RoutingCode = this.ucRoutingMain.RoutingCode;
        this.ucRoutingDetailMain.InitPageParameter(code);
        //}
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
        this.code = code;
        this.ucRoutingMain.InitPageParameter(code);
        this.ucTabNavigator.UpdateView();
        //this.ODS_Routing.SelectParameters["Code"].DefaultValue = code;
        //this.ODS_Routing.DeleteParameters["Code"].DefaultValue = code;
        //this.UpdateView();
    }
}
