using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class Inventory_PrintHu_Inventory : ModuleBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.ucList.PrintEvent += new System.EventHandler(this.PrintRender);
        if (!IsPostBack)
        {
          
          
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime? createDate = null;
        try
        {
            if (this.tbCreateDate.Text.Trim() != string.Empty)
            {
                createDate = DateTime.Parse(this.tbCreateDate.Text.Trim());
            }
        }
        catch
        {
            ShowErrorMessage("Common.Date.Error");
        }

        IList<LocationLotDetail> huLocationLotDetailList = TheLocationLotDetailMgr.GetHuLocationLotDetail(this.tbLocation.Text, this.tbArea.Text,
            this.tbBin.Text, this.tbHuId.Text, this.tbItem.Text, this.tbLotNo.Text, false, null, null, null, false, false, createDate, 501);
        if (huLocationLotDetailList.Count() > 500)
        {
            string count = huLocationLotDetailList.Count().ToString();
            huLocationLotDetailList = huLocationLotDetailList.Take(500).ToList();
            ShowWarningMessage("Common.ListCount.Warning.GreatThan500");
        }

        this.ucList.InitPageParameter(huLocationLotDetailList);
        this.ucList.Visible = true;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        this.ucList.PrintCallBack();
    }

    private void PrintRender(object sender, EventArgs e)
    {
        IList<Hu> huList = (IList<Hu>)sender;

        if (huList == null || huList.Count == 0)
        {
            this.ShowErrorMessage("Inventory.Error.PrintHu.LocationLotDetail.Required");
            return;
        }

        IList<object> huDetailObj = new List<object>();

        huDetailObj.Add(huList);
        huDetailObj.Add(CurrentUser.Code);
        string huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
        if (huTemplate != null && huTemplate.Length > 0)
        {
            string barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, "BarCode.xls");
            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");
            this.ShowSuccessMessage("Inventory.PrintHu.Successful");
        }
    }
}
