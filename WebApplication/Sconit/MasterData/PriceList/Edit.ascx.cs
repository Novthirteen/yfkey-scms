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
using com.Sconit.Control;
using com.Sconit.Entity;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;

public partial class MasterData_PriceList_PriceList_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    private string PriceListCode
    {
        get
        {
            return (string)ViewState["PriceListCode"];
        }
        set
        {
            ViewState["PriceListCode"] = value;
        }
    }

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
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            this.ODS_PriceList.DataObjectTypeName = "com.Sconit.Entity.Procurement.PurchasePriceList";
            this.ODS_PriceList.UpdateMethod = "UpdatePurchasePriceList";
            this.ODS_PriceList.DeleteMethod = "DeletePurchasePriceList";
            this.ODS_PriceList.SelectMethod = "LoadPurchasePriceList";
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            this.ODS_PriceList.DataObjectTypeName = "com.Sconit.Entity.Distribution.SalesPriceList";
            this.ODS_PriceList.UpdateMethod = "UpdateSalesPriceList";
            this.ODS_PriceList.DeleteMethod = "DeleteSalesPriceList";
            this.ODS_PriceList.SelectMethod = "LoadSalesPriceList";
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string PriceListCode)
    {
        this.PriceListCode = PriceListCode;
        this.ODS_PriceList.SelectParameters["Code"].DefaultValue = PriceListCode;
        this.ODS_PriceList.DeleteParameters["Code"].DefaultValue = PriceListCode;
        this.UpdateView();
    }

    protected void FV_PriceList_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            ((Literal)this.FV_PriceList.FindControl("ltlPartyCode")).Text = "${MasterData.Supplier.Code}:";
            ((Literal)this.FV_PriceList.FindControl("ltlPartyName")).Text = "${MasterData.Supplier.Name}:";
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            ((Literal)this.FV_PriceList.FindControl("ltlPartyCode")).Text = "${MasterData.Customer.Code}:";
            ((Literal)this.FV_PriceList.FindControl("ltlPartyName")).Text = "${MasterData.Customer.Name}:";
        }
    }

    protected void ODS_PriceList_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {        
        TextBox tbPartyCode = (TextBox)(this.FV_PriceList.FindControl("tbPartyCode"));

        if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_PURCHASE)
        {
            PurchasePriceList purchasepricelist = (PurchasePriceList)e.InputParameters[0];
            purchasepricelist.Party = ThePartyMgr.LoadParty(tbPartyCode.Text.Trim());
        }
        else if (this.PriceListType == BusinessConstants.CODE_MASTER_PRICE_LIST_TYPE_VALUE_SALES)
        {
            SalesPriceList salespricelist = (SalesPriceList)e.InputParameters[0];
            salespricelist.Party = ThePartyMgr.LoadParty(tbPartyCode.Text.Trim());
        }
    }

    protected void ODS_PriceList_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully", PriceListCode);
    }

    protected void ODS_PriceList_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.PriceList.Delete.Successfully", PriceListCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.PriceList.Delete.Failed", PriceListCode);
            e.ExceptionHandled = true;
        }
    }
}
