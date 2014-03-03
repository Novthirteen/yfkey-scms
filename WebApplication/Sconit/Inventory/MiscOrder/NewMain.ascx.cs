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
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;

public partial class Production_Feed_NewMain : MainModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

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
            this.ucNewQty.ModuleType = this.ModuleType;
            this.ucNewHu.ModuleType = this.ModuleType;

            TabCreateByHuClick_Render(sender, e);
            
        }
    }


    public void UpdateView()
    {
        this.ucTabNavigator.UpdateView();
        
        this.ucNewQty.MiscOrder = new MiscOrder();
        this.ucNewQty.InitPageParameter();
        this.ucNewHu.MiscOrder = new MiscOrder();
        this.ucNewQty.InitPageParameter();
        TabCreateByHuClick_Render(null, null);
    }


    public void InitPageParameter(string miscOrderNo)
    {
        MiscOrder miscOrder = TheMiscOrderMgr.ReLoadMiscOrder(miscOrderNo);
        if (miscOrder.MiscOrderDetails != null && miscOrder.MiscOrderDetails.Count > 0)
        {
            if (miscOrder.MiscOrderDetails[0].HuId != null && miscOrder.MiscOrderDetails[0].HuId != string.Empty)
            {
                this.ucNewHu.MiscOrder = miscOrder;
                this.ucNewHu.InitPageParameter();
                TabCreateByHuClick_Render(null, null);
            }
            else
            {
                this.ucNewQty.MiscOrder = miscOrder;
                this.ucNewQty.InitPageParameter();
                TabCreateByQtyClick_Render(null, null);
            }
        }
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
       // this.ucNewHu.MiscOrder = new MiscOrder();
        this.ucNewHu.InitPageParameter();
    }



    protected void TabCreateByQtyClick_Render(object sender, EventArgs e)
    {
        this.ucNewHu.Visible = false;
        this.ucNewQty.Visible = true;
       // this.ucNewQty.MiscOrder = new MiscOrder();
        this.ucNewQty.InitPageParameter();
    }

}
