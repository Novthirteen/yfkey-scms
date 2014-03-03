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

public partial class Order_GoodsReceipt_Main : MainModuleBase
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

    public Order_GoodsReceipt_Main()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNavigator.lblOrderClickEvent += new System.EventHandler(this.TabOrderClick_Render);
        this.ucNavigator.lblAsnClickEvent += new System.EventHandler(this.TabAsnClick_Render);

        if (!IsPostBack)
        {
            this.ModuleType = this.ModuleParameter["ModuleType"];
            this.ucNavigator.ModuleType = this.ModuleType;
            this.ucOrderReceipt.ModuleType = this.ModuleType;
            this.ucAsnReceipt.ModuleType = this.ModuleType;
            this.ucAsnReceipt.Action = "Receive";

            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                this.ucNavigator.Visible = false;
                this.ucAsnReceipt.Visible = false;
            }
            if (this.Session["Temp_Session_OrderNo"] != null)
            {
                TabOrderClick_Render(this, null);
            }
        }
    }

    protected void TabOrderClick_Render(object sender, EventArgs e)
    {
        this.ucOrderReceipt.Visible = true;
        this.ucAsnReceipt.Visible = false;
    }

    protected void TabAsnClick_Render(object sender, EventArgs e)
    {
        this.ucOrderReceipt.Visible = false;
        this.ucAsnReceipt.Visible = true;
    }
}
