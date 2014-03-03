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
using com.Sconit.Utility;

public partial class MasterData_Flow_TabNavigator : System.Web.UI.UserControl
{
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

    public event EventHandler lblMstrClickEvent;
    public event EventHandler lblDetailClickEvent;
    public event EventHandler lblStrategyClickEvent;
    public event EventHandler lblBindingClickEvent;
    public event EventHandler lblRoutingClickEvent;
    public event EventHandler lblViewClickEvent;

    public void UpdateView()
    {
        lbMstr_Click(this, null);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.lbMstr.Text = FlowHelper.GetFlowLabel(this.ModuleType);
        this.lbStrategy.Text = FlowHelper.GetFlowStrategyLabel(this.ModuleType);
        this.lbDetail.Text = FlowHelper.GetFlowDetailLabel(this.ModuleType);
        this.lbRouting.Text = FlowHelper.GetFlowRoutingLabel(this.ModuleType);
        
    }

    protected void lbMstr_Click(object sender, EventArgs e)
    {
        if (lblMstrClickEvent != null)
        {
            lblMstrClickEvent(this, e);
        }


        this.tab_mstr.Attributes["class"] = "ajax__tab_active";
        this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
        this.tab_strategy.Attributes["class"] = "ajax__tab_inactive";
        this.tab_binding.Attributes["class"] = "ajax__tab_inactive";
        this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
        this.tab_view.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbDetail_Click(object sender, EventArgs e)
    {
        if (lblDetailClickEvent != null)
        {
            lblDetailClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_detail.Attributes["class"] = "ajax__tab_active";
            this.tab_strategy.Attributes["class"] = "ajax__tab_inactive";
            this.tab_binding.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_view.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbStrategy_Click(object sender, EventArgs e)
    {
        if (lblStrategyClickEvent != null)
        {
            lblStrategyClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
            this.tab_strategy.Attributes["class"] = "ajax__tab_active";
            this.tab_binding.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_view.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbBinding_Click(object sender, EventArgs e)
    {
        if (lblBindingClickEvent != null)
        {
            lblBindingClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
            this.tab_strategy.Attributes["class"] = "ajax__tab_inactive";
            this.tab_binding.Attributes["class"] = "ajax__tab_active";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_view.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbRouting_Click(object sender, EventArgs e)
    {
        if (lblRoutingClickEvent != null)
        {
            lblRoutingClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
            this.tab_strategy.Attributes["class"] = "ajax__tab_inactive";
            this.tab_binding.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_active";
            this.tab_view.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbView_Click(object sender, EventArgs e)
    {
        if (lblViewClickEvent != null)
        {
            lblViewClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
            this.tab_strategy.Attributes["class"] = "ajax__tab_inactive";
            this.tab_binding.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_view.Attributes["class"] = "ajax__tab_active";
        }
    }
}
