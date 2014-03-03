using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FlowBindingMgr : FlowBindingBaseMgr, IFlowBindingMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IFlowMgr flowMgr;
        private IOrderDetailMgr orderDetailMgr;
        private IUomConversionMgr uomConversionMgr;
        public FlowBindingMgr(IFlowBindingDao entityDao,
            ICriteriaMgr criteriaMgr,
            IFlowMgr flowMgr,
            IOrderDetailMgr orderDetailMgr,
            IUomConversionMgr uomConversionMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.flowMgr = flowMgr;
            this.orderDetailMgr = orderDetailMgr;
            this.uomConversionMgr = uomConversionMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public FlowBinding LoadFlowBinding(string flowCode, string slaveFlowCode)
        {

            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowBinding));
            criteria.Add(Expression.Eq("MasterFlow.Code", flowCode)).Add(Expression.Eq("SlaveFlow.Code", slaveFlowCode));
            IList<FlowBinding> fbList = criteriaMgr.FindAll<FlowBinding>(criteria);

            if (fbList.Count > 0)
                return fbList[0];
            else return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<FlowBinding> GetFlowBinding(string flowCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowBinding));
            criteria.Add(Expression.Eq("MasterFlow.Code", flowCode));
            return criteriaMgr.FindAll<FlowBinding>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<FlowBinding> GetFlowBinding(string flowCode, params string[] bindingTypes)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowBinding));
            criteria.Add(Expression.Eq("MasterFlow.Code", flowCode));
            if (bindingTypes.Length == 1)
            {
                criteria.Add(Expression.Eq("BindingType", bindingTypes[0]));
            }
            else
            {
                criteria.Add(Expression.In("BindingType", bindingTypes));
            }
            return criteriaMgr.FindAll<FlowBinding>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<FlowDetail> GetBindedFlowDetail(OrderDetail orderDetail, string slaveFlowCode)
        {
            return this.GetBindedFlowDetail(orderDetail.Id, slaveFlowCode);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<FlowDetail> GetBindedFlowDetail(int orderDetailId, string slaveFlowCode)
        {
            OrderDetail orderDetail = this.orderDetailMgr.LoadOrderDetail(orderDetailId);
            IList<FlowDetail> returnList = new List<FlowDetail>();

            IList<OrderLocationTransaction> orderLocTransList =
                this.orderDetailMgr.LoadOrderDetail(orderDetail.Id, true).OrderLocationTransactions;

            Flow flow = flowMgr.LoadFlow(slaveFlowCode, true, true);
            IList<FlowDetail> flowDetailList = flow.FlowDetails;

            //记录已经被绑定的路线明细，避免采购绑定销售的时候重复累加数量
            //（因为百利得有内部销售的需求，所以现在双方都没有目的库位也可以绑定，这就造成采购绑定销售的时候重复累加数量）
            IDictionary<int, FlowDetail> bindedFlowDetail = new Dictionary<int, FlowDetail>();

            if (orderLocTransList != null && orderLocTransList.Count > 0 && flowDetailList.Count > 0)
            {
                foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
                {
                    //if (orderLocTrans.Location == null)
                    //{
                    //    continue;
                    //}

                    string itemCode = orderLocTrans.Item.Code;
                    string locationCode = orderLocTrans.Location != null ? orderLocTrans.Location.Code : null;

                    foreach (FlowDetail flowDetail in flowDetailList)
                    {
                        if (bindedFlowDetail.ContainsKey(flowDetail.Id))
                        {
                            continue;
                        }

                        if (itemCode.Trim().ToUpper() != flowDetail.Item.Code.Trim().ToUpper())
                        {
                            continue;
                        }

                        Location fromLoc = null;
                        Location toLoc = null;
                        if (flowDetail.Flow.Code.Trim().ToUpper() == slaveFlowCode.Trim().ToUpper())
                        {
                            fromLoc = flowDetail.DefaultLocationFrom;
                            toLoc = flowDetail.DefaultLocationTo;
                        }
                        else
                        {
                            //reference flow
                            fromLoc = flow.LocationFrom;
                            toLoc = flow.LocationTo;
                        }

                        if (orderLocTrans.IOType == BusinessConstants.IO_TYPE_IN)
                        {
                            //Master LocationTo == Slave LocationFrom
                            //if (fromLoc == null)
                            //{
                            //    continue;
                            //}

                            if ((fromLoc == null && locationCode == null)
                                || (fromLoc != null && locationCode != null && fromLoc.Code.Trim().ToUpper() == locationCode.Trim().ToUpper()))
                            {
                                bindedFlowDetail.Add(flowDetail.Id, flowDetail);
                                decimal orderedQty = orderLocTrans.OrderedQty;
                                if (flowDetail.Uom.Code != orderLocTrans.Uom.Code)
                                {
                                    orderedQty = uomConversionMgr.ConvertUomQty(flowDetail.Item, orderLocTrans.Uom, orderLocTrans.OrderedQty, flowDetail.Uom);
                                }
                                flowDetail.OrderedQty += orderedQty;

                                if (!returnList.Contains(flowDetail))
                                {
                                    returnList.Add(flowDetail);
                                }
                            }
                        }
                        else
                        {
                            //Master LocationFrom == Slave LocationTo
                            //if (toLoc == null)
                            //{
                            //    continue;
                            //}

                            if ((toLoc == null && locationCode == null)
                                || (toLoc != null && locationCode != null && toLoc.Code.Trim().ToUpper() == locationCode.Trim().ToUpper()))
                            {
                                bindedFlowDetail.Add(flowDetail.Id, flowDetail);
                                decimal orderedQty = orderLocTrans.OrderedQty;
                                if (flowDetail.Uom.Code != orderLocTrans.Uom.Code)
                                {
                                    orderedQty = uomConversionMgr.ConvertUomQty(flowDetail.Item, orderLocTrans.Uom, orderLocTrans.OrderedQty, flowDetail.Uom);
                                }
                                flowDetail.OrderedQty += orderedQty;

                                if (!returnList.Contains(flowDetail))
                                {
                                    returnList.Add(flowDetail);
                                }
                            }
                        }

                    }

                }
            }

            return returnList;
        }

        #endregion Customized Methods
    }
}