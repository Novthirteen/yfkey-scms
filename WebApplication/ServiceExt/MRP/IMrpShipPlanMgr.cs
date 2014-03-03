using System;
using com.Sconit.Entity.MRP;
using System.Collections.Generic;
using System.Collections;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface IMrpShipPlanMgr : IMrpShipPlanBaseMgr
    {
        #region Customized Methods

        IList<MrpShipPlan> GetMrpShipPlans(string flowCode, string locCode, string itemCode, DateTime effectiveDate, DateTime? winDate, DateTime? startDate);

        //added by williamlu@esteering.cn
        //2012/5
        IList<MrpShipPlan> GetMrpShipPlansBB(string flowCode, string locCode, string itemCode, List<string> itemCodes, DateTime effectiveDate, DateTime? winDate, DateTime? startDate);
        //added end

        void UpdateMrpShipPlan(IList<MrpShipPlan> mrpShipPlans);

        #endregion Customized Methods
    }
}