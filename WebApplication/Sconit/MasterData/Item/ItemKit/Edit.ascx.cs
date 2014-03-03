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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;

public partial class MasterData_Item_ItemKit_Edit : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler EditEvent;

    protected string ChildItemCode
    {
        get
        {
            return (string)ViewState["ChildItemCode"];
        }
        set
        {
            ViewState["ChildItemCode"] = value;
        }
    }

    protected string ParentItemCode
    {
        get
        {
            return (string)ViewState["ParentItemCode"];
        }
        set
        {
            ViewState["ParentItemCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        decimal Qty = 0;
        try
        {
            Qty = Convert.ToDecimal(this.tbQty.Text.Trim());
        }
        catch (Exception)
        {
            ShowErrorMessage("Common.Validator.Valid.Number");
            return;
        }
        if (TheItemKitMgr.LoadItemKit(this.ParentItemCode, this.ChildItemCode) != null)
        {
            ItemKit itemKit = new ItemKit();
            itemKit.ChildItem = TheItemMgr.LoadItem(this.ChildItemCode);
            itemKit.ParentItem = TheItemMgr.LoadItem(this.ParentItemCode);
            itemKit.IsActive = this.cbIsActive.Checked;
            itemKit.Qty = Qty;
            TheItemKitMgr.UpdateItemKit(itemKit);
        }
        else
        {
            ShowErrorMessage("Common.Code.Exist", ChildItemCode);
        }
        EditEvent(this, e);
    }

    public void PageCleanup(string code)
    {
        ItemKit kit = TheItemKitMgr.LoadItemKit(this.ParentItemCode, code);

        this.ChildItemCode = code;
        this.tbCode.Text = code;

        this.tbQty.Text = kit.Qty.ToString("0.########");
        this.cbIsActive.Checked = kit.IsActive;
    }

    public void InitPageParameter(string code)
    {
        this.ParentItemCode = code;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheItemKitMgr.DeleteItemKit(this.ParentItemCode, this.ChildItemCode);
            ShowSuccessMessage("MasterData.Item.DeleteItem.Successfully", this.ChildItemCode);
            btnBack_Click(this, e);
        }
        catch (Castle.Facilities.NHibernateIntegration.DataException ex)
        {
            ShowErrorMessage("MasterData.Item.DeleteItem.Fail", this.ChildItemCode);
        }
    }
}
