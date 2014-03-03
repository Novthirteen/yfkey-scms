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

public partial class Order_GoodsReceipt_ViewReceipt_List : ModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string receiptNo)
    {
        Receipt receipt = TheReceiptMgr.LoadReceipt(receiptNo, true);
        this.InitPageParameter(receipt);
    }
    public void InitPageParameter(Receipt receipt)
    {
        bool isScanHu = (receipt.ReceiptDetails[0].HuId != null);
        List<Transformer> transformerList = this.ConvertReceiptToTransformer(receipt.ReceiptDetails);
        this.ucTransformer.InitPageParameter(transformerList, this.ModuleType, BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE, isScanHu);
    }
    public void InitPageParameter(Resolver resolver)
    {
        bool isScanHu = resolver.IsScanHu;
        this.ucTransformer.InitPageParameter(resolver);
    }

    private List<Transformer> ConvertReceiptToTransformer(IList<ReceiptDetail> receiptDetails)
    {
        var query =
            from r in receiptDetails
            group r by r.OrderLocationTransaction into g
            select new { g.Key, Qty = g.Sum(r => r.ShippedQty), CurrentQty = g.Sum(r => r.ReceivedQty), Details = g.Where(r => r.OrderLocationTransaction == g.Key) };

        List<Transformer> transformers = new List<Transformer>();
        foreach (var item in query)
        {
            Transformer transformer = TransformerHelper.ConvertOrderLocationTransactionToTransformer(item.Key);

            transformer.Qty = transformer.OrderedQty;
          //  transformer.Qty = item.Qty.HasValue ? item.Qty.Value : 0;
            transformer.CurrentQty = item.CurrentQty.HasValue ? item.CurrentQty.Value : 0;
            transformer.TransformerDetails = TransformerHelper.ConvertReceiptsToTransformerDetails(new List<ReceiptDetail>(item.Details));
            transformers.Add(transformer);
        }

        return transformers;
    }
}
