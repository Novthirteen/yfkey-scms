using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class AutoOrderTrackBaseMgr : SessionBase, IAutoOrderTrackBaseMgr
    {
        protected IAutoOrderTrackDao entityDao;
        
        public AutoOrderTrackBaseMgr(IAutoOrderTrackDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateAutoOrderTrack(AutoOrderTrack entity)
        {
            entityDao.CreateAutoOrderTrack(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual AutoOrderTrack LoadAutoOrderTrack(Int32 id)
        {
            return entityDao.LoadAutoOrderTrack(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<AutoOrderTrack> GetAllAutoOrderTrack()
        {
            return entityDao.GetAllAutoOrderTrack();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateAutoOrderTrack(AutoOrderTrack entity)
        {
            entityDao.UpdateAutoOrderTrack(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAutoOrderTrack(Int32 id)
        {
            entityDao.DeleteAutoOrderTrack(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAutoOrderTrack(AutoOrderTrack entity)
        {
            entityDao.DeleteAutoOrderTrack(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAutoOrderTrack(IList<Int32> pkList)
        {
            entityDao.DeleteAutoOrderTrack(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteAutoOrderTrack(IList<AutoOrderTrack> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteAutoOrderTrack(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
