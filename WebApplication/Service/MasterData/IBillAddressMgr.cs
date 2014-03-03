
//TODO: Add other using statements here.

using com.Sconit.Entity.MasterData;
namespace com.Sconit.Service.MasterData
{
    public interface IBillAddressMgr : IBillAddressBaseMgr
    {
        #region Customized Methods

        BillAddress GetDefaultBillAddress(string party);

        #endregion Customized Methods
    }
}
