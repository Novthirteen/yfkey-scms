
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
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Entity.Exception;


public partial class MasterData_MiscOrder_TransEdit : EditModuleBase
{
    public event EventHandler BackEvent;
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
    public MiscOrder MiscOrder
    {
        get
        {
            return (MiscOrder)ViewState["MiscOrder"];
        }
        set
        {
            ViewState["MiscOrder"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbMiscOrderRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GI))
            //this.tbMiscOrderReason.ServiceParameter = "string:" + "StockInReason";
            this.ddlReason.Code = "StockInReason";
        if (ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR))
            //this.tbMiscOrderReason.ServiceParameter = "string:" + "StockOutReason";
            this.ddlReason.Code = "StockOutReason";
        if (ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_ADJ))
            //this.tbMiscOrderReason.ServiceParameter = "string:" + "StockAdjReason";
            this.ddlReason.Code = "StockAdjReason";

        this.ddlReason.DataBind();
        this.tbMiscOrderRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
    }
    public void ViewMiscOrder(string actionParameter)
    {
        if (actionParameter != null)
        {
            MiscOrder = TheMiscOrderMgr.ReLoadMiscOrder(actionParameter);
        }
        this.updateView(false);
        noEditable(true);
    }

    private bool buildMiscOrderDetails(bool saveHead)
    {
        bool result = true;
        int rowCount = saveHead ? this.MiscOrderDetailsGV.Rows.Count - 1 : this.MiscOrderDetailsGV.Rows.Count;
        IList<MiscOrderDetail> miscOrderDetails = new List<MiscOrderDetail>();
        for (int i = 0; i < rowCount; i++)
        {
            MiscOrderDetail miscOrderDetail = new MiscOrderDetail();
            Controls_TextBox tbItemCode = this.MiscOrderDetailsGV.Rows[i].FindControl("tbItemCode") as Controls_TextBox;
            if (tbItemCode.Text.Length == 0)
            {
                Label lblItemCode = this.MiscOrderDetailsGV.Rows[i].FindControl("lblItemCode") as Label;
                if (lblItemCode.Text.Length == 0)
                {
                    continue;
                }
                else
                {
                    miscOrderDetail.Item = TheItemMgr.LoadItem(lblItemCode.Text);
                }
            }
            else
            {
                miscOrderDetail.Item = TheItemMgr.LoadItem(tbItemCode.Text);
            }

            TextBox tbGridQtyTextBox = this.MiscOrderDetailsGV.Rows[i].FindControl("tbQty") as TextBox;
            string gridRowInputQty = tbGridQtyTextBox.Text.Trim();
            //int m;
            //if (!int.TryParse(gridRowInputQty, out m))
            //{
            //    ShowErrorMessage("MasterData.MiscOrder.WarningMessage.InputNoIntegerValue");
            //    result = false;
            //}
            //decimal InputQty = decimal.Parse(gridRowInputQty);
            //if ((this.ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GI)
            //    || this.ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR)) && InputQty <= 0)
            //{
            //    ShowErrorMessage("MasterData.MiscOrder.WarningMessage.InputQtyMustThanZero");
            //    result = false;
            //    return result;
            //}
            //if (this.ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_ADJ) && InputQty == 0)
            //{
            //    ShowErrorMessage("MasterData.MiscOrder.WarningMessage.InputQtyDontEqualsZero");
            //    result = false;
            //    return result;
            //}
            miscOrderDetail.Qty = decimal.Parse(tbGridQtyTextBox.Text.Trim());

            miscOrderDetail.IsBlankDetail = false;
            miscOrderDetails.Add(miscOrderDetail);
        }
        MiscOrder.MiscOrderDetails = miscOrderDetails;
        return result;
    }


    private bool savePreCheck()
    {
        Controls_TextBox tbItemCode = this.MiscOrderDetailsGV.Rows[0].FindControl("tbItemCode") as Controls_TextBox;
        if (tbItemCode.Text.Length <= 0)
        {
            Label lblItemCode = this.MiscOrderDetailsGV.Rows[0].FindControl("lblItemCode") as Label;
            if (lblItemCode.Text.Length <= 0)
            {
                return false;
            }
        }
        return true;
    }

    private bool MiscOrderViewStateSave(bool saveHead)
    {
        return MiscOrderSave(false, saveHead);
    }
    private bool MiscOrderDBSave()
    {
        return MiscOrderSave(true, true);
    }
    private bool MiscOrderSave(bool savetodb, bool saveHead)
    {
        if (saveHead)
        {
            string tbEffectDate = this.tbMiscOrderEffectDate.Text != string.Empty ? this.tbMiscOrderEffectDate.Text.Trim() : string.Empty;
            if (tbEffectDate == string.Empty)
            {
                ShowErrorMessage("MasterData.MiscOrder.WarningMessage.EffectDateEmpty");
                return false;
            }

            MiscOrder.Reason = this.ddlReason.SelectedIndex != -1 ? this.ddlReason.SelectedValue : string.Empty;
            MiscOrder.Remark = this.tbMiscOrderDescription.Text;
            MiscOrder.Type = this.ModuleType;
            MiscOrder.Location = this.TheLocationMgr.LoadLocation(this.tbMiscOrderLocation.Text);
            MiscOrder.EffectiveDate = DateTime.Parse(this.tbMiscOrderEffectDate.Text);
            MiscOrder.ReferenceOrderNo = this.tbRefNo.Text.Trim();
        }
        if (buildMiscOrderDetails(saveHead))
        {
            if (savetodb)
            {
                if (savePreCheck())
                {
                    MiscOrder = TheMiscOrderMgr.SaveMiscOrder(MiscOrder, this.CurrentUser);
                    return true;
                }
                else
                {
                    ShowErrorMessage("MasterData.MiscOrder.Error.NoDetails");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    public void updateView(bool needBlank)
    {

        if (MiscOrder == null)
        {
            MiscOrder = new MiscOrder();
            MiscOrder.CreateUser = this.CurrentUser;
            IList<MiscOrderDetail> miscOrderDetails = new List<MiscOrderDetail>();
            MiscOrder.MiscOrderDetails = miscOrderDetails;

            this.tbMiscOrderRegion.Text = string.Empty;
            this.tbMiscOrderLocation.Text = string.Empty;
            this.tbMiscOrderEffectDate.Text = string.Empty;

            this.tbRefNo.Text = string.Empty;
            this.tbMiscOrderDescription.Text = string.Empty;
            this.tbMiscOrderCreateDate.Text = DateTime.Now.ToLongDateString();
            this.lbCreateUser.Text = this.CurrentUser.Code;
            this.tbMiscOrderID.Text = string.Empty;
            this.MiscOrderDetailsGV.Columns[4].Visible = true;
            noEditable(false);
        }
        else
        {
            this.tbMiscOrderID.Text = MiscOrder.OrderNo;
            this.tbMiscOrderCreateDate.Text = MiscOrder.CreateDate.ToLongDateString();
            this.tvMiscOrderRegion.Text = MiscOrder.Location == null ? string.Empty : MiscOrder.Location.Region.Name;
            this.tvMiscOrderLocation.Text = MiscOrder.Location == null ? string.Empty : MiscOrder.Location.Name;
            this.lbRefNo.Text = MiscOrder.ReferenceOrderNo;
            this.lbCreateUser.Text = MiscOrder.CreateUser.Code;
            if (MiscOrder.Reason != null && MiscOrder.Reason != string.Empty)
            {
                if (ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GI))
                    this.tvMiscOrderReason.Text = TheCodeMasterMgr.GetCachedCodeMaster("StockInReason", MiscOrder.Reason).Description;
                if (ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR))
                    this.tvMiscOrderReason.Text = TheCodeMasterMgr.GetCachedCodeMaster("StockOutReason", MiscOrder.Reason).Description;
                if (ModuleType.Equals(BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_ADJ))
                    this.tvMiscOrderReason.Text = TheCodeMasterMgr.GetCachedCodeMaster("StockAdjReason", MiscOrder.Reason).Description;
            }
            this.tvMiscOrderDescription.Text = MiscOrder.Remark;
            this.tvMiscOrderEffectDate.Text = MiscOrder.EffectiveDate.ToLongDateString();
            if (MiscOrder.OrderNo != null && MiscOrder.OrderNo.Length > 0)
            {
                this.MiscOrderDetailsGV.Columns[4].Visible = false;
            }
        }
        updateGridView(needBlank);
    }
    private void updateGridView(bool needBlank)
    {
        if (needBlank)
        {
            MiscOrderDetail blankMiscOrderDetail = new MiscOrderDetail();
            blankMiscOrderDetail.Qty = 0;
            MiscOrder.MiscOrderDetails.Add(blankMiscOrderDetail);
            blankMiscOrderDetail.IsBlankDetail = true;
        }
        this.MiscOrderDetailsGV.DataSource = MiscOrder.MiscOrderDetails;
        this.MiscOrderDetailsGV.DataBind();

    }

    public void noEditable(bool noEditable)
    {
        this.tbMiscOrderEffectDate.Visible = !noEditable;
        this.tvMiscOrderEffectDate.Visible = noEditable;
        this.tbMiscOrderRegion.Visible = !noEditable;
        this.tvMiscOrderRegion.Visible = noEditable;
        this.tbMiscOrderLocation.Visible = !noEditable;
        this.tvMiscOrderLocation.Visible = noEditable;
        this.ddlReason.Visible = !noEditable;
        this.tvMiscOrderReason.Visible = noEditable;
        this.tbMiscOrderDescription.Visible = !noEditable;
        this.tvMiscOrderDescription.Visible = noEditable;

        this.tbRefNo.Visible = !noEditable;
        this.lbRefNo.Visible = noEditable;

        // this.btnSave.Visible = !noEditable;
        this.btnSubmit.Visible = !noEditable;

    }


    protected void MiscOrderDetailsGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MiscOrderDetail orderDetail = (MiscOrderDetail)e.Row.DataItem;
            if (MiscOrder.OrderNo != null)
            {
                ((Label)e.Row.FindControl("lblItemCode")).Visible = true;
                ((Controls_TextBox)e.Row.FindControl("tbItemCode")).Visible = false;
                ((Label)e.Row.FindControl("lblQty")).Visible = true;
                ((TextBox)e.Row.FindControl("tbQty")).Visible = false;

                ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = false;
                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = false;

            }
            else
            {
                if (orderDetail.IsBlankDetail)
                {
                    Controls_TextBox tbItemCode = ((Controls_TextBox)e.Row.FindControl("tbItemCode"));
                    tbItemCode.Visible = true;
                    tbItemCode.SuggestTextBox.Attributes.Add("onchange", "GenerateItem(this);");
                    //((Controls_TextBox)e.Row.FindControl("tbItemCode")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = true;
                    ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = false;



                    RegularExpressionValidator revQty = (RegularExpressionValidator)e.Row.FindControl("revQty");
                    revQty.Enabled = true;
                    if (this.ModuleType == BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GI || this.ModuleType == BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR)
                    {
                        revQty.ValidationExpression = "^(0|([1-9]\\d*))(\\.\\d+)?$";
                        revQty.ErrorMessage = "${MasterData.MiscOrder.WarningMessage.InputQtyMustThanZero}";
                    }
                    if (this.ModuleType == BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_ADJ)
                    {
                        revQty.ValidationExpression = "^(-)?(0|([1-9]\\d*))(\\.\\d+)?$";
                        revQty.ErrorMessage = "${MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error}";
                    }
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = false;
                    ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = true;
                }
            }
        }
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
        MiscOrder.MiscOrderDetails.RemoveAt(rowIndex);
        this.MiscOrderDetailsGV.DataSource = MiscOrder.MiscOrderDetails;
        this.MiscOrderDetailsGV.DataBind();
    }
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
        GridViewRow row = this.MiscOrderDetailsGV.Rows[rowIndex];
        RequiredFieldValidator rfvItemCode = (RequiredFieldValidator)row.FindControl("rfvItemCode");

        if (rfvItemCode.IsValid)
        {
            if (MiscOrderViewStateSave(false))
            {
                updateView(true);
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (this.MiscOrderDBSave())
        {
            updateView(true);
        }

    }

    private bool CheckDetail()
    {
        int detailNo = 0;
        for (int i = 0; i < this.MiscOrderDetailsGV.Rows.Count; i++)
        {
            TextBox tbQty = (TextBox)this.MiscOrderDetailsGV.Rows[i].FindControl("tbQty");
            if (decimal.Parse(tbQty.Text.Trim()) != 0)
            {
                detailNo++;
                break;
            }
        }
        return detailNo == 0 ? false : true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (CheckDetail())
        {
            if (this.MiscOrderDBSave())
            {
                updateView(false);
                noEditable(true);
            }
        }
        else
        {
            ShowErrorMessage("MasterData.MiscOrder.Error.NoDetails");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

}
