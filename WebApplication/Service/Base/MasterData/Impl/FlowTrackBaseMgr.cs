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
    public class FlowTrackBaseMgr : SessionBase, IFlowTrackBaseMgr
    {
        protected IFlowTrackDao entityDao;
        
        public FlowTrackBaseMgr(IFlowTrackDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlowTrack(FlowTrack entity)
        {
            entityDao.CreateFlowTrack(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlowTrack LoadFlowTrack(Int32 id)
        {
            return entityDao.LoadFlowTrack(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowTrack> GetAllFlowTrack()
        {
            return entityDao.GetAllFlowTrack(false);
        }
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowTrack> GetAllFlowTrack(bool includeInactive)
        {
            return entityDao.GetAllFlowTrack(includeInactive);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlowTrack(FlowTrack entity)
        {
            entityDao.UpdateFlowTrack(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowTrack(Int32 id)
        {
            entityDao.DeleteFlowTrack(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowTrack(FlowTrack entity)
        {
            entityDao.DeleteFlowTrack(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowTrack(IList<Int32> pkList)
        {
            entityDao.DeleteFlowTrack(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowTrack(IList<FlowTrack> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlowTrack(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
