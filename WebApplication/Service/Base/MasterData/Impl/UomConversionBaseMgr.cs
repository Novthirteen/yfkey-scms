using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MasterData;
using com.Sconit.Persistence.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData.Impl
{
    [Transactional]
    public class UomConversionBaseMgr : SessionBase, IUomConversionBaseMgr
    {
        protected IUomConversionDao entityDao;
        
        public UomConversionBaseMgr(IUomConversionDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateUomConversion(UomConversion entity)
        {
            entityDao.CreateUomConversion(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual UomConversion LoadUomConversion(Int32 id)
        {
            return entityDao.LoadUomConversion(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual UomConversion LoadUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom)
        {
            return entityDao.LoadUomConversion(item, alterUom, baseUom);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual UomConversion LoadUomConversion(String itemCode, String alterUomCode, String baseUomCode)
        {
            return entityDao.LoadUomConversion(itemCode, alterUomCode, baseUomCode);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<UomConversion> GetAllUomConversion()
        {
            return entityDao.GetAllUomConversion();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUomConversion(UomConversion entity)
        {
            entityDao.UpdateUomConversion(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUomConversion(Int32 id)
        {
            entityDao.DeleteUomConversion(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUomConversion(com.Sconit.Entity.MasterData.Item item, com.Sconit.Entity.MasterData.Uom alterUom, com.Sconit.Entity.MasterData.Uom baseUom)
        {
            entityDao.DeleteUomConversion(item, alterUom, baseUom);
        }
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUomConversion(String itemCode, String alterUomCode, String baseUomCode)
        {
            entityDao.DeleteUomConversion(itemCode, alterUomCode, baseUomCode);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUomConversion(UomConversion entity)
        {
            entityDao.DeleteUomConversion(entity);
        }
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUomConversion(IList<Int32> pkList)
        {
            entityDao.DeleteUomConversion(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteUomConversion(IList<UomConversion> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteUomConversion(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
