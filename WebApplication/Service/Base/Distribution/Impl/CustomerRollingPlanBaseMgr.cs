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
    public class CustomerRollingPlanBaseMgr : SessionBase, ICustomerRollingPlanBaseMgr
    {
        protected ICustomerRollingPlanDao entityDao;
        
        public CustomerRollingPlanBaseMgr(ICustomerRollingPlanDao entityDao)
        {
            this.entityDao = entityDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public virtual void CreateCustomerRollingPlan(CustomerRollingPlan entity)
        {
            entityDao.CreateCustomerRollingPlan(entity);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual CustomerRollingPlan LoadCustomerRollingPlan(Int32 id)
        {
            return entityDao.LoadCustomerRollingPlan(id);
        }

        [Transaction(TransactionMode.Unspecified)]
        public virtual IList<CustomerRollingPlan> GetAllCustomerRollingPlan()
        {
            return entityDao.GetAllCustomerRollingPlan();
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateCustomerRollingPlan(CustomerRollingPlan entity)
        {
            entityDao.UpdateCustomerRollingPlan(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlan(Int32 id)
        {
            entityDao.DeleteCustomerRollingPlan(id);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlan(CustomerRollingPlan entity)
        {
            entityDao.DeleteCustomerRollingPlan(entity);
        }
    
        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlan(IList<Int32> pkList)
        {
            entityDao.DeleteCustomerRollingPlan(pkList);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void DeleteCustomerRollingPlan(IList<CustomerRollingPlan> entityList)
        {
            if ((entityList == null) || (entityList.Count == 0))
            {
                return;
            }
            
            entityDao.DeleteCustomerRollingPlan(entityList);
        }   
        #endregion Method Created By CodeSmith
    }
}
