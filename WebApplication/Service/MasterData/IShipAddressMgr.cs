using com.Sconit.Entity.MasterData;
namespace com.Sconit.Service.MasterData
{
    public interface IShipAddressMgr : IShipAddressBaseMgr
    {
        #region Customized Methods

        ShipAddress GetDefaultShipAddress(string partyCode);

        ShipAddress GetDefaultShipAddress(Party party);

        #endregion Customized Methods
    }
}
