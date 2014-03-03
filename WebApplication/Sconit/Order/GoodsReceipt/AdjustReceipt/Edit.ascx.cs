using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;

public partial class Order_GoodsReceipt_AdjustReceipt_Edit : EditModuleBase
{
    public string ReceiptNo
    {
        get { return (string)ViewState["ReceiptNo"]; }
        set { ViewState["ReceiptNo"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(Receipt receipt)
    {
        this.ODS_Receipt.SelectParameters["code"].DefaultValue = receipt.ReceiptNo;
        this.ReceiptNo = receipt.ReceiptNo;
        this.FV_Receipt.DataBind();
    }
}
