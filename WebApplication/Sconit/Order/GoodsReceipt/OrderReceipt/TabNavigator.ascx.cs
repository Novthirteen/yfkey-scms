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

public partial class Order_GoodsReceipt_OrderReceipt_TabNavigator : ModuleBase
{
    public event EventHandler lbDetailClickEvent;
    public event EventHandler lbNewItemInLocTransEvent;
    public event EventHandler lbInLocTransEvent;
    public event EventHandler lbOutLocTransEvent;

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

    public bool IsNewItem
    {
        get
        {
            return (bool)ViewState["IsNewItem"];
        }
        set
        {
            ViewState["IsNewItem"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void UpdateView()
    {
        if (this.IsNewItem)
        {
            this.tab_newiteminloctrans.Visible = true;
        }
        lbDetail_Click(this, null);
    }

    protected void lbDetail_Click(object sender, EventArgs e)
    {
        if (lbDetailClickEvent != null)
        {
            lbDetailClickEvent(this, e);
        }

        this.tab_detail.Attributes["class"] = "ajax__tab_active";
        this.tab_inloctrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_newiteminloctrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_outloctrans.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbInLocTrans_Click(object sender, EventArgs e)
    {
        if (lbInLocTransEvent != null)
        {
            lbInLocTransEvent(this, e);
        }

        this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
        this.tab_inloctrans.Attributes["class"] = "ajax__tab_active";
        this.tab_newiteminloctrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_outloctrans.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbNewItemInLocTrans_Click(object sender, EventArgs e)
    {
        if (lbNewItemInLocTransEvent != null)
        {
            lbNewItemInLocTransEvent(this, e);
        }

        this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
        this.tab_inloctrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_newiteminloctrans.Attributes["class"] = "ajax__tab_active";
        this.tab_outloctrans.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbOutLocTrans_Click(object sender, EventArgs e)
    {
        
        if (lbOutLocTransEvent != null)
        {
            lbOutLocTransEvent(this, e);
        }

        this.tab_detail.Attributes["class"] = "ajax__tab_inactive";
        this.tab_inloctrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_newiteminloctrans.Attributes["class"] = "ajax__tab_inactive";
        this.tab_outloctrans.Attributes["class"] = "ajax__tab_active";
    }
}
