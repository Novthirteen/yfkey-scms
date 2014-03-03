using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IBillDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBillDetail(BillDetail entity);

        BillDetail LoadBillDetail(Int32 id);
  
        IList<BillDetail> GetAllBillDetail();
  
        void UpdateBillDetail(BillDetail entity);
        
        void DeleteBillDetail(Int32 id);
    
        void DeleteBillDetail(BillDetail entity);
    
        void DeleteBillDetail(IList<Int32> pkList);
    
        void DeleteBillDetail(IList<BillDetail> entityList);

        BillDetail LoadBillDetail(com.Sconit.Entity.MasterData.Bill billNo, com.Sconit.Entity.MasterData.ActingBill actingBill);

        void DeleteBillDetail(String billNoBillNo, Int32 actingBillId);

        BillDetail LoadBillDetail(String billNoBillNo, Int32 actingBillId);
        #endregion Method Created By CodeSmith
    }
}
