
//TODO: Add other using statements here.

using com.Sconit.Entity.MasterData;
namespace com.Sconit.Service.MasterData
{
    public interface IPriceListMgr : IPriceListBaseMgr
    {
        #region Customized Methods
        PriceList LoadPriceList(string code, bool includeDetail);

        #endregion Customized Methods
    }
}
