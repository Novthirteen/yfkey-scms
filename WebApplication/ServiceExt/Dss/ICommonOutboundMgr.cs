using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Dss;
using System.Collections;
using NHibernate.Expression;

namespace com.Sconit.Service.Dss
{
    public interface ICommonOutboundMgr
    {
        bool CheckBillStatusValid(string billNo);

        OrderLocationTransaction GetOrderLocationTransactionInfo(string orderNo, string itemCode, string ioType);

        IList ExtractOutboundDataFromLocationTransaction(DssOutboundControl dssOutboundControl, string transType, MatchMode matchMode);

        IList ExtractOutboundDataFromLocationTransaction(DssOutboundControl dssOutboundControl, string transType, MatchMode matchMode,bool includeRefLoc);

        DssExportHistory ConvertLocationTransactionToDssExportHistory(object obj, DssOutboundControl dssOutboundControl);
    }
}
