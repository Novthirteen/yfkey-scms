using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPriceListDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePriceListDetail(PriceListDetail entity);

        PriceListDetail LoadPriceListDetail(Int32 id);
  
        IList<PriceListDetail> GetAllPriceListDetail();
  
        void UpdatePriceListDetail(PriceListDetail entity);
        
        void DeletePriceListDetail(Int32 id);
    
        void DeletePriceListDetail(PriceListDetail entity);
    
        void DeletePriceListDetail(IList<Int32> pkList);
    
        void DeletePriceListDetail(IList<PriceListDetail> entityList);

        PriceListDetail LoadPriceListDetail(com.Sconit.Entity.MasterData.PriceList priceList, DateTime startDate, com.Sconit.Entity.MasterData.Item item, Currency currency);

        void DeletePriceListDetail(String priceListCode, DateTime startDate, String itemCode, String currencyCode);

        PriceListDetail LoadPriceListDetail(String priceListCode, DateTime startDate, String itemCode, String currencyCode);
        #endregion Method Created By CodeSmith
    }
}
