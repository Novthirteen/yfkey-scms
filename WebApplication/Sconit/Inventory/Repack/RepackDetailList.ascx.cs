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

public partial class Inventory_Repack_RepackDetailList : BusinessModuleBase
{
    public event EventHandler RepackEvent;
    public event EventHandler BackEvent;

    public string RepackType
    {
        get
        {
            return (string)ViewState["RepackType"];
        }
        set
        {
            ViewState["RepackType"] = value;
        }
    }

    public bool IsQty
    {
        get
        {
            return (bool)ViewState["IsQty"];
        }
        set
        {
            ViewState["IsQty"] = value;
        }
    }


    private List<TransformerDetail> InTransformerDetailList
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

    private List<TransformerDetail> OutTransformerDetailList
    {
        get
        {
            if (CacheResolver != null && CacheResolver.Transformers != null && CacheResolver.Transformers.Count > 0)
            {
                return CacheResolver.Transformers[1].TransformerDetails;
            }
            else
            {
                return null;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;

        if (!IsPostBack)
        {
            this.InitialResolver(this.CurrentUser.Code, BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK);
        }
    }

    protected override void InitialResolver(string userCode, string moduleType)
    {
        base.InitialResolver(userCode, moduleType);

        //添加两个空的Transformer，投入产出
        this.CacheResolver.Transformers = new List<Transformer>();
        this.CacheResolver.Transformers.Add(new Transformer());
        this.CacheResolver.Transformers.Add(new Transformer());
    }

    public void InitPageParameter(bool isQty)
    {
        this.IsQty = isQty;
        this.CacheResolver.Transformers = new List<Transformer>();
        this.CacheResolver.Transformers.Add(new Transformer());
        this.CacheResolver.Transformers.Add(new Transformer());
        BindTransformerDetail();
        if (this.RepackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
        {
            this.CacheResolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_REPACK;
        }
        else
        {
            this.CacheResolver.ModuleType = BusinessConstants.TRANSFORMER_MODULE_TYPE_DEVANNING;
        }
        if (this.RepackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
        {
            this.ltlInHuScan.Text = Resources.Language.DevanningInHu;
            this.ltlOutHuScan.Text = Resources.Language.DevanningOutHu;
            this.btnDevanning.Visible = true;
            this.btnAdd.Visible = true;
        }
        this.InitialHuScan(BusinessConstants.IO_TYPE_OUT);
        this.InitialHuScan(BusinessConstants.IO_TYPE_IN);


        this.InDiv.Visible = !IsQty;
        this.divLoc.Visible = IsQty;
        this.tbLocation.Text = string.Empty;
    }

    public void InitPageParameter()
    {

        InitPageParameter(false);
    }

    protected void tbInHuScan_TextChanged(object sender, EventArgs e)
    {
        string huId = this.tbInHuScan.Text.Trim();
        InitialHuScan(BusinessConstants.IO_TYPE_IN);


        this.HuScan(huId, BusinessConstants.IO_TYPE_IN);
    }

    protected void tbOutHuScan_TextChanged(object sender, EventArgs e)
    {
        string huId = this.tbOutHuScan.Text.Trim();
        InitialHuScan(BusinessConstants.IO_TYPE_OUT);

        this.HuScan(huId, BusinessConstants.IO_TYPE_OUT);
    }



    private void HuScan(string huId, string type)
    {
        try
        {
            UpdateOutTransformer();
            this.CacheResolver.IOType = type;
            ResolveInput(huId);
        }
        catch (BusinessErrorException ex)
        {
            if (type == BusinessConstants.IO_TYPE_IN)
            {
                this.lblInMessage.Text = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
            }
            if (type == BusinessConstants.IO_TYPE_OUT)
            {
                this.lblOutMessage.Text = TheLanguageMgr.TranslateMessage(ex.Message, this.CurrentUser, ex.MessageParams);
            }
        }
    }

    protected override void BindTransformer()
    {
    }

    protected override void BindTransformerDetail()
    {
        this.GV_InList.DataSource = this.InTransformerDetailList;
        this.GV_InList.DataBind();

        this.GV_OutList.DataSource = this.OutTransformerDetailList;
        this.GV_OutList.DataBind();
    }



    private void InitialHuScan(string type)
    {

        this.lblInMessage.Text = string.Empty;
        this.tbInHuScan.Text = string.Empty;

        this.lblOutMessage.Text = string.Empty;
        this.tbOutHuScan.Text = string.Empty;

        if (type == BusinessConstants.IO_TYPE_IN)
        {
            this.tbInHuScan.Focus();
        }
        if (type == BusinessConstants.IO_TYPE_OUT)
        {
            this.tbOutHuScan.Focus();
        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.InTransformerDetailList == null || this.InTransformerDetailList.Count == 0)
            {
                ShowErrorMessage("MasterData.Inventory.Repack.Devanning.Empty");
                return;
            }
            TransformerDetail newTransformerDetail = CloneHelper.DeepClone<TransformerDetail>(this.InTransformerDetailList[0]);
            newTransformerDetail.HuId = string.Empty;
            newTransformerDetail.Qty = 0;
            newTransformerDetail.UnitCount = 0;
            newTransformerDetail.IOType = BusinessConstants.IO_TYPE_OUT;

            if (this.OutTransformerDetailList == null)
            {
                CacheResolver.Transformers[1].TransformerDetails = new List<TransformerDetail>();
            }

            this.OutTransformerDetailList.Add(newTransformerDetail);
            BindTransformerDetail();

        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void btnRepack_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.RepackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
            {
                if (this.IsQty)
                {
                    if (this.tbLocation.Text.Trim() == string.Empty)
                    {
                        ShowErrorMessage("MasterData.Inventory.Repack.Location.Empty");
                        return;
                    }

                    if (this.OutTransformerDetailList == null || this.OutTransformerDetailList.Count == 0)
                    {
                        ShowErrorMessage("MasterData.Inventory.Repack.Error.RepackDetailEmpty");
                        return;
                    }

                    IList<RepackDetail> repackDetailList = new List<RepackDetail>();
                   

                    IDictionary<string, decimal> ItemDic = new Dictionary<string, decimal>();

                    foreach (TransformerDetail transformerDetail in OutTransformerDetailList)
                    {
                        RepackDetail outRepackDetail = new RepackDetail();

                        outRepackDetail.IOType = BusinessConstants.IO_TYPE_OUT;

                        outRepackDetail.Hu = TheHuMgr.LoadHu(transformerDetail.HuId);
                        outRepackDetail.Qty = outRepackDetail.Hu.Qty * outRepackDetail.Hu.UnitQty;
                    
                        if (ItemDic.ContainsKey(outRepackDetail.Hu.Item.Code))
                        {
                            ItemDic[outRepackDetail.Hu.Item.Code] += outRepackDetail.Qty;
                        }
                        else
                        {
                            ItemDic.Add(outRepackDetail.Hu.Item.Code, outRepackDetail.Qty);
                        }
                        repackDetailList.Add(outRepackDetail);

                    }
                    if (repackDetailList.Count > 0)
                    {
                        foreach (string item in ItemDic.Keys)
                        {
                            IList<LocationLotDetail> locationLotDetailList = TheLocationLotDetailMgr.GetLocationLotDetail(this.tbLocation.Text.Trim(), item, false, false, BusinessConstants.PLUS_INVENTORY, null, false);
                            if (locationLotDetailList == null || locationLotDetailList.Count == 0)
                            {
                                ShowErrorMessage("MasterData.Inventory.Repack.LocationLotDetail.Empty");
                                return;
                            }

                            decimal locQty = (from l in locationLotDetailList select l.Qty).Sum();
                            decimal outQty = ItemDic[item];
                            if (outQty > locQty)
                            {
                                ShowErrorMessage("MasterData.Inventory.LocationLotDetail.LessThanHuQty",item, locQty.ToString("0.########"), outQty.ToString("0.########"));
                                return;
                            }

                            foreach (LocationLotDetail locationLotDetail in locationLotDetailList)
                            {
                                RepackDetail inRepackDetail = new RepackDetail();
                                inRepackDetail.LocationLotDetail = locationLotDetail;
                                inRepackDetail.IOType = BusinessConstants.IO_TYPE_IN;
                                repackDetailList.Add(inRepackDetail);
                                if (locationLotDetail.Qty < outQty)
                                {

                                    inRepackDetail.Qty = locationLotDetail.Qty;
                                    outQty -= inRepackDetail.Qty;
                                }
                                else
                                {
                                    inRepackDetail.Qty = outQty;
                                    break;
                                }
                            }
                        }
                    }


                    Repack repack = TheRepackMgr.CreateRepack(repackDetailList, this.CurrentUser);

                    if (this.IsQty)
                    {
                        RepackEvent(repack.RepackNo, e);
                    }

                }
                else
                {
                    ExecuteSubmit();
                    Repack repack = TheRepackMgr.LoadRepack(this.CacheResolver.Code, true);
                    IList<Hu> huList = new List<Hu>();
                    foreach (RepackDetail repackDet in repack.RepackDetails)
                    {
                        if (repackDet.IOType == BusinessConstants.IO_TYPE_OUT && repackDet.LocationLotDetail.Hu != null
                            && repackDet.LocationLotDetail.Hu.PrintCount == 0)
                        {
                            huList.Add(repackDet.LocationLotDetail.Hu);
                        }
                    }

                    if (huList.Count > 0)
                    {
                        IList<object> huDetailObj = new List<object>();

                        huDetailObj.Add(huList);
                        huDetailObj.Add(CurrentUser.Code);
                        string huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
                        if (huTemplate != null && huTemplate.Length > 0)
                        {
                            string barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, "BarCode.xls");
                            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");

                        }

                    }
                }
            }
            else if (this.RepackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
            {
                UpdateOutTransformer();
                ExecuteSubmit();
            }
            if (RepackEvent != null && !this.IsQty)
            {
                RepackEvent(this.CacheResolver.Code, e);

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
            BackEvent(sender, e);
        }
    }

    protected void btnDevanning_Click(object sender, EventArgs e)
    {
        if (this.InTransformerDetailList == null || this.InTransformerDetailList.Count == 0)
        {
            ShowErrorMessage("MasterData.Inventory.Repack.Devanning.Empty");
            return;
        }
        this.ucRepackDetailInfo.Visible = true;
        this.ucRepackDetailInfo.InitPageParameter(this.InTransformerDetailList[0]);
    }

    protected void GV_OutList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (this.RepackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TransformerDetail transformerDetail = (TransformerDetail)e.Row.DataItem;


                if (transformerDetail.HuId == string.Empty)
                {
                    ((Label)e.Row.FindControl("lblOrderQty")).Visible = false;
                    ((TextBox)e.Row.FindControl("tbOrderQty")).Visible = true;
                }
            }
        }
    }

    private void UpdateOutTransformer()
    {
        for (int i = 0; i < this.GV_OutList.Rows.Count; i++)
        {
            GridViewRow row = this.GV_OutList.Rows[i];

            TextBox tbOrderQty = (TextBox)row.FindControl("tbOrderQty");

            if (tbOrderQty.Text.Trim() != string.Empty)
            {
                this.OutTransformerDetailList[i].Qty = decimal.Parse(tbOrderQty.Text.Trim());
            }
        }
    }
}
