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
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportationAddress_Edit : EditModuleBase
{
    private TransportationAddress transportationAddress;
    public event EventHandler BackEvent;

    protected string TransportationAddressId
    {
        get
        {
            return (string)ViewState["TransportationAddressId"];
        }
        set
        {
            ViewState["TransportationAddressId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string id)
    {
        this.TransportationAddressId = id;
        this.ODS_TransportationAddress.SelectParameters["Id"].DefaultValue = TransportationAddressId;
        this.ODS_TransportationAddress.DeleteParameters["Id"].DefaultValue = TransportationAddressId;
    }

    protected void FV_TransportationAddress_DataBound(object sender, EventArgs e)
    {
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_TransportationAddress_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        transportationAddress = (TransportationAddress)e.InputParameters[0];
        transportationAddress.Country = transportationAddress.Country.Trim();
        transportationAddress.Province = transportationAddress.Province.Trim();
        transportationAddress.City = transportationAddress.City.Trim();
        transportationAddress.District = transportationAddress.District.Trim();
        transportationAddress.Address = transportationAddress.Address.Trim();
    }

    protected void ODS_TransportationAddress_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.TransportationAddress.UpdateTransportationAddress.Successfully");
        
    }

    protected void ODS_TransportationAddress_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.TransportationAddress.DeleteTransportationAddress.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.TransportationAddress.DeleteTransportationAddress.Fail");
            e.ExceptionHandled = true;
        }
    }
}
