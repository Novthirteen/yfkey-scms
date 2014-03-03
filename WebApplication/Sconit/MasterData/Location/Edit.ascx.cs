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
using com.Sconit.Entity.MasterData;
using com.Sconit.Control;

public partial class MasterData_Location_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string LocationCode
    {
        get
        {
            return (string)ViewState["LocationCode"];
        }
        set
        {
            ViewState["LocationCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    protected void FV_Location_DataBound(object sender, EventArgs e)
    {
        if (LocationCode != null && LocationCode != string.Empty)
        {
            Location location = (Location)((FormView)sender).DataItem;
        }
    }

    public void InitPageParameter(string code)
    {
        this.LocationCode = code;
        this.ODS_Location.SelectParameters["code"].DefaultValue = this.LocationCode;
        
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void ODS_Location_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.Location.UpdateLocation.Successfully", LocationCode);
        //btnBack_Click(this, e);
    }

    protected void ODS_Location_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Location location = (Location)e.InputParameters[0];
        if (location != null)
        {
            string region = ((TextBox)(this.FV_Location.FindControl("tbRegion"))).Text.Trim();
            location.Region = TheRegionMgr.LoadRegion(region);
            location.Name = location.Name.Trim();
        }
    }

    protected void ODS_Location_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.Location.DeleteLocation.Successfully", LocationCode);
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.Location.DeleteLocation.Fail", LocationCode);
            e.ExceptionHandled = true;
        }
    }
}
