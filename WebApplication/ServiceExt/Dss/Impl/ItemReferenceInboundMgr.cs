using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Entity;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Service.Dss.Impl
{
    public class ItemReferenceInboundMgr : AbstractInboundMgr
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Log.DssInbound");

        private IItemMgr itemMgr;
        private IItemReferenceMgr itemReferenceMgr;
        private IUserMgr userMgr;
        private IPartyMgr partyMgr;

        private string[] ItemRef2ItemRefFields = new string[] 
            { 
              
            };

        public ItemReferenceInboundMgr(IItemMgr itemMgr,
           IItemReferenceMgr itemReferenceMgr,
            IUserMgr userMgr,
             IPartyMgr partyMgr,
            IDssImportHistoryMgr dssImportHistoryMgr,
            IGenericMgr genericMgr)
            : base(dssImportHistoryMgr, genericMgr)
        {
            this.itemMgr = itemMgr;
            this.itemReferenceMgr = itemReferenceMgr;
            this.userMgr = userMgr;
            this.partyMgr = partyMgr;
        }

        protected override void FillDssImportHistory(string[] lineData, DssImportHistory dssImportHistory)
        {
            if (lineData != null && lineData.Length > 0 && dssImportHistory != null)
            {
                for (int i = 0; i < 4; i++)
                {
                    dssImportHistory[i] = lineData[i].ToUpper();
                }
                if (lineData[1].ToUpper() == "YFK")
                {
                    dssImportHistory.IsActive = false;
                }
            }
        }

        protected override object DeserializeForDelete(DssImportHistory dssImportHistory)
        {
            return this.Deserialize(dssImportHistory, false);
        }

        protected override object DeserializeForCreate(DssImportHistory dssImportHistory)
        {
            return this.Deserialize(dssImportHistory, true);
        }

        private object Deserialize(DssImportHistory dssImportHistory, bool isUpdate)
        {
            ItemReference itemReference = new ItemReference();

            if (isUpdate)
            {
                if (dssImportHistory[1] != null)
                {
                    itemReference.Party = partyMgr.CheckAndLoadParty(dssImportHistory[1]);
                }
                if (dssImportHistory[2] != null)
                {
                    itemReference.ReferenceCode = dssImportHistory[2];
                }
                if (dssImportHistory[3] != null)
                {
                    itemReference.Item = itemMgr.CheckAndLoadItem(dssImportHistory[3]);
                }
            }

            #region 默认值
            itemReference.IsActive = true;
            #endregion

            return itemReference;
        }

        protected override void CreateOrUpdateObject(object obj)
        {

            ItemReference itemReference = (ItemReference)obj;
            
            IList<ItemReference> itemReferenceList = itemReferenceMgr.GetItemReference(itemReference.Item, itemReference.Party);
            ItemReference newItemReference = itemReferenceList.Count != 0? itemReferenceList[0]:null;
            if (newItemReference == null)
            {
                itemReferenceMgr.CreateItemReference(itemReference);
            }
            else
            {
                newItemReference.ReferenceCode = itemReference.ReferenceCode;
                itemReferenceMgr.UpdateItemReference(newItemReference);
            }
        }

        protected override void DeleteObject(object obj)
        {
            ItemReference itemReference = (ItemReference)obj;

            ItemReference newItemReference = itemReferenceMgr.LoadItemReference(itemReference.Item, itemReference.Party, itemReference.ReferenceCode);

            if (newItemReference != null)
            {
                newItemReference.IsActive = false;

                itemReferenceMgr.UpdateItemReference(newItemReference);
            }
        }

    }
}
