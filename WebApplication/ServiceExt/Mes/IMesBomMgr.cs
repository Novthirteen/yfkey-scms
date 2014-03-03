using System;
using com.Sconit.Entity.Mes;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IMesBomMgr : IMesBomBaseMgr
    {
        #region Customized Methods

        MesBom LoadBom(String code, bool includeDetail);

        string FindBomCode(string itemCode);

        string FindBomCode(Item item);

        MesBom CheckAndLoadBom(string bomCode);

        #endregion Customized Methods
    }
}
