using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IShipAddressBaseDao
    {
        #region Method Created By CodeSmith

        void CreateShipAddress(ShipAddress entity);

        ShipAddress LoadShipAddress(String code);
  
        IList<ShipAddress> GetAllShipAddress();
  
        IList<ShipAddress> GetAllShipAddress(bool includeInactive);
  
        void UpdateShipAddress(ShipAddress entity);
        
        void DeleteShipAddress(String code);
    
        void DeleteShipAddress(ShipAddress entity);
    
        void DeleteShipAddress(IList<String> pkList);
    
        void DeleteShipAddress(IList<ShipAddress> entityList);    
        #endregion Method Created By CodeSmith
    }
}
