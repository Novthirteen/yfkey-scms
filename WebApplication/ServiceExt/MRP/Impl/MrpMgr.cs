using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Service.MRP;
using NHibernate.Expression;
using com.Sconit.Entity.MRP;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Hql;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Ext.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using System.Collections;
using NHibernate.SqlCommand;
using com.Sconit.Service.MasterData;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using com.Sconit.Entity.Procurement;

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class MrpMgr : IMrpMgr
    {
        public IMrpRunLogMgr mrpRunLogMgr;
        public ICriteriaMgr criteriaMgr;
        public IHqlMgr hqlMgr;
        public IFinanceCalendarMgr financeCalendarMgr;
        public IMrpShipPlanMgr mrpShipPlanMgr;
        public IUomConversionMgr uomConversionMgr;
        public ICustomerScheduleDetailMgr customerScheduleDetailMgr;
        public IItemMgr itemMgr;
        public IBomMgr bomMgr;
        public IBomDetailMgr bomDetailMgr;
        public IRoutingDetailMgr routingDetailMgr;
        public IMrpReceivePlanMgr mrpReceivePlanMgr;
        public IExpectTransitInventoryMgr expectTransitInventoryMgr;
        public IGenericMgr genericMgr;
        public INumberControlMgr iNumberControlMgr;
        public IShiftMgr shiftMgr;
        public IFlowMgr flowMgr;
        public IOrderMgr orderMgr;
        public IWorkCalendarMgr workCalendarMgr;
        public IFlowDetailMgr flowDetailMgr;


        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.MRP");

        public MrpMgr(IMrpRunLogMgr mrpRunLogMgr,
                        ICriteriaMgr criteriaMgr,
                        IHqlMgr hqlMgr,
                        IFinanceCalendarMgr financeCalendarMgr,
                        IMrpShipPlanMgr mrpShipPlanMgr,
                        IUomConversionMgr uomConversionMgr,
                        ICustomerScheduleDetailMgr customerScheduleDetailMgr,
                        IItemMgr itemMgr,
                        IBomMgr bomMgr,
                        IBomDetailMgr bomDetailMgr,
                        IRoutingDetailMgr routingDetailMgr,
                        IMrpReceivePlanMgr mrpReceivePlanMgr,
                        IExpectTransitInventoryMgr expectTransitInventoryMgr,
                        IGenericMgr genericMgr,
                        INumberControlMgr iNumberControlMgr,
                        IShiftMgr shiftMgr,
                        IFlowMgr flowMgr,
                        IOrderMgr orderMgr,
                        IFlowDetailMgr flowDetailMgr,
                        IWorkCalendarMgr workCalendarMgr
            )
        {
            this.mrpRunLogMgr = mrpRunLogMgr;
            this.criteriaMgr = criteriaMgr;
            this.hqlMgr = hqlMgr;
            this.financeCalendarMgr = financeCalendarMgr;
            this.mrpShipPlanMgr = mrpShipPlanMgr;
            this.uomConversionMgr = uomConversionMgr;
            this.customerScheduleDetailMgr = customerScheduleDetailMgr;
            this.itemMgr = itemMgr;
            this.bomMgr = bomMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.routingDetailMgr = routingDetailMgr;
            this.mrpReceivePlanMgr = mrpReceivePlanMgr;
            this.expectTransitInventoryMgr = expectTransitInventoryMgr;
            this.genericMgr = genericMgr;
            this.iNumberControlMgr = iNumberControlMgr;
            this.shiftMgr = shiftMgr;
            this.flowMgr = flowMgr;
            this.orderMgr = orderMgr;
            this.workCalendarMgr = workCalendarMgr;
            this.flowDetailMgr = flowDetailMgr;
        }

        [Transaction(TransactionMode.Requires)]
        public void RunMrp(User user)
        {
            RunMrp(DateTime.Now, user);
        }

        [Transaction(TransactionMode.Requires)]
        public void RunMrp(DateTime effectiveDate, User user)
        {
            DateTime dateTimeNow = DateTime.Now;
            IList<MrpShipPlan> mrpShipPlanList = new List<MrpShipPlan>();
            #region EffectiveDate格式化
            effectiveDate = effectiveDate.Date;
            #endregion

            log.Info("----------------------------------Invincible's dividing line---------------------------------------");
            log.Info("Start run mrp effectivedate:" + effectiveDate.ToLongDateString());

            #region 删除有效期相同的ShipPlan、ReceivePlan、TransitInventory
            string hql = @"from MrpShipPlan entity where entity.EffectiveDate = ?";
            hqlMgr.Delete(hql, new object[] { effectiveDate }, new IType[] { NHibernateUtil.DateTime });

            hql = @"from MrpReceivePlan entity where entity.EffectiveDate = ?";
            hqlMgr.Delete(hql, new object[] { effectiveDate }, new IType[] { NHibernateUtil.DateTime });

            hql = @"from ExpectTransitInventory entity where entity.EffectiveDate = ?";
            hqlMgr.Delete(hql, new object[] { effectiveDate }, new IType[] { NHibernateUtil.DateTime });



            this.hqlMgr.FlushSession();
            this.hqlMgr.CleanSession();


            #endregion

            #region 获取实时库存和在途
            #region 查询
            #region 订单待收
            hql = @"select oh.OrderNo, oh.Type, oh.Flow, olt.Location.Code, olt.Item.Code, olt.Uom.Code, od.UnitCount, oh.StartTime, oh.WindowTime, od.OrderedQty, od.ShippedQty, od.ReceivedQty, olt.UnitQty
                    from OrderLocationTransaction as olt 
                    join olt.OrderDetail as od
                    join od.OrderHead as oh
                    where oh.Status in (?, ?) and oh.SubType = ? and not oh.Type in (?, ?) and olt.IOType = ?";

            IList<object[]> expectTransitInvList = hqlMgr.FindAll<object[]>(hql,
                new Object[] {
                    BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT, 
                    BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS, 
                    BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION,   //不是销售和生产
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION,
                    BusinessConstants.IO_TYPE_IN
                });
            #endregion

            #region 安全库存
            hql = @"select fl.Code, fdl.Code, i.Code, fd.SafeStock from FlowDetail as fd 
                                        join fd.Flow as f 
                                        left join fd.LocationTo as fdl 
                                        left join f.LocationTo as fl
                                        join fd.Item as i
                                        where (fd.LocationTo is not null and fdl.IsMRP = 1)
                                        or (f.LocationTo is not null and fl.IsMRP = 1)";
            IList<object[]> safeQtyList = hqlMgr.FindAll<object[]>(hql);
            #endregion

            #region 实时库存
            /*
            hql = @"select l.Code, i.Code, sum(lld.Qty) from LocationLotDetail as lld
                    join lld.Location as l
                    join lld.Item as i
                    where not lld.Qty = 0 and l.Type = ? and l.IsMRP = 1
                    group by l.Code, i.Code";
            IList<object[]> invList = hqlMgr.FindAll<object[]>(hql, BusinessConstants.CODE_MASTER_LOCATION_TYPE_VALUE_NORMAL);
             */
            hql = @"select l.Code, i.Code, sum(lld.Qty) from LocationLotDetail as lld
                    join lld.Location as l
                    join lld.Item as i
                    where not lld.Qty = 0 and l.IsMRP = 1 and l.Code not in (?, ?)
                    group by l.Code, i.Code";
            IList<object[]> invList = hqlMgr.FindAll<object[]>(hql, new object[] { BusinessConstants.SYSTEM_LOCATION_INSPECT, BusinessConstants.SYSTEM_LOCATION_REJECT });
            #endregion

            #region 发运在途
            DetachedCriteria criteria = DetachedCriteria.For<InProcessLocationDetail>();

            //criteria.CreateAlias("LocationTo", "lt");
            criteria.CreateAlias("InProcessLocation", "ip");
            criteria.CreateAlias("OrderLocationTransaction", "olt");
            criteria.CreateAlias("olt.OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            criteria.CreateAlias("olt.Item", "i");
            criteria.CreateAlias("od.LocationTo", "lt", JoinType.LeftOuterJoin);
            criteria.CreateAlias("oh.LocationTo", "ohlt", JoinType.LeftOuterJoin);

            criteria.Add(Expression.Eq("ip.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            criteria.Add(Expression.Eq("oh.SubType", BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML));
            criteria.Add(Expression.In("ip.OrderType", new string[] { 
                            BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS, 
                            BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                            BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING, 
                            BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));

            criteria.SetProjection(Projections.ProjectionList()
              .Add(Projections.GroupProperty("od.LocationTo"))
              .Add(Projections.GroupProperty("i.Code"))
              .Add(Projections.Sum("Qty"))
              .Add(Projections.Sum("ReceivedQty"))
              .Add(Projections.GroupProperty("ip.ArriveTime"))
              .Add(Projections.GroupProperty("oh.LocationTo"))
              );
            IList<object[]> ipDetList = this.criteriaMgr.FindAll<object[]>(criteria);
            #endregion

            #region 检验在途
            criteria = DetachedCriteria.For<InspectOrderDetail>();

            criteria.CreateAlias("InspectOrder", "io");
            criteria.CreateAlias("LocationTo", "lt");
            criteria.CreateAlias("LocationLotDetail", "lld");
            criteria.CreateAlias("lld.Item", "i");

            criteria.Add(Expression.Eq("io.IsSeperated", false));
            criteria.Add(Expression.Eq("io.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));

            criteria.SetProjection(Projections.ProjectionList()
               .Add(Projections.GroupProperty("lt.Code").As("Location"))
               .Add(Projections.GroupProperty("i.Code").As("Item"))
               .Add(Projections.Sum("lld.Qty"))
               .Add(Projections.GroupProperty("io.EstimateInspectDate"))
               );

            IList<object[]> inspLocList = this.criteriaMgr.FindAll<object[]>(criteria);
            #endregion
            #endregion

            #region 处理数据
            #region 获取所有库位的安全库存
            IList<SafeInventory> locationSafeQtyList = new List<SafeInventory>();
            if (safeQtyList != null && safeQtyList.Count > 0)
            {
                var unGroupSafeQtyList = from safeQty in safeQtyList
                                         select new
                                         {
                                             Location = (safeQty[1] != null ? (string)safeQty[1] : (string)safeQty[0]),
                                             Item = (string)safeQty[2],
                                             SafeQty = safeQty[3] != null ? (decimal)safeQty[3] : 0
                                         };

                var groupSafeQtyList = from g in unGroupSafeQtyList
                                       group g by new { g.Location, g.Item } into result
                                       select new SafeInventory
                                       {
                                           Location = result.Key.Location,
                                           Item = result.Key.Item,
                                           SafeQty = result.Max(g => g.SafeQty)
                                       };

                locationSafeQtyList = groupSafeQtyList != null ? groupSafeQtyList.ToList() : new List<SafeInventory>();
            }
            #endregion

            #region 获取实时库存
            IList<MrpLocationLotDetail> inventoryBalanceList = new List<MrpLocationLotDetail>();
            if (invList != null && invList.Count > 0)
            {
                IListHelper.AddRange<MrpLocationLotDetail>(inventoryBalanceList, (from inv in invList
                                                                                  select new MrpLocationLotDetail
                                                                                  {
                                                                                      Location = (string)inv[0],
                                                                                      Item = (string)inv[1],
                                                                                      Qty = (decimal)inv[2],
                                                                                      SafeQty = (from g in locationSafeQtyList
                                                                                                 where g.Location == (string)inv[0]
                                                                                                    && g.Item == (string)inv[1]
                                                                                                 select g.SafeQty).FirstOrDefault()
                                                                                  }).ToList());
            }
            #endregion

            #region 没有库存的安全库存全部转换为InventoryBalance
            if (locationSafeQtyList != null && locationSafeQtyList.Count > 0)
            {
                var eqSafeQtyList = from sq in locationSafeQtyList
                                    join inv in inventoryBalanceList on new { Location = sq.Location, Item = sq.Item } equals new { Location = inv.Location, Item = inv.Item }
                                    select sq;

                IList<SafeInventory> lackSafeQtyList = null;
                if (eqSafeQtyList != null && eqSafeQtyList.Count() > 0)
                {
                    lackSafeQtyList = locationSafeQtyList.Except(eqSafeQtyList.ToList(), new SafeInventoryComparer()).ToList();
                }
                else
                {
                    lackSafeQtyList = locationSafeQtyList;
                }

                if (lackSafeQtyList != null && lackSafeQtyList.Count > 0)
                {
                    var mlldList = from sq in lackSafeQtyList
                                   where sq.SafeQty > 0
                                   select new MrpLocationLotDetail
                                   {
                                       Location = sq.Location,
                                       Item = sq.Item,
                                       Qty = 0,
                                       SafeQty = sq.SafeQty
                                   };

                    if (mlldList != null && mlldList.Count() > 0)
                    {
                        if (inventoryBalanceList == null)
                        {
                            inventoryBalanceList = mlldList.ToList();
                        }
                        else
                        {
                            IListHelper.AddRange<MrpLocationLotDetail>(inventoryBalanceList, mlldList.ToList());
                        }
                    }
                }
            }
            #endregion

            #region 发运在途 ASN
            IList<TransitInventory> transitInventoryList = new List<TransitInventory>();

            if (ipDetList != null && ipDetList.Count > 0)
            {
                foreach (object[] ipDet in ipDetList)
                {
                    //记录在途库存
                    TransitInventory transitInventory = new TransitInventory();
                    transitInventory.Location = ipDet[0] != null ? ((Location)ipDet[0]).Code : (ipDet[5] != null ? ((Location)ipDet[5]).Code : null);
                    transitInventory.Item = (string)ipDet[1];
                    transitInventory.Qty = (decimal)ipDet[2] - (ipDet[3] != null ? (decimal)ipDet[3] : decimal.Zero);
                    transitInventory.EffectiveDate = (DateTime)ipDet[4];

                    transitInventoryList.Add(transitInventory);
                }
            }
            #endregion

            #region 检验在途
            if (inspLocList != null && inspLocList.Count > 0)
            {
                foreach (object[] inspLoc in inspLocList)
                {
                    //记录在途库存
                    TransitInventory transitInventory = new TransitInventory();
                    transitInventory.Location = (string)inspLoc[0];
                    transitInventory.Item = (string)inspLoc[1];
                    transitInventory.Qty = (decimal)inspLoc[2];
                    transitInventory.EffectiveDate = (DateTime)inspLoc[3];

                    transitInventoryList.Add(transitInventory);
                    log.Debug("In-Process inspect order detail records as transit inventory. location[" + transitInventory.Location + "], item[" + transitInventory.Item + "], qty[" + transitInventory.Qty + "], effectiveDate[" + transitInventory.EffectiveDate + "]");
                }
            }
            #endregion

            #region Snapshot 订单待收
            if (expectTransitInvList != null)
            {
                var expTransInvListSnapShot = from inv in expectTransitInvList
                                              select new ExpectTransitInventory
                                              {
                                                  OrderNo = (string)inv[0],
                                                  Flow = (string)inv[2],
                                                  Location = (string)inv[3],
                                                  Item = (string)inv[4],
                                                  Uom = (string)inv[5],
                                                  UnitCount = (decimal)inv[6],
                                                  StartTime = (DateTime)inv[7],
                                                  WindowTime = (DateTime)inv[8],
                                                  TransitQty = (string)inv[1] != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION ?
                                                                                ((decimal)inv[9] - (inv[10] != null ? (decimal)inv[10] : 0) * (decimal)inv[12])
                                                                                : ((decimal)inv[9] - (inv[11] != null ? (decimal)inv[11] : 0) * (decimal)inv[12]),
                                                  EffectiveDate = effectiveDate
                                              };

                foreach (ExpectTransitInventory snapShot in expTransInvListSnapShot)
                {
                    if (snapShot.TransitQty != 0)
                    {
                        this.expectTransitInventoryMgr.CreateExpectTransitInventory(snapShot);
                    }
                }
            }
            #endregion
            #endregion
            #endregion

            #region 根据生产单/主生产计划生成发货计划
            #region 获取所有生产路线明细
            criteria = DetachedCriteria.For<Flow>();

            criteria.SetProjection(Projections.ProjectionList()
                .Add(Projections.GroupProperty("Code"))
                .Add(Projections.GroupProperty("MRPOption")));

            criteria.Add(Expression.Eq("IsActive", true));
            criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            criteria.Add(Expression.Eq("FlowStrategy", BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_MRP));

            IList<object[]> flowList = this.criteriaMgr.FindAll<object[]>(criteria);
            #endregion

            #region 获取主生产计划
            criteria = DetachedCriteria.For<CustomerScheduleDetail>();
            criteria.CreateAlias("CustomerSchedule", "cs");

            criteria.Add(Expression.Eq("cs.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
            criteria.Add(Expression.Ge("StartTime", effectiveDate));

            IList<CustomerScheduleDetail> customerScheduleDetailList = this.criteriaMgr.FindAll<CustomerScheduleDetail>(criteria);

            #region 取得有效的CustomerScheduleDetail
            IList<CustomerScheduleDetail> effectiveCustomerScheduleDetailList = customerScheduleDetailMgr.GetEffectiveCustomerScheduleDetail(customerScheduleDetailList, effectiveDate);
            #endregion
            #endregion

            #region 获取所有生产单明细
            criteria = DetachedCriteria.For<OrderDetail>();
            criteria.CreateAlias("OrderHead", "od");

            criteria.Add(Expression.Eq("od.Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            criteria.Add(Expression.Eq("od.SubType", BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML));
            criteria.Add(Expression.In("od.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT, BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS }));
            criteria.Add(Expression.Or(Expression.GtProperty("OrderedQty", "ReceivedQty"), Expression.IsNull("ReceivedQty")));
            //criteria.Add(Expression.Ge("od.StartTime", effectiveDate));
            criteria.AddOrder(Order.Asc("od.StartTime"));

            IList<OrderDetail> workOrderDetailList = this.criteriaMgr.FindAll<OrderDetail>(criteria);
            #endregion

            #region 循环生产线生成生产计划
            if (flowList != null && flowList.Count > 0)
            {
                foreach (object[] flow in flowList)
                {
                    string flowCode = (string)flow[0];
                    string mrpOption = (string)flow[1];

                    var targetWorkOrderDetailList = from det in workOrderDetailList
                                                    where det.OrderHead.Flow == flowCode
                                                    select det;

                    var targetCustomerScheduleDetailList = from det in effectiveCustomerScheduleDetailList
                                                           where det.CustomerSchedule.Flow == flowCode
                                                           select det;

                    if (mrpOption == BusinessConstants.CODE_MASTER_MRP_OPTION_VALUE_ORDER_BEFORE_PLAN)
                    {
                        IListHelper.AddRange(mrpShipPlanList, TransferWorkOrderAndCustomerPlan2ShipPlan(targetWorkOrderDetailList != null ? targetWorkOrderDetailList.ToList() : null,
                            targetCustomerScheduleDetailList != null ? targetCustomerScheduleDetailList.ToList() : null,
                            effectiveDate, dateTimeNow, user));
                    }
                    else if (mrpOption == BusinessConstants.CODE_MASTER_MRP_OPTION_VALUE_PLAN_ONLY)
                    {
                        IListHelper.AddRange(mrpShipPlanList, TransferCustomerPlan2ShipPlan(targetCustomerScheduleDetailList != null ? targetCustomerScheduleDetailList.ToList() : null, effectiveDate, dateTimeNow, user));
                    }
                    else if (mrpOption == BusinessConstants.CODE_MASTER_MRP_OPTION_VALUE_ORDER_ONLY)
                    {
                        IListHelper.AddRange(mrpShipPlanList, TransferWorkOrder2ShipPlan(targetWorkOrderDetailList != null ? targetWorkOrderDetailList.ToList() : null, effectiveDate, dateTimeNow, user));
                    }
                    else
                    {
                        throw new TechnicalException("MRP option " + mrpOption + " is not valid.");
                    }
                }
            }
            #endregion
            #endregion

            #region 查询并缓存所有FlowDetail
            criteria = DetachedCriteria.For<FlowDetail>();
            criteria.CreateAlias("Flow", "f");
            criteria.CreateAlias("Item", "i");
            criteria.CreateAlias("i.Uom", "iu");
            criteria.CreateAlias("Uom", "u");
            criteria.CreateAlias("i.Location", "il", JoinType.LeftOuterJoin);
            criteria.CreateAlias("i.Bom", "ib", JoinType.LeftOuterJoin);
            criteria.CreateAlias("i.Routing", "ir", JoinType.LeftOuterJoin);
            criteria.CreateAlias("LocationFrom", "lf", JoinType.LeftOuterJoin);
            criteria.CreateAlias("LocationTo", "lt", JoinType.LeftOuterJoin);
            criteria.CreateAlias("f.LocationFrom", "flf", JoinType.LeftOuterJoin);
            criteria.CreateAlias("f.LocationTo", "flt", JoinType.LeftOuterJoin);
            criteria.CreateAlias("Bom", "b", JoinType.LeftOuterJoin);
            criteria.CreateAlias("Routing", "r", JoinType.LeftOuterJoin);
            criteria.CreateAlias("f.Routing", "fr", JoinType.LeftOuterJoin);

            criteria.SetProjection(Projections.ProjectionList()
                .Add(Projections.GroupProperty("f.Code").As("Flow"))
                .Add(Projections.GroupProperty("f.Type").As("FlowType"))
                .Add(Projections.GroupProperty("i.Code").As("Item"))
                .Add(Projections.GroupProperty("lf.Code").As("LocationFrom"))
                .Add(Projections.GroupProperty("lt.Code").As("LocationTo"))
                .Add(Projections.GroupProperty("flf.Code").As("FlowLocationFrom"))
                .Add(Projections.GroupProperty("flt.Code").As("FlowLocationTo"))
                .Add(Projections.GroupProperty("MRPWeight").As("MRPWeight"))
                .Add(Projections.GroupProperty("b.Code").As("Bom"))
                .Add(Projections.GroupProperty("r.Code").As("Routing"))
                .Add(Projections.GroupProperty("fr.Code").As("FlowRouting"))
                .Add(Projections.GroupProperty("iu.Code").As("ItemUom"))
                .Add(Projections.GroupProperty("u.Code").As("Uom"))
                .Add(Projections.GroupProperty("f.LeadTime").As("LeadTime"))
                .Add(Projections.GroupProperty("ib.Code").As("ItemBom"))
                .Add(Projections.GroupProperty("ir.Code").As("ItemRouting"))
                .Add(Projections.GroupProperty("il.Code").As("ItemLocation"))
                .Add(Projections.GroupProperty("UnitCount").As("UnitCount"))
                .Add(Projections.GroupProperty("i.Desc1").As("ItemDesc1"))
                .Add(Projections.GroupProperty("i.Desc2").As("ItemDesc2"))
                .Add(Projections.GroupProperty("Id").As("Id"))
                );

            criteria.Add(Expression.Eq("f.IsActive", true));
            //criteria.Add(Expression.Not(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)));
            criteria.Add(Expression.Gt("MRPWeight", 0));
            criteria.Add(Expression.Eq("f.IsMRP", true));

            IList<object[]> flowDetailList = this.criteriaMgr.FindAll<object[]>(criteria);

            var targetFlowDetailList = from fd in flowDetailList
                                       select new FlowDetailSnapShot
                                       {
                                           Flow = (string)fd[0],
                                           FlowType = (string)fd[1],
                                           Item = (string)fd[2],
                                           LocationFrom = fd[3] != null ? (string)fd[3] : fd[5] != null ? (string)fd[5] : (string)fd[16],
                                           LocationTo = fd[4] != null ? (string)fd[4] : (string)fd[6],
                                           MRPWeight = (int)fd[7],
                                           Bom = (string)fd[1] != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION ? null : fd[8] != null ? (string)fd[8] : fd[14] != null ? (string)fd[14] : (string)fd[2],  //FlowDetail --> Item.Bom --> Item.Code
                                           Routing = (string)fd[1] != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION ? null : fd[9] != null ? (string)fd[9] : fd[10] != null ? (string)fd[10] : fd[15] != null ? (string)fd[15] : null, //FlowDetail --> Flow --> Item.Routing
                                           BaseUom = (string)fd[11],
                                           Uom = (string)fd[12],
                                           LeadTime = fd[13] != null ? (decimal)fd[13] : 0,
                                           UnitCount = (decimal)fd[17],
                                           ItemDescription = ((fd[18] != null ? fd[18] : string.Empty) + ((fd[19] != null && fd[19] != string.Empty) ? "[" + fd[19] + "]" : string.Empty)),
                                           Id = (int)fd[20]
                                       };

            IList<FlowDetailSnapShot> flowDetailSnapShotList = new List<FlowDetailSnapShot>();
            if (targetFlowDetailList != null && targetFlowDetailList.Count() > 0)
            {
                flowDetailSnapShotList = targetFlowDetailList.ToList();
            }

            #region 处理引用
            if (flowDetailSnapShotList != null && flowDetailSnapShotList.Count > 0)
            {
                criteria = DetachedCriteria.For<Flow>();

                criteria.CreateAlias("LocationFrom", "flf", JoinType.LeftOuterJoin);
                criteria.CreateAlias("LocationTo", "flt", JoinType.LeftOuterJoin);
                criteria.CreateAlias("Routing", "fr", JoinType.LeftOuterJoin);

                criteria.SetProjection(Projections.ProjectionList()
                    .Add(Projections.GroupProperty("Code").As("Flow"))
                    .Add(Projections.GroupProperty("Type").As("FlowType"))
                    .Add(Projections.GroupProperty("ReferenceFlow").As("ReferenceFlow"))
                    .Add(Projections.GroupProperty("flf.Code").As("FlowLocationFrom"))
                    .Add(Projections.GroupProperty("flt.Code").As("FlowLocationTo"))
                    .Add(Projections.GroupProperty("fr.Code").As("FlowRouting"))
                    );

                criteria.Add(Expression.Eq("IsActive", true));
                criteria.Add(Expression.IsNotNull("ReferenceFlow"));
                criteria.Add(Expression.Eq("IsMRP", true));
                criteria.Add(Expression.Not(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)));

                IList<object[]> refFlowList = this.criteriaMgr.FindAll<object[]>(criteria);

                if (refFlowList != null && refFlowList.Count > 0)
                {
                    foreach (object[] refFlow in refFlowList)
                    {
                        var refFlowDetailList = from fd in flowDetailSnapShotList
                                                where string.Compare(fd.Flow, (string)refFlow[2]) == 0
                                                select fd;

                        if (refFlowDetailList != null && refFlowDetailList.Count() > 0)
                        {
                            IListHelper.AddRange(flowDetailSnapShotList, (from fd in refFlowDetailList
                                                                          select new FlowDetailSnapShot
                                                                          {
                                                                              Flow = (string)refFlow[0],
                                                                              FlowType = (string)refFlow[1],
                                                                              Item = fd.Item,
                                                                              LocationFrom = (string)refFlow[3],
                                                                              LocationTo = (string)refFlow[4],
                                                                              MRPWeight = fd.MRPWeight,
                                                                              Bom = fd.Bom,
                                                                              Routing = (string)refFlow[5],
                                                                              BaseUom = fd.BaseUom,
                                                                              Uom = fd.Uom,
                                                                              LeadTime = fd.LeadTime,
                                                                              UnitCount = fd.UnitCount,
                                                                              ItemDescription = fd.ItemDescription
                                                                          }).ToList());
                        }
                    }
                }
            }
            #endregion
            #endregion

            #region 补充安全库存
            if (inventoryBalanceList != null && inventoryBalanceList.Count > 0)
            {
                var lackInventoryList = from inv in inventoryBalanceList
                                        where inv.ActiveQty < 0  //可用库存小于0，要补充安全库存
                                        select inv;

                if (lackInventoryList != null && lackInventoryList.Count() > 0)
                {
                    foreach (MrpLocationLotDetail lackInventory in lackInventoryList)
                    {
                        #region 扣减在途，不考虑在途的到货时间
                        var transitConsumed = from trans in transitInventoryList
                                              where trans.Location == lackInventory.Location
                                                  && trans.Item == lackInventory.Item && trans.Qty > 0
                                              select trans;

                        if (transitConsumed != null && transitConsumed.Count() > 0)
                        {
                            foreach (TransitInventory inventory in transitConsumed)
                            {
                                if ((-lackInventory.ActiveQty) > inventory.Qty)
                                {
                                    lackInventory.Qty += inventory.Qty;
                                    inventory.Qty = 0;
                                }
                                else
                                {
                                    inventory.Qty += lackInventory.ActiveQty;
                                    lackInventory.Qty = lackInventory.SafeQty;

                                    break;
                                }
                            }
                        }

                        if (lackInventory.ActiveQty == 0)
                        {
                            //在途满足库存短缺
                            continue;
                        }
                        else
                        {
                            //在途不满足库存短缺
                            Item item = this.itemMgr.CheckAndLoadItem(lackInventory.Item);

                            MrpReceivePlan mrpReceivePlan = new MrpReceivePlan();
                            mrpReceivePlan.Item = lackInventory.Item;
                            mrpReceivePlan.Uom = item.Uom.Code;
                            mrpReceivePlan.Location = lackInventory.Location;
                            mrpReceivePlan.Qty = -lackInventory.ActiveQty;
                            mrpReceivePlan.UnitCount = item.UnitCount;
                            mrpReceivePlan.ReceiveTime = effectiveDate;
                            mrpReceivePlan.SourceType = BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_SAFE_STOCK;
                            mrpReceivePlan.SourceDateType = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY;
                            mrpReceivePlan.SourceId = lackInventory.Location;
                            mrpReceivePlan.SourceUnitQty = 1;
                            mrpReceivePlan.SourceItemCode = item.Code;
                            mrpReceivePlan.SourceItemDescription = item.Description;
                            mrpReceivePlan.EffectiveDate = effectiveDate;
                            mrpReceivePlan.CreateDate = dateTimeNow;
                            mrpReceivePlan.CreateUser = user.Code;
                            mrpReceivePlan.ItemDescription = item.Description;

                            //this.mrpReceivePlanMgr.CreateMrpReceivePlan(mrpReceivePlan);

                            log.Debug("Create receive plan for safe stock, location[" + mrpReceivePlan.Location + "], item[" + mrpReceivePlan.Item + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "]");

                            CalculateNextShipPlan(mrpReceivePlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                        }
                        #endregion
                    }
                }
            }
            #endregion

            #region 循环生成入库计划/发货计划
            if (mrpShipPlanList != null && mrpShipPlanList.Count > 0)
            {
                var sortedMrpShipPlanList = from plan in mrpShipPlanList
                                            orderby plan.StartTime ascending
                                            select plan;

                foreach (MrpShipPlan mrpShipPlan in sortedMrpShipPlanList)
                {
                    NestCalculateMrpShipPlanAndReceivePlan(mrpShipPlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                }
            }
            #endregion

            #region 记录MRP Run日志
            MrpRunLog currLog = new MrpRunLog();
            currLog.RunDate = effectiveDate;
            currLog.StartTime = dateTimeNow;
            currLog.EndTime = DateTime.Now;
            currLog.CreateDate = dateTimeNow;
            currLog.CreateUser = user.Code;

            this.mrpRunLogMgr.CreateMrpRunLog(currLog);
            #endregion

            log.Info("End run mrp effectivedate:" + effectiveDate.ToLongDateString());
        }

        #region Private Methods
        private void ProcessEffectiveInventoryBalance(ref IList<MrpLocationLotDetail> inventoryBalanceList, object[] invLoc, IList<SafeInventory> safeQtyList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            MrpLocationLotDetail matchedInv = (from g in inventoryBalanceList
                                               where g.Location == ((string)invLoc[0])
                                                  && g.Item == ((string)invLoc[1])
                                               select g).FirstOrDefault();

            if (matchedInv != null)
            {
                matchedInv.Qty += (decimal)invLoc[2];
            }
            else
            {
                MrpLocationLotDetail locationLotDetail = new MrpLocationLotDetail();
                locationLotDetail.Location = (string)invLoc[0];
                locationLotDetail.Item = (string)invLoc[1];
                locationLotDetail.Qty = (decimal)invLoc[2];
                locationLotDetail.SafeQty = (from g in safeQtyList
                                             where g.Location == locationLotDetail.Location
                                                && g.Item == locationLotDetail.Item
                                             select g.SafeQty).FirstOrDefault();

                inventoryBalanceList.Add(locationLotDetail);
            }
        }

        private IList<MrpShipPlan> TransferWorkOrder2ShipPlan(IList<OrderDetail> workOrderDetailList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            IList<MrpShipPlan> mrpShipPlanList = new List<MrpShipPlan>();

            if (workOrderDetailList != null && workOrderDetailList.Count > 0)
            {
                foreach (OrderDetail workOrderDetail in workOrderDetailList)
                {
                    OrderHead orderHead = workOrderDetail.OrderHead;
                    MrpShipPlan mrpShipPlan = new MrpShipPlan();

                    if (workOrderDetail.OrderHead.StartTime < effectiveDate)
                    {
                        mrpShipPlan.IsExpire = true;
                        mrpShipPlan.ExpireStartTime = workOrderDetail.OrderHead.StartTime;
                    }
                    else
                    {
                        mrpShipPlan.IsExpire = false;
                    }
                    mrpShipPlan.Flow = workOrderDetail.OrderHead.Flow;
                    mrpShipPlan.FlowType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION;
                    mrpShipPlan.Item = workOrderDetail.Item.Code;
                    mrpShipPlan.ItemDescription = workOrderDetail.Item.Description;
                    mrpShipPlan.Bom = workOrderDetail.Bom.Code;
                    if (mrpShipPlan.IsExpire)
                    {
                        mrpShipPlan.StartTime = DateTime.Now;
                    }
                    else
                    {
                        mrpShipPlan.StartTime = workOrderDetail.OrderHead.StartTime;
                    }
                    if (workOrderDetail.OrderHead.WindowTime < effectiveDate)
                    {
                        mrpShipPlan.WindowTime = DateTime.Now;
                    }
                    else
                    {
                        mrpShipPlan.WindowTime = workOrderDetail.OrderHead.WindowTime;
                    }
                    mrpShipPlan.LocationFrom = workOrderDetail.DefaultLocationFrom.Code;
                    mrpShipPlan.SourceType = BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_ORDER;
                    mrpShipPlan.SourceDateType = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY;
                    mrpShipPlan.SourceId = workOrderDetail.Id.ToString();
                    mrpShipPlan.SourceUnitQty = 1;
                    mrpShipPlan.SourceItemCode = workOrderDetail.Item.Code;
                    mrpShipPlan.SourceItemDescription = workOrderDetail.Item.Description;
                    mrpShipPlan.EffectiveDate = effectiveDate;
                    mrpShipPlan.Qty = (workOrderDetail.OrderedQty - (workOrderDetail.ShippedQty.HasValue ? workOrderDetail.ShippedQty.Value : 0)); ;
                    mrpShipPlan.Uom = workOrderDetail.Uom.Code;
                    mrpShipPlan.BaseUom = workOrderDetail.Item.Uom.Code;
                    mrpShipPlan.UnitCount = workOrderDetail.UnitCount;
                    if (mrpShipPlan.Uom != mrpShipPlan.BaseUom)
                    {
                        mrpShipPlan.UnitQty = this.uomConversionMgr.ConvertUomQty(mrpShipPlan.Item, mrpShipPlan.Uom, 1, mrpShipPlan.BaseUom);
                    }
                    else
                    {
                        mrpShipPlan.UnitQty = 1;
                    }
                    mrpShipPlan.CreateDate = dateTimeNow;
                    mrpShipPlan.CreateUser = user.Code;

                    this.mrpShipPlanMgr.CreateMrpShipPlan(mrpShipPlan);
                    mrpShipPlanList.Add(mrpShipPlan);

                    log.Debug("Create ship plan for sales order, flow[" + mrpShipPlan.Flow + "], item[" + mrpShipPlan.Item + "], qty[" + mrpShipPlan.Qty + "], sourceType[" + mrpShipPlan.SourceType + "], sourceId[" + (mrpShipPlan.SourceId != null ? mrpShipPlan.SourceId : string.Empty) + "]");
                }
            }

            return mrpShipPlanList;
        }

        private IList<MrpShipPlan> TransferCustomerPlan2ShipPlan(IList<CustomerScheduleDetail> customerScheduleDetaillList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            IList<MrpShipPlan> mrpShipPlanList = new List<MrpShipPlan>();

            if (customerScheduleDetaillList != null && customerScheduleDetaillList.Count() > 0)
            {
                foreach (CustomerScheduleDetail customerScheduleDetail in customerScheduleDetaillList)
                {
                    Item item = this.itemMgr.CheckAndLoadItem(customerScheduleDetail.Item);
                    MrpShipPlan mrpShipPlan = new MrpShipPlan();

                    mrpShipPlan.Flow = customerScheduleDetail.CustomerSchedule.Flow;
                    mrpShipPlan.FlowType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION;
                    mrpShipPlan.Item = customerScheduleDetail.Item;
                    mrpShipPlan.ItemDescription = customerScheduleDetail.ItemDescription;
                    mrpShipPlan.ItemReference = customerScheduleDetail.ItemReference;
                    mrpShipPlan.Bom = customerScheduleDetail.Bom;
                    #region 查询BomCode, ScheduleDetail.Bom > FlowDetail.Bom > Item.Bom > Item.Code
                    if (mrpShipPlan.Bom == null || mrpShipPlan.Bom.Trim() == string.Empty)
                    {
                        DetachedCriteria criteria = DetachedCriteria.For<FlowDetail>();

                        criteria.CreateAlias("Flow", "f");
                        criteria.CreateAlias("Item", "i");
                        criteria.CreateAlias("Bom", "b");

                        criteria.Add(Expression.Eq("f.Code", mrpShipPlan.Flow));
                        criteria.Add(Expression.Eq("i.Code", mrpShipPlan.Item));
                        criteria.Add(Expression.Or(Expression.IsNull("StartDate"), Expression.Le("StartDate", customerScheduleDetail.StartTime)));
                        criteria.Add(Expression.Or(Expression.IsNull("EndDate"), Expression.Ge("EndDate", customerScheduleDetail.DateFrom)));

                        criteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("b.Code")));

                        IList bomList = this.criteriaMgr.FindAll(criteria);
                        if (bomList != null && bomList.Count > 0 && bomList[0] != null)
                        {
                            mrpShipPlan.Bom = (string)bomList[0];
                        }
                    }

                    if ((mrpShipPlan.Bom == null || mrpShipPlan.Bom.Trim() == string.Empty) && item.Bom != null)
                    {
                        mrpShipPlan.Bom = item.Bom.Code;
                    }

                    if (mrpShipPlan.Bom == null || mrpShipPlan.Bom.Trim() == string.Empty)
                    {
                        mrpShipPlan.Bom = item.Code;
                    }
                    #endregion
                    mrpShipPlan.StartTime = customerScheduleDetail.StartTime;
                    mrpShipPlan.WindowTime = customerScheduleDetail.DateFrom;
                    mrpShipPlan.LocationFrom = customerScheduleDetail.Location;
                    mrpShipPlan.SourceType = BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_CUSTOMER_PLAN;
                    mrpShipPlan.SourceDateType = customerScheduleDetail.Type;
                    mrpShipPlan.SourceId = customerScheduleDetail.Id.ToString();
                    mrpShipPlan.SourceUnitQty = 1;
                    mrpShipPlan.SourceItemCode = item.Code;
                    mrpShipPlan.SourceItemDescription = item.Description;
                    mrpShipPlan.EffectiveDate = effectiveDate;
                    mrpShipPlan.Qty = customerScheduleDetail.Qty;
                    mrpShipPlan.Uom = customerScheduleDetail.Uom;
                    mrpShipPlan.UnitCount = customerScheduleDetail.UnitCount;
                    mrpShipPlan.BaseUom = item.Uom.Code;
                    if (mrpShipPlan.Uom != mrpShipPlan.BaseUom)
                    {
                        mrpShipPlan.UnitQty = this.uomConversionMgr.ConvertUomQty(mrpShipPlan.Item, mrpShipPlan.Uom, 1, mrpShipPlan.BaseUom);
                    }
                    else
                    {
                        mrpShipPlan.UnitQty = 1;
                    }
                    mrpShipPlan.CreateDate = dateTimeNow;
                    mrpShipPlan.CreateUser = user.Code;

                    this.mrpShipPlanMgr.CreateMrpShipPlan(mrpShipPlan);
                    mrpShipPlanList.Add(mrpShipPlan);

                    log.Debug("Create ship plan for customer schedule, flow[" + mrpShipPlan.Flow + "], item[" + mrpShipPlan.Item + "], qty[" + mrpShipPlan.Qty + "], sourceType[" + mrpShipPlan.SourceType + "], sourceId[" + (mrpShipPlan.SourceId != null ? mrpShipPlan.SourceId : string.Empty) + "]");
                }
            }

            return mrpShipPlanList;
        }

        private IList<MrpShipPlan> TransferWorkOrderAndCustomerPlan2ShipPlan(IList<OrderDetail> workOrderDetailList, IList<CustomerScheduleDetail> customerScheduleDetaillList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            IList<MrpShipPlan> mrpShipPlanList = TransferWorkOrder2ShipPlan(workOrderDetailList, effectiveDate, dateTimeNow, user);

            if (mrpShipPlanList != null && mrpShipPlanList.Count > 0
                && customerScheduleDetaillList != null && customerScheduleDetaillList.Count > 0)
            {
                #region 查找路线+零件最后一次的开始日期
                var lastShipPlanByFlowAndItem = from plan in mrpShipPlanList
                                                group plan by new { plan.Flow, plan.Item } into result
                                                select new
                                                {
                                                    Flow = result.Key.Flow,
                                                    Item = result.Key.Item,
                                                    MaxWindowTime = result.Max(p => p.WindowTime)
                                                };
                #endregion

                #region 根据最后一次开始日期过滤掉无效的客户计划
                foreach (var lastPlan in lastShipPlanByFlowAndItem)
                {
                    var inactiveList = from plan in customerScheduleDetaillList
                                       where plan.CustomerSchedule.Flow == lastPlan.Flow
                                         && plan.Item == lastPlan.Item
                                         && plan.DateFrom <= lastPlan.MaxWindowTime
                                       select plan;

                    if (inactiveList != null && inactiveList.Count() > 0)
                    {
                        IList<CustomerScheduleDetail> removeCustomerScheduleDetaillList = new List<CustomerScheduleDetail>();
                        foreach (CustomerScheduleDetail det in inactiveList)
                        {
                            removeCustomerScheduleDetaillList.Add(det);
                        }

                        foreach (CustomerScheduleDetail det in removeCustomerScheduleDetaillList)
                        {
                            customerScheduleDetaillList.Remove(det);
                        }
                    }
                }
                #endregion
            }

            IListHelper.AddRange(mrpShipPlanList, TransferCustomerPlan2ShipPlan(customerScheduleDetaillList, effectiveDate, dateTimeNow, user));

            return mrpShipPlanList;
        }

        private void NestCalculateMrpShipPlanAndReceivePlan(MrpShipPlan mrpShipPlan, IList<MrpLocationLotDetail> inventoryBalanceList, IList<TransitInventory> transitInventoryList, IList<FlowDetailSnapShot> flowDetailSnapShotList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            //if (mrpShipPlan.IsExpire)
            //{
            //    return; //过期需求不往下传递
            //}

            if (mrpShipPlan.LocationFrom != null && mrpShipPlan.LocationFrom.Trim() != string.Empty)
            {
                #region 消耗本机物料
                if (mrpShipPlan.Qty == 0)
                {
                    return;
                }
                else if (mrpShipPlan.Qty < 0)
                {
                    throw new TechnicalException("Mrp Ship Plan Qty Can't < 0");
                }

                //回冲库存
                BackFlushInventory(mrpShipPlan, mrpShipPlan.Item, mrpShipPlan.UnitQty, inventoryBalanceList);

                //回冲在途
                BackFlushTransitInventory(mrpShipPlan, mrpShipPlan.Item, mrpShipPlan.UnitQty, transitInventoryList);
                //if (mrpShipPlan.StartTime >= effectiveDate      //只有StartTime>= EffectiveDate才能回冲
                //|| mrpShipPlan.SourceType == BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_SAFE_STOCK)  //或者回冲安全库存
                //{

                //}
                #endregion

                #region 生成入库计划
                if (mrpShipPlan.Qty == 0)
                {
                    return;
                }

                IList<MrpReceivePlan> currMrpReceivePlanList = new List<MrpReceivePlan>();
                if (mrpShipPlan.FlowType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                {
                    #region 非生产直接从发运计划变为入库计划
                    MrpReceivePlan mrpReceivePlan = new MrpReceivePlan();
                    mrpReceivePlan.IsExpire = mrpShipPlan.IsExpire;
                    mrpReceivePlan.ExpireStartTime = mrpShipPlan.ExpireStartTime;
                    mrpReceivePlan.Item = mrpShipPlan.Item;
                    mrpReceivePlan.ItemDescription = mrpReceivePlan.ItemDescription;
                    mrpReceivePlan.ItemReference = mrpReceivePlan.ItemReference;
                    mrpReceivePlan.Location = mrpShipPlan.LocationFrom;
                    mrpReceivePlan.Qty = mrpShipPlan.Qty * mrpShipPlan.UnitQty;  //转换为库存单位
                    mrpReceivePlan.UnitCount = mrpShipPlan.UnitCount;
                    mrpReceivePlan.Uom = mrpShipPlan.BaseUom;
                    mrpReceivePlan.ReceiveTime = mrpShipPlan.StartTime;
                    mrpReceivePlan.SourceId = mrpShipPlan.SourceId;
                    mrpReceivePlan.SourceDateType = mrpShipPlan.SourceDateType;
                    mrpReceivePlan.SourceType = mrpShipPlan.SourceType;
                    mrpReceivePlan.SourceUnitQty = mrpShipPlan.SourceUnitQty * mrpShipPlan.UnitQty;
                    mrpReceivePlan.SourceItemCode = mrpShipPlan.SourceItemCode;
                    mrpReceivePlan.SourceItemDescription = mrpShipPlan.SourceItemDescription;
                    mrpReceivePlan.EffectiveDate = effectiveDate;
                    mrpReceivePlan.CreateDate = dateTimeNow;
                    mrpReceivePlan.CreateUser = user.Code;
                    mrpReceivePlan.FlowDetailIdList = mrpShipPlan.FlowDetailIdList;

                    //this.mrpReceivePlanMgr.CreateMrpReceivePlan(mrpReceivePlan);

                    currMrpReceivePlanList.Add(mrpReceivePlan);

                    log.Debug("Transfer ship plan flow[" + mrpShipPlan.Flow + "], qty[" + mrpShipPlan.Qty + "] to receive plan location[" + mrpReceivePlan.Location + "], item[" + mrpReceivePlan.Item + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "]");
                    #endregion
                }
                else
                {
                    #region 生产，需要分解Bom
                    log.Debug("Production flow start resolve bom");
                    Bom bom = this.bomMgr.CheckAndLoadBom(mrpShipPlan.Bom);
                    IList<BomDetail> bomDetailList = this.bomDetailMgr.GetFlatBomDetail(mrpShipPlan.Bom, mrpShipPlan.StartTime);

                    if (bomDetailList != null && bomDetailList.Count > 0)
                    {
                        IList<RoutingDetail> routingDetailList = null;
                        if (mrpShipPlan.Routing != null && mrpShipPlan.Routing.Trim() != null)
                        {
                            routingDetailList = this.routingDetailMgr.GetRoutingDetail(mrpShipPlan.Routing, mrpShipPlan.StartTime);
                        }

                        foreach (BomDetail bomDetail in bomDetailList)
                        {
                            log.Debug("Find bomDetail FG[" + mrpShipPlan.Item + "], RM[" + bomDetail.Item.Code + "]");

                            #region 创建MrpReceivePlan
                            MrpReceivePlan mrpReceivePlan = new MrpReceivePlan();
                            mrpReceivePlan.IsExpire = mrpShipPlan.IsExpire;
                            mrpReceivePlan.ExpireStartTime = mrpShipPlan.ExpireStartTime;
                            mrpReceivePlan.Item = bomDetail.Item.Code;
                            mrpReceivePlan.UnitCount = bomDetail.Item.UnitCount;
                            mrpReceivePlan.ItemDescription = bomDetail.Item.Description;
                            #region 取库位
                            mrpReceivePlan.Location = mrpShipPlan.LocationFrom;  //默认库位
                            if (bomDetail.Location != null)
                            {
                                mrpReceivePlan.Location = bomDetail.Location.Code;
                            }
                            else
                            {
                                if (routingDetailList != null)
                                {
                                    Location location = (from det in routingDetailList
                                                         where det.Operation == bomDetail.Operation
                                                         && det.Reference == bomDetail.Reference
                                                         select det.Location).FirstOrDefault();

                                    if (location != null)
                                    {
                                        mrpReceivePlan.Location = location.Code;
                                    }
                                }
                            }
                            #endregion
                            decimal fgQty = mrpShipPlan.Qty;
                            decimal fgSourceUnitQty = mrpShipPlan.SourceUnitQty;
                            if (mrpShipPlan.Uom != bom.Uom.Code)
                            {
                                //成品数量转换为Bom单位
                                fgQty = this.uomConversionMgr.ConvertUomQty(mrpShipPlan.Item, mrpShipPlan.Uom, fgQty, bom.Uom.Code);
                                fgSourceUnitQty = this.uomConversionMgr.ConvertUomQty(mrpShipPlan.Item, mrpShipPlan.Uom, fgSourceUnitQty, bom.Uom.Code);
                            }
                            mrpReceivePlan.Uom = bomDetail.Item.Uom.Code;
                            #region 计算用量
                            //BomDetail上的单位
                            mrpReceivePlan.Qty = fgQty //成品用量                                  
                                * bomDetail.RateQty //乘以单位用量
                                * (1 + bomDetail.ScrapPercentage);  //乘以损耗
                            mrpReceivePlan.SourceUnitQty = fgSourceUnitQty
                                * bomDetail.RateQty //乘以单位用量
                                * (1 + bomDetail.ScrapPercentage);  //乘以损耗
                            if (mrpReceivePlan.Uom != bomDetail.Uom.Code)
                            {
                                //转换为库存单位
                                mrpReceivePlan.Qty = this.uomConversionMgr.ConvertUomQty(mrpReceivePlan.Item, bomDetail.Uom.Code, mrpReceivePlan.Qty, mrpReceivePlan.Uom);
                                mrpReceivePlan.SourceUnitQty = this.uomConversionMgr.ConvertUomQty(mrpReceivePlan.Item, bomDetail.Uom.Code, mrpReceivePlan.SourceUnitQty, mrpReceivePlan.Uom);
                            }

                            #region 消耗本级物料
                            #region 扣减线边库位库存
                            BackFlushInventory(mrpReceivePlan, mrpReceivePlan.Item, 1, inventoryBalanceList);
                            #endregion

                            #region 扣减线边在途库存
                            BackFlushTransitInventory(mrpReceivePlan, mrpReceivePlan.Item, 1, transitInventoryList);
                            #endregion
                            #endregion
                            #endregion

                            mrpReceivePlan.ReceiveTime = mrpShipPlan.StartTime;
                            mrpReceivePlan.SourceId = mrpShipPlan.SourceId;
                            mrpReceivePlan.SourceDateType = mrpShipPlan.SourceDateType;
                            mrpReceivePlan.SourceType = mrpShipPlan.SourceType;
                            mrpReceivePlan.SourceItemCode = mrpShipPlan.SourceItemCode;
                            mrpReceivePlan.SourceItemDescription = mrpShipPlan.SourceItemDescription;
                            mrpReceivePlan.EffectiveDate = effectiveDate;
                            mrpReceivePlan.CreateDate = dateTimeNow;
                            mrpReceivePlan.CreateUser = user.Code;
                            mrpReceivePlan.FlowDetailIdList = mrpShipPlan.FlowDetailIdList;

                            //this.mrpReceivePlanMgr.CreateMrpReceivePlan(mrpReceivePlan);
                            currMrpReceivePlanList.Add(mrpReceivePlan);
                            #endregion
                        }
                    }
                    else
                    {
                        log.Error("Can't find bom detial for code " + mrpShipPlan.Bom);
                    }
                    log.Debug("Production flow end resolve bom");
                    #endregion
                }
                #endregion

                #region 计算下游发运计划
                foreach (MrpReceivePlan mrpReceivePlan in currMrpReceivePlanList)
                {
                    log.Debug("Transfer ship plan flow[" + mrpShipPlan.Flow + "], qty[" + mrpShipPlan.Qty + "] to receive plan location[" + mrpReceivePlan.Location + "], item[" + mrpReceivePlan.Item + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "]");
                    CalculateNextShipPlan(mrpReceivePlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                }
                #endregion
            }
        }

        private void CalculateNextShipPlan(MrpReceivePlan mrpReceivePlan, IList<MrpLocationLotDetail> inventoryBalanceList, IList<TransitInventory> transitInventoryList, IList<FlowDetailSnapShot> flowDetailSnapShotList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            if (mrpReceivePlan.ReceiveTime < effectiveDate)
            {
                //如果窗口时间小于effectivedate，不往下计算
                //return;
            }

            var nextFlowDetailList = from det in flowDetailSnapShotList
                                     where det.LocationTo == mrpReceivePlan.Location
                                    && det.Item == mrpReceivePlan.Item
                                     select det;

            //#region 如果有多条下游路线，根据Item.DefaultFlow过滤
            //if (nextFlowDetailList != null && nextFlowDetailList.Count() > 1)
            //{
            //    Item item = this.itemMgr.LoadItem(mrpReceivePlan.Item);

            //    if (item.DefaultFlow != null && item.DefaultFlow.Trim() != string.Empty)
            //    {
            //        var defaultFlow = from det in nextFlowDetailList
            //                          where det.Flow == item.DefaultFlow
            //                          select det;

            //        if (defaultFlow != null && defaultFlow.Count() > 0)
            //        {
            //            nextFlowDetailList = defaultFlow;
            //        }
            //    }
            //}
            //#endregion

            if (nextFlowDetailList != null && nextFlowDetailList.Count() > 0)
            {
                int mrpWeight = nextFlowDetailList.Sum(p => p.MRPWeight);
                decimal rate = mrpReceivePlan.Qty / mrpWeight;
                decimal remainQty = mrpReceivePlan.Qty;

                for (int i = 0; i < nextFlowDetailList.Count(); i++)
                {
                    FlowDetailSnapShot flowDetail = nextFlowDetailList.ElementAt(i);

                    MrpShipPlan mrpShipPlan = new MrpShipPlan();

                    if (mrpReceivePlan.ContainFlowDetailId(flowDetail.Id))
                    {
                        log.Error("Cycle Flow Detail Find when transfer receive plan location[" + mrpReceivePlan.Location + "], item[" + mrpReceivePlan.Item + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "] to ship plan flow[" + flowDetail.Flow + "]");
                        continue;
                    }
                    else
                    {
                        mrpShipPlan.FlowDetailIdList = mrpReceivePlan.FlowDetailIdList;
                        mrpShipPlan.AddFlowDetailId(flowDetail.Id);
                    }

                    mrpShipPlan.Flow = flowDetail.Flow;
                    mrpShipPlan.FlowType = flowDetail.FlowType;
                    mrpShipPlan.Item = flowDetail.Item;
                    mrpShipPlan.ItemDescription = flowDetail.ItemDescription;
                    if (mrpReceivePlan.SourceDateType != BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_SAFE_STOCK)
                    {
                        mrpShipPlan.StartTime = mrpReceivePlan.ReceiveTime.AddHours(-Convert.ToDouble(flowDetail.LeadTime));
                    }
                    else
                    {
                        mrpShipPlan.StartTime = mrpReceivePlan.ReceiveTime;
                    }
                    if (mrpShipPlan.StartTime < effectiveDate)
                    {
                        mrpShipPlan.IsExpire = true;
                        mrpShipPlan.ExpireStartTime = mrpShipPlan.StartTime;
                        mrpShipPlan.StartTime = dateTimeNow;
                    }
                    else
                    {
                        mrpShipPlan.IsExpire = false;
                    }
                    mrpShipPlan.WindowTime = mrpReceivePlan.ReceiveTime;
                    mrpShipPlan.LocationFrom = flowDetail.LocationFrom;
                    mrpShipPlan.LocationTo = flowDetail.LocationTo;
                    mrpShipPlan.SourceType = mrpReceivePlan.SourceType;
                    mrpShipPlan.SourceDateType = mrpReceivePlan.SourceDateType;
                    mrpShipPlan.SourceId = mrpReceivePlan.SourceId;
                    mrpShipPlan.SourceItemCode = mrpReceivePlan.SourceItemCode;
                    mrpShipPlan.SourceItemDescription = mrpReceivePlan.SourceItemDescription;
                    mrpShipPlan.EffectiveDate = effectiveDate;
                    mrpShipPlan.Uom = flowDetail.Uom;
                    mrpShipPlan.BaseUom = flowDetail.BaseUom;
                    if (mrpShipPlan.Uom != mrpShipPlan.BaseUom)
                    {
                        mrpShipPlan.UnitQty = this.uomConversionMgr.ConvertUomQty(mrpShipPlan.Item, mrpShipPlan.Uom, 1, mrpShipPlan.BaseUom);
                    }
                    else
                    {
                        mrpShipPlan.UnitQty = 1;
                    }
                    if (i != nextFlowDetailList.Count() - 1)
                    {
                        remainQty -= rate * flowDetail.MRPWeight;
                        mrpShipPlan.Qty = rate * flowDetail.MRPWeight / mrpShipPlan.UnitQty;   //转换为定单单位                        
                    }
                    else
                    {
                        mrpShipPlan.Qty = remainQty / mrpShipPlan.UnitQty;   //转换为定单单位
                    }
                    mrpShipPlan.SourceUnitQty = mrpReceivePlan.SourceUnitQty / mrpWeight * flowDetail.MRPWeight / mrpShipPlan.UnitQty;
                    mrpShipPlan.UnitCount = flowDetail.UnitCount;
                    mrpShipPlan.Bom = flowDetail.Bom;
                    mrpShipPlan.Routing = flowDetail.Routing;
                    //mrpShipPlan.IsExpire = mrpReceivePlan.IsExpire;
                    //mrpShipPlan.ExpireStartTime = mrpReceivePlan.ExpireStartTime;
                    mrpShipPlan.CreateDate = dateTimeNow;
                    mrpShipPlan.CreateUser = user.Code;

                    this.mrpShipPlanMgr.CreateMrpShipPlan(mrpShipPlan);

                    log.Debug("Transfer receive plan location[" + mrpReceivePlan.Location + "], item[" + mrpReceivePlan.Item + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "] to ship plan flow[" + mrpShipPlan.Flow + "], qty[" + mrpShipPlan.Qty + "]");

                    NestCalculateMrpShipPlanAndReceivePlan(mrpShipPlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                }
            }
            else
            {
                log.Warn("Can't find next flow for location[" + mrpReceivePlan.Location + "], item[" + mrpReceivePlan.Item + "]");
            }
        }

        private void BackFlushInventory(MrpShipPlan mrpShipPlan, string itemCode, decimal unitQty, IList<MrpLocationLotDetail> inventoryBalanceList)
        {
            #region 先消耗库存
            if (mrpShipPlan.Qty == 0)
            {
                return;
            }

            var inventoryConsumed = from inv in inventoryBalanceList
                                    where inv.Location == mrpShipPlan.LocationFrom
                                    && inv.Item == itemCode && inv.Qty > inv.SafeQty
                                    select inv;

            if (inventoryConsumed != null && inventoryConsumed.Count() > 0)
            {
                foreach (MrpLocationLotDetail inventory in inventoryConsumed)
                {
                    if (mrpShipPlan.Qty * unitQty > inventory.ActiveQty)
                    {
                        log.Debug("Backflush inventory for mrpShipPlan flow[" + mrpShipPlan.Flow + "], item[" + itemCode + "], qty[" + mrpShipPlan.Qty + "], sourceType[" + mrpShipPlan.SourceType + "], sourceId[" + (mrpShipPlan.SourceId != null ? mrpShipPlan.SourceId : string.Empty) + "], backflushQty[" + inventory.ActiveQty / unitQty + "]");
                        mrpShipPlan.Qty -= inventory.ActiveQty / unitQty;
                        inventory.Qty = inventory.SafeQty;
                    }
                    else
                    {
                        log.Debug("Backflush inventory for mrpShipPlan flow[" + mrpShipPlan.Flow + "], item[" + itemCode + "], qty[" + mrpShipPlan.Qty + "], sourceType[" + mrpShipPlan.SourceType + "], sourceId[" + (mrpShipPlan.SourceId != null ? mrpShipPlan.SourceId : string.Empty) + "], backflushQty[" + mrpShipPlan.Qty * unitQty + "]");
                        inventory.Qty -= mrpShipPlan.Qty * unitQty;
                        mrpShipPlan.Qty = 0;

                        break;
                    }
                }
            }
            #endregion
        }

        private void BackFlushTransitInventory(MrpShipPlan mrpShipPlan, string itemCode, decimal unitQty, IList<TransitInventory> transitInventoryList)
        {
            #region 再根据ShipPlan的StartTime < 在途库存的EffectiveDate消耗在途库存
            if (mrpShipPlan.Qty == 0)
            {
                return;
            }

            var transitConsumed = from trans in transitInventoryList
                                  where trans.Location == mrpShipPlan.LocationFrom
                                      && trans.Item == itemCode && trans.Qty > 0
                                      && trans.EffectiveDate <= mrpShipPlan.StartTime
                                  select trans;

            if (transitConsumed != null && transitConsumed.Count() > 0)
            {
                foreach (TransitInventory inventory in transitConsumed)
                {
                    if (mrpShipPlan.Qty * unitQty > inventory.Qty)
                    {
                        log.Debug("Backflush transit inventory for mrpShipPlan flow[" + mrpShipPlan.Flow + "], item[" + itemCode + "], qty[" + mrpShipPlan.Qty + "], sourceType[" + mrpShipPlan.SourceType + "], sourceId[" + (mrpShipPlan.SourceId != null ? mrpShipPlan.SourceId : string.Empty) + "], effectiveDate[" + inventory.EffectiveDate + "], backflushQty[" + inventory.Qty / unitQty + "]");
                        mrpShipPlan.Qty -= inventory.Qty / unitQty;
                        inventory.Qty = 0;
                    }
                    else
                    {
                        log.Debug("Backflush transit inventory for mrpShipPlan flow[" + mrpShipPlan.Flow + "], item[" + itemCode + "], qty[" + mrpShipPlan.Qty + "], sourceType[" + mrpShipPlan.SourceType + "], sourceId[" + (mrpShipPlan.SourceId != null ? mrpShipPlan.SourceId : string.Empty) + "], effectiveDate[" + inventory.EffectiveDate + "], backflushQty[" + mrpShipPlan.Qty * unitQty + "]");
                        inventory.Qty -= mrpShipPlan.Qty * unitQty;
                        mrpShipPlan.Qty = 0;

                        break;
                    }
                }
            }
            #endregion
        }

        private void BackFlushInventory(MrpReceivePlan mrpReceivePlan, string itemCode, decimal unitQty, IList<MrpLocationLotDetail> inventoryBalanceList)
        {
            #region 先消耗库存
            if (mrpReceivePlan.Qty == 0)
            {
                return;
            }

            var wipInventoryConsumed = from inv in inventoryBalanceList
                                       where inv.Location == mrpReceivePlan.Location
                                       && inv.Item == mrpReceivePlan.Item && inv.Qty > inv.SafeQty
                                       select inv;

            if (wipInventoryConsumed != null && wipInventoryConsumed.Count() > 0)
            {
                foreach (MrpLocationLotDetail inventory in wipInventoryConsumed)
                {
                    if (mrpReceivePlan.Qty * unitQty > inventory.ActiveQty)
                    {
                        log.Debug("Backflush inventory for mrpReceivePlan location[" + mrpReceivePlan.Location + "], item[" + itemCode + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "], backflushQty[" + inventory.ActiveQty / unitQty + "]");
                        mrpReceivePlan.Qty -= inventory.ActiveQty / unitQty;
                        inventory.Qty = inventory.SafeQty;
                    }
                    else
                    {
                        log.Debug("Backflush inventory for mrpReceivePlan location[" + mrpReceivePlan.Location + "], item[" + itemCode + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "], backflushQty[" + mrpReceivePlan.Qty * unitQty + "]");

                        inventory.Qty -= mrpReceivePlan.Qty * unitQty;
                        mrpReceivePlan.Qty = 0;

                        break;
                    }
                }
            }
            #endregion
        }

        private void BackFlushTransitInventory(MrpReceivePlan mrpReceivePlan, string itemCode, decimal unitQty, IList<TransitInventory> transitInventoryList)
        {
            #region 再根据ShipPlan的StartTime < 在途库存的EffectiveDate消耗在途库存
            if (mrpReceivePlan.Qty == 0)
            {
                return;
            }

            var transitConsumed = from trans in transitInventoryList
                                  where trans.Location == mrpReceivePlan.Location
                                      && trans.Item == itemCode && trans.Qty > 0
                                      && trans.EffectiveDate <= mrpReceivePlan.ReceiveTime
                                  select trans;

            if (transitConsumed != null && transitConsumed.Count() > 0)
            {
                foreach (TransitInventory inventory in transitConsumed)
                {
                    if (mrpReceivePlan.Qty * unitQty > inventory.Qty)
                    {
                        log.Debug("Backflush transit inventory for mrpReceivePlan location[" + mrpReceivePlan.Location + "], item[" + itemCode + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "], effectiveDate[" + inventory.EffectiveDate + "], backflushQty[" + inventory.Qty / unitQty + "]");
                        mrpReceivePlan.Qty -= inventory.Qty / unitQty;
                        inventory.Qty = 0;
                    }
                    else
                    {
                        log.Debug("Backflush transit inventory for mrpReceivePlan location[" + mrpReceivePlan.Location + "], item[" + itemCode + "], qty[" + mrpReceivePlan.Qty + "], sourceType[" + mrpReceivePlan.SourceType + "], sourceId[" + (mrpReceivePlan.SourceId != null ? mrpReceivePlan.SourceId : string.Empty) + "], effectiveDate[" + inventory.EffectiveDate + "], backflushQty[" + mrpReceivePlan.Qty * unitQty + "]");
                        inventory.Qty -= mrpReceivePlan.Qty * unitQty;
                        mrpReceivePlan.Qty = 0;

                        break;
                    }
                }
            }
            #endregion
        }
        #endregion

        class SafeInventory
        {
            public string Location { get; set; }
            public string Item { get; set; }
            public decimal SafeQty { get; set; }
        }

        class TransitInventory
        {
            public string Location { get; set; }
            public string Item { get; set; }
            public decimal Qty { get; set; }
            public DateTime EffectiveDate { get; set; }
        }

        class FlowDetailSnapShot
        {
            public string Flow { get; set; }
            public string FlowType { get; set; }
            public string LocationFrom { get; set; }
            public string LocationTo { get; set; }
            public string Item { get; set; }
            public int MRPWeight { get; set; }
            public string Bom { get; set; }
            public string Routing { get; set; }
            public string BaseUom { get; set; }
            public string Uom { get; set; }
            public decimal LeadTime { get; set; }
            public decimal UnitCount { get; set; }
            public string ItemDescription { get; set; }
            public int Id { get; set; }
        }

        class SafeInventoryComparer : IEqualityComparer<SafeInventory>
        {
            public bool Equals(SafeInventory x, SafeInventory y)
            {
                return x.Location == y.Location && x.Item == y.Item;
            }

            public int GetHashCode(SafeInventory obj)
            {
                string hCode = obj.Location + "|" + obj.Item;
                return hCode.GetHashCode();
            }
        }

        class LastActionQty
        {
            public string Flow { get; set; }
            public string Item { get; set; }
            public decimal Qty { get; set; }
            public decimal UnitQty { get; set; }
        }

        //class MrpReceivePlan
        //{
        //    private string _location;
        //    public string Location
        //    {
        //        get
        //        {
        //            return _location;
        //        }
        //        set
        //        {
        //            _location = value;
        //        }
        //    }
        //    private string _item;
        //    public string Item
        //    {
        //        get
        //        {
        //            return _item;
        //        }
        //        set
        //        {
        //            _item = value;
        //        }
        //    }
        //    private Decimal _qty;
        //    public Decimal Qty
        //    {
        //        get
        //        {
        //            return _qty;
        //        }
        //        set
        //        {
        //            _qty = value;
        //        }
        //    }
        //    private Decimal _unitCount;
        //    public Decimal UnitCount
        //    {
        //        get
        //        {
        //            return _unitCount;
        //        }
        //        set
        //        {
        //            _unitCount = value;
        //        }
        //    }
        //    private DateTime _receiveTime;
        //    public DateTime ReceiveTime
        //    {
        //        get
        //        {
        //            return _receiveTime;
        //        }
        //        set
        //        {
        //            _receiveTime = value;
        //        }
        //    }
        //    private string _sourceDateType;
        //    public string SourceDateType
        //    {
        //        get
        //        {
        //            return _sourceDateType;
        //        }
        //        set
        //        {
        //            _sourceDateType = value;
        //        }
        //    }
        //    private string _sourceType;
        //    public string SourceType
        //    {
        //        get
        //        {
        //            return _sourceType;
        //        }
        //        set
        //        {
        //            _sourceType = value;
        //        }
        //    }
        //    private string _sourceId;
        //    public string SourceId
        //    {
        //        get
        //        {
        //            return _sourceId;
        //        }
        //        set
        //        {
        //            _sourceId = value;
        //        }
        //    }
        //    private Boolean _isExpire;
        //    public Boolean IsExpire
        //    {
        //        get
        //        {
        //            return _isExpire;
        //        }
        //        set
        //        {
        //            _isExpire = value;
        //        }
        //    }
        //    private string _uom;
        //    public string Uom
        //    {
        //        get
        //        {
        //            return _uom;
        //        }
        //        set
        //        {
        //            _uom = value;
        //        }
        //    }
        //    private DateTime? _expireStartTime;
        //    public DateTime? ExpireStartTime
        //    {
        //        get
        //        {
        //            return _expireStartTime;
        //        }
        //        set
        //        {
        //            _expireStartTime = value;
        //        }
        //    }
        //    public string ItemDescription { get; set; }
        //    public string ItemReference { get; set; }
        //}

        class MrpLocationLotDetail
        {
            private string _location;
            public string Location
            {
                get
                {
                    return _location;
                }
                set
                {
                    _location = value;
                }
            }
            private string _item;
            public string Item
            {
                get
                {
                    return _item;
                }
                set
                {
                    _item = value;
                }
            }
            private Decimal _safeQty;
            public Decimal SafeQty
            {
                get
                {
                    return _safeQty;
                }
                set
                {
                    _safeQty = value;
                }
            }
            private Decimal _qty;
            public Decimal Qty
            {
                get
                {
                    return _qty;
                }
                set
                {
                    _qty = value;
                }
            }

            public decimal ActiveQty
            {
                get
                {
                    return Qty - SafeQty;
                }
            }
        }

    }
}
