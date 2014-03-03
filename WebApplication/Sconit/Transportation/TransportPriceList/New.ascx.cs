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

public partial class Transportation_TransportPriceList_New : NewModuleBase
{
    private TransportPriceList transportPriceList;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        Controls_TextBox tbParty = (Controls_TextBox)(this.FV_TransportPriceList.FindControl("tbParty"));
        tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
    }
    
    public void PageCleanup()
    {
        ((TextBox)(this.FV_TransportPriceList.FindControl("tbCode"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_TransportPriceList.FindControl("tbParty"))).Text = string.Empty;
        ((CheckBox)(this.FV_TransportPriceList.FindControl("cbIsActive"))).Checked = true;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvCode":
                if (TheTransportPriceListMgr.LoadTransportPriceList(args.Value) != null)
                {
                    ShowWarningMessage("Transportation.TransportPriceList.Code.Exists", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvParty":
                if (ThePartyMgr.LoadParty(args.Value) == null)
                {
                    ShowWarningMessage("Transportation.TransportPriceList.Party.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_TransportPriceList_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbParty = ((Controls_TextBox)(this.FV_TransportPriceList.FindControl("tbParty")));

        transportPriceList = (TransportPriceList)e.InputParameters[0];
        transportPriceList.Code = transportPriceList.Code.Trim();
        transportPriceList.Party = ThePartyMgr.LoadParty(tbParty.Text.Trim());
    }

    protected void ODS_TransportPriceList_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(transportPriceList.Code, e);
            ShowSuccessMessage("Transportation.TransportPriceList.AddTransportPriceList.Successfully", transportPriceList.Code);
        }
    }
}
