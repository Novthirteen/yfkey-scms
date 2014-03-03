using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IUomMgr : IUomBaseMgr
    {
        #region Customized Methods

        Uom CheckAndLoadUom(string uomCode);

        IList GetUom(string code, string name, string desc);

        IList<Uom> GetItemUom(string itemCode);

        #endregion Customized Methods
    }
}
