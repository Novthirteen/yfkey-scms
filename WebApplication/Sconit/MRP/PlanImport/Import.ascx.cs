using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;

public partial class MRP_PlanImport_Import : ModuleBase
{
    public event EventHandler ImportEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.InitialUI();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        this.Import();
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
        this.hlTemplate.Enabled = false;//todo

        if (ModuleType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
        {
            this.lblRegion.Text = "${Common.Business.Customer}:";

            this.tbRegion.ServiceMethod = "GetCustomer";
            this.tbRegion.ServicePath = "CustomerMgr.service";
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;

            //this.hlTemplate.NavigateUrl = "~/Reports/Templates/ExcelTemplates/ShipSchedule_YFK.xls";
        }
        else if (ModuleType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
        {
            this.lblRegion.Text = "${Common.Business.Supplier}:";

            this.tbRegion.ServiceMethod = "GetSupplier";
            this.tbRegion.ServicePath = "SupplierMgr.service";
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
        else
        {
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
    }
}
