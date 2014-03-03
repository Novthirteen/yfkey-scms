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

public partial class MasterData_Routing_RoutingDetail_Edit : EditModuleBase
{
    private RoutingDetail Routingdetail;
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ODS_RoutingDetail.SelectParameters["Id"].DefaultValue = code;
        this.ODS_RoutingDetail.DeleteParameters["Id"].DefaultValue = code;
        this.UpdateView();
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
                //if (RoutingDetailMgr.LoadRoutingDetail(routingCode, Convert.ToInt32(args.Value.Trim()), reference) != null)
                //{
                //    ShowWarningMessage("MasterData.RoutingDetail.UniqueExistError");
                //    args.IsValid = false;
                //}
                break;
            default:
                break;
        }
    }

    protected void FV_RoutingDetail_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        Routingdetail = TheRoutingDetailMgr.LoadRoutingDetail(Convert.ToInt32(this.code));
        TextBox tbCode = (TextBox)(this.FV_RoutingDetail.FindControl("tbCode"));
        TextBox tbRouting = (TextBox)(this.FV_RoutingDetail.FindControl("tbRouting"));
        TextBox tbStartDate = (TextBox)(this.FV_RoutingDetail.FindControl("tbStartDate"));
        TextBox tbEndDate = (TextBox)(this.FV_RoutingDetail.FindControl("tbEndDate"));
        TextBox tbOperation = (TextBox)(this.FV_RoutingDetail.FindControl("tbOperation"));
        TextBox tbReference = (TextBox)(this.FV_RoutingDetail.FindControl("tbReference"));
        Controls_TextBox tbWorkCenter = (Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbWorkCenter"));
        Controls_TextBox tbLocation = (Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbLocation"));
        TextBox tbSetupTime = (TextBox)(this.FV_RoutingDetail.FindControl("tbSetupTime"));
        TextBox tbRunTime = (TextBox)(this.FV_RoutingDetail.FindControl("tbRunTime"));
        TextBox tbMoveTime = (TextBox)(this.FV_RoutingDetail.FindControl("tbMoveTime"));
        TextBox tbTactTime = (TextBox)(this.FV_RoutingDetail.FindControl("tbTactTime"));
        TextBox tbActivity = (TextBox)(this.FV_RoutingDetail.FindControl("tbActivity"));

        if (Routingdetail != null)
        {
            tbCode.Text = Routingdetail.Id.ToString();
            tbRouting.Text = Routingdetail.Routing.Code;
            tbStartDate.Text = Routingdetail.StartDate.ToString("yyyy-MM-dd");
            if (Routingdetail.EndDate != null)
            {
                tbEndDate.Text = ((DateTime)Routingdetail.EndDate).ToString("yyyy-MM-dd");
            }
            tbOperation.Text = Routingdetail.Operation.ToString();
            tbReference.Text = Routingdetail.Reference;
            tbWorkCenter.Text = Routingdetail.WorkCenter.Code;
            tbLocation.Text = Routingdetail.Location == null ? string.Empty : Routingdetail.Location.Code;
            tbSetupTime.Text = Routingdetail.SetupTime.ToString("0.########");
            tbRunTime.Text = Routingdetail.RunTime.ToString("0.########");
            tbMoveTime.Text = Routingdetail.MoveTime.ToString("0.########");
            tbTactTime.Text = Routingdetail.TactTime.ToString("0.########");
            tbActivity.Text = Routingdetail.Activity;
        }

    }

    protected void ODS_RoutingDetail_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Routingdetail = (RoutingDetail)e.InputParameters[0];

        string routingCode = ((TextBox)(this.FV_RoutingDetail.FindControl("tbRouting"))).Text.Trim();
        string locationCode = ((Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbLocation"))).Text.Trim();
        string workCenterCode = ((Controls_TextBox)(this.FV_RoutingDetail.FindControl("tbWorkCenter"))).Text.Trim();
        string startDate = ((TextBox)(this.FV_RoutingDetail.FindControl("tbStartDate"))).Text.Trim();
        string endDate = ((TextBox)(this.FV_RoutingDetail.FindControl("tbEndDate"))).Text.Trim();

        if (Routingdetail != null)
        {
            Routingdetail.Routing = TheRoutingMgr.LoadRouting(routingCode);
            Routingdetail.Location = TheLocationMgr.LoadLocation(locationCode);
            Routingdetail.WorkCenter = TheWorkCenterMgr.LoadWorkCenter(workCenterCode);
        }
    }

    protected void ODS_RoutingDetail_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        btnBack_Click(this, e);
        ShowSuccessMessage("Common.Business.Result.Update.Successfully");
    }

    protected void ODS_RoutingDetail_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("Common.Business.Result.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }
}
