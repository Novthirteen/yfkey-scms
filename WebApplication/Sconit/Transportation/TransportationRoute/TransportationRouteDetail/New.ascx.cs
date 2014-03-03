using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class Transportation_TransportationRoute_TransportationRouteDetail_New : NewModuleBase
{
    private TransportationRouteDetail transportationRouteDetail;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string TransportationRouteCode
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

    public void PageCleanup()
    {
        ((Literal)(this.FV_TransportationRouteDetail.FindControl("lbCurrentTransportationRoute"))).Text = this.TransportationRouteCode;
        ((TextBox)(this.FV_TransportationRouteDetail.FindControl("tbSequence"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_TransportationRouteDetail.FindControl("tbTAddress"))).Text = string.Empty;
    }

    protected void checkSeqExists(object source, ServerValidateEventArgs args)
    {
        String seq = ((TextBox)(this.FV_TransportationRouteDetail.FindControl("tbSequence"))).Text.Trim();

        IList<TransportationRouteDetail> transportationRouteDetailList = TheTransportationRouteDetailMgr.GetAllTransportationRouteDetail(this.TransportationRouteCode);
        if (transportationRouteDetailList != null && transportationRouteDetailList.Count > 0)
        {
            foreach (TransportationRouteDetail transportationRouteDetail in transportationRouteDetailList)
            {
                if (transportationRouteDetail.Sequence == int.Parse(seq))
                {
                    args.IsValid = false;
                    break;
                }
            }
        }
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvTAddress":
                if (TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(args.Value)) == null)
                {
                    ShowWarningMessage("Transportation.TransportationRouteDetail.TAddress.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }

    protected void ODS_TransportationRouteDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbTAddress = ((Controls_TextBox)(this.FV_TransportationRouteDetail.FindControl("tbTAddress")));

        transportationRouteDetail = (TransportationRouteDetail)e.InputParameters[0];
        transportationRouteDetail.TransportationAddress = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbTAddress.Text.Trim()));
        transportationRouteDetail.TransportationRoute = TheTransportationRouteMgr.LoadTransportationRoute(this.TransportationRouteCode);
    }

    protected void ODS_TransportationRouteDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(transportationRouteDetail.Id, e);
            ShowSuccessMessage("Transportation.TransportationRouteDetail.AddTransportationRouteDetail.Successfully");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private int GetTransportationAddressId(string fullAddressAndId)
    {
        return Convert.ToInt32(fullAddressAndId.Substring(fullAddressAndId.LastIndexOf("[") + 1,
                                          fullAddressAndId.Length - fullAddressAndId.LastIndexOf("[") - 2));
    }
}
