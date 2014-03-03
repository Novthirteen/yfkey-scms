using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using Castle.Services.Transaction;
using System.Collections;
using com.Sconit.Entity;
using NHibernate.Expression;
using com.Sconit.Entity.Dss;
using com.Sconit.Entity.MasterData;
namespace com.Sconit.Service.Dss.Impl
{
    public class IssunpOutboundMgr : UnpOutboundBaseMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ICommonOutboundMgr commonOutboundMgr;
        private ILocationMgr locationMgr;
        protected IMiscOrderMgr miscOrderMgr { get; set; }
        private IDssOutboundControlMgr dssoutcontrolmgr;
        public IssunpOutboundMgr(INumberControlMgr numberControlMgr,
           IDssExportHistoryMgr dssExportHistoryMgr,
           ICriteriaMgr criteriaMgr,
           IDssOutboundControlMgr dssOutboundControlMgr,
           IDssObjectMappingMgr dssObjectMappingMgr,
            ICommonOutboundMgr commonOutboundMgr,
            IMiscOrderMgr miscOrderMgr,
            ILocationMgr locationMgr)
            : base(numberControlMgr, dssExportHistoryMgr, criteriaMgr, dssOutboundControlMgr, dssObjectMappingMgr, commonOutboundMgr, miscOrderMgr, locationMgr)
        {
            this.criteriaMgr = criteriaMgr;
            this.commonOutboundMgr = commonOutboundMgr;
            this.dssoutcontrolmgr = dssOutboundControlMgr;
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override IList<DssExportHistory> ExtractOutboundData(DssOutboundControl dssOutboundControl)
        {
            IList result = commonOutboundMgr.ExtractOutboundDataFromLocationTransaction(dssOutboundControl,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP, MatchMode.Exact, true);

            //mod by djin 2012-09-15
            // IList<DssExportHistory> list = this.ConvertList(result, dssOutboundControl);
            IList<DssExportHistory> list = this.ConvertList(result, dssOutboundControl, true);//10-15

            //去除固定科目的Iss-unp
            string[] delCCode = { "0000" };
            DetachedCriteria subjectList = DetachedCriteria.For(typeof(SubjectList));
            subjectList.Add(Expression.In("CostCenterCode", delCCode));
            subjectList.Add(Expression.Eq("SubjectCode", "11111111"));
            IList subjectListCode = criteriaMgr.FindAll(subjectList);
            var miscoOrder = (from l in list select l.KeyCode).Distinct().ToList();
            DetachedCriteria misOrderCriteria = DetachedCriteria.For(typeof(MiscOrder));//需要删除的miscorder
            misOrderCriteria.Add(Expression.In("OrderNo", miscoOrder));
            misOrderCriteria.Add(Expression.In("SubjectList", subjectListCode));
            IList<MiscOrder> miscOrderList = criteriaMgr.FindAll<MiscOrder>(misOrderCriteria);
            IList<DssExportHistory> delList = (from l in list join n in miscOrderList on l.KeyCode equals n.OrderNo select l).ToList();
            foreach (DssExportHistory dss in list)
            {
                if (delList.Contains(dss))
                    dss.Comments = "DEL";
            }
            //mod end
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Qty = -item.Qty;//修正数量

                }
            }

            return list;
        }
    }
}
