using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity.Procurement;
using System.Web.UI;

public partial class Order_GoodsReceipt_OrderReceipt_ReceiptItemList : BusinessModuleBase
{
    public event EventHandler ReceiveEvent;
    public event EventHandler BackEvent;

    #region ViewState
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    #endregion

    private List<Transformer> transformerList
    {
        get { return this.ucTransformer.GetTransformer(); }
    }

    private string ExternalRecNo
    {
        get { return this.tbRefNo.Text.Trim() != string.Empty ? this.tbRefNo.Text.Trim() : null; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucHuList.HuSaveEvent += new EventHandler(this.HuListSave_Render);

        if (!IsPostBack)
        {
            this.ltlReceiptDetails.Text = TheLanguageMgr.TranslateMessage("Receipt.ReceiptDetails", this.CurrentUser);
            InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE);
        }
    }

    public void InitPageParameter(string orderNo)
    {
        try
        {
            this.InitialAll();
            this.CacheResolver.Transformers = this.ucTransformer.GetTransformer();
            ResolveInput(orderNo);
            this.cbPrintReceipt.Checked = this.CacheResolver.NeedPrintReceipt;

            this.ucTransformer.InitPageParameter(this.CacheResolver);

            this.InitialUI();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }

    protected void btnReceive_Click(object sender, EventArgs e)
    {
        if (this.tbRefNo.Visible && this.tbRefNo.Text.Trim() == string.Empty)
        {
            ShowErrorMessage("MasterData.ReceiveOrder.RefNo.Empty");
            return;
        }
        try
        {
            this.CacheResolver.Transformers = this.ucTransformer.GetTransformer();
            this.CacheResolver.ExternalOrderNo = this.ExternalRecNo;
            ExecuteSubmit();
            //Receipt receipt = TheOrderMgr.ReceiveOrder(recDetList, this.CurrentUser, this.Ip, this.ExternalRecNo);
            ShowSuccessMessage("Receipt.Receive.Successfully", this.CacheResolver.Code);
            if (ReceiveEvent != null)
            {
                ReceiveEvent(new object[] { this.CacheResolver.Code, this.cbPrintReceipt.Checked }, null);
            }

            this.InitialAll();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected override void BindTransformer()
    {
        this.ucTransformer.BindTransformer(this.CacheResolver.Transformers);
    }

    protected override void BindTransformerDetail()
    {
        
    }

    protected void btnScanHu_Click(object sender, EventArgs e)
    {
        this.ucHuList.Visible = true;
        this.ucHuList.InitPageParameter();
    }

    protected void tbScanBarcode_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        this.HuInput(this.tbScanBarcode.Text.Trim());
        this.InitialHuInput();
    }

    private void HuInput(string huId)
    {
        try
        {
            this.CacheResolver.Transformers = this.ucTransformer.GetTransformer();
            ResolveInput(huId);
            BindTransformer();
        }
        catch (BusinessErrorException ex)
        {
            this.lblMessage.Text = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
        }
    }

    private void InitialHuInput()
    {
        this.tbScanBarcode.Text = string.Empty;
        this.tbScanBarcode.Focus();
    }

    void HuListSave_Render(object sender, EventArgs e)
    {
        IList<Hu> huList = (IList<Hu>)((object[])sender)[0];
        foreach (Hu hu in huList)
        {
            this.HuInput(hu.HuId);
        }
    }

    private void InitialAll()
    {
        this.lblMessage.Text = string.Empty;
        this.ucTransformer.BindTransformer(null);//todo
        this.tbRefNo.Text = string.Empty;

        InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_RECEIVE);
    }

    private void InitialUI()
    {
        this.trBarcode.Visible = CacheResolver.IsScanHu;
        this.btnScanHu.Visible = CacheResolver.IsScanHu;

        this.lblRefNo.Visible = (this.CacheResolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION);
        this.tbRefNo.Visible = (this.CacheResolver.OrderType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION);

        if (this.CacheResolver.IsScanHu)
            this.InitialHuInput();
    }
}
