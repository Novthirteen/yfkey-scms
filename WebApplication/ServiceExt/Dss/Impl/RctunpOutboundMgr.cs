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

namespace com.Sconit.Service.Dss.Impl
{
    public class RctunpOutboundMgr : UnpOutboundBaseMgr
    {
        private ICriteriaMgr criteriaMgr;
        private ICommonOutboundMgr commonOutboundMgr;

        public RctunpOutboundMgr(INumberControlMgr numberControlMgr,
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
        }

        [Transaction(TransactionMode.Unspecified)]
        protected override IList<DssExportHistory> ExtractOutboundData(DssOutboundControl dssOutboundControl)
        {
            IList result = commonOutboundMgr.ExtractOutboundDataFromLocationTransaction(dssOutboundControl,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_UNP, MatchMode.Exact);

            return this.ConvertList(result, dssOutboundControl);
        }
    }
}
