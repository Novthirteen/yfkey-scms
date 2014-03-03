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
    public class CustomerRollingPlanDetailBaseMgr : SessionBase, ICustomerRollingPlanDetailBaseMgr
    {
        protected ICustomerRollingPlanDetailDao entityDao;
        
        public CustomerRollingPlanDetailBaseMgr(ICustomerRollingPlanDetailDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCustomerRollingPlanDetail(CustomerRollingPlanDetail entity)
        {
            entityDao.CreateCustomerRollingPlanDetail(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CustomerRollingPlanDetail LoadCustomerRollingPlanDetail(Int32 id)
        {
            return entityDao.LoadCustomerRollingPlanDetail(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CustomerRollingPlanDetail> GetAllCustomerRollingPlanDetail()
        {
            return entityDao.GetAllCustomerRollingPlanDetail();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCustomerRollingPlanDetail(CustomerRollingPlanDetail entity)
        {
            entityDao.UpdateCustomerRollingPlanDetail(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlanDetail(Int32 id)
        {
            entityDao.DeleteCustomerRollingPlanDetail(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlanDetail(CustomerRollingPlanDetail entity)
        {
            entityDao.DeleteCustomerRollingPlanDetail(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlanDetail(IList<Int32> pkList)
        {
            entityDao.DeleteCustomerRollingPlanDetail(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlanDetail(IList<CustomerRollingPlanDetail> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCustomerRollingPlanDetail(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
