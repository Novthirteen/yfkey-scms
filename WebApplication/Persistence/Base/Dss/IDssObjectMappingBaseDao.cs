using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssObjectMappingBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssObjectMapping(DssObjectMapping entity);

        DssObjectMapping LoadDssObjectMapping(Int32 id);
  
        IList<DssObjectMapping> GetAllDssObjectMapping();
  
        void UpdateDssObjectMapping(DssObjectMapping entity);
        
        void DeleteDssObjectMapping(Int32 id);
    
        void DeleteDssObjectMapping(DssObjectMapping entity);
    
        void DeleteDssObjectMapping(IList<Int32> pkList);
    
        void DeleteDssObjectMapping(IList<DssObjectMapping> entityList);    
        #endregion Method Created By CodeSmith
    }
}
