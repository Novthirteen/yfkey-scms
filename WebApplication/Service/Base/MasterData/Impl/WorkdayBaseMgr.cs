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
    public class WorkdayBaseMgr : SessionBase, IWorkdayBaseMgr
    {
        protected IWorkdayDao entityDao;
        
        public WorkdayBaseMgr(IWorkdayDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateWorkday(Workday entity)
        {
            entityDao.CreateWorkday(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Workday LoadWorkday(Int32 id)
        {
            return entityDao.LoadWorkday(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Workday> GetAllWorkday()
        {
            return entityDao.GetAllWorkday();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateWorkday(Workday entity)
        {
            entityDao.UpdateWorkday(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkday(Int32 id)
        {
            entityDao.DeleteWorkday(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkday(Workday entity)
        {
            entityDao.DeleteWorkday(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkday(IList<Int32> pkList)
        {
            entityDao.DeleteWorkday(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkday(IList<Workday> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteWorkday(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
