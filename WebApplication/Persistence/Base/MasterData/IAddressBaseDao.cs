using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IAddressBaseDao
    {
        #region Method Created By CodeSmith

        void CreateAddress(Address entity);

        Address LoadAddress(String code);
  
        IList<Address> GetAllAddress();
  
        IList<Address> GetAllAddress(bool includeInactive);
  
        void UpdateAddress(Address entity);
        
        void DeleteAddress(String code);
    
        void DeleteAddress(Address entity);
    
        void DeleteAddress(IList<String> pkList);
    
        void DeleteAddress(IList<Address> entityList);    
        #endregion Method Created By CodeSmith
    }
}
