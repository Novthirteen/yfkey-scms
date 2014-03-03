using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;

public partial class Inventory_PrintHu_TabNavigator : ModuleBase
{
    public event EventHandler lbFlowClickEvent;
    public event EventHandler lbOrderClickEvent;
    public event EventHandler lbAsnClickEvent;
    public event EventHandler lbReceiptClickEvent;
    public event EventHandler lbInventoryClickEvent;
    public event EventHandler noTabClickEvent;

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
        if (this.ModuleType == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER)
        {
            this.tab_asn.Visible = false;
            this.tab_receipt.Visible = false;
            this.tab_inventory.Visible = false;
        }
        else
        {
            this.tab_flow.Visible = false;
            this.tab_order.Visible = false;
            this.tab_asn.Visible = false;
            this.tab_receipt.Visible = false;
            this.tab_inventory.Visible = false;

            if (this.CurrentUser.PagePermission != null && this.CurrentUser.PagePermission.Count > 0)
            {
                foreach (Permission permission in this.CurrentUser.PagePermission)
                {
                    if (permission.Code == "CreateHuByFlow")
                    {
                        this.tab_flow.Visible = true;                        
                    }

                    if (permission.Code == "CreateHuByOrder")
                    {
                        this.tab_order.Visible = true;
                    }

                    if (permission.Code == "PrintHuByAsn")
                    {
                        this.tab_asn.Visible = true;
                    }

                    if (permission.Code == "PrintHuByReceipt")
                    {
                        this.tab_receipt.Visible = true;
                    }

                    if (permission.Code == "PrintHuByInventory")
                    {
                        this.tab_inventory.Visible = true;
                    }
                }
            }

            if (!this.tab_flow.Visible)
            {
                this.lbOrder_Click(this, null);

                if (!this.tab_order.Visible)
                {
                    this.lbAsn_Click(this, null);

                    if (!this.tab_asn.Visible)
                    {
                        this.lbReceipt_Click(this, null);

                        if (!this.tab_receipt.Visible)
                        {
                            this.lbInventory_Click(this, null);

                            if (!this.tab_inventory.Visible)
                            {
                                this.Visible = false;
                                this.noTabClickEvent(this, null);
                            }
                        }
                    }
                }
            }
        }
    }

    protected void lbFlow_Click(object sender, EventArgs e)
    {
        if (lbFlowClickEvent != null)
        {
            lbFlowClickEvent(this, e);

            this.tab_flow.Attributes["class"] = "ajax__tab_active";
            this.tab_order.Attributes["class"] = "ajax__tab_inactive";
            this.tab_asn.Attributes["class"] = "ajax__tab_inactive";
            this.tab_receipt.Attributes["class"] = "ajax__tab_inactive";
            this.tab_inventory.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbOrder_Click(object sender, EventArgs e)
    {
        if (lbOrderClickEvent != null)
        {
            lbOrderClickEvent(this, e);

            this.tab_flow.Attributes["class"] = "ajax__tab_inactive";
            this.tab_order.Attributes["class"] = "ajax__tab_active";
            this.tab_asn.Attributes["class"] = "ajax__tab_inactive";
            this.tab_receipt.Attributes["class"] = "ajax__tab_inactive";
            this.tab_inventory.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbAsn_Click(object sender, EventArgs e)
    {
        if (lbAsnClickEvent != null)
        {
            lbAsnClickEvent(this, e);

            this.tab_flow.Attributes["class"] = "ajax__tab_inactive";
            this.tab_order.Attributes["class"] = "ajax__tab_inactive";
            this.tab_asn.Attributes["class"] = "ajax__tab_active";
            this.tab_receipt.Attributes["class"] = "ajax__tab_inactive";
            this.tab_inventory.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbReceipt_Click(object sender, EventArgs e)
    {
        if (lbReceiptClickEvent != null)
        {
            lbReceiptClickEvent(this, e);

            this.tab_flow.Attributes["class"] = "ajax__tab_inactive";
            this.tab_order.Attributes["class"] = "ajax__tab_inactive";
            this.tab_asn.Attributes["class"] = "ajax__tab_inactive";
            this.tab_receipt.Attributes["class"] = "ajax__tab_active";
            this.tab_inventory.Attributes["class"] = "ajax__tab_inactive";
        }
    }

    protected void lbInventory_Click(object sender, EventArgs e)
    {
        if (lbInventoryClickEvent != null)
        {
            lbInventoryClickEvent(this, e);

            this.tab_flow.Attributes["class"] = "ajax__tab_inactive";
            this.tab_order.Attributes["class"] = "ajax__tab_inactive";
            this.tab_asn.Attributes["class"] = "ajax__tab_inactive";
            this.tab_receipt.Attributes["class"] = "ajax__tab_inactive";
            this.tab_inventory.Attributes["class"] = "ajax__tab_active";
        }
    }
    
}
