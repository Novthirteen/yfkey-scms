using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;

public partial class Inventory_Stocktaking_Import : ModuleBase
{
    public event EventHandler BtnImportClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEffDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            CycleCount cycleCount = this.GetCycleCount();
            if (cycleCount == null)
                return;

            IList<CycleCountDetail> cycleCountDetailList = TheImportMgr.ReadCycleCountFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, cycleCount);
            TheCycleCountMgr.SaveCycleCount(cycleCount, cycleCountDetailList, this.CurrentUser);
            ShowSuccessMessage("Import.Result.Successfully");
            if (BtnImportClick != null)
            {
                BtnImportClick(cycleCount.Code, null);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    private CycleCount GetCycleCount()
    {
        string region = this.tbRegion.Text.Trim();
        string location = this.tbLocation.Text.Trim();
        string effectiveDate = this.tbEffDate.Text.Trim();
        DateTime effDate = DateTime.Today;

        if (region == string.Empty)
        {
            ShowErrorMessage("Common.Business.Error.RegionInvalid");
            return null;
        }
        else if (location == string.Empty)
        {
            ShowErrorMessage("Common.Business.Error.LocationInvalid");
            return null;
        }

        try
        {
            effDate = Convert.ToDateTime(effectiveDate);
        }
        catch (Exception)
        {
            ShowErrorMessage("Common.Business.Error.DateInvalid");
            return null;
        }

        CycleCount cycleCount = new CycleCount();
        cycleCount.Location = TheLocationMgr.LoadLocation(location);
        cycleCount.EffectiveDate = effDate;
        cycleCount.Type = this.ddlType.SelectedValue;

        return cycleCount;
    }

}
