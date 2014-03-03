using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IHuBaseDao
    {
        #region Method Created By CodeSmith

        void CreateHu(Hu entity);

        Hu LoadHu(String huId);
  
        IList<Hu> GetAllHu();
  
        void UpdateHu(Hu entity);
        
        void DeleteHu(String huId);
    
        void DeleteHu(Hu entity);
    
        void DeleteHu(IList<String> pkList);
    
        void DeleteHu(IList<Hu> entityList);    
        #endregion Method Created By CodeSmith
    }
}
