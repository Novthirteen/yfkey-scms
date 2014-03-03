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
                #region �ж��Ƿ���Ҫ�¼�
                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(orderLocationTransaction.Location.Code, inProcessLocationDetail.HuId.Trim());
                if (locationLotDetailList != null && locationLotDetailList.Count > 0 && locationLotDetailList[0].StorageBin != null)
                {
                    //�¼�
                    this.InventoryPick(locationLotDetailList[0], user);
                }
                #endregion
            }

            #region ���ҳ����λ
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
                    //������������������ȡ��λ
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

            #region ���¿��
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

            #region ��������״̬�Ϳ�λ
            //ֻ����ͨ�Ķ����ż�¼���˻��͵������ÿ��ǣ���Ϊ�˻��͵��������ջ����������������ջ��ļ�¼�������˻�������
            if (orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML
                && inProcessLocationDetail.HuId != null && inProcessLocationDetail.HuId.Trim() != string.Empty
                && inProcessLocationDetail.Qty > 0)
            {
                Hu hu = this.huMgr.LoadHu(inProcessLocationDetail.HuId.Trim());
                if (orderDetail.DefaultLocationTo != null)
                {
                    hu.Location = orderDetail.DefaultLocationTo.Code;
                    //�����ѿ����գ���Ȼ�ջ�û���Ļ���ʾ�����Ŀ��
                    hu.StorageBin = null;
                }
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INPROCESS;
                hu.TransferFlag = orderDetail.OrderHead.IsMes;
                hu.IsMes = orderDetail.OrderHead.IsMes;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region ��¼�������
            foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
            {
                this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, inProcessLocationDetail.InProcessLocation, user);

                #region �˻����ϼ�
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
                #region �ж��Ƿ���Ҫ�¼�
                LocationLotDetail locationLotDetail = this.locationLotDetailMgr.CheckLoadHuLocationLotDetail(miscOrderDetail.HuId.Trim(), miscOrderDetail.MiscOrder.Location);
                if (locationLotDetail.StorageBin != null)
                {
                    //�¼�
                    this.InventoryPick(locationLotDetail, user);
                }
                #endregion
            }

            #region ���¿��
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

            #region ��������״̬�Ϳ�λ
            if (miscOrderDetail.HuId != null && miscOrderDetail.HuId.Trim() != string.Empty
                && miscOrderDetail.Qty > 0)
            {
                Hu hu = this.huMgr.LoadHu(miscOrderDetail.HuId.Trim());
                hu.Location = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CLOSE;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region ��¼�������
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
            #region ���¿��
            LocationLotDetail locLotDet = this.locationLotDetailMgr.LoadLocationLotDetail(huOdd.LocationLotDetail.Id);
            locLotDet.Qty -= huOdd.CurrentCreateQty;
            this.locationLotDetailMgr.UpdateLocationLotDetail(locLotDet);
            #endregion

            #region ��¼�������
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

                    #region �ж��Ƿ���Ҫ�¼�
                    if (huLocationLotDetail.StorageBin != null)
                    {
                        //�¼�
                        this.InventoryPick(huLocationLotDetail, user);
                    }
                    #endregion
                }
                else
                {
                    throw new BusinessErrorException("Hu.Error.NoInventory", hu.HuId);
                }
            }

            #region ���¿��
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

            #region ��������״̬��Ͷ��Ҳ����;
            if (hu != null && materialIn.Qty > 0)
            {
                hu.Location = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_INPROCESS;
                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region ��¼�������
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

            #region ���ۿ����⣬��¼Planned Bill
            if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
            {
                #region ��¼������
                plannedBill = this.plannedBillMgr.CreatePlannedBill(receiptDetail, user);
                #endregion

                #region ί��ӹ���������
                if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
                {
                    plannedBill.CurrentActingQty = plannedBill.PlannedQty;
                    this.billMgr.CreateActingBill(plannedBill, user);
                    return null;  //ί��ӹ������ӿ�棬�������������
                }
                #endregion

                #region �жϼ��۸�����Ƿ���������
                bool autoSettleMinusCSInventory = true;
                if (autoSettleMinusCSInventory && isCS &&
                    (receiptDetail.ReceivedQty.HasValue ? receiptDetail.ReceivedQty.Value : 0) < 0)
                {
                    plannedBill.CurrentActingQty = plannedBill.PlannedQty;
                    this.billMgr.CreateActingBill(plannedBill, user);
                    isCS = false;
                }
                #endregion

                #region �ж�Ŀ�Ŀ�λΪ�յ���������
                if (isCS && orderLocationTransaction.Location == null && isReceiveSettle)
                {
                    plannedBill.CurrentActingQty = plannedBill.PlannedQty;
                    this.billMgr.CreateActingBill(plannedBill, user);
                    isCS = false;
                }
                #endregion
            }
            #endregion

            #region ������
            if (orderLocationTransaction.Location != null)
            {
                #region �ջ�LotNo����С�����п���LotNoУ��
                if (orderHead.IsGoodsReceiveFIFO && receiptDetail.HuId != null && receiptDetail.ReceivedQty.Value > 0) //�˻�������
                {
                    Hu hu = this.huMgr.CheckAndLoadHu(receiptDetail.HuId);

                    #region ����ջ�����С�ڱ����ջ���ϸ���������ڣ�ȡ��С������Ϊ�Ƚ��ȳ��Ļ�׼����
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


                    if (!minManufactureDate.HasValue  //�����ջ�Ϊ��һ���ջ���Ҫ����
                        || minManufactureDate.Value.CompareTo(hu.ManufactureDate) > 0)   //�ջ�����С�������ڴ��ڵ��ڱ����ջ���ϸ���������ڲ���ҪУ�飬Ҳ���Ǳ����ջ�����������Ϊ��ǰ��Сֵ
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
                    #region ����
                    IList<InventoryTransaction> resultInventoryTransactionList = new List<InventoryTransaction>();

                    #region ��Ʒ������
                    if (receiptDetail.ReceivedQty.HasValue && receiptDetail.ReceivedQty.Value != 0)
                    {
                        Location locIn = orderLocationTransaction.Location; //Ĭ�����λ

                        #region ����Hu�˻������Hu�ڴ�Ʒ��λ�������λ����Ϊ��Ʒ��λ
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

                        #region ���¿��
                        IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                            orderLocationTransaction.Item,
                            locIn,
                            receiptDetail.HuId,
                            receiptDetail.LotNo,
                            receiptDetail.ReceivedQty.Value * orderLocationTransaction.UnitQty,
                            isCS ? true : receiptDetail.IsConsignment,                     //�����Ǽ����ƿ�
                            plannedBill != null ? plannedBill : (receiptDetail.PlannedBill.HasValue ? this.plannedBillMgr.LoadPlannedBill(receiptDetail.PlannedBill.Value) : null),  //��Ҫ��ASNDetail���ҵ�������Ϣ
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

                        #region ��¼�������
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            #region �ɹ������˻�����������
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

                    #region ��Ʒ������
                    if (receiptDetail.RejectedQty.HasValue && receiptDetail.RejectedQty.Value > 0)
                    {
                        #region ���¿��
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

                        #region ��¼�������
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
                    #region ����
                    IList<InventoryTransaction> resultInventoryTransactionList = new List<InventoryTransaction>();

                    #region ��Ʒ������
                    if (receiptDetail.ReceivedQty.HasValue && receiptDetail.ReceivedQty.Value != 0)
                    {
                        #region ���¿��
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

                        #region ��¼�������
                        foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                        {
                            this.locationTransactionMgr.RecordLocationTransaction(orderLocationTransaction, inventoryTransaction, receipt, user);
                        }
                        #endregion
                    }
                    #endregion

                    #region ��Ʒ������
                    if (receiptDetail.RejectedQty.HasValue && receiptDetail.RejectedQty.Value > 0)
                    {
                        #region ���¿��
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

                        #region ��¼�������
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
                #region ���û�п�λ�ر�Hu
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
            #region ���¿��
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

            #region ��¼�������
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

            #region ���¿��
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

            #region ��¼�������
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

            #region �����¼ܿ��
            if (oldLocationLotDetail.StorageBin == null)
            {
                throw new BusinessErrorException("Location.Error.PickUp.NotInBin", oldLocationLotDetail.Hu.HuId);
            }
            #endregion

            #region ��¼��������
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_PICK, user);
            #endregion

            #region �¼�
            oldLocationLotDetail.StorageBin = null;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            #region ��¼�������
            InventoryTransaction inventoryInTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryInTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PICK, user);
            #endregion

            #region ����Hu��λ
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

            #region �����ϼܿ��
            if (oldLocationLotDetail.StorageBin != null)
            {
                if (locationLotDetail.NewStorageBin != null
                    && oldLocationLotDetail.StorageBin.Code == locationLotDetail.NewStorageBin.Code)
                {
                    throw new BusinessErrorException("Location.Error.PutAway.AlreadyInBin", oldLocationLotDetail.Hu.HuId, oldLocationLotDetail.StorageBin.Code);
                }

                //�����¼�
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

            #region ��¼��������
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_PUT, user);
            #endregion

            #region �ϼ�
            oldLocationLotDetail.StorageBin = locationLotDetail.NewStorageBin;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            #region ��¼�������
            InventoryTransaction inventoryInTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, oldLocationLotDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryInTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PUT, user);
            #endregion

            #region ����Hu��λ
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

            #region �¼�
            if (oldLocationLotDetail.StorageBin != null)
            {
                this.InventoryPick(oldLocationLotDetail, user);
            }
            #endregion

            #region ���㣬ֻ�з�����������
            if (oldLocationLotDetail.IsConsignment == true
                && oldLocationLotDetail.PlannedBill.HasValue
                && repackDetail.Repack.Type == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
            {
                PlannedBill pb = this.plannedBillMgr.LoadPlannedBill(oldLocationLotDetail.PlannedBill.Value);
                pb.CurrentActingQty = repackDetail.Qty / pb.UnitQty;
                this.billMgr.CreateActingBill(pb, user);
            }
            #endregion

            #region ����
            oldLocationLotDetail.Qty -= repackDetail.Qty;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            #region Hu�ر�
            if (oldLocationLotDetail.Hu != null)
            {
                Hu hu = this.huMgr.LoadHu(oldLocationLotDetail.Hu.HuId);
                hu.Location = null;
                hu.Status = BusinessConstants.CODE_MASTER_HU_STATUS_VALUE_CLOSE;

                this.huMgr.UpdateHu(hu);
            }
            #endregion

            #region ��¼��������
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - repackDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_REPACK, user);
            #endregion
        }

        [Transaction(TransactionMode.Requires)]
        public InventoryTransaction InventoryRepackOut(RepackDetail repackDetail, Location location, int? plannedBillId, User user)
        {
            #region ����Hu�ϵ�OrderNo
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

            #region ���
            PlannedBill plannedBill = plannedBillId.HasValue ? this.plannedBillMgr.LoadPlannedBill(plannedBillId.Value) : null;
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                hu != null ? hu.Item : this.itemMgr.CheckAndLoadItem(repackDetail.itemCode),
                location,
                hu != null ? hu.HuId : null,
                hu != null ? hu.LotNo : null,
                repackDetail.Qty,     //��浥λ
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

            #region ��¼��������
            LocationLotDetail inLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransactionList[0].LocationLotDetailId);

            InventoryTransaction inventoryInTransaction = InventoryTransactionHelper.CreateInventoryTransaction(inLocationLotDetail, repackDetail.Qty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryInTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_REPACK, user);
            #endregion

            #region �ϼ�
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

            #region �¼�
            if (oldLocationLotDetail.StorageBin != null)
            {
                this.InventoryPick(oldLocationLotDetail, user);
            }
            #endregion

            #region ����
            oldLocationLotDetail.Qty -= locationLotDetail.CurrentInspectQty;
            this.locationLotDetailMgr.UpdateLocationLotDetail(oldLocationLotDetail);
            #endregion

            //�������
            if (needSettle && locationLotDetail.IsConsignment && locationLotDetail.PlannedBill.HasValue)
            {
                PlannedBill plannedBill = this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value);
                if (plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION)
                {
                    plannedBill.CurrentActingQty = locationLotDetail.CurrentInspectQty/plannedBill.UnitQty;
                    this.billMgr.CreateActingBill(plannedBill, locationLotDetail, user);
                }
            }

            #region ��¼��������
            InventoryTransaction inventoryOutTransaction = InventoryTransactionHelper.CreateInventoryTransaction(oldLocationLotDetail, 0 - locationLotDetail.CurrentInspectQty, false);
            this.locationTransactionMgr.RecordLocationTransaction(inventoryOutTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP, user, inspectNo, locationTo);
            #endregion

            return inventoryOutTransaction;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<InventoryTransaction> InspectOut(Location location, Item item, decimal qty, User user, string inspectNo, Location locationTo)
        {
            #region ���¿��
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

            #region ��¼�������
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
            bool isBillSettled = false;  //�Ƿ��Ѿ��������
            if (needSettle && locationLotDetail.IsConsignment && locationLotDetail.PlannedBill.HasValue)
            {
                PlannedBill plannedBill = this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value);
                if (plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION)
                {
                    isBillSettled = true;
                }
            }

            #region ���
            IList<InventoryTransaction> inventoryTransactionList = RecordInventory(
                    locationLotDetail.Item,
                    locIn,
                //�벻�ϸ�Ʒ��λ����Ҫ������
                    locIn.Code != BusinessConstants.SYSTEM_LOCATION_REJECT && locationLotDetail.Hu != null ? locationLotDetail.Hu.HuId : null,
                    locationLotDetail.LotNo,
                    locationLotDetail.CurrentInspectQty,     //��浥λ
                    isBillSettled ? false : locationLotDetail.IsConsignment,   //�Ѿ��������ֱ�Ӽ�Ϊ�Ǽ��ۿ��
                    isBillSettled ? null : (locationLotDetail.PlannedBill.HasValue ? this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value) : null),      //�Ѿ��������ֱ�Ӽ�Ϊ�Ǽ��ۿ��
                    BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP,
                    null,
                //false,
                    user,
                    false,
                    false,
                    (locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT && locIn.Code == BusinessConstants.SYSTEM_LOCATION_REJECT
                    ? locationLotDetail.RefLocation : (locIn.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT ? locationLotDetail.Location.Code : null)) //�������λ���粻�ϸ�Ʒ��λ����Ҫ��RefLoc����ȥ
                    );
            #endregion

            #region ��¼�������
            if (inventoryTransactionList != null && inventoryTransactionList.Count > 0)
            {
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP, user, inspectNo, locationLotDetail.Location, null, inrNo,inspectDetailId);
                }
            }
            #endregion

            #region �ϼ�
            if (bin != null)
            {
                LocationLotDetail oldLocationLotDetail = this.locationLotDetailMgr.LoadLocationLotDetail(inventoryTransactionList[0].LocationLotDetailId);
                this.InventoryPut(oldLocationLotDetail, user);
            }
            #endregion

            #region ��������״̬�Ϳ�λ
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
            #region ���¿��
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

            #region ��¼�������
            if (inventoryTransactionList != null && inventoryTransactionList.Count > 0)
            {
                foreach (InventoryTransaction inventoryTransaction in inventoryTransactionList)
                {
                    this.locationTransactionMgr.RecordLocationTransaction(inventoryTransaction, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_INP, user, inspectNo, locationFrom);
                }
            }
            #endregion

            #region ��������״̬�Ϳ�λ
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
            #region ���¿��
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

            #region ��¼�������
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

            #region ��������״̬
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
         * transType ��������
         * loc ��λ
         * effdateStart ������Ч����
         * effDateEnd ������Ч������
         * createDateStart ���񴴽�ʱ��
         * createDateEnd ���񴴽�ʱ����
         * ����ʱ��  --ȥ��
         * ����ʱ���� --ȥ��
         * partyFrom ��Դ��Ӧ��/����
         * partyTo Ŀ�Ŀͻ�/����
         * itemCode �����
         * orderNo ������
         * recNo �ջ���
         * createUser ������
         * ipNo asn��
         * userCode ��ǰ�û�
         */
        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationTransaction> GetLocationTransaction(string[] transType,
           string[] loc, DateTime effdateStart, DateTime effDateEnd, DateTime createDateStart, DateTime createDateEnd,
           string partyFrom, string partyTo, string[] itemCode,
           string[] orderNo, string[] recNo, string createUser, string[] ipNo, string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            //��������
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

            //��λ
            addDisjunction(list, "Location", ref criteria);

            //������Ч����
            if (effdateStart != null)
            {
                criteria.Add(Expression.Ge("EffectiveDate", effdateStart));
            }

            //������Ч������
            if (effDateEnd != null)
            {
                criteria.Add(Expression.Le("EffectiveDate", effDateEnd));
            }
            //���񴴽�ʱ��
            if (createDateStart != null)
            {
                criteria.Add(Expression.Ge("CreateDate", createDateStart));
            }
            //���񴴽�ʱ����
            if (createDateEnd != null)
            {
                criteria.Add(Expression.Le("CreateDate", createDateEnd));
            }
            //��Դ��Ӧ��/����
            if (partyFrom != null && partyTo.Length > 0)
            {
                criteria.Add(Expression.Eq("PartyFrom", partyFrom));
            }
            //Ŀ�Ŀͻ�/����
            if (partyTo != null && partyTo.Length > 0)
            {
                criteria.Add(Expression.Eq("PartyTo", partyTo));
            }
            //�����
            addDisjunction(itemCode, "Item", ref criteria);


            //������
            addDisjunction(orderNo, "OrderNo", ref criteria);

            //�ջ���recNo
            addDisjunction(recNo, "ReceiptNo", ref criteria);
            //������createUser
            if (createUser != null && createUser.Length > 0)
            {
                criteria.Add(Expression.Eq("CreateUser", createUser));
            }
            //asn��ipNo
            addDisjunction(ipNo, "IpNo", ref criteria);

            //��λ����
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
                #region ��Hu
                //����/�Ǽ��۴����߼���ͬ
                //��֧�ֶ�����HU���ƻ������
                if (qty > 0)
                {
                    #region ������� > 0
                    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(location.Code, huId);
                    if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                    {
                        //������Ѿ�������ͬ��Hu
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
                    #region ������� < 0 / ����
                    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetHuLocationLotDetail(location.Code, huId);
                    if (locationLotDetailList != null && locationLotDetailList.Count > 0)
                    {
                        LocationLotDetail locationLotDetail = locationLotDetailList[0];  //�����ѯ����¼��ֻ������һ��

                        ////�������Hu�Ƿ񱻼����ռ��
                        //if (this.IsHuOcuppyByPickList(huId))
                        //{
                        //    throw new BusinessErrorException("Order.Error.PickUp.HuOcuppied", huId);
                        //}

                        if (locationLotDetail.Qty + qty < 0)
                        {
                            //Hu��Item������С�ڳ�����
                            throw new BusinessErrorException("Hu.Error.NoEnoughInventory", huId, location.Code, item.Code);
                        }

                        bool isBillSettled = false;
                        if (locationLotDetail.IsConsignment && locationLotDetail.PlannedBill.HasValue)
                        {
                            PlannedBill pb = this.plannedBillMgr.LoadPlannedBill(locationLotDetail.PlannedBill.Value);
                            if (
                                //(pb.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING   //���߽��������������ƿ��������Ž��㣬���Ա����ջ�����ʱ������λ�ͽ���
                                //&& (transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR
                                //    || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN))
                            //|| 
                            (pb.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING   //���߽�������
                                && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO)
                                //|| (locationLotDetail.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //��������������Ӽ����λ���Ⲣ��ISS-INP����
                                //    && locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT
                                //    && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP)
                                //|| (locationLotDetail.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //������㣬�Ӳ��ϸ�Ʒ��λ����
                                //    && locationLotDetail.Location.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP                                   //����ISS_UNP����CYC_CNT����ǿ�н���
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO
                            || transType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT))
                            {
                                //����
                                //���ۿ����Ҫ���н��㣬�Ա��س�Ŀ����и�������
                                pb.CurrentActingQty = (0 - qty) / pb.UnitQty; //����������
                                this.billMgr.CreateActingBill(pb, locationLotDetail, user);
                                isBillSettled = true;
                            }
                        }

                        //��¼���س�ļ�¼
                        inventoryTransactionList.Add(InventoryTransactionHelper.CreateInventoryTransaction(locationLotDetail, qty, isBillSettled));

                        //���¿������
                        locationLotDetail.Qty += qty;
                        this.locationLotDetailMgr.UpdateLocationLotDetail(locationLotDetail);
                    }
                    else
                    {
                        //û���ҵ�ָ����HU
                        throw new BusinessErrorException("Hu.Error.NoEnoughInventory", huId, location.Code, item.Code);
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region û��Hu
                if (isCS)
                {
                    #region ���۴���
                    if (qty > 0)
                    {
                        #region ������� > 0

                        //������ջ����������ջ����㣬���س��档
                        if (!(needInspection
                            || plannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_RECEIVING_SETTLEMENT))
                        {
                            //#region �س�ָ���ջ����ŵļ��ۿ��
                            //if (refOrderNo != null && refOrderNo.Trim() != string.Empty)
                            //{
                            //    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.MINUS_INVENTORY, false, refOrderNo, flushbackIgnoreHu);
                            //    BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                            //}
                            //#endregion

                            #region �س���ۿ��
                            if (qty > 0)
                            {
                                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.MINUS_INVENTORY, false, flushbackIgnoreHu);
                                BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                            }
                            #endregion

                            #region �س�Ǽ��ۿ��
                            if (qty > 0)
                            {
                                IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.MINUS_INVENTORY, false, flushbackIgnoreHu);
                                BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                            }
                            #endregion
                        }

                        #region ��¼���
                        if (qty > 0)
                        {
                            CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, plannedBill, inventoryTransactionList, user, refLoc);
                        }
                        #endregion
                        #endregion
                    }
                    else if (qty < 0)
                    {
                        #region ������� < 0

                        //#region �س�ָ���ջ����ŵļ��ۿ��
                        //if (refOrderNo != null && refOrderNo.Trim() != string.Empty)
                        //{
                        //    IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.PLUS_INVENTORY, false, refOrderNo, flushbackIgnoreHu);
                        //    BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                        //}
                        //#endregion

                        #region �س���ۿ��
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                        }
                        #endregion

                        #region �س�Ǽ��ۿ��
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, plannedBill, user, transType);
                        }
                        #endregion

                        #region ��¼���
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
                    #region �Ǽ��۴���
                    if (qty > 0)
                    {
                        #region ������� > 0

                        //�ջ������Ĳ��ܻس���
                        if (!needInspection)
                        {
                            #region �س�Ǽ��ۿ��
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.MINUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, null, user, transType);
                            #endregion
                        }

                        #region ��¼���
                        if (qty > 0)
                        {
                            CreateNewLocationLotDetail(item, location, huId, lotNo, qty, isCS, null, inventoryTransactionList, user,refLoc);
                        }
                        #endregion
                        #endregion
                    }
                    else if (qty < 0)
                    {
                        #region ������� < 0
                        #region �س�Ǽ��ۿ��
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, false, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, null, user, transType);
                        }
                        #endregion

                        #region �س���ۿ��
                        if (qty < 0)
                        {
                            IList<LocationLotDetail> locationLotDetailList = this.locationLotDetailMgr.GetLocationLotDetail(location.Code, item.Code, true, false, BusinessConstants.PLUS_INVENTORY, false, flushbackIgnoreHu);
                            BackFlushInventory(locationLotDetailList, ref qty, inventoryTransactionList, null, user, transType);
                        }
                        #endregion

                        #region ��¼���
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

                    #region ֻ�з����ڰ����뷢�����������ջ����������Ϊ��Щ���س�Ŀ���Ѿ�����һ��������������Ͳ������س��ˡ�
                    if (backFlushLocLotDet.Qty == 0)
                    {
                        continue;
                    }
                    #endregion

                    #region �ж��Ƿ�����س�����
                    if (qtyIn == 0)
                    {
                        return;
                    }

                    //������س�����Ķ��Ǽ��ۿ�沢����ͬһ����Ӧ�̣�һ��Ҫ�س�
                    if (backFlushLocLotDet.IsConsignment && plannedBill != null
                        && backFlushPlannedBill.BillAddress.Code == plannedBill.BillAddress.Code)
                    {

                    }
                    else
                    {
                        //���س�ļ��ۿ�棬�����ļ��ۿ�治��һ����Ӧ�̣����ܻس�
                        if (backFlushLocLotDet.IsConsignment && plannedBill != null
                            && backFlushPlannedBill.BillAddress.Code != plannedBill.BillAddress.Code)
                        {
                            return;
                        }

                        //���س�ļ��ۿ��Ľ��㷽ʽ�����߽��㣬�ж��Ƿ�ǰ���������Ƿ���ISS-*�������㲻�ܻس�
                        //if (backFlushLocLotDet.IsConsignment
                        //    && backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING)
                        //{
                        //    if (!transType.StartsWith("ISS-"))
                        //    {
                        //        return;
                        //    }
                        //}

                        //���س�ļ��ۿ��Ľ��㷽ʽ�����߽��㣬�ж��Ƿ�ǰ���������Ƿ���ISS-*���Ҳ�����ISS-TR�������㲻�ܻس�
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

                    #region �س���
                    decimal currentBFQty = 0; //���λس���
                    if (qtyIn > 0)
                    {
                        if (backFlushLocLotDet.Qty + qtyIn < 0)
                        {
                            //��������� < ���������ȫ���س壬�س��������ڱ��������
                            currentBFQty = qtyIn;
                        }
                        else
                        {
                            //��������� >= ��������������Ŀ�����س�
                            currentBFQty = 0 - backFlushLocLotDet.Qty;
                        }
                    }
                    else
                    {
                        if (backFlushLocLotDet.Qty + qtyIn > 0)
                        {
                            //���γ����� < ���������ȫ���س壬�س��������ڱ��γ�����
                            currentBFQty = qtyIn;
                        }
                        else
                        {
                            //���γ����� >= ��������������Ŀ�����س�
                            currentBFQty = 0 - backFlushLocLotDet.Qty;
                        }
                    }

                    //���¿������
                    backFlushLocLotDet.Qty += currentBFQty;
                    this.locationLotDetailMgr.UpdateLocationLotDetail(backFlushLocLotDet);

                    #endregion

                    #region ����
                    bool isBillSettled = false;
                    //ֻ�г���(qtyIn < 0 && plannedBill == null)�س���ۿ��(backFlushLocLotDet.IsConsignment == true)�Ű���SettleTerm����
                    //�������������������
                    if (qtyIn < 0 && plannedBill == null && backFlushLocLotDet.IsConsignment)
                    {
                        if (
                            //(backFlushPlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_ONLINE_BILLING   //���߽��������������ƿ��������Ž��㣬���Ա����ջ�����ʱ������λ�ͽ���
                            //&& (transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR
                            //        || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN)) 
                            //|| 
                            (backFlushPlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_LINEAR_CLEARING   //���߽�������
                                && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO)
                            //|| (backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //��������������Ӽ����λ���⣬���ҷ���ISS-INP����
                            //    && backFlushLocLotDet.Location.Code == BusinessConstants.SYSTEM_LOCATION_INSPECT
                            //        && transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_INP)
                            //|| (backFlushLocLotDet.PlannedBill.SettleTerm == BusinessConstants.CODE_MASTER_BILL_SETTLE_TERM_VALUE_INSPECTION         //������㣬�Ӳ��ϸ�Ʒ��λ���⣬��������
                            //    && backFlushLocLotDet.Location.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP                              //�������ISS_UNP����CYC_CNT����ǿ�н���
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO
                            || transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO
                            || transType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT))
                        {
                            //���ۿ����Ҫ���н��㣬�Ա��س�Ŀ����и�������
                            backFlushPlannedBill.CurrentActingQty = (0 - currentBFQty) / backFlushPlannedBill.UnitQty; //����������
                            this.billMgr.CreateActingBill(backFlushPlannedBill, backFlushLocLotDet, user);
                            isBillSettled = true;
                        }
                    }
                    else
                    {
                        if (backFlushLocLotDet.IsConsignment)
                        {
                            //���ۿ����Ҫ���н��㣬�Ա��س�Ŀ����и�������
                            backFlushPlannedBill.CurrentActingQty = (0 - currentBFQty) / backFlushPlannedBill.UnitQty; //����������
                            this.billMgr.CreateActingBill(backFlushPlannedBill, backFlushLocLotDet, user);
                            isBillSettled = true;
                        }

                        if (plannedBill != null)
                        {
                            //�����Ŀ����н���
                            plannedBill.CurrentActingQty = currentBFQty / plannedBill.UnitQty;  //����������
                            this.billMgr.CreateActingBill(plannedBill, user);
                        }
                    }
                    #endregion

                    //��¼���س�ļ�¼
                    inventoryTransactionList.Add(InventoryTransactionHelper.CreateInventoryTransaction(backFlushLocLotDet, currentBFQty, isBillSettled));

                    qtyIn -= currentBFQty;
                }
            }
        }

        private LocationLotDetail CreateNewLocationLotDetail(Item item, Location location, string huId, string lotNo, decimal qty, bool isCS, PlannedBill plannedBill, IList<InventoryTransaction> inventoryTransactionList, User user, string refLoc)
        {
            #region �Ƿ��������
            if (!location.AllowNegativeInventory && qty < 0)
            {
                throw new BusinessErrorException("Location.Error.NotAllowNegativeInventory", location.Code);
            }
            #endregion

            #region �������&�ɹ����������������
            if (item.Type != BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_P && item.Type != BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_M)
            {
                throw new BusinessErrorException("Location.Error.ItemTypeNotValid", item.Type);
            }
            #endregion

            bool isBillSettled = false;
            #region �ջ�����/������
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

                //��������������ϵ������в��죬���������ϵ�����
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