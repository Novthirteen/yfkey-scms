using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Mes;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IShelfItemBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateShelfItem(ShelfItem entity);

        ShelfItem LoadShelfItem(Int32 id);

        IList<ShelfItem> GetAllShelfItem();
    
        void UpdateShelfItem(ShelfItem entity);

        void DeleteShelfItem(Int32 id);
    
        void DeleteShelfItem(ShelfItem entity);
    
        void DeleteShelfItem(IList<Int32> pkList);
    
        void DeleteShelfItem(IList<ShelfItem> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
