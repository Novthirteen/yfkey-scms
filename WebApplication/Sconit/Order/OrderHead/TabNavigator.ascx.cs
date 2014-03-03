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
using com.Sconit.Utility;

public partial class Order_OrderHead_TabNavigator : ModuleBase
{
    public event EventHandler lblMstrClickEvent;
    public event EventHandler lblRoutingClickEvent;
    public event EventHandler lblLocTransClickEvent;
    public event EventHandler lblActBillClickEvent;
    public event EventHandler lblOrderBindingClickEvent;

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

    public void SelectFirstTab()
    {
        lbMstr_Click(this, null);
    }

    public void HideTab(int tabNo)
    {
        switch (tabNo)
        {
            case 2:
                this.tab_routing.Visible = false;
                break;
            case 4:
                this.tab_actBill.Visible = false;
                break;
            case 5:
                this.tab_orderBinding.Visible = false;
                break;
            default:
                break;
        }
    }

    public void ShowAllTab()
    {
        this.tab_actBill.Visible = true;
        this.tab_locTrans.Visible = true;
        this.tab_mstr.Visible = true;
        this.tab_orderBinding.Visible = true;
        this.tab_routing.Visible = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lbRouting.Text = FlowHelper.GetFlowRoutingLabel(this.ModuleType);
    }

    protected void lbMstr_Click(object sender, EventArgs e)
    {
        if (lblMstrClickEvent != null)
        {
            lblMstrClickEvent(this, e);
        }

        this.tab_mstr.Attributes["class"] = "ajax__tab_active";
        this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
        this.tab_locTrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_actBill.Attributes["class"] = "ajax__tab_inactive";
        this.tab_orderBinding.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbRouting_Click(object sender, EventArgs e)
    {
        if (lblRoutingClickEvent != null)
        {
            lblRoutingClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_active";
            this.tab_locTrans.Attributes["class"] = "ajax__tab_inactive";
            this.tab_actBill.Attributes["class"] = "ajax__tab_inactive";
            this.tab_orderBinding.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbLocTrans_Click(object sender, EventArgs e)
    {
        if (lblLocTransClickEvent != null)
        {
            lblLocTransClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_locTrans.Attributes["class"] = "ajax__tab_active";
            this.tab_actBill.Attributes["class"] = "ajax__tab_inactive";
            this.tab_orderBinding.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbActBill_Click(object sender, EventArgs e)
    {
        if (lblActBillClickEvent != null)
        {
            lblActBillClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_locTrans.Attributes["class"] = "ajax__tab_inactive";
            this.tab_actBill.Attributes["class"] = "ajax__tab_active";
            this.tab_orderBinding.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbOrderBinding_Click(object sender, EventArgs e)
    {
        if (lblOrderBindingClickEvent != null)
        {
            lblOrderBindingClickEvent(this, e);

            this.tab_mstr.Attributes["class"] = "ajax__tab_inactive";
            this.tab_routing.Attributes["class"] = "ajax__tab_inactive";
            this.tab_locTrans.Attributes["class"] = "ajax__tab_inactive";
            this.tab_actBill.Attributes["class"] = "ajax__tab_inactive";
            this.tab_orderBinding.Attributes["class"] = "ajax__tab_active";
        }
    }
}
