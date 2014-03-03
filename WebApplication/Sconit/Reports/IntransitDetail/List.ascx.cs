using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity.Exception;

public partial class Reports_IntransitDetail_List : ListModuleBase
{
    public event EventHandler DetailEvent;

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

    private string FlowCode
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

    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        IList<RoutingDetail> routingDetailList = new List<RoutingDetail>();
        try
        {
            Flow flow = this.TheFlowMgr.CheckAndLoadFlow(flowCode);
            if (flow.Routing != null)
            {
                routingDetailList = this.TheRoutingDetailMgr.GetRoutingDetail(flow.Routing,DateTime.Now);
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
            return;
        }

        #region 初始化使用的列
        for (int i = 8; i < (8 + routingDetailList.Count); i++)
        {
            this.GV_List.Columns[i].Visible = true;
            this.GV_List.Columns[i].HeaderText = routingDetailList[i - 8].Activity;
        }
        #endregion

        #region 隐藏多余列
        if (routingDetailList.Count == 0)
        {
            this.GV_List.Columns[7].Visible = true;
        }
        else
        {
            this.GV_List.Columns[7].Visible = false;
        }

        for (int i = (8 + routingDetailList.Count); i < 28; i++)
        {
            this.GV_List.Columns[i].Visible = false;
        }
        #endregion

        #region 查询
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailTrackView));
        selectCriteria.CreateAlias("OrderDetail", "od");
        selectCriteria.CreateAlias("Flow", "f");

        selectCriteria.Add(Expression.Eq("f.Code", flowCode));

        selectCriteria.AddOrder(Order.Asc("od.Item"));
        selectCriteria.AddOrder(Order.Asc("od.Uom"));
        selectCriteria.AddOrder(Order.Asc("od.UnitCount"));
        selectCriteria.AddOrder(Order.Asc("CurrentOperation"));

        IList<InProcessLocationDetailTrackView> inProcessLocationDetailTrackViewList = this.TheCriteriaMgr.FindAll<InProcessLocationDetailTrackView>(selectCriteria);
        #endregion

        #region 转换查询结果为IntransitDetail
        if (inProcessLocationDetailTrackViewList != null && inProcessLocationDetailTrackViewList.Count > 0)
        {
            string oldItemCode = null;
            string oldUom = null;
            decimal oldUnitCount = 0;
            IntransitDetail intransitDetail = null;
            IList<IntransitDetail> intransitDetailList = new List<IntransitDetail>();
            foreach (InProcessLocationDetailTrackView inProcessLocationDetailTrackView in inProcessLocationDetailTrackViewList)
            {
                if (oldItemCode == null
                    || oldItemCode != inProcessLocationDetailTrackView.OrderDetail.Item.Code
                    || oldUom != inProcessLocationDetailTrackView.OrderDetail.Uom.Code
                    || oldUnitCount != inProcessLocationDetailTrackView.OrderDetail.UnitCount)
                {
                    intransitDetail = new IntransitDetail();
                    intransitDetail.PartyFrom = inProcessLocationDetailTrackView.OrderDetail.OrderHead.PartyFrom.Name;
                    intransitDetail.PartyTo = inProcessLocationDetailTrackView.OrderDetail.OrderHead.PartyTo.Name;
                    intransitDetail.ItemCode = inProcessLocationDetailTrackView.OrderDetail.Item.Code;
                    intransitDetail.ItemName = inProcessLocationDetailTrackView.OrderDetail.Item.Description;
                    intransitDetail.ReferenceItem = inProcessLocationDetailTrackView.OrderDetail.ReferenceItemCode;
                    intransitDetail.Uom = inProcessLocationDetailTrackView.OrderDetail.Uom.Code;
                    intransitDetail.UnitCount = inProcessLocationDetailTrackView.OrderDetail.UnitCount;

                    oldItemCode = inProcessLocationDetailTrackView.OrderDetail.Item.Code;
                    oldUom = inProcessLocationDetailTrackView.OrderDetail.Uom.Code;
                    oldUnitCount = inProcessLocationDetailTrackView.OrderDetail.UnitCount;

                    intransitDetailList.Add(intransitDetail);
                }

                switch (FindActvitySeq(routingDetailList, inProcessLocationDetailTrackView.CurrentOperation))
                {
                    case 0:
                        intransitDetail.DefaultActivity += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 1:
                        intransitDetail.Activity1 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 2:
                        intransitDetail.Activity2 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 3:
                        intransitDetail.Activity3 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 4:
                        intransitDetail.Activity4 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 5:
                        intransitDetail.Activity5 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 6:
                        intransitDetail.Activity6 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 7:
                        intransitDetail.Activity7 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 8:
                        intransitDetail.Activity8 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 9:
                        intransitDetail.Activity9 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 10:
                        intransitDetail.Activity10 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 11:
                        intransitDetail.Activity11 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 12:
                        intransitDetail.Activity12 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 13:
                        intransitDetail.Activity13 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 14:
                        intransitDetail.Activity14 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 15:
                        intransitDetail.Activity15 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 16:
                        intransitDetail.Activity16 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 17:
                        intransitDetail.Activity17 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 18:
                        intransitDetail.Activity18 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 19:
                        intransitDetail.Activity19 += inProcessLocationDetailTrackView.Qty;
                        break;
                    case 20:
                        intransitDetail.Activity20 += inProcessLocationDetailTrackView.Qty;
                        break;
                }
            }

            this.lblNoRecordFound.Visible = false;
            this.GV_List.DataSource = intransitDetailList;
            this.GV_List.DataBind();
        }
        else
        {
            this.lblNoRecordFound.Visible = true;
            this.GV_List.DataSource = null;
            this.GV_List.DataBind();
        }
        #endregion

    }

    public override void UpdateView()
    {
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lbtnDetail_Click(object sender, EventArgs e)
    {
        string s = ((LinkButton)sender).CommandArgument;
        string[] array = s.Split('$');
        string itemCode = array[0];
        string uom = array[1];
        decimal unitCount = decimal.Parse(array[2]);
        int position = int.Parse(array[3]);

        DetailEvent(new object[] { this.FlowCode, itemCode, uom, unitCount, position }, e);
    }

    private int FindActvitySeq(IList<RoutingDetail> routingDetailList, int? operation)
    {
        if (!operation.HasValue)
        {
            return 0;
        }

        if (routingDetailList != null && routingDetailList.Count > 0)
        {
            for (int i = 0; i < routingDetailList.Count; i++)
            {
                if (routingDetailList[i].Operation == operation.Value)
                {
                    return i + 1;
                }
            }

            return 0;
        }
        else
        {
            return 0;
        }
    }
}

class IntransitDetail
{
    public string PartyFrom { get; set; }
    public string PartyTo { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string ReferenceItem { get; set; }
    public string Uom { get; set; }
    public decimal UnitCount { get; set; }
    public decimal DefaultActivity { get; set; }
    public decimal Activity1 { get; set; }
    public decimal Activity2 { get; set; }
    public decimal Activity3 { get; set; }
    public decimal Activity4 { get; set; }
    public decimal Activity5 { get; set; }
    public decimal Activity6 { get; set; }
    public decimal Activity7 { get; set; }
    public decimal Activity8 { get; set; }
    public decimal Activity9 { get; set; }
    public decimal Activity10 { get; set; }
    public decimal Activity11 { get; set; }
    public decimal Activity12 { get; set; }
    public decimal Activity13 { get; set; }
    public decimal Activity14 { get; set; }
    public decimal Activity15 { get; set; }
    public decimal Activity16 { get; set; }
    public decimal Activity17 { get; set; }
    public decimal Activity18 { get; set; }
    public decimal Activity19 { get; set; }
    public decimal Activity20 { get; set; }
}
