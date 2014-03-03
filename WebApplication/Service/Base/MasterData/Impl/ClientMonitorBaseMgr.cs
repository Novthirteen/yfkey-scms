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
    public class ClientMonitorBaseMgr : SessionBase, IClientMonitorBaseMgr
    {
        protected IClientMonitorDao entityDao;
        
        public ClientMonitorBaseMgr(IClientMonitorDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateClientMonitor(ClientMonitor entity)
        {
            entityDao.CreateClientMonitor(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ClientMonitor LoadClientMonitor(Int32 id)
        {
            return entityDao.LoadClientMonitor(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ClientMonitor> GetAllClientMonitor()
        {
            return entityDao.GetAllClientMonitor();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateClientMonitor(ClientMonitor entity)
        {
            entityDao.UpdateClientMonitor(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientMonitor(Int32 id)
        {
            entityDao.DeleteClientMonitor(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientMonitor(ClientMonitor entity)
        {
            entityDao.DeleteClientMonitor(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientMonitor(IList<Int32> pkList)
        {
            entityDao.DeleteClientMonitor(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteClientMonitor(IList<ClientMonitor> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteClientMonitor(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
