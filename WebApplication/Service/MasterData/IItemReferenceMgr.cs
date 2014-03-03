using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IItemReferenceMgr : IItemReferenceBaseMgr
    {
        #region Customized Methods

        IList<ItemReference> GetItemReference(string itemCode);

        IList<ItemReference> GetItemReference(Item item, Party party);

        IList<ItemReference> GetItemReference(string itemCode, string partyCode);

        IList<ItemReference> GetItemReference(Item item, Party partyFrom, Party partyTo);

        IList<ItemReference> GetItemReference(string itemCode, string partyFromCode, string partyToCode);

        Item GetItemReferenceByRefItem(string refItemCode, string firstPartyCode, string secondPartyCode);

        string GetItemReferenceByItem(string itemCode, string firstPartyCode, string secondPartyCode);

        IList<ItemReference> GetItemReferenceByParty(string partyFromCode, string partyToCode);

        #endregion Customized Methods
    }
}
