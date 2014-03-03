using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Mes;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Mes
{
    public interface IByMaterialBaseDao
    {
        #region Method Created By CodeSmith

        void CreateByMaterial(ByMaterial entity);

        ByMaterial LoadByMaterial(Int32 id);
  
        IList<ByMaterial> GetAllByMaterial();
  
        void UpdateByMaterial(ByMaterial entity);
        
        void DeleteByMaterial(Int32 id);
    
        void DeleteByMaterial(ByMaterial entity);
    
        void DeleteByMaterial(IList<Int32> pkList);
    
        void DeleteByMaterial(IList<ByMaterial> entityList);    
        #endregion Method Created By CodeSmith
    }
}
