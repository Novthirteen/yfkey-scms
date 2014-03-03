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
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_Routing_RoutingDetail_New : NewModuleBase
{
    private RoutingDetail routingDetail;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public string RoutingCode
    {
        get
        {
            return (string)ViewState["RoutingCode"];
        }
        set
        {
            ViewState["RoutingCode"] = value;
        }
    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbRouting"))).Text = this.RoutingCode;
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbStartDate"))).Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbEndDate"))).Text = string.Empty;
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbOperation"))).Text = string.Empty;
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbReference"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbWorkCenter"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbLocation"))).Text = string.Empty;
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbSetupTime"))).Text = "0";
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbRunTime"))).Text = "0";
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbMoveTime"))).Text = "0";
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbTactTime"))).Text = "0";
        ((TextBox)(this.FV_RoutingDetail.FindControl("tbOperation"))).Text = "0";
        Controls_TextBox tbParty = (Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbParty"));
        tbParty.ServiceParameter = "string:" + this.CurrentUser.Code + ",string:";
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        switch (cv.ID)
        {
            case "cvSetupTime":
            case "cvMoveTime":
            case "cvRunTime":
            case "cvTactTime":
                try
                {
                    Convert.ToDecimal(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Common.Validator.Valid.Number");
                    args.IsValid = false;
                }
                break;
            case "cvStartDate":
                try
                {
                    Convert.ToDateTime(args.Value);
                }
                catch (Exception)
                {
                    ShowWarningMessage("Common.Date.Error");
                    args.IsValid = false;
                }
                break;
            case "cvEndDate":
                try
                {
                    if (args.Value.Trim() != "")
                    {
                        string startDate = ((TextBox)(this.FV_RoutingDetail.FindControl("tbStartDate"))).Text.Trim();
                        if (DateTime.Compare(Convert.ToDateTime(startDate), Convert.ToDateTime(args.Value.Trim())) >= 0)
                        {
                            ShowErrorMessage("Common.StarDate.EndDate.Compare");
                            args.IsValid = false;
                        }
                    }
                }
                catch (Exception)
                {
                    ShowWarningMessage("Common.Date.Error");
                    args.IsValid = false;
                }
                break;
            case "cvWorkCenter":
                if (args.Value.Trim() != "")
                {
                    if (TheWorkCenterMgr.LoadWorkCenter(args.Value) == null)
                    {
                        ShowWarningMessage("MasterData.WorkCenter.Code.NotExist", args.Value);
                        args.IsValid = false;
                    }
                }
                break;
            case "cvOperation":
                string routingCode = ((TextBox)(this.FV_RoutingDetail.FindControl("tbRouting"))).Text.Trim();
                string reference = ((TextBox)(this.FV_RoutingDetail.FindControl("tbReference"))).Text.Trim();
                if (TheRoutingDetailMgr.LoadRoutingDetail(routingCode, Convert.ToInt32(args.Value.Trim()), reference) != null)
                {
                    ShowWarningMessage("MasterData.RoutingDetail.UniqueExistError");
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }
    }

    protected void ODS_RoutingDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string routingCode = ((TextBox)(this.FV_RoutingDetail.FindControl("tbRouting"))).Text.Trim();
        string workCenterCode = ((Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbWorkCenter"))).Text.Trim();
        string locationCode = ((Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbLocation"))).Text.Trim();

        routingDetail = (RoutingDetail)e.InputParameters[0];
        if (routingDetail != null)
        {
            routingDetail.Routing = TheRoutingMgr.LoadRouting(routingCode);
            routingDetail.WorkCenter = TheWorkCenterMgr.LoadWorkCenter(workCenterCode);
            routingDetail.Location = TheLocationMgr.LoadLocation(locationCode);
        }
    }

    protected void ODS_RoutingDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(routingDetail.Id.ToString(), e);
            ShowSuccessMessage("Common.Business.Result.Insert.Successfully", routingDetail.Id.ToString());

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
