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
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;

public partial class Transportation_TransportationRoute_TransportationRouteDetail_Edit : EditModuleBase
{
    private TransportationRouteDetail transportationRouteDetail;

    public event EventHandler BackEvent;

    protected string TransportationRouteDetailId
    {
        get
        {
            return (string)ViewState["TransportationRouteDetailId"];
        }
        set
        {
            ViewState["TransportationRouteDetailId"] = value;
        }
    }

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

    public void InitPageParameter(string id)
    {
        this.TransportationRouteDetailId = id;

        this.ODS_TransportationRouteDetail.SelectParameters["Id"].DefaultValue = this.TransportationRouteDetailId;
        this.ODS_TransportationRouteDetail.DeleteParameters["Id"].DefaultValue = this.TransportationRouteDetailId;        
    }

    protected void FV_TransportationRouteDetail_DataBound(object sender, EventArgs e)
    {
        ((Literal)(this.FV_TransportationRouteDetail.FindControl("lbCurrentTransportationRoute"))).Text = this.TransportationRouteCode;

        if (TransportationRouteDetailId != null && TransportationRouteDetailId != string.Empty)
        {
            TransportationRouteDetail transportationRouteDetail = (TransportationRouteDetail)((FormView)sender).DataItem;

            Controls_TextBox tbTAddress = (Controls_TextBox)this.FV_TransportationRouteDetail.FindControl("tbTAddress");

            if (transportationRouteDetail.TransportationAddress != null && transportationRouteDetail.TransportationAddress.Id.ToString() != string.Empty)
            {
                tbTAddress.Text = transportationRouteDetail.TransportationAddress.FullAddressAndId;
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
    
    protected void ODS_TransportationRouteDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbTAddress = ((Controls_TextBox)(this.FV_TransportationRouteDetail.FindControl("tbTAddress")));

        transportationRouteDetail = (TransportationRouteDetail)e.InputParameters[0];
        transportationRouteDetail.TransportationAddress = TheTransportationAddressMgr.LoadTransportationAddress(GetTransportationAddressId(tbTAddress.Text.Trim()));
        transportationRouteDetail.TransportationRoute = TheTransportationRouteMgr.LoadTransportationRoute(this.TransportationRouteCode);
    }

    protected void ODS_TransportationRouteDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("Transportation.TransportationRouteDetail.UpdateTransportationRouteDetail.Successfully");
        btnBack_Click(this, e);
    }

    protected void ODS_TransportationRouteDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Transportation.TransportationRouteDetail.DeleteTransportationRouteDetail.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Transportation.TransportationRouteDetail.DeleteTransportationRouteDetail.Fail");
            e.ExceptionHandled = true;
        }
    }

    protected void checkSeqExists(object source, ServerValidateEventArgs args)
    {
        String seq = ((TextBox)(this.FV_TransportationRouteDetail.FindControl("tbSequence"))).Text.Trim();

        IList<TransportationRouteDetail> transportationRouteDetailList = TheTransportationRouteDetailMgr.GetAllTransportationRouteDetail(this.TransportationRouteCode);
        if (transportationRouteDetailList != null && transportationRouteDetailList.Count > 0)
        {
            foreach (TransportationRouteDetail transportationRouteDetail in transportationRouteDetailList)
            {
                if (transportationRouteDetail.Sequence == int.Parse(seq) && transportationRouteDetail.Id.ToString() != this.TransportationRouteDetailId)
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

    private int GetTransportationAddressId(string fullAddressAndId)
    {
        return Convert.ToInt32(fullAddressAndId.Substring(fullAddressAndId.LastIndexOf("[") + 1,
                                          fullAddressAndId.Length - fullAddressAndId.LastIndexOf("[") - 2));
    }
}
