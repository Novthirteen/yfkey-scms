using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportPriceList_Operation_New : NewModuleBase
{
    private TransportPriceListDetail transportPriceListDetail;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string TransportPriceListCode
    {
        get
        {
            return (string)ViewState["TransportPriceListCode"];
        }
        set
        {
            ViewState["TransportPriceListCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void PageCleanup()
    {
        ((Literal)(this.FV_Operation.FindControl("lbCurrentTransportPriceList"))).Text = this.TransportPriceListCode;
        ((Controls_TextBox)(this.FV_Operation.FindControl("tbItem"))).Text = string.Empty;
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Operation.FindControl("ddlBillingMethod"))).SelectedIndex = 0;
        ((TextBox)(this.FV_Operation.FindControl("tbUnitPrice"))).Text = "0";
        ((Controls_TextBox)(this.FV_Operation.FindControl("tbCurrency"))).Text = string.Empty;
        ((TextBox)(this.FV_Operation.FindControl("tbServiceCharge"))).Text = "0";
        ((TextBox)(this.FV_Operation.FindControl("tbTaxCode"))).Text = string.Empty;
        ((CheckBox)(this.FV_Operation.FindControl("cbIsIncludeTax"))).Checked = false;
        ((TextBox)(this.FV_Operation.FindControl("tbStartDate"))).Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        ((TextBox)(this.FV_Operation.FindControl("tbEndDate"))).Text = string.Empty;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvItem":

                if (TheItemMgr.LoadItem(args.Value.Trim()) == null)
                {
                    ShowWarningMessage("MasterData.Item.Code.NotExist");
                    args.IsValid = false;
                }
                else
                {
                    Controls_TextBox tbUom = ((Controls_TextBox)(this.FV_Operation.FindControl("tbUom")));
                    if (tbUom.Text.Trim() == "")
                    {
                        ShowWarningMessage("Transportation.TransportPriceListDetail.Uom.Empty");
                        args.IsValid = false;
                    }
                }
                break;
            case "cvUnitPrice":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.UnitPrice.Error");
                    args.IsValid = false;
                }
                break;
            case "cvCurrency":
                if (TheCurrencyMgr.LoadCurrency(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Currency.Code.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvServiceCharge":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.ServiceCharge.Error");
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
                        DateTime startDate = Convert.ToDateTime(((TextBox)(this.FV_Operation.FindControl("tbStartDate"))).Text.Trim());
                        if (DateTime.Compare(startDate, Convert.ToDateTime(args.Value)) > 0)
                        {
                            ShowErrorMessage("Transportation.TransportPriceListDetail.TimeCompare");
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
            case "cvBillingMethod":
                TextBox tbStartDate = ((TextBox)(this.FV_Operation.FindControl("tbStartDate")));
                TextBox tbEndDate = ((TextBox)(this.FV_Operation.FindControl("tbEndDate")));
                Controls_TextBox tbItem = ((Controls_TextBox)(this.FV_Operation.FindControl("tbItem")));
                com.Sconit.Control.CodeMstrDropDownList ddlBillingMethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Operation.FindControl("ddlBillingMethod")));

                IList<TransportPriceListDetail> transportPriceListDetailList = TheTransportPriceListDetailMgr.CheckOperation(this.TransportPriceListCode, tbStartDate.Text.Trim(), tbEndDate.Text.Trim(), tbItem.Text.Trim(), ddlBillingMethod.SelectedValue);
                if (transportPriceListDetailList != null && transportPriceListDetailList.Count > 0)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.ExistOperation");
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }

    protected void ODS_TransportPriceListDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbItem = ((Controls_TextBox)(this.FV_Operation.FindControl("tbItem")));
        com.Sconit.Control.CodeMstrDropDownList ddlBillingMethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Operation.FindControl("ddlBillingMethod")));
        Controls_TextBox tbCurrency = ((Controls_TextBox)(this.FV_Operation.FindControl("tbCurrency")));
        TextBox tbEndDate = ((TextBox)(this.FV_Operation.FindControl("tbEndDate")));

        transportPriceListDetail = (TransportPriceListDetail)e.InputParameters[0];
        transportPriceListDetail.TransportPriceList = TheTransportPriceListMgr.LoadTransportPriceList(this.TransportPriceListCode);
        transportPriceListDetail.Item = tbItem.Text.Trim() == String.Empty ? null : TheItemMgr.LoadItem(tbItem.Text.Trim());
        transportPriceListDetail.BillingMethod = ddlBillingMethod.SelectedValue;
        transportPriceListDetail.Currency = TheCurrencyMgr.LoadCurrency(tbCurrency.Text.Trim());
        transportPriceListDetail.TaxCode = transportPriceListDetail.TaxCode.Trim();
        transportPriceListDetail.EndDate = tbEndDate.Text.Trim() == "" ? null : transportPriceListDetail.EndDate;
        transportPriceListDetail.Type = BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_OPERATION;
    }

    protected void ODS_TransportPriceListDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(transportPriceListDetail.Id, e);
            ShowSuccessMessage("Transportation.TransportPriceListDetail.AddOperation.Successfully");
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
