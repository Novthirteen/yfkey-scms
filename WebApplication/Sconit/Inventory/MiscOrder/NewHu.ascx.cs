
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


public partial class MasterData_MiscOrder_NewHu : EditModuleBase
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

        this.tbMiscOrderRegion.ServiceParameter = "string:" + this.CurrentUser.Code;

        if (!this.IsPostBack)
        {
            this.MiscOrder = new MiscOrder();
        }
    }


    protected void MiscOrderDetailsGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
        MiscOrder.MiscOrderDetails.RemoveAt(rowIndex);
        this.MiscOrderDetailsGV.DataSource = MiscOrder.MiscOrderDetails;
        this.MiscOrderDetailsGV.DataBind();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (this.rfvEffectDate.IsValid && this.rfvLocation.IsValid && this.rfvRegion.IsValid && this.rfvCostCenterCode.IsValid
            && this.rfvSubjectCode.IsValid && this.rfvEffectDate.IsValid && this.rfvAccountCode.IsValid)
        {
            if (this.MiscOrder.MiscOrderDetails != null && this.MiscOrder.MiscOrderDetails.Count == 0)
            {
                ShowErrorMessage("MasterData.MiscOrder.Error.NoDetails");
                return;
            }
            try
            {
                MiscOrder.SubjectList = TheSubjectListMgr.LoadSubjectList(this.tbSubjectCode.Text.Trim(), this.tbCostCenterCode.Text.Trim(), this.tbAccountCode.Text.Trim());

                MiscOrder.Remark = this.tbMiscOrderDescription.Text;
                MiscOrder.Type = this.ModuleType;
                MiscOrder.Location = this.TheLocationMgr.LoadLocation(this.tbMiscOrderLocation.Text);
                MiscOrder.EffectiveDate = DateTime.Parse(this.tbMiscOrderEffectDate.Text);
                MiscOrder.ReferenceOrderNo = this.tbRefNo.Text.Trim();
                MiscOrder.ProjectCode = this.tbProjectCode.Text.Trim();

                MiscOrder = TheMiscOrderMgr.SaveMiscOrder(MiscOrder, this.CurrentUser);

                InitPageParameter();
            }
            catch (BusinessErrorException ex)
            {
                ShowErrorMessage(ex);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter()
    {
        if (this.MiscOrder == null || this.MiscOrder.OrderNo == null)
        {
            //绑定头
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
            this.tbMiscOrderCode.Text = string.Empty;
            this.MiscOrderDetailsGV.Columns[5].Visible = true;
            this.tbProjectCode.Text = string.Empty;
            this.tbCostCenterCode.Text = string.Empty;
            this.tbSubjectCode.Text = string.Empty;
            this.tbAccountCode.Text = string.Empty;

            this.tvMiscOrderRegion.Visible = false;
            this.tbMiscOrderRegion.Visible = true;

            this.tvMiscOrderLocation.Visible = false;
            this.tbMiscOrderLocation.Visible = true;

            this.lbRefNo.Visible = false;
            this.tbRefNo.Visible = true;

            this.lbProjectCode.Visible = false;
            this.tbProjectCode.Visible = true;

            this.tvMiscOrderDescription.Visible = false;
            this.tbMiscOrderDescription.Visible = true;

            this.tvMiscOrderEffectDate.Visible = false;
            this.tbMiscOrderEffectDate.Visible = true;


            this.tvCostCenterCode.Visible = false;
            this.tbCostCenterCode.Visible = true;

            this.tvSubjectCode.Visible = false;
            this.tbSubjectCode.Visible = true;

            this.tvAccountCode.Visible = false;
            this.tbAccountCode.Visible = true;

            this.MiscOrderDetailsGV.Columns[5].Visible = true;
            this.btnSubmit.Visible = true;

            //绑定明细
            BindMiscOrderDetails();

        }
        else
        {
            this.tbMiscOrderCode.Text = MiscOrder.OrderNo;
            this.tbMiscOrderCreateDate.Text = MiscOrder.CreateDate.ToLongDateString();

            this.tvMiscOrderRegion.Text = MiscOrder.Location == null ? string.Empty : MiscOrder.Location.Region.Name;
            this.tvMiscOrderRegion.Visible = true;
            this.tbMiscOrderRegion.Visible = false;

            this.tvMiscOrderLocation.Text = MiscOrder.Location == null ? string.Empty : MiscOrder.Location.Name;
            this.tvMiscOrderLocation.Visible = true;
            this.tbMiscOrderLocation.Visible = false;

            this.lbRefNo.Text = MiscOrder.ReferenceOrderNo;
            this.lbRefNo.Visible = true;
            this.tbRefNo.Visible = false;

            this.lbProjectCode.Text = MiscOrder.ProjectCode;
            this.lbProjectCode.Visible = true;
            this.tbProjectCode.Visible = false;


            this.lbCreateUser.Text = MiscOrder.CreateUser.Code;

            this.tvMiscOrderDescription.Text = MiscOrder.Remark;
            this.tvMiscOrderDescription.Visible = true;
            this.tbMiscOrderDescription.Visible = false;

            this.tvMiscOrderEffectDate.Text = MiscOrder.EffectiveDate.ToLongDateString();
            this.tvMiscOrderEffectDate.Visible = true;
            this.tbMiscOrderEffectDate.Visible = false;

            this.tvCostCenterCode.Text = MiscOrder.SubjectList.CostCenterCode;
            this.tvCostCenterCode.Visible = true;
            this.tbCostCenterCode.Visible = false;

            this.tvSubjectCode.Text = MiscOrder.SubjectList.SubjectCode;
            this.tvSubjectCode.Visible = true;
            this.tbSubjectCode.Visible = false;

            this.tvAccountCode.Text = MiscOrder.SubjectList.AccountCode;
            this.tvAccountCode.Visible = true;
            this.tbAccountCode.Visible = false;

            this.MiscOrderDetailsGV.Columns[5].Visible = false;

            this.ltlHuScan.Visible = false;
            this.tbHuScan.Visible = false;

            this.btnSubmit.Visible = false;


            //绑定明细
            BindMiscOrderDetails();
        }
    }

    private void BindMiscOrderDetails()
    {
        this.MiscOrderDetailsGV.DataSource = this.MiscOrder.MiscOrderDetails;
        this.MiscOrderDetailsGV.DataBind();
    }

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {
        this.HuInput(this.tbHuScan.Text.Trim());
        InitialHuInput();
    }

    private void HuInput(string huId)
    {
        try
        {

            if (MiscOrder.MiscOrderDetails != null)
            {
                foreach (MiscOrderDetail miscOrderDetail in MiscOrder.MiscOrderDetails)
                {
                    if (miscOrderDetail.HuId == huId)
                    {
                        ShowErrorMessage("MasterData.MiscOrder.Location.Exists");
                    }
                }

            }
            if (this.ModuleType != BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR)
            {
                if (this.tbMiscOrderLocation.Text.Trim() == string.Empty)
                {
                    ShowErrorMessage("MasterData.MiscOrder.Location.Empty");
                    return;
                }
                IList<LocationLotDetail> locationLotDetailList = TheLocationLotDetailMgr.GetHuLocationLotDetail(this.tbMiscOrderLocation.Text.Trim(), huId);
                if (locationLotDetailList.Count == 0)
                {
                    ShowErrorMessage("MasterData.MiscOrder.Location.NotExists.Hu", huId);
                }
            }
                Hu hu = TheHuMgr.LoadHu(huId);
                MiscOrderDetail newMiscOrderDetail = new MiscOrderDetail();
                newMiscOrderDetail.HuId = huId;
                newMiscOrderDetail.Item = hu.Item;
                newMiscOrderDetail.LotNo = hu.LotNo;
                newMiscOrderDetail.Qty = hu.Qty * hu.UnitQty;
                MiscOrder.AddMiscOrderDetail(newMiscOrderDetail);

                BindMiscOrderDetails();
            
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    private void InitialHuInput()
    {
        this.tbHuScan.Text = string.Empty;
        this.tbHuScan.Focus();
    }

}
