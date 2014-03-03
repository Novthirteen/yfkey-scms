using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.MRP;
using com.Sconit.Persistence.MRP;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class MrpReceivePlanBaseMgr : SessionBase, IMrpReceivePlanBaseMgr
    {
        protected IMrpReceivePlanDao entityDao;

        public MrpReceivePlanBaseMgr(IMrpReceivePlanDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMrpReceivePlan(MrpReceivePlan entity)
        {
            entityDao.CreateMrpReceivePlan(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MrpReceivePlan LoadMrpReceivePlan(Int32 id)
        {
            return entityDao.LoadMrpReceivePlan(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MrpReceivePlan> GetAllMrpReceivePlan()
        {
            return entityDao.GetAllMrpReceivePlan();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMrpReceivePlan(MrpReceivePlan entity)
        {
            entityDao.UpdateMrpReceivePlan(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpReceivePlan(Int32 id)
        {
            entityDao.DeleteMrpReceivePlan(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpReceivePlan(MrpReceivePlan entity)
        {
            entityDao.DeleteMrpReceivePlan(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpReceivePlan(IList<Int32> pkList)
        {
            entityDao.DeleteMrpReceivePlan(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpReceivePlan(IList<MrpReceivePlan> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteMrpReceivePlan(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
