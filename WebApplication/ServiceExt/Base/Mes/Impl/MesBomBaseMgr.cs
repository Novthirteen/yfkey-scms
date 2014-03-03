using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Mes;
using com.Sconit.Entity.Mes;


//TODO: Add other using statements here.

namespace com.Sconit.Service.Mes.Impl
{
    [Transactional]
    public class MesBomBaseMgr : SessionBase, IMesBomBaseMgr
    {
        protected IMesBomDao entityDao;
        
        public MesBomBaseMgr(IMesBomDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateBom(MesBom entity)
        {
            entityDao.CreateBom(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MesBom LoadBom(String code)
        {
            return entityDao.LoadBom(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesBom> GetAllBom()
        {
            return entityDao.GetAllBom(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MesBom> GetAllBom(bool includeInactive)
        {
            return entityDao.GetAllBom(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateBom(MesBom entity)
        {
            entityDao.UpdateBom(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(String code)
        {
            entityDao.DeleteBom(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(MesBom entity)
        {
            entityDao.DeleteBom(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(IList<String> pkList)
        {
            entityDao.DeleteBom(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteBom(IList<MesBom> entityList)
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
