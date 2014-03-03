using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ICycleCountResultMgr : ICycleCountResultBaseMgr
    {
        #region Customized Methods

        IList<CycleCountResult> GetCycleCountResult(string orderNo);

        void ClearOldCycleCountResult(string orderNo);

        void CreateCycleCountResult(IList<CycleCountResult> cycleCountResultList);

        void SaveCycleCountResult(string orderNo, IList<CycleCountResult> cycleCountResultList);

        void UpdateCycleCountResult(IList<CycleCountResult> cycleCountResultList);

        #endregion Customized Methods
    }
}
