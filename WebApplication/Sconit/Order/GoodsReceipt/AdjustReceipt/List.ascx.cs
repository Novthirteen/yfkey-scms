using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Order_GoodsReceipt_AdjustReceipt_List : ModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    private List<TransformerDetail> TransformerDetailList
    {
        get { return (List<TransformerDetail>)ViewState["TransformerDetailList"]; }
        set { ViewState["TransformerDetailList"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(Receipt receipt)
    {
        this.TransformerDetailList = TransformerHelper.ConvertReceiptsToTransformerDetails(receipt.ReceiptDetails);
        this.GV_List.DataSource = this.TransformerDetailList;
        this.GV_List.DataBind();
    }

    private void UpdateTransformerDetailList()
    {
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            TextBox tbAdjustQty = (TextBox)row.FindControl("tbAdjustQty");
            decimal adjustQty = 0;
            if (tbAdjustQty.Text.Trim() != string.Empty)
            {
                adjustQty = decimal.Parse(tbAdjustQty.Text.Trim());
            }
            this.TransformerDetailList[i].AdjustQty = adjustQty;
        }
    }

    public List<TransformerDetail> PopulateTransformerDetailList()
    {
        UpdateTransformerDetailList();
        return this.TransformerDetailList;
    }
 
}
