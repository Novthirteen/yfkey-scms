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

public partial class Warehouse_PutAway_Edit : BusinessModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitialAll();
            InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_PUTAWAY);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected override void BindTransformer()
    {
    }

    protected override void BindTransformerDetail()
    {
        this.ucList.BindList(this.CacheResolver.Transformers[0].TransformerDetails);
    }

    protected void tbScanBarcode_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        this.HuInput(this.tbScanBarcode.Text.Trim());
        this.tbScanBarcode.Text = string.Empty;
        this.tbScanBarcode.Focus();
    }
    
    private void HuInput(string huId)
    {
        try
        {
            ResolveInput(huId);
            this.ltlBin.Text = this.CacheResolver.Result;
        }
        catch (BusinessErrorException ex)
        {
            this.lblMessage.Text = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
        }
    }

    protected void btnPutAway_Click(object sender, EventArgs e)
    {
        try
        {
            ExecuteSubmit();
            ShowSuccessMessage("Common.Business.Result.Save.Successfully");
            this.InitialAll();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    private void InitialAll()
    {
        this.ltlBin.Text = TheLanguageMgr.TranslateMessage("Warehouse.PutAway.PlzScanBin", this.CurrentUser, null);
        this.lblMessage.Text = string.Empty;
        this.ucList.BindList(null);
        this.tbScanBarcode.Focus();
    }
}
