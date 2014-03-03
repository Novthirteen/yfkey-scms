using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity.View;
using com.Sconit.Persistence.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View.Impl
{
    [Transactional]
    public class PlannedBillViewBaseMgr : SessionBase, IPlannedBillViewBaseMgr
    {
        protected IPlannedBillViewDao entityDao;
        
        public PlannedBillViewBaseMgr(IPlannedBillViewDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePlannedBillView(PlannedBillView entity)
        {
            entityDao.CreatePlannedBillView(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PlannedBillView LoadPlannedBillView(Int32 id)
        {
            return entityDao.LoadPlannedBillView(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PlannedBillView> GetAllPlannedBillView()
        {
            return entityDao.GetAllPlannedBillView();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePlannedBillView(PlannedBillView entity)
        {
            entityDao.UpdatePlannedBillView(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBillView(Int32 id)
        {
            entityDao.DeletePlannedBillView(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBillView(PlannedBillView entity)
        {
            entityDao.DeletePlannedBillView(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBillView(IList<Int32> pkList)
        {
            entityDao.DeletePlannedBillView(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBillView(IList<PlannedBillView> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePlannedBillView(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
