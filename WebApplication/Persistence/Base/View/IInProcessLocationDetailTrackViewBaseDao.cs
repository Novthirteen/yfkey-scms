using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface IInProcessLocationDetailTrackViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity);

        InProcessLocationDetailTrackView LoadInProcessLocationDetailTrackView(Int32 id);
  
        IList<InProcessLocationDetailTrackView> GetAllInProcessLocationDetailTrackView();
  
        void UpdateInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity);
        
        void DeleteInProcessLocationDetailTrackView(Int32 id);
    
        void DeleteInProcessLocationDetailTrackView(InProcessLocationDetailTrackView entity);
    
        void DeleteInProcessLocationDetailTrackView(IList<Int32> pkList);
    
        void DeleteInProcessLocationDetailTrackView(IList<InProcessLocationDetailTrackView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
