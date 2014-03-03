using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

using System.Linq;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class InspectOrderMgr : InspectOrderBaseMgr, IInspectOrderMgr
    {
        private INumberControlMgr numberControlMgr;
        private ILocationMgr locationMgr;
        private ILocationLotDetailMgr locationLotDetailMgr;
        private IInspectOrderDetailMgr inspectOrderDetailMgr;
        private IItemMgr itemMgr;
        private IUserMgr userMgr;
        private IBomDetailMgr bomDetailMgr;
        private IInspectResultMgr inspectResultMgr;

        private IFlowMgr flowMgr;

        public InspectOrderMgr(IInspectOrderDao entityDao,
            INumberControlMgr numberControlMgr,
            ILocationMgr locationMgr,
            ILocationLotDetailMgr locationLotDetailMgr,
            IInspectOrderDetailMgr inspectOrderDetailMgr,
            IItemMgr itemMgr,
            IUserMgr userMgr,
            IBomDetailMgr bomDetailMgr,
            IInspectResultMgr inspectResultMgr,
            IFlowMgr flowMgr)
            : base(entityDao)
        {
            this.numberControlMgr = numberControlMgr;
            this.locationMgr = locationMgr;
            this.locationLotDetailMgr = locationLotDetailMgr;
            this.inspectOrderDetailMgr = inspectOrderDetailMgr;
            this.itemMgr = itemMgr;
            this.userMgr = userMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.inspectResultMgr = inspectResultMgr;
            this.flowMgr = flowMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public InspectOrder CheckAndLoadInspectOrder(string inspectOrderNo)
        {
            return this.CheckAndLoadInspectOrder(inspectOrderNo, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public InspectOrder CheckAndLoadInspectOrder(string inspectOrderNo, bool includeDetail)
        {
            InspectOrder inspectOrder = this.LoadInspectOrder(inspectOrderNo);
            if (inspectOrder == null)
            {
                throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.InspectOrderNoNotExist", inspectOrderNo);
            }

            if (includeDetail && inspectOrder.InspectOrderDetails != null && inspectOrder.InspectOrderDetails.Count > 0)
            {
            }

            return inspectOrder;
        }



        [Transaction(TransactionMode.Requires)]
        public InspectOrder CreateInspectOrder(IList<LocationLotDetail> locationLotDetailList, User user)
        {
            return CreateInspectOrder(locationLotDetailList, user, null, null, true);
        }

        [Transaction(TransactionMode.Requires)]
        public InspectOrder CreateInspectOrder(IList<LocationLotDetail> locationLotDetailList, User user, string ipNo, string receiptNo, bool isSeperated)
        {
            IList<LocationLotDetail> noneZeroLocationLotDetailList = new List<LocationLotDetail>();

            bool? isDetailHasHu = null;
            string region = null;
            if (locationLotDetailList != null && locationLotDetailList.Count > 0)
            {
                foreach (LocationLotDetail locationLotDetail in locationLotDetailList)
                {
                    if (locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT
                        || locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.LocationFrom", locationLotDetail.Location.Code);
                    }

                    if (locationLotDetail.CurrentInspectQty > 0)
                    {
                        if (isDetailHasHu == null)
                        {
                            isDetailHasHu = (locationLotDetail.Hu != null);
                        }
                        else if (isDetailHasHu != (locationLotDetail.Hu != null))
                        {
                            throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.NotAllDetailHasHu");
                        }
                        noneZeroLocationLotDetailList.Add(locationLotDetail);
                    }

                    if (region == null)
                    {
                        region = locationLotDetail.Location.Region.Code;
                    }
                    else if (region != locationLotDetail.Location.Region.Code)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.RegionNotEqual");
                    }
                }
            }

            if (noneZeroLocationLotDetailList.Count == 0)
            {
                throw new BusinessErrorException("Order.Error.Inspection.DetailEmpty");
            }

            #region 创建检验单头
            DateTime dateTimeNow = DateTime.Now;
            InspectOrder inspectOrder = new InspectOrder();
            inspectOrder.InspectNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION);
            inspectOrder.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            inspectOrder.CreateUser = user;
            inspectOrder.CreateDate = dateTimeNow;
            inspectOrder.LastModifyUser = user;
            inspectOrder.LastModifyDate = dateTimeNow;
            inspectOrder.IsDetailHasHu = isDetailHasHu.Value;
            inspectOrder.IpNo = ipNo;
            inspectOrder.ReceiptNo = receiptNo;
            inspectOrder.IsSeperated = isSeperated;
            inspectOrder.Region = region;

            this.CreateInspectOrder(inspectOrder);
            #endregion

            #region 创建检验明细
            foreach (LocationLotDetail locationLotDetail in noneZeroLocationLotDetailList)
            {
                //零件出库
                this.locationMgr.InspectOut(locationLotDetail, user, false, inspectOrder.InspectNo, this.locationMgr.GetInspectLocation());

                //入待验库位
                IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InspectIn(locationLotDetail, this.locationMgr.GetInspectLocation(), user, false, inspectOrder.InspectNo, null, null);

                if (inventoryTransactionList != null && inventoryTransactionList.Count > 0)
                {
                    foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                    {
                        InspectOrderDetail inspectOrderDetail = new InspectOrderDetail();
                        inspectOrderDetail.InspectOrder = inspectOrder;
                        inspectOrderDetail.InspectQty = inventoryTransaction.Qty;
                        inspectOrderDetail.LocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                        inspectOrderDetail.LocationFrom = locationLotDetail.Location;
                        inspectOrderDetail.LocationTo = locationLotDetail.InspectQualifyLocation != null ? locationLotDetail.InspectQualifyLocation : locationLotDetail.Location;

                        this.inspectOrderDetailMgr.CreateInspectOrderDetail(inspectOrderDetail);

                        inspectOrder.AddInspectOrderDetail(inspectOrderDetail);
                    }
                }
            }
            #endregion

            return inspectOrder;
        }

        [Transaction(TransactionMode.Requires)]
        public InspectOrder CreateInspectOrder(Location location, IDictionary<string, decimal> itemQtyDic, User user)
        {
            return CreateInspectOrder(location.Code, itemQtyDic, user);
        }

        [Transaction(TransactionMode.Requires)]
        public InspectOrder CreateInspectOrder(string locationCode, IDictionary<string, decimal> itemQtyDic, User user)
        {
            if (locationCode == BusinessConstants.SYSTEM_LOCATION_INSPECT
                       || locationCode == BusinessConstants.SYSTEM_LOCATION_REJECT)
            {
                throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.LocationFrom", locationCode);
            }

            #region 创建检验单头
            DateTime dateTimeNow = DateTime.Now;
            InspectOrder inspectOrder = new InspectOrder();
            inspectOrder.InspectNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION);
            inspectOrder.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            inspectOrder.CreateUser = user;
            inspectOrder.CreateDate = dateTimeNow;
            inspectOrder.LastModifyUser = user;
            inspectOrder.LastModifyDate = dateTimeNow;
            inspectOrder.IsDetailHasHu = false;
            inspectOrder.IsSeperated = true;

            Location location = this.locationMgr.CheckAndLoadLocation(locationCode);
            inspectOrder.Region = location.Region.Code;

            this.CreateInspectOrder(inspectOrder);
            #endregion

            #region 创建检验明细
            if (itemQtyDic != null && itemQtyDic.Count > 0)
            {


                foreach (string itemCode in itemQtyDic.Keys)
                {
                    if (itemQtyDic[itemCode] == 0)
                    {
                        continue;
                    }

                    Item item = this.itemMgr.CheckAndLoadItem(itemCode);

                    //零件出库
                    IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InspectOut(location, item, itemQtyDic[itemCode], user, inspectOrder.InspectNo, this.locationMgr.GetInspectLocation());

                    //入待验库位
                    foreach (InventoryTransaction outInventoryTransaction in inventoryTransactionList)
                    {
                        IList<InventoryTransaction> inInventoryTransactionList = this.locationMgr.InspectIn(item, 0 - outInventoryTransaction.Qty, user, inspectOrder.InspectNo, outInventoryTransaction.PlannedBill, location);

                        if (inInventoryTransactionList != null && inInventoryTransactionList.Count > 0)
                        {
                            foreach (InventoryTransaction inInventoryTransaction in inInventoryTransactionList)
                            {
                                InspectOrderDetail inspectOrderDetail = new InspectOrderDetail();
                                inspectOrderDetail.InspectOrder = inspectOrder;
                                inspectOrderDetail.InspectQty = inInventoryTransaction.Qty;
                                inspectOrderDetail.LocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inInventoryTransaction.LocationLotDetailId);
                                inspectOrderDetail.LocationFrom = location;
                                inspectOrderDetail.LocationTo = location;


                                this.inspectOrderDetailMgr.CreateInspectOrderDetail(inspectOrderDetail);

                                inspectOrder.AddInspectOrderDetail(inspectOrderDetail);
                            }
                        }
                    }
                }
            }
            #endregion

            return inspectOrder;
        }

        [Transaction(TransactionMode.Requires)]
        public InspectOrder CreateFgInspectOrder(string locationCode, IDictionary<string, decimal> itemFgQtyDic, User user)
        {
            #region 创建检验单头
            DateTime dateTimeNow = DateTime.Now;
            InspectOrder inspectOrder = new InspectOrder();
            inspectOrder.InspectNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION);
            inspectOrder.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
            inspectOrder.CreateUser = user;
            inspectOrder.CreateDate = dateTimeNow;
            inspectOrder.LastModifyUser = user;
            inspectOrder.LastModifyDate = dateTimeNow;
            inspectOrder.IsDetailHasHu = false;
            inspectOrder.IsSeperated = true;
            inspectOrder.Region = this.locationMgr.CheckAndLoadLocation(locationCode).Region.Code;

            this.CreateInspectOrder(inspectOrder);
            #endregion

            #region 创建检验明细
            if (itemFgQtyDic != null && itemFgQtyDic.Count > 0)
            {
                Location location = this.locationMgr.CheckAndLoadLocation(locationCode);
                string itemCode = string.Empty;
                string fgCode = string.Empty;
                string defectClassification = string.Empty;
                string defectFactor = string.Empty;

                foreach (string itemFgCode in itemFgQtyDic.Keys)
                {
                    if (itemFgQtyDic[itemFgCode] == 0)
                    {
                        continue;
                    }
                    string[] itemFg = itemFgCode.Split('-');
                    itemCode = itemFg[0];
                    fgCode = itemFg[1];
                    defectClassification = itemFg[2];
                    defectFactor = itemFg[3];

                    Item item = this.itemMgr.CheckAndLoadItem(itemCode);

                    //零件出库
                    this.locationMgr.InspectOut(location, item, itemFgQtyDic[itemFgCode], user, inspectOrder.InspectNo, this.locationMgr.GetInspectLocation());

                    //入待验库位
                    IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InspectIn(item, itemFgQtyDic[itemFgCode], user, inspectOrder.InspectNo, null, location);

                    if (inventoryTransactionList != null && inventoryTransactionList.Count > 0)
                    {
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            InspectOrderDetail inspectOrderDetail = new InspectOrderDetail();
                            inspectOrderDetail.InspectOrder = inspectOrder;
                            inspectOrderDetail.InspectQty = inventoryTransaction.Qty;
                            inspectOrderDetail.LocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransaction.LocationLotDetailId);
                            inspectOrderDetail.LocationFrom = location;
                            inspectOrderDetail.LocationTo = location;
                            inspectOrderDetail.FinishGoods = this.itemMgr.LoadItem(fgCode);
                            inspectOrderDetail.DefectClassification = defectClassification;
                            inspectOrderDetail.DefectFactor = defectFactor;

                            this.inspectOrderDetailMgr.CreateInspectOrderDetail(inspectOrderDetail);

                            inspectOrder.AddInspectOrderDetail(inspectOrderDetail);
                        }
                    }
                }
            }
            #endregion

            return inspectOrder;
        }

        [Transaction(TransactionMode.Requires)]
        public void PendInspectOrder(IList<InspectOrderDetail> inspectOrderDetailList, string userCode)
        {
            PendInspectOrder(inspectOrderDetailList, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void PendInspectOrder(IList<InspectOrderDetail> inspectOrderDetailList, User user)
        {
            PendInspectOrder(inspectOrderDetailList, user, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void PendInspectOrder(IList<InspectOrderDetail> inspectOrderDetailList, User user, string rejNo)
        {
            #region 过滤检验数量为0的检验明细
            IList<InspectOrderDetail> noneZeroInspectOrderDetailList = new List<InspectOrderDetail>();

            if (inspectOrderDetailList != null && inspectOrderDetailList.Count > 0)
            {
                foreach (InspectOrderDetail inspectOrderDetail in inspectOrderDetailList)
                {
                    if (inspectOrderDetail.CurrentQualifiedQty > 0 || inspectOrderDetail.CurrentRejectedQty > 0)
                    {
                        noneZeroInspectOrderDetailList.Add(inspectOrderDetail);
                    }
                }
            }

            if (noneZeroInspectOrderDetailList.Count == 0)
            {
                throw new BusinessErrorException("Order.Error.Inspection.DetailEmpty");
            }
            #endregion

            #region 循环检验单明细
            IDictionary<string, InspectOrder> cachedInspectOrderDic = new Dictionary<string, InspectOrder>();
            string irNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION_RESULT);  //检验结果单号
            IList<InspectOrderDetail> pendingList = new List<InspectOrderDetail>();

            foreach (InspectOrderDetail inspectOrderDetail in noneZeroInspectOrderDetailList)
            {
                #region 缓存检验单头
                InspectOrder inspectOrder = inspectOrderDetail.InspectOrder;
                if (!cachedInspectOrderDic.ContainsKey(inspectOrder.InspectNo))
                {
                    cachedInspectOrderDic.Add(inspectOrder.InspectNo, inspectOrder);
                }
                #endregion

                #region 检验数量
                InspectOrderDetail oldInspectOrderDetail = this.inspectOrderDetailMgr.LoadInspectOrderDetail(inspectOrderDetail.Id);
                oldInspectOrderDetail.Disposition = inspectOrderDetail.Disposition;
                oldInspectOrderDetail.CurrentQualifiedQty = inspectOrderDetail.CurrentQualifiedQty;
                oldInspectOrderDetail.CurrentRejectedQty = inspectOrderDetail.CurrentRejectedQty;
                decimal totalInspectedQty = (oldInspectOrderDetail.QualifiedQty.HasValue ? oldInspectOrderDetail.QualifiedQty.Value : 0)
                                          + (oldInspectOrderDetail.RejectedQty.HasValue ? oldInspectOrderDetail.RejectedQty.Value : 0)
                                          + oldInspectOrderDetail.PendingQualifiedQty
                                          + oldInspectOrderDetail.PendingRejectedQty
                                          + oldInspectOrderDetail.CurrentQualifiedQty
                                          + oldInspectOrderDetail.CurrentRejectedQty;

                if (oldInspectOrderDetail.LocationLotDetail.Hu != null
                    && oldInspectOrderDetail.InspectQty != totalInspectedQty)
                {
                    //有条码的只能检验一次
                    throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.HuInspectQtyNotMatch", oldInspectOrderDetail.LocationLotDetail.Hu.HuId);
                }

                if (oldInspectOrderDetail.InspectQty < totalInspectedQty)
                {
                    throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.InspectQtyExcceed", oldInspectOrderDetail.LocationLotDetail.Item.Code);
                }
                #endregion

                #region 合格品
                if (oldInspectOrderDetail.CurrentQualifiedQty > 0)
                {
                    //更新待处理合格品数量
                    oldInspectOrderDetail.PendingQualifiedQty += oldInspectOrderDetail.CurrentQualifiedQty;
                }
                #endregion

                #region 不合格品
                if (oldInspectOrderDetail.CurrentRejectedQty > 0)
                {
                    //更新不合格品数量
                    oldInspectOrderDetail.PendingRejectedQty += oldInspectOrderDetail.CurrentRejectedQty;
                }
                #endregion

                #region 保存检验结果
                InspectResult inspectResult = new InspectResult();
                inspectResult.InspectResultNo = irNo;
                inspectResult.InspectOrderDetail = oldInspectOrderDetail;
                inspectResult.QualifiedQty = oldInspectOrderDetail.CurrentQualifiedQty;
                inspectResult.RejectedQty = oldInspectOrderDetail.CurrentRejectedQty;
                inspectResult.CreateDate = DateTime.Now;
                inspectResult.CreateUser = user;
                inspectResult.LastModifyDate = DateTime.Now;
                inspectResult.LastModifyUser = user;
                inspectResult.PrintCount = 0;
                inspectResult.IsPrinted = false;
                if (rejNo != null && rejNo != string.Empty)
                {
                    inspectResult.PrintNo = rejNo;
                    inspectResult.IsPrinted = true;
                }

                this.inspectResultMgr.CreateInspectResult(inspectResult);
                #endregion

                #region 更新检验单明细
                this.inspectOrderDetailMgr.UpdateInspectOrderDetail(oldInspectOrderDetail);
                #endregion

                #region 根据库位标记确认
                if (!oldInspectOrderDetail.LocationTo.IsAutoConfirm)
                {
                    oldInspectOrderDetail.CurrentQualifiedQty = 0;
                }
                if (!locationMgr.GetRejectLocation().IsAutoConfirm)
                {
                    oldInspectOrderDetail.CurrentRejectedQty = 0;
                }
                if (oldInspectOrderDetail.CurrentQualifiedQty > 0 || oldInspectOrderDetail.CurrentRejectedQty > 0)
                {
                    pendingList.Add(oldInspectOrderDetail);
                }
                #endregion
            }
            #endregion

            #region 将需要确认的自动确认
            if (pendingList != null && pendingList.Count > 0)
            {
                ProcessInspectOrder(pendingList, user);
            }
            #endregion

            #region 更新检验单
            DateTime dataTimeNow = DateTime.Now;
            foreach (InspectOrder oldInspectOrder in cachedInspectOrderDic.Values)
            {
                InspectOrder inspectOrder = this.LoadInspectOrder(oldInspectOrder.InspectNo);
                inspectOrder.LastModifyUser = user;
                inspectOrder.LastModifyDate = dataTimeNow;
                this.UpdateInspectOrder(inspectOrder);
            }
            #endregion


        }

        [Transaction(TransactionMode.Requires)]
        public void ProcessInspectOrder(IList<InspectOrderDetail> inspectOrderDetailList, string userCode)
        {
            ProcessInspectOrder(inspectOrderDetailList, userMgr.CheckAndLoadUser(userCode));
        }

        [Transaction(TransactionMode.Requires)]
        public void ProcessInspectOrder(IList<InspectOrderDetail> inspectOrderDetailList, User user)
        {
            #region 过滤检验数量为0的检验明细
            IList<InspectOrderDetail> noneZeroInspectOrderDetailList = new List<InspectOrderDetail>();

            if (inspectOrderDetailList != null && inspectOrderDetailList.Count > 0)
            {
                foreach (InspectOrderDetail inspectOrderDetail in inspectOrderDetailList)
                {
                    if (inspectOrderDetail.CurrentQualifiedQty > 0 || inspectOrderDetail.CurrentRejectedQty > 0)
                    {
                        noneZeroInspectOrderDetailList.Add(inspectOrderDetail);
                    }
                }
            }

            if (noneZeroInspectOrderDetailList.Count == 0)
            {
                throw new BusinessErrorException("Order.Error.Inspection.DetailEmpty");
            }
            #endregion

            #region 循环检验单明细
            IDictionary<string, InspectOrder> cachedInspectOrderDic = new Dictionary<string, InspectOrder>();
            //string inrNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION_RESULT);  //检验结果单号
            foreach (InspectOrderDetail inspectOrderDetail in noneZeroInspectOrderDetailList)
            {
                #region 缓存检验单头
                InspectOrder inspectOrder = inspectOrderDetail.InspectOrder;
                if (!cachedInspectOrderDic.ContainsKey(inspectOrder.InspectNo))
                {
                    cachedInspectOrderDic.Add(inspectOrder.InspectNo, inspectOrder);
                }
                #endregion

                #region 检验数量
                InspectOrderDetail oldInspectOrderDetail = this.inspectOrderDetailMgr.LoadInspectOrderDetail(inspectOrderDetail.Id);
                //oldInspectOrderDetail.Disposition = inspectOrderDetail.Disposition;
                oldInspectOrderDetail.CurrentQualifiedQty = inspectOrderDetail.CurrentQualifiedQty;
                oldInspectOrderDetail.CurrentRejectedQty = inspectOrderDetail.CurrentRejectedQty;

                if (oldInspectOrderDetail.LocationLotDetail.Hu != null)
                {
                    if (oldInspectOrderDetail.PendingQualifiedQty != inspectOrderDetail.CurrentQualifiedQty
                    && oldInspectOrderDetail.PendingRejectedQty != inspectOrderDetail.CurrentRejectedQty)
                    {
                        //有条码的只能检验一次
                        throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.HuInspectQtyNotMatch", oldInspectOrderDetail.LocationLotDetail.Hu.HuId);
                    }
                }
                else
                {
                    if (oldInspectOrderDetail.PendingQualifiedQty < inspectOrderDetail.CurrentQualifiedQty)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.QualifyQtyExcceed", oldInspectOrderDetail.LocationLotDetail.Item.Code);
                    }
                    if (oldInspectOrderDetail.PendingRejectedQty < inspectOrderDetail.CurrentRejectedQty)
                    {
                        throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.RejectQtyExcceed", oldInspectOrderDetail.LocationLotDetail.Item.Code);
                    }
                }
                #endregion

                #region 合格品
                if (oldInspectOrderDetail.CurrentQualifiedQty > 0)
                {
                    oldInspectOrderDetail.LocationLotDetail.CurrentInspectQty = oldInspectOrderDetail.CurrentQualifiedQty;

                    //出待验库位
                    this.locationMgr.InspectOut(oldInspectOrderDetail.LocationLotDetail, user, true, inspectOrder.InspectNo, oldInspectOrderDetail.LocationTo);

                    //入合格品库位
                    this.locationMgr.InspectIn(oldInspectOrderDetail.LocationLotDetail, oldInspectOrderDetail.LocationTo, oldInspectOrderDetail.CurrentStorageBin, user, true, inspectOrder.InspectNo, null, inspectOrderDetail.Id);

                    //更新合格品数量
                    if (!oldInspectOrderDetail.QualifiedQty.HasValue)
                    {
                        oldInspectOrderDetail.QualifiedQty = 0;
                    }
                    oldInspectOrderDetail.QualifiedQty += oldInspectOrderDetail.CurrentQualifiedQty;
                    oldInspectOrderDetail.PendingQualifiedQty -= oldInspectOrderDetail.CurrentQualifiedQty;

                }
                #endregion

                #region 不合格品
                if (oldInspectOrderDetail.CurrentRejectedQty > 0)
                {
                    oldInspectOrderDetail.LocationLotDetail.CurrentInspectQty = oldInspectOrderDetail.CurrentRejectedQty;

                    //出待验库位
                    this.locationMgr.InspectOut(oldInspectOrderDetail.LocationLotDetail, user, false, inspectOrder.InspectNo, this.locationMgr.GetRejectLocation());

                    //入不合格品库位
                    this.locationMgr.InspectIn(oldInspectOrderDetail.LocationLotDetail, this.locationMgr.GetRejectLocation(), user, false, inspectOrder.InspectNo, null, inspectOrderDetail.Id);

                    //更新不合格品数量
                    if (!oldInspectOrderDetail.RejectedQty.HasValue)
                    {
                        oldInspectOrderDetail.RejectedQty = 0;
                    }
                    oldInspectOrderDetail.RejectedQty += oldInspectOrderDetail.CurrentRejectedQty;
                    oldInspectOrderDetail.PendingRejectedQty -= oldInspectOrderDetail.CurrentRejectedQty;
                }
                #endregion

                #region 更新检验单明细
                this.inspectOrderDetailMgr.UpdateInspectOrderDetail(oldInspectOrderDetail);
                #endregion
            }
            #endregion

            #region 更新检验单
            DateTime dataTimeNow = DateTime.Now;
            foreach (InspectOrder oldInspectOrder in cachedInspectOrderDic.Values)
            {
                InspectOrder inspectOrder = this.LoadInspectOrder(oldInspectOrder.InspectNo);
                inspectOrder.LastModifyUser = user;
                inspectOrder.LastModifyDate = dataTimeNow;

                bool allClose = true;
                IList<InspectOrderDetail> detailList = inspectOrderDetailMgr.GetInspectOrderDetail(inspectOrder);
                foreach (InspectOrderDetail inspectOrderDetail in detailList)
                {
                    if (inspectOrderDetail.InspectQty !=
                        (inspectOrderDetail.QualifiedQty.HasValue ? inspectOrderDetail.QualifiedQty.Value : 0)
                        + (inspectOrderDetail.RejectedQty.HasValue ? inspectOrderDetail.RejectedQty.Value : 0))
                    {
                        allClose = false;
                        break;
                    }
                }

                if (allClose)
                {
                    inspectOrder.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE;
                }

                this.UpdateInspectOrder(inspectOrder);
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        public InspectOrder LoadInspectOrder(String inspectNo, bool includeDetail)
        {
            return this.LoadInspectOrder(inspectNo, includeDetail, false);
        }


        [Transaction(TransactionMode.Unspecified)]
        public InspectOrder LoadInspectOrder(String inspectNo, bool includeDetail, bool isSort)
        {
            InspectOrder inspectOrder = this.LoadInspectOrder(inspectNo);

            if (includeDetail && inspectOrder.InspectOrderDetails != null && inspectOrder.InspectOrderDetails.Count > 0)
            {
                if (isSort)
                {
                    InspectOrderDetailComparer inspectOrderDetailComparer = new InspectOrderDetailComparer();

                    IList<InspectOrderDetail> inspectOrderDetails = inspectOrder.InspectOrderDetails;
                    List<InspectOrderDetail> targetInspectOrderDetails = new List<InspectOrderDetail>();
                    foreach (InspectOrderDetail inspectOrderDetail in inspectOrderDetails)
                    {
                        targetInspectOrderDetails.Add(inspectOrderDetail);
                    }
                    targetInspectOrderDetails.Sort(inspectOrderDetailComparer);
                    inspectOrderDetails.Clear();
                    foreach (InspectOrderDetail targetInspectOrderDetail in targetInspectOrderDetails)
                    {
                        inspectOrderDetails.Add(targetInspectOrderDetail);
                    }
                }
            }
            return inspectOrder;
        }

        [Transaction(TransactionMode.Requires)]
        public override void CreateInspectOrder(InspectOrder entity)
        {
            entity.EstimateInspectDate = DateTime.Now;
            base.CreateInspectOrder(entity);
        }
        #endregion Customized Methods

        

    }

    class InspectOrderDetailComparer : IComparer<InspectOrderDetail>
    {
        public int Compare(InspectOrderDetail x, InspectOrderDetail y)
        {

            if (x.LocationLotDetail.Item.Code == y.LocationLotDetail.Item.Code
                 && (x.LocationLotDetail.Hu == null
                        || (x.LocationLotDetail.Hu.Uom.Code == y.LocationLotDetail.Hu.Uom.Code
                 && x.LocationLotDetail.Hu.UnitCount == y.LocationLotDetail.Hu.UnitCount))
                 && x.LocationFrom.Code == y.LocationFrom.Code
                 && x.LocationTo.Code == y.LocationTo.Code
                 )
            {
                return 0;
            }
            if (x.LocationLotDetail.Item.Code != y.LocationLotDetail.Item.Code)
            {
                return string.Compare(x.LocationLotDetail.Item.Code, y.LocationLotDetail.Item.Code);
            }
            if (x.LocationLotDetail.Hu != null && x.LocationLotDetail.Hu.Uom.Code != y.LocationLotDetail.Hu.Uom.Code)
            {
                return string.Compare(x.LocationLotDetail.Hu.Uom.Code, y.LocationLotDetail.Hu.Uom.Code);
            }
            if (x.LocationLotDetail.Hu != null && x.LocationLotDetail.Hu.UnitCount != y.LocationLotDetail.Hu.UnitCount)
            {
                return x.LocationLotDetail.Hu.UnitCount.CompareTo(y.LocationLotDetail.Hu.UnitCount);
            }
            if (x.LocationFrom.Code != y.LocationFrom.Code)
            {
                return string.Compare(x.LocationFrom.Code, y.LocationFrom.Code);
            }

            return string.Compare(x.LocationTo.Code, y.LocationTo.Code);


        }
    }


}