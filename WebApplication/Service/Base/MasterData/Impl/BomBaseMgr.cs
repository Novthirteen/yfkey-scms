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
    public class BomBaseMgr : SessionBase, IBomBaseMgr
    {
        protected IBomDao entityDao;
        
        public BomBaseMgr(IBomDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBom(Bom entity)
        {
            entityDao.CreateBom(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Bom LoadBom(String code)
        {
            return entityDao.LoadBom(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Bom> GetAllBom()
        {
            return entityDao.GetAllBom(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Bom> GetAllBom(bool includeInactive)
        {
            return entityDao.GetAllBom(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBom(Bom entity)
        {
            entityDao.UpdateBom(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(String code)
        {
            entityDao.DeleteBom(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(Bom entity)
        {
            entityDao.DeleteBom(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(IList<String> pkList)
        {
            entityDao.DeleteBom(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(IList<Bom> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteBom(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
