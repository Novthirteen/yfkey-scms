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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Mes;
using com.Sconit.Entity.Exception;

public partial class Mes_Shelf_ShelfItem_New : NewModuleBase
{

    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string ShelfCode
    {
        get
        {
            return (string)ViewState["ShelfCode"];
        }
        set
        {
            ViewState["ShelfCode"] = value;
        }
    }

    public void PageCleanup()
    {
        this.tbShelfCode.Text = this.ShelfCode;
        this.tbItem.Text = string.Empty;
    }



    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            string shelfCode = this.tbShelfCode.Text.Trim();
            string itemCode = this.tbItem.Text.Trim();

            if (!this.rfvItem.IsValid)
            {
                return;
            }
            ShelfItem shelfItem = new ShelfItem();
            if (shelfItem != null)
            {
                shelfItem.Shelf = TheShelfMgr.LoadShelf(shelfCode);
                shelfItem.Item = TheItemMgr.LoadItem(itemCode);
            }

            TheShelfItemMgr.CreateShelfItem(shelfItem);
            ShowSuccessMessage("Mes.ShelfItem.Insert.Successfully");
            if (CreateEvent != null)
            {
                CreateEvent(sender, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

 

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
