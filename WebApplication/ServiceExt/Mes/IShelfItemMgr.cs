using System;
using com.Sconit.Entity.Mes;


//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes
{
    public interface IShelfItemMgr : IShelfItemBaseMgr
    {
        #region Customized Methods

        ShelfItem LoadShelfItem(string shelfCode, string itemCode);

        #endregion Customized Methods
    }
}
