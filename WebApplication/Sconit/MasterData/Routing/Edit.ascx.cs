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
using com.Sconit.Service.MasterData;
using com.Sconit.Control;
using com.Sconit.Entity.MasterData;

public partial class MasterData_Routing_Edit : EditModuleBase
{

    public event EventHandler BackEvent;

    private string RoutingCode
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string RoutingCode)
    {
        this.RoutingCode = RoutingCode;
        this.ODS_Routing.SelectParameters["Code"].DefaultValue = RoutingCode;
        this.ODS_Routing.DeleteParameters["Code"].DefaultValue = RoutingCode;
        this.UpdateView();
    }

    protected void FV_Routing_DataBound(object sender, EventArgs e)
    {
        Controls_TextBox tbRegion = ((Controls_TextBox)(this.FV_Routing.FindControl("tbRegion")));
        tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        tbRegion.DataBind();
        this.UpdateView();
    }

    private void UpdateView()
    {
        Routing routing = TheRoutingMgr.LoadRouting(RoutingCode);
        Controls_TextBox tbRegion = ((Controls_TextBox)(this.FV_Routing.FindControl("tbRegion")));
        CodeMstrDropDownList ddlRoutingType = ((CodeMstrDropDownList)(this.FV_Routing.FindControl("ddlRoutingType")));
        tbRegion.Text = routing.Region == null ? string.Empty : routing.Region.Code;
        ddlRoutingType.SelectedValue = routing.Type;
    }

    protected void ODS_Routing_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbRegion = ((Controls_TextBox)(this.FV_Routing.FindControl("tbRegion")));
        CodeMstrDropDownList ddlRoutingType = ((CodeMstrDropDownList)(this.FV_Routing.FindControl("ddlRoutingType")));
        Routing routing = (Routing)e.InputParameters[0];
        routing.Type = ddlRoutingType.SelectedValue;
        routing.Region = TheRegionMgr.LoadRegion(tbRegion.Text);
    }

    protected void ODS_Routing_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully", RoutingCode);
    }

    protected void ODS_Routing_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Routing.Delete.Successfully", RoutingCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Routing.Delete.Failed", RoutingCode);
            e.ExceptionHandled = true;
        }
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvRegion":
                if (TheRegionMgr.LoadRegion(args.Value) == null)
                {
                    ShowWarningMessage("MasterData.Region.Code.NotExist", args.Value);
                    args.IsValid = false;
                }
                break;
            default:
                break;
        }

    }
}
