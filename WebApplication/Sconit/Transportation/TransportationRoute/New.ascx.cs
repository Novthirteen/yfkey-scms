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

public partial class Transportation_TransportationRoute_New : NewModuleBase
{
    private TransportationRoute transportationRoute;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_TransportationRoute.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_TransportationRoute.FindControl("tbDescription"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_TransportationRoute.FindControl("tbShipFrom"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_TransportationRoute.FindControl("tbShipTo"))).Text = string.Empty;
        ((CheckBox)(this.FV_TransportationRoute.FindControl("cbIsActive"))).Checked = true;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvCode":
                if (TheTransportationRouteMgr.LoadTransportationRoute(args.Value) != null)
                {
                    ShowWarningMessage("Transportation.TransportationRoute.Code.Exists", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvShipFrom":
                if (TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(args.Value)) == null)
                {
                    ShowWarningMessage("Transportation.TransportationRoute.ShipFrom.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            case "cvShipTo":
                if (TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(args.Value)) == null)
                {
                    ShowWarningMessage("Transportation.TransportationRoute.ShipTo.NotExist", args.Value);
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

    protected void ODS_TransportationRoute_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbShipFrom = ((Controls_TextBox)(this.FV_TransportationRoute.FindControl("tbShipFrom")));
        Controls_TextBox tbShipTo = ((Controls_TextBox)(this.FV_TransportationRoute.FindControl("tbShipTo")));

        transportationRoute = (TransportationRoute)e.InputParameters[0];
        transportationRoute.Code = transportationRoute.Code.Trim();
        transportationRoute.Description = transportationRoute.Description.Trim();
        transportationRoute.ShipFrom = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbShipFrom.Text.Trim()));
        transportationRoute.ShipTo = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbShipTo.Text.Trim()));
    }

    protected void ODS_TransportationRoute_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(transportationRoute.Code, e);
            ShowSuccessMessage("Transportation.TransportationRoute.AddTransportationRoute.Successfully", transportationRoute.Code);
        }
    }

    private int GetTransportationAddressId(string fullAddressAndId)
    {
        return Convert.ToInt32(fullAddressAndId.Substring(fullAddressAndId.LastIndexOf("[") + 1,
                                          fullAddressAndId.Length - fullAddressAndId.LastIndexOf("[") - 2));
    }
}
