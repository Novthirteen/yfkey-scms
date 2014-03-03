using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Distribution;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Distribution
{
    public interface IInProcessLocationBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInProcessLocation(InProcessLocation entity);

        InProcessLocation LoadInProcessLocation(String ipNo);
  
        IList<InProcessLocation> GetAllInProcessLocation();
  
        void UpdateInProcessLocation(InProcessLocation entity);
        
        void DeleteInProcessLocation(String ipNo);
    
        void DeleteInProcessLocation(InProcessLocation entity);
    
        void DeleteInProcessLocation(IList<String> pkList);
    
        void DeleteInProcessLocation(IList<InProcessLocation> entityList);    
        #endregion Method Created By CodeSmith
    }
}
