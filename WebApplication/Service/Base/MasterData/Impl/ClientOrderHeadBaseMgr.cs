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
    public class ClientOrderHeadBaseMgr : SessionBase, IClientOrderHeadBaseMgr
    {
        protected IClientOrderHeadDao entityDao;
        
        public ClientOrderHeadBaseMgr(IClientOrderHeadDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateClientOrderHead(ClientOrderHead entity)
        {
            entityDao.CreateClientOrderHead(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ClientOrderHead LoadClientOrderHead(String id)
        {
            return entityDao.LoadClientOrderHead(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ClientOrderHead> GetAllClientOrderHead()
        {
            return entityDao.GetAllClientOrderHead();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateClientOrderHead(ClientOrderHead entity)
        {
            entityDao.UpdateClientOrderHead(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderHead(String id)
        {
            entityDao.DeleteClientOrderHead(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderHead(ClientOrderHead entity)
        {
            entityDao.DeleteClientOrderHead(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderHead(IList<String> pkList)
        {
            entityDao.DeleteClientOrderHead(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientOrderHead(IList<ClientOrderHead> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteClientOrderHead(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
