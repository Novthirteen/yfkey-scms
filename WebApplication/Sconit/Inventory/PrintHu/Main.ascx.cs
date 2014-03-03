using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;

public partial class Inventory_PrintHu_Main : MainModuleBase
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

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbFlowClickEvent += new EventHandler(ucTabNavigator_lbFlowClickEvent);
        this.ucTabNavigator.lbOrderClickEvent += new EventHandler(ucTabNavigator_lbOrderClickEvent);
        this.ucTabNavigator.lbAsnClickEvent += new EventHandler(ucTabNavigator_lbAsnClickEvent);
        this.ucTabNavigator.lbReceiptClickEvent += new EventHandler(ucTabNavigator_lbReceiptClickEvent);
        this.ucTabNavigator.lbInventoryClickEvent += new EventHandler(ucTabNavigator_lbInventoryClickEvent);
        this.ucTabNavigator.noTabClickEvent += new EventHandler(ucTabNavigator_noTabClickEvent);

        if (!IsPostBack)
        {
            this.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucTabNavigator.ModuleType = this.ModuleType;
            this.ucFlow.ModuleType = this.ModuleType;
            this.ucOrder.ModuleType = this.ModuleType;
        }
    }

    private void ucTabNavigator_lbFlowClickEvent(object sender, EventArgs e)
    {
        this.ucFlow.Visible = true;
        this.ucOrder.Visible = false;
        this.ucAsn.Visible = false;
        this.ucReceipt.Visible = false;
        this.ucInventory.Visible = false;
    }

    private void ucTabNavigator_lbOrderClickEvent(object sender, EventArgs e)
    {
        this.ucFlow.Visible = false;
        this.ucOrder.Visible = true;
        this.ucAsn.Visible = false;
        this.ucReceipt.Visible = false;
        this.ucInventory.Visible = false;
    }

    private void ucTabNavigator_lbAsnClickEvent(object sender, EventArgs e)
    {
        this.ucFlow.Visible = false;
        this.ucOrder.Visible = false;
        this.ucAsn.Visible = true;
        this.ucReceipt.Visible = false;
        this.ucInventory.Visible = false;
    }

    private void ucTabNavigator_lbReceiptClickEvent(object sender, EventArgs e)
    {
        this.ucFlow.Visible = false;
        this.ucOrder.Visible = false;
        this.ucAsn.Visible = false;
        this.ucReceipt.Visible = true;
        this.ucInventory.Visible = false;
    }

    private void ucTabNavigator_lbInventoryClickEvent(object sender, EventArgs e)
    {
        this.ucFlow.Visible = false;
        this.ucOrder.Visible = false;
        this.ucAsn.Visible = false;
        this.ucReceipt.Visible = false;
        this.ucInventory.Visible = true;
    }

    private void ucTabNavigator_noTabClickEvent(object sender, EventArgs e)
    {
        this.ucFlow.Visible = false;
        this.ucOrder.Visible = false;
        this.ucAsn.Visible = false;
        this.ucReceipt.Visible = false;
        this.ucInventory.Visible = false;
    }
}
