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
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class MasterData_Address_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string AddressCode
    {
        get
        {
            return (string)ViewState["AddressCode"];
        }
        set
        {
            ViewState["AddressCode"] = value;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( this.AddrType == BusinessConstants.PARTY_ADDRESS_TYPE_SHIP_ADDRESS)
        {
            this.ODS_Address.DataObjectTypeName = "com.Sconit.Entity.MasterData.ShipAddress";
            this.ODS_Address.TypeName = "com.Sconit.Web.ShipAddressMgrProxy";
            this.ODS_Address.UpdateMethod = "UpdateShipAddress";
            this.ODS_Address.SelectMethod = "LoadShipAddress";
            this.ODS_Address.DeleteMethod = "DeleteShipAddress";
        }
    }

    public void InitPageParameter(string code)
    {
        this.AddressCode = code;
        
        this.ODS_Address.SelectParameters["code"].DefaultValue = this.AddressCode;
        this.ODS_Address.DeleteParameters["code"].DefaultValue = this.AddressCode;
        
    }

    protected void FV_Address_DataBound(object sender, EventArgs e)
    {
        ((Literal)(this.FV_Address.FindControl("lbCurrentParty"))).Text = this.PartyCode;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


    protected void ODS_Address_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Address Address = (Address)e.InputParameters[0];
        Address.Party = ThePartyMgr.LoadParty(this.PartyCode);

    }

    protected void ODS_Address_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Address.UpdateAddress.Successfully", AddressCode);
        btnBack_Click(this, e);
    }

    protected void ODS_Address_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Address.DeleteAddress.Successfully", AddressCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Address.DeleteAddress.Fail", AddressCode);
            e.ExceptionHandled = true;
        }
    }

}
