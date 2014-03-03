using System;
using Castle.Services.Transaction;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Mes;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.Mes;
using com.Sconit.Service.Mes.Impl;
using com.Sconit.Persistence.Mes;
using com.Sconit.Service.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class MesBomMgr : MesBomBaseMgr, IMesBomMgr
    {
        private IItemMgr itemMgr;
        public MesBomMgr(IMesBomDao entityDao, IItemMgr itemMgr)
            : base(entityDao)
        {
            this.itemMgr = itemMgr;
        }

        #region Customized Methods
        [Transaction(TransactionMode.Unspecified)]
        public MesBom LoadBom(String code, bool includeDetail)
        {
            MesBom bom = this.LoadBom(code);
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
        public MesBom CheckAndLoadBom(string bomCode)
        {
            MesBom bom = this.LoadBom(bomCode);
            if (bom == null)
            {
                throw new BusinessErrorException("Bom.Error.BomCodeNotExist", bomCode);
            }

            return bom;
        }
        #endregion Customized Methods
    }
}