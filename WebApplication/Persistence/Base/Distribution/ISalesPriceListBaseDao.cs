using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Distribution;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Distribution
{
    public interface ISalesPriceListBaseDao
    {
        #region Method Created By CodeSmith

        void CreateSalesPriceList(SalesPriceList entity);

        SalesPriceList LoadSalesPriceList(String code);
  
        IList<SalesPriceList> GetAllSalesPriceList();
  
        IList<SalesPriceList> GetAllSalesPriceList(bool includeInactive);
  
        void UpdateSalesPriceList(SalesPriceList entity);
        
        void DeleteSalesPriceList(String code);
    
        void DeleteSalesPriceList(SalesPriceList entity);
    
        void DeleteSalesPriceList(IList<String> pkList);
    
        void DeleteSalesPriceList(IList<SalesPriceList> entityList);    
        #endregion Method Created By CodeSmith
    }
}
