using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Entity.Production;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class LocationMgr : LocationBaseMgr, ILocationMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private INumberControlMgr numberControlMgr;
        private ILocationTransactionMgr locationTransactionMgr;
        private IRegionMgr regionMgr;
        private IUserMgr userMgr;
        private IPlannedBillMgr plannedBillMgr;
        private IStorageBinMgr storageBinMgr;
        private IHuMgr huMgr;
        private IBillMgr billMgr;
        private IItemMgr itemMgr;

        public LocationMgr(ILocationDao entityDao,
            ILocationLotDetailMgr locationLotDetailMgr,
            INumberControlMgr numberControlMgr,
            ICriteriaMgr criteriaMgr,
            ILocationTransactionMgr locationTransactionMgr,
            IRegionMgr regionMgr,
            IUserMgr userMgr,
            IPlannedBillMgr plannedBillMgr,
            IStorageBinMgr storageBinMgr,
            IHuMgr huMgr,
            IBillMgr billMgr,
            IItemMgr itemMgr)
            : base(entityDao)
        {
            this.numberControlMgr = numberControlMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.criteriaMgr = criteriaMgr;
            this.locationTransactionMgr = locationTransactionMgr;
            this.regionMgr = regionMgr;
            this.userMgr = userMgr;
            this.plannedBillMgr = plannedBillMgr;
            this.storageBinMgr = storageBinMgr;
            this.huMgr = huMgr;
            this.billMgr = billMgr;
            this.itemMgr = itemMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Location CheckAndLoadLocation(string locationCode)
        {
            Location location = this.LoadLocation(locationCode);
            if (location == null)
            {
                throw new BusinessErrorException("Location.Error.LocationCodeNotExist", locationCode);
            }

            return location;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(Region region)
        {
            return GetLocation(region.Code, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(Region region, bool includeInactive)
        {
            return GetLocation(region.Code, includeInactive);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(string regionCode)
        {
            return GetLocation(regionCode, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(string regionCode, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Location)).Add(Expression.Eq("Region.Code", regionCode));
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }
            return criteriaMgr.FindAll<Location>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(string regionCode, bool includeInactive, bool includeReject)
        {
            IList<Location> locationList = GetLocation(regionCode, includeInactive);
            if (includeReject)
            {
                locationList.Add(GetRejectLocation());
            }
            return locationList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocationByUserCode(string userCode)
        {
            User user = userMgr.LoadUser(userCode);
            return GetLocation(user);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(User user)
        {
            return GetLocation(user, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(User user, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Location>();
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }

            DetachedCriteria[] pCrieteria = SecurityHelper.GetRegionPermissionCriteria(user.Code);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Region.Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Region.Code", pCrieteria[1])));

            criteria.AddOrder(Order.Desc("Region.Code"));
            return criteriaMgr.FindAll<Location>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(string userCode, string type, bool includeInactive)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Location>();
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }
            if (type != string.Empty)
            {
                criteria.Add(Expression.Eq("Type", type));
            }

            DetachedCriteria[] pCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("Region.Code", pCrieteria[0]),
                    Subqueries.PropertyIn("Region.Code", pCrieteria[1])));

            return criteriaMgr.FindAll<Location>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(string userCode, string type)
        {
            return GetLocation(userCode, type, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Location> GetLocation(IList<string> locationCodeList)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Location>();
            if (locationCodeList.Count == 1)
            {
                criteria.Add(Expression.Eq("Code", locationCodeList[0]));
            }
            else
            {
                criteria.Add(Expression.InG<string>("Code", locationCodeList));
            }

            return criteriaMgr.FindAll<Location>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Location GetRejectLocation()
        {
            return this.LoadLocation(BusinessConstants.SYSTEM_LOCATION_REJECT);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Location GetInspectLocation()
        {
            return this.LoadLocation(BusinessConstants.SYSTEM_LOCATION_INSPECT);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryOut(InProcessLocationDetail inProcessLocationDetail, User user)
        {
            OrderLocationTransaction orderLocationTransaction = inProcessLocationDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
          
            if (inProcessLocationDetail.HuId != null && inProcessLocationDetail.HuId.Trim() != string.Empty
                && inProcessLocationDetail.OrderLocationTransaction.OrderDetail.OrderHead.SubType != BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
            {
                #region 判断是否需要下架
                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(orderLocationTransaction.Location.Code, inProcessLocationDetail.HuId.Trim());
                if (locationLotDetailList != null && locationLotDetailList.Count > 0 && locationLotDetailList[0].StorageBin != null)
                {
                    //下架
                    this.InventoryPick(locationLotDetailList[0], user);
                }
                #endregion
            }

            #region 查找出库库位
            Location outLoc = null;
            if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                outLoc = orderLocationTransaction.Location;
            }
            else
            {
                if (inProcessLocationDetail.HuId != null && inProcessLocationDetail.HuId.Trim() != string.Empty
                    && orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO)
                {
                    //生产返工，从条码上取库位
                    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(inProcessLocationDetail.HuId);
                    if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                    {
                        LocationLotDetail locationLotDetail = locationLotDetailList[0];
                        outLoc = locationLotDetail.Location;
                    }
                    else
                    {
                        throw new BusinessErrorException("Hu.Error.NoInventory", inProcessLocationDetail.HuId);
                    }
                }
                else
                {
                    outLoc = orderLocationTransaction.Location;
                }
            }
            #endregion

            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                orderLocationTransaction.Item,
                outLoc,
                inProcessLocationDetail.HuId,
                inProcessLocationDetail.LotNo,
                (0 - inProcessLocationDetail.Qty * orderLocationTransaction.UnitQty),
                false,
                null,
                orderLocationTransaction.TransactionType,
                orderHead.ReferenceOrderNo,
                //true,
                user,
                false,
                false,//(orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION),
                null
                );
            #endregion

            #region 更新条码状态和库位
            //只有普通的订单才记录，退货和调整不用考虑，因为退货和调整先做收货在做发货，会冲掉收货的记录，发货退货不考虑
            if (orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML
                && inProcessLocationDetail.HuId != null && inProcessLocationDetail.HuId.Trim() != string.Empty
                && inProcessLocationDetail.Qty > 0)
            {
                Hu hu = this.huMgr.LoadHu(inProcessLocationDetail.HuId.Trim());
                if (orderDetail.DefaultLocationTo != null)
                {
                    hu.Location = orderDetail.DefaultLocationTo.Code;
                    //出库后把库格清空，不然收货没库格的会显示发货的库格
                    hu.StorageBin = null;
                }
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INPROCESS;
                hu.TransferFlag = orderDetail.OrderHead.IsMes;
                hu.IsMes = orderDetail.OrderHead.IsMes;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, inProcessLocationDetail.InProcessLocation, user);

                #region 退货并上架
                if (inProcessLocationDetail.OrderLocationTransaction.OrderDetail.OrderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN
                    && inProcessLocationDetail.ReturnPutAwaySorageBinCode != null && inProcessLocationDetail.ReturnPutAwaySorageBinCode.Trim() != string.Empty)
                {
                    LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                    locationLotDetail.NewStorageBin = this.storageBinMgr.LoadStorageBin(inProcessLocationDetail.ReturnPutAwaySorageBinCode);
                    inventoryTransaction.StorageBin = locationLotDetail.NewStorageBin;

                    InventoryPut(locationLotDetail, user);
                }
                #endregion
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryOut(MiscOrderDetail miscOrderDetail, User user)
        {
            if (miscOrderDetail.HuId != null && miscOrderDetail.HuId.Trim() != string.Empty)
            {
                #region 判断是否需要下架
                LocationLotDetail locationLotDetail = this.locationLotDetailMgr.CheckLoadHuLocationLotDetail(miscOrderDetail.HuId.Trim(), miscOrderDetail.MiscOrder.Location);
                if (locationLotDetail.StorageBin != null)
                {
                    //下架
                    this.InventoryPick(locationLotDetail, user);
                }
                #endregion
            }

            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                miscOrderDetail.Item,
                miscOrderDetail.MiscOrder.Location,
                miscOrderDetail.HuId,
                miscOrderDetail.LotNo,
                (0 - miscOrderDetail.Qty),
                false,
                null,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP,
                null,
                //true,
                user,
                false,
                false,
                null
                );
            #endregion

            #region 更新条码状态和库位
            if (miscOrderDetail.HuId != null && miscOrderDetail.HuId.Trim() != string.Empty
                && miscOrderDetail.Qty > 0)
            {
                Hu hu = this.huMgr.LoadHu(miscOrderDetail.HuId.Trim());
                hu.Location = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CLOSE;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(miscOrderDetail, inventoryTransaction, user);
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public InventoryTransaction InventoryOut(HuOdd huOdd, ReceiptDetail receiptDetail, User user)
        {
            #region 更新库存
            LocationLotDetail locLotDet = this.locationLotDetailMgr.LoadLocationLotDetail(huOdd.LocationLotDetail.Id);
            locLotDet.Qty -= huOdd.CurrentCreateQty;
            this.locationLotDetailMgr.UpdateLocationLotDetail(locLotDet);
            #endregion

            #region 记录库存事务
            InventoryTransaction inventoryTransaction = InventoryTransactionHelper.CreateInventoryTransaction(locLotDet, 0 - huOdd.CurrentCreateQty, false);
            this.locationTransactionMgr.RecordLocationTransaction(receiptDetail.OrderLocationTransaction, inventoryTransaction, receiptDetail.Receipt, user);
            #endregion

            return inventoryTransaction;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryOut(MaterialIn materialIn, User user, Flow ProductLine)
        {
            Hu hu = null;
            LocationLotDetail huLocationLotDetail = null;
            if (materialIn.HuId != null && materialIn.HuId.Trim() != string.Empty)
            {
                hu = this.huMgr.CheckAndLoadHu(materialIn.HuId);
                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(materialIn.Location.Code, materialIn.HuId);
                if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                {
                    huLocationLotDetail = locationLotDetailList[0];

                    #region 判断是否需要下架
                    if (huLocationLotDetail.StorageBin != null)
                    {
                        //下架
                        this.InventoryPick(huLocationLotDetail, user);
                    }
                    #endregion
                }
                else
                {
                    throw new BusinessErrorException("Hu.Error.NoInventory", hu.HuId);
                }
            }

            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                materialIn.RawMaterial,
                huLocationLotDetail != null ? huLocationLotDetail.Location : materialIn.Location,
                hu != null ? hu.HuId : null,
                hu != null ? hu.LotNo : null,
                0 - materialIn.Qty,
                false,
                null,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR,
                null,
                //true,
                user,
                false,
                false,
                null
                );
            #endregion

            #region 更新条码状态，投料也算在途
            if (hu != null && materialIn.Qty > 0)
            {
                hu.Location = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INPROCESS;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN, user, null, null, ProductLine);
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryIn(ReceiptDetail receiptDetail, User user)
        {
            Receipt receipt = receiptDetail.Receipt;

            OrderLocationTransaction orderLocationTransaction = receiptDetail.OrderLocationTransaction;
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;

            PlannedBill plannedBill = null;
            bool isCS = (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING);
            bool isReceiveSettle = (orderDetail.DefaultBillSettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT);

            #region 寄售库存入库，记录Planned Bill
            if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
            {
                #region 记录待结算
                plannedBill = this.plannedBillMgr.CreatePlannedBill(receiptDetail, user);
                #endregion

                #region 委外加工立即结算
                if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
                {
                    plannedBill.CurrentActingQty = plannedBill.PlannedQty;
                    this.billMgr.CreateActingBill(plannedBill, user);
                    return null;  //委外加工不增加库存，不产生库存事务
                }
                #endregion

                #region 判断寄售负库存是否立即结算
                bool autoSettleMinusCSInventory = true;
                if (autoSettleMinusCSInventory && isCS &&
                    (receiptDetail.ReceivedQty.HasValue ? receiptDetail.ReceivedQty.Value : 0) < 0)
                {
                    plannedBill.CurrentActingQty = plannedBill.PlannedQty;
                    this.billMgr.CreateActingBill(plannedBill, user);
                    isCS = false;
                }
                #endregion

                #region 判断目的库位为空的立即结算
                if (isCS && orderLocationTransaction.Location == null && isReceiveSettle)
                {
                    plannedBill.CurrentActingQty = plannedBill.PlannedQty;
                    this.billMgr.CreateActingBill(plannedBill, user);
                    isCS = false;
                }
                #endregion
            }
            #endregion

            #region 库存操作
            if (orderLocationTransaction.Location != null)
            {
                #region 收货LotNo不能小于现有库存的LotNo校验
                if (orderHead.IsGoodsReceiveFIFO && receiptDetail.HuId != null && receiptDetail.ReceivedQty.Value > 0) //退货不检验
                {
                    Hu hu = this.huMgr.CheckAndLoadHu(receiptDetail.HuId);

                    #region 如果收货单有小于本次收货明细的生产日期，取最小日期作为先进先出的基准日期
                    DateTime? minManufactureDate = null;

                    if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
                    {
                        foreach (ReceiptDetail rd in receipt.ReceiptDetails)
                        {
                            if (rd.HuId != null && rd.HuId != string.Empty)
                            {
                                Hu rdHu = this.huMgr.CheckAndLoadHu(rd.HuId);
                                if ((!minManufactureDate.HasValue || rdHu.ManufactureDate.CompareTo(minManufactureDate) < 0)
                                    && rdHu.ManufactureParty.Code == hu.ManufactureParty.Code
                                    && rdHu.Item.Code == hu.Item.Code)
                                {
                                    minManufactureDate = rdHu.ManufactureDate;
                                }
                            }
                        }
                    }
                    #endregion


                    if (!minManufactureDate.HasValue  //本次收货为第一次收货需要检验
                        || minManufactureDate.Value.CompareTo(hu.ManufactureDate) > 0)   //收货的最小生产日期大于等于本次收货明细的生产日期才需要校验，也就是本次收货的生产日期为当前最小值
                    {
                        if (!this.locationLotDetailMgr.CheckGoodsReceiveFIFO(orderLocationTransaction.Location.Code,
                            orderLocationTransaction.Item.Code, hu.ManufactureDate, hu.ManufactureParty.Code, minManufactureDate))
                        {
                            throw new BusinessErrorException("MasterData.InventoryIn.LotNoIsOld",
                                orderLocationTransaction.Item.Code,
                                hu.HuId,
                                hu.LotNo,
                                orderLocationTransaction.Location.Code);
                        }
                    }
                }
                #endregion

                if (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    #region 物流
                    IList<InventoryTransaction> resultInventoryTransactionList = new List<InventoryTransaction>();

                    #region 正品库存操作
                    if (receiptDetail.ReceivedQty.HasValue && receiptDetail.ReceivedQty.Value != 0)
                    {
                        Location locIn = orderLocationTransaction.Location; //默认入库位

                        #region 处理按Hu退货，如果Hu在次品库位，出库库位更新为次品库位
                        if (receiptDetail.HuId != null && receiptDetail.HuId.Trim() != string.Empty
                            && orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(BusinessConstants.SYSTEM_LOCATION_REJECT, receiptDetail.HuId);

                            if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                            {
                                locIn = this.GetRejectLocation();
                            }
                        }
                        #endregion

                        #region 更新库存
                        IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                            orderLocationTransaction.Item,
                            locIn,
                            receiptDetail.HuId,
                            receiptDetail.LotNo,
                            receiptDetail.ReceivedQty.Value * orderLocationTransaction.UnitQty,
                            isCS ? true : receiptDetail.IsConsignment,                     //可能是寄售移库
                            plannedBill != null ? plannedBill : (receiptDetail.PlannedBill.HasValue ? this.plannedBillMgr.LoadPlannedBill(receiptDetail.PlannedBill.Value) : null),  //需要从ASNDetail上找到寄售信息
                            orderLocationTransaction.TransactionType,
                            orderHead.ReferenceOrderNo,
                            //isReceiveSettle,
                            user,
                            orderDetail.NeedInspection && orderHead.NeedInspection,
                            false,
                            locIn.Code == BusinessConstants.SYSTEM_LOCATION_REJECT ? orderLocationTransaction.Location.Code : null
                            );
                        #endregion

                        IListHelper.AddRange<InventoryTransaction>(resultInventoryTransactionList, inventoryTransactionList);

                        #region 记录库存事务
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            #region 采购发生退货，立即结算
                            if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                                && inventoryTransaction.Qty < 0 && inventoryTransaction.IsConsignment && inventoryTransaction.PlannedBill.HasValue)
                            {
                                PlannedBill pb = this.plannedBillMgr.LoadPlannedBill(inventoryTransaction.PlannedBill.Value);
                                pb.CurrentActingQty = 0 - inventoryTransaction.Qty;
                                this.billMgr.CreateActingBill(pb, user);
                            }
                            #endregion

                            this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, receipt, user);
                        }
                        #endregion
                    }
                    #endregion

                    #region 次品库存操作
                    if (receiptDetail.RejectedQty.HasValue && receiptDetail.RejectedQty.Value > 0)
                    {
                        #region 更新库存
                        IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                            orderLocationTransaction.Item,
                            (orderLocationTransaction.RejectLocation != null ? orderLocationTransaction.RejectLocation : orderLocationTransaction.Location),
                            receiptDetail.HuId,
                            receiptDetail.LotNo,
                            receiptDetail.RejectedQty.Value * orderLocationTransaction.UnitQty,
                            isCS,
                            plannedBill,
                            orderLocationTransaction.TransactionType,
                            orderHead.ReferenceOrderNo,
                            //isReceiveSettle,
                            user,
                            false,
                            false,
                            orderLocationTransaction.Location.Code
                            );
                        #endregion

                        IListHelper.AddRange<InventoryTransaction>(resultInventoryTransactionList, inventoryTransactionList);

                        #region 记录库存事务
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, receipt, user);
                        }
                        #endregion
                    }
                    #endregion

                    return resultInventoryTransactionList;
                    #endregion
                }
                else
                {
                    #region 生产
                    IList<InventoryTransaction> resultInventoryTransactionList = new List<InventoryTransaction>();

                    #region 正品库存操作
                    if (receiptDetail.ReceivedQty.HasValue && receiptDetail.ReceivedQty.Value != 0)
                    {
                        #region 更新库存
                        IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                            orderLocationTransaction.Item,
                            orderLocationTransaction.Location,
                            receiptDetail.HuId,
                            receiptDetail.LotNo,
                            receiptDetail.ReceivedQty.Value * orderLocationTransaction.UnitQty,
                            false,
                            null,
                            orderLocationTransaction.TransactionType,
                            orderHead.ReferenceOrderNo,
                            //isReceiveSettle,
                            user,
                            orderDetail.NeedInspection && orderHead.NeedInspection,
                            false,
                            null
                            );
                        #endregion

                        IListHelper.AddRange<InventoryTransaction>(resultInventoryTransactionList, inventoryTransactionList);

                        #region 记录库存事务
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, receipt, user);
                        }
                        #endregion
                    }
                    #endregion

                    #region 次品库存操作
                    if (receiptDetail.RejectedQty.HasValue && receiptDetail.RejectedQty.Value > 0)
                    {
                        #region 更新库存
                        IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                            orderLocationTransaction.Item,
                            (orderLocationTransaction.RejectLocation != null ? orderLocationTransaction.RejectLocation : orderLocationTransaction.Location),
                            receiptDetail.HuId,
                            receiptDetail.LotNo,
                            receiptDetail.RejectedQty.Value * orderLocationTransaction.UnitQty,
                            false,
                            null,
                            orderLocationTransaction.TransactionType,
                            orderHead.ReferenceOrderNo,
                            //isReceiveSettle,
                            user,
                            false,
                            false,
                            null
                            );
                        #endregion

                        IListHelper.AddRange<InventoryTransaction>(resultInventoryTransactionList, inventoryTransactionList);

                        #region 记录库存事务
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, receipt, user);
                        }
                        #endregion
                    }
                    #endregion

                    return resultInventoryTransactionList;
                    #endregion
                }
            }
            else
            {
                #region 入库没有库位关闭Hu
                if (receiptDetail.HuId != null && receiptDetail.HuId.Trim() != string.Empty)
                {
                    Hu hu = this.huMgr.CheckAndLoadHu(receiptDetail.HuId);
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CLOSE;
                    this.huMgr.UpdateHu(hu);
                }
                #endregion
            }

            return null;
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryIn(ReceiptDetail receiptDetail, User user, string binCode)
        {
            IList<InventoryTransaction> inventoryTransactionList = InventoryIn(receiptDetail, user);

            if (binCode != null && binCode != string.Empty && inventoryTransactionList != null && inventoryTransactionList.Count > 0)
            {
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                    locationLotDetail.NewStorageBin = this.storageBinMgr.LoadStorageBin(binCode);
                    inventoryTransaction.StorageBin = locationLotDetail.NewStorageBin;

                    InventoryPut(locationLotDetail, user);
                }
            }

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryIn(Receipt receipt, User user)
        {
            if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
            {
                IList<InventoryTransaction> resultInventoryTransactionList = new List<InventoryTransaction>();

                foreach (ReceiptDetail receiptDetail in receipt.ReceiptDetails)
                {
                    IList<InventoryTransaction> inventoryTransactionList = InventoryIn(receiptDetail, user);

                    IListHelper.AddRange<InventoryTransaction>(resultInventoryTransactionList, inventoryTransactionList);
                }

                return resultInventoryTransactionList;
            }

            return null;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryIn(MiscOrderDetail miscOrderDetail, User user)
        {
            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                miscOrderDetail.Item,
                miscOrderDetail.MiscOrder.Location,
                miscOrderDetail.HuId,
                miscOrderDetail.LotNo,
                miscOrderDetail.Qty,
                false,
                null,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP,
                null,
                //true,
                user,
                false,
                false,
                null
                );
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(miscOrderDetail, inventoryTransaction, user);
            }

            return inventoryTransactionList;
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryIn(MiscOrderDetail miscOrderDetail, User user, string binCode)
        {
            IList<InventoryTransaction> inventoryTransactionList = InventoryIn(miscOrderDetail, user);

            if (binCode != null && binCode != string.Empty && inventoryTransactionList != null && inventoryTransactionList.Count > 0)
            {
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                    locationLotDetail.NewStorageBin = this.storageBinMgr.LoadStorageBin(binCode);
                    inventoryTransaction.StorageBin = locationLotDetail.NewStorageBin;

                    InventoryPut(locationLotDetail, user);
                }
            }

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryIn(ProductLineInProcessLocationDetail productLineInProcessLocationDetail, User user)
        {
            Hu hu = null;
            if (productLineInProcessLocationDetail.HuId != null && productLineInProcessLocationDetail.HuId.Trim() != string.Empty)
            {
                hu = this.huMgr.CheckAndLoadHu(productLineInProcessLocationDetail.HuId);
            }

            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                productLineInProcessLocationDetail.Item,
                productLineInProcessLocationDetail.LocationFrom,
                hu != null ? hu.HuId : null,
                hu != null ? hu.LotNo : null,
                productLineInProcessLocationDetail.Qty,
                false,
                null,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR,
                null,
                //true,
                user,
                false,
                false,
                null
                );
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR, user);
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryPick(LocationLotDetail locationLotDetail, User user)
        {
            LocationLotDetail oldLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(locationLotDetail.Id);

            #region 检验下架库格
            if (oldLocationLotDetail.StorageBin == null)
            {
                throw new BusinessErrorException("Location.Error.PickUp.NotInBin", oldLocationLotDetail.Hu.HuId);
            }
            #endregion

            #region 记录出库事务
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_PICK, user);
            #endregion

            #region 下架
            oldLocationLotDetail.StorageBin = null;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            #region 记录入库事务
            InventoryTransaction inventoryInTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryInTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PICK, user);
            #endregion

            #region 更新Hu库位
            Hu hu = this.huMgr.LoadHu(oldLocationLotDetail.Hu.HuId);
            hu.StorageBin = null;
            this.huMgr.UpdateHu(hu);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryPick(IList<LocationLotDetail> locationLotDetailList, User user)
        {
            if (locationLotDetailList != null && locationLotDetailList.Count > 0)
            {
                foreach (LocationLotDetail locationLotDetail in locationLotDetailList)
                {
                    this.InventoryPick(locationLotDetail, user);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryPick(IList<LocationLotDetail> locationLotDetailList, string userCode)
        {
            User user = userMgr.LoadUser(userCode);
            InventoryPick(locationLotDetailList, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryPut(LocationLotDetail locationLotDetail, User user)
        {
            LocationLotDetail oldLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(locationLotDetail.Id);

            #region 检验上架库格
            if (oldLocationLotDetail.StorageBin != null)
            {
                if (locationLotDetail.NewStorageBin != null
                    && oldLocationLotDetail.StorageBin.Code == locationLotDetail.NewStorageBin.Code)
                {
                    throw new BusinessErrorException("Location.Error.PutAway.AlreadyInBin", oldLocationLotDetail.Hu.HuId, oldLocationLotDetail.StorageBin.Code);
                }

                //先做下架
                this.InventoryPick(oldLocationLotDetail, user);
            }

            if (locationLotDetail.NewStorageBin == null)
            {
                throw new BusinessErrorException("Location.Error.PutAway.BinEmpty");
            }

            if (locationLotDetail.NewStorageBin.Area.Location.Code != oldLocationLotDetail.Location.Code)
            {
                throw new BusinessErrorException("Location.Error.PutAway.BinNotInLocation", locationLotDetail.NewStorageBin.Code, oldLocationLotDetail.Location.Code);
            }
            #endregion

            #region 记录出库事务
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_PUT, user);
            #endregion

            #region 上架
            oldLocationLotDetail.StorageBin = locationLotDetail.NewStorageBin;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            #region 记录入库事务
            InventoryTransaction inventoryInTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryInTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PUT, user);
            #endregion

            #region 更新Hu库位
            Hu hu = this.huMgr.LoadHu(oldLocationLotDetail.Hu.HuId);
            hu.StorageBin = locationLotDetail.NewStorageBin.Code;
            this.huMgr.UpdateHu(hu);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryPut(IList<LocationLotDetail> locationLotDetailList, string userCode)
        {
            User user = userMgr.LoadUser(userCode);
            InventoryPut(locationLotDetailList, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryPut(IList<LocationLotDetail> locationLotDetailList, User user)
        {
            if (locationLotDetailList != null && locationLotDetailList.Count > 0)
            {
                foreach (LocationLotDetail locationLotDetail in locationLotDetailList)
                {
                    this.InventoryPut(locationLotDetail, user);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void InventoryRepackIn(RepackDetail repackDetail, User user)
        {
            LocationLotDetail oldLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(repackDetail.LocationLotDetail.Id);

            if (oldLocationLotDetail.Qty == 0)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.ZeroInRepackDetailQty", oldLocationLotDetail.Hu.HuId);
            }
            else if (repackDetail.Qty > oldLocationLotDetail.Qty)
            {
                throw new BusinessErrorException("MasterData.Inventory.Repack.Error.LocationQtyLessThanInRepackDetailQty", oldLocationLotDetail.Hu.HuId);
            }

            #region 下架
            if (oldLocationLotDetail.StorageBin != null)
            {
                this.InventoryPick(oldLocationLotDetail, user);
            }
            #endregion

            #region 结算，只有翻箱立即结算
            if (oldLocationLotDetail.IsConsignment == true
                && oldLocationLotDetail.PlannedBill.HasValue
                && repackDetail.Repack.Type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
            {
                PlannedBill pb = this.plannedBillMgr.LoadPlannedBill(oldLocationLotDetail.PlannedBill.Value);
                pb.CurrentActingQty = repackDetail.Qty / pb.UnitQty;
                this.billMgr.CreateActingBill(pb, user);
            }
            #endregion

            #region 出库
            oldLocationLotDetail.Qty -= repackDetail.Qty;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            #region Hu关闭
            if (oldLocationLotDetail.Hu != null)
            {
                Hu hu = this.huMgr.LoadHu(oldLocationLotDetail.Hu.HuId);
                hu.Location = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CLOSE;

                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region 记录翻箱事务
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - repackDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_REPACK, user);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public InventoryTransaction InventoryRepackOut(RepackDetail repackDetail, Location location, int? plannedBillId, User user)
        {
            #region 更新Hu上的OrderNo
            Hu hu = null;
            if (repackDetail.Hu != null)
            {
                hu = this.huMgr.CheckAndLoadHu(repackDetail.Hu.HuId);

                if (repackDetail.Repack.Type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
                {
                    if (hu.OrderNo != null)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Repack.Error.OrderNoIsNotEmpty", hu.HuId);
                    }
                    else
                    {
                        hu.OrderNo = repackDetail.Repack.RepackNo;
                        this.huMgr.UpdateHu(hu);
                    }
                }
            }
            #endregion

            #region 入库
            PlannedBill plannedBill = plannedBillId.HasValue ? this.plannedBillMgr.LoadPlannedBill(plannedBillId.Value) : null;
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                hu != null ? hu.Item : this.itemMgr.CheckAndLoadItem(repackDetail.itemCode),
                location,
                hu != null ? hu.HuId : null,
                hu != null ? hu.LotNo : null,
                repackDetail.Qty,     //库存单位
                plannedBill != null,
                plannedBill,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_REPACK,
                null,
                //true,
                user,
                false,
                false,
                null
                );
            #endregion

            #region 记录翻箱事务
            LocationLotDetail inLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransactionList[0].LocationLotDetailId);

            InventoryTransaction inventoryInTransaction = InventoryTransactionHelper.CreateInventoryTransaction(inLocationLotDetail, repackDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryInTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_REPACK, user);
            #endregion

            #region 上架
            if (hu != null && repackDetail.StorageBinCode != null && repackDetail.StorageBinCode != string.Empty)
            {
                inLocationLotDetail.NewStorageBin = this.storageBinMgr.CheckAndLoadStorageBin(repackDetail.StorageBinCode);
                this.InventoryPut(inLocationLotDetail, user);
            }
            #endregion

            return inventoryInTransaction;
        }

        [Transaction(TransactionMode.Requires)]
        public InventoryTransaction InspectOut(LocationLotDetail locationLotDetail, User user, bool needSettle, string inspectNo, Location locationTo)
        {
            LocationLotDetail oldLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(locationLotDetail.Id);

            if (oldLocationLotDetail.Qty < locationLotDetail.CurrentInspectQty)
            {
                throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.NotEnoughInventory", locationLotDetail.Item.Code);
            }

            #region 下架
            if (oldLocationLotDetail.StorageBin != null)
            {
                this.InventoryPick(oldLocationLotDetail, user);
            }
            #endregion

            #region 出库
            oldLocationLotDetail.Qty -= locationLotDetail.CurrentInspectQty;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            //检验结算
            if (needSettle && locationLotDetail.IsConsignment && locationLotDetail.PlannedBill.HasValue)
            {
                PlannedBill plannedBill = this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value);
                if (plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION)
                {
                    plannedBill.CurrentActingQty = locationLotDetail.CurrentInspectQty/plannedBill.UnitQty;
                    this.billMgr.CreateActingBill(plannedBill, locationLotDetail, user);
                }
            }

            #region 记录出库事务
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - locationLotDetail.CurrentInspectQty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP, user, inspectNo, locationTo);
            #endregion

            return inventoryOutTransaction;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InspectOut(Location location, Item item, decimal qty, User user, string inspectNo, Location locationTo)
        {
            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                item,
                location,
                null,
                null,
                0 - qty,
                false,
                null,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP,
                null,
                user,
                false,
                false,
                location.Code
                );
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP, user, inspectNo, locationTo);
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InspectIn(LocationLotDetail locationLotDetail, Location locIn, User user, bool needSettle, string inspectNo, string inrNo, int? inspectDetailId)
        {
            return InspectIn(locationLotDetail, locIn, null, user, needSettle, inspectNo, inrNo,inspectDetailId);
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InspectIn(LocationLotDetail locationLotDetail, Location locIn, StorageBin bin, User user, bool needSettle, string inspectNo, string inrNo, int? inspectDetailId)
        {
            bool isBillSettled = false;  //是否已经检验结算
            if (needSettle && locationLotDetail.IsConsignment && locationLotDetail.PlannedBill.HasValue)
            {
                PlannedBill plannedBill = this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value);
                if (plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION)
                {
                    isBillSettled = true;
                }
            }

            #region 入库
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                    locationLotDetail.Item,
                    locIn,
                //入不合格品库位，需要按数量
                    locIn.Code != BusinessConstants.SYSTEM_LOCATION_REJECT && locationLotDetail.Hu != null ? locationLotDetail.Hu.HuId : null,
                    locationLotDetail.LotNo,
                    locationLotDetail.CurrentInspectQty,     //库存单位
                    isBillSettled ? false : locationLotDetail.IsConsignment,   //已经检验结算直接记为非寄售库存
                    isBillSettled ? null : (locationLotDetail.PlannedBill.HasValue ? this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value) : null),      //已经检验结算直接记为非寄售库存
                    BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP,
                    null,
                //false,
                    user,
                    false,
                    false,
                    (locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT && locIn.Code == BusinessConstants.SYSTEM_LOCATION_REJECT
                    ? locationLotDetail.RefLocation : (locIn.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT ? locationLotDetail.Location.Code : null)) //出检验库位，如不合格品库位，需要把RefLoc带过去
                    );
            #endregion

            #region 记录库存事务
            if (inventoryTransactionList != null && inventoryTransactionList.Count > 0)
            {
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP, user, inspectNo, locationLotDetail.Location, null, inrNo,inspectDetailId);
                }
            }
            #endregion

            #region 上架
            if (bin != null)
            {
                LocationLotDetail oldLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransactionList[0].LocationLotDetailId);
                this.InventoryPut(oldLocationLotDetail, user);
            }
            #endregion

            #region 更新条码状态和库位
            if (locationLotDetail.Hu != null)
            {
                Hu hu = this.huMgr.LoadHu(locationLotDetail.Hu.HuId);
                if (locIn.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
                {
                    hu.Location = null;
                    hu.Qty = 0;
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CLOSE;
                }
                else
                {
                    if (locIn != null)
                    {
                        hu.Location = locIn.Code;
                    }
                    hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INVENTORY;
                }
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InspectIn(Item item, decimal qty, User user, string inspectNo, int? plannedBillId, Location locationFrom)
        {
            #region 更新库存
            PlannedBill plannedBill = plannedBillId.HasValue ? this.plannedBillMgr.LoadPlannedBill(plannedBillId.Value) : null;
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                item,
                this.GetInspectLocation(),
                null,
                null,
                qty,
                plannedBill != null,
                plannedBill,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP,
                null,
                //true,
                user,
                false,
                false,
                locationFrom.Code
                );
            #endregion

            #region 记录库存事务
            if (inventoryTransactionList != null && inventoryTransactionList.Count > 0)
            {
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP, user, inspectNo, locationFrom);
                }
            }
            #endregion

            #region 更新条码状态和库位
            if (inventoryTransactionList[0].Hu != null)
            {
                Hu hu = this.huMgr.LoadHu(inventoryTransactionList[0].Hu.HuId);
                hu.Location = BusinessConstants.SYSTEM_LOCATION_INSPECT;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INVENTORY;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            return inventoryTransactionList;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InventoryAdjust(CycleCountResult cycleCountResult, User user)
        {
            #region 更新库存
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                cycleCountResult.Item,
                cycleCountResult.CycleCount.Location,
                cycleCountResult.HuId != null ? cycleCountResult.HuId : null,
                cycleCountResult.HuId != null ? cycleCountResult.LotNo : null,
                cycleCountResult.DiffQty,
                false,
                null,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT,
                null,
                //true,
                user,
                false,
                false,
                null
                );
            #endregion

            #region 记录库存事务
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT, user, cycleCountResult.CycleCount.Code);

                if (cycleCountResult.StorageBin != null)
                {
                    LocationLotDetail locationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                    if (locationLotDetail.StorageBin == null || locationLotDetail.StorageBin.Code != cycleCountResult.StorageBin)
                    {
                        locationLotDetail.NewStorageBin = this.storageBinMgr.LoadStorageBin(cycleCountResult.StorageBin);
                        this.InventoryPut(locationLotDetail, user);
                    }
                }
            }
            #endregion

            #region 更新条码状态
            if (cycleCountResult.HuId != null && cycleCountResult.HuId.Trim() != string.Empty && cycleCountResult.DiffQty < 0)
            {
                Hu hu = this.huMgr.LoadHu(cycleCountResult.HuId);
                hu.Location = null;
                hu.StorageBin = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INPROCESS;

                this.huMgr.UpdateHu(hu);
            }
            #endregion

            return inventoryTransactionList;
        }

        /**
         * transType 事物类型
         * loc 库位
         * effdateStart 事务生效日期
         * effDateEnd 事务生效日期至
         * createDateStart 事务创建时间
         * createDateEnd 事务创建时间至
         * 窗口时间  --去除
         * 窗口时间至 --去除
         * partyFrom 来源供应商/区域
         * partyTo 目的客户/区域
         * itemCode 零件号
         * orderNo 订单号
         * recNo 收货单
         * createUser 操作人
         * ipNo asn号
         * userCode 当前用户
         */
        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationTransaction> GetLocationTransaction(string[] transType,
           string[] loc, DateTime effdateStart, DateTime effDateEnd, DateTime createDateStart, DateTime createDateEnd,
           string partyFrom, string partyTo, string[] itemCode,
           string[] orderNo, string[] recNo, string createUser, string[] ipNo, string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            //事物类型
            addDisjunction(transType, "TransactionType", ref criteria);

            IList<Region> regionList = regionMgr.GetRegion(userCode);
            IList<String> list = new List<String>();
            foreach (string locId in loc)
            {
                Location location = this.LoadLocation(locId);
                if (regionList != null && regionList.Count > 0 && regionList.IndexOf(location.Region) < 0)
                    continue;
                list.Add(locId);
            }

            //库位
            addDisjunction(list, "Location", ref criteria);

            //事务生效日期
            if (effdateStart != null)
            {
                criteria.Add(Expression.Ge("EffectiveDate", effdateStart));
            }

            //事务生效日期至
            if (effDateEnd != null)
            {
                criteria.Add(Expression.Le("EffectiveDate", effDateEnd));
            }
            //事务创建时间
            if (createDateStart != null)
            {
                criteria.Add(Expression.Ge("CreateDate", createDateStart));
            }
            //事务创建时间至
            if (createDateEnd != null)
            {
                criteria.Add(Expression.Le("CreateDate", createDateEnd));
            }
            //来源供应商/区域
            if (partyFrom != null && partyTo.Length > 0)
            {
                criteria.Add(Expression.Eq("PartyFrom", partyFrom));
            }
            //目的客户/区域
            if (partyTo != null && partyTo.Length > 0)
            {
                criteria.Add(Expression.Eq("PartyTo", partyTo));
            }
            //零件号
            addDisjunction(itemCode, "Item", ref criteria);


            //订单号
            addDisjunction(orderNo, "OrderNo", ref criteria);

            //收货单recNo
            addDisjunction(recNo, "ReceiptNo", ref criteria);
            //操作人createUser
            if (createUser != null && createUser.Length > 0)
            {
                criteria.Add(Expression.Eq("CreateUser", createUser));
            }
            //asn号ipNo
            addDisjunction(ipNo, "IpNo", ref criteria);

            //库位排序
            criteria.AddOrder(Order.Asc("Location"));

            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        public bool IsHuOcuppyByPickList(string huId)
        {
            DetachedCriteria criteria = DetachedCriteria.For<PickListResult>();
            criteria.SetProjection(Projections.Count("Id"));

            criteria.CreateAlias("PickListDetail", "pld");
            criteria.CreateAlias("pld.PickList", "pl");
            criteria.CreateAlias("LocationLotDetail", "lld");
            criteria.CreateAlias("lld.Hu", "hu");

            criteria.Add(Expression.Eq("pl.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
            criteria.Add(Expression.Eq("hu.HuId", huId));

            int count = this.criteriaMgr.FindAll<int>(criteria)[0];

            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Private Methods
        private void addDisjunction(IList<string> values, string columnLabel, ref DetachedCriteria criteria)
        {
            if (values != null && values.Count > 0)
            {
                Disjunction dis = Expression.Disjunction();
                foreach (String value in values)
                {
                    dis.Add(Expression.Eq(columnLabel, value));
                }
                criteria.Add(dis);
            }
        }

        private void addDisjunction(String[] values, String columnLabel, ref DetachedCriteria criteria)
        {
            if (values != null && values.Length > 0)
            {
                Disjunction dis = Expression.Disjunction();
                foreach (String value in values)
                {
                    dis.Add(Expression.Eq(columnLabel, value));
                }
                criteria.Add(dis);
            }
        }

        private IList<InventoryTransaction> RecordInventory(Item item, Location location, string huId, string lotNo, decimal qty, bool isCS, PlannedBill plannedBill, string transType, string refOrderNo, User user, bool needInspection, bool flushbackIgnoreHu, string refLoc)
        {
            IList<InventoryTransaction> inventoryTransactionList = new List<InventoryTransaction>();

            if (huId != null && huId.Trim() != string.Empty)
            {
                #region 有Hu
                //寄售/非寄售处理逻辑相同
                //不支持对已有HU做计划外入库
                if (qty > 0)
                {
                    #region 入库数量 > 0
                    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(location.Code, huId);
                    if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                    {
                        //库存中已经存在相同的Hu
                        throw new BusinessErrorException("Hu.Error.HuIdAlreadyExist", huId, location.Code);
                    }
                    else
                    {
                        CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, plannedBill, inventoryTransactionList, user, refLoc);
                    }
                    #endregion
                }
                else if (qty < 0)
                {
                    #region 入库数量 < 0 / 出库
                    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(location.Code, huId);
                    if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                    {
                        LocationLotDetail locationLotDetail = locationLotDetailList[0];  //如果查询到记录，只可能有一条

                        ////检查出库的Hu是否被拣货单占用
                        //if (this.IsHuOcuppyByPickList(huId))
                        //{
                        //    throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", huId);
                        //}

                        if (locationLotDetail.Qty + qty < 0)
                        {
                            //Hu中Item的数量小于出库数
                            throw new BusinessErrorException("Hu.Error.NoEnoughInventory", huId, location.Code, item.Code);
                        }

                        bool isBillSettled = false;
                        if (locationLotDetail.IsConsignment && locationLotDetail.PlannedBill.HasValue)
                        {
                            PlannedBill pb = this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value);
                            if (
                                //(pb.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING   //上线结算条件，发生移库出库事务才结算，可以避免收货检验时入检验库位就结算
                                //&& (transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR
                                //    || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN))
                            //|| 
                            (pb.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING   //下线结算条件
                                && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO)
                                //|| (locationLotDetail.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //检验结算条件，从检验库位出库并且ISS-INP事务
                                //    && locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT
                                //    && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP)
                                //|| (locationLotDetail.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //检验结算，从不合格品库位出库
                                //    && locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP                                   //发生ISS_UNP或者CYC_CNT事务强行结算
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO
                            || transType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT))
                            {
                                //结算
                                //寄售库存需要进行结算，对被回冲的库存进行负数结算
                                pb.CurrentActingQty = (0 - qty) / pb.UnitQty; //按负数结算
                                this.billMgr.CreateActingBill(pb, locationLotDetail, user);
                                isBillSettled = true;
                            }
                        }

                        //记录被回冲的记录
                        inventoryTransactionList.Add(InventoryTransactionHelper.CreateInventoryTransaction(locationLotDetail, qty, isBillSettled));

                        //更新库存数量
                        locationLotDetail.Qty += qty;
                        this.locationLotDetailMgr.UpdateLocationLotDetail(locationLotDetail);
                    }
                    else
                    {
                        //没有找到指定的HU
                        throw new BusinessErrorException("Hu.Error.NoEnoughInventory", huId, location.Code, item.Code);
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region 没有Hu
                if (isCS)
                {
                    #region 寄售处理。
                    if (qty > 0)
                    {
                        #region 入库数量 > 0

                        //如果是收货后检验或者收货结算，不回冲库存。
                        if (!(needInspection
                            || plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT))
                        {
                            //#region 回冲指定收货单号的寄售库存
                            //if (refOrderNo != null && refOrderNo.Trim() != string.Empty)
                            //{
                            //    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.MINUS_INVENTORY, false, refOrderNo, flushbackIgnoreHu);
                            //    BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                            //}
                            //#endregion

                            #region 回冲寄售库存
                            if (qty > 0)
                            {
                                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.MINUS_INVENTORY, false, flushbackIgnoreHu);
                                BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                            }
                            #endregion

                            #region 回冲非寄售库存
                            if (qty > 0)
                            {
                                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.MINUS_INVENTORY, false, flushbackIgnoreHu);
                                BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                            }
                            #endregion
                        }

                        #region 记录库存
                        if (qty > 0)
                        {
                            CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, plannedBill, inventoryTransactionList, user, refLoc);
                        }
                        #endregion
                        #endregion
                    }
                    else if (qty < 0)
                    {
                        #region 入库数量 < 0

                        //#region 回冲指定收货单号的寄售库存
                        //if (refOrderNo != null && refOrderNo.Trim() != string.Empty)
                        //{
                        //    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.PLUS_INVENTORY, false, refOrderNo, flushbackIgnoreHu);
                        //    BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                        //}
                        //#endregion

                        #region 回冲寄售库存
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                        }
                        #endregion

                        #region 回冲非寄售库存
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                        }
                        #endregion

                        #region 记录库存
                        if (qty < 0)
                        {
                            CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, plannedBill, inventoryTransactionList, user, refLoc);
                        }
                        #endregion
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 非寄售处理
                    if (qty > 0)
                    {
                        #region 入库数量 > 0

                        //收货后检验的不能回冲库存
                        if (!needInspection)
                        {
                            #region 回冲非寄售库存
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.MINUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, null, user, transType);
                            #endregion
                        }

                        #region 记录库存
                        if (qty > 0)
                        {
                            CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, null, inventoryTransactionList, user,refLoc);
                        }
                        #endregion
                        #endregion
                    }
                    else if (qty < 0)
                    {
                        #region 入库数量 < 0
                        #region 回冲非寄售库存
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, null, user, transType);
                        }
                        #endregion

                        #region 回冲寄售库存
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, null, user, transType);
                        }
                        #endregion

                        #region 记录库存
                        if (qty < 0)
                        {
                            CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, null, inventoryTransactionList, user,refLoc);
                        }
                        #endregion
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }

            return inventoryTransactionList;
        }

        private void BackFlushInventory(IList<LocationLotDetail> backFlushLocLotDetList, ref decimal qtyIn, IList<InventoryTransaction> inventoryTransactionList, PlannedBill plannedBill, User user, string transType)
        {
            if (backFlushLocLotDetList != null && backFlushLocLotDetList.Count > 0)
            {
                foreach (LocationLotDetail backFlushLocLotDet in backFlushLocLotDetList)
                {
                    PlannedBill backFlushPlannedBill = backFlushLocLotDet.IsConsignment ? this.plannedBillMgr.LoadPlannedBill(backFlushLocLotDet.PlannedBill.Value) : null;

                    #region 只有发生在按条码发货，按数量收货的情况，因为有些待回冲的库存已经被上一个条码冲掉，这里就不继续回冲了。
                    if (backFlushLocLotDet.Qty == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region 判断是否满足回冲条件
                    if (qtyIn == 0)
                    {
                        return;
                    }

                    //如果被回冲和入库的都是寄售库存并且是同一个供应商，一定要回冲
                    if (backFlushLocLotDet.IsConsignment && plannedBill != null
                        && backFlushPlannedBill.BillAddress.Code == plannedBill.BillAddress.Code)
                    {

                    }
                    else
                    {
                        //被回冲的寄售库存，和入库的寄售库存不是一个供应商，不能回冲
                        if (backFlushLocLotDet.IsConsignment && plannedBill != null
                            && backFlushPlannedBill.BillAddress.Code != plannedBill.BillAddress.Code)
                        {
                            return;
                        }

                        //被回冲的寄售库存的结算方式是上线结算，判断是否当前事务类型是否是ISS-*，不满足不能回冲
                        //if (backFlushLocLotDet.IsConsignment
                        //    && backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING)
                        //{
                        //    if (!transType.StartsWith("ISS-"))
                        //    {
                        //        return;
                        //    }
                        //}

                        //被回冲的寄售库存的结算方式是下线结算，判断是否当前事务类型是否是ISS-*并且不等于ISS-TR，不满足不能回冲
                        //if (backFlushLocLotDet.IsConsignment
                        //   && backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING)
                        //{
                        //    if (!(transType.StartsWith("ISS-") && transType != BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR))
                        //    {
                        //        return;
                        //    }
                        //}
                    }
                    #endregion

                    #region 回冲库存
                    decimal currentBFQty = 0; //本次回冲数
                    if (qtyIn > 0)
                    {
                        if (backFlushLocLotDet.Qty + qtyIn < 0)
                        {
                            //本次入库数 < 库存数量，全部回冲，回冲数量等于本次入库数
                            currentBFQty = qtyIn;
                        }
                        else
                        {
                            //本次入库数 >= 库存数量，按负的库存数回冲
                            currentBFQty = 0 - backFlushLocLotDet.Qty;
                        }
                    }
                    else
                    {
                        if (backFlushLocLotDet.Qty + qtyIn > 0)
                        {
                            //本次出库数 < 库存数量，全部回冲，回冲数量等于本次出库数
                            currentBFQty = qtyIn;
                        }
                        else
                        {
                            //本次出库数 >= 库存数量，按正的库存数回冲
                            currentBFQty = 0 - backFlushLocLotDet.Qty;
                        }
                    }

                    //更新库存数量
                    backFlushLocLotDet.Qty += currentBFQty;
                    this.locationLotDetailMgr.UpdateLocationLotDetail(backFlushLocLotDet);

                    #endregion

                    #region 结算
                    bool isBillSettled = false;
                    //只有出库(qtyIn < 0 && plannedBill == null)回冲寄售库存(backFlushLocLotDet.IsConsignment == true)才按照SettleTerm结算
                    //其它情况都是立即结算
                    if (qtyIn < 0 && plannedBill == null && backFlushLocLotDet.IsConsignment)
                    {
                        if (
                            //(backFlushPlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING   //上线结算条件，发生移库出库事务才结算，可以避免收货检验时入检验库位就结算
                            //&& (transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR
                            //        || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN)) 
                            //|| 
                            (backFlushPlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING   //下线结算条件
                                && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO)
                            //|| (backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //检验结算条件，从检验库位出库，并且发生ISS-INP事务
                            //    && backFlushLocLotDet.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT
                            //        && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP)
                            //|| (backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //检验结算，从不合格品库位出库，立即结算
                            //    && backFlushLocLotDet.Location.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP                              //如果发生ISS_UNP或者CYC_CNT事务，强行结算
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO
                            || transType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT))
                        {
                            //寄售库存需要进行结算，对被回冲的库存进行负数结算
                            backFlushPlannedBill.CurrentActingQty = (0 - currentBFQty) / backFlushPlannedBill.UnitQty; //按负数结算
                            this.billMgr.CreateActingBill(backFlushPlannedBill, backFlushLocLotDet, user);
                            isBillSettled = true;
                        }
                    }
                    else
                    {
                        if (backFlushLocLotDet.IsConsignment)
                        {
                            //寄售库存需要进行结算，对被回冲的库存进行负数结算
                            backFlushPlannedBill.CurrentActingQty = (0 - currentBFQty) / backFlushPlannedBill.UnitQty; //按负数结算
                            this.billMgr.CreateActingBill(backFlushPlannedBill, backFlushLocLotDet, user);
                            isBillSettled = true;
                        }

                        if (plannedBill != null)
                        {
                            //对入库的库存进行结算
                            plannedBill.CurrentActingQty = currentBFQty / plannedBill.UnitQty;  //按正数结算
                            this.billMgr.CreateActingBill(plannedBill, user);
                        }
                    }
                    #endregion

                    //记录被回冲的记录
                    inventoryTransactionList.Add(InventoryTransactionHelper.CreateInventoryTransaction(backFlushLocLotDet, currentBFQty, isBillSettled));

                    qtyIn -= currentBFQty;
                }
            }
        }

        private LocationLotDetail CreateNewLocationLotDetail(Item item, Location location, string huId, string lotNo, decimal qty, bool isCS, PlannedBill plannedBill, IList<InventoryTransaction> inventoryTransactionList, User user, string refLoc)
        {
            #region 是否允许负库存
            if (!location.AllowNegativeInventory && qty < 0)
            {
                throw new BusinessErrorException("Location.Error.NotAllowNegativeInventory", location.Code);
            }
            #endregion

            #region 非制造件&采购件不能做库存事务
            if (item.Type != BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_P && item.Type != BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_M)
            {
                throw new BusinessErrorException("Location.Error.ItemTypeNotValid", item.Type);
            }
            #endregion

            bool isBillSettled = false;
            #region 收货结算/入库结算
            if (isCS && (plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT
                        || (location.IsSettleConsignment
                            && (plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING
                                || plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING))))
            {
                plannedBill.CurrentActingQty = qty / plannedBill.UnitQty;
                this.billMgr.CreateActingBill(plannedBill, user);

                isCS = false;
                isBillSettled = true;
            }
            #endregion

            DateTime createDate = DateTime.Now;

            LocationLotDetail newLocationLotDetail = new LocationLotDetail();
            newLocationLotDetail.Item = item;
            newLocationLotDetail.Location = location;
            newLocationLotDetail.LotNo = lotNo;
            if (huId != null && huId != string.Empty)
            {
                huId = huId.ToUpper();
                newLocationLotDetail.Hu = this.huMgr.LoadHu(huId);

                //库存数量和条码上的数量有差异，更新条码上的数量
                if (newLocationLotDetail.Hu.Qty * newLocationLotDetail.Hu.UnitQty != qty)
                {
                    newLocationLotDetail.Hu.Qty = qty / newLocationLotDetail.Hu.UnitQty;
                }
                newLocationLotDetail.Hu.Location = location.Code;
                newLocationLotDetail.Hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INVENTORY;

                this.huMgr.UpdateHu(newLocationLotDetail.Hu);
            }
            newLocationLotDetail.CreateDate = createDate;
            newLocationLotDetail.Qty = qty;
            newLocationLotDetail.IsConsignment = isCS;
            if (plannedBill != null)
            {
                newLocationLotDetail.PlannedBill = plannedBill.Id;
            }

            newLocationLotDetail.RefLocation = refLoc;

            this.locationLotDetailMgr.CreateLocationLotDetail(newLocationLotDetail);
            inventoryTransactionList.Add(InventoryTransactionHelper.CreateInventoryTransaction(newLocationLotDetail, qty, isBillSettled));

            return newLocationLotDetail;
        }
        #endregion
    }
}