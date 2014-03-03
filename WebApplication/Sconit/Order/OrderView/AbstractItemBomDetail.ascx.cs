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

public partial class Order_OrderView_AbstractItemBomDetail : ListModuleBase
{
    public event EventHandler SaveEvent;
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
    public int LocTransId
    {
        get
        {
            return (int)ViewState["LocTransId"];
        }
        set
        {
            ViewState["LocTransId"] = value;
        }
    }

    public IList<OrderLocationTransaction> OrderLocTransList
    {
        get
        {
            return (IList<OrderLocationTransaction>)ViewState["OrderLocTransList"];
        }
        set
        {
            ViewState["OrderLocTransList"] = value;
        }
    }

    public override void UpdateView()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {




    }

    public void InitPageParameter(string itemCode)
    {

        OrderLocationTransaction currentOrderLocTrans = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(this.LocTransId);
            OrderHead orderHead = currentOrderLocTrans.OrderDetail.OrderHead;
            //IList<BomDetail> bomDetailList = new List<BomDetail>();
            IList<BomDetail> bomDetailList = TheBomDetailMgr.GetBomDetailListForAbstractItem(itemCode, orderHead.Routing, orderHead.StartTime, currentOrderLocTrans.OrderDetail.DefaultLocationFrom);

            IList<OrderLocationTransaction> orderLocTransList = new List<OrderLocationTransaction>();
            // OrderLocationTransaction orderLocTrans = OrderLocationTransactionMgr.LoadOrderLocationTransaction(this.LocTransId);

            foreach (BomDetail bomDetail in bomDetailList)
            {
                OrderLocationTransaction newOrderLocTrans = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(this.LocTransId);
                newOrderLocTrans.Id = 0;
                newOrderLocTrans.Item = bomDetail.Item;
                newOrderLocTrans.Uom = bomDetail.Item.Uom;
                newOrderLocTrans.BomDetail = bomDetail;
                newOrderLocTrans.Operation = bomDetail.Operation;
                newOrderLocTrans.Location = bomDetail.Location;
                newOrderLocTrans.OrderedQty = newOrderLocTrans.OrderedQty * bomDetail.RateQty;
                newOrderLocTrans.AccumulateQty = newOrderLocTrans.AccumulateQty == null ? null : newOrderLocTrans.AccumulateQty * bomDetail.RateQty;
                newOrderLocTrans.AccumulateRejectQty = newOrderLocTrans.AccumulateRejectQty == null ? null : newOrderLocTrans.AccumulateRejectQty * bomDetail.RateQty;
                newOrderLocTrans.AccumulateScrapQty = newOrderLocTrans.AccumulateScrapQty == null ? null : newOrderLocTrans.AccumulateScrapQty * bomDetail.RateQty;

                orderLocTransList.Add(newOrderLocTrans);
            }
            this.OrderLocTransList = orderLocTransList;
            this.GV_List.DataSource = orderLocTransList;
            this.GV_List.DataBind();
       
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int currentLocTransId = -1;
        foreach (GridViewRow row in this.GV_List.Rows)
        {
            RadioButton rbSelect = (RadioButton)row.FindControl("rbSelect");
            HiddenField hfRowIndex = (HiddenField)row.FindControl("hfRowIndex");
            if (rbSelect.Checked)
            {
                currentLocTransId = int.Parse(hfRowIndex.Value);
                break;
            }
        }
        if (currentLocTransId == -1)
        {
            ShowErrorMessage("Common.Message.Record.Not.Select");
        }
        else
        {
            OrderLocationTransaction currentLocationTrans = OrderLocTransList[currentLocTransId];
            OrderLocationTransaction oldLocationTrans = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(this.LocTransId);
            OrderDetail orderDetail = TheOrderDetailMgr.LoadOrderDetail(oldLocationTrans.OrderDetail.Id, true);
            orderDetail.OrderHead = TheOrderHeadMgr.LoadOrderHead(orderDetail.OrderHead.OrderNo, true, true, true);
            oldLocationTrans.OrderDetail = orderDetail;
            try
            {
                TheOrderLocationTransactionMgr.ReplaceAbstractItem(oldLocationTrans, currentLocationTrans.BomDetail);

                if (SaveEvent != null)
                {
                    SaveEvent(currentLocationTrans.OrderDetail.OrderHead.OrderNo, e);
                    this.Visible = false;
                }
            }
            catch (BusinessErrorException ex)
            {
                ShowErrorMessage(ex);
            }
        }

    }
}
