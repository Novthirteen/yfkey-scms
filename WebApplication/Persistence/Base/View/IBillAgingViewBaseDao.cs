using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IBillAgingViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBillAgingView(BillAgingView entity);

        BillAgingView LoadBillAgingView(Int32 id);
  
        IList<BillAgingView> GetAllBillAgingView();
  
        void UpdateBillAgingView(BillAgingView entity);

        void DeleteBillAgingView(Int32 id);
    
        void DeleteBillAgingView(BillAgingView entity);
    
        void DeleteBillAgingView(IList<Int32> pkList);
    
        void DeleteBillAgingView(IList<BillAgingView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
