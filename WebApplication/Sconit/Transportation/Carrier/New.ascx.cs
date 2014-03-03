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

public partial class Transportation_Carrier_New : NewModuleBase
{
    private Carrier carrier;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    public void PageCleanup()
    {
        ((TextBox)(this.FV_Carrier.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Carrier.FindControl("tbName"))).Text = string.Empty;
        ((CheckBox)(this.FV_Carrier.FindControl("cbIsActive"))).Checked = true;
        ((TextBox)(this.FV_Carrier.FindControl("tbCountry"))).Text = string.Empty;
        ((TextBox)(this.FV_Carrier.FindControl("tbPaymentTerm"))).Text = string.Empty;
        ((TextBox)(this.FV_Carrier.FindControl("tbTradeTerm"))).Text = string.Empty;
        ((TextBox)(this.FV_Carrier.FindControl("tbReferenceSupplier"))).Text = string.Empty;
    }

    protected void checkCarrierExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_Carrier.FindControl("tbCode"))).Text.Trim();

        if (TheCarrierMgr.LoadCarrier(code) != null)
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

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        CustomValidator cvInsert = ((CustomValidator)(this.FV_Carrier.FindControl("cvInsert")));
        if (cvInsert.IsValid)
        {
            carrier = new Carrier();

            carrier.Code = ((TextBox)(this.FV_Carrier.FindControl("tbCode"))).Text.Trim();
            carrier.Name = ((TextBox)(this.FV_Carrier.FindControl("tbName"))).Text.Trim();
            carrier.IsActive = ((CheckBox)(this.FV_Carrier.FindControl("cbIsActive"))).Checked;
            carrier.Country = ((TextBox)(this.FV_Carrier.FindControl("tbCountry"))).Text.Trim();
            carrier.PaymentTerm = ((TextBox)(this.FV_Carrier.FindControl("tbPaymentTerm"))).Text.Trim();
            carrier.TradeTerm = ((TextBox)(this.FV_Carrier.FindControl("tbTradeTerm"))).Text.Trim();
            carrier.ReferenceSupplier = ((TextBox)(this.FV_Carrier.FindControl("tbReferenceSupplier"))).Text.Trim();

            TheCarrierMgr.CreateCarrier(carrier, this.CurrentUser);
            if (CreateEvent != null)
            {
                CreateEvent(carrier.Code, e);
                ShowSuccessMessage("Transportation.Carrier.AddCarrier.Successfully", carrier.Code);
            }
        }
    }
}
