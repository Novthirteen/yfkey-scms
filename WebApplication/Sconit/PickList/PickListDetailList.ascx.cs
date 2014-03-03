using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;

public partial class Distribution_PickList_PickListDetailList : ModuleBase
{
    public event EventHandler ShipEvent;
    public event EventHandler ConfirmEvent;

    public string PickBy
    {
        get
        {
            return (string)ViewState["PickBy"];
        }
        set
        {
            ViewState["PickBy"] = value;
        }
    }

    public string PickListNo
    {
        get
        {
            return (string)ViewState["PickListNo"];
        }
        set
        {
            ViewState["PickListNo"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ucHuList.HuSaveEvent += new EventHandler(this.HuListSave_Render);
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            Hu_HuInput ucHuInput = (Hu_HuInput)gvr.FindControl("ucHuInput");
            ucHuInput.QtyChangeEvent += new EventHandler(this.HuInputQtyChange_Render);
            ucHuInput.ReadOnly = true;
        }
    }

    public void InitPageParameter(PickList pickList)
    {
        this.PickBy = pickList.PickBy;
        this.PickListNo = pickList.PickListNo;

        this.GV_List.DataSource = pickList.PickListDetails;
        this.GV_List.DataBind();
        if (pickList.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            this.ltlHuScan.Visible = true;
            this.tbHuScan.Visible = true;
            this.btnScanHu.Visible = true;
        }
        else
        {
            this.ltlHuScan.Visible = false;
            this.tbHuScan.Visible = false;
            this.btnScanHu.Visible = false;

        }
        UpdateView();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PickListDetail pickListDetail = (PickListDetail)e.Row.DataItem;
            Hu_HuInput ucHuInput = (Hu_HuInput)e.Row.FindControl("ucHuInput");
            TextBox tbShipQty = (TextBox)e.Row.FindControl("tbShipQty");
            IList<PickListResult> pickListResultList = ThePickListResultMgr.GetPickListResult(pickListDetail);
            if (pickListResultList != null && pickListResultList.Count > 0)
            {
                foreach (PickListResult pickListResult in pickListResultList)
                {
                    string huId = pickListResult.LocationLotDetail.Hu.HuId;
                    Hu newHu = TheHuMgr.LoadHu(huId);
                    newHu.Qty = pickListResult.Qty;
                    ucHuInput.HuInput(newHu);
                    tbShipQty.Text = ucHuInput.SumQty().ToString("F2");
                }
            }
        }
    }

    private void GV_DataBind(IList<InProcessLocationDetail> ipDetList)
    {
        this.GV_List.DataSource = ipDetList;
        this.GV_List.DataBind();
    }

    //根据订单类型隐藏相应列
    private void UpdateView()
    {
        if (this.PickBy == BusinessConstants.CODE_MASTER_PICKBY_HU)
        {
            this.GV_List.Columns[7].Visible = false;
            this.GV_List.Columns[8].Visible = true;
        }
        else if (this.PickBy == BusinessConstants.CODE_MASTER_PICKBY_LOTNO)
        {
            this.GV_List.Columns[7].Visible = true;
            this.GV_List.Columns[8].Visible = false;
        }
    }

    protected void btnScanHu_Click(object sender, EventArgs e)
    {
        this.ucHuList.Visible = true;
        this.ucHuList.InitPageParameter();
    }

    protected void btnShip_Click(object sender, EventArgs e)
    {


    }

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        string huId = this.tbHuScan.Text.Trim();
        this.HuScan(huId);
    }

    private void HuScan(string huId)
    {
        Hu hu = TheHuMgr.LoadHu(huId);
        if (hu != null)
        {
            this.HuScan(hu);
        }
        else
        {
            ShowErrorMessage("Hu.Not.Exist");
            this.InitialHuScan();
            return;
        }
    }

    private void HuScan(Hu hu)
    {
        bool isMatch = false;

        #region 按Hu拣货
        if (this.PickBy == BusinessConstants.CODE_MASTER_PICKBY_HU)
        {
            foreach (GridViewRow row in GV_List.Rows)
            {
                Label lblOrderQty = (Label)row.FindControl("lblOrderQty");
                TextBox tbShipQty = (TextBox)row.FindControl("tbShipQty");
                Label lblHuId = (Label)row.FindControl("lblHuId");
                Label lblLoc = (Label)row.FindControl("lblLoc");
                Label lblStorageBin = (Label)row.FindControl("lblStorageBin");
                HiddenField hfId = (HiddenField)row.FindControl("hfId");
                Hu_HuInput ucHuInput = (Hu_HuInput)row.FindControl("ucHuInput");
                if (ucHuInput.CheckExist(hu.HuId))
                {
                    this.lblMessage.Text = Resources.Language.MasterDataHuExist;
                    break;
                }
                if (hu.HuId == lblHuId.Text.Trim())
                {
                    IList<LocationLotDetail> locationLotDetList = TheLocationLotDetailMgr.GetHuLocationLotDetail(hu.HuId);
                    if (locationLotDetList != null && locationLotDetList.Count > 0)
                    {
                        LocationLotDetail locLotDet = locationLotDetList[0];
                        if (locLotDet.Location.Code == lblLoc.Text.Trim() && (locLotDet.StorageBin == null || locLotDet.StorageBin.Code.ToUpper() == lblStorageBin.Text.Trim().ToUpper()))
                        {
                            decimal orderQty = lblOrderQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(lblOrderQty.Text.Trim());
                            decimal shipQty = tbShipQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbShipQty.Text.Trim());
                            if (orderQty >= shipQty + hu.Qty)
                            {
                                ucHuInput.HuInput(hu);
                                tbShipQty.Text = ucHuInput.SumQty().ToString("F2");
                                PickListResult pickListResult = new PickListResult();
                                pickListResult.LocationLotDetail = locLotDet;
                                pickListResult.PickListDetail = ThePickListDetailMgr.LoadPickListDetail(int.Parse(hfId.Value));
                                pickListResult.Qty = hu.Qty;

                                isMatch = true;
                                InitialHuScan();
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 按LotNo拣货
        else if (this.PickBy == BusinessConstants.CODE_MASTER_PICKBY_LOTNO)
        {
            foreach (GridViewRow row in GV_List.Rows)
            {
                Label lblOrderQty = (Label)row.FindControl("lblOrderQty");
                TextBox tbShipQty = (TextBox)row.FindControl("tbShipQty");
                Label lblLotNo = (Label)row.FindControl("lblLotNo");
                Label lblItemCode = (Label)row.FindControl("lblItemCode");
                Label lblUnitCount = (Label)row.FindControl("lblUnitCount");
                Label lblUom = (Label)row.FindControl("lblUom");
                Label lblLoc = (Label)row.FindControl("lblLoc");
                Label lblStorageBin = (Label)row.FindControl("lblStorageBin");
                Hu_HuInput ucHuInput = (Hu_HuInput)row.FindControl("ucHuInput");
                HiddenField hfId = (HiddenField)row.FindControl("hfId");
                if (ucHuInput.CheckExist(hu.HuId))
                {
                    this.lblMessage.Text = Resources.Language.MasterDataHuExist;
                    break;
                }
                if (lblLotNo.Text.Trim().ToUpper() == hu.LotNo.ToUpper() && lblItemCode.Text.Trim().ToUpper() == hu.Item.Code.ToUpper() && decimal.Parse(lblUnitCount.Text.Trim()) == hu.UnitCount && lblUom.Text.Trim().ToUpper() == hu.Uom.Code.ToUpper())
                {
                    IList<LocationLotDetail> locationLotDetList = TheLocationLotDetailMgr.GetHuLocationLotDetail(hu.HuId);
                    if (locationLotDetList != null && locationLotDetList.Count > 0)
                    {
                        LocationLotDetail locLotDet = locationLotDetList[0];
                        if (locLotDet.Location.Code == lblLoc.Text.Trim() && (locLotDet.StorageBin == null || locLotDet.StorageBin.Code == lblStorageBin.Text.Trim()))
                        {
                            decimal orderQty = lblOrderQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(lblOrderQty.Text.Trim());
                            decimal shipQty = tbShipQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbShipQty.Text.Trim());
                            if (orderQty >= shipQty + hu.Qty)
                            {
                                ucHuInput.HuInput(hu);
                                tbShipQty.Text = ucHuInput.SumQty().ToString("F2");
                                PickListResult pickListResult = new PickListResult();
                                pickListResult.LocationLotDetail = locLotDet;
                                pickListResult.PickListDetail = ThePickListDetailMgr.LoadPickListDetail(int.Parse(hfId.Value));
                                pickListResult.Qty = hu.Qty;

                                isMatch = true;
                                InitialHuScan();
                                break;
                            }
                        }

                    }
                }
            }
        }
        #endregion

        if (!isMatch)
        {
            this.lblMessage.Text = Resources.Language.MasterDataPickListNotExistHu;
            this.tbHuScan.Text = string.Empty;
            this.tbHuScan.Focus();
        }

    }

    private void InitialHuScan()
    {
        this.lblMessage.Text = string.Empty;
        this.tbHuScan.Text = string.Empty;
        this.tbHuScan.Focus();
    }

    void HuInputQtyChange_Render(object sender, EventArgs e)
    {
        Hu_HuInput ucHuInput = (Hu_HuInput)((((TextBox)sender).NamingContainer).NamingContainer).Parent;
        GridViewRow row = (GridViewRow)ucHuInput.NamingContainer;
        TextBox tbShipQty = (TextBox)row.FindControl("tbShipQty");
        tbShipQty.Text = ucHuInput.SumQty().ToString("F2");
    }

    void HuListSave_Render(object sender, EventArgs e)
    {
        IList<Hu> huList = (IList<Hu>)((object[])sender)[0];
        foreach (Hu hu in huList)
        {
            this.HuScan(hu);
        }
    }

    public void DoPick()
    {
        if (ConfirmEvent != null)
        {
            int resultCount = 0;
            PickList pickList = ThePickListMgr.LoadPickList(this.PickListNo);
            pickList.PickListDetails = new List<PickListDetail>();
            foreach (GridViewRow row in GV_List.Rows)
            {
                Label lblLoc = (Label)row.FindControl("lblLoc");

                HiddenField hfId = (HiddenField)row.FindControl("hfId");
                Hu_HuInput ucHuInput = (Hu_HuInput)row.FindControl("ucHuInput");

                PickListDetail pickListDetail = ThePickListDetailMgr.LoadPickListDetail(int.Parse(hfId.Value), true);

                IList<Hu> huList = ucHuInput.GetHuList();
                if (huList != null && huList.Count > 0)
                {
                    foreach (Hu hu in huList)
                    {
                        IList<LocationLotDetail> locationLotDetailList = TheLocationLotDetailMgr.GetHuLocationLotDetail(lblLoc.Text.Trim(), hu.HuId);
                        if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                        {
                            PickListResult pickListResult = new PickListResult();
                            pickListResult.LocationLotDetail = locationLotDetailList[0];
                            pickListResult.PickListDetail = pickListDetail;
                            pickListResult.Qty = hu.Qty * pickListDetail.OrderLocationTransaction.UnitQty;
                            pickListDetail.AddPickListResult(pickListResult);

                            resultCount++;
                        }
                    }

                }
                pickList.AddPickListDetail(pickListDetail);
            }

            if (resultCount == 0)
            {
                ShowErrorMessage("MasterData.No.PickListResult");
                return;
            }
            try
            {
                ThePickListMgr.DoPick(pickList, this.CurrentUser);
                ShowSuccessMessage("MasterData.PickList.Pick.Successfully", pickList.PickListNo);
                ConfirmEvent(this.PickListNo, null);
            }
            catch (BusinessErrorException ex)
            {
                ShowErrorMessage(ex);
            }
        }
    }
}
