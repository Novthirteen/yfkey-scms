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
    public class WorkCenterBaseMgr : SessionBase, IWorkCenterBaseMgr
    {
        protected IWorkCenterDao entityDao;
        
        public WorkCenterBaseMgr(IWorkCenterDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateWorkCenter(WorkCenter entity)
        {
            entityDao.CreateWorkCenter(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual WorkCenter LoadWorkCenter(String code)
        {
            return entityDao.LoadWorkCenter(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<WorkCenter> GetAllWorkCenter()
        {
            return entityDao.GetAllWorkCenter(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<WorkCenter> GetAllWorkCenter(bool includeInactive)
        {
            return entityDao.GetAllWorkCenter(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateWorkCenter(WorkCenter entity)
        {
            entityDao.UpdateWorkCenter(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkCenter(String code)
        {
            entityDao.DeleteWorkCenter(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkCenter(WorkCenter entity)
        {
            entityDao.DeleteWorkCenter(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkCenter(IList<String> pkList)
        {
            entityDao.DeleteWorkCenter(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkCenter(IList<WorkCenter> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteWorkCenter(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
