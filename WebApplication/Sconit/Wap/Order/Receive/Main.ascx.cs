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
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;

public partial class Wap_Order_Receive_Main : MainModuleBase
{
    protected List<string> HuIdList
    {
        get
        {
            return (List<string>)ViewState["HuIdList"];
        }
        set
        {
            ViewState["HuIdList"] = value;
        }
    }

    protected List<int> HuDetailIdList
    {
        get
        {
            return (List<int>)ViewState["HuDetailIdList"];
        }
        set
        {
            ViewState["HuDetailIdList"] = value;
        }
    }

    private int _rowIndex
    {
        get
        {
            return (int)ViewState["RowIndex"];
        }
        set
        {
            ViewState["RowIndex"] = value;
        }
    }

    private string refPartyCode
    {
        get
        {
            return (string)ViewState["refPartyCode"];
        }
        set
        {
            ViewState["refPartyCode"] = value;
        }
    }

    private string OrderNo
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

  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.tbOrderNo.Focus();
        }
    }

    protected void btnOrderNo_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = string.Empty;
        this.OrderNo = this.tbOrderNo.Text.Trim();
        OrderHead orderHead = null;
        try
        {
            orderHead = TheOrderMgr.LoadOrder(this.OrderNo, this.CurrentUser);
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
            this.tbOrderNo.Text = string.Empty;
            this.tbOrderNo.Focus();
        }
        if (orderHead != null)
        {
            this.gv_List.DataSource = TheOrderDetailMgr.GetOrderDetail(this.OrderNo);
            this.gv_List.DataBind();
            this.tbOrderNo.ReadOnly = true;
            this.gv_List.Visible = false;
            this.gv_Hu.Visible = true;
            this.btn_Hu_Create.Visible = true;
            this.btnReceive.Visible = false;
            InitPageParameter(orderHead.PartyFrom.Code);
        }

    }

    public void InitPageParameter(string refPartyCode)
    {
        //this.lblMessage.Text = string.Empty;
        //this.HuIdList = new List<string>();
        //this.HuDetailIdList = new List<int>();
        //this.refPartyCode = refPartyCode;
        //IList<HuDetail> _huDetailList = new List<HuDetail>();

        //InitialHuIdInput(_huDetailList);
    }

    protected void btnReceive_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "收货成功";
        this.tbOrderNo.ReadOnly = false;
        this.tbOrderNo.Focus();
        this.gv_List.Visible = false;
        this.btnReceive.Visible = false;
        this.tbOrderNo.Text = string.Empty;
    }

    protected void btn_Hu_Create_Click(object sender, EventArgs e)
    {
        //IList<OrderDetail> orderDetailList = TheOrderDetailMgr.GetOrderDetail(this.OrderNo);
        //foreach (OrderDetail orderDetail in orderDetailList)
        //{
        //    foreach (int huDetailId in this.HuDetailIdList)
        //    {
        //        HuDetail huDetail = TheHuDetailMgr.LoadHuDetail(huDetailId);
        //        if (orderDetail.Item.Code.Trim().ToLower() == huDetail.Item.Code.Trim().ToLower())
        //        {
        //            orderDetail.CurrentReceiveQty += huDetail.Qty;
        //        }
        //    }
        //}

        //this.gv_List.DataSource = orderDetailList;
        //this.gv_List.DataBind();
        //this.divOrder.Visible = true;
        //this.gv_List.Visible = true;
        //this.gv_Hu.Visible = false;
        //this.btn_Hu_Create.Visible = false;
        //this.btnReceive.Visible = true;
    }

    protected void tbHuId_TextChanged(object sender, EventArgs e)
    {
        //Label _displayControl = (Label)this.gv_Hu.Rows[_rowIndex].FindControl("lblHuId");
        //TextBox _editControl = (TextBox)this.gv_Hu.Rows[_rowIndex].FindControl("tbHuId");
        //string huId = _editControl.Text.Trim();
        //IList<HuDetail> gv_Hu_Data = new List<HuDetail>();
        //IList<HuDetail> huDetailList = new List<HuDetail>();
        ////Hu _hu = TheHuMgr.LoadHu(huId, refPartyCode);

        //try
        //{
        //}
        //catch (BusinessErrorException)
        //{
        //    _editControl.Text = string.Empty;
        //    _editControl.Focus();
        //    return;
        //}

        //if (huDetailList != null && huDetailList.Count > 0)
        //{
        //    if (HuIdList.Count > 0)
        //    {
        //        foreach (string str in HuIdList)
        //        {
        //            IList<HuDetail> huDetailTempList = TheHuDetailMgr.GetHuDetail(str);
        //            foreach (HuDetail hD in huDetailTempList)
        //            {
        //                if (HuDetailIdList.Contains(hD.Id))
        //                {
        //                    gv_Hu_Data.Add(hD);
        //                }
        //            }
        //        }
        //    }

        //    if (!this.HuIdList.Contains(huDetailList[0].Hu.HuId))
        //    {
        //        this.HuIdList.Add(huDetailList[0].Hu.HuId);
        //    }

        //    foreach (HuDetail huDetail in huDetailList)
        //    {
        //        if (!gv_Hu_Data.Contains(huDetail)) //避免重复HuDetail
        //        {
        //            gv_Hu_Data.Add(huDetail);
        //            HuDetailIdList.Add(huDetail.Id);
        //        }
        //    }
        //    this.lblMessage.Text = string.Empty;
        //    InitialHuIdInput(gv_Hu_Data);
        //}
        //else
        //{
        //    _editControl.Text = string.Empty;
        //    _editControl.Focus();
        //    this.lblMessage.Text = Resources.Language.MasterDataHuDetailNotExit + " (" + huId + ")";
        //}
    }

    private void InitialHuIdInput()
    {
        //_rowIndex = hDList.Count;

        ////增加新行
        //hDList.Add(new HuDetail());
        //this.gv_Hu.DataSource = hDList;
        //this.gv_Hu.DataBind();

        //Label _displayControl = (Label)this.gv_Hu.Rows[_rowIndex].FindControl("lblHuId");
        //TextBox _editControl = (TextBox)this.gv_Hu.Rows[_rowIndex].FindControl("tbHuId");
        //LinkButton _linkButton = (LinkButton)this.gv_Hu.Rows[_rowIndex].FindControl("lbtnDeleteHu");
        //_linkButton.Visible = false;
        //_displayControl.Visible = false;
        //_editControl.Visible = true;
        //_editControl.Focus();
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        //IList<HuDetail> gv_Hu_Data = new List<HuDetail>();
        //int huDetailId = int.Parse(((LinkButton)sender).CommandArgument);
        //if (this.HuDetailIdList.Contains(huDetailId))
        //{
        //    this.HuDetailIdList.Remove(huDetailId);
        //}

        //foreach (string str in HuIdList)
        //{
        //    IList<HuDetail> huDetailTempList = TheHuDetailMgr.GetHuDetail(str);
        //    foreach (HuDetail hD in huDetailTempList)
        //    {
        //        if (HuDetailIdList.Contains(hD.Id))
        //        {
        //            gv_Hu_Data.Add(hD);
        //        }
        //    }
        //}
        //InitialHuIdInput(gv_Hu_Data);
    }
}
