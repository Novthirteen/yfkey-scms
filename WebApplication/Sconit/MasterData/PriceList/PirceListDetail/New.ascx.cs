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

public partial class MasterData_PriceList_PriceListDetail_New : NewModuleBase
{
    private PriceListDetail priceListdetail;
    private Item item;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string PriceListCode
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

    public void PageCleanup()
    {
        ((TextBox)(this.FV_PriceListDetail.FindControl("tbPriceList"))).Text = this.PriceListCode;
        ((TextBox)(this.FV_PriceListDetail.FindControl("tbStartDate"))).Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        ((TextBox)(this.FV_PriceListDetail.FindControl("tbEndDate"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbItem"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbCurrency"))).Text = string.Empty;
        ((TextBox)(this.FV_PriceListDetail.FindControl("tbUnitPrice"))).Text = "0";          // //
        ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbUom"))).Text = string.Empty;
        ((TextBox)(this.FV_PriceListDetail.FindControl("tbTaxCode"))).Text = string.Empty;
        ((CheckBox)(this.FV_PriceListDetail.FindControl("cbIsIncludeTax"))).Checked = false;
        ((CheckBox)(this.FV_PriceListDetail.FindControl("cbIsProvEst"))).Checked = false;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvUom":

                if (TheUomMgr.LoadUom(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.PriceList.UomInvalid", args.Value);
                    args.IsValid = false;
                }

                break;
            case "cvItem":

                if (TheItemMgr.LoadItem(args.Value.Trim()) == null)
                {
                    ShowWarningMessage("MasterData.Item.Code.NotExist");
                    args.IsValid = false;
                }
                else
                {
                    string priceListCode = ((TextBox)(this.FV_PriceListDetail.FindControl("tbPriceList"))).Text.Trim();
                    string currencyCode = ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbCurrency"))).Text.Trim(); 
                    DateTime startDate = Convert.ToDateTime(((TextBox)(this.FV_PriceListDetail.FindControl("tbStartDate"))).Text.Trim());
                    if (ThePriceListDetailMgr.LoadPriceListDetail(priceListCode, startDate, args.Value.Trim(), currencyCode) != null)
                    {
                        ShowWarningMessage("MasterData.PriceListDetail.UniqueExistError");
                        args.IsValid = false;
                    }
                }
                break;
            case "cvPriceList":

                if (ThePriceListMgr.LoadPriceList(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.PriceList.Code.NotExist");
                    args.IsValid = false;
                }
                break;
            case "cvUnitPrice":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("MasterData.PriceListDetail.UnitPrice.Error");
                    args.IsValid = false;
                }
                break;
            case "cvStartDate":
                try
                {
                    Convert.ToDateTime(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Common.Date.Error");
                    args.IsValid = false;
                }
                break;
            case "cvEndDate":
                try
                {
                    if (args.Value.Trim() != "")
                    {
                        DateTime startDate = Convert.ToDateTime(((TextBox)(this.FV_PriceListDetail.FindControl("tbStartDate"))).Text.Trim());
                        if (DateTime.Compare(startDate, Convert.ToDateTime(args.Value)) >= 0)
                        {
                            ShowErrorMessage("MasterData.PriceList.TimeCompare");
                            args.IsValid = false; 
                        }
                    }
                }
                catch (Exception)
                {
                    ShowWarningMessage("Common.Date.Error");
                    args.IsValid = false;
                }
                break;
            case "cvCurrency":
                if (args.Value.Trim() != "")
                {
                    if (TheCurrencyMgr.LoadCurrency(args.Value) == null)
                    {
                        ShowWarningMessage("MasterData.Currency.Code.NotExist", args.Value);
                        args.IsValid = false;
                    }
                }
                break;
            default:
                break;
        }
    }

    protected void ODS_PriceListDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string priceListCode = ((TextBox)(this.FV_PriceListDetail.FindControl("tbPriceList"))).Text.Trim();
        string itemCode = ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbItem"))).Text.Trim();
        string currencyCode = ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbCurrency"))).Text.Trim();
        string uomCode = ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbUom"))).Text.Trim();

        priceListdetail = (PriceListDetail)e.InputParameters[0];
        if (priceListdetail != null)
        {
            priceListdetail.PriceList = ThePriceListMgr.LoadPriceList(priceListCode);
            item = TheItemMgr.LoadItem(itemCode);
            priceListdetail.Item = item;
            priceListdetail.Currency = TheCurrencyMgr.LoadCurrency(currencyCode);

            //default uom
            if (uomCode == "")
            {
                priceListdetail.Uom = item.Uom;
            }
            else
            {
                priceListdetail.Uom = TheUomMgr.LoadUom(uomCode);
            }
        }

    }

    protected void ODS_PriceListDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(priceListdetail.Id.ToString(), e);
            ShowSuccessMessage("Common.Business.Result.Insert.Successfully", priceListdetail.Id.ToString());

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
