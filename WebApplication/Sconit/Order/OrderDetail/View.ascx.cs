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
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Web;

public partial class Order_OrderDetail_View : EditModuleBase
{
    public event EventHandler NewEvent;
    public event EventHandler EditEvent;

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

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }



    public void InitPageParameterForView(OrderDetail orderDetail)
    {

        FillForm(orderDetail);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.trBom.Visible = false;
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.trBom.Visible = false;
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                this.trBill.Visible = true;
                this.trPriceList.Visible = true;
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
            {
                this.trBom.Visible = false;
                this.trBill.Visible = true;
                this.trPriceList.Visible = true;
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                this.trBom.Visible = false;
                this.trBill.Visible = false;
                this.trPriceList.Visible = false;
            }
        }
    }



    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    private void FillForm(OrderDetail orderDetail)
    {
        this.tbSeq.Text = orderDetail.Sequence.ToString();
        this.tbItemCode.Text = orderDetail.Item.Code;
        this.tbUC.Text = orderDetail.UnitCount.ToString();
        this.tbUom.Text = orderDetail.Uom.Code;
        this.tbBom.Text = orderDetail.Bom != null ? orderDetail.Bom.Code : string.Empty;
        this.tbGoodsReceiptLotSize.Text = orderDetail.GoodsReceiptLotSize.HasValue ? orderDetail.GoodsReceiptLotSize.Value.ToString() : string.Empty;
        this.tbLocFrom.Text = orderDetail.LocationFrom != null ? orderDetail.LocationFrom.Code : string.Empty;
        this.tbLocTo.Text = orderDetail.LocationTo != null ? orderDetail.LocationTo.Code : string.Empty;
        this.tbBillFrom.Text = orderDetail.BillFrom != null ? orderDetail.BillFrom.Code : string.Empty;
        this.tbBillTo.Text = orderDetail.BillTo != null ? orderDetail.BillTo.Code : string.Empty;
        this.tbPriceListFrom.Text = orderDetail.PriceListFrom != null ? orderDetail.PriceListFrom.Code : string.Empty;
        this.tbPriceListTo.Text = orderDetail.PriceListTo != null ? orderDetail.PriceListTo.Code : string.Empty;
        
    }


}
