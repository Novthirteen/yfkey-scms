using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IPlannedBillViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreatePlannedBillView(PlannedBillView entity);

        PlannedBillView LoadPlannedBillView(Int32 id);
  
        IList<PlannedBillView> GetAllPlannedBillView();
  
        void UpdatePlannedBillView(PlannedBillView entity);
        
        void DeletePlannedBillView(Int32 id);
    
        void DeletePlannedBillView(PlannedBillView entity);
    
        void DeletePlannedBillView(IList<Int32> pkList);
    
        void DeletePlannedBillView(IList<PlannedBillView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
