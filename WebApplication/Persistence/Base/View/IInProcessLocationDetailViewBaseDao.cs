using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IInProcessLocationDetailViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInProcessLocationDetailView(InProcessLocationDetailView entity);

        InProcessLocationDetailView LoadInProcessLocationDetailView(Int32 id);
  
        IList<InProcessLocationDetailView> GetAllInProcessLocationDetailView();
  
        void UpdateInProcessLocationDetailView(InProcessLocationDetailView entity);
        
        void DeleteInProcessLocationDetailView(Int32 id);
    
        void DeleteInProcessLocationDetailView(InProcessLocationDetailView entity);
    
        void DeleteInProcessLocationDetailView(IList<Int32> pkList);
    
        void DeleteInProcessLocationDetailView(IList<InProcessLocationDetailView> entityList);    
        
        InProcessLocationDetailView LoadInProcessLocationDetailView(com.Sconit.Entity.Distribution.InProcessLocation inProcessLocation);
    
        void DeleteInProcessLocationDetailView(String inProcessLocationIpNo);
        
        InProcessLocationDetailView LoadInProcessLocationDetailView(String inProcessLocationIpNo);
        
        void DeleteInProcessLocationDetailView(IList<com.Sconit.Entity.Distribution.InProcessLocation> UniqueList);   
        #endregion Method Created By CodeSmith
    }
}
