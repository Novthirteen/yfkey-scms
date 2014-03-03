using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPriceListBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePriceList(PriceList entity);

        PriceList LoadPriceList(String code);
  
        IList<PriceList> GetAllPriceList();
  
        IList<PriceList> GetAllPriceList(bool includeInactive);
  
        void UpdatePriceList(PriceList entity);
        
        void DeletePriceList(String code);
    
        void DeletePriceList(PriceList entity);
    
        void DeletePriceList(IList<String> pkList);
    
        void DeletePriceList(IList<PriceList> entityList);    
        #endregion Method Created By CodeSmith
    }
}
