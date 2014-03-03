using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using NHibernate.Expression;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using System.Collections;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity.Dss;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.View;
using NHibernate.Transform;

namespace com.Sconit.Service.Dss.Impl
{
    public class RctwoOutboundMgr : AbstractOutboundMgr
    {
        private ICriteriaMgr criteriaMgr;
        private IOrderHeadMgr orderHeadMgr;
        private ILocationMgr locMgr;
        public RctwoOutboundMgr(INumberControlMgr numberControlMgr,
            IDssExportHistoryMgr dssExportHistoryMgr,
            ICriteriaMgr criteriaMgr,
            IDssOutboundControlMgr dssOutboundControlMgr,
            IDssObjectMappingMgr dssObjectMappingMgr,
            IOrderHeadMgr orderHeadMgr,
              ILocationMgr locationMgr)
            : base(numberControlMgr, dssExportHistoryMgr, criteriaMgr, dssOutboundControlMgr, dssObjectMappingMgr, locationMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.orderHeadMgr = orderHeadMgr;
            this.locMgr = locationMgr;
        }

        protected override IList<DssExportHistory> ExtractOutboundData(DssOutboundControl dssOutboundControl)
        {
            IList<LocationTransaction> locTrans = this.GetLocationTransaction(dssOutboundControl.Mark);
            log.Debug("Get records: LocTrans:" + locTrans.Count);

            locTrans = locTrans.Where(l => l.OrderDetailId > 0).ToList();//保险代码

            #region 投料事务修正
            if (locTrans != null && locTrans.Count > 0)
            {
                foreach (var locTran in locTrans)
                {
                    if (StringHelper.Eq(locTran.TransactionType, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO_BF))
                    {
                        locTran.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO;
                        locTran.Location = locTran.RefLocation;
                    }
                }
            }
            #endregion

            //按生效日期分类汇总
            var query = from l in locTrans
                        orderby l.EffectiveDate
                        group l by l.EffectiveDate into g
                        select new
                        {
                            EffectiveDate = g.Key,
                            list = g.ToList()
                        };

            IList<DssExportHistory> result = new List<DssExportHistory>();
            foreach (var q in query)
            {
                log.Debug("Now processing EffectiveDate:" + q.EffectiveDate.ToString("yyyy-MM-dd"));
                IList<DssExportHistory> group = this.GroupData(q.list, dssOutboundControl, q.EffectiveDate);

                result = result.Concat(group).ToList();
            }
            log.Debug("Result count after processing:" + result.Count);

            return result;
        }

        protected override object GetOutboundData(DssExportHistory dssExportHistory)
        {
            return dssExportHistory;
        }

        protected override object Serialize(object obj)
        {
            DssExportHistory dssExportHistory = (DssExportHistory)obj;
            DateTime effDate = dssExportHistory.EffectiveDate.HasValue ? dssExportHistory.EffectiveDate.Value : DateTime.Today;

            log.Debug("Now write data:" + dssExportHistory.Location + "," + dssExportHistory.Item + "," + dssExportHistory.Qty.ToString("0.##"));

            string[] line1 = new string[] 
            { 
                DssHelper.GetEventValue(dssExportHistory.EventCode),
                dssExportHistory.UndefinedString2,//雇员
                DssHelper.FormatDate(effDate,dssExportHistory.DssOutboundControl.ExternalSystem.Code),//生效日期
                dssExportHistory.PartyTo,//QAD:Site
                dssExportHistory.Item,//零件号
                dssExportHistory.UndefinedString3,//工序
                null,
                null,
                null,
                dssExportHistory.Qty.ToString("0.##"),//数量
                null,// dssExportHistory.DefinedString1,//废品数
                null,// dssExportHistory.DefinedString2,//次品数
                null,
                dssExportHistory.PartyTo,//QAD:Site
                dssExportHistory.Location//库位
            };

            List<string[]> dataList = new List<string[]>();
            dataList.Add(line1);

            IList<DssExportHistoryDetail> dssExportHistoryDetailList = dssExportHistory.DssExportHistoryDetails;
            if (dssExportHistory.DssExportHistoryDetails != null && dssExportHistory.DssExportHistoryDetails.Count > 0)
            {
                foreach (DssExportHistoryDetail dssExportHistoryDetail in dssExportHistoryDetailList)
                {
                    string[] line2 = new string[]
                    {
                        DssHelper.GetEventValue(dssExportHistory.EventCode),
                        dssExportHistoryDetail.Item,//零件号
                        dssExportHistory.UndefinedString3,//工序
                        dssExportHistoryDetail.Qty.ToString("0.####"),//数量
                        dssExportHistory.PartyFrom,//QAD:Site
                        dssExportHistoryDetail.Location//库位
                    };
                    dataList.Add(line2);
                }
            }

            string[][] data = dataList.ToArray();

            return new object[] { effDate, data };
        }

        #region Private Method
        private IList<LocationTransaction> GetLocationTransaction(int mark)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransaction));
            criteria.Add(Expression.Eq("IsSubcontract", false));
            criteria.Add(Expression.In("TransactionType", new string[] { 
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO_BF}));
            criteria.Add(Expression.Gt("Id", mark));

            criteria.SetProjection(Projections.ProjectionList()
                .Add(Projections.Max("Id").As("Id"))
                .Add(Projections.GroupProperty("OrderNo").As("OrderNo"))
                .Add(Projections.GroupProperty("OrderDetailId").As("OrderDetailId"))
                .Add(Projections.GroupProperty("OrderLocationTransactionId").As("OrderLocationTransactionId"))
                .Add(Projections.GroupProperty("TransactionType").As("TransactionType"))
                .Add(Projections.GroupProperty("Item").As("Item"))
                .Add(Projections.GroupProperty("Location").As("Location"))
                .Add(Projections.Sum("Qty").As("Qty"))
                .Add(Projections.GroupProperty("EffectiveDate").As("EffectiveDate"))
                .Add(Projections.GroupProperty("PartyFrom").As("PartyFrom"))
                .Add(Projections.GroupProperty("PartyTo").As("PartyTo"))
                .Add(Projections.GroupProperty("RefLocation").As("RefLocation")));

            criteria.SetResultTransformer(Transformers.AliasToBean(typeof(LocationTransaction)));
            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        private IList<OrderLocationTransaction> GetOrderLocationTransaction(List<string> orderNoList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            criteria.CreateAlias("OrderDetail", "od");
            CriteriaHelper.SetInCriteria<string>(criteria, "od.OrderHead.OrderNo", orderNoList);

            return criteriaMgr.FindAll<OrderLocationTransaction>(criteria);
        }

        private IList<LocationTransaction> GetVirtualRCTWO(List<int> orderDetIdList)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            CriteriaHelper.SetInCriteria<int>(criteria, "od.Id", orderDetIdList);
            criteria.Add(Expression.Eq("IOType", BusinessConstants.IO_TYPE_IN));

            criteria.SetProjection(Projections.Distinct(Projections.ProjectionList()
                .Add(Projections.Property("od.Item.Code").As("Item"))
                .Add(Projections.Property("Location.Code").As("Location"))
                .Add(Projections.Property("oh.OrderNo").As("OrderNo"))
                .Add(Projections.Property("od.Id").As("OrderDetailId"))
                .Add(Projections.Property("Id").As("OrderLocationTransactionId"))
                .Add(Projections.Property("oh.PartyFrom.Code").As("PartyFrom"))
                .Add(Projections.Property("oh.PartyTo.Code").As("PartyTo"))));

            criteria.SetResultTransformer(Transformers.AliasToBean(typeof(LocationTransaction)));
            return criteriaMgr.FindAll<LocationTransaction>(criteria);
        }

        private IList<DssExportHistory> GroupData(IList<LocationTransaction> locTrans, DssOutboundControl dssOutboundControl, DateTime effectiveDate)
        {
            #region 补充0成品工单
            var fgOrderDetIdList = locTrans
                 .Where(l => StringHelper.Eq(l.TransactionType, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO))
                 .Select(l => l.OrderDetailId).Distinct().ToList();
            var rmOrderDetIdList = locTrans
                   .Where(l => StringHelper.Eq(l.TransactionType, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO))
                   .Select(l => l.OrderDetailId).Distinct().ToList();

            var addList = rmOrderDetIdList.Except(fgOrderDetIdList).Distinct().ToList();

            #region 添加虚拟RCT-WO
            if (addList.Count > 0)
            {
                IList<LocationTransaction> virtualWOList = this.GetVirtualRCTWO(addList);
                if (virtualWOList.Count > 0)
                {
                    foreach (var virtualWO in virtualWOList)
                    {
                        virtualWO.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO;
                        virtualWO.EffectiveDate = effectiveDate;
                        virtualWO.Qty = 0;
                    }
                    locTrans = locTrans.Concat(virtualWOList).ToList();
                }
            }
            #endregion
            #endregion

            #region Transformer
            var fgQuery = locTrans.Where(l => StringHelper.Eq(l.TransactionType, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO)).ToList();
            var rmQuery = locTrans.Where(l => StringHelper.Eq(l.TransactionType, BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO)).ToList();

            log.Debug("Begin to group single data, count:" + fgQuery.Count);
            IList<DssExportHistory> dssExportHistoryList = this.GroupSingleDssExportHistory(fgQuery, rmQuery, dssOutboundControl, effectiveDate);
            #endregion

            return dssExportHistoryList;
        }

        private IList<DssExportHistory> GroupSingleDssExportHistory(IList<LocationTransaction> fgLocTrans, IList<LocationTransaction> rmLocTrans, DssOutboundControl dssOutboundControl, DateTime effectiveDate)
        {
            var query = from l in fgLocTrans
                        group l by new { l.PartyFrom, l.PartyTo, l.Location, l.Item, l.RefLocation } into g
                        select new DssExportHistory
                        {
                            PartyFrom = g.Key.PartyFrom,
                            PartyTo = getPartTo(g.Key.Location, g.Key.PartyTo, g.Key.RefLocation),
                            Location = g.Key.Location,
                            Item = g.Key.Item,
                            Qty = g.Sum(d => d.Qty),
                            EffectiveDate = effectiveDate,
                            OriginalId = g.Max(d => d.Id),
                            DssOutboundControl = dssOutboundControl,
                            EventCode = BusinessConstants.DSS_EVENT_CODE_CREATE,
                            IsActive = true,
                            CreateDate = DateTime.Now,
                            DssExportHistoryDetails = this.GroupSingleDssExportHistoryDetail(g.Select(d => d.OrderDetailId).Distinct().ToList(), rmLocTrans, dssOutboundControl, effectiveDate),
                            UndefinedString1 = dssOutboundControl.ExternalSystem.UndefinedString1,//Site,备用
                            UndefinedString2 = dssOutboundControl.UndefinedString1,//雇员
                            UndefinedString3 = dssOutboundControl.UndefinedString2,//工序
                            KeyCode = g.Max(d => d.Id).ToString()//Max LocTransId
                        };

            return query.ToList();
        }
        //djin 20120907 add
        private string getPartTo(string location, string partyTo, string refLoc)
        {
            if (location == "Reject")
            {
                return locMgr.LoadLocation(refLoc).Region.Code;
            }
            return partyTo;
        }

        private IList<DssExportHistoryDetail> GroupSingleDssExportHistoryDetail(List<int> orderDetIdList, IList<LocationTransaction> rmLocTrans, DssOutboundControl dssOutboundControl, DateTime effectiveDate)
        {
            var details = rmLocTrans.Where(r => orderDetIdList.Contains(r.OrderDetailId)).ToList();

            var query = from d in details
                        group d by new { d.PartyFrom, d.PartyTo, d.Location, d.Item } into g
                        select new DssExportHistoryDetail
                        {
                            PartyFrom = g.Key.PartyFrom,
                            PartyTo = g.Key.PartyTo,
                            Location = this.GetMappingExternalCode(BusinessConstants.DSS_ENTITY_LOCATION, dssOutboundControl.ExternalSystem.Code, g.Key.Location, g.Key.Location),
                            Item = g.Key.Item,
                            Qty = -g.Sum(d => d.Qty),//生产消耗事务数为负
                            EffDate = effectiveDate,
                            OriginalId = g.Max(d => d.Id),
                            KeyCode = g.Max(d => d.Id).ToString()
                        };

            return query.ToList();
        }

        #endregion
    }
}
