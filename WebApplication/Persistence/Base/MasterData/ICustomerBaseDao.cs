using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ICustomerBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCustomer(Customer entity);

        Customer LoadCustomer(String code);
  
        IList<Customer> GetAllCustomer();
  
        IList<Customer> GetAllCustomer(bool includeInactive);
  
        void UpdateCustomer(Customer entity);
        
        void DeleteCustomer(String code);
    
        void DeleteCustomer(Customer entity);
    
        void DeleteCustomer(IList<String> pkList);
    
        void DeleteCustomer(IList<Customer> entityList);    
        #endregion Method Created By CodeSmith
    }
}
