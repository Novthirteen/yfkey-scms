using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IBillBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBill(Bill entity);

        Bill LoadBill(String billNo);
  
        IList<Bill> GetAllBill();
  
        void UpdateBill(Bill entity);
        
        void DeleteBill(String billNo);
    
        void DeleteBill(Bill entity);
    
        void DeleteBill(IList<String> pkList);
    
        void DeleteBill(IList<Bill> entityList);    
        #endregion Method Created By CodeSmith
    }
}
