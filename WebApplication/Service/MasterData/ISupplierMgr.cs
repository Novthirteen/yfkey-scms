using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ISupplierMgr : ISupplierBaseMgr
    {
        #region Customized Methods

        IList<Supplier> GetSupplier(string userCode, bool includeInactive);

        IList<Supplier> GetSupplier(string userCode);

        void CreateSupplier(Supplier entity, User currentUser);

        Supplier CheckAndLoadSupplier(string supplierCode);

        #endregion Customized Methods
    }
}
