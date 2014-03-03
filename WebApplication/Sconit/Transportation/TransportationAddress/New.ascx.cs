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
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.Transportation;

public partial class Transportation_TransportationAddress_New : NewModuleBase
{
    private TransportationAddress transportationAddress;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    
    public void PageCleanup()
    {
        ((TextBox)(this.FV_TransportationAddress.FindControl("tbCountry"))).Text = string.Empty;
        ((TextBox)(this.FV_TransportationAddress.FindControl("tbProvince"))).Text = string.Empty;
        ((TextBox)(this.FV_TransportationAddress.FindControl("tbCity"))).Text = string.Empty;
        ((TextBox)(this.FV_TransportationAddress.FindControl("tbDistrict"))).Text = string.Empty;
        ((TextBox)(this.FV_TransportationAddress.FindControl("tbAddress"))).Text = string.Empty;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_TransportationAddress_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        transportationAddress = (TransportationAddress)e.InputParameters[0];
        transportationAddress.Country = transportationAddress.Country.Trim();
        transportationAddress.Province = transportationAddress.Province.Trim();
        transportationAddress.City = transportationAddress.City.Trim();
        transportationAddress.District = transportationAddress.District.Trim();
        transportationAddress.Address = transportationAddress.Address.Trim();
    }

    protected void ODS_TransportationAddress_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(transportationAddress.Id.ToString(), e);
            ShowSuccessMessage("Transportation.TransportationAddress.AddTransportationAddress.Successfully");
        }
    }
}
