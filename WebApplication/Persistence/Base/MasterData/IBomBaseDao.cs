using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IBomBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBom(Bom entity);

        Bom LoadBom(String code);
  
        IList<Bom> GetAllBom();
  
        IList<Bom> GetAllBom(bool includeInactive);
  
        void UpdateBom(Bom entity);
        
        void DeleteBom(String code);
    
        void DeleteBom(Bom entity);
    
        void DeleteBom(IList<String> pkList);
    
        void DeleteBom(IList<Bom> entityList);    
        #endregion Method Created By CodeSmith
    }
}
