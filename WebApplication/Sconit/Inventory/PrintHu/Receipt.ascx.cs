using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

public partial class Inventory_PrintHu_Receipt : ModuleBase
{
    private string CurrentReceiptNo
    {
        get
        {
            return (string)ViewState["CurrentReceiptNo"];
        }
        set
        {
            ViewState["CurrentReceiptNo"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void tbReceipt_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            if (this.CurrentReceiptNo == null || this.CurrentReceiptNo != this.tbReceipt.Text.Trim())
            {
                Receipt currentReceipt = TheReceiptMgr.LoadReceipt(this.tbReceipt.Text.Trim(), this.CurrentUser, true);

                this.CurrentReceiptNo = currentReceipt.ReceiptNo;

                this.ucList.InitPageParameter(currentReceipt);
                this.ucList.Visible = true;
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }    
}
