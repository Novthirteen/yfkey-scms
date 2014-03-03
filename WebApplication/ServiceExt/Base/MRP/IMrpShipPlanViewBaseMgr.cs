using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface IMrpShipPlanViewBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateMrpShipPlanView(MrpShipPlanView entity);

        MrpShipPlanView LoadMrpShipPlanView(String flow, String item, String uom, Decimal uC, DateTime startTime, DateTime windowTime, DateTime effDate);

        IList<MrpShipPlanView> GetAllMrpShipPlanView();
    
        void UpdateMrpShipPlanView(MrpShipPlanView entity);

        void DeleteMrpShipPlanView(String flow, String item, String uom, Decimal uC, DateTime startTime, DateTime windowTime, DateTime effDate);
    
        void DeleteMrpShipPlanView(MrpShipPlanView entity);
    
        void DeleteMrpShipPlanView(IList<MrpShipPlanView> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
