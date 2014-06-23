using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MRP
{
    public interface IFlatBomBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFlatBom(FlatBom entity);

        FlatBom LoadFlatBom(Int32 id);
  
        IList<FlatBom> GetAllFlatBom();
  
        void UpdateFlatBom(FlatBom entity);
        
        void DeleteFlatBom(Int32 id);
    
        void DeleteFlatBom(FlatBom entity);
    
        void DeleteFlatBom(IList<Int32> pkList);
    
        void DeleteFlatBom(IList<FlatBom> entityList);    
        #endregion Method Created By CodeSmith
    }
}
