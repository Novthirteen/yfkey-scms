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
    public class WorkingHoursBaseMgr : SessionBase, IWorkingHoursBaseMgr
    {
        protected IWorkingHoursDao entityDao;
        
        public WorkingHoursBaseMgr(IWorkingHoursDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateWorkingHours(WorkingHours entity)
        {
            entityDao.CreateWorkingHours(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual WorkingHours LoadWorkingHours(Int32 id)
        {
            return entityDao.LoadWorkingHours(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<WorkingHours> GetAllWorkingHours()
        {
            return entityDao.GetAllWorkingHours();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateWorkingHours(WorkingHours entity)
        {
            entityDao.UpdateWorkingHours(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkingHours(Int32 id)
        {
            entityDao.DeleteWorkingHours(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkingHours(WorkingHours entity)
        {
            entityDao.DeleteWorkingHours(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkingHours(IList<Int32> pkList)
        {
            entityDao.DeleteWorkingHours(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkingHours(IList<WorkingHours> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteWorkingHours(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
