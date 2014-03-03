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

            #region �������鵥ͷ
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

            #region ����������ϸ
            foreach (LocationLotDetail locationLotDetail in noneZeroLocationLotDetailList)
            {
                //�������
                this.locationMgr.InspectOut(locationLotDetail, user, false, inspectOrder.InspectNo, this.locationMgr.GetInspectLocation());

                //������λ
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

            #region �������鵥ͷ
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

            #region ����������ϸ
            if (itemQtyDic != null && itemQtyDic.Count > 0)
            {


                foreach (string itemCode in itemQtyDic.Keys)
                {
                    if (itemQtyDic[itemCode] == 0)
                    {
                        continue;
                    }

                    Item item = this.itemMgr.CheckAndLoadItem(itemCode);

                    //�������
                    IList<InventoryTransaction> inventoryTransactionList = this.locationMgr.InspectOut(location, item, itemQtyDic[itemCode], user, inspectOrder.InspectNo, this.locationMgr.GetInspectLocation());

                    //������λ
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
            #region �������鵥ͷ
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

            #region ����������ϸ
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

                    //�������
                    this.locationMgr.InspectOut(location, item, itemFgQtyDic[itemFgCode], user, inspectOrder.InspectNo, this.locationMgr.GetInspectLocation());

                    //������λ
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
            #region ���˼�������Ϊ0�ļ�����ϸ
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

            #region ѭ�����鵥��ϸ
            IDictionary<string, InspectOrder> cachedInspectOrderDic = new Dictionary<string, InspectOrder>();
            string irNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION_RESULT);  //����������
            IList<InspectOrderDetail> pendingList = new List<InspectOrderDetail>();

            foreach (InspectOrderDetail inspectOrderDetail in noneZeroInspectOrderDetailList)
            {
                #region ������鵥ͷ
                InspectOrder inspectOrder = inspectOrderDetail.InspectOrder;
                if (!cachedInspectOrderDic.ContainsKey(inspectOrder.InspectNo))
                {
                    cachedInspectOrderDic.Add(inspectOrder.InspectNo, inspectOrder);
                }
                #endregion

                #region ��������
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
                    //�������ֻ�ܼ���һ��
                    throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.HuInspectQtyNotMatch", oldInspectOrderDetail.LocationLotDetail.Hu.HuId);
                }

                if (oldInspectOrderDetail.InspectQty < totalInspectedQty)
                {
                    throw new BusinessErrorException("MasterData.Inventory.Inspect.Error.InspectQtyExcceed", oldInspectOrderDetail.LocationLotDetail.Item.Code);
                }
                #endregion

                #region �ϸ�Ʒ
                if (oldInspectOrderDetail.CurrentQualifiedQty > 0)
                {
                    //���´�����ϸ�Ʒ����
                    oldInspectOrderDetail.PendingQualifiedQty += oldInspectOrderDetail.CurrentQualifiedQty;
                }
                #endregion

                #region ���ϸ�Ʒ
                if (oldInspectOrderDetail.CurrentRejectedQty > 0)
                {
                    //���²��ϸ�Ʒ����
                    oldInspectOrderDetail.PendingRejectedQty += oldInspectOrderDetail.CurrentRejectedQty;
                }
                #endregion

                #region ���������
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

                #region ���¼��鵥��ϸ
                this.inspectOrderDetailMgr.UpdateInspectOrderDetail(oldInspectOrderDetail);
                #endregion

                #region ���ݿ�λ���ȷ��
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

            #region ����Ҫȷ�ϵ��Զ�ȷ��
            if (pendingList != null && pendingList.Count > 0)
            {
                ProcessInspectOrder(pendingList, user);
            }
            #endregion

            #region ���¼��鵥
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
            #region ���˼�������Ϊ0�ļ�����ϸ
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

            #region ѭ�����鵥��ϸ
            IDictionary<string, InspectOrder> cachedInspectOrderDic = new Dictionary<string, InspectOrder>();
            //string inrNo = this.numberControlMgr.GenerateNumber(BusinessConstants.CODE_PREFIX_INSPECTION_RESULT);  //����������
            foreach (InspectOrderDetail inspectOrderDetail in noneZeroInspectOrderDetailList)
            {
                #region ������鵥ͷ
                InspectOrder inspectOrder = inspectOrderDetail.InspectOrder;
                if (!cachedInspectOrderDic.ContainsKey(inspectOrder.InspectNo))
                {
                    cachedInspectOrderDic.Add(inspectOrder.InspectNo, inspectOrder);
                }
                #endregion

                #region ��������
                InspectOrderDetail oldInspectOrderDetail = this.inspectOrderDetailMgr.LoadInspectOrderDetail(inspectOrderDetail.Id);
                //oldInspectOrderDetail.Disposition = inspectOrderDetail.Disposition;
                oldInspectOrderDetail.CurrentQualifiedQty = inspectOrderDetail.CurrentQualifiedQty;
                oldInspectOrderDetail.CurrentRejectedQty = inspectOrderDetail.CurrentRejectedQty;

                if (oldInspectOrderDetail.LocationLotDetail.Hu != null)
                {
                    if (oldInspectOrderDetail.PendingQualifiedQty != inspectOrderDetail.CurrentQualifiedQty
                    && oldInspectOrderDetail.PendingRejectedQty != inspectOrderDetail.CurrentRejectedQty)
                    {
                        //�������ֻ�ܼ���һ��
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

                #region �ϸ�Ʒ
                if (oldInspectOrderDetail.CurrentQualifiedQty > 0)
                {
                    oldInspectOrderDetail.LocationLotDetail.CurrentInspectQty = oldInspectOrderDetail.CurrentQualifiedQty;

                    //�������λ
                    this.locationMgr.InspectOut(oldInspectOrderDetail.LocationLotDetail, user, true, inspectOrder.InspectNo, oldInspectOrderDetail.LocationTo);

                    //��ϸ�Ʒ��λ
                    this.locationMgr.InspectIn(oldInspectOrderDetail.LocationLotDetail, oldInspectOrderDetail.LocationTo, oldInspectOrderDetail.CurrentStorageBin, user, true, inspectOrder.InspectNo, null, inspectOrderDetail.Id);

                    //���ºϸ�Ʒ����
                    if (!oldInspectOrderDetail.QualifiedQty.HasValue)
                    {
                        oldInspectOrderDetail.QualifiedQty = 0;
                    }
                    oldInspectOrderDetail.QualifiedQty += oldInspectOrderDetail.CurrentQualifiedQty;
                    oldInspectOrderDetail.PendingQualifiedQty -= oldInspectOrderDetail.CurrentQualifiedQty;

                }
                #endregion

                #region ���ϸ�Ʒ
                if (oldInspectOrderDetail.CurrentRejectedQty > 0)
                {
                    oldInspectOrderDetail.LocationLotDetail.CurrentInspectQty = oldInspectOrderDetail.CurrentRejectedQty;

                    //�������λ
                    this.locationMgr.InspectOut(oldInspectOrderDetail.LocationLotDetail, user, false, inspectOrder.InspectNo, this.locationMgr.GetRejectLocation());

                    //�벻�ϸ�Ʒ��λ
                    this.locationMgr.InspectIn(oldInspectOrderDetail.LocationLotDetail, this.locationMgr.GetRejectLocation(), user, false, inspectOrder.InspectNo, null, inspectOrderDetail.Id);

                    //���²��ϸ�Ʒ����
                    if (!oldInspectOrderDetail.RejectedQty.HasValue)
                    {
                        oldInspectOrderDetail.RejectedQty = 0;
                    }
                    oldInspectOrderDetail.RejectedQty += oldInspectOrderDetail.CurrentRejectedQty;
                    oldInspectOrderDetail.PendingRejectedQty -= oldInspectOrderDetail.CurrentRejectedQty;
                }
                #endregion

                #region ���¼��鵥��ϸ
                this.inspectOrderDetailMgr.UpdateInspectOrderDetail(oldInspectOrderDetail);
                #endregion
            }
            #endregion

            #region ���¼��鵥
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