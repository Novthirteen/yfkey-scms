using Castle.Services.Transaction;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class PriceListMgr : PriceListBaseMgr, IPriceListMgr
    {
        public PriceListMgr(IPriceListDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.
        [Transaction(TransactionMode.Unspecified)]
        public PriceList LoadPriceList(string code, bool includeDetail)
        {
            PriceList priceList = this.LoadPriceList(code);
            if (priceList != null && includeDetail && priceList.PriceListDetails != null && priceList.PriceListDetails.Count > 0)
            {

            }
            return priceList;
        }

        #endregion Customized Methods
    }
}