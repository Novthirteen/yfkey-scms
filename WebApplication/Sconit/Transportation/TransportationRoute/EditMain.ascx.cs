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

public partial class Transportation_TransportationRoute_EditMain : System.Web.UI.UserControl
{
    public event EventHandler BackEvent;

    public string TransportationRouteCode
    {
        get
        {
            return (string)ViewState["TransportationRouteCode"];
        }
        set
        {
            ViewState["TransportationRouteCode"] = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTransportationRouteDetail.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblTransportationRouteClickEvent += new System.EventHandler(this.TabTransportationRouteClick_Render);
        this.ucTabNavigator.lblTransportationRouteDetailClickEvent += new System.EventHandler(this.TabTransportationRouteDetailClick_Render);
    }

    public void InitPageParameter(string code)
    {
        this.TransportationRouteCode = code;
        this.ucTabNavigator.Visible = true;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(code);
        this.ucTabNavigator.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void TabTransportationRouteClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucTransportationRouteDetail.Visible = false;
    }

    protected void TabTransportationRouteDetailClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucTransportationRouteDetail.Visible = true;
        this.ucTransportationRouteDetail.TransportationRouteCode = this.TransportationRouteCode;
        this.ucTransportationRouteDetail.InitPageParameter();
    }
}
