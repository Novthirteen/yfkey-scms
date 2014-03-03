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
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Procurement;
using com.Sconit.Entity.Distribution;

public partial class Order_OrderIssue_ShipItemList : BusinessModuleBase
{
    public event EventHandler ShipEvent;
    public event EventHandler ShipBackEvent;
    public event EventHandler BindInfoEvent;
    public event EventHandler CreatePickListEvent;

    #region ViewState
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    private bool IsPickList
    {
        get { return (bool)ViewState["IsPickList"]; }
        set { ViewState["IsPickList"] = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ucHuList.HuSaveEvent += new EventHandler(this.HuListSave_Render);

        if (!IsPostBack)
        {
            InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_SHIP);
        }
    }

    public void InitPageParameter(List<string> orderNoList)
    {
        this.InitPageParameter(orderNoList, false);
    }
    public void InitPageParameter(List<string> orderNoList, bool createPickList)
    {
        try
        {
            //先清空上次记录
           this.CacheResolver.Transformers = new List<Transformer>();
            
            this.IsPickList = createPickList;
            foreach (string orderNo in orderNoList)
            {
                ResolveInput(orderNo);
            }
            if (IsPickList)
            {
                this.CacheResolver.IsScanHu = false;
                //this.CacheResolver.IsDetailContainHu = false;
            }
            this.ucTransformer.InitPageParameter(this.CacheResolver);

            if (BindInfoEvent != null)
                BindInfoEvent(new object[] { orderNoList[0] }, null);

            this.InitialUI();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnScanHu_Click(object sender, EventArgs e)
    {
        this.ucHuList.Visible = true;
        this.ucHuList.InitPageParameter();
    }

    protected void btnShip_Click(object sender, EventArgs e)
    {
        try
        {
            this.CacheResolver.Transformers = this.ucTransformer.GetTransformer();
            ExecuteSubmit();
            ShowSuccessMessage("MasterData.Distribution.Ship.Successfully", this.CacheResolver.Code);
            if (ShipEvent != null)
            {
                ShipEvent(new object[] { this.CacheResolver.Code, cbPrintAsn.Checked }, null);
            }

        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnCreatePickList_Click(object sender, EventArgs e)
    {
        try
        {
            PickList pickList = ThePickListMgr.CreatePickList(this.ucTransformer.GetTransformer(), this.CurrentUser);
            ShowSuccessMessage("MasterData.PickList.CreatePickList.Successfully", pickList.PickListNo);
            if (CreatePickListEvent != null)
            {
                CreatePickListEvent(pickList.PickListNo, null);
            }
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        if (this.ShipBackEvent != null)
        {
            this.ShipBackEvent(this, e);
        }
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

    private void InitialUI()
    {
        this.lblMessage.Text = string.Empty;

        this.ltlScanBarcode.Visible = !IsPickList && CacheResolver.IsScanHu;
        this.tbScanBarcode.Visible = !IsPickList && CacheResolver.IsScanHu;
        this.btnScanHu.Visible = !IsPickList && CacheResolver.IsScanHu;
        this.cbPrintAsn.Visible = !IsPickList;
        this.btnShip.Visible = !IsPickList;
        this.btnCreatePickList.Visible = IsPickList;

        if (!IsPickList && CacheResolver.IsScanHu)
            this.InitialHuInput();
    }
}
