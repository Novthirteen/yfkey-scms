using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class MRP_ShiftPlan_Import_Preview : ModuleBase
{
    public event EventHandler BtnCreateClick;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public IDictionary<string, decimal> ItemDic
    {
        get
        {
            return (IDictionary<string, decimal>)ViewState["ItemDic"];
        }
        set
        {
            ViewState["ItemDic"] = value;
        }
    }


    #region GridViewRow Control Value
    private MRP_ShiftPlan_Import_PreviewDetail GetDetailControl(GridViewRow gvr)
    {
        return (MRP_ShiftPlan_Import_PreviewDetail)gvr.FindControl("ucDetail");
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(IList<OrderHead> orderHeadList, string ModuleType)
    {
        this.ModuleType = ModuleType;
        this.InitialUI();

        this.GV_List.DataSource = orderHeadList;
        this.GV_List.DataBind();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderHead oh = (OrderHead)e.Row.DataItem;
            if (ItemDic == null)
            {
                ItemDic = new Dictionary<string, decimal>();
            }
            FillDictionary(oh, ItemDic);

            GetDetailControl(e.Row).BindList(oh.OrderDetails);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        
        this.ItemDic.Clear();
        this.Visible = false;
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //TheOrderMgr.CreateOrder(this.GetList(), this.CurrentUser.Code);
        if (BtnCreateClick != null)
        {
            IList<OrderHead> orderHeadList = this.GetList();

            #region  收集当前数量
            IDictionary<string, decimal> orderItemDic = new Dictionary<string, decimal>();
            foreach (OrderHead orderHead in orderHeadList)
            {
                FillDictionary(orderHead, orderItemDic);
            }
            #endregion

            #region 比较数量
            foreach (string itemCode in ItemDic.Keys)
            {
                if (orderItemDic.ContainsKey(itemCode))
                {
                    decimal inQty = ItemDic[itemCode];
                    decimal outQty = orderItemDic[itemCode];

                    if (inQty != outQty)
                    {
                        ShowErrorMessage("MasterData.ShiftPlan.Error.InOutQtyNotMatch", itemCode, outQty.ToString("F2"), inQty.ToString("F2"));
                        return;
                    }
                }
            }
            #endregion

            this.ItemDic.Clear();
            BtnCreateClick(orderHeadList, null);
        }
    }

    private IList<OrderHead> GetList()
    {
        IList<OrderHead> orderHeadList = new List<OrderHead>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            string flowCode = ((Label)gvr.FindControl("lblFlow")).Text;
            DateTime startTime = DateTime.Parse(((TextBox)gvr.FindControl("tbStartTime")).Text);
            DateTime windowTime = DateTime.Parse(((TextBox)gvr.FindControl("tbWindowTime")).Text);
            string refOrderNo = ((HiddenField)gvr.FindControl("tbRefOrderNo")).Value;//客户参考订单号 
            OrderHead oh = new OrderHead();
            oh = TheOrderMgr.TransferFlow2Order(flowCode);
            oh.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;
            oh.StartTime = startTime;
            oh.WindowTime = windowTime;
            oh.ReferenceOrderNo = refOrderNo;
            this.GetDetailControl(gvr).CollectData(oh);
            OrderHelper.FilterZeroOrderQty(oh);
            orderHeadList.Add(oh);
        }
        return orderHeadList;
    }

    private void InitialUI()
    {
        if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
        {
            this.GV_List.Columns[0].HeaderText = TheLanguageMgr.TranslateMessage("MasterData.Flow.Flow.Distribution", this.CurrentUser);
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
        {
            this.GV_List.Columns[0].HeaderText = TheLanguageMgr.TranslateMessage("MasterData.Flow.Flow.Production", this.CurrentUser);
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT)
        {
            this.GV_List.Columns[0].HeaderText = TheLanguageMgr.TranslateMessage("MasterData.Flow.Flow.Procurement", this.CurrentUser);
        }
    }

    private void FillDictionary(OrderHead orderHead, IDictionary<string, decimal> itemQtyDic)
    {
        foreach (OrderDetail orderDetail in orderHead.OrderDetails)
        {
            if (itemQtyDic.ContainsKey(orderDetail.Item.Code))
            {
                itemQtyDic[orderDetail.Item.Code] += orderDetail.OrderedQty;
            }
            else
            {
                itemQtyDic.Add(orderDetail.Item.Code, orderDetail.OrderedQty);
            }
        }
    }
}
