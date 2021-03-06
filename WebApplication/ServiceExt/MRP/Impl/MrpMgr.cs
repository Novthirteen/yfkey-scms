﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Hql;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using NHibernate;
using NHibernate.Expression;
using NHibernate.SqlCommand;
using NHibernate.Type;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

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
        public INumberControlMgr numberControlMgr;
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
                        INumberControlMgr numberControlMgr,
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
            this.numberControlMgr = numberControlMgr;
            this.shiftMgr = shiftMgr;
            this.flowMgr = flowMgr;
            this.orderMgr = orderMgr;
            this.workCalendarMgr = workCalendarMgr;
            this.flowDetailMgr = flowDetailMgr;
        }

        public void RunShipPlan(User user)
        {
            lock (RunPlanLock)
            {
                //RunShipPlan(DateTime.Now, user);
                SqlParameter[] sqlParameterArr = new SqlParameter[1];
                sqlParameterArr[0] = new SqlParameter("@RunUser", SqlDbType.VarChar, 50);
                sqlParameterArr[0].Value = user.Code;
                this.genericMgr.GetDatasetByStoredProcedure("RunShipPlanProxy", sqlParameterArr);
            }
        }

        private static object RunPlanLock = new object();
        public void RunProductionPlan(User user)
        {
            lock (RunPlanLock)
            {
                //string searchPlanSql = @"select r.Plant from MRP_ShipPlanDet as d  inner join Location as l on l.Code=d.LocFrom inner join Region as r on r.Code=l.Region group by r.Plant";
                //var plans = this.genericMgr.GetDatasetBySql(searchPlanSql).Tables[0];
                //var planList = new List<string>();
                //foreach (System.Data.DataRow row in plans.Rows)
                //{
                //    planList.Add(row[0].ToString());
                //}
                ////RunShipPlan(DateTime.Now, user);
                //foreach (var plan in planList)
                //{
                //    SqlParameter[] sqlParameterArr = new SqlParameter[2];
                //    sqlParameterArr[0] = new SqlParameter("@Plant", SqlDbType.VarChar, 50);
                //    sqlParameterArr[0].Value = plan;
                //    sqlParameterArr[1] = new SqlParameter("@RunUser", SqlDbType.VarChar, 50);
                //    sqlParameterArr[1].Value = user.Code;
                //    this.genericMgr.GetDatasetByStoredProcedure("RunProductionPlan", sqlParameterArr);  
                //}
                SqlParameter[] sqlParameterArr = new SqlParameter[1];
                sqlParameterArr[0] = new SqlParameter("@RunUser", SqlDbType.VarChar, 50);
                sqlParameterArr[0].Value = user.Code;
                this.genericMgr.GetDatasetByStoredProcedure("RunProductionPlanProxy", sqlParameterArr);

            }
        }

        public void RunShipPlan(DateTime effectiveDate, User user)
        {
            lock (RunPlanLock)
            {
                int batchNo = int.Parse(this.numberControlMgr.GetNextSequence("RunShipPlan"));
                DateTime dateTimeNow = DateTime.Now;
                IList<MrpShipPlan> mrpShipPlanList = new List<MrpShipPlan>();
                #region EffectiveDate格式化
                effectiveDate = effectiveDate.Date;
                #endregion

                InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始运行发运计划");

                try
                {
                    #region 删除有效期相同的ShipPlan、ReceivePlan、TransitInventory
                    string hql = @"from MrpShipPlan entity where entity.EffectiveDate = ?";
                    hqlMgr.Delete(hql, new object[] { effectiveDate }, new IType[] { NHibernateUtil.DateTime });

                    //hql = @"from MrpReceivePlan entity where entity.EffectiveDate = ?";
                    //hqlMgr.Delete(hql, new object[] { effectiveDate }, new IType[] { NHibernateUtil.DateTime });

                    //hql = @"from expecttransitinventory entity where entity.effectivedate = ?";
                    //hqlMgr.Delete(hql, new object[] { effectiveDate }, new IType[] { NHibernateUtil.DateTime });

                    this.hqlMgr.FlushSession();
                    this.hqlMgr.CleanSession();
                    #endregion

                    #region 获取实时库存和在途
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取数据");
                    #region 安全库存
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取安全库存");
                    hql = @"select fl.Code, fdl.Code, i.Code, fd.SafeStock from FlowDetail as fd 
                                        join fd.Flow as f 
                                        left join fd.LocationTo as fdl 
                                        left join f.LocationTo as fl
                                        join fd.Item as i
                                        where (fd.LocationTo is not null 
                                        or f.LocationTo is not null) and f.IsActive = 1";
                    IList<object[]> safeQtyList = hqlMgr.FindAll<object[]>(hql);
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取安全库存完成");
                    #endregion

                    #region 实时库存
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取实时库存");
                    hql = @"select l.Code, i.Code, sum(lld.Qty) from LocationLotDetail as lld
                    join lld.Location as l
                    join lld.Item as i
                    where not lld.Qty = 0 and l.IsMRP = 1 and l.Code not in (?, ?)
                    group by l.Code, i.Code";
                    IList<object[]> invList = hqlMgr.FindAll<object[]>(hql, new object[] { BusinessConstants.SYSTEM_LOCATION_INSPECT, BusinessConstants.SYSTEM_LOCATION_REJECT });
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取实时库存完成");
                    #endregion

                    #region 发运在途
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取发运在途");
                    DetachedCriteria criteria = DetachedCriteria.For<InProcessLocationDetail>();

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
                      .Add(Projections.Sum("ReceivedQty2"))
                      .Add(Projections.GroupProperty("ip.ArriveTime"))
                      .Add(Projections.GroupProperty("oh.LocationTo"))
                      );
                    IList<object[]> ipDetList = this.criteriaMgr.FindAll<object[]>(criteria);
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取发运在途完成");
                    #endregion

                    #region 检验在途
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取检验在途");
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
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取检验在途完成");
                    #endregion
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取数据完成");
                    #endregion

                    #region 处理数据
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始处理数据");
                    #region 获取所有库位的安全库存
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始处理所有库位的安全库存");
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
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "处理所有库位的安全库存完成");
                    #endregion

                    #region 获取实时库存
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始处理实时库存");
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
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "处理实时库存完成");
                    #endregion

                    #region 没有库存的安全库存全部转换为InventoryBalance
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始处理没有库存的安全库存全部转换为InventoryBalance");
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
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "处理没有库存的安全库存全部转换为InventoryBalance完成");
                    #endregion

                    #region 发运在途 ASN
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始处理发运在途");
                    IList<TransitInventory> transitInventoryList = new List<TransitInventory>();

                    if (ipDetList != null && ipDetList.Count > 0)
                    {
                        foreach (object[] ipDet in ipDetList)
                        {
                            //记录在途库存
                            TransitInventory transitInventory = new TransitInventory();
                            transitInventory.Location = ipDet[0] != null ? ((Location)ipDet[0]).Code : (ipDet[5] != null ? ((Location)ipDet[5]).Code : null);
                            transitInventory.Item = (string)ipDet[1];
                            transitInventory.Qty = (decimal)ipDet[2] - (decimal)ipDet[3];
                            transitInventory.EffectiveDate = (DateTime)ipDet[4];

                            transitInventoryList.Add(transitInventory);
                        }
                    }
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "处理发运在途完成");
                    #endregion

                    #region 检验在途
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始处理检验在途");
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
                        }
                    }
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "处理检验在途完成");
                    #endregion
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "处理数据完成");
                    #endregion

                    #region 根据客户需求生成发货计划
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始根据客户需求生成发货计划");
                    #region 获取所有销售路线明细
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取所有销售路线明细");
                    criteria = DetachedCriteria.For<Flow>();

                    criteria.SetProjection(Projections.ProjectionList()
                        .Add(Projections.GroupProperty("Code"))
                        .Add(Projections.GroupProperty("MRPOption")));

                    criteria.Add(Expression.Eq("IsActive", true));
                    criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION));

                    IList<object[]> flowList = this.criteriaMgr.FindAll<object[]>(criteria);
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取所有销售路线明细完成");
                    #endregion

                    #region 获取客户需求
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始获取客户需求");
                    criteria = DetachedCriteria.For<CustomerScheduleDetail>();
                    criteria.CreateAlias("CustomerSchedule", "cs");

                    criteria.Add(Expression.Eq("cs.Type", BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY));
                    criteria.Add(Expression.Eq("cs.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
                    criteria.Add(Expression.Ge("StartTime", effectiveDate));
                    criteria.Add(Expression.Gt("Qty", decimal.Zero));

                    IList<CustomerScheduleDetail> customerScheduleDetailList = this.criteriaMgr.FindAll<CustomerScheduleDetail>(criteria);

                    #region 取得有效的CustomerScheduleDetail
                    IList<CustomerScheduleDetail> effectiveCustomerScheduleDetailList = customerScheduleDetailMgr.GetEffectiveCustomerScheduleDetail(customerScheduleDetailList, effectiveDate);
                    #endregion
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "获取客户需求完成");
                    #endregion

                    #region 循环销售路线生成发货计划
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始循环销售路线生成发货计划");
                    if (flowList != null && flowList.Count > 0)
                    {
                        foreach (object[] flow in flowList)
                        {
                            string flowCode = (string)flow[0];
                            string mrpOption = (string)flow[1];

                            var targetCustomerScheduleDetailList = from det in effectiveCustomerScheduleDetailList
                                                                   where det.CustomerSchedule.Flow == flowCode
                                                                   select det;

                            IListHelper.AddRange(mrpShipPlanList, TransferCustomerPlan2ShipPlan(targetCustomerScheduleDetailList != null ? targetCustomerScheduleDetailList.ToList() : null, effectiveDate, dateTimeNow, user));
                        }
                    }
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "循环销售路线生成发货计划完成");
                    #endregion
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "根据客户需求生成发货计划完成");
                    #endregion

                    #region 查询并缓存所有FlowDetail
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始查询并缓存所有移库路线明细");
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
                    criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER));
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
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "查询并缓存所有移库路线明细完成");
                    #endregion

                    #region 循环生成入库计划/发货计划
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "开始循环生成入库计划/发货计划");
                    if (mrpShipPlanList != null && mrpShipPlanList.Count > 0)
                    {
                        var sortedMrpShipPlanList = from plan in mrpShipPlanList
                                                    orderby plan.StartTime ascending
                                                    select plan;

                        foreach (MrpShipPlan mrpShipPlan in sortedMrpShipPlanList)
                        {
                            NestCalculateMrpShipPlanAndReceivePlan(batchNo, effectiveDate, mrpShipPlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                        }
                    }
                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "循环生成入库计划/发货计划完成");
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
                                //在途不满足库存短缺
                                Item item = this.itemMgr.CheckAndLoadItem(lackInventory.Item);
                                lackInventory.ItemDescription = item.Description;

                                InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, lackInventory);

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
                                    MrpReceivePlan mrpReceivePlan = new MrpReceivePlan();
                                    mrpReceivePlan.Item = lackInventory.Item;
                                    mrpReceivePlan.ItemDescription = item.Description;
                                    mrpReceivePlan.Uom = item.Uom.Code;
                                    mrpReceivePlan.Location = lackInventory.Location;
                                    mrpReceivePlan.Qty = -lackInventory.ActiveQty;
                                    mrpReceivePlan.UnitCount = item.UnitCount;
                                    mrpReceivePlan.ReceiveTime = effectiveDate;
                                    mrpReceivePlan.SourceType = BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_SAFE_STOCK;
                                    mrpReceivePlan.SourceDateType = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY;
                                    mrpReceivePlan.SourceId = lackInventory.Location + "__" + lackInventory.Item;
                                    mrpReceivePlan.SourceUnitQty = 1;
                                    mrpReceivePlan.EffectiveDate = effectiveDate;
                                    mrpReceivePlan.CreateDate = dateTimeNow;
                                    mrpReceivePlan.CreateUser = user.Code;

                                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, mrpReceivePlan, "生成毛需求");
                                    CalculateNextShipPlan(batchNo, effectiveDate, mrpReceivePlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion

                    InsertInfoRunShipPlanLog(batchNo, effectiveDate, user.Name, "运行发运计划完成");
                }
                catch (Exception ex)
                {
                    InsertErrRunShipPlanLog(batchNo, effectiveDate, user.Name, "运行发运计划异常，" + ex.Message);
                }
            }
        }

        public void RunMrp(User user)
        {
            lock (RunPlanLock)
            {
                SqlParameter[] sqlParameterArr = new SqlParameter[1];
                sqlParameterArr[0] = new SqlParameter("@RunUser", SqlDbType.VarChar, 50);
                sqlParameterArr[0].Value = user.Code;
                this.genericMgr.GetDatasetByStoredProcedure("RunPurchasePlanProxy", sqlParameterArr);

            }
        }

        public void RunMrp2(User user)
        {
            lock (RunPlanLock)
            {
                SqlParameter[] sqlParameterArr = new SqlParameter[1];
                sqlParameterArr[0] = new SqlParameter("@RunUser", SqlDbType.VarChar, 50);
                sqlParameterArr[0].Value = user.Code;
                this.genericMgr.GetDatasetByStoredProcedure("RunPurchasePlanProxy2", sqlParameterArr);

            }
        }

        public void RunMrp(DateTime effectiveDate, User user)
        {
        }

        #region  Import  CustomerPlan
        private static object readCustomerPlanFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public List<CustomerScheduleDetail> ReadCustomerPlanFromXls(Stream inputStream, string dateType, User user)
        {
            lock (readCustomerPlanFromXlsLock)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = startDate.AddDays(14);
                if (dateType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
                {
                    endDate = startDate.AddDays(7 * 30);
                }

                if (startDate > endDate)
                {
                    throw new BusinessErrorException("开始日期必须小于结束日期");
                }

                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }

                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

                Sheet sheet = workbook.GetSheetAt(0);

                IEnumerator rows = sheet.GetRowEnumerator();

                Row dateRow = sheet.GetRow(1);

                ImportHelper.JumpRows(rows, 2);

                var customerPlanList = new List<CustomerScheduleDetail>();

                //int colRefScheduleNo = 0;//路线
                int colFlow = 0;//路线
                int colItemCode = 1;//物料代码
                int colRefItemCode = 3;//参考物料号

                List<string> errorMessages = new List<string>();
                var flowDets = this.genericMgr.GetDatasetBySql(@"select d.Flow,d.Item,d.RefItemCode,i.Desc1,d.UC,d.Uom,m.LocFrom,isnull(m.MrpLeadTime,0)as MrpLeadTime,m.ShipFlow from FlowDet as d 
                                                                inner join Item as i on i.Code=d.Item 
                                                                inner join FlowMstr as m on d.Flow=m.Code 
                                                                where m.type='Distribution' ").Tables[0];          //and d.RefItemCode is not null and d.RefItemCode<>''
                var allActiveFlowDetList = new List<object[]>();
                foreach (System.Data.DataRow row in flowDets.Rows)
                {
                    allActiveFlowDetList.Add(new object[] { row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), Convert.ToDecimal(row[4].ToString()), row[5].ToString(), row[6].ToString(), Convert.ToDecimal(row[7].ToString()), row[8].ToString() });
                }
                //
                var flowCodelist = allActiveFlowDetList.Select(d => d[0]).Distinct();

                var refScheduleNos = this.genericMgr.GetDatasetBySql(@"select RefScheduleNo from dbo.CustScheduleMstr group by RefScheduleNo").Tables[0];
                var refScheduleNoList = new List<string>();
                foreach (System.Data.DataRow row in flowDets.Rows)
                {
                    refScheduleNoList.Add(row[0].ToString());
                }

                while (rows.MoveNext())
                {
                    string refScheduleNo = string.Empty;
                    string itemReference = null;
                    string flowCode = null;
                    string itemCode = null;

                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 0, 3))
                    {
                        break;//边界
                    }
                    string rowIndex = (row.RowNum + 1).ToString();
                    #region 客户版本号
                    //refScheduleNo = ImportHelper.GetCellStringValue(row.GetCell(colRefScheduleNo));
                    //if (string.IsNullOrEmpty(refScheduleNo))
                    //{
                    //    errorMessages.Add(string.Format("客户版本号,第{0}行", rowIndex));
                    //    continue;
                    //}
                    //else
                    //{
                    //    if (refScheduleNoList.Contains(refScheduleNo))
                    //    {
                    //        errorMessages.Add(string.Format("客户版本号{0}已经存在,第{1}行", flowCode, rowIndex));
                    //        continue;
                    //    }
                    //}

                    #endregion

                    #region 读取路线代码
                    flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlow));
                    if (string.IsNullOrEmpty(flowCode))
                    {
                        errorMessages.Add(string.Format("路线不能为空,第{0}行", rowIndex));
                        continue;
                    }
                    else
                    {
                        if (!flowCodelist.Contains(flowCode))
                        {
                            errorMessages.Add(string.Format("路线{0}不是销售路线,第{1}行", flowCode, rowIndex));
                            continue;
                        }
                    }

                    #endregion

                    #region 读取物料代码
                    try
                    {
                        itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                        if (itemCode == null)
                        {
                            errorMessages.Add(string.Format("物料不能为空,第{0}行", rowIndex));
                            continue;
                        }
                        var checkItem = allActiveFlowDetList.Where(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode);
                        if (checkItem == null || checkItem.Count() == 0)
                        {
                            errorMessages.Add(string.Format("物料号{0}不存在销售路线{2}中请维护,第{1}行.", itemCode, rowIndex, flowCode));
                            continue;
                        }
                    }
                    catch
                    {
                        errorMessages.Add(string.Format("读取物料时出错,第{0}行.", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 客户零件号
                    try
                    {
                        itemReference = ImportHelper.GetCellStringValue(row.GetCell(colRefItemCode));

                        //if (string.IsNullOrEmpty(itemReference))
                        //{
                        //    errorMessages.Add(string.Format("客户零件号不能为空,第{0}行." , rowIndex));
                        //    continue;
                        //}
                        //var checkItem = allActiveFlowDetList.Where(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode && d[2].ToString() == itemReference);
                        //if (checkItem == null || checkItem.Count() == 0)
                        //{
                        //    errorMessages.Add(string.Format("物料号{0}客户零件号{3}销售路线{2},不匹配请维护,第{1}行.", itemCode, rowIndex, flowCode, itemReference));
                        //    continue;
                        //}
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(string.Format("读取单位出错,第{0}行." + ex.Message, rowIndex));
                        continue;
                    }

                    #endregion

                    #region 读取数量
                    try
                    {
                        for (int i = 5; ; i++)
                        {
                            if (i > 56)
                            {
                                break;
                            }
                            Cell dateCell = dateRow.GetCell(i);
                            string dateIndex = null;

                            #region 读取计划日期
                            if (dateCell != null)
                            {
                                DateTime currentDateTime = DateTime.Today;
                                if (dateCell.CellType == CellType.STRING)
                                {
                                    dateIndex = dateCell.StringCellValue;
                                }
                                else
                                {
                                    if (dateType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
                                    {
                                        if (dateCell.CellType == CellType.NUMERIC)
                                        {
                                            dateIndex = dateCell.DateCellValue.ToString("yyyy-MM-dd");
                                            currentDateTime = Convert.ToDateTime(dateIndex);
                                        }
                                        else
                                        {
                                            throw new BusinessErrorException("天的时间索引必须为文本或日期格式");
                                        }
                                    }
                                    else
                                    {
                                        dateIndex = dateCell.DateCellValue.ToString("yyyy-MM-dd");
                                        //周一
                                        currentDateTime = DateTimeHelper.GetWeekStart(Convert.ToDateTime(dateIndex));
                                    }
                                }

                                if (currentDateTime.CompareTo(startDate) < 0)
                                {
                                    continue;
                                }
                                if (currentDateTime.CompareTo(endDate) > 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                            #endregion

                            decimal qty = 0;
                            if (row.GetCell(i) != null)
                            {
                                if (row.GetCell(i).CellType == CellType.NUMERIC)
                                {
                                    qty = Convert.ToDecimal(row.GetCell(i).NumericCellValue);
                                }
                                else
                                {
                                    string qtyValue = ImportHelper.GetCellStringValue(row.GetCell(i));
                                    if (qtyValue != null)
                                    {
                                        if (!decimal.TryParse(qtyValue, out qty))
                                        {
                                            errorMessages.Add(string.Format("数量格式不正确,第{0}行", rowIndex));
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                continue;
                            }

                            if (qty < 0)
                            {
                                errorMessages.Add(string.Format("数量不能小于0,第{0}行", rowIndex));
                                continue;
                            }
                            else if (qty == 0)
                            {
                                continue;
                            }
                            else
                            {
                                //d.Flow,d.Item,d.RefItemCode,i.Desc1,d.UC,d.Uom,m.LocFrom
                                CustomerScheduleDetail det = new CustomerScheduleDetail();
                                //det.ReferenceScheduleNo = refScheduleNo;
                                det.Item = itemCode;
                                det.ItemDescription = (string)(allActiveFlowDetList.FirstOrDefault(d => d[1].ToString() == itemCode)[3]);
                                det.ItemReference = (string)(allActiveFlowDetList.FirstOrDefault(d => d[1].ToString() == itemCode)[2]);
                                det.Type = dateType;
                                det.DateFrom = Convert.ToDateTime(dateIndex);
                                det.DateTo = dateType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK ? det.DateFrom.AddDays(7) : det.DateFrom;
                                det.Uom = (string)(allActiveFlowDetList.FirstOrDefault(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode)[5]);
                                det.UnitCount = (decimal)(allActiveFlowDetList.FirstOrDefault(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode)[4]);
                                det.Qty = qty;
                                det.Location = (string)(allActiveFlowDetList.FirstOrDefault(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode)[6]);
                                //det.StartTime = det.DateFrom;
                                det.StartTime = det.DateFrom.AddDays(Convert.ToInt32(allActiveFlowDetList.FirstOrDefault(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode)[7])).Date;
                                //det.Version = mstr.Version;
                                det.Flow = flowCode;
                                //det.ReferenceScheduleNo = mstr.ReferenceScheduleNo;
                                det.ShipFlow = (string)(allActiveFlowDetList.FirstOrDefault(d => d[0].ToString() == flowCode && d[1].ToString() == itemCode)[8]);
                                det.ShipQty = 0;
                                det.FordPlanId = 0;
                                //this.genericMgr.Create(det);
                                customerPlanList.Add(det);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(ex.Message);
                    }
                    #endregion
                }

                if (errorMessages.Count > 0)
                {
                    string errorMes = string.Empty;
                    foreach (var error in errorMessages)
                    {
                        errorMes += error + "-";
                    }
                    throw new BusinessErrorException(errorMes);
                }

                Dictionary<string, int> planVersions = new Dictionary<string, int>();
                foreach (var allFlowCode in customerPlanList.Select(p => p.Flow).Distinct())
                {
                    planVersions.Add(allFlowCode, numberControlMgr.GenerateNumberNextSequence(string.Format("CustomerPlan_{0}_{1}", allFlowCode.ToString(), dateType.ToString())));
                }

                var custmerPlnaGroup = customerPlanList.GroupBy(g => new { g.Flow }).ToDictionary(d => d.Key, d => d.ToList());

                foreach (var g in custmerPlnaGroup)
                {
                    DateTime datetimeNow = System.DateTime.Now;
                    CustomerSchedule mstr = new CustomerSchedule
                     {
                         ReferenceScheduleNo = g.Key.Flow + "-" + dateType + "-" + planVersions[g.Key.Flow].ToString().PadLeft(3, '0'),
                         Flow = g.Key.Flow,
                         Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT,
                         Type = g.Value.First().Type,
                         CreateDate = datetimeNow,
                         CreateUser = user.Code,
                         LastModifyDate = datetimeNow,
                         LastModifyUser = user.Code,
                         Version = planVersions[g.Key.Flow],
                         ShipFlow = g.Value.First().ShipFlow,
                         ReleaseDate = datetimeNow,
                         ReleaseUser = user.Code,
                     };
                    this.genericMgr.Create(mstr);
                    foreach (var r in g.Value)
                    {
                        r.Version = mstr.Version;
                        r.CustomerSchedule = mstr;
                        r.ReferenceScheduleNo = mstr.ReferenceScheduleNo;
                        this.genericMgr.Create(r);
                    }
                }

                return customerPlanList;
            }
        }

        private decimal ConvertUomQty(string itemCode, string sourceUomCode, decimal sourceQty, string targetUomCode)
        {
            if (itemCode == null || sourceUomCode == null || targetUomCode == null)
            {
                //throw new BusinessErrorException("UomConversion Error:itemCode Or sourceUomCode Or targetUomCode is null");
                log.Error("UomConversion Error:itemCode Or sourceUomCode Or targetUomCode is null");
                return sourceQty;
            }

            if (sourceUomCode == targetUomCode || sourceQty == 0)
            {
                return sourceQty;
            }

            DetachedCriteria criteria = DetachedCriteria.For(typeof(UomConversion));
            criteria.Add(Expression.Or(Expression.IsNull("Item"), Expression.Eq("Item.Code", itemCode)));

            IList<UomConversion> unGroupUomConversionList = criteriaMgr.FindAll<UomConversion>(criteria);
            if (unGroupUomConversionList != null)
            {
                List<UomConversion> uomConversionList = unGroupUomConversionList.Where(u => u.Item != null).ToList();
                foreach (UomConversion y in unGroupUomConversionList)
                {
                    if (uomConversionList.Where(x => (StringHelper.Eq(x.AlterUom.Code, y.AlterUom.Code) && StringHelper.Eq(x.BaseUom.Code, y.BaseUom.Code))
                        || (StringHelper.Eq(x.AlterUom.Code, y.BaseUom.Code) && StringHelper.Eq(x.BaseUom.Code, y.AlterUom.Code))).Count() == 0)
                    {
                        uomConversionList.Add(y);
                    }
                }
                foreach (UomConversion u in uomConversionList)
                {
                    //顺
                    if (StringHelper.Eq(u.BaseUom.Code, sourceUomCode))
                    {
                        u.Qty = sourceQty * u.AlterQty / u.BaseQty;
                        u.IsAsc = true;
                        if (StringHelper.Eq(u.AlterUom.Code, targetUomCode))
                        {
                            return u.Qty.Value;
                        }
                    }
                    //反
                    else if (StringHelper.Eq(u.AlterUom.Code, sourceUomCode))
                    {
                        u.Qty = sourceQty * u.BaseQty / u.AlterQty;
                        u.IsAsc = false;
                        if (StringHelper.Eq(u.BaseUom.Code, targetUomCode))
                        {
                            return u.Qty.Value;
                        }
                    }
                }

                for (int i = 1; i < uomConversionList.Count; i++)
                {
                    foreach (UomConversion uomConversion1 in uomConversionList)
                    {
                        if (uomConversion1.Qty.HasValue)
                        {
                            foreach (UomConversion uomConversion2 in uomConversionList)
                            {
                                //顺
                                if (uomConversion1.IsAsc)
                                {
                                    //顺
                                    if (StringHelper.Eq(uomConversion2.BaseUom.Code, uomConversion1.AlterUom.Code) && !uomConversion2.Qty.HasValue)
                                    {
                                        uomConversion2.Qty = uomConversion1.Qty.Value * uomConversion2.AlterQty / uomConversion2.BaseQty;
                                        uomConversion2.IsAsc = true;
                                        if (StringHelper.Eq(uomConversion2.AlterUom.Code, targetUomCode))
                                        {
                                            return uomConversion2.Qty.Value;
                                        }
                                    }
                                    //反
                                    else if (StringHelper.Eq(uomConversion2.AlterUom.Code, uomConversion1.AlterUom.Code) && !uomConversion2.Qty.HasValue)
                                    {
                                        uomConversion2.Qty = uomConversion1.Qty.Value * uomConversion2.BaseQty / uomConversion2.AlterQty;
                                        uomConversion2.IsAsc = false;
                                        if (StringHelper.Eq(uomConversion2.BaseUom.Code, targetUomCode))
                                        {
                                            return uomConversion2.Qty.Value;
                                        }
                                    }
                                }
                                //反
                                else
                                {
                                    //顺
                                    if (StringHelper.Eq(uomConversion2.BaseUom.Code, uomConversion1.BaseUom.Code) && !uomConversion2.Qty.HasValue)
                                    {
                                        uomConversion2.Qty = uomConversion1.Qty.Value * uomConversion2.AlterQty / uomConversion2.BaseQty;
                                        uomConversion2.IsAsc = true;
                                        if (StringHelper.Eq(uomConversion2.AlterUom.Code, targetUomCode))
                                        {
                                            return uomConversion2.Qty.Value;
                                        }
                                    }
                                    //反
                                    else if (StringHelper.Eq(uomConversion2.AlterUom.Code, uomConversion1.BaseUom.Code) && !uomConversion2.Qty.HasValue)
                                    {
                                        uomConversion2.Qty = uomConversion1.Qty.Value * uomConversion2.BaseQty / uomConversion2.AlterQty;
                                        uomConversion2.IsAsc = false;
                                        if (StringHelper.Eq(uomConversion2.BaseUom.Code, targetUomCode))
                                        {
                                            return uomConversion2.Qty.Value;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            log.Error("UomConversion.Error.NotFound,itemCode:" + itemCode + ",sourceUomCode:" + sourceUomCode + ",targetUomCode:" + targetUomCode);
            return sourceQty;
        }

        #endregion

        #region    修改发货计划
        [Transaction(TransactionMode.Requires)]
        public void UpdateShipPlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, string type)
        {
            var dateTimeNow = System.DateTime.Now;
            Random r = new Random();
            for (int i = 0; i < idList.Count; i++)
            {
                int id = int.Parse(idList[i]);
                if (id == 0)
                {
                    IList<ShipPlanMstr> pMaster = this.genericMgr.FindAllWithCustomQuery<ShipPlanMstr>(" select m from ShipPlanMstr as m where m.ReleaseNo=? ", new object[] { releaseNoList[i] });
                    IList<ShipPlanDet> searchPlandets = this.genericMgr.FindAllWithCustomQuery<ShipPlanDet>(" select d from ShipPlanDet as d where d.Flow=? and d.Item=? and d.ShipPlanId=? ", new object[] { flowList[i], itemList[i], pMaster.First().Id });
                    if (searchPlandets != null && searchPlandets.Count > 0)
                    {
                        var first = searchPlandets.First();
                        ShipPlanDet newShipPlanDet = new ShipPlanDet();
                        newShipPlanDet.ShipPlanId = first.ShipPlanId;
                        newShipPlanDet.Flow = first.Flow;
                        newShipPlanDet.Item = first.Item;
                        newShipPlanDet.ItemDesc = first.ItemDesc;
                        newShipPlanDet.RefItemCode = first.RefItemCode;
                        newShipPlanDet.ShipQty = Convert.ToDecimal(qtyList[i]);
                        newShipPlanDet.Uom = first.Uom;
                        newShipPlanDet.BaseUom = first.BaseUom;
                        newShipPlanDet.UnitCount = first.UnitCount;
                        newShipPlanDet.UnitQty = first.UnitQty;
                        newShipPlanDet.LocFrom = first.LocFrom;
                        newShipPlanDet.LocTo = first.LocTo;
                        newShipPlanDet.OrgShipQty = 0;
                        newShipPlanDet.WindowTime = Convert.ToDateTime(dateFrom[i]);
                        newShipPlanDet.StartTime = Convert.ToDateTime(dateFrom[i]);
                        newShipPlanDet.CreateDate = dateTimeNow;
                        newShipPlanDet.CreateUser = user.Code;
                        newShipPlanDet.LastModifyDate = dateTimeNow;
                        newShipPlanDet.LastModifyUser = user.Code;
                        newShipPlanDet.Version = 1;
                        newShipPlanDet.Id = 0;
                        newShipPlanDet.Type = type;
                        newShipPlanDet.UUID = System.DateTime.Now.ToString() + r.Next(100, 500) + r.Next(511, 1000);
                        this.genericMgr.Create(newShipPlanDet);
                    }
                }
                else
                {
                    this.genericMgr.ExecuteSql(string.Format(" update MRP_ShipPlanDet set ShipQty={0},Version=Version+1,LastModifyDate='{2}',LastModifyUser='{3}' where id={1} ", qtyList[i], id, dateTimeNow, user.Code));
                }
            }
        }
        #endregion

        #region    修改生产计划
        [Transaction(TransactionMode.Requires)]
        public void UpdateProductionPlanQty(IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, string type)
        {
            var dateTimeNow = System.DateTime.Now;
            for (int i = 0; i < idList.Count; i++)
            {
                int id = int.Parse(idList[i]);
                if (id == 0)
                {
                    IList<ProductionPlanMstr> pMaster = this.genericMgr.FindAllWithCustomQuery<ProductionPlanMstr>(" select m from ProductionPlanMstr as m where m.ReleaseNo=? ", new object[] { releaseNoList[i] });
                    IList<ProductionPlanDet> searchPlandets = this.genericMgr.FindAllWithCustomQuery<ProductionPlanDet>(" select d from ProductionPlanDet as d where d.Item=? and d.ShipPlanId=? ", new object[] { itemList[i], pMaster.First().Id });
                    if (searchPlandets != null && searchPlandets.Count > 0)
                    {
                        Random r = new Random();
                        //Id, ProductionPlanId, Type, UUID, Item, ItemDesc, RefItemCode, ReqQty, OrgQty, Qty, 
                        //OrderQty, Uom, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyUser, LastModifyDate, Version
                        var first = searchPlandets.First();
                        ProductionPlanDet newShipPlanDet = new ProductionPlanDet();
                        newShipPlanDet.ProductionPlanId = first.ProductionPlanId;
                        newShipPlanDet.UUID = System.DateTime.Now.ToString() + r.Next(100, 500) + r.Next(511, 1000);
                        newShipPlanDet.Type = type;
                        newShipPlanDet.Item = first.Item;
                        newShipPlanDet.ItemDesc = first.ItemDesc;
                        newShipPlanDet.RefItemCode = first.RefItemCode;

                        newShipPlanDet.ReqQty = 0;
                        newShipPlanDet.OrgQty = 0;
                        newShipPlanDet.Qty = Convert.ToDecimal(qtyList[i]);
                        newShipPlanDet.OrderQty = 0;
                        newShipPlanDet.Uom = first.Uom;
                        newShipPlanDet.UnitCount = first.UnitCount;
                        newShipPlanDet.MinLotSize = first.MinLotSize;
                        newShipPlanDet.WindowTime = Convert.ToDateTime(dateFrom[i]);
                        newShipPlanDet.StartTime = Convert.ToDateTime(dateFrom[i]);
                        newShipPlanDet.CreateDate = dateTimeNow;
                        newShipPlanDet.CreateUser = user.Code;
                        newShipPlanDet.LastModifyDate = dateTimeNow;
                        newShipPlanDet.LastModifyUser = user.Code;
                        newShipPlanDet.Version = 1;
                        newShipPlanDet.Id = 0;
                        this.genericMgr.Create(newShipPlanDet);
                    }
                }
                else
                {
                    this.genericMgr.ExecuteSql(string.Format(" update MRP_ProductionPlanDet set Qty={0},Version=Version+1,LastModifyDate='{2}',LastModifyUser='{3}' where id={1} ", qtyList[i], id, dateTimeNow, user.Code));
                }
            }
        }
        #endregion

        #region    修改采购计划
        [Transaction(TransactionMode.Requires)]
        public void UpdatePurchasePlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, string type)
        {
            var dateTimeNow = System.DateTime.Now;
            Random r = new Random();
            for (int i = 0; i < idList.Count; i++)
            {
                int id = int.Parse(idList[i]);
                if (id == 0)
                {
                    IList<PurchasePlanMstr> pMaster = this.genericMgr.FindAllWithCustomQuery<PurchasePlanMstr>(" select m from PurchasePlanMstr as m where m.ReleaseNo=? ", new object[] { releaseNoList[i] });
                    IList<PurchasePlanDet> searchPlandets = this.genericMgr.FindAllWithCustomQuery<PurchasePlanDet>(" select d from PurchasePlanDet as d where d.Flow=? and d.Item=? and d.PurchasePlanId=? ", new object[] { flowList[i], itemList[i], pMaster.First().Id });
                    if (searchPlandets != null && searchPlandets.Count > 0)
                    {
                        //Id, PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, 
                        //PurchaseQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                        var first = searchPlandets.First();
                        PurchasePlanDet newPlan = new PurchasePlanDet();
                        newPlan.PurchasePlanId = first.PurchasePlanId;
                        newPlan.Flow = first.Flow;
                        newPlan.Item = first.Item;
                        newPlan.ItemDesc = first.ItemDesc;
                        newPlan.RefItemCode = first.RefItemCode;
                        newPlan.PurchaseQty = Convert.ToDecimal(qtyList[i]);
                        newPlan.Uom = first.Uom;
                        newPlan.BaseUom = first.BaseUom;
                        newPlan.UnitCount = first.UnitCount;
                        newPlan.UnitQty = first.UnitQty;
                        newPlan.OrgPurchaseQty = 0;
                        newPlan.WindowTime = Convert.ToDateTime(dateFrom[i]);
                        newPlan.StartTime = Convert.ToDateTime(dateFrom[i]);
                        newPlan.CreateDate = dateTimeNow;
                        newPlan.CreateUser = user.Code;
                        newPlan.LastModifyDate = dateTimeNow;
                        newPlan.LastModifyUser = user.Code;
                        newPlan.Version = 1;
                        newPlan.Id = 0;
                        newPlan.Type = type;
                        newPlan.UUID = System.DateTime.Now.ToString() + r.Next(100, 500) + r.Next(511, 1000);
                        this.genericMgr.Create(newPlan);
                    }
                }
                else
                {
                    this.genericMgr.ExecuteSql(string.Format(" update MRP_PurchasePlanDet set PurchaseQty={0},Version=Version+1,LastModifyDate='{2}',LastModifyUser='{3}' where id={1} ", qtyList[i], id, dateTimeNow, user.Code));
                }
            }
        }
        #endregion

        #region    修改周采购计划
        [Transaction(TransactionMode.Requires)]
        public void UpdateWeeklyPurchasePlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user)
        {
            var dateTimeNow = System.DateTime.Now;
            for (int i = 0; i < idList.Count; i++)
            {
                int id = int.Parse(idList[i]);
                if (id == 0)
                {
                    IList<WeeklyPurchasePlanMstr> pMaster = this.genericMgr.FindAllWithCustomQuery<WeeklyPurchasePlanMstr>(" select m from WeeklyPurchasePlanMstr as m where m.ReleaseNo=? ", new object[] { releaseNoList[i] });
                    IList<WeeklyPurchasePlanDet> searchPlandets = this.genericMgr.FindAllWithCustomQuery<WeeklyPurchasePlanDet>(" select d from WeeklyPurchasePlanDet as d where d.Flow=? and d.Item=? and d.PurchasePlanId=? ", new object[] { flowList[i], itemList[i], pMaster.First().Id });
                    if (searchPlandets != null && searchPlandets.Count > 0)
                    {
                        //Id, PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, 
                        //PurchaseQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                        var first = searchPlandets.First();
                        WeeklyPurchasePlanDet newPlan = new WeeklyPurchasePlanDet();
                        newPlan.PurchasePlanId = first.PurchasePlanId;
                        newPlan.Flow = first.Flow;
                        newPlan.Item = first.Item;
                        newPlan.ItemDesc = first.ItemDesc;
                        newPlan.RefItemCode = first.RefItemCode;
                        newPlan.PurchaseQty = Convert.ToDecimal(qtyList[i]);
                        newPlan.Uom = first.Uom;
                        newPlan.BaseUom = first.BaseUom;
                        newPlan.UnitCount = first.UnitCount;
                        newPlan.UnitQty = first.UnitQty;
                        newPlan.OrgPurchaseQty = 0;
                        newPlan.WindowTime = Convert.ToDateTime(dateFrom[i]);
                        newPlan.StartTime = Convert.ToDateTime(dateFrom[i]);
                        newPlan.CreateDate = dateTimeNow;
                        newPlan.CreateUser = user.Code;
                        newPlan.LastModifyDate = dateTimeNow;
                        newPlan.LastModifyUser = user.Code;
                        newPlan.Version = 1;
                        newPlan.Id = 0;
                        this.genericMgr.Create(newPlan);
                    }
                }
                else
                {
                    this.genericMgr.ExecuteSql(string.Format(" update MRP_WeeklyPurchasePlanDet set PurchaseQty={0},Version=Version+1 where id={1} ", qtyList[i], id));
                }
            }
        }
        #endregion

        #region    修改采购计划2
        [Transaction(TransactionMode.Requires)]
        public void UpdatePurchasePlanQty2(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, string type)
        {
            var dateTimeNow = System.DateTime.Now;
            Random r = new Random();
            for (int i = 0; i < idList.Count; i++)
            {
                int id = int.Parse(idList[i]);
                if (id == 0)
                {
                    IList<PurchasePlanMstr2> pMaster = this.genericMgr.FindAllWithCustomQuery<PurchasePlanMstr2>(" select m from PurchasePlanMstr2 as m where m.ReleaseNo=? ", new object[] { releaseNoList[i] });
                    IList<PurchasePlanDet2> searchPlandets = this.genericMgr.FindAllWithCustomQuery<PurchasePlanDet2>(" select d from PurchasePlanDet2 as d where d.Flow=? and d.Item=? and d.PurchasePlanId=? ", new object[] { flowList[i], itemList[i], pMaster.First().Id });
                    if (searchPlandets != null && searchPlandets.Count > 0)
                    {
                        //Id, PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, 
                        //PurchaseQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                        var first = searchPlandets.First();
                        PurchasePlanDet2 newPlan = new PurchasePlanDet2();
                        newPlan.PurchasePlanId = first.PurchasePlanId;
                        newPlan.Flow = first.Flow;
                        newPlan.Item = first.Item;
                        newPlan.ItemDesc = first.ItemDesc;
                        newPlan.RefItemCode = first.RefItemCode;
                        newPlan.PurchaseQty = Convert.ToDecimal(qtyList[i]);
                        newPlan.Uom = first.Uom;
                        newPlan.BaseUom = first.BaseUom;
                        newPlan.UnitCount = first.UnitCount;
                        newPlan.UnitQty = first.UnitQty;
                        newPlan.OrgPurchaseQty = 0;
                        newPlan.WindowTime = Convert.ToDateTime(dateFrom[i]);
                        newPlan.StartTime = Convert.ToDateTime(dateFrom[i]);
                        newPlan.CreateDate = dateTimeNow;
                        newPlan.CreateUser = user.Code;
                        newPlan.LastModifyDate = dateTimeNow;
                        newPlan.LastModifyUser = user.Code;
                        newPlan.Version = 1;
                        newPlan.Id = 0;
                        newPlan.Type = type;
                        newPlan.UUID = System.DateTime.Now.ToString() + r.Next(100, 500) + r.Next(511, 1000);
                        this.genericMgr.Create(newPlan);
                    }
                }
                else
                {
                    this.genericMgr.ExecuteSql(string.Format(" update MRP_PurchasePlanDet2 set PurchaseQty={0},Version=Version+1,LastModifyDate='{2}',LastModifyUser='{3}' where id={1} ", qtyList[i], id, dateTimeNow, user.Code));
                }
            }
        }
        #endregion


        #region    修改班产计划
        [Transaction(TransactionMode.Requires)]
        public void UpdateShiftPlanPlanQty(IList<string> flowList, IList<string> itemList, IList<string> idList, IList<decimal> qtyList, IList<string> releaseNoList, IList<string> dateFrom, User user, IList<string> shiftCodeList)
        {
            var dateTimeNow = System.DateTime.Now;
            Random r = new Random();
            for (int i = 0; i < idList.Count; i++)
            {
                int id = int.Parse(idList[i]);
                if (id == 0)
                {
                    IList<ShiftPlanMstr> pMaster = this.genericMgr.FindAllWithCustomQuery<ShiftPlanMstr>(" select m from ShiftPlanMstr as m where m.ReleaseNo=? ", new object[] { releaseNoList[i] });
                    IList<ShiftPlanDet> searchPlandets = this.genericMgr.FindAllWithCustomQuery<ShiftPlanDet>(" select d from ShiftPlanDet as d where d.ProdLine=? and d.Item=? and d.MstrId=? ", new object[] { flowList[i], itemList[i], pMaster.First().Id });
                    if (searchPlandets != null && searchPlandets.Count > 0)
                    {
                        //Id, PurchasePlanId, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, 
                        //PurchaseQty, Uom, BaseUom, UnitQty, UC, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                        var first = searchPlandets.First();
                        ShiftPlanDet shiftPlan = new ShiftPlanDet();
                        shiftPlan.MstrId = first.MstrId;
                        shiftPlan.ProdLine = first.ProdLine;
                        shiftPlan.Item = first.Item;
                        shiftPlan.ItemDesc = first.ItemDesc;
                        shiftPlan.Qty = Convert.ToDecimal(qtyList[i]);
                        shiftPlan.Uom = first.Uom;
                        shiftPlan.UnitCount = first.UnitCount;
                        shiftPlan.PlanDate = Convert.ToDateTime(dateFrom[i]);
                        shiftPlan.Shift = shiftCodeList[i];
                        shiftPlan.CreateDate = dateTimeNow;
                        shiftPlan.CreateUser = user.Code;
                        shiftPlan.LastModifyDate = dateTimeNow;
                        shiftPlan.LastModifyUser = user.Code;
                        this.genericMgr.Create(shiftPlan);
                    }
                }
                else
                {
                    this.genericMgr.ExecuteSql(string.Format(" update MRP_ShiftPlanDet set Qty={0},LastModifyDate='{2}',LastModifyUser='{3}' where id={1} ", qtyList[i], id, dateTimeNow, user.Code));
                }
            }
        }
        #endregion

        #region    Import ShiftPlan
        private static object readShiftPlanFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public List<ShiftPlanDet> ReadShiftPlanFromXls(Stream inputStream, User user)
        {
            lock (readShiftPlanFromXlsLock)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = DateTime.Today.AddDays(13);
                List<ShiftPlanDet> shiftPlanList = new List<ShiftPlanDet>();
                IList<Shift> shifts = shiftMgr.GetAllShift();
                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }
                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);
                Sheet sheet = workbook.GetSheetAt(0);
                IEnumerator rows = sheet.GetRowEnumerator();
                Row dateRow = sheet.GetRow(0);
                Row shiftRow = sheet.GetRow(1);
                ImportHelper.JumpRows(rows, 2);

                #region 列定义
                int colFlow = 1;//生产线
                int colItem = 2;//物料代码
                #endregion

                var parameters = this.genericMgr.GetDatasetBySql(" select d.Flow,d.Item,i.Desc1,d.uom,d.uc from FlowDet d inner join FlowMstr m on d.Flow=m.Code inner join Item i on i.Code=d.Item where m.Type='Production' ").Tables[0];
                IList<object[]> parameterList = new List<object[]>();
                foreach (System.Data.DataRow flowRow in parameters.Rows)
                {
                    parameterList.Add(new object[] { flowRow[0].ToString(), flowRow[1].ToString(), flowRow[2].ToString(), flowRow[3].ToString(), Convert.ToDecimal(flowRow[4].ToString()) });
                }

                while (rows.MoveNext())
                {
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 0, 3))
                    {
                        break;//边界
                    }

                    string flowCode = string.Empty;
                    string itemCode = string.Empty;

                    #region 读取生产线
                    flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlow));
                    if (string.IsNullOrEmpty(flowCode))
                    {
                        continue;
                        //throw new BusinessErrorException("Import.PSModel.Empty.Error.Flow", (row.RowNum + 1).ToString());
                    }

                    if (parameterList.Where(d => (d[0]).ToString().ToUpper() == flowCode.ToUpper()).Count() == 0)
                    {
                        throw new BusinessErrorException(string.Format("第{0}行：生产线{1}不存在", (row.RowNum + 1).ToString(), flowCode));
                    }
                    #endregion

                    #region 读取成品代码
                    try
                    {
                        itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItem));
                        if (string.IsNullOrEmpty(itemCode))
                        {
                            throw new BusinessErrorException("Import.PSModel.Empty.Error.ItemCode", (row.RowNum + 1).ToString());
                        }
                        else
                        {
                            if (parameterList.Where(d => (d[0]).ToString().ToUpper() == flowCode.ToUpper() && (d[1]).ToString().ToUpper() == itemCode.ToUpper()).Count() == 0)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行：生产线{1}与物料{2}不匹配。", (row.RowNum + 1).ToString(), flowCode, itemCode));
                            }
                        }
                    }
                    catch
                    {
                        throw new BusinessErrorException("Import.PSModel.Read.Error.ItemCode", (row.RowNum + 1).ToString());
                    }
                    #endregion

                    #region
                    int startColIndex = 4;
                    var parameter = parameterList.Where(d => (d[0]).ToString().ToUpper() == flowCode.ToUpper() && (d[1]).ToString().ToUpper() == itemCode.ToUpper()).First();
                    DateTime planDate = DateTime.Now;
                    while (true)
                    {
                        Cell dateCell = dateRow.GetCell(startColIndex);
                        if (dateCell == null)
                        {
                            break;
                        }
                        try
                        {
                            try
                            {
                                planDate = dateCell.DateCellValue;
                            }
                            catch (Exception)
                            {
                                planDate = Convert.ToDateTime(dateCell.StringCellValue);
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        if (planDate < startDate)
                        {
                            startColIndex += 3;
                            continue;
                        }
                        else if (planDate > endDate)
                        {
                            break;
                        }

                        string qty_a = ImportHelper.GetCellStringValue(row.GetCell(startColIndex++));
                        string qty_b = ImportHelper.GetCellStringValue(row.GetCell(startColIndex++));
                        string qty_c = ImportHelper.GetCellStringValue(row.GetCell(startColIndex++));

                        decimal planQty = 0;
                        if (qty_a != null)
                        {
                            bool isSuccess = decimal.TryParse(qty_a, out planQty);
                            if (!isSuccess)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行:早班数量{1}输入有误", (row.RowNum + 1).ToString()));
                            }

                            if (planQty < 0)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行:班产计划数量不能小于0", (row.RowNum + 1).ToString()));
                            }
                            //Id, MstrId, ProdLine, Item, ItemDesc, Qty, , Uom, UnitCount, PlanDate, Shift, CreateDate, CreateUser, LastModifyDate, LastModifyUser
                            ShiftPlanDet shiftPlan = new ShiftPlanDet();
                            shiftPlan.ProdLine = flowCode;
                            shiftPlan.Item = parameter[1].ToString();
                            shiftPlan.ItemDesc = parameter[2].ToString();
                            shiftPlan.Qty = planQty;
                            shiftPlan.Uom = parameter[3].ToString();
                            shiftPlan.UnitCount = Convert.ToDecimal(parameter[4]);
                            shiftPlan.PlanDate = planDate;
                            shiftPlan.Shift = "A";
                            shiftPlanList.Add(shiftPlan);
                        }

                        if (qty_b != null)
                        {
                            bool isSuccess = decimal.TryParse(qty_b, out planQty);
                            if (!isSuccess)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行:中班数量{1}输入有误", (row.RowNum + 1).ToString()));
                            }

                            if (planQty < 0)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行:班产计划数量不能小于0", (row.RowNum + 1).ToString()));
                            }
                            //Id, MstrId, ProdLine, Item, ItemDesc, Qty, , Uom, UnitCount, PlanDate, Shift, CreateDate, CreateUser, LastModifyDate, LastModifyUser
                            ShiftPlanDet shiftPlan = new ShiftPlanDet();
                            shiftPlan.ProdLine = flowCode;
                            shiftPlan.Item = parameter[1].ToString();
                            shiftPlan.ItemDesc = parameter[2].ToString();
                            shiftPlan.Qty = planQty;
                            shiftPlan.Uom = parameter[3].ToString();
                            shiftPlan.UnitCount = Convert.ToDecimal(parameter[4]);
                            shiftPlan.PlanDate = planDate;
                            shiftPlan.Shift = "B";
                            shiftPlanList.Add(shiftPlan);
                        }

                        if (qty_c != null)
                        {
                            bool isSuccess = decimal.TryParse(qty_c, out planQty);
                            if (!isSuccess)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行:晚班数量{1}输入有误", (row.RowNum + 1).ToString()));
                            }

                            if (planQty < 0)
                            {
                                throw new BusinessErrorException(string.Format("第{0}行:班产计划数量不能小于0", (row.RowNum + 1).ToString()));
                            }
                            //Id, MstrId, ProdLine, Item, ItemDesc, Qty, , Uom, UnitCount, PlanDate, Shift, CreateDate, CreateUser, LastModifyDate, LastModifyUser
                            ShiftPlanDet shiftPlan = new ShiftPlanDet();
                            shiftPlan.ProdLine = flowCode;
                            shiftPlan.Item = parameter[1].ToString();
                            shiftPlan.ItemDesc = parameter[2].ToString();
                            shiftPlan.Qty = planQty;
                            shiftPlan.Uom = parameter[3].ToString();
                            shiftPlan.UnitCount = Convert.ToDecimal(parameter[4]);
                            shiftPlan.PlanDate = planDate;
                            shiftPlan.Shift = "C";
                            shiftPlanList.Add(shiftPlan);
                        }
                    }
                    #endregion
                }
                if (shiftPlanList.Count == 0)
                {
                    throw new BusinessErrorException("Import.Result.Error.ImportNothing");
                }


                var planDateList = shiftPlanList.Select(p => p.PlanDate.ToString("yyyy-MM-dd")).Distinct().ToArray();

                string sql = string.Format(" Delete MRP_ShiftPlanDet  where exists(select 1 from MRP_ShiftPlanMstr as m where m.Id=MstrId and m.Status='Create') and CreateUser ='{0}';delete MRP_ShiftPlanMstr where Status='Create' and  CreateUser ='{0}' ", user.Code);
                this.genericMgr.ExecuteSql(sql, null);
                //Dictionary<string, int> planVersions = new Dictionary<string, int>();
                //foreach (var allFlowCode in shiftPlanList.Select(p => p.ProdLine).Distinct())
                //{
                //    planVersions.Add(allFlowCode, numberControlMgr.GenerateNumberNextSequence(string.Format("MRP_ShiftPlan_{0}", allFlowCode)));
                //}
                var batchNo = numberControlMgr.GenerateNumberNextSequence(string.Format("MRP_ShiftPlan_{0}", user.Code));
                string createUsersSql = "select isnull(Max(ReleaseNo),0) from  MRP_ShiftPlanMstr ";

                var createUsers = this.genericMgr.GetDatasetBySql(createUsersSql).Tables[0];
                var releasNo = 0;
                foreach (System.Data.DataRow row in createUsers.Rows)
                {
                    releasNo = int.Parse(row[0].ToString());
                }
                var groupByFlow = shiftPlanList.GroupBy(g => g.ProdLine);

                DateTime dateNow = System.DateTime.Now;
                ShiftPlanMstr m = new ShiftPlanMstr();
                m.Status = "Create";
                m.CreateDate = dateNow;
                m.CreateUser = user.Code;
                m.LastModifyDate = dateNow;
                m.LastModifyUser = user.Code;
                m.ReleaseNo = releasNo + 1;
                m.BatchNo = batchNo;
                this.genericMgr.Create(m);

                foreach (var g in shiftPlanList)
                {
                    g.MstrId = m.Id;
                    g.CreateDate = dateNow;
                    g.CreateUser = user.Code;
                    g.LastModifyDate = dateNow;
                    g.LastModifyUser = user.Code;
                    this.genericMgr.Create(g);
                }
                return shiftPlanList;
            }
        }

        #endregion

        #region   发运计划参数导入
        private static object ReadShipPlanParametersFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadShipPlanParametersFromXls(Stream inputStream, User user)
        {
            lock (ReadShipPlanParametersFromXlsLock)
            {
                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }
                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);
                Sheet sheet = workbook.GetSheetAt(0);
                IEnumerator rows = sheet.GetRowEnumerator();
                ImportHelper.JumpRows(rows, 10);

                #region 销售路线	销售提前期	MRP Code	周起始	周工作日	发运路线	发运提前期	物料代码	安全库存	最大库存	包装量


                int colDisFlow = 1;//销售路线
                int colDisLeadTime = 2;//销售提前期
                int colMrpCode = 3;//MRP Code
                int colDateFst = 4;//周起始
                int colWorkDate = 5;//周工作日
                int colShipFlow = 6;//发运路线
                int colShipLeadTime = 7;//发运提前期
                int colItem = 8;//物料代码
                int colSafeStock = 9;//安全库存
                int colMaxStock = 10;//最大库存
                int colUnitCount = 11;//包装量
                int colIsMrp = 12;//包装量
                #endregion

                var disFlowCodes = this.genericMgr.GetDatasetBySql(" select m.Code,d.Item,d.Id from flowdet as d inner join flowmstr as m on  m.code=d.flow where m.type='Distribution' and m.IsActive=1 ").Tables[0];
                IList<object[]> allDFlowCodeList = new List<object[]>();
                foreach (System.Data.DataRow flowRow in disFlowCodes.Rows)
                {
                    allDFlowCodeList.Add(new object[] { flowRow[0].ToString(), flowRow[1].ToString(), flowRow[2] });
                }

                var traFlowCodes = this.genericMgr.GetDatasetBySql(" select m.Code,d.Item,d.Id from flowdet as d inner join flowmstr as m on  m.code=d.flow where m.type='Transfer' and m.IsActive=1 ").Tables[0];
                IList<object[]> allTFlowCodeList = new List<object[]>();
                foreach (System.Data.DataRow flowRow in traFlowCodes.Rows)
                {
                    allTFlowCodeList.Add(new object[] { flowRow[0].ToString(), flowRow[1].ToString(), flowRow[2].ToString() });
                }
                int rowCount = 10;
                IList<object[]> allReadList = new List<object[]>();
                string errorMessages = string.Empty;
                while (rows.MoveNext())
                {
                    rowCount++;
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 1, 4))
                    {
                        break;//边界
                    }

                    string dFlowCode = string.Empty;
                    int dLeadTime = 0;
                    string mrpCode = string.Empty;
                    int? dateFst = null;
                    string workDate = string.Empty;
                    string tFlowCode = string.Empty;
                    int tLeadTime = 0;
                    string itemCode = string.Empty;
                    decimal safeStock = 0;
                    decimal maxStock = 0;
                    decimal? uc = null;
                    bool? isMrp = null;

                    #region 读取销售路线
                    dFlowCode = ImportHelper.GetCellStringValue(row.GetCell(colDisFlow));
                    if (string.IsNullOrEmpty(dFlowCode))
                    {
                        errorMessages += string.Format("第{0}行：销售路线不能为空。", rowCount);
                        continue;
                    }

                    if (allDFlowCodeList.Where(d => (d[0]).ToString() == dFlowCode).Count() == 0)
                    {
                        //throw new BusinessErrorException(string.Format("第{0}行：销售路线{1}不存在。", rowCount, dFlowCode));
                        errorMessages += "</br>" + string.Format("第{0}行：销售路线{1}不存在。", rowCount, dFlowCode);
                        continue;
                    }
                    #endregion

                    #region 读取销售提前期
                    string rdLeadTime = ImportHelper.GetCellStringValue(row.GetCell(colDisLeadTime));
                    if (string.IsNullOrEmpty(rdLeadTime))
                    {
                        dLeadTime = 0;
                    }
                    else
                    {
                        if (!int.TryParse(rdLeadTime, out dLeadTime))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：销售路线提前期只能填写大于0数字。", rowCount));
                            errorMessages += "</br>" + string.Format("第{0}行：销售路线提前期只能填写大于0数字。", rowCount);
                            continue;
                        }
                        if (dLeadTime < 0)
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：销售路线提前期只能填写大于0数字。", rowCount));
                            errorMessages += "</br>" + string.Format("第{0}行：销售路线提前期只能填写大于0数字。", rowCount);
                            continue;
                        }
                    }
                    #endregion

                    #region MrpCode
                    mrpCode = ImportHelper.GetCellStringValue(row.GetCell(colMrpCode));
                    #endregion

                    #region 周起始
                    string rDateFst = ImportHelper.GetCellStringValue(row.GetCell(colDateFst));
                    if (string.IsNullOrEmpty(rDateFst))
                    {
                        dateFst = null;
                    }
                    else
                    {
                        int s;
                        if (!int.TryParse(rDateFst, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：周起始只能填写1-7数字。", rowCount));
                            errorMessages += "</br>" + string.Format("第{0}行：周起始只能填写1-7数字。", rowCount);
                            continue;
                        }
                        if (s < 0 || s > 7)
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：周起始只能填写1-7数字。", rowCount));
                            errorMessages += "</br>" + string.Format("第{0}行：周起始只能填写1-7数字。", rowCount);
                            continue;
                        }
                        dateFst = s;
                    }
                    #endregion

                    #region WorkDate
                    workDate = ImportHelper.GetCellStringValue(row.GetCell(colWorkDate));
                    #endregion

                    #region 读取发运路线
                    tFlowCode = ImportHelper.GetCellStringValue(row.GetCell(colShipFlow));
                    if (!string.IsNullOrEmpty(tFlowCode))
                    {
                        if (allTFlowCodeList.Where(d => (d[0]).ToString() == tFlowCode).Count() == 0)
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：发运路线{1}不存在。", rowCount, dFlowCode));
                            errorMessages += "</br>" + string.Format("第{0}行：发运路线{1}不存在。", rowCount, dFlowCode);
                            continue;
                        }


                        #region 读取发运路线提前期
                        string rtLeadTime = ImportHelper.GetCellStringValue(row.GetCell(colShipLeadTime));
                        if (string.IsNullOrEmpty(rtLeadTime))
                        {
                            tLeadTime = 0;
                        }
                        else
                        {
                            if (!int.TryParse(rtLeadTime, out tLeadTime))
                            {
                                //throw new BusinessErrorException(string.Format("第{0}行：发运路线提前期只能填写大于0数字。", rowCount));
                                errorMessages += "</br>" + string.Format("第{0}行：发运路线提前期只能填写大于0数字。", rowCount);
                                continue;
                            }
                            if (tLeadTime < 0)
                            {
                                //throw new BusinessErrorException(string.Format("第{0}行：发运路线提前期只能填写大于0数字。", rowCount));
                                errorMessages += "</br>" + string.Format("第{0}行：发运路线提前期只能填写大于0数字。", rowCount);
                                continue;
                            }
                        }
                        #endregion

                        #region 读取安全库存
                        string rSafeStock = ImportHelper.GetCellStringValue(row.GetCell(colSafeStock));
                        if (string.IsNullOrEmpty(rSafeStock))
                        {
                            safeStock = 0;
                        }
                        else
                        {
                            if (!decimal.TryParse(rSafeStock, out safeStock))
                            {
                                //throw new BusinessErrorException(string.Format("第{0}行：安全库存只能填写大于0数字。", rowCount));
                                errorMessages += "</br>" + string.Format("第{0}行：安全库存只能填写大于0数字。", rowCount);
                                continue;
                            }
                            if (safeStock < 0)
                            {
                                //throw new BusinessErrorException(string.Format("第{0}行：安全库存只能填写大于0数字。", rowCount));
                                errorMessages += "</br>" + string.Format("第{0}行：安全库存只能填写大于0数字。", rowCount);
                                continue;
                            }
                        }
                        #endregion

                        #region 读取最大库存库存
                        string rMaxStock = ImportHelper.GetCellStringValue(row.GetCell(colMaxStock));
                        if (string.IsNullOrEmpty(rMaxStock))
                        {
                            maxStock = 0;
                        }
                        else
                        {
                            if (!decimal.TryParse(rMaxStock, out maxStock))
                            {
                                //throw new BusinessErrorException(string.Format("第{0}行：最大库存只能填写大于0数字。", rowCount));
                                errorMessages += "</br>" + string.Format("第{0}行：最大库存只能填写大于0数字。", rowCount);
                                continue;
                            }
                            if (maxStock < 0)
                            {
                                //throw new BusinessErrorException(string.Format("第{0}行：最大库存只能填写大于0数字。", rowCount));
                                errorMessages += "</br>" + string.Format("第{0}行：最大库存只能填写大于0数字。", rowCount);
                                continue;
                            }
                        }
                        #endregion



                    }

                    #endregion

                    //2014-10-09  包装量暂时步允许修改
                    #region 读取包装量
                    //string rUnitCount = ImportHelper.GetCellStringValue(row.GetCell(colUnitCount));
                    //if (!string.IsNullOrEmpty(rUnitCount))
                    //{
                    //    decimal cUc = 0;
                    //    if (!decimal.TryParse(rUnitCount, out cUc))
                    //    {
                    //        //throw new BusinessErrorException(string.Format("第{0}行：包装量只能填写大于0数字。", rowCount));
                    //        errorMessages += "</br>" + string.Format("第{0}行：包装量只能填写大于0数字。", rowCount);
                    //        continue;
                    //    }
                    //    if (uc < 0)
                    //    {
                    //        //throw new BusinessErrorException(string.Format("第{0}行：包装量只能填写大于0数字。", rowCount));
                    //        errorMessages += "</br>" + string.Format("第{0}行：包装量只能填写大于0数字。", rowCount);
                    //        continue;
                    //    }
                    //    uc = cUc;
                    //}
                    #endregion

                    #region 是否参与Mrp
                    string rIsMrp = ImportHelper.GetCellStringValue(row.GetCell(colIsMrp));
                    if (!string.IsNullOrEmpty(rIsMrp))
                    {
                        if (rIsMrp == "0")
                        {
                            isMrp = false;
                        }
                        else if (rIsMrp == "1")
                        {
                            isMrp = true;
                        }
                        else
                        {
                            errorMessages += "</br>" + string.Format("第{0}行：IsMrp填写有误。", rowCount);
                            continue;
                        }
                    }
                    #endregion

                    #region 读取物料代码

                    itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItem));
                    if (string.IsNullOrEmpty(itemCode))
                    {
                        //throw new BusinessErrorException(string.Format("第{0}行：物料代码不能为空。", rowCount));
                        errorMessages += "</br>" + string.Format("第{0}行：物料代码不能为空。", rowCount);
                        continue;
                    }
                    if (allDFlowCodeList.Where(f => f[0].ToString() == dFlowCode && f[1].ToString() == itemCode).Count() == 0)
                    {
                        //throw new BusinessErrorException(string.Format("第{0}行：物料代码{1}不存在销售路线{2}中，请维护。", rowCount, itemCode, dFlowCode));
                        errorMessages += "</br>" + string.Format("第{0}行：物料代码{1}不存在销售路线{2}中，请维护。", rowCount, itemCode, dFlowCode);
                        continue;
                    }
                    if (!string.IsNullOrEmpty(tFlowCode))
                    {
                        if (allTFlowCodeList.Where(f => f[0].ToString() == tFlowCode && f[1].ToString() == itemCode).Count() == 0)
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：物料代码{1}不存在发运路线{2}中,请维护。", rowCount, itemCode, tFlowCode));
                            errorMessages += "</br>" + string.Format("第{0}行：物料代码{1}不存在发运路线{2}中,请维护。", rowCount, itemCode, tFlowCode);
                            continue;
                        }
                    }

                    #endregion
                    //销售路线	销售提前期	MRPCode	周起始	周工作日	发运路线	发运提前期	物料代码	安全库存	最大库存	包装量	

                    object[] objArr = new object[] { dFlowCode, dLeadTime, mrpCode, dateFst, workDate, tFlowCode, tLeadTime, itemCode, safeStock, maxStock, uc, isMrp };
                    allReadList.Add(objArr);
                }
                if (allReadList.Count == 0)
                {
                    throw new BusinessErrorException("导入的有效数据为空。");
                }
                if (!string.IsNullOrEmpty(errorMessages))
                {
                    throw new BusinessErrorException(errorMessages);
                }
                var groupByFlows = (from tak in allReadList
                                    group tak by new
                                    {
                                        dFlowCode = tak[0],
                                        dLeadTime = tak[1],
                                        mrpCode = tak[2],
                                        dateFst = tak[3],
                                        workDate = tak[4],
                                        tFlowCode = tak[5],
                                        tLeadTime = tak[6],
                                    } into result
                                    select new
                                    {
                                        dFlowCode = result.Key.dFlowCode,
                                        dLeadTime = result.Key.dLeadTime,
                                        mrpCode = result.Key.mrpCode,
                                        dateFst = result.Key.dateFst,
                                        workDate = result.Key.workDate,
                                        tFlowCode = result.Key.tFlowCode,
                                        tLeadTime = result.Key.tLeadTime,
                                        list = result.ToList()
                                    }).ToList();
                DateTime newTime = System.DateTime.Now;
                foreach (var byFlow in groupByFlows)
                {
                    string upSql = "update FlowMstr set Code=Code ";
                    if (byFlow.dLeadTime != null)
                    {
                        upSql += string.Format(",MrpLeadTime={0}", byFlow.dLeadTime);
                    }
                    if (!string.IsNullOrEmpty(byFlow.mrpCode.ToString()))
                    {
                        upSql += string.Format(",MrpCode='{0}'", byFlow.mrpCode);
                    }
                    if (byFlow.dateFst != null)
                    {
                        upSql += string.Format(" ,DateFst='{0}' ", byFlow.dateFst);
                    }
                    if (!string.IsNullOrEmpty(byFlow.workDate.ToString()))
                    {
                        upSql += string.Format(",WorkDate='{0}'", byFlow.workDate);
                    }
                    if (byFlow.tFlowCode != null && !string.IsNullOrEmpty(byFlow.tFlowCode.ToString()))
                    {
                        upSql += string.Format(",ShipFlow='{0}'", byFlow.tFlowCode);
                    }
                    upSql += string.Format(",lastModifyDate='{1}',LastModifyUser='{2}' where code='{0}' ", byFlow.dFlowCode, newTime, user.Code);
                    this.genericMgr.ExecuteSql(upSql);

                    //upSql = string.Format(" update FlowMstr set MrpLeadTime={0},lastModifyDate='{2}',LastModifyUser='{3}' where Code='{1}' ", byFlow.tLeadTime, byFlow.tFlowCode, newTime, user.Code);
                    //this.genericMgr.ExecuteSql(upSql);

                    foreach (var l in byFlow.list)
                    {
                        //销售路线	销售提前期	MRPCode	周起始	周工作日	发运路线	发运提前期	物料代码	安全库存	最大库存	包装量	
                        //upSql = string.Format(" update FlowDet set Uc={0},lastModifyDate='{1}',LastModifyUser='{2}' where Flow='{3}' and Item='{4}' ", l[10], newTime, user.Code, l[0], l[7]);
                        //this.genericMgr.ExecuteSql(upSql);

                        //2014-10-09  包装量暂时步允许修改
                        //if (l[10] != null && ((decimal?)l[10]).HasValue)
                        //{
                        //    upSql = string.Format(" update FlowDet set MaxStock={0},SafeStock={1},HuLotSize={2},lastModifyDate='{3}',LastModifyUser='{4}' where Flow='{5}' and Item='{6}' ", l[9], l[8], l[10], newTime, user.Code, l[5], l[7]);
                        //}
                        //else
                        //{
                        // upSql = string.Format(" update FlowDet set MaxStock={0},SafeStock={1},lastModifyDate='{3}',LastModifyUser='{4}' where Flow='{5}' and Item='{6}' ", l[9], l[8],"", newTime, user.Code, l[5], l[7]);
                        //}
                        // upSql = string.Format(" update FlowDet set MaxStock={0},SafeStock={1},lastModifyDate='{3}',LastModifyUser='{4}' where Flow='{5}' and Item='{6}' ", l[9], l[8], "", newTime, user.Code, l[5], l[7]);

                        if (l[11] != null && ((bool?)l[11]).HasValue)
                        {
                            upSql = string.Format(" update FlowDet set MaxStock={0},SafeStock={1},lastModifyDate='{3}',LastModifyUser='{4}',IsMrp={5} where Flow='{6}' and Item='{7}' ", l[9], l[8],"", newTime, user.Code, ((bool?)l[11]).Value?1:0, l[5], l[7]);
                        }
                        else
                        {
                            upSql = string.Format(" update FlowDet set MaxStock={0},SafeStock={1},lastModifyDate='{3}',LastModifyUser='{4}' where Flow='{5}' and Item='{6}' ", l[9], l[8], "", newTime, user.Code, l[5], l[7]);
                        }
                        this.genericMgr.ExecuteSql(upSql);
                    }

                }
            }
        }
        #endregion

        #region   生产计划参数导入
        private static object ReadProductionPlanParametersFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadProductionPlanParametersFromXls(Stream inputStream, User user)
        {
            lock (ReadProductionPlanParametersFromXlsLock)
            {
                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }
                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);
                Sheet sheet = workbook.GetSheetAt(0);
                IEnumerator rows = sheet.GetRowEnumerator();
                ImportHelper.JumpRows(rows, 10);

                #region 成品/半成品代码	Bom代码	生产提前期	报废率	安全库存	最大库存	经济批量	包装量



                int colItemCode = 1;//成品/半成品代码
                int colBom = 2;//Bom代码
                int colLeadTime = 3;//生产提前期
                int colScrapPct = 4;//报废率
                int colsafeStock = 5;//安全库存
                int colMaxStock = 6;//最大库存
                int colMinLotSize = 7;//经济批量
                int colUnitCount = 8;//包装量
                int colInventoryCountDown = 9;//库存倒数
                #endregion

                //var disFlowCodes = this.genericMgr.GetDatasetBySql(" select m.Code,d.Item,d.Id from flowdet as d inner join flowmstr as m on  m.code=d.flow where m.type='Distribution' and m.IsActive=1 ").Tables[0];
                //IList<object[]> allDFlowCodeList = new List<object[]>();
                //foreach (System.Data.DataRow flowRow in disFlowCodes.Rows)
                //{
                //    allDFlowCodeList.Add(new object[] { flowRow[0].ToString(), flowRow[1].ToString(), flowRow[2] });
                //}

                var allProdItems = this.genericMgr.FindAllWithCustomQuery<Item>(" select i from Item as i where i.IsActive=1 ");
                var allBoms = this.genericMgr.FindAllWithCustomQuery<Bom>(" select i from Bom as i where i.IsActive=1 ");
                var updateItemList = new List<Item>();
                int rowCount = 10;
                string errorMessages = string.Empty;
                while (rows.MoveNext())
                {
                    rowCount++;
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 1, 4))
                    {
                        break;//边界
                    }
                    // 成品/半成品代码	Bom代码	生产提前期	报废率	安全库存	最大库存	经济批量	包装量
                    string itemCode = string.Empty;
                    string bomCode = string.Empty;
                    int? leadTime = null;
                    int? scrapPct = null;
                    int? safeStock = null;
                    int? maxStock = null;
                    int? minLotSize = null;
                    decimal? uc = null;
                    decimal? inventoryCountDown = null;
                    Item currentItem = null;

                    #region 读取成品代码
                    itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                    if (string.IsNullOrEmpty(itemCode))
                    {
                        //throw new BusinessErrorException(string.Format("第{0}行：成品代码不能为空。", rowCount));
                        errorMessages += string.Format("第{0}行：成品代码不能为空。", rowCount);
                        continue;
                    }

                    if (allProdItems.Where(d => d.Code == itemCode).Count() == 0)
                    {
                        errorMessages += "</br/>" + string.Format("第{0}行：成品代码{1}不存在。", rowCount, itemCode);
                        continue;
                        //throw new BusinessErrorException(string.Format("第{0}行：成品代码{1}不存在。", rowCount, itemCode));
                    }
                    currentItem = allProdItems.FirstOrDefault(d => d.Code == itemCode);
                    #endregion

                    #region Bom代码
                    bomCode = ImportHelper.GetCellStringValue(row.GetCell(colBom));
                    if (!string.IsNullOrEmpty(bomCode))
                    {
                        if (allBoms.Where(d => d.Code == bomCode).Count() == 0)
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：Bom代码{1}不存在。", rowCount, bomCode));
                            errorMessages += "</br/>" + string.Format("第{0}行：Bom代码{1}不存在。", rowCount, bomCode);
                            continue;
                        }
                        currentItem.Bom = allBoms.FirstOrDefault(d => d.Code == bomCode);
                    }


                    #endregion

                    #region 生产提前期
                    string rLeadTime = ImportHelper.GetCellStringValue(row.GetCell(colLeadTime));
                    if (!string.IsNullOrEmpty(rLeadTime))
                    {
                        int s;
                        if (!int.TryParse(rLeadTime, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：生产提前期只能填写数字。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：生产提前期只能填写数字。", rowCount);
                            continue;
                        }
                        currentItem.LeadTime = s;
                    }
                    #endregion

                    #region 报废率
                    string rScrapPct = ImportHelper.GetCellStringValue(row.GetCell(colScrapPct));
                    if (!string.IsNullOrEmpty(rScrapPct))
                    {
                        decimal s;
                        if (!decimal.TryParse(rScrapPct, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：报废率填写有误。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：报废率填写有误。", rowCount);
                            continue;
                        }
                        currentItem.ScrapPct = s;
                    }
                    #endregion

                    #region 安全库存
                    string rSafeStock = ImportHelper.GetCellStringValue(row.GetCell(colsafeStock));
                    if (!string.IsNullOrEmpty(rSafeStock))
                    {
                        int s;
                        if (!int.TryParse(rSafeStock, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：安全库存填写有误。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：安全库存填写有误。", rowCount);
                            continue;
                        }
                        currentItem.SafeStock = s;
                    }
                    #endregion

                    #region 最大库存
                    string rMaxstock = ImportHelper.GetCellStringValue(row.GetCell(colMaxStock));
                    if (!string.IsNullOrEmpty(rMaxstock))
                    {
                        int s;
                        if (!int.TryParse(rMaxstock, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：最大库存填写有误。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：最大库存填写有误。", rowCount);
                            continue;
                        }
                        currentItem.MaxStock = s;
                    }
                    #endregion

                    #region 经济批量
                    string rMinLotSize = ImportHelper.GetCellStringValue(row.GetCell(colMinLotSize));
                    if (!string.IsNullOrEmpty(rMinLotSize))
                    {
                        int s;
                        if (!int.TryParse(rMinLotSize, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：经济批量填写有误。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：经济批量填写有误。", rowCount);
                            continue;
                        }
                        currentItem.MinLotSize = s;
                    }
                    #endregion

                    //2014-10-09  包装量暂时步允许修改
                    #region 包装量
                    //string rUc = ImportHelper.GetCellStringValue(row.GetCell(colUnitCount));
                    //if (!string.IsNullOrEmpty(rUc))
                    //{
                    //    decimal s;
                    //    if (!decimal.TryParse(rUc, out s))
                    //    {
                    //        //throw new BusinessErrorException(string.Format("第{0}行：包装量填写有误。", rowCount));
                    //        errorMessages += "</br/>" + string.Format("第{0}行：包装量填写有误。", rowCount);
                    //        continue;
                    //    }
                    //    //currentItem.HuLotSize = s;
                    //    currentItem.UnitCount = s;
                    //}
                    #endregion

                    #region 库存倒数
                    string rInventoryCountDown = ImportHelper.GetCellStringValue(row.GetCell(colInventoryCountDown));
                    if (!string.IsNullOrEmpty(rInventoryCountDown))
                    {
                        decimal s;
                        if (!decimal.TryParse(rInventoryCountDown, out s))
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：包装量填写有误。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：库存倒数填写有误。", rowCount);
                            continue;
                        }
                        if (s<=0)
                        {
                            //throw new BusinessErrorException(string.Format("第{0}行：包装量填写有误。", rowCount));
                            errorMessages += "</br/>" + string.Format("第{0}行：库存倒数填写有误。", rowCount);
                            continue;
                        }
                        //currentItem.HuLotSize = s;
                        currentItem.InventoryCountDown = s;
                    }
                    #endregion
                    updateItemList.Add(currentItem);

                }
                if (updateItemList.Count == 0)
                {
                    throw new BusinessErrorException("导入的有效数据为空。");
                }
                if (!string.IsNullOrEmpty(errorMessages))
                {
                    throw new BusinessErrorException(errorMessages);
                }
                DateTime nowTime = System.DateTime.Now;
                foreach (var item in updateItemList)
                {
                    item.LastModifyDate = nowTime;
                    item.LastModifyUser = user;
                    this.genericMgr.Update(item);

                    //if (item.HuLotSize.HasValue && item.HuLotSize.Value > 0)
                    //{
                    //    string upSql = string.Format(" update d set d.HuLotSize={0} from FlowDet d,FlowMstr  f where d.Flow=f.Code and d.Item='{1}' and f.Type='Production'",item.HuLotSize.Value,item.Code);
                    //}
                }
            }
        }
        #endregion

        #region   采购计划参数导入
        private static object ReadPurchasePlanParametersFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadPurchasePlanParametersFromXls(Stream inputStream, User user)
        {
            lock (ReadPurchasePlanParametersFromXlsLock)
            {
                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }
                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);
                Sheet sheet = workbook.GetSheetAt(0);
                IEnumerator rows = sheet.GetRowEnumerator();
                ImportHelper.JumpRows(rows, 10);

                #region 采购路线	采购提前期	物料代码	安全库存	最大库存	包装量	MOQ	"窗口时间

                int colFlowCode = 1;//采购路线
                int colLeadTime = 2;//采购提前期
                int colItemCode = 3;//物料代码
                int colsafeStock = 4;//安全库存
                int colMaxStock = 5;//最大库存
                int colUnitCount = 6;//包装量
                int colMinLotSize = 7;//MOQ
                int colWindowTime1 = 8;//窗口时间1
                int colWindowTime2 = 9;//窗口时间2
                int colWindowTime3 = 10;//窗口时间3
                int colWindowTime4 = 11;//窗口时间4
                int colWindowTime5 = 12;//窗口时间5
                int colWindowTime6 = 13;//窗口时间6
                int colWindowTime7 = 14;//窗口时间7
                #endregion

                IList<Flow> upFlows = new List<Flow>();
                IList<Item> upItems = new List<Item>();
                //IList<FlowDetail> upFlowDets = new List<FlowDetail>();

                //var allProdItems = this.genericMgr.FindAllWithCustomQuery<Item>(" select i from Item as i where i.Type <> 'P' ");
                //var allBoms = this.genericMgr.FindAllWithCustomQuery<Bom>(" select i from Bom as i where i.IsActive=1 ");
                //var updateItemList = new List<Item>();
                int rowCount = 10;
                string errorMessages = string.Empty;
                while (rows.MoveNext())
                {
                    rowCount++;
                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 1, 2))
                    {
                        break;//边界
                    }
                    // 采购路线	采购提前期	物料代码	安全库存	最大库存	包装量	MOQ	"窗口时间
                    string flowCode = string.Empty;
                    int? leadTime = null;
                    string itemCode = null;
                    int? safeStock = null;
                    int? maxStock = null;
                    decimal? uc = null;
                    int? minLotSize = null;
                    string windowTime1 = string.Empty;
                    string windowTime2 = string.Empty;
                    string windowTime3 = string.Empty;
                    string windowTime4 = string.Empty;
                    string windowTime5 = string.Empty;
                    string windowTime6 = string.Empty;
                    string windowTime7 = string.Empty;

                    Flow currentFlow = null;
                    Item currentItem = null;
                    //FlowDetail currentFlowDetail = null;

                    #region 采购路线
                    flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlowCode));
                    if (string.IsNullOrEmpty(flowCode))
                    {
                        //throw new BusinessErrorException(string.Format("第{0}行：成品代码不能为空。", rowCount));
                        errorMessages += string.Format("第{0}行：采购路线代码不能为空。", rowCount);
                        continue;
                    }
                    #endregion

                    if (upFlows.Where(c => c.Code == flowCode).Count() > 0)
                    {
                        currentFlow = upFlows.First(c => c.Code == flowCode);
                    }
                    else
                    {
                        currentFlow = this.flowMgr.LoadFlow(flowCode);

                        if (currentFlow == null)
                        {
                            errorMessages += "</br/>" + string.Format("第{0}行：采购路线{1}不存在。", rowCount, flowCode);
                            continue;
                        }
                        if (currentFlow.Type != "Procurement")
                        {
                            errorMessages += "</br/>" + string.Format("第{0}行：采购路线{1}不存在。", rowCount, flowCode);
                            continue;
                        }

                        #region 生产提前期
                        string rLeadTime = ImportHelper.GetCellStringValue(row.GetCell(colLeadTime));
                        if (!string.IsNullOrEmpty(rLeadTime))
                        {
                            decimal s;
                            if (!decimal.TryParse(rLeadTime, out s))
                            {
                                errorMessages += "</br/>" + string.Format("第{0}行：生产提前期只能填写数字。", rowCount);
                                continue;
                            }
                            currentFlow.MrpLeadTime = s;
                        }
                        #endregion

                        #region  窗口时间
                        windowTime1 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime1));
                        windowTime2 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime2));
                        windowTime3 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime3));
                        windowTime4 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime4));
                        windowTime5 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime5));
                        windowTime6 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime6));
                        windowTime7 = ImportHelper.GetCellStringValue(row.GetCell(colWindowTime7));
                        #endregion
                        //currentFlow.WinTime1 = windowTime1;
                        //currentFlow.WinTime2 = windowTime2;
                        //currentFlow.WinTime3 = windowTime3;
                        //currentFlow.WinTime4 = windowTime4;
                        //currentFlow.WinTime5 = windowTime5;
                        //currentFlow.WinTime6 = windowTime6;
                        //currentFlow.WinTime7 = windowTime7;

                        upFlows.Add(currentFlow);
                    }

                    #region 读取物料代码
                    itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                    if (string.IsNullOrEmpty(itemCode))
                    {
                        //throw new BusinessErrorException(string.Format("第{0}行：成品代码不能为空。", rowCount));
                        errorMessages += string.Format("第{0}行：物料代码不能为空。", rowCount);
                        continue;
                    }

                    if (currentFlow.FlowDetails.Where(d => d.Item.Code == itemCode).Count() == 0)
                    {
                        errorMessages += "</br/>" + string.Format("第{0}行：物料代码{1}不存在路线{2}中，请维护。", rowCount, itemCode, currentFlow.Code);
                        continue;
                    }
                    else
                    {
                        currentItem = currentFlow.FlowDetails.First(d => d.Item.Code == itemCode).Item;


                        #region 安全库存
                        string rSafeStock = ImportHelper.GetCellStringValue(row.GetCell(colsafeStock));
                        if (!string.IsNullOrEmpty(rSafeStock))
                        {
                            int s;
                            if (!int.TryParse(rSafeStock, out s))
                            {
                                errorMessages += "</br/>" + string.Format("第{0}行：安全库存填写有误。", rowCount);
                                continue;
                            }
                            currentItem.SafeStock = s;
                        }
                        #endregion

                        #region 最大库存
                        string rMaxstock = ImportHelper.GetCellStringValue(row.GetCell(colMaxStock));
                        if (!string.IsNullOrEmpty(rMaxstock))
                        {
                            int s;
                            if (!int.TryParse(rMaxstock, out s))
                            {
                                errorMessages += "</br/>" + string.Format("第{0}行：最大库存填写有误。", rowCount);
                                continue;
                            }
                            currentItem.MaxStock = s;
                        }
                        #endregion

                        #region 经济批量
                        string rMinLotSize = ImportHelper.GetCellStringValue(row.GetCell(colMinLotSize));
                        if (!string.IsNullOrEmpty(rMinLotSize))
                        {
                            int s;
                            if (!int.TryParse(rMinLotSize, out s))
                            {
                                errorMessages += "</br/>" + string.Format("第{0}行：经济批量填写有误。", rowCount);
                                continue;
                            }
                            currentItem.MinLotSize = s;
                        }
                        #endregion

                        upItems.Add(currentItem);

                        //currentFlowDetail = currentFlow.FlowDetails.First(d => d.Item.Code == itemCode);
                        //var cFlowDetails = currentFlow.FlowDetails.Where(d => d.Item.Code == itemCode);
                        //2014-10-09  包装量暂时步允许修改
                        #region 包装量
                        //string rUc = ImportHelper.GetCellStringValue(row.GetCell(colUnitCount));

                        //if (!string.IsNullOrEmpty(rUc))
                        //{
                        //    int s;
                        //    if (!int.TryParse(rUc, out s))
                        //    {
                        //        errorMessages += "</br/>" + string.Format("第{0}行：包装量{1}填写有误。", rowCount, rUc);
                        //        continue;
                        //    }
                        //    foreach (var fd in cFlowDetails)
                        //    {
                        //        //fd.UnitCount = s;
                        //        fd.HuLotSize = s;
                        //        upFlowDets.Add(fd);
                        //    }
                        //}
                        #endregion
                    }

                    #endregion
                }

                if (!string.IsNullOrEmpty(errorMessages))
                {
                    throw new BusinessErrorException(errorMessages);
                }

                if (upFlows.Count == 0)
                {
                    throw new BusinessErrorException("导入的有效数据为空。");
                }

                DateTime nowTime = System.DateTime.Now;
                foreach (var flow in upFlows)
                {
                    flow.LastModifyDate = nowTime;
                    flow.LastModifyUser = user;
                    this.genericMgr.Update(flow);
                }
                //foreach (var det in upFlowDets)
                //{
                //    det.LastModifyDate = nowTime;
                //    det.LastModifyUser = user;
                //    this.genericMgr.Update(det);
                //}
                foreach (var item in upItems)
                {
                    item.LastModifyDate = nowTime;
                    item.LastModifyUser = user;
                    this.genericMgr.Update(item);
                }
            }
        }
        #endregion

        #region  发运计划导入
        private static object readShipPlanFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadShipPlanFromXls(Stream inputStream, User user, ShipPlanMstr shipPlanMstr)
        {
            lock (readShipPlanFromXlsLock)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = startDate.AddDays(13);
                DateTime nowTime = System.DateTime.Now;

                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }

                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

                Sheet sheet = workbook.GetSheetAt(0);

                IEnumerator rows = sheet.GetRowEnumerator();

                Row dateRow = sheet.GetRow(0);

                ImportHelper.JumpRows(rows, 2);

                var shipPlanList = new List<ShipPlanDet>();
                var existsDets = this.genericMgr.FindAllWithCustomQuery<ShipPlanDet>(" select s from ShipPlanDet as s where s.ShipPlanId=? ", shipPlanMstr.Id);

                //路线	提前期	物料号	物料描述	客户零件号	包装量	安全库存	最大库存	3PL期初	在途
                int colFlow = 1;//路线
                int colItemCode = 3;//物料号

                List<string> errorMessages = new List<string>();


                while (rows.MoveNext())
                {
                    string flowCode = null;
                    string itemCode = null;

                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 0, 3))
                    {
                        break;//边界
                    }
                    string rowIndex = (row.RowNum + 1).ToString();

                    #region 读取路线代码
                    flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlow));
                    if (string.IsNullOrEmpty(flowCode))
                    {
                        errorMessages.Add(string.Format("路线不能为空,第{0}行", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取物料代码
                    try
                    {
                        itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                        if (itemCode == null)
                        {
                            errorMessages.Add(string.Format("物料不能为空,第{0}行", rowIndex));
                            continue;
                        }
                    }
                    catch
                    {
                        errorMessages.Add(string.Format("读取物料时出错,第{0}行.", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取数量
                    try
                    {
                        int i = 11;
                        Random r = new Random();
                        int t = r.Next(0, 300);
                        int tt = r.Next(500, 1000);
                        while (true)
                        {
                            if (i > 81)
                            {
                                break;
                            }
                            Cell dateCell = dateRow.GetCell(i);
                            string dateIndex = null;

                            #region 读取计划日期
                            if (dateCell != null)
                            {
                                DateTime currentDateTime = DateTime.Today;
                                if (dateCell.CellType == CellType.STRING)
                                {
                                    dateIndex = dateCell.StringCellValue;
                                }

                                //if (currentDateTime.CompareTo(startDate) < 0)
                                //{
                                //    continue;
                                //}
                                //if (currentDateTime.CompareTo(endDate) > 0)
                                //{
                                //    break;
                                //}
                            }
                            else
                            {
                                break;
                            }
                            #endregion

                            decimal qty = 0;
                            if (row.GetCell(i + 2) != null)
                            {
                                if (row.GetCell(i + 2).CellType == CellType.NUMERIC)
                                {
                                    qty = Convert.ToDecimal(row.GetCell(i + 2).NumericCellValue);
                                }
                                else
                                {
                                    string qtyValue = ImportHelper.GetCellStringValue(row.GetCell(i + 2));
                                    if (qtyValue != null)
                                    {
                                        if (!decimal.TryParse(qtyValue, out qty))
                                        {
                                            errorMessages.Add(string.Format("数量格式不正确,第{0}行", rowIndex));
                                            i += 5;
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                i += 5;
                                continue;
                            }

                            if (qty < 0)
                            {
                                errorMessages.Add(string.Format("数量不能小于0,第{0}行", rowIndex));
                                i += 5;
                                continue;
                            }
                            else
                            {
                                var existsDet = existsDets.Where(e => e.Flow == flowCode && e.Item == itemCode && e.StartTime.ToString("yyyy-MM-dd") == dateIndex);
                                if (existsDet != null && existsDet.Count() > 0)
                                {
                                    var first = existsDet.First();
                                    if (first.ShipQty != qty)
                                    {
                                        first.ShipQty = qty;
                                        first.LastModifyDate = nowTime;
                                        first.LastModifyUser = user.Code;
                                        shipPlanList.Add(first);
                                    }
                                }
                                else
                                {
                                    if (qty == 0)
                                    {
                                        i += 5;
                                        continue;
                                    }
                                    var flowDets = this.genericMgr.GetDatasetBySql(string.Format(@"select m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1 from FlowMstr as m 
                                                                                                    inner join FlowDet as d on d.Flow=m.Code 
                                                                                                    inner join Item as i on i.Code=d.Item
                                                                                                    where m.Code='{0}' and d.Item='{1}' ", flowCode, itemCode)).Tables[0];          //m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1
                                    var getFlowDetList = new List<object[]>();
                                    foreach (System.Data.DataRow readRow in flowDets.Rows)
                                    {
                                        getFlowDetList.Add(new object[] { readRow[0].ToString(), readRow[1].ToString(), readRow[2].ToString(), readRow[3].ToString(), Convert.ToDecimal(readRow[4].ToString()), readRow[5].ToString(), readRow[6].ToString() });
                                    }
                                    if (getFlowDetList.Count == 0)
                                    {
                                        errorMessages.Add(string.Format("第{0}行:路线{1}中没有维护物料{2}", rowIndex, flowCode, itemCode));
                                        i += 5;
                                        continue;
                                    }
                                    //m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1
                                    ShipPlanDet nDet = new ShipPlanDet();
                                    nDet.Id = 0;
                                    nDet.ShipPlanId = shipPlanMstr.Id;
                                    nDet.Flow = getFlowDetList[0][0].ToString();
                                    nDet.Item = getFlowDetList[0][2].ToString();
                                    nDet.ItemDesc = getFlowDetList[0][6].ToString();
                                    nDet.RefItemCode = getFlowDetList[0][3].ToString();
                                    nDet.ShipQty = qty;
                                    nDet.Uom = getFlowDetList[0][5].ToString();
                                    nDet.BaseUom = getFlowDetList[0][5].ToString();
                                    nDet.UnitQty = 1;
                                    nDet.UnitCount = Convert.ToDecimal(getFlowDetList[0][4]);
                                    nDet.LocFrom = getFlowDetList[0][1].ToString();
                                    nDet.LocTo = string.Empty;
                                    nDet.StartTime = Convert.ToDateTime(dateIndex);
                                    nDet.WindowTime = Convert.ToDateTime(dateIndex);
                                    nDet.CreateDate = nowTime;
                                    nDet.CreateUser = user.Code;
                                    nDet.LastModifyDate = nowTime;
                                    nDet.LastModifyUser = user.Code;
                                    nDet.Version = 1;
                                    nDet.OrgShipQty = 0;
                                    nDet.ReqQty = 0;
                                    nDet.UUID = System.DateTime.Now.ToString() + t++ + tt++;
                                    nDet.OrderQty = 0;
                                    nDet.Type = "Daily";
                                    shipPlanList.Add(nDet);
                                }
                            }
                            i += 5;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(ex.Message);
                    }
                    #endregion
                }

                if (errorMessages.Count > 0)
                {
                    string errorMes = string.Empty;
                    foreach (var error in errorMessages)
                    {
                        errorMes += error + "-";
                    }
                    throw new BusinessErrorException(errorMes);
                }

                foreach (var shipPlan in shipPlanList)
                {
                    if (shipPlan.Id > 0)
                    {
                        this.genericMgr.Update(shipPlan);
                    }
                    else
                    {
                        this.genericMgr.Create(shipPlan);
                    }
                }
            }
        }
        #endregion

        #region  生产计划导入
        private static object ReadProductionPlanFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadProductionPlanFromXls(Stream inputStream, User user, ProductionPlanMstr productionPlanMstr)
        {
            lock (ReadProductionPlanFromXlsLock)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = startDate.AddDays(13);
                DateTime nowTime = System.DateTime.Now;

                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }

                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

                Sheet sheet = workbook.GetSheetAt(0);

                IEnumerator rows = sheet.GetRowEnumerator();

                Row dateRow = sheet.GetRow(0);

                ImportHelper.JumpRows(rows, 2);

                var planList = new List<ProductionPlanDet>();
                var existsDets = this.genericMgr.FindAllWithCustomQuery<ProductionPlanDet>(" select s from ProductionPlanDet as s where s.ProductionPlanId=? ", productionPlanMstr.Id);

                int colItemCode = 1;//物料号

                List<string> errorMessages = new List<string>();


                while (rows.MoveNext())
                {
                    string flowCode = null;
                    string itemCode = null;

                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 0, 3))
                    {
                        break;//边界
                    }
                    string rowIndex = (row.RowNum + 1).ToString();

                    #region 读取物料代码
                    try
                    {
                        itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                        if (itemCode == null)
                        {
                            errorMessages.Add(string.Format("物料不能为空,第{0}行", rowIndex));
                            continue;
                        }
                    }
                    catch
                    {
                        errorMessages.Add(string.Format("读取物料时出错,第{0}行.", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取数量
                    try
                    {
                        int i = 10;
                        Random r = new Random();
                        int t = r.Next(0, 300);
                        int tt = r.Next(500, 1000);
                        while (true)
                        {
                            if (i > 62)
                            {
                                break;
                            }
                            Cell dateCell = dateRow.GetCell(i);
                            string dateIndex = null;

                            #region 读取计划日期
                            if (dateCell != null)
                            {
                                DateTime currentDateTime = DateTime.Today;
                                if (dateCell.CellType == CellType.STRING)
                                {
                                    dateIndex = dateCell.StringCellValue;
                                }

                                //if (currentDateTime.CompareTo(startDate) < 0)
                                //{
                                //    continue;
                                //}
                                //if (currentDateTime.CompareTo(endDate) > 0)
                                //{
                                //    break;
                                //}
                            }
                            else
                            {
                                break;
                            }
                            #endregion

                            decimal qty = 0;
                            if (row.GetCell(i + 2) != null)
                            {
                                if (row.GetCell(i + 2).CellType == CellType.NUMERIC)
                                {
                                    qty = Convert.ToDecimal(row.GetCell(i + 2).NumericCellValue);
                                }
                                else
                                {
                                    string qtyValue = ImportHelper.GetCellStringValue(row.GetCell(i + 2));
                                    if (qtyValue != null)
                                    {
                                        if (!decimal.TryParse(qtyValue, out qty))
                                        {
                                            errorMessages.Add(string.Format("数量格式不正确,第{0}行", rowIndex));
                                            i += 4;
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                i += 4;
                                continue;
                            }

                            if (qty < 0)
                            {
                                errorMessages.Add(string.Format("数量不能小于0,第{0}行", rowIndex));
                                i += 4;
                                continue;
                            }
                            else
                            {
                                var existsDet = existsDets.Where(e => e.Item == itemCode && e.StartTime.ToString("yyyy-MM-dd") == dateIndex);
                                if (existsDet != null && existsDet.Count() > 0)
                                {
                                    var first = existsDet.First();
                                    if (first.Qty != qty)
                                    {
                                        first.Qty = qty;
                                        first.LastModifyDate = nowTime;
                                        first.LastModifyUser = user.Code;
                                        planList.Add(first);
                                    }
                                }
                                else
                                {
                                    if (qty == 0)
                                    {
                                        i += 4;
                                        continue;
                                    }
                                    var flowDets = this.genericMgr.GetDatasetBySql(string.Format(@"select i.Code,i.Desc1,d.RefItemCode,i.Uom,i.UC,i.MinLotSize from Item i left join FlowDet d on i.Code=d.Item where i.Code='{0}' and d.RefItemCode is not null and d.RefItemCode<>'' ", itemCode)).Tables[0];          //m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1
                                    var getFlowDetList = new List<object[]>();
                                    foreach (System.Data.DataRow readRow in flowDets.Rows)
                                    {
                                        getFlowDetList.Add(new object[] { readRow[0].ToString(), readRow[1].ToString(), readRow[2].ToString(), readRow[3].ToString(), Convert.ToDecimal(readRow[4].ToString()), Convert.ToDecimal(readRow[5].ToString()) });
                                    }
                                    if (getFlowDetList.Count == 0)
                                    {
                                        errorMessages.Add(string.Format("第{0}行:路线{1}中没有维护物料{2}", rowIndex, flowCode, itemCode));
                                        i += 4;
                                        continue;
                                    }
                                    //i.Code,i.Desc1,d.RefItemCode,i.Uom,i.UC,i.MinLotSize
                                    //Id, ProductionPlanId, Type, UUID, Item, ItemDesc, RefItemCode, ReqQty, OrgQty, Qty, 
                                    //OrderQty, Uom, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyUser, LastModifyDate, Version

                                    //Item itemE = this.genericMgr.FindById<Item>(itemCode);
                                    ProductionPlanDet nDet = new ProductionPlanDet();
                                    nDet.Id = 0;
                                    nDet.ProductionPlanId = productionPlanMstr.Id;
                                    nDet.Type = "Daily";
                                    nDet.UUID = System.DateTime.Now.ToString() + t++ + tt++;
                                    nDet.Item = getFlowDetList[0][0].ToString();
                                    nDet.ItemDesc = getFlowDetList[0][1].ToString();
                                    nDet.RefItemCode = getFlowDetList[0][2].ToString();
                                    nDet.ReqQty = 0;
                                    nDet.OrgQty = 0;
                                    nDet.Qty = qty;
                                    nDet.OrderQty = 0;
                                    nDet.Uom = getFlowDetList[0][3].ToString();
                                    nDet.UnitCount = Convert.ToDecimal(getFlowDetList[0][4]);
                                    nDet.MinLotSize = Convert.ToDecimal(getFlowDetList[0][5]);
                                    nDet.StartTime = Convert.ToDateTime(dateIndex);
                                    nDet.WindowTime = Convert.ToDateTime(dateIndex);
                                    nDet.CreateDate = nowTime;
                                    nDet.CreateUser = user.Code;
                                    nDet.LastModifyDate = nowTime;
                                    nDet.LastModifyUser = user.Code;
                                    nDet.Version = 1;
                                    planList.Add(nDet);
                                }
                            }
                            i += 4;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(ex.Message);
                    }
                    #endregion
                }

                if (errorMessages.Count > 0)
                {
                    string errorMes = string.Empty;
                    foreach (var error in errorMessages)
                    {
                        errorMes += error + "-";
                    }
                    throw new BusinessErrorException(errorMes);
                }

                foreach (var p in planList)
                {
                    if (p.Id > 0)
                    {
                        this.genericMgr.Update(p);
                    }
                    else
                    {
                        this.genericMgr.Create(p);
                    }
                }
            }
        }
        #endregion

        #region  采购计划导入
        private static object readPurchasePlanFromXlsLock = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadPurchasePlanFromXls(Stream inputStream, User user, PurchasePlanMstr purchasePlanMstr)
        {
            lock (readPurchasePlanFromXlsLock)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = startDate.AddDays(13);
                DateTime nowTime = System.DateTime.Now;

                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }

                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

                Sheet sheet = workbook.GetSheetAt(0);

                IEnumerator rows = sheet.GetRowEnumerator();

                Row dateRow = sheet.GetRow(0);

                ImportHelper.JumpRows(rows, 2);

                var purchasePlanDetList = new List<PurchasePlanDet>();
                var existsDets = this.genericMgr.FindAllWithCustomQuery<PurchasePlanDet>(" select s from PurchasePlanDet as s where s.PurchasePlanId=? ", purchasePlanMstr.Id);

                int colFlow = 1;//路线
                int colItemCode = 2;//物料号

                List<string> errorMessages = new List<string>();


                while (rows.MoveNext())
                {
                    string flowCode = null;
                    string itemCode = null;

                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 0, 3))
                    {
                        break;//边界
                    }
                    string rowIndex = (row.RowNum + 1).ToString();

                    #region 读取路线代码
                    flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlow));
                    if (string.IsNullOrEmpty(flowCode))
                    {
                        errorMessages.Add(string.Format("路线不能为空,第{0}行", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取物料代码
                    try
                    {
                        itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                        if (itemCode == null)
                        {
                            errorMessages.Add(string.Format("物料不能为空,第{0}行", rowIndex));
                            continue;
                        }
                    }
                    catch
                    {
                        errorMessages.Add(string.Format("读取物料时出错,第{0}行.", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取数量
                    try
                    {
                        int i = 10;
                        Random r = new Random();
                        int t = r.Next(0, 300);
                        int tt = r.Next(500, 1000);
                        while (true)
                        {
                            if (i > 66)
                            {
                                break;
                            }
                            Cell dateCell = dateRow.GetCell(i);
                            string dateIndex = null;

                            #region 读取计划日期
                            if (dateCell != null)
                            {
                                DateTime currentDateTime = DateTime.Today;
                                if (dateCell.CellType == CellType.STRING)
                                {
                                    dateIndex = dateCell.StringCellValue;
                                }

                                //if (currentDateTime.CompareTo(startDate) < 0)
                                //{
                                //    continue;
                                //}
                                //if (currentDateTime.CompareTo(endDate) > 0)
                                //{
                                //    break;
                                //}
                            }
                            else
                            {
                                break;
                            }
                            #endregion

                            decimal qty = 0;
                            if (row.GetCell(i + 2) != null)
                            {
                                if (row.GetCell(i + 2).CellType == CellType.NUMERIC)
                                {
                                    qty = Convert.ToDecimal(row.GetCell(i + 2).NumericCellValue);
                                }
                                else
                                {
                                    string qtyValue = ImportHelper.GetCellStringValue(row.GetCell(i + 2));
                                    if (qtyValue != null)
                                    {
                                        if (!decimal.TryParse(qtyValue, out qty))
                                        {
                                            errorMessages.Add(string.Format("数量格式不正确,第{0}行", rowIndex));
                                            i += 4;
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                i += 4;
                                continue;
                            }

                            if (qty < 0)
                            {
                                errorMessages.Add(string.Format("数量不能小于0,第{0}行", rowIndex));
                                i += 4;
                                continue;
                            }
                            else
                            {
                                var existsDet = existsDets.Where(e => e.Flow == flowCode && e.Item == itemCode && e.StartTime.ToString("yyyy-MM-dd") == dateIndex);
                                if (existsDet != null && existsDet.Count() > 0)
                                {
                                    var first = existsDet.First();
                                    if (first.PurchaseQty != qty)
                                    {
                                        first.PurchaseQty = qty;
                                        first.LastModifyDate = nowTime;
                                        first.LastModifyUser = user.Code;
                                        purchasePlanDetList.Add(first);
                                    }
                                }
                                else
                                {
                                    if (qty == 0)
                                    {
                                        i += 4;
                                        continue;
                                    }
                                    var flowDets = this.genericMgr.GetDatasetBySql(string.Format(@"select d.Flow,d.Item,d.RefItemCode,d.UC,d.UOM,i.Desc1,isnull(i.MinLotSize,0) from flowdet as d inner join Item as i on d.Item=i.Code where d.Flow='{0}' and d.Item='{1}' ", flowCode, itemCode)).Tables[0];          //m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1
                                    var getFlowDetList = new List<object[]>();
                                    foreach (System.Data.DataRow readRow in flowDets.Rows)
                                    {
                                        //d.Flow,d.Item,d.RefItemCode,d.UC,d.UOM,i.Desc1,i.MinLotSize 
                                        getFlowDetList.Add(new object[] { readRow[0].ToString(), readRow[1].ToString(), readRow[2].ToString(), Convert.ToDecimal(readRow[3].ToString()), readRow[4].ToString(), readRow[5].ToString(), Convert.ToDecimal(readRow[6].ToString()) });
                                    }
                                    if (getFlowDetList.Count == 0)
                                    {
                                        errorMessages.Add(string.Format("第{0}行:路线{1}中没有维护物料{2}", rowIndex, flowCode, itemCode));
                                        i += 4;
                                        continue;
                                    }
                                    //Id, PurchasePlanId, Type, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom,
                                    //UnitQty, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                                    PurchasePlanDet nDet = new PurchasePlanDet();
                                    nDet.Id = 0;
                                    nDet.PurchasePlanId = purchasePlanMstr.Id;
                                    nDet.Type = "Daily";
                                    nDet.UUID = System.DateTime.Now.ToString() + t++ + tt++;
                                    nDet.Flow = getFlowDetList[0][0].ToString();
                                    nDet.Item = getFlowDetList[0][1].ToString();
                                    nDet.ItemDesc = getFlowDetList[0][5].ToString();
                                    nDet.RefItemCode = getFlowDetList[0][2].ToString();
                                    nDet.ReqQty = 0;
                                    nDet.OrgPurchaseQty = 0;
                                    nDet.PurchaseQty = qty;
                                    nDet.OrderQty = 0;
                                    nDet.Uom = getFlowDetList[0][4].ToString();
                                    nDet.BaseUom = getFlowDetList[0][4].ToString();
                                    nDet.UnitQty = 1;
                                    nDet.UnitCount = Convert.ToDecimal(getFlowDetList[0][3]);
                                    nDet.MinLotSize = Convert.ToDecimal(getFlowDetList[0][6]);
                                    nDet.StartTime = Convert.ToDateTime(dateIndex);
                                    nDet.WindowTime = Convert.ToDateTime(dateIndex);
                                    nDet.CreateDate = nowTime;
                                    nDet.CreateUser = user.Code;
                                    nDet.LastModifyDate = nowTime;
                                    nDet.LastModifyUser = user.Code;
                                    nDet.Version = 1;
                                    purchasePlanDetList.Add(nDet);
                                }
                            }
                            i += 4;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(ex.Message);
                    }
                    #endregion
                }

                if (errorMessages.Count > 0)
                {
                    string errorMes = string.Empty;
                    foreach (var error in errorMessages)
                    {
                        errorMes += error + "-";
                    }
                    throw new BusinessErrorException(errorMes);
                }

                foreach (var shipPlan in purchasePlanDetList)
                {
                    if (shipPlan.Id > 0)
                    {
                        this.genericMgr.Update(shipPlan);
                    }
                    else
                    {
                        this.genericMgr.Create(shipPlan);
                    }
                }
            }
        }
        #endregion

        #region  采购计划导入2
        private static object readPurchasePlanFromXlsLock2 = new object();
        [Transaction(TransactionMode.Requires)]
        public void ReadPurchasePlanFromXls2(Stream inputStream, User user, PurchasePlanMstr2 purchasePlanMstr)
        {
            lock (readPurchasePlanFromXlsLock2)
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = startDate.AddDays(13);
                DateTime nowTime = System.DateTime.Now;

                if (inputStream.Length == 0)
                {
                    throw new BusinessErrorException("Import.Stream.Empty");
                }

                HSSFWorkbook workbook = new HSSFWorkbook(inputStream);

                Sheet sheet = workbook.GetSheetAt(0);

                IEnumerator rows = sheet.GetRowEnumerator();

                Row dateRow = sheet.GetRow(0);

                ImportHelper.JumpRows(rows, 2);

                var purchasePlanDetList = new List<PurchasePlanDet2>();
                var existsDets = this.genericMgr.FindAllWithCustomQuery<PurchasePlanDet2>(" select s from PurchasePlanDet2 as s where s.PurchasePlanId=? ", purchasePlanMstr.Id);

                int colFlow = 1;//路线
                int colItemCode = 2;//物料号

                List<string> errorMessages = new List<string>();


                while (rows.MoveNext())
                {
                    string flowCode = null;
                    string itemCode = null;

                    HSSFRow row = (HSSFRow)rows.Current;
                    if (!ImportHelper.CheckValidDataRow(row, 0, 3))
                    {
                        break;//边界
                    }
                    string rowIndex = (row.RowNum + 1).ToString();

                    #region 读取路线代码
                    flowCode = ImportHelper.GetCellStringValue(row.GetCell(colFlow));
                    if (string.IsNullOrEmpty(flowCode))
                    {
                        errorMessages.Add(string.Format("路线不能为空,第{0}行", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取物料代码
                    try
                    {
                        itemCode = ImportHelper.GetCellStringValue(row.GetCell(colItemCode));
                        if (itemCode == null)
                        {
                            errorMessages.Add(string.Format("物料不能为空,第{0}行", rowIndex));
                            continue;
                        }
                    }
                    catch
                    {
                        errorMessages.Add(string.Format("读取物料时出错,第{0}行.", rowIndex));
                        continue;
                    }
                    #endregion

                    #region 读取数量
                    try
                    {
                        int i = 11;
                        Random r = new Random();
                        int t = r.Next(0, 300);
                        int tt = r.Next(500, 1000);
                        while (true)
                        {
                            if (i > 80)
                            {
                                break;
                            }
                            Cell dateCell = dateRow.GetCell(i);
                            string dateIndex = null;

                            #region 读取计划日期
                            if (dateCell != null)
                            {
                                DateTime currentDateTime = DateTime.Today;
                                if (dateCell.CellType == CellType.STRING)
                                {
                                    dateIndex = dateCell.StringCellValue;
                                }

                                //if (currentDateTime.CompareTo(startDate) < 0)
                                //{
                                //    continue;
                                //}
                                //if (currentDateTime.CompareTo(endDate) > 0)
                                //{
                                //    break;
                                //}
                            }
                            else
                            {
                                break;
                            }
                            #endregion

                            decimal qty = 0;
                            if (row.GetCell(i + 2) != null)
                            {
                                if (row.GetCell(i + 2).CellType == CellType.NUMERIC)
                                {
                                    qty = Convert.ToDecimal(row.GetCell(i + 2).NumericCellValue);
                                }
                                else
                                {
                                    string qtyValue = ImportHelper.GetCellStringValue(row.GetCell(i + 2));
                                    if (qtyValue != null)
                                    {
                                        if (!decimal.TryParse(qtyValue, out qty))
                                        {
                                            errorMessages.Add(string.Format("数量格式不正确,第{0}行", rowIndex));
                                            i += 5;
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                i += 5;
                                continue;
                            }

                            if (qty < 0)
                            {
                                errorMessages.Add(string.Format("数量不能小于0,第{0}行", rowIndex));
                                i += 5;
                                continue;
                            }
                            else
                            {
                                var existsDet = existsDets.Where(e => e.Flow == flowCode && e.Item == itemCode && e.StartTime.ToString("yyyy-MM-dd") == dateIndex);
                                if (existsDet != null && existsDet.Count() > 0)
                                {
                                    var first = existsDet.First();
                                    if (first.PurchaseQty != qty)
                                    {
                                        first.PurchaseQty = qty;
                                        first.LastModifyDate = nowTime;
                                        first.LastModifyUser = user.Code;
                                        purchasePlanDetList.Add(first);
                                    }
                                }
                                else
                                {
                                    if (qty == 0)
                                    {
                                        i += 5;
                                        continue;
                                    }
                                    var flowDets = this.genericMgr.GetDatasetBySql(string.Format(@"select d.Flow,d.Item,d.RefItemCode,d.UC,d.UOM,i.Desc1,isnull(i.MinLotSize,0) from flowdet as d inner join Item as i on d.Item=i.Code where d.Flow='{0}' and d.Item='{1}' ", flowCode, itemCode)).Tables[0];          //m.Code,m.LocFrom,d.Item,d.RefItemCode,d.UC,d.Uom,i.Desc1
                                    var getFlowDetList = new List<object[]>();
                                    foreach (System.Data.DataRow readRow in flowDets.Rows)
                                    {
                                        //d.Flow,d.Item,d.RefItemCode,d.UC,d.UOM,i.Desc1,i.MinLotSize 
                                        getFlowDetList.Add(new object[] { readRow[0].ToString(), readRow[1].ToString(), readRow[2].ToString(), Convert.ToDecimal(readRow[3].ToString()), readRow[4].ToString(), readRow[5].ToString(), Convert.ToDecimal(readRow[6].ToString()) });
                                    }
                                    if (getFlowDetList.Count == 0)
                                    {
                                        errorMessages.Add(string.Format("第{0}行:路线{1}中没有维护物料{2}", rowIndex, flowCode, itemCode));
                                        i += 5;
                                        continue;
                                    }
                                    //Id, PurchasePlanId, Type, UUID, Flow, Item, ItemDesc, RefItemCode, ReqQty, OrgPurchaseQty, PurchaseQty, OrderQty, Uom, BaseUom,
                                    //UnitQty, UC, MinLotSize, StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
                                    PurchasePlanDet2 nDet = new PurchasePlanDet2();
                                    nDet.Id = 0;
                                    nDet.PurchasePlanId = purchasePlanMstr.Id;
                                    nDet.Type = "Daily";
                                    nDet.UUID = System.DateTime.Now.ToString() + t++ + tt++;
                                    nDet.Flow = getFlowDetList[0][0].ToString();
                                    nDet.Item = getFlowDetList[0][1].ToString();
                                    nDet.ItemDesc = getFlowDetList[0][5].ToString();
                                    nDet.RefItemCode = getFlowDetList[0][2].ToString();
                                    nDet.ReqQty = 0;
                                    nDet.OrgPurchaseQty = 0;
                                    nDet.PurchaseQty = qty;
                                    nDet.OrderQty = 0;
                                    nDet.Uom = getFlowDetList[0][4].ToString();
                                    nDet.BaseUom = getFlowDetList[0][4].ToString();
                                    nDet.UnitQty = 1;
                                    nDet.UnitCount = Convert.ToDecimal(getFlowDetList[0][3]);
                                    nDet.MinLotSize = Convert.ToDecimal(getFlowDetList[0][6]);
                                    nDet.StartTime = Convert.ToDateTime(dateIndex);
                                    nDet.WindowTime = Convert.ToDateTime(dateIndex);
                                    nDet.CreateDate = nowTime;
                                    nDet.CreateUser = user.Code;
                                    nDet.LastModifyDate = nowTime;
                                    nDet.LastModifyUser = user.Code;
                                    nDet.Version = 1;
                                    purchasePlanDetList.Add(nDet);
                                }
                            }
                            i += 5;
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add(ex.Message);
                    }
                    #endregion
                }

                if (errorMessages.Count > 0)
                {
                    string errorMes = string.Empty;
                    foreach (var error in errorMessages)
                    {
                        errorMes += error + "-";
                    }
                    throw new BusinessErrorException(errorMes);
                }

                foreach (var shipPlan in purchasePlanDetList)
                {
                    if (shipPlan.Id > 0)
                    {
                        this.genericMgr.Update(shipPlan);
                    }
                    else
                    {
                        this.genericMgr.Create(shipPlan);
                    }
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdatePurchaseDets(List<PurchasePlanDet2>  purchasePlanDetList)
        {
            foreach (var pPlan in purchasePlanDetList)
            {
                if (pPlan.Id > 0)
                {
                    this.genericMgr.Update(pPlan);
                }
                else
                {
                    this.genericMgr.Create(pPlan);
                }
            }
        }
        #endregion



        #region  发运计划转订单
        [Transaction(TransactionMode.Requires)]
        public void CreateOrderByShipPlan(string ids, User user)
        {
            //string[] idArr = ids.Split(',');
            string sql = string.Format(" select e from ShipPlanDet as e  where Id in ({0}) ", ids);
            IList<ShipPlanDet> getDets = this.genericMgr.FindAllWithCustomQuery<ShipPlanDet>(sql);
            DateTime nowTime = System.DateTime.Now;
            if (getDets != null && getDets.Count > 0)
            {
                var groups = getDets.GroupBy(g => new { g.Flow, g.WindowTime, g.StartTime });
                IList<OrderHead> orderHeads = new List<OrderHead>();
                foreach (var g in groups)
                {
                    Flow currentFlow = this.flowMgr.LoadFlow(g.Key.Flow);
                    IList<FlowDetail> flowDets = new List<FlowDetail>();
                    foreach (var fd in currentFlow.FlowDetails)
                    {
                        foreach (var l in g)
                        {
                            if (fd.Item.Code == l.Item && l.ShipQty > 0)
                            {
                                flowDets.Add(fd);
                            }
                        }
                    }
                    currentFlow.FlowDetails = flowDets;
                    OrderHead orderHead = this.orderMgr.TransferFlow2Order(currentFlow);
                    orderHead.WindowTime = g.Key.WindowTime;
                    orderHead.StartTime = g.Key.StartTime;
                    orderHead.SubType = "Nml";
                    orderHead.Priority = "Normal";
                    orderHead.IsAutoRelease = true;
                    foreach (var od in orderHead.OrderDetails)
                    {
                        foreach (var l in g)
                        {
                            if (od.Item.Code == l.Item)
                            {
                                od.RequiredQty = l.ShipQty;
                                od.OrderedQty = l.ShipQty;
                                l.OrderQty += l.ShipQty;
                                l.ShipQty = 0;
                            }
                        }
                    }
                    this.orderMgr.CreateOrder(orderHead, user);
                    orderHeads.Add(orderHead);
                    foreach (var od in orderHead.OrderDetails)
                    {
                        foreach (var l in g)
                        {
                            if (od.Item.Code == l.Item)
                            {
                                ShipPlanOpenOrder nOpenOrder = new ShipPlanOpenOrder();
                                nOpenOrder.ShipPlanId = l.ShipPlanId;
                                nOpenOrder.Type = l.Type;
                                nOpenOrder.UUID = l.UUID;
                                nOpenOrder.Flow = l.Flow;
                                nOpenOrder.OrderNo = od.OrderHead.OrderNo;
                                nOpenOrder.Item = l.Item;
                                nOpenOrder.OrgStartTime = l.StartTime;
                                nOpenOrder.OrgWindowTime = l.WindowTime;
                                nOpenOrder.StartTime = l.StartTime;
                                nOpenOrder.WindowTime = l.WindowTime;
                                nOpenOrder.OrderQty = od.OrderedQty;
                                nOpenOrder.ShipQty = od.ShippedQty.HasValue ? od.ShippedQty.Value : 0;
                                nOpenOrder.RecQty = od.ReceivedQty.HasValue ? od.ReceivedQty.Value : 0;
                                nOpenOrder.CreateDate = nowTime;
                                nOpenOrder.CreateUser = user.Code;
                                nOpenOrder.IsTransferOrder = true;
                                this.genericMgr.Update(l);
                                this.genericMgr.Create(nOpenOrder);
                            }
                        }
                    }
                }

                //foreach (var odHead in orderHeads)
                //{
                //    this.orderMgr.CreateOrder(odHead, user);
                //}
            }
        }
        #endregion
                                                          
        //#region  生产计划转订单
        //[Transaction(TransactionMode.Requires)]
        //public void CreateOrderByProductionPlan(string ids, User user)
        //{
        //    //string[] idArr = ids.Split(',');
        //    string sql = string.Format(" select e from ProductionPlanDet as e  where Id in ({0}) ", ids);
        //    IList<ProductionPlanDet> getDets = this.genericMgr.FindAllWithCustomQuery<ProductionPlanDet>(sql);
        //    DateTime nowTime = System.DateTime.Now;
        //    if (getDets != null && getDets.Count > 0)
        //    {
        //        var groups = getDets.GroupBy(g => new { g.Flow, g.WindowTime, g.StartTime });
        //        IList<OrderHead> orderHeads = new List<OrderHead>();
        //        foreach (var g in groups)
        //        {
        //            Flow currentFlow = this.flowMgr.LoadFlow(g.Key.Flow);
        //            IList<FlowDetail> flowDets = new List<FlowDetail>();
        //            foreach (var fd in currentFlow.FlowDetails)
        //            {
        //                foreach (var l in g)
        //                {
        //                    if (fd.Item.Code == l.Item && l.ShipQty > 0)
        //                    {
        //                        flowDets.Add(fd);
        //                    }
        //                }
        //            }
        //            currentFlow.FlowDetails = flowDets;
        //            OrderHead orderHead = this.orderMgr.TransferFlow2Order(currentFlow);
        //            orderHead.WindowTime = g.Key.WindowTime;
        //            orderHead.StartTime = g.Key.StartTime;
        //            orderHead.SubType = "Nml";
        //            orderHead.Priority = "Normal";
        //            orderHead.IsAutoRelease = true;
        //            foreach (var od in orderHead.OrderDetails)
        //            {
        //                foreach (var l in g)
        //                {
        //                    if (od.Item.Code == l.Item)
        //                    {
        //                        od.RequiredQty = l.ShipQty;
        //                        od.OrderedQty = l.ShipQty;
        //                        l.OrderQty += l.ShipQty;
        //                        l.ShipQty = 0;
        //                    }
        //                }
        //            }
        //            this.orderMgr.CreateOrder(orderHead, user);
        //            orderHeads.Add(orderHead);
        //            foreach (var od in orderHead.OrderDetails)
        //            {
        //                foreach (var l in g)
        //                {
        //                    if (od.Item.Code == l.Item)
        //                    {
        //                        ShipPlanOpenOrder nOpenOrder = new ShipPlanOpenOrder();
        //                        nOpenOrder.ShipPlanId = l.ShipPlanId;
        //                        nOpenOrder.Type = l.Type;
        //                        nOpenOrder.UUID = l.UUID;
        //                        nOpenOrder.Flow = l.Flow;
        //                        nOpenOrder.OrderNo = od.OrderHead.OrderNo;
        //                        nOpenOrder.Item = l.Item;
        //                        nOpenOrder.OrgStartTime = l.StartTime;
        //                        nOpenOrder.OrgWindowTime = l.WindowTime;
        //                        nOpenOrder.StartTime = l.StartTime;
        //                        nOpenOrder.WindowTime = l.WindowTime;
        //                        nOpenOrder.OrderQty = od.OrderedQty;
        //                        nOpenOrder.ShipQty = od.ShippedQty.HasValue ? od.ShippedQty.Value : 0;
        //                        nOpenOrder.RecQty = od.ReceivedQty.HasValue ? od.ReceivedQty.Value : 0;
        //                        nOpenOrder.CreateDate = nowTime;
        //                        nOpenOrder.CreateUser = user.Code;
        //                        nOpenOrder.IsTransferOrder = true;
        //                        this.genericMgr.Update(l);
        //                        this.genericMgr.Create(nOpenOrder);
        //                    }
        //                }
        //            }
        //        }

        //        //foreach (var odHead in orderHeads)
        //        //{
        //        //    this.orderMgr.CreateOrder(odHead, user);
        //        //}
        //    }
        //}
        //#endregion

        #region  班产计划转订单
        [Transaction(TransactionMode.Requires)]
        public IList<OrderHead> CreateOrderByShiftPlan(string ids, User user, int leadTime)
        {
            //string[] idArr = ids.Split(',');
            string sql = string.Format(" select e from ShiftPlanDet as e  where Id in ({0}) ", ids);
            IList<ShiftPlanDet> getDets = this.genericMgr.FindAllWithCustomQuery<ShiftPlanDet>(sql);
            DateTime nowTime = System.DateTime.Now;
            if (getDets == null || getDets.Count == 0)
            {
                throw new BusinessErrorException("请选择需要转订单的明细。");
            }
            List<FlowDetail> tempFlowdets = new List<FlowDetail>();
            //string searchExistsSql = string.Format("select d.orderNo,d.Item,d.orderqty,isnull(d.recqty,0) as recqty from orderdet as d where exists(select 1 from ordermstr as m where m.orderno=d.orderno and m.type='Production' and m.Status in('In-Process') ) and d.orderqty>d.recqty and d.Item in('{0}') ",string.Join("','", shiftPlanList.Select(s=>s.Item).Distinct().ToArray()));
            //var existsDetRows = TheGenericMgr.GetDatasetBySql(searchExistsSql).Tables[0];
            //var existsDetList = new List<TempOrderDet>();
            //foreach (System.Data.DataRow row in existsDetRows.Rows)
            //{
            //    existsDetList.Add(new TempOrderDet{
            //        OrderNo = row[0].ToString(),
            //        ItemCode = row[1].ToString(),
            //        OrderedQty = Convert.ToDecimal(row[2].ToString()),
            //        ReceivedQty = Convert.ToDecimal(row[3].ToString()),
            //    });
            //}
            IList<OrderHead> orderHeadList = new List<OrderHead>();
            foreach (ShiftPlanDet sps in getDets)
            {
                var currentFlowDet = this.genericMgr.GetDatasetBySql(string.Format("select  Id,orderlotsize,item  from flowdet where flow='{0}' and item='{1}' ", sps.ProdLine, sps.Item)).Tables[0];
                int flowDetId = 0;
                decimal orderLotSize = 0;
                foreach (System.Data.DataRow row in currentFlowDet.Rows)
                {
                    flowDetId = Convert.ToInt32((object)row[0]);
                    orderLotSize = !string.IsNullOrEmpty(row[1].ToString()) ? (decimal)Convert.ToDecimal(row[1].ToString()) : 0;
                }
                IList<decimal> reqQtyList = OrderHelper.SplitByOrderLotSize((decimal)sps.Qty, orderLotSize);
                DateTime startTime = this.shiftMgr.GetShiftStartTime(sps.PlanDate, sps.Shift);
                DateTime windowTime = startTime.AddHours(Convert.ToDouble(leadTime));

                //int i = 0;
                foreach (decimal reqQty in reqQtyList)
                {
                    OrderHead oh = new OrderHead();
                    Flow currentFlow = this.flowMgr.LoadFlow(sps.ProdLine, user.Code, true);
                    if (tempFlowdets.Where(d => d.Flow.Code == currentFlow.Code).Count() > 0)
                    {
                        currentFlow.FlowDetails = tempFlowdets.Where(d => d.Flow.Code == currentFlow.Code).ToList();
                    }
                    else
                    {
                        tempFlowdets.AddRange(currentFlow.FlowDetails);
                    }
                    currentFlow.FlowDetails = currentFlow.FlowDetails.Where(d => d.Id == flowDetId).ToList();
                    oh = this.orderMgr.TransferFlow2Order(currentFlow, BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML, false, DateTime.Now);
                    oh.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;

                    oh.StartTime = startTime;
                    oh.WindowTime = windowTime;

                    oh.GetOrderDetailByFlowDetailIdAndItemCode(flowDetId, sps.Item).RequiredQty = reqQty;
                    oh.GetOrderDetailByFlowDetailIdAndItemCode(flowDetId, sps.Item).OrderedQty = reqQty;
                    oh.Shift = this.shiftMgr.LoadShift(sps.Shift);

                    //if (i == 0)
                    //{
                    //    oh.ExistsProdDetails = existsDetList.Where(e => e.ItemCode == sps.Item).ToList();
                    //} 
                    //i++;


                    orderHeadList.Add(oh);

                    startTime = windowTime;
                    windowTime = startTime.AddHours(Convert.ToDouble(leadTime));
                }
            }
            return orderHeadList;
        }
        #endregion

        #region  采购计划发货
        [Transaction(TransactionMode.Requires)]
        public void CreateOrderByPurchasePlan(string ids, User user)
        {
            //string[] idArr = ids.Split(',');
            string sql = string.Format(" select e from PurchasePlanDet2 as e  where Id in ({0}) ", ids);
            IList<PurchasePlanDet2> getDets = this.genericMgr.FindAllWithCustomQuery<PurchasePlanDet2>(sql);
            DateTime nowTime = System.DateTime.Now;
            if (getDets != null && getDets.Count > 0)
            {
                var groups = getDets.GroupBy(g => new { g.Flow, g.StartTime });
                IList<OrderHead> orderHeads = new List<OrderHead>();
                foreach (var g in groups)
                {
                    Flow currentFlow = this.flowMgr.LoadFlow(g.Key.Flow);
                    IList<FlowDetail> flowDets = new List<FlowDetail>();
                    foreach (var fd in currentFlow.FlowDetails)
                    {
                        foreach (var l in g)
                        {
                            if (fd.Item.Code == l.Item && l.PurchaseQty > 0)
                            {
                                flowDets.Add(fd);
                            }
                        }
                    }
                    currentFlow.FlowDetails = flowDets;
                    var mepLeadTime = currentFlow.MrpLeadTime.HasValue ? Convert.ToInt32(currentFlow.MrpLeadTime) : 0;
                    OrderHead orderHead = this.orderMgr.TransferFlow2Order(currentFlow);
                    orderHead.WindowTime = g.Key.StartTime.AddDays(mepLeadTime);
                    orderHead.StartTime = g.Key.StartTime;
                    orderHead.SubType = "Nml";
                    orderHead.Priority = "Normal";
                    orderHead.IsAutoRelease = true;
                    foreach (var od in orderHead.OrderDetails)
                    {
                        foreach (var l in g)
                        {
                            if (od.Item.Code == l.Item)
                            {
                                od.RequiredQty = l.PurchaseQty;
                                od.OrderedQty = l.PurchaseQty;
                                l.OrderQty += l.PurchaseQty;
                                l.PurchaseQty = 0;
                            }
                        }
                    }
                    this.orderMgr.CreateOrder(orderHead, user);
                    orderHeads.Add(orderHead);
                    foreach (var od in orderHead.OrderDetails)
                    {
                        foreach (var l in g)
                        {
                            if (od.Item.Code == l.Item)
                            {
                                PurchasePlanOpenOrder2 nOpenOrder = new PurchasePlanOpenOrder2();
                                nOpenOrder.PurchasePlanId = l.PurchasePlanId;
                                nOpenOrder.Type = l.Type;
                                nOpenOrder.UUID = l.UUID;
                                nOpenOrder.Flow = l.Flow;
                                nOpenOrder.OrderNo = od.OrderHead.OrderNo;
                                nOpenOrder.Item = l.Item;
                                nOpenOrder.StartTime = l.StartTime;
                                nOpenOrder.WindowTime = l.WindowTime;
                                nOpenOrder.OrderQty = od.OrderedQty;
                                nOpenOrder.ShipQty = od.ShippedQty.HasValue ? od.ShippedQty.Value : 0;
                                nOpenOrder.RecQty = od.ReceivedQty.HasValue ? od.ReceivedQty.Value : 0;
                                nOpenOrder.CreateDate = nowTime;
                                nOpenOrder.CreateUser = user.Code;
                                //nOpenOrder.IsTransferOrder = true;
                                this.genericMgr.Update(l);
                                this.genericMgr.Create(nOpenOrder);
                            }
                        }
                    }
                }

                //foreach (var odHead in orderHeads)
                //{
                //    this.orderMgr.CreateOrder(odHead, user);
                //}
            }
        }
        #endregion

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

        private IList<MrpShipPlan> TransferSalesOrder2ShipPlan(IList<OrderDetail> salesOrderDetailList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            IList<MrpShipPlan> mrpShipPlanList = new List<MrpShipPlan>();

            if (salesOrderDetailList != null && salesOrderDetailList.Count > 0)
            {
                foreach (OrderDetail salesOrderDetail in salesOrderDetailList)
                {
                    OrderHead orderHead = salesOrderDetail.OrderHead;
                    MrpShipPlan mrpShipPlan = new MrpShipPlan();

                    if (salesOrderDetail.OrderHead.StartTime < effectiveDate)
                    {
                        mrpShipPlan.IsExpire = true;
                        mrpShipPlan.ExpireStartTime = salesOrderDetail.OrderHead.StartTime;
                    }
                    else
                    {
                        mrpShipPlan.IsExpire = false;
                    }
                    mrpShipPlan.Flow = salesOrderDetail.OrderHead.Flow;
                    mrpShipPlan.FlowType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
                    mrpShipPlan.Item = salesOrderDetail.Item.Code;
                    mrpShipPlan.ItemDescription = salesOrderDetail.Item.Description;
                    if (mrpShipPlan.IsExpire)
                    {
                        mrpShipPlan.StartTime = DateTime.Now;
                    }
                    else
                    {
                        mrpShipPlan.StartTime = salesOrderDetail.OrderHead.StartTime;
                    }
                    if (salesOrderDetail.OrderHead.WindowTime < effectiveDate)
                    {
                        mrpShipPlan.WindowTime = DateTime.Now;
                    }
                    else
                    {
                        mrpShipPlan.WindowTime = salesOrderDetail.OrderHead.WindowTime;
                    }
                    mrpShipPlan.LocationFrom = salesOrderDetail.DefaultLocationFrom.Code;
                    mrpShipPlan.SourceType = BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_ORDER;
                    mrpShipPlan.SourceDateType = BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY;
                    mrpShipPlan.SourceId = salesOrderDetail.Id.ToString();
                    mrpShipPlan.SourceUnitQty = 1;
                    mrpShipPlan.EffectiveDate = effectiveDate;
                    mrpShipPlan.Qty = (salesOrderDetail.OrderedQty - (salesOrderDetail.ShippedQty.HasValue ? salesOrderDetail.ShippedQty.Value : 0)); ;
                    mrpShipPlan.Uom = salesOrderDetail.Uom.Code;
                    mrpShipPlan.BaseUom = salesOrderDetail.Item.Uom.Code;
                    mrpShipPlan.UnitCount = salesOrderDetail.UnitCount;
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
                    Item item = this.itemMgr.LoadItem(customerScheduleDetail.Item);
                    MrpShipPlan mrpShipPlan = new MrpShipPlan();

                    mrpShipPlan.Flow = customerScheduleDetail.CustomerSchedule.Flow;
                    mrpShipPlan.FlowType = BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION;
                    mrpShipPlan.Item = customerScheduleDetail.Item;
                    mrpShipPlan.ItemDescription = customerScheduleDetail.ItemDescription;
                    mrpShipPlan.ItemReference = customerScheduleDetail.ItemReference;
                    mrpShipPlan.StartTime = customerScheduleDetail.StartTime;
                    mrpShipPlan.WindowTime = customerScheduleDetail.DateFrom;
                    mrpShipPlan.LocationFrom = customerScheduleDetail.Location;
                    mrpShipPlan.SourceType = BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_CUSTOMER_PLAN;
                    mrpShipPlan.SourceDateType = customerScheduleDetail.Type;
                    mrpShipPlan.SourceId = customerScheduleDetail.Id.ToString();
                    mrpShipPlan.SourceUnitQty = 1;
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

        private IList<MrpShipPlan> TransferSalesOrderAndCustomerPlan2ShipPlan(IList<OrderDetail> salesOrderDetailList, IList<CustomerScheduleDetail> customerScheduleDetaillList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            IList<MrpShipPlan> mrpShipPlanList = TransferSalesOrder2ShipPlan(salesOrderDetailList, effectiveDate, dateTimeNow, user);

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

        private void NestCalculateMrpShipPlanAndReceivePlan(int batchNo, DateTime effDate, MrpShipPlan mrpShipPlan, IList<MrpLocationLotDetail> inventoryBalanceList, IList<TransitInventory> transitInventoryList, IList<FlowDetailSnapShot> flowDetailSnapShotList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            if (mrpShipPlan.LocationFrom != null && mrpShipPlan.LocationFrom.Trim() != string.Empty)
            {
                #region 消耗本机物料
                if (mrpShipPlan.Qty == 0)
                {
                    return;
                }
                else if (mrpShipPlan.Qty < 0)
                {
                    InsertErrRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, "MrpShipPlan的数量不能小于零");
                    return;
                }

                //回冲库存
                BackFlushInventory(batchNo, effDate, mrpShipPlan, inventoryBalanceList, user);

                //回冲在途
                BackFlushTransitInventory(batchNo, effDate, mrpShipPlan, transitInventoryList, user);
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
                    mrpReceivePlan.RefFlows = mrpShipPlan.RefFlows;
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
                    mrpReceivePlan.EffectiveDate = effectiveDate;
                    mrpReceivePlan.CreateDate = dateTimeNow;
                    mrpReceivePlan.CreateUser = user.Code;
                    if (!mrpReceivePlan.TryAddRefFlow(mrpShipPlan.Flow))
                    {
                        InsertErrRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, "路线出现循环【" + mrpReceivePlan.RefFlows + "】");
                    }
                    else
                    {
                        InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpReceivePlan, "生成毛需求");
                        currMrpReceivePlanList.Add(mrpReceivePlan);
                    }
                    #endregion
                }
                else
                {
                    #region 生产，需要分解Bom
                    InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, "开始分解Bom");
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
                            mrpReceivePlan.EffectiveDate = effectiveDate;
                            mrpReceivePlan.CreateDate = dateTimeNow;
                            mrpReceivePlan.CreateUser = user.Code;

                            //this.mrpReceivePlanMgr.CreateMrpReceivePlan(mrpReceivePlan);
                            currMrpReceivePlanList.Add(mrpReceivePlan);
                            #endregion
                        }

                        InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, "分解Bom完成");
                    }
                    else
                    {
                        InsertErrRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, "没有找到到Bom【" + mrpShipPlan.Bom + "】");
                    }
                    #endregion
                }
                #endregion

                #region 计算下游发运计划
                foreach (MrpReceivePlan mrpReceivePlan in currMrpReceivePlanList)
                {
                    CalculateNextShipPlan(batchNo, effectiveDate, mrpReceivePlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                }
                #endregion
            }
        }

        private void CalculateNextShipPlan(int batchNo, DateTime effDate, MrpReceivePlan mrpReceivePlan, IList<MrpLocationLotDetail> inventoryBalanceList, IList<TransitInventory> transitInventoryList, IList<FlowDetailSnapShot> flowDetailSnapShotList, DateTime effectiveDate, DateTime dateTimeNow, User user)
        {
            InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpReceivePlan, "开始分解毛需求，查找下游路线");
            if (mrpReceivePlan.ReceiveTime < effectiveDate)
            {
                //如果窗口时间小于effectivedate，不往下计算
                //return;
            }

            var nextFlowDetailList = from det in flowDetailSnapShotList
                                     where det.LocationTo == mrpReceivePlan.Location
                                    && det.Item == mrpReceivePlan.Item
                                     select det;

            if (nextFlowDetailList != null && nextFlowDetailList.Count() > 0)
            {
                int mrpWeight = nextFlowDetailList.Sum(p => p.MRPWeight);
                decimal rate = mrpReceivePlan.Qty / mrpWeight;
                decimal remainQty = mrpReceivePlan.Qty;

                for (int i = 0; i < nextFlowDetailList.Count(); i++)
                {
                    FlowDetailSnapShot flowDetail = nextFlowDetailList.ElementAt(i);

                    MrpShipPlan mrpShipPlan = new MrpShipPlan();

                    mrpShipPlan.Flow = flowDetail.Flow;
                    mrpShipPlan.FlowType = flowDetail.FlowType;
                    mrpShipPlan.Item = flowDetail.Item;
                    mrpShipPlan.ItemDescription = flowDetail.ItemDescription;
                    if (mrpReceivePlan.SourceType != BusinessConstants.CODE_MASTER_MRP_SOURCE_TYPE_VALUE_SAFE_STOCK)
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
                    mrpShipPlan.RefFlows = mrpReceivePlan.RefFlows;

                    this.mrpShipPlanMgr.CreateMrpShipPlan(mrpShipPlan);

                    InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, "毛需求分解成功，找到下游路线");

                    NestCalculateMrpShipPlanAndReceivePlan(batchNo, effDate, mrpShipPlan, inventoryBalanceList, transitInventoryList, flowDetailSnapShotList, effectiveDate, dateTimeNow, user);
                }
            }
            else
            {
                InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpReceivePlan, "毛需求无法分解，没有找到下游路线");
            }
        }

        private void BackFlushInventory(int batchNo, DateTime effDate, MrpShipPlan mrpShipPlan, IList<MrpLocationLotDetail> inventoryBalanceList, User user)
        {
            #region 先消耗库存
            if (mrpShipPlan.Qty == 0)
            {
                return;
            }

            var inventoryConsumed = from inv in inventoryBalanceList
                                    where inv.Location == mrpShipPlan.LocationFrom
                                    && inv.Item == mrpShipPlan.Item && inv.Qty > inv.SafeQty
                                    select inv;

            if (inventoryConsumed != null && inventoryConsumed.Count() > 0)
            {
                foreach (MrpLocationLotDetail inventory in inventoryConsumed)
                {
                    InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, inventory);
                    if (mrpShipPlan.Qty * mrpShipPlan.UnitQty > inventory.ActiveQty)
                    {
                        mrpShipPlan.Qty -= inventory.ActiveQty / mrpShipPlan.UnitQty;
                        inventory.Qty = inventory.SafeQty;
                    }
                    else
                    {
                        inventory.Qty -= mrpShipPlan.Qty * mrpShipPlan.UnitQty;
                        mrpShipPlan.Qty = 0;

                        break;
                    }
                }
            }
            #endregion
        }

        private void BackFlushTransitInventory(int batchNo, DateTime effDate, MrpShipPlan mrpShipPlan, IList<TransitInventory> transitInventoryList, User user)
        {
            #region 再根据ShipPlan的StartTime < 在途库存的EffectiveDate消耗在途库存
            if (mrpShipPlan.Qty == 0)
            {
                return;
            }

            var transitConsumed = from trans in transitInventoryList
                                  where trans.Location == mrpShipPlan.LocationFrom
                                      && trans.Item == mrpShipPlan.Item && trans.Qty > 0
                                      && trans.EffectiveDate <= mrpShipPlan.StartTime
                                  select trans;

            if (transitConsumed != null && transitConsumed.Count() > 0)
            {
                foreach (TransitInventory inventory in transitConsumed)
                {
                    InsertInfoRunShipPlanLog(batchNo, effDate, user.Name, mrpShipPlan, inventory);

                    if (mrpShipPlan.Qty * mrpShipPlan.UnitQty > inventory.Qty)
                    {
                        mrpShipPlan.Qty -= inventory.Qty / mrpShipPlan.UnitQty;
                        inventory.Qty = 0;
                    }
                    else
                    {
                        inventory.Qty -= mrpShipPlan.Qty * mrpShipPlan.UnitQty;
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

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, string msg)
        {

        }

        private void InsertErrRunShipPlanLog(int batchNo, DateTime effDate, string userName, string msg)
        {

        }

        private void InsertErrRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpShipPlan mrpShipPlan, string msg)
        {

        }

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpShipPlan mrpShipPlan, string msg)
        {

        }

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpReceivePlan mrpReceivePlan, string msg)
        {

        }

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpShipPlan mrpShipPlan, MrpLocationLotDetail inventory)
        {

        }

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpShipPlan mrpShipPlan, TransitInventory inventory)
        {

        }

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpLocationLotDetail lackInventory)
        {

        }

        private void InsertInfoRunShipPlanLog(int batchNo, DateTime effDate, string userName, MrpLocationLotDetail lackInventory, TransitInventory inventory)
        {

        }


        #endregion

        class SafeInventory
        {
            public string Location { get; set; }
            public string Item { get; set; }
            public decimal SafeQty { get; set; }
        }

        public class TransitInventory
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

        public class MrpLocationLotDetail
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
            public string ItemDescription { get; set; }
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
