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

public partial class Inventory_InspectOrder_New : ModuleBase
{

    public event EventHandler CreateEvent;


    private IDictionary<string, decimal> InspectItemDic
    {
        get
        {
            return (IDictionary<string, decimal>)ViewState["InspectItemDic"];
        }
        set
        {
            ViewState["InspectItemDic"] = value;
        }
    }

    private string LocationCode
    {
        get
        {
            return (string)ViewState["LocationCode"];
        }
        set
        {
            ViewState["LocationCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            InspectItemDic = new Dictionary<string, decimal>();

        }
    }


    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        FillInspectItemDic();

        bool hasDetail = false;

        if (this.InspectItemDic.Count > 0)
        {
            foreach (string fgItemCode in this.InspectItemDic.Keys)
            {
                if (InspectItemDic[fgItemCode] != 0)
                {
                    hasDetail = true;
                    break;
                }
            }
        }
        if (!hasDetail)
        {
            ShowErrorMessage("MasterData.InspectOrder.Detail.Empty");
            return;
        }

        try
        {

            InspectOrder inspectOrder = TheInspectOrderMgr.CreateFgInspectOrder(this.LocationCode, this.InspectItemDic, this.CurrentUser);
            ShowSuccessMessage("MasterData.InspectOrder.Create.Successfully", inspectOrder.InspectNo);
            if (CreateEvent != null)
            {
                CreateEvent(inspectOrder.InspectNo, e);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }


    public void UpdateView()
    {
        this.tbFlow.Text = string.Empty;
        this.InspectItemDic = new Dictionary<string, decimal>();
        this.InitPageParameter();
    }

    public void InitPageParameter()
    {

        IList<InspectItem> inspectItemList = new List<InspectItem>();
        foreach (string fgItemCode in this.InspectItemDic.Keys)
        {
            string[] fgItem = fgItemCode.Split('-');
            string itemCode = fgItem[0];
            string fgCode = fgItem[1];
            string defectClassification = fgItem[2];
             string defectFactor = fgItem[3];
            InspectItem inspectItem = new InspectItem();
            inspectItem.IsBlank = false;
            inspectItem.InspectQty = InspectItemDic[fgItemCode];
            inspectItem.Item = TheItemMgr.LoadItem(itemCode);
            inspectItem.FinishGoods = TheItemMgr.LoadItem(fgCode);
            inspectItem.DefectClassification = defectClassification;
            inspectItem.DefectFactor = defectFactor;
            inspectItemList.Add(inspectItem);
        }

        //新行
        InspectItem blankInspectItem = new InspectItem();
        blankInspectItem.IsBlank = true;
        inspectItemList.Add(blankInspectItem);

        this.GV_List.DataSource = inspectItemList;
        this.GV_List.DataBind();
    }

    protected void tbFlow_Changed(object sender, EventArgs e)
    {
        if (tbFlow.Text.Trim() != string.Empty)
        {
            Flow flow = TheFlowMgr.LoadFlow(tbFlow.Text.Trim());
            if (flow != null)
            {
                this.LocationCode = flow.LocationFrom.Code;
               // this.InspectItemDic.Clear();
                InitPageParameter();
            }
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            InspectItem inspectItem = (InspectItem)e.Row.DataItem;

            if (inspectItem.IsBlank)
            {
                e.Row.FindControl("lblItemCode").Visible = false;
                Controls_TextBox tbItemCode = (Controls_TextBox)e.Row.FindControl("tbItemCode");
                tbItemCode.Visible = true;
                tbItemCode.ServiceParameter = "string:" + tbFlow.Text.Trim();
                tbItemCode.DataBind();
                e.Row.FindControl("lbtnAdd").Visible = true;
            }
            else
            {
                e.Row.FindControl("lblItemCode").Visible = true;
                e.Row.FindControl("tbItemCode").Visible = false;
                com.Sconit.Control.CodeMstrDropDownList tbDefectClassification = (com.Sconit.Control.CodeMstrDropDownList)e.Row.FindControl("tbDefectClassification");
                tbDefectClassification.SelectedValue = inspectItem.DefectClassification;
                com.Sconit.Control.CodeMstrDropDownList tbDefectFactor = (com.Sconit.Control.CodeMstrDropDownList)e.Row.FindControl("tbDefectFactor");
                tbDefectFactor.SelectedValue = inspectItem.DefectFactor;
            }

        }
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        FillInspectItemDic();
        Controls_TextBox tbItemCode = (Controls_TextBox)((LinkButton)sender).Parent.FindControl("tbItemCode");
        TextBox tbInspectQty = (TextBox)((LinkButton)sender).Parent.FindControl("tbInspectQty");
        com.Sconit.Control.CodeMstrDropDownList tbDefectClassification = (com.Sconit.Control.CodeMstrDropDownList)((LinkButton)sender).Parent.FindControl("tbDefectClassification");
        com.Sconit.Control.CodeMstrDropDownList tbDefectFactor = (com.Sconit.Control.CodeMstrDropDownList)((LinkButton)sender).Parent.FindControl("tbDefectFactor");


        Item newItem = TheItemMgr.LoadItem(tbItemCode.Text.Trim());
        IList<BomDetail> bomDetailList = TheBomDetailMgr.GetFlatBomDetail(tbItemCode.Text.Trim(), DateTime.Now);
        foreach (BomDetail bomDetail in bomDetailList)
        {
            if (bomDetail.BackFlushMethod != BusinessConstants.CODE_MASTER_BACKFLUSH_METHOD_VALUE_BATCH_FEED)
            {
                if (InspectItemDic.ContainsKey(bomDetail.Item.Code + "-" + bomDetail.Bom.Code + "-" + tbDefectClassification.Text.Trim()))
                {
                    InspectItemDic[bomDetail.Item.Code + "-" + bomDetail.Bom.Code + "-" + tbDefectClassification.Text.Trim() + "-" + tbDefectFactor.Text.Trim()] += bomDetail.RateQty * decimal.Parse(tbInspectQty.Text.Trim());
                }
                else
                {
                    InspectItemDic.Add(bomDetail.Item.Code + "-" + bomDetail.Bom.Code + "-" + tbDefectClassification.Text.Trim() +"-"+ tbDefectFactor.Text.Trim(), bomDetail.RateQty * decimal.Parse(tbInspectQty.Text.Trim()));

                }
            }
        }
        InitPageParameter();

    }

    private void FillInspectItemDic()
    {
        this.InspectItemDic.Clear();
        for (int i = 0; i < this.GV_List.Rows.Count - 1; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            string itemCode = ((Label)row.FindControl("lblItemCode")).Text.Trim();
            TextBox tbInspectQty = (TextBox)row.FindControl("tbInspectQty");
            string fgCode = ((HiddenField)row.FindControl("hfFgCode")).Value;
            decimal inspectQty = tbInspectQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbInspectQty.Text.Trim());
            com.Sconit.Control.CodeMstrDropDownList tbDefectClassification = (com.Sconit.Control.CodeMstrDropDownList)row.FindControl("tbDefectClassification");
            com.Sconit.Control.CodeMstrDropDownList tbDefectFactor = (com.Sconit.Control.CodeMstrDropDownList)row.FindControl("tbDefectFactor");
            InspectItemDic.Add(itemCode + "-" + fgCode + "-" + tbDefectClassification.SelectedValue +"-"+ tbDefectFactor.SelectedValue, inspectQty);
        }
    }

}
