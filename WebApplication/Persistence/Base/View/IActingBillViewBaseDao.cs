using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IActingBillViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateActingBillView(ActingBillView entity);

        ActingBillView LoadActingBillView(Int32 id);
  
        IList<ActingBillView> GetAllActingBillView();
  
        void UpdateActingBillView(ActingBillView entity);
        
        void DeleteActingBillView(Int32 id);
    
        void DeleteActingBillView(ActingBillView entity);
    
        void DeleteActingBillView(IList<Int32> pkList);
    
        void DeleteActingBillView(IList<ActingBillView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
