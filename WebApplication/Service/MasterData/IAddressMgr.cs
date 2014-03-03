using System.Collections;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IAddressMgr : IAddressBaseMgr
    {
        #region Customized Methods

        ShipAddress LoadShipAddress(string code);

        BillAddress LoadBillAddress(string code);

        IList GetAllBillAddress();

        IList GetAllBillAddress(bool includeInactive);

        IList GetBillAddress(string partyCode);

        IList GetBillAddress(string partyCode, bool includeInactive);

        IList GetBillAddress(Party party);

        IList GetBillAddress(Party party, bool includeInactive);

        IList GetAllShipAddress();

        IList GetAllShipAddress(bool includeInactive);

        IList GetShipAddress(string partyCode);

        IList GetShipAddress(string partyCode, bool includeInactive);

        IList GetShipAddress(Party party);

        IList GetShipAddress(Party party, bool includeInactive);

        void DeleteAddressByParent(string code);

        #endregion Customized Methods
    }
}
