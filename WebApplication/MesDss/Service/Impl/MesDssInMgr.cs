using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.Services.Transaction;
using com.Mes.Dss.Entity;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Mes;
using com.Sconit.Service;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Mes;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Mes.Dss.Service.Impl
{
    [Transactional]
    public class MesDssInMgr : SessionBase, IMesDssInMgr
    {
        public IMesScmsTableIndexMgr mesScmsTableIndexMgr { get; set; }
        public IMesScmsCompletedOrderMgr mesScmsCompletedOrderMgr { get; set; }
        public IMesScmsCompletedBoxMgr mesScmsCompletedBoxMgr { get; set; }
        public IMesScmsRepairMaterialMgr mesScmsRepairMaterialMgr { get; set; }
        public IMesScmsStationShelfMgr mesScmsStationShelfMgr { get; set; }
        public IMesScmsStationBoxMgr mesScmsStationBoxMgr { get; set; }
        public IShelfMgr shelfMgr { get; set; }
        public IUserMgr userMgr { get; set; }
        public IShelfItemMgr shelfItemMgr { get; set; }
        public IBomMgr bomMgr { get; set; }
        public IBomDetailMgr bomDetailMgr { get; set; }
        public IHuMgr huMgr { get; set; }
        public IOrderMgr orderMgr { get; set; }
        public IInspectOrderMgr inspectOrderMgr { get; set; }
        public IRoutingDetailMgr routingDetailMgr { get; set; }
        public IMesScmsShelfPartMgr mesScmsShelfPartMgr { get; set; }
        public IMesScmsBomMgr mesScmsBomMgr { get; set; }
        public IItemMgr itemMgr { get; set; }
        public IFlowMgr flowMgr { get; set; }
        public ICriteriaMgr criteriaMgr { get; set; }
        public IOrderLocationTransactionMgr orderLocationTransactionMgr { get; set; }
        public IMesScmsCompletedIssueMgr mesScmsCompletedIssueMgr { get; set; }
        

        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MesDssIn");


        public static readonly string DSS_IN_SHELF = "MES_SCMS_STATION_SHELF";
        public static readonly string DSS_IN_ORDER = "MES_SCMS_COMPLETED_ORDER";
        public static readonly string DSS_IN_ORDERLOCATIONTRANSACTION = "MES_SCMS_STATION_BOX";
        public static readonly string DSS_IN_SHELFITEM = "MES_SCMS_SHELF_PART";
        public static readonly string DSS_IN_ORDER_BOX = "MES_SCMS_COMPLETED_BOX";
        public static readonly string DSS_IN_BOMDETAIL = "MES_SCMS_BOM";
        public static readonly string DSS_IN_INSPECTORDER = "MES_SCMS_REPAIR_MATERIAL";

        public void ProcessIn(MesScmsTableIndex mesScmsTableIndex)
        {
            if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_SHELF)
            {
                this.ProcessShelfIn(mesScmsTableIndex);
            }
            else if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_ORDER)
            {
                this.ProcessOrderIn(mesScmsTableIndex);
            }
            else if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_ORDERLOCATIONTRANSACTION)
            {
                this.ProcessOrderLocationTransactionIn(mesScmsTableIndex);
            }
            else if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_SHELFITEM)
            {
                this.ProcessShelfItemIn(mesScmsTableIndex);
            }
            else if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_ORDER_BOX)
            {
                this.ProcessOrderIn(mesScmsTableIndex);
            }
            else if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_BOMDETAIL)
            {
                this.ProcessBomDetailIn(mesScmsTableIndex);
            }
            else if (mesScmsTableIndex.TableName.Trim().ToUpper() == BusinessConstants.DSS_IN_INSPECTORDER)
            {
                this.ProcessInspectOrderIn(mesScmsTableIndex);
            }
        }


        private void ProcessOrderIn(MesScmsTableIndex mesScmsTableIndex)
        {
            if (mesScmsTableIndex.TableName == MesDssConstants.MES_SCMS_TABLEINDEX_TABLE_NAME_MES_SCMS_COMPLETED_ORDER)
            {
                IList<MesScmsCompletedOrder> orderList = mesScmsCompletedOrderMgr.GetUpdateMesScmsCompletedOrder();
                if (orderList != null && orderList.Count > 0)
                {
                    foreach (MesScmsCompletedOrder mesScmsCompletedOrder in orderList)
                    {
                        
                        try
                        {
                            if (mesScmsCompletedBoxMgr.GetMesScmsCompletedBox(mesScmsCompletedOrder.OrderNo) > 0) continue;
                            orderMgr.ManualCompleteOrder(mesScmsCompletedOrder.OrderNo, userMgr.GetMonitorUser());
                            mesScmsCompletedOrderMgr.Complete(mesScmsCompletedOrder);
                         
                        }
                        catch (Exception e)
                        {
                            this.CleanSession();
                            log.Error(mesScmsCompletedOrder.OrderNo + " complete exception", e);
                            continue;
                        }
                    }
                }
            }
            else if (mesScmsTableIndex.TableName == MesDssConstants.MES_SCMS_TABLEINDEX_TABLE_NAME_MES_SCMS_COMPLETED_BOX)
            {
                IList<MesScmsCompletedBox> huList = mesScmsCompletedBoxMgr.GetUpdateMesScmsCompletedBox();
                if (huList != null && huList.Count > 0)
                {
                    foreach (MesScmsCompletedBox mesScmsCompletedBox in huList)
                    {
                        try
                        {
                            DetachedCriteria criteria = DetachedCriteria.For<OrderDetail>();
                            criteria.CreateAlias("OrderHead", "oh");
                            criteria.Add(Expression.Eq("oh.OrderNo", mesScmsCompletedBox.OrderNo));
                            criteria.Add(Expression.Eq("Item.Code", mesScmsCompletedBox.ItemCode));
                            criteria.Add(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
                            IList<OrderDetail> orderDetailList = criteriaMgr.FindAll<OrderDetail>(criteria);
                            if (orderDetailList.Count == 0)
                            {
                                log.Error(mesScmsCompletedBox.HuId + " not found order");
                                continue;
                            }
                            orderMgr.DoReceiveWO(mesScmsCompletedBox.HuId, orderDetailList[0], (decimal)mesScmsCompletedBox.Qty);
                            //可能在这里添加逻辑  
                            //
                            //
                            
                            //IList<MesScmsCompletedIssue> itemList = mesScmsCompletedIssueMgr.GetUpdateMesScmsCompletedIssue(mesScmsCompletedBox.OrderNo, mesScmsCompletedBox.HuId);

                            //Bom updateBom = BomCompare(orderDetailList[0].Bom.BomDetails, itemList);
                            mesScmsCompletedBoxMgr.Complete(mesScmsCompletedBox);
                        }
                        catch (Exception e)
                        {
                            this.CleanSession();
                            log.Error(mesScmsCompletedBox.HuId + " complete exception", e);
                            continue;
                        }
                    }
                }
            }
            mesScmsTableIndexMgr.Complete(mesScmsTableIndex);
        }

        //private Bom BomCompare(IList<BomDetail> bomScms, IList<MesScmsCompletedIssue> bomMes)
        //{
        //    Dictionary<string, string> updateDict = new Dictionary<string, string>();
        //    string[] delList = { };
        //    string[] insertList = { };
        //    foreach (BomDetail detail in bomScms)
        //    {
             
        //        foreach (MesScmsCompletedIssue mes in bomMes)
        //        { 
                    
        //        }
        //    }
            
            
        //    return null;
        // }

        private void ProcessInspectOrderIn(MesScmsTableIndex mesScmsTableIndex)
        {

            IList<MesScmsRepairMaterial> repairMaterialList = mesScmsRepairMaterialMgr.GetUpdateMesScmsRepairMaterial();
            if (repairMaterialList != null && repairMaterialList.Count > 0)
            {

                List<string> rejList = repairMaterialList.Select(l => l.RejectNo).Distinct().ToList();
                foreach (string rejNo in rejList)
                {
                    IList<MesScmsRepairMaterial> rejMaterialList = repairMaterialList.Where(l => l.RejectNo == rejNo).ToList();
                    IDictionary<string, decimal> itemDic = new Dictionary<string, decimal>();
                    foreach (MesScmsRepairMaterial repairMaterial in rejMaterialList)
                    {
                        if (itemDic.ContainsKey(repairMaterial.ItemCode))
                        {
                            itemDic[repairMaterial.ItemCode] += Convert.ToDecimal(repairMaterial.Qty);
                        }
                        else
                        {
                            itemDic.Add(repairMaterial.ItemCode, Convert.ToDecimal(repairMaterial.Qty));
                        }
                    }


                    try
                    {
                        Flow flow = flowMgr.LoadFlowByDesc(rejMaterialList[0].ProductLine);

                        InspectOrder inspectOrder = inspectOrderMgr.CreateInspectOrder(flow.LocationFrom.Code, itemDic, userMgr.GetMonitorUser());
                        if (inspectOrder.InspectOrderDetails != null && inspectOrder.InspectOrderDetails.Count > 0)
                        {
                            foreach (InspectOrderDetail inspectOrderDetail in inspectOrder.InspectOrderDetails)
                            {
                                inspectOrderDetail.CurrentRejectedQty = inspectOrderDetail.InspectQty;
                            }
                        }
                        inspectOrderMgr.PendInspectOrder(inspectOrder.InspectOrderDetails, userMgr.GetMonitorUser(), rejNo);


                        foreach (MesScmsRepairMaterial repairMaterial in rejMaterialList)
                        {

                            mesScmsRepairMaterialMgr.Complete(repairMaterial);
                        }
                    }
                    catch (Exception e)
                    {
                        this.CleanSession();
                        log.Error(repairMaterialList[0].RejectNo + " complete exception", e);
                        continue;
                    }

                }
            }
            mesScmsTableIndexMgr.Complete(mesScmsTableIndex);
        }

        private void ProcessShelfIn(MesScmsTableIndex mesScmsTableIndex)
        {
            #region 工位货架
            IList<MesScmsStationShelf> mesScmsStationShelfList = mesScmsStationShelfMgr.GetUpdateMesScmsStationShelf();
            if (mesScmsStationShelfList != null && mesScmsStationShelfList.Count > 0)
            {
                foreach (MesScmsStationShelf mesScmsStationShelf in mesScmsStationShelfList)
                {
                    if (mesScmsStationShelf.Flag == MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED)
                    {
                        Shelf shelf = shelfMgr.LoadShelf(mesScmsStationShelf.ShelfNo);
                        if (shelf == null)
                        {
                            shelf = new Shelf();
                            shelf.Code = mesScmsStationShelf.ShelfNo;
                            Flow flow = flowMgr.LoadFlowByDesc(mesScmsStationShelf.LineName);
                            if (flow == null)
                            {
                                log.Error(mesScmsStationShelf.LineName + " not found ");
                                continue;
                            }
                            shelf.ProductLine = flow;
                            shelf.IsActive = true;
                            shelf.Capacity = mesScmsStationShelf.Qty;
                            shelf.TagNo = mesScmsStationShelf.StationName;
                            shelfMgr.CreateShelf(shelf);
                        }
                        else
                        {
                            Flow flow = flowMgr.LoadFlow(mesScmsStationShelf.LineName);
                            if (flow == null)
                            {
                                log.Error(mesScmsStationShelf.LineName + " not found ");
                                continue;
                            }
                            shelf.ProductLine = flow;
                            shelf.IsActive = true;
                            shelf.Capacity = mesScmsStationShelf.Qty;
                            shelf.TagNo = mesScmsStationShelf.StationName;
                            shelfMgr.UpdateShelf(shelf);

                        }
                    }
                    else if (mesScmsStationShelf.Flag == MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED)
                    {
                        try
                        {
                            Shelf shelf = shelfMgr.LoadShelf(mesScmsStationShelf.ShelfNo);
                            if (shelf != null)
                            {
                                shelf.IsActive = false;
                                shelfMgr.UpdateShelf(shelf);
                            }
                        }
                        catch (Exception e)
                        {
                            this.CleanSession();
                            log.Error(mesScmsStationShelf.ShelfNo + " delete exception", e);
                            continue;
                        }
                    }

                    try
                    {
                        mesScmsStationShelfMgr.Complete(mesScmsStationShelf);
                    }
                    catch (Exception e)
                    {
                        this.CleanSession();
                        log.Error(mesScmsStationShelf.ShelfNo + " complete exception", e);
                        continue;
                    }
                }
            }
            #endregion

            mesScmsTableIndexMgr.Complete(mesScmsTableIndex);
        }

        private void ProcessOrderLocationTransactionIn(MesScmsTableIndex mesScmsTableIndex)
        {
            #region 物料消耗
            IList<MesScmsStationBox> stationBoxList = mesScmsStationBoxMgr.GetUpdateMesScmsStationBox();

            if (stationBoxList != null && stationBoxList.Count > 0)
            {
                foreach (MesScmsStationBox stationBox in stationBoxList)
                {
                    try
                    {
                        Hu hu = huMgr.CheckAndLoadHu(stationBox.HuId);
                        OrderLocationTransaction olt = orderLocationTransactionMgr.GetOrderLocationTransaction(stationBox.OrderNo, stationBox.TagNo, hu.Item.Code);
                        if (olt == null)
                        {
                            //没找到，算传错了，记错误日志，更新标记
                            log.Error(stationBox.Id + " not found match order");
                        }
                        else
                        {
                            //货架上没有了,跳过
                            if (olt.Cartons == 0)
                            {
                                log.Error(stationBox.Id + "," + hu.Item.Code + "," + "current carton zero");
                            }
                            else
                            {
                                olt.Cartons = olt.Cartons - 1;
                                this.orderLocationTransactionMgr.UpdateOrderLocationTransaction(olt);

                                hu.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                                huMgr.UpdateHu(hu);
                            }
                        }
                        mesScmsStationBoxMgr.Complete(stationBox);
                    }
                    catch (Exception e)
                    {
                        this.CleanSession();
                        log.Error(stationBox.Id + " complete exception", e);
                        continue;
                    }
                }
            }
            #endregion

            mesScmsTableIndexMgr.Complete(mesScmsTableIndex);
        }

        private void ProcessBomDetailIn(MesScmsTableIndex mesScmsTableIndex)
        {
            IList<MesScmsBom> bomDetailList = mesScmsBomMgr.GetUpdateMesScmsBom();
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                foreach (MesScmsBom mesScmsBom in bomDetailList)
                {
                    try
                    {
                        Bom bom = bomMgr.LoadBom(mesScmsBom.Bom);
                        if (bom == null)
                        {
                            bom = new Bom();
                            bom.Code = mesScmsBom.Bom;
                            bom.IsActive = true;
                            bom.Uom = itemMgr.LoadItem(mesScmsBom.ItemCode).Uom;
                            bomMgr.CreateBom(bom);
                        }
                        BomDetail bomDetail = LoadBomDetail(mesScmsBom.Bom, mesScmsBom.ItemCode, Int32.Parse(mesScmsBom.Operation), DateTime.Now);
                        if (bomDetail == null && mesScmsBom.Qty > 0)
                        {
                            bomDetail = new BomDetail();
                            bomDetail.Bom = bom;
                            bomDetail.Item = itemMgr.LoadItem(mesScmsBom.ItemCode);
                            Item bomItem = itemMgr.LoadItem(mesScmsBom.Bom);
                            bomDetail.BackFlushMethod = bomDetail.Item.BackFlushMethod;
                            bomDetail.NeedPrint = true;
                            bomDetail.Operation = Convert.ToInt32(mesScmsBom.Operation);
                            bomDetail.RateQty = mesScmsBom.Qty;
                            bomDetail.StructureType = BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_N;
                            bomDetail.StartDate = DateTime.Now;
                            bomDetail.Uom = itemMgr.LoadItem(mesScmsBom.ItemCode).Uom;
                            if (mesScmsBom.Flag == MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED)
                            {
                                bomDetail.EndDate = DateTime.Now.AddDays(-1);
                            }
                            bomDetailMgr.CreateBomDetail(bomDetail);
                        }
                        else
                        {
                            if (mesScmsBom.Qty == 0)
                            {
                                bomDetail.EndDate = DateTime.Now.AddDays(-1);
                            }
                            bomDetail.RateQty = mesScmsBom.Qty;
                            bomDetailMgr.UpdateBomDetail(bomDetail);
                        }
                        #region 更新工序和工位对应,现在工序和工位唯一对应

                        #region 找routing
                        DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
                        criteria.Add(Expression.Eq("Code", mesScmsBom.ProductLine));

                        IList<Flow> flowList = criteriaMgr.FindAll<Flow>(criteria);
                        if (flowList == null || flowList.Count == 0)
                        {
                            log.Error(mesScmsBom.ProductLine + " not found");
                            continue;
                        }

                        if (flowList[0].Routing == null)
                        {
                            log.Error(mesScmsBom.ProductLine + " not found routing");
                            continue;
                        }
                        #endregion

                        #region 找routingdet
                        DetachedCriteria rcriteria = DetachedCriteria.For(typeof(RoutingDetail));
                        rcriteria.Add(Expression.Eq("Routing.Code", flowList[0].Routing.Code));
                        rcriteria.Add(Expression.Eq("Operation", Convert.ToInt32(mesScmsBom.Operation)));

                        IList<RoutingDetail> routingDetailList = criteriaMgr.FindAll<RoutingDetail>(rcriteria);
                        if (routingDetailList == null || routingDetailList.Count == 0)
                        {
                            log.Error(flowList[0].Routing.Code + " not found routing detail");
                            continue;
                        }
                        #endregion

                        RoutingDetail rd = routingDetailList[0];
                        rd.TagNo = mesScmsBom.TagNo;
                        routingDetailMgr.UpdateRoutingDetail(rd);
                        #endregion

                        mesScmsBomMgr.Complete(mesScmsBom);
                    }
                    catch (Exception e)
                    {
                        this.CleanSession();
                        log.Error(mesScmsBom.Bom + "," + mesScmsBom.ItemCode + "," + mesScmsBom.ProductLine + "," + mesScmsBom.TagNo + " complete exception", e);
                        continue;
                    }
                }
            }

            mesScmsTableIndexMgr.Complete(mesScmsTableIndex);
        }

        private void ProcessShelfItemIn(MesScmsTableIndex mesScmsTableIndex)
        {
            IList<MesScmsShelfPart> mesScmsShelfPartList = mesScmsShelfPartMgr.GetUpdateMesScmsShelfPart();
            if (mesScmsShelfPartList != null && mesScmsShelfPartList.Count > 0)
            {
                foreach (MesScmsShelfPart mesScmsShelfPart in mesScmsShelfPartList)
                {
                    if (mesScmsShelfPart.Flag == MesDssConstants.MES_SCMS_FLAG_FTPC_UPDATED)
                    {
                        ShelfItem shelfItem = shelfItemMgr.LoadShelfItem(mesScmsShelfPart.ShelfNo, mesScmsShelfPart.ItemCode);
                        if (shelfItem == null)
                        {
                            shelfItem = new ShelfItem();
                            Shelf shelf = shelfMgr.LoadShelf(mesScmsShelfPart.ShelfNo);

                            if (shelf == null)
                            {
                                log.Error(mesScmsShelfPart.ShelfNo + " not found ");
                                continue;
                            }

                            Item item = itemMgr.LoadItem(mesScmsShelfPart.ItemCode);
                            if (item == null)
                            {
                                log.Error(mesScmsShelfPart.ItemCode + " not found ");
                                continue;
                            }

                            shelfItem.Shelf = shelf;
                            shelfItem.Item = item;
                            shelfItem.IsActive = true;

                            shelfItemMgr.CreateShelfItem(shelfItem);
                        }

                    }
                    else if (mesScmsShelfPart.Flag == MesDssConstants.MES_SCMS_FLAG_FTPC_DELETED)
                    {
                        try
                        {
                            ShelfItem shelfItem = shelfItemMgr.LoadShelfItem(mesScmsShelfPart.ShelfNo, mesScmsShelfPart.ItemCode);
                            if (shelfItem != null)
                            {
                                shelfItem.IsActive = false;
                                shelfItemMgr.UpdateShelfItem(shelfItem);
                            }
                        }
                        catch (Exception e)
                        {
                            this.CleanSession();
                            log.Error(mesScmsShelfPart.ShelfNo + "-" + mesScmsShelfPart.ItemCode + " delete exception", e);
                            continue;
                        }
                    }

                    try
                    {
                        mesScmsShelfPartMgr.Complete(mesScmsShelfPart);
                    }
                    catch (Exception e)
                    {
                        this.CleanSession();
                        log.Error(mesScmsShelfPart.ShelfNo + " complete exception", e);
                        continue;
                    }
                }
            }

            mesScmsTableIndexMgr.Complete(mesScmsTableIndex);
        }

        private BomDetail LoadBomDetail(string bomCode, string itemCode, int op, DateTime effDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(BomDetail));
            criteria.Add(Expression.Eq("Bom.Code", bomCode));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.Add(Expression.Eq("Operation", op));
            criteria.Add(Expression.Le("StartDate", effDate));
            criteria.Add(Expression.Or(Expression.Ge("EndDate", effDate), Expression.IsNull("EndDate")));
            criteria.AddOrder(Order.Desc("StartDate"));

            IList<BomDetail> bomDetailList = criteriaMgr.FindAll<BomDetail>(criteria);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                return bomDetailList[0];
            }
            else
            {
                return null;
            }
        }
    }
}
