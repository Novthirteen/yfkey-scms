using System;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;
using com.Sconit.Entity.Exception;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class BomMgr : BomBaseMgr, IBomMgr
    {
        private IItemMgr itemMgr;
        public BomMgr(IBomDao entityDao, IItemMgr itemMgr)
            : base(entityDao)
        {
            this.itemMgr = itemMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public Bom LoadBom(String code, bool includeDetail)
        {
            Bom bom  = this.LoadBom(code);
            if (bom != null && includeDetail && bom.BomDetails != null && bom.BomDetails.Count > 0)
            {
                
            }

            return bom;
        }

        [Transaction(TransactionMode.Unspecified)]
        public string FindBomCode(string itemCode)
        {
            Item item = this.itemMgr.LoadItem(itemCode);
            return FindBomCode(item);
        }

        [Transaction(TransactionMode.Unspecified)]
        public string FindBomCode(Item item)
        {
            //默认用Item上的BomCode，如果Item上面没有设置Bom，直接用ItemCode作为BomCode去找
            return (item.Bom != null ? item.Bom.Code : item.Code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public Bom CheckAndLoadBom(string bomCode)
        {
            Bom bom = this.LoadBom(bomCode);
            if (bom == null)
            {
                throw new BusinessErrorException("Bom.Error.BomCodeNotExist", bomCode);
            }

            return bom;
        }
        #endregion Customized Methods
    }
}