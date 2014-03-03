using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IItemMgr : IItemBaseMgr
    {
        #region Customized Methods

        IList<Item> GetCacheAllItem();

        IList<Item> GetPMItem();

        IList<Item> GetItem(DateTime lastModifyDate, int firstRow, int maxRows);

        IList<Item> GetItem(IList<string> itemCodeList);

        int GetItemCount(DateTime lastModifyDate);

        Item CheckAndLoadItem(string itemCode);

        void UpdateItem(Item item, User user);

        string GetCacheAllItemString();
        #endregion Customized Methods
    }
}
