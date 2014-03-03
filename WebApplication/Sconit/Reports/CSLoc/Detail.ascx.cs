using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Utility;
using NHibernate.Transform;

public partial class Reports_CSLoc_Detail : EditModuleBase
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

    private PlannedBillView plannedBillView;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.GV_List.Columns[0].Visible =false;
            this.GV_List.Columns[1].Visible = false;
        }
    }

    public void InitPageParameter(int id)
    {
        plannedBillView = this.ThePlannedBillViewMgr.LoadPlannedBillView(id);

        #region 查询PlannedBillView对应那些PlannedBill
        DetachedCriteria planBillCriteria = DetachedCriteria.For(typeof(PlannedBill));
        planBillCriteria.CreateAlias("Item", "item");
        planBillCriteria.CreateAlias("Uom", "uom");
        planBillCriteria.CreateAlias("BillAddress", "billAddr");

        planBillCriteria.Add(Expression.Or(Expression.IsNull("ActingQty"), Expression.Not(Expression.EqProperty("PlannedQty", "ActingQty"))));
        planBillCriteria.Add(Expression.Eq("item.Code", plannedBillView.Item.Code));
        planBillCriteria.Add(Expression.Eq("uom.Code", plannedBillView.Uom.Code));
        planBillCriteria.Add(Expression.Eq("UnitCount", plannedBillView.UnitCount));
        planBillCriteria.Add(Expression.Eq("billAddr.Code", plannedBillView.BillAddress.Code));

        IList<PlannedBill> plannedBillList = this.TheCriteriaMgr.FindAll<PlannedBill>(planBillCriteria);
        #endregion

        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            #region 查询LoctionLotDetail并转换为CSInventoryDetail
            DetachedCriteria locLotDetCriteria = DetachedCriteria.For(typeof(LocationLotDetail));
            
            locLotDetCriteria.CreateAlias("Location", "loc");
            locLotDetCriteria.CreateAlias("StorageBin", "bin", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            locLotDetCriteria.CreateAlias("Item", "item");
            locLotDetCriteria.CreateAlias("PlannedBill", "pb");
            locLotDetCriteria.CreateAlias("pb.OrderHead", "od");
            //locLotDetCriteria.CreateAlias("Hu", "hu");

            locLotDetCriteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.GroupProperty("loc.Code").As("LocationCode"))
                .Add(Projections.GroupProperty("loc.Name").As("LocationName"))
                .Add(Projections.GroupProperty("bin.Code").As("Bin"))
                //.Add(Projections.GroupProperty("hu.HuId"))
                .Add(Projections.GroupProperty("LotNo").As("LotNo"))
                .Add(Projections.GroupProperty("od.OrderNo").As("OrderNo"))
                .Add(Projections.GroupProperty("pb.ReceiptNo").As("ReceiptNo"))
                .Add(Projections.GroupProperty("pb.CreateDate").As("ReceiptDate"))
                .Add(Projections.GroupProperty("pb.SettleTerm").As("SettleTerm"))
                .Add(Projections.Sum("Qty").As("i"))
                .Add(Projections.GroupProperty("pb.UnitQty").As("j"))
                );

            locLotDetCriteria.Add(Expression.Not(Expression.Eq("Qty", decimal.Zero)));
            locLotDetCriteria.Add(Expression.Eq("IsConsignment", true));
            locLotDetCriteria.Add(Expression.InG("PlannedBill", plannedBillList));
         
            IList<CSInventoryDetail> csInventoryDetailList = ConvertLocLotDet2CSInventoryDetail(this.TheCriteriaMgr.FindAll(locLotDetCriteria));
            #endregion

            #region 查询IpDetail并转换为CSInventoryDetail
            DetachedCriteria ipDetCriteria = DetachedCriteria.For(typeof(InProcessLocationDetail));
            ipDetCriteria.CreateAlias("OrderLocationTransaction", "olt");
            ipDetCriteria.CreateAlias("InProcessLocation", "ip");
            ipDetCriteria.CreateAlias("PlannedBill", "pb");
            ipDetCriteria.CreateAlias("pb.OrderHead", "od");

            ipDetCriteria.SetProjection(
               Projections.ProjectionList()
               .Add(Projections.GroupProperty("ip.IpNo"))
                //.Add(Projections.GroupProperty("HuId"))
               .Add(Projections.GroupProperty("LotNo"))
               .Add(Projections.GroupProperty("od.OrderNo"))
               .Add(Projections.GroupProperty("pb.ReceiptNo"))
               .Add(Projections.GroupProperty("pb.CreateDate"))
               .Add(Projections.GroupProperty("pb.SettleTerm"))
               .Add(Projections.Sum("Qty"))
               .Add(Projections.GroupProperty("pb.UnitQty"))
               );

            ipDetCriteria.Add((Expression.Eq("ip.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)));
            ipDetCriteria.Add(Expression.InG("PlannedBill", plannedBillList));
            IList<CSInventoryDetail> csInventoryDetailList2 = ConvertIpDet2CSInventoryDetail(this.TheCriteriaMgr.FindAll(ipDetCriteria));

            if (csInventoryDetailList2.Count > 0)
            {
                IListHelper.AddRange<CSInventoryDetail>(csInventoryDetailList, csInventoryDetailList2);

                IListHelper.Sort<CSInventoryDetail>(csInventoryDetailList, "ReceiptDate");
            }
            #endregion

            this.GV_List.DataSource = csInventoryDetailList;
            this.GV_List.DataBind();
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            DetachedCriteria planBillDetailCriteria = DetachedCriteria.For(typeof(PlannedBill));
            planBillDetailCriteria.CreateAlias("OrderHead", "od");

            planBillDetailCriteria.SetProjection(
               Projections.ProjectionList()
               .Add(Projections.GroupProperty("LotNo"))
               .Add(Projections.GroupProperty("od.OrderNo"))
               .Add(Projections.GroupProperty("ExternalReceiptNo"))
               .Add(Projections.GroupProperty("CreateDate"))
               .Add(Projections.GroupProperty("SettleTerm"))
               .Add(Projections.Sum("PlannedQty"))
               .Add(Projections.Sum("ActingQty"))
               );

            IList<int> idList = new List<int>();
            foreach(PlannedBill pb in plannedBillList)
            {
                idList.Add(pb.Id);
            }

            planBillDetailCriteria.Add(Expression.InG("Id", idList));

            this.GV_List.DataSource = ConvertPlanBill2CSInventoryDetail(this.TheCriteriaMgr.FindAll(planBillDetailCriteria));
            this.GV_List.DataBind();
        }
    }   

    private IList<CSInventoryDetail> ConvertLocLotDet2CSInventoryDetail(IList list)
    {
        IList<CSInventoryDetail> csInventoryDetailList = new List<CSInventoryDetail>();

        if (list != null && list.Count > 0)
        {
            foreach (object[] objArray in list)
            {
                CSInventoryDetail csInventoryDetail = new CSInventoryDetail();
                csInventoryDetail.LocationCode = objArray[0] as string;
                csInventoryDetail.LocationName = objArray[1] as string;
                csInventoryDetail.Bin = objArray[2] as string;
                csInventoryDetail.ItemCode = plannedBillView.Item.Code;
                csInventoryDetail.ItemName = plannedBillView.Item.Description;
                csInventoryDetail.UnitCount = plannedBillView.UnitCount;
                csInventoryDetail.Uom = plannedBillView.Uom.Code;
                //csInventoryDetail.HuId = objArray[3] as string;
                csInventoryDetail.LotNo = objArray[3] as string;
                csInventoryDetail.OrderNo = objArray[4] as string;
                csInventoryDetail.ReceiptNo = objArray[5] as string;
                csInventoryDetail.ReceiptDate = DateTime.Parse(objArray[6].ToString());
                csInventoryDetail.SettleTerm = objArray[7] as string;
                csInventoryDetail.Qty = decimal.Parse(objArray[8].ToString()) / decimal.Parse(objArray[9].ToString());

                csInventoryDetailList.Add(csInventoryDetail);
            }
        }

        return csInventoryDetailList;
    }

    private IList<CSInventoryDetail> ConvertIpDet2CSInventoryDetail(IList list)
    {
        IList<CSInventoryDetail> csInventoryDetailList = new List<CSInventoryDetail>();

        if (list != null && list.Count > 0)
        {
            foreach (object[] objArray in list)
            {
                CSInventoryDetail csInventoryDetail = new CSInventoryDetail();
                csInventoryDetail.LocationCode = "${Reports.CSLoc.Ip} (" + objArray[0] as string + ")";
                csInventoryDetail.LocationName = "${Reports.CSLoc.Ip} (" + objArray[0] as string + ")";
                csInventoryDetail.ItemCode = plannedBillView.Item.Code;
                csInventoryDetail.ItemName = plannedBillView.Item.Description;
                csInventoryDetail.UnitCount = plannedBillView.UnitCount;
                csInventoryDetail.Uom = plannedBillView.Uom.Code;
                //csInventoryDetail.HuId = objArray[1] as string;
                csInventoryDetail.LotNo = objArray[1] as string;
                csInventoryDetail.OrderNo = objArray[2] as string;
                csInventoryDetail.ReceiptNo = objArray[3] as string;
                csInventoryDetail.ReceiptDate = DateTime.Parse(objArray[4].ToString());
                csInventoryDetail.SettleTerm = objArray[5] as string;
                csInventoryDetail.Qty = decimal.Parse(objArray[6].ToString()) - decimal.Parse(objArray[7].ToString());

                csInventoryDetailList.Add(csInventoryDetail);
            }
        }

        return csInventoryDetailList;
    }

    private IList<CSInventoryDetail> ConvertPlanBill2CSInventoryDetail(IList list)
    {
        IList<CSInventoryDetail> csInventoryDetailList = new List<CSInventoryDetail>();

        if (list != null && list.Count > 0)
        {
            foreach (object[] objArray in list)
            {
                CSInventoryDetail csInventoryDetail = new CSInventoryDetail();
                csInventoryDetail.ItemCode = plannedBillView.Item.Code;
                csInventoryDetail.ItemName = plannedBillView.Item.Description;
                csInventoryDetail.UnitCount = plannedBillView.UnitCount;
                csInventoryDetail.Uom = plannedBillView.Uom.Code;
                //csInventoryDetail.HuId = objArray[1] as string;
                csInventoryDetail.LotNo = objArray[0] as string;
                csInventoryDetail.OrderNo = objArray[1] as string;
                csInventoryDetail.ReceiptNo = objArray[2] as string;
                csInventoryDetail.ReceiptDate = DateTime.Parse(objArray[3].ToString());
                csInventoryDetail.SettleTerm = objArray[4] as string;
                csInventoryDetail.Qty = decimal.Parse(objArray[5].ToString()) - (objArray[6] != null ? decimal.Parse(objArray[6].ToString()) : 0);

                csInventoryDetailList.Add(csInventoryDetail);
            }
        }

        return csInventoryDetailList;
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }
}

class CSInventoryDetail
{
    public string LocationCode { get; set; }
    public string LocationName { get; set; }
    public string Bin { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public string Uom { get; set; }
    public decimal UnitCount { get; set; }
    public string HuId { get; set; }
    public string LotNo { get; set; }
    public decimal Qty { get; set; }
    public string OrderNo { get; set; }
    public string ReceiptNo { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string SettleTerm { get; set; }
}
