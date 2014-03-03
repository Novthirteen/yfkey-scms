using System;
using com.Sconit.Entity.Mes;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IProductLineUserMgr : IProductLineUserBaseMgr
    {
        #region Customized Methods

        ProductLineUser LoadProductLineUser(string userCode, string flowCode);

        #endregion Customized Methods
    }
}
