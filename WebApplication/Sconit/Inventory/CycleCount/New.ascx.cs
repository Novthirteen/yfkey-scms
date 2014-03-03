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
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

public partial class Inventory_CycleCount_New : NewModuleBase
{
    public event EventHandler SaveEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEffDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string region = this.tbRegion.Text.Trim();
        string location = this.tbLocation.Text.Trim();
        string effectiveDate = this.tbEffDate.Text.Trim();
        DateTime effDate = DateTime.Today;

        if (region == string.Empty)
        {
            ShowErrorMessage("Common.Business.Error.RegionInvalid");
            return;
        }
        else if (location == string.Empty)
        {
            ShowErrorMessage("Common.Business.Error.LocationInvalid");
            return;
        }

        try
        {
            effDate = Convert.ToDateTime(effectiveDate);
        }
        catch (Exception)
        {
            ShowErrorMessage("Common.Business.Error.DateInvalid");
            return;
        }

        CycleCount cycleCount = new CycleCount();
        cycleCount.Location = TheLocationMgr.LoadLocation(location);
        cycleCount.EffectiveDate = effDate;
        cycleCount.Type = this.ddlType.SelectedValue;
        TheCycleCountMgr.CreateCycleCount(cycleCount, this.CurrentUser);
        ShowSuccessMessage("Common.Business.Result.Insert.Successfully");

        if (SaveEvent != null)
        {
            SaveEvent(cycleCount.Code, e);
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
