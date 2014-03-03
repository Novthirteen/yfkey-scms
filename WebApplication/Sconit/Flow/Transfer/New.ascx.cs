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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Procurement;
using com.Sconit.Service.MasterData.Impl;

public partial class MasterData_Flow_New : NewModuleBase
{
    private Flow flow;
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        Controls_TextBox tbRefFlow = (Controls_TextBox)this.FV_Flow.FindControl("tbRefFlow");
        Controls_TextBox tbPartyFrom = (Controls_TextBox)this.FV_Flow.FindControl("tbPartyFrom");
        Controls_TextBox tbPartyTo = (Controls_TextBox)this.FV_Flow.FindControl("tbPartyTo");
        tbPartyFrom.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER + ",string:" + this.CurrentUser.Code;
        tbPartyFrom.DataBind();
        tbPartyTo.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER + ",string:" + this.CurrentUser.Code;
        tbPartyTo.DataBind();
        tbRefFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        tbRefFlow.DataBind();
        Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Flow.FindControl("tbCarrier"));
        tbCarrier.ServiceParameter = "string:" + this.CurrentUser.Code;
        tbCarrier.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Flow_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

        flow = (Flow)e.InputParameters[0];

        Controls_TextBox tbRefFlow = (Controls_TextBox)this.FV_Flow.FindControl("tbRefFlow");
        Controls_TextBox tbPartyFrom = (Controls_TextBox)this.FV_Flow.FindControl("tbPartyFrom");
        Controls_TextBox tbPartyTo = (Controls_TextBox)this.FV_Flow.FindControl("tbPartyTo");
        Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_Flow.FindControl("tbLocFrom");
        Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_Flow.FindControl("tbLocTo");
        com.Sconit.Control.CodeMstrDropDownList ddlGrGapTo = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Flow.FindControl("ddlGrGapTo");
        com.Sconit.Control.CodeMstrDropDownList ddlCheckDetailOption = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Flow.FindControl("ddlCheckDetailOption");
        com.Sconit.Control.CodeMstrDropDownList ddlOrderTemplate = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Flow.FindControl("ddlOrderTemplate"));
        com.Sconit.Control.CodeMstrDropDownList ddlAsnTemplate = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Flow.FindControl("ddlAsnTemplate"));
        com.Sconit.Control.CodeMstrDropDownList ddlReceiptTemplate = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Flow.FindControl("ddlReceiptTemplate"));
        com.Sconit.Control.CodeMstrDropDownList ddlHuTemplate = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Flow.FindControl("ddlHuTemplate"));

        com.Sconit.Control.CodeMstrDropDownList ddlCreateHuOption = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Flow.FindControl("ddlCreateHuOption");

        Controls_TextBox tbCarrier = (Controls_TextBox)this.FV_Flow.FindControl("tbCarrier");
        Controls_TextBox tbCarrierBillAddress = (Controls_TextBox)this.FV_Flow.FindControl("tbCarrierBillAddress");
        Controls_TextBox tbCurrency = (Controls_TextBox)this.FV_Flow.FindControl("tbCurrency");
        Controls_TextBox tbPriceListTo = (Controls_TextBox)this.FV_Flow.FindControl("tbPriceListTo");
        Controls_TextBox tbTPriceList = (Controls_TextBox)this.FV_Flow.FindControl("tbTPriceList");
        Controls_TextBox tbTRoute = (Controls_TextBox)this.FV_Flow.FindControl("tbTRoute");

        if (tbRefFlow != null && tbRefFlow.Text.Trim() != string.Empty)
        {
            flow.ReferenceFlow = TheFlowMgr.CheckAndLoadFlow(tbRefFlow.Text.Trim()).Code;
        }

        if (tbPartyFrom != null && tbPartyFrom.Text.Trim() != string.Empty)
        {
            flow.PartyFrom = ThePartyMgr.LoadParty(tbPartyFrom.Text.Trim());
        }

        if (tbPartyTo != null && tbPartyTo.Text.Trim() != string.Empty)
        {
            flow.PartyTo = ThePartyMgr.LoadParty(tbPartyTo.Text.Trim());
        }

        if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
        {
            flow.LocationFrom = TheLocationMgr.LoadLocation(tbLocFrom.Text.Trim());
        }
        if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
        {
            flow.LocationTo = TheLocationMgr.LoadLocation(tbLocTo.Text.Trim());
        }
        if (ddlOrderTemplate.SelectedIndex != -1)
        {
            flow.OrderTemplate = ddlOrderTemplate.SelectedValue;
        }
        if (ddlGrGapTo != null && ddlGrGapTo.SelectedIndex != -1)
        {
            flow.GoodsReceiptGapTo = ddlGrGapTo.SelectedValue;
        }
        if (ddlCheckDetailOption != null && ddlCheckDetailOption.SelectedIndex != -1)
        {
            flow.CheckDetailOption = ddlCheckDetailOption.SelectedValue;
        }
        if (ddlAsnTemplate.SelectedIndex != -1)
        {
            flow.AsnTemplate = ddlAsnTemplate.SelectedValue;
        }
        if (ddlReceiptTemplate.SelectedIndex != -1)
        {
            flow.ReceiptTemplate = ddlReceiptTemplate.SelectedValue;
        }
        if (ddlHuTemplate.SelectedIndex != -1)
        {
            flow.HuTemplate = ddlHuTemplate.SelectedValue;
        }
        if (ddlCreateHuOption.SelectedIndex != -1)
        {
             flow.CreateHuOption =  ddlCreateHuOption.SelectedValue;
        }
        if (tbCarrier != null && tbCarrier.Text.Trim() != string.Empty)
        {
            flow.Carrier = TheCarrierMgr.LoadCarrier(tbCarrier.Text.Trim());
        }
        if (tbCarrierBillAddress != null && tbCarrierBillAddress.Text.Trim() != string.Empty)
        {
            flow.CarrierBillAddress = TheAddressMgr.LoadBillAddress(tbCarrierBillAddress.Text.Trim());
        }
        if (tbTPriceList != null && tbTPriceList.Text.Trim() != string.Empty)
        {
            flow.TransportPriceList = TheTransportPriceListMgr.LoadTransportPriceList(tbTPriceList.Text.Trim());
        }
        if (tbTRoute != null && tbTRoute.Text.Trim() != string.Empty)
        {
            flow.TransportationRoute = TheTransportationRouteMgr.LoadTransportationRoute(tbTRoute.Text.Trim());
        }

        if (tbCurrency != null && tbCurrency.Text.Trim() != string.Empty)
        {
            flow.Currency = TheCurrencyMgr.LoadCurrency(tbCurrency.Text.Trim());
        }
        else
        {
            string currencyCode = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_BASE_CURRENCY).Value;
            flow.Currency = TheCurrencyMgr.LoadCurrency(currencyCode);
        }
        flow.BillSettleTerm = null;
        flow.CheckDetailOption = BusinessConstants.CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_NOT_CHECK;
        flow.Type = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER;
        flow.AntiResolveHu = BusinessConstants.CODE_MASTER_ANTI_RESOLVE_HU_VALUE_NOT_RESOLVE;
        flow.CreateUser = this.CurrentUser;
        flow.CreateDate = DateTime.Now;
        flow.LastModifyUser = this.CurrentUser;
        flow.LastModifyDate = DateTime.Now;
        flow.Version = 0;
    }


    protected void ODS_Flow_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(flow.Code, e);
            ShowSuccessMessage("MasterData.Flow.AddFlow.Successfully", flow.Code);
        }
    }

    protected void checkFlowExists(object source, ServerValidateEventArgs args)
    {
        String flowCode = ((TextBox)(this.FV_Flow.FindControl("tbCode"))).Text;
        if (TheFlowMgr.LoadFlow(flowCode) != null)
        {
            args.IsValid = false;
        }

    }

    protected void checkLocation(object source, ServerValidateEventArgs args)
    {

        string locFrom = ((Controls_TextBox)(this.FV_Flow.FindControl("tbLocFrom"))).Text.Trim();
        string locTo = ((Controls_TextBox)(this.FV_Flow.FindControl("tbLocTo"))).Text.Trim();
        if (locFrom == locTo)
        {
            args.IsValid = false;
        }

    }

    public void PageCleanup()
    {
        ((Controls_TextBox)this.FV_Flow.FindControl("tbRefFlow")).Text = string.Empty;
        ((Controls_TextBox)this.FV_Flow.FindControl("tbPartyFrom")).Text = string.Empty;
        ((Controls_TextBox)this.FV_Flow.FindControl("tbPartyTo")).Text = string.Empty;
        ((Controls_TextBox)this.FV_Flow.FindControl("tbLocFrom")).Text = string.Empty;
        ((Controls_TextBox)this.FV_Flow.FindControl("tbLocTo")).Text = string.Empty;
        ((TextBox)(this.FV_Flow.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Flow.FindControl("tbDescription"))).Text = string.Empty;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsActive"))).Checked = true;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsAutoCreate"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsAutoRelease"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsAutoStart"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbNeedPrintOrder"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbAllowExceed"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsAutoReceive"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsListDetail"))).Checked = true;
        ((CheckBox)(this.FV_Flow.FindControl("cbAllowCreateDetail"))).Checked = false;
        ((CheckBox)(this.FV_Flow.FindControl("cbFulfillUC"))).Checked = true;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsShipByOrder"))).Checked = true;
        ((CheckBox)(this.FV_Flow.FindControl("cbIsAsnUniqueReceipt"))).Checked = true;
        ((com.Sconit.Control.CodeMstrDropDownList)this.FV_Flow.FindControl("ddlGrGapTo")).SelectedIndex = 0;
        ((com.Sconit.Control.CodeMstrDropDownList)this.FV_Flow.FindControl("ddlCheckDetailOption")).SelectedIndex = 0;
        ((com.Sconit.Control.CodeMstrDropDownList)this.FV_Flow.FindControl("ddlCreateHuOption")).SelectedIndex = 0;
        ((Controls_TextBox)(this.FV_Flow.FindControl("tbCurrency"))).Text = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_BASE_CURRENCY).Value;
        ((Controls_TextBox)this.FV_Flow.FindControl("tbCarrier")).Text = string.Empty;
        ((Controls_TextBox)this.FV_Flow.FindControl("tbCarrierBillAddress")).Text = string.Empty;
    }
}
