using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;

public partial class Order_GoodsReceipt_OrderReceipt_NewItemInLocTransList : BusinessModuleBase
{
    public EventHandler InLocTransEvent;
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

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

    public List<TransformerDetail> NewItemInLocTransList
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
            this.InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_PRODUCTIONRECEIVE);
        }
    }

    protected override void BindTransformer()
    {

    }

    protected override void BindTransformerDetail()
    {
        this.GV_List.DataSource = NewItemInLocTransList;
        this.GV_List.DataBind();
    }

    public void InitPageParameter(OrderHead orderHead, bool isChanged)
    {

        BindTransformerDetail();

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
            ResolveInput(huId);
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
     
        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }
}
