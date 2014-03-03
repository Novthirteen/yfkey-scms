using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Procurement;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Procurement
{
    public interface IAutoOrderTrackBaseDao
    {
        #region Method Created By CodeSmith

        void CreateAutoOrderTrack(AutoOrderTrack entity);

        AutoOrderTrack LoadAutoOrderTrack(Int32 id);
  
        IList<AutoOrderTrack> GetAllAutoOrderTrack();
  
        void UpdateAutoOrderTrack(AutoOrderTrack entity);
        
        void DeleteAutoOrderTrack(Int32 id);
    
        void DeleteAutoOrderTrack(AutoOrderTrack entity);
    
        void DeleteAutoOrderTrack(IList<Int32> pkList);
    
        void DeleteAutoOrderTrack(IList<AutoOrderTrack> entityList);    
        #endregion Method Created By CodeSmith
    }
}
