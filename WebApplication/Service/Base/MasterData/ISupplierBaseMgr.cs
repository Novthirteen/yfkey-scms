using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ISupplierBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateSupplier(Supplier entity);

        Supplier LoadSupplier(String code);

        IList<Supplier> GetAllSupplier();
    
        IList<Supplier> GetAllSupplier(bool includeInactive);
      
        void UpdateSupplier(Supplier entity);

        void DeleteSupplier(String code);
    
        void DeleteSupplier(Supplier entity);
    
        void DeleteSupplier(IList<String> pkList);
    
        void DeleteSupplier(IList<Supplier> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
