using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;


public partial class Inventory_PrintHu_FlowDetailList : ModuleBase
{
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

    protected string PartyFromCode
    {
        get
        {
            return (string)ViewState["PartyFromCode"];
        }
        set
        {
            ViewState["PartyFromCode"] = value;
        }
    }

    protected string PartyToCode
    {
        get
        {
            return (string)ViewState["PartyToCode"];
        }
        set
        {
            ViewState["PartyToCode"] = value;
        }
    }

    protected string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    public string FlowType
    {
        get
        {
            return (string)ViewState["FlowType"];
        }
        set
        {
            ViewState["FlowType"] = value;
        }
    }


    public void InitPageParameter(Flow flow)
    {
        this.PartyFromCode = flow.PartyFrom.Code;
        this.PartyToCode = flow.PartyTo.Code;
        this.FlowType = flow.Type;
        this.FlowCode = flow.Code;

        int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);

        if (flow.AllowCreateDetail && false) //新增的Detail打印有问题，暂时不支持
        {
            FlowDetail blankFlowDetail = new FlowDetail();
            if (flow.FlowDetails == null || flow.FlowDetails.Count == 0)
            {
                blankFlowDetail.Sequence = seqInterval;
            }
            else
            {
                int CurrentSeq = flow.FlowDetails.Last<FlowDetail>().Sequence + seqInterval;
                blankFlowDetail.Sequence = CurrentSeq;
            }
            blankFlowDetail.IsBlankDetail = true;
            flow.AddFlowDetail(blankFlowDetail);
        }

        #region 设置默认LotNo
        string lotNo = LotNoHelper.GenerateLotNo();
        foreach (FlowDetail flowDetail in flow.FlowDetails)
        {
            flowDetail.HuLotNo = lotNo;
        }
        #endregion

        this.GV_List.DataSource = flow.FlowDetails;
        this.GV_List.DataBind();

        BindShift();

        if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.TabProd.Visible = false;
            this.GV_List.Columns[8].Visible = true;
        }
        else
        {
            this.TabProd.Visible = true;
            this.GV_List.Columns[8].Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucShift.Date = DateTime.Now;
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblReferenceItemCode = ((Label)e.Row.FindControl("lblReferenceItemCode"));
        TextBox tbOrderQty = (TextBox)e.Row.FindControl("tbOrderQty");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FlowDetail flowDetail = (FlowDetail)e.Row.DataItem;
            Flow flow = flowDetail.Flow;

            #region 处理新增行
            if (flowDetail.IsBlankDetail)
            {
                ((Label)e.Row.FindControl("lblSeq")).Visible = false;
                ((TextBox)e.Row.FindControl("tbSeq")).Visible = true;

                ((Label)e.Row.FindControl("lblItemDescription")).Visible = false;
                ((TextBox)e.Row.FindControl("tbItemDescription")).Visible = true;

                ((TextBox)e.Row.FindControl("lblItemCode")).Visible = false;

                Controls_TextBox tbItemCode = (Controls_TextBox)e.Row.FindControl("tbItemCode");
                tbItemCode.Visible = true;
                tbItemCode.SuggestTextBox.Attributes.Add("onchange", "GenerateFlowDetail(this);");

                ((Controls_TextBox)e.Row.FindControl("tbItemCode")).Visible = true;
                ((Label)e.Row.FindControl("lblReferenceItemCode")).Visible = false;

                ((Label)e.Row.FindControl("lblUom")).Visible = false;
                Controls_TextBox tbUom = (Controls_TextBox)e.Row.FindControl("tbUom");
                tbUom.Visible = true;
                tbUom.SuggestTextBox.Attributes.Add("onchange", "GetUnitPriceByUom(this);");

                ((Label)e.Row.FindControl("lblUnitCount")).Visible = false;
                ((TextBox)e.Row.FindControl("tbUnitCount")).Visible = true;

                ((Label)e.Row.FindControl("lblPackageType")).Visible = false;
                ((com.Sconit.Control.CodeMstrDropDownList)e.Row.FindControl("ddlPackageType")).Visible = true;

                Controls_TextBox tbRefItemCode = (Controls_TextBox)e.Row.FindControl("tbRefItemCode");
                tbRefItemCode.Visible = true;

                tbRefItemCode.ServiceParameter = "string:" + this.PartyFromCode + ",string:" + this.PartyToCode;

                tbRefItemCode.DataBind();
                tbRefItemCode.SuggestTextBox.Attributes.Add("onchange", "GenerateFlowDetailProxyByReferenceItem(this);");

                ((RequiredFieldValidator)e.Row.FindControl("rfvItemCode")).Enabled = true;
                ((RequiredFieldValidator)e.Row.FindControl("rfvUom")).Enabled = true;
                ((RequiredFieldValidator)e.Row.FindControl("rfvUC")).Enabled = true;
                ((RangeValidator)e.Row.FindControl("rvUC")).Enabled = true;

                ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = true;
                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = false;
                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = true;
            }
            #endregion
        }
    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        IList<FlowDetail> flowDetailList = PopulateFlowDetailList(true);

        if (CheckItemExists(flowDetailList))
        {
            return;
        }

        int newRowId = flowDetailList != null ? flowDetailList.Count - 1 : 0;
        GridViewRow newRow = this.GV_List.Rows[newRowId];
        RequiredFieldValidator rfvItemCode = (RequiredFieldValidator)newRow.FindControl("rfvItemCode");
        RequiredFieldValidator rfvUom = (RequiredFieldValidator)newRow.FindControl("rfvUom");
        int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);

        if (rfvItemCode.IsValid && rfvUom.IsValid)
        {
            if (flowDetailList != null)
            {
                Flow flow = flowDetailList[0].Flow;
                FlowDetail flowDetail = flowDetailList.Last<FlowDetail>();
                flowDetailList.RemoveAt(newRowId);

                if (flowDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                {
                    IList<ItemKit> itemKitList = this.TheItemKitMgr.GetChildItemKit(flowDetail.Item);
                    decimal? convertRate = null;

                    foreach (ItemKit itemKit in itemKitList)
                    {
                        if (!convertRate.HasValue)
                        {
                            if (itemKit.ParentItem.Uom.Code != flowDetail.Uom.Code)
                            {
                                convertRate = this.TheUomConversionMgr.ConvertUomQty(flowDetail.Item, flowDetail.Uom, 1, itemKit.ParentItem.Uom);
                            }
                            else
                            {
                                convertRate = 1;
                            }
                        }

                        FlowDetail newKitFlowDetail = new FlowDetail();

                        newKitFlowDetail.Sequence = flowDetailList.Last<FlowDetail>().Sequence + seqInterval;
                        newKitFlowDetail.Item = itemKit.ChildItem;
                        newKitFlowDetail.Uom = itemKit.ChildItem.Uom;
                        newKitFlowDetail.UnitCount = flowDetail.UnitCount * itemKit.Qty * convertRate.Value;
                        newKitFlowDetail.OrderedQty = flowDetail.OrderedQty * itemKit.Qty * convertRate.Value;
                        newKitFlowDetail.PackageType = flowDetail.PackageType;
                        newKitFlowDetail.IsBlankDetail = false;
                        newKitFlowDetail.Flow = flow;

                        flowDetailList.Add(newKitFlowDetail);
                    }
                }
                else
                {
                    flowDetail.IsBlankDetail = false;
                    flowDetailList.Add(flowDetail);
                }

                FlowDetail blankFlowDetail = new FlowDetail();
                blankFlowDetail.Sequence = flowDetailList.Last<FlowDetail>().Sequence + seqInterval;
                blankFlowDetail.IsBlankDetail = true;
                blankFlowDetail.HuLotNo = LotNoHelper.GenerateLotNo();
                flowDetailList.Add(blankFlowDetail);

                this.GV_List.DataSource = flowDetailList;
                this.GV_List.DataBind();
            }
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
        IList<FlowDetail> flowDetailList = PopulateFlowDetailList(true);
        flowDetailList.RemoveAt(rowIndex);
        this.GV_List.DataSource = flowDetailList;
        this.GV_List.DataBind();
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IList<FlowDetail> flowDetailList = this.PopulateFlowDetailList(false);
        IList<FlowDetail> targetFlowDetailList = new List<FlowDetail>();

        if (flowDetailList != null && flowDetailList.Count > 0)
        {
            foreach (FlowDetail flowDetail in flowDetailList)
            {
                if (flowDetail.OrderedQty > 0)
                {
                    targetFlowDetailList.Add(flowDetail);
                }
            }
        }

        if (targetFlowDetailList.Count == 0)
        {
            this.ShowErrorMessage("Inventory.Error.PrintHu.FlowDetail.Required");
            return;
        }

        IList<Hu> huList = null;

        #region  内/外包装
        string packageType = null;
        RadioButtonList rblPackageType = (RadioButtonList)this.Parent.FindControl("rblPackageType");
        if (rblPackageType.SelectedValue == "0")
        {
            packageType = BusinessConstants.CODE_MASTER_PACKAGETYPE_INNER;
        }
        else if (rblPackageType.SelectedValue == "1")
        {
            packageType = BusinessConstants.CODE_MASTER_PACKAGETYPE_OUTER;
        }
        #endregion

        if (this.ModuleType == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER)
        {
            huList = TheHuMgr.CreateHu(targetFlowDetailList, this.CurrentUser, null, packageType);
        }
        else
        {
            EntityPreference entityPreference = this.TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_ID_MARK);
            huList = TheHuMgr.CreateHu(targetFlowDetailList, this.CurrentUser, entityPreference.Value, packageType);
        }

        String huTemplate = "";
        if (this.ModuleType == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION)
        {
            huTemplate = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_HU_TEMPLATE).Value;
        }
        else if (targetFlowDetailList != null
                    && targetFlowDetailList.Count > 0
                    && targetFlowDetailList[0].Flow != null
                    && targetFlowDetailList[0].Flow.HuTemplate != null
                    && targetFlowDetailList[0].Flow.HuTemplate.Length > 0)
        {
            huTemplate = targetFlowDetailList[0].Flow.HuTemplate;
        }

        if (huTemplate != null && huTemplate.Length > 0)
        {

            IList<object> huDetailObj = new List<object>();

            huDetailObj.Add(huList);
            huDetailObj.Add(CurrentUser.Code);

            string barCodeUrl = "";
            if (packageType == BusinessConstants.CODE_MASTER_PACKAGETYPE_OUTER)
            {
                //"BarCode.xls"
                //targetFlowDetailList[0].Flow.HuTemplate
                barCodeUrl = TheReportMgr.WriteToFile(huTemplate, huDetailObj, huTemplate);
            }
            else
            {
                //"InsideBarCodeA4.xls" 
                barCodeUrl = TheReportMgr.WriteToFile("Inside" + huTemplate, huDetailObj, "Inside" + huTemplate);
            }
            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");

            this.ShowSuccessMessage("Inventory.PrintHu.Successful");
        }
    }

    private void BindShift()
    {
        DateTime dateTime = this.tbWinTime.Text.Trim() == string.Empty ? DateTime.Today : DateTime.Parse(this.tbWinTime.Text);
        this.ucShift.BindList(dateTime, this.PartyFromCode);
    }

    //返回订单明细
    private IList<FlowDetail> PopulateFlowDetailList(bool includeBlank)
    {
        if (this.GV_List.Rows != null && this.GV_List.Rows.Count > 0)
        {
            Flow flow = null;
            DateTime? winTime = null;
            IList<FlowDetail> flowDetailList = new List<FlowDetail>();
            if (this.tbWinTime.Text.Trim() != string.Empty)
            {
                winTime = DateTime.Parse(this.tbWinTime.Text.Trim());
            }

            int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
            int lastSeq = 0;

            foreach (GridViewRow row in this.GV_List.Rows)
            {
                HiddenField hfId = (HiddenField)row.FindControl("hfId");
                TextBox tbLotNo = (TextBox)row.FindControl("tbLotNo");
                TextBox tbOrderQty = (TextBox)row.FindControl("tbOrderQty");

                if (hfId.Value != string.Empty && hfId.Value != "0")
                {
                    FlowDetail flowDetail = TheFlowDetailMgr.LoadFlowDetail(int.Parse(hfId.Value));
                    flowDetail.HuLotNo = tbLotNo.Text.Trim() != string.Empty ? tbLotNo.Text.Trim() : null;
                    flowDetail.OrderedQty = tbOrderQty.Text != string.Empty ? decimal.Parse(tbOrderQty.Text) : decimal.Zero;
                    flowDetailList.Add(flowDetail);
                    lastSeq = flowDetail.Sequence;
                    if (flow == null)
                    {
                        flow = flowDetail.Flow;
                    }

                    if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    {
                        flowDetail.HuLotNo = tbLotNo.Text.Trim() != string.Empty ? tbLotNo.Text.Trim() : null;
                    }
                    else
                    {
                        flowDetail.HuLotNo = LotNoHelper.GenerateLotNo(winTime.Value);
                        flowDetail.HuShiftCode = this.ucShift.ShiftCode;
                    }
                }
                else
                {
                    if (!includeBlank)
                    {
                        continue;
                    }

                    if (flow == null)
                    {
                        flow = this.TheFlowMgr.LoadFlow(this.FlowCode);
                    }

                    TextBox tbNewSeq = (TextBox)row.FindControl("tbSeq");
                    Controls_TextBox tbNewItemCode = (Controls_TextBox)row.FindControl("tbItemCode");
                    Controls_TextBox tbNewUom = (Controls_TextBox)row.FindControl("tbUom");
                    com.Sconit.Control.CodeMstrDropDownList ddlPackageType = (com.Sconit.Control.CodeMstrDropDownList)row.FindControl("ddlPackageType");
                    TextBox tbNewUnitCount = (TextBox)row.FindControl("tbUnitCount");
                    TextBox tbNewOrderQty = (TextBox)row.FindControl("tbOrderQty");

                    FlowDetail newFlowDetail = new FlowDetail();
                    newFlowDetail.Sequence = tbNewSeq.Text != string.Empty ? int.Parse(tbNewSeq.Text) : (lastSeq + seqInterval);
                    newFlowDetail.Item = this.TheItemMgr.LoadItem(tbNewItemCode.Text.Trim());
                    newFlowDetail.Uom = this.TheUomMgr.LoadUom(tbNewUom.Text.Trim());
                    newFlowDetail.PackageType = ddlPackageType.SelectedValue;
                    newFlowDetail.UnitCount = tbNewUnitCount.Text.Trim() != string.Empty ? decimal.Parse(tbNewUnitCount.Text) : 1;
                    newFlowDetail.OrderedQty = tbNewOrderQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbNewOrderQty.Text.Trim());
                    newFlowDetail.Flow = flow;
                    newFlowDetail.IsBlankDetail = true;

                    if (flow.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                    {
                        newFlowDetail.HuLotNo = tbLotNo.Text.Trim() != string.Empty ? tbLotNo.Text.Trim() : null;
                    }
                    else
                    {
                        newFlowDetail.HuLotNo = LotNoHelper.GenerateLotNo(winTime.Value);
                        newFlowDetail.HuShiftCode = this.ucShift.ShiftCode;
                    }

                    flowDetailList.Add(newFlowDetail);
                }


            }

            return flowDetailList;
        }

        return null;
    }

    //检查零件是否存在
    private bool CheckItemExists(IList<FlowDetail> flowDetailList)
    {
        int newRowId = flowDetailList != null ? flowDetailList.Count - 1 : 0;
        GridViewRow newRow = this.GV_List.Rows[newRowId];
        Controls_TextBox tbItemCode = (Controls_TextBox)newRow.FindControl("tbItemCode");
        Controls_TextBox tbUom = (Controls_TextBox)newRow.FindControl("tbUom");
        TextBox tbUnitCount = (TextBox)newRow.FindControl("tbUnitCount");
        if (flowDetailList != null)
        {
            Flow flow = flowDetailList[0].Flow;
            IList<Item> itemList = new List<Item>();
            Item item = this.TheItemMgr.LoadItem(tbItemCode.Text.Trim());
            item.Uom = this.TheUomMgr.LoadUom(tbUom.Text.Trim());
            item.UnitCount = decimal.Parse(tbUnitCount.Text.Trim());
            if (item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
            {
                IList<ItemKit> itemKitList = TheItemKitMgr.GetChildItemKit(item);

                if (itemKitList != null && itemKitList.Count > 0)
                {
                    foreach (ItemKit itemKit in itemKitList)
                    {
                        itemList.Add(itemKit.ChildItem);
                    }
                }
                else
                {
                    ShowErrorMessage("ItemKit.Error.NotFoundForParentItem", item.Code);
                    return true;
                }
            }
            else
            {
                itemList.Add(item);
            }

            for (int i = 0; i < flowDetailList.Count - 1; i++)
            {
                foreach (Item checkItem in itemList)
                {
                    string oLocFrom = flow.LocationFrom == null ? null : flow.LocationFrom.Code;
                    string oLocTo = flow.LocationTo == null ? null : flow.LocationTo.Code;
                    string dLocFrom = flowDetailList[i].DefaultLocationFrom == null ? null : flowDetailList[i].DefaultLocationFrom.Code;
                    string dLocTo = flowDetailList[i].DefaultLocationTo == null ? null : flowDetailList[i].DefaultLocationTo.Code;

                    if (flowDetailList[i].Item.Code == checkItem.Code
                        && flowDetailList[i].Item.Uom.Code == checkItem.Uom.Code
                        && oLocFrom == dLocFrom && oLocTo == dLocTo
                        && flowDetailList[i].UnitCount == checkItem.UnitCount)
                    {
                        if (checkItem.Code == item.Code
                            && item.Uom.Code == checkItem.Uom.Code
                            && oLocFrom == dLocFrom && oLocTo == dLocTo
                            && checkItem.UnitCount == item.UnitCount)
                        {
                            ShowErrorMessage("MasterData.Order.OrderDetail.ItemCode.Exists");
                        }
                        else
                        {
                            ShowErrorMessage("MasterData.Order.OrderDetail.ItemCode.Exists2", item.Code);
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
