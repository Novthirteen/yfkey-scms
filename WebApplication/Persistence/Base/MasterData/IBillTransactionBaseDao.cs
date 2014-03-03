using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IBillTransactionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBillTransaction(BillTransaction entity);

        BillTransaction LoadBillTransaction(Int32 id);
  
        IList<BillTransaction> GetAllBillTransaction();
  
        void UpdateBillTransaction(BillTransaction entity);
        
        void DeleteBillTransaction(Int32 id);
    
        void DeleteBillTransaction(BillTransaction entity);
    
        void DeleteBillTransaction(IList<Int32> pkList);
    
        void DeleteBillTransaction(IList<BillTransaction> entityList);    
        #endregion Method Created By CodeSmith
    }
}
