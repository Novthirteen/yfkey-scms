using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ICurrencyBaseDao
    {
        #region Method Created By CodeSmith

        void CreateCurrency(Currency entity);

        Currency LoadCurrency(String code);
  
        IList<Currency> GetAllCurrency();
  
        void UpdateCurrency(Currency entity);
        
        void DeleteCurrency(String code);
    
        void DeleteCurrency(Currency entity);
    
        void DeleteCurrency(IList<String> pkList);
    
        void DeleteCurrency(IList<Currency> entityList);    
        #endregion Method Created By CodeSmith
    }
}
