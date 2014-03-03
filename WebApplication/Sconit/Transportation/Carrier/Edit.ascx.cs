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

public partial class Transportation_Carrier_Edit : EditModuleBase
{
    private Carrier carrier;
    public event EventHandler BackEvent;

    protected string CarrierCode
    {
        get
        {
            return (string)ViewState["CarrierCode"];
        }
        set
        {
            ViewState["CarrierCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.CarrierCode = code;
        this.ODS_Carrier.SelectParameters["code"].DefaultValue = this.CarrierCode;
        this.ODS_Carrier.DeleteParameters["code"].DefaultValue = this.CarrierCode;
    }

    protected void FV_Carrier_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Carrier_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        carrier = (Carrier)e.InputParameters[0];
        carrier.Code = carrier.Code.Trim();
        carrier.Name = carrier.Name.Trim();
        carrier.Country = carrier.Country.Trim();
        carrier.PaymentTerm = carrier.PaymentTerm.Trim();
        carrier.TradeTerm = carrier.TradeTerm.Trim();
        carrier.ReferenceSupplier = carrier.ReferenceSupplier.Trim();
    }

    protected void ODS_Carrier_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.Carrier.UpdateCarrier.Successfully", CarrierCode);
        
    }

    protected void ODS_Carrier_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.Carrier.DeleteCarrier.Successfully", CarrierCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.Carrier.DeleteCarrier.Fail", CarrierCode);
            e.ExceptionHandled = true;
        }
    }
  
}
