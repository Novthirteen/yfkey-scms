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
    public class ClientBaseMgr : SessionBase, IClientBaseMgr
    {
        protected IClientDao entityDao;
        
        public ClientBaseMgr(IClientDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateClient(Client entity)
        {
            entityDao.CreateClient(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Client LoadClient(String clientId)
        {
            return entityDao.LoadClient(clientId);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Client> GetAllClient()
        {
            return entityDao.GetAllClient(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Client> GetAllClient(bool includeInactive)
        {
            return entityDao.GetAllClient(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateClient(Client entity)
        {
            entityDao.UpdateClient(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClient(String clientId)
        {
            entityDao.DeleteClient(clientId);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClient(Client entity)
        {
            entityDao.DeleteClient(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClient(IList<String> pkList)
        {
            entityDao.DeleteClient(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClient(IList<Client> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteClient(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
