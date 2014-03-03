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
    public class NamedQueryBaseMgr : SessionBase, INamedQueryBaseMgr
    {
        protected INamedQueryDao entityDao;
        
        public NamedQueryBaseMgr(INamedQueryDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires, IsolationMode.ReadUncommitted)]
        public virtual void CreateNamedQuery(NamedQuery entity)
        {
            entityDao.CreateNamedQuery(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual NamedQuery LoadNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName)
        {
            return entityDao.LoadNamedQuery(user, queryName);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual NamedQuery LoadNamedQuery(String userCode, String queryName)
        {
            return entityDao.LoadNamedQuery(userCode, queryName);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<NamedQuery> GetAllNamedQuery()
        {
            return entityDao.GetAllNamedQuery();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateNamedQuery(NamedQuery entity)
        {
            entityDao.UpdateNamedQuery(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNamedQuery(com.Sconit.Entity.MasterData.User user, String queryName)
        {
            entityDao.DeleteNamedQuery(user, queryName);
        }
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNamedQuery(String userCode, String queryName)
        {
            entityDao.DeleteNamedQuery(userCode, queryName);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNamedQuery(NamedQuery entity)
        {
            entityDao.DeleteNamedQuery(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteNamedQuery(IList<NamedQuery> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteNamedQuery(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
