using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.Hql;
using NHibernate;
using NHibernate.Type;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using System.Collections;
using NHibernate.SqlCommand;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Mes;

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class MesMgr : IMesMgr
    {
        public ICriteriaMgr criteriaMgr;
        public IShelfMgr shelfMgr;
        public IOrderLocationTransactionMgr orderLocationTransactionMgr;
        public IOrderMgr orderMgr;
        public IHqlMgr hqlMgr;
        public IUserMgr userMgr;
        public IFlowMgr flowMgr;
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.Mes");

        public MesMgr(ICriteriaMgr criteriaMgr,
                        IShelfMgr shelfMgr,
                        IOrderLocationTransactionMgr orderLocationTransactionMgr,
                        IOrderMgr orderMgr,
                        IHqlMgr hqlMgr,
                        IUserMgr userMgr,
                        IFlowMgr flowMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.shelfMgr = shelfMgr;
            this.orderLocationTransactionMgr = orderLocationTransactionMgr;
            this.orderMgr = orderMgr;
            this.hqlMgr = hqlMgr;
            this.userMgr = userMgr;
            this.flowMgr = flowMgr;
        }



        [Transaction(TransactionMode.Requires)]
        public void RunMes()
        {
            #region 找到所有Mes的生产线,默认一个生产线对应一个线边库位
            log.Info("Flow begin.");
            DetachedCriteria fCriteria = DetachedCriteria.For<Flow>();
            fCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            fCriteria.Add(Expression.Eq("FlowStrategy", BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_MES));
            fCriteria.Add(Expression.Eq("IsActive", true));
            fCriteria.Add(Expression.Or(Expression.Le("NextWinTime", DateTime.Now), Expression.IsNull("NextWinTime")));
            IList<Flow> flowList = criteriaMgr.FindAll<Flow>(fCriteria);
            log.Info("Flow end.");
            #endregion

            #region 找到所有Mes的移库路线
            log.Info("TransferFlow begin.");
            DetachedCriteria tCriteria = DetachedCriteria.For<FlowDetail>();
            tCriteria.CreateAlias("Flow", "f");
            tCriteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER));
            tCriteria.Add(Expression.Eq("f.IsActive", true));
            tCriteria.Add(Expression.Ge("f.IsMes", true));
            IList<FlowDetail> transferFlowList = criteriaMgr.FindAll<FlowDetail>(tCriteria);
            log.Info("TransferFlow end.");
            #endregion

            #region 找到所有货架和活动工单
            log.Info("Shelf begin.");
            IList<Shelf> shelfList = shelfMgr.GetAllShelf();
            log.Info("Shelf end.");

            log.Info("ActiveOrder begin.");
            DetachedCriteria oCriteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            oCriteria.CreateAlias("OrderDetail", "od");
            oCriteria.CreateAlias("od.OrderHead", "oh");
            oCriteria.Add(Expression.In("oh.Status", new string[] { BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS, BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT }));
            oCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO));
            oCriteria.Add(Expression.Gt("Cartons", 0));
            oCriteria.Add(Expression.IsNotNull("Shelf"));
            IList<OrderLocationTransaction> orderLocTransList = criteriaMgr.FindAll<OrderLocationTransaction>(oCriteria);
            log.Info("ActiveOrder end.");

            IDictionary<string, int> rawDemandDic = new Dictionary<string, int>();
            IDictionary<string, int> nonZeroRawDemandDic = new Dictionary<string, int>();
            #endregion


            #region 找到所有库存
            log.Info("Inventory begin.");
            string hql = @"select h.Location, h.Item.Code,Count(h.HuId)
                    from Hu as h 
                    where h.Status = ? and h.IsMes = ? 
                    group by h.Location, h.Item.Code";

            IList<object[]> invList = hqlMgr.FindAll<object[]>(hql,
                new Object[] {
                    BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS, 
                    true
                });

            log.Info("Inventory end.");

            #endregion

            #region 获取实时库存和在途,只有待发,全部为一步移库无待收
            log.Info("ToShip begin.");
            hql = @"select olt.Location.Code, olt.Item.Code,olt.Cartons
                    from OrderLocationTransaction as olt 
                    join olt.OrderDetail as od
                    join od.OrderHead as oh
                    where oh.Status in (?, ?) and oh.SubType = ? and  oh.Type=? and olt.IOType = ? and oh.IsMes = ?
                    and olt.Cartons > 0";

            IList<object[]> expectTransitInvList = hqlMgr.FindAll<object[]>(hql,
                new Object[] {
                    BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT, 
                    BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS, 
                    BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER,   //只考虑移库
                    BusinessConstants.IO_TYPE_IN,
                    true
                });
            log.Info("ToShip end.");

            IList<object[]> totalInvList = new List<object[]>();
            if (invList != null)
            {
                totalInvList = totalInvList.Union(invList).ToList();
            }
            if (expectTransitInvList != null)
            {
                totalInvList = totalInvList.Union(expectTransitInvList).ToList();
            }
            #endregion

            if (flowList != null && flowList.Count > 0)
            {
                foreach (Flow flow in flowList)
                {
                    #region 对每个生产线找到下面的货架
                    IList<Shelf> flowShelfList = shelfList.Where(s => s.ProductLine.Code == flow.Code).ToList();
                    #endregion

                    #region 计算总的毛需求
                    if (flowShelfList != null && flowShelfList.Count > 0)
                    {

                        foreach (Shelf shelf in flowShelfList)
                        {
                            #region 货架找到对应的活动工单
                            IList<OrderLocationTransaction> shelfOrderLocTransList = orderLocTransList.Where(l => l.Shelf.Trim().ToUpper() == shelf.Code.Trim().ToUpper()).OrderByDescending(l => l.OrderDetail.OrderHead.StartTime).ToList();
                            if (shelfOrderLocTransList != null && shelfOrderLocTransList.Count > 0)
                            {
                                int count = shelf.Capacity;
                                foreach (OrderLocationTransaction ol in shelfOrderLocTransList)
                                {

                                    if (rawDemandDic.ContainsKey(ol.Item.Code))
                                    {
                                        rawDemandDic[ol.Item.Code] += ol.Cartons <= count ? ol.Cartons : count;
                                    }
                                    else
                                    {
                                        rawDemandDic.Add(ol.Item.Code, ol.Cartons <= count ? ol.Cartons : count);
                                    }
                                    count -= ol.Cartons <= count ? ol.Cartons : count;
                                    if (count == 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    #region 计算净需求,和整个库位比较
                    foreach (string item in rawDemandDic.Keys)
                    {
                        var locQty = totalInvList.Where(h => (string)h[0] == flow.LocationFrom.Code && (string)h[1] == item).Sum(h => Convert.ToInt32(h[2]));

                        int requireQty = rawDemandDic[item] > locQty ? rawDemandDic[item] - locQty : 0;
                        if (rawDemandDic[item] > 0)
                        {
                            nonZeroRawDemandDic.Add(item, requireQty);
                        }
                    }
                    #endregion


                    #region 生成要货单

                    var q = from l in transferFlowList
                            where l.DefaultLocationTo.Code == flow.LocationFrom.Code
                            && nonZeroRawDemandDic.Keys.ToArray<string>().Contains(l.Item.Code)
                            group l by new { l.Flow } into g
                            select new { g.Key };

                    foreach (var f in q)
                    {
                        try
                        {
                            OrderHead oh = orderMgr.TransferFlow2Order(f.Key.Flow);
                            oh.StartTime = DateTime.Now;
                            oh.WindowTime = DateTime.Now.AddHours(Convert.ToDouble(f.Key.Flow.LeadTime));
                            oh.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;
                            IList<OrderDetail> nonZeroOrderDetailList = new List<OrderDetail>();
                            foreach (OrderDetail od in oh.OrderDetails)
                            {
                                if (nonZeroRawDemandDic.Count > 0)
                                {
                                    var qty = (from r in nonZeroRawDemandDic where r.Key == od.Item.Code select new { Value = r.Value }).SingleOrDefault();
                                    if (qty != null && Convert.ToDecimal(qty.Value) > 0)
                                    {
                                        OrderDetail newOrderDetail = od;
                                        newOrderDetail.OrderedQty = Convert.ToDecimal(qty.Value) * od.UnitCount;
                                        newOrderDetail.RequiredQty = Convert.ToDecimal(qty.Value) * od.UnitCount;
                                        nonZeroOrderDetailList.Add(newOrderDetail);
                                        nonZeroRawDemandDic.Remove(od.Item.Code);
                                    }
                                }
                            }
                            if (nonZeroOrderDetailList.Count > 0)
                            {
                                oh.OrderDetails = nonZeroOrderDetailList;
                                orderMgr.CreateOrder(oh, userMgr.GetMonitorUser());
                            }
                        }
                        catch (Exception e)
                        {
                            log.Error("error create order", e);
                            continue;
                        }
                    }



                    #endregion

                    #region 更新路线
                    flow.NextOrderTime = DateTime.Now.AddHours(Convert.ToDouble(flow.Interval));
                    flowMgr.UpdateFlow(flow);

                    #endregion

                }

            }

        }

        #region Private Methods

        #endregion
    }



}
