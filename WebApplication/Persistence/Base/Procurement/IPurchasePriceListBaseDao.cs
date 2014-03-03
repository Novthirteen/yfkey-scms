using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Procurement;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Procurement
{
    public interface IPurchasePriceListBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePurchasePriceList(PurchasePriceList entity);

        PurchasePriceList LoadPurchasePriceList(String code);
  
        IList<PurchasePriceList> GetAllPurchasePriceList();
  
        IList<PurchasePriceList> GetAllPurchasePriceList(bool includeInactive);
  
        void UpdatePurchasePriceList(PurchasePriceList entity);
        
        void DeletePurchasePriceList(String code);
    
        void DeletePurchasePriceList(PurchasePriceList entity);
    
        void DeletePurchasePriceList(IList<String> pkList);
    
        void DeletePurchasePriceList(IList<PurchasePriceList> entityList);    
        #endregion Method Created By CodeSmith
    }
}
