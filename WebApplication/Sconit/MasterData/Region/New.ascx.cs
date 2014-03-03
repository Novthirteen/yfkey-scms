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

public partial class MasterData_Region_New : NewModuleBase
{
    private Region region;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    public void PageCleanup()
    {
        ((TextBox)(this.FV_Region.FindControl("tbCode"))).Text = string.Empty;
        ((TextBox)(this.FV_Region.FindControl("tbName"))).Text = string.Empty;
        ((CheckBox)(this.FV_Region.FindControl("cbIsActive"))).Checked = true;
    }

    protected void checkRegionExists(object source, ServerValidateEventArgs args)
    {
        string code = ((TextBox)(this.FV_Region.FindControl("tbCode"))).Text;
        if (TheRegionMgr.LoadRegion(code) != null)
        {
            args.IsValid = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

	  protected void btnInsert_Click(object sender, EventArgs e)
    {
        CustomValidator cvInsert = ((CustomValidator)(this.FV_Region.FindControl("cvInsert")));
         if (cvInsert.IsValid)
         {
             TextBox tbCode = (TextBox)(this.FV_Region.FindControl("tbCode"));
             TextBox tbName = (TextBox)(this.FV_Region.FindControl("tbName"));
             CheckBox cbIsActive = (CheckBox)(this.FV_Region.FindControl("cbIsActive"));
             com.Sconit.Control.CodeMstrDropDownList ddlRegionType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Region.FindControl("ddlRegionType");

             Region region = new Region();
             region.Code = tbCode.Text.Trim();
             region.Name = tbName.Text.Trim();
             region.IsActive = cbIsActive.Checked;
             region.RegionType = ddlRegionType.SelectedValue;
             TheRegionMgr.CreateRegion(region, this.CurrentUser);
             if (CreateEvent != null)
             {
                 CreateEvent(region.Code, e);
                 ShowSuccessMessage("MasterData.Region.AddRegion.Successfully", region.Code);
             }
         }
    }
}
