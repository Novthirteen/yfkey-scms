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
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportPriceList_WarehouseLease_Edit : EditModuleBase
{
    private TransportPriceListDetail transportPriceListDetail;

    public event EventHandler BackEvent;

    protected string TransportPriceListDetailId
    {
        get
        {
            return (string)ViewState["TransportPriceListDetailId"];
        }
        set
        {
            ViewState["TransportPriceListDetailId"] = value;
        }
    }

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

    public void InitPageParameter(string id)
    {
        this.TransportPriceListDetailId = id;

        this.ODS_WarehouseLease.SelectParameters["Id"].DefaultValue = this.TransportPriceListDetailId;
        this.ODS_WarehouseLease.DeleteParameters["Id"].DefaultValue = this.TransportPriceListDetailId;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
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
            case "cvEndDate":
                try
                {
                    if (args.Value.Trim() != "")
                    {
                        DateTime startDate = Convert.ToDateTime(((TextBox)(this.FV_WarehouseLease.FindControl("tbStartDate"))).Text.Trim());
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
            default:
                break;
        }
    }

    protected void FV_TransportPriceListDetail_DataBound(object sender, EventArgs e)
    {
        ((Literal)(this.FV_WarehouseLease.FindControl("lbCurrentTransportPriceList"))).Text = this.TransportPriceListCode;

        if (TransportPriceListDetailId != null && TransportPriceListDetailId != string.Empty)
        {
            TransportPriceListDetail transportPriceListDetail = (TransportPriceListDetail)((FormView)sender).DataItem;

            Controls_TextBox tbCurrency = (Controls_TextBox)(this.FV_WarehouseLease.FindControl("tbCurrency"));

            if (transportPriceListDetail.Currency != null && transportPriceListDetail.Currency.Code != string.Empty)
            {
                tbCurrency.Text = transportPriceListDetail.Currency.Code;
            }

            TextBox tbStartDate = ((TextBox)(this.FV_WarehouseLease.FindControl("tbStartDate")));

            if (transportPriceListDetail.StartDate != null)
            {
                tbStartDate.Text = transportPriceListDetail.StartDate.ToString("yyyy-MM-dd");
            }

            TextBox tbEndDate = ((TextBox)(this.FV_WarehouseLease.FindControl("tbEndDate")));

            if (transportPriceListDetail.EndDate != null)
            {
                tbEndDate.Text = ((DateTime)transportPriceListDetail.EndDate).ToString("yyyy-MM-dd");
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_TransportPriceListDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbCurrency = ((Controls_TextBox)(this.FV_WarehouseLease.FindControl("tbCurrency")));
        TextBox tbEndDate = ((TextBox)(this.FV_WarehouseLease.FindControl("tbEndDate")));

        transportPriceListDetail = (TransportPriceListDetail)e.InputParameters[0];
        transportPriceListDetail.TransportPriceList = TheTransportPriceListMgr.LoadTransportPriceList(this.TransportPriceListCode);
        transportPriceListDetail.Remark = transportPriceListDetail.Remark.Trim();
        transportPriceListDetail.Currency = TheCurrencyMgr.LoadCurrency(tbCurrency.Text.Trim());
        transportPriceListDetail.TaxCode = transportPriceListDetail.TaxCode.Trim();
        transportPriceListDetail.EndDate = tbEndDate.Text.Trim() == "" ? null : transportPriceListDetail.EndDate;
        transportPriceListDetail.Type = BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_WAREHOUSELEASE;
    }

    protected void ODS_TransportPriceListDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.TransportPriceListDetail.UpdateWarehouseLease.Successfully");
        btnBack_Click(this, e);
    }

    protected void ODS_TransportPriceListDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.TransportPriceListDetail.DeleteWarehouseLease.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.TransportPriceListDetail.DeleteWarehouseLease.Fail");
            e.ExceptionHandled = true;
        }
    }
}
