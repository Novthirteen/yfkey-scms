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

public partial class Production_Feed_NewMain : System.Web.UI.UserControl
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.ucTabNavigator.lblCreateByHuClickEvent += new System.EventHandler(this.TabCreateByHuClick_Render);
        this.ucTabNavigator.lblCreateByQtyClickEvent += new System.EventHandler(this.TabCreateByQtyClick_Render);
        this.ucNewHu.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucNewQty.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucNewHu.BackEvent += new System.EventHandler(this.Back_Render);

        if (!IsPostBack)
        {
        }
    }


    public void UpdateView()
    {
        this.ucNewHu.Visible = true;
        this.ucNewHu.UpdateView();
        this.ucNewQty.Visible = false;
        this.ucTabNavigator.UpdateView();
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void Create_Render(object sender, EventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(sender, e);
        }
    }


    protected void TabCreateByHuClick_Render(object sender, EventArgs e)
    {
        this.ucNewHu.Visible = true;
        this.ucNewQty.Visible = false;
        this.ucNewHu.UpdateView();
    }



    protected void TabCreateByQtyClick_Render(object sender, EventArgs e)
    {
        this.ucNewHu.Visible = false;
        this.ucNewQty.Visible = true;
        this.ucNewQty.InitPageParameter();
    }

}
