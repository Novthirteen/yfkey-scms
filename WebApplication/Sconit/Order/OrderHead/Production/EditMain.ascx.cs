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
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

public partial class Order_OrderHead_EditMain : EditModuleBase
{
    public event EventHandler BackEvent;

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

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

    //新品
    public bool NewItem
    {
        get
        {
            return (bool)ViewState["NewItem"];
        }
        set
        {
            ViewState["NewItem"] = value;
        }
    }

    //报废
    public bool IsScrap
    {
        get
        {
            return (bool)ViewState["IsScrap"];
        }
        set
        {
            ViewState["IsScrap"] = value;
        }
    }

    //原材料回用
    public bool IsReuse
    {
        get
        {
            return (bool)ViewState["IsReuse"];
        }
        set
        {
            ViewState["IsReuse"] = value;
        }
    }

    public void InitPageParameter(string orderNo)
    {
        this.ucTabNavigator.ShowAllTab();
        this.ucEdit.InitPageParameter(orderNo);
        this.ucRouting.InitPageParameter(orderNo);
        if (!this.ucRouting.Visible)
        {
            this.ucTabNavigator.HideTab(2);
        }
        this.ucActBillView.InitPageParameter(orderNo);
        if (!this.ucActBillView.Visible)
        {
            this.ucTabNavigator.HideTab(4);
        }
        this.ucLocTransView.InitPageParameter(orderNo);
        this.ucOrderBinding.OrderNo = orderNo;
        this.ucOrderBinding.UpdateView();
        if (!this.ucOrderBinding.Visible || this.IsScrap)
        {
            this.ucTabNavigator.HideTab(5);
        }

        this.ucTabNavigator.SelectFirstTab();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lblMstrClickEvent += new System.EventHandler(this.TabMstrClick_Render);
        this.ucTabNavigator.lblRoutingClickEvent += new System.EventHandler(this.TabRoutingClick_Render);
        this.ucTabNavigator.lblActBillClickEvent += new System.EventHandler(this.TabActBillClick_Render);
        this.ucTabNavigator.lblLocTransClickEvent += new System.EventHandler(this.TabLocTransClick_Render);
        this.ucTabNavigator.lblOrderBindingClickEvent += new System.EventHandler(this.TabOrderBindingClick_Render);
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucRouting.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucActBillView.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucLocTransView.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucEdit.UpdateLocTransAndActBillEvent += new System.EventHandler(this.UpdateLocTransAndActBill_Render);
        this.ucLocTransView.UpdateRoutingEvent += new System.EventHandler(this.UpdateRouting_Render);
        this.ucOrderBinding.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucEdit.GetLocTransHuListEvent += new System.EventHandler(this.GetLocTransHuList_Render);

        if (!IsPostBack)
        {
            this.ucTabNavigator.ModuleType = this.ModuleType;
            this.ucTabNavigator.ModuleSubType = this.ModuleSubType;
            this.ucEdit.ModuleType = this.ModuleType;
            this.ucEdit.ModuleSubType = this.ModuleSubType;
            this.ucActBillView.ModuleType = this.ModuleType;
            this.ucActBillView.ModuleSubType = this.ModuleSubType;
            this.ucLocTransView.ModuleType = this.ModuleType;
            this.ucLocTransView.ModuleSubType = this.ModuleSubType;
            this.ucRouting.ModuleType = this.ModuleType;
            this.ucRouting.ModuleSubType = this.ModuleType;
            this.ucEdit.NewItem = this.NewItem;
            this.ucEdit.IsScrap = this.IsScrap;
            this.ucLocTransView.IsScrap = this.IsScrap;
            this.ucLocTransView.NewItem = this.NewItem;
            this.ucEdit.IsReuse = this.IsReuse;
            this.ucLocTransView.IsReuse = this.IsReuse;
        }
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void UpdateLocTransAndActBill_Render(object sender, EventArgs e)
    {
        this.InitPageParameter((string)sender);
    }

    protected void UpdateRouting_Render(object sender, EventArgs e)
    {
        ucRouting.InitPageParameter((string)sender);
        this.ucRouting.Visible = false;
    }

    protected void TabMstrClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucRouting.Visible = false;
        this.ucActBillView.Visible = false;
        this.ucLocTransView.Visible = false;
        this.ucOrderBinding.Visible = false;
    }

    protected void TabRoutingClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRouting.Visible = true;
        this.ucActBillView.Visible = false;
        this.ucLocTransView.Visible = false;
        this.ucOrderBinding.Visible = false;
    }

    protected void TabActBillClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRouting.Visible = false;
        this.ucActBillView.Visible = true;
        this.ucLocTransView.Visible = false;
        this.ucOrderBinding.Visible = false;
    }

    protected void TabLocTransClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRouting.Visible = false;
        this.ucActBillView.Visible = false;
        this.ucLocTransView.Visible = true;
        this.ucOrderBinding.Visible = false;
    }

    protected void TabOrderBindingClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucRouting.Visible = false;
        this.ucActBillView.Visible = false;
        this.ucLocTransView.Visible = false;
        this.ucOrderBinding.Visible = true;
    }

    public void GetLocTransHuList_Render(object sender, EventArgs e)
    {
        this.ucEdit.HuList= this.ucLocTransView.GetLocTransHuList();
    }
}
