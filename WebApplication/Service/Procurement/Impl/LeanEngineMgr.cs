using System;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Utility;
using System.Linq;
using com.Sconit.Entity.View;
using com.Sconit.Entity.Exception;
using LeanEngine.Utility;
using LeanEngine;
using Castle.Services.Transaction;
using NHibernate.Transform;

namespace com.Sconit.Service.Procurement.Impl
{
    public class LeanEngineMgr : SessionBase, ILeanEngineMgr
    {
        #region 变量
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.LeanEngine");
        private const string user = "Monitor";
        private IList<Flow> _flows;
        private IList<LocationDetail> _locationDetails;
        private IList<InspectOrderDetail> _inspectOrderDetails;
        private IList<LeanEngineView> _leanEngineViews;
        private IList<OrderLocTransView> _orderLocTransViews;

        private ICriteriaMgr CriteriaMgr;
        private IFlowMgr FlowMgr;
        private IFlowDetailMgr FlowDetailMgr;
        private IOrderMgr OrderMgr;
        private IOrderDetailMgr OrderDetailMgr;
        private IUserMgr UserMgr;
        private ILocationDetailMgr LocDetMgr;
        private IAutoOrderTrackMgr AutoOrderTrackMgr;
        private IOrderLocationTransactionMgr OrderLocTransMgr;
        private IUomConversionMgr UomConversionMgr;
        private IShiftMgr shiftMgr;
        #endregion

        #region 构造函数
        public LeanEngineMgr(
            ICriteriaMgr CriteriaMgr,
            IFlowMgr FlowMgr,
            IFlowDetailMgr FlowDetailMgr,
            IOrderMgr OrderMgr,
            IUserMgr UserMgr,
            ILocationDetailMgr LocDetMgr,
            IOrderDetailMgr OrderDetailMgr,
            IAutoOrderTrackMgr AutoOrderTrackMgr,
            IOrderLocationTransactionMgr OrderLocTransMgr,
            IUomConversionMgr UomConversionMgr,
            IShiftMgr shiftMgr)
            : base()
        {
            this.CriteriaMgr = CriteriaMgr;
            this.FlowMgr = FlowMgr;
            this.FlowDetailMgr = FlowDetailMgr;
            this.OrderMgr = OrderMgr;
            this.UserMgr = UserMgr;
            this.LocDetMgr = LocDetMgr;
            this.OrderDetailMgr = OrderDetailMgr;
            this.AutoOrderTrackMgr = AutoOrderTrackMgr;
            this.OrderLocTransMgr = OrderLocTransMgr;
            this.UomConversionMgr = UomConversionMgr;
            this.shiftMgr = shiftMgr;
        }
        #endregion


        #region Public Methods
        public void CreateOrder(OrderHead order, string userCode)
        {
            if (order.OrderDetails == null || order.OrderDetails.Count == 0)
                return;

            IList<OrderHead> orderList = new List<OrderHead>();

            //OrderLotSize
            Flow flow = this.FlowMgr.LoadFlow(order.Flow);
            if (order.OrderDetails.Count == 1 && flow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                FlowDetail fd = order.OrderDetails[0].FlowDetail;
                decimal orderLotSize = fd.OrderLotSize.HasValue ? (decimal)fd.OrderLotSize : 0;
                decimal reqQty = order.OrderDetails[0].RequiredQty;
                decimal remainReqQty = order.OrderDetails[0].RequiredQty;

                if (orderLotSize > 0)
                {
                    OrderDetail od = order.OrderDetails[0];

                    decimal oddQty = reqQty % orderLotSize;
                    if (oddQty > 0)
                    {
                        order.OrderDetails[0].RequiredQty = oddQty;
                        order.OrderDetails[0].OrderedQty = oddQty;
                        orderList.Add(order);
                    }

                    int count = (int)Math.Floor(reqQty / orderLotSize);
                    for (int i = 0; i < count; i++)
                    {
                        OrderHead or = new OrderHead();
                        or = OrderMgr.TransferFlow2Order(order.Flow);
                        CloneHelper.CopyProperty(order, or, new string[] { "OrderNo", "OrderDetails" }, true);
                        if (or.OrderDetails != null && or.OrderDetails.Count > 0)
                        {
                            foreach (OrderDetail orderDetail in or.OrderDetails)
                            {
                                if (orderDetail.FlowDetail.Id == od.FlowDetail.Id && od.FlowDetail.Id > 0)
                                {
                                    orderDetail.RequiredQty = orderLotSize;
                                    orderDetail.OrderedQty = orderLotSize;
                                }
                            }
                        }

                        orderList.Add(or);
                    }
                }
                else
                {
                    orderList.Add(order);
                }
            }
            else
            {
                orderList.Add(order);
            }

            //Write to DB
            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderHead oh in orderList)
                {
                    try
                    {
                        //all Empty check
                        bool isValid = false;
                        foreach (OrderDetail od in oh.OrderDetails)
                        {
                            if (od.RequiredQty > 0)
                            {
                                isValid = true;
                                break;
                            }
                        }

                        if (isValid)
                        {
                            //oh.Shift = shiftMgr.LoadShift("A");//temp,todo

                            log.Debug("Begin to create order," + oh.Flow);

                            this.OrderMgr.CreateOrder(oh, userCode);

                            log.Debug("End to create order," + oh.OrderNo);
                            log.Debug("----------------------------------Invincible's dividing line---------------------------------------");
                        }

                    }
                    catch (Exception ex)
                    {
                        this.OrderMgr.CleanSession();
                        log.Error("create order fail", ex);
                        log.Debug("----------------------------------Invincible's dividing line---------------------------------------");
                    }
                }
            }
        }



        //3G LeanEngine
        public void OrderGenerate()
        {
            log.Info("----------------------------------Invincible's dividing line---------------------------------------");
            log.Debug("Lean Engine start working.");
            this.Initial_DBCache();

            #region Load data
            IEngine engine = new Engine();
            LeanEngine.Entity.EngineContainer container = new LeanEngine.Entity.EngineContainer();
            log.Debug("Begin to load data:");
            container.ItemFlows = this.TransformToItemFlow();
            container.InvBalances = this.TransformToInvBalances();
            container.Plans = this.TransformToPlans();
            log.Debug("FlowDetail count:" + container.ItemFlows.Count);
            log.Debug("LocationDetail count:" + container.InvBalances.Count);
            log.Debug("OrderLocTrans count:" + container.Plans.Count);
            log.Debug("Load data finished.");
            #endregion

            #region Lean Engine Core Logic
            log.Debug("Begin to calculate:");
            engine.TellMeDemands(container);
            log.Debug("Calculating finished.");

            log.Debug("Begin to create orders:");
            List<LeanEngine.Entity.Orders> orders = engine.DemandToOrders(container.ItemFlows);
            if (orders == null || orders.Count == 0)
            {
                log.Debug("No orders to create.");
                return;
            }
            #endregion

            List<string> flowList = orders.Select(o => o.Flow.Code).Distinct().ToList();
            _flows = this.GetFlow(flowList, true);
            log.Debug("Flow count:" + _flows.Count);
            foreach (var order in orders)
            {
                this.ProcessNewOrder(order);
            }

            this.Initial_DBCache();
            log.Debug("Lean Engine finished the job.");
        }

        [Transaction(TransactionMode.RequiresNew)]
        public void ProcessNewOrder(LeanEngine.Entity.Orders order)
        {
            log.Debug("-------------------------------Current flow code:" + order.Flow.Code + "-------------------------------------");
            Flow flow = _flows.SingleOrDefault(o => StringHelper.Eq(order.Flow.Code, o.Code));
            if (flow == null)
                return;

            #region 更新窗口时间
            if (order.Flow.IsUpdateWindowTime)
            {
                if (flow.NextOrderTime != order.Flow.NextOrderTime || flow.NextWinTime != order.Flow.NextWindowTime)
                {
                    flow.NextOrderTime = order.Flow.NextOrderTime;
                    flow.NextWinTime = order.Flow.NextWindowTime;
                    this.FlowMgr.UpdateFlow(flow, false);
                    log.Debug("Update window time to :" + flow.NextWinTime);
                }
            }
            #endregion

            if (order.ItemFlows == null || order.ItemFlows.Count == 0)
                return;

            #region Initialize
            OrderHead orderHead = OrderMgr.TransferFlow2Order(flow);
            orderHead.IsAutoRelease = true;
            orderHead.WindowTime = order.WindowTime;
            orderHead.StartTime = order.StartTime;
            #endregion

            #region New Orders
            if (orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
            {
                foreach (var orderDetail in orderHead.OrderDetails)
                {
                    var itemFlow = order.ItemFlows.SingleOrDefault(i => i.ID == orderDetail.FlowDetail.Id);

                    if (itemFlow != null)
                    {
                        orderDetail.RequiredQty = itemFlow.ReqQty;
                        orderDetail.OrderedQty = itemFlow.OrderQty;
                        orderDetail.OrderTracers = this.TransformToOrderTracer(itemFlow.OrderTracers);

                        if (orderDetail.OrderTracers != null && orderDetail.OrderTracers.Count > 0)
                        {
                            foreach (var orderTracer in orderDetail.OrderTracers)
                            {
                                orderTracer.OrderDetail = orderDetail;
                            }
                        }
                    }
                }
                orderHead.OrderDetails = orderHead.OrderDetails.Where(o => o.OrderedQty > 0).ToList();

                if (orderHead.OrderDetails.Count > 0)
                {
                    log.Debug("Detail count:" + orderHead.OrderDetails.Count);

                    try
                    {
                        orderHead.Priority = order.IsEmergency ? BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_URGENT :
                            BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;

                        this.OrderMgr.CreateOrder(orderHead, user);
                        this.FlushSession();
                        log.Info("Create order:" + orderHead.OrderNo + " finished. Detail count:" + orderHead.OrderDetails.Count + ",IsEmergency:" + order.IsEmergency);
                        //todo,orderlotsize
                    }
                    catch (BusinessErrorException ex)
                    {
                        log.Error("Fail to create order:", ex);
                    }
                }
            }
            #endregion
        }

        public OrderHead PreviewGenOrder(string flowCode)
        {
            IList<LeanEngineView> leanEngineViews = this.GetLeanEngineView(null, flowCode, null, null, null, null, null);
            List<string> locList = leanEngineViews.Where(l => l.LocTo != null).Select(l => l.LocTo).Distinct().ToList();
            List<string> itemList = leanEngineViews.Select(l => l.Item).Distinct().ToList();
            IList<LocationDetail> locationDetails = this.GetLocationDetail(locList, itemList);
            IList<OrderLocTransView> orderLocTrans = this.GetOrderLocTransView(locList, itemList);

            IEngine engine = new Engine();
            LeanEngine.Entity.EngineContainer container = new LeanEngine.Entity.EngineContainer();
            container.ItemFlows = this.TransformToItemFlow(leanEngineViews);
            container.InvBalances = this.TransformToInvBalances(locationDetails);
            container.Plans = this.TransformToPlans(orderLocTrans);

            engine.TellMeDemands(container);
            List<LeanEngine.Entity.Orders> orders = engine.DemandToOrders(container.ItemFlows);
            List<LeanEngine.Entity.ItemFlow> itemFlows = (from o in orders
                                                          from i in o.ItemFlows
                                                          where i.OrderQty > 0
                                                          select i).ToList();

            OrderHead orderHead = null;
            if (itemFlows != null && itemFlows.Count > 0)
            {
                orderHead = this.OrderMgr.TransferFlow2Order(flowCode);

                if (orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
                {
                    foreach (var orderDetail in orderHead.OrderDetails)
                    {
                        var itemFlow = itemFlows.SingleOrDefault(i => i.ID == orderDetail.FlowDetail.Id);

                        if (itemFlow != null)
                        {
                            orderDetail.RequiredQty = itemFlow.ReqQty;
                            orderDetail.OrderedQty = itemFlow.OrderQty;
                        }
                    }
                    orderHead.OrderDetails = orderHead.OrderDetails.Where(o => o.OrderedQty > 0).ToList();
                }
            }

            return orderHead;
        }
        #region Transformer
        private List<LeanEngine.Entity.ItemFlow> TransformToItemFlow()
        {
            _leanEngineViews = this.GetLeanEngineView();
            return this.TransformToItemFlow(_leanEngineViews);
        }
        private List<LeanEngine.Entity.ItemFlow> TransformToItemFlow(IList<LeanEngineView> leanEngineViews)
        {
            if (leanEngineViews == null)
                return null;

            #region Transform
            List<LeanEngine.Entity.ItemFlow> itemFlows
                = (from l in leanEngineViews
                   select new LeanEngine.Entity.ItemFlow
                   {
                       ID = l.FlowDetId,
                       Flow = new LeanEngine.Entity.Flow
                       {
                           Code = l.Flow,
                           PartyFrom = l.PartyFrom,
                           PartyTo = l.PartyTo,
                           FlowStrategy = new LeanEngine.Entity.FlowStrategy
                           {
                               Code = l.Flow,
                               Strategy = this.MappingStrategy(l.FlowStrategy),
                               LeadTime = l.LeadTime.HasValue ? Convert.ToDouble(l.LeadTime.Value) : 0,
                               EmLeadTime = l.EmTime.HasValue ? Convert.ToDouble(l.EmTime.Value) : 0,
                               WeekInterval = l.WeekInterval.HasValue ? l.WeekInterval.Value : 0,
                               MonWinTime = this.GetWindowTime(l.WinTime1),
                               TueWinTime = this.GetWindowTime(l.WinTime2),
                               WedWinTime = this.GetWindowTime(l.WinTime3),
                               ThuWinTime = this.GetWindowTime(l.WinTime4),
                               FriWinTime = this.GetWindowTime(l.WinTime5),
                               SatWinTime = this.GetWindowTime(l.WinTime6),
                               SunWinTime = this.GetWindowTime(l.WinTime7)
                           },
                           FlowType = this.MappingFlowType(l.Type),
                           OrderTime = l.NextOrderTime,
                           WindowTime = l.NextWinTime
                       },
                       Bom = null,
                       Item = l.Item,
                       LocFrom = l.LocFrom,
                       LocTo = l.LocTo,
                       MaxInv = l.MaxStock.HasValue ? l.MaxStock.Value : 0,
                       SafeInv = l.SafeStock.HasValue ? l.SafeStock.Value : 0,
                       UC = l.HuLotSize.HasValue ? l.HuLotSize.Value : l.UC,//大包装:HuLotSize,小包装:UC,优先取HuLotSize,没有则取UC
                       MinLotSize = l.MinLotSize.HasValue ? l.MinLotSize.Value : 0,//按照最小订单量得到修正需求数(企业选项：小于最小订单量是否订货)
                       OrderLotSize = l.OrderLotSize.HasValue ? l.OrderLotSize.Value : 0,//按照OrderLotSize拆分需求数,拆成多张订单
                       RoundUp = this.MappingRoundUp(l.RoundUpOpt)
                   }).ToList();
            #endregion

            return itemFlows;
        }

        private List<LeanEngine.Entity.InvBalance> TransformToInvBalances()
        {
            _locationDetails = this.GetLocationDetail();
            return this.TransformToInvBalances(_locationDetails);
        }
        private List<LeanEngine.Entity.InvBalance> TransformToInvBalances(IList<LocationDetail> locationDetails)
        {
            if (locationDetails == null)
                return null;

            #region Transform
            List<LeanEngine.Entity.InvBalance> invBalances
                = (from l in locationDetails
                   select new LeanEngine.Entity.InvBalance
                   {
                       Loc = l.Location.Code,
                       Item = l.Item.Code,
                       Qty = l.Qty,
                       InvType = Enumerators.InvType.Normal
                   }).ToList();
            #endregion

            #region Inspect
            var _inspectInvBalances = this.GetInspectInvBalance();
            if (_inspectInvBalances != null && _inspectInvBalances.Count > 0)
            {
                invBalances = invBalances.Union(_inspectInvBalances).ToList();
            }
            #endregion

            return invBalances;
        }

        private List<LeanEngine.Entity.Plans> TransformToPlans()
        {
            _orderLocTransViews = this.GetOrderLocTransView();
            return this.TransformToPlans(_orderLocTransViews);
        }
        private List<LeanEngine.Entity.Plans> TransformToPlans(IList<OrderLocTransView> orderLocTransViews)
        {
            if (orderLocTransViews == null)
                return null;

            #region Transform
            List<LeanEngine.Entity.Plans> plans
                = (from o in orderLocTransViews
                   select new LeanEngine.Entity.Plans
                   {
                       ID = o.Id,
                       Loc = o.Location,
                       Item = o.Item.Code,
                       FlowCode = o.Flow,
                       OrderNo = o.OrderNo,
                       ReqTime = o.IOType == BusinessConstants.IO_TYPE_IN ? o.WindowTime : o.StartTime,
                       IRType = o.IOType == BusinessConstants.IO_TYPE_IN ? Enumerators.IRType.RCT : Enumerators.IRType.ISS,
                       PlanType = Enumerators.PlanType.Orders,
                       FlowType = this.MappingFlowType(o.Type),
                       OrderedQty = o.PlanQty,
                       FinishedQty = o.AccumQty
                   }).ToList();
            #endregion

            return plans;
        }

        private List<OrderTracer> TransformToOrderTracer(List<LeanEngine.Entity.OrderTracer> list)
        {
            if (list != null && list.Count > 0)
            {
                var query = from l in list
                            where l.Qty != 0
                            select new OrderTracer
                            {
                                TracerType = l.TracerType.ToString(),
                                Code = l.Code,
                                ReqTime = l.ReqTime,
                                Item = l.Item,
                                OrderedQty = l.OrderedQty,
                                AccumulateQty = l.FinishedQty,
                                Qty = l.Qty,
                                RefOrderLocTransId = l.RefOrderLocTransId
                            };

                return query.ToList();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Mappings
        private Enumerators.Strategy MappingStrategy(string strategy)
        {
            if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_KB, strategy))
                return Enumerators.Strategy.KB;
            else if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_JIT, strategy))
                return Enumerators.Strategy.JIT;
            else if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_ODP, strategy))
                return Enumerators.Strategy.ODP;
            else if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_MRP, strategy))
                return Enumerators.Strategy.MRP;
            else
                return Enumerators.Strategy.Manual;
        }

        private Enumerators.FlowType MappingFlowType(string type)
        {
            if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION, type))
            {
                return Enumerators.FlowType.Production;
            }
            else if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION, type))
            {
                return Enumerators.FlowType.Distribution;
            }
            else if (StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT, type)
                || StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS, type)
                || StringHelper.Eq(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING, type))
            {
                return Enumerators.FlowType.Procurement;
            }
            else
            {
                return Enumerators.FlowType.Transfer;
            }
        }

        private Enumerators.RoundUp MappingRoundUp(string roundUpOpt)
        {
            double roundUp = 0;
            try
            {
                if (roundUpOpt != null && roundUpOpt.Trim() != string.Empty)
                    roundUp = double.Parse(roundUpOpt);
            }
            catch { }

            if (roundUp > 0)
                return Enumerators.RoundUp.Ceiling;
            else if (roundUp < 0)
                return Enumerators.RoundUp.Floor;
            else
                return Enumerators.RoundUp.None;
        }
        #endregion

        private string[] GetWindowTime(string windowTime)
        {
            return windowTime != null ? windowTime.Trim().Split('|') : null;
        }

        #endregion Public Methods

        #region Database Cache
        private void Initial_DBCache()
        {
            _flows = null;
            _locationDetails = null;
            _inspectOrderDetails = null;
            _leanEngineViews = null;
            _orderLocTransViews = null;
        }

        private IList<LocationDetail> GetLocationDetail()
        {
            return this.GetLocationDetail(null, null);
        }
        private IList<LocationDetail> GetLocationDetail(List<string> locList, List<string> itemList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
            criteria.Add(Expression.Not(Expression.Eq("Qty", new decimal(0))));
            this.SetInCriteria<string>(criteria, "Location.Code", locList);
            this.SetInCriteria<string>(criteria, "Item.Code", itemList);

            return CriteriaMgr.FindAll<LocationDetail>(criteria);
        }

        private IList<InspectOrderDetail> GetInspectOrderDetail()
        {
            //报验库存
            DetachedCriteria criteria = DetachedCriteria.For(typeof(InspectOrderDetail));
            criteria.CreateAlias("InspectOrder", "io");
            criteria.Add(Expression.Eq("io.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));

            return CriteriaMgr.FindAll<InspectOrderDetail>(criteria);
        }

        private List<LeanEngine.Entity.InvBalance> GetInspectInvBalance()
        {
            //报验库存
            DetachedCriteria criteria = DetachedCriteria.For(typeof(InspectOrderDetail));
            criteria.CreateAlias("InspectOrder", "io");
            criteria.CreateAlias("LocationLotDetail", "lld");
            criteria.Add(Expression.Eq("io.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            criteria.Add(Expression.Eq("io.IsSeperated", false));

            criteria.SetProjection(Projections.ProjectionList()
                .Add(Projections.Sum("InspectQty").As("InspectQty"))
                .Add(Projections.Sum("QualifiedQty").As("QualifiedQty"))
                .Add(Projections.Sum("RejectedQty").As("RejectedQty"))
                .Add(Projections.GroupProperty("LocationFrom.Code").As("LocationFromCode"))
                .Add(Projections.GroupProperty("lld.Item.Code").As("ItemCode")));

            criteria.SetResultTransformer(Transformers.AliasToBean(typeof(InspectOrderDetail)));
            IList<InspectOrderDetail> inspectOrderDetailList = CriteriaMgr.FindAll<InspectOrderDetail>(criteria);
            var query = from i in inspectOrderDetailList
                        select new LeanEngine.Entity.InvBalance
                        {
                            Loc = i.LocationFromCode,
                            Item = i.ItemCode,
                            Qty = i.InspectedQty,
                            InvType = Enumerators.InvType.Inspect
                        };

            return query.Where(q => q.Qty != 0).ToList();
        }

        private IList<Flow> GetFlow()
        {
            return this.GetFlow(null, true);
        }
        private IList<Flow> GetFlow(List<string> flowList, bool? isAutoCreate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
            if (isAutoCreate.HasValue)
            {
                criteria.Add(Expression.Eq("IsAutoCreate", isAutoCreate.Value));
            }
            this.SetInCriteria<string>(criteria, "Code", flowList);
            //criteria.Add(Expression.Or(Expression.IsNull("NextOrderTime"), Expression.Le("NextOrderTime", DateTime.Now)));

            return CriteriaMgr.FindAll<Flow>(criteria);
        }

        private IList<LeanEngineView> GetLeanEngineView()
        {
            return this.GetLeanEngineView(null, null, null, null, null, null, true);
        }
        private IList<LeanEngineView> GetLeanEngineView(int? flowDetId, string flowCode, List<string> locFromList, List<string> locToList, List<string> itemList, string flowStrategy, bool? isAutoCreate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LeanEngineView));
            if (flowDetId.HasValue)
            {
                criteria.Add(Expression.Eq("FlowDetId", flowDetId.Value));
            }
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flowCode));
            }
            this.SetInCriteria<string>(criteria, "LocFrom", locFromList);
            this.SetInCriteria<string>(criteria, "LocTo", locToList);
            this.SetInCriteria<string>(criteria, "Item", itemList);
            if (flowStrategy != null && flowStrategy.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("FlowStrategy", flowStrategy));
            }
            if (isAutoCreate.HasValue)
            {
                criteria.Add(Expression.Eq("IsAutoCreate", isAutoCreate.Value));
            }

            return CriteriaMgr.FindAll<LeanEngineView>(criteria);
        }

        private IList<OrderLocTransView> GetOrderLocTransView()
        {
            return this.GetOrderLocTransView(null, null);
        }
        private IList<OrderLocTransView> GetOrderLocTransView(List<string> locList, List<string> itemList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocTransView));
            OrderHelper.SetOpenOrderStatusCriteria(criteria, "Status");
            this.SetInCriteria<string>(criteria, "Location", locList);
            this.SetInCriteria<string>(criteria, "ItemCode", itemList);

            return CriteriaMgr.FindAll<OrderLocTransView>(criteria);
        }
        #endregion


        #region Criteria
        public void SetInCriteria<T>(DetachedCriteria criteria, string propertyName, List<T> list)
        {
            if (list != null && list.Count > 0)
            {
                if (list.Count == 1)
                {
                    criteria.Add(Expression.Eq(propertyName, list[0]));
                }
                else
                {
                    criteria.Add(Expression.InG<T>(propertyName, list));
                }
            }
        }
        #endregion
    }
}
