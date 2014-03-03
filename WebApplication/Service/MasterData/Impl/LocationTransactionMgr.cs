using System;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;
using NHibernate.Transform;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class LocationTransactionMgr : LocationTransactionBaseMgr, ILocationTransactionMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ICodeMasterMgr codeMasterMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IItemMgr itemMgr;

        public LocationTransactionMgr(ILocationTransactionDao entityDao,
            ICriteriaMgr criteriaMgr,
            ICodeMasterMgr codeMasterMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IItemMgr itemMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.codeMasterMgr = codeMasterMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.itemMgr = itemMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(OrderLocationTransaction orderLocationTransaction, InventoryTransaction inventoryTransaction, Receipt receipt, User user)
        {
            LocationTransaction locationTransaction = GenerateOrderLocationTransaction(orderLocationTransaction, orderLocationTransaction.Location, user);

            if (inventoryTransaction.Hu != null)
            {
                locationTransaction.HuId = inventoryTransaction.Hu.HuId;
                locationTransaction.LotNo = inventoryTransaction.Hu.LotNo;
            }
            if (locationTransaction.LotNo == null || locationTransaction.LotNo == string.Empty)
            {
                locationTransaction.LotNo = inventoryTransaction.LotNo;
            }
            locationTransaction.BatchNo = inventoryTransaction.LocationLotDetailId;
            locationTransaction.RefLocation = inventoryTransaction.RefLocation;
            locationTransaction.ReceiptNo = receipt.ReceiptNo;
            locationTransaction.IpNo = receipt.ReferenceIpNo;
            locationTransaction.Qty = inventoryTransaction.Qty;
            locationTransaction.EffectiveDate = DateTime.Parse(receipt.CreateDate.ToString("yyyy-MM-dd"));
            if (orderLocationTransaction.OrderDetail.OrderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                OrderLocationTransaction outOrderLocationTransaction = this.orderLocationTransactionMgr.GetOrderLocationTransaction(orderLocationTransaction.OrderDetail.Id, BusinessConstants.IO_TYPE_OUT)[0];
                locationTransaction.RefLocation = outOrderLocationTransaction.Location.Code;
                locationTransaction.RefLocationName = outOrderLocationTransaction.Location.Name;
            }

            this.CreateLocationTransaction(locationTransaction);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(OrderLocationTransaction orderLocationTransaction, InventoryTransaction inventoryTransaction, InProcessLocation inProcessLocation, User user)
        {
            LocationTransaction locationTransaction = GenerateOrderLocationTransaction(orderLocationTransaction, inventoryTransaction.Location, user);

            if (inventoryTransaction.Hu != null)
            {
                locationTransaction.HuId = inventoryTransaction.Hu.HuId;
                locationTransaction.LotNo = inventoryTransaction.Hu.LotNo;
            }
            if (locationTransaction.LotNo == null || locationTransaction.LotNo == string.Empty)
            {
                locationTransaction.LotNo = inventoryTransaction.LotNo;
            }
            locationTransaction.BatchNo = inventoryTransaction.LocationLotDetailId;
            locationTransaction.IpNo = inProcessLocation.IpNo;
            locationTransaction.Qty = inventoryTransaction.Qty;
            locationTransaction.EffectiveDate = DateTime.Parse(inProcessLocation.CreateDate.ToString("yyyy-MM-dd"));

            if (inventoryTransaction.Location.Code == BusinessConstants.SYSTEM_LOCATION_REJECT)
            {
                locationTransaction.RefLocation = inventoryTransaction.RefLocation;
            }

            this.CreateLocationTransaction(locationTransaction);
        }

        public void RecordWOBackflushLocationTransaction(OrderLocationTransaction orderLocationTransaction, string huId, string lotNo, decimal qty, string ipNo, User user, Location locFrom)
        {
            LocationTransaction locationTransaction = new LocationTransaction();

            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
            string prodline = orderHead.Flow;

            locationTransaction.OrderNo = orderHead.OrderNo;
            locationTransaction.OrderDetailId = orderDetail.Id;
            locationTransaction.OrderLocationTransactionId = orderLocationTransaction.Id;
            locationTransaction.ExternalOrderNo = orderHead.ExternalOrderNo;
            locationTransaction.ReferenceOrderNo = orderHead.ReferenceOrderNo;
            locationTransaction.IpNo = ipNo;
            //locationTransaction.ReceiptNo = 
            //locationTransaction.TransactionType = orderLocationTransaction.TransactionType;
            locationTransaction.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO_BF; //投料回冲事务写死 ISS-WO-BF
            locationTransaction.Item = orderLocationTransaction.Item.Code;
            locationTransaction.ItemDescription = orderLocationTransaction.Item.Description;
            locationTransaction.Uom = orderLocationTransaction.Item.Uom.Code;
            //locationTransaction.Qty = 
            locationTransaction.PartyFrom = orderHead.PartyFrom.Code;
            locationTransaction.PartyFromName = orderHead.PartyFrom.Name;
            locationTransaction.PartyTo = orderHead.PartyTo.Code;
            locationTransaction.PartyToName = orderHead.PartyTo.Name;
            locationTransaction.ShipFrom = orderHead.ShipFrom != null ? orderHead.ShipFrom.Code : null;
            locationTransaction.ShipFromAddress = orderHead.ShipFrom != null ? orderHead.ShipFrom.Address : null;
            locationTransaction.ShipTo = orderHead.ShipTo != null ? orderHead.ShipTo.Code : null;
            locationTransaction.ShipToAddress = orderHead.ShipTo != null ? orderHead.ShipTo.Address : null;
            locationTransaction.Location = prodline;
            locationTransaction.LocationName = prodline;
            locationTransaction.DockDescription = orderHead.DockDescription;
            locationTransaction.Carrier = orderHead.Carrier != null ? orderHead.Carrier.Code : null;
            locationTransaction.CarrierBillCode = orderHead.CarrierBillAddress != null ? orderHead.CarrierBillAddress.Code : null;
            locationTransaction.CarrierBillAddress = orderHead.CarrierBillAddress != null ? orderHead.CarrierBillAddress.Address : null;
            locationTransaction.CreateDate = DateTime.Now;
            locationTransaction.CreateUser = user.Code;
            if (huId != null && huId.Trim() != string.Empty)
            {
                locationTransaction.HuId = huId;
            }
            if (lotNo != null && lotNo.Trim() != string.Empty)
            {
                locationTransaction.LotNo = lotNo;
            }
            locationTransaction.Qty = 0 - qty;
            locationTransaction.EffectiveDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            locationTransaction.RefLocation = locFrom.Code;
            locationTransaction.RefLocationName = locFrom.Name;

            this.CreateLocationTransaction(locationTransaction);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(MiscOrderDetail miscOrderDetail, InventoryTransaction inventoryTransaction, User user)
        {
            LocationTransaction locationTransaction = new LocationTransaction();
            MiscOrder miscOrder = miscOrderDetail.MiscOrder;

            locationTransaction.OrderNo = miscOrder.OrderNo;
            if (inventoryTransaction.Hu != null)
            {
                locationTransaction.HuId = inventoryTransaction.Hu.HuId;
                locationTransaction.LotNo = inventoryTransaction.Hu.LotNo;
            }
            if (locationTransaction.LotNo == null || locationTransaction.LotNo == string.Empty)
            {
                locationTransaction.LotNo = inventoryTransaction.LotNo;
            }
            locationTransaction.BatchNo = inventoryTransaction.LocationLotDetailId;
            locationTransaction.Item = miscOrderDetail.Item.Code;
            locationTransaction.ItemDescription = miscOrderDetail.Item.Description;
            locationTransaction.Uom = miscOrderDetail.Item.Uom.Code;
            locationTransaction.Qty = inventoryTransaction.Qty;
            locationTransaction.PartyFrom = miscOrder.Location.Region.Code;
            locationTransaction.PartyFromName = miscOrder.Location.Region.Name;
            locationTransaction.PartyTo = miscOrder.Location.Region.Code;
            locationTransaction.PartyToName = miscOrder.Location.Region.Name;
            locationTransaction.Location = miscOrder.Location.Code;
            if (locationTransaction.Location == BusinessConstants.SYSTEM_LOCATION_INSPECT)
            {
                throw new BusinessErrorException("Invalided.Location.Inspect");
            }
            if (locationTransaction.Location == BusinessConstants.SYSTEM_LOCATION_REJECT)
            {
                locationTransaction.RefLocation = inventoryTransaction.RefLocation;
            }

            locationTransaction.LocationName = miscOrder.Location.Name;
            locationTransaction.LocInOutReason = miscOrder.Reason;
            // CodeMaster codeMaster = null;
            if (miscOrder.Type == BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GI)
            {
                locationTransaction.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP;
                //codeMaster = codeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STOCK_IN_REASON, miscOrder.Reason);
            }
            else if (miscOrder.Type == BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_GR)
            {
                locationTransaction.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP;
                //codeMaster = codeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STOCK_OUT_REASON, miscOrder.Reason);
            }
            else if (miscOrder.Type == BusinessConstants.CODE_MASTER_MISC_ORDER_TYPE_VALUE_ADJ)
            {
                locationTransaction.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_CYC_CNT;
                //codeMaster = codeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STOCK_ADJUST_REASON, miscOrder.Reason);
            }
            //if (codeMaster != null)
            //{
            //    locationTransaction.LocInOutReasonDescription = codeMaster.Description;
            //}
            locationTransaction.CreateDate = DateTime.Now;
            locationTransaction.CreateUser = user.Code;
            locationTransaction.EffectiveDate = DateTime.Parse(miscOrder.EffectiveDate.ToString("yyyy-MM-dd"));

            this.CreateLocationTransaction(locationTransaction);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user)
        {
            this.RecordLocationTransaction(inventoryTransaction, transType, user, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo)
        {
            this.RecordLocationTransaction(inventoryTransaction, transType, user, orderNo, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation)
        {
            this.RecordLocationTransaction(inventoryTransaction, transType, user, orderNo, refLocation, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation, Flow productLine)
        {
            this.RecordLocationTransaction(inventoryTransaction, transType, user, orderNo, refLocation, null, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation, Flow productLine, string receiptNo)
        {
            this.RecordLocationTransaction(inventoryTransaction, transType, user, orderNo, refLocation, null, null, null);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation, Flow productLine, string receiptNo, int? orderDetailId)
        {
            LocationTransaction locationTransaction = new LocationTransaction();

            if (inventoryTransaction.Hu != null)
            {
                locationTransaction.HuId = inventoryTransaction.Hu.HuId;
                locationTransaction.LotNo = inventoryTransaction.Hu.LotNo;
            }
            if (locationTransaction.LotNo == null || locationTransaction.LotNo == string.Empty)
            {
                locationTransaction.LotNo = inventoryTransaction.LotNo;
            }
            locationTransaction.BatchNo = inventoryTransaction.LocationLotDetailId;
            locationTransaction.Item = inventoryTransaction.Item.Code;
            locationTransaction.ItemDescription = inventoryTransaction.Item.Description;
            locationTransaction.Uom = inventoryTransaction.Item.Uom.Code;
            locationTransaction.Qty = inventoryTransaction.Qty;
            locationTransaction.PartyFrom = inventoryTransaction.Location.Region.Code;
            locationTransaction.PartyFromName = inventoryTransaction.Location.Region.Name;
            locationTransaction.PartyTo = inventoryTransaction.Location.Region.Code;
            locationTransaction.PartyToName = inventoryTransaction.Location.Region.Name;
            locationTransaction.Location = inventoryTransaction.Location.Code;
            locationTransaction.LocationName = inventoryTransaction.Location.Name;
            if (inventoryTransaction.StorageBin != null)
            {
                locationTransaction.StorageArea = inventoryTransaction.StorageBin.Area.Code;
                locationTransaction.StorageAreaDescription = inventoryTransaction.StorageBin.Area.Description;
                locationTransaction.StorageBin = inventoryTransaction.StorageBin.Code;
                locationTransaction.StorageBinDescription = inventoryTransaction.StorageBin.Description;
            }
            locationTransaction.TransactionType = transType;
            locationTransaction.CreateDate = DateTime.Now;
            locationTransaction.CreateUser = user.Code;
            locationTransaction.EffectiveDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            locationTransaction.OrderNo = orderNo;
            locationTransaction.ReceiptNo = receiptNo;
            if (orderDetailId.HasValue)
            {
                locationTransaction.OrderDetailId = orderDetailId.Value;

            }
            if (refLocation != null)
            {
                locationTransaction.RefLocation = refLocation.Code;
                locationTransaction.RefLocationName = refLocation.Name;
            }
            else if (productLine != null)
            {
                locationTransaction.RefLocation = productLine.Code;
                locationTransaction.RefLocationName = productLine.Description;
            }

            this.CreateLocationTransaction(locationTransaction);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void RecordLocationTransaction(ProductLineInProcessLocationDetail productLineInProcessLocationDetail, string transType, User user, string ioType)
        {
            LocationTransaction locationTransaction = new LocationTransaction();

            locationTransaction.HuId = productLineInProcessLocationDetail.HuId;
            locationTransaction.LotNo = productLineInProcessLocationDetail.LotNo;
            //locationTransaction.BatchNo = inventoryTransaction.LocationLotDetailId;
            locationTransaction.Item = productLineInProcessLocationDetail.Item.Code;
            locationTransaction.ItemDescription = productLineInProcessLocationDetail.Item.Description;
            locationTransaction.Uom = productLineInProcessLocationDetail.Item.Uom.Code;
            if (transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_MATERIAL_IN)
            {
                locationTransaction.Qty = productLineInProcessLocationDetail.Qty;

                if (ioType == BusinessConstants.IO_TYPE_OUT)
                {
                    locationTransaction.RefLocation = productLineInProcessLocationDetail.ProductLine.Code;
                    locationTransaction.RefLocationName = productLineInProcessLocationDetail.ProductLine.Description;
                    locationTransaction.Location = productLineInProcessLocationDetail.LocationFrom.Code;
                    locationTransaction.LocationName = productLineInProcessLocationDetail.LocationFrom.Name;
                }
                else
                {
                    locationTransaction.Location = productLineInProcessLocationDetail.ProductLine.Code;
                    locationTransaction.LocationName = productLineInProcessLocationDetail.ProductLine.Description;
                    locationTransaction.RefLocation = productLineInProcessLocationDetail.LocationFrom.Code;
                    locationTransaction.RefLocationName = productLineInProcessLocationDetail.LocationFrom.Name;
                }
            }
            else if (transType == BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_MATERIAL_IN)
            {
                locationTransaction.Qty = 0 - productLineInProcessLocationDetail.CurrentBackflushQty;

                if (ioType == BusinessConstants.IO_TYPE_OUT)
                {
                    locationTransaction.Location = productLineInProcessLocationDetail.ProductLine.Code;
                    locationTransaction.LocationName = productLineInProcessLocationDetail.ProductLine.Description;
                    locationTransaction.RefLocation = productLineInProcessLocationDetail.LocationFrom.Code;
                    locationTransaction.RefLocationName = productLineInProcessLocationDetail.LocationFrom.Name;
                }
                else
                {
                    locationTransaction.RefLocation = productLineInProcessLocationDetail.ProductLine.Code;
                    locationTransaction.RefLocationName = productLineInProcessLocationDetail.ProductLine.Description;
                    locationTransaction.Location = productLineInProcessLocationDetail.LocationFrom.Code;
                    locationTransaction.LocationName = productLineInProcessLocationDetail.LocationFrom.Name;
                }
            }
            else
            {
                throw new TechnicalException("Invalided TransType: " + transType);
            }
            locationTransaction.PartyFrom = productLineInProcessLocationDetail.ProductLine.PartyFrom.Code;
            locationTransaction.PartyFromName = productLineInProcessLocationDetail.ProductLine.PartyFrom.Name;
            locationTransaction.PartyTo = productLineInProcessLocationDetail.ProductLine.PartyTo.Code;
            locationTransaction.PartyToName = productLineInProcessLocationDetail.ProductLine.PartyTo.Name;
            locationTransaction.TransactionType = transType;
            locationTransaction.CreateDate = DateTime.Now;
            locationTransaction.CreateUser = user.Code;
            locationTransaction.EffectiveDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CreateLocationTransaction(locationTransaction);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationTransaction> GetLocationTransactionAfterEffDate(string itemCode, string loc, DateTime effDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            if (itemCode != null && itemCode.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item", itemCode));
            if (loc != null && loc.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Location", loc));
            criteria.Add(Expression.Gt("EffectiveDate", effDate));

            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationTransaction> GetPeriodLocationTransaction(string itemCode, string loc, string transType, DateTime startDate, DateTime endDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            if (itemCode != null && itemCode.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item", itemCode));
            if (loc != null && loc.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Location", loc));
            if (transType != null && transType.Trim() != string.Empty)
                criteria.Add(Expression.Like("TransactionType", transType, MatchMode.Start));
            criteria.Add(Expression.Ge("EffectiveDate", startDate));
            criteria.Add(Expression.Le("EffectiveDate", endDate));

            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationTransaction> GetLocationTransaction(IList<string> itemList, IList<string> locList, DateTime? startDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            if (itemList != null && itemList.Count > 0)
            {
                if (itemList.Count == 1)
                {
                    criteria.Add(Expression.Eq("Item", itemList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("Item", itemList));
                }
            }
            if (locList != null && locList.Count > 0)
            {
                if (locList.Count == 1)
                {
                    criteria.Add(Expression.Eq("Location", locList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("Location", locList));
                }
            }
            if (startDate.HasValue)
                criteria.Add(Expression.Ge("EffectiveDate", startDate.Value));

            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<LocationTransaction> GetProjectionLocationTransaction(IList<string> itemList, IList<string> locList, DateTime? endDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            if (itemList != null && itemList.Count > 0)
            {
                if (itemList.Count == 1)
                {
                    criteria.Add(Expression.Eq("Item", itemList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("Item", itemList));
                }
            }
            if (locList != null && locList.Count > 0)
            {
                if (locList.Count == 1)
                {
                    criteria.Add(Expression.Eq("Location", locList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("Location", locList));
                }
            }
            if (endDate.HasValue)
                criteria.Add(Expression.Gt("EffectiveDate", endDate.Value));

            #region Projections
            ProjectionList projectionList = Projections.ProjectionList()
                .Add(Projections.Max("Id").As("Id"))
                .Add(Projections.Sum("Qty").As("Qty"))
                .Add(Projections.GroupProperty("Item").As("Item"))
                .Add(Projections.GroupProperty("Location").As("Location"));

            criteria.SetProjection(projectionList);
            criteria.SetResultTransformer(Transformers.AliasToBean(typeof(LocationTransaction)));
            #endregion

            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public override void CreateLocationTransaction(LocationTransaction loctrans)
        {
            Item item = this.itemMgr.LoadItem(loctrans.Item);
            if (!item.IsActive)
            {
                throw new BusinessErrorException("MasterData.LocTrans.Item.Inactive", item.Code);
            }
            base.CreateLocationTransaction(loctrans);
        }

        #endregion Customized Methods

        #region Private Methods
        private LocationTransaction GenerateOrderLocationTransaction(
            OrderLocationTransaction orderLocationTransaction, Location location, User createUser)
        {
            LocationTransaction locationTransaction = new LocationTransaction();

            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;

            locationTransaction.OrderNo = orderHead.OrderNo;
            locationTransaction.ExternalOrderNo = orderHead.ExternalOrderNo;
            locationTransaction.ReferenceOrderNo = orderHead.ReferenceOrderNo;
            locationTransaction.IsSubcontract = orderHead.IsSubcontract;
            locationTransaction.OrderDetailId = orderDetail.Id;
            locationTransaction.OrderLocationTransactionId = orderLocationTransaction.Id;
            //locationTransaction.IpNo = 
            //locationTransaction.ReceiptNo = 
            locationTransaction.TransactionType = orderLocationTransaction.TransactionType;
            locationTransaction.Item = orderLocationTransaction.Item.Code;
            locationTransaction.ItemDescription = orderLocationTransaction.Item.Description;
            locationTransaction.Uom = orderLocationTransaction.Item.Uom.Code;
            //locationTransaction.Qty = 
            locationTransaction.PartyFrom = orderHead.PartyFrom.Code;
            locationTransaction.PartyFromName = orderHead.PartyFrom.Name;
            locationTransaction.PartyTo = orderHead.PartyTo.Code;
            locationTransaction.PartyToName = orderHead.PartyTo.Name;
            locationTransaction.ShipFrom = orderHead.ShipFrom != null ? orderHead.ShipFrom.Code : null;
            locationTransaction.ShipFromAddress = orderHead.ShipFrom != null ? orderHead.ShipFrom.Address : null;
            locationTransaction.ShipTo = orderHead.ShipTo != null ? orderHead.ShipTo.Code : null;
            locationTransaction.ShipToAddress = orderHead.ShipTo != null ? orderHead.ShipTo.Address : null;
            locationTransaction.Location = location != null ? location.Code : null;
            locationTransaction.LocationName = location != null ? location.Name : null;
            locationTransaction.DockDescription = orderHead.DockDescription;
            locationTransaction.Carrier = orderHead.Carrier != null ? orderHead.Carrier.Code : null;
            locationTransaction.CarrierBillCode = orderHead.CarrierBillAddress != null ? orderHead.CarrierBillAddress.Code : null;
            locationTransaction.CarrierBillAddress = orderHead.CarrierBillAddress != null ? orderHead.CarrierBillAddress.Address : null;
            locationTransaction.CreateDate = DateTime.Now;
            locationTransaction.CreateUser = createUser.Code;

            return locationTransaction;
        }

        #endregion
    }
}