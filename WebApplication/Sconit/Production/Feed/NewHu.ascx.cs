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
using com.Sconit.Entity.Production;

public partial class Production_Feed_NewHu : BusinessModuleBase
{
    public event EventHandler BackEvent;

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

        this.tbProductLine.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            //this.CacheResolver = new Resolver();
            //this.CacheResolver.UserCode = this.CurrentUser.Code;
            //this.CacheResolver.Transformers = new List<Transformer>();
            //this.CacheResolver.Transformers.Add(new Transformer());
            //this.CacheResolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN;
            this.InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_MATERIALIN);
        }
    }

    protected override void InitialResolver(string userCode, string moduleType)
    {
        base.InitialResolver(userCode, moduleType);
        this.CacheResolver.Transformers = new List<Transformer>();
        this.CacheResolver.Transformers.Add(new Transformer());
    }

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        this.HuInput(this.tbHuScan.Text.Trim());
        InitialHuInput();
    }

    private void HuInput(string huId)
    {
        try
        {
            //this.CacheResolver.Input = huId;
            //TheScanBarcodeMgr.ScanBarcode(this.CacheResolver);
            ResolveInput(huId);
            //BindTransformer(this.TransformerDetailList);
            BindTransformerDetail();
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
        InitialHuInput();

        #region 已投料
        if (tbProductLine.Text.Trim() != string.Empty)
        {
            this.GV_List_Feeded.DataSource = TheProductLineInProcessLocationDetailMgr.GetProductLineInProcessLocationDetail(this.tbProductLine.Text.Trim(), BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE);
            this.GV_List_Feeded.DataBind();

        }
        #endregion

        #region 新投料

        //BindTransformer(this.TransformerDetailList);
        BindTransformerDetail();

        #endregion
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.tbProductLine.Text.Trim() == string.Empty)
            {
                ShowErrorMessage("MasterData.Feed.ProdLine.Empty");
                return;
            }
            UpdateTransformer();
            this.CacheResolver.Code = this.tbProductLine.Text.Trim();
            //TheScanBarcodeMgr.RawMaterialIn(this.CacheResolver);
            ExecuteSubmit();
            ShowSuccessMessage("MasterData.Feed.MaterialIn.Successfully");
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void tbProductLine_TextChanged(object sender, EventArgs e)
    {
        this.CacheResolver.Transformers[0].TransformerDetails = null;
        if (this.tbProductLine.Text.Trim() == string.Empty)
        {
            ShowErrorMessage("MasterData.Feed.ProdLine.Empty");
            return;
        }
        UpdateView();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected override void BindTransformer()
    {
    }

    protected override void BindTransformerDetail()
    {
        this.GV_List.DataSource = this.TransformerDetailList;
        this.GV_List.DataBind();
    }

    //private void BindTransformer(IList<TransformerDetail> transformerDetailList)
    //{
    //    this.GV_List.DataSource = transformerDetailList;
    //    this.GV_List.DataBind();
    //}

    private void UpdateTransformer()
    {
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            TextBox tbOperation = (TextBox)row.FindControl("tbOperation");
            int? operation = null;
            if (tbOperation.Text.Trim() != string.Empty)
            {
                operation = int.Parse(tbOperation.Text.Trim());
            }
            this.TransformerDetailList[i].Operation = operation;
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        try
        {
            TheProductLineInProcessLocationDetailMgr.DeleteProductLineInProcessLocationDetail(id);
            this.ShowSuccessMessage("MasterData.Feed.MaterialIn.Delete.Successfully");
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }

    }
}
