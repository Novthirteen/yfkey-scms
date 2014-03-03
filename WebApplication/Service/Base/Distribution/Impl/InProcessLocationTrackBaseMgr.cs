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
    public class InProcessLocationTrackBaseMgr : SessionBase, IInProcessLocationTrackBaseMgr
    {
        protected IInProcessLocationTrackDao entityDao;
        
        public InProcessLocationTrackBaseMgr(IInProcessLocationTrackDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateInProcessLocationTrack(InProcessLocationTrack entity)
        {
            entityDao.CreateInProcessLocationTrack(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual InProcessLocationTrack LoadInProcessLocationTrack(Int32 id)
        {
            return entityDao.LoadInProcessLocationTrack(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<InProcessLocationTrack> GetAllInProcessLocationTrack()
        {
            return entityDao.GetAllInProcessLocationTrack();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateInProcessLocationTrack(InProcessLocationTrack entity)
        {
            entityDao.UpdateInProcessLocationTrack(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationTrack(Int32 id)
        {
            entityDao.DeleteInProcessLocationTrack(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationTrack(InProcessLocationTrack entity)
        {
            entityDao.DeleteInProcessLocationTrack(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationTrack(IList<Int32> pkList)
        {
            entityDao.DeleteInProcessLocationTrack(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteInProcessLocationTrack(IList<InProcessLocationTrack> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteInProcessLocationTrack(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
