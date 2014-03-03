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
using com.Sconit.Entity;

public partial class MasterData_PriceList_PriceListDetail_Edit : EditModuleBase
{
    private PriceListDetail priceListdetail;
    private Item item;
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ODS_PriceListDetail.SelectParameters["Id"].DefaultValue = code;
        this.ODS_PriceListDetail.DeleteParameters["Id"].DefaultValue = code;
        this.UpdateView();
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

                if (TheItemMgr.LoadItem(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Item.Code.NotExist");
                    args.IsValid = false;
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

    protected void FV_PriceListDetail_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        priceListdetail = ThePriceListDetailMgr.LoadPriceListDetail(Convert.ToInt32(this.code));
        TextBox tbCode = (TextBox)(this.FV_PriceListDetail.FindControl("tbCode"));
        TextBox tbPriceList = (TextBox)(this.FV_PriceListDetail.FindControl("tbPriceList"));
        TextBox tbStartDate = (TextBox)(this.FV_PriceListDetail.FindControl("tbStartDate"));
        TextBox tbEndDate = (TextBox)(this.FV_PriceListDetail.FindControl("tbEndDate"));
        TextBox tbItem = (TextBox)(this.FV_PriceListDetail.FindControl("tbItem"));
        Controls_TextBox tbCurrency = (Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbCurrency"));
        TextBox tbUnitPrice = (TextBox)(this.FV_PriceListDetail.FindControl("tbUnitPrice"));
        Controls_TextBox tbUom = (Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbUom"));
        TextBox tbTaxCode = (TextBox)(this.FV_PriceListDetail.FindControl("tbTaxCode"));
        CheckBox cbIsIncludeTax = (CheckBox)(this.FV_PriceListDetail.FindControl("cbIsIncludeTax"));
       // CheckBox cbIsProvEst = (CheckBox)(this.FV_PriceListDetail.FindControl("cbIsProvEst"));

        if (priceListdetail != null)
        {
            tbCode.Text = priceListdetail.Id.ToString();
            tbPriceList.Text = priceListdetail.PriceList.Code;
            tbStartDate.Text = priceListdetail.StartDate.ToString("yyyy-MM-dd");
            if (priceListdetail.EndDate != null)
            {
                tbEndDate.Text = ((DateTime)priceListdetail.EndDate).ToString("yyyy-MM-dd");
            }
            tbItem.Text = priceListdetail.Item.Code;
            tbCurrency.Text = priceListdetail.Currency.Code;
            tbUnitPrice.Text = priceListdetail.UnitPrice.ToString("0.########");
            tbUom.Text = priceListdetail.Uom.Code;
            tbTaxCode.Text = priceListdetail.TaxCode;
            cbIsIncludeTax.Checked = priceListdetail.IsIncludeTax;
          //  cbIsProvEst.Checked = priceListdetail.IsProvisionalEstimate;
        }

    }

    protected void ODS_PriceListDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string priceListCode = ((TextBox)(this.FV_PriceListDetail.FindControl("tbPriceList"))).Text.Trim();
        string itemCode = ((TextBox)(this.FV_PriceListDetail.FindControl("tbItem"))).Text.Trim();
        string currencyCode = ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbCurrency"))).Text.Trim();
        string uomCode = ((Controls_TextBox)(this.FV_PriceListDetail.FindControl("tbUom"))).Text.Trim();
        string startDate = ((TextBox)(this.FV_PriceListDetail.FindControl("tbStartDate"))).Text.Trim();
        string endDate = ((TextBox)(this.FV_PriceListDetail.FindControl("tbEndDate"))).Text.Trim();

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

    protected void ODS_PriceListDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        btnBack_Click(this, e);
        ShowSuccessMessage("Common.Business.Result.Update.Successfully");
    }

    protected void ODS_PriceListDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Common.Business.Result.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }
}
