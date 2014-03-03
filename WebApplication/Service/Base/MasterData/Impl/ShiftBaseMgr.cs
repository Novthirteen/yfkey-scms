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
    public class ShiftBaseMgr : SessionBase, IShiftBaseMgr
    {
        protected IShiftDao entityDao;
        
        public ShiftBaseMgr(IShiftDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateShift(Shift entity)
        {
            entityDao.CreateShift(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual Shift LoadShift(String code)
        {
            return entityDao.LoadShift(code);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<Shift> GetAllShift()
        {
            return entityDao.GetAllShift();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateShift(Shift entity)
        {
            entityDao.UpdateShift(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShift(String code)
        {
            entityDao.DeleteShift(code);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShift(Shift entity)
        {
            entityDao.DeleteShift(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShift(IList<String> pkList)
        {
            entityDao.DeleteShift(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShift(IList<Shift> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteShift(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
