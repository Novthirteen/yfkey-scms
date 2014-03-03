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
using com.Sconit.Entity;
using com.Sconit.Control;

public partial class MasterData_Routing_New : NewModuleBase
{
    private Routing routing;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        Controls_TextBox tbRegion = ((Controls_TextBox)(this.FV_Routing.FindControl("tbRegion")));
        tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    public void PageCleanup()
    {
        ((TextBox)(this.FV_Routing.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Routing.FindControl("tbDescription"))).Text = string.Empty;
        ((CheckBox)(this.FV_Routing.FindControl("cbIsActive"))).Checked = true;
        Controls_TextBox tbRegion = ((Controls_TextBox)(this.FV_Routing.FindControl("tbRegion")));
        tbRegion.Text = string.Empty;
    }

    protected void CV_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;
        switch (cv.ID)
        {
            case "cvCode":
                if (TheRoutingMgr.LoadRouting(args.Value) != null)
                {
                    ShowWarningMessage("MasterData.Routing.Code.Exist", args.Value);
                    args.IsValid = false;
                }
                break;
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

    protected void ODS_Routing_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Controls_TextBox tbRegion = ((Controls_TextBox)(this.FV_Routing.FindControl("tbRegion")));
        CodeMstrDropDownList ddlRoutingType = ((CodeMstrDropDownList)(this.FV_Routing.FindControl("ddlRoutingType")));
        routing = (Routing)e.InputParameters[0];
        routing.Type = ddlRoutingType.SelectedValue;
        routing.Region = TheRegionMgr.LoadRegion(tbRegion.Text);
    }

    protected void ODS_Routing_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(routing.Code, e);
            ShowSuccessMessage("MasterData.Routing.Insert.Successfully", routing.Code);
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
