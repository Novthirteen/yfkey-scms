using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using com.Sconit.Utility;
using NHibernate.Transform;
using com.Sconit.Entity.Mes;



//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class OrderLocationTransactionMgr : OrderLocationTransactionBaseMgr, IOrderLocationTransactionMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IBomMgr bomMgr;
        private IBomDetailMgr bomDetailMgr;
        private IOrderOperationMgr orderOperationMgr;
        private IHuMgr huMgr;


        public OrderLocationTransactionMgr(IOrderLocationTransactionDao entityDao,
            ICriteriaMgr criteriaMgr,
            IUomConversionMgr uomConversionMgr,
            IBomMgr bomMgr,
            IBomDetailMgr bomDetailMgr,
            IOrderOperationMgr orderOperationMgr,
            IHuMgr huMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.bomMgr = bomMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.orderOperationMgr = orderOperationMgr;
            this.huMgr = huMgr;
        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<OrderLocationTransaction>();
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");

            criteria.Add(Expression.Eq("oh.OrderNo", orderNo));

            return criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(string orderNo, string ioType)
        {
            DetachedCriteria criteria = DetachedCriteria.For<OrderLocationTransaction>();
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");

            criteria.Add(Expression.Eq("oh.OrderNo", orderNo));
            criteria.Add(Expression.Eq("IOType", ioType));

            return criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(IList<string> orderNoList, string ioType)
        {
            IList<OrderLocationTransaction> orderLocTransList = new List<OrderLocationTransaction>();
            if (orderNoList != null && orderNoList.Count > 0)
            {
                foreach (string orderNo in orderNoList)
                {
                    IListHelper.AddRange<OrderLocationTransaction>(orderLocTransList,
                        this.GetOrderLocationTransaction(orderNo, BusinessConstants.IO_TYPE_OUT));
                }
            }
            return orderLocTransList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderHead orderHead)
        {
            return GetOrderLocationTransaction(orderHead.OrderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderHead orderHead, string ioType)
        {
            return GetOrderLocationTransaction(orderHead.OrderNo, ioType);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(int orderDetailId)
        {
            return GetOrderLocationTransaction(orderDetailId, null, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(int orderDetailId, string ioType)
        {
            return GetOrderLocationTransaction(orderDetailId, ioType, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(int orderDetailId, string ioType, string backflushMethod)
        {
            DetachedCriteria criteria = DetachedCriteria.For<OrderLocationTransaction>();
            criteria.CreateAlias("OrderDetail", "od");
            criteria.Add(Expression.Eq("od.Id", orderDetailId));

            if (ioType != null)
            {
                criteria.Add(Expression.Eq("IOType", ioType));
            }

            if (backflushMethod != null)
            {
                criteria.Add(Expression.Eq("BackFlushMethod", backflushMethod));
            }

            return criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderDetail orderDetail)
        {
            return GetOrderLocationTransaction(orderDetail.Id, null, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderDetail orderDetail, string ioType)
        {
            return GetOrderLocationTransaction(orderDetail.Id, ioType, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOrderLocationTransaction(OrderDetail orderDetail, string ioType, string backFlushMethod)
        {
            return GetOrderLocationTransaction(orderDetail.Id, ioType, backFlushMethod);
        }

        [Transaction(TransactionMode.Unspecified)]
        public OrderLocationTransaction GenerateOrderLocationTransaction(
            OrderDetail orderDetail, Item item, BomDetail bomDetail, Uom uom, int operation,
            string ioType, string transactionType, decimal unitQty, Location loc,
            bool isShipScanHu, int? huLotSize, bool needPrint, string backFlushMethod, string itemVersion, Location rejectLocation)
        {
            OrderLocationTransaction orderLocationTransaction = new OrderLocationTransaction();
            orderLocationTransaction.OrderDetail = orderDetail;
            orderLocationTransaction.Item = item;
            orderLocationTransaction.OrderedQty = unitQty * orderDetail.OrderedQty;   //����unitQty����ʵ�ʵĶ�����
            orderLocationTransaction.Uom = uom;
            orderLocationTransaction.Operation = operation;
            orderLocationTransaction.IOType = ioType;
            orderLocationTransaction.TransactionType = transactionType;
            orderLocationTransaction.UnitQty = unitQty;
            orderLocationTransaction.Location = loc;
            orderLocationTransaction.RejectLocation = rejectLocation;
            orderLocationTransaction.IsShipScanHu = isShipScanHu;  //��������Ч
            orderLocationTransaction.BackFlushMethod = backFlushMethod;  //�����س����Ϸ�ʽ
            orderLocationTransaction.ItemVersion = itemVersion;
            if (huLotSize.HasValue)
            {
                orderLocationTransaction.HuLotSize = int.Parse(Math.Round(huLotSize.Value * unitQty).ToString("0.#########"));

            }
            orderLocationTransaction.NeedPrint = needPrint;
            orderLocationTransaction.IsAssemble = true;   //Ĭ�϶���װ
            if (bomDetail != null)
            {
                orderLocationTransaction.BomDetail = bomDetail;
                if (bomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O)
                {
                    //�����ѡװ����ȡbomDetail.Priority��Ϊѡװ����Ĭ��ֵ��0����Ĭ�ϲ�װ��1����Ĭ�ϰ�װ
                    orderLocationTransaction.IsAssemble = (bomDetail.Priority != 0);
                }
                #region ȡ��λ�ͻ���
               
                DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
                criteria.Add(Expression.Eq("Code", orderDetail.OrderHead.Flow));

                IList<Flow> flowList = criteriaMgr.FindAll<Flow>(criteria);
                if (flowList != null && flowList.Count > 0)
                {
                    if (flowList[0].Routing != null)
                    {
                        #region ��routingdet
                        DetachedCriteria rcriteria = DetachedCriteria.For(typeof(RoutingDetail));
                        rcriteria.Add(Expression.Eq("Routing.Code", flowList[0].Routing.Code));
                        rcriteria.Add(Expression.Eq("Operation", bomDetail.Operation));

                        IList<RoutingDetail> routingDetailList = criteriaMgr.FindAll<RoutingDetail>(rcriteria);
                        if (routingDetailList != null && routingDetailList.Count > 0)
                        {
                            if (routingDetailList[0].TagNo != null && routingDetailList[0].TagNo != string.Empty)
                            {
                                orderLocationTransaction.TagNo = routingDetailList[0].TagNo;

                                #region �Ҷ�Ӧ�Ĺ�λ����
                                DetachedCriteria scriteria = DetachedCriteria.For(typeof(ShelfItem));
                                scriteria.CreateAlias("Shelf", "s");
                                scriteria.Add(Expression.Eq("s.TagNo", orderLocationTransaction.TagNo));
                                scriteria.Add(Expression.Eq("IsActive", true));
                                scriteria.Add(Expression.Eq("Item.Code", orderLocationTransaction.Item.Code));
                                IList<ShelfItem> shelfItemDetailList = criteriaMgr.FindAll<ShelfItem>(scriteria);
                                if (shelfItemDetailList != null && shelfItemDetailList.Count > 0)
                                {
                                    orderLocationTransaction.Shelf = shelfItemDetailList[0].Shelf.Code;
                                }
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
                #endregion
            }
            //����
            orderLocationTransaction.Cartons = Convert.ToInt32(orderLocationTransaction.OrderedQty / orderLocationTransaction.Item.UnitCount);

            if (uom.Code != item.Uom.Code)   //�Ϳ�浥λ��һ�£���Ҫ����ת��
            {
                //��λת��������UnitQty��OrderedQtyֵ
                orderLocationTransaction.Uom = item.Uom;
                orderLocationTransaction.UnitQty = this.uomConversionMgr.ConvertUomQty(item, uom, unitQty, item.Uom);
                orderLocationTransaction.OrderedQty = this.uomConversionMgr.ConvertUomQty(item, uom, orderLocationTransaction.OrderedQty, item.Uom);

                if (orderLocationTransaction.HuLotSize.HasValue)
                {
                    orderLocationTransaction.HuLotSize = int.Parse(Math.Round(orderLocationTransaction.HuLotSize.Value * orderLocationTransaction.UnitQty).ToString("#"));
                }
            }

            orderDetail.AddOrderLocationTransaction(orderLocationTransaction);
            return orderLocationTransaction;
        }

        [Transaction(TransactionMode.Requires)]
        public void AutoReplaceAbstractItem(OrderLocationTransaction orderLocationTransaction)
        {
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
            //OrderOperation orderOperation = orderHead.GetOrderOperationByOperation(orderLocationTransaction.Operation.Value);

            //ȡ�ó�������Ӽ�
            BomDetail bomDetail = this.bomDetailMgr.GetDefaultBomDetailForAbstractItem(
                orderLocationTransaction.Item, orderHead.Routing, orderHead.StartTime,
                orderLocationTransaction.OrderDetail.DefaultLocationFrom);

            if (bomDetail != null)
            {
                //ɾ�������
                this.DeleteOrderLocationTransaction(orderLocationTransaction);
                orderDetail.RemoveOrderLocationTransaction(orderLocationTransaction);
                if (orderLocationTransaction.Operation != 0)
                {
                    //ɾ����Ӧ��OrderOp
                    this.orderOperationMgr.TryDeleteOrderOperation(orderHead, orderLocationTransaction.Operation);
                }

                //���Էֽ��Ӽ�
                string bomCode = this.bomMgr.FindBomCode(bomDetail.Item);
                IList<BomDetail> bomDetailList = this.bomDetailMgr.GetFlatBomDetail(bomCode, orderHead.StartTime);
                if (bomDetailList != null && bomDetailList.Count > 0)
                {
                    //�Ӽ���Bom
                    foreach (BomDetail subBomDetail in bomDetailList)
                    {
                        //ѭ�������Ӽ���Bom
                        OrderLocationTransaction newOrderLocationTransaction = this.AddNewMaterial(orderDetail, subBomDetail, orderLocationTransaction.Location, orderLocationTransaction.OrderedQty);
                        if (newOrderLocationTransaction != null &&
                            newOrderLocationTransaction.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_A)
                        {
                            //����Ӽ���Bom�������������Ƕ�׵����滻�����������
                            this.AutoReplaceAbstractItem(newOrderLocationTransaction);
                        }
                    }
                }
                else
                {
                    //�Ӽ�û��Bom�����Ӽ�ֱ���滻�����
                    this.AddNewMaterial(orderDetail, bomDetail, orderLocationTransaction.Location, orderLocationTransaction.OrderedQty);
                }
            }
            else
            {
                throw new BusinessErrorException("Bom.Error.NotFoundForItem", orderLocationTransaction.Item.Code);
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ReplaceAbstractItem(OrderLocationTransaction orderLocationTransaction, BomDetail bomDetail)
        {
            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;

            //ɾ�������
            this.DeleteOrderLocationTransaction(orderLocationTransaction);
            orderDetail.RemoveOrderLocationTransaction(orderLocationTransaction);
            if (orderLocationTransaction.Operation != 0)
            {
                //ɾ����Ӧ��OrderOp
                this.orderOperationMgr.TryDeleteOrderOperation(orderHead, orderLocationTransaction.Operation);
            }

            //���Էֽ��Ӽ�
            string bomCode = this.bomMgr.FindBomCode(bomDetail.Item);
            IList<BomDetail> bomDetailList = this.bomDetailMgr.GetFlatBomDetail(bomCode, orderHead.StartTime);
            if (bomDetailList != null && bomDetailList.Count > 0)
            {
                //�Ӽ���Bom
                foreach (BomDetail subBomDetail in bomDetailList)
                {
                    //ѭ�������Ӽ���Bom
                    OrderLocationTransaction newOrderLocationTransaction = this.AddNewMaterial(orderDetail, subBomDetail, orderLocationTransaction.Location, orderLocationTransaction.OrderedQty);
                    if (newOrderLocationTransaction != null &&
                        newOrderLocationTransaction.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_A)
                    {
                        //����Ӽ���Bom�������������Ƕ�׵����滻�����������
                        this.AutoReplaceAbstractItem(newOrderLocationTransaction);
                    }
                }
            }
            else
            {
                //�Ӽ�û��Bom�����Ӽ�ֱ���滻�����
                this.AddNewMaterial(orderDetail, bomDetail, orderLocationTransaction.Location, orderLocationTransaction.OrderedQty);
            }

        }

        [Transaction(TransactionMode.Requires)]
        public OrderLocationTransaction AddNewMaterial(OrderDetail orderDetail, BomDetail bomDetail, Location orgLocation, decimal orgOrderedQty)
        {
            //�����ѡװ������Ĭ�ϲ���װ�������������
            if (bomDetail.StructureType != BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O || bomDetail.Priority != 0)
            {
                foreach (OrderLocationTransaction orderLocationTransaction in orderDetail.OrderLocationTransactions)
                {
                    if (orderLocationTransaction.Item.Code == bomDetail.Item.Code
                        && orderLocationTransaction.Operation == bomDetail.Operation)
                    {
                        //�ϲ���ͬ����
                        //decimal orderedQty = orderDetail.OrderedQty * orgOrderedQty * bomDetail.RateQty * (1 + bomDetail.ScrapPercentage);
                        decimal orderedQty = orgOrderedQty * bomDetail.RateQty * (1 + bomDetail.ScrapPercentage);
                        if (orderLocationTransaction.Uom.Code != bomDetail.Uom.Code)
                        {
                            orderedQty = this.uomConversionMgr.ConvertUomQty(orderLocationTransaction.Item.Code, bomDetail.Uom, orderedQty, orderLocationTransaction.Uom);
                        }
                        orderLocationTransaction.UnitQty += orgOrderedQty * bomDetail.RateQty * (1 + bomDetail.ScrapPercentage);
                        orderLocationTransaction.OrderedQty += orderedQty;
                        this.UpdateOrderLocationTransaction(orderLocationTransaction);

                        return orderLocationTransaction;
                    }
                }

                Location bomLocFrom = bomDetail.Location != null ? bomDetail.Location : orgLocation;
                OrderLocationTransaction newOrderLocationTransaction =
                    this.GenerateOrderLocationTransaction(orderDetail, bomDetail.Item, bomDetail,
                                                        bomDetail.Uom, bomDetail.Operation, BusinessConstants.IO_TYPE_OUT, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO,
                                                        orgOrderedQty * bomDetail.RateQty * (1 + bomDetail.ScrapPercentage), bomLocFrom,
                                                        bomDetail.IsShipScanHu, bomDetail.HuLotSize, bomDetail.NeedPrint, bomDetail.BackFlushMethod, null, null);

                this.CreateOrderLocationTransaction(newOrderLocationTransaction);
                orderDetail.AddOrderLocationTransaction(newOrderLocationTransaction);
                this.orderOperationMgr.TryAddOrderOperation(orderDetail.OrderHead, bomDetail.Operation, bomDetail.Reference);

                return newOrderLocationTransaction;
            }

            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOpenOrderLocTransIn(string item, string location, string IOType, DateTime? winTime)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction), "orderLocTrans");
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            if (winTime.HasValue)
                criteria.Add(Expression.Le("oh.WindowTime", (DateTime)winTime));
            criteria.Add(Expression.Eq("orderLocTrans.Item.Code", item));
            criteria.Add(Expression.Eq("orderLocTrans.Location.Code", location));
            criteria.Add(Expression.Eq("orderLocTrans.IOType", IOType));
            criteria.Add(Expression.Or(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT),
                Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)));
            IList<OrderLocationTransaction> orderLocTransList = criteriaMgr.FindAll<OrderLocationTransaction>(criteria);

            return orderLocTransList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOpenOrderLocTransOut(string item, string location, string IOType, DateTime? startTime)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction), "orderLocTrans");
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            if (startTime.HasValue)
                criteria.Add(Expression.Le("oh.StartTime", (DateTime)startTime));
            criteria.Add(Expression.Eq("orderLocTrans.Item.Code", item));
            criteria.Add(Expression.Eq("orderLocTrans.Location.Code", location));
            criteria.Add(Expression.Eq("orderLocTrans.IOType", IOType));
            criteria.Add(Expression.Or(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT),
                Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)));
            IList<OrderLocationTransaction> orderLocTransList = criteriaMgr.FindAll<OrderLocationTransaction>(criteria);

            return orderLocTransList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetPairOrderLocTrans(int orderDetId, string item, string IOType)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction), "orderLocTrans");
            criteria.CreateAlias("OrderDetail", "od");
            criteria.Add(Expression.Eq("orderLocTrans.OrderDetail.Id", orderDetId));
            criteria.Add(Expression.Eq("orderLocTrans.Item.Code", item));
            criteria.Add(Expression.Eq("orderLocTrans.IOType", IOType));
            IList<OrderLocationTransaction> orderLocTransList = criteriaMgr.FindAll<OrderLocationTransaction>(criteria);

            return orderLocTransList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderLocationTransaction> GetOpenOrderLocationTransaction(IList<string> itemList, IList<string> locList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            OrderHelper.SetOpenOrderStatusCriteria(criteria, "oh.Status");
            if (itemList != null && itemList.Count > 0)
            {
                if (itemList.Count == 1)
                {
                    criteria.Add(Expression.Eq("Item.Code", itemList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("Item.Code", itemList));
                }
            }
            if (locList != null && locList.Count > 0)
            {
                if (locList.Count == 1)
                {
                    criteria.Add(Expression.Eq("Location.Code", locList[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<string>("Location.Code", locList));
                }
            }
            #region Projections
            ProjectionList projectionList = Projections.ProjectionList()
                .Add(Projections.Max("Id").As("Id"))
                .Add(Projections.Sum("OrderedQty").As("OrderedQty"))
                .Add(Projections.Sum("AccumulateQty").As("AccumulateQty"))
                .Add(Projections.GroupProperty("IOType").As("IOType"))
                .Add(Projections.GroupProperty("Item").As("Item"))
                .Add(Projections.GroupProperty("Location").As("Location"));

            criteria.SetProjection(projectionList);
            criteria.SetResultTransformer(Transformers.AliasToBean(typeof(OrderLocationTransaction)));
            #endregion

            return criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public OrderLocationTransaction GetOrderLocationTransaction(string orderNo, string tagNo, string itemCode)
        {
            return GetOrderLocationTransaction(orderNo, tagNo, itemCode, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public OrderLocationTransaction GetOrderLocationTransaction(string orderNo, string tagNo, string itemCode, string status)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            criteria.Add(Expression.Eq("oh.OrderNo", orderNo));
            if (status != null && status != string.Empty)
            {
                criteria.Add(Expression.Eq("oh.Status", status));
            }
            criteria.Add(Expression.Eq("TagNo", tagNo));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.Add(Expression.Eq("TransactionType", BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO));
            IList<OrderLocationTransaction> orderLocTransList = criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
            return orderLocTransList.Count > 0 ? orderLocTransList[0] : null;
        }

        #endregion Customized Methods
    }
}