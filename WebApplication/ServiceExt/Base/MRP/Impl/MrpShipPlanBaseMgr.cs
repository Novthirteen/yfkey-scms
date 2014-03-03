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
    public class MrpShipPlanBaseMgr : SessionBase, IMrpShipPlanBaseMgr
    {
        protected IMrpShipPlanDao entityDao;

        public MrpShipPlanBaseMgr(IMrpShipPlanDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateMrpShipPlan(MrpShipPlan entity)
        {
            entityDao.CreateMrpShipPlan(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual MrpShipPlan LoadMrpShipPlan(Int32 id)
        {
            return entityDao.LoadMrpShipPlan(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<MrpShipPlan> GetAllMrpShipPlan()
        {
            return entityDao.GetAllMrpShipPlan();
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateMrpShipPlan(MrpShipPlan entity)
        {
            entityDao.UpdateMrpShipPlan(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlan(Int32 id)
        {
            entityDao.DeleteMrpShipPlan(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlan(MrpShipPlan entity)
        {
            entityDao.DeleteMrpShipPlan(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlan(IList<Int32> pkList)
        {
            entityDao.DeleteMrpShipPlan(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteMrpShipPlan(IList<MrpShipPlan> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }

            entityDao.DeleteMrpShipPlan(entityList);
        }
        #endregion Method Created By CodeSmith
    }
}
