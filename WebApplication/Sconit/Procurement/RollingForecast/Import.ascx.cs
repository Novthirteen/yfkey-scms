using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;

public partial class Procurement_RollingForecast_Import : ModuleBase
{
    public event EventHandler BtnImportClick;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            IList<RollingForecast> rollingForecastList = TheImportMgr.ReadRollingForecastFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser);
            TheRollingForecastMgr.SaveRollingForecast(rollingForecastList);
            ShowSuccessMessage("Import.Result.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

}
