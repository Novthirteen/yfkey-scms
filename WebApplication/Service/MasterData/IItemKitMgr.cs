using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IItemKitMgr : IItemKitBaseMgr
    {
        #region Customized Methods

        IList<ItemKit> GetChildItemKit(string parentItemCode);

        IList<ItemKit> GetChildItemKit(string parentItemCode, bool includeInActive);

        IList<ItemKit> GetChildItemKit(Item parentItem);

        IList<ItemKit> GetChildItemKit(Item parentItem, bool includeInActive);

        #endregion Customized Methods
    }
}
