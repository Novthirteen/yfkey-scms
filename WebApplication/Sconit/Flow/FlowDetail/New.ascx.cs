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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Distribution;
using com.Sconit.Service.Procurement;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using System.Collections.Generic;
using com.Sconit.Utility;

public partial class MasterData_FlowDetail_New : ModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler EditEvent;

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

    public string FlowCode
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

    private FlowDetail flowDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            this.FV_FlowDetail.FindControl("trBom").Visible = true;
        }
    }

    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        PageCleanup();
    }

    protected void ODS_FlowDetail_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        flowDetail = (FlowDetail)e.InputParameters[0];
        Flow flow = TheFlowMgr.LoadFlow(FlowCode, true);

        flowDetail.Flow = flow;

        //seq
        if (flowDetail.Sequence == 0)
        {
            int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
            flowDetail.Sequence = seqInterval + FlowHelper.GetMaxFlowSeq(flow);
        }
        Controls_TextBox tbItemCode = (Controls_TextBox)(this.FV_FlowDetail.FindControl("tbItemCode"));
        Controls_TextBox tbUom = (Controls_TextBox)(this.FV_FlowDetail.FindControl("tbUom"));
        if (tbItemCode != null && tbItemCode.Text.Trim() != string.Empty)
        {
            flowDetail.Item = TheItemMgr.LoadItem(tbItemCode.Text.Trim());
        }

        if (tbUom != null && tbUom.Text.Trim() != string.Empty)
        {
            flowDetail.Uom = TheUomMgr.LoadUom(tbUom.Text.Trim());
        }



        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
        {
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProcurementLocTo");
            if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
            {
                flowDetail.LocationTo = TheLocationMgr.LoadLocation(tbLocTo.Text.Trim());
            }
            if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
                || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {
                DropDownList ddlBillSettleTerm = (DropDownList)this.FV_FlowDetail.FindControl("ddlBillSettleTerm");
                if (ddlBillSettleTerm.SelectedIndex != -1)
                {
                    if (ddlBillSettleTerm.SelectedValue == string.Empty)
                    {
                        flowDetail.BillSettleTerm = null;
                    }
                    else
                    {
                        flowDetail.BillSettleTerm = ddlBillSettleTerm.SelectedValue;
                    }
                }

                com.Sconit.Control.CodeMstrDropDownList ddlBarCodeType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_FlowDetail.FindControl("ddlBarCodeType");
                if (ddlBarCodeType.SelectedIndex != -1)
                {
                    flowDetail.BarCodeType = ddlBarCodeType.SelectedValue;
                }

            }
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            DropDownList ddlBillSettleTerm = (DropDownList)this.FV_FlowDetail.FindControl("ddlBillSettleTerm");
            if (ddlBillSettleTerm.SelectedIndex != -1)
            {
                if (ddlBillSettleTerm.SelectedValue == string.Empty)
                {
                    flowDetail.BillSettleTerm = null;
                }
                else
                {
                    flowDetail.BillSettleTerm = ddlBillSettleTerm.SelectedValue;
                }
            }

            DropDownList ddlOddShipOption = (DropDownList)this.FV_FlowDetail.FindControl("ddlOddShipOption");
            if (ddlOddShipOption.SelectedIndex != -1)
            {

                flowDetail.OddShipOption = ddlOddShipOption.SelectedValue;

            }
            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbDistributionLocFrom");
            if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
            {
                flowDetail.LocationFrom = TheLocationMgr.LoadLocation(tbLocFrom.Text.Trim());
            }
            flowDetail.PackagingCode = ((System.Web.UI.HtmlControls.HtmlSelect)this.FV_FlowDetail.FindControl("tbPackagingCode")).Value;
            flowDetail.TransModeCode = ((System.Web.UI.HtmlControls.HtmlSelect)this.FV_FlowDetail.FindControl("tbTransModeCode")).Value;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            Controls_TextBox tbBom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbBom");
            if (tbBom != null && tbBom.Text.Trim() != string.Empty)
            {
                flowDetail.Bom = TheBomMgr.LoadBom(tbBom.Text.Trim());
            }

            TextBox tbBatchSize = (TextBox)this.FV_FlowDetail.FindControl("tbBatchSize");
            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProductionLocFrom");
            if (tbBatchSize.Text.Trim() != string.Empty)
            {
                flowDetail.BatchSize = decimal.Parse(tbBatchSize.Text.Trim());
            }
            if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
            {
                flowDetail.LocationFrom = TheLocationMgr.LoadLocation(tbLocFrom.Text.Trim());
            }
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProductionLocTo");
            if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
            {
                flowDetail.LocationTo = TheLocationMgr.LoadLocation(tbLocTo.Text.Trim());
            }
            Controls_TextBox tbCustomer = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbCustomer");
            if (tbCustomer != null && tbCustomer.Text.Trim() != string.Empty)
            {
                flowDetail.Customer = TheCustomerMgr.LoadCustomer(tbCustomer.Text.Trim());
            }

            com.Sconit.Control.CodeMstrDropDownList ddlBarCodeType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_FlowDetail.FindControl("ddlBarCodeType");
            if (ddlBarCodeType.SelectedIndex != -1)
            {
                flowDetail.BarCodeType = ddlBarCodeType.SelectedValue;
            }
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {
            DropDownList ddlOddShipOption = (DropDownList)this.FV_FlowDetail.FindControl("ddlOddShipOption");
            if (ddlOddShipOption.SelectedIndex != -1)
            {

                flowDetail.OddShipOption = ddlOddShipOption.SelectedValue;

            }

            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbTransferLocFrom");
            if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
            {
                flowDetail.LocationFrom = TheLocationMgr.LoadLocation(tbLocFrom.Text.Trim());
            }
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbTransferLocTo");
            if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
            {
                flowDetail.LocationTo = TheLocationMgr.LoadLocation(tbLocTo.Text.Trim());
            }
        }

        flowDetail.CreateDate = DateTime.Now;
        flowDetail.CreateUser = this.CurrentUser;
        flowDetail.LastModifyDate = DateTime.Now;
        flowDetail.LastModifyUser = this.CurrentUser;
        flowDetail.Version = 0;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected void ODS_FlowDetail_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {

        if (EditEvent != null)
        {
            EditEvent(flowDetail.Id, e);
            decimal unitCount = flowDetail.UnitCount;
            decimal orderLotSize = flowDetail.OrderLotSize == null ? 0 : (decimal)flowDetail.OrderLotSize;
            if (unitCount != 0 && orderLotSize != 0 && orderLotSize % unitCount != 0)
            {
                ShowWarningMessage("MasterData.Flow.FlowDetail.AddFlowDetail.Successfully.UC.Not.Divisible", flowDetail.Sequence.ToString());
            }
            else
            {
                ShowSuccessMessage("MasterData.Flow.FlowDetail.AddFlowDetail.Successfully", flowDetail.Sequence.ToString());
            }
        }

    }

    protected void checkItemExists(object source, ServerValidateEventArgs args)
    {
        Flow flow = TheFlowMgr.LoadFlow(this.FlowCode, true);
        string itemCode = ((Controls_TextBox)(this.FV_FlowDetail.FindControl("tbItemCode"))).Text;
        string uomCode = ((Controls_TextBox)(this.FV_FlowDetail.FindControl("tbUom"))).Text;
        decimal unitCount = decimal.Parse(((TextBox)(this.FV_FlowDetail.FindControl("tbUC"))).Text);
        string locFrom = string.Empty;
        string locTo = string.Empty;

        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
        {
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProcurementLocTo");
            if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
            {
                locTo = tbLocTo.Text.Trim();
            }
            else
            {
                locTo = flow.LocationTo != null ? flow.LocationTo.Code : string.Empty;
            }
            locFrom = string.Empty;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbDistributionLocFrom");
            if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
            {
                locFrom = tbLocFrom.Text.Trim();
            }
            else
            {
                locFrom = flow.LocationFrom != null ? flow.LocationFrom.Code : string.Empty;
            }
            locTo = string.Empty;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {

            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProductionLocFrom");
            if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
            {
                locFrom = tbLocFrom.Text.Trim();
            }
            else
            {
                locFrom = flow.LocationFrom != null ? flow.LocationFrom.Code : string.Empty;
            }
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProductionLocTo");
            if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
            {
                locTo = tbLocTo.Text.Trim();
            }
            else
            {
                locTo = flow.LocationTo != null ? flow.LocationTo.Code : string.Empty;
            }
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {
            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbTransferLocFrom");
            if (tbLocFrom != null && tbLocFrom.Text.Trim() != string.Empty)
            {
                locFrom = tbLocFrom.Text.Trim();
            }
            else
            {
                locFrom = flow.LocationFrom != null ? flow.LocationFrom.Code : string.Empty;
            }
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbTransferLocTo");
            if (tbLocTo != null && tbLocTo.Text.Trim() != string.Empty)
            {
                locTo = tbLocTo.Text.Trim();
            }
            else
            {
                locTo = flow.LocationTo != null ? flow.LocationTo.Code : string.Empty;
            }
        }

        IList<FlowDetail> flowDetailList = flow.FlowDetails;
        if (flowDetailList != null && flowDetailList.Count > 0)
        {
            foreach (FlowDetail flowDetail in flowDetailList)
            {
                string defaultLocFrom = flowDetail.DefaultLocationFrom == null ? string.Empty : flowDetail.DefaultLocationFrom.Code;
                string defaultLocTo = flowDetail.DefaultLocationTo == null ? string.Empty : flowDetail.DefaultLocationTo.Code;
                if (flowDetail.Item.Code == itemCode && flowDetail.Uom.Code == uomCode && defaultLocFrom == locFrom && defaultLocTo == locTo && flowDetail.UnitCount == unitCount)
                {
                    args.IsValid = false;
                    ((CustomValidator)(this.FV_FlowDetail.FindControl("cvItemCheck"))).ErrorMessage = "${MasterData.Flow.FlowDetail.ItemCode.Exists}";
                    break;
                }
            }
        }
        if (flow.ReferenceFlow != null && flow.ReferenceFlow.Trim() != string.Empty && args.IsValid)
        {
            IList<FlowDetail> refFlowDetailList = TheFlowDetailMgr.GetFlowDetail(flow.ReferenceFlow);
            if (refFlowDetailList != null && refFlowDetailList.Count > 0)
            {
                foreach (FlowDetail flowDetail in refFlowDetailList)
                {
                    string defaultLocFrom = flowDetail.DefaultLocationFrom == null ? string.Empty : flowDetail.DefaultLocationFrom.Code;
                    string defaultLocTo = flowDetail.DefaultLocationTo == null ? string.Empty : flowDetail.DefaultLocationTo.Code;
                    if (flowDetail.Item.Code == itemCode && flowDetail.Uom.Code == uomCode && defaultLocFrom == locFrom && defaultLocTo == locTo && flowDetail.UnitCount == unitCount)
                    {
                        args.IsValid = false;
                        ((CustomValidator)(this.FV_FlowDetail.FindControl("cvItemCheck"))).ErrorMessage = "${MasterData.Flow.FlowDetail.ItemCode.Exists}";
                        break;
                    }
                }
            }
        }
    }

    protected void checkSeqExists(object source, ServerValidateEventArgs args)
    {
        String seq = ((TextBox)(this.FV_FlowDetail.FindControl("tbSeq"))).Text.Trim();

        IList<FlowDetail> flowDetailList = TheFlowDetailMgr.GetFlowDetail(this.FlowCode, true);
        if (flowDetailList != null && flowDetailList.Count > 0)
        {
            foreach (FlowDetail flowDetail in flowDetailList)
            {
                if (flowDetail.Sequence == int.Parse(seq))
                {
                    args.IsValid = false;
                    break;
                }
            }
        }
    }

    private void PageCleanup()
    {
        ((Controls_TextBox)(this.FV_FlowDetail.FindControl("tbRefItemCode"))).Text = string.Empty;
        ((Controls_TextBox)this.FV_FlowDetail.FindControl("tbItemCode")).Text = string.Empty;
        ((Controls_TextBox)this.FV_FlowDetail.FindControl("tbUom")).Text = string.Empty;

        ((TextBox)(this.FV_FlowDetail.FindControl("tbUC"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbSeq"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbSafeStock"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbMaxStock"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbMinLotSize"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbOrderLotSize"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbOrderGoodsReceiptLotSize"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbRoundUpOpt"))).Text = string.Empty;


        ((TextBox)(this.FV_FlowDetail.FindControl("tbPackageVol"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbHuLotSize"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbProjectDescription"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbRemark"))).Text = string.Empty;
        ((CheckBox)(this.FV_FlowDetail.FindControl("cbIsAutoCreate"))).Checked = true;

        ((TextBox)(this.FV_FlowDetail.FindControl("tbMRPWeight"))).Text = "1";

        #region   EDI Option
        ((TextBox)(this.FV_FlowDetail.FindControl("tbGrossWeight"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbNetWeight"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbWeightUom"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbCarrierCode"))).Text = string.Empty;
        ((System.Web.UI.HtmlControls.HtmlSelect)this.FV_FlowDetail.FindControl("tbTransModeCode")).Value = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbConveyanceNumber"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbReceivingPlant"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbShipFrom"))).Text = string.Empty;
        ((System.Web.UI.HtmlControls.HtmlSelect)this.FV_FlowDetail.FindControl("tbPackagingCode")).Value = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbLadingQuantity"))).Text = string.Empty;
        ((TextBox)(this.FV_FlowDetail.FindControl("tbUnitsPerContainer"))).Text = string.Empty;
        #endregion

        Literal lblBillSettleTerm = (Literal)this.FV_FlowDetail.FindControl("lblBillSettleTerm");
        DropDownList ddlBillSettleTerm = (DropDownList)this.FV_FlowDetail.FindControl("ddlBillSettleTerm");
        if (ddlBillSettleTerm.Visible)
        {
            ddlBillSettleTerm.SelectedIndex = 0;
        }

        Literal lblOddShipOption = (Literal)this.FV_FlowDetail.FindControl("lblOddShipOption");
        DropDownList ddlOddShipOption = (DropDownList)this.FV_FlowDetail.FindControl("ddlOddShipOption");
        if (ddlOddShipOption.Visible)
        {
            ddlOddShipOption.SelectedIndex = 0;
        }



        Flow flow = TheFlowMgr.LoadFlow(FlowCode);

        Controls_TextBox tbRefItemCode = (Controls_TextBox)(this.FV_FlowDetail.FindControl("tbRefItemCode"));
        tbRefItemCode.ServiceParameter = "string:#tbItemCode,string:" + flow.PartyFrom.Code + ",string:" + flow.PartyTo.Code;
        tbRefItemCode.DataBind();
        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
        {
            this.FV_FlowDetail.FindControl("fdProcurement").Visible = true;
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProcurementLocTo");
            tbLocTo.ServiceParameter = "string:" + flow.PartyTo.Code;
            tbLocTo.DataBind();
            tbLocTo.Text = string.Empty;
            if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
                || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
            {

                lblBillSettleTerm.Visible = true;
                ddlBillSettleTerm.Visible = true;

                BillSettleTermDataBind();
                ((Literal)this.FV_FlowDetail.FindControl("lblNeedInspect")).Visible = true;
                ((CheckBox)(this.FV_FlowDetail.FindControl("cbNeedInspect"))).Visible = true;

                Literal lblIdMark = ((Literal)this.FV_FlowDetail.FindControl("lblIdMark"));
                TextBox tbIdMark = ((TextBox)this.FV_FlowDetail.FindControl("tbIdMark"));
                Literal lblBarCodeType = ((Literal)this.FV_FlowDetail.FindControl("lblBarCodeType"));
                com.Sconit.Control.CodeMstrDropDownList ddlBarCodeType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_FlowDetail.FindControl("ddlBarCodeType");

                lblIdMark.Visible = true;
                tbIdMark.Visible = true;
                lblBarCodeType.Visible = true;
                ddlBarCodeType.Visible = true;
            }
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            lblBillSettleTerm.Visible = true;
            ddlBillSettleTerm.Visible = true;
            lblOddShipOption.Visible = true;
            ddlOddShipOption.Visible = true;

            BillSettleTermDataBind();
            this.FV_FlowDetail.FindControl("fdDistribution").Visible = true;
            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbDistributionLocFrom");
            tbLocFrom.ServiceParameter = "string:" + flow.PartyFrom.Code;
            tbLocFrom.DataBind();
            tbLocFrom.Text = string.Empty;
            this.FV_FlowDetail.FindControl("fdFordEDIOption").Visible = true;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            this.FV_FlowDetail.FindControl("fdProduction").Visible = true;
            this.FV_FlowDetail.FindControl("trBom").Visible = true;
            this.FV_FlowDetail.FindControl("lblCustomer").Visible = true;
            this.FV_FlowDetail.FindControl("lblCustomerItemCode").Visible = true;
            this.FV_FlowDetail.FindControl("tbCustomerItemCode").Visible = true;
            ((Controls_TextBox)this.FV_FlowDetail.FindControl("tbBom")).Text = string.Empty;
            ((TextBox)(this.FV_FlowDetail.FindControl("tbBatchSize"))).Text = string.Empty;
            Controls_TextBox tbCustomer = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbCustomer");

            tbCustomer.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS + ",string:" + this.CurrentUser.Code;
            tbCustomer.DataBind();
            tbCustomer.Visible = true;

            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProductionLocFrom");
            tbLocFrom.ServiceParameter = "string:" + flow.PartyFrom.Code;
            tbLocFrom.DataBind();
            tbLocFrom.Text = string.Empty;
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbProductionLocTo");
            tbLocTo.ServiceParameter = "string:" + flow.PartyTo.Code;
            tbLocTo.DataBind();
            tbLocTo.Text = string.Empty;

            ((Literal)this.FV_FlowDetail.FindControl("lblNeedInspect")).Visible = true;
            ((CheckBox)(this.FV_FlowDetail.FindControl("cbNeedInspect"))).Visible = true;

            Literal lblIdMark = ((Literal)this.FV_FlowDetail.FindControl("lblIdMark"));
            TextBox tbIdMark = ((TextBox)this.FV_FlowDetail.FindControl("tbIdMark"));
            Literal lblBarCodeType = ((Literal)this.FV_FlowDetail.FindControl("lblBarCodeType"));
            com.Sconit.Control.CodeMstrDropDownList ddlBarCodeType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_FlowDetail.FindControl("ddlBarCodeType");

            lblIdMark.Visible = true;
            tbIdMark.Visible = true;
            lblBarCodeType.Visible = true;
            ddlBarCodeType.Visible = true;
            lblIdMark.Text = "${MasterData.Flow.FlowDetail.IdMark.Production}";
            ddlBarCodeType.Code = "FGBarCodeType";
            ddlBarCodeType.DataBind();

        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {
            lblOddShipOption.Visible = true;
            ddlOddShipOption.Visible = true;
            this.FV_FlowDetail.FindControl("fdTransfer").Visible = true;
            Controls_TextBox tbLocFrom = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbTransferLocFrom");
            tbLocFrom.ServiceParameter = "string:" + flow.PartyFrom.Code;
            tbLocFrom.DataBind();
            tbLocFrom.Text = string.Empty;
            Controls_TextBox tbLocTo = (Controls_TextBox)this.FV_FlowDetail.FindControl("tbTransferLocTo");
            tbLocTo.ServiceParameter = "string:" + flow.PartyTo.Code;
            tbLocTo.DataBind();
            tbLocTo.Text = string.Empty;

        }
    }

    private void BillSettleTermDataBind()
    {
        DropDownList ddlBillSettleTerm = (DropDownList)this.FV_FlowDetail.FindControl("ddlBillSettleTerm");
        ddlBillSettleTerm.DataSource = this.GetBillSettleTermGroup(this.ModuleType);
        ddlBillSettleTerm.DataBind();
    }
    private IList<CodeMaster> GetBillSettleTermGroup(string ModuleType)
    {
        IList<CodeMaster> billSettleTermGroup = new List<CodeMaster>();



        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT
            || this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING)
        {
            billSettleTermGroup.Add(new CodeMaster()); //添加空选项
            billSettleTermGroup.Add(GetBillSettleTerm(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT));
            billSettleTermGroup.Add(GetBillSettleTerm(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING));
            billSettleTermGroup.Add(GetBillSettleTerm(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING));
            billSettleTermGroup.Add(GetBillSettleTerm(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION));
        }
        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            billSettleTermGroup.Add(GetBillSettleTerm(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT));
            billSettleTermGroup.Add(GetBillSettleTerm(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_CONSIGNMENT));
        }
        return billSettleTermGroup;
    }
    private CodeMaster GetBillSettleTerm(string billSettleTermValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM, billSettleTermValue);
    }
}
