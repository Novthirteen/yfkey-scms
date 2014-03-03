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
    public class ShiftPlanScheduleBaseMgr : SessionBase, IShiftPlanScheduleBaseMgr
    {
        protected IShiftPlanScheduleDao entityDao;
        
        public ShiftPlanScheduleBaseMgr(IShiftPlanScheduleDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateShiftPlanSchedule(ShiftPlanSchedule entity)
        {
            entityDao.CreateShiftPlanSchedule(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ShiftPlanSchedule LoadShiftPlanSchedule(Int32 id)
        {
            return entityDao.LoadShiftPlanSchedule(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ShiftPlanSchedule> GetAllShiftPlanSchedule()
        {
            return entityDao.GetAllShiftPlanSchedule();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateShiftPlanSchedule(ShiftPlanSchedule entity)
        {
            entityDao.UpdateShiftPlanSchedule(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftPlanSchedule(Int32 id)
        {
            entityDao.DeleteShiftPlanSchedule(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftPlanSchedule(ShiftPlanSchedule entity)
        {
            entityDao.DeleteShiftPlanSchedule(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftPlanSchedule(IList<Int32> pkList)
        {
            entityDao.DeleteShiftPlanSchedule(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftPlanSchedule(IList<ShiftPlanSchedule> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteShiftPlanSchedule(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
