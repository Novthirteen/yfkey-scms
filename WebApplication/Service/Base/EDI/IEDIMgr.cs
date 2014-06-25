using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Batch;
using com.Sconit.Entity.EDI;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.EDI
{
    public interface IEDIMgr
    {
        #region Method Created By CodeSmith

        void RunBat();

        void LoadEDI(User user);

        void TransformationPlan(User user);

        void ShipEDIFordPlan(List<EDIFordPlan> shipEDIFordPlanList, string currentUserCode, Flow currentFlow);

        void ReadEDIFordPlanASN();
        #endregion Method Created By CodeSmith
    }
}
