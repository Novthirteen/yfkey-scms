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

public partial class MasterData_Item_ItemKit_New : NewModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler NewEvent;
      
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
        string childerItemCode = this.tbCode.Text.Trim();
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
        if (TheItemKitMgr.LoadItemKit(this.ParentItemCode, childerItemCode) == null)
        {
            ItemKit itemKit = new ItemKit();
            itemKit.ChildItem = TheItemMgr.LoadItem(childerItemCode);
            itemKit.ParentItem = TheItemMgr.LoadItem(this.ParentItemCode);
            itemKit.IsActive = this.cbIsActive.Checked;
            itemKit.Qty = Qty;
            TheItemKitMgr.CreateItemKit(itemKit);
            ShowSuccessMessage("MasterData.Item.AddItem.Successfully", childerItemCode);
        }
        else
        {
            ShowErrorMessage("Common.Code.Exist",childerItemCode);
            return;
        }
        NewEvent(this, e);
    }
  
    public void PageCleanup(string code)
    {
        this.ParentItemCode = code;
        this.tbCode.Text = string.Empty;
        this.tbQty.Text = string.Empty;
        this.cbIsActive.Checked = true;
    }
    
}
