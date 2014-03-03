using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IBillAddressBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBillAddress(BillAddress entity);

        BillAddress LoadBillAddress(String code);
  
        IList<BillAddress> GetAllBillAddress();
  
        IList<BillAddress> GetAllBillAddress(bool includeInactive);
  
        void UpdateBillAddress(BillAddress entity);
        
        void DeleteBillAddress(String code);
    
        void DeleteBillAddress(BillAddress entity);
    
        void DeleteBillAddress(IList<String> pkList);
    
        void DeleteBillAddress(IList<BillAddress> entityList);    
        #endregion Method Created By CodeSmith
    }
}
