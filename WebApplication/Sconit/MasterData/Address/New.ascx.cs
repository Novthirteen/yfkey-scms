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

public partial class MasterData_Address_New : NewModuleBase
{
    private Address Address;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string PartyCode
    {
        get
        {
            return (string)ViewState["PartyCode"];
        }
        set
        {
            ViewState["PartyCode"] = value;
        }
    }
    public string AddrType
    {
        get
        {
            return (string)ViewState["AddrType"];
        }
        set
        {
            ViewState["AddrType"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.AddrType == BusinessConstants.PARTY_ADDRESS_TYPE_SHIP_ADDRESS)
        {
            this.ODS_Address.DataObjectTypeName = "com.Sconit.Entity.MasterData.ShipAddress";
            this.ODS_Address.TypeName = "com.Sconit.Web.ShipAddressMgrProxy";
            this.ODS_Address.InsertMethod = "CreateShipAddress";
        }
    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_Address.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbSequence"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbAddress"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbPostalCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbPostalCodeExtention"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbTelephoneNumber"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbMobilePhone"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbContactPersonName"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbFax"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbEmail"))).Text = string.Empty;
        ((TextBox)(this.FV_Address.FindControl("tbWebSite"))).Text = string.Empty;
        ((CheckBox)(this.FV_Address.FindControl("tbIsPrimary"))).Checked = false;
        ((CheckBox)(this.FV_Address.FindControl("tbIsActive"))).Checked = true;
        ((Literal)(this.FV_Address.FindControl("lbCurrentParty"))).Text = this.PartyCode;
    }

    protected void ODS_Address_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (this.AddrType == BusinessConstants.PARTY_ADDRESS_TYPE_BILL_ADDRESS)
        {
            Address = (BillAddress)e.InputParameters[0];
        }
        else if (this.AddrType == BusinessConstants.PARTY_ADDRESS_TYPE_SHIP_ADDRESS)
        {
            Address = (ShipAddress)e.InputParameters[0];
        }
        Address.Party = ThePartyMgr.LoadParty(this.PartyCode);
    }

    protected void ODS_Address_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(Address.Code, e);
            ShowSuccessMessage("MasterData.Address.AddAddress.Successfully", Address.Code);
        }
    }

    protected void checkAddressExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_Address.FindControl("tbCode"))).Text;
        if (TheAddressMgr.LoadAddress(code) != null)
        {
            args.IsValid = false;
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
