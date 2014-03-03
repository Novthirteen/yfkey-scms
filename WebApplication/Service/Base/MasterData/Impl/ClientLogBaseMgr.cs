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
    public class ClientLogBaseMgr : SessionBase, IClientLogBaseMgr
    {
        protected IClientLogDao entityDao;
        
        public ClientLogBaseMgr(IClientLogDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateClientLog(ClientLog entity)
        {
            entityDao.CreateClientLog(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ClientLog LoadClientLog(Int32 id)
        {
            return entityDao.LoadClientLog(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ClientLog> GetAllClientLog()
        {
            return entityDao.GetAllClientLog();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateClientLog(ClientLog entity)
        {
            entityDao.UpdateClientLog(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientLog(Int32 id)
        {
            entityDao.DeleteClientLog(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientLog(ClientLog entity)
        {
            entityDao.DeleteClientLog(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientLog(IList<Int32> pkList)
        {
            entityDao.DeleteClientLog(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientLog(IList<ClientLog> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteClientLog(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
