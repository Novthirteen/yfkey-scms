using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Persistence.MasterData
{
    public interface IAddressDao : IAddressBaseDao
    {
        #region Customized Methods

        ShipAddress LoadShipAddress(string code);

        BillAddress LoadBillAddress(string code);

        void DeleteAddressByParent(String parentCode);

        #endregion Customized Methods
    }
}