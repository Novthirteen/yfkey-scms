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
using NHibernate.Expression;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class MasterData_Item_EditMain : MainModuleBase
{
    public event EventHandler BackEvent;

    protected string ItemCode
    {
        get
        {
            return (string)ViewState["ItemCode"];
        }
        set
        {
            ViewState["ItemCode"] = value;
        }
    }

    public void InitPageParameter(string code)
    {
        this.ItemCode = code;
        this.ucEdit.InitPageParameter(code);
        this.TabItemClick_Render(this,null);
        this.ucTabNavigator.UpdateView();
        ShowTabKit();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbItemClickEvent += new System.EventHandler(this.TabItemClick_Render);
        this.ucTabNavigator.lbItemKitClickEvent += new System.EventHandler(this.TabItemKitClick_Render);
        this.ucTabNavigator.lbItemRefClickEvent += new System.EventHandler(this.TabItemRefClick_Render);
        this.ucEdit.BackEvent += new System.EventHandler(this.EditBack_Render);
        this.ucItemKit.NewEvent += new System.EventHandler(this.ItemKitNew_Render);
        this.ucItemKit.EditEvent += new System.EventHandler(this.ItemKitEdit_Render);
        this.ucItemKit.BackEvent += new System.EventHandler(this.EditBack_Render);
        this.ucItemKitNew.BackEvent += new System.EventHandler(this.ItemKitNewBack_Render);
        this.ucItemKitNew.NewEvent += new System.EventHandler(this.ItemKitNewCreate_Render);
        this.ucItemKitEdit.EditEvent += new System.EventHandler(this.ItemKitEditEdit_Render);
        this.ucItemKitEdit.BackEvent += new System.EventHandler(this.ItemKitEditBack_Render);
        this.ucItemRef.BackEvent += new System.EventHandler(this.EditBack_Render);
    }

    protected void TabItemClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = true;
        this.ucItemKit.Visible = false;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
        this.ucItemRef.Visible = false;
        this.ucEdit.InitPageParameter(ItemCode);
        ShowTabKit();
    }

    protected void TabItemKitClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
        this.ucItemRef.Visible = false;
        this.ucItemKit.InitPageParameter(ItemCode);
    }

    protected void TabItemRefClick_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = false;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
        this.ucItemRef.Visible = true;
        this.ucItemRef.InitPageParameter(ItemCode);
    }

    protected void EditBack_Render(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ItemKitNew_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = true;
        this.ucItemKitEdit.Visible = false;
        this.ucItemKitNew.PageCleanup(ItemCode);
    }

    protected void ItemKitNewBack_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
    }

    protected void ItemKitNewCreate_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
        this.ucItemKit.InitPageParameter(ItemCode);
    }

    protected void ItemKitEdit_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = true;
        this.ucItemKitEdit.InitPageParameter(ItemCode);
        this.ucItemKitEdit.PageCleanup((string)sender);
    }

    protected void ItemKitEditEdit_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
        this.ucItemKit.InitPageParameter(ItemCode);
    }

    protected void ItemKitEditBack_Render(object sender, EventArgs e)
    {
        this.ucEdit.Visible = false;
        this.ucItemKit.Visible = true;
        this.ucItemKitNew.Visible = false;
        this.ucItemKitEdit.Visible = false;
        this.ucItemKit.InitPageParameter(ItemCode);
    }

    private void ShowTabKit()
    {
        Item item = TheItemMgr.LoadItem(ItemCode);
        if (item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
        {
            this.ucTabNavigator.ShowTabKit(true);
            //this.ucTabNavigator.Visible = true;
        }
        else
        {
            this.ucTabNavigator.ShowTabKit(false);
            //this.ucTabNavigator.Visible = false;
        }
    }
}
