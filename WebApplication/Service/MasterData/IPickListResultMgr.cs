using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IPickListResultMgr : IPickListResultBaseMgr
    {
        #region Customized Methods

        IList<PickListResult> GetPickListResult(int pickListDetailId);

        IList<PickListResult> GetPickListResult(PickListDetail pickListDetail);

        IList<PickListResult> GetPickListResult(string pickListNo);

        IList<PickListResult> GetPickListResult(string locationCode, string itemCode, decimal? unitCount, string uomCode, string[] status);

        IList<PickListResult> GetPickListResult(string[] locationCodes, string[] itemCodes, decimal? unitCount, string uomCode, string[] status);

        IList<PickListResult> GetPickListResult(string[] locationCodes, string[] itemCodes, decimal? unitCount, string uomCode, string[] status, bool isGroup);

        #endregion Customized Methods
    }
}
