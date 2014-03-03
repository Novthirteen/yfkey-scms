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

public partial class Transportation_TransportPriceList_Edit : EditModuleBase
{
    private TransportPriceList transportPriceList;
    public event EventHandler BackEvent;

    protected string TransportPriceListCode
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

    public void InitPageParameter(string code)
    {
        this.TransportPriceListCode = code;
        this.ODS_TransportPriceList.SelectParameters["code"].DefaultValue = this.TransportPriceListCode;
        this.ODS_TransportPriceList.DeleteParameters["code"].DefaultValue = this.TransportPriceListCode;
    }

    protected void FV_TransportPriceList_DataBound(object sender, EventArgs e)
    {
        if (TransportPriceListCode != null && TransportPriceListCode != string.Empty)
        {
            TransportPriceList transportPriceList = (TransportPriceList)((FormView)sender).DataItem;

            ((Literal)(this.FV_TransportPriceList.FindControl("tbParty"))).Text = transportPriceList.Party.Code;

            if (transportPriceList.Party.Type == BusinessConstants.PARTY_TYPE_CARRIER)
            {
                ((Literal)(this.FV_TransportPriceList.FindControl("lblParty"))).Text = "${Transportation.Carrier.Code}:";
            }
            else
            {
                ((Literal)(this.FV_TransportPriceList.FindControl("lblParty"))).Text = "${MasterData.Region.Code}:";
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

    protected void ODS_TransportPriceList_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Literal tbParty = ((Literal)(this.FV_TransportPriceList.FindControl("tbParty")));

        transportPriceList = (TransportPriceList)e.InputParameters[0];
        transportPriceList.Code = transportPriceList.Code.Trim();
        transportPriceList.Party = ThePartyMgr.LoadParty(tbParty.Text.Trim());
    }

    protected void ODS_TransportPriceList_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.TransportPriceList.UpdateTransportPriceList.Successfully", TransportPriceListCode);
        
    }

    protected void ODS_TransportPriceList_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.TransportPriceList.DeleteTransportPriceList.Successfully", TransportPriceListCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.TransportPriceList.DeleteTransportPriceList.Fail", TransportPriceListCode);
            e.ExceptionHandled = true;
        }
    }
}
