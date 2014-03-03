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

public partial class Inventory_CycleCount_Business : BusinessModuleBase
{
    public event EventHandler BackEvent;

    private bool EditMode
    {
        get { return ViewState["EditMode"] != null ? (bool)ViewState["EditMode"] : false; }
        set { ViewState["EditMode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING);
        }
    }

    public void InitPageParameter(string code)
    {
        this.InitPageParameter(code, false);
    }
    public void InitPageParameter(string code, bool isEditMode)
    {
        try
        {
            InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_STOCKTAKING);
            CycleCount cycleCount = TheCycleCountMgr.LoadCycleCount(code);
            this.CacheResolver.Code = code;
            this.CacheResolver.Status = cycleCount.Status;
            IList<CycleCountDetail> cycleCountDetailList = TheCycleCountDetailMgr.GetCycleCountDetail(code);
            CacheResolver.Transformers = this.ConvertCycleCountToTransformer(cycleCountDetailList);
            this.EditMode = isEditMode;
            this.Refresh();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected override void BindTransformer()
    {
        this.ucItemList.BindList(this.CacheResolver.Transformers, EditMode);
    }

    protected override void BindTransformerDetail()
    {
        this.ucHuList.BindList(GetTransformerDetailList(), EditMode);
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            this.ResolveInput(this.CacheResolver.Code);
            this.EditMode = true;
            this.InitialUI();
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
            TheCycleCountMgr.SubmitCycleCount(this.CacheResolver.Code, this.CurrentUser);
            this.ucEdit.InitPageParameter(this.CacheResolver.Code);
            this.EditMode = false;
            this.CacheResolver.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
            this.InitialUI();
            ShowSuccessMessage("Common.Business.Result.Update.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage("Common.Business.Result.Update.Failed.Reason", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            this.ExecuteSubmit();
            this.EditMode = false;
            this.Refresh();
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
            TheCycleCountMgr.CancelCycleCount(this.CacheResolver.Code, this.CurrentUser);
            this.CacheResolver.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL;
            this.EditMode = false;
            this.Refresh();
            ShowSuccessMessage("MasterData.Order.OrderHead.Cancel.Successfully", this.CacheResolver.Code);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnScanHu_Click(object sender, EventArgs e)
    {
        this.ucHuList.Visible = true;
        this.BindTransformerDetail();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    #region ItemList Event
    protected void ucItemList_ItemInput(object sender, EventArgs e)
    {
        try
        {
            this.ItemInput((string)sender);
        }
        catch (BusinessErrorException ex)
        {
            string message = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
            this.ucItemList.ShowResolverMessage(message);
        }
    }
    //todo, move to service
    private void ItemInput(string itemCode)
    {
        var query = this.CacheResolver.Transformers.Where(t => t.ItemCode == itemCode && t.TransformerDetails == null).Count();
        if (query > 0)
            throw new BusinessErrorException("Common.Business.Error.EntityExist", itemCode);

        Item item = TheItemMgr.CheckAndLoadItem(itemCode);
        Transformer transformer = TransformerHelper.ConvertItemToTransformer(item);
        if (CacheResolver.Transformers == null)
            CacheResolver.Transformers = new List<Transformer>();

        CacheResolver.Transformers.Add(transformer);
        this.ucItemList.ItemInputCallBack(this.CacheResolver.Transformers);
        this.ucItemList.BindList(this.CacheResolver.Transformers, false);
    }
    protected void ucItemList_QtyChanged(object sender, EventArgs e)
    {
        decimal qty = (decimal)((object[])sender)[0];
        int rowIndex = (int)((object[])sender)[1];

        this.CacheResolver.Transformers[rowIndex].CurrentQty = qty;
        BindTransformer();
    }
    #endregion

    #region HuList Event
    protected void ucHuList_HuInput(object sender, EventArgs e)
    {
        try
        {
            this.ResolveInput((string)sender);
            this.ucHuList.ShowResolverMessage(CacheResolver.Result);
        }
        catch (BusinessErrorException ex)
        {
            string message = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
            this.ucHuList.ShowResolverMessage(message);
        }
    }

    protected void ucHuList_Closed(object sender, EventArgs e)
    {
        this.BindTransformer();
    }
    #endregion

    private void InitialUI()
    {
        this.btnEdit.Visible = !this.EditMode && this.CacheResolver.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
        this.btnSave.Visible = this.EditMode && this.CacheResolver.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
        //this.btnScanHu.Visible = this.EditMode && this.CacheResolver.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
        this.btnSubmit.Visible = !this.EditMode && this.CacheResolver.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
        this.btnCancel.Visible = !this.EditMode && this.CacheResolver.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
        this.ucItemList.EditMode = this.EditMode;
        this.BindTransformer();
    }

    private void Refresh()
    {
        this.ucEdit.InitPageParameter(this.CacheResolver.Code);
        this.InitialUI();
    }

    //to be refactored
    private List<Transformer> ConvertCycleCountToTransformer(IList<CycleCountDetail> cycleCountDetailList)
    {
        var qHu =
            from c in cycleCountDetailList
            where c.HuId != null
            group c by c.Item into g
            select new Transformer
            {
                ItemCode = g.Key.Code,
                ItemDescription = g.Key.Description,
                UnitCount = g.Key.UnitCount,
                UomCode = g.Key.Uom.Code,
                Qty = g.Sum(c => c.Qty),
                CurrentQty = g.Sum(c => c.Qty),
                TransformerDetails = new List<TransformerDetail>(
                    from c in cycleCountDetailList
                    where c.Item == g.Key
                    select new TransformerDetail
                    {
                        Id = c.Id,
                        ItemCode = c.Item.Code,
                        ItemDescription = c.Item.Description,
                        UnitCount = c.Item.UnitCount,
                        UomCode = c.Item.Uom.Code,
                        Qty = c.Qty,
                        CurrentQty = c.Qty,
                        HuId = c.HuId != null ? c.HuId : null,
                        StorageBinCode = c.StorageBin != null ? c.StorageBin : null,
                        LotNo = c.LotNo
                    }
                    )
            };


        var qItem =
            from c in cycleCountDetailList
            where c.HuId == null
            select new Transformer
            {
                Id = c.Id,
                ItemCode = c.Item.Code,
                ItemDescription = c.Item.Description,
                UnitCount = c.Item.UnitCount,
                UomCode = c.Item.Uom.Code,
                Qty = c.Qty,
                CurrentQty = c.Qty
            };

        return (qHu.Union(qItem)).ToList<Transformer>();
    }

}
