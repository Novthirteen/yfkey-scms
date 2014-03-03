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
    public class PickListResultBaseMgr : SessionBase, IPickListResultBaseMgr
    {
        protected IPickListResultDao entityDao;
        
        public PickListResultBaseMgr(IPickListResultDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePickListResult(PickListResult entity)
        {
            entityDao.CreatePickListResult(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PickListResult LoadPickListResult(Int32 id)
        {
            return entityDao.LoadPickListResult(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PickListResult> GetAllPickListResult()
        {
            return entityDao.GetAllPickListResult();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePickListResult(PickListResult entity)
        {
            entityDao.UpdatePickListResult(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListResult(Int32 id)
        {
            entityDao.DeletePickListResult(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListResult(PickListResult entity)
        {
            entityDao.DeletePickListResult(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListResult(IList<Int32> pkList)
        {
            entityDao.DeletePickListResult(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListResult(IList<PickListResult> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePickListResult(entityList);
        }   
        
        [Transaction(TransactionMode.Unspecified)]
        public virtual PickListResult LoadPickListResult(com.Sconit.Entity.MasterData.PickListDetail pickListDetail, com.Sconit.Entity.MasterData.LocationLotDetail locationLotDetail)
        {
            return entityDao.LoadPickListResult(pickListDetail, locationLotDetail);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePickListResult(Int32 pickListDetailId, Int32 locationLotDetailId)
        {
            entityDao.DeletePickListResult(pickListDetailId, locationLotDetailId);
        }   
    
        [Transaction(TransactionMode.Unspecified)]
        public virtual PickListResult LoadPickListResult(Int32 pickListDetailId, Int32 locationLotDetailId)
        {
            return entityDao.LoadPickListResult(pickListDetailId, locationLotDetailId);
        }
        #endregion Method Created By CodeSmith
    }
}
