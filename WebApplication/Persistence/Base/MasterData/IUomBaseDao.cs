using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IUomBaseDao
    {
        #region Method Created By CodeSmith

        void CreateUom(Uom entity);

        Uom LoadUom(String code);
  
        IList<Uom> GetAllUom();
  
        void UpdateUom(Uom entity);
        
        void DeleteUom(String code);
    
        void DeleteUom(Uom entity);
    
        void DeleteUom(IList<String> pkList);
    
        void DeleteUom(IList<Uom> entityList);    
        #endregion Method Created By CodeSmith
    }
}
