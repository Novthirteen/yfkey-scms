using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MRP;
using com.Sconit.Persistence.MRP;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Ext.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class FlatBomBaseMgr : SessionBase, IFlatBomBaseMgr
    {
        public IFlatBomDao entityDao;

        public FlatBomBaseMgr(IGenericMgr genericMgr,
            IBomDetailMgr bomDetailMgr,
            IUomConversionMgr uomConversionMgr,
            IFlatBomDao entityDao)
        {
            this.entityDao = entityDao;
        }

        public FlatBomBaseMgr(IFlatBomDao entityDao)
        {
            this.entityDao = entityDao;
        }
        
        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlatBom(FlatBom entity)
        {
            entityDao.CreateFlatBom(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlatBom LoadFlatBom(Int32 id)
        {
            return entityDao.LoadFlatBom(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlatBom> GetAllFlatBom()
        {
            return entityDao.GetAllFlatBom();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlatBom(FlatBom entity)
        {
            entityDao.UpdateFlatBom(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlatBom(Int32 id)
        {
            entityDao.DeleteFlatBom(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlatBom(FlatBom entity)
        {
            entityDao.DeleteFlatBom(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlatBom(IList<Int32> pkList)
        {
            entityDao.DeleteFlatBom(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlatBom(IList<FlatBom> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlatBom(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
