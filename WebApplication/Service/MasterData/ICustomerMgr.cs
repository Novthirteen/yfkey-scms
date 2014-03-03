using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ICustomerMgr : ICustomerBaseMgr
    {
        #region Customized Methods

        IList<Customer> GetCustomer(string userCode, bool includeInactive);

        IList<Customer> GetCustomer(string userCode);

        void CreateCustomer(Customer entity, User currentUser);

        #endregion Customized Methods
    }
}
