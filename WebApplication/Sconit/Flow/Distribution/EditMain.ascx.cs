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

public partial class MasterData_Flow_EditMain : System.Web.UI.UserControl
{
    public event EventHandler BackEvent;

   

    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    public void InitPageParameter(String flowCode)
    {
        this.FlowCode = flowCode;
        this.ucTabNavigator.Visible = true;
        this.ucEdit.Visible = true;
        this.ucEdit.InitPageParameter(flowCode);
        this.ucDetail.Visible = false;
        this.ucRouting.Visible = false;
        this.ucStrategy.Visible = false;
        this.ucBinding.Visible = false;
        this.ucView.Visible = false;
        this.ucTabNavigator.UpdateView();
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.ucEdit.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucEdit.UpdateViewEvent += new System.EventHandler(this.UpdateView_Render);
        this.ucDetail.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblMstrClickEvent += new System.EventHandler(this.TabMstrClick_Render);
        this.ucTabNavigator.lblDetailClickEvent += new System.EventHandler(this.TabDetailClick_Render);
        this.ucTabNavigator.lblStrategyClickEvent += new System.EventHandler(this.TabStrategyClick_Render);
        this.ucTabNavigator.lblBindingClickEvent += new System.EventHandler(this.TabBindingClick_Render);
        this.ucTabNavigator.lblRoutingClickEvent += new System.EventHandler(this.TabRoutingClick_Render);
        this.ucStrategy.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucStrategy.UpdateViewEvent += new System.EventHandler(this.UpdateView_Render);
        this.ucRouting.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucRouting.UpdateViewEvent += new System.EventHandler(this.UpdateView_Render);
        this.ucBinding.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucView.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucDetail.BackEvent += new System.EventHandler(this.Back_Render);
        this.ucTabNavigator.lblViewClickEvent += new System.EventHandler(this.TabViewClick_Render);
        if (!IsPostBack)
        {
            this.ucTabNavigator.ModuleType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
            this.ucDetail.ModuleType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
            this.ucRouting.ModuleType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
            this.ucStrategy.ModuleType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
            this.ucBinding.ModuleType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
            this.ucView.ModuleType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;

        }
    }

    protected void Back_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void TabMstrClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucDetail.Visible = false;
        this.ucRouting.Visible = false;
        this.ucStrategy.Visible = false;
        this.ucBinding.Visible = false;
        this.ucView.Visible = false;
    }

    protected void UpdateView_Render(object sender, EventArgs e)
    {
        this.ucView.InitPageParameter((string)sender);
    }

    protected void TabDetailClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucDetail.Visible = true;
        this.ucRouting.Visible = false;
        this.ucStrategy.Visible = false;
        this.ucBinding.Visible = false;
        this.ucView.Visible = false;
        this.ucDetail.FlowCode = FlowCode;
        this.ucDetail.InitPageParameter();
    }

    protected void TabStrategyClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucDetail.Visible = false;
        this.ucRouting.Visible = false;
        this.ucStrategy.Visible = true;
        this.ucBinding.Visible = false;
        this.ucView.Visible = false;
        this.ucStrategy.InitPageParameter(FlowCode);
    }

    protected void TabBindingClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucDetail.Visible = false;
        this.ucRouting.Visible = false;
        this.ucStrategy.Visible = false;
        this.ucBinding.Visible = true;
        this.ucView.Visible = false;
        this.ucBinding.InitPageParameter(FlowCode);
       
    }

    protected void TabRoutingClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucDetail.Visible = false;
        this.ucRouting.Visible = true;
        this.ucStrategy.Visible = false;
        this.ucBinding.Visible = false;
        this.ucView.Visible = false;
        this.ucRouting.InitPageParameter(FlowCode);
    }

    protected void TabViewClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucDetail.Visible = false;
        this.ucRouting.Visible = false;
        this.ucStrategy.Visible = false;
        this.ucBinding.Visible = false;
        this.ucView.Visible = true;
        this.ucView.InitPageParameter(FlowCode);
    }

}
