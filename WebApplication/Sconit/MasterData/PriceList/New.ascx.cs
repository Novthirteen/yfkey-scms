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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Control;
using com.Sconit.Entity;

public partial class MasterData_PriceList_PriceList_New : NewModuleBase
{
    private PriceList priceList;
    private Item item;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string PriceListType
    {
        get
        {
            return (string)ViewState["PriceListType"];
        }
        set
        {
            ViewState["PriceListType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Controls_TextBox tbParty = ((Controls_TextBox)(this.FV_PriceList.FindControl("tbParty")));
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            tbParty.ServiceMethod = "GetSupplier";
            tbParty.ServicePath = "SupplierMgr.service";
            tbParty.ServiceParameter = "string:" + this.CurrentUser.Code; 
            this.ODS_PriceList.DataObjectTypeName = "com.Sconit.Entity.Procurement.PurchasePriceList";
            this.ODS_PriceList.InsertMethod = "CreatePurchasePriceList";
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            tbParty.ServiceMethod = "GetCustomer";
            tbParty.ServicePath = "CustomerMgr.service";
            tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
            this.ODS_PriceList.DataObjectTypeName = "com.Sconit.Entity.Distribution.SalesPriceList";
            this.ODS_PriceList.InsertMethod = "CreateSalesPriceList";
        }
    
    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_PriceList.FindControl("tbCode"))).Text = string.Empty;
      //  ((TextBox)(this.FV_PriceList.FindControl("tbDescription"))).Text = string.Empty;
        ((CheckBox)(this.FV_PriceList.FindControl("cbIsActive"))).Checked = true;
        Controls_TextBox tbParty = ((Controls_TextBox)(this.FV_PriceList.FindControl("tbParty")));
        tbParty.Text = string.Empty;
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            ((Literal)this.FV_PriceList.FindControl("ltlParty")).Text = "${MasterData.Supplier.Code}:";
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            ((Literal)this.FV_PriceList.FindControl("ltlParty")).Text = "${MasterData.Customer.Code}:";
        }
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvCode":
                if (ThePriceListMgr.LoadPriceList(args.Value) != null)
                {
                    ShowWarningMessage("MasterData.PriceList.Code.Exist", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvParty":
                if (ThePartyMgr.LoadParty(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Party.Code.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }

    }

    protected void ODS_PriceList_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbParty = ((Controls_TextBox)(this.FV_PriceList.FindControl("tbParty")));
        priceList = (PriceList)e.InputParameters[0];
        priceList.Code = priceList.Code.Trim();
        
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            priceList.Party = TheSupplierMgr.LoadSupplier(tbParty.Text.Trim());
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            priceList.Party = TheCustomerMgr.LoadCustomer(tbParty.Text.Trim());
        }
    }

    protected void ODS_PriceList_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(priceList.Code, e);
            ShowSuccessMessage("MasterData.PriceList.Insert.Successfully", priceList.Code);
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
