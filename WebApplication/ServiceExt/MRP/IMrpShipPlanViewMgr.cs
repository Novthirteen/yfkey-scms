using System;
using com.Sconit.Entity.MRP;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using System.Collections;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface IMrpShipPlanViewMgr : IMrpShipPlanViewBaseMgr
    {
        #region Customized Methods

        //added by williamlu@esteering.cn
        //2012/5
        IList<MrpShipPlanView> GetMrpShipPlanViewsBB(string flowCode, string locCode, string itemCode, List<string> itemCodes, DateTime effectiveDate, DateTime? winDate, DateTime? startDate);
        //added end

        IList<MrpShipPlanView> GetMrpShipPlanViews(string flowCode, string locCode, string itemCode, DateTime effectiveDate, DateTime? winDate, DateTime? startDate);

        ScheduleView TransferMrpShipPlanViews2ScheduleView(IList<MrpShipPlanView> mrpShipPlanViews, IList<ExpectTransitInventoryView> expectTransitInventoryViews,
             string locOrFlow, string winOrStartTime);

        #endregion Customized Methods
    }
}
