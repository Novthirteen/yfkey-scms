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
    public class WorkdayShiftBaseMgr : SessionBase, IWorkdayShiftBaseMgr
    {
        protected IWorkdayShiftDao entityDao;
        
        public WorkdayShiftBaseMgr(IWorkdayShiftDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateWorkdayShift(WorkdayShift entity)
        {
            entityDao.CreateWorkdayShift(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual WorkdayShift LoadWorkdayShift(Int32 id)
        {
            return entityDao.LoadWorkdayShift(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<WorkdayShift> GetAllWorkdayShift()
        {
            return entityDao.GetAllWorkdayShift();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateWorkdayShift(WorkdayShift entity)
        {
            entityDao.UpdateWorkdayShift(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkdayShift(Int32 id)
        {
            entityDao.DeleteWorkdayShift(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkdayShift(WorkdayShift entity)
        {
            entityDao.DeleteWorkdayShift(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkdayShift(IList<Int32> pkList)
        {
            entityDao.DeleteWorkdayShift(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteWorkdayShift(IList<WorkdayShift> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteWorkdayShift(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
