using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IPlannedBillBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePlannedBill(PlannedBill entity);

        PlannedBill LoadPlannedBill(Int32 id);
  
        IList<PlannedBill> GetAllPlannedBill();
  
        void UpdatePlannedBill(PlannedBill entity);
        
        void DeletePlannedBill(Int32 id);
    
        void DeletePlannedBill(PlannedBill entity);
    
        void DeletePlannedBill(IList<Int32> pkList);
    
        void DeletePlannedBill(IList<PlannedBill> entityList);    
        #endregion Method Created By CodeSmith
    }
}
