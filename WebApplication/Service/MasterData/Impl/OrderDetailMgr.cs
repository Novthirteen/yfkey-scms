using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.Distribution;


//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class OrderDetailMgr : OrderDetailBaseMgr, IOrderDetailMgr
    {
        private string[] FlowDetail2OrderDetailCloneFields = new string[] 
            { 
                "Item",
                "ReferenceItemCode",
                "Uom",
                "UnitCount",
                "GoodsReceiptLotSize",
                "BatchSize",
                "Bom",
                "LocationFrom",
                "LocationTo",
                "BillFrom",
                "BillTo",
                "PriceListFrom",
                "PriceListTo",
                "HuLotSize",
                "BillSettleTerm",
                "Customer",
                "PackageVolumn",
                "PackageType",
                "NeedInspection",
                "IdMark",
                "BarCodeType",
                "OddShipOption",
                "CustomerItemCode"
            };

        private string[] OrderDetailOfChildKitCloneFields = new string[] 
            { 
                "FlowDetail", 
                "OrderHead", 
                "RequiredQty",
                "OrderedQty",
                "GoodsReceiptLotSize",
                "BatchSize",
                "LocationFrom",
                "LocationTo",
                "BillFrom",
                "BillTo",
                "PriceListFrom",
                "PriceListTo",
                "HuLotSize",
                "BillSettleTerm",
                "Customer",
                "PackageVolumn",
                "PackageType",
                "NeedInspection",
                "IdMark",
                "BarCodeType",
                "OddShipOption",
                "CustomerItemCode"
            };

        private string[] ReferenceOrderDetailCloneFields = new string[] 
            { 
                "Item",
                "ReferenceItemCode",
                "Uom",
                "UnitCount",
                "GoodsReceiptLotSize",
                "BatchSize"
            };

        private ICriteriaMgr criteriaMgr;
        private IUomConversionMgr uomConversionMgr;
        private IBomMgr bomMgr;
        private IBomDetailMgr bomDetailMgr;
        private IRoutingDetailMgr routingDetailMgr;
        private IOrderOperationMgr orderOperationMgr;
        private IOrderLocationTransactionMgr orderLocationTransactionMgr;
        private IItemKitMgr itemKitMgr;
        private IItemReferenceMgr itemReferenceMgr;
        private ILocationMgr locationMgr;
        private ILocationDetailMgr LocationDetailMgr;
        private IPriceListDetailMgr priceListDetailMgr;
        private IEntityPreferenceMgr entityPreferenceMgr;
        private IFlowMgr flowMgr;
  

        public OrderDetailMgr(ICriteriaMgr criteriaMgr,
            IOrderDetailDao entityDao,
            IUomConversionMgr uomConversionMgr,
            IBomMgr bomMgr,
            IBomDetailMgr bomDetailMgr,
            IRoutingDetailMgr routingDetailMgr,
            IOrderOperationMgr orderOperationMgr,
            IOrderLocationTransactionMgr orderLocationTransactionMgr,
            IItemKitMgr itemKitMgr,
            IItemReferenceMgr itemReferenceMgr,
            ILocationMgr locationMgr,
            ILocationDetailMgr LocationDetailMgr,
            IPriceListDetailMgr priceListDetailMgr,
            IEntityPreferenceMgr entityPreferenceMgr,
            IFlowMgr flowMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.bomMgr = bomMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.routingDetailMgr = routingDetailMgr;
            this.orderOperationMgr = orderOperationMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.itemKitMgr = itemKitMgr;
            this.itemReferenceMgr = itemReferenceMgr;
            this.locationMgr = locationMgr;
            this.LocationDetailMgr = LocationDetailMgr;
            this.priceListDetailMgr = priceListDetailMgr;
            this.entityPreferenceMgr = entityPreferenceMgr;
            this.flowMgr = flowMgr;

        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public OrderDetail LoadOrderDetail(int id, bool includeLocTrans)
        {
            OrderDetail orderDetail = this.LoadOrderDetail(id);

            if (orderDetail != null)
            {
                if (includeLocTrans && orderDetail.OrderLocationTransactions != null && orderDetail.OrderLocationTransactions.Count > 0)
                {
                }
            }

            return orderDetail;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderDetail> GenerateOrderDetail(OrderHead orderHead, FlowDetail flowDetail)
        {
            return GenerateOrderDetail(orderHead, flowDetail, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderDetail> GenerateOrderDetail(OrderHead orderHead, FlowDetail flowDetail, bool isReferencedFlow)
        {
            EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL);
            int seqInterval = int.Parse(entityPreference.Value);

            IList<OrderDetail> orderDetailList = new List<OrderDetail>();
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.FlowDetail = flowDetail;
            orderDetail.OrderHead = orderHead;
            if (!isReferencedFlow)
            {
                CloneHelper.CopyProperty(flowDetail, orderDetail, FlowDetail2OrderDetailCloneFields);
            }
            else
            {
                CloneHelper.CopyProperty(flowDetail, orderDetail, ReferenceOrderDetailCloneFields);
            }

            #region ���Ҽ۸�
            if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                if (orderDetail.DefaultPriceListFrom != null)
                {
                    PriceListDetail priceListDetailFrom = priceListDetailMgr.GetLastestPriceListDetail(orderDetail.DefaultPriceListFrom, orderDetail.Item, orderHead.StartTime, orderHead.Currency, orderDetail.Uom);
                    if (priceListDetailFrom != null)
                    {
                        orderDetail.PriceListDetailFrom = priceListDetailFrom;
                    }
                }
                if (orderDetail.DefaultPriceListTo != null)
                {
                    PriceListDetail priceListDetailTo = priceListDetailMgr.GetLastestPriceListDetail(orderDetail.DefaultPriceListTo, orderDetail.Item, orderHead.StartTime, orderHead.Currency, orderDetail.Uom);
                    if (priceListDetailTo != null)
                    {
                        orderDetail.PriceListDetailTo = priceListDetailTo;
                    }
                }
            }
            #endregion

            #region �����˻��ʹ�Ʒ��λ
            //if (orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN
            //    || orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO)
            //{
            //    if (flowDetail.LocationFrom != null && flowDetail.LocationFrom.ActingLocation != null)
            //    {
            //        orderDetail.LocationFrom = flowDetail.LocationFrom.ActingLocation;
            //    }

            //    if (flowDetail.LocationTo != null && flowDetail.LocationTo.ActingLocation != null)
            //    {
            //        orderDetail.LocationTo = flowDetail.LocationTo.ActingLocation;
            //    }
            //}
            if (orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO)
            {
                orderDetail.LocationTo = this.locationMgr.GetRejectLocation();
            }
            #endregion

            //if (orderDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
            //{
            //    #region �����׼�
            //    IList<ItemKit> itemKitList = this.itemKitMgr.GetChildItemKit(orderDetail.Item.Code);

            //    if (itemKitList != null && itemKitList.Count > 0)
            //    {
            //        int count = 0;
            //        decimal? convertRate = null;
            //        foreach (ItemKit itemKit in itemKitList)
            //        {
            //            count++;

            //            if (!convertRate.HasValue)
            //            {
            //                if (itemKit.ParentItem.Uom.Code != orderDetail.Uom.Code)
            //                {
            //                    convertRate = this.uomConversionMgr.ConvertUomQty(orderDetail.Item, orderDetail.Uom, 1, itemKit.ParentItem.Uom);
            //                }
            //                else
            //                {
            //                    convertRate = 1;
            //                }
            //            }

            //            OrderDetail orderDetailOfChildKit = new OrderDetail();
            //            CloneHelper.CopyProperty(orderDetail, orderDetailOfChildKit, OrderDetailOfChildKitCloneFields);
            //            orderDetailOfChildKit.Sequence = orderDetailOfChildKit.Sequence + count;
            //            orderDetailOfChildKit.Item = itemKit.ChildItem;
            //            orderDetailOfChildKit.ReferenceItemCode = this.itemReferenceMgr.GetItemReferenceByItem(itemKit.ChildItem.Code, orderDetail.OrderHead.PartyFrom.Code, orderDetail.OrderHead.PartyTo.Code);
            //            orderDetailOfChildKit.Uom = itemKit.ChildItem.Uom;
            //            orderDetailOfChildKit.UnitCount = itemKit.ChildItem.UnitCount;
            //            //orderDetailOfChildKit.RequiredQty = orderDetailOfChildKit.RequiredQty * itemKit.Qty * convertRate;
            //            //orderDetailOfChildKit.OrderedQty = orderDetailOfChildKit.OrderedQty * itemKit.Qty * convertRate;
            //            orderDetailOfChildKit.GoodsReceiptLotSize = orderDetailOfChildKit.GoodsReceiptLotSize * itemKit.Qty * convertRate;
            //            orderDetailOfChildKit.BatchSize = orderDetailOfChildKit.BatchSize * itemKit.Qty * convertRate;
            //            if (orderDetailOfChildKit.HuLotSize.HasValue)
            //            {
            //                orderDetailOfChildKit.HuLotSize = int.Parse((orderDetailOfChildKit.HuLotSize.Value * itemKit.Qty * convertRate.Value).ToString("#"));
            //            }

            //            #region ����۸�
            //            if (orderDetailOfChildKit.DefaultPriceListFrom != null)
            //            {
            //                PriceListDetail priceListDetailFrom = priceListDetailMgr.GetLastestPriceListDetail(orderDetailOfChildKit.DefaultPriceListFrom, orderDetailOfChildKit.Item, orderHead.StartTime, orderHead.Currency, orderDetailOfChildKit.Uom);
            //                if (priceListDetailFrom != null)
            //                {
            //                    orderDetailOfChildKit.PriceListDetailFrom = priceListDetailFrom;
            //                }
            //            }
            //            if (orderDetailOfChildKit.DefaultPriceListTo != null)
            //            {
            //                PriceListDetail priceListDetailTo = priceListDetailMgr.GetLastestPriceListDetail(orderDetailOfChildKit.DefaultPriceListTo, orderDetailOfChildKit.Item, orderHead.StartTime, orderHead.Currency, orderDetailOfChildKit.Uom);
            //                if (priceListDetailTo != null)
            //                {
            //                    orderDetailOfChildKit.PriceListDetailTo = priceListDetailTo;
            //                }
            //            }
            //            #endregion

            //            //��������Sequence
            //            int detailCount = orderHead.OrderDetails != null ? orderHead.OrderDetails.Count : 0;
            //            orderDetail.Sequence = (detailCount + 1) * seqInterval;

            //            orderHead.AddOrderDetail(orderDetailOfChildKit);
            //            orderDetailList.Add(orderDetailOfChildKit);
            //        }
            //    }
            //    else
            //    {
            //        throw new BusinessErrorException("ItemKit.Error.NotFoundForParentItem", orderDetail.Item.Code);
            //    }
            //    #endregion
            //}
            //else
            //{
            //��������Sequence
            int detailCount = orderHead.OrderDetails != null ? orderHead.OrderDetails.Count : 0;
            orderDetail.Sequence = (detailCount + 1) * seqInterval;

            #region �ο������
            if (orderDetail.ReferenceItemCode == null || orderDetail.ReferenceItemCode == string.Empty)
            {
                string firstPartyCode = string.Empty;
                string secondPartyCode = string.Empty;
                if (orderHead.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
                {
                    firstPartyCode = orderHead.PartyTo.Code;
                }
                else
                {
                    firstPartyCode = orderHead.PartyFrom.Code;
                }
                orderDetail.ReferenceItemCode = itemReferenceMgr.GetItemReferenceByItem(orderDetail.Item.Code, firstPartyCode, secondPartyCode);
            }
            #endregion

            #region ����Ĭ�Ͽ�λ
            orderDetail.LocationFrom = orderDetail.LocationFrom == null ? orderHead.LocationFrom : orderDetail.LocationFrom;
            orderDetail.LocationTo = orderDetail.LocationTo == null ? orderHead.LocationTo : orderDetail.LocationTo;
            #endregion

            orderHead.AddOrderDetail(orderDetail);
            orderDetailList.Add(orderDetail);
            //}

            return orderDetailList;
        }

        //����OrderLocationTransaction��OrderOperation
        [Transaction(TransactionMode.Unspecified)]
        public void GenerateOrderDetailSubsidiary(OrderDetail orderDetail)
        {
            OrderHead orderHead = orderDetail.OrderHead;
            int maxOp = 0;    //��¼�����ţ�����Ʒ�ջ�ʱ��
            int minOp = 0;    //��¼��С����ţ���������Ʒ��

            #region ��OrderDetail���ջ���λ�͵�λ����ת��ΪBOM��λ�͵�λ����
            //fgUom��fgUnityQty�������һ��orderDetail.Uom��λ(���ڶ������ջ���λ)��FG�����ڵ�λ(fgUom)�ж���(fgUnityQty)ֵ
            Uom fgUom = orderDetail.Uom;
            decimal fgUnityQty = 1;     //������Ʒ�������ĳ�ƷUnitQtyĬ��Ϊ1
            //�����Bom�ϵĵ�λ��һ�£�ת��ΪBom�ϵĵ�λ����Ȼ�ᵼ�����ϻس岻��ȷ��            
            if (orderDetail.Bom != null && orderDetail.Uom.Code.ToUpper() != orderDetail.Bom.Uom.Code.ToUpper())
            {
                fgUom = orderDetail.Bom.Uom;
                fgUnityQty = this.uomConversionMgr.ConvertUomQty(orderDetail.Item, orderDetail.Uom, fgUnityQty, fgUom);
            }
            #endregion

            #region ����OrderLocTrans

            if (orderDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
            {
                //����һ�㲻�����е�����׼��Ĳ�ֶ���ǰ̨����
                //�û����¶�����ʱ���Ѿ�������׼�
                //2010-1-19 dingxin
                throw new BusinessErrorException("Order.Error.CreateOrder.ItemTypeK", orderDetail.Item.Code);
            }
            else
            {
                if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                    || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS
                    || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
                {
                    #region �ɹ���ֻ��Ҫ��¼�������RCT-PO
                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                                fgUom, maxOp, BusinessConstants.IO_TYPE_OUT, null,
                                fgUnityQty, orderDetail.DefaultLocationFrom,
                                false, orderDetail.HuLotSize, true, null, orderDetail.ItemVersion, this.locationMgr.GetRejectLocation());

                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                                fgUom, maxOp, BusinessConstants.IO_TYPE_IN, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PO,
                                fgUnityQty, orderDetail.DefaultLocationTo,
                                false, orderDetail.HuLotSize, true, null, orderDetail.ItemVersion, this.locationMgr.GetRejectLocation());
                    #endregion
                }
                else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                {
                    #region ���ۣ�ֻ��Ҫ��¼��������ISS-SO
                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                                fgUom, maxOp, BusinessConstants.IO_TYPE_OUT, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO,
                                fgUnityQty, orderDetail.DefaultLocationFrom,
                                false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());

                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                                fgUom, maxOp, BusinessConstants.IO_TYPE_IN, null,
                                fgUnityQty, orderDetail.DefaultLocationTo,
                                false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());
                    #endregion
                }
                else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                {
                    #region �ƿ⣬��Ҫ��¼��������ISS-TR���������RCT-TR
                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                                fgUom, maxOp, BusinessConstants.IO_TYPE_OUT, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR,
                                fgUnityQty, orderDetail.DefaultLocationFrom,
                                false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());

                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                                fgUom, maxOp, BusinessConstants.IO_TYPE_IN, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR,
                                fgUnityQty, orderDetail.DefaultLocationTo,
                                false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());
                    #endregion
                }
                //else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_INSPECTION)
                //{
                //    #region ���飬��Ҫ��¼��������ISS-TR���������RCT-TR
                //    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                //                fgUom, maxOp, BusinessConstants.IO_TYPE_OUT, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR,
                //                fgUnityQty, orderDetail.DefaultLocationFrom,
                //                false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());

                //    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                //                fgUom, maxOp, BusinessConstants.IO_TYPE_IN, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR,
                //                fgUnityQty, orderDetail.DefaultLocationTo,
                //                false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());
                //    #endregion
                //}
                else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    #region �����������ϵ�OrderLocTrans
                    //�������������������Ҫ��Bom
                    FillBomForOrderDetail(orderDetail);

                    IList<BomDetail> bomDetailList = this.bomDetailMgr.GetFlatBomDetail(orderDetail.Bom.Code, orderHead.StartTime);
                    foreach (BomDetail bomDetail in bomDetailList)
                    {
                        #region ��¼������
                        //�������Ǵ�Bom��ȡ���Ǵ�Routing��ȡ��
                        if (maxOp < bomDetail.Operation)
                        {
                            //��¼������
                            maxOp = bomDetail.Operation;
                        }
                        #endregion

                        #region ��¼��С�����
                        //��С������Ǵ�Bom��ȡ���Ǵ�Routing��ȡ��
                        if (minOp > bomDetail.Operation || minOp == 0)
                        {
                            //��¼������
                            minOp = bomDetail.Operation;
                        }
                        #endregion

                        #region �������ϵ���Դ��λ
                        //��Դ��λ�����߼�BomDetail-->RoutingDetail-->FlowDetail-->Flow
                        Location bomLocFrom = bomDetail.Location;

                        if (orderHead.Routing != null)
                        {
                            //��Routing�ϲ��ң�������Routing�ϵĹ����BOM�ϵ��Ƿ�ƥ��
                            RoutingDetail routingDetail = routingDetailMgr.LoadRoutingDetail(orderHead.Routing, bomDetail.Operation, bomDetail.Reference);
                            if (routingDetail != null)
                            {
                                if (bomLocFrom == null)
                                {
                                    bomLocFrom = routingDetail.Location;
                                }

                                //if (maxOp < routingDetail.Operation)
                                //{
                                //    //��¼������
                                //    maxOp = routingDetail.Operation;
                                //}

                                orderHead.AddOrderOperation(this.orderOperationMgr.GenerateOrderOperation(orderHead, routingDetail));
                            }
                            //else
                            //{
                            //    //û���ҵ���BOM����ƥ��Ĺ���
                            //    throw new BusinessErrorException("Order.Error.OpNotMatch", bomDetail.Bom.Code, bomDetail.Item.Code, routing.Code, bomDetail.Operation.ToString(), bomDetail.Reference);
                            //}
                        }

                        if (bomLocFrom == null)
                        {
                            //ȡĬ�Ͽ�λFlowDetail-->Flow
                            bomLocFrom = orderDetail.DefaultLocationFrom;
                        }
                        //string bomLocFromType = bomLocFrom != null ? bomLocFrom.Type : null;
                        #endregion

                        #region �������ϵ�Ŀ�Ŀ�λ
                        //Ŀ�Ŀ�λ��������������ͣ�ֱ����ΪNull���������FlowDetail-->Flow
                        Location bomLocTo = (orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION) ?
                            orderDetail.DefaultLocationTo : null;
                        //string bomLocToType = bomLocTo != null ? bomLocTo.Type : null;
                        #endregion

                        #region �������ϣ�ֻ��Ҫ��¼��������ISS-TR
                        this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, bomDetail.Item, bomDetail,
                                   bomDetail.Uom, bomDetail.Operation, BusinessConstants.IO_TYPE_OUT, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO,
                                   bomDetail.CalculatedQty * fgUnityQty,   //������ԭ��������Ĭ�ϵ���0
                                   bomLocFrom, bomDetail.IsShipScanHu, bomDetail.HuLotSize, bomDetail.NeedPrint, bomDetail.BackFlushMethod, null, this.locationMgr.GetRejectLocation());
                        #endregion
                    }
                    #endregion

                    #region ������Ʒ��ֻ��Ҫ��¼�������RCT-WO
                    this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                        fgUom, maxOp, BusinessConstants.IO_TYPE_IN, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO,
                                fgUnityQty, orderDetail.DefaultLocationTo,
                                false, orderDetail.HuLotSize, true, null, orderDetail.ItemVersion, this.locationMgr.GetRejectLocation());

                    //���������Լ���ӵ�������
                    if (orderHead.SubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO)
                    {
                        //Location loc = orderDetail.DefaultLocationTo.ActingLocation != null ? orderDetail.DefaultLocationTo.ActingLocation : orderDetail.DefaultLocationTo;
                        Location loc = this.locationMgr.GetRejectLocation();


                        //todo �������ĳ�Ʒ�Ƿ���Ҫɨ��Hu�����ڲ�ɨ��
                        //�����Գ�Ʒ��Ͷ�ϼ�RCT-WO����
                        this.orderLocationTransactionMgr.GenerateOrderLocationTransaction(orderDetail, orderDetail.Item, null,
                          fgUom, minOp, BusinessConstants.IO_TYPE_OUT, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO,
                            fgUnityQty, loc,
                         false, orderDetail.HuLotSize, true, null, null, this.locationMgr.GetRejectLocation());
                    }
                    #endregion
                }
            }
            #endregion

            #region ��������û��Op��OrderLocTrans��ֵmaxOp
            if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                if (maxOp == 0)
                {
                    EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL);
                    int seqInterval = int.Parse(entityPreference.Value);
                    maxOp = seqInterval; //Ĭ�Ϲ����
                }

                if (orderDetail.OrderLocationTransactions != null && orderDetail.OrderLocationTransactions.Count > 0)
                {
                    foreach (OrderLocationTransaction orderLocationTransaction in orderDetail.OrderLocationTransactions)
                    {
                        if (orderLocationTransaction.Operation == 0)
                        {
                            orderLocationTransaction.Operation = maxOp;
                        }
                    }
                }
            }
            #endregion
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderDetail> GetOrderDetail(string orderNo)
        {
            DetachedCriteria criteria = DetachedCriteria.For<OrderDetail>();
            criteria.Add(Expression.Eq("OrderHead.OrderNo", orderNo));
            return this.criteriaMgr.FindAll<OrderDetail>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<OrderDetail> GetOrderDetail(OrderHead orderHead)
        {
            return GetOrderDetail(orderHead.OrderNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public bool CheckOrderDet(OrderDetail orderDetail)
        {
            if (orderDetail.DefaultLocationFrom == null)
                return true;
            else
                return this.CheckOrderDet(orderDetail.Item.Code, orderDetail.DefaultLocationFrom.Code, orderDetail.OrderHead.CheckDetailOption);
        }

        [Transaction(TransactionMode.Unspecified)]
        public bool CheckOrderDet(string item, string loc, string checkOrderDetOption)
        {
            //�����
            if (checkOrderDetOption == BusinessConstants.CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_NOT_CHECK)
                return true;

            //�����Դ
            if (checkOrderDetOption == BusinessConstants.CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_CHECK_SOURCE)
            {
                DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
                criteria.Add(Expression.Eq("Item.Code", item));
                IList<FlowDetail> flowDetailList = criteriaMgr.FindAll<FlowDetail>(criteria);

                if (flowDetailList != null && flowDetailList.Count > 0)
                {
                    foreach (FlowDetail flowDetail in flowDetailList)
                    {
                        if (flowDetail.DefaultLocationTo != null && flowDetail.DefaultLocationTo.Code == loc)
                        {
                            return true;
                        }

                        //ReferenceFlow
                        if (flowDetail.Flow.ReferenceFlow != null && flowDetail.Flow.ReferenceFlow.Trim() != string.Empty)
                        {
                            Flow refFlow = this.flowMgr.LoadFlow(flowDetail.Flow.ReferenceFlow);
                            if (refFlow.LocationTo != null && refFlow.LocationTo.Code == loc)
                            {
                                return true;
                            }
                        }
                    }
                }

                throw new BusinessErrorException("OrderDetail.Error.CheckOrderDetOption.NoSource", item);
            }

            //�����
            if (checkOrderDetOption == BusinessConstants.CODE_MASTER_CHECK_ORDER_DETAIL_OPTION_VALUE_CHECK_INVENTORY)
            {
                decimal currentInv = LocationDetailMgr.GetCurrentInv(loc, item);
                if (currentInv > 0)
                    return true;
                else
                    throw new BusinessErrorException("OrderDetail.Error.CheckOrderDetOption.NoInventory", item, loc);
            }

            return false;
        }
        [Transaction(TransactionMode.Unspecified)]
        public OrderDetail TransferFlowDetail2OrderDetail(FlowDetail flowDetail)
        {
            OrderDetail orderDetail = new OrderDetail();
            CloneHelper.CopyProperty(flowDetail, orderDetail, FlowDetail2OrderDetailCloneFields);
            return orderDetail;
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordOrderShipQty(int orderLocationTransactionId, InProcessLocationDetail inProcessLocationDetail, bool checkExcceed)
        {
            RecordOrderShipQty(this.orderLocationTransactionMgr.LoadOrderLocationTransaction(orderLocationTransactionId), inProcessLocationDetail, checkExcceed);
        }

        [Transaction(TransactionMode.Requires)]
        public void RecordOrderShipQty(OrderLocationTransaction orderLocationTransaction, InProcessLocationDetail inProcessLocationDetail, bool checkExcceed)
        {
            //orderLocationTransaction = this.orderLocationTransactionMgr.LoadOrderLocationTransaction(orderLocationTransaction.Id);
            EntityPreference entityPreference = this.entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ALLOW_EXCEED_GI_GR);
            bool allowExceedGiGR = bool.Parse(entityPreference.Value); //��ҵ���ԣ���������������ջ�

            OrderDetail orderDetail = orderLocationTransaction.OrderDetail;
            OrderHead orderHead = orderDetail.OrderHead;
            decimal shipQty = inProcessLocationDetail.Qty * orderLocationTransaction.UnitQty;

            #region �Ƿ���������ж�
            //�������Ϸ�����������ѡ��
            if (orderLocationTransaction.OrderDetail.OrderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION
                && orderLocationTransaction.OrderDetail.OrderHead.SubType != BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ)
            {
                //���AccumulateQty(�ѷ���)���ܴ��ڵ���OrderedQty(������)
                //if (!(orderHead.AllowExceed && allowExceedGiGR))
                //{
                //    if ((orderLocationTransaction.OrderedQty > 0 && orderLocationTransaction.AccumulateQty.HasValue && orderLocationTransaction.AccumulateQty.Value >= orderLocationTransaction.OrderedQty && shipQty > 0)
                //            || (orderLocationTransaction.OrderedQty < 0 && orderLocationTransaction.AccumulateQty.HasValue && orderLocationTransaction.AccumulateQty.Value <= orderLocationTransaction.OrderedQty && shipQty < 0))
                //    {
                //        throw new BusinessErrorException("Order.Error.ShipExcceed", orderHead.OrderNo, orderLocationTransaction.Item.Code);
                //    }
                //}

                if (!(orderHead.AllowExceed && allowExceedGiGR) && checkExcceed)   //�������������
                {
                    //���AccumulateQty(�ѷ���) + shipQty(���η�����)���ܴ���OrderedQty(������)
                    orderLocationTransaction.AccumulateQty = orderLocationTransaction.AccumulateQty.HasValue ? orderLocationTransaction.AccumulateQty.Value : 0;
                    if ((orderLocationTransaction.OrderedQty > 0 && (orderLocationTransaction.AccumulateQty + shipQty > orderLocationTransaction.OrderedQty))
                        || (orderLocationTransaction.OrderedQty < 0 && (orderLocationTransaction.AccumulateQty + shipQty < orderLocationTransaction.OrderedQty)))
                    {
                        throw new BusinessErrorException("Order.Error.ShipExcceed", orderHead.OrderNo, orderLocationTransaction.Item.Code);
                    }
                }
            }
            #endregion

            #region ��¼������
            if (orderLocationTransaction.BackFlushMethod == null ||
                orderLocationTransaction.BackFlushMethod == BusinessConstants.CODE_MASTER_BACKFLUSH_METHOD_VALUE_GOODS_RECEIVE)
            {
                if (!orderLocationTransaction.AccumulateQty.HasValue)
                {
                    orderLocationTransaction.AccumulateQty = 0;
                }
                //��¼LocationTransaction���ۼƷ�����
                orderLocationTransaction.AccumulateQty += shipQty;
                this.orderLocationTransactionMgr.UpdateOrderLocationTransaction(orderLocationTransaction);

                //��¼OrderDetail���ۼƷ�����
                if (orderLocationTransaction.Item.Code == orderLocationTransaction.OrderDetail.Item.Code)
                {
                    //���OrderLocationTransaction��OrderDetail�ϵ�Itemһ�£���Ҫ����OrderDetail�ϵ�ShippedQty
                    if (!orderDetail.ShippedQty.HasValue)
                    {
                        orderDetail.ShippedQty = 0;
                    }

                    //OrderLocationTransaction��OrderDetail�ϵ�Item���ܵ�λ��һ�£�ֱ�ӳ���UnitQty�Ϳ���ת����
                    orderDetail.ShippedQty += (shipQty / orderLocationTransaction.UnitQty);
                    this.UpdateOrderDetail(orderDetail);
                }
            }
            #endregion
        }


        #endregion Customized Methods

        #region private Methods
        private void FillBomForOrderDetail(OrderDetail orderDetail)
        {
            //Bom��ѡȡ˳��orderDetail.Bom(Copy from ·����ϸ) --> orderDetail.Item.Bom--> ��orderDetail.Item.Code��ΪBomCode
            if (orderDetail.Bom == null && orderDetail.Item.Bom != null)
            {
                orderDetail.Bom = orderDetail.Item.Bom;
            }

            if (orderDetail.Bom == null)
            {
                Bom bom = this.bomMgr.LoadBom(orderDetail.Item.Code);
                if (bom != null)
                {
                    orderDetail.Bom = bom;
                }
            }

            if (orderDetail.Bom == null)
            {
                throw new BusinessErrorException("OrderDetail.Error.NoBom", orderDetail.Item.Code);
            }
        }



        #endregion
    }
}