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

public partial class Transportation_TransportationRoute_Edit : EditModuleBase
{
    private TransportationRoute transportationRoute;
    public event EventHandler BackEvent;

    protected string TransportationRouteCode
    {
        get
        {
            return (string)ViewState["TransportationRouteCode"];
        }
        set
        {
            ViewState["TransportationRouteCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.TransportationRouteCode = code;
        this.ODS_TransportationRoute.SelectParameters["code"].DefaultValue = this.TransportationRouteCode;
        this.ODS_TransportationRoute.DeleteParameters["code"].DefaultValue = this.TransportationRouteCode;
    }

    protected void FV_TransportationRoute_DataBound(object sender, EventArgs e)
    {
        if (TransportationRouteCode != null && TransportationRouteCode != string.Empty)
        {
            TransportationRoute transportationRoute = (TransportationRoute)((FormView)sender).DataItem;

            Controls_TextBox tbShipFrom = (Controls_TextBox)this.FV_TransportationRoute.FindControl("tbShipFrom");

            if (transportationRoute.ShipFrom != null && transportationRoute.ShipFrom.Id.ToString() != string.Empty)
            {
                tbShipFrom.Text = transportationRoute.ShipFrom.FullAddressAndId;
            }

            Controls_TextBox tbShipTo = (Controls_TextBox)this.FV_TransportationRoute.FindControl("tbShipTo");

            if (transportationRoute.ShipTo != null && transportationRoute.ShipTo.Id.ToString() != string.Empty)
            {
                tbShipTo.Text = transportationRoute.ShipTo.FullAddressAndId;
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

    protected void ODS_TransportationRoute_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbShipFrom = ((Controls_TextBox)(this.FV_TransportationRoute.FindControl("tbShipFrom")));
        Controls_TextBox tbShipTo = ((Controls_TextBox)(this.FV_TransportationRoute.FindControl("tbShipTo")));

        transportationRoute = (TransportationRoute)e.InputParameters[0];
        transportationRoute.Code = transportationRoute.Code.Trim();
        transportationRoute.Description = transportationRoute.Description.Trim();
        transportationRoute.ShipFrom = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbShipFrom.Text.Trim()));
        transportationRoute.ShipTo = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbShipTo.Text.Trim()));
    }

    protected void ODS_TransportationRoute_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.TransportationRoute.UpdateTransportationRoute.Successfully", TransportationRouteCode);
        
    }

    protected void ODS_TransportationRoute_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.TransportationRoute.DeleteTransportationRoute.Successfully", TransportationRouteCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.TransportationRoute.DeleteTransportationRoute.Fail", TransportationRouteCode);
            e.ExceptionHandled = true;
        }
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
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

    private int GetTransportationAddressId(string fullAddressAndId)
    {
        return Convert.ToInt32(fullAddressAndId.Substring(fullAddressAndId.LastIndexOf("[") + 1,
                                          fullAddressAndId.Length - fullAddressAndId.LastIndexOf("[") - 2));
    }
}
