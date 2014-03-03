using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Utility;
using System.Collections;

public partial class Inventory_Stocktaking_Business : ModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler TabEvent;
    public string OrderNo
    {
        get
        {
            return (string)ViewState["OrderNo"];
        }
        set
        {
            ViewState["OrderNo"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public void InitPageParameter(string code)
    {
        this.OrderNo = code;
        CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(code);
        this.ucEdit.InitPageParameter(cycleCount);

        InitialUI();


        //全盘 不显示 库格明细和物料明细
        if (cycleCount.Type == BusinessConstants.CODE_MASTER_PHYCNT_TYPE_WHOLECHECK)
        {
            this.ucStorageBinList.Visible = false;
            this.ucItemList.Visible = false;
            this.btnSave.Visible = false;
        }
        else //抽盘
        {
            // 不选择 是否扫描条码 就没有库格明细
            if (!cycleCount.IsScanHu)
            {
                this.ucStorageBinList.Visible = false;
            }
            else
            {
                this.ucStorageBinList.InitPageParameter(cycleCount);
                this.ucStorageBinList.Visible = true;
            }

            this.ucItemList.InitPageParameter(cycleCount);
            ucItemList.Visible = true;
        }

        if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            //this.ucCycleCountResultList.InitPageParameter(cycleCount);
        }

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheCycleCountMgr.DeleteCycleCount(this.OrderNo);
            ShowSuccessMessage("Common.Business.Result.Delete.Successfully");
            if (BackEvent != null)
            {
                BackEvent(sender, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(this.OrderNo);
            if (cycleCount.Type == BusinessConstants.CODE_MASTER_PHYCNT_TYPE_WHOLECHECK)
            {
                TheCycleCountMgr.ReleaseCycleCount(this.OrderNo, this.CurrentUser);
            }
            else
            {
                if (cycleCount.IsScanHu)
                {
                    cycleCount.Bins = this.ucStorageBinList.GetBins();
                }
                cycleCount.Items = this.ucItemList.GetItems();
                TheCycleCountMgr.ReleaseCycleCount(cycleCount, this.CurrentUser);
            }
            if (TabEvent != null)
            {
                TabEvent(new object[] { true, this.OrderNo }, e);
            }

            ShowSuccessMessage("Common.Business.Result.Submit.Successfully");
            this.InitPageParameter(this.OrderNo);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(this.OrderNo);

            if (cycleCount.IsScanHu)
            {
                cycleCount.Bins = this.ucStorageBinList.GetBins();
            }
            cycleCount.Items = this.ucItemList.GetItems();
            TheCycleCountMgr.UpdateCycleCount(cycleCount);
            this.Refresh();

            if (TabEvent != null)
            {
                TabEvent(new object[] { false, "" }, e);
            }

            ShowSuccessMessage("Common.Business.Result.Update.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage("Common.Business.Result.Update.Failed.Reason", ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            TheCycleCountMgr.CancelCycleCount(this.OrderNo, this.CurrentUser);
            this.Refresh();

            if (TabEvent != null)
            {
                TabEvent(new object[] { false, "" }, e);
            }

            ShowSuccessMessage("Common.Business.Result.Cancel.Successfully", this.OrderNo);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {

        try
        {
            TheCycleCountMgr.CompleteCycleCount(this.OrderNo, this.CurrentUser);

            ShowSuccessMessage("Common.Business.Result.Complete.Successfully");
            this.Refresh();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(this.OrderNo);

        IList<object> cycleCountList = new List<object>();
        IList<CycleCount> cycleCountListT = new List<CycleCount>();
        cycleCountListT.Add(cycleCount);
        cycleCountList.Add(cycleCountListT);
        cycleCountList.Add(CurrentUser.Code);
        string printUrl = TheReportMgr.WriteToFile("Stocktaking.xls", cycleCountList, "Stocktaking.xls");
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(this.OrderNo);
            IList<CycleCountDetail> cycleCountDetailList = TheImportMgr.ReadCycleCountFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser, cycleCount);
            this.TheCycleCountMgr.RecordCycleCountDetail(this.OrderNo, cycleCountDetailList, this.CurrentUser);
            ShowSuccessMessage("Import.Result.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    private void InitialUI()
    {
        CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(this.OrderNo);
        if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            this.btnSave.Visible = true;
            this.btnDelete.Visible = true;
            this.btnSubmit.Visible = true;
            this.btnCancel.Visible = false;
            this.btnComplete.Visible = false;
            this.btnPrint.Visible = false;
            this.btnImport.Visible = false;
            this.fileUpload.Visible = false;
            this.hlTemplate.Visible = false;
        }
        else if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
        {
            this.btnSave.Visible = false;
            this.btnDelete.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnCancel.Visible = true;
            this.btnComplete.Visible = true;
            this.btnPrint.Visible = true;
            this.btnImport.Visible = false;
            this.fileUpload.Visible = false;
            this.hlTemplate.Visible = false;
        }
        else if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            this.btnSave.Visible = false;
            this.btnDelete.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnCancel.Visible = true;
            this.btnComplete.Visible = true;
            this.btnPrint.Visible = true;
            this.btnImport.Visible = true;
            this.fileUpload.Visible = true;
            this.hlTemplate.Visible = true;
        }
        else if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
        {
            this.btnSave.Visible = false;
            this.btnDelete.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnCancel.Visible = false;
            this.btnComplete.Visible = false;
            this.btnPrint.Visible = false;
            this.btnImport.Visible = false;
            this.fileUpload.Visible = false;
            this.hlTemplate.Visible = false;
        }
        else if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
        {
            this.btnSave.Visible = false;
            this.btnDelete.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnCancel.Visible = false;
            this.btnComplete.Visible = false;
            this.btnPrint.Visible = false;
            this.btnImport.Visible = false;
            this.fileUpload.Visible = false;
            this.hlTemplate.Visible = false;
        }
        else if (cycleCount.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)
        {
            this.btnSave.Visible = false;
            this.btnDelete.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnCancel.Visible = false;
            this.btnComplete.Visible = false;
            this.btnPrint.Visible = false;
            this.btnImport.Visible = false;
            this.fileUpload.Visible = false;
            this.hlTemplate.Visible = false;
        }
    }

    private void Refresh()
    {
        this.ucEdit.InitPageParameter(this.OrderNo);
        this.InitialUI();
    }

}
