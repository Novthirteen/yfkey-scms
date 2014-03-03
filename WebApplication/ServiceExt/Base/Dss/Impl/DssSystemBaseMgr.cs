using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Dss;
using com.Sconit.Persistence.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssSystemBaseMgr : SessionBase, IDssSystemBaseMgr
    {
        protected IDssSystemDao entityDao;
        
        public DssSystemBaseMgr(IDssSystemDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateDssSystem(DssSystem entity)
        {
            entityDao.CreateDssSystem(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual DssSystem LoadDssSystem(String code)
        {
            return entityDao.LoadDssSystem(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<DssSystem> GetAllDssSystem()
        {
            return entityDao.GetAllDssSystem();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateDssSystem(DssSystem entity)
        {
            entityDao.UpdateDssSystem(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssSystem(String code)
        {
            entityDao.DeleteDssSystem(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssSystem(DssSystem entity)
        {
            entityDao.DeleteDssSystem(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssSystem(IList<String> pkList)
        {
            entityDao.DeleteDssSystem(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteDssSystem(IList<DssSystem> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteDssSystem(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
