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
    public class FlowDetailTrackBaseMgr : SessionBase, IFlowDetailTrackBaseMgr
    {
        protected IFlowDetailTrackDao entityDao;
        
        public FlowDetailTrackBaseMgr(IFlowDetailTrackDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateFlowDetailTrack(FlowDetailTrack entity)
        {
            entityDao.CreateFlowDetailTrack(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual FlowDetailTrack LoadFlowDetailTrack(Int32 id)
        {
            return entityDao.LoadFlowDetailTrack(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<FlowDetailTrack> GetAllFlowDetailTrack()
        {
            return entityDao.GetAllFlowDetailTrack();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateFlowDetailTrack(FlowDetailTrack entity)
        {
            entityDao.UpdateFlowDetailTrack(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetailTrack(Int32 id)
        {
            entityDao.DeleteFlowDetailTrack(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetailTrack(FlowDetailTrack entity)
        {
            entityDao.DeleteFlowDetailTrack(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetailTrack(IList<Int32> pkList)
        {
            entityDao.DeleteFlowDetailTrack(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteFlowDetailTrack(IList<FlowDetailTrack> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteFlowDetailTrack(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
