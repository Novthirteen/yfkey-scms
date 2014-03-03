using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.View;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.View
{
    public interface ILeanEngineViewBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLeanEngineView(LeanEngineView entity);

        LeanEngineView LoadLeanEngineView(Int32 flowDetId);
  
        IList<LeanEngineView> GetAllLeanEngineView();
  
        void UpdateLeanEngineView(LeanEngineView entity);
        
        void DeleteLeanEngineView(Int32 flowDetId);
    
        void DeleteLeanEngineView(LeanEngineView entity);
    
        void DeleteLeanEngineView(IList<Int32> pkList);
    
        void DeleteLeanEngineView(IList<LeanEngineView> entityList);    
        #endregion Method Created By CodeSmith
    }
}
