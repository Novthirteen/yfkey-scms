using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class Visualization_GoodsTraceability_Traceability_View : ModuleBase
{
    public string CodePrefix
    {
        get { return (string)ViewState["CodePrefix"]; }
        set { ViewState["CodePrefix"] = value; }
    }

    public string OrderNo
    {
        get { return this.lbOrderNo.Text; }
        set { this.lbOrderNo.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string orderNo, string codePrefix)
    {
        this.OrderNo = orderNo;
        this.CodePrefix = codePrefix;
    }

    protected void lbOrderNo_Click(object sender, EventArgs e)
    {
        if (this.CodePrefix == BusinessConstants.CODE_PREFIX_ASN)
        {
            this.ucASN.Visible = true;
            this.ucASN.InitPageParameter(this.OrderNo);
        }
        else if (this.CodePrefix == BusinessConstants.CODE_PREFIX_RECEIPT)
        {
            this.ucReceipt.Visible = true;
            this.ucReceipt.InitPageParameter(this.OrderNo, false);
        }
        else if (this.CodePrefix == BusinessConstants.CODE_PREFIX_ORDER)
        {

        }
    }
}
