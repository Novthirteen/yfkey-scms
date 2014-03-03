using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Distribution;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Distribution
{
    public interface IInProcessLocationTrackBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInProcessLocationTrack(InProcessLocationTrack entity);

        InProcessLocationTrack LoadInProcessLocationTrack(Int32 id);
  
        IList<InProcessLocationTrack> GetAllInProcessLocationTrack();
  
        void UpdateInProcessLocationTrack(InProcessLocationTrack entity);
        
        void DeleteInProcessLocationTrack(Int32 id);
    
        void DeleteInProcessLocationTrack(InProcessLocationTrack entity);
    
        void DeleteInProcessLocationTrack(IList<Int32> pkList);
    
        void DeleteInProcessLocationTrack(IList<InProcessLocationTrack> entityList);    
        #endregion Method Created By CodeSmith
    }
}
