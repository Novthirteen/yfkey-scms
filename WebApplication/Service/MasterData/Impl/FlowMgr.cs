using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.View;
using System.Linq;
using com.Sconit.Entity.Mes;
using com.Sconit.Service.Mes;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class FlowMgr : FlowBaseMgr, IFlowMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IFlowDetailMgr flowDetailMgr;
        private IBomMgr bomMgr;
        private IBomDetailMgr bomDetailMgr;
        private IFlowTrackMgr flowTrackMgr;


        public FlowMgr(IFlowDao entityDao,
            ICriteriaMgr criteriaMgr,
            IFlowDetailMgr flowDetailMgr,
            IBomMgr bomMgr,
            IBomDetailMgr bomDetailMgr,
            IFlowTrackMgr flowTrackMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
            this.flowDetailMgr = flowDetailMgr;
            this.bomMgr = bomMgr;
            this.bomDetailMgr = bomDetailMgr;
            this.flowTrackMgr = flowTrackMgr;
        }

        [Transaction(TransactionMode.Requires)]
        public override void DeleteFlow(string flowCode)
        {
            Flow flow = this.LoadFlow(flowCode, true);
            flowTrackMgr.CreateFlowTrack(flow, BusinessConstants.TRACK_EVENT_DELETE);
            flowDetailMgr.DeleteFlowDetail(flow.FlowDetails);
            base.DeleteFlow(flowCode);
        }

        [Transaction(TransactionMode.Requires)]
        public override void CreateFlow(Flow flow)
        {
            base.Create(flow);
            flowTrackMgr.CreateFlowTrack(flow, BusinessConstants.TRACK_EVENT_CREATE);

        }

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public Flow LoadFlow(string code, bool includeFlowDetail)
        {
            return LoadFlow(code, includeFlowDetail, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow LoadFlow(string code, bool includeFlowDetail, bool includeRefDetail)
        {
            Flow flow = this.LoadFlow(code);
            if (includeFlowDetail && flow != null && flow.FlowDetails != null && flow.FlowDetails.Count > 0)
            {
            }

            if (includeRefDetail && flow != null && flow.ReferenceFlow != null && flow.ReferenceFlow.Trim() != string.Empty)
            {
                flow.FlowDetails = flowDetailMgr.GetFlowDetail(flow.ReferenceFlow, includeRefDetail);
            }
            return flow;
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow CheckAndLoadFlow(string code)
        {
            return CheckAndLoadFlow(code, false, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow CheckAndLoadFlow(string code, bool includeFlowDetail)
        {
            return CheckAndLoadFlow(code, includeFlowDetail, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow CheckAndLoadFlow(string code, bool includeFlowDetail, bool includeRefDetail)
        {
            Flow flow = this.LoadFlow(code, true);

            if (flow == null)
            {
                throw new BusinessErrorException("Flow.Error.FlowCodeNotExist", code);
            }

            if (includeFlowDetail && flow != null && flow.FlowDetails != null && flow.FlowDetails.Count > 0)
            {
            }

            if (includeRefDetail && flow != null && flow.ReferenceFlow != null && flow.ReferenceFlow.Trim() != string.Empty)
            {
                flow.FlowDetails = flowDetailMgr.GetFlowDetail(flow.ReferenceFlow, includeRefDetail);
            }

            return flow;
        }


        [Transaction(TransactionMode.Unspecified)]
        public void UpdateFlow(Flow flow, bool isTrack)
        {

            base.UpdateFlow(flow);
            if (isTrack)
            {
                flowTrackMgr.CreateFlowTrack(flow, BusinessConstants.TRACK_EVENT_UPDATE);
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetProcurementFlow(string userCode)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, BusinessConstants.PARTY_AUTHRIZE_OPTION_TO);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetProcurementFlow(string userCode, bool includeInactive)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, BusinessConstants.PARTY_AUTHRIZE_OPTION_TO, includeInactive);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetProcurementFlow(string userCode, string partyAuthrizeOpt)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, partyAuthrizeOpt);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetDistributionFlow(string userCode)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetDistributionFlow(string userCode, bool includeInactive)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM, includeInactive);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetProductionFlow(string userCode)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION, BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetProductionFlow(string userCode, bool includeInactive)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION, BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH, includeInactive);
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<Flow> GetProductionFlow(string userCode, bool includeInactive, string flowStrategy)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION, BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH, includeInactive, flowStrategy);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetTransferFlow(string userCode, string partyAuthrizeOpt)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER, partyAuthrizeOpt);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetTransferFlow(string userCode)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER, BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetTransferFlow(string userCode, bool includeInactive)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER, BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH, includeInactive);
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetCustomerGoodsFlow(string userCode)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS, BusinessConstants.PARTY_AUTHRIZE_OPTION_TO);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetCustomerGoodsFlow(string userCode, bool includeInactive)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_CUSTOMERGOODS, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS, BusinessConstants.PARTY_AUTHRIZE_OPTION_TO, includeInactive);
        }


        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetSubconctractingFlow(string userCode)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING, BusinessConstants.PARTY_AUTHRIZE_OPTION_TO);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetSubconctractingFlow(string userCode, bool includeInactive)
        {
            return GetFlow(BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING, userCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING, BusinessConstants.PARTY_AUTHRIZE_OPTION_TO, includeInactive);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetMRPFlow(string userCode)
        {
            List<Flow> flowList = new List<Flow>();

            IList<Flow> productionList = GetProductionFlow(userCode, false, BusinessConstants.CODE_MASTER_FLOW_STRATEGY_VALUE_MRP);
            flowList.AddRange(productionList);

            return flowList;
        }

        private IList<Flow> GetFlow(string flowType, string userCode, string specifiedType, string partyAuthrizeOpt)
        {
            return GetFlow(flowType, userCode, specifiedType, partyAuthrizeOpt, false);
        }

        private IList<Flow> GetFlow(string flowType, string userCode, string specifiedType, string partyAuthrizeOpt, bool includeInactive)
        {
            return GetFlow(flowType, userCode, specifiedType, partyAuthrizeOpt, includeInactive, null);
        }

        private IList<Flow> GetFlow(string flowType, string userCode, string specifiedType, string partyAuthrizeOpt, bool includeInactive, string flowStrategy)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Flow>();
            criteria.CreateAlias("PartyTo", "pt");
            criteria.CreateAlias("PartyFrom", "pf");
            criteria.Add(Expression.Eq("Type", flowType));
            if (!includeInactive)
            {
                criteria.Add(Expression.Eq("IsActive", true));
            }
            if (flowStrategy != null && flowStrategy != string.Empty)
            {
                criteria.Add(Expression.Eq("FlowStrategy", flowStrategy));
            }

            if (specifiedType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                //供货路线
                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] pfCrieteria = SecurityHelper.GetSupplierPermissionCriteria(userCode);

                    criteria.Add(
                          Expression.Or(
                              Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                              Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));
                }

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] ptCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    criteria.Add(
                           Expression.Or(
                               Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                               Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));
                }
            }
            else if (specifiedType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                //发货路线
                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] pfCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));
                }

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] ptCrieteria = SecurityHelper.GetCustomerPermissionCriteria(userCode);

                    criteria.Add(
                       Expression.Or(
                           Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                           Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                   ));
                }
            }
            else if (specifiedType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                //生产
                DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", regionCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", regionCrieteria[1])
                    ));
                }

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    criteria.Add(
                       Expression.Or(
                           Subqueries.PropertyIn("pt.Code", regionCrieteria[0]),
                           Subqueries.PropertyIn("pt.Code", regionCrieteria[1])
                   ));
                }
            }
            else if (specifiedType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                //移库路线
                DetachedCriteria[] rpCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    criteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pf.Code", rpCrieteria[0]),
                            Subqueries.PropertyIn("pf.Code", rpCrieteria[1])
                    ));
                }

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    criteria.Add(
                        Expression.Or(
                            Subqueries.PropertyIn("pt.Code", rpCrieteria[0]),
                            Subqueries.PropertyIn("pt.Code", rpCrieteria[1])
                    ));
                }
            }
            else if (specifiedType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS)
            {
                //客供品路线
                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] pfCrieteria = SecurityHelper.GetCustomerPermissionCriteria(userCode);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));
                }

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] ptCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                        Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));
                }
            }
            else if (specifiedType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING)
            {
                //委外加工路线
                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] pfCrieteria = SecurityHelper.GetSupplierPermissionCriteria(userCode);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                    ));
                }

                if (partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_TO
                   || partyAuthrizeOpt == BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH)
                {
                    DetachedCriteria[] ptCrieteria = SecurityHelper.GetRegionPermissionCriteria(userCode);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                        Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                    ));
                }
            }

            return criteriaMgr.FindAll<Flow>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetAllFlow(string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Flow>();
            criteria.CreateAlias("PartyTo", "pt");
            criteria.CreateAlias("PartyFrom", "pf");


            DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER);

            DetachedCriteria[] ptCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                    Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
            ));

            criteria.Add(
                Expression.Or(
                    Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                    Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
            ));

            return criteriaMgr.FindAll<Flow>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindWinTime(Flow flow, DateTime date)
        {
            if (date != null)
            {
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        return ParseWinTime(flow.WinTime1);

                    case DayOfWeek.Tuesday:
                        return ParseWinTime(flow.WinTime2);

                    case DayOfWeek.Wednesday:
                        return ParseWinTime(flow.WinTime3);

                    case DayOfWeek.Thursday:
                        return ParseWinTime(flow.WinTime4);

                    case DayOfWeek.Friday:
                        return ParseWinTime(flow.WinTime5);

                    case DayOfWeek.Saturday:
                        return ParseWinTime(flow.WinTime6);

                    case DayOfWeek.Sunday:
                        return ParseWinTime(flow.WinTime7);
                }
            }

            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<string> FindWinTime(string flowCode, DateTime date)
        {
            Flow flow = entityDao.LoadFlow(flowCode);
            if (flow == null)
            {
                return null;
            }
            return FindWinTime(flow, date);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow LoadFlow(string flowCode, string userCode)
        {
            return LoadFlow(flowCode, userCode, false, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow LoadFlow(string flowCode, string userCode, bool includeFlowDetail)
        {
            return LoadFlow(flowCode, userCode, includeFlowDetail, null);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Flow LoadFlow(string flowCode, string userCode, bool includeFlowDetail, string moduleType)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Flow>();
            criteria.Add(Expression.Eq("Code", flowCode));
            criteria.Add(Expression.Eq("IsActive", true));
            criteria.CreateAlias("PartyTo", "pt");
            criteria.CreateAlias("PartyFrom", "pf");

            DetachedCriteria[] pfCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
          BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER);

            DetachedCriteria[] ptCrieteria = SecurityHelper.GetPartyPermissionCriteria(userCode,
                BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_REGION, BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_CUSTOMER);

            if (moduleType != null)
            {
                IList<string> typeList = new List<string>();
                if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                {
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT);
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER);
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS);
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING);


                    criteria.Add(
                     Expression.Or(
                         Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                         Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                          ));
                }
                else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                {
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION);
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                        ));
                }
                else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION);

                    criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                        ));
                }

                else if (moduleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
                {
                    typeList.Add(BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER);

                    criteria.Add(
                    Expression.Or(
                      Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                      Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                      ));

                    criteria.Add(
                     Expression.Or(
                         Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                         Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                         ));
                }


                criteria.Add(Expression.In("Type", typeList.ToArray()));
            }

            else
            {
                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[0]),
                        Subqueries.PropertyIn("pf.Code", pfCrieteria[1])
                ));

                criteria.Add(
                    Expression.Or(
                        Subqueries.PropertyIn("pt.Code", ptCrieteria[0]),
                        Subqueries.PropertyIn("pt.Code", ptCrieteria[1])
                ));
            }
            IList<Flow> listFlow = criteriaMgr.FindAll<Flow>(criteria);
            if (listFlow.Count > 0)
            {
                if (includeFlowDetail)
                {
                    return this.LoadFlow(listFlow[0].Code, true);
                }
                else
                {
                    return listFlow[0];
                }
            }
            return null;
        }

        private IList<string> ParseWinTime(string winTime)
        {
            if (winTime == null || winTime.Trim() == string.Empty)
            {
                return null;
            }

            IList<string> result = new List<string>();
            string[] speratedWinTime = winTime.Split('|');

            foreach (string wt in speratedWinTime)
            {
                result.Add(wt.Trim());
            }

            return result;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetAllFlow(DateTime lastModifyDate, int firstRow, int maxRows)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
            criteria.Add(Expression.Gt("LastModifyDate", lastModifyDate));
            criteria.AddOrder(Order.Asc("LastModifyDate"));

            IList<Flow> flowList = criteriaMgr.FindAll<Flow>(criteria, firstRow, maxRows);
            if (flowList.Count > 0)
            {
                return flowList;
            }
            return null;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetFlowList(string userCode, bool includeProcurement, bool includeDistribution, bool includeTransfer, bool includeProduction, bool includeCustomerGoods, bool includeSubconctracting, string partyAuthrizeOpt)
        {
            List<Flow> flowList = new List<Flow>();
            if (includeProcurement)
            {
                IList<Flow> procurementList = GetProcurementFlow(userCode, partyAuthrizeOpt);
                flowList.AddRange(procurementList);
            }
            if (includeDistribution)
            {
                IList<Flow> distributionList = GetDistributionFlow(userCode);
                flowList.AddRange(distributionList);
            }
            if (includeTransfer)
            {
                IList<Flow> transferList = GetTransferFlow(userCode, partyAuthrizeOpt);
                flowList.AddRange(transferList);
            }
            if (includeProduction)
            {
                IList<Flow> productionList = GetProductionFlow(userCode);
                flowList.AddRange(productionList);
            }
            if (includeCustomerGoods)
            {
                IList<Flow> customerGoodsList = GetCustomerGoodsFlow(userCode);
                flowList.AddRange(customerGoodsList);
            }
            if (includeSubconctracting)
            {
                IList<Flow> subconctractingGoodsList = GetSubconctractingFlow(userCode);
                flowList.AddRange(subconctractingGoodsList);
            }
            return flowList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetBatchFeedBomDetail(Flow flow)
        {
            return GetBatchFeedBomDetail(flow.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<BomDetail> GetBatchFeedBomDetail(string flowCode)
        {
            Flow flow = this.LoadFlow(flowCode, true);

            if (flow != null && flow.FlowDetails != null && flow.FlowDetails.Count > 0)
            {
                IList<BomDetail> batchFeedBomDetailList = new List<BomDetail>();
                foreach (FlowDetail flowDetail in flow.FlowDetails)
                {
                    string bomCode = this.bomMgr.FindBomCode(flowDetail.Item);
                    IList<BomDetail> bomDetailList = this.bomDetailMgr.GetFlatBomDetail(bomCode, DateTime.Now);

                    if (bomDetailList != null && bomDetailList.Count > 0)
                    {
                        foreach (BomDetail bomDetail in bomDetailList)
                        {
                            if (bomDetail.BackFlushMethod == BusinessConstants.CODE_MASTER_BACKFLUSH_METHOD_VALUE_BATCH_FEED && !batchFeedBomDetailList.Contains(bomDetail))
                            {
                                bomDetail.DefaultLocation = flowDetail.DefaultLocationFrom;
                                batchFeedBomDetailList.Add(bomDetail);
                            }
                        }
                    }
                }

                return batchFeedBomDetailList;
            }
            return null;
        }

        //[Transaction(TransactionMode.Unspecified)]
        //public Flow CheckAndLoadTransferFlow(Location locationFrom, Location locationTo, Hu hu, string userCode)
        //{
        //    DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowView));
        //    criteria.CreateAlias("Flow", "f");
        //    criteria.CreateAlias("FlowDetail", "fd");
        //    criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER));
        //    SecurityHelper.SetPartySearchCriteria(criteria, "f.PartyFrom.Code", userCode);
        //    SecurityHelper.SetPartySearchCriteria(criteria, "f.PartyTo.Code", userCode);
        //    criteria.Add(Expression.Eq("LocationFrom.Code", locationFrom.Code));
        //    criteria.Add(Expression.Eq("LocationTo.Code", locationTo.Code));
        //    criteria.Add(Expression.Eq("fd.Item.Code", hu.Item.Code));
        //    criteria.Add(Expression.Eq("fd.Uom.Code", hu.Uom.Code));
        //    criteria.Add(Expression.Eq("fd.UnitCount", hu.UnitCount));

        //    IList<FlowView> list = criteriaMgr.FindAll<FlowView>(criteria);

        //    Flow flow = null;
        //    if (list != null && list.Count > 0)
        //    {
        //        //如果匹配上明细
        //        flow = list[0].Flow;
        //    }
        //    else
        //    {
        //        //如果没有匹配上明细 看是否能够新建明细
        //        IList<Flow> flowList = this.GetFlows(locationFrom, locationTo, true, userCode, new string[] { BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER });
        //        if (flowList != null && flowList.Count > 0)
        //        {
        //            flow = flowList[0];
        //        }
        //    }

        //    if (flow != null)
        //    {
        //        //返回带明细的Flow,考虑RefFlow
        //        return this.LoadFlow(flow.Code, true, true);
        //    }
        //    else
        //    {
        //        //如果没有匹配上明细,也没有允许新建明细的Flow
        //        throw new BusinessErrorException("Flow.Error.NotFoundMacthFlow", hu.HuId);
        //    }
        //}

        [Transaction(TransactionMode.Unspecified)]
        public FlowView CheckAndLoadFlowView(string flowCode, string userCode, string locationFromCode, string locationToCode, Hu hu, List<string> flowTypes)
        {

            //按物料号,单位,单包装
            FlowView flowView = LoadFlowView(flowCode, userCode, locationFromCode, locationToCode, hu, flowTypes);

            //看是否允许新建明细
            if (flowView == null)
            {
                IList<Flow> flowList = this.GetFlows(flowCode, locationFromCode, locationToCode, true, userCode, flowTypes);
                if (flowList != null && flowList.Count > 0)
                {
                    flowView = new FlowView();
                    flowView.Flow = flowList[0];
                    FlowDetail flowDetail = new FlowDetail();
                    flowDetail.Item = hu.Item;
                    flowDetail.Uom = hu.Uom;
                    flowDetail.UnitCount = hu.UnitCount;
                    flowView.LocationFrom = flowView.Flow.LocationFrom;
                    flowView.LocationTo = flowView.Flow.LocationTo;
                }
            }
            if (flowView != null)
            {
                return flowView;
            }
            else
            {
                throw new BusinessErrorException("Flow.Error.NotFoundMacthFlow", hu.HuId, flowCode);
            }
        }

        private IList<Flow> GetAllProductionFlow()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Flow>();
            criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            return criteriaMgr.FindAll<Flow>(criteria);
        }

        public IList<Flow> GetProductLinesNotInUser(string userCode)
        {
            List<Flow> otherproductLines = new List<Flow>();
            if (userCode != null && userCode != string.Empty)
            {
                IList<Flow> allproductLines = GetAllProductionFlow();
                IList<Flow> userproductLines = GetProductLinesInUser(userCode);

                if (allproductLines != null && allproductLines.Count > 0)
                {
                    foreach (Flow pl in allproductLines)
                    {
                        if (!userproductLines.Contains(pl))
                        {
                            otherproductLines.Add(pl);
                        }
                    }
                }
            }
            var q = otherproductLines.OrderBy(a => a.Code);
            return q.ToList();
        }

        public IList<Flow> GetProductLinesInUser(string userCode)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ProductLineUser)).Add(Expression.Eq("DeliveryUser.Code", userCode))
          .SetProjection(Projections.Property("ProductLine"));

            IList<Flow> productLines = criteriaMgr.FindAll<Flow>(criteria);
            var q = productLines.OrderBy(p => p.Code);
            return q.ToList();
        }


        [Transaction(TransactionMode.Unspecified)]
        public Flow LoadFlowByDesc(string flowDesc)
        {
            DetachedCriteria criteria = DetachedCriteria.For<Flow>();
            criteria.Add(Expression.Eq("Description", flowDesc));
            criteria.Add(Expression.Eq("IsActive", true));
            IList<Flow> flowList = criteriaMgr.FindAll<Flow>(criteria);

            if (flowList != null && flowList.Count > 0)
            {
                return flowList[0];
            }
            return null;
        }
        #endregion Customized Methods

        #region Private Method

        [Transaction(TransactionMode.Unspecified)]
        private FlowView LoadFlowView(string flowCode, string userCode, string locationFromCode, string locationToCode, Hu hu, List<string> flowTypes)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowView));
            criteria.CreateAlias("Flow", "f");
            criteria.CreateAlias("FlowDetail", "fd");
            if (flowTypes != null && flowTypes.Count > 0)
            {
                if (flowTypes.Count == 1)
                {
                    criteria.Add(Expression.Eq("f.Type", flowTypes[0]));
                }
                else
                {
                    criteria.Add(Expression.In("f.Type", flowTypes));
                }
            }
            //权限待处理
            //if (userCode != null && userCode.Trim() != string.Empty)
            //{
            //    SecurityHelper.SetPartySearchCriteria(criteria, "PartyFromCode", userCode);
            //    SecurityHelper.SetPartySearchCriteria(criteria, "PartyToCode", userCode);
            //}
            if (locationFromCode != null && locationFromCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LocationFrom.Code", locationFromCode));
            }
            if (locationToCode != null && locationToCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LocationTo.Code", locationToCode));
            }
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("f.Code", flowCode));
            }
            criteria.Add(Expression.Eq("fd.Item.Code", hu.Item.Code));
            criteria.Add(Expression.Eq("fd.Uom.Code", hu.Uom.Code));
            //if (!allowCreateDetail)
            //{
            //    criteria.Add(Expression.Eq("ItemCode", hu.Item.Code));
            //    criteria.Add(Expression.Eq("UomCode", hu.Uom.Code));
            //    if (isMatchUnitCount)
            //    {
            //        criteria.Add(Expression.Eq("UnitCount", hu.UnitCount));
            //    }
            //}
            //criteria.Add(Expression.Eq("AllowCreateDetail", allowCreateDetail));

            IList<FlowView> list = criteriaMgr.FindAll<FlowView>(criteria);

            if (list != null && list.Count > 0)
            {
                var query = new List<FlowView>(list.Where(f => f.FlowDetail.UnitCount == hu.UnitCount));
                if (query != null && query.Count > 0)
                {
                    //按物料号,单位,单包装严格匹配
                    return query[0];
                }
                //按物料号,单位匹配
                return list[0];
            }
            else
            {
                return null;
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<Flow> GetFlows(string locationFromCode, string locationToCode, bool allowCreateDetail, string userCode, List<string> flowTypes)
        {
            return this.GetFlows(null, locationFromCode, locationToCode, allowCreateDetail, userCode, flowTypes);
        }
        [Transaction(TransactionMode.Unspecified)]
        private IList<Flow> GetFlows(string flowCode, string locationFromCode, string locationToCode, bool allowCreateDetail, string userCode, List<string> flowTypes)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
            if (flowTypes != null && flowTypes.Count > 0)
            {
                if (flowTypes.Count == 1)
                {
                    criteria.Add(Expression.Eq("Type", flowTypes[0]));
                }
                else
                {
                    criteria.Add(Expression.In("Type", flowTypes));
                }
            }
            //权限待处理
            //if (userCode != null && userCode.Trim() != string.Empty)
            //{
            //    SecurityHelper.SetPartySearchCriteria(criteria, "PartyFrom.Code", userCode);
            //    SecurityHelper.SetPartySearchCriteria(criteria, "PartyTo.Code", userCode);
            //}
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Code", flowCode));
            }
            if (locationFromCode != null && locationFromCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LocationFrom.Code", locationFromCode));
            }
            if (locationToCode != null && locationToCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("LocationTo.Code", locationToCode));
            }
            criteria.Add(Expression.Eq("AllowCreateDetail", allowCreateDetail));

            return criteriaMgr.FindAll<Flow>(criteria);
        }
        #endregion
    }
}