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
    public class ShiftDetailBaseMgr : SessionBase, IShiftDetailBaseMgr
    {
        protected IShiftDetailDao entityDao;
        
        public ShiftDetailBaseMgr(IShiftDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateShiftDetail(ShiftDetail entity)
        {
            entityDao.CreateShiftDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual ShiftDetail LoadShiftDetail(Int32 id)
        {
            return entityDao.LoadShiftDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<ShiftDetail> GetAllShiftDetail()
        {
            return entityDao.GetAllShiftDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateShiftDetail(ShiftDetail entity)
        {
            entityDao.UpdateShiftDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftDetail(Int32 id)
        {
            entityDao.DeleteShiftDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftDetail(ShiftDetail entity)
        {
            entityDao.DeleteShiftDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftDetail(IList<Int32> pkList)
        {
            entityDao.DeleteShiftDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteShiftDetail(IList<ShiftDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteShiftDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
