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
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Xml;

public partial class NewMrp_ShiftPlan_Preview : ModuleBase
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
    private NewMrp_ShiftPlan_PreviewDetail GetDetailControl(GridViewRow gvr)
    {
        return (NewMrp_ShiftPlan_PreviewDetail)gvr.FindControl("ucDetail");
    }

    //private NewMrp_ShiftPlan_ExistsDetail GetExistsDetailControl(GridViewRow gvr)
    //{
    //    return (NewMrp_ShiftPlan_ExistsDetail)gvr.FindControl("ucExistsDetail");
    //}
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

            //GetExistsDetailControl(e.Row).BindList(oh.ExistsProdDetails);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        
        this.ItemDic.Clear();
        this.Visible = false;
        if (BtnCreateClick != null)
        {
            BtnCreateClick(null, null);
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
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
            this.Create(orderHeadList);
            this.Visible = false;
            if (BtnCreateClick != null)
            {
                BtnCreateClick(null, null);
            }
    }

    private IList<OrderHead> GetList()
    {
        IList<OrderHead> orderHeadList = new List<OrderHead>();
        List<FlowDetail> tempFlowdets = new List<FlowDetail>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            string flowCode = ((Label)gvr.FindControl("lblFlow")).Text;
            DateTime startTime = DateTime.Parse(((TextBox)gvr.FindControl("tbStartTime")).Text);
            DateTime windowTime = DateTime.Parse(((TextBox)gvr.FindControl("tbWindowTime")).Text);
            string refOrderNo = ((HiddenField)gvr.FindControl("tbRefOrderNo")).Value;//客户参考订单号 
            OrderHead oh = new OrderHead();
            Flow currentFlow = TheFlowMgr.LoadFlow(flowCode, CurrentUser.Code, true);
            if (tempFlowdets.Where(d => d.Flow.Code == currentFlow.Code).Count() > 0)
            {
                currentFlow.FlowDetails = tempFlowdets.Where(d => d.Flow.Code == currentFlow.Code).ToList();
            }
            else
            {
                tempFlowdets.AddRange(currentFlow.FlowDetails);
            }
            this.GetDetailControl(gvr).CollectDataByFlow(currentFlow);
            oh = TheOrderMgr.TransferFlow2Order(currentFlow);
            //oh = TheOrderMgr.TransferFlow2Order(flowCode);
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

    private void Create(IList<OrderHead> orderHeadList)
    {
        try
        {
            string sqlText = string.Empty;
            string ConnString = string.Empty;
            XmlTextReader reader = new XmlTextReader(Server.MapPath("Config/properties.config"));
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);//  
            reader.Close();//
            ConnString = doc.SelectSingleNode("/configuration/properties/connectionString").InnerText.Trim();

            //IList<OrderHead> orderHeadList = (IList<OrderHead>)sender;
            if (orderHeadList != null && orderHeadList.Count > 0)
            {
                //string shiftCode = this.ucShift.ShiftCode;
                //Shift shift = TheShiftMgr.LoadShift(shiftCode);
                //foreach (var item in orderHeadList)
                //{
                //    item.Shift = shift;
                //}
                TheOrderMgr.CreateOrder(orderHeadList, this.CurrentUser.Code);
                var result = (from order in orderHeadList select order.OrderDetails[0].Item.Code).Distinct();

                foreach (var s in result)
                {
                    var _ordNo = (from order in orderHeadList
                                  where order.OrderDetails[0].Item.Code == s
                                  orderby order.WinDate, order.WindowTime
                                  select order.OrderNo);
                    int i = 0;
                    string refOrderNo = string.Empty;
                    foreach (var ord in _ordNo)
                    {

                        UpdateOrderMstr(ord, refOrderNo, ConnString);
                        if (i == 0) refOrderNo = ord;

                        i++;
                    }
                }
                ShowSuccessMessage("Common.Business.Result.Insert.Successfully");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    private void UpdateOrderMstr(string orderNo, string refOrderNo, string ConnString)
    {

        string buchong = refOrderNo == "" ? "" : ",IsAdditional='1'";
        string sqlText = "update ordermstr set reforderno='"
                            + refOrderNo
                            + "'" + buchong
                           + " where orderno='"
                           + orderNo
                           + "'";
        SqlHelper.ExecuteNonQuery(ConnString, CommandType.Text, sqlText);
    }
}
