using System;
using System.Collections.Generic;
using com.Sconit.Entity.Mes;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IMesBomDetailMgr : IMesBomDetailBaseMgr
    {
        #region Customized Methods

        List<MesBomDetail> GetBomDetailList(MesBom mesBom);

        List<MesBomDetail> GetBomDetailList(string bomCode, string itemCode);

        IList<MesBomDetail> GetNextLevelBomDetail(string bomCode);

        MesBomDetail GetBomDetail(string bomCode, string itemCode);

        IList<MesBom> GetRelatedBomDetail(MesBomDetail mesBomDetail);

        #endregion Customized Methods
    }
}
