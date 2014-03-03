using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Distribution;
using com.Sconit.Persistence.Distribution;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution.Impl
{
    [Transactional]
    public class InProcessLocationBaseMgr : SessionBase, IInProcessLocationBaseMgr
    {
        protected IInProcessLocationDao entityDao;
        
        public InProcessLocationBaseMgr(IInProcessLocationDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInProcessLocation(InProcessLocation entity)
        {
            entityDao.CreateInProcessLocation(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocation LoadInProcessLocation(String ipNo)
        {
            return entityDao.LoadInProcessLocation(ipNo);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InProcessLocation> GetAllInProcessLocation()
        {
            return entityDao.GetAllInProcessLocation();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInProcessLocation(InProcessLocation entity)
        {
            entityDao.UpdateInProcessLocation(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocation(String ipNo)
        {
            entityDao.DeleteInProcessLocation(ipNo);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocation(InProcessLocation entity)
        {
            entityDao.DeleteInProcessLocation(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocation(IList<String> pkList)
        {
            entityDao.DeleteInProcessLocation(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocation(IList<InProcessLocation> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInProcessLocation(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
