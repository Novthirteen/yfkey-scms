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
    public class PlannedBillBaseMgr : SessionBase, IPlannedBillBaseMgr
    {
        protected IPlannedBillDao entityDao;
        
        public PlannedBillBaseMgr(IPlannedBillDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreatePlannedBill(PlannedBill entity)
        {
            entityDao.CreatePlannedBill(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual PlannedBill LoadPlannedBill(Int32 id)
        {
            return entityDao.LoadPlannedBill(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<PlannedBill> GetAllPlannedBill()
        {
            return entityDao.GetAllPlannedBill();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdatePlannedBill(PlannedBill entity)
        {
            entityDao.UpdatePlannedBill(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBill(Int32 id)
        {
            entityDao.DeletePlannedBill(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBill(PlannedBill entity)
        {
            entityDao.DeletePlannedBill(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBill(IList<Int32> pkList)
        {
            entityDao.DeletePlannedBill(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeletePlannedBill(IList<PlannedBill> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeletePlannedBill(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
