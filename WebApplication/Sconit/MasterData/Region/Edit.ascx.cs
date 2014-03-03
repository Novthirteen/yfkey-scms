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

public partial class MasterData_Region_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string RegionCode
    {
        get
        {
            return (string)ViewState["RegionCode"];
        }
        set
        {
            ViewState["RegionCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string code)
    {
        this.RegionCode = code;
        this.ODS_Region.SelectParameters["code"].DefaultValue = this.RegionCode;
        this.ODS_Region.DeleteParameters["code"].DefaultValue = this.RegionCode;
    }

    protected void FV_Region_DataBound(object sender, EventArgs e)
    {
        com.Sconit.Control.CodeMstrDropDownList ddlRegionType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Region.FindControl("ddlRegionType");
        Region region = TheRegionMgr.LoadRegion(this.RegionCode);
        ddlRegionType.SelectedValue = region.RegionType;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }


    protected void ODS_Region_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        com.Sconit.Control.CodeMstrDropDownList ddlRegionType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Region.FindControl("ddlRegionType");
        Region region = (Region)e.InputParameters[0];
        region.RegionType = ddlRegionType.SelectedValue;
    }

    protected void ODS_Region_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Region.UpdateRegion.Successfully", RegionCode);

    }

    protected void ODS_Region_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Region.DeleteRegion.Successfully", RegionCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Region.DeleteRegion.Fail", RegionCode);
            e.ExceptionHandled = true;
        }
    }

}
