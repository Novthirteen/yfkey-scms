using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class SupplyChainMgr : ISupplyChainMgr
    {
        private ICriteriaMgr CriteriaMgr;
        private IFlowMgr FlowMgr;
        private IBomDetailMgr BomDetailMgr;
        public SupplyChainMgr(
            ICriteriaMgr CriteriaMgr,
            IFlowMgr FlowMgr,
            IBomDetailMgr BomDetailMgr)
            : base()
        {
            this.CriteriaMgr = CriteriaMgr;
            this.FlowMgr = FlowMgr;
            this.BomDetailMgr = BomDetailMgr;
        }

        #region Public Method

        [Transaction(TransactionMode.Unspecified)]
        public IList<SupplyChain> GenerateSupplyChain(string flowCode, string itemCode)
        {
            IList<SupplyChain> supplyChainList = new List<SupplyChain>();
            Flow flow = FlowMgr.LoadFlow(flowCode, true);
            if (flow.FlowDetails != null && flow.FlowDetails.Count > 0)
            {
                foreach (FlowDetail flowDetail in flow.FlowDetails)
                {
                    if (flowDetail.Item.Code == itemCode)
                    {
                        supplyChainList.Add(this.GenerateSupplyChain(flowDetail));
                    }
                }
            }
            else
            {
                if (flow.ReferenceFlow != null && flow.ReferenceFlow.Trim() != string.Empty)                                        
                {
                    Flow refFlow = this.FlowMgr.LoadFlow(flow.ReferenceFlow, true);
                    if (refFlow.FlowDetails != null && refFlow.FlowDetails.Count > 0) 
                    {
                        foreach (FlowDetail flowDetail in refFlow.FlowDetails)
                        {
                            if (flowDetail.Item.Code == itemCode)
                            {
                                supplyChainList.Add(this.GenerateSupplyChain(flow, flowDetail));
                            }
                        }
                    }
                }
            }

            return supplyChainList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public SupplyChain GenerateSupplyChain(FlowDetail flowDetail)
        {
            return this.GenerateSupplyChain(flowDetail.Flow, flowDetail);
        }

        [Transaction(TransactionMode.Unspecified)]
        public SupplyChain GenerateSupplyChain(Flow flow, FlowDetail flowDetail)
        {
            SupplyChain supplyChain = new SupplyChain();
            supplyChain.Flow = flow;
            supplyChain.FlowDetail = flowDetail;

            SupplyChainDetail supplyChainDetail = new SupplyChainDetail();
            supplyChainDetail.SupplyChain = supplyChain;
            supplyChainDetail.Id = 1;
            supplyChainDetail.ParentId = 0;
            supplyChainDetail.Flow = flow;
            supplyChainDetail.FlowDetail = flowDetail;
            supplyChainDetail.LocationTo = flowDetail.DefaultLocationTo == null ? null : flowDetail.DefaultLocationTo;
            supplyChainDetail.QuantityPer = 1;

            IList<SupplyChainDetail> supplyChainDetailList = new List<SupplyChainDetail>();
            supplyChainDetailList.Add(supplyChainDetail);
            this.GetSupplyChainDetail(supplyChainDetailList, supplyChainDetail);

            supplyChain.AddRangeSupplyChainDetail(supplyChainDetailList);
            return supplyChain;
        }

        #endregion

        #region Private Method

        [Transaction(TransactionMode.Unspecified)]
        private void GetSupplyChainDetail(IList<SupplyChainDetail> supplyChainDetailList, SupplyChainDetail parentSupplyChainDetail)
        {
            if (parentSupplyChainDetail.Flow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
            {
                string bomCode = this.GetBomCode(parentSupplyChainDetail.FlowDetail);
                IList<BomDetail> bomDetailList = BomDetailMgr.GetFlatBomDetail(bomCode, DateTime.Now);
                if (bomDetailList != null && bomDetailList.Count > 0)
                {
                    foreach (BomDetail bomDetail in bomDetailList)
                    {
                        string itemCode = bomDetail.Item.Code;
                        string locFrom = string.Empty;
                        if (bomDetail.Location != null)
                        {
                            locFrom = bomDetail.Location.Code;
                        }
                        else
                        {
                            if (parentSupplyChainDetail.FlowDetail.DefaultLocationFrom == null)
                            {
                                //end
                                continue;
                            }
                            else
                            {
                                locFrom = parentSupplyChainDetail.FlowDetail.DefaultLocationFrom.Code;
                            }
                        }
                        decimal QtyPer = bomDetail.RateQty * (1 + bomDetail.ScrapPercentage);

                        IList<FlowDetail> flowDetailList = this.GetFlowDetailList(itemCode, locFrom);
                        this.FillSupplyChainDetail(supplyChainDetailList, parentSupplyChainDetail, flowDetailList, QtyPer, locFrom);
                    }
                }
            }
            else
            {
                string locFrom = this.GetLocFrom(parentSupplyChainDetail);
                if (locFrom != null)
                {
                    decimal QtyPer = 1;
                    IList<FlowDetail> flowDetailList = this.GetFlowDetailList(parentSupplyChainDetail.FlowDetail.Item.Code, locFrom);
                    this.FillSupplyChainDetail(supplyChainDetailList, parentSupplyChainDetail, flowDetailList, QtyPer, locFrom);
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private void FillSupplyChainDetail(IList<SupplyChainDetail> supplyChainDetailList, SupplyChainDetail parentSupplyChainDetail, IList<FlowDetail> flowDetailList, decimal QtyPer, string locFrom)
        {
            SupplyChainDetail supplyChainDetail = new SupplyChainDetail();

            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail flowDetail in flowDetailList)
                {
                    supplyChainDetail = new SupplyChainDetail();
                    supplyChainDetail.SupplyChain = parentSupplyChainDetail.SupplyChain;
                    supplyChainDetail.Id = supplyChainDetailList.Count + 1;
                    supplyChainDetail.ParentId = parentSupplyChainDetail.Id;
                    supplyChainDetail.Flow = flowDetail.Flow;
                    supplyChainDetail.FlowDetail = flowDetail;
                    supplyChainDetail.LocationTo = flowDetail.DefaultLocationTo == null ? null : flowDetail.DefaultLocationTo;
                    supplyChainDetail.QuantityPer = QtyPer;

                    if (supplyChainDetailList.Contains(supplyChainDetail))
                    {
                        return;
                    }
                    else
                    {
                        supplyChainDetailList.Add(supplyChainDetail);
                        this.GetSupplyChainDetail(supplyChainDetailList, supplyChainDetail);
                    }

                    IList<Flow> flowList = this.GetReferenceFlow(flowDetail, locFrom);
                    if (flowList != null && flowList.Count > 0)
                    {
                        //ReferenceFlow
                        foreach (Flow flow in flowList)
                        {
                            if (flow.ReferenceFlow != null && flow.ReferenceFlow == flowDetail.Flow.Code)
                            {
                                supplyChainDetail = new SupplyChainDetail();
                                supplyChainDetail.SupplyChain = parentSupplyChainDetail.SupplyChain;
                                supplyChainDetail.Id = supplyChainDetailList.Count + 1;
                                supplyChainDetail.ParentId = parentSupplyChainDetail.Id;
                                supplyChainDetail.Flow = flow;
                                supplyChainDetail.FlowDetail = flowDetail;
                                supplyChainDetail.LocationTo = flowDetail.DefaultLocationTo == null ? null : flowDetail.DefaultLocationTo;
                                supplyChainDetail.QuantityPer = QtyPer;

                                if (supplyChainDetailList.Contains(supplyChainDetail))
                                {
                                    return;
                                }
                                else
                                {
                                    supplyChainDetailList.Add(supplyChainDetail);
                                    this.GetSupplyChainDetail(supplyChainDetailList, supplyChainDetail);
                                }
                            }
                        }
                    }
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private string GetBomCode(FlowDetail flowDetail)
        {
            string bomCode = string.Empty;
            if (flowDetail.Bom != null)
            {
                bomCode = flowDetail.Bom.Code;
            }
            else
            {
                bomCode = flowDetail.Item.Bom == null ? flowDetail.Item.Code : flowDetail.Item.Bom.Code;
            }

            return bomCode;
        }

        [Transaction(TransactionMode.Unspecified)]
        private string GetLocFrom(SupplyChainDetail supplyChainDetail)
        {
            string locFrom = null;
            if (supplyChainDetail.Flow.Code == supplyChainDetail.FlowDetail.Flow.Code)
            {
                locFrom = supplyChainDetail.FlowDetail.DefaultLocationFrom == null ? null : supplyChainDetail.FlowDetail.DefaultLocationFrom.Code;
            }
            else
            {
                //ReferenceFlow
                locFrom = supplyChainDetail.Flow.LocationFrom == null ? null : supplyChainDetail.Flow.LocationFrom.Code;
            }

            return locFrom;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<FlowDetail> GetFlowDetailList(string itemCode, string locTo)
        {
            IList<FlowDetail> returnFlowDetailList = new List<FlowDetail>();
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            IList<FlowDetail> flowDetailList = CriteriaMgr.FindAll<FlowDetail>(criteria);

            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail flowDetail in flowDetailList)
                {
                    if (flowDetail.DefaultLocationTo != null)
                    {
                        if (flowDetail.Item.Code == itemCode && flowDetail.DefaultLocationTo.Code == locTo)
                        {
                            returnFlowDetailList.Add(flowDetail);
                        }
                    }
                }
            }

            return returnFlowDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<Flow> GetReferenceFlow(FlowDetail flowDetail, string locTo)
        {
            IList<Flow> refFlowList = new List<Flow>();
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
            criteria.Add(Expression.IsNotNull("ReferenceFlow"));
            IList<Flow> flowList = CriteriaMgr.FindAll<Flow>(criteria);
            if (flowList != null && flowList.Count > 0)
            {
                foreach (Flow flow in flowList)
                {
                    if (flow.LocationTo != null)
                    {
                        if (flow.LocationTo.Code == locTo && flowDetail.Flow.Code == flow.ReferenceFlow)
                        {
                            refFlowList.Add(flow);
                            break;
                        }
                    }
                }
            }

            return refFlowList;
        }

        #endregion
    }
}