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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Inventory_InspectOrder_NewHu : BusinessModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;


    private List<TransformerDetail> TransformerDetailList
    {
        get
        {
            if (CacheResolver != null && CacheResolver.Transformers != null && CacheResolver.Transformers.Count > 0)
            {
                return CacheResolver.Transformers[0].TransformerDetails;
            }
            else
            {
                return null;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //this.CacheResolver = new Resolver();
            //this.CacheResolver.UserCode = this.CurrentUser.Code;
            //this.CacheResolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION;
            //this.CacheResolver.Transformers = new List<Transformer>();
            //this.CacheResolver.Transformers.Add(new Transformer());

            this.InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_INSPECTION);
        }
    }

    protected override void InitialResolver(string userCode, string moduleType)
    {
        base.InitialResolver(userCode, moduleType);

        //this.CacheResolver.Transformers = new List<Transformer>();
        //this.CacheResolver.Transformers.Add(new Transformer());
        this.CacheResolver.IsScanHu = true;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.CacheResolver.Transformers == null || this.CacheResolver.Transformers[0].TransformerDetails.Count == 0)
            {
                ShowErrorMessage("MasterData.InspectOrder.Detail.Empty");
                return;
            }

            ExecuteSubmit();
            ShowSuccessMessage("MasterData.InspectOrder.Create.Successfully", CacheResolver.Code);
            if (CreateEvent != null)
            {
                CreateEvent(CacheResolver.Code, e);
            }
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

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        this.HuInput(this.tbHuScan.Text.Trim());
        this.InitialHuInput();
    }

    protected override void BindTransformer()
    {
    }

    protected override void BindTransformerDetail()
    {
        this.GV_List.DataSource = this.TransformerDetailList;
        this.GV_List.DataBind();
    }

    private void HuInput(string huId)
    {
        try
        {
            //this.CacheResolver.Input = huId;
            //this.CacheResolver = TheScanBarcodeMgr.ScanBarcode(this.CacheResolver);
            ResolveInput(huId);
            //this.BindTransformerDetail();
        }
        catch (BusinessErrorException ex)
        {
            this.lblMessage.Text = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
        }
    }
    private void InitialHuInput()
    {
        this.tbHuScan.Text = string.Empty;
        this.tbHuScan.Focus();
    }

    public void UpdateView()
    {
        if (this.CacheResolver.Transformers != null && this.CacheResolver.Transformers.Count > 0)
        {
            this.CacheResolver.Transformers[0].TransformerDetails = new List<TransformerDetail>();
        }
        BindTransformerDetail();
    }

}