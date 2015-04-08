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

public partial class Transportation_TransportPriceList_TransportPriceListDetail_Edit : EditModuleBase
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

        this.ODS_TransportPriceListDetail.SelectParameters["Id"].DefaultValue = this.TransportPriceListDetailId;
        this.ODS_TransportPriceListDetail.DeleteParameters["Id"].DefaultValue = this.TransportPriceListDetailId;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvShipFrom":
                if (TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(args.Value)) == null)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.ShipFrom.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvShipTo":
                if (TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(args.Value)) == null)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.ShipTo.NotExist", args.Value);
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
            case "cvMinVolume":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.MinVolume.Error");
                    args.IsValid = false;
                }
                break;
            case "cvMinPrice":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Transportation.TransportPriceListDetail.MinPrice.Error");
                    args.IsValid = false;
                }
                break;
            case "cvEndDate":
                try
                {
                    if (args.Value.Trim() != "")
                    {
                        DateTime startDate = Convert.ToDateTime(((TextBox)(this.FV_TransportPriceListDetail.FindControl("tbStartDate"))).Text.Trim());
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
        ((Literal)(this.FV_TransportPriceListDetail.FindControl("lbCurrentTransportPriceList"))).Text = this.TransportPriceListCode;

        if (TransportPriceListDetailId != null && TransportPriceListDetailId != string.Empty)
        {
            TransportPriceListDetail transportPriceListDetail = (TransportPriceListDetail)((FormView)sender).DataItem;

       

            Controls_TextBox tbShipFrom = (Controls_TextBox)this.FV_TransportPriceListDetail.FindControl("tbShipFrom");

            if (transportPriceListDetail.ShipFrom != null && transportPriceListDetail.ShipFrom.Id.ToString() != string.Empty)
            {
                tbShipFrom.Text = transportPriceListDetail.ShipFrom.FullAddressAndId;
            }

            Controls_TextBox tbShipTo = (Controls_TextBox)this.FV_TransportPriceListDetail.FindControl("tbShipTo");

            if (transportPriceListDetail.ShipTo != null && transportPriceListDetail.ShipTo.Id.ToString() != string.Empty)
            {
                tbShipTo.Text = transportPriceListDetail.ShipTo.FullAddressAndId;
            }

            com.Sconit.Control.CodeMstrDropDownList ddlPricingMethod = (com.Sconit.Control.CodeMstrDropDownList)this.FV_TransportPriceListDetail.FindControl("ddlPricingMethod");

            if (transportPriceListDetail.PricingMethod != null)
            {
                ddlPricingMethod.SelectedValue = transportPriceListDetail.PricingMethod;
                ddlPricingMethod_SelectedIndexChanged(null, null);
            }
            else
            {
                ddlPricingMethod.SelectedIndex = 0;
            }

            DropDownList ddlVehicleType = (DropDownList)this.FV_TransportPriceListDetail.FindControl("ddlVehicleType");

            if (transportPriceListDetail.VehicleType != null)
            {
                ddlVehicleType.SelectedValue = transportPriceListDetail.VehicleType;
            }

            Controls_TextBox tbCurrency = (Controls_TextBox)(this.FV_TransportPriceListDetail.FindControl("tbCurrency"));

            if (transportPriceListDetail.Currency != null && transportPriceListDetail.Currency.Code != string.Empty)
            {
                tbCurrency.Text = transportPriceListDetail.Currency.Code;
            }

            TextBox tbStartDate = ((TextBox)(this.FV_TransportPriceListDetail.FindControl("tbStartDate")));

            if (transportPriceListDetail.StartDate != null)
            {
                tbStartDate.Text = transportPriceListDetail.StartDate.ToString("yyyy-MM-dd");
            }

            TextBox tbEndDate = ((TextBox)(this.FV_TransportPriceListDetail.FindControl("tbEndDate")));

            if (transportPriceListDetail.EndDate != null)
            {
                tbEndDate.Text = ((DateTime)transportPriceListDetail.EndDate).ToString("yyyy-MM-dd");
            }

            TextBox tbStartQty = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbStartQty"));
            if (transportPriceListDetail.StartQty != null)
            {
                tbStartQty.Text = transportPriceListDetail.StartQty.Value.ToString("0.########");
            }

            TextBox tbEndQty = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbEndQty"));
            if (transportPriceListDetail.EndQty != null)
            {
                tbEndQty.Text = transportPriceListDetail.EndQty.Value.ToString("0.########");
            }

            TextBox tbMinPrice = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbMinPrice"));
            if (transportPriceListDetail.MinPrice != null)
            {
                tbMinPrice.Text = transportPriceListDetail.MinPrice.Value.ToString("0.########");
            }


            DropDownList ddlTransportMethod = (DropDownList)this.FV_TransportPriceListDetail.FindControl("ddlTransportMethod");

            if (transportPriceListDetail.TransportMethod != null)
            {
                ddlTransportMethod.SelectedValue = transportPriceListDetail.TransportMethod;
            }

            //HtmlSelect transportMethodSelect = (HtmlSelect)(this.FV_TransportPriceListDetail.FindControl("TransportMethodSelect"));
            //IList<CodeMaster> codeMasterList = TheGenericMgr.FindAllWithCustomQuery<CodeMaster>(" from CodeMaster as c where c.Code='TransportMethod' " );
            //string transportMethod=transportPriceListDetail.TransportMethod;
            //foreach (var codeMaster in codeMasterList.OrderBy(c => c.Seq))
            //{
            //    if (transportMethod == codeMaster.Code)
            //    {
            //        transportMethodSelect.Items.Add(new ListItem(codeMaster.Description, codeMaster.Value));
            //    }
            //    else
            //    {
            //        transportMethodSelect.Items.Add(new ListItem(codeMaster.Description, codeMaster.Value));
            //    }
            //}
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
        Controls_TextBox tbShipFrom = ((Controls_TextBox)(this.FV_TransportPriceListDetail.FindControl("tbShipFrom")));
        Controls_TextBox tbShipTo = ((Controls_TextBox)(this.FV_TransportPriceListDetail.FindControl("tbShipTo")));
        com.Sconit.Control.CodeMstrDropDownList ddlPricingMethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_TransportPriceListDetail.FindControl("ddlPricingMethod")));
        DropDownList ddlVehicleType = ((DropDownList)(this.FV_TransportPriceListDetail.FindControl("ddlVehicleType")));
        Controls_TextBox tbCurrency = ((Controls_TextBox)(this.FV_TransportPriceListDetail.FindControl("tbCurrency")));
        TextBox tbEndDate = ((TextBox)(this.FV_TransportPriceListDetail.FindControl("tbEndDate")));
        DropDownList transportMethodSelect = ((DropDownList)(this.FV_TransportPriceListDetail.FindControl("ddlTransportMethod")));

        transportPriceListDetail = (TransportPriceListDetail)e.InputParameters[0];
        transportPriceListDetail.TransportPriceList = TheTransportPriceListMgr.LoadTransportPriceList(this.TransportPriceListCode);
        transportPriceListDetail.ShipFrom = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbShipFrom.Text.Trim()));
        transportPriceListDetail.ShipTo = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbShipTo.Text.Trim()));
        transportPriceListDetail.PricingMethod = ddlPricingMethod.SelectedValue;
        transportPriceListDetail.VehicleType = ddlVehicleType.SelectedValue;
        transportPriceListDetail.Currency = TheCurrencyMgr.LoadCurrency(tbCurrency.Text.Trim());
        transportPriceListDetail.TaxCode = transportPriceListDetail.TaxCode.Trim();
        transportPriceListDetail.EndDate = tbEndDate.Text.Trim() == "" ? null : transportPriceListDetail.EndDate;
        transportPriceListDetail.Type = BusinessConstants.TRANSPORTATION_PRICELIST_DETAIL_TYPE_TRANSPORTATION;
        transportPriceListDetail.TransportMethod = transportMethodSelect.SelectedValue;
    }

    protected void ODS_TransportPriceListDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.TransportPriceListDetail.UpdateTransportPriceListDetail.Successfully");
        btnBack_Click(this, e);
    }

    protected void ODS_TransportPriceListDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.TransportPriceListDetail.DeleteTransportPriceListDetail.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.TransportPriceListDetail.DeleteTransportPriceListDetail.Fail");
            e.ExceptionHandled = true;
        }
    }

    protected void ddlPricingMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        com.Sconit.Control.CodeMstrDropDownList ddlPricingMethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_TransportPriceListDetail.FindControl("ddlPricingMethod")));
        TextBox tbMinVolume = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbMinVolume"));
        DropDownList ddlVehicleType = ((DropDownList)(this.FV_TransportPriceListDetail.FindControl("ddlVehicleType")));

        if (ddlPricingMethod.SelectedValue == BusinessConstants.TRANSPORTATION_PRICING_METHOD_SHIPT)
        {
            tbMinVolume.Enabled = false;
        }
        else
        {
            tbMinVolume.Enabled = true;
        }
        tbMinVolume.Text = "0";

        TextBox tbStartQty = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbStartQty"));
        TextBox tbEndQty = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbEndQty"));
        TextBox tbMinPrice = (TextBox)(this.FV_TransportPriceListDetail.FindControl("tbMinPrice"));

        if (ddlPricingMethod.SelectedValue == BusinessConstants.TRANSPORTATION_PRICING_METHOD_LADDERSTERE)
        {
            tbStartQty.Enabled = true;
            tbEndQty.Enabled = true;
            tbMinPrice.Enabled = true;
        }
        else
        {
            tbStartQty.Enabled = false;
            tbEndQty.Enabled = false;
            tbMinPrice.Enabled = false;
        }

        tbStartQty.Text = "0";
        tbEndQty.Text = "0";

        ddlVehicleType.DataSource = GetVehicleTypeGroup(ddlPricingMethod.SelectedValue);
        ddlVehicleType.DataBind();
    }

    private int GetTransportationAddressId(string fullAddressAndId)
    {
        return Convert.ToInt32(fullAddressAndId.Substring(fullAddressAndId.LastIndexOf("[") + 1,
                                          fullAddressAndId.Length - fullAddressAndId.LastIndexOf("[") - 2));
    }

    private IList<CodeMaster> GetVehicleTypeGroup(string pricingMethod)
    {
        IList<CodeMaster> vehicleTypeGroup = new List<CodeMaster>();
        if (pricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_SHIPT)
        {
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_2T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_5T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_8T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_10T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_12T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_20FOOT));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_40FOOT));
        }
        else if (pricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3 || pricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_LADDERSTERE)
        {
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_SCATTERED));
        }
        else
        {
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_2T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_5T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_8T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_10T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_12T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_20FOOT));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_40FOOT));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_SCATTERED));
        }

        return vehicleTypeGroup;
    }

    private CodeMaster GetVehicleType(string vehicleType)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE, vehicleType);
    }


}
