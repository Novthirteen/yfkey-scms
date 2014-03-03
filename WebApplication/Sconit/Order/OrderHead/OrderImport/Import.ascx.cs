using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

public partial class Order_OrderHead_OrderImport_Import : ModuleBase
{
    public event EventHandler ImportEvent;
    public event EventHandler BtnBackClick;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.InitialUI();
    }

    public void Create(object sender)
    {
        try
        {
            IList<OrderHead> orderHeadList = (IList<OrderHead>)sender;
            if (orderHeadList != null && orderHeadList.Count > 0)
            {
                TheOrderMgr.CreateOrder(orderHeadList, this.CurrentUser.Code);
                ShowSuccessMessage("Common.Business.Result.Insert.Successfully");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        this.Import();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BtnBackClick != null)
        {
            BtnBackClick(null, null);
        }
    }

    private void Import()
    {
        try
        {
            string partyCode = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string timePeriodType = this.ucDateSelect.TimePeriodType;
            DateTime date = this.ucDateSelect.Date;

            IList<FlowPlan> flowPlanList = TheImportMgr.ReadShipScheduleYFKFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, this.ModuleType, partyCode, timePeriodType, date);
            IList<OrderHead> ohList = TheOrderMgr.ConvertFlowPlanToOrders(flowPlanList);
            if (ImportEvent != null)
            {
                ImportEvent(new object[] { ohList }, null);
            }
            ShowSuccessMessage("Import.Result.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    private void InitialUI()
    {

        if (ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            this.lblRegion.Text = "${Common.Business.Customer}:";

            this.tbRegion.ServiceMethod = "GetOrderToParty";
            this.tbRegion.ServicePath = "PartyMgr.service";
            this.tbRegion.ServiceParameter = "string:" + ModuleType + ",string:" + this.CurrentUser.Code;

            this.hlTemplate.NavigateUrl = "~/Reports/Templates/ExcelTemplates/ShipSchedule_YFK.xls";
        }
        else if (ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
        {
            this.lblRegion.Text = "${Common.Business.Supplier}:";

            this.tbRegion.ServiceMethod = "GetSupplier";
            this.tbRegion.ServicePath = "SupplierMgr.service";
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;

            this.hlTemplate.Enabled = false;//todo
        }
        else
        {
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;

            this.hlTemplate.Enabled = false;//todo
        }
    }
}
