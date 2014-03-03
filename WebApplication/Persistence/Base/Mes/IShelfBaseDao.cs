using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Mes;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Mes
{
    public interface IShelfBaseDao
    {
        #region Method Created By CodeSmith

        void CreateShelf(Shelf entity);

        Shelf LoadShelf(String code);
  
        IList<Shelf> GetAllShelf();

        IList<Shelf> GetAllShelf(bool includeInactive);
  
        void UpdateShelf(Shelf entity);
        
        void DeleteShelf(String code);
    
        void DeleteShelf(Shelf entity);
    
        void DeleteShelf(IList<String> pkList);
    
        void DeleteShelf(IList<Shelf> entityList);    
        #endregion Method Created By CodeSmith
    }
}
