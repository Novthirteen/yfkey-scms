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

namespace com.Sconit.Service.Dss.Impl
{
    /// <summary>
    /// QAD:7.9.8
    /// </summary>
    public class SoBillOutboundMgr : SoBillOutboundBaseMgr
    {
        public SoBillOutboundMgr(INumberControlMgr numberControlMgr,
            IDssExportHistoryMgr dssExportHistoryMgr,
            ICriteriaMgr criteriaMgr,
            IDssOutboundControlMgr dssOutboundControlMgr,
            IDssObjectMappingMgr dssObjectMappingMgr,
            ICommonOutboundMgr commonOutboundMgr,
            ILocationMgr locationMgr)
            : base(numberControlMgr, dssExportHistoryMgr, criteriaMgr, dssOutboundControlMgr, dssObjectMappingMgr, commonOutboundMgr,locationMgr)
        {
        }

        protected override object Serialize(object obj)
        {
            DssExportHistory dssExportHistory = (DssExportHistory)obj;
            DateTime effDate = dssExportHistory.EffectiveDate.HasValue ? dssExportHistory.EffectiveDate.Value : DateTime.Today;

            string[] line1 = new string[] 
            { 
                DssHelper.GetEventValue(dssExportHistory.EventCode),
                dssExportHistory.PartyFrom,//QAD:Site
                dssExportHistory.KeyCode,
                dssExportHistory.PartyTo,//客户
                null,
                null,
                null
            };

            string[] line2 = new string[]
            {
                DssHelper.GetEventValue(dssExportHistory.EventCode),
                dssExportHistory.Item,//零件号
                null,
                null,
                dssExportHistory.Qty.ToString("0.########"),//数量
                dssExportHistory.PartyFrom,//QAD:Site
                dssExportHistory.Location//客户库位
            };

            string[][] data = new string[][] { line1, line2 };

            return new object[] { effDate, data };
        }


    }
}
